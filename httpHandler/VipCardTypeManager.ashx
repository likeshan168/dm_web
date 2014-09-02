<%@ WebHandler Language="C#" Class="VipCardTypeManager" %>

using System;
using System.Web;
using System.Data;
using System.Web.SessionState;
using System.Text;
public class VipCardTypeManager : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //string flag = context.Request.Params["flag"];
        //if (flag == "VT")
        //{
        //    string result = GetVipCardTypeInfo();
        //    if (!string.IsNullOrEmpty(result))
        //    {
        //        context.Response.Write(result);
        //    }
        //    else
        //    {
        //        context.Response.Write("null");
        //    }
        //}
        //else
        //{
        //    context.Response.Write("NO");
        //}

    }
    //public string GetVipCardTypeInfo()
    //{
    //    try
    //    {
    //        SQL_Operate so = new SQL_Operate();
    //        StringBuilder sb = new StringBuilder();
    //        DataTable dt = so.sqlExcuteQueryTable(SqlStrHelper.GVipCardTypeSqlStr());
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            HttpContext.Current.Session["VipCardType"] = dt;
    //            foreach (DataRow dr in dt.Rows)
    //            {
    //                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td><img src='images/config.png'/></td><td><img src='images/XX.png'/></td></tr>", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
    //            }
    //            return sb.ToString();
    //        }
    //        else
    //        {
    //            return string.Empty;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    }

    //}
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}