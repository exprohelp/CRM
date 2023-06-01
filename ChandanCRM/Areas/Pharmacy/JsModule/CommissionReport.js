$(document).ready(function() {
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');   
});
function GetCalculateCommission() {
    $('#tblOrderTrackingDetail tbody').empty();
    var url = config.baseUrl + "/api/customerdata/OrderTrackingReport";
    var objBO = {};
    objBO.dtFrom = $('#txtFrom').val();
    objBO.dtTo = $('#txtTo').val();
    objBO.Logic = "CalculateCommission";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {         
            var tbody = '';
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table,function(key,val) {
                        tbody += "<tr>";
                        tbody += "<td style='text-align:center;'>" + val.sh_name +"</td>";
                        tbody += "<td style='text-align:center;'>"+ val.emp_name +"</td>";
                        tbody += "<td style='text-align:right;'>" + val.orderNos +"</td > ";
                        tbody += "<td style='text-align:right;'>" + val.total +"</td>";
                        tbody += "<td style='text-align:right;'>" + val.received +"</td>";
                        tbody += "<td style='text-align:right;'>" + val.discount + "</td>";
                        tbody += "<td style='text-align:right;'>" + val.commission + "</td>";
                        tbody +="</tr>";
                    });
                    $('#tblOrderTrackingDetail tbody').append(tbody);
                }
            }
        }
    })
}

function CommissionReport(){
    var url = config.baseUrl + "/api/customerdata/OrderTrackingReport";
    var objBO = {};
    objBO.dtFrom = $('#txtFrom').val();
    objBO.dtTo = $('#txtTo').val();    
    objBO.OutPutType = "Excel";
    Global_DownloadExcel(url,objBO,"Report.xlsx");
}