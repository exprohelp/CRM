var _orderNo ='New';
var _item_id = '';
$(document).ready(function() {
    CloseSidebar();
    FillCurrentDate('txtPromoDate');
    FillCurrentDate('txtDelDate');
    FillCurrentDate('txtNextCalldate');    
    $('#txtQty').on('keyup',function(e) {
        if (e.keyCode == 13) {
            RCMOnCallOrder();
            $('#txtItemName').val('');
            $('#txtQty').val('');
        }
    });
    $('#txtNewQty').on('keyup',function(e) {
        if (e.keyCode == 13) {
            RCMOnCallOrderforNewProduct();
            $('#txtNewProduct').val('');
            $('#txtNewQty').val('');
        }
    }); 
    //Order On Phone
    $('#ddlCardNo').on('change',function() {      
        var cardno = $(this).val();      
        GetCardById(cardno);
    });
    $('#btnSearch').on('click',function(){     
        var cardno = $('#txtSearch').val();
        ByCardNo(cardno);
    });
    $('#btnSearch').on('click',function() {      
        var mobileno = $('#txtSearch').val();
        ByMobileNo(mobileno);
    });         
    $('select').select2();
    $('#txtItemName').keyup(function(e) {
        var tbody = $('#tblnavigate').find('tbody');
        var selected = tbody.find('.selected');
        var KeyCode = e.keyCode;
        switch (KeyCode) {
            case (KeyCode = 40):
                tbody.find('.selected').removeClass('selected')
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
    })       
});
function ByCardNo(cardno) { 
    var url = config.baseUrl + "/api/customerdata/RCMSmallQueries";
    var objBO = {};
    objBO.card_no = cardno;
    objBO.Logic = "ByCard";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {          
        if(data.result.Table.length > 0) {
            $("#ddlCardNo").empty().append($("<option></option>").val(0).html('Select'));
             $.each(data.result.Table,function(key,value) {
                 $("#ddlCardNo").append($("<option></option>").val(value.card_no).html(value.card_no));
              });
         }
      }
    })
}
function ByMobileNo(mobileno) {
    var url = config.baseUrl + "/api/customerdata/RCMSmallQueries";
    var objBO = {};  
    objBO.card_no = mobileno;
    objBO.Logic = "ByMobile";
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {
            if (data.result.Table.length > 0) {
                $("#ddlCardNo").empty().append($("<option></option>").val(0).html('Select'));
                $.each(data.result.Table,function (key,value) {
                    $("#ddlCardNo").append($("<option></option>").val(value.card_no).html(value.card_no));
                });
            }
        }
    });
}
function GetcardInfo() {
    $('#tblCardDetails tbody').empty();
    var url = config.baseUrl + "/api/customerdata/RCMSmallQueries";
    var objBO = {};
    objBO.time = $('#ddlTimeSlot option:selected').text();
    objBO.Logic = "OnCallTimeCardNo";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {            
            //console.log(data);
            var tbody ='';
            var count = 0;
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {                 
                    $.each(data.result.Table, function (key, val) {
                        count++;
                     if(count < 50) {
                         tbody += "<tr>";
                         tbody += "<td>" + val.card_no + "</td>";
                         tbody += "<td>" + val.rmd_time + "</td>";
                         tbody += "<td><button class='btn-success' style='padding:1px 9px;border:none;' onclick=selectRow(this);GetCardById('" + val.card_no.trim()+ "')><span class='fa fa-sign-in'></span></button></td >";
                         tbody += "</tr>";
                      }
                    });
                    $('#tblCardDetails tbody').append(tbody);
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
function GetCardById(cardno) {      
    $("#ddlFamilyMember").select2("val", "");    
    $('#txtOrderNo').text('');
    $('#txtCardNo').text('');
    $('#txtCurdName').text('');
    $('#txtMobileNo').text('');
    $('#txtPhoneNo').text('');
    $('#txtLastDate').text('');
    $('#txtAddress').val('');  
    var url = config.baseUrl + "/api/customerdata/RCMSmallQueries";
    var objBO = {};
    objBO.card_no = cardno; 
    objBO.Logic = "CustomerInfo";
    $.ajax({
        method:"POST",
        url:url,
        data:JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {     
           //console.log(data);                
            if(Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table, function (key,val) {
                        $('#txtSalInvNo').val(val.sale_inv_no);                      
                       //$('#txtOrderNo').val(_orderNo);
                        $('#txtCardNo').text(val.card_no);
                        $('#txtCurdName').text(val.cust_name);
                        $('#txtMobileNo').text(val.mobileNo);
                        $('#txtPhoneNo').text(val.phoneno);
                        $('#txtLastDate').text(val.last_pur_date);
                        $('#txtAddress').val(val.address);                          
                    });
                }
                else {
                    alert('Record Not Found.');
                    return;
                }
                if (Object.keys(data.result.Table1).length > 0) {
                    $('#ddlTransferTo').empty().append($("<option></option>").val('0').html('Select'));
                    $.each(data.result.Table1, function(key,val) {
                        $('#ddlTransferTo').append($("<option></option>").val(val.sh_code).html(val.sh_name));
                    });
                }
                if (Object.keys(data.result.Table2).length > 0) {                   
                    $('#ddlFamilyMember').empty().append($("<option></option>").val('0').html('Select'));
                    $.each(data.result.Table2,function(key,val) {
                        $('#ddlFamilyMember').append($("<option></option>").val(val.member_name).html(val.member_name));
                    });
                }   
                if (Object.keys(data.result.Table3).length > 0) {
                    $.each(data.result.Table3,function(key,val) {
                        $('#txtOrderNo').val(val.order_no);
                         OrderDetail();
                    });                
                }
            }
            else
            {
                alert('No Data Found');
            }
        },
        error:function(response) {
            alert('Server Error...!');
        }
    });
}
function ItemSelection() {
    if ($('#txtItemName').val().length < 3) {
        return;
    }
    var url = config.baseUrl + "/api/Product/Product_Queries";
    var objBO = {};
    objBO.prm_1 = $('#txtItemName').val();
    objBO.Logic = "Search:ForTeleCaller";
    $.ajax({
        url:url,
        method: "POST",
        data:JSON.stringify(objBO),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        success:function(data) {
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
function ValidateRCMOrder(){
    var cardNo = $('#txtCurdNo').text();
    var presby = $('#txtPresBy').val();
    var Remark = $('#txtRemark').val();
    var Message = $('#txtMessageForUnit').val();
    var trf_to = $('#ddlTransferTo option:selected').text();
    if (cardNo == 'XXXX') {
        alert('Please Select CardInformation');
        return false;
    }
    if (Message == '') {
        alert('Please Enter MessageForUnit');
        $('#txtMessageForUnit').css('border-color','red').focus();
        return false;
    }
    else {
        $('#txtMessageForUnit').removeAttr('style');
    }
    if (Remark == '') {
        alert('Please Enter Remark');
        $('#txtRemark').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtRemark').removeAttr('style');
    }
    if (presby == '') {
        alert('Please Enter PrescribedBy');
        $('#txtPresBy').css('border-color', 'red').focus();
        return false;
    }
    else {
        $('#txtPresBy').removeAttr('style');
    }
    if (trf_to == 'Select') {
        alert('Please choose TransferTo');
        $('#ddlTransferTo').css('border-color','red').focus();
        return false;
    }
    else {
        $('#ddlTransferTo').removeAttr('style');
    }
    return true;
}
function RCMOnCallOrder() {
    var url = config.baseUrl + "/api/customerdata/RCMOnCallOrder";
    var objBO = {};
    objBO.card_no = $('#txtCardNo').text();
    objBO.cust_name = $('#txtCurdName').text();
    objBO.delivery_date = $('#txtDelDate').val();   
    objBO.delivery_time = $('#ddlDelTime option:selected').text();
    objBO.qty = $('#txtQty').val();    
    objBO.item_id = $('#txtItemId').val();
    objBO.order_no = _orderNo;
    objBO.login_id = Active.userId;       
    objBO.unit_id = Active.unitId;      
    $.ajax({
        method:"POST",
        url:url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {
              //console.log(data);
              if (data.message.includes('|')) {
                  _orderNo = data.message.split('|')[1];
                  $('#txtOrderNo').val(_orderNo);
                  OrderDetail();   
                 }
                else {
                  $('#txtOrderNo').val(_orderNo);
              }  
          }
    })
}
function RCMOnCallOrderforNewProduct() {
    var url = config.baseUrl + "/api/customerdata/RCMOnCallOrder";
    var objBO = {};
    objBO.card_no = $('#txtCardNo').text();
    objBO.cust_name = $('#txtCurdName').text();
    objBO.delivery_date = $('#txtDelDate').val();
    objBO.delivery_date = $('#txtDelDate').val();
    objBO.newProductName = $('#txtNewProduct').val();
    objBO.qty = $('#txtNewQty').val();
    objBO.delivery_time = $('#ddlDelTime option:selected').text();
    objBO.item_id = 'New';   
    objBO.order_no = _orderNo;
    objBO.login_id = Active.userId;
    objBO.unit_id = Active.unitId;
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType:"application/json;charset=utf-8",
        success:function(data) {
            if(data.message.includes('|')) {
                _orderNo = data.message.split('|')[1];
                $('#txtOrderNo').val(_orderNo);
                 OrderDetail();
            }
            else
            {
                $('#txtOrderNo').val(_orderNo);
            }
        }
    })
}
function OrderDetail() {
    if(_orderNo == '') {
        alert('OrderNo Not Found');
        return;
    }
    $('#tblItemDetails tbody').empty();   
    var url = config.baseUrl + "/api/customerdata/RCMSmallQueries";
    var objBO = {};
    objBO.card_no = _orderNo;
    objBO.logic = 'OrderDetail';
    $.ajax({
        method:"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType: "json",
        contentType:"application/json;charset=utf-8",
        success: function (data) {          
            var tbody = "";        
            if (Object.keys(data.result).length > 0) {
                if (Object.keys(data.result.Table).length > 0) {
                    $.each(data.result.Table,function(key,val) {
                        tbody += "<tr>";                      
                        tbody += "<td>" + val.item_name + "</td>";
                        tbody += "<td>" + val.new_med + "</td>";
                        tbody += "<td>" + val.OrdQty + "</td>";
                        tbody += "<td>" + val.qty + "</td>";
                       // tbody += "<td><button class='btn-danger'  onclick=DeleteItem('" + val.autoid + "') style='padding:1px 9px;border:none;'><span class='fa fa-remove'></td>";
                        tbody += "</tr>";
                    });                     
                    $('#tblItemDetails tbody').append(tbody);
                }
            }
            else {
                alert('No Data Found');
            }
        }
    })
}
function RCMCompleteOrderNew() {   
    if (confirm('Are you sure to complete Order?')) {
        if (ValidateRCMOrder()) {
            var url = config.baseUrl + "/api/customerdata/RCMCompleteOrderNew";
            var objBO = {};
            objBO.card_no = $('#txtCardNo').text();
            objBO.order_no = $('#txtOrderNo').val();
            objBO.ref_by = $('#txtPresBy').val();
            objBO.prm_1 =  $('#txtPromoDate').val();
            objBO.del_date = $('#txtDelDate').val();
            objBO.del_time = $('#ddlDelTime option:selected').val();
            objBO.trf_to = $('#ddlTransferTo option:selected').val();
            objBO.rmd_time = $('#ddlNextCalltime option:selected').val();
            objBO.rmd_date = $('#txtNextCalldate').val();
            objBO.prm_2 = $('#txtMessageForUnit').val();
            objBO.remark = $('#txtRemark').val();   
            objBO.sale_inv_no = $('#LSaleInvoiceNo').val();
            objBO.promo_flag = ($('input[id=chkPromo]').is(':checked') == true) ? 'Y' : 'N',
            objBO.home_delflag = ($('input[id=chkHomeDel]').is(':checked') == true) ? 'Y' : 'N',
            objBO.logic = "FIRSTCALL_COM";
            objBO.callType = "Scheduled";
            objBO.completedBy = '-';           
            objBO.old_order_no = 'Y';         
            objBO.unit_id = Active.unitId;
            $.ajax({
                method:"POST",
                url:url,
                data:JSON.stringify(objBO),
                dataType:"json",
                contentType:"application/json;charset=utf-8",
                success:function(data) {                   
                    if(data.includes('Success')) {
                        alert(data);          
                        Clear();
                    }
                    else {
                        alert(data);
                    }
                },
                error:function(response) {
                    alert('Server Error..!');
                }
            });

        }
    }   
}
function Delete(autoid) {
    var url = config.baseUrl + "/api/customerdata/RCMSmallQueries";
    var objBO = {};
    objBO.autoid = autoid;
    objBO.logic ="Delete";
    $.ajax({
        method:"POST",
        url:url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
        success: function(data) {
            console.log(data);
        }
    })
}
function Clear(){
    $('#txtCardNo').text('');
    $('#txtOrderNo').text('');
    $('#txtMobileNo').text('');
    $('#txtPhoneNo').text('');
    $('#txtLastDate').text('');
    $('#txtAddress').text('');
    $('input[type=date]').val('');
    $('input[type=text]').val('');         
}