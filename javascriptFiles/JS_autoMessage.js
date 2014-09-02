function setTab(name, cursel, n) {
    for (i = 1; i <= n; i++) {//one 1 2;one 2 2
        var menu = document.getElementById(name + i);//one1 one2
        var con = document.getElementById("con_" + name + "_" + i); //con_one_1 con_one_2
        menu.className = i == cursel ? "hover" : "";
        con.style.display = i == cursel ? "block" : "none";
    }
}

function sendType() {
    if ($("#ctl00_cph_rbDynamics").attr("checked") == "checked") {
        $("#s2").css("display", "block");
        $("#s1").css("display", "none");
        $("#s3").css("display", "none");
    }
    else if ($("#ctl00_cph_rbNormal").attr("checked") == "checked") {
        $("#s2").css("display", "none");
        $("#s1").css("display", "block");
        $("#s3").css("display", "none");
    }
    else {
        $("#s2").css("display", "none");
        $("#s1").css("display", "none");
        $("#s3").css("display", "block");
    }
}

function couponDis() {
    if ($("#ctl00_cph_rbCoupon2").attr("checked") == "checked") {
        $("#trCoupon").css("display", "none");
        $("#trCoupon1").css("display", "none");
    }
    else {
        $("#trCoupon").css("display", "table-row");
        $("#trCoupon1").css("display", "table-row");
    }
}

function scoreDis() {
    if ($("#ctl00_cph_rbScore2").attr("checked") == "checked") {
        $("#trScore").css("display", "none");
    }
    else {
        $("#trScore").css("display", "table-row");
    }
}

function insertAtCursor() {
    //IE support
    var myValue = document.getElementById("ctl00_cph_ddlField").options[document.getElementById("ctl00_cph_ddlField").selectedIndex].value;
    var myField = document.getElementById("ctl00_cph_txtText");
    if (myField.value.indexOf(myValue) != -1) {
        alert(myValue + '字段只能在内容中出现一次,请重新编辑.');
        return;
    }
    if (document.selection) {
        myField.focus();
        sel = document.selection.createRange();
        sel.text = myValue;
        sel.select();
    }
    //MOZILLA/NETSCAPE support   
    else if (myField.selectionStart || myField.selectionStart == '0') {
        var startPos = myField.selectionStart;
        var endPos = myField.selectionEnd;
        // save scrollTop before insert   
        var restoreTop = myField.scrollTop;
        myField.value = myField.value.substring(0, startPos) + myValue + myField.value.substring(endPos, myField.value.length);
        if (restoreTop > 0) {
            myField.scrollTop = restoreTop;
        }
        myField.focus();
        myField.selectionStart = startPos + myValue.length;
        myField.selectionEnd = startPos + myValue.length;
    } else {
        myField.value += myValue;
        myField.focus();
    }
}



$(document).ready(function () {
    $("[id$=btnSubmit]").click(function (e) {
        if ($("[id$=rbNormal]").is(":checked")) {
            if ($("[id$=txtSendDate]").val().length == 0) {
                e.preventDefault();
                alert("请输入要填写的固定日期！");
            }
        }
    });
});