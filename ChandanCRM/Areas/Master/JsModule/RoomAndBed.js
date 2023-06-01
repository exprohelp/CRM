$(document).ready(function () {
	CloseSidebar();
	$('select').select2();
	$('#ddlRoomForBed').append($('<option>Select Room</option>')).select2();
	//Create Room
	searchTable('txtSearch', 'tblRoom')
	$('#ddlFloor').on('change', function () {
		$('#txtPackType').val($(this).data('pack_type'));
		$('#txtPackQty').val($(this).data('pack_quantity'));
		$('span[data-packid]').text($(this).data('autoid'));
		$('#btnSavePackType').val('Update').addClass('btn-warning');
	});
	$('#tblRoom tbody').on('click', 'button', function () {
		if (confirm('Are you sure want to delete this record?'))
			$(this).closest('tr').remove();
	});
	$('#tblRoom tbody').on('keyup', 'input', function () {
		roomId = $(this).val().toUpperCase();
		$(this).closest('tr').find('span.roomid').text(roomId);
	});

	//Create Bed
	$('#ddlFloorForBed').on('change', function () {
		floor = $(this).find('option:selected').text();
		RoomInfoByFloor(floor);
	});
	$('#tblBed tbody').on('click', 'button', function () {
		if (confirm('Are you sure want to delete this record?'))
			$(this).closest('tr').remove();
	});

});

//Create Room 
function CreateRoom() {
	if (Validation()) {
		floor = $('#ddlFloor option:selected').text();
		roomType = $('#ddlRoomType option:selected').text();
		billingCategory = $('#ddlBillingCategory option:selected').text();
		roomNo = parseInt($('#txtRoomNo').val());
		tbody = "";
		$('#tblRoom tbody').empty();
		for (var i = 0; i < roomNo; i++) {
			roomId = 'F-' + i;
			tbody += "<tr>";
			tbody += "<td><button class='btn-danger btn-action'><i class='fa fa-trash'></i></button></td>";
			tbody += "<td>" + floor + "</td>";
			tbody += "<td><input type='text' style='width:100%;' class='text-uppercase' placeholder='Room Name' value='" + roomId.toUpperCase() + "'/></td>";
			tbody += "<td>" + roomType + "</td>";
			tbody += "<td>" + billingCategory + "</td>";
			tbody += "<td>" + floor + "/" + roomType + "/<span class='roomid'>" + roomId.toUpperCase() + "</span></td>";
			tbody += "</tr>";
		}
		$('#tblRoom tbody').append(tbody);
	}
}
function InsertRoom() {
	if (ValidateRoomInsert()) {
		var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
		var objBO = [];
		$('#tblRoom tbody tr').each(function () {
			objBO.push({
				'hosp_id': Active.unitId,
				'room_id': '-',
				'room_name': $(this).find('td:eq(2)').find('input[type=text]').val(),
				'room_type': $(this).find('td:eq(3)').text(),
				'floor_name': $(this).find('td:eq(1)').text(),
				'room_full_name': $(this).find('td:eq(5)').text(),
				'billing_category': $('#ddlBillingCategory option:selected').text(),
				'description': '-',
				'login_id': Active.userId,
				'Logic': 'InsertRoom',
			});
		});
		$.ajax({
			method: "POST",
			url: url,
			data: JSON.stringify(objBO),
			dataType: "json",
			contentType: "application/json;charset=utf-8",
			success: function (data) {
				if (data == 'Success') {
					alert(data);
					$('#tblRoom tbody').empty();
					$('input[id=txtRoomNo]').val('');
					$('#ddlFloor,#ddlRoomType,#ddlBillingCategory').prop('selectedIndex', '0').change();
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
function Validation() {
	floor = $('#ddlFloor option:selected').text();
	roomType = $('#ddlRoomType option:selected').text();
	billingCategory = $('#ddlBillingCategory option:selected').text();
	roomNo = $('#txtRoomNo').val();

	if (floor == 'Select Floor') {
		$('span.selection').find('span[aria-labelledby=select2-ddlFloor-container]').css('border-color', 'red').focus();
		alert('Please Select Floor..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlFloor-container]').removeAttr('style');
	}
	if (roomType == 'Select Room Type') {
		$('span.selection').find('span[aria-labelledby=select2-ddlRoomType-container]').css('border-color', 'red').focus();
		alert('Please Select Room Type..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlRoomType-container]').removeAttr('style');
	}
	if (billingCategory == 'Select Billing Category') {
		$('span.selection').find('span[aria-labelledby=select2-ddlBillingCategory-container]').css('border-color', 'red').focus();
		alert('Please Select Billing Category..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlBillingCategory-container]').removeAttr('style');
	}
	if (roomNo == '') {
		$('#txtRoomNo').css('border-color', 'red').focus();
		alert('Please Provide Room Number..');
		return false;
	}
	else {
		$('#txtRoomNo').removeAttr('style');
	}
	return true;
}
//Create Bed
function RoomInfoByFloor(floor) {
	var url = config.baseUrl + "/api/RoomAndBed/RoomMasterQueries";
	var objBO = {};
	objBO.floor_name = floor;
	objBO.Logic = 'RoomInfoByFloor';
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		dataType: "json",
		contentType: "application/json;charset=utf-8",
		success: function (data) {
			$("#ddlRoomForBed").empty().append($('<option>Select Room</option>'));
			$.each(data.ResultSet.Table, function (key, val) {
				$("#ddlRoomForBed").append($("<option data-roomid=" + val.RoomId + "></option>").val(val.RoomId).html(val.roomFullName)).select2();
			});
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function CreateBed() {
	if (ValidationBed()) {
		floor = $('#ddlFloorForBed option:selected').text();
		room = $('#ddlRoomForBed option:selected').text();
		bedNo = parseInt($('#txtBedNo').val());
		tbody = "";
		$('#tblBed tbody').empty();
		for (var i = 0; i < bedNo; i++) {
			bedId = 'F-' + i;
			tbody += "<tr>";
			tbody += "<td><button class='btn-danger btn-action'><i class='fa fa-trash'></i></button></td>";
			tbody += "<td>" + floor + "</td>";
			tbody += "<td>" + room + "</td>";
			tbody += "<td><input type='text' style='width:70%;' class='text-uppercase' value='" + bedId.toUpperCase() + "'/></td>";
			tbody += "</tr>";
		}
		$('#tblBed tbody').append(tbody);
	}
}
function InsertBed() {
	if (ValidateBedInsert()) {
		var url = config.baseUrl + "/api/RoomAndBed/InsertRoomMaster";
		var objBO = [];
		$('#tblBed tbody tr').each(function () {
			objBO.push({
				'hosp_id': Active.unitId,
				'room_id': $('#ddlRoomForBed option:selected').val(),
				'bedName': $(this).find('td:eq(3)').find('input[type=text]').val(),
				'IPDNo': '-',
				'login_id': Active.userId,
				'Logic': 'InsertBed',
			});
		});
		$.ajax({
			method: "POST",
			url: url,
			data: JSON.stringify(objBO),
			dataType: "json",
			contentType: "application/json;charset=utf-8",
			success: function (data) {
				if (data == 'Success') {
					alert(data);
					$('#tblBed tbody').empty();
					$('input[id=txtBedNo]').val('');
					$('#ddlRoomForBed,#ddlFloorForBed').prop('selectedIndex', '0').change();
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
function ValidationBed() {
	floor = $('#ddlFloorForBed option:selected').text();
	room = $('#ddlRoomForBed option:selected').text();
	bedNo = $('#txtBedNo').val();

	if (floor == 'Select Floor') {
		$('span.selection').find('span[aria-labelledby=select2-ddlFloorForBed-container]').css('border-color', 'red').focus();
		alert('Please Select Floor..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlFloorForBed-container]').removeAttr('style');
	}
	if (room == 'Select Room') {
		$('span.selection').find('span[aria-labelledby=select2-ddlRoomForBed-container]').css('border-color', 'red').focus();
		alert('Please Select Room..');
		return false;
	}
	else {
		$('span.selection').find('span[aria-labelledby=select2-ddlRoomForBed-container]').removeAttr('style');
	}
	if (bedNo == '') {
		$('#txtBedNo').css('border-color', 'red').focus();
		alert('Please Provide Bed Number..');
		return false;
	}
	else {
		$('#txtBedNo').removeAttr('style');
	}
	return true;
}
function ValidateBedInsert() {
	var isBed = $('#tblBed tbody tr').length;
	if (isBed <= 0) {
		alert('Oops! You have not Created Beds. First Create Beds..')
		return false;
	}
	return true;
}
function ValidateRoomInsert() {
	var isBed = $('#tblRoom tbody tr').length;
	if (isBed <= 0) {
		alert('Oops! You have not Created Rooms. First Create Rooms..')
		return false;
	}
	return true;
}

