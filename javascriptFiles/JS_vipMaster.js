/// <reference path="jquery-1.4.2-vsdoc.js" />
/// <reference path="jquery.form.js" />

var vipPage = 1;
var pageIndex = 1;

/***************新版start()*******************/
function start(place, object) {
    if (object.value === "无")
        return;
    var arr = object.value.split('-');
    switch (arr[1]) {
        case "单选按钮":
            $("#ctl00_cph_sp_text_" + place).css("display", "none");
            $("#ctl00_cph_sp_rb_" + place).css("display", "block");
            $("#ctl00_cph_sp_date_" + place).css("display", "none");
            break;
        case "日期":
        case "日期型":
        case "公式(日期)":
            $("#ctl00_cph_sp_text_" + place).css("display", "none");
            $("#ctl00_cph_sp_rb_" + place).css("display", "none");
            $("#ctl00_cph_sp_date_" + place).css("display", "block");
            break;
        default:
            $("#ctl00_cph_sp_text_" + place).css("display", "block");
            $("#ctl00_cph_sp_rb_" + place).css("display", "none");
            $("#ctl00_cph_sp_date_" + place).css("display", "none");
            break;
    }
}
/***************新版start()*******************/

/***************初始化关联问题的*******************/
function invsInit(object) {
    xmlHttp = GetXmlHttpObject();

    if (xmlHttp === null) {
        alert("您的浏览器不支持AJAX！");
        return;
    }
    if (object.value === "NONE")
        return;
    var url = "httpHandler/vipMaster_Response.ashx?type=invs&invsID=" + object.value;
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "success") {
                $("[id$=btnHideInitInvs]").click(); //触发其点击事件
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}
/***************初始化关联问题的*******************/

/***************单击一页显示多少条记录*******************/

function pageChanged(state) {
    //初始化按钮使用状态
    $("#pcFirst").removeAttr("disabled");
    $("#pcPreview").removeAttr("disabled");
    $("#pcNext").removeAttr("disabled");
    $("#pcLast").removeAttr("disabled");

    if (state === "last")
        pageIndex = vipPage; //显示第一页
    else if (state === "preview")
        pageIndex--;
    else if (state === "next")
        pageIndex++;
    else if (state === "first")
        pageIndex = 1;
    else if (state === "goto") {
        if ($("#txtGoto").val() === "" || $("#txtGoto").val() === "0" || $("#txtGoto").val() > vipPage) {
            alert('请输入正确的跳转页面');
            return;
        }
        pageIndex = $("#txtGoto").val();
    }
    if (pageIndex === 1) {
        $("#pcFirst").attr("disabled", true);
        $("#pcPreview").attr("disabled", true);
    } else if (pageIndex === vipPage) {
        $("#pcNext").attr("disabled", true); //这种操作只能是在ie浏览器中进行(不能跨浏览器)
        $("#pcLast").attr("disabled", true);
    }
    //alert(pageIndex);
    xmlHttp = GetXmlHttpObject();
    var url = "httpHandler/vipMaster_Response.ashx?type=paging&pageIndex=" + pageIndex + "&pageSize=" + $("#vipSize").val();
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('error') >= 0) {
                alert(xmlHttp.responseText);
            } else {
                if (xmlHttp.responseText != "success")
                    vipPage = xmlHttp.responseText;
                $("#spTip").css("display", "block");
                $("#tdVipTotal").css("display", "block"); //显示页数提示
                $("#spVipNow").html(pageIndex); //当前页赋值
                $("#spVipTotal").html(vipPage); //总页数赋值
                //$("#ctl00_cph_btnHideVipBind").click();
                document.getElementById('ctl00_cph_btnHideVipBind').click();
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0"); //消除浏览器中的缓存
    xmlHttp.send(null);
}
/***************单击一页显示多少条记录*******************/


function temp() {
    xmlHttp = GetXmlHttpObject();
    var url = "vipMaster.aspx?type=temp";
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            alert(xmlHttp.responseText);
        }
    }
    xmlHttp.send(null);
}
/***************单击开始查询按钮触发的事件*****************/
function startQuery(state) {
    var txt1 = $("[id$=txtText1]"), txt2 = $("[id$=txtText2]"), txt3 = $("[id$=txtText3]"), txt4 = $("[id$=txtText4]").val(), txt5 = $("[id$=txtText5]").val();
    if ($("[id$=ddlFiled1] option:selected").val() !== "无") {
        if (txt1.val().length === 0) {
            txt1.css("border", "2px solid red").focus();
            return false;
        }
        else {
            txt1.css("border", "1px solid black");
        }
    }
    if ($("[id$=ddlFiled2] option:selected").val() !== "无") {
        if (txt2.val().length === 0) {
            txt2.css("border", "2px solid red").focus();
            return false;
        }
        else {
            txt2.css("border", "1px solid black");
        }
    }
    if ($("[id$=ddlFiled3] option:selected").val() !== "无") {
        if (txt3.val().length === 0) {
            txt3.css("border", "2px solid red").focus();
            return false;
        }
        else {
            txt3.css("border", "1px solid black");
        }
    }
    if ($("[id$=ddlFiled4] option:selected").val() !== "无") {
        if (txt4.val().length === 0) {
            txt4.css("border", "2px solid red").focus();
            return false;
        }
        else {
            txt4.css("border", "1px solid black");
        }
    }
    if ($("[id$=ddlFiled5] option:selected").val() !== "无") {
        if (txt5.val().length === 0) {
            txt5.css("border", "2px solid red").focus();
            return false;
        }
        else {
            txt5.css("border", "1px solid black");
        }
    }
    move('next');
    var options = {
        url: "vipMaster.aspx?type=query",
        type: "post",
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                if (state === "default")
                    pageChanged('first');
                else
                    pageChanged(pageIndex);
            } else
                alert(responseText);
        }
    };
    $("#spTip").css("display", "block");
    $("#aspnetForm").ajaxSubmit(options);
}
/***************单击开始查询按钮触发的事件*****************/
function move(dir) {
    if (dir === "next")
        $("#rightControl").click();
    else if (dir === "preview")
        $("#leftControl").click();
}
function myCallBack(result) {
    alert(result.value);
}
function selectAll(obj) {
    var chk = $(obj).attr("checked");
    if (chk === undefined) {
        $("[id$=CheckBox1]").removeAttr("checked");
    }
    else {
        $("[id$=CheckBox1]").attr("checked", "checked");
    }
}
function alterDetails(s) {
    var txtAlter = $("[id$=txtAlter]");
    var value = txtAlter.val();
    if (value.length === 0) {
        txtAlter.css("border", "2px solid red").focus();
        return false;
    }
    else {
        txtAlter.css("border", "1px solid black");
    }
    var urll = "vipMaster.aspx?type=alter";
    if (s === "all") {
        var v = $("#ctl00_cph_ddlAlterFiled>option:selected").text();
        if (v === "手机号码" || v === "电子邮箱" || v === "姓名") {
            alert("此字段不允许批量修改.");
            return;
        }
        urll += "&state=all";
    }
    else
        urll += "&state=0";
    urll += "&filed=" + $("#ctl00_cph_ddlAlterFiled").val() + "&value=" + encodeURIComponent(value);
    urll += "&filedCN=" + encodeURIComponent($("#ctl00_cph_ddlAlterFiled>option:selected").text());
    var options = {
        url: urll,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                alert("资料修改完成");
                pageChanged("refresh"); //重新加载当前页
            } else
                alert(responseText);
        }
    };
    $("#aspnetForm").ajaxSubmit(options);
}
function alterPoints(s) {
    var addPoints = $("[id$=txtAddPoints]"), descPoints = $("[id$=txtDescPoints]"), points;
    if (addPoints.val().length !== 0 && descPoints.val().length === 0) {
        points = addPoints.val();
    }
    else if (addPoints.val().length === 0 && descPoints.val().length !== 0) {
        points = parseInt(descPoints.val());
        points = points < 0 ? points : -points;
    }
    else if (addPoints.val().length === 0 && descPoints.val().length === 0) {
        alert("请您填写增加积分或者减少积分");
        return
    }
    else {
        alert("您只能增加积分或者减少积分，不能同时增加积分和减少积分，只能填写其中一个");
        return;
    }
    var $obje = $("[id$=CheckBox1]:checked");
    if ($obje.length > 1) {
        alert("一次只能修改一个VIP的积分，请选择你要修改的VIP积分");
        return;
    }
    var urll = "vipMaster.aspx?type=alterPoints";
    if (s === "all") {
        urll += "&state=all";
    }
    else
        urll += "&state=0";
    urll += "&filed=points&value=" + points;
    urll += "&filedCN=" + encodeURIComponent("积分");
    var options = {
        url: urll,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                alert("资料修改完成");
                pageChanged("refresh"); //重新加载当前页
            } else
                alert(responseText);
        }
    };
    $("#aspnetForm").ajaxSubmit(options);
}
function smsMain(obj, state) {//obj=send,state=false/all
    if (obj === "save") {
        if ($('#ctl00_cph_txtSMSTitle').val() === "" || $('#ctl00_cph_txtSMSText').val() === "") {
            alert('保存短信前请先完善短信信息!');
            return;
        }
    }
    var smsTitle = $('#ctl00_cph_txtSMSTitle');
    var smsText = $('#ctl00_cph_txtSMSText');
    smsTitle.css("border", "1px solid black");
    var doc = document;
    doc.getElementById("titleErr").style.visibility = "hidden";
    if (obj === "send") {//发送短信
        if (smsText.val().length === 0) {
            smsText.css("border", "2px solid red").focus();
            doc.getElementById("contentErr").style.visibility = "visible";

            return;
        }
        else {
            smsText.css("border", "1px solid black");
            doc.getElementById("contentErr").style.visibility = "hidden";
        }
    }
    var adrs = "vipMaster.aspx?details=" + obj;
    if (state != "false")
        adrs += "&mode=" + state;
    if ($("#channel1").attr("checked") === true)
        adrs += "&channel=1";
    else
        adrs += "&channel=2";
    adrs += "&content=" + document.getElementById('ctl00_cph_txtSMSText').value;
    var options = {
        url: adrs,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                if (obj === "save")
                    alert("短信已保存");
                if (obj === "send")
                    alert('操作已成功,短信息正在发送.');
                $('#smsSendTips').fadeOut("fast");
            } else
                alert(responseText);
        }
    };
    $("#smsSendTips").text('短信正在处理,请稍等...').fadeTo("normal", 1);
    $("#aspnetForm").ajaxSubmit(options);
}

function addMailModel() {
    var name = $('#ctl00_cph_FileUpload1').val();
    if (name.indexOf('.html') <= 0 || name.indexOf('.htm') <= 0) {
        alert('模板只能为HTML或HTM格式.');
        return;
    }
    else {
        var tmpImg = $("[id$=FileUpload2]").val();
        if (tmpImg.length > 0) {
            if (tmpImg.indexOf(".jpg") < 0) {
                alert("请选择jpg格式的图片进行上传");
                return;
            }
        }
        var options = {
            url: "vipMaster.aspx?type=mailModel",
            success: function (responseText) {
                if (responseText.indexOf('success') >= 0) {
                    $('#a_addModel').text('模板已加载').attr("disabled", "disabled");
                    $('#hidMailState').val("1");
                } else
                    alert(responseText);
                $("#a_addModel").css("display", "block");
                $("#imgModel").css("display", "none");
            }
        };
        $("#a_addModel").css("display", "none");
        $("#imgModel").css("display", "block");
        $("#aspnetForm").ajaxSubmit(options);
    }
}

function mailSend() {
    if ($('#hidMailState').val() != "1") {
        alert("请先上传一个邮件模板!");
        return;
    }
    else {

        var options = {
            url: "vipMaster.aspx?type=mailSend",
            success: function (responseText) {
                if (responseText.indexOf('success') >= 0) {
                    $('#ctl00_cph_hidMailState').val("0");
                    alert("操作已完成.邮件发送中...");
                } else
                    alert(responseText);
                $('#sendingMail').text('发送邮件').removeAttr("disabled");
                $('#a_addModel').text('加载模板文件').removeAttr("disabled");
            }
        };
        $('#sendingMail').text('正在发送').attr("disabled", "disabled");
        $("#aspnetForm").ajaxSubmit(options);
    }
}

function insertAtCursor() {
    //IE support
    var myValue = document.getElementById("ctl00_cph_ddlProperty").options[document.getElementById("ctl00_cph_ddlProperty").selectedIndex].text;
    var myField = document.getElementById("ctl00_cph_txtSMSText");
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
    else if (myField.selectionStart || myField.selectionStart === '0') {
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

function couponTypeChanged() {
    var type = $("#ctl00_cph_ddlCouponType").val();
    if (type === "0")
        return;
    xmlHttp = GetXmlHttpObject();
    var url = "httpHandler/vipMaster_Response.ashx?type=couponTypeChanged&listItem=" + type;
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "success") {
                $("#ctl00_cph_btnHideTypeChanged").click();
            }
            $("#stips").css("display", "none");
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    $("#stips").css("display", "block");
    xmlHttp.send(null);
}
function couponSend(cs) {
    var txtEndDate = $("[id$=txtEndDate]");
    var endDate = txtEndDate.val();
    if (endDate.length === 0) {
        // txtEndDate.css("border", "2px solid red").focus();
        alert("请输入截至日期");
        return false;
    }
    var urll = "vipMaster.aspx?type=coupon";
    if (cs === "all")
        urll += "&state=all";
    else
        urll += "&state=0";
    urll += "&typeID=" + $("#ctl00_cph_ddlCTD").val();
    urll += "&endDate=" + endDate;
    var options = {
        url: urll,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                alert("优惠券发送完成!");
            } else
                alert(responseText);
            $("#trCouponTips").css("display", "none");
        }
    };
    $("#trCouponTips").css("display", "table-row");
    $("#aspnetForm").ajaxSubmit(options);
}

function showDivInvs(other) {
    if (other === 1) {
        $("#cbInvs").removeAttr("checked");
    }
    if ($("#cbInvs").attr("checked") === "checked")
        $('#divInvsMain').fadeTo("normal", 1); //显示（完全显示0-1之间表示透明度的）
    else
        $("#divInvsMain").fadeOut("fast"); //消失
}

function smsModelSave() {
    var smsTitle = $('[id$=txtSMSTitle]');
    var smsText = $('[id$=txtSMSText]');
    var smsSendTips = $("#smsSendTips");
    var doc = document;
    if (smsTitle.val().length === 0) {
        smsTitle.focus();
        doc.getElementById("titleErr").style.visibility = "visible";
        return;
    }
    else {
        doc.getElementById("titleErr").style.visibility = "hidden";
    }
    if (smsText.val().length === 0) {
        smsText.focus();
        doc.getElementById("contentErr").style.visibility = "visible";
        return;
    }
    else {
        doc.getElementById("contentErr").style.visibility = "hidden";
    }
    var adrs = "httpHandler/vipMaster_Response.ashx?type=smsMO&mode=save&Title=" + smsTitle.val() + "&Text=" + smsText.val();
    var options = {
        url: adrs,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                $("[id$=ddlSMSlist]").append("<option value='" + smsTitle.val() + "'>" + smsTitle.val() + "</option>");
                alert("新短信模板保存成功");
                smsSendTips.fadeOut("fast");
            } else
                alert(responseText);
        }
    };
    smsSendTips.text('短信模板正在保存...').fadeTo("normal", 1);
    $("#aspnetForm").ajaxSubmit(options);
}
function smsModelRead() {
    var txt = $("[id$=ddlSMSlist]>option:selected").text();
    if (txt === '--无--') {
        document.getElementById('ctl00_cph_txtSMSTitle').value = '';
        document.getElementById('ctl00_cph_txtSMSText').value = '';
        return;
    }
    xmlHttp = GetXmlHttpObject();
    var url = "httpHandler/vipMaster_Response.ashx?type=smsMO&mode=read&value=" + encodeURI(txt);
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('error') >= 0) {
                alert(xmlHttp.responseText);
            } else {
                document.getElementById('ctl00_cph_txtSMSTitle').value = txt;
                document.getElementById('ctl00_cph_txtSMSText').value = xmlHttp.responseText;
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}

function smsModelDelete() {
    var txt = $("[id$=ddlSMSlist] option:selected").text();
    if (txt === '--无--') {
        return;
    }
    var tips = $("#smsSendTips");
    var adrs = "httpHandler/vipMaster_Response.ashx?type=smsMO&mode=del&value=" + encodeURI(txt);
    var options = {
        url: adrs,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                $("[id$=ddlSMSlist] option:selected").remove();
                $('[id$=txtSMSText]').val('');
                $('[id$=txtSMSTitle]').val('');
                alert("短息模板已删除!");
                tips.fadeOut("fast");
            } else
                alert(responseText);
        }
    };
    tips.text('短信模板正在删除...').fadeTo("normal", 1);
    $("#aspnetForm").ajaxSubmit(options);
}