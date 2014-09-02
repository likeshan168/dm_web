using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ErrorPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Application["error"] != null)
                lblErrorText.Text = Application["error"].ToString();
        }
        catch (Exception ex)
        {
            lblErrorText.Text = ex.Message;
        }
    }
}