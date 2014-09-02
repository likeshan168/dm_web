using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
/// <summary>
///VipClientOperate 的摘要说明(编写：李克善)
/// </summary>
public class VipClientOperate
{
    private string sqlStr = string.Empty;
    private SQL_Operate SO = null;
    public VipClientOperate()
    {
        SO = new SQL_Operate();
    }
    /// <summary>
    /// 验证vip客户登录，并获取vip客户的名称
    /// </summary>
    /// <param name="clientInfo">vip客户信息</param>
    /// <param name="flag">标志是用手机号进行登录的，还是用卡号进行登录的</param>
    /// <returns>客户姓名</returns>
    public DataTable ValidateVipClientLogOn(string countID, string pwd)
    {
        sqlStr = SqlStrHelper.VipClientLoginSqlStr(countID, pwd);
        try
        {
            //string userName = (string)SO.sqlExecuteScalar(sqlStr);
            return SO.sqlExcuteQueryTable(sqlStr);


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 获取可用的vip卡号
    /// </summary>
    /// <returns></returns>
    public string GetAvaliableCardID()
    {
        sqlStr = SqlStrHelper.GVipIDSqlStr();
        try
        {
            ArrayList ar = SO.sqlExcuteQueryList(SqlStrHelper.GVipIDSqlStr());
            if (ar != null && ar.Count > 0)
            {
                return ar[0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 注册vip卡号
    /// </summary>
    /// <param name="vipClientInfo">vip信息</param>
    /// <returns></returns>
    public int RVipClientInfo(VipClientInfo vipClientInfo)
    {
        sqlStr = SqlStrHelper.RVipClientInfoSqlStr(vipClientInfo);
        try
        {
            return SO.sqlExcuteNonQueryInt(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 验证用户手机是否已经被注册
    /// </summary>
    /// <param name="mobile">手机号</param>
    /// <returns></returns>
    public bool VVipMobile(string mobile)
    {
        sqlStr = SqlStrHelper.VUserMobileExistsSqlStr(mobile);
        try
        {
            return (int)SO.sqlExecuteScalar(sqlStr) > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 将那些状态已经改变，但是还没有被申请的卡号恢复为可用的状态
    /// </summary>
    public void ReductionCardNo()
    {
        sqlStr = SqlStrHelper.ReductionCardNoSqlStr();
        try
        {
            SO.sqlExcuteNonQuery(sqlStr, false);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 根据卡号或者手机号获取vip客户的信息
    /// </summary>
    /// <param name="vipIDOrMobile">卡号或者手机号</param>
    /// <param name="flag">标志是手机号还是卡号</param>
    /// <returns></returns>
    public DataTable GetVipClientInfoByIDOrMobile(string countID)
    {
        sqlStr = SqlStrHelper.GVipClientInfoSqlStr(countID);
        try
        {
            return SO.sqlExcuteQueryTable(sqlStr);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 根据卡号或者手机号更新vip客户资料
    /// </summary>
    /// <param name="vipClientInfo">vip客户资料</param>
    /// <param name="vipIdOrMobile">手机号或者卡号</param>
    /// <param name="flag">标志是卡号还是手机号</param>
    /// <returns></returns>
    public int UpdateVipCardInfo(VipClientInfo vipClientInfo, string vipID)
    {
        sqlStr = SqlStrHelper.UCardInfoTbSqlStr(vipClientInfo, vipID);
        try
        {
            return SO.sqlExcuteNonQueryInt(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 获取可用的优惠劵类型
    /// </summary>
    /// <returns></returns>
    public DataTable GetValiableCounponType()
    {
        sqlStr = SqlStrHelper.GValiableCouponTypeSqlStr();
        try
        {
            return SO.sqlExcuteQueryTable(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 根据vip卡号或者手机号获取积分
    /// </summary>
    /// <param name="vipIdOrMobile">vip卡号或者手机号</param>
    /// <param name="flag">手机号或者卡号</param>
    /// <returns></returns>
    public string GetVipPoints(string vipId)
    {
        sqlStr = SqlStrHelper.GVipPointsSqlStr(vipId);
        try
        {
            DataTable dt = SO.sqlExcuteQueryTable(sqlStr);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                if (string.IsNullOrEmpty(dr[0].ToString()))
                {
                    return "0";
                }
                else
                {
                    return dr[0].ToString();
                }
            }
            else
            {
                return "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 验证卡号是否被注册
    /// </summary>
    /// <param name="cardID">vip卡号</param>
    /// <returns></returns>
    public int ValidateVipCardIDRegistered(string cardID)
    {
        sqlStr = SqlStrHelper.VVipCardIDRegistered(cardID);
        try
        {
            return (int)SO.sqlExecuteScalar(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 验证卡号是否存在且未使用，且未过期
    /// </summary>
    /// <param name="cardID"></param>
    /// <returns></returns>
    public object ValidateVipCardIDExists(string cardID)
    {
        sqlStr = SqlStrHelper.VVipCardIDExists(cardID);
        try
        {
            return SO.sqlExecuteScalar(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 验证vip卡表里是否有数据，如果没有数据的话，就自己填写vip卡号进行注册
    /// </summary>
    /// <returns></returns>
    public int ValidateVipCardDataNull()
    {
        sqlStr = SqlStrHelper.VVipCardIDDataNull();
        try
        {
            return (int)SO.sqlExecuteScalar(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
    /// <summary>
    /// 返回vip卡类型信息
    /// </summary>
    /// <returns></returns>
    public DataTable GetVipCardTypeInfo()
    {
        sqlStr = SqlStrHelper.GetVipCardTypeInfo();
        try
        {
            return SO.sqlExcuteQueryTable(sqlStr);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message,ex);
        }
    }
}