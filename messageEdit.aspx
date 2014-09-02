<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="messageEdit.aspx.cs" Inherits="messageEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript" src="javascriptFiles/JSmessageEdit.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr valign="top">
                <td width="10" rowspan="3">
                    <img alt="" src="images/c.gif" width="10" height="1"/>
                </td>
                <td width="100%">
                </td>
            </tr>
            <tr valign="top">
                <td>
                    <table width="119" border="0" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td width="9" class="v14-graphic-tab-selected">
                                    <img class="display-img" alt="" src="images/c.gif" width="9" height="19" />
                                </td>
                                <td width="153" class="v14-graphic-tab-selected">
                                    <a class="v14-tab-link-selected" href="javascript:void(0);">短信模板编辑</a>
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
                    <p>
                        功能说明: 编写及修改终端开卡,销售及积分换礼时系统自动发送的短信内容.</p>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="v14-header-3">
                                短信内容编写
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <span class="v14-ttd">短信类别：</span>
                                        </td>
                                        <td>
                                            <asp:RadioButton runat="server" GroupName="msgType" ID="type_1" Text="开卡短信" 
                                                Checked="true" oncheckedchanged="RB_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton runat="server" GroupName="msgType" ID="type_2" Text="销售短信" 
                                                oncheckedchanged="RB_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton runat="server" GroupName="msgType" ID="type_3" Text="积分兑换(成功)" 
                                                oncheckedchanged="RB_CheckedChanged" AutoPostBack="true" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton runat="server" GroupName="msgType" ID="type_4" Text="积分兑换(失败)" 
                                                oncheckedchanged="RB_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="v14-ttd">可使用字段：</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlMsgField" Width="150px">
                                            </asp:DropDownList>&nbsp;&nbsp;<img src="images/d_add.gif" alt="增加" width="16" height="16" /><span class="bluebullet"><a href="javascript:void(0);" onclick="insertAtCursor()">增加</a></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">
                                            <span class="v14-ttd">短信内容：</span>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" TextMode="MultiLine" Width="200px" Rows="5" ID="txtMessage" MaxLength="130"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">预估已使用字符<label id="lblUsed" style="color:Red">0</label>个. 剩余可编写字符数为: 
                                        <label id="lblunUsed" style="color:Red">130</label>个</td>
                                        </tr>
                                    <tr>
                                        <td align="left" colspan="2">
                                            <span class="bluebullet">
                                            <asp:LinkButton runat="server" ID="lbSave" Text="保存并替换" onclick="lbSave_Click"></asp:LinkButton>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:LinkButton runat="server" ID="lbRead" Text="获取该类别已经存在的模版内容" onclick="lbRead_Click"></asp:LinkButton>
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <a href="Main.aspx">返回首页</a>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

