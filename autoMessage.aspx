<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="autoMessage.aspx.cs" Inherits="autoMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="cssFiles/couponPage.css" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManager runat="server" ID="sm1">
    </asp:ScriptManager>
    <div id="lib_Tab1">
        <div class="lib_Menubox lib_tabborder">
            <%--<ul>
               <li id="one1" onclick="setTab('one',1,2)" class="hover">自动短信设定</li>
               <li id="one2" onclick="setTab('one',2,2)">自动短信管理</li>  
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
        <div class="lib_Contentbox lib_tabborder"">
            <div id="con_one_1">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="v14-header-3">
                            新建自动短信模板
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" style="border:1px dotted;" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td width="135px">
                                        <span class="v14-ttd">发送时间类型：</span>
                                    </td>
                                    <td width="235px">
                                        <asp:RadioButton runat="server" GroupName="date" ID="rbDynamics" Text="动态日期" onclick="sendType()" />
                                        &nbsp;&nbsp;
                                        <asp:RadioButton runat="server" GroupName="date" ID="rbNormal" Text="固定日期" Checked="true"
                                            onclick="sendType()" />
                                            &nbsp;&nbsp;
                                            <asp:RadioButton runat="server" GroupName="date" ID="rbHoliday" Text="节日" onclick="sendType()" />
                                    </td>
                                    <td style="color: #575757">
                                        *可设定发送日期为某月某日发送比如"中秋节",或按照会员相关资料发送,比如"生日"当天
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="v14-ttd">发送日期：</span>
                                    </td>
                                    <td>
                                        <p id="s1">
                                            <asp:TextBox runat="server" ID="txtSendDate" Width="200px"></asp:TextBox></p>
                                        <p id="s2" style="display: none">
                                            <asp:DropDownList runat="server" ID="ddlSendDate" Width="200px">
                                                <asp:ListItem Selected="True" Text="生日" Value="生日"></asp:ListItem>
                                                <asp:ListItem Text="开卡日期" Value="开卡日期"></asp:ListItem>
                                            </asp:DropDownList>
                                        </p>
                                        <p id="s3" style="display: none">
                                            <asp:DropDownList runat="server" ID="ddlHoliday" Width="200px">
                                                <asp:ListItem Selected="True" Text="元旦" Value="元旦"></asp:ListItem>
                                                <asp:ListItem Text="春节" Value="春节"></asp:ListItem>
                                                <asp:ListItem Text="腊八" Value="腊八"></asp:ListItem>
                                                <asp:ListItem Text="七夕" Value="七夕"></asp:ListItem>
                                                <asp:ListItem Text="小年" Value="小年"></asp:ListItem>
                                                <asp:ListItem Text="端午节" Value="端午节"></asp:ListItem>
                                                <asp:ListItem Text="重阳节" Value="重阳节"></asp:ListItem>
                                                <asp:ListItem Text="元宵节" Value="元宵节"></asp:ListItem>
                                                <asp:ListItem Text="清明节" Value="清明节"></asp:ListItem>
                                                <asp:ListItem Text="中秋节" Value="中秋节"></asp:ListItem>
                                                
                                            </asp:DropDownList>
                                        </p>
                                    </td>
                                    <td style="color: #575757">
                                        *选择发送日期(格式如：2012-05-20为2012年5月20日)或根据会员资料进行发送.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="v14-ttd">发送日期调整：</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtDateAdd" Width="200px" Text="0" onfocus="javascript:this.select()"></asp:TextBox><br />
                                    </td>
                                    <td style="color: #575757">
                                        *在当前发送日期基础上提前(输入-2为提前两天)或延后(输入3为延后3天)发送.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="v14-ttd">模板概述：</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtTitle" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="color: #575757">
                                        *模板主要功能说明
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="v14-ttd">短信内容：</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlField" Width="160px">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp; <a href="javascript:void(0);" onclick="insertAtCursor()" class="bluebullet">添加</a><br />
                                        <asp:TextBox runat="server" TextMode="MultiLine" Rows="5" ID="txtText" Width="200px"></asp:TextBox>
                                    </td>
                                    <td valign="top" style="color: #575757">
                                        *短信模板编写.可选择相关字段填充,发送时将自动被替换掉.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="v14-ttd">发送目标筛选条件：</span>
                                    </td>
                                    <td colspan="2">
                                        <asp:UpdatePanel runat="server" ID="up1" UpdateMode="Always" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:DropDownList runat="server" ID="ddlFields" Width="135px">
                                                </asp:DropDownList>
                                                <asp:DropDownList runat="server" ID="ddlCdt">
                                                    <asp:ListItem Text="大于" Value="大于"></asp:ListItem>
                                                    <asp:ListItem Text="等于" Value="等于"></asp:ListItem>
                                                    <asp:ListItem Text="小于" Value="小于"></asp:ListItem>
                                                    <asp:ListItem Text="大于或等于" Value="大于或等于"></asp:ListItem>
                                                    <asp:ListItem Text="小于或等于" Value="小于或等于"></asp:ListItem>
                                                    <asp:ListItem Text="包含" Value="包含"></asp:ListItem>
                                                    <asp:ListItem Text="不包含" Value="不包含"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="txtValue" Width="155px"></asp:TextBox>
                                                <br />
                                                <asp:RadioButton runat="server" ID="rbCdt1" GroupName="rcdt" Text="并且" Checked="true" />
                                                <asp:RadioButton runat="server" ID="rbCdt2" GroupName="rcdt" Text="或者" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton runat="server" ID="btnAddcdt" Text="添加一组条件" OnClick="btnAddcdt_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton runat="server" ID="btnRemovecdt" Text="删除选中条件" OnClick="btnRemovecdt_Click"></asp:LinkButton>
                                                <br />
                                                <asp:ListBox ID="lbCdt" runat="server" Height="82px" Width="380px"></asp:ListBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="v14-header-3">
                            附属功能
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" style="border:1px dotted;" cellpadding="1" cellspacing="0">
                                <tr>
                                    <td width="135px">
                                        <span class="v14-ttd">是否附赠优惠券：</span>
                                    </td>
                                    <td width="225px">
                                        <asp:RadioButton runat="server" ID="rbCoupon1" GroupName="coupon" Text="附赠" Checked="true"
                                            onclick="couponDis()" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton runat="server" ID="rbCoupon2" GroupName="coupon" Text="暂不附赠" onclick="couponDis()" />
                                    </td>
                                    <td style="color: #575757">
                                        *用户在收到短信时将自动获得一个优惠券编号.若积分不符合此优惠券要求则不会获取.
                                    </td>
                                </tr>
                                <tr id="trCoupon">
                                    <td>
                                        <span class="v14-ttd">已有优惠券类型：</span>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlCouponList" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="color: #575757">
                                        *当前已存在的优惠券类型,可选择一个进行赠送.
                                    </td>
                                </tr>
                                <tr id="trCoupon1">
                                    <td>
                                        <span class="v14-ttd">优惠券过期时间：</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCouponDate" Width="200px" Text="30"></asp:TextBox>
                                    </td>
                                    <td style="color: #575757">
                                        *优惠券使用期限.
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span class="v14-ttd">是否赠送积分：</span>
                                    </td>
                                    <td>
                                        <asp:RadioButton runat="server" ID="rbScore1" GroupName="score" Text="附赠" Checked="true"
                                            onclick="scoreDis()" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton runat="server" ID="rbScore2" GroupName="score" Text="暂不附赠" onclick="scoreDis()" />
                                    </td>
                                    <td style="color: #575757">
                                        *用户收到短信同时会获取一定的积分并在短信中通知.
                                    </td>
                                </tr>
                                <tr id="trScore">
                                    <td>
                                        <span class="v14-ttd">输入积分面值：</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtScore" Width="200px" Text="0" onfocus="javascript:this.select()"></asp:TextBox>
                                    </td>
                                    <td style="color: #575757">
                                        *赠送的积分额度.
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="center">
                                        <asp:Button runat="server" ID="btnSubmit" Text="生成自动短信模板" OnClick="btnSubmit_Click" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="con_one_2"  style="display:none" align="left">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="v14-header-3">
                            现有自动短信模板状态管理
                        </td>
                    </tr>
                    <tr>
                        <td>
                        <asp:UpdatePanel runat="server" ID="upc">
                            <ContentTemplate>
                                <asp:GridView ID="GVmessage" CssClass="gvItem" runat="server" CellPadding="4" 
                                    BackColor="White" BorderColor="#CCCCCC"
                                        BorderStyle="Dotted" BorderWidth="1px" ForeColor="Black" GridLines="Horizontal"
                                        RowStyle-Wrap="false" Width="100%" Style="word-break: keep-all" 
                                AutoGenerateColumns="False" onrowcancelingedit="GVmessage_RowCancelingEdit" 
                                onrowdeleting="GVmessage_RowDeleting" onrowcommand="GVmessage_RowCommand" 
                                    onrowdatabound="GVmessage_RowDataBound" DataKeyNames="id">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="编号" />
                                            <asp:BoundField DataField="type" HeaderText="时间类型" />
                                            <asp:BoundField DataField="modelName" HeaderText="模板概述" />
                                            <asp:BoundField DataField="mainField" HeaderText="动态发送日期" />
                                            <asp:BoundField DataField="sendDate" HeaderText="固定发送日期" />
                                            <asp:BoundField DataField="dateAdd" HeaderText="发送时间调整" />
                                            <asp:BoundField DataField="lastSendDate" HeaderText="最后发送日期" />
                                            <asp:BoundField DataField="presendCoupon" HeaderText="附赠优惠券" />
                                            <asp:BoundField DataField="presendScore" HeaderText="附赠积分" />
                                            <asp:TemplateField HeaderText="使用状态">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbUsed" runat="server" Checked='<%# Eval("usingState") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/XX.gif" HeaderText="删除"
                                                ShowDeleteButton="True" ShowHeader="True">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:CommandField>--%>
                                            <asp:TemplateField HeaderText="删除">
                                            <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="delete" CommandName="del" ImageUrl="~/images/XX.gif" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    <RowStyle Wrap="False"></RowStyle>
                                </asp:GridView>
                                <br />
                                <img src="images/ico_config.png" width="16" height="16" />
                                <asp:LinkButton runat="server" ID="btnUpdate" OnClick="btnUpdate_Click">保存所做修改</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="javascriptFiles/JS_autoMessage.js"></script>
</asp:Content>
