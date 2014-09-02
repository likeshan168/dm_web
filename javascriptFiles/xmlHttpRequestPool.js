///XMLHttpRequest对象池
var xmlHttps = new Array();
///获取异步访问对象
function GetXmlHttp() {
    for (var index = 0; index < xmlHttps.length; index++) {
        ///如果XMLHttpRequest对象状态为为初始化或者为完成，直接返回此对象
        if (xmlHttps[index].readyState == 0 || xmlHttps[index].readyState == 4) {
            return xmlHttps[index];
        }
    }
    ///对象池没有空闲对象则创建一个新对象
    xmlHttps[xmlHttps.length] = CreateXmlHttp();
    return xmlHttps[xmlHttps.length - 1];
}
///创建一个XmlHttpRequst对象
function CreateXmlHttp() {
    var xmlHttpTemp;
    ///如果浏览器是IE
    if (window.ActiveXObject) {
        var MSXML = ['MSXML2.XMLHTTP.6.0', 'MSXML2.XMLHTTP.5.0', 'MSXML2.XMLHTTP.4.0', 'MSXML2.XMLHTTP.3.0', 'MSXML2.XMLHTTP', 'Microsoft.XMLHTTP'];
        for (var index = 0; index < MSXML.length; index++) {
            try {
                xmlHttpTemp = new ActiveXObject(MSXML[index]);
                break;
            }
            catch (e) {
                //什么都不做
            }
        }
    }
    else {
        xmlHttpTemp = new XMLHttpRequest();
    }
    return xmlHttpTemp;
}
///发送请求(POST,GET),地址，数据，回调函数
function SendRequest(method, url, data, callback) {
    var xmlHttp = GetXmlHttp();
    ///设置为默认函数
    with (xmlHttp) {
        try {
            if (url.indexOf("?") > 0) {
                url += "&date=" + new Date().getTime();
            }
            else {
                url += "?date=" + new Date().getTime();
            }
            Open(method, encodeURI(url), true);
            // 设定请求编码方式
            setRequestHeader('Content-Type', 'application/x-www-form-urlencoded; charset=UTF-8');
            send(data);
            onreadystatechange = function() {
                if (readyState == 4) {
                    if (status == 200 || status == 304) {
                        callback(xmlHttp);
                    }
                }
            }
        }
        catch (e) {

        }
    }
}
