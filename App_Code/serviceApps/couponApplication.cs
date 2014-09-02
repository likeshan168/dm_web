using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
///couponApplication 的摘要说明
/// </summary>
public class couponApplication
{
    XmlParseFunction xmlParse;
    SQL_Operate so;

    public couponApplication()
    {
        xmlParse = new XmlParseFunction();
        so = new SQL_Operate();
    }

    public string GetNewPCwithTID(string xmlStr)
    {
        //<data vid="v00000001" getDate="2009-01-01" endDate="2009-03-03 23:59:59" tid="100" />
        string returnStr = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");

            string vid = xmlParse.GetNodeAttribute(root, "vid");
            //DateTime endDate = Convert.ToDateTime(xmlParse.GetNodeAttribute(root, "endDate")).AddHours(23).AddMinutes(59).AddSeconds(59);
            //DateTime getDate = Convert.ToDateTime(xmlParse.GetNodeAttribute(root, "getDate"));
            string endDate = Convert.ToDateTime(xmlParse.GetNodeAttribute(root, "endDate")).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("yyyy-MM-dd hh:mm:ss");
            string getDate = xmlParse.GetNodeAttribute(root, "getDate");
            //string endDate = ed;
            int tid = Convert.ToInt16(xmlParse.GetNodeAttribute(root, "tid"));
            XmlDocument xml = new XmlDocument();
            //返回xml
            string xmlNew = "<data/>";
            XmlDocument docNew = new XmlDocument();
            docNew.LoadXml(xmlNew);
            XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");
            //积分验证
            #region
            if (scoreValidate(vid, tid.ToString()))
            {
                string cid = getNewCid();//001046866
                string sql = "insert into couponMain values(" + tid + ",'" + cid + "','" + vid + "','" +
                    getDate + "','" + endDate + "',0,0,'网站发放',null,null,null)";
                so.sqlExcuteNonQuery(sql, false);
                string sql2 = "select * from couponMain where cid='" + cid + "'";
                DataTable mydt = so.sqlExcuteQueryTable(sql2);


                DataRow dr = mydt.Rows[0];
                XmlNode cTypeNode = docNew.CreateNode(XmlNodeType.Element, "cType", null);

                XmlNode cidNode = docNew.CreateNode(XmlNodeType.Element, "cid", null);

                XmlNode proposerNode = docNew.CreateNode(XmlNodeType.Element, "proposer", null);

                XmlNode applyTimeNode = docNew.CreateNode(XmlNodeType.Element, "applyTime", null);

                XmlNode isUseNode = docNew.CreateNode(XmlNodeType.Element, "isUse", null);

                XmlNode isComrNode = docNew.CreateNode(XmlNodeType.Element, "isCom", null);

                XmlNode stateNode = docNew.CreateNode(XmlNodeType.Element, "state", null);


                cTypeNode.InnerText = dr[0].ToString();//3
                cidNode.InnerText = dr[1].ToString();//001046866优惠劵编号
                rootNew.AppendChild(cTypeNode);
                rootNew.AppendChild(cidNode);
                rootNew.AppendChild(proposerNode);
                rootNew.AppendChild(applyTimeNode);
                rootNew.AppendChild(isUseNode);
                rootNew.AppendChild(isComrNode);
                rootNew.AppendChild(stateNode);
            }
            #endregion
            else
            {
                XmlNode stateNode = docNew.CreateNode(XmlNodeType.Element, "state", null);
                stateNode.InnerText = "失败,可用积分不足.";
                rootNew.AppendChild(stateNode);
            }
            returnStr = docNew.InnerXml;//<data><cType>3</cType><cid>001046866</cid><proposer /><applyTime /><isUse /><isCom /><state /></data>
        }
        catch (Exception ex)
        {
            //returnStr = "[error]-" + ex.Message;
            throw new Exception(ex.Message, ex);
        }
        return returnStr;
    }

    public string GetAllPCwithTID(string insql, string xmlStr)
    {
        //<data vid="v00000001" getDate="2009-01-01" endDate="2009-03-03 23:59:59" tid="100" />
        string returnStr = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");

            string vid = xmlParse.GetNodeAttribute(root, "vid");
            DateTime endDate = Convert.ToDateTime(xmlParse.GetNodeAttribute(root, "endDate"));
            DateTime getDate = Convert.ToDateTime(xmlParse.GetNodeAttribute(root, "getDate"));
            //string endDate = ed;
            int tid = Convert.ToInt16(xmlParse.GetNodeAttribute(root, "tid"));

            //形成输入参数表
            DateTime newDT = new DateTime(2010, 1, 1);
            TimeSpan sp = DateTime.Now - newDT;
            double li = sp.TotalMilliseconds;
            DataTable innerTable = new DataTable();
            innerTable.Columns.Add("data", typeof(string));
            innerTable.Columns.Add("value", typeof(object));
            innerTable.Columns.Add("type", typeof(string));
            innerTable.Rows.Add(new object[] { "@vipsql", insql, "varchar" });
            innerTable.Rows.Add(new object[] { "@tid", tid, "int" });
            innerTable.Rows.Add(new object[] { "@ydm", li.ToString(), "varchar" });
            innerTable.Rows.Add(new object[] { "@begT", getDate, "varchar" });
            innerTable.Rows.Add(new object[] { "@endT", endDate.ToString("yyyy-MM-dd"), "varchar" });
            //形成输出参数表
            DataTable outTable = new DataTable();
            outTable.Columns.Add("data", typeof(string));
            outTable.Columns.Add("type", typeof(string));
            outTable.Rows.Add(new object[] { "@zzt", "int" });
            outTable.Rows.Add(new object[] { "@sbsl", "int" });
            //执行存储过程
            DataSet newDS = so.sqlExcuteQueryDataSet("couponProc", innerTable, outTable);
            DataTable odt = newDS.Tables[1];
            if (odt.Rows[0][0].ToString() == "1" && odt.Rows[1][0].ToString() == "0")
                returnStr = "本次申请全部成功.";
            else if (odt.Rows[0][0].ToString() == "1" && Convert.ToInt32(odt.Rows[1][0]) > 0)
                returnStr = "申请完成,申请失败" + odt.Rows[1][0].ToString() + "个,原因:积分不足.";
            else
                returnStr = "申请失败,剩余优惠券数量不足.";
        }
        catch (Exception ex)
        {
            returnStr = "[GetNewCoupon]-" + ex.Message;
        }
        return returnStr;
    }

    public string GetNewPCwithPOS(string xmlStr)
    {
        //<?xml version='1.0' encoding='gb2312'?><data proposer="姓名" vip="卡号" companyId="公司ID" clientId="店铺ID" aDate="申请时间" money="面值" point="积分" ></data>
        string returnStr = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            string money = xmlParse.GetNodeAttribute(root, "money");
            string point = xmlParse.GetNodeAttribute(root, "point");
            string vid = xmlParse.GetNodeAttribute(root, "vip");
            string clientId = xmlParse.GetNodeAttribute(root, "clientId");
            DateTime endDate = DateTime.Now.AddDays(29).AddHours(23).AddMinutes(59).AddSeconds(59);
            DateTime getDate = DateTime.Now;
            //类型匹配验证
            so = new SQL_Operate();
            int temp = (int)so.sqlExecuteScalar("select tid from couponType where score=" + point + " and money=" + money);
            string tid = temp.ToString();
            if (tid != null)
            {
                if (scoreValidate(vid, tid.ToString()))
                {
                    string cid = getNewCid();
                    object clientName = so.sqlExecuteScalar("select clientName from shopInfo where clientId='" + clientId + "'");
                    string sql = "insert into couponMain values(" + tid + ",'" + cid + "','" + vid + "','" +
                        getDate + "','" + endDate + "',0,0,'" + clientName + "',null,null,null)";
                    so.sqlExcuteNonQuery(sql, false);
                    string sql2 = "select * from couponMain where cid='" + cid + "'";
                    DataTable mydt = so.sqlExcuteQueryTable(sql2);
                    DataSet myds = new DataSet();
                    myds.Tables.Add(mydt);
                    returnStr = myds.GetXml();
                }
                else
                {
                    DataSet myds = new DataSet();
                    returnStr = myds.GetXml();
                }
            }
            else
            {
                DataSet myds = new DataSet();
                returnStr = myds.GetXml();
            }
        }
        catch (Exception ex)
        {
            returnStr = "[error]-" + ex.Message;
        }
        return returnStr;
    }

    public string getCouponType()
    {
        string returnStr = "";
        try
        {
            string sql = "select ct.tid as '类型编号',ct.ctype as '主类型',ct.typeDetails as '详细类型'," +
                    "cp.pname as '使用地点',ct.score as '所需积分',ct.money as '对应金额',ct.article as '对应物品'" +
                    " from couponType as ct inner join couponPlace as cp on ct.usedPlace=cp.pid where ct.displayed=1";
            DataSet ds = so.sqlExcuteQueryTable(sql).DataSet;
            returnStr = ds.GetXml();
        }
        catch (Exception ex)
        {
            returnStr = "[getCouponType]-" + ex.Message;
        }
        return returnStr;
    }

    private string getNewCid()
    {
        DateTime newDT = new DateTime(2010, 1, 1);
        TimeSpan sp = DateTime.Now - newDT;
        double li = sp.TotalMilliseconds;
        try
        {
            so.sqlExcuteNonQuery("update lants_Coupon set userid ='" + li.ToString() +
                "' where couponid in (select top 1 couponid from lants_Coupon where isused = 0 and userid='0')", false);
            object cid = so.sqlExecuteScalar("select top 1 couponid from lants_Coupon where userid='" + li.ToString() + "'");
            if (cid == null || cid.ToString() == "")
            {
                throw new Exception("优惠券数量不足");
            }
            else
            {
                so.sqlExcuteNonQuery("update lants_Coupon set isused=1 where userid='" + li.ToString() + "'", false);
                return cid.ToString();
            }
        }
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// 积分验证
    /// </summary>
    /// <param name="vipID">会员卡号</param>
    /// <param name="tid">优惠券编号</param>
    /// <returns></returns>
    private bool scoreValidate(string vipID, string tid)//3033399246,3
    {
        //目前剩余积分
        DataTable dt = so.sqlExcuteQueryTable("select points from cardInfo where card_id='" + vipID + "'");
        int score = 0;
        if (dt != null && dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
            {
                score = Int32.Parse(dt.Rows[0][0].ToString());
            }
        }

        //已申请没消费的vip列表
        DataTable mydt = so.sqlExcuteQueryTable("select cm.vipID,ct.score from couponMain as cm inner join " +
            "couponType as ct on cm.tid=ct.tid where cm.deduction=0 and endDate>='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'");
        //此次申请需扣减积分
        decimal fs = Convert.ToDecimal(so.sqlExecuteScalar("select score from couponType where tid=" + tid));
        //比较并扣减未消费积分
        decimal addscore = 0;
        DataRow[] drs = mydt.Select("vipID='" + vipID + "'");
        if (drs.Length > 0)
        {
            foreach (DataRow dr in drs)
                addscore += Convert.ToDecimal(dr[1]);
        }
        if (score - addscore >= fs)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 消费验证
    /// </summary>
    /// <param name="xmlStr"></param>
    /// <returns></returns>
    public string consumeValidate(string xmlStr)
    {
        //<data cid="14949218" companyId="00006"/>
        string returnStr = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            string companyId = xmlParse.GetNodeAttribute(root, "companyId");
            string cid = xmlParse.GetNodeAttribute(root, "cid");

            string sql = "select ct.money,cm.consumed,cm.endDate,cm.vipID from couponMain as cm inner join couponType as ct " +
                "on cm.tid=ct.tid where cm.cid='" + cid + "'";
            DataTable dt = so.sqlExcuteQueryTable(sql);


            //返回xml
            string xmlNew = "<data/>";
            XmlDocument docNew = new XmlDocument();
            docNew.LoadXml(xmlNew);
            XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");

            XmlNode cidNode = docNew.CreateNode(XmlNodeType.Element, "cid", null);

            XmlNode moneyNode = docNew.CreateNode(XmlNodeType.Element, "money", null);

            XmlNode eDateNode = docNew.CreateNode(XmlNodeType.Element, "eDate", null);

            XmlNode stateNode = docNew.CreateNode(XmlNodeType.Element, "state", null);

            XmlNode vidNode = docNew.CreateNode(XmlNodeType.Element, "vid", null);

            XmlNode proposerNode = docNew.CreateNode(XmlNodeType.Element, "proposer", null);

            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                cidNode.InnerText = cid;
                moneyNode.InnerText = dr[0].ToString();
                eDateNode.InnerText = Convert.ToDateTime(dr[2].ToString()).ToShortDateString();
                vidNode.InnerText = dr[3].ToString();
                proposerNode.InnerText = "";

                if (dr[1].ToString().ToLower() == "true")
                    stateNode.InnerText = "3";
                else if ((DateTime)dt.Rows[0][2] < DateTime.Now)
                    stateNode.InnerText = "2";
                else
                    stateNode.InnerText = "1";
            }
            else//不存在
            {
                cidNode.InnerText = cid;
                moneyNode.InnerText = "0";
                eDateNode.InnerText = "";
                stateNode.InnerText = "4";
                vidNode.InnerText = "";
                proposerNode.InnerText = "";
            }
            rootNew.AppendChild(cidNode);
            rootNew.AppendChild(moneyNode);
            rootNew.AppendChild(eDateNode);
            rootNew.AppendChild(vidNode);
            rootNew.AppendChild(proposerNode);
            rootNew.AppendChild(stateNode);

            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnStr;
    }

    public string ConsumePC(string xmlStr)
    {
        //<data cid="67032000" companyId="00006" mobileId="4600XXXXXX" clientId="S001" cDate="2008-03-05 10:20:14"/>
        string returnStr = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            string companyId = xmlParse.GetNodeAttribute(root, "companyId");
            string cid = xmlParse.GetNodeAttribute(root, "cid");
            DateTime ConsumeTime = Convert.ToDateTime(xmlParse.GetNodeAttribute(root, "cDate"));
            string Mobile_no = xmlParse.GetNodeAttribute(root, "mobileId");
            string Client_id = xmlParse.GetNodeAttribute(root, "clientId");

            string sql = "select ct.money,cm.consumed,cm.endDate,cm.vipID,ct.score from couponMain as cm inner join couponType as ct " +
                "on cm.tid=ct.tid where cm.cid='" + cid + "'";
            DataTable dt = so.sqlExcuteQueryTable(sql);

            //返回xml
            string xmlNew = "<data/>";
            XmlDocument docNew = new XmlDocument();
            docNew.LoadXml(xmlNew);
            XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");

            XmlNode cidNode = docNew.CreateNode(XmlNodeType.Element, "cid", null);

            XmlNode stateNode = docNew.CreateNode(XmlNodeType.Element, "state", null);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    cidNode.InnerText = cid;

                    if (dr[1].ToString().ToLower() == "true")
                        stateNode.InnerText = "3";
                    else if ((DateTime)dt.Rows[0][2] < DateTime.Now)
                        stateNode.InnerText = "2";
                    else
                    {
                        string mobile = so.sqlExecuteScalar("select userMobile from cardInfo where card_id='" + dr[3].ToString() + "'").ToString();
                        string sqlStr = "insert into lpc_ActionLog(autoId,companyID,lpcID,vipId,mobileNo,aId,state,point,par_Value,isOK,errInfo,sendType,email) " +
                                "values(0,'" + companyId + "','" + cid + "','" + dr[3] +
                                "','" + mobile + "',0,0," + dr[4] + "," + dr[0] + ",0,'',0,'')";
                        so.sqlExcuteNonQuery(sqlStr, false);
                        object clientName = so.sqlExecuteScalar("select clientName from shopInfo where clientId='" + Client_id + "'");
                        sqlStr = "update couponMain set consumed=1,consumePlace='" + clientName + "' where cid='" + cid + "'";
                        so.sqlExcuteNonQuery(sqlStr, false);
                        stateNode.InnerText = "1";
                    }
                }
            }
            else//不存在
            {
                cidNode.InnerText = cid;
                stateNode.InnerText = "4";
            }
            rootNew.AppendChild(cidNode);
            rootNew.AppendChild(stateNode);

            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return returnStr;
    }

    public bool csmSuccess(string xmlStr)
    {
        try
        {
            //string vipID, string cid, decimal score
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            string vipID = xmlParse.GetNodeAttribute(root, "vipID");
            string cid = xmlParse.GetNodeAttribute(root, "cid");
            string score = xmlParse.GetNodeAttribute(root, "score");
            string state = xmlParse.GetNodeAttribute(root, "state");
            string reason = xmlParse.GetNodeAttribute(root, "reason");

            ArrayList al = new ArrayList();
            al.Add("update couponMain set deduction=1 where cid='" + cid + "'");
            al.Add("update cardInfo set points=points-" + score + " where card_id='" + vipID + "'");
            al.Add("update lpc_ActionLog set isOK=" + state + " ,errInfo='" + reason + "' where lpcid='" + cid + "'");
            so.sqlExcuteNonQuery(al, true);
            return true;
        }
        catch (Exception ex)
        { throw ex; }
    }
    /// <summary>
    /// 获取需要发送的开卡，销售短信的内容
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public DataSet GetZF_SMS_DATA(string companyId)
    {
        DataSet ds = new DataSet();
        try
        {
            string sqlStr = "select top 20 * from lpc_zfsms_table where companyId='" + companyId + "' and state=0 order by instime";
            ds.Tables.Add(so.sqlExcuteQueryTable(sqlStr));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sqlStr = "update lpc_zfsms_table set state=1 where autoId in (select top " + ds.Tables[0].Rows.Count + " autoId from lpc_zfsms_table where companyId='" + companyId + "' and state=0 order by instime)";
                    so.sqlExcuteNonQuery(sqlStr, false);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds;
    }

    public string SetSMS_DATA_STATE(string companyId, string upID)
    {
        string resultStr = string.Empty;
        try
        {
            string sqlStr = "delete from lpc_zfsms_table where companyId='" + companyId + "'and state=1 and autoId in (" + upID + ")";
            so.sqlExcuteNonQuery(sqlStr, false);
            resultStr = "SUCCESS";
        }
        catch (Exception ex)
        {
            resultStr = "ERROR";
            throw ex;
        }
        return resultStr;
    }

    public string GetChangePointPC(string xmlStr)
    {
        //<data  companyId="00006" aId="1"/>
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            string companyId = xmlParse.GetNodeAttribute(root, "companyId");
            int aId = Convert.ToInt32(xmlParse.GetNodeAttribute(root, "aId"));

            string sqlStr = "select top 20 * from lpc_ActionLog where companyID='" + companyId + "' and  state=0";

            dt = so.sqlExcuteQueryTable(sqlStr);
            ds.Tables.Add(dt);
            if (dt != null && dt.Rows.Count > 0)
            {
                string whereStr = string.Empty;
                foreach (DataRow dr in dt.Rows)
                    whereStr += "'" + dr["autoId"].ToString() + "',";
                whereStr = whereStr.TrimEnd(',');
                sqlStr = "update lpc_ActionLog set state=1 where autoId in(" + whereStr + ")";
                so.sqlExcuteNonQuery(sqlStr, false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return ds.GetXml();
    }

    public string DelChangePointLog(string companyId, string upID)
    {
        string resultStr = string.Empty;
        try
        {
            string sqlStr = "delete from lpc_ActionLog where companyId='" + companyId + "'and state=1 and autoId in (" + upID + ")";
            so.sqlExcuteNonQuery(sqlStr, false);
            resultStr = "SUCCESS";
        }
        catch (Exception ex)
        {
            resultStr = "ERROR";
            throw ex;
        }
        return resultStr;
    }
    /// <summary>
    /// 通过pos机进行开卡（同样的道理我们也可以通过网站进行开卡）
    /// </summary>
    /// <param name="companyId"></param>
    /// <param name="xmlStr"></param>
    /// <returns></returns>
    public string PosVipOpenPC(string companyId, string xmlStr)
    {
        string resultStr = string.Empty;
        try
        {
            string sqlStr = "insert into lpc_zfsms_table (companyID,insTime,smsType,sysContent,state )" +
                                "values('" + companyId + "',getdate(),'开卡','" + xmlStr + "',0)";
            so.sqlExcuteNonQuery(sqlStr, false);
            return "开卡信息保存成功.";
        }
        catch (Exception ex)
        {
            resultStr = "保存直复开卡信息出错！";
            throw ex;
        }
    }

    public string SendSaleMsg(string companyId, string xmlStr)
    {
        string resultStr = string.Empty;

        try
        {
            string sqlStr = "insert into lpc_zfsms_table (companyID,insTime,smsType,sysContent,state )" +
                                "values('" + companyId + "',getdate(),'销售','" + xmlStr + "',0)";
            so.sqlExcuteNonQuery(sqlStr, false);
            return "销售信息保存成功.";
        }
        catch (Exception ex)
        {
            resultStr = "保存直复销售信息出错！";
            throw ex;
        }
    }

    public string SendLpcApplyResMsg(string companyId, string xmlStr)
    {
        string resultStr = string.Empty;
        try
        {
            string sqlStr = "insert into lpc_zfsms_table (companyID,insTime,smsType,sysContent,state )" +
                                "values('" + companyId + "',getdate(),'积分换礼','" + xmlStr + "',0)";
            so.sqlExcuteNonQuery(sqlStr, false);
            return "积分换礼信息保存成功.";
        }
        catch (Exception ex)
        {
            resultStr = "保存积分换礼信息出错！";
            throw ex;
        }
    }

    public string enabledCouponList()
    {
        string returnStr = "";
        try
        {
            DataTable dt = so.sqlExcuteQueryTable("select score,money from couponType where usingState=1");
            DataSet tds = new DataSet();
            tds.Tables.Add(dt);
            returnStr = tds.GetXml();
        }
        catch (Exception ex)
        { returnStr = "[error]-" + ex.Message; }
        return returnStr;
    }
}