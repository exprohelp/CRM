
var _itemid = '';
$(document).ready(function() {      
    $('#tblSaltInfo').on('click','tbody tr',function(){
        $(this).addClass('selectrow').siblings().removeClass('selectrow');
          var itemid = $(this).closest('tr').find('td:eq(0)').text();
           FillProductInfo(itemid);
    });
    $('#txtSearchItem').on('keyup',function() {
        var prm1 = $(this).val();
        SearchBySalt(prm1);
    });
});
function SearchBySalt(prm1) {   
    var url = config.baseUrl + "/api/salts/SaltQueries";
    var objBO = {};
    objBO.prm_1 = prm1;
    objBO.Logic = "SaltSearchList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success:function(data) {          
            var tbody = '';
            var count = 0;
            if(Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function (key,val) {                       
                        tbody += "<tr>";                      
                        tbody += "<td>" + val.salt_name + "</td>";
                        tbody += "<td><button class='btn-success'  style='border:none; padding:1px 9px;' onclick=selectRow(this);GetProductInfoById('" + val.salt_code + "')><span class='fa fa-sign-in'></button></td >";
                        tbody += "</tr>";
                    });
                    $('#tblProductDetails tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found');
            }
        },
        error: function(response) {
            alert('Server Error....!');
        }
    });
}
function GetProductInfoById(saltcode) {  
    $('#tblSaltInfo tbody').empty();
    var url = config.baseUrl + "/api/salts/SaltQueries";
    var objBO = {};
    objBO.prm_1 = saltcode;
    objBO.logic = "SaltInProducts";
    $.ajax({
        method:"POST",
        url:url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {          
            var tbody = '';    
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {                         
                    $.each(data.result.Table,function(key,val) {                       
                            tbody += "<tr>";
                            tbody += "<td style='display:none'>" + val.item_id + "</td>";
                            tbody += "<td>" + val.item_name + "</td>";
                            tbody += "<td>" + val.strn_info + "</td>";
                            tbody += "<td>" + val.mfd_name + "</td>";
                            tbody += "</tr>";
                    });
                    $('#tblSaltInfo tbody').append(tbody);                  
                }
            }
        }
    })
}
function FillProductInfo(itemid) {            
    $('#tblSubItems tbody').empty();
    var url = config.baseUrl + "/api/salts/SaltQueries";
    var objBO = {};
    objBO.prm_1 = itemid;
    objBO.logic = "SaltInProduct";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {           
            var tbody = '';
            var count = '';
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result).length > 0) {
                    $.each(data.result.Table, function (key,val) {
                            count++;                     
                            tbody += "<tr>";                         
                            tbody += "<td>" + val.Salt_Name + "</td>";
                            tbody += "<td>" + val.strn_Info + "</td>";
                            tbody += "</tr>";                                           
                    });
                    $('#tblSubItems tbody').append(tbody);
                }
            }
        }
    })
}
