$(document).ready(function () {	
	$('#btnSearch').on('click', function () {
		var Prm1 = $('#txtSearch').val();
		GetCoupon(Prm1);
	});
});
function GetCoupon(Prm1) {
	var url = config.baseUrl + "/api/OnlineDiagnostic/Online_DiagnosticPackageQueries";
    var objBO = {};
	objBO.prm_1 = Prm1;
	objBO.Logic = "AvailMobileCoupon";
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: "application/json;charset=utf-8",
		dataType: "JSON",
		success: function (data) {
			console.log(data);
			$("#tblCoupon tbody").empty();
			var tbody = "";
			$.each(data.ResultSet.Table, function (key,val) {
				tbody +="<tr style='background:"+val.status+"'>";
				tbody +="<td>" + val.mobileNo + "</td>";
				tbody +="<td>" + val.cust_name + "</td>";
				tbody +="<td>" + val.couponCode + "</td>";
				tbody +="<td><button class='btn-flat btn-warning' onclick='selectRow(this);InsertCoupons()'>Avail</button></td>";
				tbody +="</tr>";		
			});
			$("#tblCoupon tbody").append(tbody);
		},		
		error: function (response) {
			alert('Server Error...!');
		}
	});
}
function InsertCoupons() {
	var url = config.baseUrl + "/api/OnlineDiagnostic/InsertMobileAppCoupons";
	var objBO = {};
	objBO.Mobile = $('.select-row').find('td:eq(0)').text();
	objBO.PatientName = $('.select-row').find('td:eq(1)').text();
	objBO.CouponCode = $('.select-row').find('td:eq(2)').text();
	objBO.Logic = "InsertCoupon";
	$.ajax({
		method: "POST",
		url: url,
		data: JSON.stringify(objBO),
		contentType: "application/json;charset=utf-8",
		dataType: "JSON",
		success: function (data) {
			if (data.includes('Success')) {
				alert('Coupon Availed Successfully..!');
			}
			else {
				alert(data);
			}
		},
		error: function (response) {
			alert('Server Error...!');
		}
	});
}

