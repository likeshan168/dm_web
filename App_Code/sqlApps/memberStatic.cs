using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///memberStatic 的摘要说明
/// </summary>
public static class memberStatic
{
    /// <summary>
    /// DM_TEST
    /// </summary>
    public static string clientDataBase = ConfigurationManager.AppSettings["clientDataBase"].ToString();
    /// <summary>
    /// 00202
    /// </summary>
    public static string companyID = ConfigurationManager.AppSettings["userID"].ToString();
    /// <summary>
    /// "server=219.232.48.105;database=master;uid=sa;pwd=empoxweb!@90zgtyb"
    /// </summary>
    public static string connectionString = ConfigurationManager.ConnectionStrings["LocalConnectionString"].ToString();
    /// <summary>
    /// "http://localhost:1919/EmpoxWebSite/webService/couponService.asmx"
    /// </summary>
   // public static string webServiceUrl = ConfigurationManager.AppSettings["couponWeb.couponService"].ToString();
}
