<%@ WebHandler Language="C#" Class="Main_Response" %>

using System;
using System.Web;

public class Main_Response : IHttpHandler {

    initDataBase idb = new initDataBase();
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            idb.greateTable();
            context.Response.Write("Finished");
        }
        catch (Exception ex)
        { context.Response.Write(ex.Message); }
        finally
        { context.Response.End(); }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}