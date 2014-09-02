using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class userManage : System.Web.UI.Page
{
    SQL_Operate so = new SQL_Operate();
    DESencrypt des = new DESencrypt();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            initGVAccount();
    }

    private void initGVAccount()
    {
        try
        {
            string sql = "select cid,password,loginCount,lastLoginTime from clientInfo";
            DataTable mydt = so.sqlExcuteQueryTable(sql);

            mydt = so.sqlExcuteQueryTable(sql);
            //将密码字段进行DES解密便于呈现
            foreach (DataRow dr in mydt.Rows)
            {
                dr["password"] = des.Decrypt(dr["password"].ToString(), "empoxdes");
            }
            Cache["dataSource"] = mydt;
            gvAccount.DataSource = mydt;
            gvAccount.DataBind();
        }
        catch
        { ClientScript.RegisterClientScriptBlock(GetType(), "error_1", "alert('用户信息读取错误.请稍后再试.')", true); }
    }
    protected void gvAccount_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvAccount.EditIndex = e.NewEditIndex;
        gvAccount.DataSource = Cache["dataSource"];
        gvAccount.DataBind();
    }
    protected void gvAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //string sql = "delete from clientInfo where cid='" + gvAccount.Rows[e.RowIndex].Cells[0].Text + "'";
        //so.sqlExcuteNonQuery(sql, false);
        //initGVAccount();
    }

    protected void gvAccount_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string sql = "update clientInfo set password='" + des.Encrypt(e.NewValues[0].ToString(), "empoxdes") + "' where cid='" + gvAccount.Rows[e.RowIndex].Cells[0].Text + "'";
        so.sqlExcuteNonQuery(sql, false);
        gvAccount.EditIndex = -1;
        initGVAccount();
    }
    protected void gvAccount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvAccount.EditIndex = -1;
        gvAccount.DataSource = Cache["dataSource"];

        gvAccount.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string userName = txtUid.Text.Trim();
            string password = txtPwd.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ScriptManager.RegisterClientScriptBlock(up2, GetType(), "nullmsg", "alert('请将信息填写完整.')", true);
                return;
            }

            int t = Convert.ToInt16(so.sqlExecuteScalar("select count(cid) from clientInfo where cid='" + userName + "'"));
            if (t == 1)
            {
                ScriptManager.RegisterClientScriptBlock(up2, GetType(), "tips1", "alert('该账户名已存在.')", true);
                return;
            }
            string comid = so.sqlExecuteScalar("select companyID from clientInfo where cid='Admin'").ToString();
            string sql = "insert into clientInfo(cid,password,COMname,companyID,loginCount) values('" + userName + "','" +
                des.Encrypt(password, "empoxdes") + "','" + Session["userName"].ToString() + "','" + comid + "',0)";
            so.sqlExcuteNonQuery(sql, false);
            initGVAccount();
            ScriptManager.RegisterClientScriptBlock(up2, GetType(), "tips2", "alert('新用户建立完成.')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(up2, GetType(), "tips3", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true);
        }
    }
    protected void gvAccount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = Int32.Parse(e.CommandArgument.ToString());
            if (index <= -1 || index == gvAccount.Rows.Count) return;
            string sql = "delete from clientInfo where cid='" + gvAccount.Rows[index].Cells[0].Text + "'";
            try
            {
                so.sqlExcuteNonQuery(sql, false);
                ScriptManager.RegisterClientScriptBlock(up1, GetType(), "delError", "alert('删除成功！')", true);
                initGVAccount();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(up1, GetType(), "delError", "alert('" + ex.Message.Replace("\r\n", "").Replace("'", "") + "')", true);
            }
        }
    }
    protected void gvAccount_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Alternate || e.Row.RowState == DataControlRowState.Normal)
            {
                ImageButton img = (ImageButton)e.Row.FindControl("del");
                if (img != null)
                {
                    img.CommandArgument = e.Row.RowIndex.ToString();
                }
            }
        }
    }
}