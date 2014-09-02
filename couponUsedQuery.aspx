<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="couponUsedQuery.aspx.cs" Inherits="couponUsedQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="cssFiles/couponUsedQuery.css" type="text/css" />
    <script language="javascript" type="text/javascript">
        function selStation(locationid) {
            var Obj = document.getElementById(locationid);
            Obj.style.left = event.x;
            Obj.style.top = event.y;
            Obj.style.display = "block";
        }
        function closeDiv(obj) {
            document.getElementById(obj).style.display = "none";
        }
        function showCondition(group,state) {
            if (group == 1) {
                if (state == 0) {
                    $("#spBdate").css("display", "block");
                    $("#spR1").css("display", "none");
                }
                else {
                    $("#spBdate").css("display", "none");
                    $("#spR1").css("display", "block");
                }
            }
            else if (group == 2) {
                if (state == 0) {
                    $("#spEdate").css("display", "block");
                    $("#spR2").css("display", "none");
                }
                else {
                    $("#spEdate").css("display", "none");
                    $("#spR2").css("display", "block");
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">
<br />
    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin-left:5px; border-spacing:5px" >
        <tr>
            <td class="v14-header-3">
                查询条件
            </td>
        </tr>
        <tr>
            <td>
                发放时间:
                <asp:TextBox runat="server" ID="txtBdate1" ReadOnly="true" Width="105px"></asp:TextBox>----
                <asp:TextBox runat="server" ID="txtBdate2" ReadOnly="true" Width="105px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                截止时间:
                <asp:TextBox runat="server" ID="txtEdate1" ReadOnly="true" Width="105px"></asp:TextBox>----
                <asp:TextBox runat="server" ID="txtEdate2" ReadOnly="true" Width="105px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                状态:&nbsp;&nbsp;&nbsp; &nbsp;
                <asp:RadioButton runat="server" GroupName="rbState" ID="rbState_used" Text="已兑换" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton runat="server" GroupName="rbState" ID="rbState_unused" Text="未兑换" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton runat="server" GroupName="rbState" ID="rbState_all" Text="全部" Checked="true" />
            </td>
        </tr>
        <tr>
            <td>
                发放店铺:
                <asp:DropDownList runat="server" ID="sendPlace" Width="150px"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                匹配会员: <asp:TextBox runat="server" ID="txtCardID"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="v14-header-3">
                查询结果
            </td>
        </tr>
        <tr>
            <td>building</td>
        </tr>
    </table>
<div id="blockCity">
    <table>
        <tr>
            <td>编号:</td>
            <td id="td1">C8JJINS</td>
        </tr>
        <tr>
            <td>使用地点:</td>
            <td id="td2">北京市朝阳区燕莎商城</td>
        </tr>
        <tr>
            <td>使用日期:</td>
            <td id="td3">2011-10-20 10:43:20</td>
        </tr>
        <tr>
            <td>兑换物品:</td>
            <td id="td4">娃娃</td>
        </tr>
        <tr>
            <td>相应积分:</td>
            <td id="td5">2100</td>
        </tr>
        </table>
        <table>
        <tr>
            <td colspan="2" align="center"><a href="javascript:void(0);" onclick="closeDiv('blockCity')">关闭</a></td>
        </tr>
    </table>
</div>
</asp:Content>

