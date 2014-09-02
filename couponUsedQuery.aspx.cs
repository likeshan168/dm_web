using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class couponUsedQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        txtBdate1.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        txtBdate2.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        txtEdate1.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
        txtEdate2.Attributes.Add("onclick", "new Calendar('2011', '2100', 0,'yyyy-MM-dd').show(this);");
    }
}