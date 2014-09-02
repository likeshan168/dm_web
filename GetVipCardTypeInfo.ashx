<%@ WebHandler Language="C#" Class="GetVipCardTypeInfo" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Data;
using System.Text;
public class GetVipCardTypeInfo : IHttpHandler, IRequiresSessionState
{
    /*
     *专用于前台获取卡类型使用的
     */
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string action = context.Request.Params["action"];
        if (action == "getVIPType")
        {
            DataTable dt = new VipClientOperate().GetVipCardTypeInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<option value='0'>--请选择--</option>");
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    sb.AppendFormat("<option value='{0}'>{1}[折扣:{2}]</option>", i, dr[0].ToString(), dr[1].ToString());
                    i++;
                }
                context.Response.Write(sb.ToString());
            }
            else
            {
                context.Response.Write(string.Empty);
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}