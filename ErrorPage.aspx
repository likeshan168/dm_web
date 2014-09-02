<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
<title>您访问的页面不存在 请转到首页进入</title>
<meta http-equiv="Content-Type" content="text/html"; charset="GB2312"/>
<style type="text/css"></style>
<link type="text/css" rel="stylesheet"/>
<style type="text/css">BODY {
	FONT-SIZE: 9pt; COLOR: #842b00; LINE-HEIGHT: 16pt; FONT-FAMILY: "Tahoma", "宋体"; TEXT-DECORATION: none
}
TABLE {
	FONT-SIZE: 9pt; COLOR: #842b00; LINE-HEIGHT: 16pt; FONT-FAMILY: "Tahoma", "宋体"; TEXT-DECORATION: none
}
TD {
	FONT-SIZE: 9pt; COLOR: #842b00; LINE-HEIGHT: 16pt; FONT-FAMILY: "Tahoma", "宋体"; TEXT-DECORATION: none
}
BODY {
	SCROLLBAR-HIGHLIGHT-COLOR: buttonface; SCROLLBAR-SHADOW-COLOR: buttonface; SCROLLBAR-3DLIGHT-COLOR: buttonhighlight; SCROLLBAR-TRACK-COLOR: #eeeeee; BACKGROUND-COLOR: #ffffff
}
A {
	FONT-SIZE: 9pt; COLOR: #842b00; LINE-HEIGHT: 16pt; FONT-FAMILY: "Tahoma", "宋体"; TEXT-DECORATION: none
}
A:hover {
	FONT-SIZE: 9pt; COLOR: #0188d2; LINE-HEIGHT: 16pt; FONT-FAMILY: "Tahoma", "宋体"; TEXT-DECORATION: underline
}
H1 {
	FONT-SIZE: 9pt; FONT-FAMILY: "Tahoma", "宋体"
}
</style>
</head>
<body topmargin="20">
    <form runat="server" id="form1">
    <table cellspacing="0" width="600" align="center" border="0" cepadding="0">
        <tbody>
            <tr colspan="2">
                <td valign="top" align="middle">
                    <img height="100" src="images/404.jpg" width="100" border="0">
                </td>
                <td>
                    <h1>
                        您所做的操作导致该页暂时无法访问</h1>
                    HTTP 错误 ：您正在搜索的页面可能已经删除、更名或暂时不可用。
                    <hr noshade size="0" />
                    <p>
                        ☉ 请尝试以下操作：</p>
                    <ul>
                        <li>确保浏览器的地址栏中显示的网站地址的拼写和格式正确无误。 </li>
                        <li>如果通过单击链接而到达了该网页，请与网站管理员联系，通知他们该链接的格式不正确。 </li>
                        <li>单击<a href="Main.aspx" style="color: Red">返回首页</a>按钮,返回主页重新进行之前的操作.</li>
                        <li>您还可以将之前的操作流程反馈给网站管理人员,以便改善该问题.</li>
                    </ul>
                    <hr noshade size="0" />
                    <p>
                        ☉ <a href="javascript:void(0);" onclick="javascript:document.getElementById('lblErrorText').style.display=''">点击查看详细错误信息</a><br />
                        &nbsp;&nbsp;&nbsp;<br />
                        <asp:Label runat="server" ID="lblErrorText" style="display:none"></asp:Label>
                    </p>
                </td>
            </tr>
        </tbody>
    </table>
    </form>
</body>
</html>
