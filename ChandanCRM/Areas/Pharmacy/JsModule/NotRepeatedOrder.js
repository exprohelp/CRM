﻿$(document).ready(function() {
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
});
function GetNotRepeatOrder(){
    $('#tblNotRepeatOrder tbody').empty();
    var url = config.baseUrl + "/api/Analysis/MultiPurposeAnalysisQueries";
    var objBO = {};
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = "NotRepeatedOrder";
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function(data) {
            var tbody = "";
            //console.log(data);
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function(key,val) {
                        tbody += "<tr>";
                        tbody += "<td  class='text-center'>" + val.card_no + "</td>";
                        tbody += "<td  class='text-center'>" + val.cardtype + "</td>";
                        tbody += "<td  class='text-left'>" + val.cust_name + "</td>";
                        tbody += "<td  class='text-center'>" + val.mobileno + "</td>";
                        tbody += "<td  class='text-right'>" + val.Purch_amt + "</td>";
                        tbody += "<td  class='text-center'>" + val.lst_order + "</td>";
                        tbody += "<td  class='text-center'>" + '-' + "</td>";
                        tbody += "<td  class='text-center'>" + '-' + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblNotRepeatOrder tbody').append(tbody);
                }
            }
        }
    });
}