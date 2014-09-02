<%@ WebHandler Language="C#" Class="ValidateCode" %>

using System;
using System.Web;
using System.Web.SessionState;
public class ValidateCode : IHttpHandler,IRequiresSessionState{
    /*
     *这个是验证输入的验证码是否正确
     */
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string code = context.Request.Params["code"];
        if (context.Session["code"] != null)
        {
            if (string.Compare(code, context.Session["code"].ToString(), true) != 0)
            {
                context.Response.Write("N");
            }
        }
               
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}