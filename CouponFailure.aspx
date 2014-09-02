<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponFailure.aspx.cs" Inherits="CouponFailure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=GBK" />
    <meta name="keywords" content="积分换礼页面-上云端网上商城" />
    <meta name="description" content="积分换礼页面-上云端网上商城" />
    <title>积分换礼页面-上云端网上商城</title>
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
                <li class="va_Navigation_li01"><a href="http://www.eissy.com.cn/"><span>首页</span></a></li>
                <li id="ch1" onmouseover="document.getElementById('ch1').className='va_Navigation_li01'"
                    onmouseout="document.getElementById('ch1').className='va_Navigation_li02'" class="va_Navigation_li02">
                    <a href="http://www.eissy.com.cn/" title="商城首页"><span>商城首页</span></a></li>
            </ul>
        </div>
    </div>
    <!--头部结束-->
    <!--这是中间部分-->
    <div class="list_weizhi">
        <span class="color01"><b>您所在的位置</b></span> >> 积分换礼失败 >><a href="javascript:history.go(-1);">返回</a>
    </div>
    <div class="login">
        <form id="form1" runat="server">
        <table style="width: 100%;">
            <tr>
                <td>
                    <p style="font-size: xx-large; color: #FF0000; font-weight: bolder; text-align: center;">
                        不在积分礼品兑换期内，暂停兑换。</p>
                </td>
            </tr>
            <tr>
                <td>
                    <p style="font-size: medium;">
                        ①积分不再清零，可长期累计。</p>
                    <p style="font-size: medium;">
                        ②以后请在兑换期内兑换：每年两次。</p>
                    <p style="font-size: medium;">
                        第一次：每年的7月1日至8月31日；</p>
                    <p style="font-size: medium;">
                        第二次：当年12月1日至次年1月31日。</p>
                    <p style="font-size: medium;">
                        ③若在兑换期间内未兑换礼品，则积分继续有效，可累积至下一个兑换期间再行兑换。</p>
                    <p style="font-size: medium;">
                        ④兑换期间之外任何时间，暂停兑换事宜。</p>
                </td>
            </tr>
        </table>
        </form>
        <%--<p  style="color: #0000FF; font-size: medium;text-align:center;"><a href="javascript:history.go(-1);">返回上页</a></p>--%>
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
                <li>客服电话：4006807701</li>
                <li style="font-size: medium"></li>
            </ul>
        </div>
    </div>
</body>
</html>
