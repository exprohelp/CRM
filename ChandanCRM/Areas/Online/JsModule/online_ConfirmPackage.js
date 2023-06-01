var counterP = 90;
$(document).ready(function () {
    $('#btnNewReqList').on('click', function () {
        BookingList('PendingList');
    });
    $('#btnPayedList').on('click', function () {
        BookingList('PaymentDoneList');
    });
    $('#btnPayedList').on('click', function () {
        BookingList('PaymentDoneList');
    });
    $('#btnCancelled').on('click', function () {
        BookingList('CancelledList');
    });
    $('#btnAll').on('click', function () {
        BookingList('AllList');
    });
    FillCurrentDate("txtFrom");
    FillCurrentDate("txtTo");
    FillCurrentDate("dtolddate");
    FillCurrentDate("dtAppDatetime");
    var d = GetPreviousDate();
    document.getElementById("dtAppDatetime").min = d + 'T10:38'
    UnitList();
    AutoRefreshStatus();
    StartCountDown();
});
function StartCountDown() {
    var counter = counterP;
    var interval = setInterval(function () {
        counter--;
        // Display 'counter' wherever you want to display it.
        if (counter <= 0) {
            clearInterval(interval);
            AutoRefreshStatus();
            StartCountDown();
            return;
        } else {
            // $('#txtRefDuration').val(counter);

        }
    }, 1000);
}
function AutoRefreshStatus() {
    var url = config.baseUrl + "/api/OnlineDiagnostic/Online_DiagnosticPackageQueries";
    var from = Properdate($("#txtFrom").val(), '-')
    var to = Properdate($("#txtTo").val(), '-')
    $('#spnNew').html(Loading.small_gear)
    $('#spnPay').html(Loading.small_gear)

    setTimeout(function () {
        var obj = {};
        obj.unit_id = Active.unitId;
        obj.TokenNo = "-";
        obj.prm_1 = "-";
        obj.login_id = "-"
        obj.Logic = "CuurentStatus";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(obj),
            contentType: "application/json;charset=utf-8",
            dataType: "JSON",
            success: function (data) {
                if (data != '') {
                    $.each(data.ResultSet.Table, function (key, val) {
                        $('#spnNew').html(val.New)
                        $('#spnPay').html(val.Paid)
                    });
                }
            },
            error: function (response) {
            }
        });
    }, 3000);


}
function UnitList() {
    var url = config.baseUrl + "/api/OnlineDiagnostic/Online_DiagnosticPackageQueries";
    var obj = {};
    obj.unit_id = Active.unitId;
    obj.TokenNo = "-";
    obj.BookingId = "-";
    obj.fromdate = '1900/01/01';
    obj.todate = '1900/01/01';
    obj.prm_1 = "-";
    obj.login_id = Active.userId;
    obj.Logic = "UnitList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#ddlUnitList').empty().append($('<option>Select Unit</option>'));
            if (data != '') {
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#ddlUnitList').append($('<option></option>').val(val.Unit_Code).html(val.unit_name)).select2();
                });

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
function getdate() {
    if ($("#dtAppDatetime").val() == "") {
        alert("Appointment date time is not selected");
        return;
    }
    $("#txtAppointmentdate").val($("#dtAppDatetime").val());
    $('#myModal').modal('hide');
}
function BookingList(ReportType) {
    $("#txtBookingId").val('');
    var url = config.baseUrl + "/api/OnlineDiagnostic/Online_DiagnosticPackageQueries";
    var from = Properdate($("#txtFrom").val(), '-')
    var to = Properdate($("#txtTo").val(), '-')
    //var ReportType = $('#ddlReportType').val();
    var obj = {};
    obj.unit_id = "CH01";
    obj.TokenNo = "-";
    obj.BookingId = "-";
    obj.fromdate = from;
    obj.todate = to;
    obj.prm_1 = "-";
    obj.login_id = Active.userId;
    obj.Logic = ReportType;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            $('#tblBooking tbody').empty();
            if (data != '') {
                console.log(data);
                $.each(data.ResultSet.Table, function (key, val) {
                    $('<tr><td>' + val.booking_date + '</td><td>' + val.BookingId + '</td><td>' + val.patient_name + '</td><td>' + val.gender + '</td><td>' + val.age + '</td><td>' + val.mobile_no + '</td><td>' + val.IsConfirmed + '</td><td><input id=' + val.BookingId + ' type="button" class="btn btn-warning" value="V" onclick="selectRow(this);BookingDetail(this.id)" /></td></tr>').appendTo($('#tblBooking tbody'));
                });
            }
            else {
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function PayUMoneyByDate() {
    $("#tblPayUPayment tbody").empty();
    var url = config.baseUrl + "/api/OnlineDiagnostic/CallWebApiPayUMoneyByDate";
    var from = Properdate($("#txtFrom").val(), '-')
    var to = Properdate($("#txtTo").val(), '-')
    var obj = {};
    obj.from = from;
    obj.to = to;
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {            
            if (Object.keys(data).length > 0) {                
                    var tbody = "";
                    var temp = "";
                var count = 0;
                $.each(data, function (key, val) {
                    count++;
                    if (val.status =='captured')
                        tbody += "<tr style='background:#dcf7dc'>";
                    else
                        tbody += "<tr>";

                    tbody += "<td>" + val.addedon + "</td>";
                    tbody += "<td>" + val.id + "</td>";
                    tbody += "<td>" + val.txnid + "</td>";
                    tbody += "<td>" + val.firstname + "</td>";
                    tbody += "<td>" + val.phone + "</td>";
                    tbody += "<td>" + val.bank_name + "</td>";
                    tbody += "<td>" + val.amount + "</td>";
                    tbody += "<td>" + val.bank_ref_no + "</td>";
                    tbody += "<td>" + val.email + "</td>";
                    tbody += "<td>" + val.status + "</td>";
                    tbody += "</tr>";
                });
                $('#tblPayUPayment tbody').append(tbody);                              
            }
            $('#modalPayUPayment').modal('show');
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BookingDetail(BookingId) {

    $("#txtBookingId").val(BookingId);
    var url = config.baseUrl + "/api/OnlineDiagnostic/Online_DiagnosticPackageQueries";
    var obj = {};
    var from = Properdate($("#txtFrom").val(), '-')
    var to = Properdate($("#txtTo").val(), '-')
    obj.unit_id = Active.unitId;
    obj.TokenNo = "-";
    obj.BookingId = BookingId;
    obj.fromdate = from;
    obj.todate = to;
    obj.prm_1 = "-";
    obj.login_id = Active.userId;
    obj.Logic = 'BookingDetail';
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            if (data != '') {
                console.log(data);
                $.each(data.ResultSet.Table, function (key, val) {
                    $('#idBookingReason').html(val.Remark)
                    $('#idAddress').html(val.pt_address)
                    $('#idConfirmRemark').html(val.Confirm_remark)
                    $('#idEmail').html(val.email_id)
                    $('#idFee').html(val.PackageCost)
                    $('#idOnlinePayCost').html(val.PackageCost2)
                    $('#idAllotedUnit').html(val.UnitName)
                    $('#txtManagerName').html(val.managerName)
                    $('#txtManagerMobile').html(val.managerMobile)
                    $('#idAppDateTime').html(val.booking_date)
                    $('#idPayStatus').html(val.PayStatus2)
                    $('#txtMobile1').val(val.mobile_no)


                    if (val.IsConfirmed == "Y") {
                        $("#btnConfirm").prop("disabled", true);
                    }
                    else {
                        $("#btnConfirm").prop("disabled", false);
                    }
                    if (val.BookingStatus == "Already Paid") {
                        $("#btnSendAppLink").prop("disabled", false);
                        $("#btnCancel").prop("disabled", true);
                    }
                    else {
                        $("#btnSendAppLink").prop("disabled", true);
                        $("#btnCancel").prop("disabled", false);
                    }
                    if (val.IsConfirmed == "Y" && val.payStatus == "Pending") {
                        $("#btnResendLink").prop("disabled", false);
                    }
                    else {
                        $("#btnResendLink").prop("disabled", true);
                    }
                });
            }
            else {
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BookingConfirmation() {

    var r = confirm("Are You sure to Confirm");
    if (r == false) {
        return
    }

    var obj = {};
    if ($("#ddlUnitList option:selected").text() == "Select Unit") {
        alert("Unit is not selected");
        return;
    }
    obj.BookingId = $("#txtBookingId").val();
    obj.unit_id = Active.unitId;
    obj.Confirm_remark = $("#txtRemark").val();
    obj.payment_link = "-";
    obj.meeting_link = "-";
    obj.login_id = Active.userId;
    obj.Logic = "TakeConfirmation";
    var url = config.baseUrl + "/api/OnlineDiagnostic/PackageConfirmation";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.Msg.includes('Successfully')) {
                alert(data.Msg);
                BookingDetail($("#txtBookingId").val());
                $("#ddlUnitList").prop('selectedIndex', '0').change();
                $('#tblBooking tbody').find('.select-row').find('td:eq(6)').text('Y');
            }
            else {

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function CancelBooking() {
    var r = confirm("Are You sure to cancel");
    if (r == false) {
        return
    }


    if ($("#txtRemark").val() == "") {
        alert("Remark is mandatory");
        return;
    }
    var obj = {};
    obj.BookingId = $("#txtBookingId").val();
    obj.unit_id = Active.unitId;
    obj.Confirm_remark = $("#txtRemark").val();
    obj.payment_link = "-";
    obj.meeting_link = "-";
    obj.login_id = Active.userId;
    obj.Logic = "CancelBooking";
    var url = config.baseUrl + "/api/OnlineDiagnostic/PackageConfirmation";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data.Msg.includes('Successfully')) {
                alert(data.Msg);
                BookingDetail($("#txtBookingId").val());
                $("#txtRemark").val('');
                $('#tblBooking tbody').find('.select-row').find('td:eq(6)').text('X');
            }
            else {

            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function Online_BookingNotification(cmd) {

    //var logic;
    if (cmd == "btnSendPayLink") {
        logic = "SendPaymentLink";
    }
    if (cmd == "btnSendAppLink") {
        logic = "SendAppLoginLink";
    }

    var url = config.baseUrl + "/api/OnlineDiagnostic/OnlinePackageNotification";
    var from = Properdate($("#txtFrom").val(), '-')
    var to = Properdate($("#txtTo").val(), '-')
    var obj = {};
    obj.unit_id = Active.unitId;
    obj.PatientId = $("#txtPatientId").val();
    obj.fromdate = from;
    obj.todate = to;
    obj.prm_1 = $("#txtMeetingLink").val();
    obj.login_id = Active.userId;
    obj.Logic = logic;

    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            if (data != '') {
                alert(data);
            }
            else {
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetPaymentStatus() {

    $('#myModal2').modal('show');
    var obj = {};
    obj.merchantKey = "E3Ghpv8l";
    obj.merchantTransactionIds = $("#txtBookingId").val();
    var url = config.baseUrl + "/api/OnlineDiagnostic/getPaymentResponse";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#tblPayments tbody').empty();
            if (data != '') {
                console.log(data);
                if (data.Msg == "Found") {
                    $.each(data.ResultSet.Table1, function (key, val) {
                        var btn = val.cmdType
                        if (val.cmdType == 'Update' || val.cmdType == 'Refund')
                            btn = "<input type='button' class='btn-success' id='" + val.paymentId + '|' + val.status + '|' + val.amount + "'  onclick='UpdatePaymentStatus(this.value,this.id)' value='" + val.cmdType + "' />"

                        $('<tr><td>' + val.AppointmentId + '</td><td>' + val.paymentId + '</td><td>' + val.mode + '</td><td>' + val.status + '</td><td>' + val.amount + '</td><td>' + val.addedon + '</td><td>' + btn + '</td></tr>').appendTo($('#tblPayments tbody'));
                    });
                }
                else {
                    alert(data.Msg);
                }
            }
            else {
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdatePaymentStatus(cmd, content) {

    var obj = {};
    obj.command = cmd;
    obj.AppointmentId = $("#txtBookingId").val();
    obj.StrValues = content;
    obj.loginId = Active.userId;
    obj.Logic = "UpdatePayment";
    var url = config.baseUrl + "/api/OnlineDiagnostic/UpdatePaymentStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#tblPayments tbody').empty();
            if (data != '') {
                alert(data);
            }
            else {
                alert(data.Msg);
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function showSmsDialog() {
    var t = $('#txtMobile1').val();
    if (t.length > 2) {
        $('#txtMobileLog').val(t);
        GetSmsLog();
    }

    $('#myModal3').modal('show');
}
function showUHIDDialog() {
    var t = $('#txtMobile1').val();
    if (t.length > 2) {
        HISCustomerList();
        $('#myModal4').modal('show');
    }
}
function GetSmsLog() {

    var url = config.baseUrl + "/api/OnlineDiagnostic/Online_DiagnosticPackageQueries";
    var obj = {};
    obj.unit_id = Active.unitId;
    obj.TokenNo = "-";
    obj.BookingId = $("#txtBookingId").val();
    obj.fromdate = '1900/01/01';
    obj.todate = '1900/01/01';
    obj.prm_1 = '-';
    obj.login_id = Active.userId;
    obj.Logic = "SMSLog";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            $('#tblSmsLog tbody').empty();
            if (data != '') {
                console.log(data);
                $.each(data.ResultSet.Table, function (key, val) {
                    $('<tr><td>' + val.cr_date + '</td><td>' + val.sms + '</td><td>' + val.response_message + '</td></tr>').appendTo($('#tblSmsLog tbody'));
                });
            }
            else {
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function HISCustomerList() {

    var url = config.baseUrl + "/api/OnlineDiagnostic/HISCustomerList";
    var obj = {};
    obj.unit_id = Active.unitId;
    obj.PatientId = "-";
    obj.prm_1 = $('#txtMobile1').val();
    obj.login_id = "-"
    obj.Logic = "SMSLog";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            console.log(data);
            $('#tplPatientList tbody').empty();
            if (data != '') {
                console.log(data);
                $.each(data.ResultSet.Table, function (key, val) {
                    $('<tr><td>' + val.Patient_ID + '</td><td>' + val.PName + '</td><td><button id="' + val.Patient_ID + '" type="button" class="btn btn-success btn-block" onclick="UpdateUHID(this.id)">Update</button></td></tr>').appendTo($('#tplPatientList tbody'));
                });
            }
            else {
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function UpdateUHID(UHID) {

    var obj = {};
    obj.command = "Update";
    obj.AppointmentId = $("#txtPatientId").val();
    obj.StrValues = UHID + '|-|0|';
    obj.loginId = Active.userId;
    obj.Logic = "updateUHID";
    var url = config.baseUrl + "/api/OnlineDiagnostic/UpdatePaymentStatus";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(obj),
        contentType: "application/json;charset=utf-8",
        dataType: "JSON",
        success: function (data) {
            $('#tblPayments tbody').empty();
            if (data != '') {
                alert(data);
            }
            else {
                alert(data.Msg);
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}