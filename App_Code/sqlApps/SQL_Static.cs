using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
///数据库连接对象集合
///**已废弃**
/// </summary>
public static class SQL_Static
{
    public static SqlConnection scon;
    public static SqlCommand scom;
    public static SqlDataAdapter sda;
    public static SqlDataReader sdr;
    public static SqlTransaction tran;

    static SQL_Static()
    {
        scon = new SqlConnection(memberStatic.connectionString);
    }

    /// <summary>
    /// 初始化SqlCommand对象
    /// </summary>
    public static void initSqlCommand()
    {
        scom = new SqlCommand();
        scom.Connection = scon;
    }

    /// <summary>
    /// 初始化SqlCommand对象
    /// </summary>
    /// <param name="sql">需要执行的SQL语句</param>
    public static void initSqlCommand(string sql)
    {
        scom = new SqlCommand(sql,scon);
    }

    /// <summary>
    /// 初始化SqlDataAdapter对象
    /// </summary>
    /// <param name="sql">需要执行的SQL语句</param>
    public static void initSqlDataAdapter(string sql)
    {
        sda = new SqlDataAdapter(sql, scon);
    }

    /// <summary>
    /// 打开数据库连接.失败时返回异常信息.
    /// </summary>
    public static void openConnection()
    {
        try
        {
            if (scon.State != ConnectionState.Open)
                scon.Open();
        }
        catch(Exception ex) 
        { throw ex; };
    }

    /// <summary>
    /// 关闭数据库连接.
    /// </summary>
    public static void closeConnection()
    {
        if (scon.State == ConnectionState.Open)
            scon.Close();
    }
}
