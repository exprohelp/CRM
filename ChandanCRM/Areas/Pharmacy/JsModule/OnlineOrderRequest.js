var _pres = '';
$(document).ready(function() {
    $('#txtItemName').keyup(function(e) {
        var tbody = $('#tblnavigate').find('tbody');
        var selected = tbody.find('.selected');
        var KeyCode = e.keyCode;
        switch (KeyCode) {
            case (KeyCode = 40):
                tbody.find('.selected').removeClass('selected');
                if (selected.next().length == 0) {
                    tbody.find('tr:first').addClass('selected');
                }
                else {
                    tbody.find('.selected').removeClass('selected');
                    selected.next().addClass('selected');
                }
                break;
            case (KeyCode = 38):
                tbody.find('.selected').removeClass('selected');
                if (selected.prev().length == 0) {
                    tbody.find('tr:last').addClass('selected');
                }
                else {
                    selected.prev().addClass('selected');
                }
                break;
            case (KeyCode = 13):
                var ItemId = $('#tblnavigate').find('tbody').find('.selected').data('itemid');
                var itemName = $('#tblnavigate').find('tbody').find('.selected').text();
                $('#txtItemName').val(itemName).blur();
                $('#txtItemId').val(ItemId);
                $('#txtQty').focus();
                $('#ItemList').hide();
                break;
            default:
                var val = $('#txtItemName').val();
                if (val == '') {
                    $('#ItemList').hide();
                }
                else {
                    $('#ItemList').show();
                    ItemSelection();
                }
                break;
        }
    });
    GetOrderInfo();    
    $('select').select2();
    $('#txtQty').keyup(function(e) {
        if(e.keyCode == 13) {
            InsertOrder();
        }
    });   
});
function ShowPres() {
    if (_pres != null)
        window.open(_pres, '_blank');
    else
        alert('Not Found');
}
function GetOrderInfo() {
    $('#tblOrderDetails tbody').empty();
    var url = config.baseUrl + "/api/OnlineOrder/OnlineOrderQueries";
    var objBO = {};
    objBO.Logic = "OnlienOrderList";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            var tbody = '';
            var count = 0;
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table,function (key,val) {
                        count++
                        tbody += "<tr>";
                        tbody += "<td>" + count + "</td>";
                        tbody += "<td>" + val.card_no + "</td>";
                        tbody += "<td>" + val.order_no + "</td>";
                        tbody += "<td><button class='btn-success' style='padding:1px 9px;border: none;' onclick=selectRow(this);GetOrderbyId('" + val.order_no + "')><span class='fa fa-sign-in'></button></td >";
                        tbody += "</tr>";
                    });
                    $('#tblOrderDetails tbody').append(tbody);
                    $.each(data.ResultSet.Table1, function(key, value) {
                        $("#ddlTransferList").append($("<option></option>").val(value.sh_code).html(value.sh_name));
                    });
                }
            }
            else {
                alert('No Data Found')
            }
        },
        error:function(response) {
            alert('Server Error.....!');
        }
    });
}
function GetOrderbyId(orderno) {
    var url = config.baseUrl + "/api/OnlineOrder/OnlineOrderQueries";
    var objBO = {};
    var cardno = $('#tblOrderDetails tbody tr.select-row').find('td:eq(1)').text();
    objBO.order_no = orderno;
    objBO.prm_1 = 'PtInfo';
    objBO.Logic = "OrderDetailWithPatientInfo";
    $.ajax({
        method: "POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success:function (data) {         
            var tbody = '';
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function(key, val) {
                        var path = val.file_path.replace('I:', '').replace(/\\/g, '/');
                        _pres = 'http://192.168.0.11//DocLocation' + path;
                        $('#txtCardNo').text(cardno);
                        $('#txtCurdName').text(val.cust_name);
                        $('#txtMobileNo').text(val.mobileNo);
                        $('#txtPhoneNo').text(val.phoneno);
                        $('#txtLastDate').text(val.last_pur_date);
                        $('#txtOrderNo').val(orderno);
                        $('#txtDelivaryTime').val(val.del_time);
                        $('#txtPresrcribeBy').val(val.prescribe_by);
                        var orderdate = moment(val.order_date).format('DD-MM-YYYY, hh:mm a');
                        $('#txtTime').val(orderdate);
                        var deldate = val.del_date.split('T')[0];
                        $('#txtDelivaryDate').val(deldate);
                        $('#txtAddress').val(val.address);
                        $('#txtDelivaryTime').val(val.del_time);
                    });
                    $.each(data.ResultSet.Table1,function(key,val) {
                        $('#tblItemDetails tbody').empty(tbody);
                        tbody += "<tr>";
                        tbody += "<td style='display:none;'>" + val.item_id + "</td>";
                        tbody += "<td>" + val.item_name + "</td>";
                        tbody += "<td>" + val.qty + "</td>";
                        tbody += "<td><button class='btn-danger'  style='padding:1px 9px;border: none;  onclick=CancelOrderbyId('" + val.item_id + "') ><span class='fa fa-remove'></button></td >";
                        tbody += "</tr>";
                    });
                    $('#tblItemDetails tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found');
            }
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });
}
function GetRCMOrderInfo() {
    if (ValidateRCMOrder()) {
        var url = config.baseUrl + "/api/OnlineOrder/RCMCompleteOrder";
        var objBO = {};
        objBO.unit_id = Active.unitId;
        objBO.card_no = $('#txtCardNo').text();
        objBO.order_no = $('#txtOrderNo').val();
        objBO.del_date = $('#txtDelivaryDate').val();
        objBO.del_time = $('#txtDelivaryTime').val();
        objBO.remark = $('#txtRemark').val();
        objBO.trf_to = $('#ddlTransferList option:selected').val();
        objBO.ref_by = $('#txtPresrcribeBy').val();
        objBO.old_order_no = 'Y';
        objBO.sale_inv_no = '-';
        objBO.promo_flag = 'N/A';
        objBO.prm_1 = 'N/A';
        objBO.login_id = Active.userId;
        objBO.Logic = "FIRSTCALL_COM";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType:"application/json;charset=utf-8",
            success:function (data) {
               //console.log(data);
                //if (data.includes('Success')) {
                //    alert(data)
                //}
                //else {
                //    alert(data);
                //}
            },
            error: function (response) {
                alert('Server Error...!');
            }
        });
    }    
}
function CancelOrder() {
    if(confirm('Are You sure to Cancel Order?')) {
        var url = config.baseUrl + "/api/OnlineOrder/UpdateTablesInfo";
        var objBO = {};
        objBO.unit_id = Active.unitId;
        objBO.tran_id = $('#txtOrderNo').val();
        objBO.login_id = Active.userId;
        objBO.prm_1 = $('#txtRemark').val();
        objBO.Logic ="update:CancelOrder";
        $.ajax({
            method:"POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success:function(data) {
                //console.log(data);
                if (data.includes('Success')) {
                    alert(data)
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
}
function FillOnlineOrderInfo() {
    $('#tblItemDetails tbody').empty();
    var url = config.baseUrl + "/api/OnlineOrder/OnlineOrderQueries";
    var objBO = {};
    objBO.order_no = $('#txtOrderNo').Val();
    objBO.Logic = "OrderDetailWithPatientInfo";
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {           
            var tbody = '';
            if (Object.keys(data.ResultSet).length > 0) {
                if (Object.keys(data.ResultSet.Table).length > 0) {
                    $.each(data.ResultSet.Table, function (key,val) {
                        tbody += "<tr>";
                        tbody += "<td>" + val.item_name + "</td>";
                        tbody += "<td>" + val.qty + "</td>";
                        tbody += "<td><button class='btn btn-danger btn-xs' onclick=CancelOrderbyId('" + val.item_id + "') ><span class='fa fa-remove'></button></td >";
                        tbody += "</tr>";
                    });
                    $('#tblItemDetails tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found');
            }
        },
        error:function (response) {
            alert('Server Error...!');
        }
    });
}
function CancelOrderbyId(itemid) {
    if (confirm('Are you sure to Cancel Order?')) {
        var url = config.baseUrl + "/api/OnlineOrder/OnlineOrderInsert";
        var objBO = {};
        objBO.card_no = $('#txtCardNo').text();
        objBO.order_no = $('#txtOrderNo').val();
        objBO.del_date = $('#txtDelivaryDate').val();
        objBO.unit_id = '-';
        objBO.item_id = itemid;
        objBO.new_med = '-';
        objBO.qty = 0;
        objBO.Logic = "Cancel";
        $.ajax({
            method: "POST",
            url: url,
            data: JSON.stringify(objBO),
            dataType: "json",
            contentType: "application/json;charset=utf-8",
            success:function(data) {               
                FillOnlineOrderInfo();
            },
            error:function(response) {
                alert('Server Error....!');
            }
        });
    }
}
function InsertOrder() {   
if (ValidateInsertOrder()) {
    var url = config.baseUrl + "/api/OnlineOrder/OnlineOrderInsert";
    var objBO = {};
    objBO.card_no = $('#txtCardNo').text();
    objBO.order_no = $('#txtOrderNo').val();
    objBO.del_date = $('#txtDelivaryDate').val();
    objBO.unit_id ='-';
    objBO.item_id = $('#txtItemId').val();
    objBO.qty = $('#txtQty').val();
    objBO.Logic = "Insert";
    //$.ajax({
    //    method: "POST",
    //    url: url,
    //    data: JSON.stringify(objBO),
    //    dataType: "json",
    //    contentType: "application/json;charset=utf-8",
    //    success: function(data) {
    //        FillOnlineOrderInfo();
    //        $('#txtItemName').val('');
    //        $('#txtQty').val('');
    //    },
    //    error: function (response) {
    //        alert('Server Error....!');
    //    }
    //});      
   }
}
function ItemSelection(){  
    if($('#txtItemName').val().length < 3)
    {
        return;
    }
    var url = config.baseUrl + "/api/Product/ProductQueries";
    var objBO = {};
    objBO.searchString = $('#txtItemName').val();
    objBO.Logic = "ProductSearch";
    $.ajax({
        url: url,
        method:"POST",
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            $('#ItemList tbody').empty();
            if (data != '') {
                $.each(data.result.Table,function(key,val) {
                    $('#ItemList').show();
                    $('<tr class="searchitems" data-itemid=' + val.item_id + '><td>' + val.item_name + "</td></tr>").appendTo($('#ItemList tbody'));
                });
            }
        }
    });

}
function ValidateInsertOrder() {
    var itemName = $('#txtItemName').val();
    var qty = $('#txtQty').val();
    if (itemName == '') {
        alert('Please Choose Item');
        $('#txtItemName').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtItemName').removeAttr('style');
    }
    if (qty == '') {
        alert('Please Provide Quantity');
        $('#txtQty').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtQty').removeAttr('style');
    }
    return true;
}
function ValidateRCMOrder() {
    var cardName = $('#txtCurdName').text();
    var remark = $('#txtRemark').val();
    var trf_to = $('#ddlTransferList option:selected').text();
    if (cardName == 'XXXX') {
        alert('please select CardInformation');
        return false;
    }
    if (remark == '') {
        alert('Please Enter Remark');
        $('#txtRemark').css('border-color','red').focus();
        return false;
    }
    else {
        $('#txtRemark').removeAttr('style');
    }
    if (trf_to == 'Select') {
        alert('Please choose TransferUnit');
        $('#ddlTransferList').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#ddlTransferList').removeAttr('style');
    }
    return true;
}