var divPage = 1;
function btnNext_Click() {
    $("#div" + divPage).css("display", "none");
    divPage++;
    if ($("#type1").attr("checked") === true) {
        $("#tableDefault").css("display", "none");
        $("#tableRadio").css("display", "none");
        $("#tableFormula").css("display", "block");
    } else if ($("#type6").attr("checked") === true) {
        $("#tableDefault").css("display", "none");
        $("#tableFormular").css("display", "none");
        $("#tableRadio").css("display", "block");
    }
    else {
        var v = "";
        for (var i = 1; i <= 12; i++) {
            var o = document.getElementById('type' + i.toString());
            if (o.checked)
                v = o.value;
        }
        $("#tableDefault").css("display", "block");
        $("#tableFormular").css("display", "none");
        $("#tableRadio").css("display", "none");
        //        $("#ctl00_cph_txtFiledType").val(v);
        $("[id$=txtFiledType]").val(v);
    }
    $("#div" + divPage).css("display", "block");
}

function btnPreview_Click() {
    $('#div' + divPage).css("display", "none");
    divPage--;
    $('#div' + divPage).css("display", "block");
}

function nameCheck() {
    var re = /[\u4e00-\u9fa5]+/;
    $("#nameTips").css("display", "block");
    if (re.test($("[id$=txtFiledName]").val())) {
        $("#nameTips").css("color", "red").html("字段名称不能包含中文.");
        return;
    } else {
        xmlHttp = GetXmlHttpObject();
        var url = "newCustomField.aspx?mtype=nameCheck&name=" + $("[id$=txtFiledName]").val();
        xmlHttp.open("GET", url, true);
        xmlHttp.onreadystatechange = function () {
            if (xmlHttp.readyState === 4) {
                if (xmlHttp.responseText === "success") {
                    $("#nameTips").css("color", "lime").html("此名称可以使用.");
                } else if (xmlHttp.responseText === "used") {
                    $("#nameTips").css("color", "red").html("此名称已被占用,请更换名称后再试.");
                } else {
                    alert(xmlHttp.responseText);
                }
            }
        }
        xmlHttp.setRequestHeader("If-Modified-Since", "0");
        xmlHttp.send(null);
    }
}

function addNewField(obj) {
    nameCheck();
    xmlHttp = GetXmlHttpObject();
    var url = "";
    if (obj === "default")
        url = "newCustomField.aspx?mtype=addField&name=" + $("#ctl00_cph_txtFiledName").val() + "&length=" +
        $("#ctl00_cph_txtFieldLenth").val() + "&type=" + encodeURIComponent($("#ctl00_cph_txtFiledType").val()) + "&default=" +
        encodeURIComponent($("#ctl00_cph_txtFieldDefault").val()) + "&cname=" + encodeURIComponent($("#ctl00_cph_txtFieldCNname").val());
    else if (obj === "radio") {
        url = "newCustomField.aspx?mtype=addField&name=" + $("#ctl00_cph_txtRadioName").val() +
        "&type=" + encodeURIComponent($("#ctl00_cph_txtRadioType").val()) + "&cname=" + encodeURIComponent($("#ctl00_cph_txtRadioCNname").val()) + "&default=";
        if ($("#ctl00_cph_radio1").attr("checked") === true)
            url += "1";
        else
            url += "0";
    }
   
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "success") {
                alert("新增字段成功.\n可在VIP资料查询页面修改具体数值.");
            } else {
                alert(xmlHttp.responseText);
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}

function addFormulaField() {
    xmlHttp = GetXmlHttpObject();
    var url = "newCustomField.aspx?mtype=addFormulaField&name=" + $("#ctl00_cph_txtFormulaName").val() +
        "&cname=" + encodeURIComponent($("#ctl00_cph_txtFormulaCName").val()) + "&mainText=" + $("#ctl00_cph_txtMain").val() + "&type=";
    if ($("#ctl00_cph_fr1").attr("checked") === true)
        url += "datetime";
    else if ($("#ctl00_cph_fr2").attr("checked") === true)
        url += "int";
    else if ($("#ctl00_cph_fr3").attr("checked") === true)
        url += "float";
    else
        url += "string";
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "success") {
                alert("新增公式字段成功.");
            } else {
                alert(xmlHttp.responseText);
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}

function insertAtCursor() {
    //IE support
    var myValue = "[" + document.getElementById("ctl00_cph_ddlField").options[document.getElementById("ctl00_cph_ddlField").selectedIndex].value + "]";
    var myField = document.getElementById("ctl00_cph_txtMain");
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