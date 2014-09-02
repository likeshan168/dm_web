<%@ WebHandler Language="C#" Class="vipMaster_Response" %>

using System;
using System.Web;
using System.Data;
using System.Web.SessionState;


public class vipMaster_Response : IHttpHandler, IRequiresSessionState {

    SQL_Operate so = new SQL_Operate();
    public void ProcessRequest (HttpContext context) {
        string type = context.Request.QueryString["type"].ToString();
        switch (type)
        {
            case "vip": vipOperate(context);
                break;
            case "invs": invsOperate(context);
                break;
            case "paging": pagingOperate(context);
                break;
            case "couponTypeChanged": couponOperate(context);
                break;
            case "smsMO": smsOperate(context);
                break;
        }
    }

    protected void vipOperate(HttpContext context)
    {
        //
        //TODO:可以不用总是请求数据库，可以直接放在前台进行操作，因为每一次的切换都会请求数据库，这样效率很低
        //
        string name = context.Request.QueryString["filed"].ToString();
        string r = (string)so.sqlExecuteScalar("select type from DataInfo where ENname='" + name + "'");
        switch (r)
        {
            case "日期":
            case "日期型":
            case "公式(日期)":
                r = "日期";
                break;
            case "单选按钮":
                r = "单选按钮";
                break;
            default: r = "文本";
                break;
        }
        context.Response.Write(r);
        context.Response.End();
    }
    protected void invsOperate(HttpContext context)
    {
        //返回某个调研问卷的详细问题信息
        string id = context.Request.QueryString["invsID"].ToString();
        DataTable dt = so.sqlExcuteQueryTable("select * from Investigate_details where TID='" + id + "'");
        context.Session["invsDetailsTable"] = dt;
        context.Response.Write("success");
        context.Response.End();
    }

    protected void pagingOperate(HttpContext context)
    {
        try
        {
            int start = 0, end = 0, total = 0;
            int pageIndex = Convert.ToInt16(context.Request.QueryString["pageIndex"]);
            int pageSize = Convert.ToInt16(context.Request.QueryString["pageSize"]);
            //获取查询出的VIP原始数据
            //
            //TODO:性能还有待优化
            //
            DataTable oldVipTable = (DataTable)context.Cache["vipTable"];//Cache也能跨页面
            DataTable vipTable = oldVipTable.Clone();
            //为了给每一行自动编号,添加自增长列
            DataColumn column = new DataColumn();
            column.ColumnName = "iden";
            column.DataType = typeof(int);
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            vipTable.Columns.Add(column);
            foreach (DataRow dr in oldVipTable.Rows)
                vipTable.Rows.Add(dr.ItemArray);
            //计算返回数据的起始及结束位置
            start = 1 + (pageIndex - 1) * pageSize;
            end = pageSize * pageIndex;
            //计算总数据量
            total = vipTable.Rows.Count;
            //计算总页数
            total = total % pageSize > 0 ? total / pageSize + 1 : total / pageSize;
            //开始筛选数据
            DataRow[] dt = vipTable.Select("iden>=" + start + " and iden<=" + end);
            DataTable newVipTable = vipTable.Clone();
            foreach (DataRow dr in dt)
                newVipTable.Rows.Add(dr.ItemArray);
            vipTable.Clear();

            //移除自增长列
            newVipTable.Columns.Remove("iden");
            context.Cache["vipPagedTable"] = newVipTable;
            context.Response.Write(total);
        }
        catch (Exception ex) { context.Response.Write("error:" + ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { context.Response.End(); }
    }

    protected void couponOperate(HttpContext context)
    {
        try
        {
            string value = context.Request.QueryString["listItem"];//LOC
            string sql = "select typeDetails,tid from couponType where ctype='" + value + "' and usingState=1";
            DataTable mydt = so.sqlExcuteQueryTable(sql);
            context.Cache["couponTypeTable"] = mydt;
            context.Response.Write("success");
        }
        catch (Exception ex) { context.Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { context.Response.End(); }
    }

    protected void smsOperate(HttpContext context)
    {
        try
        {
            string mode = context.Request.QueryString["mode"];
            smsOperate sms = new smsOperate();
            #region
            if (mode == "save")
            {
                string title = context.Request.QueryString["Title"];
                string text = context.Request.QueryString["Text"];
                sms.saveSMS(title, text);
                context.Response.Write("success");
            }
            #endregion 
            #region
         
            #endregion
            if (mode == "read")
            {
                string key = context.Request.QueryString["value"];
                string t = sms.readSMS(key);
                context.Response.Write(t);
            }
            if (mode == "del")
            {
                string key = context.Request.QueryString["value"];
                sms.deleteSMS(key);
                context.Response.Write("success");
            }
        }
        catch (Exception ex) { context.Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { context.Response.End(); }
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}