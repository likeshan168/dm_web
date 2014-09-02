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
            // 移除滚动条
            $('#slidesContainer').css('overflow', 'hidden');
            // 使用ID为slideInner的DIV包裹所有slides
            slides.wrapAll('<div id="slideInner"></div>').css({ 'float': 'left', 'width': slideWidth });
            // 设置#slideInner总宽度
            $('#slideInner').css('width', slideWidth * numberOfSlides);
            // 在DOM元素中加入移动控件
            $('#slideshow')
            .prepend('<span class="control" id="leftControl" style="display:none"></span>')
            .append('<span class="control" id="rightControl" style="display:none"></span>');
            // 首次加载时隐藏[上一步]控件
            //manageControls(currentPosition);
            // 创建具有.control样式控件的click事件
            // Determine new position 计算控制点
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
                VIP综合管理
            </td>
        </tr>
        <tr>
            <td align="left">
                <div id="divScreen" runat="server" style="position: relative; width: 100%; top: 0px;
                    left: 0px;">
                    <%--为了能够在服务端也能进行访问--%>
                    <div id="pageContainer">
                        <!-- Slideshow HTML -->
                        <div id="slideshow">
                            <div id="slidesContainer">
                                <div class="slide">
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="3" class="v14-header-3">
                                                VIP综合查询,请从下方选择查询基础查询条件
                                            </td>
                                        </tr>
                                        <tr id="tr1">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled1" Width="149px" onchange="start(1,this)">
                                                    <asp:ListItem Value="无">--无--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
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
                                            <td align="left">
                                                <span runat="server" id="sp_text_1">
                                                    <asp:TextBox runat="server" ID="txtText1"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_1" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex1" ID="rb1_1" Text="男" />
                                                    <asp:RadioButton runat="server" GroupName="sex1" ID="rb1_2" Text="女" />
                                                </span><span runat="server" id="sp_date_1" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_1_y" class="dateText"></asp:TextBox>年
                                                    <asp:TextBox runat="server" ID="date_1_m" class="dateText"></asp:TextBox>月
                                                    <asp:TextBox runat="server" ID="date_1_d" class="dateText"></asp:TextBox>日 </span>
                                            </td>
                                        </tr>
                                        <tr id="tr2">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled2" Width="149px" onchange="start(2,this)">
                                                    <asp:ListItem Value="无">--无--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
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
                                            <td align="left">
                                                <span runat="server" id="sp_text_2">
                                                    <asp:TextBox runat="server" ID="txtText2"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_2" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex2" ID="rb2_1" Text="男" />
                                                    <asp:RadioButton runat="server" GroupName="sex2" ID="rb2_2" Text="女" />
                                                </span><span runat="server" id="sp_date_2" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_2_y" class="dateText"></asp:TextBox>年
                                                    <asp:TextBox runat="server" ID="date_2_m" class="dateText"></asp:TextBox>月
                                                    <asp:TextBox runat="server" ID="date_2_d" class="dateText"></asp:TextBox>日 </span>
                                            </td>
                                        </tr>
                                        <tr id="tr3">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled3" Width="149px" onchange="start(3,this)">
                                                    <asp:ListItem Value="无">--无--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
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
                                            <td align="left">
                                                <span runat="server" id="sp_text_3">
                                                    <asp:TextBox runat="server" ID="txtText3"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_3" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex3" ID="rb3_1" Text="男" />
                                                    <asp:RadioButton runat="server" GroupName="sex3" ID="rb3_2" Text="女" />
                                                </span><span runat="server" id="sp_date_3" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_3_y" class="dateText"></asp:TextBox>年
                                                    <asp:TextBox runat="server" ID="date_3_m" class="dateText"></asp:TextBox>月
                                                    <asp:TextBox runat="server" ID="date_3_d" class="dateText"></asp:TextBox>日 </span>
                                            </td>
                                        </tr>
                                        <tr id="tr4">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled4" Width="149px" onchange="start(4,this)">
                                                    <asp:ListItem Value="无">--无--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
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
                                            <td align="left">
                                                <span runat="server" id="sp_text_4">
                                                    <asp:TextBox runat="server" ID="txtText4"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_4" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex4" ID="rb4_1" Text="男" />
                                                    <asp:RadioButton runat="server" GroupName="sex4" ID="rb4_2" Text="女" />
                                                </span><span runat="server" id="sp_date_4" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_4_y" class="dateText"></asp:TextBox>年
                                                    <asp:TextBox runat="server" ID="date_4_m" class="dateText"></asp:TextBox>月
                                                    <asp:TextBox runat="server" ID="date_4_d" class="dateText"></asp:TextBox>日 </span>
                                            </td>
                                        </tr>
                                        <tr id="tr5">
                                            <td align="center" style="width: 220px; height: 30px">
                                                <asp:DropDownList runat="server" ID="ddlFiled5" Width="149px" onchange="start(5,this)">
                                                    <asp:ListItem Value="无">--无--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="center" style="width: 200px">
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
                                            <td align="left">
                                                <span runat="server" id="sp_text_5">
                                                    <asp:TextBox runat="server" ID="txtText5"></asp:TextBox>
                                                </span><span runat="server" id="sp_rb_5" style="display: none">
                                                    <asp:RadioButton runat="server" GroupName="sex5" ID="rb5_1" Text="男" />
                                                    <asp:RadioButton runat="server" GroupName="sex5" ID="rb5_2" Text="女" />
                                                </span><span runat="server" id="sp_date_5" style="display: none">
                                                    <asp:TextBox runat="server" ID="date_5_y" class="dateText"></asp:TextBox>年
                                                    <asp:TextBox runat="server" ID="date_5_m" class="dateText"></asp:TextBox>月
                                                    <asp:TextBox runat="server" ID="date_5_d" class="dateText"></asp:TextBox>日 </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                条件间关系:<asp:TextBox runat="server" ID="txtCds" Width="160px"></asp:TextBox><br />
                                                可以使用 OR,AND和英文括号将上列条件组成逻辑关系,不填默认为并列关系<br />
                                                例如：(1 AND 2) OR 3 表示第一和第二个条件与关系后，再和第三个条件或运算
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <input type="checkbox" id="cbInvs" onclick="showDivInvs()" /><label for="cbInvs">此次查询是否关联调研问卷</label>
                                                <span runat="server" id="spInvsTips" style="display: none"><span style="width: 50px;
                                                    margin: 0px auto; margin-bottom: 20px; border: 1px solid #007130; height: 18px;
                                                    position: relative; background-color: #22ac38; color: White; font-size: 18">已关联</span></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td align="left" class="bluebullet">
                                                <div align="center" class="agree_Bottom">
                                                    <a href="javascript:void(0);" onclick="startQuery('default');">执行查询</a></div>
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
                                                VIP联合查询结果
                                            </td>
                                            <td class="v14-header-3" align="right">
                                                每页显示
                                                <select id="vipSize" onchange="pageChanged('first')">
                                                    <option value="100" selected="selected">100条</option>
                                                    <option value="300">300条</option>
                                                    <option value="500">500条</option>
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
                                                                <span id="spTip" style="color: Red; display: none">正在查询中,请稍候..</span>
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
                                                                                        <input id="CheckAll" type="checkbox" onclick="selectAll(this);" checked="checked" />本页全选
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="X-Small" Wrap="false" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                        <asp:LinkButton runat="server" ID="btnHideVipBind" OnClick="btnHideVipBind_Click"
                                                                            Text="click me" Style="display: none" />
                                                                        <!--修改资料部分-->
                                                                        <div id="divAlter" style="display: none;">
                                                                            <table cellpadding="0" cellspacing="0" border="1px" rules="none" style="border-color: Blue">
                                                                                <tr style="background-color:Gray;color:White;">
                                                                                    <td align="center" colspan="2" >
                                                                                        修改会员详细信息
                                                                                    </td>
                                                                                    <%-- <td align="right">
                                                                                        <a href="#divAlter" class="nyroModalClose">关&nbsp;闭</a>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <hr style="width: 100%" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        可修改的字段:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:DropDownList runat="server" ID="ddlAlterFiled" Width="150px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        将字段修改为:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="txtAlter" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr_alterTips" style="display: none">
                                                                                    <td colspan="2" align="center">
                                                                                        资料修改中,请稍候……
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td colspan="2">
                                                                                        <table align="center" width="250px">
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="alterDetails('0')" id="a_alter">修改选中数据</a>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="alterDetails('all')" id="a_alter_all">修改全部数据</a>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <!--修改积分-->
                                                                        <div id="divPoints" style="display: none;">
                                                                            <table cellpadding="0" cellspacing="0" border="1px" rules="none" style="border-color: Blue">
                                                                                <tr style="background-color:Gray;color:White;">
                                                                                    <td align="center" colspan="2">
                                                                                        修改会员积分
                                                                                    </td>
                                                                                    <%--  <td align="right">
                                                                                        <a href="#divAlter" class="nyroModalClose">关&nbsp;闭</a>
                                                                                    </td>--%>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2">
                                                                                        <hr style="width: 100%" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        增加积分:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="txtAddPoints" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td width="100px" align="center">
                                                                                        减少积分:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:TextBox runat="server" ID="txtDescPoints" Width="150px"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="tr6" style="display: none">
                                                                                    <td colspan="2" align="center">
                                                                                        资料修改中,请稍候……
                                                                                    </td>
                                                                                </tr>
                                                                                <tr style="height: 50px">
                                                                                    <td colspan="2">
                                                                                        <table align="center" width="250px">
                                                                                            <tr>
                                                                                                <td align="center" colspan="2">
                                                                                                    <a href="javascript:void(0);" onclick="alterPoints('0')" id="a3">修改选中数据</a>
                                                                                                </td>
                                                                                                <%-- <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="alterPoints('all')" id="a4">修改全部数据</a>
                                                                                                </td>--%>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <!--优惠券发送部分 -->
                                                                        <div id="divCoupon" style="display: none;">
                                                                            <table border="0" cellpadding="0" cellspacing="0" class="data-table-3" align="center">
                                                                                <tr>
                                                                                    <td colspan="2" style="background-color:Gray;color:White;" align="center" valign="middle">
                                                                                        申请及发送优惠券
                                                                                        <%-- <table>
                                                                                            <tr>
                                                                                                <td class="v14-header-3">
                                                                                                    申请及发送优惠券
                                                                                                </td>
                                                                                                <td class="v14-header-3" align="right">
                                                                                                    <a href="#divCoupon" class="nyroModalClose">关闭</a>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>--%>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="90px" align="right">
                                                                                        优惠券主类型:
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:DropDownList runat="server" ID="ddlCouponType" Width="150px" onchange="couponTypeChanged()"
                                                                                            AutoPostBack="false">
                                                                                            <asp:ListItem Text="--请选择主类型--" Value="0"></asp:ListItem>
                                                                                            <asp:ListItem Text="代金" Value="LOC"></asp:ListItem>
                                                                                            <asp:ListItem Text="实物" Value="GOODS"></asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="90px" align="right">
                                                                                        优惠券详细分类:
                                                                                    </td>
                                                                                    <td nowrap="nowrap" align="center">
                                                                                        <asp:UpdatePanel runat="server" ID="UP3" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                                                            <ContentTemplate>
                                                                                                <asp:DropDownList runat="server" ID="ddlCTD" Width="150px">
                                                                                                    <asp:ListItem Text="--请选择主类型--" Value="0"></asp:ListItem>
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
                                                                                        有效截止日期:
                                                                                    </td>
                                                                                    <td align="center">
                                                                                        <asp:TextBox runat="server" ID="txtEndDate" Width="150px" onclick="new Calendar('2010', '2100', 0,'yyyy-MM-dd').show(this);"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="trCouponTips" style="display: none">
                                                                                    <td colspan="2" align="center">
                                                                                        此操作可能需要较长时间,请耐心等待……
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="2" width="360px">
                                                                                        <table align="center" width="360px">
                                                                                            <tr>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="couponSend('0')" id="a1">对选中用户发送</a>
                                                                                                </td>
                                                                                                <td align="center">
                                                                                                    <a href="javascript:void(0);" onclick="couponSend('all')" id="a2">对全部用户发送</a>
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
                                                                <a href="javascript:void(0);" onclick="move('preview')">上一步</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <!--<a href="javascript:void(0);" onclick="tipsWindown('修改查询结果','id:divAlter','300','200','true','','true','')">
                                                        修改查询结果</a></div>-->
                                                                <a href="#divAlter" class="nyroModal">修改查询结果</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <a href="#divPoints" class="nyroModal">修改积分</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <a href="#divCoupon" class="nyroModal">发送优惠券</a></div>
                                                        </td>
                                                        <td>
                                                            <div align="center" class="agree_Bottom">
                                                                <a href="javascript:void(0);" onclick="move('next');">发送短信及邮件</a>
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
                                                                        html：<asp:FileUpload runat="server" ID="FileUpload1" />
                                                                        图片：<asp:FileUpload runat="server" ID="FileUpload2" />
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
                                                                    <td>
                                                                        <div align="center" class="agree_Bottom">
                                                                            <a href="javascript:void(0);" onclick="move('preview')">上一步</a></div>
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
                    本次查询需要关联的调研问卷
                </td>
            </tr>
            <tr>
                <td>
                    需要关联的问卷描述:
                    <asp:DropDownList runat="server" ID="ddlInvs" onchange="invsInit(this)" Width="200px">
                        <asp:ListItem Selected="True" Text="--无--" Value="NONE"></asp:ListItem>
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
                    <asp:Button runat="server" ID="btnSendInvs" Text="选择完成" OnClick="btnSendInvs_Click"
                        OnClientClick="showDivInvs(1)" />
                    <input type="button" value="暂不关联" onclick="showDivInvs(1)" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Button runat="server" ID="btnHideTypeChanged" OnClick="btnHideTypeChanged_Click"
        Style="display: none" />
    <input type="hidden" value="0" id="hidMailState" />
    <script type="text/javascript" src="javascriptFiles/JS_vipMaster.js"></script>
</asp:Content>
