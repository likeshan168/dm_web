using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class DM_VIPCard_Type_Managment : System.Web.UI.Page
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
            string sql = "select TypeID,TypeName,Discount,Remark from VipCardTypeInfo";
            DataTable mydt = so.sqlExcuteQueryTable(sql);

            mydt = so.sqlExcuteQueryTable(sql);

            Cache["dataSource"] = mydt;
            gvAccount.DataSource = mydt;
            gvAccount.DataBind();
        }
        catch
        { ClientScript.RegisterClientScriptBlock(GetType(), "error_1", "alert('卡类型信息读取错误.请稍后再试.')", true); }
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
        string sql = "update VipCardTypeInfo set TypeName='" + e.NewValues[0].ToString() + "'Discount=" + float.Parse(e.NewValues[1].ToString()) + ",Remark='" + e.NewValues[2].ToString() + "' where TypeID='" + gvAccount.Rows[e.RowIndex].Cells[0].Text + "'";
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

            string typeName = txtUid.Text.Trim();
            string remark = txtPwd.Text.Trim();
            //float discount = float.Parse(txtDiscount.Text.Trim());
            if (string.IsNullOrEmpty(typeName))
            {
                ScriptManager.RegisterClientScriptBlock(up2, GetType(), "typeNameNull", "alert('卡类型名不能为空！')", true);
                return;
            }

            int t = Convert.ToInt16(so.sqlExecuteScalar("select count(*) from VipCardTypeInfo where TypeName='" + typeName + "'"));
            if (t == 1)
            {
                ScriptManager.RegisterClientScriptBlock(up2, GetType(), "tips1", "alert('该卡类型名已存在.')", true);
                return;
            }

            if (string.IsNullOrEmpty(txtDiscount.Text.Trim()))
            {
                ScriptManager.RegisterClientScriptBlock(up2, GetType(), "discountNull", "alert('卡折扣不能为空！')", true);
                return;
            }
            float discount = float.Parse(txtDiscount.Text.Trim());


            string sql = "insert into dbo.VipCardTypeInfo(TypeName,Discount,Remark) values('" + typeName + "'," + discount + ",'" + remark + "')";

            so.sqlExcuteNonQuery(sql, false);
            initGVAccount();
            ScriptManager.RegisterClientScriptBlock(up2, GetType(), "tips2", "alert('新卡类型名称建立完成.')", true);
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
            string sql = "delete from VipCardTypeInfo where TypeID='" + gvAccount.Rows[index].Cells[0].Text + "'";
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