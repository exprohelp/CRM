var status = "";
$(document).ready(function () {    
    CloseSidebar();
    onloadbindDropdown();
    $("#txtStartTime").on('change', function () {
        var startval = parseInt($(this).val());
        if (startval < 10) {
            startval = '0' + startval;
        }
        $(this).val(startval);
    });
    $("#txtEndTime").on('change', function () {
        var endval = parseInt($(this).val());
        if (endval < 10)
        {
            endval = '0' + endval;
        }
        $(this).val(endval);
    });
    $('input[type=radio][name=daysdate]').change(function() {
        
        var bydaysdate = $("input[name='daysdate']:checked").val();
        if (bydaysdate == "days") {
            $("#divDays").show();
            $("#divDate").hide();
        }
        else {
            $("#divDays").hide();
            $("#divDate").show();
        }
    });
});

function onloadbindDropdown() {
    var url = config.baseUrl + "/api/master/DoctorMasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.Logic = "OnLoadBindDropDown";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                $("#ddlDepartment").empty();
                $("#ddlSpecialization").empty();
                $("#ddlDegree").empty();
                $("#ddlDepartment").append($("<option></option>").val("0").html("Please Select"));
                $("#ddlSpecialization").append($("<option></option>").val("").html("Please Select"));
                $("#ddlDegree").append($("<option></option>").val("").html("Please Select"));
                $.each(data.ResultSet.Table, function (key, value) {
                    $("#ddlDepartment").append($("<option></option>").val(value.DeptId).html(value.DepartmentName));
                });
                $.each(data.ResultSet.Table1, function (key, value) {
                    $("#ddlSpecialization").append($("<option></option>").val(value.SpecializationName).html(value.SpecializationName));
                });
                $.each(data.ResultSet.Table2, function (key, value) {
                    $("#ddlDegree").append($("<option></option>").val(value.DegreeName).html(value.DegreeName));
                });
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AddSpecialization() {
    var specname = $("#txtSpec").val();
    var specdesc = $("#txtSpecDesc").val();

    if (specname == "") {
        alert('Please enter specialization');
        return false;
    }

    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.SpecName = specname;
    objBO.SpecDesc = specdesc;
    objBO.login_id = Active.userId;
    objBO.Tagname = "Specialization";
    objBO.Logic = "insert";
    var url = config.baseUrl + "/api/master/mInsertUpdateSpecialization";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data);
            $("#SpecModal").modal('hide');
            onloadbindDropdown();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AddDepartment() {
    var Deptname = $("#txtDepartment").val();
    var deptdesc = $("#txtDepetmentDesc").val();

    if (Deptname == "") {
        alert('Please enter Department');
        return false;
    }
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.DeptName = Deptname;
    objBO.DeptDesc = deptdesc;
    objBO.login_id = Active.userId;
    objBO.Tagname = "Department";
    objBO.Logic = "insert";
    var url = config.baseUrl + "/api/master/mInsertUpdateDepartment";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data);
            $("#DepartmentModal").modal('hide');
            onloadbindDropdown();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AddDegree() {
    var Degree = $("#txtDegree").val();
    var degdesc = $("#txtDegDesc").val();

    if (Degree == "") {
        alert('Please enter Degree');
        return false;
    }

    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.DegName = Degree;
    objBO.DegDesc = degdesc;
    objBO.login_id = Active.userId;
    objBO.Tagname = "Degree";
    objBO.Logic = "insert";
    var url = config.baseUrl + "/api/master/mInsertUpdateDegree";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            alert(data);
            $("#DegreeModal").modal('hide');
            onloadbindDropdown();
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function AddUpdateDoctorProfile() {

    if (ValidateField()) {
        var objBO = {};
        var txtdocid = $("#EditDocId").text();
        if (txtdocid != "" && typeof txtdocid != 'undefined') {
            objBO.Logic = "Update";
            objBO.doctorId = txtdocid;
        }
        else {
            objBO.Logic = "Insert";
            objBO.doctorId = "-";
        }
        objBO.hosp_id = Active.unitId;
        objBO.title = $("#ddlTitle option:selected").val();
        objBO.doctorname = $("#txtDoctorName").val();
        objBO.doctype = $("#ddlDoctorType option:selected").text();
        objBO.phone = $("#txtPhone").val() == "" ? null : $("#txtPhone").val();
        objBO.mobile = $("#txtMobile").val();
        objBO.address = $("#txtAddress1").val();
        objBO.specialization = $("#ddlSpecialization option:selected").val() == "" ? null : $("#ddlSpecialization option:selected").val();
        objBO.DeptId = $("#ddlDepartment option:selected").val();
        objBO.degree = $("#ddlDegree option:selected").val() == "" ? null : $("#ddlDegree option:selected").val();
        objBO.gender = $("#ddlgender option:selected").val();
        objBO.imaregno = $("#txtRegImaNo").val() == "" ? null : $("#txtRegImaNo").val();
        objBO.regdate = $("#txtRegDate").val() == "" ? null : $("#txtRegDate").val();
        objBO.Emrgavail = $("input[name='Emrgavail']:checked").val();
        objBO.docshare = $("input[name='docshare']:checked").val();
        objBO.onlineAppoint = $("input[name='onlineAppoint']:checked").val();
        objBO.IsTokenReq = $("input[name='Token']:checked").val();
        objBO.drstatus = $("input[name='drstatus']:checked").val();
        objBO.feefreq = $("#txtFeeFreq").val();
        objBO.floorno = $("#ddlFloorNo option:selected").val();
        objBO.roomno = $("#ddlRoomNo option:selected").val();
        objBO.patientduration = $("#ddlDuration option:selected").val();
        objBO.login_id = Active.userId;
        var url = config.baseUrl + "/api/master/mInsertUpdateDoctor";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                Clear();
                var res = data.split("|");
                alert(res[0]);
                $("#docname").text(res[2]);
                $('#docid').text(' (' + res[1] + ')');
                $("#EditDocId").text(res[1]);

            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function AddUpdateDoctorSlot() {
    if (ValidateOPDField()) {
        var days = '';
        var objBO = {};
        $.each($("input[name='daysname']:checked"), function () {
            days += $(this).val() + ',';
        });
        newDays = days.toString().replace(/,\s*$/, '');
        objBO.doctorId = $("#EditDocId").text();
        objBO.StartTime = $("#txtStartTime").val() + " " + $("#ddlStartSLots option:selected").val();
        objBO.EndTime = $("#txtEndTime").val() + " " + $("#ddlEndSLots option:selected").val();
        objBO.ShiftName = $("#ddlShift option:selected").val();
        objBO.PatientLimit = $("#txtpatientLimit").val();
        objBO.Daysvalues = newDays;
        objBO.hosp_id = Active.unitId;
        objBO.Logic = "Insert";
        objBO.login_id = Active.userId;
        var url = config.baseUrl + "/api/master/mInsertUpdateDoctorTimeSlot";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                alert(data);
                BindOPDSchedule($("#EditDocId").text());
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }
}
function BindDoctorList() {
    var url = config.baseUrl + "/api/master/DoctorMasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.prm_1 = $("input[name='optradio']:checked").val();
    objBO.Logic = "GetDoctorsList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                var htmldata = "";
                var Deptname = ""
                if (data.ResultSet.Table.length > 0) {
                    $('#tblDoctorsList').show();
                    $('#tblDoctorsList tbody').empty();
                    $.each(data.ResultSet.Table, function (key, val) {
                        if (Deptname != val.DepartmentName) {
                            htmldata += '<tr>';
                            htmldata += '<td colspan="4" style="font-weight:bold;background-color:#d1ebfb">' + val.DepartmentName + '</td>';
                            htmldata += '</tr>';
                            Deptname = val.DepartmentName;
                        }
                        htmldata += '<tr>';
                        htmldata += '<td>' + val.DoctorId + '</td>';
                        htmldata += '<td>' + val.DoctorName + '</td>';
                        htmldata += '<td><a href="javascript:void(0)" onclick=EditDoctor(' + "'" + val.DoctorId + "'" + ')><i class="fa fa-pencil fa-lg text-blue" style="font-size:11px"></i></a></td>';
                        htmldata += '</tr>';
                    });
                    $('#tblDoctorsList tbody').append(htmldata);
                }
                else {
                    $('#tblDoctorsList').show();
                    $('#tblDoctorsList tbody').empty();
                    htmldata += '<tr>';
                    htmldata += '<td colspan="7" style="color:red;text-align:center">' + "No record found" + '</td>';
                    htmldata += '</tr>';
                    $('#tblDoctorsList tbody').append(htmldata);
                }
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function EditDoctor(DoctorId) {
    var url = config.baseUrl + "/api/master/DoctorMasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.doctorId = DoctorId;
    objBO.Logic = "GetDoctorById";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            if (data.ResultSet.Table.length > 0) {
                
                var newDate = "";
                $("#docname").text(data.ResultSet.Table[0].DoctorName);
                $("#docid").text('(' + data.ResultSet.Table[0].DoctorId + ')');
                $("#EditDocId").text(data.ResultSet.Table[0].DoctorId);
                $("#ddlTitle").val(data.ResultSet.Table[0].Title);
                $("#txtDoctorName").val(data.ResultSet.Table[0].DoctorName);
                $("#ddlDoctorType").val(data.ResultSet.Table[0].DoctorType);
                $("#txtPhone").val(data.ResultSet.Table[0].landline_no);
                $("#txtMobile").val(data.ResultSet.Table[0].mobile_no);
                $("#txtAddress1").val(data.ResultSet.Table[0].addres1);
                $("#ddlSpecialization").val(data.ResultSet.Table[0].speciality);
                $("#ddlDepartment").val(data.ResultSet.Table[0].DeptId);
                $("#ddlDegree").val(data.ResultSet.Table[0].degree);
                $("#ddlgender").val(data.ResultSet.Table[0].gender);
                $("#ddlDuration").val(data.ResultSet.Table[0].Patient_duration);
                $("#txtRegImaNo").val(data.ResultSet.Table[0].Imaregno);
                if (data.ResultSet.Table[0].Imaregdate != "") {
                    var olddate = data.ResultSet.Table[0].Imaregdate.split('/');
                    newDate = olddate[2] + "-" + olddate[1] + "-" + olddate[0];
                }
                $("#txtRegDate").val(newDate);
                $("#ddlFloorNo").val(data.ResultSet.Table[0].FloorName);
                $("#ddlRoomNo").val(data.ResultSet.Table[0].RoomNo);
                $("#txtFeeFreq").val(data.ResultSet.Table[0].fee_freq);
                $("input[name=Emrgavail][value=" + data.ResultSet.Table[0].emergency_availability + "]").prop('checked', 'checked');
                $("input[name=docshare][value=" + data.ResultSet.Table[0].share_flag + "]").prop('checked', 'checked');
                $("input[name=onlineAppoint][value=" + data.ResultSet.Table[0].online_appointment + "]").prop('checked', 'checked');
                if (data.ResultSet.Table[0].IsTokenRequired == true) {
                    $("input[name=Token][value='1']").prop('checked', 'checked');
                }
                else {
                    $("input[name=Token][value='0']").prop('checked', 'checked');
                }
                if (data.ResultSet.Table[0].Isactive == true) {
                    $("input[name=drstatus][value='1']").prop('checked', 'checked');
                }
                else {
                    $("input[name=drstatus][value='0']").prop('checked', 'checked');
                }

                //$("input[name=drstatus][value='" +  + "']").prop('checked', 'checked');
                $("#btnSaveDoctor").val('Update');
                BindOPDSchedule($("#EditDocId").text());
            }
            else {
                alert("No Data Found");
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function BindOPDSchedule(doctorId) {
    var url = config.baseUrl + "/api/master/DoctorMasterQueries";
    var objBO = {};
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.doctorId = doctorId;
    objBO.Logic = "GetOPDSceduleByDoctorId";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            
            var htmldata = "";
            if (data.ResultSet.Table.length > 0) {
                $('#tblopdSchedule').show();
                $('#tblopdSchedule tbody').empty();
                $.each(data.ResultSet.Table, function (key, val) {
                    htmldata += '<tr>';
                    htmldata += '<td>' + val.DoctorName + '</td>';
                    htmldata += '<td>' + val.day_name + '</td>';
                    htmldata += '<td>' + val.shift_start + '</td>';
                    htmldata += '<td>' + val.shift_end + '</td>';
                    htmldata += '<td>' + val.patient_limit + '</td>';
                    htmldata += '<td>' + val.shift_name + '</td>';
                    htmldata += '<td><a href="javascript:void(0)" onclick=DeleteSchedule(' + "'" + val.auto_id + "'" + ')><i class="fa fa-trash fa-lg text-red"></i></a></td>';
                    htmldata += '</tr>';
                });
                $('#tblopdSchedule tbody').append(htmldata);
            }
            else {
                $('#tblopdSchedule').show();
                $('#tblopdSchedule tbody').empty();
                htmldata += '<tr>';
                htmldata += '<td colspan="7" style="color:red;text-align:center">' + "No record found" + '</td>';
                htmldata += '</tr>';
                $('#tblopdSchedule tbody').append(htmldata);
            }

        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function DeleteSchedule(autoid) {
    var objBO = {};
    objBO.autoid = autoid;
    objBO.hosp_id = Active.unitId;
    objBO.login_id = Active.userId;
    objBO.doctorId = $("#EditDocId").text();
    objBO.Logic = "Delete";
    var url = config.baseUrl + "/api/master/mDeleteDoctorSchedule";
    if (confirm("Are you sure to delete schedule ?")) {
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success: function (data) {
                alert(data);
                BindOPDSchedule($("#EditDocId").text());
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }

}


function GenerateRow() {
    
    var fDate = $("#txtFromDate").val();
    var splitfdate = fDate.split('-');
    var newfdate = splitfdate[2];
    var TDate = $("#txttoDate").val();
    var splittdate = TDate.split('-');
    var newtdate = splittdate[2];
    days = (parseInt(newtdate) - parseInt(newfdate) + 1);

    var table = $("#tbldynamic");
    var resultHtml = '';
    resultHtml += [
        "<tr>",
        "<th>" + 'S.No.' + "</th>",
        "<th>" + 'Date' + "</th>",
        "<th>" + 'Start Time' + "</th>",
        "<th>" + 'End Time' + "</th>",
        '</tr>'];
    for (var i = 0; i < days; i++) {
        resultHtml += [
            "<tr>",
            "<td>",
            (i + 1),
            "</td>",
            '<td><input type="date" class="form-control"></td>',
            '<td><input type="number" class="form-control"></td>',
            '<td><input type="number" class="form-control"></td>',
            '</tr>'].join("\n");
    }
    table.html(resultHtml);   

}



function ValidateField() {
    var title = $("#ddlTitle option:selected").val();
    var doctorname = $("#txtDoctorName").val();
    var doctype = $("#ddlDoctorType option:selected").val();
    var mobile = $("#txtMobile").val();
    var gender = $("#ddlgender option:selected").val();
    var FeeFreq = $("#txtFeeFreq").val();

    if (title == "0") {
        alert('Please select title');
        return false;
    }
    if (doctorname == "") {
        alert('Please enter doctor name');
        return false;
    }
    if (doctype == "0") {
        alert('Please select doctor type');
        return false;
    }
    if (mobile == "") {
        alert('Please enter mobile number');
        return false;
    }
    if (gender == "0") {
        alert('Please select gender');
        return false;
    }
    if (FeeFreq == "") {
        alert('Please enter Fee Frequency');
        return false;
    }
    return true;

}
function ValidateOPDField() {

    var shift = $("#ddlShift option:selected").val();
    var strattime = $("#txtStartTime").val();
    var endtime = $("#txtEndTime").val();
    var patlimit = $("#txtpatientLimit").val();


    if ($('input[name=daysname]:checked').length == 0) {
        alert("ERROR! Please select at least one checkbox");
        return false;
    }
    if (shift == "") {
        alert('Please select shift');
        return false;
    }
    if (strattime == "") {
        alert('Please enter shit start time');
        return false;
    }
    if (endtime == "") {
        alert('Please enter shit end time');
        return false;
    }
    if (patlimit == "") {
        alert('Please enter patient limit');
        return false;
    }
}
function Clear() {
    
    $("#docname").text('');
    $("#docid").text('');
    $("#EditDocId").text('');
    $("#ddlTitle").prop('selectedIndex', '0');
    $("#txtDoctorName").val("");
    $("#ddlDoctorType").prop('selectedIndex', '0');
    $("#txtPhone").val("");
    $("#txtMobile").val("");
    $("#txtAddress1").val("");
    $("#ddlSpecialization").prop('selectedIndex', '0');
    $("#ddlDepartment").prop('selectedIndex', '0');
    $("#ddlDegree").prop('selectedIndex', '0');
    $("#ddlgender").prop('selectedIndex', '0');
    $("#txtRegImaNo").val("");
    $("#txtRegDate").val("");
    $("#ddlFloorNo").prop('selectedIndex', '0');
    $("#ddlRoomNo").prop('selectedIndex', '0');
    $("#ddlDuration").prop('selectedIndex', '2');
    $("#txtFeeFreq").val("");
    $("input[name=Emrgavail][value=N").prop('checked', true);
    $("input[name=docshare][value=N").prop('checked', true);
    $("input[name=onlineAppoint][value=Y").prop('checked', true);
    $("input[name=Token][value='1'").prop('checked', true);
    $("input[name=drstatus][value='1'").prop('checked', true);
    $("#btnSaveDoctor").val('Save');
}