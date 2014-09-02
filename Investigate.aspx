<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Investigate.aspx.cs" Inherits="Investigate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var currentPosition = 0;
            var slideWidth = 800;
            var slides = $('.slide');
            var numberOfSlides = slides.length; //个数
            var doc = document;
            $('#slidesContainer').css('overflow', 'hidden');
            slides.wrapAll('<div id="slideInner"></div>').css({ 'float': 'left', 'width': slideWidth });
            $('#slideInner').css('width', slideWidth * numberOfSlides);
            $('#slideshow')
            .prepend('<span class="control" id="leftControl" style="display:none"></span>')
            .append('<span class="control" id="rightControl" style="display:none"></span>');
            $('.control').bind('click', function () {
                //验证数据
                if (currentPosition == 0 && doc.getElementById('ctl00_cph_invDescribe').value == "") {
                    doc.getElementById("ctl00_cph_invDescribe").style.border = "2px solid red";
                    doc.getElementById("ctl00_cph_invDescribe").focus();
                    doc.getElementById("titleDErr").style.visibility = "visible";
                    return;
                }
                else {
                    doc.getElementById("ctl00_cph_invDescribe").style.border = "1px solid black";
                    doc.getElementById("titleDErr").style.visibility = "hidden";
                }
                currentPosition = ($(this).attr('id') == 'rightControl') ? currentPosition + 1 : currentPosition - 1;
                $('#slideInner').animate({ 'marginLeft': slideWidth * (-currentPosition) });
            });
            /***********************将后台的验证放在了这里************************/
            $("[id$=btnSave]").click(function (e) {
                if ($("[id$=CheckBox1]:checked").length > 1) {
                    e.preventDefault();
                    alert("同一时间只能启用一个问卷.请修改后重新提交!");
                    return false;
                }
            });
            /***********************将后台的验证放在了这里************************/

            /***********************内容描述不能为空************************/
            $("[id$=ask_add]").click(function (e) {
                if ($("[id$=invTextDcb]").val().length === 0) {
                    e.preventDefault();
                    $("[id$=invTextDcb]").css("border", "2px solid red").focus();
                    doc.getElementById("conDecErr").style.visibility = "visible";
                    return false;
                }
                else {
                    $("[id$=invTextDcb]").css("border", "1px solid black");
                    doc.getElementById("conDecErr").style.visibility = "hidden";
                }
            });

            /***********************内容描述不能为空************************/
        });

        function goTest(obj) {
            var str = prompt("输入要调研问卷的VIP的卡号"); //针对某个VIP进行调研问卷
            if (str) {
                if (str.length === 0) {
                    return false;
                }
                window.open("invsAnswer.aspx?vipID=" + str + "&invsID=" + obj);
            }
            else {
                return false;
            }
        }
        function transferModel(obj) {
            window.open("EmailModel.htm?invsID=" + obj);
        }
    </script>
    <link rel="stylesheet" href="cssFiles/InvestigatePage.css" type="text/css" />
    <script type="text/javascript" src="javascriptFiles/JS_Investigate.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManager runat="server">
    </asp:ScriptManager>
    <div style="width: 100%; position: relative; left: 10px;">
        <div style="left: 5px">
            调查问卷管理<br />
            <asp:GridView ID="GridView1" CssClass="gridview1" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Horizontal" Width="796px" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TID" HeaderText="编码标识" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="invDescribe" HeaderText="问卷描述" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="invDate" HeaderText="建立时间" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="启用状态">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("insState") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="转向测试">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID='link1' ToolTip='<%# Bind("TID") %>' OnClientClick='return goTest(this.title)'
                                Text="转向测试"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="生成邮件模板">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="model" ToolTip='<%# Bind("TID") %>' OnClientClick="transferModel(this.title)"
                                Text="生成邮件模板"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <img alt="保存" src="images/ico_config.png" width="16" height="16" />
            <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click">保存修改</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img alt="" src="images/ico_config.png" width="16" height="16" />
            <a href="javascript:void(0);" onclick="divShow(1)">新建调研问卷</a>
        </div>
        <br />
        <div id="pageContainer" style="position: absolute; margin-top: 0px; margin-left: 0px;
            display: none">
            <div id="head">
                <p style="font-size: large; font-weight: bold" class="v14-header-3">
                    调研问卷新建向导&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);" onclick="divShow(0)">关闭</a>
                </p>
            </div>
            <!-- Slideshow HTML -->
            <div id="slideshow">
                <div id="slidesContainer">
                    <div class="slide">
                        <table width="600px" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <h2>
                                        调研问卷主要描述</h2>
                                    <br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    *为此问卷的简介.用户在访问调研页面时及管理员进行VIP相关查询时可见.<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="invDescribe" Width="400px" Height="20px"></asp:TextBox><span
                                        id="titleDErr" style="visibility: hidden; color: Red;">*问卷描述不能为空！</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    *注:英文或汉字组成.5个字符以上.<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="bluebullet">
                                    <div align="center" class="agree_Bottom">
                                        <a href="javascript:void(0);" onclick="move('next')">下一步</a></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="slide">
                        <table width="600px" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <h2>
                                        问卷内容编辑</h2>
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px">
                                    内容类型:<asp:RadioButton runat="server" GroupName="type" ID="rb1" Text="短文本" Checked="true"
                                        onclick="inputType()" />&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" GroupName="type" ID="rb2" Text="长文本" onclick="inputType()" />&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" GroupName="type" ID="rb3" Text="单选按钮组" onclick="inputType()" />&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" GroupName="type" ID="rb4" Text="复选框" onclick="inputType()" /><br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px">
                                    *内容描述:<br />
                                    <asp:TextBox runat="server" ID="invTextDcb" Width="350px" Height="20px"></asp:TextBox><span
                                        id="conDecErr" style="visibility: hidden; color: Red;">*内容描述不能为空！</span><br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="inputTxt" style="display: none">
                                <td style="line-height: 20px">
                                    *复选/单选组内容预编辑(选项间需用逗号"<span style="color: Red">,</span>"分割,比如输入"<span style="color: Red">男,女</span>"不包括双引号""):<br />
                                    <asp:TextBox runat="server" ID="inputText" Width="350px" Height="20px"></asp:TextBox><br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px">
                                    <asp:UpdatePanel runat="server" ID="up1">
                                        <ContentTemplate>
                                            <table width="400px">
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="ask_add" Text="添加一个问卷内容" OnClick="ask_add_Click"></asp:LinkButton>
                                                    </td>
                                                    <td align="center">
                                                        <p style="background-color: Lime; height: 10px; color: Black; display: none" id="PTIPS">
                                                            已添加完成</p>
                                                    </td>
                                                    <td align="right">
                                                        已添加&nbsp;<asp:Label runat="server" ID="lblTip" Text="0" Style="color: Red"></asp:Label>&nbsp;条问题
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px">
                                    <table width="100%">
                                        <tr>
                                            <td align="left">
                                                <div align="left" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="move('preview')">上一步</a></div>
                                            </td>
                                            <td align="right" class="bluebullet">
                                                <div align="center" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="move('next')">下一步</a></div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="slide">
                        <table width="90%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <h3>
                                                已添加问题总览.点击[x]按钮可删除</h3>
                                            <div style="height: 300px; overflow: scroll">
                                                <asp:PlaceHolder ID="PH" runat="server"></asp:PlaceHolder>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <div align="left" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="move('preview')">上一步</a></div>
                                            </td>
                                            <td align="right" class="bluebullet">
                                                <div align="center" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="move('next')">下一步</a></div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="slide">
                        <table width="90%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <h2>
                                        完成调研问卷新建</h2>
                                    <br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    调研问卷问题已构建完成.<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    您可以选择"预览"该问卷,或返回修改问卷问题<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    或<asp:LinkButton runat="server" ID="lbSubmit" Text="点此完成" OnClick="invAdd_Click"></asp:LinkButton>该问卷<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <div align="left" class="agree_Bottom">
                                        <a href="javascript:void(0);" onclick="move('preview')">上一步</a></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
