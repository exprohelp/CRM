var _itemid = '';
$(document).ready(function () {
    $('#btnCheckSalt').on('click', function () {
        CheckSaltInfo();
    });
    $('#btnCheckStock').on('click', function () {
        CheckStockInfo();
    });
    $('#txtSearchItem').on('keyup',function() {
        var prm1 = $(this).val();
        if (prm1.length > 3)
            SearchItem(prm1);
    });
});
function SearchItem(prm1) {
    var url = config.baseUrl + "/api/Product/ProductQueries";
    var objBO = {};
    objBO.searchString = prm1;
    objBO.Logic = "ProductSearch";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function (key, val) {
                        count++;
                        tbody += "<tr>";
                        tbody += "<td>" + val.item_name + "</td>";
                        tbody += "<td><button class='btn-success'  style='border:none; padding:1px 9px;' onclick=selectRow(this);GetAllInfoById('" + val.item_id + "')><span class='fa fa-sign-in'></button></td >";
                        tbody += "</tr>";
                    });
                    $('#tblProductDetails tbody').append(tbody);
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
function GetAllInfoById(itemId) {
    _itemid = itemId;
    $('#tblConstituents tbody').empty();
    $('#tblStore tbody').empty();
    var url = config.baseUrl + "/api/Product/ProductQueries";
    var objBO = {};
    objBO.searchString = itemId;
    objBO.Logic = "All-Info";
    $.ajax({
        method: "POST",
        url: url,
        data:JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function (key, val) {
                        $('#txtCompany').text(val.Mfd_Name);
                        $('#txtCategory').text(val.category);
                        $('#txtPackType').text(val.pack_type);
                        $('#txtPackQty').text(val.pack_qty);
                        $('#txtStatus').text(val.status_flag);
                        $('#txtMRP').text(val.mrp);
                    });
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
function CheckSaltInfo() {
    $('#tblConstituents tbody').empty();
    var url = config.baseUrl + "/api/salts/SaltQueries";
    var objBO = {};
    objBO.item_id = _itemid;
    objBO.Logic = "SaltsofAnItem";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function (key, val) {
                        count++;
                        //if(count<1000) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.Salt_Name + "</td>";
                        tbody += "<td>" + val.st_info + "</td>";
                        tbody += "</tr>";
                        //}
                    });
                    $('#tblConstituents tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function CheckStockInfo() {
    $('#tblStore tbody').empty();
    var url = config.baseUrl + "/api/stocks/StockAtOtherStores";
    var objBO = {};
    objBO.item_id = _itemid;
    $.ajax({
        method: "POST",
        url: url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success: function (data) {
            var tbody = "";
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function (key, val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.sh_name + "</td>";
                        tbody += "<td style='text-align:right;'>" + val.Stock + "</td>";
                        tbody += "</tr>";
                    });
                    $('#tblStore tbody').append(tbody);
                }
            }
        }
    })
}

