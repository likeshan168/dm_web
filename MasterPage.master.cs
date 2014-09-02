using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginState"] == null || Session["LoginState"].ToString() != "login")
            Response.Redirect("Login.aspx");
    }
    protected void lbExit_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        //Response.Redirect("Login.aspx");
        Server.Transfer("~/Login.aspx",false);
    }
}
