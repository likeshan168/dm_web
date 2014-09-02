/// <reference path="../javascriptFiles/jquery-1.4.2-vsdoc.js" />

/// <reference path="../VipClientLogin.aspx" />
$(document).ready(function () {
    var txtName = $("#" + getClientId().Id1),
    txtPass = $("#" + getClientId().Id2),
    txtVCode = $("#" + getClientId().Id3),
    nameErr = $("#nameErr"),
    pwdErr = $("#passErr"),
    btnSub = $("#" + getClientId().Id4);

    /*****************************/
    //登录之前的验证
    btnSub.click(function (e) {
        
        if (txtName.val().length === 0) {
            nameErr.css("display", "inline-block");
            txtName.focus();
            e.preventDefault();
            return false;

        }
        else {
            nameErr.css("display", "none");
        }

        if (txtPass.val().length === 0) {
            pwdErr.css("display", "inline-block");
            txtPass.focus();
            e.preventDefault();
            return false;
        }
        else {
            pwdErr.css("display", "none");
        }
        
        if (txtVCode.val().length === 0) {
            alert("请输入验证码");
            e.preventDefault();
            return false;
        }

    });
    /*****************************/
    /*****************************/
    //验证码的验证
    txtVCode.change(function () {
        $.post("ValidateCode.ashx", "code=" + txtVCode.val(), function (data) {
            if (data === "N") {
                alert("输入的验证有误！");
                return false;
            }
        });
    });
    /*****************************/


});