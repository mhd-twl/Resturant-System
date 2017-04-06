
$(".caret").click(function () {
    $(".myDropdownMenu").toggle();
});

console.log('hii');

function toggleCategories(e) {
    var hdr_id = e; //$("#hdrCtg").attr('id');
    $("#ctgBdy_" + hdr_id).toggle();
}


//====================================== LOG IN===============


$(document).ready(function () {
    var lstt = new Array();///new
    var LogOn_Stts = { "table": 1, "user": 2, "admin": 3  , 'err' : -1}
    lstt = [];
    function checklogIn() {
        if ($("select").val() == "tbl") {
            val0 = $("#tblId").val()
            lstt.push(val0);
        } else {
            val1 = $("#user").val();
            val2 = $("#pass").val();
            lstt.push(val1);
            lstt.push(val2);
        }
        $.ajax({
            type: "GET",
            traditional: true,
            url: "/Home/CheckLogin",
            data: { val: lstt },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: Login_result,
            error: console.log('err')
        });
        lstt = [];
    }
    var Login_result = function (data) {
        if (data == LogOn_Stts.table) {
            window.location.href = 'MealsView/Index';
        }
        else if (data == LogOn_Stts.admin || data == LogOn_Stts.user)
            window.location.href = 'InvoiceView/Index';
        else if (data == LogOn_Stts.err)
        { $("#ErrLogIn").val() = "Incorrect inputs...!!"; $("#ErrLogIn").toggle(); }
        console.log("yes");
    }
    $("#LoginBtn").click(checklogIn);
    $("select#optsUsrTbl").change(function () {
        $(".logInTbl").toggle();
        $(".logInUser").toggle();
    });
});



