<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    MaintainScrollPositionOnPostback="true" CodeFile="couponManager.aspx.cs" Inherits="couponManager" %>

<asp:Content ID="C1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="cssFiles/couponPage.css" type="text/css" />
    <script type="text/javascript" src="javascriptFiles/JS_couponManager.js"></script>
</asp:Content>
<asp:Content ID="C2" ContentPlaceHolderID="cph" runat="Server">
    <div style="display: none">
        <asp:ScriptManager runat="server" ID="scriptManager1">
        </asp:ScriptManager>
    </div>
    <div id="lib_Tab1">
        <div class="lib_Menubox lib_tabborder">
            <%--<ul>
                <li id="one1" onclick="setTab('one',1,2)" class="hover">优惠券查询</li>
                <li id="one2" onclick="setTab('one',2,2);closeDiv('blockCity');">类别管理</li>
            </ul>--%>
            <table>
                <tr>
                    <td align="center" id="one1" onclick="setTab('one',1,2)" class="hover">
                        优惠券查询
                    </td>
                    <td align="center" id="one2" onclick="setTab('one',2,2);closeDiv('blockCity');">
                        类别管理
                    </td>
                </tr>
            </table>
        </div>
        <div class="lib_Contentbox lib_tabborder">
            <div id="con_one_1">
                <table width="90%" border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr valign="top">
                            <td>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td class="v14-header-3">
                                                查询条件
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <span class="v14-ttd">发放店名称(模糊)：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtClientID"></asp:TextBox>
                                                        </td>
                                                        <td style="color: Gray">
                                                            *可以不填写
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span class="v14-ttd">使用状态：</span>
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox runat="server" ID="cbUsed" Text="已兑换" Checked="true" />
                                                            <asp:CheckBox runat="server" ID="cbGetUnUsed" Text="未兑换" Checked="true" /><br />
                                                        </td>
                                                        <td style="color: Gray">
                                                            *至少要选择一个
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span class="v14-ttd">是否匹配VIP信息：</span>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButton runat="server" GroupName="c1" Text="是" ID="ra1" onclick="document.getElementById('trVip').style.display='table-row';" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:RadioButton runat="server" GroupName="c1" Text="否" ID="ra2" Checked="true" onclick="document.getElementById('trVip').style.display='none';" />
                                                        </td>
                                                        <td style="color: Gray">
                                                            *可以不匹配会员信息
                                                        </td>
                                                    </tr>
                                                    <tr id="trVip" style="display: none">
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlVipTypes">
                                                                <asp:ListItem Text="卡号" Value="卡号"></asp:ListItem>
                                                                <asp:ListItem Text="手机号码" Value="手机号码"></asp:ListItem>
                                                                <asp:ListItem Text="姓名" Value="姓名"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCardID"></asp:TextBox>
                                                        </td>
                                                        <td style="color: Gray">
                                                            *如果为空的话，那就相当于没有匹配会员信息
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span class="v14-ttd">发放日期区间：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtBdate" onclick="new Calendar('2011', '2100', 0, 'yyyy-MM-dd').show(this);" />
                                                            ----
                                                            <asp:TextBox runat="server" ID="txtEdate" onclick="new Calendar('2011', '2100', 0, 'yyyy-MM-dd').show(this);" />
                                                        </td>
                                                        <td style="color: Gray">
                                                            *可以不选择申请日期区间(为空就是查询所有申请日期的优惠券)
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <span class="v14-ttd">到期日期区间：</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCBdate" onclick="new Calendar('2011', '2100', 0, 'yyyy-MM-dd').show(this);" />
                                                            ----
                                                            <asp:TextBox runat="server" ID="txtCEdate" onclick="new Calendar('2011', '2100', 0, 'yyyy-MM-dd').show(this);" />
                                                        </td>
                                                        <td style="color: Gray">
                                                            *可以不选择到期日期区间（为空就是查询所有到期日期的优惠券）
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="center">
                                                            <asp:LinkButton runat="server" class="bluebullet" Text="执行搜索" ID="btnQuery" OnClick="btnQuery_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                    <tbody>
                                        <tr>
                                            <td class="v14-header-3">
                                                查询结果(已申请/使用)
                                            </td>
                                            <td align="right" class="v14-header-3">
                                                显示结果条数
                                                <asp:DropDownList runat="server" ID="ddlPageSize" Width="80px" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="10">10条</asp:ListItem>
                                                    <asp:ListItem Value="50">50条</asp:ListItem>
                                                    <asp:ListItem Value="100">100条</asp:ListItem>
                                                    <asp:ListItem Value="500">500条</asp:ListItem>
                                                    <asp:ListItem Value="2000">2000条</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="left">
                                                <label style="color: Red" runat="server" id="lblSelInfo">
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div runat="server" id="divInfo" style="width: 100%; height: 350px; overflow: scroll;">
                                                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999"
                                                        BorderStyle="Solid" CssClass="gvItem" BorderWidth="1px" CellPadding="3" ForeColor="Black"
                                                        GridLines="Vertical" Font-Size="Small" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                        AutoGenerateColumns="False" Width="100%" OnRowDataBound="GridView1_RowDataBound">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="优惠券详情">
                                                                <ItemTemplate>
                                                                    <a id="seeDetails" title="单击查看详情" style="cursor: pointer">详细信息</a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="cid" HeaderText="优惠券编号"></asp:BoundField>
                                                            <asp:BoundField DataField="userName" HeaderText="VIP姓名"></asp:BoundField>
                                                            <asp:BoundField DataField="card_Id" HeaderText="VIP卡号"></asp:BoundField>
                                                            <asp:BoundField DataField="getDate" HeaderText="发放日期"></asp:BoundField>
                                                            <asp:BoundField DataField="endDate" HeaderText="到期日期"></asp:BoundField>
                                                            <asp:BoundField DataField="applyPlace" HeaderText="发放地点"></asp:BoundField>
                                                            <asp:CheckBoxField DataField="consumed" HeaderText="消费状态" ReadOnly="true"></asp:CheckBoxField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#E6F8FC" />
                                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle BackColor="#50A5CA" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="#E6F8FC" />
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div id="con_one_2" style="display: none" align="left">
                当前剩余可使用优惠券数量为<asp:Label runat="server" ID="lbpc" Style="color: Red"></asp:Label>,您可以继续手动生成
                <asp:TextBox runat="server" ID="txtpc" Width="74px"></asp:TextBox><a href="javascript:void(0);"
                    onclick="createPC()" id="apc">优惠券生成</a>
                <br />
                已拥有优惠券类型:<br />
                <asp:UpdatePanel runat="server" ID="UP2" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:GridView runat="server" CssClass="gvItem" ID="gvCT" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="3" ForeColor="Black" GridLines="Both" Font-Size="Small" OnRowDataBound="gvCT_RowDataBound"
                            OnRowCommand="gvCT_RowCommand" DataKeyNames="tid" OnRowDeleting="gvCT_RowDeleting">
                            <Columns>
                                <asp:BoundField DataField="tid" HeaderText="类型编号" />
                                <asp:BoundField DataField="ctype" HeaderText="主要类型" />
                                <asp:BoundField DataField="typeDetails" HeaderText="详细类型" />
                                <asp:BoundField DataField="pname" HeaderText="使用场所" />
                                <asp:BoundField DataField="score" HeaderText="所需积分" />
                                <asp:BoundField DataField="money" HeaderText="对应金额" />
                                <asp:BoundField DataField="article" HeaderText="对应物品" />
                                <asp:TemplateField HeaderText="使用状态">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbDisplayed" runat="server" Checked='<%# Eval("usingState") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="管理">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Delete" Text="删除该类型"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:LinkButton runat="server" ID="lbSave" Text="保存更改" Font-Underline="false" OnClick="saveChange"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a href="#typeAdd" class="nyroModal">
                            新增类别</a>
                        <div id="typeAdd" style="display: none;">
                            <table align="center" style="height: 100%">
                                <%-- <tr>
                                    <td align="right" colspan="2">
                                        <a href="#typeAdd" class="nyroModalClose">关闭</a>
                                    </td>
                                </tr>--%>
                                <tr style="background-color: Gray; color: White;">
                                    <td align="center" colspan="2">
                                        添加优惠券类型
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        优惠券主类型:
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlCouponType" Width="131px" onchange="typeChanged()">
                                            <asp:ListItem Text="代金" Value="LOC"></asp:ListItem>
                                            <asp:ListItem Text="实物" Value="GOODS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        优惠券详细类别:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCDT" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        使用场所:
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlUsedPlace" Width="131px" onchange="placeChanged()">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trOther" style="display: none">
                                    <td>
                                        自定义场所:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPlaceOther" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        所需积分:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtScore" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trMoney">
                                    <td>
                                        对应金额:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtMoney" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trArticle" style="display: none">
                                    <td>
                                        对应礼品:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtArticle" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trArtiCode" style="display: none">
                                    <td>
                                        对应金额:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtArtiCode" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <a href="javascript:void(0);" onclick="newTypeReg()">注册新类型</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <asp:Button runat="server" ID="btnHide" Style="display: none" OnClick="btnHide_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div id="blockCity">
        <table>
            <tr>
                <td>
                    优惠券号:
                </td>
                <td id="tdNo">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    优惠券类型:
                </td>
                <td id="tdType">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    到期时间:
                </td>
                <td id="tdEDate">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    兑换日期:
                </td>
                <td id="tdUsedDate">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    兑换地点:
                </td>
                <td id="tdUsedPlace">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    兑换物品:
                </td>
                <td id="tdUsed">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    所需积分:
                </td>
                <td id="tdScore">
                    加载中...
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    用户手机:
                </td>
                <td id="tdMobile" align="left">
                    加载中...
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <a id="a_send" onclick="reSendCoupon()" style="cursor: pointer">发送优惠券编号</a>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td align="left">
                    <a id="a_exchange" onclick="Exchange()" style="cursor: pointer">兑换优惠券</a>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="4" align="center">
                    <a style="cursor: pointer" onclick="closeDiv('blockCity')">关闭</a>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".nyroModal").nyroModal();
        });
    </script>
</asp:Content>
