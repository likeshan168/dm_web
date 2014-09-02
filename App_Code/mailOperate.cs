using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading;

/// <summary>
///邮件群发操作综合处理
/// </summary>
public class mailOperate
{
    SQL_Operate so;
    public mailOperate()
    {
        so = new SQL_Operate();
    }

    /// <summary>
    /// 邮件发送主方法
    /// </summary>
    /// <param name="columnName">数据字段名,数据值必须唯一</param>
    /// <param name="isUdField">此字段是否自定义字段</param>
    /// <param name="columnValue">字段值数组</param>
    public void SendmailOperate(string columnName, bool isUdField, ArrayList columnValue)
    {
        try
        {
            string sql = "", value = "";
            string mailText = "";
            FileStream fs = new FileStream("C:/EmpoxWebModel/mailModel/03ffed.tmp", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            mailText = sr.ReadToEnd();
            sr.Close();
            fs.Close();
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
            //判断是否自定义字段,组合不同语句查询电子邮箱序列
            if (!isUdField)
                sql = "select userEmail from cardInfo where " + columnName + " in(" + value + ")";
            else
                sql = "select cf.userEmail from cardInfo as cf inner join UD_Field as ud on cf.card_id = ud.card_id where ud." + columnName +
                    " in(" + value + ")";
            ArrayList al = so.sqlExcuteQueryList(sql);
            //发送邮件
            for (int i = 0; i < al.Count; i++)
            {
                sendMail.SendMail(al[i].ToString().Trim(), mailText, 1);//添加去空格
                Thread.Sleep(1000);
            }
        }
        catch (Exception ex)
        { throw ex; }
    }
}
