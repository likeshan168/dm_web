using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public partial class couponManager : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();

    protected void Page_Load(object sender, EventArgs e)
    {
        //txtBdate.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        //txtEdate.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        //txtCBdate.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        //txtCEdate.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        if (!IsPostBack)
        {
            initCouponTypeTable();//初始化优惠券类型
            initPlaceList();//初始化优惠券使用的地方
            GetCouponCount();//获取还有多少可用的优惠券
        }
        if (Request.QueryString["type"] == "createPC")//这个是生成优惠劵
            CouponList();
        if (Request.QueryString["type"] == "PCdetails")
            cpDetails();
        if (Request.QueryString["type"] == "reSendCoupon")//再一次发送优惠卷的编号
            reSend();
        /*-----------新添加的优惠券兑换功能（李克善）---------------*/
        if (Request.QueryString["type"] == "Exchange")
            Exchange();
    }

    private void initCouponTypeTable()
    {
        //获取优惠劵类型信息
        string sql = "select ct.tid,ct.ctype,ct.typeDetails,cp.pname,ct.score,ct.money,ct.article,ct.usingState from " +
            "couponType as ct inner join couponPlace as cp on ct.usedPlace = cp.pid order by ct.tid";
        DataTable mydt = new DataTable();
        //
        //TODO:可以进行优化
        //
        mydt = so.sqlExcuteQueryTable(sql);
        if (mydt.Rows.Count == 0)
        {
            //mydt.Rows.Add(mydt.NewRow());
            DataRow dr = mydt.NewRow();
            dr[7] = false;
            mydt.Rows.Add(dr);
            gvCT.DataSource = mydt;
            gvCT.DataBind();
            int columnCount = gvCT.Rows[0].Cells.Count;
            gvCT.Rows[0].Cells.Clear();
            gvCT.Rows[0].Cells.Add(new TableCell());
            gvCT.Rows[0].Cells[0].ColumnSpan = columnCount;
            gvCT.Rows[0].Cells[0].Text = "暂无优惠券类别!     <a href='#typeAdd' class='nyroModal'>添加一个新类别</a>";
        }
        else
        {
            gvCT.DataSource = mydt;
            gvCT.DataBind();
        }
    }

    private void initPlaceList()
    {
        ddlUsedPlace.Items.Clear();
        ListItem dft = new ListItem();
        dft.Text = "-请选择地点-";
        dft.Value = "none";
        ddlUsedPlace.Items.Add(dft);
        DataTable mydt = so.sqlExcuteQueryTable("select pid,pname from couponPlace");
        //
        //TODO:可以进行优化
        //
        foreach (DataRow dr in mydt.Rows)
        {
            ListItem li = new ListItem();
            if (string.IsNullOrEmpty(dr[1].ToString()))
            {
                continue;
            }
            li.Text = dr[1].ToString();
            li.Value = dr[0].ToString();
            ddlUsedPlace.Items.Add(li);
        }
        ListItem other = new ListItem();
        other.Text = "其他";
        other.Value = "other";
        ddlUsedPlace.Items.Add(other);
    }

    protected void btnHide_Click(object o, EventArgs e)
    {
        initCouponTypeTable();
        //UP2.Update();
    }
    /// <summary>
    /// 保存修改
    /// </summary>
    /// <param name="o"></param>
    /// <param name="e"></param>
    protected void saveChange(object o, EventArgs e)
    {
        try
        {
            string sqlstr = "";
            for (int i = 0; i < gvCT.Rows.Count; i++)
            {
                sqlstr = "update couponType set usingState="
                    + (((CheckBox)(gvCT.Rows[i].Cells[7].Controls[1])).Checked ? "1" : "0").ToString() + " where tid="
                    + gvCT.Rows[i].Cells[0].Text + "";
                so.sqlExcuteNonQuery(sqlstr, false);
                gvCT.EditIndex = -1;
            }
            UP2.Update();
            ScriptManager.RegisterClientScriptBlock(UP2, GetType(), "success", "alert('保存修改完成')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(UP2, GetType(), "success", "alert('" +
                ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true);
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if (cbUsed.Checked || cbGetUnUsed.Checked)
        {
            #region 组织查询语句
            string sql = "select * from couponConsumeRecordView where";
            if (!string.IsNullOrEmpty(txtClientID.Text.Trim()))//发放店名称
                sql += " applyPlace like '%" + txtClientID.Text + "%' and ";
            if (cbUsed.Checked && !cbGetUnUsed.Checked)
            {
                sql += " consumed=1 and ";
            }
            else if (((!cbUsed.Checked) && cbGetUnUsed.Checked))
            {
                sql += " consumed=0 and ";
            }
            bool isCardIdSelected = false, isUserMobileSelected = false, isUserNameSelected = false;
            string cardId = string.Empty, userMobile = string.Empty, userName = string.Empty;

            if (ra1.Checked)
            {
                if (ddlVipTypes.SelectedValue == "卡号")
                {
                    isCardIdSelected = true;
                    cardId = txtCardID.Text.Trim();
                    sql += string.IsNullOrEmpty(cardId) == true ? string.Empty : " card_id='" + cardId + "' and ";
                }
                else if (ddlVipTypes.SelectedValue == "手机号码")
                {
                    isUserMobileSelected = true;
                    userMobile = txtCardID.Text.Trim();
                    sql += string.IsNullOrEmpty(userMobile) ? string.Empty : " userMobile='" + userMobile + "' and ";
                }
                else if (ddlVipTypes.SelectedValue == "姓名")
                {
                    isUserNameSelected = true;
                    userName = txtCardID.Text.Trim();
                    sql += string.IsNullOrEmpty(txtCardID.Text.Trim()) == true ? string.Empty : " userName='" + userName + "' and ";
                }
            }
            if (!string.IsNullOrEmpty(txtBdate.Text.Trim()))
                sql += " getDate>='" + txtBdate.Text + "' and ";
            if (!string.IsNullOrEmpty(txtEdate.Text.Trim()))
                sql += " getDate<='" + txtEdate.Text + "' and ";
            if (!string.IsNullOrEmpty(txtCBdate.Text.Trim()))
                sql += " endDate>='" + txtCBdate.Text + "' and ";
            if (!string.IsNullOrEmpty(txtCEdate.Text.Trim()))
                sql += " endDate<='" + txtCEdate.Text + "' and ";

            sql = sql.Substring(0, sql.Length - 5);//最后都会去掉最后一个and
            #endregion
            so = new SQL_Operate();
            #region
            DataTable finalTable = so.sqlExcuteQueryTable(sql);
            Cache["finalTable"] = finalTable;
            #endregion
            //DataTable finalTable = (DataTable)Cache["finalTable"];
            //if (finalTable == null)
            //{
            //    finalTable = so.sqlExcuteQueryTable(sql);
            //    Cache["finalTable"] = finalTable;
            //}

            #region 结果标记
            if (!string.IsNullOrEmpty(txtCardID.Text.Trim()))
            {
                DataTable mydt2 = finalTable.Clone();
                string whereStr = isCardIdSelected == true ? "card_Id='" + txtCardID.Text.Trim() + "'" : isUserMobileSelected == true ? "userMobile='" + txtCardID.Text.Trim() + "'" : "userName='" + txtCardID.Text.Trim() + "'";
                //DataRow[] drs = finalTable.Select("card_Id='" + txtCardID.Text.Trim() + "'");
                DataRow[] drs = finalTable.Select(whereStr);
                for (int i = 0; i < drs.Length; i++)
                {
                    mydt2.ImportRow(drs[i]);
                }
                if (((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) < mydt2.Rows.Count)
                    lblSelInfo.InnerText = "本次查询结束. 共查询结果" + mydt2.Rows.Count + "条." + (mydt2.Rows.Count == 0 ? " 没有查询到任何数据，请重新选择查询条件." : "当前显示第 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1).ToString() +
                    " 至 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) + " 条");

                else
                    lblSelInfo.InnerText = "本次查询结束. 共查询结果" + mydt2.Rows.Count + "条." +
                        (mydt2.Rows.Count == 0 ? " 没有查询到任何数据，请重新选择查询条件" : " 当前显示第 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1).ToString() +
                    " 至 " + mydt2.Rows.Count + " 条");
                GridView1.DataSource = mydt2;
                GridView1.DataBind();
            }
            else
            {
                if (((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) < finalTable.Rows.Count)
                    lblSelInfo.InnerText = "本次查询结束. 共查询结果" + finalTable.Rows.Count + "条." +
                        (finalTable.Rows.Count == 0 ? " 没有查询到任何数据，请重新选择查询条件" : " 当前显示第 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1).ToString() +
                    " 至 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) + " 条");
                else
                    lblSelInfo.InnerText = "本次查询结束. 共查询结果" + finalTable.Rows.Count + "条." +
                        (finalTable.Rows.Count == 0 ? " 没有查询到任何数据，请重新选择查询条件" : " 当前显示第 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1).ToString() +
                    " 至 " + finalTable.Rows.Count + " 条");
                GridView1.DataSource = finalTable;
                GridView1.DataBind();
            }
            #endregion

            ra2.Checked = true;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        DataTable finalTable = (DataTable)Cache["finalTable"];
        GridView1.DataSource = finalTable;
        GridView1.DataBind();
        if (((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) < finalTable.Rows.Count)
            lblSelInfo.InnerText = "本次查询结束. 共查询结果" + finalTable.Rows.Count + "条. 当前显示第 " +
            ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1) +
            " 至 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) + " 条";
        else
            lblSelInfo.InnerText = "本次查询结束. 共查询结果" + finalTable.Rows.Count + "条. 当前显示第 " +
            ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1) +
            " 至 " + finalTable.Rows.Count + " 条";
    }
    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable finalTable = (DataTable)Cache["finalTable"];
        GridView1.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
        if (((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) < finalTable.Rows.Count)
            lblSelInfo.InnerText = "本次查询结束. 共查询结果" + finalTable.Rows.Count + "条. 当前显示第 " +
            ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1) +
            " 至 " + ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue)) + " 条";
        else
            lblSelInfo.InnerText = "本次查询结束. 共查询结果" + finalTable.Rows.Count + "条. 当前显示第 " +
            ((GridView1.PageIndex + 1) * Convert.ToInt32(ddlPageSize.SelectedValue) - Convert.ToInt32(ddlPageSize.SelectedValue) + 1) +
            " 至 " + finalTable.Rows.Count + " 条";
        GridView1.DataSource = finalTable;
        GridView1.DataBind();
    }

    protected void CouponList()
    {
        string filename = "";
        int icount = Convert.ToInt32(Request.QueryString["count"]);//120
        try
        {
            int myrandnum, i, j, checksum;
            int resultrandnum;
            int[] resultlist = new int[icount];
            int first4bit, end4bit;
            int startnum = (int)(so.sqlExecuteScalar("select isnull(max(MaxNum),0) from MaxNumber"));//1100250


            Random rand = new Random();

            filename = DateTime.Now.ToString("yyyyMMddHHmmssfff");//年月年时分秒毫秒

            for (i = 0; i < icount; i++)
            {
                myrandnum = startnum + i * 25 + rand.Next() % 24;

                first4bit = myrandnum / 10000;//110
                end4bit = myrandnum % 10000;//270

                checksum = (first4bit + end4bit) % 10;
                resultrandnum = myrandnum * 10 + checksum;

                resultlist[i % icount] = resultrandnum;
                if (i % icount == icount - 1)//看是不是最后一个
                {
                    if (!Directory.Exists("C:\\DMService\\" + memberStatic.clientDataBase + "\\couponFile"))
                    {
                        Directory.CreateDirectory("C:\\DMService\\" + memberStatic.clientDataBase + "\\couponFile");
                    }
                    FileStream fs = new FileStream("C:\\DMService\\" + memberStatic.clientDataBase + "\\couponFile\\" + filename + ".txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    for (j = 0; j < icount; j++)
                        sw.Write(resultlist[j].ToString().Trim().PadLeft(9, '0') + ",0,0;");
                    sw.Close();
                    fs.Close();
                }
            }

            if (ImportCoupon(25 * icount + startnum, filename))
            {
                File.Delete("C:\\DMService\\" + memberStatic.clientDataBase + "\\couponFile\\" + filename + ".txt");
                string t = so.sqlExecuteScalar("select count(isused) from lants_Coupon where isused=0").ToString();
                Response.Write("success-" + t);
            }
            else
                Response.Write("[ImportCoupon]执行失败");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.Replace("\r\n", "").Replace("'", ""));
        }
        finally { Response.End(); }
    }

    private bool ImportCoupon(int iMax, string _file)
    {
        try
        {
            ArrayList al = new ArrayList(2);
            al.Add("BULK INSERT lants_Coupon  " +
                                     "  FROM 'C:\\DMService\\" + memberStatic.clientDataBase + "\\couponFile\\" + _file + ".txt'" +
                                     " WITH " +
                                     " ( " +
                                     " FIELDTERMINATOR = ','," +
                                     " ROWTERMINATOR = ';'" +
                                     ")");

            al.Add("insert into MaxNumber(maxNum) values (" + iMax + ")");
            so.sqlExcuteNonQuery(al, true);
            return true;
        }
        catch (Exception ex)
        { throw ex; }
    }

    private void GetCouponCount()
    {
        try
        {
            lbpc.Text = so.sqlExecuteScalar("select count(isused) from lants_Coupon where isused=0").ToString();
        }
        catch (Exception ex)
        { ClientScript.RegisterClientScriptBlock(GetType(), "key1", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true); }
    }

    //protected void gvCDT_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        string selsql = "select count(tid) from couponMain where tid=" + gvCDT.Rows[e.RowIndex].Cells[0].Text;
    //        int t = Convert.ToInt32(so.sqlExecuteScalar(selsql));
    //        if (t > 0)
    //        {
    //            string update = "update couponType set displayed=1,usingState=0 where tid=" + gvCDT.Rows[e.RowIndex].Cells[0].Text;
    //            so.sqlExcuteNonQuery(update, false);
    //            ScriptManager.RegisterStartupScript(Page, this.GetType(), "sxf", "alert('此类型已被会员申请且未经使用,不能被删除.已自动归为停止发放并隐藏显示.')", true);
    //        }
    //        else
    //        {
    //            string delete = "delete from couponType where tid=" + gvCDT.Rows[e.RowIndex].Cells[0].Text;
    //            so.sqlExcuteNonQuery(delete, false);
    //            GridView1.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    { ScriptManager.RegisterStartupScript(Page, this.GetType(), "error_del", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true); }
    //}


    #region//旧版
    //protected void gvCT_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
    //        {
    //            ((LinkButton)e.Row.Cells[8].FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:delPC('" + e.Row.Cells[2].Text + "' ,'" + e.Row.Cells[0].Text + "')");
    //        }
    //    }
    //}
    #endregion

    #region//新版
    protected void gvCT_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtn = (LinkButton)e.Row.FindControl("LinkButton1");
            if (lbtn != null)
            {
                lbtn.Attributes.Add("onclick", "return confirm('您确定要删除吗！');");
                lbtn.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }

    protected void gvCT_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandName))
        {
            if (e.CommandName == "Delete")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                if (index <= -1 || index == gvCT.Rows.Count) return;
                string tid = gvCT.DataKeys[index]["tid"].ToString();
                try
                {
                    if (Delete(tid) > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(gvCT, typeof(GridView), "fuc1", @"<script>alert('此类型已被会员申请且未经使用,不能被删除.已自动归为停止发放并隐藏显示.')</script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(gvCT, typeof(GridView), "fuc2", @"<script>alert('删除成功')</script>", false);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(gvCT, typeof(GridView), "fuc2", @"<script>alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')</script>", false);
                }

            }
        }
    }
    /// <summary>
    /// 执行删除操作
    /// </summary>
    /// <param name="tid"></param>
    private int Delete(string tid)
    {
        try
        {
            string selsql = "select count(tid) from couponMain where tid=" + tid;
            int t = Convert.ToInt32(so.sqlExecuteScalar(selsql));
            if (t > 0)
            {
                string update = "update couponType set displayed=1,usingState=0 where tid=" + tid;
                so.sqlExcuteNonQuery(update, false);
                initCouponTypeTable();
                return t;

            }
            else
            {
                string delete = "delete from couponType where tid=" + tid;
                so.sqlExcuteNonQuery(delete, false);
                initCouponTypeTable();
                return 0;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);

        }

    }

    #endregion

    private void cpDetails()
    {
        string cid = Request.QueryString["cid"];
        try
        {
            DataTable finalTable = (DataTable)Cache["finalTable"];
            DataRow[] dr = finalTable.Select("cid='" + cid + "'");

            string isExpired = "false";//是否过期
            DateTime endDate = DateTime.Parse(dr[0]["endDate"].ToString());
            isExpired = endDate < DateTime.Now ? "true" : "false";

            string isConsumed = "false";//是否已经消费了
            isConsumed = dr[0]["consumed"].ToString();

            //输出顺序: 编号,详细类型,到期时间,使用日期,使用地点,对应物品,对应积分.
            string returnStr = "success:" + cid + "," + dr[0]["typeDetails"] + "," + endDate.ToString("yyyy-MM-dd hh:mm:ss") + "," + (dr[0]["consumedDate"].ToString() == "尚未使用" ? "尚未使用" : DateTime.Parse(dr[0]["consumedDate"].ToString()).ToString("yyyy-MM-dd hh:mm:ss")) + "," + dr[0]["consumedPlace"] + "," +
                dr[0]["money"] + "," + dr[0]["score"] + "," + dr[0]["userMobile"] + "," + isExpired + "," + isConsumed;
            Response.Write(returnStr);
        }
        catch (Exception ex)
        { Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }
    /// <summary>
    /// 发送优惠券短息
    /// </summary>
    private void reSend()
    {
        string cid = Request.QueryString["cid"];
        try
        {
            DataTable finalTable = (DataTable)Cache["finalTable"];
            DataRow[] dr = finalTable.Select("cid='" + cid + "'");
            string product = dr[0]["article"].ToString();//对应的礼品
            string money = dr[0]["money"].ToString();//对应的金额
            money = money.Contains("元") == false ? money + "元" : money;
            string txtMsg = (string.IsNullOrEmpty(product) == true ? "可作为" + money + "的消费劵" : "可兑换" + product + "礼品,或者作为" + money + "的消费劵");
            string sql = "insert into sendData values('" + dr[0]["userMobile"] + "','直复','尊敬的会员您好！您的卡号为" + dr[0]["card_id"] + "已超过" + dr[0]["score"] +
                            "积分," + txtMsg + ",请于" + Convert.ToDateTime(dr[0]["endDate"]).ToString("yyyy-MM-dd") + "前凭此条短信和优惠券号["
                            + cid + "]到指定专卖店申请兑换。详询0755-86621896.','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',0," + 1 + ")";
            SQL_Operate so = new SQL_Operate();
            so.sqlExcuteNonQuery(sql, false);
            Response.Write("success");
        }
        catch (Exception ex)
        { Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { Response.End(); }
    }
    /// <summary>
    /// 兑换优惠券
    /// </summary>
    private void Exchange()
    {
        /*先判断是不是过期了*/
        //update couponMain set consumed=1,consumedPlace='网站兑换',consumedDate=GETDATE() where cid='001118589' and endDate>getDate()
        string cid = Request.QueryString["cid"];
        try
        {
            DataTable finalTable = (DataTable)Cache["finalTable"];
            DataRow[] dr = finalTable.Select("cid='" + cid + "'");

            float score = float.Parse(dr[0]["score"].ToString());//所需积分
            string card_Id = dr[0]["card_Id"].ToString();//vip卡号
            string money = dr[0]["money"].ToString();//面值
            string userName = dr[0]["userName"].ToString();//vip姓名
            string mobile = dr[0]["userMobile"].ToString();//手机号码
            SQL_Operate so = new SQL_Operate();

            //更新优惠券的使用状态
            if (so.sqlExcuteNonQueryInt(string.Format("update couponMain set consumed=1,deduction=1,consumedPlace='网站兑换',consumedDate=GETDATE() where cid='{0}' and endDate>getDate() and deduction=0 and consumed=0", cid)) < 0)
            {
                Response.Write("fail");
                Response.End();
                return;
            }
            //扣除积分
            if (so.sqlExcuteNonQueryInt(string.Format("update cardInfo set points=points-{0} where card_Id='{1}'", score, card_Id)) < 0)
            {
                Response.Write("fail");
                Response.End();
                return;
            }


            object currentPoints = so.sqlExecuteScalar(string.Format("select points from dbo.cardInfo where card_Id='{0}'", card_Id));//获取当前积分
            //兑换成功之后发短信通知vip客户
            //编辑短信内容
            string msgContent = string.Format("尊敬的顾客您好,您的卡号为：{0},编号为：{1}的优惠券兑换成功，面值{2}，您当前积分：{3}。", card_Id, cid, money, currentPoints.ToString());
            int pri = (int)new SQL_Operate().sqlExecuteScalar(SqlStrHelper.GPRISqlStr("兑换"));
            so.sqlExcuteNonQuery(SqlStrHelper.ASendDataSqlStr(mobile, "兑换", msgContent, pri), false);//将短信信息插入到数据库中准备发送给客户，是用来通知客户优惠券兑换失败的！

            //将积分更新的结果反映到erp软件中去
            string sql = string.Format("select points from dbo.cardinfo where card_Id='{0}'", card_Id);
            string points = so.sqlExecuteScalar(sql).ToString();

            DateTime newDT = new DateTime(2010, 1, 1);
            TimeSpan sp;
            double li;
            byte[] tt;
            // string sql;
            DataTable dt;
            /*以下是更新vip积分*/
            sp = DateTime.Now - newDT;
            li = sp.TotalMilliseconds;
            tt = Encoding.Default.GetBytes("points" + (char)22 + points + (char)22 + card_Id);//这个是发送数据的格式最后一行要跟卡号
            sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
               + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
            dt = new DataTable();
            dt.Columns.Add("data", typeof(string));
            dt.Columns.Add("value", typeof(object));
            dt.Columns.Add("type", typeof(string));
            dt.Rows.Add(new object[] { "@b", tt, "binary" });
            so.sqlExcuteNonQuery(sql, dt);
            Response.Write("success");

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.Replace("\r\n", "").Replace("'", ""));
        }
        finally
        {
            Response.End();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            e.Row.Cells[0].Attributes.Add("onclick", "selStation('blockCity','" + e.Row.Cells[1].Text + "',event)");//加个event就能实现跨浏览器
    }
    protected void gvCT_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}