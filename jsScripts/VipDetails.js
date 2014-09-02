/// <reference path="../javascriptFiles/jquery-1.4.2-vsdoc.js" />
/// <reference path="../VipDetails.aspx" />

$(document).ready(function () {
    var txtUserName = $("#" + getClientID().userName),
    txtPass1 = $("#" + getClientID().password),
    txtPass2 = $("#" + getClientID().password2),
    txtBirthday = $("#" + getClientID().birthday),
    txtMobile = $("#" + getClientID().mobile),
    txtEmail = $("#" + getClientID().email),
    doc = document,
    lblName = doc.getElementById("lblName"),
    lblPwd = doc.getElementById("lblPwd"),
    lblPwd2 = doc.getElementById("lblPwd2"),
    lblBirthday = doc.getElementById("lblBirthday"),
    lblMobile = doc.getElementById("lblMobile"),
    lblEmail = doc.getElementById("lblEmail"),
    namePatrn = /^([A-Za-z]+|[\u4E00-\u9FA5])*$/,
    birthdayPatrn = /^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)/,
    mailPatrn = /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/,
    mobilePatrn = /^(13)[0-9]{1}\d{8}|(15)[0-9]{1}\d{8}|(188)\d{8}/,
    btn = $("#" + getClientID().btnSub);
    var flag = true;
    /*************************/
    //姓名的验证
    txtUserName.blur(function () {
        //alert('123');
        if (txtUserName.val().length === 0) {
            lblName.style.color = "Red";
            return false;
        }
        else {
            if (!namePatrn.test(txtUserName.val())) {
                lblName.style.color = "Red";
                return false;
            }
            else {
                lblName.style.color = "#575757";
            }

        }
    });
    /*************************/
    /*************************/
    //密码的验证
    txtPass1.blur(function () {
        if (txtPass1.val().length === 0) {
            lblPwd.style.color = "Red";
            return false;
        }
        else {//长度要在6-12之间
            if (txtPass1.val().length < 6 || txtPass1.val().length > 12) {
                lblPwd.style.color = "Red";
                return false;
            }
            else {
                lblPwd.style.color = "#575757";
            }
        }
    });
    /*************************/
    /*************************/
    //确认密码的验证
    txtPass2.blur(function () {
        if (txtPass2.val().length === 0) {
            lblPwd2.style.color = "Red";
            return false;
        }
        else {
            if (txtPass2.val() !== txtPass1.val()) {
                lblPwd2.style.color = "Red";
                return false;
            }
            else {
                lblPwd2.style.color = "#575757";
            }
        }
    });
    /*************************/
    /*************************/
    //出生日期的验证
    txtBirthday.blur(function () {
        if (txtBirthday.val().length === 0) {
            lblBirthday.style.color = "Red";
            return false;
        }
        else {//验证出生日期的格式
            if (!birthdayPatrn.test(txtBirthday.val())) {
                lblBirthday.style.color = "Red";
                return false;
            }
            else {
                lblBirthday.style.color = "#575757";
            }
        }
    });
    /*************************/
    /*************************/
    //邮箱的验证
    txtEmail.blur(function () {
        if (txtEmail.val().length === 0) {
            lblEmail.style.color = "Red";
            return false;
        }
        else {
            if (!mailPatrn.test(txtEmail.val())) {
                lblEmail.style.color = "Red";
                return false;
            }
            else {
                lblEmail.style.color = "#575757";
            }
        }
    });
    /*************************/
    /*************************/
    //提交修改
    $("#asubmitClick").click(function () {
       
        //vip姓名验证
        if (txtUserName.val().length === 0) {
            lblName.style.color = "Red";
            return false;
        }
        else {
            lblName.style.color = "#575757";
        }
       
        //密码验证
        if (txtPass1.val().length === 0) {
            lblPwd.style.color = "Red";
            return false;
        }
        else {//长度要在6-12之间
            if (txtPass1.val().length < 6 || txtPass1.val().length > 12) {
                lblPwd.style.color = "Red";
                return false;
            }
            else {
                lblPwd.style.color = "#575757";
            }
        }
       
        //出生日期验证
        if (txtBirthday.val().length === 0) {
            lblBirthday.style.color = "Red";
            return false;
        }
        else {//验证出生日期的格式
            if (!birthdayPatrn.test(txtBirthday.val())) {
                lblBirthday.style.color = "Red";
                return false;
            }
            else {
                lblBirthday.style.color = "#575757";
            }
        }
        
        //邮箱验证
        if (txtEmail.val().length === 0) {
            lblEmail.style.color = "Red";
            return false;
        }
        else {
            if (!mailPatrn.test(txtEmail.val())) {
                lblEmail.style.color = "Red";
                return false;
            }
            else {
                lblEmail.style.color = "#575757";
            }
        }
        
        btn.click();

    });
    /*************************/


});