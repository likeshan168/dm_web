<%@ WebHandler Language="C#" Class="autoMessage_Response" %>

using System;
using System.Web;

public class autoMessage_Response : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string type = context.Request.QueryString["type"].ToString();
        if (type == "state")
            usingStateChange(context);
    }

    private void usingStateChange(HttpContext context)
    {
        string id = context.Request.QueryString["id"].ToString();
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}