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
///vipInterfaceApp 的摘要说明
/// </summary>
public class vipInterfaceApp
{
    XmlParseFunction xmlParse;
    SQL_Operate so;
    public vipInterfaceApp()
    {
        xmlParse = new XmlParseFunction();
        so = new SQL_Operate();
    }

    //VIP登录验证
    public string vipLogin(string xmlStr)
    {
        //<data vid="v00000001" pwd="abc"/>
        string returnStr = "";
        //声明返回xml
        string xmlNew = "<data/>";
        XmlDocument docNew = new XmlDocument();
        docNew.LoadXml(xmlNew);
        XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");
        XmlNode state = docNew.CreateNode(XmlNodeType.Element, "state", null);
        XmlNode message = docNew.CreateNode(XmlNodeType.Element, "message", null);
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");

            string vid = xmlParse.GetNodeAttribute(root, "vid");
            string pwd = xmlParse.GetNodeAttribute(root, "pwd");

            //驗證
            int i = Convert.ToInt16(so.sqlExecuteScalar("select count(card_id) from cardInfo where card_id='" + vid + "' and pwd='" + pwd + "'"));
            if (i == 1)
            {
                state.InnerText = "1";
                message.InnerText = "";
            }
            else
            {
                state.InnerText = "0";
                message.InnerText = "用户名或密码不正确.";
            }
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        {
            state.InnerText = "0";
            message.InnerText = ex.Message;
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        return returnStr;
    }

    //用户详细资料获取
    public string vipDetails(string xmlStr)
    {
        //<data vid="v00000001" />
        string returnStr = "";
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");

            string vid = xmlParse.GetNodeAttribute(root, "vid");
            //返回xml
            string xmlNew = "<data/>";
            XmlDocument docNew = new XmlDocument();
            docNew.LoadXml(xmlNew);
            XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");

            XmlNode card_Type = docNew.CreateNode(XmlNodeType.Element, "card_Type", null);
            XmlNode points = docNew.CreateNode(XmlNodeType.Element, "points", null);
            XmlNode sendClient = docNew.CreateNode(XmlNodeType.Element, "sendClient", null);
            XmlNode userAddress = docNew.CreateNode(XmlNodeType.Element, "userAddress", null);
            XmlNode userBirthday = docNew.CreateNode(XmlNodeType.Element, "userBirthday", null);
            XmlNode userEmail = docNew.CreateNode(XmlNodeType.Element, "userEmail", null);
            XmlNode userMobile = docNew.CreateNode(XmlNodeType.Element, "userMobile", null);
            XmlNode userName = docNew.CreateNode(XmlNodeType.Element, "userName", null);
            XmlNode userPhone = docNew.CreateNode(XmlNodeType.Element, "userPhone", null);
            XmlNode userPost = docNew.CreateNode(XmlNodeType.Element, "userPost", null);
            XmlNode userSex = docNew.CreateNode(XmlNodeType.Element, "userSex", null);

            DataTable dt = so.sqlExcuteQueryTable("select card_Type,points,sendClient,userAddress," +
                "userBirthday,userEmail,userMobile,userName,userPhone,userPost,userSex from cardInfo where card_id='" + vid + "'");

            card_Type.InnerText = dt.Rows[0][0] == null ? "" : dt.Rows[0][0].ToString();
            points.InnerText = dt.Rows[0][1] == null ? "" : dt.Rows[0][1].ToString();
            sendClient.InnerText = dt.Rows[0][2] == null ? "" : dt.Rows[0][2].ToString();
            userAddress.InnerText = dt.Rows[0][3] == null ? "" : dt.Rows[0][3].ToString();
            userBirthday.InnerText = dt.Rows[0][4] == null ? "" : dt.Rows[0][4].ToString();
            userEmail.InnerText = dt.Rows[0][5] == null ? "" : dt.Rows[0][5].ToString();
            userMobile.InnerText = dt.Rows[0][6] == null ? "" : dt.Rows[0][6].ToString();
            userName.InnerText = dt.Rows[0][7] == null ? "" : dt.Rows[0][7].ToString();
            userPhone.InnerText = dt.Rows[0][8] == null ? "" : dt.Rows[0][8].ToString();
            userPost.InnerText = dt.Rows[0][9] == null ? "" : dt.Rows[0][9].ToString();
            userSex.InnerText = dt.Rows[0][10] == null ? "" : dt.Rows[0][10].ToString();

            rootNew.AppendChild(card_Type);
            rootNew.AppendChild(points);
            rootNew.AppendChild(sendClient);
            rootNew.AppendChild(userAddress);
            rootNew.AppendChild(userBirthday);
            rootNew.AppendChild(userEmail);
            rootNew.AppendChild(userMobile);
            rootNew.AppendChild(userName);
            rootNew.AppendChild(userPhone);
            rootNew.AppendChild(userPost);
            rootNew.AppendChild(userSex);

            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        { returnStr = "[error]-" + ex.Message; }
        return returnStr;
    }

    //用户详细资料修改
    public string alterVipDetails(string xmlStr)
    {
        /*<?xml version='1.0' encoding='gb2312'?>
            <data >
         * <vid>卡号</vid>
            <userAddress>联系地址</userAddress>
            <userBirthday>生日</userBirthday >
            <userEmail>电子邮箱</userEmail>
            <userMobile>手机号码</userMobile>
            <userName>姓名</userName>
            <userPhone>家庭电话</userPhone>
            <userPost>邮编</userPost>
            <userSex>性别</userSex>
            </data></xml>*/

        string returnStr = "";
        //声明返回xml
        string xmlNew = "<data/>";
        XmlDocument docNew = new XmlDocument();
        docNew.LoadXml(xmlNew);
        XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");
        XmlNode state = docNew.CreateNode(XmlNodeType.Element, "state", null);
        XmlNode message = docNew.CreateNode(XmlNodeType.Element, "message", null);
        try
        {
            string vid = "", userAddress = "", userBirthday = "", userEmail = "", userMobile = "", userName = "", userPhone = "", userPost = "", userSex = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            XmlNodeList xnl = root.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                switch (xn.Name)
                {
                    case "vid": vid = xn.InnerText;
                        break;
                    case "userAddress": userAddress = xn.InnerText;
                        break;
                    case "userBirthday": userBirthday = xn.InnerText;
                        break;
                    case "userEmail": userEmail = xn.InnerText;
                        break;
                    case "userMobile": userMobile = xn.InnerText;
                        break;
                    case "userName": userName = xn.InnerText;
                        break;
                    case "userPhone": userPhone = xn.InnerText;
                        break;
                    case "userPost": userPost = xn.InnerText;
                        break;
                    case "userSex": userSex = xn.InnerText;
                        break;
                }
            }
            //驗證
            int i = Convert.ToInt16(so.sqlExcuteNonQueryInt("update cardInfo set userAddress='" + userAddress + "',userBirthday='" + userBirthday +
                "',userEmail='" + userEmail + "',userMobile='" + userMobile + "',userName='" + userName + "',userPhone='" + userPhone +
                "',userPost='" + userPost + "',userSex='" + userSex + "' where card_id='" + vid + "'"));
            if (i == 1)
            {
                state.InnerText = "1";
                message.InnerText = "";
            }
            else
            {
                state.InnerText = "0";
                message.InnerText = "修改资料失败,请稍后再试.";
            }
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        {
            state.InnerText = "0";
            message.InnerText = ex.Message;
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        return returnStr;
    }

    //修改密码
    public string alterVipPwd(string xmlStr)
    {
        //<data vid="v00000001"  oldpwd=”123”  newpwd="abc"/>
        string returnStr = "";
        //声明返回xml
        string xmlNew = "<data/>";
        XmlDocument docNew = new XmlDocument();
        docNew.LoadXml(xmlNew);
        XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");
        XmlNode state = docNew.CreateNode(XmlNodeType.Element, "state", null);
        XmlNode message = docNew.CreateNode(XmlNodeType.Element, "message", null);
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");
            string vid = xmlParse.GetNodeAttribute(root, "vid");
            string oldpwd = xmlParse.GetNodeAttribute(root, "oldpwd");
            string newpwd = xmlParse.GetNodeAttribute(root, "newpwd");

            //驗證
            string opwd = so.sqlExecuteScalar("select pwd from cardInfo where card_id='" + vid + "'").ToString();
            if (opwd != oldpwd)
            {
                state.InnerText = "0";
                message.InnerText = "原始密码不正确";
            }
            else
            {
                state.InnerText = "0";
                message.InnerText = "修改资料失败,请稍后再试.";
            }
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        {
            state.InnerText = "0";
            message.InnerText = ex.Message;
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        return returnStr;
    }

    //积分换礼类别获取
    public string getCouponType()
    {
        string returnStr = "";
        try
        {
            //返回xml
            string xmlNew = "<data/>";
            XmlDocument docNew = new XmlDocument();
            docNew.LoadXml(xmlNew);
            XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");

            DataTable dt = so.sqlExcuteQueryTable("select tid,score from couponType where usingState=1");

            foreach (DataRow dr in dt.Rows)
            {
                XmlNode type = docNew.CreateNode(XmlNodeType.Element, "type", null);
                XmlAttribute tid = docNew.CreateAttribute("tid");
                XmlAttribute points = docNew.CreateAttribute("points");
                tid.Value = dr[0].ToString();
                points.Value = dr[1].ToString();

                type.Attributes.Append(tid);
                type.Attributes.Append(points);

                rootNew.AppendChild(type);
            }
            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        { returnStr = "[error]-" + ex.Message; }
        return returnStr;
    }

    //积分换礼下单
    public string couponConsume(string xmlStr)
    {
        //<data vid="v00000001"  tid=”102”  endDate="2011-12-12"/>
        string returnStr = "";
        //声明返回xml
        string xmlNew = "<data/>";
        XmlDocument docNew = new XmlDocument();
        docNew.LoadXml(xmlNew);
        XmlNode rootNew = xmlParse.SelectXmlNode(docNew, "data");
        XmlNode state = docNew.CreateNode(XmlNodeType.Element, "state", null);
        XmlNode message = docNew.CreateNode(XmlNodeType.Element, "message", null);
        try
        {
            //申請
            if (applyCoupon(xmlStr))
            {
                state.InnerText = "1";
                message.InnerText = "";
            }
            else
            {
                state.InnerText = "0";
                message.InnerText = "积分换礼失败";
            }
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        catch (Exception ex)
        {
            state.InnerText = "0";
            message.InnerText = ex.Message;
            rootNew.AppendChild(state);
            rootNew.AppendChild(message);
            returnStr = docNew.InnerXml;
        }
        return returnStr;
    }

    private bool applyCoupon(string xmlStr)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlStr);
        XmlNode root = xmlParse.SelectXmlNode(doc, "data");

        string vid = xmlParse.GetNodeAttribute(root, "vid");
        string tid = xmlParse.GetNodeAttribute(root, "tid");
        string endDate = xmlParse.GetNodeAttribute(root, "endDate");
        #region 删除couponWeb的引用之后
        //couponWeb.couponService cs = new couponWeb.couponService();
        //cs.Url = memberStatic.webServiceUrl;
        //cs.Timeout = 60 * 1000;
        #endregion

        couponService cs = new couponService();

        //<data vid="v00000001" getDate="2009-01-01" endDate="2009-03-03 23:59:59" tid="100" />
        string innerXML = "<data vid=\"" + vid + "\" getDate=\"" + DateTime.Now.ToShortDateString() +
            "\" endDate=\"" + endDate + "\" tid=\"" + tid + "\"/>";
        string xml = cs.GetNewPCwithTid(innerXML);
        if (xml.IndexOf("error") >= 0)
        {
            throw new Exception(xml);
        }
        else
        {
            string x = "";
            string cid = "";
            XmlDocument rd = new XmlDocument();
            rd.LoadXml(xml);
            XmlNode r = xmlParse.SelectXmlNode(doc, "data");
            XmlNodeList xnl = r.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                if (xn.Name == "cidNode")
                {
                    cid = xn.InnerText;
                }
                if (xn.Name == "state")
                {
                    x = xn.InnerText;
                }
            }
            if (x == "" || x == null)
            {
                if (sendApplyMsg(cid))
                    return true;
                else
                    throw new Exception("申请成功,短信发送失败,请与客服联系.");
            }
            else
                throw new Exception("会员积分不足,兑换失败.");
        }
    }

    private bool sendApplyMsg(string cid)
    {
        try
        {
            string sql = "select cm.vipId,ct.money from couponMain as cm inner join couponType as ct on cm.tid=ct.tid where cm.cid='" + cid + "'";
            DataTable dt = so.sqlExcuteQueryTable(sql);
            string vid = dt.Rows[0][0].ToString();
            string money = dt.Rows[0][1].ToString();
            string msg = "尊敬的顾客,您卡号为：[" + vid + "]的VIP卡积分兑换成功，优惠券编号：[" + cid + "]，面值[" + money + "]，请妥善保管并及时使用。";
            sql = "select userMobile from cardInfo where card_id='" + vid + "'";
            string mobile = (string)so.sqlExecuteScalar(sql);
            sql = "insert into sendDate values('" + mobile + "','直复','" + msg + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',0,1)";
            so.sqlExcuteNonQuery(sql, false);
            return true;
        }
        catch (Exception ex)
        { return false; }
    }
}