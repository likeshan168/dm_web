<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DM_VIP_Register.aspx.cs" Inherits="DM_VIP_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <style type="text/css">
        .table tr
        {
            height: 30px;
        }
    </style>
    <script type="text/javascript">
        function getClientID() {
            var paraID1 = '<%=txtCardID.ClientID %>',
            paraID2 = '<%=txtUserName.ClientID %>',
            paraID3 = '<%=txtMobile.ClientID %>',
            paraID4 = '<%=txtBirthday.ClientID %>',
            paraID5 = '<%=txtEmail.ClientID %>',
            paraID6 = '<%=selectCardType.ClientID %>',
            paraID7 = '<%=btnRegister.ClientID %>';
            return { Id1: paraID1, Id2: paraID2, Id3: paraID3, Id4: paraID4, Id5: paraID5, Id6: paraID6 ,Id7:paraID7};
        }
    </script>
    <fieldset>
        <legend>账户信息</legend>
        <table class="table">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="font-size: 15px; font-weight: bold; text-align: center;">
                    注册Vip会员
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bold; text-align: right;">
                    *卡号:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCardID" Width="150px" Height="16px"></asp:TextBox>
                </td>
                <td>
                    <span id="spanCardId" style="visibility: hidden; color: Red;"></span>
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bold; text-align: right;">
                    *卡类型:
                </td>
                <td>
                    <select runat="server" id="selectCardType" style="width: 155px; height: 23px;">
                        <%--<option value="0">--请选择--</option>--%><%--在这里放了runat="server"之后那么就在客户端获取不到其id--%>
                    </select>
                </td>
                <td>
                    <span id="spanCardType" style="visibility: hidden; color: Red;"></span>
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bold; text-align: right;">
                    *姓名:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtUserName" Width="150px" Height="16px"></asp:TextBox>
                </td>
                <td>
                    <span id="spanUserName" style="visibility: hidden; color: Red;"></span>
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bold; text-align: right;">
                    *性别:
                </td>
                <td>
                    <asp:RadioButton runat="server" ID="rdbMale" GroupName="gender" Checked="true" Text="男" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton runat="server" ID="rdbFemale" GroupName="gender" Text="女" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bold; text-align: right">
                    *手机号码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtMobile" Width="150px" Height="16px"></asp:TextBox>
                </td>
                <td>
                    <span id="spanMobile" style="visibility: hidden; color: Red;"></span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold; text-align: right;">
                    生日:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtBirthday" Width="150px" Height="16px"></asp:TextBox>
                </td>
                <td>
                    <span id="spanBirthday" style="visibility: hidden; color: Red;"></span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold; text-align: right;">
                    邮箱:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEmail" Width="150px" Height="16px"></asp:TextBox>
                </td>
                <td>
                    <span id="spanEmail" style="visibility: hidden; color: Red;"></span>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: center;">
                    <asp:Button runat="server" ID="btnRegister" Text="注册" Width="80px" OnClick="btnRegister_Click" />
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </fieldset>
    <script src="jsScripts/DM_VIP_Register.js" type="text/javascript"></script>
</asp:Content>
