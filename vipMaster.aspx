<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="vipMaster.aspx.cs" Inherits="vipMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="cssFiles/vipMasterPage.css" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            var currentPosition = 0;
            var slideWidth = 800;
            var slides = $('.slide');
            var numberOfSlides = slides.length;
            $("#ctl00_cph_up1").css('position', 'static');
            // �Ƴ�������
            $('#slidesContainer').css('overflow', 'hidden');
            // ʹ��IDΪslideInner��DIV��������slides
            slides.wrapAll('<div id="slideInner"></div>').css({ 'float': 'left', 'width': slideWidth });
            // ����#slideInner�ܿ��
            $('#slideInner').css('width', slideWidth * numberOfSlides);
            // ��DOMԪ���м����ƶ��ؼ�
            $('#slideshow')
            .prepend('<span class="control" id="leftControl" style="display:none"></span>')
            .append('<span class="control" id="rightControl" style="display:none"></span>');
            // �״μ���ʱ����[��һ��]�ؼ�
            //manageControls(currentPosition);
            // ��������.control��ʽ�ؼ���click�¼�
            // Determine new position ������Ƶ�
            $('.control').bind('click', function () {
                currentPosition = ($(this).attr('id') == 'rightControl') ? currentPosition + 1 : currentPosition - 1;
                $('#slideInner').animate({ 'marginLeft': slideWidth * (-currentPosition) });
            });

            $(".nyroModal").nyroModal();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <asp:ScriptManager runat="server" ID="scriptManager1" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table width="100%" border="1px" style="border-collapse: collapse; border-color: threeddarkshadow"
        rules="rows">
        <tr>
            <td align="left">
                VIP�ۺϹ���
            </td>
        </tr>
        <tr>
            <td align="left">
                <div id="divScreen" runat="server" style="position: relative; width: 100%; top: 0px;
                    left: 0px;">
                    <%--Ϊ���ܹ��ڷ����Ҳ�ܽ��з���--%>
                    <div id="pageContainer">
                        <!-- Slideshow HTML -->
                        <div id="slideshow">
                            <div id="slidesContainer">
                                <div class="slide">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="3" class="v14-header-3">
                                                VIP�ۺϲ�ѯ,����·�ѡ���ѯ������ѯ����
                                            </td>
                                        </tr>
                                        <tr id="tr1">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled1" Width="149px" onchange="start(1,this)">
                                                    <asp:ListItem Value="��">--��--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
                                                <asp:DropDownList runat="server" ID="ddlCondition1" Width="97px">
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="���ڻ����">���ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="С�ڻ����">С�ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="С��">С��</asp:ListItem>
                                                    <asp:ListItem>����</asp:ListItem>
                                                    <asp:ListItem>������</asp:ListItem>
                                                    <asp:ListItem>��ʼ�ַ�</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <span runat="server" id="sp_text_1">
                                                    <asp:TextBox runat="server" ID="txtText1"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_1" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex1" ID="rb1_1" Text="��" />
                                                    <asp:RadioButton runat="server" GroupName="sex1" ID="rb1_2" Text="Ů" />
                                                </span><span runat="server" id="sp_date_1" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_1_y" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_1_m" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_1_d" class="dateText"></asp:TextBox>�� </span>
                                            </td>
                                        </tr>
                                        <tr id="tr2">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled2" Width="149px" onchange="start(2,this)">
                                                    <asp:ListItem Value="��">--��--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
                                                <asp:DropDownList runat="server" ID="ddlCondition2" Width="97px">
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="���ڻ����">���ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="С�ڻ����">С�ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="С��">С��</asp:ListItem>
                                                    <asp:ListItem>����</asp:ListItem>
                                                    <asp:ListItem>������</asp:ListItem>
                                                    <asp:ListItem>��ʼ�ַ�</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <span runat="server" id="sp_text_2">
                                                    <asp:TextBox runat="server" ID="txtText2"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_2" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex2" ID="rb2_1" Text="��" />
                                                    <asp:RadioButton runat="server" GroupName="sex2" ID="rb2_2" Text="Ů" />
                                                </span><span runat="server" id="sp_date_2" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_2_y" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_2_m" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_2_d" class="dateText"></asp:TextBox>�� </span>
                                            </td>
                                        </tr>
                                        <tr id="tr3">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled3" Width="149px" onchange="start(3,this)">
                                                    <asp:ListItem Value="��">--��--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
                                                <asp:DropDownList runat="server" ID="ddlCondition3" Width="97px">
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="���ڻ����">���ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="С�ڻ����">С�ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="С��">С��</asp:ListItem>
                                                    <asp:ListItem>����</asp:ListItem>
                                                    <asp:ListItem>������</asp:ListItem>
                                                    <asp:ListItem>��ʼ�ַ�</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <span runat="server" id="sp_text_3">
                                                    <asp:TextBox runat="server" ID="txtText3"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_3" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex3" ID="rb3_1" Text="��" />
                                                    <asp:RadioButton runat="server" GroupName="sex3" ID="rb3_2" Text="Ů" />
                                                </span><span runat="server" id="sp_date_3" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_3_y" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_3_m" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_3_d" class="dateText"></asp:TextBox>�� </span>
                                            </td>
                                        </tr>
                                        <tr id="tr4">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled4" Width="149px" onchange="start(4,this)">
                                                    <asp:ListItem Value="��">--��--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
                                                <asp:DropDownList runat="server" ID="ddlCondition4" Width="97px">
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="���ڻ����">���ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="С�ڻ����">С�ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="С��">С��</asp:ListItem>
                                                    <asp:ListItem>����</asp:ListItem>
                                                    <asp:ListItem>������</asp:ListItem>
                                                    <asp:ListItem>��ʼ�ַ�</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <span runat="server" id="sp_text_4">
                                                    <asp:TextBox runat="server" ID="txtText4"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_4" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex4" ID="rb4_1" Text="��" />
                                                    <asp:RadioButton runat="server" GroupName="sex4" ID="rb4_2" Text="Ů" />
                                                </span><span runat="server" id="sp_date_4" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_4_y" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_4_m" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_4_d" class="dateText"></asp:TextBox>�� </span>
                                            </td>
                                        </tr>
                                        <tr id="tr5">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled5" Width="149px" onchange="start(5,this)">
                                                    <asp:ListItem Value="��">--��--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
                                                <asp:DropDownList runat="server" ID="ddlCondition5" Width="97px">
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="���ڻ����">���ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="����">����</asp:ListItem>
                                                    <asp:ListItem Value="С�ڻ����">С�ڻ����</asp:ListItem>
                                                    <asp:ListItem Value="С��">С��</asp:ListItem>
                                                    <asp:ListItem>����</asp:ListItem>
                                                    <asp:ListItem>������</asp:ListItem>
                                                    <asp:ListItem>��ʼ�ַ�</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="left">
                                                <span runat="server" id="sp_text_5">
                                                    <asp:TextBox runat="server" ID="txtText5"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_5" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex5" ID="rb5_1" Text="��" />
                                                    <asp:RadioButton runat="server" GroupName="sex5" ID="rb5_2" Text="Ů" />
                                                </span><span runat="server" id="sp_date_5" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_5_y" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_5_m" class="dateText"></asp:TextBox>��
                                                    <asp:TextBox runat="server" ID="date_5_d" class="dateText"></asp:TextBox>�� </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                �������ϵ:<asp:TextBox runat="server" ID="txtCds" Width="160px"></asp:TextBox><br />
                                                ����ʹ�� OR,AND��Ӣ�����Ž�������������߼���ϵ,����Ĭ��Ϊ���й�ϵ<br />
                                                ���磺(1 AND 2) OR 3 ��ʾ��һ�͵ڶ����������ϵ���ٺ͵���������������
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <input type="checkbox" id="cbInvs" onclick="showDivInvs()" /><label for="cbInvs">�˴β�ѯ�Ƿ���������ʾ�</label>
                                                <span runat="server" id="spInvsTips" style="display: none"><span style="width: 50px;
                                                    margin: 0px auto; margin-bottom: 20px; border: 1px solid #007130; height: 18px;
                                                    position: relative; background-color: #22ac38; color: White; font-size: 18">�ѹ���</span></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left" class="bluebullet">
                                                <div align="center" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="startQuery('default');">ִ�в�ѯ</a></div>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField runat="server" ID="hid_ddl1_index" Value="0" />
                                    <asp:HiddenField runat="server" ID="hid_ddl2_index" Value="1" />
                                    <asp:HiddenField runat="server" ID="hid_ddl3_index" Value="2" />
                                    <asp:HiddenField runat="server" ID="hid_ddl4_index" Value="3" />
                                    <asp:HiddenField runat="server" ID="hid_ddl5_index" Value="4" />
                                </div>
                                <div class="slide">
                                    <table border="0" cellpadding="0" cellspacing="0" class="data-table-3">
                                        <tr>
                                            <td class="v14-header-3">
                                                VIP���ϲ�ѯ���
                                            </td>
                                            <td class="v14-header-3" align="right">
                                                ÿҳ��ʾ
                                                <select id="vipSize" onchange="pageChanged('first')">
                                                    <option value="100" selected="selected">100��</option>
                                                    <option value="300">300��</option>
                                                    <option value="500">500��</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <table id="tbGV" width="100%">
                                                    <tr>
                                                        <td>
                                                            <div style="overflow: scroll; height: 260px; width: 760px; word-break: keep-all;
                                                                word-wrap: normal">
                                                                <span id="spTip" style="color: Red; display: none">���ڲ�ѯ��,���Ժ�..</span>
                                                                <asp:UpdatePanel runat="server" ID="UP1" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:GridView CssClass="gridview" runat="server" ID="GVvip" FooterStyle-Wrap="False"
                                                                            HeaderStyle-Wrap="False" PagerStyle-Wrap="False" RowStyle-Wrap="False" Style="word-break: keep-all;
                                                                            word-wrap: normal; text-align: left" OnRowCreated="GVvip_RowCreated">
                                                                            <Columns>
                                                                                <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-Font-Size="Small" ItemStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderTemplate>
                                                                                        <input id="CheckAll" type="checkbox" onclick="selectAll(this);" checked="checked" />��ҳȫѡ
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="X-Small" Wrap="false" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:LinkButton runat="server" ID="btnHideVipBind" OnClick="btnHideVipBind_Click"
                                                                            Text="click me" Style="display: none" />
                                                                        <!--�޸����ϲ���-->
                                                                        <div id="divAlter" style="display: none;">
                                                                            <table cellpadding="0" cellspacing="0" border="1px" rules="none" style="border-color: Blue">
                                                                                <tr style="background-color:Gray;color:White;">
                                                                                    <td align="center" colspan="2" >
                                                                                        �޸Ļ�Ա��ϸ��Ϣ
                                                                                    </td>
                                                                                    <%-- <td align="right">
                                                                                        <a href="#divAlter" class="nyroModalClose">��&nbsp;��</a>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <hr style="width: 100%" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        ���޸ĵ��ֶ�:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList runat="server" ID="ddlAlterFiled" Width="150px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        ���ֶ��޸�Ϊ:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="txtAlter" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr_alterTips" style="display: none">
                                                                                    <td colspan="2" align="center">
                                                                                        �����޸���,���Ժ򡭡�
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td colspan="2">
                                                                                        <table align="center" width="250px">
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="alterDetails('0')" id="a_alter">�޸�ѡ������</a>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="alterDetails('all')" id="a_alter_all">�޸�ȫ������</a>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <!--�޸Ļ���-->
                                                                        <div id="divPoints" style="display: none;">
                                                                            <table cellpadding="0" cellspacing="0" border="1px" rules="none" style="border-color: Blue">
                                                                                <tr style="background-color:Gray;color:White;">
                                                                                    <td align="center" colspan="2">
                                                                                        �޸Ļ�Ա����
                                                                                    </td>
                                                                                    <%--  <td align="right">
                                                                                        <a href="#divAlter" class="nyroModalClose">��&nbsp;��</a>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <hr style="width: 100%" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        ���ӻ���:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="txtAddPoints" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        ���ٻ���:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="txtDescPoints" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr6" style="display: none">
                                                                                    <td colspan="2" align="center">
                                                                                        �����޸���,���Ժ򡭡�
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td colspan="2">
                                                                                        <table align="center" width="250px">
                                                                                            <tr>
                                                                                                <td align="center" colspan="2">
                                                                                                    <a href="javascript:void(0);" onclick="alterPoints('0')" id="a3">�޸�ѡ������</a>
                                                                                                </td>
                                                                                                <%-- <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="alterPoints('all')" id="a4">�޸�ȫ������</a>
                                                                                                </td>--%>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <!--�Ż�ȯ���Ͳ��� -->
                                                                        <div id="divCoupon" style="display: none;">
                                                                            <table border="0" cellpadding="0" cellspacing="0" class="data-table-3" align="center">
                                                                                <tr>
                                                                                    <td colspan="2" style="background-color:Gray;color:White;" align="center" valign="middle">
                                                                                        ���뼰�����Ż�ȯ
                                                                                        <%-- <table>
                                                                                            <tr>
                                                                                                <td class="v14-header-3">
                                                                                                    ���뼰�����Ż�ȯ
                                                                                                </td>
                                                                                                <td class="v14-header-3" align="right">
                                                                                                    <a href="#divCoupon" class="nyroModalClose">�ر�</a>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>--%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="90px" align="right">
                                                                                        �Ż�ȯ������:
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList runat="server" ID="ddlCouponType" Width="150px" onchange="couponTypeChanged()"
                                                                                            AutoPostBack="false">
                                                                                            <asp:ListItem Text="--��ѡ��������--" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="����" Value="LOC"></asp:ListItem>
                                                                                            <asp:ListItem Text="ʵ��" Value="GOODS"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="90px" align="right">
                                                                                        �Ż�ȯ��ϸ����:
                                                                                    </td>
                                                                                    <td nowrap="nowrap" align="center">
                                                                                        <asp:UpdatePanel runat="server" ID="UP3" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList runat="server" ID="ddlCTD" Width="150px">
                                                                                                    <asp:ListItem Text="--��ѡ��������--" Value="0"></asp:ListItem>
                                                                                                </asp:DropDownList>
                                                                                                <img id="stips" style="display: none" src="images/loading.gif" alt="" />
                                                                                            </ContentTemplate>
                                                                                            <Triggers>
                                                                                                <asp:AsyncPostBackTrigger ControlID="btnHideTypeChanged" EventName="Click" />
                                                                                            </Triggers>
                                                                                        </asp:UpdatePanel>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="90px" align="right">
                                                                                        ��Ч��ֹ����:
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:TextBox runat="server" ID="txtEndDate" Width="150px" onclick="new Calendar('2010', '2100', 0,'yyyy-MM-dd').show(this);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trCouponTips" style="display: none">
                                                                                    <td colspan="2" align="center">
                                                                                        �˲���������Ҫ�ϳ�ʱ��,�����ĵȴ�����
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" width="360px">
                                                                                        <table align="center" width="360px">
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="couponSend('0')" id="a1">��ѡ���û�����</a>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="couponSend('all')" id="a2">��ȫ���û�����</a>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="btnHideVipBind" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="tdVipTotal" style="display: none">
                                                            ��ǰ��ʾ��ѯ����ĵ�<span id="spVipNow" style="color: Red"></span>ҳ, ���ι���ѯ����<span id="spVipTotal"
                                                                style="color: Red"></span>ҳ.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <a href="javascript:void(0);" id="pcFirst" onclick="pageChanged('first')">��ҳ</a>&nbsp;&nbsp;
                                                            <a href="javascript:void(0);" id="pcPreview" onclick="pageChanged('preview')">��һҳ</a>&nbsp;&nbsp;
                                                            <a href="javascript:void(0);" id="pcNext" onclick="pageChanged('next')">��һҳ</a>&nbsp;&nbsp;
                                                            <a href="javascript:void(0);" id="pcLast" onclick="pageChanged('last')">βҳ</a>&nbsp;&nbsp;
                                                            ��ת��
                                                            <input type="text" id="txtGoto" style="width: 40px" />&nbsp;&nbsp;
                                                            <input type="button" value="��ת" onclick="pageChanged('goto')" />
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
                                                                <a href="javascript:void(0);" onclick="move('preview')">��һ��</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <!--<a href="javascript:void(0);" onclick="tipsWindown('�޸Ĳ�ѯ���','id:divAlter','300','200','true','','true','')">
                                                        �޸Ĳ�ѯ���</a></div>-->
                                                                <a href="#divAlter" class="nyroModal">�޸Ĳ�ѯ���</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <a href="#divPoints" class="nyroModal">�޸Ļ���</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <a href="#divCoupon" class="nyroModal">�����Ż�ȯ</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <a href="javascript:void(0);" onclick="move('next');">���Ͷ��ż��ʼ�</a>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="slide">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:UpdatePanel runat="server" ID="up2" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="data-table-3"
                                                            align="center">
                                                            <tr>
                                                                <td class="v14-header-3" colspan="2">
                                                                    ���ŷ���
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    *�ѱ������:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList runat="server" ID="ddlSMSlist" Width="150px" onchange="smsModelRead()">
                                                                        <asp:ListItem Selected="True" Text="--��--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <input type="button" value="ɾ��" id="btnDelSMS" onclick="smsModelDelete()" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    *��������ֶ�:
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlProperty" runat="server" Width="150px">
                                                                    </asp:DropDownList>
                                                                    <a href="javascript:void(0);" class="bluebullet" onclick="insertAtCursor()">����</a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    *�������ݼ��:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtSMSTitle" Width="150px"></asp:TextBox>
                                                                    <span id="titleErr" style="visibility: hidden; color: Red">*���ݼ�鲻��Ϊ��</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    *��������:
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox runat="server" ID="txtSMSText" TextMode="MultiLine" Width="300px" Rows="5"></asp:TextBox>
                                                                    <span id="contentErr" style="visibility: hidden; color: Red">*�������ݲ���Ϊ��</span>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    *����ͨ��:
                                                                </td>
                                                                <td>
                                                                    <input type="radio" name="channel" id="channel1" checked="checked" /><label for="channel1">һ��ͨ��</label>
                                                                    (���:<asp:Label runat="server" ID="lblSmsBabalce1"></asp:Label>) &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <input type="radio" name="channel" id="channel2" /><label for="channel2">���ͨ��</label>
                                                                    (���:<asp:Label runat="server" ID="lblSmsBabalce2"></asp:Label>)
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">
                                                                    *��ʾ:���д��ۡ������ȹ���������ѡ����ͨ����<label style="color: red">������ܵ��¶��ŷ���ʧ�ܲ��۷Ѽ���ͨͨ��������</label>��
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left">
                                                                    <div id="smsSendTips" style="color: Black; background-color: Lime; width: 100%; display: none">
                                                                        ���Ŵ�����,���Ժ�...</div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="2">
                                                                    <table align="left" width="70%">
                                                                        <tr>
                                                                            <td>
                                                                                <div align="center" class="agree_Bottom">
                                                                                    <a href="javascript:void(0);" onclick="smsModelSave()">�����������</a></div>
                                                                            </td>
                                                                            <td>
                                                                                <div align="center" class="agree_Bottom">
                                                                                    <a href="javascript:void(0);" onclick="smsMain('send','false')">����ѡ�û����Ͷ���</a></div>
                                                                            </td>
                                                                            <td>
                                                                                <div align="center" class="agree_Bottom">
                                                                                    <a href="javascript:void(0);" onclick="javascript:if(confirm('��ȷ����ȷ���������û����Ͷ�����')){smsMain('send','all');}">
                                                                                        �������û����Ͷ���</a></div>
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
                                                            �ʼ�����
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            *��ȡģ��:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        html��<asp:FileUpload runat="server" ID="FileUpload1" />
                                                                        ͼƬ��<asp:FileUpload runat="server" ID="FileUpload2" />
                                                                    </td>
                                                                    <td>
                                                                        <img src="images/loading.gif" id="imgModel" alt="������" style="display: none" />
                                                                    </td>
                                                                    <td>
                                                                        <a href="javascript:void(0);" id="a_addModel" class="bluebullet" onclick="addMailModel()">
                                                                            ����ģ���ļ�</a>
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
                                                                            <a href="javascript:void(0);" id="sendingMail" onclick="mailSend()">�����ʼ�</a>
                                                                        </div>
                                                                    </td>
                                                                    <td>
                                                                        <div align="center" class="agree_Bottom">
                                                                            <a href="javascript:void(0);" onclick="move('preview')">��һ��</a></div>
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
                            </div>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <div style="position: absolute; width: 500px; height: 300px; top: 300px; left: 330px;
        margin: 0px auto; margin-bottom: 20px; border: 1px solid #96C2F1; background-color: #EFF7FF;
        display: none" id="divInvsMain">
        <table width="100%">
            <tr>
                <td style="margin: 1px; background-color: #B2D3F5; font-weight: bold;">
                    ���β�ѯ��Ҫ�����ĵ����ʾ�
                </td>
            </tr>
            <tr>
                <td>
                    ��Ҫ�������ʾ�����:
                    <asp:DropDownList runat="server" ID="ddlInvs" onchange="invsInit(this)" Width="200px">
                        <asp:ListItem Selected="True" Text="--��--" Value="NONE"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width: 100%; height: 220px; overflow: hidden">
                        <asp:UpdatePanel runat="server" ID="upInvs" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:PlaceHolder runat="server" ID="phInvs"></asp:PlaceHolder>
                                <asp:Button runat="server" ID="btnHideInitInvs" OnClick="btnHideInitInvs_Click" Style="display: none" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button runat="server" ID="btnSendInvs" Text="ѡ�����" OnClick="btnSendInvs_Click"
                        OnClientClick="showDivInvs(1)" />
                    <input type="button" value="�ݲ�����" onclick="showDivInvs(1)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Button runat="server" ID="btnHideTypeChanged" OnClick="btnHideTypeChanged_Click"
        Style="display: none" />
    <input type="hidden" value="0" id="hidMailState" />
    <script type="text/javascript" src="javascriptFiles/JS_vipMaster.js"></script>
</asp:Content>
