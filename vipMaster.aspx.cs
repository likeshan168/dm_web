using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using AjaxPro;
using System.IO;
public partial class vipMaster : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();
    smsOperate sms = new smsOperate();
    LogModule lm = new LogModule();
    static char[] splitChar1 = { (char)21 };
    static char[] splitChar2 = { (char)22 };

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            initVipPage();//初始化dropdownlist
            initSMSlist();//初始化短信模版
            initInvsDropDownList();//问卷调查的相关问题
            initSmsBalance();//初始化短信账户余额
            // AjaxPro.Utility.RegisterTypeForAjax(typeof(vipMaster));

        }

        //VIP主查询异步调用↓↓↓
        if (Request.QueryString["type"] == "query")//点击执行查询按钮
        {
            mainQuery();
        }
        //修改VIP资料异步调用↓↓↓
        if (Request.QueryString["type"] == "alter")
        {
            if (Request.QueryString["state"] == "all")
                alterDetails("all");
            else
                alterDetails("0");
        }
        if (Request.QueryString["type"] == "alterPoints")
        {
            if (Request.QueryString["state"] == "all")
            {
                alterPoints("all");
            }
            else
            {
                alterPoints("0");
            }
        }
        //优惠券申请及发送
        if (Request.QueryString["type"] == "coupon")
        {
            if (Request.QueryString["state"] == "all")
                couponApplication("all");
            else
                couponApplication("0");
        }

        //邮件群发异步调用↓↓↓邮件模版的加载
        if (Request.QueryString["type"] == "mailModel")
        {
            mailModelUpload();
        }
        //邮件的发送
        if (Request.QueryString["type"] == "mailSend")
        {
            mailSend();
        }


        //  ↓↓↓短信功能模块异步数据处理 ↓↓↓
        #region 短信模块异步数据处理
        if (Request.QueryString["details"] != null)
        {
            try
            {
                if (Request.QueryString["details"] == "read")
                {
                    #region
                    //if (ddlSMSlist.SelectedValue != "0")
                    //{
                    //    DataTable rdDT = sms.readSMS(ddlSMSlist.SelectedValue.ToString());
                    //    if (rdDT.Rows.Count > 0)
                    //    {
                    //        txtSMSTitle.Text = rdDT.Rows[0][0].ToString();
                    //        txtSMSText.Text = rdDT.Rows[0][1].ToString();
                    //    }
                    //}
                    #endregion
                }
                else if (Request.QueryString["details"] == "del")
                {
                    #region
                    string id = ddlSMSlist.SelectedValue;
                    int index = ddlSMSlist.SelectedIndex;
                    if (sms.deleteSMS(id))
                        ddlSMSlist.Items.RemoveAt(index);
                    #endregion
                }
                else if (Request.QueryString["details"] == "save")
                {
                    #region
                    //sms.saveSMS(txtSMSTitle.Text.Trim(), txtSMSText.Text);
                    //initSMSlist();
                    #endregion
                }
                else if (Request.QueryString["details"] == "send")//短信的发送
                {
                    if (Request.QueryString["mode"] == "all")//对所有用户发送
                        sendSMS(true);
                    else
                        sendSMS(false);//对所选用户进行发送(发送直复短信)
                }
                Response.Write("success");
            }
            catch (Exception ex)
            { Response.Write("Error_" + ex.Message.Replace("\r\n", "").Replace("'", "")); }
            finally { Response.End(); }
        }
        #endregion
        //初始化调研问卷选择后的问题
        if (Session["invsDetailsTable"] != null)
        {
            addControlToInvsPanel();
        }


    }
    /// <summary>
    /// 实现局部刷新的效果
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnHideInitInvs_Click(object sender, EventArgs e)
    {
        Page_Load(null, null);
    }

    /// <summary>
    /// 根据选择不同的问卷调查获取问卷调查的具体内容
    /// </summary>
    private void addControlToInvsPanel()
    {
        int index = 1;
        string controlString = "";
        DataTable innerTable = (DataTable)Session["invsDetailsTable"];
        phInvs.Controls.Clear();
        foreach (DataRow mydr in innerTable.Rows)
        {
            switch (mydr[3].ToString())
            {
                case "短文本":
                    controlString += index.ToString() + ". " + mydr[2].ToString() + ":  <asp:TextBox runat='server' ID='"
                        + mydr[0].ToString() + "'></asp:TextBox><br/>";

                    break;
                case "长文本":
                    controlString += index.ToString() + ". " + mydr[2].ToString() + ":  <asp:TextBox runat='server' ID='"
                        + mydr[0].ToString() + "' TextMode='MultiLine'></asp:TextBox><br/>";
                    break;
                case "单选按钮组":
                    controlString += index.ToString() + ". " + mydr[2].ToString() + ":  ";
                    string[] Rlist = mydr[4].ToString().Split(',');
                    int RcbIndex = 1;
                    foreach (string insertString in Rlist)
                    {
                        controlString += "<asp:RadioButton runat=\"server\" GroupName=\"" + mydr[0].ToString() + "\" ID=\"" + mydr[0].ToString() + "_" + RcbIndex.ToString() + "\" Text=\"" + insertString + "\" />&nbsp;&nbsp;";
                        RcbIndex++;
                    }
                    controlString += "<br/>";
                    break;
                case "复选框":
                    controlString += index.ToString() + ". " + mydr[2].ToString() + ":  ";
                    string[] list = mydr[4].ToString().Split(',');
                    int cbIndex = 1;
                    foreach (string insertString in list)
                    {
                        controlString += "<asp:CheckBox runat=\"server\" ID=\"" + mydr[0].ToString() + "_" + cbIndex.ToString() + "\" Text=\"" + insertString + "\" />&nbsp;&nbsp;";
                        cbIndex++;
                    }
                    controlString += "<br/>";
                    break;
            }
            index++;
        }
        Control c = ParseControl(controlString);
        phInvs.Controls.Add(c);
        upInvs.Update();
    }

    protected void btnHideVipBind_Click(object sender, EventArgs e)
    {
        //绑定处理后的VIP数据
        DataTable vipTable = (DataTable)Cache["vipPagedTable"];
        GVvip.DataSource = vipTable;
        GVvip.DataBind();
        //绑定修改字段列表
        DataTable alterFiledTable = (DataTable)Cache["alterFiledTable"];//来自于getVipSelect方法.两个字段:CNname,ENname
        ddlAlterFiled.Items.Clear();
        foreach (DataRow dr in alterFiledTable.Rows)
        {
            //if (dr[1].ToString() == "卡号" || dr[1].ToString() == "积分")//禁止修改卡号与积分字段
            //    break;
            if (dr[1].ToString() == "卡号" || dr[1].ToString() == "积分")//禁止修改卡号与积分字段
                continue;
            ddlAlterFiled.Items.Add(new ListItem(dr[1].ToString(), dr[0].ToString()));
        }
        //绑定短信发送字段列表
        ddlProperty.Items.Clear();
        for (int i = 0; i < vipTable.Columns.Count; i++)
        {
            string temp = vipTable.Columns[i].ColumnName;
            ddlProperty.Items.Add(new ListItem("[%" + temp + "%]", temp));
        }
        //}
        up2.Update();
        //隐藏正在查询提示
        ScriptManager.RegisterClientScriptBlock(UP1, GetType(), "disTip", "$(\"#spTip\").css(\"display\", \"none\")", true);
    }

    protected void btnHideTypeChanged_Click(object sender, EventArgs e)
    {
        DataTable mydt = (DataTable)Cache["couponTypeTable"];
        if (mydt.Rows.Count > 0)
        {
            ddlCTD.Items.Clear();
            ListItem li;
            foreach (DataRow dr in mydt.Rows)
            {
                li = new ListItem(dr[0].ToString(), dr[1].ToString());
                ddlCTD.Items.Add(li);
            }
        }
        UP3.Update();
    }

    protected void initVipPage()
    {
        try
        {
            int x = (int)so.sqlExecuteScalar("select count(name) from sysobjects where name='cardInfo' and type='U'");
            if (x == 0)
                ClientScript.RegisterStartupScript(GetType(), "s", "<script>if(confirm('首次使用,需要下载VIP资料,是否现在下载?'))window.location.href='DataDownload.aspx'</script>");
            else
            {
                #region
                DataTable conTable = so.sqlExcuteQueryTable("select ENname,CNname,[type] from DataInfo where isCondition=1");
                if (conTable == null || conTable.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "s", "<script>if(confirm('首次使用,需要设置显示条件,是否现在设置?'))window.location.href='FilterSet.aspx'</script>");
                }
                else
                {
                    string ctrlName = "";
                    ListItem li;

                    for (int i = 0, c = conTable.Rows.Count; i < c; i++)
                    {
                        for (int j = 1; j <= 5; j++)
                        {
                            ctrlName = "ddlFiled" + j.ToString();
                            DropDownList myddl = (DropDownList)divScreen.FindControl(ctrlName);
                            li = new ListItem(conTable.Rows[i][1].ToString(), conTable.Rows[i][0].ToString() + "-" + conTable.Rows[i][2].ToString());
                            myddl.Items.Add(li);
                        }
                    }
                }
                #endregion
            }
        }
        catch (HttpException)
        {
            ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('登录超时,请重新登录');window.location.href='Login.aspx'</script>");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "error_ex", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true);
        }
    }

    protected void initInvsDropDownList()
    {
        try
        {
            DataTable dt = so.sqlExcuteQueryTable("select invDescribe,TID from Investigate");
            ListItem li;
            foreach (DataRow dr in dt.Rows)
            {
                li = new ListItem(dr[0].ToString(), dr[1].ToString());
                ddlInvs.Items.Add(li);
            }
        }
        catch (Exception ex)
        { ClientScript.RegisterClientScriptBlock(GetType(), "invsError", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true); }
    }

    protected void initSmsBalance()
    {
        try
        {
            smsClient.smsService ss = new smsClient.smsService();
            Serialize ser = new Serialize();
            string id = so.sqlExecuteScalar("select uid from smsSysNum").ToString();
            DataTable mydt = ser.DeserializeDataTable(ss.getClientBalance(id, memberStatic.companyID));//admin
            DataRow[] dr1 = mydt.Select("channel=1");
            DataRow[] dr2 = mydt.Select("channel=2");
            lblSmsBabalce1.Text = dr1[0]["balance"].ToString();//4440
            if (dr2 != null && dr2.Length > 0)
            {
                lblSmsBabalce2.Text = dr2[0]["balance"].ToString();//0
            }
            else
            {
                lblSmsBabalce2.Text = "0";//0
            }
        }
        catch
        {
            lblSmsBabalce1.Text = "无法获取";
            lblSmsBabalce2.Text = "无法获取";
        }
    }

    protected void initSMSlist()
    {
        try
        {
            DataTable listDT = sms.readSMSlist();
            if (listDT != null && listDT.Rows.Count > 0)
            {
                //ddlSMSlist.Items.Clear();
                ListItem li;
                for (int i = 0; i < listDT.Rows.Count; i++)
                {
                    li = new ListItem(listDT.Rows[i]["smsTitle"].ToString(), listDT.Rows[i]["smsTitle"].ToString());
                    ddlSMSlist.Items.Add(li);
                }
            }
        }
        catch (Exception ex)
        { throw new Exception(ex.Message, ex); }
    }
    /// <summary>
    /// 发送直复短信（分对所选用户，还是对所用用户进行发送）
    /// </summary>
    /// <param name="allSend"></param>
    protected void sendSMS(bool allSend)
    {
        try
        {

            #region//对所选用户进行短信的发送
            if (!allSend)
            {
                int count = 0;//记录发送短信个数以记录日志
                ArrayList al = new ArrayList();
                for (int i = 0; i < GVvip.Rows.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)GVvip.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                    {
                        al.Add(GVvip.Rows[i].Cells[1].Text);
                        count++;
                    }
                }
                if (al == null || al.Count == 0)
                {
                    Exception nex = new Exception("没有可发送对象");
                    throw nex;
                }
                //提交短信至数据库
                DateTime newDT = new DateTime(2010, 1, 1);
                TimeSpan sp = DateTime.Now - newDT;
                string li = sp.TotalMilliseconds.ToString();
                if (li.IndexOf(".") > 0)
                    li = li.Substring(0, li.IndexOf("."));
                ArrayList sqlAL = new ArrayList(al.Count + 1);
                sqlAL.Add("insert into smsModel_zf values('" + li + "','" + Request.QueryString["content"] + "'," + Request.QueryString["channel"] + ")");
                for (int i = 0; i < al.Count; i++)
                    sqlAL.Add("insert into smsCard_zf values('" + li + "','" + al[i].ToString() + "')");
                so.sqlExcuteNonQuery(sqlAL, true);
            }
            #endregion
            else//对所有用户进行发送
            {
                ArrayList al = new ArrayList();
                DataTable vipTable = (DataTable)Cache["vipTable"];
                foreach (DataRow dr in vipTable.Rows)
                    al.Add(dr[0].ToString());
                if (al == null || al.Count == 0)
                {
                    Exception nex = new Exception("没有可发送对象");
                    throw nex;
                }
                //提交短信至数据库
                DateTime newDT = new DateTime(2010, 1, 1);
                TimeSpan sp = DateTime.Now - newDT;
                string li = sp.TotalMilliseconds.ToString();
                if (li.IndexOf(".") > 0)
                    li = li.Substring(0, li.IndexOf("."));
                int s = so.sqlExcuteNonQueryInt("insert into smsModel_zf values('" + li + "','" + Request.QueryString["content"] + "'," + Request.QueryString["channel"] + ")");
                if (s == 1)
                {
                    DataTable tempDT = new DataTable();
                    tempDT.Columns.Add("iden", typeof(string));
                    tempDT.Columns.Add("text", typeof(string));
                    for (int i = 0; i < al.Count; i++)
                    {
                        DataRow dr = tempDT.NewRow();
                        dr[0] = li;
                        dr[1] = al[i].ToString();
                        tempDT.Rows.Add(dr);
                    }
                    string tx = so.sqlExcuteBulkCopy("smsCard_zf", tempDT);
                    if (tx != "OK")
                    {
                        so.sqlExcuteNonQuery("delete from smsModel_zf where mid='" + li + "'", false);
                        Exception ne = new Exception(tx);
                        throw ne;
                    }
                }
            }
        }
        catch (Exception ex)
        { throw ex; }
    }

    protected void mailModelUpload()
    {
        try
        {
            if (!System.IO.Directory.Exists("C:/EmpoxWebModel/mailModel/img"))
            {
                System.IO.Directory.CreateDirectory("C:/EmpoxWebModel/mailModel/img");

            }
            FileUpload1.PostedFile.SaveAs("C:/EmpoxWebModel/mailModel/03ffed.tmp");
            HttpPostedFile img = FileUpload2.PostedFile;//图片文件
            if (img != null)
            {
               
                string ext = Path.GetExtension(img.FileName);
                img.SaveAs("C:/EmpoxWebModel/mailModel/img/tmp" + ext);
            }
            Response.Write("success");
        }
        catch (Exception ex)
        { Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }

    protected void mailSend()
    {
        try
        {
            int count = 0;//记录发送邮件个数以记录日志
            ArrayList al = new ArrayList();
            #region
            for (int i = 0; i < GVvip.Rows.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)GVvip.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                {
                    al.Add(GVvip.Rows[i].Cells[1].Text);
                    count++;
                }
            }
            if (al == null || al.Count == 0)
            {
                Exception nex = new Exception("没有可发送对象");
                throw nex;
            }
            #endregion

            mailOperate mo = new mailOperate();
            mo.SendmailOperate("card_id", false, al);
            Response.Write("success");
        }
        catch (Exception ex)
        { Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }

    protected void mainQuery()
    {
        try
        {
            string whereString = getWhereString();//修改全部资料时使用
            Session["whereString"] = whereString;//存储查询的条件
            string mainSql = string.Empty;
            #region//新加的
            if (string.IsNullOrEmpty(whereString))
            {
                mainSql = "select " + getSelectString() + " from vipInfo_view";
            }
            else
            {
                mainSql = "select " + getSelectString() + " from vipInfo_view where " + whereString;
            }
            #endregion

            //string mainSql = "select " + getSelectString() + " from vipInfo_view where " + whereString;
            Session["mainSql"] = mainSql;//card_id as 卡号,spareScore as 可用积分,card_Discount as 折扣,card_Type as 卡类型,points as 积分,pwd as 密码,sendClient as 发卡店铺,sendMan as 发卡人,userMobile as 手机号码,userName as 姓名,userSex as 性别
            DataTable dt = so.sqlExcuteQueryTable(mainSql);

            Cache["vipTable"] = dt;//这样会导致性能下降
            Response.Write("success");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.Replace("'", "").Replace("\r\n", ""));
        }
        finally { Response.End(); }
    }

    private string getWhereString()
    {
        string ws = "";
        ws += getVipWhere();//获取查询条件
        if (ddlInvs.SelectedIndex != 0)//关联的问题
            //ws += getInvWhere();
            ws += ViewState["invsWhere"];
        if (ws != "")
        {
            ws = ws.Substring(0, ws.Length - 4);

        }
        return ws;
    }

    #region//旧版
    //private string getSelectString()
    //{
    //    //可修改的字段,修改资料时调用
    //    DataTable alterFiledTable = new DataTable();
    //    alterFiledTable.Columns.Add("ENname", typeof(string));
    //    alterFiledTable.Columns.Add("CNname", typeof(string));
    //    //alterFiledTable.Columns.Add("type", typeof(string));
    //    string vs = "";
    //    //查询初始字段
    //    //
    //    //TODO:这里有重复操作数据的嫌疑
    //    //
    //    DataTable mydt = so.sqlExcuteQueryTable("select ENname,CNname from DataInfo where Displayd=1");
    //    for (int i = 0; i < mydt.Rows.Count; i++)
    //    {
    //        vs += mydt.Rows[i][0].ToString() + " as " + mydt.Rows[i][1].ToString() + ",";
    //        //绑入可修改字段表
    //        alterFiledTable.Rows.Add(new object[] { mydt.Rows[i][0].ToString(), mydt.Rows[i][1].ToString() });
    //    }
    //    //添加卡号及可用积分vs=card_Discount as 折扣,card_Type as 卡类型,points as 积分,pwd as 密码,sendClient as 发卡店铺,sendMan as 发卡人,userMobile as 手机号码,userName as 姓名,userSex as 性别,
    //    vs.Replace("card_id as 卡号,", "");
    //    vs = "card_id as 卡号,spareScore as 可用积分," + vs;
    //    mydt = new DataTable();
    //    if (vs.Length > 0)
    //        vs = vs.Substring(0, vs.Length - 1);
    //    Session["alterFiledTable"] = alterFiledTable;
    //    return vs;
    //}
    #endregion

    #region//新版
    private string getSelectString()
    {
        //可修改的字段,修改资料时调用
        string vs = "";
        //
        //TODO:这里有重复操作数据的嫌疑
        //
        DataTable mydt = so.sqlExcuteQueryTable("select ENname,CNname from DataInfo where Displayd=1");
        for (int i = 0; i < mydt.Rows.Count; i++)
        {
            vs += mydt.Rows[i][0].ToString() + " as " + mydt.Rows[i][1].ToString() + ",";
        }
        vs = vs.Replace("card_Id as 卡号,", "");
        vs = "card_id as 卡号,spareScore as 可用积分," + vs;
        if (vs.Length > 0)
            vs = vs.Substring(0, vs.Length - 1);
        Cache["alterFiledTable"] = mydt;
        return vs;
    }
    #endregion
    private string getVipWhere()
    {
        #region//旧版本
        //string vw = string.Empty, filedName = string.Empty, conditionName = string.Empty, textName = string.Empty;
        //string cds = txtCds.Text.Replace("1", "#").Replace("2", "$").Replace("3", "%").Replace("4", "^").Replace("5", "&");//条件之间的关系
        //DataTable dt = new DataTable();
        //dt = so.sqlExcuteQueryTable("select ENname,type,Udefined from DataInfo where isCondition=1");
        //for (int i = 1; i <= 5; i++)
        //{
        //    filedName = "ddlFiled" + i.ToString();
        //    conditionName = "ddlCondition" + i.ToString();
        //    textName = "txtText" + i.ToString();
        //    DropDownList myFiled = (DropDownList)divScreen.FindControl(filedName);
        //    DropDownList myCondition = (DropDownList)divScreen.FindControl(conditionName);

        //    if (dt == null || dt.Rows.Count == 0)
        //        return null;
        //    if (myFiled.SelectedIndex > 0)
        //    {
        //        string x = getCondition(myCondition.SelectedValue, i.ToString(), myFiled.SelectedValue, dt);
        //        vw += x;
        //        //按照条件进行替换
        //        if (cds != "")
        //        {
        //            switch (i)
        //            {
        //                case 1:
        //                    cds = cds.Replace("#", vw);
        //                    break;
        //                case 2:
        //                    cds = cds.Replace("$", vw);
        //                    break;
        //                case 3:
        //                    cds = cds.Replace("%", vw);
        //                    break;
        //                case 4:
        //                    cds = cds.Replace("^", vw);
        //                    break;
        //                case 5:
        //                    cds = cds.Replace("&", vw);
        //                    break;
        //            }
        //            cds = cds.Replace("or", " or ").Replace("and", " and ");
        //            vw = "";
        //        }
        //        else
        //            vw += " and ";
        //    }
        //}
        //if (cds.Trim() != "")
        //    return cds + " and ";//由于where语句在最后组织时需要cut最后4个字符,所以加上4个无效字符避免出错
        //else
        //    return vw;
        #endregion

        #region//新版本

        string vw = string.Empty, filedName = string.Empty, conditionName = string.Empty, textName = string.Empty;
        string cds = txtCds.Text.Replace("1", "#").Replace("2", "$").Replace("3", "%").Replace("4", "^").Replace("5", "&");//条件之间的关系
        //DataTable dt = new DataTable();
        //dt = so.sqlExcuteQueryTable("select ENname,type,Udefined from DataInfo where isCondition=1");
        for (int i = 1; i <= 5; i++)
        {
            filedName = "ddlFiled" + i.ToString();
            conditionName = "ddlCondition" + i.ToString();
            textName = "txtText" + i.ToString();
            DropDownList myFiled = (DropDownList)divScreen.FindControl(filedName);
            DropDownList myCondition = (DropDownList)divScreen.FindControl(conditionName);

            //if (dt == null || dt.Rows.Count == 0)
            //    return null;
            if (myFiled.SelectedIndex > 0)
            {
                string x = getCondition(myCondition.SelectedValue, i.ToString(), myFiled.SelectedValue);
                vw += x;
                //按照条件进行替换
                if (!string.IsNullOrEmpty(cds))
                {
                    switch (i)
                    {
                        case 1:
                            cds = cds.Replace("#", vw);
                            break;
                        case 2:
                            cds = cds.Replace("$", vw);
                            break;
                        case 3:
                            cds = cds.Replace("%", vw);
                            break;
                        case 4:
                            cds = cds.Replace("^", vw);
                            break;
                        case 5:
                            cds = cds.Replace("&", vw);
                            break;
                    }
                    cds = cds.Replace("or", " or ").Replace("and", " and ");
                    vw = "";
                }
                else
                    vw += " and ";
            }
        }
        if (cds.Trim() != "")
            return cds + " and ";//由于where语句在最后组织时需要cut最后4个字符,所以加上4个无效字符避免出错
        else
            return vw;
        #endregion

    }

    #region//旧版
    //private string getCondition(string input, string t, string columnName, DataTable dt)
    //{
    //    string rst = string.Empty;
    //    string value = "";
    //    HtmlGenericControl c;
    //    c = (HtmlGenericControl)divScreen.FindControl("sp_text_" + t);
    //    if (c.Style["display"] != "none")
    //    {
    //        TextBox tb = (TextBox)divScreen.FindControl("txtText" + t);
    //        value = tb.Text;
    //    }
    //    else
    //    {
    //        c = (HtmlGenericControl)divScreen.FindControl("sp_rb_" + t);
    //        if (c.Style["display"] != "none")
    //        {
    //            RadioButton rb1 = (RadioButton)divScreen.FindControl("rb" + t + "_1");
    //            RadioButton rb2 = (RadioButton)divScreen.FindControl("rb" + t + "_2");
    //            if (rb1.Checked == true)
    //                value = rb1.Text;
    //            else
    //                value = rb2.Text;
    //        }
    //    }

    //    if (value.Trim().Length > 0)
    //    {
    //        DataRow[] dr = dt.Select("ENname='" + columnName + "'");
    //        string type = dr[0][1].ToString();
    //        if (!(type == "整数" || type == "数字" || type == "货币" || type == "整数型" || type == "公式(整数)" || type == "公式(小数)" || type == "小数型"))
    //            value = "'" + value + "'";
    //        switch (input)
    //        {
    //            case "大于":
    //                rst = columnName + " > " + value;
    //                break;
    //            case "小于":
    //                rst = columnName + " <" + value;
    //                break;
    //            case "等于":
    //                rst = columnName + " =" + value;
    //                break;
    //            case "大于或等于":
    //                rst = columnName + " >= " + value;
    //                break;
    //            case "小于或等于":
    //                rst = columnName + " <= " + value;
    //                break;
    //            case "包含":
    //                value = value.Replace("'", "");
    //                rst = columnName + " like '%" + value + "%'";
    //                break;
    //            case "不包含":
    //                value = value.Replace("'", "");
    //                rst = columnName + "not like '%" + value + "%'";
    //                break;
    //            case "起始字符":
    //                value = value.Replace("'", "");
    //                rst = columnName + " like '" + value + "'%";
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        c = (HtmlGenericControl)divScreen.FindControl("sp_date_" + t);
    //        if (c.Style["display"] != "none")
    //        {
    //            TextBox date1 = (TextBox)divScreen.FindControl("date_" + t + "_y");
    //            TextBox date2 = (TextBox)divScreen.FindControl("date_" + t + "_m");
    //            TextBox date3 = (TextBox)divScreen.FindControl("date_" + t + "_d");
    //            if (date1.Text.Trim().Length > 0 && date2.Text.Trim().Length > 0 && date3.Text.Trim().Length > 0)
    //            {
    //                string date = date1.Text.Trim() + "-" + date2.Text.Trim() + "-" + date3.Text.Trim();
    //                switch (input)
    //                {
    //                    case "大于":
    //                        rst = "cast(" + columnName + " as datetime) > '" + date + "'";
    //                        break;
    //                    case "小于":
    //                        rst = "cast(" + columnName + " as datetime) < '" + date + "'";
    //                        break;
    //                    case "大于或等于":
    //                        rst = "cast(" + columnName + " as datetime) >= '" + date + "'";
    //                        break;
    //                    case "小于或等于":
    //                        rst = "cast(" + columnName + " as datetime) <= '" + date + "'";
    //                        break;
    //                    case "等于":
    //                    case "包含":
    //                    case "起始字符":
    //                    case "不包含":
    //                        rst = "cast(" + columnName + " as datetime) = '" + date + "'";
    //                        break;
    //                }
    //            }
    //            else if (date1.Text.Trim().Length == 0 && date2.Text.Trim().Length > 0 && date3.Text.Trim().Length > 0)
    //            {
    //                switch (input)
    //                {
    //                    case "大于":
    //                        rst = "month(cast(" + columnName + " as datetime)) > " + date2.Text.Trim() +
    //                            " or (month(cast(" + columnName + " as datetime)) = " + date2.Text.Trim() +
    //                            " and day(cast(" + columnName + " as datetime)) >" + date3.Text.Trim() + ")";
    //                        break;
    //                    case "小于":
    //                        rst = "month(cast(" + columnName + " as datetime)) < " + date2.Text.Trim() +
    //                            " or (month(cast(" + columnName + " as datetime)) = " + date2.Text.Trim() +
    //                            " and day(cast(" + columnName + " as datetime)) <" + date3.Text.Trim() + ")";
    //                        break;
    //                    case "大于或等于":
    //                        rst = "month(cast(" + columnName + " as datetime)) >= " + date2.Text.Trim() +
    //                            " or (month(cast(" + columnName + " as datetime)) = " + date2.Text.Trim() +
    //                            " and day(cast(" + columnName + " as datetime)) >=" + date3.Text.Trim() + ")";
    //                        break;
    //                    case "小于或等于":
    //                        rst = "month(cast(" + columnName + " as datetime)) <= " + date2.Text.Trim() +
    //                            " or (month(cast(" + columnName + " as datetime)) = " + date2.Text.Trim() +
    //                            " and day(cast(" + columnName + " as datetime)) <=" + date3.Text.Trim() + ")";
    //                        break;
    //                    case "等于":
    //                    case "包含":
    //                    case "起始字符":
    //                    case "不包含":
    //                        rst = "month(cast(" + columnName + " as datetime))=" + date2.Text.Trim() + " and  day(cast(" + columnName + " as datetime))=" + date3.Text.Trim();
    //                        break;
    //                }

    //            }
    //            else if (date1.Text.Trim().Length > 0 && date2.Text.Trim().Length == 0 && date3.Text.Trim().Length == 0)
    //            {
    //                switch (input)
    //                {
    //                    case "大于":
    //                        rst = "year(cast(" + columnName + " as datetime)) > " + date1.Text.Trim();
    //                        break;
    //                    case "小于":
    //                        rst = "year(cast(" + columnName + " as datetime)) < " + date1.Text.Trim();
    //                        break;
    //                    case "大于或等于":
    //                        rst = "year(cast(" + columnName + " as datetime)) >= " + date1.Text.Trim();
    //                        break;
    //                    case "小于或等于":
    //                        rst = "year(cast(" + columnName + " as datetime)) <= " + date1.Text.Trim();
    //                        break;
    //                    case "等于":
    //                    case "包含":
    //                    case "起始字符":
    //                    case "不包含":
    //                        rst = "year(cast(" + columnName + " as datetime))=" + date1.Text.Trim();
    //                        break;

    //                }
    //            }
    //        }
    //    }
    //    return rst;
    //}
    #endregion
    #region//新版
    private string getCondition(string input, string t, string columnName)
    {
        string rst = string.Empty;
        string value = string.Empty;
        HtmlGenericControl c;
        c = (HtmlGenericControl)divScreen.FindControl("sp_text_" + t);
        if (c.Style["display"] != "none")
        {
            TextBox tb = (TextBox)divScreen.FindControl("txtText" + t);
            value = tb.Text;
        }
        else
        {
            c = (HtmlGenericControl)divScreen.FindControl("sp_rb_" + t);
            if (c.Style["display"] != "none")
            {
                RadioButton rb1 = (RadioButton)divScreen.FindControl("rb" + t + "_1");
                RadioButton rb2 = (RadioButton)divScreen.FindControl("rb" + t + "_2");
                if (rb1.Checked == true)
                    value = rb1.Text;
                else
                    value = rb2.Text;
            }
        }
        string[] dr = columnName.Split('-');
        if (value.Trim().Length > 0)
        {
            //DataRow[] dr = dt.Select("ENname='" + columnName + "'");

            //string type = dr[0][1].ToString();
            string type = dr[1].ToString();
            if (!(type == "整数" || type == "数字" || type == "货币" || type == "整数型" || type == "公式(整数)" || type == "公式(小数)" || type == "小数型"))
                value = "'" + value + "'";


            #region//旧版
            //switch (input)
            //{
            //    case "大于":
            //        rst = dr[0] + " > " + value;
            //        break;
            //    case "小于":
            //        rst = dr[0] + " <" + value;
            //        break;
            //    case "等于":
            //        rst = dr[0] + " =" + value;
            //        break;
            //    case "大于或等于":
            //        rst = dr[0] + " >= " + value;
            //        break;
            //    case "小于或等于":
            //        rst = dr[0] + " <= " + value;
            //        break;
            //    case "包含":
            //        value = value.Replace("'", "");
            //        rst = dr[0] + " like '%" + value + "%'";
            //        break;
            //    case "不包含":
            //        value = value.Replace("'", "");
            //        rst = dr[0] + "not like '%" + value + "%'";
            //        break;
            //    case "起始字符":
            //        value = value.Replace("'", "");
            //        rst = dr[0] + " like '" + value + "'%";
            //        break;
            //}
            #endregion

            #region//新版
            if (dr[0] == "beginDate")
            {
                rst = "CAST(beginDate as datetime)";
                value = "CAST( " + value + " as datetime)";
            }
            else
            {
                rst = dr[0];

            }
            switch (input)
            {
                case "大于":
                    rst = rst + " > " + value;
                    break;
                case "小于":
                    rst = rst + " <" + value;
                    break;
                case "等于":
                    rst = rst + " =" + value;
                    break;
                case "大于或等于":
                    rst = rst + " >= " + value;
                    break;
                case "小于或等于":
                    rst = rst + " <= " + value;
                    break;
                case "包含":
                    value = value.Replace("'", "");
                    rst = rst + " like '%" + value + "%'";
                    break;
                case "不包含":
                    value = value.Replace("'", "");
                    rst = rst + "not like '%" + value + "%'";
                    break;
                case "起始字符":
                    value = value.Replace("'", "");
                    rst = rst + " like '" + value + "'%";
                    break;
            }
            #endregion
        }
        else
        {
            c = (HtmlGenericControl)divScreen.FindControl("sp_date_" + t);
            if (c.Style["display"] != "none")
            {
                TextBox date1 = (TextBox)divScreen.FindControl("date_" + t + "_y");
                TextBox date2 = (TextBox)divScreen.FindControl("date_" + t + "_m");
                TextBox date3 = (TextBox)divScreen.FindControl("date_" + t + "_d");
                if (date1.Text.Trim().Length > 0 && date2.Text.Trim().Length > 0 && date3.Text.Trim().Length > 0)
                {
                    string date = date1.Text.Trim() + "-" + date2.Text.Trim() + "-" + date3.Text.Trim();
                    switch (input)
                    {
                        case "大于":
                            rst = "cast(" + dr[0] + " as datetime) > '" + date + "'";
                            break;
                        case "小于":
                            rst = "cast(" + dr[0] + " as datetime) < '" + date + "'";
                            break;
                        case "大于或等于":
                            rst = "cast(" + dr[0] + " as datetime) >= '" + date + "'";
                            break;
                        case "小于或等于":
                            rst = "cast(" + dr[0] + " as datetime) <= '" + date + "'";
                            break;
                        case "等于":
                        case "包含":
                        case "起始字符":
                        case "不包含":
                            rst = "cast(" + dr[0] + " as datetime) = '" + date + "'";
                            break;
                    }
                }
                else if (date1.Text.Trim().Length == 0 && date2.Text.Trim().Length > 0 && date3.Text.Trim().Length > 0)
                {
                    switch (input)
                    {
                        case "大于":
                            rst = "month(cast(" + dr[0] + " as datetime)) > " + date2.Text.Trim() +
                                " or (month(cast(" + dr[0] + " as datetime)) = " + date2.Text.Trim() +
                                " and day(cast(" + dr[0] + " as datetime)) >" + date3.Text.Trim() + ")";
                            break;
                        case "小于":
                            rst = "month(cast(" + dr[0] + " as datetime)) < " + date2.Text.Trim() +
                                " or (month(cast(" + dr[0] + " as datetime)) = " + date2.Text.Trim() +
                                " and day(cast(" + dr[0] + " as datetime)) <" + date3.Text.Trim() + ")";
                            break;
                        case "大于或等于":
                            rst = "month(cast(" + dr[0] + " as datetime)) >= " + date2.Text.Trim() +
                                " or (month(cast(" + dr[0] + " as datetime)) = " + date2.Text.Trim() +
                                " and day(cast(" + dr[0] + " as datetime)) >=" + date3.Text.Trim() + ")";
                            break;
                        case "小于或等于":
                            rst = "month(cast(" + dr[0] + " as datetime)) <= " + date2.Text.Trim() +
                                " or (month(cast(" + dr[0] + " as datetime)) = " + date2.Text.Trim() +
                                " and day(cast(" + dr[0] + " as datetime)) <=" + date3.Text.Trim() + ")";
                            break;
                        case "等于":
                        case "包含":
                        case "起始字符":
                        case "不包含":
                            rst = "month(cast(" + dr[0] + " as datetime))=" + date2.Text.Trim() + " and  day(cast(" + dr[0] + " as datetime))=" + date3.Text.Trim();
                            break;
                    }

                }
                else if (date1.Text.Trim().Length > 0 && date2.Text.Trim().Length == 0 && date3.Text.Trim().Length == 0)
                {
                    switch (input)
                    {
                        case "大于":
                            rst = "year(cast(" + dr[0] + " as datetime)) > " + date1.Text.Trim();
                            break;
                        case "小于":
                            rst = "year(cast(" + dr[0] + " as datetime)) < " + date1.Text.Trim();
                            break;
                        case "大于或等于":
                            rst = "year(cast(" + dr[0] + " as datetime)) >= " + date1.Text.Trim();
                            break;
                        case "小于或等于":
                            rst = "year(cast(" + dr[0] + " as datetime)) <= " + date1.Text.Trim();
                            break;
                        case "等于":
                        case "包含":
                        case "起始字符":
                        case "不包含":
                            rst = "year(cast(" + dr[0] + " as datetime))=" + date1.Text.Trim();
                            break;

                    }
                }
            }
        }
        return rst;
    }
    #endregion

    /// <summary>
    /// 拼接问卷调查问题的html字符串
    /// </summary>
    /// <returns></returns>
    private string getInvWhere()
    {
        string conditionStr = "card_id in (";
        string innerStr = "";
        foreach (Control c in phInvs.Controls[0].Controls)
        {
            switch (c.GetType().Name)
            {
                case "TextBox":
                    if (((TextBox)c).Text.Trim() != "")
                    {
                        string tid = c.ClientID.Replace("ctl00_cph_", "");
                        string txt = ((TextBox)c).Text.Trim();
                        string r = (string)so.sqlExecuteScalar("select CID from Investigate_vip where QID='" + tid + "' and invAnswer like '%" + txt + "%'");
                        if (r != null)
                            innerStr += "'" + r + "',";
                    }
                    break;
                case "CheckBox":
                    if (((CheckBox)c).Checked)
                    {
                        string tid = c.ClientID.Replace("ctl00_cph_", "");
                        tid = tid.Substring(0, tid.LastIndexOf("_"));
                        string txt = ((CheckBox)c).Text.Trim();
                        string r = (string)so.sqlExecuteScalar("select CID from Investigate_vip where QID='" + tid + "' and invAnswer like '%" + txt + "%'");
                        if (r != null)
                        {
                            if (innerStr.IndexOf(r) < 0)
                                innerStr += "'" + r + "',";
                        }
                    }
                    break;
                case "RadioButton":
                    if (((RadioButton)c).Checked)
                    {
                        string tid = c.ClientID.Replace("ctl00_cph_", "");
                        tid = tid.Substring(0, tid.LastIndexOf("_"));
                        string txt = ((RadioButton)c).Text.Trim();
                        string r = (string)so.sqlExecuteScalar("select CID from Investigate_vip where QID='" + tid + "' and invAnswer like '%" + txt + "%'");
                        if (r != null)
                            innerStr += "'" + r + "',";
                    }
                    break;
            }
        }
        if (innerStr.Length > 0)
            return conditionStr + innerStr.Substring(0, innerStr.Length - 1) + ") and ";
        else
            return "1<>1";
    }

    private void alterDetails(string t)
    {
        try
        {
            #region
            //判断修改资料方式
            if (t != "all")
            {
                string CID = "";
                string filedName = Request.QueryString["filed"];//card_Discount
                string filedValue = Request.QueryString["value"];//10
                string filedText = Request.QueryString["filedCN"];//折扣
                int count = 0;//记录修改记录数以保存日志
                DataTable tdt = so.sqlExcuteQueryTable("select type,Udefined from DataInfo where ENname='" +
                    filedName + "'");
                string type = tdt.Rows[0][0].ToString();//小数型
                string ud = tdt.Rows[0][1].ToString();//False
                string usql = "";
                if (type == "文本型" || type == "日期型" || type == "文本" || type == "日期" ||
                    type == "电话" || type == "传真" || type == "电子邮件" || type == "邮编" ||
                    type == "币种" || type == "复选框" || type == "URL" || type == "自动编号" || type == "选项列表")
                {
                    usql = "update @tbname set [" + filedName + "] = '" + filedValue + "' where ";
                }
                else if (type.IndexOf("公式") >= 0)
                {
                    Response.Write("此字段为公式字段,不可修改!");
                    Response.End();
                    return;
                }
                else
                {
                    usql = "update @tbname set [" + filedName + "] = " +
                        filedValue + " where ";
                }
                string uw = "card_id in (";
                for (int i = 0; i < GVvip.Rows.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)GVvip.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                    {
                        uw += "'" + GVvip.Rows[i].Cells[1].Text + "',";
                        CID += GVvip.Rows[i].Cells[1].Text + splitChar1[0];
                        count++;
                    }
                }
                if (uw == "card_id in (")
                {
                    Response.Write("请至少选择一行需要修改的会员信息!");
                    Response.End();
                    return;
                }
                else
                    uw = uw.Substring(0, uw.Length - 1) + ")";
                usql += uw;
                //判断是否自定义字段,修改不同的表
                if (ud.ToLower() == "1" || ud.ToLower() == "true")
                    usql = usql.Replace("@tbname", "UD_Field");
                else
                    usql = usql.Replace("@tbname", "cardInfo");
                so.sqlExcuteNonQuery(usql, false);
                //提交数据处理平台
                if (CID.Length > 0)
                {
                    CID = CID.Substring(0, CID.Length - 1);
                    DateTime newDT = new DateTime(2010, 1, 1);
                    TimeSpan sp = DateTime.Now - newDT;
                    double li = sp.TotalMilliseconds;

                    byte[] tt = Encoding.Default.GetBytes(filedName + splitChar2[0] + filedValue + splitChar2[0] + CID);
                    string sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                        + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    DataTable dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    //修改内存表中的数据便于显示给用户修改结果
                    DataTable VipTable = (DataTable)Cache["vipTable"];
                    string[] tcid = CID.Split(splitChar1[0]);
                    foreach (string id in tcid)
                    {
                        DataRow[] dr = VipTable.Select("卡号='" + id + "'");
                        dr[0][filedText] = filedValue;
                    }
                    Cache["vipTable"] = VipTable;
                }
                //记录系统日志
                //lm.InsLog(Session["LoginID"].ToString(), "修改" + count + "条VIP基本资料");
                Response.Write("success");
            }
            #endregion
            #region
            else
            {
                string filedName = Request.QueryString["filed"];
                string filedValue = Request.QueryString["value"];
                string filedText = Request.QueryString["filedCN"];
                DataTable tdt = so.sqlExcuteQueryTable("select type,Udefined from DataInfo where ENname='" +
                    filedName + "'");
                string type = tdt.Rows[0][0].ToString();
                string ud = tdt.Rows[0][1].ToString();
                string usql = "";
                if (type == "文本型" || type == "日期型" || type == "文本" || type == "日期" ||
                    type == "电话" || type == "传真" || type == "电子邮件" || type == "邮编" ||
                    type == "币种" || type == "复选框" || type == "URL" || type == "自动编号" || type == "选项列表")
                {
                    usql = "update @tbname set [" + filedName + "] = '" + filedValue + "' from cardInfo as cf inner join " +
                        "UD_Fileds as ud on cf.card_id = ud.card_id ";
                }
                else if (type.IndexOf("公式") >= 0)
                {
                    Response.Write("此字段为公式字段,不可修改!");
                    Response.End();
                    return;
                }
                else
                {
                    usql = "update @tbname set [" + filedName + "] = " +
                        filedValue + " where ";
                }
                //判断是否自定义字段,修改不同的表
                if (ud.ToLower() == "1" || ud.ToLower() == "true")
                    usql = usql.Replace("@tbname", "UD_Field");
                else
                    usql = usql.Replace("@tbname", "cardInfo");
                usql += " where " + Session["whereString"].ToString();
                so.sqlExcuteNonQuery(usql, false);

                //提交数据处理平台
                DateTime newDT = new DateTime(2010, 1, 1);
                TimeSpan sp = DateTime.Now - newDT;
                double li = sp.TotalMilliseconds;

                byte[] tt = Encoding.Default.GetBytes(filedName + splitChar2[0] + filedValue + splitChar2[0] + "all");
                string sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                    + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//48+16=64;
                DataTable dt = new DataTable();
                dt.Columns.Add("data", typeof(string));
                dt.Columns.Add("value", typeof(object));
                dt.Columns.Add("type", typeof(string));
                dt.Rows.Add(new object[] { "@b", tt, "binary" });
                so.sqlExcuteNonQuery(sql, dt);
                //重新查询修改之后的数据并显示
                DataTable vt = so.sqlExcuteQueryTable(Session["mainSql"].ToString());
                Cache["vipTable"] = vt;
                Response.Write("success");
            }
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.Replace("'", "").Replace("\r\n", ""));
        }
        finally { Response.End(); }
    }

    private void alterPoints(string t)
    {
        try
        {
            #region
            //判断修改资料方式
            if (t != "all")
            {
                string CID = "";
                string filedName = Request.QueryString["filed"];//card_Discount
                string filedValue = Request.QueryString["value"];//10
                float tempValue = float.Parse(filedValue);

                string filedText = Request.QueryString["filedCN"];//折扣
                int count = 0;//记录修改记录数以保存日志
                DataTable tdt = so.sqlExcuteQueryTable("select type,Udefined from DataInfo where ENname='" +
                    filedName + "'");
                string type = tdt.Rows[0][0].ToString();//小数型
                string ud = tdt.Rows[0][1].ToString();//False
                string usql = "";
                if (type == "文本型" || type == "日期型" || type == "文本" || type == "日期" ||
                    type == "电话" || type == "传真" || type == "电子邮件" || type == "邮编" ||
                    type == "币种" || type == "复选框" || type == "URL" || type == "自动编号" || type == "选项列表")
                {
                    usql = "update @tbname set [" + filedName + "] = '" + filedValue + "' where ";
                }
                else if (type.IndexOf("公式") >= 0)
                {
                    Response.Write("此字段为公式字段,不可修改!");
                    Response.End();
                    return;
                }
                else
                {
                    if (tempValue < 0)
                    {
                        usql = "update @tbname set [" + filedName + "] = " + filedName + "-" +
                                               (-tempValue) + " where ";
                    }
                    else
                    {
                        usql = "update @tbname set [" + filedName + "] = " + filedName + "+" +
                                              (filedValue) + " where ";
                    }

                }
                string uw = "card_id in (";
                for (int i = 0; i < GVvip.Rows.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)GVvip.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                    {
                        uw += "'" + GVvip.Rows[i].Cells[1].Text + "',";
                        CID += GVvip.Rows[i].Cells[1].Text + splitChar1[0];
                        count++;
                    }
                }
                if (uw == "card_id in (")
                {
                    Response.Write("请至少选择一行需要修改的会员信息!");
                    Response.End();
                    return;
                }
                else
                    uw = uw.Substring(0, uw.Length - 1) + ")";
                usql += uw;
                //判断是否自定义字段,修改不同的表
                if (ud.ToLower() == "1" || ud.ToLower() == "true")
                    usql = usql.Replace("@tbname", "UD_Field");
                else
                    usql = usql.Replace("@tbname", "cardInfo");
                so.sqlExcuteNonQuery(usql, false);
                //提交数据处理平台
                if (CID.Length > 0)
                {
                    CID = CID.Substring(0, CID.Length - 1);
                    DateTime newDT = new DateTime(2010, 1, 1);
                    TimeSpan sp = DateTime.Now - newDT;
                    double li = sp.TotalMilliseconds;

                    byte[] tt = Encoding.Default.GetBytes(filedName + splitChar2[0] + filedValue + splitChar2[0] + CID);
                    string sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                        + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    DataTable dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    //修改内存表中的数据便于显示给用户修改结果
                    DataTable VipTable = (DataTable)Cache["vipTable"];
                    string[] tcid = CID.Split(splitChar1[0]);
                    foreach (string id in tcid)
                    {
                        DataRow[] dr = VipTable.Select("卡号='" + id + "'");
                        dr[0][filedText] = float.Parse(dr[0][filedText].ToString()) + tempValue;
                        dr[0]["可用积分"] = float.Parse(dr[0]["可用积分"].ToString()) + tempValue;
                    }
                    Cache["vipTable"] = VipTable;
                }
                //记录系统日志
                //lm.InsLog(Session["LoginID"].ToString(), "修改" + count + "条VIP基本资料");
                //string path = @"c:/lkspoints";
                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //FileStream fs = new FileStream(@"c:/lkspoints/points.txt", FileMode.OpenOrCreate, FileAccess.Write);
                //BinaryWriter bw = new BinaryWriter(fs);
                //bw.Write(filedValue);
                //fs.Close();
                //bw.Close();
                Response.Write("success");
            }
            #endregion
            #region
            else
            {
                string filedName = Request.QueryString["filed"];
                string filedValue = Request.QueryString["value"];
                float tempValue = float.Parse(filedValue);
                string filedText = Request.QueryString["filedCN"];
                DataTable tdt = so.sqlExcuteQueryTable("select type,Udefined from DataInfo where ENname='" +
                    filedName + "'");
                string type = tdt.Rows[0][0].ToString();
                string ud = tdt.Rows[0][1].ToString();
                string usql = "";
                if (type == "文本型" || type == "日期型" || type == "文本" || type == "日期" ||
                    type == "电话" || type == "传真" || type == "电子邮件" || type == "邮编" ||
                    type == "币种" || type == "复选框" || type == "URL" || type == "自动编号" || type == "选项列表")
                {
                    usql = "update @tbname set [" + filedName + "] = '" + filedValue + "' from cardInfo as cf inner join " +
                        "UD_Fileds as ud on cf.card_id = ud.card_id ";
                }
                else if (type.IndexOf("公式") >= 0)
                {
                    Response.Write("此字段为公式字段,不可修改!");
                    Response.End();
                    return;
                }
                else
                {
                    if (tempValue < 0)
                    {
                        usql = "update @tbname set [" + filedName + "] = " + filedName + "-" +
                                               (-tempValue) + " where ";
                    }
                    else
                    {
                        usql = "update @tbname set [" + filedName + "] = " + filedName + "+" +
                       filedValue + " where ";
                    }

                }
                //判断是否自定义字段,修改不同的表
                if (ud.ToLower() == "1" || ud.ToLower() == "true")
                    usql = usql.Replace("@tbname", "UD_Field");
                else
                    usql = usql.Replace("@tbname", "cardInfo");
                usql += " where " + Session["whereString"].ToString();
                so.sqlExcuteNonQuery(usql, false);

                //提交数据处理平台
                DateTime newDT = new DateTime(2010, 1, 1);
                TimeSpan sp = DateTime.Now - newDT;
                double li = sp.TotalMilliseconds;

                byte[] tt = Encoding.Default.GetBytes(filedName + splitChar2[0] + filedValue + splitChar2[0] + "all");
                string sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                    + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//48+16=64;
                DataTable dt = new DataTable();
                dt.Columns.Add("data", typeof(string));
                dt.Columns.Add("value", typeof(object));
                dt.Columns.Add("type", typeof(string));
                dt.Rows.Add(new object[] { "@b", tt, "binary" });
                so.sqlExcuteNonQuery(sql, dt);
                //重新查询修改之后的数据并显示
                DataTable vt = so.sqlExcuteQueryTable(Session["mainSql"].ToString());
                Cache["vipTable"] = vt;
                Response.Write("success");
            }
            #endregion
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.Replace("'", "").Replace("\r\n", ""));
        }
        finally { Response.End(); }
    }

    private void couponApplication(string t)
    {
        try
        {
            if (so.sqlExecuteScalar("select couponUsed from sysState").ToString().ToLower() == "true")
            {
                throw new Exception("服务器繁忙,请稍后再试.");
            }

            #region//测试
            //couponWeb.couponService cs = new couponWeb.couponService();
            //cs.Url = memberStatic.webServiceUrl;//修改其地址
            //cs.Timeout = 60 * 1000;
            #endregion
            couponService cs = new couponService();
            //<data vid="v00000001" getDate="2009-01-01 01:00:00" endDate="2009-03-03 23:59:59" tid="100" />
            string cardID = "", input = "", endDate = "", tid = "", getDate = "";
            tid = Request.QueryString["typeID"];//3
            endDate = Request.QueryString["endDate"];//2012-06-29
            getDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");//2012-6-12 14:16:32
            if (t == "all")
            {
                try
                {
                    so.sqlExcuteNonQuery("update sysState set couponUsed=1", false);
                    input = "<data vid=\"" + cardID + "\" getDate=\"" + getDate + "\" endDate=\"" + endDate + "\" tid=\"" + tid + "\" />";
                    string x = cs.GetAllPCwithTID(Session["fromString"] + " where " + Session["whereString"].ToString(), input);
                    if (x != "本次申请全部成功.")
                        throw new Exception(x);
                }
                catch (Exception ex)
                { throw ex; }
                finally { so.sqlExcuteNonQuery("update sysState set couponUsed=0", false); }
            }
            else
            {
                ArrayList al = new ArrayList();
                for (int i = 0; i < GVvip.Rows.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)GVvip.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                        al.Add(GVvip.Rows[i].Cells[1].Text);
                }
                string rs = "";
                int errorCount = 0;
                int noPoints = 0;
                ArrayList notSendCardid = new ArrayList();

                XmlDocument doc = new XmlDocument();
                XmlParseFunction xmlParse = new XmlParseFunction();

                for (int i = 0; i < al.Count; i++)
                {
                    input = "<data vid=\"" + al[i].ToString() + "\" getDate=\"" + getDate + "\" endDate=\"" + endDate + "\" tid=\"" + tid + "\" />";
                    //<data vid="3033399246" getDate="2012-6-12 14:16:32" endDate="2012-06-29" tid="3" />
                    rs = cs.GetNewPCwithTid(input);


                    if (rs.IndexOf("[error]") >= 0)
                    {
                        errorCount++;
                        notSendCardid.Add(al[i].ToString());
                        continue;
                    }

                    doc.LoadXml(rs);
                    XmlNode root = xmlParse.SelectXmlNode(doc, "data");

                    if (root.ChildNodes.Count == 1)
                    {
                        notSendCardid.Add(al[i].ToString());
                        noPoints++;
                    }


                }
                string ext = "操作完成.申请失败" + (errorCount + noPoints).ToString() + "个.";
                if (noPoints > 0)
                    ext += noPoints + "个由于[可用积分不足].";
                if (errorCount > 0)
                    ext += errorCount + "个由于[" + rs + "]";


                string r = sendCoupon(tid, notSendCardid);
                if (r != "true")
                {
                    //若发送方法执行失败,则删除所有刚申请的优惠券
                    so.sqlExcuteNonQuery("delete from couponMain where getDate='" + getDate + "'", false);
                    throw new Exception("优惠券短信发送失败_" + r);
                }
                if ((errorCount + noPoints) > 0)
                    throw new Exception(ext);
            }
            Response.Write("success");
        }
        catch (Exception ex) { Response.Write(ex.Message.Replace("'", "").Replace("\r\n", "")); }
        finally { Response.End(); }
    }

    /// <summary>
    /// 申请优惠券后的发送方法
    /// </summary>
    /// <param name="t">申请优惠券方式</param>
    /// <returns>无异常则返回true</returns>
    private string sendCoupon(string couponTypeID, ArrayList notSend)
    {
        try
        {
            string sql = "", cardid = "", mobile = "";
            sql = "select PRI from PRIsetting where smsType='直复'";
            string pri = so.sqlExecuteScalar(sql).ToString();

            ArrayList al = new ArrayList();
            for (int i = 0; i < GVvip.Rows.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)GVvip.Rows[i].Cells[0].FindControl("CheckBox1")).Checked)
                    al.Add(GVvip.Rows[i].Cells[1].Text);
            }
            //在本页选中的卡号中移除申请失败的会员卡号

            for (int i = 0; i < notSend.Count; i++)
            {
                al.Remove(notSend[i]);
            }


            for (int i = 0; i < al.Count; i++)
            {
                cardid = al[i].ToString();
                string selsql = "select score,article,money from couponType where tid=" + couponTypeID;
                DataTable articleTable = so.sqlExcuteQueryTable(selsql);
                sql = "select cid,endDate from couponMain where vipID='" + cardid + "' and convert(varchar(20),[getDate],120) like '" + DateTime.Now.ToString("yyyy-MM-dd") + "%' order by [getDate] desc";
                DataTable dt = so.sqlExcuteQueryTable(sql);
                if (dt.Rows[0][0] != null && dt.Rows[0][0].ToString().Trim() != "")
                {
                    sql = "select userMobile,points from cardInfo where card_id='" + cardid + "'";
                    string product = articleTable.Rows[0][1].ToString();//对应的礼品
                    string money = articleTable.Rows[0][2].ToString();//对应的金额
                    money = money.Contains("元") == false ? money + "元" : money;
                    string txtMsg = (string.IsNullOrEmpty(product) == true ? "可作为" + money + "的消费劵" : "可兑换" + product + "礼品,或者作为" + money + "的消费劵");
                    //mobile = (string)so.sqlExecuteScalar(sql);
                    DataTable dtlks = so.sqlExcuteQueryTable(sql);
                    if (dtlks != null && dtlks.Rows.Count != 0)
                    {
                        mobile = dtlks.Rows[0][0].ToString();

                        if (mobile != null && mobile.Trim() != "")
                        {
                            sql = "insert into sendData values('" + mobile + "','直复','尊敬的会员您好！您的卡号为" + cardid + "已有" + dtlks.Rows[0][1].ToString() + "超过" + articleTable.Rows[0][0].ToString() +
                                "积分," + txtMsg + ",请于" + Convert.ToDateTime(dt.Rows[0][1].ToString()).ToString("yyyy-MM-dd") + "前凭此条短信和优惠券号"
                                + dt.Rows[0][0].ToString() + "到指定专卖店申请兑换。详询0755-86621896.','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',0," + pri + ")";
                            so.sqlExcuteNonQuery(sql, false);
                        }
                    }
                }
            }
            return "true";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    protected void GVvip_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#feb193'");
        }
    }

    /// <summary>
    /// 设置已关联字样
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSendInvs_Click(object sender, EventArgs e)
    {
        if (phInvs.Controls.Count == 0)//没有选择关联的问题就不会进行关联
            return;
        spInvsTips.Attributes.Add("style", "display:block");
        ViewState["invsWhere"] = getInvWhere();
    }

    //[AjaxMethod]
    //public  string initMSG()//必须是静态的
    //{
    //    try
    //    {
    //        initSmsBalance();
    //        initSMSlist();
    //        return "success";
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    }
    //}
}
