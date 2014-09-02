using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Data;
/// <summary>
///couponService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ToolboxItem(false)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class couponService : System.Web.Services.WebService {
    couponApplication ca;
    public couponService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
        ca = new couponApplication();
    }
    /// <summary>
    /// 单次申请优惠劵编号
    /// </summary>
    /// <param name="xmlStr"></param>
    /// <returns></returns>
    [WebMethod(Description = "单次申请优惠券编号")]
    public string GetNewPCwithTid(string xmlStr)
    {
        return ca.GetNewPCwithTID(xmlStr);
    }

    [WebMethod(Description = "批量申请优惠券编号")]
    public string GetAllPCwithTID(string sql, string xmlStr)
    {
        return ca.GetAllPCwithTID(sql, xmlStr);
    }

    [WebMethod(Description = "POS申请优惠券编号")]
    public string GetNewPCwithPOS(string xmlStr)
    {
        return ca.GetNewPCwithPOS(xmlStr);
    }

    [WebMethod(Description = "获取可用优惠券类型")]
    public string getCouponType()
    {
        return ca.getCouponType();
    }

    [WebMethod(Description = "校验优惠券编号")]
    public string ValidatePC(string xmlStr)
    {
        return ca.consumeValidate(xmlStr);
    }

    [WebMethod(Description = "消费优惠券")]
    public string ConsumePC(string xmlStr)
    {
        return ca.ConsumePC(xmlStr);
    }

    [WebMethod(Description = "消费成功扣减积分")]
    public bool csmSuccess(string xmlStr)
    {
        return ca.csmSuccess(xmlStr);
    }

    [WebMethod(Description = "获取直复平台处理后需发短信数据")]
    public DataSet GetZF_SMS_DATA(string companyId)
    {
        return ca.GetZF_SMS_DATA(companyId);
    }

    [WebMethod(Description = "删除DM接收的短信数据")]
    public string SetSMS_DATA_STATE(string companyId, string upID)
    {
        return ca.SetSMS_DATA_STATE(companyId, upID);
    }

    [WebMethod(Description = "获取已使用但未扣减的优惠券相关信息")]
    public string GetChangePointPC(string xmlStr)
    {
        return ca.GetChangePointPC(xmlStr);
    }

    [WebMethod(Description = "删除积分换礼已扣减记录")]
    public string DelChangePointLog(string companyId, string upID)
    {
        return ca.DelChangePointLog(companyId, upID);
    }

    [WebMethod(Description = "发送会员Pos开卡短信")]
    public string PosVipOpenPC(string companyId, string xmlStr)
    {
        return ca.PosVipOpenPC(companyId, xmlStr);
    }

    [WebMethod(Description = "发送会员销售短信")]
    public string SendSaleMsg(string companyId, string xmlStr)
    {
        return ca.SendSaleMsg(companyId, xmlStr);
    }

    [WebMethod(Description = "发送优惠券申请结果消息")]
    public string SendLpcApplyResMsg(string companyId, string xmlStr)
    {
        return ca.SendLpcApplyResMsg(companyId, xmlStr);
    }

    [WebMethod(Description = "获取可用优惠券信息")]
    public string enabledCouponList()
    {
        return ca.enabledCouponList();
    }

    [WebMethod(Description = "helloworld")]
    public string hello(string x)
    {
        return x;
    }
}
