using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
///vipInterface 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class vipInterface : System.Web.Services.WebService {

    vipInterfaceApp va;
    public vipInterface () {
        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        va = new vipInterfaceApp();
    }

    [WebMethod(Description = "VIP用户登录验证")]
    public string vipLogin(string xml)
    {
        return va.vipLogin(xml);
    }

    [WebMethod(Description = "VIP详细资料获取")]
    public string vipDetails(string xml)
    {
        return va.vipDetails(xml);
    }

    [WebMethod(Description = "用户详细资料修改")]
    public string alterVipDetails(string xml)
    {
        return va.alterVipDetails(xml);
    }

    [WebMethod(Description = "修改密码")]
    public string alterVipPwd(string xml)
    {
        return va.alterVipPwd(xml);
    }

    [WebMethod(Description = "积分换礼类别获取")]
    public string getCouponType()
    {
        return va.getCouponType();
    }

    [WebMethod(Description = "积分换礼下单")]
    public string couponConsume(string xml)
    {
        return va.couponConsume(xml);
    }
}
