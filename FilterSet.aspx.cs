using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Caching;

public partial class FilterSet : System.Web.UI.Page
{
    private DataTable dt_gv1_datasource;
    private DataTable dt_gv2_datasource;
    SQL_Operate so = new SQL_Operate();
    LogModule lm = new LogModule();
    //private SqlCacheDependency sqldep = null;
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Server.Transfer("Main.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["LoginState"] != null)
            {
                this.bind();
                this.gvBind();
            }
            else
            {
                //Response.Redirect("Login.aspx");
                Server.Transfer("~/Login.aspx");
            }

        }
    }

    /// <summary>
    /// 绑定gridview1的数据源
    /// </summary>
    public void bind()
    {
        try
        {
            dt_gv1_datasource = so.sqlExcuteQueryTable("select * from DataInfo where ENname <> 'is_Upload'");
            //
            //TODO:这里有效率的问题，应该使用cache而不要使用session
            //
            //sqldep = new SqlCacheDependency("DM_TEST", "DataInfo");
            //if (Cache["myds"] == null)
            //{
            //    Cache.Insert("myds", so.sqlExcuteQueryTable("select * from DataInfo where ENname <> 'is_Upload'"), sqldep, DateTime.Now.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Normal, null
            //        );

            //}
            if (Cache["myds"] != null) Cache.Remove("myds");
            Cache["myds"] = dt_gv1_datasource;
            //Session["myds"] = dt_gv1_datasource;
            GridView1.DataSource = dt_gv1_datasource;
            GridView1.DataKeyNames = new string[] { "ENname" };
            GridView1.DataBind();
        }
        catch
        {
            ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('没有找到对应数据库.请先进入数据中转服务下载VIP资料');</script>");
        }
    }

    /// <summary>
    /// 绑定gridview2的数据源
    /// </summary>
    public void gvBind()
    {
        try
        {
            dt_gv2_datasource = so.sqlExcuteQueryTable("select * from DataInfo where Udefined = 1 and ENname <> 'is_Upload' and ENname <> 'empox_favor'");
            DataTable mydt = new DataTable();
            if (Cache["myds2"] != null) Cache.Remove("myds2");
            Cache["myds2"] = dt_gv2_datasource;
            //Session["myds2"] = dt_gv2_datasource;
            mydt = dt_gv2_datasource.Copy();
            if (mydt.Rows.Count == 0)
            {
                mydt.Rows.Add(mydt.NewRow());
                GridView2.DataSource = mydt;
                GridView2.DataBind();
                //int columnCount = GridView2.Rows[0].Cells.Count;//列数
                int columnCount = GridView2.Columns.Count;
                GridView2.Rows[0].Cells.Clear();
                GridView2.Rows[0].Cells.Add(new TableCell());
                GridView2.Rows[0].Cells[0].ColumnSpan = columnCount;
                GridView2.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                GridView2.Rows[0].Cells[0].Text = "暂无自定义字段!<a href='#' onclick=\"javascript:window.location='newCustomField.aspx'\">点此添加一个新字段</a>";

            }
            else
            {
                GridView2.DataSource = dt_gv2_datasource;
                GridView2.DataKeyNames = new string[] { "ENname" };
                GridView2.DataBind();
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "');</script>");
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView1.EditIndex = e.NewEditIndex;
        bind();
    }

    private byte[] stringTobyte(string s)
    {
        ArrayList al = new ArrayList();
        for (int i = 0; i < s.Length; i++)
        {
            al.Add((byte)s[i]);
        }
        return (byte[])al.ToArray(System.Type.GetType("System.Byte"));
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        bind();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //dt_gv1_datasource = (DataTable)Session["myds"];
        dt_gv1_datasource = (DataTable)Cache["myds"];
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = dt_gv1_datasource;
        GridView1.DataBind();
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //dt_gv2_datasource = (DataTable)Session["myds2"];
        dt_gv2_datasource = (DataTable)Cache["myds2"];
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataSource = dt_gv2_datasource;
        GridView2.DataBind();
    }
    protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView2.EditIndex = -1;
        gvBind();
    }
    protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView2.EditIndex = e.NewEditIndex;
        gvBind();
    }
    #region//以前版
    //protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        ArrayList al = new ArrayList();
    //        al.Add("delete from DataInfo where ENname='" + GridView2.Rows[e.RowIndex].Cells[0].Text + "'");
    //        //int t=(int)so.sqlExecuteScalar("select count(autoId) from TempStor where data ='0x31' and flag=0 and companyid='" + memberStatic.companyID + "'");
    //        //if (t == 0)
    //        //{
    //        //    al.Add("insert into tempstor values('','0x31',0,'','" + memberStatic.companyID + "',null)");
    //        //}
    //        al.Add("DECLARE @tablename VARCHAR(100), @columnname VARCHAR(100), @tab VARCHAR(100) SET @tablename='UD_Fileds' SET @columnname='" +
    //            GridView2.Rows[e.RowIndex].Cells[0].Text + "' declare @defname varchar(100) declare @cmd varchar(100) select @defname = name FROM " +
    //            "sysobjects so JOIN sysconstraints sc ON so.id = sc.constid WHERE object_name(so.parent_obj) = @tablename AND so.xtype = 'D'AND " +
    //            "sc.colid =(SELECT colid FROM syscolumns WHERE id = object_id(@tablename) AND name = @columnname) " +
    //            "select @cmd='alter table '+ @tablename+ ' drop constraint '+ @defname exec (@cmd)");
    //        al.Add("IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_UD_Fileds_" + GridView2.Rows[e.RowIndex].Cells[0].Text + "' AND type  = 'TR') DROP TRIGGER T_UD_Fileds_" + GridView2.Rows[e.RowIndex].Cells[0].Text + "");
    //        al.Add("alter table UD_Fileds drop column " + GridView2.Rows[e.RowIndex].Cells[0].Text);
    //        so.sqlExcuteNonQuery(al, true);
    //        //记录删除字段操作
    //        //lm.InsLog(Session["LoginID"].ToString(), "删除字段[" + GridView2.Rows[e.RowIndex].Cells[0].Text + "],状态:成功");
    //        GridView2.EditIndex = -1;
    //        this.gvBind();
    //        this.bind();
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')</script>");
    //    }
    //}
    #endregion
    protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            TextBox mytb;
            mytb = (TextBox)e.Row.Cells[1].Controls[0];
            mytb.Width = Unit.Pixel(60);
            TextBox mytb2;
            mytb2 = (TextBox)e.Row.Cells[2].Controls[0];
            mytb2.Width = Unit.Pixel(80);
        }
    }

    //protected void winSend(object sender, EventArgs e)
    //{
    //    //ClientScript.RegisterStartupScript(GetType(), "s", "<script>window.location='newCustomField.aspx'</script>");
    //    Server.Transfer("~/newCustomField.aspx");
    //}

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            TextBox mytb;
            mytb = (TextBox)e.Row.Cells[1].Controls[0];
            mytb.Width = Unit.Pixel(80);
            TextBox mytb2;
            mytb2 = (TextBox)e.Row.Cells[2].Controls[0];
            mytb2.Width = Unit.Pixel(60);
        }
    }

    //protected void savetoserver_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string selsql = "select count(autoId) from TempStor where data ='0x31' and flag=0 and companyid='" + memberStatic.companyID + "'";
    //        int t = (int)so.sqlExecuteScalar(selsql);
    //        if (t == 0)
    //        {
    //            DateTime newDT = new DateTime(2010, 1, 1);
    //            TimeSpan sp = DateTime.Now - newDT;
    //            double li = sp.TotalMilliseconds;
    //            so.sqlExcuteNonQuery("insert into tempstor values('" + li + "','0x31',0,'','" + memberStatic.companyID + "',null)", false);
    //        }
    //        ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('同步完成')</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')</script>");
    //    }
    //}

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList al = new ArrayList();
            string sqlstr = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                sqlstr = "update DataInfo set Displayd='"
                    + (((CheckBox)(GridView1.Rows[i].Cells[3].Controls[1])).Checked ? "1" : "0").ToString() + "',isCondition='"
                    + (((CheckBox)(GridView1.Rows[i].Cells[4].Controls[1])).Checked ? "1" : "0").ToString() + "' where ENname='" +
                    GridView1.Rows[i].Cells[0].Text + "'";
                al.Add(sqlstr);
            }
            so.sqlExcuteNonQuery(al, true);
            GridView1.EditIndex = -1;
            bind();
            //string selsql = "select count(autoId) from TempStor where data ='0x31' and flag=0 and companyid='" + memberStatic.companyID + "'";
            //int t = (int)so.sqlExecuteScalar(selsql);
            //if (t == 0)
            //{
            //    DateTime newDT = new DateTime(2010, 1, 1);
            //    TimeSpan sp = DateTime.Now - newDT;
            //    double li = sp.TotalMilliseconds;
            //    so.sqlExcuteNonQuery("insert into tempstor values('" + li + "','0x31',0,'','" + memberStatic.companyID + "',null)", false);
            //}
            ClientScript.RegisterStartupScript(GetType(), "suc", "<script>alert('修改成功!')</script>");
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "err", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')</script>");
        }
    }
    #region//新修改版的
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtn = (LinkButton)e.Row.FindControl("del");
            if (lbtn != null)
            {
                lbtn.Attributes.Add("onclick", "return confirm('确定要删除吗！');");
                lbtn.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandName))
        {
            if (e.CommandName == "del")
            {
                int index = int.Parse(e.CommandArgument.ToString());
                if (index <= -1 && index == GridView2.Rows.Count) return;
                string ENname = GridView2.DataKeys[index]["ENname"].ToString();
                Delete(ENname);
            }
        }
    }
    private void Delete(string ENname)
    {
        try
        {
            ArrayList al = new ArrayList();
            al.Add("delete from DataInfo where ENname='" + ENname + "'");
            al.Add("DECLARE @tablename VARCHAR(100), @columnname VARCHAR(100), @tab VARCHAR(100) SET @tablename='UD_Fileds' SET @columnname='" +
                ENname + "' declare @defname varchar(100) declare @cmd varchar(100) select @defname = name FROM " +
                "sysobjects so JOIN sysconstraints sc ON so.id = sc.constid WHERE object_name(so.parent_obj) = @tablename AND so.xtype = 'D'AND " +
                "sc.colid =(SELECT colid FROM syscolumns WHERE id = object_id(@tablename) AND name = @columnname) " +
                "select @cmd='alter table '+ @tablename+ ' drop constraint '+ @defname exec (@cmd)");
            al.Add("IF EXISTS (SELECT name FROM sysobjects WHERE name = 'T_UD_Fileds_" + ENname + "' AND type  = 'TR') DROP TRIGGER T_UD_Fileds_" + ENname + "");
            al.Add("alter table UD_Fileds drop column " + ENname);
            so.sqlExcuteNonQuery(al, true);
            this.gvBind();
            this.bind();
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')</script>");
        }
    }
    #endregion
}
