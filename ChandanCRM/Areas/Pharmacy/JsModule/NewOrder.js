$(document).ready(function () {
    $('select').select2();
});
function GetNewOrderReport() {
    $('#tblNewOrderReport tbody').empty();
    var url = config.baseUrl + "/api/Analysis/MultiPurposeAnalysisQueries";
    var objBO = {};
    objBO.from = $('#txtFrom').val();
    objBO.to = $('#txtTo').val();
    objBO.Logic = "New_Order";
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function (data) {           
            var tbody = '';
            var emp_name = '';     
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table,function(key,val) {            
                        if (emp_name != val.emp_name) {
                            tbody += "<tr style='background:#f1d5d259'>";
                            tbody += "<td colspan='7'><b>Name of Employee</b>:-" + val.emp_name + "</td>";
                            tbody += "</tr>";
                            emp_name = val.emp_name;
                        }
                        tbody += "<tr>";                        
                        tbody += "<td class='text-center'>" + val.card_no + "</td>";
                        tbody += "<td>" + val.cardtype + "</td>";
                        tbody += "<td class='text-left'>" + val.cust_name + "</td>";
                        tbody += "<td>" + val.mobileno + "</td>";
                        tbody += "<td class='text-right'>" + val.Purch_amt + "</td>";                        
                        tbody += "<td class='text-right'>" + val.New_Purch + "</td>";                        
                        tbody += "<td class='text-center'>"+ val.lst_order.split('T')[0] + "</td>";                        
                        tbody += "</tr>";                      
                        tbody += "</tr>";                      
                    });
                    $('#tblNewOrderReport  tbody').append(tbody);
                }
            }
        }
    })
}
