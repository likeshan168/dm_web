using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
///短信综合操作
/// </summary>
public class smsOperate
{
    SQL_Operate so;
    public smsOperate()
    {
        so = new SQL_Operate();
    }

    /// <summary>
    /// 读取指定短信
    /// </summary>
    /// <param name="smsID">短信ID</param>
    /// <returns>返回对应短信内容</returns>
    public string readSMS(string key)
    {
        try
        {
            if (key == "0")
                return "";
            else
                return so.sqlExecuteScalar("select smsText from sms_sendModel where smsTitle='" + key + "'").ToString();
        }
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// 读取短信列表
    /// </summary>
    /// <returns>返回短信标题及短信编号的数据表</returns>
    public DataTable readSMSlist()
    {
        try
        {
            DataTable rdt = new DataTable();
            rdt = so.sqlExcuteQueryTable("select smsTitle,mid,smsText from sms_sendModel");
            return rdt;
        }
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// 删除已保存的短信
    /// </summary>
    /// <param name="smsID">短信ID</param>
    /// <returns>返回是否删除成功</returns>
    public bool deleteSMS(string key)
    {
        try
        {
            int i = so.sqlExcuteNonQueryInt("delete from sms_sendModel where smsTitle='" + key + "'");
            if (i == 1)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        { throw ex; }
    }

    /// <summary>
    /// 保存短信内容
    /// </summary>
    /// <param name="title">标题</param>
    /// <param name="text">内容</param>
    public void saveSMS(string title, string text)
    {
        try
        {
            string sql = string.Format("declare @temp int;select @temp=COUNT(*) from sms_sendModel where smsTitle='{0}'; if @temp=0 begin insert into dbo.sms_sendModel values('{1}','{2}');end else begin update dbo.sms_sendModel set smsText='{3}' where smsTitle='{4}';end",title,title,text,text,title);
            //so.sqlExcuteNonQuery("insert into sms_sendModel values('" + title + "','" + text + "')", false);
            so.sqlExcuteNonQuery(sql, false);
        }
        catch (Exception ex)
        { throw ex; }
    }

    /// <summary>
    /// 短信发送主方法
    /// </summary>
    /// <param name="columnName">数据字段名,数据值必须唯一</param>
    /// <param name="isUdField">此字段是否自定义字段</param>
    /// <param name="columnValue">字段值数组</param>
    /// <param name="smsText">短信内容</param>
    public void sendSMS(string columnName, bool isUdField, ArrayList columnValue, string smsText)
    {
        try
        {
            string sql = "", value = "";
            //判断字段类型,in语句中是否加引号
            string type = so.sqlExecuteScalar("select type from dataInfo where ENname='" + columnName + "'").ToString();
            for (int i = 0; i < columnValue.Count; i++)
            {
                if (type == "整数" || type == "数字" || type == "货币" || type == "整数型" ||
                    type == "公式(整数)" || type == "公式(小数)" || type == "小数型")
                    value += columnValue[i].ToString() + ",";
                else
                    value += "'" + columnValue[i].ToString() + "',";
            }
            value = value.Substring(0, value.Length - 1);
            //判断是否自定义字段,组合不同语句查询手机号码序列
            if (!isUdField)
                sql = "select userMobile from cardInfo where " + columnName + " in(" + value + ")";
            else
                sql = "select cf.userMobile from cardInfo as cf inner join UD_Field as ud on cf.card_id = ud.card_id where ud." + columnName +
                    " in(" + value + ")";
            ArrayList al = so.sqlExcuteQueryList(sql);
            //插入短信群发表
            ArrayList sqlal = new ArrayList();
            int x = (int)so.sqlExecuteScalar("select PRI from PRIsetting where smsType='直复'");
            for (int i = 0; i < al.Count; i++)
                sqlal.Add("insert into sendData values('" + al[i].ToString() + "','直复','" + smsText + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',0," + x + ")");
            so.sqlExcuteNonQuery(sqlal, false);
        }
        catch (Exception ex)
        { throw ex; }
    }
}
