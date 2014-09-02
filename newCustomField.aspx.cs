using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Policy;
using System.Web.Caching;

public partial class newCustomField : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();
    private DataTable mydt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            init_ddlField();
        if (Request.QueryString["mtype"] == "nameCheck")
            nameCheck();
        if (Request.QueryString["mtype"] == "addField")
            addNewField();
        if (Request.QueryString["mtype"] == "addFormulaField")
            addNewFormulaField();
    }

    //protected void btnOK_Click(object sender, EventArgs e)
    //{
    //    if (txtFiledType.Text == "日期")
    //    {
    //        DateTime m = DateTime.MinValue;
    //        if (txtFieldDefault.Text.Trim() != "无" && !DateTime.TryParse(txtFieldDefault.Text.Trim(), out m))
    //        {
    //            ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('初始值不是日期类型如yyyy-MM-dd,请重新输入!');</script>");
    //            return;
    //        }
    //    }
    //    scon = new SqlConnection(sqlConStr);
    //    scon.Open();
    //    tran = scon.BeginTransaction();
    //    try
    //    {
    //        string insSql = "";
    //        if (txtFieldDefault.Text.Trim() == "无")
    //            insSql = "insert into DataInfo values('" + Session["DBname"].ToString() + "','" + txtFiledName.Text.Trim() + "','" + txtFieldCNname.Text.Trim() +
    //                "','" + txtFiledType.Text + "'," + txtFieldLenth.Text.Trim() + ",null,0,0,0,0,0,1,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
    //        else
    //            insSql = "insert into DataInfo values('" + Session["DBname"].ToString() + "','" + txtFiledName.Text.Trim() + "','" + txtFieldCNname.Text.Trim() +
    //                "','" + txtFiledType.Text + "'," + txtFieldLenth.Text.Trim() + ",'" + txtFieldDefault.Text.Trim() + "',0,0,0,0,0,1,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
    //        scom = new SqlCommand(insSql, scon);
    //        scom.Transaction = tran;
    //        scom.ExecuteNonQuery();
    //        string alterSql = "use " + Session["DBname"].ToString() + " alter table cardInfo add [" + txtFiledName.Text.Trim() + "]";
    //        switch (txtFiledType.Text)
    //        {
    //            case "URL":
    //            case "百分比":
    //                alterSql += " varchar(" + txtFieldLenth.Text.Trim() + ")";
    //                if (txtFieldDefault.Text != "无" && txtFieldDefault.Text.IndexOf('%') < 0)
    //                    alterSql += " default '" + txtFieldDefault.Text.Trim() + "%'";
    //                else if (txtFieldDefault.Text != "无" && txtFieldDefault.Text.IndexOf('%') > 0)
    //                    alterSql += " default '" + txtFieldDefault.Text.Trim() + "'";
    //                break;
    //            case "电话":
    //            case "传真":
    //            case "电子邮件":
    //            case "邮编":
    //            case "选项列表":
    //            case "文本": alterSql += " varchar(" + txtFieldLenth.Text.Trim() + ")";
    //                break;
    //            case "日期": alterSql += " datetime";
    //                break;
    //            case "整数": alterSql += " int";
    //                break;
    //            case "币种":
    //            case "数字": alterSql += " float";
    //                break;
    //            case "复选框": alterSql += " bit";
    //                break;
    //        }
    //        if (txtFiledType.Text != "百分比")
    //        {
    //            if (txtFieldDefault.Text.Trim() != "无")
    //            {
    //                if (txtFiledType.Text == "整数" || txtFiledType.Text == "数字" || txtFiledType.Text == "币种" || txtFiledType.Text == "复选框")
    //                    alterSql += " default " + txtFieldDefault.Text.Trim() + "";
    //                else
    //                    alterSql += " default '" + txtFieldDefault.Text.Trim() + "'";
    //            }
    //        }
    //        if (notAlowNull.Checked)
    //            alterSql += " not null";
    //        scom.CommandText = alterSql;
    //        scom.Transaction = tran;
    //        scom.ExecuteNonQuery();
    //        tran.Commit();
    //        ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('自定义字段新增成功!')</script>");
    //    }
    //    catch (Exception ex)
    //    {
    //        tran.Rollback();
    //        ClientScript.RegisterStartupScript(GetType(), "s", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "") + "');</script>");
    //    }
    //    finally
    //    {
    //        if (scon.State == ConnectionState.Open)
    //            scon.Close();
    //    }
    //}

    protected void init_ddlField()
    {
        try
        {
            ddlField.Items.Clear();
            string sql = "select ENname,CNname from DataInfo where type not like '公式(%)'";

            if (Cache["initddlFeield"] == null)
            {
                Cache.Insert("initddlFeield", so.sqlExcuteQueryTable(sql), null, DateTime.Now.AddMinutes(60), TimeSpan.Zero, CacheItemPriority.Normal, null);

            }
            mydt = (DataTable)Cache["initddlFeield"];
            foreach (DataRow dr in mydt.Rows)
            {
                ListItem li = new ListItem();

                if (string.IsNullOrEmpty(dr["CNname"].ToString()))
                {
                    li.Text = dr["ENname"].ToString();
                }
                else
                {
                    li.Text = dr["CNname"].ToString();
                }
                li.Value = dr["ENname"].ToString();
                ddlField.Items.Add(li);
            }
        }
        catch (Exception ex)
        { ClientScript.RegisterClientScriptBlock(GetType(), "fieldError", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true); }
    }
    /// <summary>
    /// 验证字段名是否存在
    /// </summary>
    protected void nameCheck()
    {
        try
        {
            #region//原始代码
            //string sql = "select count(ENname) from DataInfo where ENname='" + Request.QueryString["name"] + "'";
            //if (Convert.ToInt16(so.sqlExecuteScalar(sql)) != 0)
            //    Response.Write("used");
            //else
            //    Response.Write("success");
            #endregion
            #region//修改之后的代码
            DataRow[] dr = mydt.Select("ENname='" + Request.QueryString["name"] + "'");
            if (dr == null || dr.Length == 0)
            {
                Response.Write("success");
            }
            else
            {
                Response.Write("used");
            }
            #endregion
        }
        catch (Exception ex)
        { Response.Write(ex.Message.Replace("'", "").Replace("\r\n", "")); }
        finally
        { Response.End(); }
    }
    /// <summary>
    /// 新增字段
    /// </summary>
    protected void addNewField()
    {
        try
        {
            //string cname = Server.UrlDecode(Request.QueryString["cname"]);
            string cname = Uri.UnescapeDataString(Request.QueryString["cname"]);
            string name = Request.QueryString["name"];
            //string type = Server.UrlDecode(Request.QueryString["type"]);
            string type = Uri.UnescapeDataString(Request.QueryString["type"]);
            string length = Request.QueryString["length"] == null ? "1" : Request.QueryString["length"];
            //string defaultVal = Server.UrlDecode(Request.QueryString["default"]);
            string defaultVal = Uri.UnescapeDataString(Request.QueryString["default"]);
            defaultVal = defaultVal == "无" ? null : defaultVal;
            if (type == "日期")
            {
                DateTime m = DateTime.MinValue;
                if (defaultVal != "无" && !DateTime.TryParse(defaultVal, out m))
                {
                    Response.Write("初始值不是日期类型如yyyy-MM-dd,请重新输入!");
                    return;
                }
            }

            ArrayList sqlAl = new ArrayList();
            sqlAl.Add("insert into DataInfo values('" + name + "','" + cname +
                "','" + type + "'," + length + ",'" + defaultVal + "',0,0,0,1,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')");

            string alterSql = "alter table UD_Fileds add [" + name + "]";
            switch (type)
            {
                case "URL":
                case "电话":
                case "传真":
                case "电子邮件":
                case "邮编":
                case "文本": alterSql += " varchar(" + length + ")";
                    break;
                case "日期": alterSql += " datetime";
                    break;
                case "整数": alterSql += " int";
                    break;
                case "币种":
                case "数字": alterSql += " float";
                    break;
                case "复选框": alterSql += " bit";
                    break;
            }
            if (defaultVal != null)
            {
                if (type == "整数" || type == "数字" || type == "币种")
                    alterSql += " default " + defaultVal + " not null";
                else
                    alterSql += " default '" + defaultVal + "' not null";
            }
            sqlAl.Add(alterSql);
            so.sqlExcuteNonQuery(sqlAl, true);
            Response.Write("success");
        }
        catch (Exception ex)
        { Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally
        { Response.End(); }
    }

    protected void addNewFormulaField()
    {
        //string cname = Server.UrlDecode(Request.QueryString["cname"]);
        string cname = Uri.UnescapeDataString(Request.QueryString["cname"]);
        string name = Request.QueryString["name"];
        string type = Request.QueryString["type"];
        string mainText = Request.QueryString["mainText"];
        Response.Write("no");
        Response.End();
    }
}