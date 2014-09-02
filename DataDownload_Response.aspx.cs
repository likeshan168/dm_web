using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

public partial class DataDownload_Response : System.Web.UI.Page
{
    string[] row;
    int columnCount;
    DataTable columnTable;
    string[] column;
    SQL_Operate so = new SQL_Operate();
    static char[] splitChar1 = { (char)21 };
    static char[] splitChar2 = { (char)22 };
    static char[] splitChar3 = { (char)23 };

    protected void Page_Load(object sender, EventArgs e)
    {
        string type = Request.QueryString["type"].ToString();
        if (type == "vip")
        {
            VIP_DownLoad();
        }
        else if (type == "saler")
        {
            Saler_DownLoad();
        }
        else if (type == "shop")
        {
            Shop_DownLoad();
        }
        else if (type == "vipCard")//新增是用来下载vip卡号的
        {
            VIPCard_DownLoad();
        }
        else if (type == "vipkdt")//口袋通 vip 信息下载
        {
            VIP_KDT_DownLoad();
        }
        else if (type == "vipkdtSale")//口袋通 vip 销售信息下载
        {
            VIP_KDT_Sale_DownLoad();
        }
    }
    /// <summary>
    /// 新增
    /// func:将口袋通vip销售数据下载
    /// author:李克善
    /// date:2014-08-19
    /// </summary>
    private void VIP_KDT_Sale_DownLoad()
    {
        try
        {
            LogicModel logic = new LogicModel();
            string jsonStr = logic.GetVipSaleKdtInfo(string.Empty, 1, 100);
            JObject jo = JObject.Parse(jsonStr);
            int total;
            int.TryParse(jo["response"]["total_results"].ToString(), out total);

            int total_page = total / 100;
            if (total % 100 > 0)
            {
                total_page += 1;
            }
            string sqlStr = "delete from lants_sale_mst where bill_id  like 'E%';delete from lants_sale_dtl where bill_id like 'E%';delete from shopInfo where clientId='eissy';delete from salerInfo where salerId='kdt';";
            so.sqlExcuteNonQuery(sqlStr);

            sqlStr = "insert into shopInfo(clientId,clientName) values('eissy','上元端');insert salerInfo(salerId,salerName) values('kdt','Eissy微商城');";
            so.sqlExcuteNonQuery(sqlStr);


            #region 新增的，查看销售记录的(视图)
            sqlStr = "select COUNT(*) from INFORMATION_SCHEMA.VIEWS where TABLE_NAME =N'saleCount'";
            
            int j = (int)(so.sqlExecuteScalar(sqlStr));
            if (j == 0)
            {
               sqlStr = "create view [dbo].[saleCount] as select cif.sendMan as '发卡人',cif.userName as '持卡人',cif.card_id as '卡号',ls.bill_id as 销售单号,ls.sale_time as '销售时间',cif.card_Type as '卡类型',ls.amount as '金额',ct.times as '消费次数',ls.qty as '购买数量' from (select mst.vip_id,mst.bill_id,mst.sale_time,sum(cast(dtl.amount as float)) as amount,sum(dtl.qty) as qty from lants_sale_mst mst inner join lants_sale_dtl dtl on mst.bill_id = dtl.bill_id group by mst.bill_id,mst.vip_id,mst.sale_time) ls inner join cardInfo cif on ls.vip_id = cif.card_id inner join (select vip_id,count(vip_id) as times from lants_sale_mst group by vip_id) ct on cif.card_id = ct.vip_id";
                so.sqlExcuteNonQuery(sqlStr);
            }
            #endregion

            #region 新增的
            sqlStr = "select COUNT(*) from INFORMATION_SCHEMA.VIEWS  where TABLE_NAME =N'salesParticularView'";
            j = Convert.ToInt32(so.sqlExecuteScalar(sqlStr));
            Console.WriteLine("判断视图saleCount是否存在，如果不存在就创建");
            if (j == 0)
            {
                sqlStr = "CREATE VIEW [dbo].[salesParticularView] as select cif.sendMan as sendMan,cif.userName as userName,cif.card_id as card_id,cif.card_Type,ls.bill_id as bill_id,ls.sale_time as sale_time,ls.amount as amount,ct.times as times,ls.qty as qty,status from (select mst.vip_id,mst.bill_id,mst.sale_time,sum(cast(dtl.amount as float)) as amount,sum(dtl.qty) as qty,status from lants_sale_mst mst inner join lants_sale_dtl dtl on mst.bill_id = dtl.bill_id group by mst.bill_id,mst.vip_id,mst.sale_time,mst.status) ls inner join cardInfo cif on ls.vip_id = cif.card_id inner join (select vip_id,count(vip_id) as times from lants_sale_mst group by vip_id) ct on cif.card_id = ct.vip_id";

                so.sqlExcuteNonQuery(sqlStr);
            }

            #endregion

            JArray ja = JArray.Parse(jo["response"]["trades"].ToString());
            insert_vip_kdt_sale_info(ja, logic);

            for (int i = 2; i <= total_page; i++)
            {
                jsonStr = logic.GetVipSaleKdtInfo(string.Empty, i, 100);
                jo = JObject.Parse(jsonStr);
                ja = JArray.Parse(jo["response"]["trades"].ToString());
                insert_vip_kdt_sale_info(ja, logic);
            }
            Response.Write("vipKdtSaleFinished");

            //Thread.Sleep(1000 * 60 * 60 * 12);//12小时
        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);

        }
    }
    /// <summary>
    /// 新增
    /// func:将口袋通vip销售数据插入数据库
    /// author:李克善
    /// date:2014-08-19
    /// </summary>
    /// <param name="ja">json数组</param>
    /// <param name="logic">调用口袋通的接口</param>
    private void insert_vip_kdt_sale_info(JArray ja, LogicModel logic)
    {
        StringBuilder sb1 = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        string wx_id = string.Empty;
        for (int i = 0; i < ja.Count; i++)
        {
            wx_id = logic.GetWeiXinUserInfo(ja[i]["weixin_user_id"].ToString());
            sb1.AppendFormat("insert into lants_sale_mst(bill_id,sale_time,client_id,saler_id,vip_id,status) values('{0}','{1}','{2}','{3}','{4}',{5});", ja[i]["tid"].ToString(), ja[i]["created"].ToString(), "eissy", "kdt", wx_id, GetStatus(ja[i]["status"].ToString()));

            JArray jb = JArray.Parse(ja[i]["orders"].ToString());
            for (int j = 0; j < jb.Count; j++)
            {
                sb2.AppendFormat("insert into lants_sale_dtl(bill_id,product_id,price,discount,qty,amount,product_name) values('{0}','{1}',{2},{3},{4},{5},'{6}');", ja[i]["tid"].ToString(), jb[j]["outer_sku_id"].ToString(), float.Parse(jb[j]["price"].ToString()), float.Parse(jb[j]["discount_fee"].ToString()), int.Parse(jb[j]["num"].ToString()), float.Parse(jb[j]["total_fee"].ToString()), jb[j]["title"].ToString());
            }

        }
        if (ja.Count > 0)
        {
            so.sqlExcuteNonQuery(sb1.ToString());
            so.sqlExcuteNonQuery(sb2.ToString());
        }
    }

    /// <summary>
    /// 获取订单状态
    /// </summary>
    /// <param name="statusStr">口袋通返回的状态字符串</param>
    /// <returns></returns>
    private int GetStatus(string statusStr)
    {
        switch (statusStr)
        {
            case "TRADE_NO_CREATE_PAY"://没有创建支付交易
                return 0;
            case "WAIT_BUYER_PAY"://等待买家付款
                return 1;
            case "TRADE_CLOSED_BY_USER"://付款以前，卖家或买家主动关闭交易
                return 2;
            case "WAIT_SELLER_SEND_GOODS"://等待卖家发货，即：买家已付款
                return 3;
            case "WAIT_BUYER_CONFIRM_GOODS"://等待买家确认收货，即：卖家已发货
                return 4;
            case "TRADE_BUYER_SIGNED"://买家已签收
                return 5;
            case "TRADE_CLOSED"://付款以后用户退款成功，交易自动关闭
                return 6;
            default:
                return 0;
        }
    }


    /// <summary>
    /// 新增
    /// func:将口袋通vip数据下载下来
    /// author:李克善
    /// date:2014-08-19
    /// </summary>
    private void VIP_KDT_DownLoad()
    {
        Session["DownLoad_Error"] = null;
        //直接去口袋通借口获取数据 
        try
        {
            LogicModel logic = new LogicModel();
            string jsonStr = logic.GetVipKdtInfo(1, 100);
            JObject jo = JObject.Parse(jsonStr);
            int total;
            int.TryParse(jo["response"]["total_results"].ToString(), out total);

            int total_page = total / 100;
            if (total % 100 > 0)
            {
                total_page += 1;
            }
            string sqlStr = "delete from cardinfo where remark='kdt'";
            so.sqlExcuteNonQuery(sqlStr);
            JArray ja = JArray.Parse(jo["response"]["users"].ToString());
            insert_vip_kdt_info(ja, logic);

            for (int i = 2; i <= total_page; i++)
            {
                jsonStr = logic.GetVipKdtInfo(i, 100);
                jo = JObject.Parse(jsonStr);
                ja = JArray.Parse(jo["response"]["users"].ToString());
                insert_vip_kdt_info(ja, logic);
            }
            //Thread.Sleep(3 * 60 * 1000);//改成3分钟

            Response.Write("vipKdtFinished");


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

        }


    }
    /// <summary>
    /// 新增
    /// func:将口袋通vip数据插入数据库
    /// author:李克善
    /// date:2014-08-19
    /// </summary>
    /// <param name="ja">json数组</param>
    private void insert_vip_kdt_info(JArray ja, LogicModel logic)
    {
        StringBuilder sb = new StringBuilder();
        string rst = string.Empty;
        string phoneNumber = string.Empty;
        for (int i = 0; i < ja.Count; i++)
        {
            rst = logic.GetVipSaleKdtInfo(ja[i]["user_id"].ToString(), 1, 100);//获取手机号

            JObject o = JObject.Parse(rst);
            if (o["response"]["trades"].HasValues)
            {
                JArray a = JArray.Parse(o["response"]["trades"].ToString());
                phoneNumber = a[0]["receiver_mobile"].ToString();
            }
            sb.AppendFormat("insert into cardinfo(card_id,card_type,card_discount,username,usersex,begindate,remark,userPhone,userMobile) values('{0}','普通会员卡',100,'{1}','{2}','{3}','kdt','{4}','{5}');", ja[i]["weixin_openid"].ToString(), ja[i]["nick"].ToString().Replace("'", ""), ja[i]["sex"].ToString() == "m" ? "男" : "女", ja[i]["follow_time"].ToString(), phoneNumber, phoneNumber);
            phoneNumber = "";
        }
        if (ja.Count > 0)
            so.sqlExcuteNonQuery(sb.ToString());
    }
    protected void VIP_DownLoad()
    {
        Session["DownLoad_Error"] = null;
        DateTime newDT = new DateTime(2010, 1, 1);
        TimeSpan sp = DateTime.Now - newDT;
        double li = sp.TotalMilliseconds;
        //插入请求
        so.sqlExcuteNonQuery("insert into TempStor values('" + li + "','0x30',0,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','" + memberStatic.companyID + "',null)", false);
        //Thread.Sleep(10 * 60 * 1000);//10分钟
        Thread.Sleep(3 * 60 * 1000);//改成3分钟
        //等待返回VIP数据及表信息
        byte[] bytesr = (byte[])so.sqlExecuteScalar("select data from TempReturn where SessionId='" + li +
            "' and companyid='" + memberStatic.companyID + "'");
        #region//存在要更新的数据
        if (bytesr != null)
        {
            string vipData = Encoding.GetEncoding("GB2312").GetString(bytesr);
            //启用事务的sql语句集合.
            DataTable sqldt = new DataTable();
            sqldt.Columns.Add("sql");//sql操作语句
            sqldt.Columns.Add("proc");//是否是存储过程
            if (!string.IsNullOrEmpty(vipData))
            {
                try
                {
                    columnTable = new DataTable();
                    columnTable.Columns.Add("colName", typeof(string));
                    columnTable.Columns.Add("colType", typeof(string));
                    columnTable.Columns.Add("colLenth", typeof(string));
                    columnTable.Columns.Add("colCName", typeof(string));
                    string[] t = vipData.Split(splitChar3[0]);
                    string[] columnInfo = t[2].Split(splitChar1[0]);
                    for (int i = 1; i < columnInfo.Length; i++)
                    {
                        DataRow myrow = columnTable.NewRow();
                        myrow.ItemArray = columnInfo[i].Split(splitChar2[0]);
                        columnTable.Rows.Add(myrow);
                    }
                    row = t[4].Split(splitChar1[0]);
                    //columnCount = row[1].ToString().Split(splitChar2[0]).Length;
                    #region//看datainfo是否有字段的信息数据
                    int j = Convert.ToInt32(so.sqlExecuteScalar("select count(size) from datainfo"));
                    if (j == 0)
                    {
                        for (int i = 0; i < columnTable.Rows.Count; i++)
                        {
                            //这条语句可能有问题
                            string insdata = "insert into datainfo values('" + columnTable.Rows[i][0].ToString() +
                                "','" + columnTable.Rows[i][3].ToString() + "',";
                            switch (columnTable.Rows[i][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint": insdata += "'整数型',4,";
                                    break;
                                case "char":
                                case "varchar":
                                case "nchar": insdata += "'文本型'," + columnTable.Rows[i][2].ToString() + ",";
                                    break;
                                case "datetime": insdata += "'日期型',8,";
                                    break;
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": insdata += "'小数型',8,";
                                    break;
                                default: insdata += "'文本型',1000,";
                                    break;
                            }
                            insdata += "null,0,0,0,0,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                            sqldt.Rows.Add(new object[] { insdata, false });
                        }
                        sqldt.Rows.Add(new object[]{"insert into datainfo values('is_Upload','是否为导入数据','整数型',4,null,0,0,0,1,'" 
                            + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')",false});
                        sqldt.Rows.Add(new object[]{"insert into datainfo values('empox_favor','已发优惠券','复选框',1,null,0,0,0,1,'" 
                            + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')",false});
                    }
                    #endregion

                    #region//判断表cardinfo是否存在，如果不存在就去创建
                    int x = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from sysobjects where name='cardInfo' and type='U'"));
                    if (x == 0)
                    {
                        int cardPlace = Convert.ToInt32(t[1].ToString());
                        string createTablesql = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[cardInfo]')" +
                            " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table cardInfo(";
                        for (int i = 0; i < columnTable.Rows.Count; i++)
                        {
                            switch (columnTable.Rows[i][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " int,";
                                    break;
                                case "char":
                                case "varchar":
                                case "nchar": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " varchar(" + columnTable.Rows[i][2].ToString() + "),";
                                    break;
                                case "datetime": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " datetime,";
                                    break;
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " float,";
                                    break;
                                default: createTablesql += columnTable.Rows[i][0] + " varchar(1000),";
                                    break;
                            }
                            if (i == cardPlace)
                                createTablesql = createTablesql.Substring(0, createTablesql.Length - 1) + " primary key not null,";
                        }
                        createTablesql += ")";
                        sqldt.Rows.Add(new object[] { createTablesql, false });
                        sqldt.Rows.Add(new object[] { "if not exists(select * from sysobjects where id = object_id(N'[dbo].[UD_Fileds]')"+
                            " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table UD_Fileds(card_id varchar(20) not null,"+
                            "is_Upload int default 0 not null,empox_favor bit default 1 not null,Iden_ID int identity(1,1) not null)",false});

                        string createTablesql_trans = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[cardInf]')" +
                            " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table cardInf(";
                        for (int i = 0; i < columnTable.Rows.Count; i++)
                        {
                            switch (columnTable.Rows[i][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " int,";
                                    break;
                                case "char":
                                case "varchar":
                                case "nchar": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " varchar(" + columnTable.Rows[i][2].ToString() + "),";
                                    break;
                                case "datetime": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " datetime,";
                                    break;
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " float,";
                                    break;
                                default: createTablesql_trans += columnTable.Rows[i][0] + " varchar(1000),";
                                    break;
                            }
                            if (i == cardPlace)
                                createTablesql_trans = createTablesql_trans.Substring(0, createTablesql_trans.Length - 1) + " primary key not null,";
                        }
                        createTablesql_trans += ")";
                        sqldt.Rows.Add(new object[] { createTablesql_trans, false });

                        so.sqlExcuteNonQuery(sqldt, true);//执行操作语句

                        #region 新加的获取vip信息的视图
                        x = (int)so.sqlExecuteScalar("select COUNT(*) from INFORMATION_SCHEMA.VIEWS where TABLE_NAME=N'vipInfo_view'");
                        if (x == 0)
                        {
                            so.sqlExcuteNonQuery("create view [dbo].[vipInfo_view] as select zl.beginDate,zl.card_Discount,zl.card_Type,zl.empox_favor,zl.endDate,zl.is_Upload,zl.points,zl.pwd,zl.remark,zl.sendClient,zl.sendMan,zl.userAddress,zl.userBirthday,zl.userCode,zl.userEmail,zl.userMobile,zl.userName,zl.userPhone,zl.userPost,zl.userSex,zl.userTitle,jf.spareScore,jf.card_id from (select beginDate, card_Discount, card_Type, ud.empox_favor, endDate, ud.is_Upload, points, pwd, remark, sendClient, sendMan, userAddress, userBirthday, userCode, userEmail, userMobile, userName, userPhone, userPost, userSex, userTitle, cardInfo.card_id from cardInfo left join (select distinct card_id,is_Upload,empox_favor from  UD_Fileds) as ud on cardInfo.card_id = ud.card_id) zl left join (select isnull(a.points,0)-isnull(b.score,0) as spareScore,a.card_id from (select cardInfo.card_id,isnull(points,0) as points from cardInfo) a left join (select cm.vipID,sum(ct.score) as score from couponMain as cm inner join couponType as ct on cm.tid=ct.tid where cm.deduction=0 and endDate>=getdate() group by cm.vipID ) b on a.card_id = b.vipID) jf on zl.card_id = jf.card_id");
                        }
                        #endregion

                        #region 新加的查看优惠券的视图
                        x = (int)so.sqlExecuteScalar("select COUNT(*) from INFORMATION_SCHEMA.VIEWS where TABLE_NAME=N'couponConsumeRecordView'");
                        if (x == 0)
                        {
                            so.sqlExcuteNonQuery("CREATE VIEW [dbo].[couponConsumeRecordView] AS SELECT  dbo.cardInfo.card_Id, dbo.cardInfo.userName, dbo.cardInfo.userMobile, dbo.couponMain.cid, dbo.couponMain.getDate, dbo.couponMain.endDate, dbo.couponMain.consumed, dbo.couponMain.applyPlace, ISNULL(dbo.couponMain.consumedPlace, '尚未使用') AS consumedPlace, dbo.couponType.typeDetails, CASE (dbo.couponType.ctype) WHEN 'LOC' THEN '代金券' WHEN 'GOODS' THEN '实物券' END AS type, dbo.couponMain.applyOperator, ISNULL(CAST(dbo.couponMain.consumedDate AS varchar(50)), '尚未使用') AS consumedDate, dbo.couponType.score, CASE (dbo.couponType.ctype) WHEN 'LOC' THEN CAST(dbo.couponType.[money] AS varchar(50))  + '元' ELSE CAST(dbo.couponType.[money] AS varchar(50)) END AS money,dbo.couponType.article FROM dbo.cardInfo INNER JOIN dbo.couponMain ON dbo.cardInfo.card_Id = dbo.couponMain.vipID INNER JOIN dbo.couponType ON dbo.couponMain.tid = dbo.couponType.tid");
                        }
                        #endregion

                        #region 新加的
                        x = (int)so.sqlExecuteScalar("select COUNT(*) from INFORMATION_SCHEMA.VIEWS where TABLE_NAME=N'view_yyjf'");
                        if (x == 0)
                        {
                            so.sqlExcuteNonQuery("create view [dbo].[view_yyjf] as select vip.card_id,isnull(vip.points,0) as points,isnull((vip.points-isnull(sdjf.tscore,0)),0) as kyjf,isnull(sdjf.tscore,0) as tscore from cardinfo vip left join (select cm.vipid,sum(ct.score) tscore from couponMain cm left join coupontype ct on cm.tid=ct.tid where ((cm.consumed=0 or cm.deduction=1) and getdate()<cm.enddate)  group by cm.vipid) sdjf on sdjf.vipid = vip.card_id ");
                        }
                        #endregion

                        if (!Directory.Exists("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile"))
                        {
                            Directory.CreateDirectory("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile");
                        }

                        string fileAdd = "C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li.ToString() + ".txt";

                        #region//修改之后的代码
                        using (FileStream fs = new FileStream(fileAdd, FileMode.Create))//这里就是创建txt文件
                        {
                            byte[] brow = Encoding.GetEncoding("gb2312").GetBytes(t[4]);// Encoding.Default.GetBytes(t[3]);
                            BufferedStream bs = new BufferedStream(fs);
                            bs.Write(brow, 0, brow.Length);
                            bs.Flush();
                            bs.Close();
                            fs.Close();
                        }
                        #endregion

                        sqldt.Rows.Add(new object[] { "delete from cardinf ; BULK INSERT cardinf  " +
                        "  FROM 'C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li.ToString() + ".txt'" +
                        " WITH " +
                        " ( " +
                        " FIELDTERMINATOR = '', " +
                        " ROWTERMINATOR = '\\n'  " +
                        ")",false});

                        int ihas = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from sysobjects where id = object_id(N'[dbo].[BatchInsert]') and xtype='P'"));//判断该BatchInsert存储过程是否存在
                        if (ihas <= 0)
                        {
                            CreateProc.CreateProc cp = new CreateProc.CreateProc();
                            cp.CreateProcInsert();
                        }
                        sqldt.Rows.Add(new object[] { "empoxCancelBatchInsert", true });
                    }
                    #endregion

                    #region//这个里是存在datainfo表的情况
                    else
                    {
                        if (!Directory.Exists("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile"))
                        {
                            Directory.CreateDirectory("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile");
                        }

                        string fileAdd = "C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li + ".txt";
                        #region//修改之后的语句
                        using (FileStream fs = new FileStream(fileAdd, FileMode.Create))
                        {
                            byte[] brow = Encoding.GetEncoding("gb2312").GetBytes(t[4]);// Encoding.Default.GetBytes(t[3]);
                            BufferedStream bs = new BufferedStream(fs);
                            bs.Write(brow, 0, brow.Length);
                            bs.Flush();
                            bs.Close();
                            fs.Close();
                        }
                        #endregion

                        sqldt.Rows.Add(new object[]{"delete from cardinf ; BULK INSERT cardinf  " +
                        "  FROM 'C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li + ".txt'" +
                        " WITH " +
                        " ( " +
                        " FIELDTERMINATOR = '', " +
                        " ROWTERMINATOR = '\\n'  " +
                        ")",false});

                        int ihas = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from sysobjects where id = object_id(N'[dbo].[BatchUpdataInsert]') and xtype='P'"));
                        if (ihas <= 0)
                        {
                            CreateProc.CreateProc cp = new CreateProc.CreateProc();
                            string e = cp.CreateProcUPdataInsert();
                        }
                        sqldt.Rows.Add(new object[] { "empoxCancelBatchUpdataInsert", true });
                    }
                    #endregion
                    so.sqlExcuteNonQuery(sqldt, true);
                    //initDIVcolumn();
                }
                catch (Exception ex)
                {
                    Session["DownLoad_Error"] += "[VIP]接收数据过程出现错误.接收失败:" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "");
                }
            }
            else
            {
                Session["DownLoad_Error"] += "[VIP]没有接收到数据!";
            }
            //
            //TODO:下面的语句是否有多余的嫌疑
            //
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        #endregion
        else//不存在更新的数据
        {
            Session["DownLoad_Error"] += "[VIP]没有接收到数据!";
            //下面一条语句是否多余呢？
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        if (Session["DownLoad_Error"] != null)
            Response.Write(Session["DownLoad_Error"].ToString());
        else
            Response.Write("vipFinished");
        //更新VIP数量显示
        if (bytesr == null && (int)Session["vipCount"] == 0)
        {
            Session["vipCount"] = 0;
        }
        else
        {
            Session["vipCount"] = so.sqlExecuteScalar("select count(card_id) from cardInfo");//如果表不存在的话就会报错误
        }
        Response.End();
    }

    protected void Saler_DownLoad()
    {
        Session["DownLoad_Error"] = null;
        DateTime newDT = new DateTime(2010, 1, 1);
        TimeSpan sp = DateTime.Now - newDT;
        double li = sp.TotalMilliseconds;
        so.sqlExcuteNonQuery("insert into TempStor values('" + li + "','0x36',0,'','" + memberStatic.companyID + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')", false);
        Thread.Sleep(60000);
        byte[] bytesr = (byte[])so.sqlExecuteScalar("select data from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'");
        #region//数据不为空的情况
        if (bytesr != null)
        {
            string salerData = Encoding.GetEncoding("GB2312").GetString(bytesr);
            if (!string.IsNullOrEmpty(salerData))
            {
                try
                {
                    ArrayList sqlal = new ArrayList();
                    columnTable = new DataTable();
                    columnTable.Columns.Add("colName", typeof(string));
                    columnTable.Columns.Add("colType", typeof(string));
                    columnTable.Columns.Add("colLenth", typeof(string));
                    string[] t = salerData.Split(splitChar3[0]);
                    string[] columnInfo = t[2].Split(splitChar1[0]);
                    for (int i = 1; i < columnInfo.Length; i++)
                    {
                        DataRow myrow = columnTable.NewRow();
                        myrow.ItemArray = columnInfo[i].Split(splitChar2[0]);
                        columnTable.Rows.Add(myrow);
                    }
                    row = t[4].Split(splitChar1[0]);
                    columnCount = row[1].ToString().Split(splitChar2[0]).Length;
                    int cardPlace = Convert.ToInt32(t[1].ToString());
                    string dropTablesql = "if exists(select * from sysobjects where id = object_id(N'[dbo].[salerInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table salerInfo";
                    sqlal.Add(dropTablesql);
                    string createTablesql = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[salerInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table salerInfo(";
                    for (int i = 0; i < columnTable.Rows.Count; i++)
                    {
                        switch (columnTable.Rows[i][1].ToString())
                        {
                            case "int":
                            case "tinyint":
                            case "smallint":
                            case "bigint": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " int,";
                                break;
                            case "char":
                            case "varchar":
                            case "nchar": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " varchar(" + columnTable.Rows[i][2].ToString() + "),";
                                break;
                            case "datetime": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " datetime,";
                                break;
                            case "float":
                            case "double":
                            case "numeric":
                            case "money": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " float,";
                                break;
                            default: createTablesql += columnTable.Rows[i][0] + " varchar(1000),";
                                break;
                        }
                    }
                    createTablesql = createTablesql.Substring(0, createTablesql.Length - 1) + ")";
                    sqlal.Add(createTablesql);
                    string insertDataSql = "";
                    for (int i = 1; i < row.Length; i++)
                    {
                        insertDataSql = "insert into salerInfo values(";
                        column = row[i].ToString().Split(splitChar2[0]);
                        for (int m = 0; m < column.Length - 1; m++)
                        {
                            switch (columnTable.Rows[m][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint":
                                case "bool":
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": insertDataSql += column[m].ToString() + ",";
                                    break;
                                case "datetime": insertDataSql += column[m].ToString().ToLower() == "null" ? ("'',") : ("'" + Convert.ToDateTime(column[m].ToString()).ToString("yyyy-MM-dd") + "',");
                                    break;
                                default: insertDataSql += column[m].ToString().ToLower() == "null" ? ("'',") : "'" + column[m].ToString() + "',";
                                    break;
                            }
                        }
                        if (insertDataSql != "insert into salerInfo values(")
                        {
                            insertDataSql = insertDataSql.Substring(0, insertDataSql.Length - 1) + ")";
                            sqlal.Add(insertDataSql);
                        }
                    }
                    so.sqlExcuteNonQuery(sqlal, true);
                }
                catch (Exception ex)
                {
                    Session["DownLoad_Error"] += "[营业员]接收数据过程出现错误.接收失败:" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "");
                }
            }
            else
            {
                Session["DownLoad_Error"] += "[营业员]没有接收到数据!";
            }
            //
            //TODO:下面的语句是否多余呢？
            //
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        #endregion

        else//数据为空的情况
        {
            Session["DownLoad_Error"] += "[营业员]没有接收到数据!";
            //
            //TODO:下面的语句是否多余呢？
            //
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        if (Session["DownLoad_Error"] != null)
            Response.Write(Session["DownLoad_Error"].ToString());
        else
            Response.Write("salerFinished");
        Response.End();
    }

    protected void Shop_DownLoad()
    {

        Session["DownLoad_Error"] = null;
        DateTime newDT = new DateTime(2010, 1, 1);
        TimeSpan sp = DateTime.Now - newDT;
        double li = sp.TotalMilliseconds;
        so.sqlExcuteNonQuery("insert into TempStor values('" + li + "','0x38',0,'','" + memberStatic.companyID + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')", false);
        Thread.Sleep(60000);
        byte[] bytesr = (byte[])so.sqlExecuteScalar("select data from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'");
        if (bytesr != null)
        {
            string shopData = Encoding.GetEncoding("GB2312").GetString(bytesr);
            if (shopData != null && shopData != "")
            {
                try
                {
                    ArrayList sqlal = new ArrayList();
                    columnTable = new DataTable();
                    columnTable.Columns.Add("colName", typeof(string));
                    columnTable.Columns.Add("colType", typeof(string));
                    columnTable.Columns.Add("colLenth", typeof(string));
                    string[] t = shopData.Split(splitChar3[0]);
                    string[] columnInfo = t[2].Split(splitChar1[0]);
                    for (int i = 1; i < columnInfo.Length; i++)
                    {
                        DataRow myrow = columnTable.NewRow();
                        myrow.ItemArray = columnInfo[i].Split(splitChar2[0]);
                        columnTable.Rows.Add(myrow);
                    }
                    row = t[4].Split(splitChar1[0]);
                    columnCount = row[1].ToString().Split(splitChar2[0]).Length;
                    int cardPlace = Convert.ToInt32(t[1].ToString());

                    //
                    //TODO:为什么插入数据的时候一定要把原来的表删除然后从新建一个呢？
                    //
                    sqlal.Add("if exists(select * from sysobjects where id = object_id(N'[dbo].[shopInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table shopInfo");
                    string createTablesql = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[shopInfo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table shopInfo(";
                    for (int i = 0; i < columnTable.Rows.Count; i++)
                    {
                        switch (columnTable.Rows[i][1].ToString())
                        {
                            case "int":
                            case "tinyint":
                            case "smallint":
                            case "bigint": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " int,";
                                break;
                            case "char":
                            case "varchar":
                            case "nchar": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " varchar(" + columnTable.Rows[i][2].ToString() + "),";
                                break;
                            case "datetime": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " datetime,";
                                break;
                            case "float":
                            case "double":
                            case "numeric":
                            case "money": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " float,";
                                break;
                            default: createTablesql += columnTable.Rows[i][0] + " varchar(1000),";
                                break;
                        }
                    }
                    createTablesql = createTablesql.Substring(0, createTablesql.Length - 1) + ")";
                    sqlal.Add(createTablesql);
                    string insertDataSql = "";
                    for (int i = 1; i < row.Length; i++)
                    {
                        insertDataSql = "insert into shopInfo values(";
                        column = row[i].ToString().Split(splitChar2[0]);
                        for (int m = 0; m < column.Length - 1; m++)
                        {
                            switch (columnTable.Rows[m][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint":
                                case "bool":
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": insertDataSql += column[m].ToString() + ",";
                                    break;
                                case "datetime": insertDataSql += column[m].ToString().ToLower() == "null" ? ("'',") : ("'" + Convert.ToDateTime(column[m].ToString()).ToString("yyyy-MM-dd") + "',");
                                    break;
                                default: insertDataSql += column[m].ToString().ToLower() == "null" ? ("'',") : "'" + column[m].ToString() + "',";
                                    break;
                            }
                        }
                        if (insertDataSql != "insert into shopInfo values(")
                        {
                            insertDataSql = insertDataSql.Substring(0, insertDataSql.Length - 1) + ")";
                            sqlal.Add(insertDataSql);
                        }
                    }
                    so.sqlExcuteNonQuery(sqlal, true);
                }
                catch (Exception ex)
                {
                    Session["DownLoad_Error"] += "[门店]接收数据过程出现错误.接收失败:" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "");
                }
            }
            else
            {
                Session["DownLoad_Error"] += "[门店]没有接收到数据!";
            }
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        else
        {
            Session["DownLoad_Error"] += "[门店]没有接收到数据!";
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        if (Session["DownLoad_Error"] != null)
            Response.Write(Session["DownLoad_Error"].ToString());
        else
            Response.Write("shopFinished");
        Response.End();
    }
    /// <summary>
    /// 新增的用来下载vip卡号的
    /// </summary>
    protected void VIPCard_DownLoad()
    {
        Session["DownLoad_Error"] = null;
        DateTime newDT = new DateTime(2010, 1, 1);
        TimeSpan sp = DateTime.Now - newDT;
        double li = sp.TotalMilliseconds;
        //插入请求
        so.sqlExcuteNonQuery("insert into TempStor values('" + li + "','0x61',0,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','" + memberStatic.companyID + "',null)", false);
        Thread.Sleep(60000);
        //等待返回VIP数据及表信息
        byte[] bytesr = (byte[])so.sqlExecuteScalar("select data from TempReturn where SessionId='" + li +
            "' and companyid='" + memberStatic.companyID + "'");
        #region//存在要更新的数据
        if (bytesr != null)
        {
            string vipData = Encoding.GetEncoding("GB2312").GetString(bytesr);
            //启用事务的sql语句集合.
            DataTable sqldt = new DataTable();
            sqldt.Columns.Add("sql");
            sqldt.Columns.Add("proc");
            if (!string.IsNullOrEmpty(vipData))
            {
                try
                {
                    columnTable = new DataTable();
                    columnTable.Columns.Add("colName", typeof(string));
                    columnTable.Columns.Add("colType", typeof(string));
                    columnTable.Columns.Add("colLenth", typeof(string));
                    columnTable.Columns.Add("colCName", typeof(string));
                    string[] t = vipData.Split(splitChar3[0]);
                    string[] columnInfo = t[2].Split(splitChar1[0]);
                    for (int i = 1; i < columnInfo.Length; i++)
                    {
                        DataRow myrow = columnTable.NewRow();
                        myrow.ItemArray = columnInfo[i].Split(splitChar2[0]);
                        columnTable.Rows.Add(myrow);
                    }
                    row = t[4].Split(splitChar1[0]);
                    //columnCount = row[1].ToString().Split(splitChar2[0]).Length;
                    #region//看CardDataInfo是否有字段的信息数据
                    int j = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from CardDataInfo"));
                    if (j == 0)
                    {
                        for (int i = 0; i < columnTable.Rows.Count; i++)
                        {
                            //这条语句可能有问题
                            string insdata = "insert into CardDataInfo values('" + columnTable.Rows[i][0].ToString() +
                                "','" + columnTable.Rows[i][3].ToString() + "',";
                            switch (columnTable.Rows[i][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint": insdata += "'整数型',4,";
                                    break;
                                case "char":
                                case "varchar":
                                case "nchar": insdata += "'文本型'," + columnTable.Rows[i][2].ToString() + ",";
                                    break;
                                case "datetime": insdata += "'日期型',8,";
                                    break;
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": insdata += "'小数型',8,";
                                    break;
                                default: insdata += "'文本型',1000,";
                                    break;
                            }
                            insdata += "null)";
                            sqldt.Rows.Add(new object[] { insdata, false });
                        }

                    }
                    #endregion

                    #region//判断表VipSet是否存在，如果不存在就去创建
                    int x = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from sysobjects where name='VipSet' and type='U'"));
                    if (x == 0)
                    {
                        int cardPlace = Convert.ToInt32(t[1].ToString());
                        string createTablesql = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[VipSet]')" +
                            " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table VipSet(";
                        for (int i = 0; i < columnTable.Rows.Count; i++)
                        {
                            switch (columnTable.Rows[i][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " int,";
                                    break;
                                case "char":
                                case "varchar":
                                case "nchar": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " varchar(" + columnTable.Rows[i][2].ToString() + "),";
                                    break;
                                case "datetime": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " datetime,";
                                    break;
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": createTablesql += "[" + columnTable.Rows[i][0] + "]" + " float,";
                                    break;
                                default: createTablesql += columnTable.Rows[i][0] + " varchar(1000),";
                                    break;
                            }
                            if (i == cardPlace)
                                createTablesql = createTablesql.Substring(0, createTablesql.Length - 1) + " primary key not null,";
                        }
                        createTablesql = createTablesql.Substring(0, createTablesql.Length - 1);
                        createTablesql += ")";
                        sqldt.Rows.Add(new object[] { createTablesql, false });
                        //sqldt.Rows.Add(new object[] { "if not exists(select * from sysobjects where id = object_id(N'[dbo].[UD_Fileds]')"+
                        //    " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table UD_Fileds(card_id varchar(20) not null,"+
                        //    "is_Upload int default 0 not null,empox_favor bit default 1 not null,Iden_ID int identity(1,1) not null)",false});

                        string createTablesql_trans = "if not exists(select * from sysobjects where id = object_id(N'[dbo].[TempVipSet]')" +
                            " and OBJECTPROPERTY(id, N'IsUserTable') = 1) create table TempVipSet(";
                        for (int i = 0; i < columnTable.Rows.Count; i++)
                        {
                            switch (columnTable.Rows[i][1].ToString())
                            {
                                case "int":
                                case "tinyint":
                                case "smallint":
                                case "bigint": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " int,";
                                    break;
                                case "char":
                                case "varchar":
                                case "nchar": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " varchar(" + columnTable.Rows[i][2].ToString() + "),";
                                    break;
                                case "datetime": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " datetime,";
                                    break;
                                case "float":
                                case "double":
                                case "numeric":
                                case "money": createTablesql_trans += "[" + columnTable.Rows[i][0] + "]" + " float,";
                                    break;
                                default: createTablesql_trans += columnTable.Rows[i][0] + " varchar(1000),";
                                    break;
                            }
                            if (i == cardPlace)
                                createTablesql_trans = createTablesql_trans.Substring(0, createTablesql_trans.Length - 1) + " primary key not null,";
                        }
                        createTablesql_trans = createTablesql_trans.Substring(0, createTablesql_trans.Length - 1);
                        createTablesql_trans += ")";
                        sqldt.Rows.Add(new object[] { createTablesql_trans, false });

                        if (!Directory.Exists("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile"))
                        {
                            Directory.CreateDirectory("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile");
                        }

                        string fileAdd = "C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li.ToString() + ".txt";


                        #region//修改之后的代码
                        using (FileStream fs = new FileStream(fileAdd, FileMode.Create))
                        {
                            byte[] brow = Encoding.GetEncoding("gb2312").GetBytes(t[4]);// Encoding.Default.GetBytes(t[3]);
                            BufferedStream bs = new BufferedStream(fs);
                            bs.Write(brow, 0, brow.Length);
                            bs.Flush();
                            bs.Close();
                            fs.Close();
                        }
                        #endregion


                        sqldt.Rows.Add(new object[] { "delete from TempVipSet ; BULK INSERT TempVipSet  " +
                        "  FROM 'C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li.ToString() + ".txt'" +
                        " WITH " +
                        " ( " +
                        " FIELDTERMINATOR = '', " +
                        " ROWTERMINATOR = '\\n'  " +
                        ")",false});

                        int ihas = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from sysobjects where id = object_id(N'[dbo].[BatchVipCardInsert]') and xtype='P'"));//判断该BatchInsert存储过程是否存在
                        if (ihas <= 0)
                        {
                            CreateProc.CreateProc cp = new CreateProc.CreateProc();
                            cp.CreateVipCardInsertProc();
                        }
                        sqldt.Rows.Add(new object[] { "empoxCancelBatchVipCardInsert", true });
                    }
                    #endregion

                    #region//这个里是存在datainfo表的情况
                    else
                    {
                        if (!Directory.Exists("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile"))
                        {
                            Directory.CreateDirectory("C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile");
                        }

                        string fileAdd = "C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li + ".txt";
                        #region//重复性语句
                        //if (!File.Exists(fileAdd))
                        //{
                        //    using (FileStream fs = File.Create(fileAdd))
                        //    {
                        //        byte[] brow = Encoding.GetEncoding("gb2312").GetBytes(t[4]);// Encoding.Default.GetBytes(t[3]);
                        //        BufferedStream bs = new BufferedStream(fs);
                        //        bs.Write(brow, 0, brow.Length);
                        //        bs.Flush();
                        //        bs.Close();
                        //        fs.Close();
                        //    }
                        //}
                        //else
                        //{
                        //    using (FileStream fs = new FileStream("C:\\DMService\\" + memberStatic.clientDataBase +
                        //        "\\ImportFile\\" + li + ".txt", FileMode.Create))
                        //    {
                        //        byte[] brow = Encoding.GetEncoding("gb2312").GetBytes(t[4]);// Encoding.Default.GetBytes(t[3]);
                        //        BufferedStream bs = new BufferedStream(fs);
                        //        bs.Write(brow, 0, brow.Length);
                        //        bs.Flush();
                        //        bs.Close();
                        //        fs.Close();
                        //    }
                        //}
                        #endregion

                        //
                        //TODO:为什么要先将数据写入到txt文件中，然后再倒入数据中，为什么不能直接倒入数据库中呢？
                        //
                        #region//修改之后的语句
                        using (FileStream fs = new FileStream(fileAdd, FileMode.Create))
                        {
                            byte[] brow = Encoding.GetEncoding("gb2312").GetBytes(t[4]);// Encoding.Default.GetBytes(t[3]);
                            BufferedStream bs = new BufferedStream(fs);
                            bs.Write(brow, 0, brow.Length);
                            bs.Flush();
                            bs.Close();
                            fs.Close();
                        }
                        #endregion

                        sqldt.Rows.Add(new object[]{"delete from TempVipSet ; BULK INSERT TempVipSet  " +
                        "  FROM 'C:\\DMService\\" + memberStatic.clientDataBase + "\\ImportFile\\" + li + ".txt'" +
                        " WITH " +
                        " ( " +
                        " FIELDTERMINATOR = '', " +
                        " ROWTERMINATOR = '\\n'  " +
                        ")",false});

                        int ihas = Convert.ToInt32(so.sqlExecuteScalar("select count(*) from sysobjects where id = object_id(N'[dbo].[BatchVipCardUpdate]') and xtype='P'"));
                        if (ihas <= 0)
                        {
                            CreateProc.CreateProc cp = new CreateProc.CreateProc();
                            string e = cp.CreateVipCardUpdateInsertProc();
                        }
                        sqldt.Rows.Add(new object[] { "empoxCancelBatchVipCardUpdate", true });
                    }
                    #endregion
                    so.sqlExcuteNonQuery(sqldt, true);
                    //initDIVcolumn();
                }
                catch (Exception ex)
                {
                    Session["DownLoad_Error"] += "[VIPCard]接收数据过程出现错误.接收失败:" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "");
                }
            }
            else
            {
                Session["DownLoad_Error"] += "[VIPCard]没有接收到数据!";
            }
            //
            //TODO:下面的语句是否有多余的嫌疑
            //
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        #endregion
        else//不存在更新的数据
        {
            Session["DownLoad_Error"] += "[VIPCard]没有接收到数据!";
            //下面一条语句是否多余呢？
            so.sqlExcuteNonQuery("delete from TempReturn where SessionId='" + li + "' and companyid='" + memberStatic.companyID + "'", false);
        }
        if (Session["DownLoad_Error"] != null)
            Response.Write(Session["DownLoad_Error"].ToString());
        else
            Response.Write("vipCardFinished");
        //更新VIP数量显示
        //Session["vipCount"] = so.sqlExecuteScalar("select count(card_id) from cardInfo");
        Response.End();
    }
}
