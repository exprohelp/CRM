
$(document).ready(function () {
	GetTemplateMaster();
	GetTemplateInfo();
	searchTable('txtSearch', 'tblTemplateInfo');

	$('#tblTemplateInfo tbody').on('click', '.switch', function () {
		selectRow($(this));
		isCheck = $(this).find('input[type=checkbox]').is(':checked');
		var TemplateId = $(this).find('input[type=checkbox]').data('templateid');
		var ItemId = $(this).find('input[type=checkbox]').data('itemid');
		var val = $(this).find('input[type=checkbox]').data('isactive');
		if (isCheck) {
			if (val == 'Active') {				
				UpdateStatus(TemplateId, ItemId, 0);
			}
			else {				
				UpdateStatus(TemplateId, ItemId, 1);
			}
		}
	});
	$('#btnSave').on('click',function () {		
		var val = $(this).val();
		if (val == 'Submit') {
			InsertTemplateInfo();
		}
		else {
			UpdateTemplateInfo();
		}
	});
	$('#tblTemplateInfo tbody').on('click', 'button', function () {	
		var TemplateId = $(this).data('templateid');
		var ItemId = $(this).data('itemid');
		var ItemName = $(this).closest('tr').find('td:eq(3)').text();
		$('#txtItemName').val(ItemName);
		$('#btnSave').val('Update').switchClass('btn-success', 'btn btn-warning');		
		$('#hiddenItemId').text(ItemId);		
		$('#ddlTemplate option').each(function () {		
			if($(this).val()== TemplateId) {
				$('#ddlTemplate').prop('selectedIndex', '' + $(this).index() + '').change();
				$('#ddlTemplate').prop('disabled',true);
			}
		});		
	});
});

function GetTemplateMaster() {
	var url = config.baseUrl + "/api/master/CPOE_MasterQueries";
	var objBO = {};
	objBO.Logic = 'GetTemplateMaster';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			$('#ddlTemplate').empty().append($('<option></option>').val(00).html('Select Template'));
			$.each(data.ResultSet.Table, function (key, val) {
				$('#ddlTemplate').append($('<option></option>').val(val.TemplateId).html(val.TemplateName)).select2();
			});
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function GetTemplateInfo() {
	var url = config.baseUrl + "/api/master/CPOE_MasterQueries";
	var objBO = {};
	objBO.Logic = 'GetTemplateInfo';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data != '') {
				$('#tblTemplateInfo tbody').empty();
				var tbody = "";
				var temp = "";
				$.each(data.ResultSet.Table, function (key, val) {
					if (temp != val.TemplateName) {
						tbody += "<tr style='background:#b5dfff'>";						
						tbody += "<td colspan='6'>" + val.TemplateName + "</td>";
						tbody += "</tr>";	
						temp = val.TemplateName;
					}
					tbody += "<tr>";
					tbody += "<td class='text-center'><button data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='btn-flat btn-success'>Edit</button></td>";
					tbody += "<td>" + val.TemplateType + "</td>";
					tbody += "<td>" + val.TemplateName + "</td>";
					tbody += "<td>" + val.ItemName + "</td>";
					tbody += "<td>" + val.cr_date + "</td>";
					tbody += "<td>" +
						'<label class="switch">' +
						'<input type="checkbox" data-templateid=' + val.TemplateId + ' data-itemid=' + val.ItemId + ' data-isactive=' + val.IsActive + ' class="IsActive" id="chkActive" ' + val.checked + '>' +
						'<span class="slider round"></span>' +
						'</label>' +
						"</td>";
					tbody += "</tr>";
				});
				$('#tblTemplateInfo tbody').append(tbody);
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function TemplateInfoByTemplateId() {
	if ($('#ddlTemplate option:selected').text() == 'Select Template') {
		GetTemplateInfo();
		return
	}
	var url = config.baseUrl + "/api/master/CPOE_MasterQueries";
	var objBO = {};
	objBO.TemplateId = $('#ddlTemplate option:selected').val();
	objBO.Logic = 'TemplateInfoByTemplateId';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			if (data != '') {
				$('#tblTemplateInfo tbody').empty();
				var tbody = "";
				var temp = "";
				$.each(data.ResultSet.Table, function (key, val) {
					tbody += "<tr>";
					tbody += "<td class='text-center'><button data-templateid=" + val.TemplateId + " data-itemid=" + val.ItemId + " class='btn-flat btn-success'>Edit</button></td>";
					tbody += "<td>" + val.TemplateType + "</td>";
					tbody += "<td>" + val.TemplateName + "</td>";
					tbody += "<td>" + val.ItemName + "</td>";
					tbody += "<td>" + val.cr_date + "</td>";
					tbody += "<td>" +
						'<label class="switch">' +
						'<input type="checkbox" data-templateid=' + val.TemplateId + ' data-itemid=' + val.ItemId + ' data-isactive=' + val.IsActive + ' class="IsActive" id="chkActive" ' + val.checked + '>' +
						'<span class="slider round"></span>' +
						'</label>' +
						"</td>";
					tbody += "</tr>";
				});
				$('#tblTemplateInfo tbody').append(tbody);
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function InsertTemplateInfo() {
	if (Validate()) {
		var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
		var objBO = {};
		objBO.TemplateType = 'Hospital';
		objBO.DoctorId = Active.doctorId;
		objBO.TemplateId = $('#ddlTemplate option:selected').val();
		objBO.ItemId = '';
		objBO.ItemName = $('#txtItemName').val();
		objBO.login_id = Active.userId;
		objBO.Logic = 'InsertTemplateInfo';
		$.ajax({
			method: "POST",
			url: url,
			data: JSON.stringify(objBO),
			dataType: "json",
			contentType: "application/json;charset=utf-8",
			success: function (data) {
				if (data == 'success') {
					Clear();
					alert(data);
					GetTemplateInfo();
				}
				else {
					alert(data);
				};
			},
			error: function (response) {
				alert('Server Error...!');
			}
		});
	}
}
function UpdateTemplateInfo() {
	if (Validate()) {
		var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
		var objBO = {};
		objBO.TemplateType = 'Hospital';
		objBO.DoctorId = Active.doctorId;
		objBO.TemplateId = $('#ddlTemplate option:selected').val();
		objBO.ItemId = $('#hiddenItemId').text();
		objBO.ItemName = $('#txtItemName').val();
		objBO.login_id = Active.userId;
		objBO.Logic = 'UpdateTemplateInfo';
		$.ajax({
			method: "POST",
			url: url,
			data: JSON.stringify(objBO),
			dataType: "json",
			contentType: "application/json;charset=utf-8",
			success: function (data) {
				if (data == 'success') {
					Clear();
					alert(data);
					GetTemplateInfo();
				}
				else {
					alert(data);
				};
			},
			error: function (response) {
				alert('Server Error...!');
			}
		});
	}
}
function UpdateStatus(TemplateId, ItemId, IsActive) {
	var url = config.baseUrl + "/api/master/CPOE_InsertUpdateMaster";
	var objBO = {};
	objBO.DoctorId = Active.doctorId;
	objBO.TemplateId = TemplateId;
	objBO.ItemId = ItemId;
	objBO.ItemName = '';
	objBO.login_id = Active.userId;
	objBO.IsActive = IsActive;
	objBO.Logic = 'UpdateStatus';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			GetTemplateInfo();
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}

//Validation
function Validate() {
	var name = $('#txtItemName').val();
	var Template = $('#ddlTemplate option:selected').text();

	if (Template == 'Select Template') {
		$('span.selection').find('span[aria-labelledby=select2-ddlTemplate-container]').css('border-color', 'red').focus();
		alert('Please Select Template..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlTemplate-container]').removeAttr('style').focus();
	}
	if (name == '') {
		$('#txtItemName').css('border-color', 'red').focus();
		alert('Please Provide Item Name..');
		return false;
	}
	else {
		$('#txtItemName').removeAttr('style').focus();
	}
	return true;
}
function Clear() {
	$('input[type=text]').val('');
	$('select').prop('selectedIndex', '0').change();
	//$('#btnSaveCategory').val('Submit').removeClass('btn-warning').addClass('btn-success');
}





