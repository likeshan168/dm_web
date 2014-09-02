/// <reference path="../javascriptFiles/jquery-1.4.2-vsdoc.js" />
/// <reference path="../VipClientRegister.aspx" />
/*vip卡号注册的js脚本，主要是一些验证*/
$(document).ready(function () {
    var namePatrn = /^([A-Za-z]+|[\u4E00-\u9FA5])*$/; //汉字或字母
    var birthdayPatrn = /^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)/;
    var mailPatrn = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    var mobilePatrn = /^(13)[0-9]{1}\d{8}|(15)[0-9]{1}\d{8}|(18)[6-9]{1}\d{8}/;
    var cardIdPatrn = /^[A-Za-z0-9]{1,20}/
    var lblName = document.getElementById("lblName"),
    lblBirthday = document.getElementById("lblBirthday"),
    lblMobile = document.getElementById("lblMobile"),
    lblEmail = document.getElementById("lblEmail"),
    lblVCode = document.getElementById("VCode"),
    lblCardID = document.getElementById("lblCardID"),
    txtUserName = $("#" + getClientID().Id1),
    txtBirthday = $("#" + getClientID().Id2),
    txtMobile = $("#" + getClientID().Id3),
    txtEmail = $("#" + getClientID().Id4),
    txtVCode = $("#" + getClientID().Id5),
    submit = $("#Submit"),
    txtCardID = $("#" + getClientID().Id6);

    var flag = true;

    /**********************/
    //验证卡号
    txtCardID.blur(function () {
        if (txtCardID.val().length === 0) {//为空
            lblCardID.style.color = "red";
            flag = false;
        }
        else {
            if (!cardIdPatrn.test(txtCardID.val())) {//不符合规则
                lblCardID.style.color = "red";
                flag = false;
            }
            else { //验证是否存在
            //程序分为两种一种是没有先制好卡的情况，这样就去验证卡号是否已经被注册(type=0)
//                $.post("ValidateVipCardID.ashx", "cardID=" + txtCardID.val() + "&type=0&rnd=" + Math.random(), function (data) {
//                    if (data === "Y")//vip卡号已经被注册
//                    {
//                        //alert("该卡号已经被注册了");
//                        txtCardID.focus();
//                        txtCardID.css("color", "red");
//                        lblCardID.style.color = "red";
//                        flag = false;
//                    }
//                    else {
//                        lblCardID.style.color = "#575757";
//                        txtCardID.css("color", "black");
//                        flag = true
//                    }
//                });
                //第二种的情况就是卡号先制好的，然后去发放的，这样的话，就去验证卡号是否正确(type=1)
                $.post("ValidateVipCardID.ashx", "cardID=" + txtCardID.val() + "&type=1&rnd=" + Math.random(), function (data) {
                    if (data === "Y")//vip卡号已经被注册
                    {
                        //alert("该卡号已经被注册了");
                        txtCardID.focus();
                        txtCardID.css("color", "red");
                        lblCardID.style.color = "red";
                        flag = false;
                    }
                    else {
                        lblCardID.style.color = "#575757";
                        txtCardID.css("color", "black");
                        flag = true
                    }
                });
            }
            

        }
    });
    /**********************/
    /**********************/
    //验证用户名
    txtUserName.blur(function () {
        // alert('123');
        if (txtUserName.val().length === 0) {
            lblName.style.color = "red";
            flag = false;
        }
        else {
            if (namePatrn.test(txtUserName.val())) {
                //alert('成功');
                lblName.style.color = "#575757";
                flag = true;
            }
            else {
                lblName.style.color = "red";
                flag = false;
            }
        }
    });
    /**********************/
    //验证生日
    txtBirthday.blur(function () {
        if (txtBirthday.val().length === 0) {
            lblBirthday.style.color = "red";
            flag = false;
        }
        else {
            if (birthdayPatrn.test(txtBirthday.val())) {
                lblBirthday.style.color = "#575757";
                flag = true;
            }
            else {
                lblBirthday.style.color = "red";
                flag = false;
            }
        }
    })
    /**********************/
    /**********************/
    //验证手机号
    txtMobile.blur(function () {

        if (txtMobile.val().length === 0) {
            lblMobile.style.color = "red";
            flag = false;
        }
        else {
            if (mobilePatrn.test(txtMobile.val())) {

                $.post("ValidateMobileExists.ashx", "mobile=" + txtMobile.val() + "&rnd=" + Math.random(), function (data) {
                    if (data === "Y")//手机号已经被注册
                    {
                        //alert("该手机号已经被注册了");
                        txtMobile.focus();
                        txtMobile.css("color", "red");
                        lblMobile.style.color = "red";
                        flag = false;
                    }
                    else {
                        lblMobile.style.color = "#575757";
                        txtMobile.css("color", "black");
                        flag = true
                    }
                });

            }
            else {
                lblMobile.style.color = "red";
                flag = false;
            }
        }
    });
    /**********************/
    /**********************/
    //验证邮箱
    txtEmail.blur(function () {
        if (txtEmail.val().length === 0) {
            lblEmail.style.color = "red";
            flag = false;
        }
        else {
            if (mailPatrn.test(txtEmail.val())) {
                lblEmail.style.color = "#575757";
                flag = true;
            }
            else {
                lblEmail.style.color = "red";
                flag = false;
            }
        }
    });
    /**********************/
    /**********************/
    //验证验证码
    txtVCode.blur(function () {
        if (txtVCode.val().length === 0) {
            lblVCode.style.color = "red";
            flag = false;
        }
        else {
            $.post("ValidateCode.ashx", "code=" + txtVCode.val() + "&rnd=" + Math.random(), function (data) {
                if (data === "N") {
                    lblVCode.style.color = "red";

                    flag = false;
                }
                else {
                    lblVCode.style.color = "#575757";

                    flag = true;
                }
            });
        }
    });
    /**********************/
    $("#Submit").click(function () {
        if (txtUserName.val().length === 0) {
            lblName.style.color = "red";
            flag = false;
        }
        else {
            if (namePatrn.test(txtUserName.val())) {
                //alert('成功');
                lblName.style.color = "#575757";
                flag = true;
            }
            else {
                lblName.style.color = "red";
                flag = false;
            }
        }
        //验证出生日期
        if (txtBirthday.val().length === 0) {
            lblBirthday.style.color = "red";
            flag = false;
        }
        else {
            if (birthdayPatrn.test(txtBirthday.val())) {
                lblBirthday.style.color = "#575757";
                flag = true;
            }
            else {
                lblBirthday.style.color = "red";
                flag = false;
            }
        }
        //验证手机号
        if (txtMobile.val().length === 0) {
            lblMobile.style.color = "red";
            flag = false;
        }
        else {
            if (mobilePatrn.test(txtMobile.val())) {
                lblMobile.style.color = "#575757";
                flag = true
            }
            else {
                lblMobile.style.color = "red";
                flag = false;
            }
        }
        // 验证邮箱
        if (txtEmail.val().length === 0) {
            lblEmail.style.color = "red";
            flag = false;
        }
        else {
            if (mailPatrn.test(txtEmail.val())) {
                lblEmail.style.color = "#575757";
                flag = true;
            }
            else {
                lblEmail.style.color = "red";
                flag = false;
            }
        }
        //验证码
        if (txtVCode.val().length === 0) {
            lblVCode.style.color = "red";
            flag = false;
        }
        else {
            lblVCode.style.color = "#575757";
            flag = true;
        }

        if (!flag) {
            return false;
        }
        else {
            //alert("成功");
            $("#btnSubmit").click();
        }
    });



});



