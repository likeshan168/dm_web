using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RBKJEmpox_Management_System.Models;
using System.Reflection;
public partial class LoginSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["VipName"] == null)//如果其中的一个为空就重新进行登录
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "tip1", "location.href='VipClientLogin.aspx'", true);
                return;
            }
        }
    }
}