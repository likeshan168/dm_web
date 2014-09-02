




function setTab(name, cursel, n) {
    for (i = 1; i <= n; i++) {
        var menu = document.getElementById(name + i);
        var con = document.getElementById("con_" + name + "_" + i);
        menu.className = i === cursel ? "hover" : "";
        con.style.display = i === cursel ? "block" : "none";
    }
}

function placeChanged() {
    if ($('#ctl00_cph_ddlUsedPlace').val() === "other")
        $('#trOther').css("display", "table-row");
    else
        $('#trOther').css("display", "none");
}

function typeChanged() {
    if ($('#ctl00_cph_ddlCouponType').val() === "GOODS") {
        $('#trArticle').css("display", "table-row");
        $('#trArtiCode').css("display", "table-row");
        $('#trMoney').css("display", "none");
    }
    else {
        $('#trArticle').css("display", "none");
        $('#trArtiCode').css("display", "none");
        $('#trMoney').css("display", "table-row");
    }
}

function validateNum(input) {
    var pattern = /[0-9]+\.?[0-9]*/;
    if (!pattern.test(input)) {
        return false;
    }
    else {
        return true;
    }
}

/******************注册优惠劵类型**********************/
function newTypeReg(place, object) {
    if ($("#ctl00_cph_ddlUsedPlace").val() === "none") {
        alert('请选择使用地点');
        return;
    }
    var score = $("#ctl00_cph_txtScore").val();
    var money = $("#ctl00_cph_txtMoney").val();
    if (!validateNum(score)) {
        alert("积分只能为数字,请重新输入");
        return false;
    }
    if (money.length !== 0) {
        if (!validateNum(money)) {
            alert("金额只能为数字,请重新输入");
            return false;
        }
    }

    xmlHttp = GetXmlHttpObject();
    var url = "httpHandler/couponManager_Response.ashx?type=regType";
    url += "&couponType=" + $("#ctl00_cph_ddlCouponType").val();
    url += "&cdt=" + encodeURIComponent($("#ctl00_cph_txtCDT").val());
    url += "&usedPlace=" + encodeURIComponent($("#ctl00_cph_ddlUsedPlace").val());
    url += "&placeOther=" + encodeURIComponent($("#ctl00_cph_txtPlaceOther").val());

    url += "&score=" + score;
    url += "&money=" + money;
    url += "&article=" + encodeURIComponent($("#ctl00_cph_txtArticle").val());
    url += "&articode=" + encodeURIComponent($("#ctl00_cph_txtArtiCode").val());
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('success') >= 0) {
                $("#ctl00_cph_txtCDT").val("");
                $("#ctl00_cph_txtPlaceOther").val("");
                $("#ctl00_cph_txtScore").val("");
                $("#ctl00_cph_txtMoney").val("");
                $("#ctl00_cph_txtArticle").val("");
                $("#ctl00_cph_txtArtiCode").val("");
                $("#ctl00_cph_btnHide").click();
                alert("新类型注册成功.");
            } else
                alert(xmlHttp.responseText);
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}
/******************注册优惠劵类型**********************/


function save() {
    xmlHttp = GetXmlHttpObject();
    var url = "couponManager.aspx?type=save";
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('success') >= 0) {
                alert('保存修改成功');
            } else
                alert(xmlHttp.responseText);
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
}

function createPC() {
    if ($("#ctl00_cph_txtpc").val() < 1 || $("#ctl00_cph_txtpc").val() > 1000000) {
        alert('请输入十万以内的数字');
        return;
    }
    xmlHttp = GetXmlHttpObject();
    var url = "couponManager.aspx?type=createPC";
    url += "&count=" + $("#ctl00_cph_txtpc").val();
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('success') >= 0) {
                $("#ctl00_cph_lbpc").text(xmlHttp.responseText.substring(xmlHttp.responseText.indexOf('-') + 1));
                alert('处理完成');
            } else
                alert(xmlHttp.responseText);
            $("#apc").text("开始生成");
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    $("#apc").text("处理中,请稍候……");
    xmlHttp.send(null);
}



function selStation(locationid, id, ev) {
    //层定位
    //    var Obj = document.getElementById(locationid);
    ev = ev || window.event;
    var x, y;
    if (ev.pageX || ev.pageY) {
        x = ev.pageX;
        y = ev.pageY;

    }
    else {
        x = ev.clientX + document.body.scrollLeft - document.body.clientLeft;
        y = ev.clientY + document.body.scrollTop - document.body.clientTop;
    }

    //    Obj.style.left = event.x;
    //    Obj.style.top = event.y;
    //Ajax数据查询
    xmlHttp = GetXmlHttpObject();
    var url = "couponManager.aspx?type=PCdetails&cid=" + id;
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('success') >= 0) {
                var r = xmlHttp.responseText.replace("success:", "");
                var resArray = r.split(',');
                $("#tdNo").text(resArray[0]);
                $("#tdType").text(resArray[1]);
                $("#tdEDate").text(resArray[2]);
                $("#tdUsedDate").text(resArray[3]);
                $("#tdUsedPlace").text(resArray[4]);
                $("#tdUsed").text(resArray[5]);
                $("#tdScore").text(resArray[6]);
                $("#tdMobile").text(resArray[7]);
                if (resArray[8] == "true") { //说明已经过期了
                    $("#a_exchange").text("优惠券已经过期");
                    $("#a_send").attr("disabled", true);
                    $("#a_exchange").attr("disabled", true);
                }
                if (resArray[9] == "True") { //已经兑换了
                    $("#a_exchange").text("优惠券已经兑换");
                    $("#a_send").attr("disabled", true);
                    $("#a_exchange").attr("disabled", true);
                }

            } else
                alert(xmlHttp.responseText);
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
    //显示
    //    Obj.style.display = "block";
    $("#" + locationid).css({ "position": "absolute", "left": x, "top": y, "display": "block" });
}


function closeDiv(obj) {
    $("#tdNo").text("加载中...");
    $("#tdType").text("加载中...");
    $("#tdEDate").text("加载中...");
    $("#tdUsedDate").text("加载中...");
    $("#tdUsedPlace").text("加载中...");
    $("#tdUsed").text("加载中...");
    $("#tdScore").text("加载中...");
    $("#tdMobile").text("加载中...");
    document.getElementById(obj).style.display = "none";
    $("#a_send").attr("disabled", false);
}

function reSendCoupon() {
    xmlHttp = GetXmlHttpObject();
    var url = "couponManager.aspx?type=reSendCoupon&cid=" + $("#tdNo").text(); ;
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('success') >= 0) {
                $("#a_send").text("发送完成.");
                $("#a_send").attr("disabled", true);
            } else {
                $("#a_send").text("发送失败,点击重试.");
                $("#a_send").attr("disabled", false);
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
    $("#a_send").text("正在发送...");
    $("#a_send").attr("disabled", true);
}
/*----------新增的优惠券兑换功能---------------*/
function Exchange() {
    xmlHttp = GetXmlHttpObject();
    var url = "couponManager.aspx?type=Exchange&cid=" + $("#tdNo").text();
    xmlHttp.open("GET", url, true);
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText.indexOf('success') >= 0) {
                $("#a_exchange").text("兑换成功.");
                $("[id$=btnQuery]").click();
                $("#a_exchange").attr("disabled", true);
            } else {
                $("#a_exchange").text("兑换失败,点击重试.");
                $("#a_exchange").attr("disabled", false);
            }
        }
    }
    xmlHttp.setRequestHeader("If-Modified-Since", "0");
    xmlHttp.send(null);
    $("#a_exchange").text("正在兑换...");
    $("#a_exchange").attr("disabled", true);
}