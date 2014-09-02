var vip = false, shop = false, saler = false, vipCard = false, vipKdt = false, vipKdtSale = false, i = 0, j = 0, x = 0, v = 0, k = 0, s = 0;
var xmlHttp, ba_vip, ba_saler, ba_shop, ba_vipCard, ba_vipKdt, ba_vipKdtSale;
var doc = document;
function start_vip(time1) {
    ba_vip = setInterval("progress_bar_vip()", time1);
}
function start_saler(time2) {
    ba_saler = setInterval("progress_bar_saler()", time2);
}
function start_shop(time3) {
    ba_shop = setInterval("progress_bar_shop()", time3);
}
/*新增的是为了下载vip卡号的*/
function start_vipCard(time4) {
    ba_vipCard = setInterval("progress_bar_vipCard()", time4);
}

/*新增的是为了下载vip口袋通信息的*/
function start_vipKdt(time5) {
    ba_vipKdt = setInterval("progress_bar_vipKdt()", time5);
}

/*新增的是为了下载vip口袋通销售信息的*/
function start_vipKdtSale(time6) {
    ba_vipKdtSale = setInterval("progress_bar_vipKdtSale()", time6);
}

function startDownLoad() {
    xmlHttp = GetXmlHttpObject();

    if (xmlHttp === null) {
        alert("您的浏览器不支持AJAX！");
        return;
    }
    initControl();

    if (doc.getElementById("cbVIP").checked === true) {
        startVIP();
    }
    else if (doc.getElementById("cbSaler").checked === true) {
        vip = true;
        startSaler();
    }
    else if (doc.getElementById("cbShop").checked === true) {
        vip = true;
        saler = true;
        startShop();
    }
    /*新增的是用来下载vip卡号信息*/
    else if (doc.getElementById("cbVipCard").checked === true) {
        // alert("开始vip制卡下载");
        vip = true;
        saler = true;
        shop = true;
        startVipCardDown();
    }
    /*新增的是用来下载vip口袋通信息*/
    else if (doc.getElementById("cbVipKdt").checked === true) {
        vip = true;
        saler = true;
        shop = true;
        vipCard = true;
        startVIPKdt();
    }
    /*新增的是用来下载vip口袋通销售信息*/
    else if (doc.getElementById("cbVipKdtSale").checked === true) {
        vip = true;
        saler = true;
        shop = true;
        vipCard = true;
        vipKdt = true;
        startVIPKdtSale();
    }
}

function progress_bar_vip() {
    i++;
    if (i <= 99) {
        doc.getElementById("in_vip").style.width = i + "%";
        doc.getElementById("in_0_vip").innerHTML = "已完成" + i + "%";
    }
    else {
        clearInterval(ba_vip);
    }
}
function progress_bar_saler() {
    x++;
    if (x <= 99) {
        doc.getElementById("in_saler").style.width = x + "%";
        doc.getElementById("in_0_saler").innerHTML = "已完成" + x + "%";
    }
    else {
        clearInterval(ba_saler);
    }
}
function progress_bar_shop() {
    j++;
    if (j <= 99) {
        doc.getElementById("in_shop").style.width = j + "%";
        doc.getElementById("in_0_shop").innerHTML = "已完成" + j + "%";
    }
    else {
        clearInterval(ba_shop);
    }
}
//progress_bar_vipCard
/*新增的是为了下载vip卡号用的*/
function progress_bar_vipCard() {
    v++;
    if (v <= 99) {
        doc.getElementById("in_vipCard").style.width = v + "%";
        doc.getElementById("in_0_vipCard").innerHTML = "已完成" + v + "%";
    }
    else {
        clearInterval(ba_vipCard);
    }
}

//progress_bar_vipKdt
/*新增的是为了下载vip口袋通用的*/
function progress_bar_vipKdt() {
    k++;
    if (k <= 99) {
        doc.getElementById("in_vipKdt").style.width = k + "%";
        doc.getElementById("in_0_vipKdt").innerHTML = "已完成" + k + "%";
    }
    else {
        clearInterval(ba_vipKdt);
    }
}
/*新增的是为了下载vip口袋通销售数据用的*/
function progress_bar_vipKdtSale() {
    s++;
    if (s <= 99) {
        doc.getElementById("in_vipKdtSale").style.width = s + "%";
        doc.getElementById("in_0_vipKdtSale").innerHTML = "已完成" + s + "%";
    }
    else {
        clearInterval(ba_vipKdtSale);
    }
}

function initControl() {
    vip = false; shop = false; saler = false; vipCard = false; vipKdt = false; vipKdtSale = false; i = 0; j = 0; x = 0; v = 0; k = 0, s = 0;


    doc.getElementById("in_vip").style.width = "0%";
    doc.getElementById("in_0_vip").innerHTML = "已完成0%";
    doc.getElementById("in_shop").style.width = "0%";
    doc.getElementById("in_0_shop").innerHTML = "已完成0%";
    doc.getElementById("in_saler").style.width = "0%";
    doc.getElementById("in_0_saler").innerHTML = "已完成0%";
    doc.getElementById('spanVip').style.color = "gray";
    doc.getElementById('spanSaler').style.color = "gray";
    doc.getElementById('spanShop').style.color = "gray";
    doc.getElementById('spanVip').innerHTML = "等待进行会员资料同步";
    doc.getElementById('spanSaler').innerHTML = "等待进行营业员资料同步";
    doc.getElementById('spanShop').innerHTML = "等待进行门店资料同步";

    /*新增的是为了下载vip卡号用的*/
    doc.getElementById("in_vipCard").style.width = "0%";
    doc.getElementById("in_0_vipCard").innerHTML = "已完成0%";
    doc.getElementById('spanVipCard').style.color = "gray";
    doc.getElementById('spanVipCard').innerHTML = "等待进VIP制卡资料同步";

    /*新增的是为了下载vip口袋通信息用的*/
    doc.getElementById("in_vipKdt").style.width = "0%";
    doc.getElementById("in_0_vipKdt").innerHTML = "已完成0%";
    doc.getElementById('spanVipKdt').style.color = "gray";
    doc.getElementById('spanVipKdt').innerHTML = "等待进VIP口袋通信息同步";

    /*新增的是为了下载vip口袋通信息用的*/
    doc.getElementById("in_vipKdtSale").style.width = "0%";
    doc.getElementById("in_0_vipKdtSale").innerHTML = "已完成0%";
    doc.getElementById('spanVipKdtSale').style.color = "gray";
    doc.getElementById('spanVipKdtSale').innerHTML = "等待进VIP口袋通销售信息同步";
}

function startVIP() {
    var url = "DataDownload_Response.aspx?type=vip&date=" + new Date().getTime();
    doc.getElementById("divBG").style.display = "";
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "vipFinished") {
                doc.getElementById('spanVip').style.color = "green";
                doc.getElementById('spanVip').innerHTML = "已完成会员资料同步";
                clearInterval(ba_vip);
                doc.getElementById("in_vip").style.width = "100%";
                doc.getElementById("in_0_vip").innerHTML = "已完成100%";
                vip = true;
            } else {
                //停止此进度条.显示警告
                vip = xmlHttp.responseText;
                clearInterval(ba_vip);
                doc.getElementById('spanVip').innerHTML = "VIP资料下载失败";
            }
            if (doc.getElementById("cbSaler").checked === true) {
                startSaler();
            } else {
                saler = true;
                if (document.getElementById("cbShop").checked === true) {
                    startShop();
                } else {
                    shop = true;
                    if (doc.getElementById("cbVipCard").checked === true) {
                        startVipCardDown();
                    }
                    else {
                        vipCard = true;
                        if (doc.getElementById("cbVipKdt").checked === true) {
                            startVIPKdt();
                        } else {//新增口袋通
                            vipKdt = true;
                            if (doc.getElementById("cbVipKdtSale").checked === true) {
                                startVIPKdtSale();
                            } else {
                                vipKdtSale = true;
                            }
                        }
                    }
                }
            }
            if (vip === true && shop === true && saler === true && vipCard === true && vipKdt === true && vipKdtSale === true) {
                doc.getElementById("divBG").style.display = "none";
                initControl();
                alert('所选资料同步完成');
            } else if (doc.getElementById("cbSaler").checked !== true && doc.getElementById("cbShop").checked !== true && doc.getElementById("cbVipCard").checked !== true && doc.getElementById("cbVipKdt").checked !== true && doc.getElementById("cbVipKdtSale").checked !== true) {
                alert(vip);
                doc.getElementById("divBG").style.display = "none";
            }
        }
    };
    xmlHttp.open("GET", url, true);
    doc.getElementById('spanVip').style.color = "red";
    doc.getElementById('spanVip').innerHTML = "正在进行会员资料同步";
    //start_vip(6000); //6秒增加1%,共需600秒. 匹配后台10分钟   
    start_vip(1800); //1.8秒增加1%,共需180秒，180/1.8=100,180s=3m
    xmlHttp.send(null);
}


//营业员
function startSaler() {
    var url = "DataDownload_Response.aspx?type=saler&date=" + new Date().getTime();
    doc.getElementById("divBG").style.display = "";
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "salerFinished") {
                doc.getElementById('spanSaler').style.color = "green";
                doc.getElementById('spanSaler').innerHTML = "已完成营业员资料同步";
                clearInterval(ba_saler);
                doc.getElementById("in_saler").style.width = "100%";
                doc.getElementById("in_0_saler").innerHTML = "已完成100%";
                saler = true;
            } else {
                //停止此进度条.显示警告
                saler = xmlHttp.responseText; //这里是从后台返回的数据
                clearInterval(ba_saler);
                doc.getElementById('spanSaler').innerHTML = "营业员资料下载失败";
            }
            if (document.getElementById("cbShop").checked === true) {
                startShop();
            }
            else {
                shop = true;
                if (doc.getElementById("cbVipCard").checked === true) {
                    //alert("vip制卡下载");
                    startVipCardDown();
                }
                else {
                    vipCard = true;
                    if (doc.getElementById("cbVipKdt").checked === true) {
                        startVIPKdt();
                    } else {
                        vipKdt = true;
                        if (doc.getElementById("cbVipKdtSale").checked === true) {
                            startVIPKdtSale();
                        } else {
                            vipKdtSale = true;
                        }
                    }
                }
            }
            if (vip === true && shop === true && saler === true && vipCard === true && vipKdt === true && vipKdtSale === true) {
                doc.getElementById("divBG").style.display = "none";
                initControl();
                alert('所选资料同步完成');
            } else if (document.getElementById("cbShop").checked !== true && doc.getElementById("cbVipCard").checked !== true && doc.getElementById("cbVipKdt").checked !== true && doc.getElementById("cbVipKdtSale").checked !== true) {

                if (vip != true)
                    alert(vip);
                if (saler != true)
                    alert(saler);
                doc.getElementById("divBG").style.display = "none";
            }
        }
    };
    xmlHttp.open("GET", url, true);
    doc.getElementById('spanSaler').style.color = "red";
    doc.getElementById('spanSaler').innerHTML = "正在进行营业员资料同步";
    start_saler(600); //0.6秒增加1%,共需60秒. 匹配后台1分钟
    xmlHttp.send(null);
}

function startShop() {
    var url = "DataDownload_Response.aspx?type=shop&date=" + new Date().getTime();
    document.getElementById("divBG").style.display = "";
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "shopFinished") {
                document.getElementById('spanShop').style.color = "green";
                document.getElementById('spanShop').innerHTML = "已完成门店资料同步";
                clearInterval(ba_shop);
                document.getElementById("in_shop").style.width = "100%";
                document.getElementById("in_0_shop").innerHTML = "已完成100%";
                shop = true;
            } else {
                //停止此进度条.显示警告
                shop = xmlHttp.responseText; //这里显示从后台返回的数据
                clearInterval(ba_shop);
                document.getElementById('spanShop').innerHTML = "门店资料下载失败";
            }
            if (doc.getElementById("cbVipCard").checked === true) {
                startVipCardDown();
            }
            else {
                vipCard = true;
                if (doc.getElementById("cbVipKdt").checked === true) {
                    startVIPKdt();
                } else {
                    vipKdt = true;
                    if (doc.getElementById("cbVipKdtSale").checked === true) {
                        startVIPKdtSale();
                    } else {
                        vipKdtSale = true;
                    }
                }
            }

            if (vip === true && shop === true && saler === true && vipCard === true && vipKdt === true && vipKdtSale === true) {
                doc.getElementById("divBG").style.display = "none";
                initControl();
                alert('所选资料同步完成');
            } else if (doc.getElementById("cbVipCard").checked !== true && doc.getElementById("cbVipKdt").checked !== true && doc.getElementById("cbVipKdtSale").checked !== true) {

                if (vip != true)
                    alert(vip);
                if (saler != true)
                    alert(saler);
                if (shop != true)
                    alert(shop);
                doc.getElementById("divBG").style.display = "none";
            }

            //initControl();
        }
    };
    xmlHttp.open("GET", url, true);
    document.getElementById('spanShop').style.color = "red";
    document.getElementById('spanShop').innerHTML = "正在进行门店资料同步";
    start_shop(600); //0.6秒增加1%,共需60秒. 匹配后台1分钟
    xmlHttp.send(null);
}

/*新增是用来下载vip卡号的*/
function startVipCardDown() {
    // alert("123");
    var url = "DataDownload_Response.aspx?type=vipCard&date=" + new Date().getTime();
    document.getElementById("divBG").style.display = "";
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            if (xmlHttp.responseText === "vipCardFinished") {
                document.getElementById('spanVipCard').style.color = "green";
                document.getElementById('spanVipCard').innerHTML = "已完ViP制卡资料同步";
                clearInterval(ba_vipCard);
                document.getElementById("in_vipCard").style.width = "100%";
                document.getElementById("in_0_vipCard").innerHTML = "已完成100%";
                vipCard = true;
            } else {
                //停止此进度条.显示警告
                vipCard = xmlHttp.responseText;
                clearInterval(ba_vipCard);
                document.getElementById('spanVipCard').innerHTML = "ViP制卡下载失败";
            }

            if (doc.getElementById("cbVipKdt").checked === true) {
                startVIPKdt();
            }
            else {
                vipKdt = true;
                if (doc.getElementById("cbVipKdtSale").checked === true) {
                    startVIPKdtSale();
                } else {
                    vipKdtSale = true;
                }
            }

            if (vip === true && shop === true && saler === true && vipCard === true && vipKdt === true && vipKdtSale === true) {
                doc.getElementById("divBG").style.display = "none";
                initControl();
                alert('所选资料同步完成');
            } else if (doc.getElementById("cbVipKdt").checked !== true && doc.getElementById("cbVipKdtSale").checked !== true) {
                if (vip != true)
                    alert(vip);
                if (saler != true)
                    alert(saler);
                if (shop != true)
                    alert(shop);
                if (vipCard != true)
                    alert(vipCard);
                doc.getElementById("divBG").style.display = "none";

            }

        }
    };
    xmlHttp.open("GET", url, true);
    document.getElementById('spanVipCard').style.color = "red";
    document.getElementById('spanVipCard').innerHTML = "正在进行ViP制卡同步";
    start_vipCard(600); //0.6秒增加1%,共需60秒. 匹配后台1分钟
    xmlHttp.send(null);
}


///author:李克善
///date:2014-08-18
///下载口袋通中的vip数据
function startVIPKdt() {
    var url = "DataDownload_Response.aspx?type=vipkdt&date=" + new Date().getTime();
    doc.getElementById("divBG").style.display = "";
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            console.log(xmlHttp.responseText);
            if (xmlHttp.responseText === "vipKdtFinished") {
                doc.getElementById('spanVipKdt').style.color = "green";
                doc.getElementById('spanVipKdt').innerHTML = "已完成vip口袋通信息同步";
                clearInterval(ba_vipKdt);
                doc.getElementById("in_vipKdt").style.width = "100%";
                doc.getElementById("in_0_vipKdt").innerHTML = "已完成100%";
                vipKdt = true;
            } else {
                //停止此进度条.显示警告
                vipKdt = xmlHttp.responseText;
                clearInterval(ba_vipKdt);
                doc.getElementById('spanVipKdt').innerHTML = "vip口袋通信息下载失败";
            }
            if (doc.getElementById("cbVipKdtSale").checked === true) {
                startVIPKdtSale();
            }
            else {
                vipKdtSale = true;
            }
            if (vip === true && shop === true && saler === true && vipCard === true && vipKdt === true && vipKdtSale === true) {
                doc.getElementById("divBG").style.display = "none";
                initControl();
                alert('所选资料同步完成');
            } else if (doc.getElementById("cbVipKdtSale").checked !== true) {

                if (vip != true)
                    alert(vip);
                if (saler != true)
                    alert(saler);
                if (shop != true)
                    alert(shop);
                if (vipCard != true)
                    alert(vipCard);
                if (vipKdt != true)
                    alert(vipKdt);
                doc.getElementById("divBG").style.display = "none";
                //initControl();
            }

        }
    };
    xmlHttp.open("GET", url, true);
    doc.getElementById('spanVipKdt').style.color = "red";
    doc.getElementById('spanVipKdt').innerHTML = "正在进行vip口袋通信息同步";
    //start_vipKdt(6000); //6秒增加1%,共需600秒. 匹配后台10分钟   
    start_vipKdt(300);

    xmlHttp.send(null);
}



///author:李克善
///date:2014-08-18
///下载口袋通中的vip销售数据
function startVIPKdtSale() {
    var url = "DataDownload_Response.aspx?type=vipkdtSale&date=" + new Date().getTime();
    doc.getElementById("divBG").style.display = "";
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState === 4) {
            console.log(xmlHttp.responseText);
            if (xmlHttp.responseText === "vipKdtSaleFinished") {
                doc.getElementById('spanVipKdtSale').style.color = "green";
                doc.getElementById('spanVipKdtSale').innerHTML = "已完成vip口袋通销售信息同步";
                clearInterval(ba_vipKdt);
                doc.getElementById("in_vipKdtSale").style.width = "100%";
                doc.getElementById("in_0_vipKdtSale").innerHTML = "已完成100%";
                vipKdtSale = true;
            } else {
                //停止此进度条.显示警告
                vipKdt = xmlHttp.responseText;
                clearInterval(ba_vipKdtSale);
                doc.getElementById('spanVipKdtSale').innerHTML = "vip口袋通销售信息下载失败";
            }

            if (vip === true && shop === true && saler === true && vipCard === true && vipKdt === true && vipKdtSale === true) {
                doc.getElementById("divBG").style.display = "none";
                initControl();
                alert('所选资料同步完成');
            } else {
                if (vip != true)
                    alert(vip);
                if (saler != true)
                    alert(saler);
                if (shop != true)
                    alert(shop);
                if (vipCard != true)
                    alert(vipCard);
                if (vipKdt != true)
                    alert(vipKdt);
                if (vipKdtSale != true)
                    alert(vipKdtSale);

            }
            doc.getElementById("divBG").style.display = "none";
            initControl();
        }
    };
    xmlHttp.open("GET", url, true);
    doc.getElementById('spanVipKdtSale').style.color = "red";
    doc.getElementById('spanVipKdtSale').innerHTML = "正在进行vip口袋通销售信息同步";
    //start_vip(6000); //6秒增加1%,共需600秒. 匹配后台10分钟   
    start_vipKdtSale(300); //1.8秒增加1%,共需180秒，180/1.8=100,180s=3m

    xmlHttp.send(null);
}