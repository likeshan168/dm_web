using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    DESencrypt des = new DESencrypt();
    SQL_Operate so = new SQL_Operate();
    authority athy = new authority();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                initDataBase ins = new initDataBase();
                initAthy ina = new initAthy();
                //初始化用户表
                ins.initClient();
                //初始化用户权限表
                ina.createAthy();
            }
            catch (Exception ex)
            { ClientScript.RegisterClientScriptBlock(GetType(), "exx", "<script>alert('" + ex.Message.Replace("'","").Replace("\r\n","") + "');</script>"); }
        }
        txtName.Focus();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            

            DataTable mydt = new DataTable();
            mydt.Columns.Add("filed", typeof(string));
            mydt.Columns.Add("values", typeof(string));
            mydt.Columns.Add("types", typeof(string));
            mydt.Rows.Add(new object[] { "@name", txtName.Text.Trim(), "varchar" });
            mydt.Rows.Add(new object[] { "@pwd", des.Encrypt(txtPWD.Text.Trim(), "empoxdes"), "varchar" });
            int i = (int)so.sqlExecuteScalar("select count(*) from clientInfo where cid=@name and password=@pwd", mydt);
            if (i == 1)
            {
                Session["userName"] = so.sqlExecuteScalar("select COMname from clientInfo where cid='" + txtName.Text + "'");//"埃迪蒙托"
                try
                {
                    Session["vipCount"] = so.sqlExecuteScalar("select count(card_id) from cardInfo");//会员的数目：47576
                }
                catch { Session["vipCount"] = 0; }
                //so.sqlExcuteNonQuery("update clientInfo set loginCount=loginCount+1,lastLoginTime='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' where cid='" + txtName.Text + "'", false);
                so.sqlExcuteNonQuery("update clientInfo set loginCount=loginCount+1,lastLoginTime=GETDATE() where cid='" + txtName.Text + "'", false);
                Session["athy"] = athy.authorityTable(txtName.Text.Trim());//操作权限表
                Session["LoginState"] = "login";
                Session["LoginID"] = txtName.Text.Trim();//Admin
                //Response.Redirect("Main.aspx");
                //Server.Transfer("~/Main.aspx");
                string lastUrl = Request.QueryString["ReturnUrl"];
                if (lastUrl != null)
                {
                    ClientScript.RegisterStartupScript(GetType(), "succ", "location.href='" + lastUrl + "'", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "succ", "location.href='Main.aspx'", true);
                }
               
            }
            else
                ClientScript.RegisterStartupScript(GetType(), "log", "<script>alert('用户名或密码错误!登录失败');</script>");
        }
        catch (Exception ex)
        { ClientScript.RegisterStartupScript(GetType(), "ext", "<script>alert('" + ex.Message.Replace("'", "").Replace("\r\n", "") + "');</script>"); }
    }
}
