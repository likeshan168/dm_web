<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="salesParticular.aspx.cs" Inherits="salesParticular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        td
        {
            font-size: 12px;
            color: #000000;
            line-height: 150%;
        }
        .sec1
        {
            background-color: lightgray;
            cursor: pointer;
            color: #000000;
            border-left: 1px solid #FFFFFF;
            border-top: 1px solid #FFFFFF;
            border-right: 1px solid gray;
            border-bottom: 1px solid #FFFFFF;
        }
        .sec2
        {
            background-color: #c8d7e3;
            cursor: pointer;
            color: #000000;
            border-left: 1px solid #FFFFFF;
            border-top: 1px solid #FFFFFF;
            border-right: 1px solid gray;
            font-weight: bold;
        }
        .main_tab
        {
            background-color: #FFFFFF;
            color: #000000;
            border-left: 1px solid #FFFFFF;
            border-right: 1px solid gray;
            border-bottom: 1px solid gray;
        }
        .GVsales td, th
        {
            text-align: center;
        }
        
        .blankCondition
        {
            border: 1px solid red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    消费明细综合查询<br />
    <div runat="server" id="divScreen">
        <table border="1" cellspacing="0" cellpadding="0" width="300" id="secTable" bordercolor="#FF9900">
            <tr height="20" align="center">
                <td class="sec2" width="10%" onclick="secBoard(0)">
                    设置查询条件
                </td>
                <td class="sec1" width="10%" onclick="secBoard(1)">
                    消费记录查询结果
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" width="100%" height="240" id="mainTable"
            class="main_tab">
            <tbody style="display: block;">
                <tr>
                    <td align="left" valign="top" class="main_tab">
                        <table>
                            <tr>
                                <td colspan="3" class="v14-header-3">
                                    请选择排序依据
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel runat="server" ID="pan1">
                                        <asp:RadioButton runat="server" ID="times" GroupName="fz" Checked="true" Text="消费次数" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton runat="server" ID="qty" GroupName="fz" Text="购买数量" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton runat="server" ID="amount" GroupName="fz" Text="消费金额" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="v14-header-3">
                                    请选择查询条件(<span style="color: Red; font-weight: bold">注意 ：以下各行条件是与的关系,未选中任何条件则查询所有的数据</span>)
                                </td>
                            </tr>
                            <tr id="tr1">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlFiled1" Width="149px">
                                        <asp:ListItem Value="无">--无--</asp:ListItem>
                                        <asp:ListItem Value="卡号">vip卡号</asp:ListItem>
                                        <asp:ListItem Value="姓名">vip姓名</asp:ListItem>
                                        <asp:ListItem Value="卡类型">卡类型</asp:ListItem>
                                        <asp:ListItem Value="发卡人">发卡人ID</asp:ListItem>
                                        <asp:ListItem Value="购物总数">购物总数</asp:ListItem>
                                        <asp:ListItem Value="消费日期">消费日期</asp:ListItem>
                                        <asp:ListItem Value="消费金额">消费金额</asp:ListItem>
                                        <asp:ListItem Value="消费次数">消费次数</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlCondition1" Width="97px">
                                        <asp:ListItem Value="大于">大于</asp:ListItem>
                                        <asp:ListItem Value="大于或等于">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="等于">等于</asp:ListItem>
                                        <asp:ListItem Value="小于或等于">小于或等于</asp:ListItem>
                                        <asp:ListItem Value="小于">小于</asp:ListItem>
                                        <asp:ListItem>包含</asp:ListItem>
                                        <asp:ListItem>不包含</asp:ListItem>
                                        <asp:ListItem>起始字符</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox runat="server" ID="txtText1"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr2">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlFiled2" Width="149px">
                                        <asp:ListItem Value="无">--无--</asp:ListItem>
                                        <asp:ListItem Value="卡号">vip卡号</asp:ListItem>
                                        <asp:ListItem Value="姓名">vip姓名</asp:ListItem>
                                        <asp:ListItem Value="销售员">销售员</asp:ListItem>
                                        <asp:ListItem Value="发卡人">发卡人ID</asp:ListItem>
                                        <asp:ListItem Value="购物总数">购物总数</asp:ListItem>
                                        <asp:ListItem Value="消费日期">消费日期</asp:ListItem>
                                        <asp:ListItem Value="消费金额">消费金额</asp:ListItem>
                                        <asp:ListItem Value="消费次数">消费次数</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlCondition2" Width="97px">
                                        <asp:ListItem Value="大于">大于</asp:ListItem>
                                        <asp:ListItem Value="大于或等于">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="等于">等于</asp:ListItem>
                                        <asp:ListItem Value="小于或等于">小于或等于</asp:ListItem>
                                        <asp:ListItem Value="小于">小于</asp:ListItem>
                                        <asp:ListItem>包含</asp:ListItem>
                                        <asp:ListItem>不包含</asp:ListItem>
                                        <asp:ListItem>起始字符</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox runat="server" ID="txtText2"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr3">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlFiled3" Width="149px">
                                        <asp:ListItem Value="无">--无--</asp:ListItem>
                                        <asp:ListItem Value="卡号">vip卡号</asp:ListItem>
                                        <asp:ListItem Value="姓名">vip姓名</asp:ListItem>
                                        <asp:ListItem Value="卡类型">卡类型</asp:ListItem>
                                        <asp:ListItem Value="发卡人">发卡人ID</asp:ListItem>
                                        <asp:ListItem Value="购物总数">购物总数</asp:ListItem>
                                        <asp:ListItem Value="消费日期">消费日期</asp:ListItem>
                                        <asp:ListItem Value="消费金额">消费金额</asp:ListItem>
                                        <asp:ListItem Value="消费次数">消费次数</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlCondition3" Width="97px">
                                        <asp:ListItem Value="大于">大于</asp:ListItem>
                                        <asp:ListItem Value="大于或等于">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="等于">等于</asp:ListItem>
                                        <asp:ListItem Value="小于或等于">小于或等于</asp:ListItem>
                                        <asp:ListItem Value="小于">小于</asp:ListItem>
                                        <asp:ListItem>包含</asp:ListItem>
                                        <asp:ListItem>不包含</asp:ListItem>
                                        <asp:ListItem>起始字符</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox runat="server" ID="txtText3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr4">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlFiled4" Width="149px">
                                        <asp:ListItem Value="无">--无--</asp:ListItem>
                                        <asp:ListItem Value="卡号">vip卡号</asp:ListItem>
                                        <asp:ListItem Value="姓名">vip姓名</asp:ListItem>
                                        <asp:ListItem Value="卡类型">卡类型</asp:ListItem>
                                        <asp:ListItem Value="发卡人">发卡人ID</asp:ListItem>
                                        <asp:ListItem Value="购物总数">购物总数</asp:ListItem>
                                        <asp:ListItem Value="消费日期">消费日期</asp:ListItem>
                                        <asp:ListItem Value="消费金额">消费金额</asp:ListItem>
                                        <asp:ListItem Value="消费次数">消费次数</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlCondition4" Width="97px">
                                        <asp:ListItem Value="大于">大于</asp:ListItem>
                                        <asp:ListItem Value="大于或等于">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="等于">等于</asp:ListItem>
                                        <asp:ListItem Value="小于或等于">小于或等于</asp:ListItem>
                                        <asp:ListItem Value="小于">小于</asp:ListItem>
                                        <asp:ListItem>包含</asp:ListItem>
                                        <asp:ListItem>不包含</asp:ListItem>
                                        <asp:ListItem>起始字符</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox runat="server" ID="txtText4"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="tr5">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlFiled5" Width="149px">
                                        <asp:ListItem Value="无">--无--</asp:ListItem>
                                        <asp:ListItem Value="卡号">vip卡号</asp:ListItem>
                                        <asp:ListItem Value="姓名">vip姓名</asp:ListItem>
                                        <asp:ListItem Value="卡类型">卡类型</asp:ListItem>
                                        <asp:ListItem Value="发卡人">发卡人ID</asp:ListItem>
                                        <asp:ListItem Value="购物总数">购物总数</asp:ListItem>
                                        <asp:ListItem Value="消费日期">消费日期</asp:ListItem>
                                        <asp:ListItem Value="消费金额">消费金额</asp:ListItem>
                                        <asp:ListItem Value="消费次数">消费次数</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="ddlCondition5" Width="97px">
                                        <asp:ListItem Value="大于">大于</asp:ListItem>
                                        <asp:ListItem Value="大于或等于">大于或等于</asp:ListItem>
                                        <asp:ListItem Value="等于">等于</asp:ListItem>
                                        <asp:ListItem Value="小于或等于">小于或等于</asp:ListItem>
                                        <asp:ListItem Value="小于">小于</asp:ListItem>
                                        <asp:ListItem>包含</asp:ListItem>
                                        <asp:ListItem>不包含</asp:ListItem>
                                        <asp:ListItem>起始字符</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:TextBox runat="server" ID="txtText5"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="bluebullet">
                                    <div align="center" class="agree_Bottom">
                                        <a href="javascript:void(0);" onclick="startQuery(this);">按条件查询</a></div>
                                </td>
                                <td align="center" class="bluebullet">
                                    <div align="center" class="agree_Bottom">
                                        <a href="javascript:void(0);" onclick="resetConditon();">重置查询条件</a></div>
                                </td>
                                <td align="center" class="bluebullet">
                                    <div align="center" class="agree_Bottom">
                                        <a href="javascript:void(0);">取消并返回</a></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
            <tbody style="display: none;">
                <tr>
                    <td align="left" valign="top">
                        <asp:ScriptManager runat="server" ID="sm1">
                        </asp:ScriptManager>
                        <div style="overflow: scroll; height: 427px; width: 90%">
                            <asp:UpdatePanel runat="server" ID="up1" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GVsales" CssClass="GVsales" runat="server" CellPadding="4" BackColor="White"
                                        BorderColor="#CCCCCC" BorderStyle="Dotted" BorderWidth="1px" ForeColor="Black"
                                        GridLines="Horizontal" Font-Size="Smaller" RowStyle-Wrap="false" Width="100%"
                                        Style="word-break: keep-all" AutoGenerateColumns="False" OnRowCreated="GVsales_RowCreated"
                                        OnRowDataBound="GVsales_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="ckb" runat="server" Checked="true" />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <input id="ckbAll" type="checkbox" checked="checked" onclick="selectAll(this);" />本页全选
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="用户卡号" DataField="card_id" />
                                            <asp:BoundField HeaderText="卡类型" DataField="card_Type" />
                                            <asp:BoundField HeaderText="vip姓名" DataField="userName" />
                                            <asp:BoundField HeaderText="发卡人编号" DataField="sendMan" />
                                            <asp:BoundField HeaderText="消费次数" DataField="times" />
                                            <asp:BoundField HeaderText="购买数量" DataField="qty" />
                                            <asp:BoundField HeaderText="购买金额" DataField="amount" />
                                        </Columns>
                                        <RowStyle Wrap="False"></RowStyle>
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <PagerStyle Wrap="False" BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" Wrap="false" />
                                        <EditRowStyle Wrap="False" />
                                        <AlternatingRowStyle Wrap="False" />
                                    </asp:GridView>
                                    <asp:Button runat="server" ID="btnHideVipBind" OnClick="btnHideVipBind_Click" Style="display: none" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id="tdVipTotal" style="display: none">
                        当前显示查询结果的第<span id="spVipNow" style="color: Red"></span>页, 本次供查询数据<span id="spVipTotal"
                            style="color: Red"></span>页.
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0);" id="pcFirst" onclick="pageChanged('first')">首页</a>&nbsp;&nbsp;
                        <a href="javascript:void(0);" id="pcPreview" onclick="pageChanged('preview')">上一页</a>&nbsp;&nbsp;
                        <a href="javascript:void(0);" id="pcNext" onclick="pageChanged('next')">下一页</a>&nbsp;&nbsp;
                        <a href="javascript:void(0);" id="pcLast" onclick="pageChanged('last')">尾页</a>&nbsp;&nbsp;
                        跳转至
                        <input type="text" id="txtGoto" style="width: 40px" />&nbsp;&nbsp;
                        <input type="button" value="跳转" onclick="pageChanged('goto')" />
                        <a href="#divDetails" id="a_details" class="nyroModal" style="display: none;">详细信息</a>
                        <a href="#message" class="nyroModal">发送短信和邮件</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!-- 详细信息-->
    <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div id="divDetails" style="width: 950px; height: 600px; display: none">
                <table width="100%">
                  
                    <tr>
                        <td>
                            <div style="overflow: scroll; height: 427px; width: 100%">
                                <asp:GridView ID="gvDetails" CssClass="GVsales" runat="server" CellPadding="4" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="Dotted" BorderWidth="1px" ForeColor="Black"
                                    GridLines="Horizontal" Font-Size="Smaller" RowStyle-Wrap="false" Width="100%"
                                    Style="word-break: keep-all" AutoGenerateColumns="False" OnRowCreated="gvDetails_RowCreated">
                                    <Columns>
                                        <asp:BoundField DataField="vip_id" HeaderText="会员卡号" />
                                        <asp:BoundField DataField="bill_id" HeaderText="销售单号" />
                                        <asp:BoundField DataField="salerName" HeaderText="售货员" />
                                        <asp:BoundField DataField="clientName" HeaderText="消费地点" />
                                        <asp:BoundField DataField="sale_time" HeaderText="消费日期" />
                                        <asp:BoundField DataField="productCode" HeaderText="商品编号" />
                                        <asp:BoundField DataField="productName" HeaderText="商品名称" />
                                        <asp:BoundField DataField="qty" HeaderText="购买数量" />
                                        <asp:BoundField DataField="discount" HeaderText="折扣" />
                                        <asp:BoundField DataField="amount" HeaderText="金额" />
                                    </Columns>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <PagerStyle Wrap="False" BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" Wrap="false" />
                                    <EditRowStyle Wrap="False" />
                                    <AlternatingRowStyle Wrap="False" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button runat="server" ID="btnHideDetails" Text="123445" OnClick="btnHideDetails_Click"
                Style="display: none" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="display: none;width:300px;height:450px;" id="message">
        <table width="100%" style="height:100%;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="data-table-3"
                                align="center">
                                <tr>
                                    <td class="v14-header-3" colspan="2">
                                        短信发送
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *已保存短信:
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlSMSlist" Width="150px" onchange="smsModelRead()">
                                            <asp:ListItem Selected="True" Text="--无--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <input type="button" value="删除" id="btnDelSMS" onclick="smsModelDelete()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *添加属性字段:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProperty" runat="server" Width="150px">
                                        </asp:DropDownList>
                                        <a href="javascript:void(0);" class="bluebullet" onclick="insertAtCursor()">增加</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *短信内容简介:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSMSTitle" Width="150px"></asp:TextBox>
                                        <span id="titleErr" style="visibility: hidden; color: Red">*内容简介不能为空</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *短信内容:
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSMSText" TextMode="MultiLine" Width="300px" Rows="5"></asp:TextBox>
                                        <span id="contentErr" style="visibility: hidden; color: Red">*短信内容不能为空</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        *发送通道:
                                    </td>
                                    <td>
                                        <input type="radio" name="channel" id="channel1" checked="checked" /><label for="channel1">一般通道</label>
                                        (余额:<asp:Label runat="server" ID="lblSmsBabalce1"></asp:Label>) &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input type="radio" name="channel" id="channel2" /><label for="channel2">广告通道</label>
                                        (余额:<asp:Label runat="server" ID="lblSmsBabalce2"></asp:Label>)
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        *提示:具有打折、促销等广告类短信请选择广告通道，<label style="color: red">否则可能导致短信发送失败并扣费及普通通道被禁用</label>。
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left">
                                        <div id="smsSendTips" style="color: Black; background-color: Lime; width: 100%; display: none">
                                            短信处理中,请稍候...</div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="2">
                                        <table align="left" width="70%">
                                            <tr>
                                                <td>
                                                    <div align="center" class="agree_Bottom">
                                                        <a href="javascript:void(0);" onclick="smsModelSave()">保存此条短信</a></div>
                                                </td>
                                                <td>
                                                    <div align="center" class="agree_Bottom">
                                                        <a href="javascript:void(0);" onclick="smsMain('send','false')">对所选用户发送短信</a></div>
                                                </td>
                                                <td>
                                                    <div align="center" class="agree_Bottom">
                                                        <a href="javascript:void(0);" onclick="javascript:if(confirm('您确定您确定对所有用户发送短信吗')){smsMain('send','all');}">
                                                            对所有用户发送短信</a></div>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td colspan="2" class="v14-header-3">
                                邮件发送
                            </td>
                        </tr>
                        <tr>
                            <td>
                                *读取模板:
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:FileUpload runat="server" ID="FileUpload1" />
                                        </td>
                                        <td>
                                            <img src="images/loading.gif" id="imgModel" alt="加载中" style="display: none" />
                                        </td>
                                        <td>
                                            <a href="javascript:void(0);" id="a_addModel" class="bluebullet" onclick="addMailModel()">
                                                加载模板文件</a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <div align="center" class="agree_Bottom">
                                                <a href="javascript:void(0);" id="sendingMail" onclick="mailSend()">发送邮件</a>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript" src="javascriptFiles/JS_saleParticular.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".nyroModal").nyroModal();
        });
    </script>
</asp:Content>
