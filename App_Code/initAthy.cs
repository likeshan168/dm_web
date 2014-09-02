using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///初始化权限模块,建立数据表及初始权限信息
/// </summary>
public class initAthy
{
    public initAthy()
    {

    }

    public void createAthy()
    {
        SQL_Operate so = new SQL_Operate();
        ArrayList al = new ArrayList();
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[authorityList]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
            "begin CREATE TABLE [dbo].[authorityList] ([userID] [varchar] (20) NOT NULL ,[athyName] [varchar] (200) NOT NULL ," +
            "[SN] [int] IDENTITY (1, 1) NOT NULL PRIMARY KEY ) " +
        "insert into authorityList values('Admin','系统管理') " +
        "insert into authorityList values('Admin','代理商管理') " +
        "insert into authorityList values('Admin','会员资料查询') " +
        "insert into authorityList values('Admin','直复系统管理') " +
        "insert into authorityList values('Admin','会员调研管理') " +
        "insert into authorityList values('Admin','销售统计') " +
        "insert into authorityList values('Admin','资料下载') end");
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[authorityDetails]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)" +
            "begin CREATE TABLE [dbo].[authorityDetails] ([SN] [int] NOT NULL foreign key(SN) REFERENCES authorityList(SN) ," +
            "[athyDetails] [varchar] (200) NOT NULL ) "+
        "insert into authorityDetails values(1,'增加用户') "+
        "insert into authorityDetails values(1,'修改用户') "+
        "insert into authorityDetails values(1,'删除用户') end");
        try
        {
            so.sqlExcuteNonQuery(al, true);
        }
        catch (Exception ex)
        { throw ex; }
    }
}
