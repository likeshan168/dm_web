/// <reference path="../javascriptFiles/jquery-1.4.2-vsdoc.js" />
/// <reference path="../DM_VIP_Register.aspx" />
$(document).ready(function () {
    var txtCardID = getClientID().Id1, txtUserName = getClientID().Id2, txtMobile = getClientID().Id3, txtBirthday = getClientID().Id4, txtEmail = getClientID().Id5, doc = document;
    var cardID = doc.getElementById(txtCardID), userName = doc.getElementById(txtUserName), mobile = doc.getElementById(txtMobile), birthday = doc.getElementById(txtBirthday), email = doc.getElementById(txtEmail);
    var spanCardId = doc.getElementById("spanCardId"), spanUserName = doc.getElementById("spanUserName"), spanMobile = doc.getElementById("spanMobile"), spanBirthday = doc.getElementById("spanBirthday"), spanEmail = doc.getElementById("spanEmail");
    var mobilePatrn = /^(13)[0-9]{1}\d{8}|(15)[0-9]{1}\d{8}|(18)[6-9]{1}\d{8}/,
    birthdayPatrn = /^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)/,
    mailPatrn = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;


    $("#" + txtCardID).blur(function () {
        if (cardID.value.length === 0) {
            doc.getElementById("spanCardId");
            spanCardId.innerHTML = "*卡号不能为空！";
            spanCardId.style.visibility = "visible";
            //cardID.focus();
        }
        else {//如果卡号不为空，那么就去验证输入的卡号是否存在以及正确 
            $.post("ValidateVipCardID.ashx", "cardID=" + cardID.value + "&type=1&rnd=" + Math.random(), function (data) {
                if (data === "Y")//vip卡号可用且未过期
                {
                    spanCardId.innerHTML = "*卡号正确！";
                    spanCardId.style.visibility = "visible";
                    spanCardId.style.color = "black";
                }
                else {//说明卡号不可用，或者输入错误
                    spanCardId.innerHTML = "*卡号不正确！或者已过期！";
                    spanCardId.style.visibility = "visible";

                }
            });
        }
    });

    $("#" + txtUserName).blur(function () {
        if (userName.value.length === 0) {
            spanUserName.style.visibility = "visible";
            spanUserName.innerHTML = "*姓名不能为空！";

        }
        else {
            spanUserName.style.visibility = "hidden";
        }
    });

    /*这里只验证了手机号码的位数是否正确，但是并没有验证手机号是否被注册了*/
    $("#" + txtMobile).blur(function () {
        if (mobile.value.length === 0) {
            spanMobile.style.visibility = "visible";
            spanMobile.innerHTML = "*手机号码不能为空！";
        }
        else {
            if (!mobilePatrn.test(mobile.value)) {
                spanMobile.style.visibility = "visible";
                spanMobile.innerHTML = "*手机号码格式不对！";
            }
            else {
                spanMobile.style.visibility = "hidden";
            }
        }
    });

    $("#" + txtBirthday).blur(function () {
        if (birthday.value.length !== 0) {
            if (!birthdayPatrn.test(birthday.value)) {
                spanBirthday.style.visibility = "visible";
                spanBirthday.innerHTML = "*生日格式输入有误！请输入格式如:2012-02-09";
            }
            else {
                spanBirthday.style.visibility = "hidden";
            }
        }
        else {
            spanBirthday.style.visibility = "hidden";
        }
    });


    $("#" + txtEmail).blur(function () {
        if (email.value.length !== 0) {
            if (!mailPatrn.test(email.value)) {
                spanEmail.style.visibility = "visible";
                spanEmail.innerHTML = "*邮箱格式不正确！请输入格式如:xxx@xxx.com";
            }
            else {
                spanEmail.style.visibility = "hidden";
            }
        }
        else {
            spanEmail.style.visibility = "hidden";
        }
    });
    var selectCT = $("#" + getClientID().Id6), spanCardType = doc.getElementById("spanCardType");
    /*这个是获取vip卡号类型用的*/
    //    $.post("GetVipCardTypeInfo.ashx", "action=getVIPType&rnd=" + Math.random(), function (data) {
    //        if (data === "")//说明没有获取到卡类型信息
    //        {
    //            if (confirm("是否去添加卡类型信息！")) {
    //                window.location.href = "../DM_VIPCard_Type_Managment.aspx";
    //            }

    //        }
    //        else {//说明说明获取到卡类型信息

    //            selectCT.html(data);
    //        }
    //    });

    selectCT.change(function () {
        if ($("#" + getClientID().Id6 + " option:selected").val() === "0") {
            spanCardType.innerHTML = "*请选择卡类型！";
            spanCardType.style.visibility = "visible";
        }
        else {
            spanCardType.style.visibility = "hidden";

        }
    });
    $("#" + getClientID().Id7).click(function (e) {
        if (cardID.value.length === 0 || userName.value.length === 0 || mobile.value.length === 0) {
            alert("请将信息填写完整！");
            e.preventDefault();
        }
    });

});

