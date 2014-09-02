<%@ WebHandler Language="C#" Class="couponManager_Response" %>

using System;
using System.Web;

public class couponManager_Response : IHttpHandler
{

    SQL_Operate so;
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        if (context.Request.QueryString["type"] == "regType")
        {
            this.newTypeReg();
        }
        if (context.Request.QueryString["type"] == "delPC")
        {
            this.delPC();
        }
    }

    private void newTypeReg()
    {
        try
        {
            so = new SQL_Operate();
            string couponType = HttpContext.Current.Request.QueryString["couponType"];//优惠劵主类型GOODS
            string cdt = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["cdt"]);//优惠券详细类别likeshan
            string usedPlace = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["usedPlace"]);//使用场所 1
            string placeOther = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["placeOther"]);//自定义场所
            string score = HttpContext.Current.Request.QueryString["score"];//所需积分120
            string money = HttpContext.Current.Request.QueryString["money"] == "" ? "0" : HttpContext.Current.Request.QueryString["money"];//对应金额
            string article = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["article"]);//对应礼品 钱包
            string articode = HttpContext.Current.Server.UrlDecode(HttpContext.Current.Request.QueryString["articode"]);//礼品代码 110
            //
            //TODO:这个样的验证可以放在前台进行操作
            //
            #region//旧版
            //if (!(IntegerOrFloat(score) && IntegerOrFloat(money)))
            //{
            //    HttpContext.Current.Response.Write("积分或金额只能为数字,请重新输入.");
            //    HttpContext.Current.Response.End();
            //    return;
            //}
            #endregion

            if (couponType == "GOODS")
            {
                int ct = (int)so.sqlExecuteScalar("select count(tid) from couponType where [money]=" + articode);
                if (ct > 0)
                {
                    HttpContext.Current.Response.Write("礼品代码重复,请重新输入.");
                    HttpContext.Current.Response.End();
                    return;
                }
                else
                    money = articode;
            }

            if (usedPlace == "other")
            {
                int i = 0;
                i = Convert.ToInt32(so.sqlExecuteScalar("select pid from couponPlace where pname='" + placeOther + "'"));
                if (i == 0)
                {
                    i = so.sqlExcuteNonQueryInt("insert into couponPlace values('" + placeOther + "');SELECT SCOPE_IDENTITY() from couponPlace");
                }
                so.sqlExcuteNonQuery("insert into couponType values('" + couponType + "','" + cdt + "'," + i +
                    "," + score + "," + money + ",'" + article + "',1,0)", false);
            }
            else
            {
                so.sqlExcuteNonQuery("insert into couponType values('" + couponType + "','" + cdt + "'," + usedPlace +
                    "," + score + "," + money + ",'" + article + "',1,0)", false);
            }
            HttpContext.Current.Response.Write("success");
        }
        catch (Exception ex)
        { HttpContext.Current.Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { HttpContext.Current.Response.End(); }
    }

    private bool IntegerOrFloat(string sData)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(sData, @"[0-9]+\.?[0-9]*");
    }

    private void delPC()
    {
        string tid = HttpContext.Current.Request.QueryString["tid"];
        try
        {
            so = new SQL_Operate();
            string selsql = "select count(tid) from couponMain where tid=" + tid;
            int t = Convert.ToInt32(so.sqlExecuteScalar(selsql));
            if (t > 0)
            {
                string update = "update couponType set displayed=1,usingState=0 where tid=" + tid;
                so.sqlExcuteNonQuery(update, false);
                HttpContext.Current.Response.Write("此类型已被会员申请且未经使用,不能被删除.已自动归为停止发放并隐藏显示.')");
            }
            else
            {
                string delete = "delete from couponType where tid=" + tid;
                so.sqlExcuteNonQuery(delete, false);
                HttpContext.Current.Response.Write("success");
            }
        }
        catch (Exception ex)
        { HttpContext.Current.Response.Write(ex.Message.Replace("\r\n", "").Replace("'", "")); }
        finally { HttpContext.Current.Response.End(); }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}