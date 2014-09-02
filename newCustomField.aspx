<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="newCustomField.aspx.cs" Inherits="newCustomField" %>
    <%@ OutputCache Duration="6000" VaryByParam="none" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 70px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <table width="100%" border="0px">
        <tr>
            <td>
                自定义字段管理
            </td>
        </tr>
        <tr>
            <td>
                <div id="div1">
                    <table width="100%">
                        <tr>
                            <td align="left">
                                <label>
                                    请选择需要建立字段的类型:</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type1" value="公式" name="st" />
                                <label for="type1">
                                    公式----根据您定义的公式表达式派生其值的只读字段。当任何来源字段更改时，该公式字段将更新。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type2" value="URL" name="st" />
                                <label for="type2">
                                    URL----允许用户输入任何有效的网址。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type3" value="电话" name="st" />
                                <label for="type3">
                                    电话----允许用户输入任何电话号码。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type4" value="传真" name="st" />
                                <label for="type4">
                                    传真----允许用户输入任何传真号码。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type5" value="电子邮件" name="st" />
                                <label for="type5">
                                    电子邮件----允许用户输入电子邮件地址。将对输入的地址进行验证以确保其格式正确。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type6" value="复选框" name="st" />
                                <label for="type6">
                                    单选框----允许用户选择“真”（选取）或“假”（不选取）值。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type7" value="日期" name="st" />
                                <label for="type7">
                                    日期----允许用户输入日期或从弹出式日历中选择日期。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type8" value="整数" name="st" />
                                <label for="type8">
                                    整数----允许用户输入任何整数。将删除前置零。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type9" value="数字" name="st" />
                                <label for="type9">
                                    数字----允许用户输入任何数字。将删除前置零。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type10" value="文本" name="st" checked="checked" />
                                <label for="type10">
                                    文本----允许用户输入任何字母和数字组合。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type11" value="币种" name="st" />
                                <label for="type11">
                                    币种----允许用户输入人民币或其他货币金额，并将字段自动转换为货币金额格式。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <input type="radio" id="type12" value="邮编" name="st" />
                                <label for="type12">
                                    邮编----允许用户输入任何邮编号码。</label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <a id="btnPre" class="bluebullet" href="javascript:void(0);" onclick="btnPreview_Click()">上一步</a>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <a id="btnNext" class="bluebullet"
                                    href="javascript:void(0);" onclick="btnNext_Click()">下一步</a>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="div2" style="display: none">
                    <table id="tableDefault" border="0" cellpadding="2" cellspacing="0">
                        <tr>
                            <td>
                                <span class="v14-ttd">字段名：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFiledName" class="inputboder" onchange="nameCheck()"></asp:TextBox>
                                <label style="color: Lime; display: none" id="nameTips">
                                    </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">字段类型：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFiledType" class="inputboder" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">字段长度：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFieldLenth" class="inputboder"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">初始值：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFieldDefault" class="inputboder" Text="无"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">字段说明：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtFieldCNname" class="inputboder" MaxLength="10"
                                    Width="175px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span class="bluebullet">
                                    <img src="images/ico_config.png" alt="" width="16" height="16" />
                                    <a href="javascript:void(0);" id="btnOK" class="bluebullet" onclick="addNewField('default')">增加字段</a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="images/ico_del.gif" alt="" width="16" height="16" />
                                    <a href="javascript:void(0);" onclick="btnPreview_Click()">返回前页</a> </span>
                            </td>
                        </tr>
                    </table>
                    <table id="tableRadio" border="0" cellpadding="2" cellspacing="0" style="display: none">
                        <tr>
                            <td>
                                <span class="v14-ttd">字段名：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRadioName" class="inputboder"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">字段类型：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRadioType" class="inputboder" ReadOnly="True"
                                    Text="单选框"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">字段说明：</span>
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRadioCNname" class="inputboder" MaxLength="10"
                                    Width="175px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span class="v14-ttd">字段初始值：</span>
                            </td>
                            <td>
                                <asp:RadioButton runat="server" ID="radio1" GroupName="r1" Text="是(真)" Checked="true" />
                                &nbsp;&nbsp;
                                <asp:RadioButton runat="server" ID="radio2" GroupName="r1" Text="否(假)" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span class="bluebullet">
                                    <img src="images/ico_config.png" width="16" height="16" alt="" />
                                    <a href="javascript:void(0);" id="btnRadio" class="bluebullet" onclick="addNewField('radio')">增加字段</a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="images/ico_del.gif" width="16" height="16" alt="" />
                                    <a href="javascript:void(0);" onclick="btnPreview_Click()">返回前页</a> </span>
                            </td>
                        </tr>
                    </table>
                    <table id="tableFormula" width="100%" border="0" cellpadding="0" cellspacing="0"
                        style="display: none">
                        <tr>
                            <td colspan="2" class="v14-header-3">
                                添加公式
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style1">
                                &nbsp;
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style1">
                                <span class="v14-ttd">字段名称：</span>
                            </td>
                            <td valign="top">
                                <asp:TextBox runat="server" ID="txtFormulaName" class="inputboder"></asp:TextBox>
                                (英文及下划线组成)
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style1">
                                <span class="v14-ttd">字段说明： </span>
                            </td>
                            <td valign="top">
                                <asp:TextBox runat="server" ID="txtFormulaCName" class="inputboder"></asp:TextBox>
                                (10个字符以内)
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style1">
                                <span class="v14-ttd">公式类型：
                                    <br />
                                </span>
                            </td>
                            <td valign="top">
                                <table>
                                    <tr>
                                        <td align="left">
                                            <input type="radio" id="fr1" value="日期" name="st" />
                                            <label for="fr1">
                                                日期----计算一个日期，例如，通过在其它日期上相加或相减一定的天数.</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <input type="radio" id="fr2" value="整数" name="st" />
                                            <label for="fr2">
                                                整数----计算整数值。</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <input type="radio" id="fr3" value="数字" name="st" />
                                            <label for="fr3">
                                                数字----计算数字值。</label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <input type="radio" id="fr4" value="文本" name="st" checked="checked" />
                                            <label for="fr4">
                                                文本----创建一个文本字符串，例如，通过串连其它几个文本字段。</label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" class="v14-header-3">
                                公式字段函数编写:
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" class="style1">
                                <span class="v14-ttd">添加字段：</span>
                            </td>
                            <td align="left" valign="top" nowrap="nowrap">
                                <asp:DropDownList runat="server" ID="ddlField" Width="167px">
                                    <asp:ListItem Value="0">--请选择要使用的字段--</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp;&nbsp; <a href="javascript:void(0);" class="bluebullet" onclick="insertAtCursor()">增加</a>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="style1">
                                <span class="v14-ttd">公式编写：</span>
                            </td>
                            <td colspan="3" valign="top">
                                <asp:TextBox runat="server" ID="txtMain" TextMode="MultiLine" Style="height: 100px;
                                    width: 400px" class="inputboder"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <span class="bluebullet">
                                    <img src="images/ico_config.png" width="16" height="16" alt="" />
                                    <a href="javascript:void(0);" id="A1" class="bluebullet" onclick="addFormulaField()">增加字段</a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <img src="images/ico_del.gif" width="16" height="16" alt="" />
                                    <a href="javascript:void(0);" onclick="btnPreview_Click()">返回前页</a> </span>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript" src="javascriptFiles/JS_newCustomField.js"></script>
</asp:Content>
