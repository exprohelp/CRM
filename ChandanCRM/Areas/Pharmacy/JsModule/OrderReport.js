$(document).ready(function () {
    FillCurrentDate('txtFrom');
    FillCurrentDate('txtTo');
});
function GetOrderReport() {   
   //$('#tblOrderReportDetail tbody').empty();
    var url = config.baseUrl + "/api/OnlineOrder/OrderTrackingReport";
    var objBO = {};  
    objBO.from = $('#txtFrom').val(); 
    objBO.to = $('#txtTo').val();     
    objBO.Logic = "Order_Status";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType: "application/json;charset=utf-8",
        success:function(data) {        
            var tbody = "";         
            var count = '';
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key, val) {
                        count++;
                        if (count < 100) {
                            tbody += "<tr>";                                                
                            //customer detail
                            tbody += "<td>" + val.card_no + "</td>";
                            tbody += "<td>" + val.cust_name + "</td>";
                            //Order Detail
                            tbody += "<td>" + val.old_order_no + "</td>";
                            tbody += "<td>" + val.order_no + "</td>";
                            tbody += "<td>" + val.order_date + "</td>";
                            tbody += "<td>" + val.NoS + "</td>";
                            tbody += "<td>" + val.comp_flag + "</td>";
                            //Sale Detail
                            tbody += "<td>" + val.sale_date + "</td>";
                            tbody += "<td>" + val.Total + "</td>";
                            tbody += "<td>" + val.Discount + "</td>";
                            tbody += "<td>" + val.Net + "</td>";
                            tbody += "<td>" + val.Disc_perc + "</td>";
                            tbody += "<td>" + val.Sale_Delay + "</td>";
                            tbody += "<td>" + val.ByUnit + "</td>";
                            //Delivery Detail
                            tbody += "<td>" + val.del_Date + "</td>";
                            tbody += "<td>" + val.deld_date + "</td>";
                            tbody += "<td>" + val.deld_delay + "</td>";
                            //Remarks
                            tbody += "<td>" + val.unit_remark + "</td>";
                            tbody += "<td>" + val.tele_remark + "</td>";
                            tbody += "<td>" + val.emp_name + "</td>";
                            tbody += "</tr>";
                        }
                        
                    });
                    $('#tblOrderReportDetail tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found');
            }
        },
        error: function (response) {
            alert('Server Error....!');
        }
    });
}
function SearchbyOrder() {
    $('#tblOrderReportDetail tbody').empty();
    var url = config.baseUrl + "/api/OnlineOrder/OrderTrackingReport";
    var objBO = {};  
    objBO.prm_1 = $('#txtSearch').val();
    objBO.Logic = "SearchOrder_Status";   
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType: "application/json;charset=utf-8",
        success:function(data) {          
            var tbody = "";
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table,function(key,val) {
                        tbody += "<tr>";
                        //customer deatil
                        tbody += "<td>" + val.card_no + "</td>";
                        tbody += "<td>" + val.cust_name + "</td>";
                        //Order Detail
                        tbody += "<td>" + val.old_order_no + "</td>";
                        tbody += "<td>" + val.order_no + "</td>";
                        tbody += "<td>" + val.Order_Date + "</td>";
                        tbody += "<td>" + val.NoS + "</td>";
                        tbody += "<td>" + val.comp_flag + "</td>";
                        //Sale Detail
                        tbody += "<td>" + val.Sale_Date + "</td>";
                        tbody += "<td>" + val.Total + "</td>";
                        tbody += "<td>" + val.Discount + "</td>";
                        tbody += "<td>" + val.Net + "</td>";
                        tbody += "<td>" + val.Disc_perc + "</td>";
                        tbody += "<td>" + val.Sale_Delay + "</td>";
                        tbody += "<td>" + val.ByUnit + "</td>";
                        //Delivery Detail
                        tbody += "<td>" + val.del_date + "</td>";
                        tbody += "<td>" + val.deld_date + "</td>";
                        tbody += "<td>" + val.deld_delay + "</td>";
                        //Remarks
                        tbody += "<td>" + val.unit_remark + "</td>";
                        tbody += "<td>" + val.tele_remark + "</td>";
                        tbody += "<td>" + val.emp_name + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblOrderReportDetail tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found');
            }
        },
        error: function (response) {
            alert('Server Error....!');
        }
    });
}