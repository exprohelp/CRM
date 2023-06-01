
$(document).ready(function () {
	GetUnitDivision();
	GetRoleForMenuAllot();
	$('#btnGetEmployee').on('click', function () {
		var empName = $('#txtEmpName').val();
		GetEmpDetails(empName);
	});
	$('ul[id=ddRole]').on('change', 'input[type=checkbox]', function () {
		var isChecked = $(this).is(':checked');
		var roleId = $(this).data('roleid');
		var val = $(this).data('rolename');
		if (isChecked) {
			GetMenuSubMenuByRoleId(roleId, val);
		}
		else {
			$('#roleList li').filter(function () {
				return $(this).data('rolename') == val;
			}).remove();
			$('#Panel' + val).remove();
			$('div[id=' + val).remove();
		}
	});
	//Unit Division Allotment
	$('ul[id=ddDivision]').on('change', 'input[type=checkbox]', function () {
		var isChecked = $(this).is(':checked');
		var division = $(this).data('division');
		if (isChecked) {
			GetUnitByDivision(division);
		}
		else {
			$('#divisionList li').filter(function () {
				return $(this).data('divisionname') == division;
			}).remove();
			$('#Panel' + division.split(" ").join("")).remove();
			$('div[id=' + division.split(" ").join("")).remove();
		}
	});
	$('#divisionList').on('click', 'span[class="clse"]', function () {
		
		var division = $(this).closest('li').data('divisionname');
		$(this).closest('li').remove();
		$('#Panel' + division.split(" ").join("")).remove();
		$('div[id=' + division.split(" ").join("")).remove();
		$('ul[id=ddDivision] input[type=checkbox]').filter(function () {
			return $(this).data('division') == division;
		}).prop('checked', false);
	});
	$('#roleList').on('click', 'span[class="clse"]', function () {
		
		var role = $(this).closest('li').data('rolename');
		$(this).closest('li').remove();
		$('#Panel' + role).remove();
		$('div[id=' + role).remove();
		$('ul[id=ddRole] input[type=checkbox]').filter(function () {
			return $(this).data('rolename') == role;
		}).prop('checked', false);
	});
	$('ul[id=MenuList] li').on('change', 'input[type=checkbox]', function () {

		var isChecked = $(this).is(':checked');
		if (isChecked) {
			$(this).next().find('input[type=checkbox]').prop('checked', true);
		}
		else {
			$(this).next().find('input[type=checkbox]').prop('checked', false);
		}
	});

	$(document).on('change', 'input[id=AllotedMenuChk]', function () {
		
		var isChecked = $(this).is(':checked');
		if (isChecked) {
			$('#AllotedMenuList li input[type=checkbox]').each(function () {
				$(this).prop('checked', true);
			});
		}
		else {
			$('#AllotedMenuList li input[type=checkbox]').each(function () {
				$(this).prop('checked', false);
			});
		}
	});
	$('#btnShowAssignUnit').click(function () {
		GetAllotedUnitDivisionByEmpCode();
		$('#ModalAssignUnit').modal('show');
	});
	$(document).on('change', 'input[type=checkbox]', function () {
		
		var ischecked = $(this).is(':checked');
		var role = $(this).data('role');
		if (role != '') {
			if (ischecked) {
				$('#ul' + role + ' li input[type=checkbox]').each(function () {
					$(this).prop('checked', true);
				});
			}
			else {
				$('#ul' + role + ' li input[type=checkbox]').each(function () {
					$(this).prop('checked', false);
				});
			}
		}
		if (ischecked) {
			
			$(this).closest("ul li").find('input[type=checkbox]').each(function () {
				$(this).prop('checked', true);
			});
		}
		else {
			$(this).closest("ul li").find('input[type=checkbox]').each(function () {
				$(this).prop('checked', false);
			});
		}
	});
	$(document).on('keydown', '#txtSearchUnit', function () {
		
		var val = $(this).val().toLowerCase();
		$('#ddDivision li').filter(function () {
			$(this).toggle($(this).data('unitname').toLowerCase().indexOf(val) > -1);
		});
	});
	$('#ddlEmployee').on('change', function () {
		var val = $('#ddlEmployee option:selected').text();
		$('.EmployeeUnit').empty().text(val);
		GetAllotedRoleByEmpCode();
		$('#btnShowAssignUnit').show();
	});
	$('#btnAllotSubmenu').on('click', function () {
		var Employee = $('#ddlEmployee option:selected').text();
		if (Employee == 'Select Employee') {
			alert('Please Select Employee..');
			$('#ddlEmployee').css('border-color', 'red');
		}
		else {
			AllotSubMenuToEmployee();
		}
	});
	$('#btnUnAllotSubmenu').on('click', function () {
		DeleteAllotedSubMenuByEmpCode();
	});
	$("#btnUnAllotUnit").on('click', function () {
		DeleteAllotedUnitByEmpCode();
	});
	$("#btnAllotUnit").on('click', function () {
		AssignUnitToEmp();
	});
	$('#unitAllotedPanel').on('dblclick', 'ul li ul li', function () {
		var ID = $(this).data('id');
		AllotDefaultUnit(ID);
	});
});
//Get Role Master
function GetRoleMaster() {
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.EmpCode = Active.userId,
		objBO.Logic = 'GetRole'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			$('#dashnav-btn .dashboard-buttons').empty();
			if (data != '') {
				$.each(data.ResultSet.Table, function (key, val) {
					$("<a href ='#' data-roleid=" + val.role_id + " class='btn vertical-button hover-blue-alt role' title =''>" +
						"<span class='glyph-icon icon-separator-vertical pad0A medium'>" +
						"<i class='glyph-icon icon-dashboard opacity-80 font-size-20'></i>" +
						"</span>" +
						val.role_name +
						"</a>").appendTo($('#dashnav-btn .dashboard-buttons'));
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Get Role Master
function GetRoleForMenuAllot() {
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.Logic = 'GetRole'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			if (data != '') {
				$('ul[id=ddRole]').empty();
				$.each(data.ResultSet.Table1, function (key, val) {
					$('<li><div class="checkbox"><label><input type="checkbox" data-rolename="' + val.role_name + '" data-roleid="' + val.role_id + '">' + val.role_name + '</label></div></li>').appendTo($('ul[id=ddRole]'));
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Get Role Master
function GetRoll(RoleId) {
	
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.role_id = RoleId,
		objBO.EmpCode = Active.userId,
		objBO.Logic = 'GetRoleByRoleId'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			$('ul[id=ddRole]').empty();
			if (data != '') {
				$.each(data.ResultSet.Table2, function (key, val) {
					$("<a href ='#' data-roleid=" + val.role_id + " class='btn vertical-button hover-blue-alt role' title =''>" +
						"<span class='glyph-icon icon-separator-vertical pad0A medium'>" +
						"<i class='glyph-icon icon-dashboard opacity-80 font-size-20'></i>" +
						"</span>" +
						val.role_name +
						"</a>").appendTo($('#dashnav-btn .dashboard-buttons'));
					$('<li><div class="checkbox"><label><input type="checkbox" data-rolename="' + val.role_name + '" data-roleid="' + val.role_id + '">' + val.role_name + '</label></div></li>').appendTo($('ul[id=ddRole]'));
				});

			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function GetEmpDetails(empName) {

	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.Prm1 = empName,
		objBO.Logic = 'GetEmployee'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			if (data != '') {
				$('#ddlEmployee').empty().append($('<option>Select Employee</option>'));
				$.each(data.ResultSet.Table, function (key, val) {
					$('#txtEmpName').val('');
					$('#ddlEmployee').append($('<option></option>').val(val.emp_code).html(val.emp_name));
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Allot Menu Section
function GetAllotedSubMenuByEmpCode() {

	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.EmpCode = $('#ddlEmployee option:selected').val(),
		objBO.Logic = 'GetAllotedSubMenuByEmpCode'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			if (data != '') {
				$("#AllotedMenuList").empty().append('<span style="border-bottom: 1px dashed #c7c4c4;"><input type="checkbox" id="AllotedMenuChk" checked /> Select All</span>');
				$.each(data.ResultSet.Table, function (key, val) {
					$('<li  data-menuid=' + val.sub_menu_id + '><label><input type="checkbox" data-submenuid=' + val.sub_menu_id + ' checked />' + val.sub_menu_name + '</label >' +
						'</li >').appendTo($('#AllotedMenuList'));
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function AllotSubMenuToEmployee() {
	
	var url = config.baseUrl + "/api/ApplicationResource/AllotSubMenuToEmployee";
	var objBO = [];
	$("#rolePanel ul input:checkbox:checked").each(function () {
		objBO.push({
			'EmpCode': $('#ddlEmployee option:selected').val(),
			'SubMenuId': $(this).data('submenuid')
		});
	});
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			
			$("#rolePanel input:checkbox").prop("checked", false);
			GetAllotedRoleByEmpCode();
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function DeleteAllotedSubMenuByEmpCode() {
	
	var url = config.baseUrl + "/api/ApplicationResource/InsertDeleteAllotMenu";
	var objBO = {};
	var submenu = $("#roleAllotedPanel ul li input:checkbox:checked").map(function () {
		return $(this).data('submenuid')
	}).get();
	var SubMenuId = JSON.stringify(submenu);
	//var NewSubId = SubMenuId.substring(SubMenuId.indexOf('"[') + 1, SubMenuId.indexOf(']"'));
	var id = SubMenuId.replace(/"/g, '\'');
	var id1 = id.replace(/[\[\]/]+/g, '');
	var id3 = id1.replace(/'/g, '');
	objBO.SubMenuId = id3,
		objBO.EmpCode = $('#ddlEmployee option:selected').val(),
		objBO.Logic = 'DeleteAllotedSubMenuByEmpCode'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			if (data != '') {
				$("#AllotedMenuList input:checkbox").prop("checked", false);
				GetAllotedRoleByEmpCode();
			}

		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Get Menu And Sub Menu Before Allotment
function GetMenuSubMenuByRoleId(roleId, val) {

	var url = config.baseUrl + "/api/ApplicationResource/GetMenuSubmenu";
	var objBO = {};
	objBO.RoleId = roleId,
		objBO.Logic = "GetMenuByRoll",
		$.ajax({
			method: "POST",
			url: url,
			data: JSON.stringify(objBO),
			contentType: 'application/json;charset=utf-8',
			dataType: "JSON",
			success: function (data) {
				if (data != '') {
					$('<li class="btn btn-success" data-rolename="' + val.replace(/\s/g, '') + '">' + val + '<span class="clse">x</span></li>').appendTo($('#roleList'));
					
					var roleName = val;
					CreateMenuPanel(roleName);
					$("#ul" + roleName + "").empty();
					$.each(data, function (key, val) {
						var sm = $.map(val.SubMenu, function (n) {
							var li = '<li data-smid=' + n.MenuId + '><label><input type="checkbox" data-submenuid=' + n.SubMenuId + ' checked />' + n.SubMenuName + '</label></li>';
							list = li.replace(/,/g, "");
							return list;
						});
						$('<li  data-menuid=' + val.MenuId + '><label><input type="checkbox" data-menuid=' + val.MenuId + ' checked />' + val.MenuName + '</label >' +
							'<ul style="list-style:none" id="ulSubMenu' + roleName.replace(/\s/g, '') + '">' + sm.join('') +
							'</ul> </li>').appendTo($('#ul' + roleName.replace(/\s/g, '') + ''));
					});
				}
				else {
					$('ul[id=ddRole] input[type=checkbox]').filter(function () {
						return $(this).data('rolename') == val;
					}).prop('checked', false);
					alert('Selected Roles Menu Not Found.');
				}
			},
			error: function (response) {
				alert('Server Error...!');
			}
		});
}
//Get Menu And Sub Menu After Allotment

function GetAllotedMenuByEmp() {
	$('#roleAllotedPanel').empty();
	var role = $('#AllotedRoleToEmp li');
	$.each(role, function () {
		debugger
		var roleName = $(this).text();
		var roleId = $(this).data('emproleid');
		GetAllotedMenuByEmpCode(roleId, roleName);
	});
}
function GetAllotedMenuByEmpCode(roleId, roleName) {

	var url = config.baseUrl + "/api/ApplicationResource/GetAllotedMenuByEmpCode";
	var objBO = {};
	objBO.RoleId = roleId,
		objBO.EmpCode = $('#ddlEmployee option:selected').val()
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			if (data != '') {
				console.log(data);
				var roleAllotName = roleName;
				CreateAllotedMenuPanel(roleAllotName);
				$("#ulAlloted" + roleAllotName + "").empty();
				$.each(data, function (key, val) {
					
					var sm = $.map(val.SubMenu, function (n) {
						var li = '<li data-smid=' + n.MenuId + '><label><input type="checkbox" data-submenuid=' + n.SubMenuId + ' />' + n.SubMenuName + '</label></li>';
						list = li.replace(/,/g, "");
						return list;
					});
					$('<li  data-menuid=' + val.MenuId + '><label><input type="checkbox" data-menuid=' + val.MenuId + ' />' + val.MenuName + '</label >' +
						'<ul style="list-style:none" id="ulAllotedSubMenu' + roleAllotName.replace(/\s/g, '') + '">' + sm.join('') +
						'</ul> </li>').appendTo($('#ulAlloted' + roleAllotName.replace(/\s/g, '') + ''));
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function GetAllotedRoleByEmpCode() {
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.EmpCode = $('#ddlEmployee option:selected').val(),
		objBO.Logic = 'GetAllotedRoleByEmpCode'
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			if (data != '') {
				
				$("#AllotedRoleToEmp").empty();
				$.each(data.ResultSet.Table, function (key, val) {
					$('<li data-emproleid=' + val.role_id + '>' + val.role_name + '</li>').appendTo($('#AllotedRoleToEmp'));
				});
				GetAllotedMenuByEmp();
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Auto Load Menu by Session RoleId

//Get Unit Details before Assign
function GetUnitByDivision(division) {
	
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.Division = division,
		objBO.Logic = "GetUnitByDivision",
		$.ajax({
			method: "POST",
			url: url,
			data: JSON.stringify(objBO),
			contentType: 'application/json;charset=utf-8',
			dataType: "JSON",
			success: function (data) {
				if (data != '') {
					$('<li class="btn btn-success" data-divisionname="' + division + '">' + division + '<span class="clse">x</span></li>').appendTo($('#divisionList'));
					var divisionName = division;
					CreateDivisionPanel(divisionName);
					$("#ul" + divisionName + "").empty();
					$.each(data.ResultSet.Table, function (key, val) {
						$('<li  data-unitid=' + val.unit_id + '><label><input type="checkbox" data-unitid=' + val.unit_id + ' checked />' + val.unit_name + '</label >' +
							'</li>').appendTo($('#ul' + divisionName.split(" ").join("") + ''));
					});
				}
				else {
					$('ul[id=ddRole] input[type=checkbox]').filter(function () {
						return $(this).data('rolename') == val;
					}).prop('checked', false);
					alert('Selected Division Unit Not Found.');
				}
			},
			error: function (response) {
				alert('Server Error...!');
			}
		});
}
//Get Unit Details After Assign   
function GetAllotedUnitDivisionByEmpCode() {
	
	var url = config.baseUrl + "/api/ApplicationResource/GetUnitDivisionByEmpCode";
	var objBO = {};
	objBO.EmpCode = $('#ddlEmployee option:selected').val();
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: 'application/json;charset=utf-8',
		dataType: "JSON",
		success: function (data) {
			$('#unitAllotedPanel').empty();
			$.each(data, function (key, val) {
				var divisionName = val.Division;
				CreateAllotedDivisionPanel(divisionName);
				$("#ulAllotedDiv" + divisionName + "").empty();
				$.each(val.Unit, function (key, value) {
					$('<li data-ID=' + value.ID + ' data-unitid=' + value.UnitId + '><label data-ID=' + value.ID + ' data-default=' + value.DefaultUnit + '><input type="checkbox" data-unitid=' + value.UnitId + ' />' + value.UnitName + '</label >' +
						'</li>').appendTo($('#ulAllotedDiv' + divisionName.split(" ").join("") + ''));

					$('#unitAllotedPanel ul li ul li').find('label').filter(function () {
						return $(this).data('default') == 'True'
					}).css('color', 'red');
				});
			});

		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Get Unit Details By Employeee Code
function GetUnitDivision() {
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries/";
	var objBO = {};
	objBO.Logic = 'GetUnitDivision';
	$.ajax({
		method: "post",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "JSON",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data != '') {
				$("#ddDivision").empty();
				$.each(data.ResultSet.Table, function (key, val) {
					$('<li data-division="' + val.division + '"><div class="checkbox"><label><input type="checkbox" data-division="' + val.division + '">' + val.division + '</label></div></li>').appendTo($('ul[id=ddDivision]'));
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function GetUnitDetailsByEmpCode() {
	
	var url = config.baseUrl + "/api/ApplicationResource/MenuQueries";
	var objBO = {};
	objBO.EmpCode = Active.userId;
	objBO.Logic = 'GetUnitByEmpCode';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			$('#ddlUnit').empty();
			if (data.ResultSet.Table != null) {
				
				var unit = Active.unitId;
				if ((unit == 'undefined') || (unit == null)) {
					$.each(data.ResultSet.Table1, function (key, val2) {
						
						var defaultUnit = val2.unit_id;
						sessionStorage.removeItem('UnitId');
						sessionStorage.setItem('UnitId', defaultUnit);
						//$('#ddlUnit option').map(function () {
						//    if ($(this).val() == defaultUnit) return this;
						//}).attr('Selected', 'Selected');
					});
				}
				$.each(data.ResultSet.Table, function (key, val) {
					$('#ddlUnit').append($('<option data-unitid="' + val.unit_id + '"></option>').val(val.unit_id).html(val.unit_name));
					$('#ddlUnit option').map(function () {
						if ($(this).val() == Active.unitId) return this;
					}).attr('Selected', 'Selected');
				});
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function AssignUnitToEmp() {
	var url = config.baseUrl + "/api/ApplicationResource/AssignUnitToEmp";
	var objBO = [];
	$("#divisionAllotmentPanel ul li input:checkbox:checked").each(function () {
		objBO.push({
			'UnitId': $(this).data('unitid'),
			'login_id': Active.userId,
			'EmpCode': $('#ddlEmployee option:selected').val()
		});
	});
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data != '') {
				$("#divisionAllotmentPanel ul li input:checkbox:checked").prop('checked', false);
				GetAllotedUnitDivisionByEmpCode();
			}
		},
		error: function (response) {
			alert('Server Error');
		}
	});
}
function DeleteAllotedUnitByEmpCode() {
	
	var url = config.baseUrl + "/api/ApplicationResource/InsertDeleteAllotMenu";
	var objBO = {};
	var UnitId = $("#unitAllotedPanel ul li input:checkbox:checked").map(function () {
		return $(this).data('unitid')
	}).get();
	var UId = JSON.stringify(UnitId);
	var id = UId.replace(/"/g, '\'');
	var id1 = id.replace(/[\[\]/]+/g, '');
	var id3 = id1.replace(/'/g, '');
	objBO.UnitId = id3;
	objBO.EmpCode = $('#ddlEmployee option:selected').val();
	objBO.Logic = 'DeleteAllotedUnitByEmpCode';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data != '') {
				GetAllotedUnitDivisionByEmpCode();
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function AllotDefaultUnit(ID) {
	var url = config.baseUrl + "/api/ApplicationResource/InsertDeleteAllotMenu";
	var objBO = {};
	objBO.ID = ID;
	objBO.EmpCode = $('#ddlEmployee option:selected').val();
	objBO.Logic = 'AllotDefaultUnit';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data == 'Success') {
				$('#unitAllotedPanel ul li ul li').find('label').filter(function () {
					$(this).removeAttr('style');
					return $(this).data('id') == ID
				}).css('color', 'red');
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
//Dynamic Panel
function CreateDivisionPanel(divisionName) {
	$('<div class="panel-heading mt-5" id="Panel' + divisionName.split(" ").join("") + '">' +
		'<h4 class="panel-title">' +
		'<a data-toggle="collapse" data-parent="#accordion" href="#' + divisionName.split(" ").join("") + '" aria-expanded="false" class="collapsed">' +
		divisionName +
		'</a></h4></div>' +
		'<div id="' + divisionName.split(" ").join("") + '" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">' +
		'<div class="panel-body">' +
		'<ul><li><label><input type="checkbox" checked />Select All</label><ul id="ul' + divisionName.split(" ").join("") + '"></ul></li></ul>' +
		'</div></div>').appendTo($('#divisionAllotmentPanel'));
}
function CreateAllotedDivisionPanel(divisionName) {
	$('<div class="panel-heading mt-5" id="Panel' + divisionName.split(" ").join("") + '">' +
		'<h4 class="panel-title">' +
		'<a data-toggle="collapse" data-parent="#accordion" href="#AllotUnit' + divisionName.split(" ").join("") + '" aria-expanded="false" class="collapsed">' +
		divisionName +
		'</a></h4></div>' +
		'<div id="AllotUnit' + divisionName.split(" ").join("") + '" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">' +
		'<div class="panel-body">' +
		'<ul><li><label><input type="checkbox" />Select All</label><ul id="ulAllotedDiv' + divisionName.split(" ").join("") + '"></ul></li></ul>' +
		'</div></div>').appendTo($('#unitAllotedPanel'));
}
function CreateMenuPanel(roleName) {
	$('<div class="panel-heading mt-5" id="Panel' + roleName.replace(/\s/g, '') + '">' +
		'<h4 class="panel-title">' + '<label class=""><input type="checkbox" checked class="parent" data-role="' + roleName + '"/></label>&nbsp;' +
		'<a data-toggle="collapse" data-parent="#accordion" href="#' + roleName.replace(/\s/g, '') + '" aria-expanded="false" class="collapsed">' +
		roleName +
		'</a></h4></div>' +
		'<div id="' + roleName.replace(/\s/g, '') + '" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">' +
		'<div class="panel-body">' +
		//'<label><input type="checkbox" />Select All</label>' +
		'<ul id="ul' + roleName.replace(/\s/g, '') + '"></ul>' +
		'</div></div>').appendTo($('#rolePanel'));
}
function CreateAllotedMenuPanel(roleAllotName) {
	$('<div class="panel-heading mt-5">' +
		'<h4 class="panel-title">' +
		'<a data-toggle="collapse" data-parent="#accordion" href="#Allot' + roleAllotName.replace(/\s/g, '') + '" aria-expanded="false" class="collapsed">' +
		roleAllotName +
		'</a></h4></div>' +
		'<div id="Allot' + roleAllotName.replace(/\s/g, '') + '" class="panel-collapse collapse" aria-expanded="false" style="height: 0px;">' +
		'<div class="panel-body">' +
		'<ul id="ulAlloted' + roleAllotName.replace(/\s/g, '') + '"></ul>' +
		'</div></div>').appendTo($('#roleAllotedPanel'));
}







