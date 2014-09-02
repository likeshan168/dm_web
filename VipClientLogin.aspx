<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VipClientLogin.aspx.cs" Inherits="VipClientLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <meta name="keywords" content="用户登录页面-上云端网上商城" />
    <meta name="description" content="用户登录页面-上云端网上商城" />
    <title>用户登录页面-上云端网上商城</title>
    <link href="images/comstyle.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .STYLE1
        {
            color: #C42628;
        }
        .style1
        {
            height: 30px;
        }
    </style>
    <script type="text/javascript">
        var time = 30;
        function Interval() {
            setInterval(setText, 1000);
        }
        function setText() {
            if (time > 0) {
                document.getElementById('stime').innerHTML = time.toString();
                document.getElementById('stime2').innerHTML = time.toString();
                time--;
            }
            else {
                window.location.href = "http://www.eissy.com.cn/";
            }
        }
    </script>
    <script type="text/javascript">
        function getClientId() {
            var paraId1 = '<%= txtName.ClientID %>'; //注册控件1 
            var paraId2 = '<%= txtPwd.ClientID %>'; //注册控件2
            var paraId3 = '<%= txtVerifyCode.ClientID %>';
            var paraId4 = '<%= btnLogin.ClientID %>';
            return { Id1: paraId1, Id2: paraId2, Id3: paraId3, Id4: paraId4 }; //生成访问器
        }
    </script>
</head>
<body>
    <!--Top-->
    <!--这是头部-->
    <!--头部-->
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
    <!--头部结束-->
    <!--这是中间部分-->
    <div class="list_weizhi">
        <span class="color01"><b>您所在的位置</b></span> > 用户登录</div>
    <div class="login">
        <form id="Form1" name="Form1" runat="server">
        <div style="text-align: center; margin: 0 auto;width:403px;height:290px;">
            <div class="denglu">
                <div class="denglu_title font">
                    <img src="images/login_tu01.gif" align="absmiddle" />&nbsp;&nbsp;<b>登录</b>
                </div>
                <div class="denglu_ts color01">
                    <b>已注册用户请从这里登录</b>
                </div>
                <div id="divLogin" class="denglu_nr">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="right" class="denglu_nrsr">
                                卡号/手机号：
                            </td>
                            <td align="left" class="denglu_nrsr" colspan="2">
                                <asp:TextBox runat="server" ID="txtName" class="bianxian" Width="158px"></asp:TextBox><span
                                    id="nameErr" style="color: Red; display: none;">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="denglu_nrsr">
                                密&nbsp;&nbsp;&nbsp; 码：
                            </td>
                            <td align="left" class="denglu_nrsr" colspan="2">
                                <asp:TextBox runat="server" ID="txtPwd" TextMode="Password" Width="158px" class="bianxian"></asp:TextBox><span
                                    id="passErr" style="color: Red; display: none">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="denglu_nrsr">
                                验证码：
                            </td>
                            <td align="left" class="denglu_nrsr">
                                <asp:TextBox runat="server" ID="txtVerifyCode" Width="70px" class="bianxian"></asp:TextBox>
                            </td>
                            <td>
                                <img id="imgVerify" src="VerifyCode.ashx" alt="看不清?点击更换" onclick="this.src=this.src+'?'" />
                                <span class="ys"><a href="javascript:void(0);" onclick="javascript:document.getElementById('imgVerify').src+='?'">
                                    <b>[刷新] </b></a></span>
                            </td>
                        </tr>
                    </table>
                    <div class="denglu_nrsr_an01 ys font">
                        <asp:LinkButton runat="server" ID="btnLogin" OnClick="btnLogin_Click"><b>登录</b></asp:LinkButton></div>
                    <div class="denglu_nrsr_an01 denglu_nrsr_an02 ys font">
                        <a href="javascript:void(0);" onclick="javascript:alert('此功能尚未开放!')"><b>忘记了密码？</b></a></div>
                </div>
                <div class="denglu_ts color01 denglu_bz">
                    <a href="javascript:void(0);" class="STYLE1">有任何疑问请点击帮助中心或联系客服</a>
                </div>
            </div>
        </div>
        <%--<div class="denglu zhuce">
            <div class="denglu_title zhuce_title font">
                <img src="images/login_tu01.gif" align="absmiddle" />&nbsp;&nbsp;<b>新用户注册</b></div>
            <div class="denglu_ts zhuce_ts color01">
                <b>立刻享受</b></div>
            <div class="denglu_nr">
                <div class="zhuce_tsnr">
                    <ul>
                        <li>近百万种商品的选择，假一罚二</li>
                        <li>最优惠的价格，物超所值</li>
                        <li>三百多个城市货到付款，零风险购物</li>
                        <li>“最以客户为中心”的服务和客户体验</li>
                    </ul>
                    <div class="zhuce_tsnr_an ys font">
                        
                        <a href="VipClientRegister.aspx" title="注册Vip卡号"><b>注册Vip卡号 </b></a>
                    </div>
                </div>
                <div class="zhuce_tb">
                    &nbsp;&nbsp;&nbsp;&nbsp;</div>
            </div>
        </div>--%>
        </form>
    </div>
    <div class="va_foot">
        <div class="va_foot_help">
        </div>
        <div class="va_Copyright">
            <ul>
                <li>
                    <!--底部内容-->
                    <a href="http://www.eissy.com.cn/"><font color="red">首页</font></a>&nbsp;&nbsp;&nbsp;<img
                        src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="http://www.eissy.com.cn/">公司简介</a><a href="javascript:void(0);"></a> &nbsp;&nbsp;&nbsp;<img
                        src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);">客户服务</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif"
                        border="0" align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);">团购优惠</a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);">隐私申明</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif"
                        border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);">
                            投诉与建议</a> </li>
                <li>北京上云端科技有限公司 2012 All Copyright </li>
                <li>客服电话：4006807701 </li>
                <li></li>
            </ul>
        </div>
    </div>
    <script src="javascriptFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="jsScripts/VipClient.js" type="text/javascript"></script>
</body>
</html>
