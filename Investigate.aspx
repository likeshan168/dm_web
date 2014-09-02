<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Investigate.aspx.cs" Inherits="Investigate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var currentPosition = 0;
            var slideWidth = 800;
            var slides = $('.slide');
            var numberOfSlides = slides.length; //����
            var doc = document;
            $('#slidesContainer').css('overflow', 'hidden');
            slides.wrapAll('<div id="slideInner"></div>').css({ 'float': 'left', 'width': slideWidth });
            $('#slideInner').css('width', slideWidth * numberOfSlides);
            $('#slideshow')
            .prepend('<span class="control" id="leftControl" style="display:none"></span>')
            .append('<span class="control" id="rightControl" style="display:none"></span>');
            $('.control').bind('click', function () {
                //��֤����
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
            /***********************����̨����֤����������************************/
            $("[id$=btnSave]").click(function (e) {
                if ($("[id$=CheckBox1]:checked").length > 1) {
                    e.preventDefault();
                    alert("ͬһʱ��ֻ������һ���ʾ�.���޸ĺ������ύ!");
                    return false;
                }
            });
            /***********************����̨����֤����������************************/

            /***********************������������Ϊ��************************/
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

            /***********************������������Ϊ��************************/
        });

        function goTest(obj) {
            var str = prompt("����Ҫ�����ʾ��VIP�Ŀ���"); //���ĳ��VIP���е����ʾ�
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
            �����ʾ����<br />
            <asp:GridView ID="GridView1" CssClass="gridview1" runat="server" BackColor="White"
                BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                GridLines="Horizontal" Width="796px" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TID" HeaderText="�����ʶ" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="invDescribe" HeaderText="�ʾ�����" HeaderStyle-Wrap="false" />
                    <asp:BoundField DataField="invDate" HeaderText="����ʱ��" HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="����״̬">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("insState") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ת�����">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID='link1' ToolTip='<%# Bind("TID") %>' OnClientClick='return goTest(this.title)'
                                Text="ת�����"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="�����ʼ�ģ��">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="model" ToolTip='<%# Bind("TID") %>' OnClientClick="transferModel(this.title)"
                                Text="�����ʼ�ģ��"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            <img alt="����" src="images/ico_config.png" width="16" height="16" />
            <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click">�����޸�</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img alt="" src="images/ico_config.png" width="16" height="16" />
            <a href="javascript:void(0);" onclick="divShow(1)">�½������ʾ�</a>
        </div>
        <br />
        <div id="pageContainer" style="position: absolute; margin-top: 0px; margin-left: 0px;
            display: none">
            <div id="head">
                <p style="font-size: large; font-weight: bold" class="v14-header-3">
                    �����ʾ��½���&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="javascript:void(0);" onclick="divShow(0)">�ر�</a>
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
                                        �����ʾ���Ҫ����</h2>
                                    <br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    *Ϊ���ʾ�ļ��.�û��ڷ��ʵ���ҳ��ʱ������Ա����VIP��ز�ѯʱ�ɼ�.<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="invDescribe" Width="400px" Height="20px"></asp:TextBox><span
                                        id="titleDErr" style="visibility: hidden; color: Red;">*�ʾ���������Ϊ�գ�</span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    *ע:Ӣ�Ļ������.5���ַ�����.<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="bluebullet">
                                    <div align="center" class="agree_Bottom">
                                        <a href="javascript:void(0);" onclick="move('next')">��һ��</a></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="slide">
                        <table width="600px" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <h2>
                                        �ʾ����ݱ༭</h2>
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px">
                                    ��������:<asp:RadioButton runat="server" GroupName="type" ID="rb1" Text="���ı�" Checked="true"
                                        onclick="inputType()" />&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" GroupName="type" ID="rb2" Text="���ı�" onclick="inputType()" />&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" GroupName="type" ID="rb3" Text="��ѡ��ť��" onclick="inputType()" />&nbsp;&nbsp;
                                    <asp:RadioButton runat="server" GroupName="type" ID="rb4" Text="��ѡ��" onclick="inputType()" /><br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 20px">
                                    *��������:<br />
                                    <asp:TextBox runat="server" ID="invTextDcb" Width="350px" Height="20px"></asp:TextBox><span
                                        id="conDecErr" style="visibility: hidden; color: Red;">*������������Ϊ�գ�</span><br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr id="inputTxt" style="display: none">
                                <td style="line-height: 20px">
                                    *��ѡ/��ѡ������Ԥ�༭(ѡ������ö���"<span style="color: Red">,</span>"�ָ�,��������"<span style="color: Red">��,Ů</span>"������˫����""):<br />
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
                                                        <asp:LinkButton runat="server" ID="ask_add" Text="���һ���ʾ�����" OnClick="ask_add_Click"></asp:LinkButton>
                                                    </td>
                                                    <td align="center">
                                                        <p style="background-color: Lime; height: 10px; color: Black; display: none" id="PTIPS">
                                                            ��������</p>
                                                    </td>
                                                    <td align="right">
                                                        �����&nbsp;<asp:Label runat="server" ID="lblTip" Text="0" Style="color: Red"></asp:Label>&nbsp;������
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
                                                    <a href="javascript:void(0);" onclick="move('preview')">��һ��</a></div>
                                            </td>
                                            <td align="right" class="bluebullet">
                                                <div align="center" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="move('next')">��һ��</a></div>
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
                                                �������������.���[x]��ť��ɾ��</h3>
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
                                                    <a href="javascript:void(0);" onclick="move('preview')">��һ��</a></div>
                                            </td>
                                            <td align="right" class="bluebullet">
                                                <div align="center" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="move('next')">��һ��</a></div>
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
                                        ��ɵ����ʾ��½�</h2>
                                    <br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    �����ʾ������ѹ������.<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ������ѡ��"Ԥ��"���ʾ�,�򷵻��޸��ʾ�����<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ��<asp:LinkButton runat="server" ID="lbSubmit" Text="������" OnClick="invAdd_Click"></asp:LinkButton>���ʾ�<br />
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <div align="left" class="agree_Bottom">
                                        <a href="javascript:void(0);" onclick="move('preview')">��һ��</a></div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
