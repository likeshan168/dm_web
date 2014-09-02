<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VipDetails.aspx.cs" Inherits="VipDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <meta name="keywords" content="Vip客户详细资料-上运端网上商城" />
    <meta name="description" content="Vip客户详细资料-上云端网上商城" />
    <title>Vip客户详细资料查看/修改-上云端网上商城</title>
    <link href="cssFiles/comstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        function getClientID() {
            var paraId1 = "<%=txtUserName.ClientID %>",
            paraId2 = "<%=txtPassword.ClientID %>",
            paraId4 = "<%=txtBirthday.ClientID %>",
            paraId5 = "<%=txtMobile.ClientID %>",
            paraId6 = "<%=txtEmail.ClientID %>",
            paraId7 = "<%=btnSubmit.ClientID %>";
            return { userName: paraId1, password: paraId2, birthday: paraId4, mobile: paraId5, email: paraId6 ,btnSub:paraId7};

        }
    </script>
</head>
<body>
    <div class="va_head">
        <div class="va_logo">
            <a href="http://www.eissy.com.cn/" target="_blank" style="font-size:38px;font-weight:bold">
                VIP Manager</a></div>
        <div class="va_quick_menu_box">
        </div>
        <div class="va_Navigation">
            <ul>
                <li class="va_Navigation_li01"><a href="http://www.eissy.com.cn/"><span>商城首页</span></a></li>
                <li id="ch1" onmouseover="document.getElementById('ch1').className='va_Navigation_li01'"
                    onmouseout="document.getElementById('ch1').className='va_Navigation_li02'" class="va_Navigation_li02">
                    <a href="Login.aspx" title="系统用户入口"><span>系统用户入口</span></a></li>
            </ul>
        </div>
    </div>
    <form id="form1" runat="server">
    <div class="list_weizhi">
        <span class="color01"><b>您所在的位置</b></span> > Vip客户详细资料查询/修改 ><a href="LoginSuccess.aspx">返回</a> </div>
    <div class="regsetp">
        <div class="regsetp_waikuang">
            <div class="regsetp_title font">
                <b>Vip客户详细资料查询/修改</b></div>
            <table border="0" cellpadding="0" cellspacing="0" class="regsetp_nr">
                <tr>
                    <td align="center">
                        卡 号:
                    </td>
                    <td align="left" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtCardID" class="bianxian" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td align="left">
                        <label id="lblCardID" style="color: #575757">
                            &nbsp;&nbsp;VIP贵宾卡卡号,不可修改</label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        级 别:
                    </td>
                    <td align="left" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtCardNo" class="bianxian" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td align="left">
                        <label id="lblCardType" style="color: #575757">
                            &nbsp;&nbsp;VIP贵宾卡级别,会按照消费情况自动升级,不可修改</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        开卡日期:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtBeginDate" class="bianxian" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblBeginDate" style="color: #575757">
                            &nbsp;&nbsp;vip卡的生效开始日期,不可修改</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        截止日期:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtEndDate" class="bianxian" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblEndDate" style="color: #575757">
                            &nbsp;&nbsp;vip卡的到期日期,不可修改</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        积 分:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtPoints" class="bianxian" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblPoints" style="color: #575757">
                            &nbsp;&nbsp;当前可用积分,不可修改</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        姓 名:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblName" style="color: #575757">
                            &nbsp;&nbsp;真实姓名,汉字或字母组成</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        称 呼:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblTitle" style="color: #575757">
                            &nbsp;&nbsp;别称，非必填项</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" nowrap="nowrap">
                        登录密码:
                    </td>
                    <td align="left" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtPassword" ></asp:TextBox>
                    </td>
                    <td align="left">
                        <label id="lblPwd" style="color: #575757">
                            &nbsp;&nbsp;商城登录密码,长度6-12位之间</label>
                    </td>
                </tr>
              <%--  <tr>
                    <td align="center">
                        确认密码:
                    </td>
                    <td align="left" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtPassword2" TextMode="Password"></asp:TextBox>
                    </td>
                    <td align="left">
                        <label id="lblPwd2" style="color: #575757">
                            &nbsp;&nbsp;确认密码需和登录密码必须要相同</label>
                    </td>
                </tr>--%>
                <tr>
                    <td align="center">
                        性 别:
                    </td>
                    <td align="center" class="regsetp_nr_left">
                        <asp:RadioButton runat="server" GroupName="sex" ID="rbSex1" Text="男" Checked="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton runat="server" GroupName="sex" ID="rbSex2" Text="女" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        生 日:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtBirthday"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblBirthday" style="color: #575757">
                            &nbsp;&nbsp;请输入正确的出生日期. 格式 yyyy-MM-dd</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        手机号码:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtMobile"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblMobile" style="color: #575757">
                            &nbsp;&nbsp;请输入正确手机号码,可以接收到优惠信息及其他服务.</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        座机号码:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtPhone"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblPhone" style="color: #575757">
                            &nbsp;&nbsp;座机号码,方便与你进行联系.</label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 12px; margin-bottom: 5px; text-align: center;">
                        电子邮箱:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtEmail"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblEmail" style="color: #575757">
                            &nbsp;&nbsp;你也可以用Email登录,还能够帮助你找回密码</label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 12px; margin-bottom: 5px; text-align: center;">
                        证件号码:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtPasNo"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblPasNo" style="color: #575757">
                            &nbsp;&nbsp;证件号码,非必填项</label>
                    </td>
                </tr>
                <tr>
                    <td style="font-size: 12px; margin-bottom: 5px; text-align: center;">
                        邮 编:
                    </td>
                    <td style="text-align: left;" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtPost"></asp:TextBox>
                    </td>
                    <td style="text-align: left;">
                        <label id="lblPost" style="color: #575757">
                            &nbsp;&nbsp;方便我们寄送货品的邮政编码. 非必填项</label>
                    </td>
                </tr>
                <tr>
                    <td align="center" style="font-size: 12px; margin-bottom: 5px;">
                        联系地址:
                    </td>
                    <td align="left" class="regsetp_nr_left">
                        <asp:TextBox runat="server" ID="txtAddress"></asp:TextBox>
                    </td>
                    <td align="left">
                        <label id="lblAddress" style="color: #575757">
                            &nbsp;&nbsp;方便我们寄送货品的地址. 非必填项</label>
                    </td>
                </tr>
                <tr style="margin-top: 15px">
                    <td align="center" style="font-size: 12px; margin-bottom: 5px;" colspan="3">
                        <div class="agree_an font">
                            <%--<a id="asubmitClick" href="javascript:void(0);"><b>确定信息无误,提交修改信息</b></a>--%>
                            <asp:LinkButton runat="server" ID="btnSubmit" Text="确定信息无误,提交修改信息" OnClick="btnSubmit_Click">确定信息无误,提交修改信息</asp:LinkButton></div>
                        <%--<asp:Button runat="server" ID="btnSubmit" Text="提交" onclick="btnSubmit_Click" style="display:none;" />--%>
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <div class="xieyi_nr">
    </div>
    <div class="va_foot">
        <div class="va_Copyright">
            <ul>
                <li><a href="http://www.eissy.com.cn/" target="_blank"><font color="red">首页</font></a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="http://www.eissy.com.cn/">公司简介</a><a href="javascript:void(0);"
                       ></a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0"
                            align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);">客户服务</a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);">团购优惠</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif"
                        border="0" align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);">隐私申明</a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);">投诉与建议</a> </li>
                <li>北京上云端科技有限公司 2012 All Copyright </li>
                <li>客服电话：4006807701</li>
                <li></li>
            </ul>
        </div>
    </div>
    <script src="javascriptFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="jsScripts/VipDetails.js" type="text/javascript"></script>
</body>
</html>
