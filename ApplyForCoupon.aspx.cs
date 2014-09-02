using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
public partial class ApplyForCoupon : System.Web.UI.Page
{
    /// <summary>
    /// vip姓名
    /// </summary>

    public DataTable dt = null;
    SQL_Operate so = new SQL_Operate();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {
            if (Session["VipName"] == null  || Cache["vipCardID"] == null)//先判断vip客户有没有登录
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "tip1", "location.href='VipClientLogin.aspx'", true);
                return;
            }
            RegularExp.VipName = Session["VipName"].ToString();
            
            #region //已经申请没有消费的积分
            DataTable mydt = so.sqlExcuteQueryTable("select cm.vipID,ct.score from couponMain as cm inner join " +
            "couponType as ct on cm.tid=ct.tid where cm.deduction=0 and endDate>='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' and cm.vipID='" + Cache["vipCardID"].ToString() + "'");
            decimal addscore = 0;
            DataRowCollection drs = mydt.Rows;
            if (drs.Count > 0)
            {
                foreach (DataRow dr in drs)
                    addscore += Convert.ToDecimal(dr[1]);
            }
            #endregion
            RegularExp.Points = (decimal.Parse(new VipClientOperate().GetVipPoints(Cache["vipCardID"].ToString())) - addscore).ToString();

            //第一次载入的时候，生成一个初始的标志
            if (null == Session["Token"])
            {
                SetToken();
            }
            BindData();
        }
        Response.Buffer = true;
        Response.ExpiresAbsolute = System.DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.AddHeader("Pragma", "No-Cache");
    }

    //生成标志，并保存到Session
    private void SetToken()
    {
        Session.Add("Token", Session.SessionID + DateTime.Now.Ticks.ToString());
    }
    //生成标志，并保存到Session
    //protected string UserMd5(string str)
    //{
    //    string cl1 = str;
    //    string pwd = "";
    //    MD5 md5 = MD5.Create();
    //    // 加密后是一个字节类型的数组
    //    byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));
    //    // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
    //    for (int i = 0; i < s.Length; i++)
    //    {
    //        // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
    //        pwd = pwd + s[i].ToString("X");
    //    }
    //    return pwd;
    //}
    //获得当前Session里保存的标志
    public string GetToken()
    {
        if (null != Session["Token"])
        {
            return Session["Token"].ToString();
        }
        else
        {
            return string.Empty;
        }
    }

    private void BindData()
    {
        try
        {
            if (Cache["couponType"] != null)//设置缓存提高效率，也为下面的程序提供数据，以减少访问数据库的次数
            {
                dt = (DataTable)Cache["couponType"];
            }
            else
            {
                dt = new VipClientOperate().GetValiableCounponType();
                Cache["couponType"] = dt;
            }
            repeater.DataSource = dt;
            repeater.DataBind();
        }
        catch (Exception ex)
        {
            RBKJEmpox_Management_System.Models.Log4netHelper.InvokeErrorLog(MethodBase.GetCurrentMethod().DeclaringType, "获取可有优惠劵类型出错", ex);
        }
    }


    protected void repeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (Request.Form.Get("hiddenStoken").Equals(GetToken()))
            {
                SetToken();//别忘了最后要更新Session中的标志
                if (e.CommandName == "applyCoupon")//申请优惠劵
                {
                    int tid = Int32.Parse(e.CommandArgument.ToString());

                    string vipCardID = Cache["vipCardID"].ToString();//这里报错
                    int result = couponApplication(tid, vipCardID);
                    if (result == 1)//返回1则申请成功
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "appSucc", "alert('申请成功！我们将以短信形式通知你');location.href='LoginSuccess.aspx';", true);
                    }
                    else if (result == 0)//返回0则是因积分不够不能申请
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "appMsg1", "alert('很抱歉！你的积分不够，暂时不能申请');location.href='LoginSuccess.aspx';", true);
                    }
                    else if (result == -1)//返回-1则是服务器繁忙
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "appMsg2", "alert('很抱歉！服务器繁忙，请稍后再申请！');location.href='LoginSuccess.aspx';", true);
                    }
                    else if (result == -2)//返回-2则是因优惠劵短息发送失败
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "appMsg3", "alert('申请失败！由于优惠劵短息发送失败！稍后再试！');location.href='LoginSuccess.aspx';", true);
                    }
                    else if (result == -3)//返回-3则由于程序出现异常
                    {
                        ClientScript.RegisterClientScriptBlock(GetType(), "appMsg4", "alert('申请失败！程序出现异常！稍后再试！');location.href='LoginSuccess.aspx';", true);
                    }
                }

            }
            else
            {
                return;
            }


        }
        catch (Exception ex)
        {
            RBKJEmpox_Management_System.Models.Log4netHelper.InvokeErrorLog(MethodBase.GetCurrentMethod().DeclaringType, "申请优惠劵出错", ex);
            ClientScript.RegisterClientScriptBlock(GetType(), "coupErr1", "alert('" + ex.Message + "');location.href='LoginSuccess.aspx';", true);
        }

    }
    /// <summary>
    /// 申请优惠劵，返回1申请成功，0积分不足，-1服务器繁忙，-2优惠劵短信发送失败，-3异常
    /// </summary>
    /// <param name="couponTypeID"></param>
    /// <param name="vipID"></param>
    /// <returns></returns>
    private int couponApplication(int couponTypeID, string vipID)
    {
        try
        {
            if (so.sqlExecuteScalar("select couponUsed from sysState").ToString().ToLower() == "true")
            {
                //throw new Exception("服务器繁忙,请稍后再试.");
                return -1;
            }
            couponService cs = new couponService();
            string input = "", endDate = "", tid = "", getDate = "";
            tid = couponTypeID.ToString();

            getDate = DateTime.Now.ToShortDateString();
            int month = DateTime.Now.Month;
            if (month >= 7 && month <= 8)
            {
                endDate = DateTime.Now.Year + "-8-31";
            }
            else if (month >= 12 || month <= 1)
            {
                endDate = DateTime.Now.Year + "-1-31";
            }

            string rs = "";

            ArrayList notSendCardid = new ArrayList();

            XmlDocument doc = new XmlDocument();
            XmlParseFunction xmlParse = new XmlParseFunction();


            input = "<data vid=\"" + vipID + "\" getDate=\"" + getDate + "\" endDate=\"" + endDate + "\" tid=\"" + tid + "\" />";

            rs = cs.GetNewPCwithTid(input);//返回的是xml字符串
            doc.LoadXml(rs);
            XmlNode root = xmlParse.SelectXmlNode(doc, "data");

            if (root.ChildNodes.Count == 1)//这里抛出异常（会导致session中的数据丢失）
            {
                //ClientScript.RegisterClientScriptBlock(GetType(), "tip2", "alert('积分不够！')", true);
                //throw new Exception("积分不够！不能申请该优惠劵！");
                return 0;

            }
            string r = sendCoupon(tid, vipID);
            if (r != "true")
            {
                //若发送方法执行失败,则删除所有刚申请的优惠券
                so.sqlExcuteNonQuery("delete from couponMain where getDate='" + getDate + "'", false);
                //throw new Exception("优惠券短信发送失败_" + r);
                return -2;
            }
            return 1;

        }
        catch { return -3; }

    }
    private string sendCoupon(string couponTypeID, string vipID)
    {
        try
        {
            string sql = "", mobile = "";
            sql = "select PRI from PRIsetting where smsType='直复'";
            string pri = so.sqlExecuteScalar(sql).ToString();
            string selsql = "select score,article from couponType where tid=" + couponTypeID;
            DataTable articleTable = so.sqlExcuteQueryTable(selsql);
            sql = "select cid,endDate from couponMain where vipID='" + vipID + "' and convert(varchar(20),[getDate],120) like '" + DateTime.Now.ToString("yyyy-MM-dd") + "%' order by [getDate] desc";
            DataTable dt = so.sqlExcuteQueryTable(sql);
            if (dt.Rows[0][0] != null && dt.Rows[0][0].ToString().Trim() != "")
            {
                sql = "select userMobile from cardInfo where card_id='" + vipID + "'";
                mobile = (string)so.sqlExecuteScalar(sql);
                if (mobile != null && mobile.Trim() != "")
                {
                    sql = "insert into sendData values('" + mobile + "','直复','尊敬的会员您好！卡号为" + vipID + "已有" + articleTable.Rows[0][0].ToString() +
                        "积分,可兑换" + articleTable.Rows[0][1].ToString() + ",请于" + Convert.ToDateTime(dt.Rows[0][1].ToString()).ToString("yyyy-MM-dd") + "前带上个人身份证凭此条短信和兑换密码"
                        + dt.Rows[0][0].ToString() + "到指定专卖店申请兑换。详询0755-86621896.','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "',0," + pri + ")";
                    so.sqlExcuteNonQuery(sql, false);
                }
            }

            return "true";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}