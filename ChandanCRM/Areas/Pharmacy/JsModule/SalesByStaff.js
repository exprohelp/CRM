$(document).ready(function () {
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
    $('#ddlCategory').on('change', function () {       
        var value = $(this).val();
        window.location.href = value;    
    });
    $('select').select2();
});
function GetSalesReport() {
    $('#tblSalesReportDetail tbody').empty();
    var url = config.baseUrl + "/api/Analysis/MultiPurposeAnalysisQueries";
    var objBO = {};
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = "SalesAnalysis_ForTeleCaller";
    $.ajax({
        method:"POST",
        url:url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success:function(data) {      
            var tbody = '';
            var emp_name = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table,function (key,val) {
                        if(emp_name != val.emp_name) {
                            tbody += "<tr style='background:#f1d5d259'>";
                            tbody += "<td colspan='9'><b>Caller : </b>" + val.emp_name + "</td>";
                            tbody += "</tr>";
                            emp_name = val.emp_name;
                        }
                        tbody += "<tr>";
                        tbody += "<td>" + val.sh_name + "</td>";
                        tbody += "<td class='text-right'>" + val.sales + "</td>";
                        tbody += "<td class='text-right'>" + val.hd + "</td>";
                        tbody += "<td class='text-right'>" + val.nhd + "</td>";
                        tbody += "<td class='text-right'>" + val.sr + "</td>";
                        tbody += "<td class='text-right'>" + val.DisLap5 + "</td>";
                        tbody += "<td class='text-right'>" + val.DisLap7 + "</td>";
                        tbody += "<td class='text-right'>" + val.DisLap10 + "</td>";
                        tbody += "<td class='text-right'>" + val.DisLap11 + "</td>";
                        tbody += "</tr>";
                        tbody += "</tr>";
                    });
                    $('#tblSalesReportDetail  tbody').append(tbody);
                }
            }
        }
    })
}
