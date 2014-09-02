using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        couponService cou = new couponService();
        string xml = "<?xml version='1.0' encoding='gb2312'?><data proposer=\"一秒通测试\" vip=\"99999\" companyId=\"01043\" clientId=\"118\" IP=\"pos申请优惠券\" aDate=\"2013-01-15 17:48:12\" money=\"100\" point=\"1000\" ></data>";
        string result = cou.GetNewPCwithPOS(xml);
        Response.Write(result);
    }
}