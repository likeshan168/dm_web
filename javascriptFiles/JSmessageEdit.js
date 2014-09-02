function wordsCount() {
    if ($("#ctl00_cph_txtMessage").val() != "") {
        $("#lblUsed").text($("#ctl00_cph_txtMessage").val().length);
        $("#lblunUsed").text(130 - $("#ctl00_cph_txtMessage").val().length);
    } else {
    $("#lblUsed").text("0");
    $("#lblunUsed").text("130");
    }
}

function insertAtCursor() {
    //IE support
    var myValue = document.getElementById("ctl00_cph_ddlMsgField").options[document.getElementById("ctl00_cph_ddlMsgField").selectedIndex].text;
    var myField = document.getElementById("ctl00_cph_txtMessage");
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
    //增加特殊占位符字数统计
    //wordsCount(document.getElementById("ctl00_ContentPlaceHolder1_ddlMsgField").value)
    wordsCount();
}