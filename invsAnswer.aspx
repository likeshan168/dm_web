<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invsAnswer.aspx.cs" Inherits="invsAnswer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调研问卷回馈</title>
    <link rel="stylesheet" href="cssFiles/invsAnswerCss.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="880" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <img name="vote_1" src="http://www.empox.cn/temp/mail/vote_1.jpg" width="880" height="204"
                    border="0" id="vote_1" alt="" />
            </td>
        </tr>
        <tr>
            <td>
                <br />
                <br />
                <table width="80%" align="center">
                    <tr>
                        <td>
                            用户卡号:<asp:TextBox runat="server" ID="txtvipID" ForeColor="ActiveCaption" CssClass="input"></asp:TextBox><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divAns">
                                <asp:PlaceHolder runat="server" ID="phInvs"></asp:PlaceHolder>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnSubmit" Text="提交" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <img name="vote_3" src="http://www.empox.cn/temp/mail/vote_3.jpg" width="880" height="168"
                    border="0" id="vote_3" alt="" />
            </td>
        </tr>
        
    </table>
    </form>
    <script src="javascriptFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$=btnSubmit]").click(function (e) {
                var inputText = $("#divAns input:text");
                var inputTextNum = inputText.length;
                if (inputTextNum > 0) {
                    for (var i = 0; i < inputTextNum; i++) {
                        if (inputText[i].value.length === 0) {
                            inputText[i].style.border = "2px solid red";
                            inputText[i].focus();
                            e.preventDefault();
                        }
                        else {
                            inputText[i].style.border = "1px solid black";
                        }
                    }
                }
                var checkbox = $("#divAns input:checkbox");
                var checkboxNum = checkbox.length;
                var checked = false;
                if (checkboxNum > 0) {
                    for (var i = 0; i < checkboxNum; i++) {
                        if (checkbox[i].checked) {
                            checked = true;
                        }
                        else {
                            checked = false;
                        }
                    }
                    if (!checked) {
                        e.preventDefault();
                        alert("请您将信息选择完整！");
                    }
                }

                var radio = $("#divAns input:radio");
                var numOfRadio = radio.length;
                var Rck = false;
                if (numOfRadio > 0) {
                    for (var i = 0; i < numOfRadio; i++) {
                        if (radio[i].checked) {
                            Rck = true;
                        }
                        else {
                            Rck = false;
                        }
                    }
                    if (!Rck) {
                        e.preventDefault();
                        alert("请您将信息选择完整！");
                    }
                }
            });
        });
    </script>
</body>
</html>
