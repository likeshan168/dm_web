(function show() {
    xmlHttp = GetXmlHttpObject();
    if (xmlHttp === null) {
        alert("您的浏览器版与本程序存在兼容性问题,请更换浏览器");
        return;
    }
    var url = "httpHandler/Main_Response.ashx";
    xmlHttp.onreadystatechange = stateChanged;
    xmlHttp.open("GET", url, true);
    xmlHttp.send(null);
})();
function stateChanged() {
    if (xmlHttp.readyState === 4) {
        if (xmlHttp.responseText === "Finished") {
            var d = document;
            var span1 = d.getElementById("span1");
            span1.style.color = "Gray";
            span1.innerHTML = "初始化完成.欢迎使用";
        }
        else {
            var d = document;
            var span1 = d.getElementById("span1");
            span1.style.color = "red";
            span1.innerHTML = xmlHttp.responseText;
        }
    }
}