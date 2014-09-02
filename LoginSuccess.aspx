<%@ Page Language="C#" AutoEventWireup="true"   CodeFile="LoginSuccess.aspx.cs" Inherits="LoginSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <meta name="keywords" content="Vip登录成功页面-上云端网上商城" />
    <%--便宜搜索引擎的搜索--%>
    <meta name="description" content="Vip登录成功页面-上云端网上商城" />
    <title>Vip登录成功页面-上云端网上商城</title>
    <link href="cssFiles/comstyle.css" rel="stylesheet" type="text/css" />
    <link href="cssFiles/css.css" rel="stylesheet" type="text/css" />
    <link href="cssFiles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var time = 30;
        function Interval() {
            setInterval(setText, 1000);
        }
        function setText() {
            if (time > 0) {
                document.getElementById('stime').innerHTML = time.toString();
                time--;
            }
            else {
                window.location.href = "http://www.eissy.com.cn/";
            }
        }
        function validateCoupon() {//7月1号8月31、12月1号次年1月31号(积分兑换只允许在这段时间里进行)
            var date = new Date(), yearNow = date.getFullYear(), monthNow = date.getMonth() + 1, dayNow = date.getDate();
            //alert(yearNow+":"+monthNow+":"+dayNow);
            if (monthNow < 7 && monthNow > 1 || monthNow > 8 && monthNow < 12) {
                location.href = "CouponFailure.aspx";
            }
            else {
                location.href = "ApplyForCoupon.aspx";
            }
            //location.href = "ApplyForCoupon.aspx";
        }
       
      
    </script>
</head>
<body onload="Interval();">
    `<div class="va_head">
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
        <span class="color01"><b>您所在的位置</b></span> ><a href="VipClientLogin.aspx">Vip登录</a>  > 登录成功</div>
    <div id="divLoginSuccess" class="denglu_nr" style="margin: 0 auto;">
        <table style="text-align: left; border: 0;">
            <tr style="margin-top: 15px">
                <td style="text-align: center;">
                    <label style="color: Red; font-size: 25px; font-weight: bold; padding: 5px;">
                        登录成功,现在您可以做如下操作:
                    </label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: 16px; padding: 5px;">
                    <a href="VipDetails.aspx" title="修改个人资料" target="_self">修改个人资料</a>
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-size: 16px; padding: 5px;">
                    <a id="PointsForGifts" href="javascript:validateCoupon();" title="进入积分换礼品" target="_self">进入积分换礼品</a>
                </td>
            </tr>
            <tr style="margin-top: 15px">
                <td style="text-align: center; font-size: 16px; padding: 5px;">
                    <a href="http://www.eissy.com.cn/" title="返回商城主页" target="_blank">返回商城主页</a>
                </td>
            </tr>
            <tr style="margin-top: 15px">
                <td style="text-align: center; font-size: 13px; padding: 5px;">
                    本页面会在<span id="stime" style="font-size: large; color: Red">30</span>秒后自动跳转到网上商城.
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div class="xieyi_nr">
    </div>
    <div class="va_foot">
        <div class="va_Copyright">
            <ul>
                <li><a href="http://www.eissy.com.cn/"><font color="red">首页</font></a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" style="text-align: center; border: 0" />&nbsp;&nbsp;&nbsp;
                    <a href="http://www.eissy.com.cn/">公司简介</a><a href="javascript:void(0)"
                        ></a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0"
                            align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0)">客户服务</a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0)">团购优惠</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif"
                        border="0" align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0)">隐私申明</a>
                    &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0)">投诉与建议</a> </li>
                <li>北京上云端科技有限公司 2012 All Copyright </li>
                <li>客服电话：4006807701</li>
                <li></li>
            </ul>
        </div>
    </div>
</body>
</html>
