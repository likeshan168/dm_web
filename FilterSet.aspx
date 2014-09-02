<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="FilterSet.aspx.cs" Inherits="FilterSet" MaintainScrollPositionOnPostback="true" %>

<%--<%@ OutputCache SqlDependency="dmtest:DataInfo" VaryByParam="none" Duration="300" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManager runat="server" ID="sm">
    </asp:ScriptManager>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr valign="top">
                <td width="10" rowspan="3">
                    <img alt="" src="images/c.gif" width="10" height="1" />
                </td>
                <td width="100%">
                </td>
            </tr>
            <tr valign="top">
                <td>
                    <table width="180" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="9" class="v14-graphic-tab-selected">
                                    <img class="display-img" alt="" src="images/c.gif" width="9" height="19" />
                                </td>
                                <td width="171" class="v14-graphic-tab-selected">
                                    <a class="v14-tab-link-selected" href="javascript:void(0);">VIP资料综合设定管理</a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="v14-graphic-tab-lblue-table">
                                    <img class="display-img" alt="" src="images/c.gif" width="1" height="4" />
                                </td>
                            </tr>
                            <tr>
                                <td class="v14-graphic-tab-dblue-table">
                                    <img class="display-img" alt="" src="images/c.gif" width="1" height="1" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <p style="font-weight:bold;color:Green">
                        <img src="images/arrowright.gif" width="11" height="9" />本页面必须设定的字段说明为:</p>
                    <p style="color:Red">
                        <strong>姓名,性别,生日,卡号,卡类型,手机号码,电子邮箱,密码</strong>(如没有此字段需在下方自定义)</p>
                    <p>
                        <span class="bluebullet">设定VIP资料查询界面的显示顺序</span><img src="images/arrow_rd.gif" width="21"
                            height="21" align="absmiddle" style="cursor: hand" onclick="" /></p>
                    <br />
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="v14-header-3">
                                    字段管理
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="update1" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView1" CssClass="gridview" runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                                OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing"
                                                AutoGenerateColumns="False" Font-Size="Small" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging"
                                                OnRowCreated="GridView1_RowCreated" Width="800px">
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <Columns>
                                                    <asp:BoundField DataField="ENname" HeaderText="字段名称" ReadOnly="True" ItemStyle-Wrap="false">
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CNname" HeaderText="字段名称" ReadOnly="True" ItemStyle-Wrap="false">
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="type" HeaderText="字段类型" ReadOnly="true" ItemStyle-Wrap="false">
                                                        <ItemStyle Wrap="False"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="查询结果可见">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="cbDisplayed" Checked='<%# Eval("Displayd") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="是否作为查询条件">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbIsCondition" runat="server" Checked='<%# Eval("isCondition") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <img src="images/ico_config.png" width="16" height="16" />
                                    <asp:LinkButton runat="server" ID="btnUpdate" OnClick="btnUpdate_Click">保存设定</asp:LinkButton>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <br />
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td class="v14-header-3">
                                    自定义字段管理
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="update2" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView2" CssClass="gridview"  runat="server" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"
                                                OnRowCancelingEdit="GridView2_RowCancelingEdit" OnRowEditing="GridView2_RowEditing"
                                                AutoGenerateColumns="False" Font-Size="Small" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging"
                                                Width="800px" OnRowCreated="GridView2_RowCreated" OnRowCommand="GridView2_RowCommand"
                                                OnRowDataBound="GridView2_RowDataBound">
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <Columns>
                                                    <asp:BoundField DataField="ENname" HeaderText="字段名称" ReadOnly="True" />
                                                    <asp:BoundField DataField="CNname" HeaderText="字段说明" />
                                                    <asp:BoundField DataField="type" HeaderText="字段类型" ReadOnly="true" />
                                                    <asp:BoundField DataField="size" HeaderText="字段大小" />
                                                    <%-- <asp:CommandField DeleteText="删除本字段" HeaderText="删除本字段" ShowDeleteButton="True">
                                                <ItemStyle Wrap="False" />
                                            </asp:CommandField>--%>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <label>
                                                                删除本字段</label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CommandName="del" ID="del" Text="删除本字段"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lb1" Text="添加" OnClientClick="javascript:location.href='newCustomField.aspx';return false;"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <label>
                                                                增加新字段</label>
                                                        </HeaderTemplate>
                                                        <HeaderStyle Wrap="false" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <!-- Spacer -->
                    <br />
                </td>
            </tr>
            <tr valign="top">
                <td>
                    <p>
                        <br />
                    </p>
                    <table align="right" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr align="right">
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0">
                                        <tbody>
                                            <tr>
                                                <td valign="middle">
                                                    <img src="images/u_bold.gif" alt="" width="16" border="0" height="16" />
                                                    <br />
                                                </td>
                                                <td valign="top" align="right">
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
