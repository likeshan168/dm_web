using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
///权限操作模块
/// </summary>
public class authority
{
    /// <summary>
    /// 获取当前登录用户的权限表
    /// </summary>
    /// <param name="userID">登录ID</param>
    /// <returns>返回权限表</returns>
    public DataTable authorityTable(string userID)
    {
        SQL_Operate so = new SQL_Operate();
        DataTable returnTable = new DataTable();
        returnTable = so.sqlExcuteQueryTable("select al.athyName,ad.athyDetails from authorityList as al left join " +
            "authorityDetails as ad on al.SN=ad.SN where al.userID='" + userID + "'");
        return returnTable;
    }

    /// <summary>
    /// 获取某项操作的权限值
    /// </summary>
    /// <param name="inObj">权限表</param>
    /// <param name="athy">权限名称</param>
    /// <param name="returnDetails">是否返回详细权限信息</param>
    /// <returns>返回是否具有相关权限或详细权限信息:disabled=true/false</returns>
    public static object getAuthority(object inObj, string athy, bool returnDetails)
    {
        DataTable inTable = (DataTable)inObj;
        DataRow[] dr = inTable.Select("athyName='" + athy + "'");
        if (dr.Length > 0)
        {
            if (dr[0][1] == null || dr[0][1].ToString() == "")
                return "true";
            else
            {
                if (returnDetails)
                {
                    DataTable dt = new DataTable();
                    dt.Rows.Add(dr);
                    return dt;
                }
                else
                    return "true";
            }
        }
        else
            return "false";
    }
}
