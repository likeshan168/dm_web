using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 系统日志记录
/// </summary>
public class LogModule
{
    SQL_Operate sqlOp = new SQL_Operate();

    public void InsLog(string suser,string slog)
    {
        try
        {
            string str = "insert into Empox_Log (UserID,EditLog) values('" + suser.Trim() + "','" + slog.Trim() + "')";
            sqlOp.sqlExcuteNonQuery(str,false);
        }
        catch (Exception ex)
        { throw ex; }
    }
}
