/// <reference path="jquery-1.4.2-vsdoc.js" />


var vipPage = 1;
var pageIndex = 1;
function pageChanged(state) {
    //初始化按钮使用状态
    $("#pcFirst").removeAttr("disabled");
    $("#pcPreview").removeAttr("disabled");
    $("#pcNext").removeAttr("disabled");
    $("#pcLast").removeAttr("disabled");

    if (state == "last")
        pageIndex = vipPage;
    else if (state == "preview")
        pageIndex--;
    else if (state == "next")
        pageIndex++;
    else if (state == "first") {
        pageIndex = 1;
    }
    else if (state == "goto") {
        if ($("#txtGoto").val() == "" || $("#txtGoto").val() == "0" || $("#txtGoto").val() > vipPage) {
            alert('请输入正确的跳转页面');
            return;
        }
        pageIndex = $("#txtGoto").val();
    }
    if (pageIndex == 1) {
        $("#pcFirst").attr("disabled", "true");
        $("#pcPreview").attr("disabled", "true");
    } else if (pageIndex == vipPage) {
        $("#pcNext").attr("disabled", "true");
        $("#pcLast").attr("disabled", "true");
    }
    //alert(pageIndex);
    xmlHttp = GetXmlHttpObject();
    var url = "salesParticular.aspx?type=paging&pageIndex=" + pageIndex;
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
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
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}

function startQuery(obj) {
    var options = {
        url: "salesParticular.aspx?type=query",
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                pageChanged('first');
                secBoard(1);
                $(obj).text("按条件查询").attr("disabled", false);
            } else
                alert(responseText);
        }
    };
    $(obj).text("正在查询...").attr("disabled", true);
    $("#aspnetForm").ajaxSubmit(options);
}

function showDetails(id) {
    xmlHttp = GetXmlHttpObject();
    var url = "salesParticular.aspx?type=details&id=" + id;
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4) {
            if (xmlHttp.responseText == "success") {
                document.getElementById('ctl00_cph_btnHideDetails').click();

            } else {
                alert(xmlHttp.responseText);
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}

/*****************重置查询条件*********************/
function resetConditon() {

    $("[id$=ddlFiled1] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlFiled2] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlFiled3] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlFiled4] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlFiled5] option:eq(0)").attr("selected", "selected");

    $("[id$=ddlCondition1] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlCondition2] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlCondition3] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlCondition4] option:eq(0)").attr("selected", "selected");
    $("[id$=ddlCondition5] option:eq(0)").attr("selected", "selected");

    $("[id$=txtText1]").val("").removeClass("blankCondition");
    $("[id$=txtText2]").val("").removeClass("blankCondition");
    $("[id$=txtText3]").val("").removeClass("blankCondition");
    $("[id$=txtText4]").val("").removeClass("blankCondition");
    $("[id$=txtText5]").val("").removeClass("blankCondition");
}
/*****************重置查询条件*********************/

/*****************以下是可以跨浏览器的*********************/
function secBoard(n) {
    for (var i = 0, cellNum = $("#secTable td").length; i < cellNum; i++) {
        $("#secTable td").eq(i).removeClass("sec2").addClass("sec1");
    }
    $("#secTable td").eq(n).removeClass("sec1").addClass("sec2");
    for (var i = 0, tbodyNum = $("#mainTable tbody").length; i < tbodyNum; i++) {
        $("#mainTable >tbody").eq(i).css("display", "none");
    }
    $("#mainTable >tbody").eq(n).css("display", "table-row-group");
}

/*****************以下是可以跨浏览器的*********************/

function selectAll(obj) {
    var temp = $(obj).attr("checked");
    if (temp === undefined) {
        $("[id$=ckb]").removeAttr("checked");
    }
    else {
        $("[id$=ckb]").attr("checked", "checked");
    }
}
/*初始化短信*/

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
    var adrs = "salesParticular.aspx?type=" + obj;
    if (state != "false")
        adrs += "&mode=" + state;
    if ($("#channel1").attr("checked") === "checked")
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
    $("#smsSendTips").text('短信处理中,请稍等...').fadeTo("normal", 1);
    $("#aspnetForm").ajaxSubmit(options);
}

function addMailModel() {
    var name = $('#ctl00_cph_FileUpload1').val();
    if (name.indexOf('.html') <= 0 || name.indexOf('.htm') <= 0) {
        alert('模板只能为HTML或HTM格式.');
        return;
    }
    else {
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

function smsModelSave() {
    var smsTitle = $('#ctl00_cph_txtSMSTitle');
    var smsText = $('#ctl00_cph_txtSMSText');
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
                alert("新模板保存成功");
                $("#smsSendTips").fadeOut("fast");
            } else
                alert(responseText);
        }
    };
    $("#smsSendTips").text('短信正在保存...').fadeTo("normal", 1);
    $("#aspnetForm").ajaxSubmit(options);
}

function smsModelRead() {
    var txt = ($("#ctl00_cph_ddlSMSlist>option:selected").text());
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
    var txt = $("#ctl00_cph_ddlSMSlist >option:selected").text();
    if (txt === '--无--') {
        return;
    }
    var tips = $("#smsSendTips");
    var adrs = "httpHandler/vipMaster_Response.ashx?type=smsMO&mode=del&value=" + $("#ctl00_cph_ddlSMSlist>option:selected").text();
    var options = {
        url: adrs,
        success: function (responseText) {
            if (responseText.indexOf('success') >= 0) {
                $("[id$=ddlSMSlist] option:selected").remove();
                $('[id$=txtSMSText]').val('');
                $('[id$=txtSMSTitle]').val('');
                alert("短信模板已删除!");
                tips.fadeOut("fast");
            } else
                alert(responseText);
        }
    };
    tips.text('短信模板正在删除...').fadeTo("normal", 1);
    $("#aspnetForm").ajaxSubmit(options);
}