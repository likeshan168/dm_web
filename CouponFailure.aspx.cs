using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CouponFailure : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["VipName"] == null)//如果没有登录要先登录
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "tip1", "location.href='VipClientLogin.aspx'", true);
                return;
            }
        }
    }
}