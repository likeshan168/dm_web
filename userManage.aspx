<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="userManage.aspx.cs" Inherits="userManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <table style="width: 100%; border: 0;">
        <tr>
            <td class="v14-header-3">
                系统用户账户设置
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
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;用户账号" DataField="cid"
                                    ReadOnly="true" ShowHeader="False" HeaderStyle-HorizontalAlign="Center">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;登录密码" DataField="password">
                                    <ItemStyle HorizontalAlign="Center" Width="100px" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;登录次数" DataField="loginCount"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;最后登录日期" DataField="lastLoginTime"
                                    ReadOnly="true">
                                    <ItemStyle HorizontalAlign="Center" Width="120px" Wrap="False" />
                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" EditImageUrl="~/images/config.png" EditText="修改密码"
                                    HeaderText="修改密码" ShowEditButton="True" ShowHeader="True" CancelImageUrl="~/images/XX.gif"
                                    UpdateImageUrl="~/images/OK.gif">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>
                                <%-- <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/XX.gif" HeaderText="删除此记录"
                                    ShowDeleteButton="True" ShowHeader="True">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:CommandField>--%>
                                <asp:TemplateField HeaderText="删除此账户">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ImageUrl="~/images/XX.gif" CommandName="del" ID="del"
                                            OnClientClick="return confirm('您确认删除此用户吗？')" />
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
                注册新账户
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel runat="server" ID="up2" ChildrenAsTriggers="true">
                    <ContentTemplate>
                        用户名:<asp:TextBox runat="server" ID="txtUid"></asp:TextBox><span id="uErr" style="color: Red;
                            visibility: hidden">*</span> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 登录密码:<asp:TextBox
                                runat="server" ID="txtPwd"></asp:TextBox><span id="pErr" style="color: Red; visibility: hidden">*</span>&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnSubmit" Text="添加" OnClick="btnSubmit_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            var uid = $("[id$=txtUid]");
            var pwd = $("[id$=txtPwd]");
            var doc = document;
            $("[id$=btnSubmit]").click(function (e) {
                if (uid.val().length == 0) {
                    e.preventDefault();
                    uid.css("border", "2px solid red").focus();
                    doc.getElementById("uErr").style.visibility = "visible";

                }
                else {
                    uid.css("border", "1px solid black");
                    doc.getElementById("uErr").style.visibility = "hidden";
                }
                if (pwd.val().length == 0) {
                    e.preventDefault();
                    pwd.css("border", "2px solid red").focus();
                    doc.getElementById("pErr").style.visibility = "visible";

                }
                else {
                    pwd.css("border", "1px solid black");
                    doc.getElementById("pErr").style.visibility = "hidden";
                }
            });
        });
    </script>
</asp:Content>
