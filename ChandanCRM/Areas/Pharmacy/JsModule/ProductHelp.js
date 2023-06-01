$(document).ready(function () {
    $('#txtSearch').on('keyup', function () {
        var prm1 = $(this).val();
        if (prm1.length > 3)    
            GetProductInfo(prm1);
    });
});
function GetProductInfo(prm1) {    
    $('#tblProductDetails tbody').empty();
    var url = config.baseUrl + "/api/Product/ProductQueries";
    var objBO = {};
    objBO.searchString = prm1;
    objBO.Logic = "ProductSearch";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {       
            var tbody = '';
            var count = 0;
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function(key,val) {
                        count++
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.item_name + "</td>";
                        tbody += "<td><button class='btn-success'  style='border:none; padding:1px 9px;' onclick=GetAllInfoById('" + val.item_id + "')><span class='fa fa-sign-in'></button></td >";
                        tbody += "</tr>";
                    });
                    $('#tblProductDetails tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error:function (response) {
            alert('Server Error...!');
        }
    });
}
function GetAllInfoById(itemId) {
    $('#tblSaltInfo tbody').empty();
    $('#tblStoreAvailabilty tbody').empty();
    $('#tblStore tbody').empty();
    var url = config.baseUrl + "/api/Product/ProductQueries";
    var objBO = {};
    objBO.searchString = itemId;
    objBO.Logic = "All-Info";
    $.ajax({
        method:"POST",
        url: url,
        data:JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success:function(data) {
            var tbody = '';
            var tbody1 = '';
            var tbody2 = '';
            var SubstituteName ='';
            var count = 0;
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table,function (key,val) {
                        $('#txtSubtituteName').text(val.Item_Name);
                        $('#txtStoreAvailable').text(val.Item_Name);
                        $('#txtProductName').text(val.Item_Name);
                        $('#txtCompany').text(val.Mfd_Name);
                        $('#txtPackType').text(val.pack_type);
                    });
                    $.each(data.result.Table1,function(key,val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.salt_Name + "</td>";
                        tbody += "<td>" + val.strength + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblSaltInfo tbody').append(tbody);
                    $.each(data.result.Table2, function (key,val) {
                        tbody1 += "<tr>";
                        tbody1 += "<td>" + val.unit_name + "</td>";
                        tbody1 += "<td class='text-right'>" + val.Stock + "</td>";
                        tbody1 += "</tr>";
                    });
                    $('#tblStoreAvailabilty tbody').append(tbody1);
                    $.each(data.result.Table3,function (key, val) {                            
                        if(SubstituteName!= val.SubstituteName) {
                            tbody2 += "<tr class='cat-group'>";
                            tbody2 += "<td colspan='3'>" + val.SubstituteName + "</td>";
                            tbody2 += "</tr>";
                            SubstituteName = val.SubstituteName;                         
                        }
                        tbody2 += "<tr>";
                        tbody2 += "<td>" + val.unit_name + "</td>";
                        tbody2 += "<td class='text-right'>" + val.Quantity + "</td>";
                        tbody2 += "<td class='text-right'>" + val.QtyInPacks.toFixed(2) + "</td>";
                        tbody2 += "</tr>";
                    });
                    $('#tblStore tbody').append(tbody2);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function(response) {
            alert('Server Error...!');
        }
    });
}



