<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="DataDownload.aspx.cs" Inherits="DataDownload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="javascriptFiles/JS_DataDownload.js"></script>
    <link type="text/css" href="cssFiles/css_DataDownload.css" rel="Stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" runat="Server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr valign="top">
                <td width="10" rowspan="4">
                    <img alt="" src="images/c.gif" width="10" height="1" />
                </td>
                <td width="100%">
                    <!-- Begin body -->
                    <!-- TABS_BEGIN -->
                    <!-- End the center cell of the content section -->
                </td>
                <tr valign="top">
                    <td>
                        <table width="188" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td width="9" class="v14-graphic-tab-selected">
                                        <img class="display-img" alt="" src="images/c.gif" width="9" height="19" />
                                    </td>
                                    <td width="198" class="v14-graphic-tab-selected">
                                        <a class="v14-tab-link-selected" href="javascript:void(0);">VIP/营业员 数据资料下载</a>
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
                        本功能会将VIP数据资料下载到本地数据仓库并更新本地数据仓库.根据您的网络情况,可能需要几分钟的时间.<br />
                        <br />
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td class="v14-header-3">
                                        下载数据内容
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="3">
                                            <tbody>
                                                <tr>
                                                    <td width="560" valign="top">
                                                        <label for="lotus">
                                                            <img src="images/c.gif" alt="Select to search the knowledge base" width="1" height="1" /></label>
                                                        <span class="v14-ttd">资料内容：</span>
                                                        <input type="checkbox" id="cbVIP" checked="checked" /><label for="cbVIP">VIP资料</label>
                                                        <input type="checkbox" id="cbSaler" checked="checked" /><label for="cbSaler">营业员资料</label>
                                                        <input type="checkbox" id="cbShop" checked="checked" /><label for="cbShop">门店资料</label>
                                                        <input type="checkbox" id="cbVipCard" checked="checked" /><label for="cbVipCard">VIP制卡</label>
                                                        <input type="checkbox" id="cbVipKdt" checked="checked" /><label for="cbVipKdt">VIP口袋通</label>
                                                        <input type="checkbox" id="cbVipKdtSale" checked="checked" /><label for="cbVipKdtSale">VIP口袋通销售</label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="top">
                                                        <label for="qq">
                                                            <img src="images/c.gif" alt="Search for:" width="1" height="1" /></label>
                                                        <span class="bluebullet">执行数据下载和更新</span>
                                                        <img alt="进行资料同步" src="images/go.gif" id="imagebutton1" onclick="startDownLoad()"
                                                            style="cursor: pointer" />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <!-- Spacer -->
                        <br />
                    </td>
                    <tr valign="top">
                        <td>
                            <table width="188" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="9" class="v14-graphic-tab-selected">
                                        <img class="display-img" alt="" src="images/c.gif" width="9" height="19" />
                                    </td>
                                    <td width="198" class="v14-graphic-tab-selected">
                                        <a class="v14-tab-link-selected" href="javascript:void(0);">从EXCEL文件导入VIP资料 </a>
                                    </td>
                                </tr>
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
                                <!-- TABS_END -->
                                <!-- Spacer -->
                                本功能会将EXCEL文件中的VIP资料按对应格式导入到相应VIP数据库.</p>
                            <p>
                                <strong>进行此操作前请先确定EXCEL文件的数据结构与VIP数据库的数据结构相同,并且没有卡号重复</strong></p>
                            <p>
                                <span class="bluebullet">查看VIP数据库的数据结构</span>
                                <img alt="" src="images/go.gif" width="21" border="0" height="21" onclick="javascript:document.getElementById('ctl00_cph_divColumn').style.display=''" /></p>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="v14-header-3">
                                        导入EXCEL文件
                                    </td>
                                </tr>
                            </table>
                            <!-- Spacer -->
                        </td>
                    </tr>
                </tr>
            </tr>
        </tbody>
    </table>
    <div id="divBG" style="position: absolute; background-image: url('images/divBG.png');
        width: 99%; height: 316px; top: 96px; left: 165px; z-index: 3; display: none">
        <div style="position: absolute; z-index: 4; top: 110px; left: 245px;">
            <table rules="none" border="1px">
                <tr>
                    <td align="center" style="font-size: larger; font-weight: bold;" colspan="2">
                        正在进行资料同步
                    </td>
                </tr>
                <tr>
                    <td id="td1" style="color: Green">
                        <img alt="" src="images/div_go.gif" /><span id="spanVip">已完成会员资料同步</span>
                    </td>
                    <td>
                        <div id='out_vip'>
                            <div id="in_0_vip" style="z-index: 4">
                                已完成0%</div>
                            <div id="in_vip" style="z-index: 4">
                                <div id="in_1_vip">
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id="td2" style="color: Red">
                        <img alt="" src="images/div_go.gif" /><span id="spanSaler">等待进行营业员资料同步</span>
                    </td>
                    <td>
                        <div id='out_saler'>
                            <div id="in_0_saler" style="z-index: 4">
                                已完成0%</div>
                            <div id="in_saler" style="z-index: 4">
                                <div id="in_1_saler">
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td id="td3" style="color: Gray">
                        <img alt="" src="images/div_go.gif" /><span id="spanShop">等待进行门店资料同步</span>
                    </td>
                    <td>
                        <div id='out_shop'>
                            <div id="in_0_shop" style="z-index: 4">
                                已完成0%</div>
                            <div id="in_shop" style="z-index: 4">
                                <div id="in_1_shop">
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>

                <!--新增的是用来进行vip卡号下载的-->
                <tr>
                    <td id="td4" style="color: Gray">
                        <img alt="" src="images/div_go.gif" /><span id="spanVipCard">等待进行VIP制卡资料同步</span>
                    </td>
                    <td>
                        <div id='out_vipCard'>
                            <div id="in_0_vipCard" style="z-index: 4">
                                已完成0%</div>
                            <div id="in_vipCard" style="z-index: 4">
                               <%-- <div id="in_1_vipCard">
                                </div>--%>
                            </div>
                        </div>
                    </td>
                </tr>

                <!--新增的是用来进行vip口袋通信息下载的-->
                <tr>
                    <td id="td5" style="color: Gray">
                        <img alt="" src="images/div_go.gif" /><span id="spanVipKdt">等待进行VIP口袋通同步</span>
                    </td>
                    <td>
                        <div id='out_vipKdt'>
                            <div id="in_0_vipKdt" style="z-index: 4">
                                已完成0%</div>
                            <div id="in_vipKdt" style="z-index: 4">
                               <%-- <div id="in_1_vipCard">
                                </div>--%>
                            </div>
                        </div>
                    </td>
                </tr>
                <!--新增的是用来进行vip口袋通销售信息下载的-->
                <tr>
                    <td id="td6" style="color: Gray">
                        <img alt="" src="images/div_go.gif" /><span id="spanVipKdtSale">等待进行口袋通VIP销售数据同步</span>
                    </td>
                    <td>
                        <div id='out_vipKdtSale'>
                            <div id="in_0_vipKdtSale" style="z-index: 4">
                                已完成0%</div>
                            <div id="in_vipKdtSale" style="z-index: 4">
                               <%-- <div id="in_1_vipCard">
                                </div>--%>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
