using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
/// <summary>
///初始化数据库,建立程序应用的数据表
/// </summary>
public class initDataBase
{
    SQL_Operate so;
    public initDataBase()
    {
        so = new SQL_Operate();
    }

    public void initClient()
    {
        try
        {
            so.sqlExcuteNonQuery("use master if not exists(select * from sysdatabases where name= '" + memberStatic.clientDataBase +
            "') create database " + memberStatic.clientDataBase, false);

            #region//新增的（编写：李克善（为了记录系统日志用的））

            string tempTest = SqlStrHelper.CLogSqlStr();
            Console.WriteLine("tempTestSql===" + tempTest);
            so.sqlExcuteNonQuery(tempTest, false);

            #endregion
            so.sqlExcuteNonQuery("use " + memberStatic.clientDataBase + " if not exists(select * from sysobjects where id = object_id(N'[dbo].[clientInfo]') and " +
                "OBJECTPROPERTY(id, N'IsUserTable') = 1) " +
                "CREATE TABLE [dbo].[clientInfo] ([cid] [varchar] (20),[password] [varchar] (100)," +
                "[COMname] [varchar] (20),[companyID] [varchar] (20),[procText_insert] [ntext],[procText_update] [ntext],[loginCount] [int] default 0,lastLoginTime datetime default(GETDATE()))", false);

            int t = (int)so.sqlExecuteScalar("select count(*) from clientInfo where cid='Admin'");
            if (t == 0)
            {
                string sql = "insert into clientInfo([cid],[password],[COMname],[companyID],[procText_insert],[procText_update]) values('Admin','6AAs73bbIQQ=','" +
                    ConfigurationManager.AppSettings["userName"].ToString()
                    + "','" + ConfigurationManager.AppSettings["userID"].ToString() + "',null,null)";
                so.sqlExcuteNonQuery(sql, false);
            }
        }
        catch (NullReferenceException nex)
        {
            throw new Exception("initClient方法空指针异常_" + nex.Source + "_" + nex.StackTrace.Replace("\r\n", "\n").Replace("'", ""));
        }
        catch (Exception ex)
        { throw ex; }
        finally { SQL_Static.closeConnection(); }
    }

    public void greateTable()
    {
        try
        {
            ArrayList myal = getArray();
            so.sqlExcuteNonQuery(myal, false);
        }
        catch (Exception ex)
        { throw ex; }
        finally { SQL_Static.closeConnection(); }
    }
    /// <summary>
    ///以下的这些操作都会去操作数据库影响系统的性能
    /// </summary>
    /// <returns></returns>
    private ArrayList getArray()
    {
        ArrayList al = new ArrayList();

        al.Add("if not exists(select * from sysobjects where id = object_id(N'[dbo].[DataInfo]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin create table DataInfo(ENname varchar(20) not null," +
                "CNname varchar(20) not null,type varchar(20) not null,[size] int,[default] varchar(100),listSort int default 0," +
                "Displayd bit,isCondition bit,Udefined bit not null,insDate datetime) " +
                "ALTER TABLE DataInfo ADD CONSTRAINT [PK_DataInf] PRIMARY KEY CLUSTERED([ENname]) ON [PRIMARY] end");

        /*新增为了下载vip卡号用的
         *这个是存储vip卡号信息表的字段信息
         */
        #region
        al.Add("if not exists(select * from sysobjects where id = object_id(N'[dbo].[CardDataInfo]')" +
               " and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin create table CardDataInfo(ENname varchar(20) not null," +
               "CNname varchar(20) not null,type varchar(20) not null,[size] int,[default] varchar(100) ) " +
               "ALTER TABLE CardDataInfo ADD CONSTRAINT [PK_CardDataInf] PRIMARY KEY CLUSTERED([ENname]) ON [PRIMARY] end");
        #endregion
        /*
         *这个是为了存储vip卡类型的表
         */
        #region 新增是为了存储vip卡类型的表

        string sql = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[VipCardTypeInfo]')" +
               " and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin create table VipCardTypeInfo(TypeID int not null identity(1,1)," +
               "TypeName varchar(20) not null,Discount float,Remark varchar(100)) " +
               "ALTER TABLE VipCardTypeInfo ADD CONSTRAINT [PK_VipCardTypeInfo] PRIMARY KEY CLUSTERED([TypeID]) ON [PRIMARY] end";
        so.sqlExcuteNonQuery(sql, false);
        int t = (int)so.sqlExecuteScalar("select count(*) from VipCardTypeInfo");
        if (t == 0)
        {
            sql = "insert into VipCardTypeInfo([TypeName],[Discount]) values('白银卡',90)";
            so.sqlExcuteNonQuery(sql, false);
            sql = "insert into VipCardTypeInfo([TypeName],[Discount]) values('黄金卡',80)";
            so.sqlExcuteNonQuery(sql, false);
        }
        #endregion

        #region 新增为发了短信的（短信账户和密码，表smsSysNum）
        al.Add(string.Format("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[smsSysNum]')" +
            " and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin  CREATE TABLE [dbo].[smsSysNum]([uid] [varchar](20) NOT NULL,[pwd] [varchar](100) NOT NULL) ON [PRIMARY]"+
            " insert into dbo.smsSysNum values('{0}','{1}') end ",WebConfigurationManager.AppSettings["SmsUserName"],WebConfigurationManager.AppSettings["SmsPwd"]
            ));
        #endregion

        #region 新增的（没有使用的优惠券,表lants_Coupon）
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[lants_Coupon]')  and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[lants_Coupon]([couponid] [varchar](20) NOT NULL primary key,[isused] [int] NOT NULL,[userid] [varchar](50) NULL)");
        #endregion

        #region 新增的（记录优惠生产的数据量的表[dbo].[MaxNumber]）
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MaxNumber]')  and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[MaxNumber]([insertDate] [datetime] NOT NULL default(getdate()),[MaxNum] [int] NULL) ");
        #endregion

        #region 新增的（自动发送短信的表[dbo].[autoMessage]）
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[autoMessage]')  and OBJECTPROPERTY(id, N'IsUserTable') = 1)  CREATE TABLE [dbo].[autoMessage]([id] [int] IDENTITY(1,1) NOT NULL,[type] [varchar](20) NOT NULL,[modelName] [varchar](50) NOT NULL,[modelText] [varchar](1000) NOT NULL,[condition] [varchar](200) NOT NULL,[mainField] [varchar](20) NULL,[sendDate] [datetime] NULL,[dateAdd] [int] NULL,[lastSendDate] [datetime] NULL,[presendCoupon] [bit] NOT NULL,[couponModelID] [int] NULL,[couponEndDate] [int] NULL,[presendScore] [bit] NOT NULL,[scoreValue] [int] NULL,[usingState] [bit] NOT NULL) ");
        #endregion
        #region 新增的（自动短信操作日志[dbo].[autoMessage_log]）
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[autoMessage_log]')  and OBJECTPROPERTY(id, N'IsUserTable') = 1)  CREATE TABLE [dbo].[autoMessage_log]([id] [int]  NOT NULL,[isok] int,OperateDate datetime null,LogMsg varchar(50) null) ");
        #endregion


        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sendData]')" +
               " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[sendData] ([ID] [int] IDENTITY (1, 1) NOT NULL ," +
               "[Mobile] [varchar] (20) NOT NULL ,[Type] [char] (4) NOT NULL ,[Text] [varchar] (5000),[InsTime] [datetime] NOT NULL ," +
               "[ErrorCount] [int] NOT NULL ,[PRI] [int] NOT NULL )");

        al.Add("if not exists(select * from sysobjects where id = object_id(N'[dbo].[operateRecord]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table operateRecord(Iden_ID int identity(1,1) not null,insDate datetime not null," +
                "insValue varchar(5000))");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PRIsetting]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) begin CREATE TABLE [dbo].[PRIsetting] ([smsType] [varchar] (10) ,[PRI] [int] NOT NULL ) " +
                " insert into PRIsetting values('开卡',2) insert into PRIsetting values('销售',1) insert into PRIsetting values('直复',3) insert into PRIsetting values('兑换',4) end");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sendDatalog]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[sendDatalog] ([ID] [int] NOT NULL ,[ComportNum] [varchar] (20) NOT NULL ," +
                "[Mobile] [varchar] (20) NOT NULL ,[Type] [char] (4) NOT NULL ,[Text] [varchar] (5000) COLLATE Chinese_PRC_CI_AS NULL ," +
                "[SendTime] [datetime] NOT NULL ,[State] [varchar] (10) NOT NULL )");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sms_sendModel]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[sms_sendModel] ([mid] [int] identity(1000,1) NOT NULL ,[smsTitle] [varchar] (100) NOT NULL ," +
                "[smsText] [varchar] (500) NOT NULL )");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Investigate]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[Investigate] ([TID] [int] NOT NULL ," +
                "[invDescribe] [varchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,[invDate] [datetime] NOT NULL ,[insState] [bit] NOT NULL )");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Investigate_details]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[Investigate_details] ([QID] [varchar] (10) NOT NULL ," +
                "[TID] [varchar] (100) NOT NULL ,[invQuestion] [varchar] (200) NOT NULL ,[Type] [varchar] (20) NOT NULL ," +
                "[invValue] [varchar] (500) NULL  )");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Investigate_vip]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[Investigate_vip] ([CID] [varchar] (20) NOT NULL ," +
                "[QID] [varchar] (10) NOT NULL ,[invAnswer] [varchar] (200) NOT NULL )");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[smsCard_zf]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[smsCard_zf] ([mid] [varchar] (100) NOT NULL ," +
                "[card_id] [varchar] (20) NOT NULL)");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[smsModel_zf]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[smsModel_zf] ([mid] [varchar] (100) NOT NULL ," +
                "[modelText] [varchar] (500),[channel] [int])");

        //al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[couponMain]')" +
        //        " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[couponMain] ([tid] [int] NOT NULL ," +
        //        "[cid] [varchar] (20) primary key not null,[vipID] [varchar] (50) not null,[getDate] [datetime] NOT NULL ," +
        //        "[endDate] [datetime] NOT NULL ,[consumed] [bit] NOT NULL ,[deduction] [bit] NOT NULL,[applyPlace] [varchar] (50),[applyOperator] [varchar] (50),[consumePlace] [varchar] (50) )");
        #region 修改（李克善）
        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[couponMain]')  and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[couponMain]([tid] [int] NOT NULL,[cid] [varchar](20) NOT NULL primary key,[vipID] [varchar](50) NOT NULL,[getDate] [datetime] NOT NULL,[endDate] [datetime] NOT NULL,[consumed] [bit] NOT NULL,[deduction] [bit] NOT NULL,[applyPlace] [varchar](50) NULL,[applyOperator] [varchar](50) NULL,[consumedPlace] [varchar](50) NULL,[consumedDate] [datetime] NULL)");
        #endregion

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[couponType]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[couponType] ([tid] [int] NOT NULL identity(1,1)," +
                "[ctype] [varchar] (10) ,typeDetails [varchar] (100) not null,[usedPlace] [int],[score] [decimal] NOT NULL ,[money] [decimal] NOT NULL ," +
                "[article] [varchar] (100),usingState[bit] not null, displayed [bit] not null, CONSTRAINT [PK_couponType] PRIMARY KEY CLUSTERED ([score] ASC,[money] ASC) ON [PRIMARY])");

        al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[couponPlace]')" +
                " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[couponPlace] ([pid] [int] NOT NULL identity(1,1)," +
                "[pname] [varchar] (100))");


        //al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sms_field]')" +
        //        " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[sms_field] ([serialNumber] [int] NOT NULL," +
        //        "[fieldName] [varchar] (20),[fieldLength] [int] not null)");

        //al.Add("if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sms_model]')" +
        //        " and OBJECTPROPERTY(id, N'IsUserTable') = 1) CREATE TABLE [dbo].[sms_model] ([serialNumber] [int] NOT NULL," +
        //        "[messageType] [varchar] (20) not null,[messageContent] [varchar] (400) not null)");
        return al;
    }
}
