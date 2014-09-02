<%@ WebHandler Language="C#" Class="ValidateMobileExists" %>

using System;
using System.Web;

public class ValidateMobileExists : IHttpHandler
{
/*  
 *这个是验证该手机号是否已经被注册了，因为不允许同一个手机号注册两次
 * 
 */
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string mobile = context.Request.Params["mobile"];
        if (new VipClientOperate().VVipMobile(mobile))
        {
            context.Response.Write("Y");//手机号已经被注册了
        }
        else
        {
            context.Response.Write("N");//手机号没有被注册
        }
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}