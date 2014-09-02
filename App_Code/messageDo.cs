using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
///短信模板修改功能数据处理类
/// </summary>
public class messageDo
{
    SQL_Operate so = new SQL_Operate();
    public messageDo()
    {
        //注释签出测试
    }

    public DataTable msgStruct()
    {
        DataTable returnTable = new DataTable();
        try
        {
            string sql = "select sms_model.messageType,sms_field.fieldName,sms_field.fieldLength from sms_model left join " +
                "sms_field on sms_model.serialNumber=sms_field.serialNumber";
            returnTable = so.sqlExcuteQueryTable(sql);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnTable;
    }

    public string msgUpdate(string UpdateString, string type)
    {
        try
        {
            string sql = "update sms_model set messageContent='" + UpdateString + "' where messageType='" + type + "'";
            so.sqlExcuteNonQuery(sql, false);
            return "success";
        }
        catch (Exception ex)
        { return ex.Message; }
        finally { }
    }

    public string msgQuery(string type)
    {
        try
        {
            string sql = "select messageContent from sms_model where messageType='" + type + "'";
            return so.sqlExecuteScalar(sql).ToString();
        }
        catch (Exception ex)
        { return ex.Message; }
    }
}
