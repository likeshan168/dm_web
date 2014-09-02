<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="ApplyForCoupon.aspx.cs"
    Inherits="ApplyForCoupon" %>

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
    <style type="text/css">
        .table
        {
            border: 1px solid gray;
            border-collapse: collapse;
        }
        .table th, td
        {
            border: 1px solid gray;
            border-collapse: collapse;
            border-spacing: 0;
        }
    </style>
</head>
<body>
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
    <input type="hidden" name="hiddenStoken" value="<%=GetToken() %>" />
    <div class="list_weizhi">
        <span class="color01"><b>您所在的位置</b></span> >Vip优惠劵申请><a href="LoginSuccess.aspx">返回</a>
    </div>
    <div id="divLoginSuccess" class="denglu_nr" style="margin: 0 auto; height: auto;">
        <p>
            尊敬的VIP<label id="lblVipName" style="color: Red;">
                <%=RegularExp.VipName %>
            </label>
            :您好,您当前的可用积分为:<label id="lblPoints" style="color: Red;"><%=RegularExp.Points
            %></label>
        </p>
        <div style="margin: 0 auto; text-align: center; margin: 10px;">
            <table class="table">
                <caption>
                    <h3>
                        可申请优惠劵类型</h3>
                </caption>
                <thead>
                    <tr>
                        <th>
                            类型编号
                        </th>
                        <th>
                            主类型
                        </th>
                        <th>
                            详细类型
                        </th>
                        <th>
                            使用地点
                        </th>
                        <th>
                            所需积分
                        </th>
                        <th>
                            对应金额
                        </th>
                        <th>
                            对应物品
                        </th>
                        <th>
                            优惠劵申请
                        </th>
                    </tr>
                </thead>
                <asp:Repeater runat="server" ID="repeater" OnItemCommand="repeater_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("tid")
                                %>
                            </td>
                            <td>
                                <%#Eval("ctype")%>
                            </td>
                            <td>
                                <%#Eval("typeDetails")%>
                            </td>
                            <td>
                                <%#Eval("pname")%>
                            </td>
                            <td>
                                <%#Eval("score")%>
                            </td>
                            <td>
                                <%#Eval("money")%>
                            </td>
                            <td>
                                <%#Eval("article")%>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" Text="申请" CommandName="applyCoupon" CommandArgument='<%#Eval("tid") %>'></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    </form>
    <div class="xieyi_nr" style="height: 20px;">
    </div>
    <div class="va_foot">
        <div class="va_Copyright">
            <ul>
                <li><a href="http://www.eissy.com.cn/"><font color="red">首页</font></a>&nbsp;&nbsp;&nbsp;<img
                    src="images/line_bg06.gif" style="text-align: center; border: 0" />&nbsp;&nbsp;&nbsp;
                    <a href="http://www.eissy.com.cn/">公司简介</a><a href="javascript:void(0);"
                       ></a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0"
                            align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);">
                                客户服务</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);">团购优惠</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif"
                        border="0" align="absimddle" />&nbsp;&nbsp;&nbsp; <a href="javascript:void(0);">
                            隐私申明</a> &nbsp;&nbsp;&nbsp;<img src="images/line_bg06.gif" border="0" align="absimddle" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);">投诉与建议</a> </li>
                <li>北京上云端科技有限公司 2012 All Copyright </li>
                <li>客服电话：4006807701</li>
                <li></li>
            </ul>
        </div>
    </div>
</body>
</html>
