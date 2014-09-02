<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DM_VIPCard_Type_Managment.aspx.cs" Inherits="DM_VIPCard_Type_Managment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <table style="width: 100%; border: 0;">
        <tr>
            <td class="v14-header-3">
                VIP卡类型设置
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="up1" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <asp:GridView runat="server" ID="gvAccount" AutoGenerateColumns="False" OnRowDeleting="gvAccount_RowDeleting"
                            OnRowEditing="gvAccount_RowEditing" OnRowCancelingEdit="gvAccount_RowCancelingEdit"
                            OnRowUpdating="gvAccount_RowUpdating" OnRowCommand="gvAccount_RowCommand" OnRowDataBound="gvAccount_RowDataBound">
                            <Columns>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;卡类型编号" DataField="TypeID"
                                    ReadOnly="true" ShowHeader="False" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;卡类型名称" DataField="TypeName">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Vip卡折扣" DataField="Discount">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;卡类型备注" DataField="Remark">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/images/config.png" EditText="修改卡类型名称"
                                    HeaderText="修改卡类型名称" ShowEditButton="True" ShowHeader="True" CancelImageUrl="~/images/XX.gif"
                                    UpdateImageUrl="~/images/OK.gif">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <asp:TemplateField HeaderText="删除此卡类型">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ImageUrl="~/images/XX.gif" CommandName="del" ID="del"
                                            OnClientClick="return confirm('您确认删除此卡类型吗？')" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td class="v14-header-3">
                新增VIP卡类型
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="up2" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        类型名称:<asp:TextBox Width="120px" Height="16px" runat="server" ID="txtUid"></asp:TextBox><span
                            id="uErr" style="color: Red; visibility: hidden">*</span> 折扣:<asp:TextBox Width="120px"
                                Height="16px" runat="server" ID="txtDiscount"></asp:TextBox><span id="spanDis" style="color: Red;
                                    visibility: hidden">*</span> 备注:<asp:TextBox runat="server" ID="txtPwd" Width="120px"
                                        Height="16px"></asp:TextBox><span id="pErr" style="color: Red; visibility: hidden">*</span>
                        <asp:Button runat="server" ID="btnSubmit" Text="新增" OnClick="btnSubmit_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <style type="text/css">
        .error
        {
            border: 1px solid red;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var uid = $("[id$=txtUid]");
           // var pwd = $("[id$=txtPwd]");
            var dis = $("[id$=txtDiscount]");
            var doc = document;
            $("[id$=btnSubmit]").click(function (e) {
                if (uid.val().length === 0) {
                    e.preventDefault();

                    uid.addClass("error");
                    doc.getElementById("uErr").style.visibility = "visible";

                }
                else {
                    uid.removeClass("error");
                    doc.getElementById("uErr").style.visibility = "hidden";
                }

                if (dis.val().length === 0) {
                    e.preventDefault();
                    dis.addClass("error");
                    doc.getElementById("spanDis").style.visibility = "visible";
                }
                else {
                    dis.removeClass("error");
                    doc.getElementById("spanDis").style.visibility = "hidden";
                }
        });
    </script>
</asp:Content>
