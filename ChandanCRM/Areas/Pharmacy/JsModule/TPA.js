$(document).ready(function () {
    CloseSidebar();
    $('button').on('click', function () {
        InsertRecord();
    });
});
function InsertRecord() {
    var url = config.baseUrl + "/api/TPA/InsertJustDialLeads";
    var objBO = {};
    objBO.leadid ='JDF9CB89748';
    objBO.leadtype = 'Test';
    objBO.prefix = 'Dr.';
    objBO.name = 'Abhay Singh';
    objBO.mobile = '9897485754';
    objBO.phone = '224001';
    objBO.email = 'Abhay@gmail.com';
    objBO.date = '1900/01/01';
    objBO.category = 'Generator Dealers';
    objBO.city = 'Lucknow';
    objBO.area = 'Ghatkpoar West';
    objBO.brancharea ='Aplollo';
    objBO.dncmobile = 0;
    objBO.dncphone = 0;
    objBO.company = 'Greaves Cotton';
    objBO.pincode = '222026';
    objBO.time = '13:10:11';
    objBO.branchpin ='400001';
    objBO.parentid = 'PXX22.XX22.1507052305445';
    objBO.Login_id = Active.userId;
    objBO.Logic = "Insert";
     $.ajax({
        method :"POST",
        url: url,
        data: JSON.stringify(objBO),
        dataType:"json",
        contentType:"application/json;charset=utf-8",
         success: function (data) {             
             console.log(data);
        },
        error: function (response) {
            alert('Server Error...!');
        }
    });

}

