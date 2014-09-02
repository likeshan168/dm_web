/// <reference path="jquery-1.4.2-vsdoc.js" />
$(document).ready(function () {
    var txtCard = $("#txtCardNo"), vipType = $("#vipCardType"), txtPass = $("#txtCardPass"), btnRegister = $("#RegisterVIPCard"), cardMsg = $("#cardNoErr"), typeMsg = $("#cardTypeErr"), passMsg = $("#cardPassErr");
    var txtUser = $("#txtUserName"), txtPhoneNo = $("#txtPhone"), userErr = $("#UserNameErr"), phoneNoErr = $("#phoneErr"), tbodyVipCard = $("#vipCardInfo");
    /*********************************/
    //初始化vip卡类型信息
    $.ajax({
        url: "httpHandler/VipCardTypeManager.ashx",
        type: "POST",
        data: "flag=VT",
        success: function (data) {
            if (data === "null") {
                //var htmlStr = "";
                //tbodyVipCard.html(htmlStr);
            }
            else {
                tbodyVipCard.html(data);
            }
        },
        error: function (xhr, status, error) {
            alert(status + ":" + error);
        }
    });
    /*********************************/
    /*********************************/
    //vip卡进行注册
    btnRegister.click(function () {

        if (txtUser.val().length === 0) {
            userErr.css("display", "inline-block");
            txtUser.focus();
            return false;
        }
        else {
            userErr.css("display", "none");
        }

        if (txtPhoneNo.val().length === 0) {
            phoneNoErr.css("display", "inline-block");
            txtPhoneNo.focus();
            return false;
        }
        else {
            phoneNoErr.css("display", "none");
        }

        if (txtCard.val().length === 0) {
            cardMsg.css("display", "inline-block");
            txtCard.focus();
            return false;
        }
        else {
            cardMsg.css("display", "none");
        }

        if (vipType.val() === "0") {
            typeMsg.css("display", "inline-block");
            vipType.focus();
            return false;
        }
        else {
            typeMsg.css("display", "none");
        }

        if (txtPass.val().length === 0) {
            passMsg.css("display", "inline-block");
            passMsg.focus();
            return false;
        }
        else {
            passMsg.css("display", "none");
        }

        $.post("url", "{}", function () { });



    });
    /*********************************/

    /*********************************/
    //添加vip卡类型
    var btnCardType = $("#btnCardTypeSub"), txtCardTypeN = $("#txtCardTypeName"), txtCardD = $("#txtCardDiscount"), txtCardVY = $("#txtCardValidityYears"), cardTypeErr = $("#tctnErr"), cardDisErr = $("#tcdErr"), cardVYear = $("#txvyErr");
    btnCardType.click(function () {
        if (txtCardTypeN.val().length === 0) {
            cardTypeErr.css("display","inline-block");
        }
    });
    /*********************************/
});