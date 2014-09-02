using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class alterPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblNewPWD.Visible = false;
        lblTrueNewPWD.Visible = false;
        txtNewPWD.Visible = false;
        txtTrueNewPWD.Visible = false;
        btnSubmit.Visible = false;
        lblText.Text = "";
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try 
        {
            DESencrypt des = new DESencrypt();
            SQL_Operate so = new SQL_Operate();
            string primitivePWD = des.Encrypt(txtPrimitivePWD.Text.Trim(), "empoxdes");
            DataTable dt = new DataTable();
            dt.Columns.Add("filed", typeof(string));
            dt.Columns.Add("values", typeof(string));
            dt.Columns.Add("types", typeof(string));
            dt.Rows.Add(new object[] { "@name", Session["LoginID"].ToString(), "varchar" });
            dt.Rows.Add(new object[] { "@pwd", primitivePWD, "varchar" });
            int i = (int)so.sqlExecuteScalar("select count('神马') from clientInfo where password=@pwd and cid=@name", dt);
            if (i == 1)
            {
               
                lblText.Text = "<font color=green>输入正确！</font>";
                lblPrimitivePWD.Visible = false;
                txtPrimitivePWD.Visible = false;
                btnOK.Visible = false;
                lblNewPWD.Visible = true;
                lblTrueNewPWD.Visible = true;
                txtNewPWD.Visible = true;
                txtTrueNewPWD.Visible = true;
                btnSubmit.Visible = true;
                btnSubmit.Enabled = true;
            }
            else
            {
                lblText.Text = "<font color=red>输入错误！</font>";
                btnSubmit.Enabled = false;
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        DESencrypt des = new DESencrypt();
        SQL_Operate so = new SQL_Operate();
        string newPWD = des.Encrypt(txtNewPWD.Text.Trim(), "empoxdes");
        string logName = Session["LoginID"].ToString().Trim();
        int i = (int)so.sqlExcuteNonQueryInt("update  clientInfo set password = '" + newPWD + "' where cid = '" + logName+"'");
        if (i == 1)
        {
            Response.Redirect("Main.aspx");
            //Response.Write("<script>window.alert('修改成功！');location.href='Default.aspx';</script>");
        }
    }
   
}