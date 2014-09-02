using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class salesParticular : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();
    smsOperate sms = new smsOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            initSmsBalance();
            initSMSlist();
            initDdlProperty();
        }
        if (Request.QueryString["type"] == "paging")
            this.paging();
        if (Request.QueryString["type"] == "query")
            this.btnQuery_Click();
        if (Request.QueryString["type"] == "details")
            this.showDetails();
        #region 新加的功能
        if (Request.QueryString["type"] == "send")
        {
            try
            {
                if (Request.QueryString["mode"] == "all")//对所有用户发送
                    sendSMS(true);
                else
                    sendSMS(false);//对所选用户进行发送(发送直复短信)
                Response.Write("success");
            }
            catch (Exception ex)
            { Response.Write("Error_" + ex.Message.Replace("\r\n", "").Replace("'", "")); }
            finally { Response.End(); }
        }
        #endregion


    }
    protected void btnQuery_Click()
    {
        try
        {
            string filedName = "", conditionName = "", textName = "", condition = "", cdt = "";
            string sql = " select card_id,card_Type,username,sendMan,count(card_id) as times,sum(qty) as qty,sum(amount) as amount from salesParticularView ";
            #region 拼写where语句
            foreach (Control c in pan1.Controls)
            {
                if (c.GetType().Name == "RadioButton")
                {
                    string tc = "";
                    if (((RadioButton)c).Checked)
                    {
                        tc = ((RadioButton)c).ClientID;
                        switch (tc)
                        {
                            case "ctl00_cph_times":
                                cdt = "times";
                                break;
                            case "ctl00_cph_qty":
                                cdt = "qty";
                                break;
                            case "ctl00_cph_amount":
                                cdt = "amount";
                                break;
                        }
                    }
                }
            }

            for (int i = 1; i <= 5; i++)
            {
                filedName = "ddlFiled" + i.ToString();
                conditionName = "ddlCondition" + i.ToString();
                textName = "txtText" + i.ToString();
                DropDownList myFiled = (DropDownList)divScreen.FindControl(filedName);
                DropDownList myCondition = (DropDownList)divScreen.FindControl(conditionName);
                TextBox myText = (TextBox)divScreen.FindControl(textName);
                if (myFiled.SelectedIndex > 0)
                {
                    string name = "";
                    switch (myFiled.SelectedValue)
                    {
                        case "发卡人": name = "sendMan";
                            break;
                        case "卡号": name = "card_id";
                            break;
                        case "姓名": name = "username";
                            break;
                        case "卡类型": name = "card_Type";
                            break;
                        case "消费日期": name = "sale_time";
                            break;
                        case "购物总数": name = "qty";
                            break;
                        case "消费金额": name = "amount";
                            break;
                        case "消费次数": name = "times";
                            break;
                    }
                    switch (myCondition.SelectedValue)
                    {
                        case "大于":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + ">" + myText.Text + " and ";
                            else
                                condition += name + ">'" + myText.Text + "' and ";
                            break;
                        case "等于":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + "=" + myText.Text + " and ";
                            else
                                condition += name + "='" + myText.Text + "' and ";
                            break;
                        case "小于":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + "<" + myText.Text + " and ";
                            else
                                condition += name + "<'" + myText.Text + "' and ";
                            break;
                        case "大于或等于":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + ">=" + myText.Text + " and ";
                            else
                                condition += name + ">='" + myText.Text + "' and ";
                            break;
                        case "小于或等于":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + "<=" + myText.Text + " and ";
                            else
                                condition += name + "<='" + myText.Text + "' and ";
                            break;
                        case "包含":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + "=" + myText.Text + " and ";
                            else
                                condition += name + " like '%" + myText.Text + "%' and ";
                            break;
                        case "不包含":
                            if (name == "times" || name == "amount" || name == "qty")
                                condition += name + "<>" + myText.Text + " and ";
                            else
                                condition += name + " not like '%" + myText.Text + "%' and ";
                            break;
                    }
                }
            }
            if (condition.Length > 0)
            {
                condition = condition.Substring(0, condition.Length - 4);
                //原始dm_web 适用
                //sql += "where " + condition + " group by card_id,card_Type,username,sendMan having count(card_id) >= 1 order by " + cdt + " desc";
                //口袋通 适用
                sql += "where [status]>2 and" + condition + " group by card_id,card_Type,username,sendMan having count(card_id) >= 1 order by " + cdt + " desc";

            }
            else
            {
                //原始dm_web 适用
                //sql += " group by card_id,card_Type,username,sendMan having count(card_id) >= 1 order by " + cdt + " desc";
                //口袋通 适用
                sql += "where [status]>2 group by card_id,card_Type,username,sendMan having count(card_id) >= 1 order by " + cdt + " desc";
            }
            #endregion

            DataTable mydt = so.sqlExcuteQueryTable(sql);
            DataTable final = this.sort(mydt);
            Cache["vipTable"] = final;
            Response.Write("success");
        }
        catch (Exception ex) { Response.Write("error:" + ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }
    protected void GVsales_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#feb193'");
        }
    }

    protected void btnHideVipBind_Click(object sender, EventArgs e)
    {
        //绑定处理后的VIP数据
        DataTable vipTable = (DataTable)Cache["vipPagedTable"];
        GVsales.DataSource = vipTable;
        GVsales.DataBind();
        up1.Update();
    }

    private void paging()
    {
        try
        {
            int start = 0, end = 0, total = 0, pageIndex = 0;
            pageIndex = Convert.ToInt16(Request.QueryString["pageIndex"]);
            int pageSize = 200;
            //获取查询出的VIP原始数据
            DataTable oldVipTable = (DataTable)Cache["vipTable"];
            DataTable vipTable = oldVipTable.Clone();
            //为了给每一行自动编号,添加自增长列
            DataColumn column = new DataColumn();
            column.ColumnName = "iden";
            column.DataType = typeof(int);
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            vipTable.Columns.Add(column);
            foreach (DataRow dr in oldVipTable.Rows)
                vipTable.Rows.Add(dr.ItemArray);
            //计算返回数据的起始及结束位置
            start = 1 + (pageIndex - 1) * pageSize;
            end = pageSize * pageIndex;
            //计算总数据量
            total = vipTable.Rows.Count;
            //计算总页数
            total = total % pageSize > 0 ? total / pageSize + 1 : total / pageSize;
            //开始筛选数据
            DataRow[] dt = vipTable.Select("iden>=" + start + " and iden<=" + end);
            DataTable newVipTable = vipTable.Clone();
            foreach (DataRow dr in dt)
                newVipTable.Rows.Add(dr.ItemArray);
            vipTable.Clear();

            //移除自增长列
            newVipTable.Columns.Remove("iden");
            Cache["vipPagedTable"] = newVipTable;
            Response.Write(total);
        }
        catch (Exception ex) { Response.Write("error:" + ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }
    protected void gvDetails_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
            e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#feb193'");
        }
    }

    protected void showDetails()
    {
        try
        {
            string id = Request.QueryString["id"];

            //这是以前的dm web
            //string sql = string.Format("SELECT mst.vip_id,mst.bill_id,mst.sale_time,sp.clientName,sa.salerName,dtl.discount,dtl.qty,dtl.amount,pro.productCode,pro.productName FROM lants_sale_mst AS mst inner JOIN lants_sale_dtl AS dtl ON mst.bill_id=dtl.bill_id INNER JOIN lants_Product AS pro ON dtl.product_id=pro.productcode inner join dbo.salerInfo as sa on sa.salerId=mst.saler_id inner join dbo.shopInfo as sp on sp.clientId=mst.client_id WHERE mst.vip_id='{0}'", id);
            //适用于口袋通的
            string sql = string.Format("SELECT mst.vip_id,mst.bill_id,mst.sale_time,sp.clientName,sa.salerName,dtl.discount,dtl.qty,dtl.amount,dtl.product_id productCode,dtl.product_name productName FROM lants_sale_mst AS mst inner JOIN lants_sale_dtl AS dtl ON mst.bill_id=dtl.bill_id  inner join dbo.salerInfo as sa on sa.salerId=mst.saler_id inner join dbo.shopInfo as sp on sp.clientId=mst.client_id WHERE mst.vip_id='{0}' and status>1", id);
            DataTable mydt = so.sqlExcuteQueryTable(sql);
            Session["details"] = mydt;
            Response.Write("success");
        }
        catch (Exception ex) { Response.Write("error:" + ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }
    protected void GVsales_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Attributes.Add("ondblclick", "showDetails('" + e.Row.Cells[1].Text + "')");
    }

    protected void btnHideDetails_Click(object sender, EventArgs e)
    {
        DataTable mydt = (DataTable)Session["details"];
        gvDetails.DataSource = mydt;
        gvDetails.DataBind();
        up2.Update();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showDetails", "document.getElementById('a_details').click()", true);
    }

    private DataTable sort(DataTable dt)
    {
        DataTable tempTable = dt.Clone();
        DataTable finalTable = dt.Clone();
        while (dt.Rows.Count > 0)
        {
            //抓取卡号为首行首列的一批数据
            DataRow[] drs = dt.Select("card_id='" + dt.Rows[0][0].ToString() + "'");
            //赋予临时表
            foreach (DataRow dr in drs)
            {
                DataRow tdr = tempTable.NewRow();
                tdr.ItemArray = dr.ItemArray;
                tempTable.Rows.Add(tdr);
            }
            //计算临时表的数据并赋予最终表
            DataRow finalDr = finalTable.NewRow();
            finalDr[0] = tempTable.Rows[0][0];
            finalDr[1] = tempTable.Rows[0][1];
            finalDr[2] = tempTable.Rows[0][2];
            finalDr[3] = tempTable.Rows[0][3];
            finalDr[4] = tempTable.Rows[0][4];
            finalDr[5] = tempTable.Compute("sum(qty)", "");
            finalDr[6] = tempTable.Compute("sum(amount)", "");
            finalTable.Rows.Add(finalDr);
            //删除原始表中对应的数据
            foreach (DataRow dr in drs)
                dt.Rows.Remove(dr);
            //清空临时表
            tempTable.Clear();
        }
        return finalTable;
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

    protected void initDdlProperty()
    {
        DataTable mydt = so.sqlExcuteQueryTable("select ENname,CNname from DataInfo where Displayd=1");
        ddlProperty.Items.Clear();
        for (int i = 0; i < mydt.Rows.Count; i++)
        {
            string temp = mydt.Rows[i][1].ToString();
            ddlProperty.Items.Add(new ListItem("[%" + temp + "%]", temp));
        }
    }

    protected void sendSMS(bool allSend)
    {
        try
        {

            #region//对所选用户进行短信的发送
            if (!allSend)
            {
                int count = 0;//记录发送短信个数以记录日志
                ArrayList al = new ArrayList();
                for (int i = 0; i < GVsales.Rows.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)GVsales.Rows[i].Cells[0].FindControl("ckb")).Checked)
                    {
                        al.Add(GVsales.Rows[i].Cells[1].Text);
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
}