using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///sql语句类库（李克善新加）
/// </summary>
public static class SqlStrHelper
{

    /// <summary>
    /// vip客户登录的sql语句
    /// </summary>
    /// <param name="vipClientInfo">vip客户信息</param>
    /// <param name="flag">标志是输入卡号，还是手机号</param>
    /// <returns></returns>
    public static string VipClientLoginSqlStr(string countID, string pwd)
    {
        //if (flag == 0)//输入的是卡号
        //{
        //    return string.Format("select userName,card_Id from dbo.cardInfo where card_Id='{0}' and pwd='{1}'", vipClientInfo.VipID, vipClientInfo.VipPwd);
        //}
        //else//输入的是手机号
        //{
        //    return string.Format("select userName,card_Id from dbo.cardInfo where userMobile='{0}' and pwd='{1}'", vipClientInfo.VipMobile, vipClientInfo.VipPwd);
        //}
        return string.Format("select  userName,card_Id,userMobile,userEmail from dbo.cardInfo where userMobile='{0}' and pwd='{1}' or card_Id='{2}' and pwd='{3}' or userEmail='{4}' and pwd='{5}'", countID, pwd, countID, pwd, countID, pwd);
    }
    /// <summary>
    /// 通过卡号或者手机号获取vip客户的资料
    /// </summary>
    /// <param name="vipIDOrMobile">卡号或者手机号</param>
    /// <param name="flag">标志是卡号还是手机号</param>
    /// <returns></returns>
    public static string GVipClientInfoSqlStr(string countID)
    {
        //if (flag == 0)//卡号
        //{
        //    return string.Format("select card_Id,card_Type,card_Discount,userName,userSex,userTitle,userBirthday,userPhone,userMobile,userEmail,userCode,userPost,userAddress,beginDate,endDate,ISNULL(points,0) as points,pwd from dbo.cardInfo where card_Id='{0}'", vipIDOrMobile);
        //}
        //else//手机号
        //{
        //    return string.Format("select card_Id,card_Type,card_Discount,userName,userSex,userTitle,userBirthday,userPhone,userMobile,userEmail,userCode,userPost,userAddress,beginDate,endDate,ISNULL(points,0) as points,pwd from dbo.cardInfo where userMobile='{0}'", vipIDOrMobile);
        //}
        return string.Format("select card_Id,card_Type,card_Discount,userName,userSex,userTitle,userBirthday,userPhone,userMobile,userEmail,userCode,userPost,userAddress,beginDate,endDate,ISNULL(points,0) as points,pwd from dbo.cardInfo where card_Id='{0}'", countID);
    }
    /// <summary>
    /// 获取可用vip卡号
    /// </summary>
    /// <returns></returns>
    public static string GVipIDSqlStr()
    {
        return string.Format("SELECT TOP 1 VipCardID FROM dbo.VipCardNo WHERE VipCardUsed IS  NULL AND VipCardStatus =0");
    }
    /// <summary>
    /// 更新vip卡的状态的sql语句
    /// </summary>
    /// <param name="vipCardUsed"></param>
    /// <returns></returns>
    public static string UVipIDSqlStr(string vipCardUsed)
    {
        return string.Format("update dbo.VipCardNo set VipCardStatus=1,VipCardUsed='{0}'", vipCardUsed);
    }
    /// <summary>
    /// 注册vip会员
    /// </summary>
    /// <param name="vipClientInfo">vipi信息</param>
    /// <returns></returns>
    public static string RVipClientInfoSqlStr(VipClientInfo vipClientInfo)
    {
        return string.Format("insert into dbo.cardInfo(card_Id,card_Type,card_Discount,userName,userSex,userTitle,userBirthday,userPhone,userMobile,userEmail,userCode,userPost,userAddress,beginDate,endDate,pwd,points) values('{0}','{1}',{2},'{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", vipClientInfo.VipID, vipClientInfo.VipCardType, vipClientInfo.VipCardDiscount, vipClientInfo.VipName, vipClientInfo.VipGender, vipClientInfo.VipTitle.Length == 0 ? null : vipClientInfo.VipTitle, vipClientInfo.VipBirthday, vipClientInfo.VipMobile, vipClientInfo.VipMobile, vipClientInfo.VipEmail, vipClientInfo.VipCode.Length == 0 ? null : vipClientInfo.VipCode, vipClientInfo.VipPost.Length == 0 ? null : vipClientInfo.VipPost, vipClientInfo.VipAddress.Length == 0 ? null : vipClientInfo.VipAddress, vipClientInfo.BeginDate, vipClientInfo.EndDate, vipClientInfo.VipPwd, 0);
    }
    /// <summary>
    /// 创建日志表的sql语句
    /// </summary>
    /// <returns></returns>
    public static string CLogSqlStr()
    {
        return string.Format(" if not exists(select * from sysobjects where id=OBJECT_ID(N'[dbo].[Log]') and OBJECTPROPERTY(id,N'IsUserTable')=1) CREATE TABLE [dbo].[Log]([Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,[Date] [datetime] NOT NULL,[Thread] [varchar](255) NULL,[Level] [varchar](50) NULL,[Logger] [varchar](255) NULL,[Message] [varchar](3000) NULL,[Exception] [varchar](4000) NULL)");
    }
    /// <summary>
    /// 将开卡信息插入到数据库中去发送给客户通知卡已经开通
    /// </summary>
    /// <returns></returns>
    //public static string ASendDataSqlStr(string mobile, string userName, string vipCardID, int pri)
    //{
    //    return string.Format("insert into dbo.sendData(Mobile,[Type],[Text],InsTime,ErrorCount,PRI) values('{0}','开卡','{1}','{2}',0,{3})", mobile, "尊敬的" + userName + "您好:您已经注册成为我公司会员,卡号为:" + vipCardID + ",初始密码为:888888，谢谢光临。", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), pri);
    //}
    /// <summary>
    /// 将开卡信息插入到数据库中去发送给客户通知卡已经开通
    /// </summary>
    /// <returns></returns>
    public static string ASendDataSqlStr(string mobile,string type,string msgContent, int pri)
    {
        return string.Format("insert into dbo.sendData(Mobile,[Type],[Text],InsTime,ErrorCount,PRI) values('{0}','{1}','{2}','{3}',0,{4})", mobile, type,msgContent, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), pri);
    }
    /// <summary>
    /// 获取短信类型
    /// </summary>
    /// <returns></returns>
    public static string GPRISqlStr(string type)
    {
        return string.Format("select PRI from PRIsetting where smsType='{0}'",type);
    }
    /// <summary>
    /// 获取某类型短信的内容
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    //public static string GMsgContent(string type)
    //{
    //    return string.Format("select messageContent from dbo.sms_model where messageType='{0}'",type);
    //}
    /// <summary>
    /// 验证用户手机是否已经进行了注册
    /// </summary>
    /// <param name="mobile">手机号码</param>
    /// <returns></returns>
    public static string VUserMobileExistsSqlStr(string mobile)
    {
        return string.Format("select COUNT(*) from dbo.cardInfo where userMobile='{0}'", mobile);
    }
    /// <summary>
    /// 根据vipid删除vip信息
    /// </summary>
    /// <param name="vipID">vip卡号</param>
    /// <returns></returns>
    public static string DVipClientInfoSqlStr(string vipID)
    {
        return string.Format("delete from cardInfo where card_Id='{0}'", vipID);
    }
    /// <summary>
    /// 更新卡号的状态，更改为已经使用（在我们获取可用卡号的时候就更新这个卡号的状态，因为这样能保证这个卡号就会唯一的被使用）
    /// </summary>
    /// <param name="vipID">vip卡号</param>
    /// <returns></returns>
    public static string UVipCardNoTbSqlStr1(string vipID)
    {
        return string.Format("update dbo.VipCardNo set VipCardStatus=1,VipCardUsed='{0}' where VipCardID='{1}'", DateTime.Now.ToFileTime().ToString(), vipID);
    }
    /// <summary>
    /// 更新卡号的状态，更改为没有使用（如果该卡号申请失败的时候，还要将其状态变为可用的，这样其他的人还可以申请该卡号）
    /// </summary>
    /// <param name="vipID">VIP卡号</param>
    /// <returns></returns>
    public static string UVipCardNoTbSqlStr2(string vipID)
    {
        return string.Format("update dbo.VipCardNo set VipCardStatus=0,VipCardUsed=NULL where VipCardID='{0}'", vipID);
    }
    /// <summary>
    /// 将那些状态已经改变，但是还没有被申请的卡号恢复为可用的状态(这个是页面加载的时候就进行的动作)
    /// </summary>
    /// <returns></returns>
    public static string ReductionCardNoSqlStr()
    {
        return string.Format("update dbo.VipCardNo set VipCardStatus=0,VipCardUsed=null where VipCardID in (select VipCardID from dbo.VipCardNo except select card_Id from dbo.cardInfo)");
    }
    /// <summary>
    /// 更新vip客户资料的sql语句
    /// </summary>
    /// <param name="vipClientInfo">vip客户资料信息</param>
    /// <param name="vipIdOrMobile">手机号或者卡号</param>
    /// <param name="flag">标志是手机号还是卡号</param>
    /// <returns></returns>
    public static string UCardInfoTbSqlStr(VipClientInfo vipClientInfo, string vipID)
    {
        //if (flag == 0)//卡号
        //{
        //    return string.Format("update dbo.cardInfo set userName='{0}',userTitle='{1}',pwd='{2}',userSex='{3}',userBirthday='{4}',userMobile='{5}',userPhone='{6}',userEmail='{7}',userCode='{8}',userPost='{9}',userAddress='{10}' where card_Id='{11}'", vipClientInfo.VipName, vipClientInfo.VipTitle, vipClientInfo.VipPwd, vipClientInfo.VipGender, vipClientInfo.VipBirthday, vipClientInfo.VipMobile, vipClientInfo.VipPhone, vipClientInfo.VipEmail, vipClientInfo.VipCode, vipClientInfo.VipPost, vipClientInfo.VipAddress, vipIdOrMobile);
        //}
        //else//手机号
        //{
        //    return string.Format("update dbo.cardInfo set userName='{0}',userTitle='{1}',pwd='{2}',userSex='{3}',userBirthday='{4}',userMobile='{5}',userPhone='{6}',userEmail='{7}',userCode='{8}',userPost='{9}',userAddress='{10}' where userMobile='{11}'", vipClientInfo.VipName, vipClientInfo.VipTitle, vipClientInfo.VipPwd, vipClientInfo.VipGender, vipClientInfo.VipBirthday, vipClientInfo.VipMobile, vipClientInfo.VipPhone, vipClientInfo.VipEmail, vipClientInfo.VipCode, vipClientInfo.VipPost, vipClientInfo.VipAddress, vipIdOrMobile);
        //}

        return string.Format("update dbo.cardInfo set userName='{0}',userTitle='{1}',pwd='{2}',userSex='{3}',userBirthday='{4}',userMobile='{5}',userPhone='{6}',userEmail='{7}',userCode='{8}',userPost='{9}',userAddress='{10}' where card_Id='{11}'", vipClientInfo.VipName, vipClientInfo.VipTitle, vipClientInfo.VipPwd, vipClientInfo.VipGender, vipClientInfo.VipBirthday, vipClientInfo.VipMobile, vipClientInfo.VipPhone, vipClientInfo.VipEmail, vipClientInfo.VipCode, vipClientInfo.VipPost, vipClientInfo.VipAddress, vipID);



    }
    /// <summary>
    /// 获取可用优惠劵类型
    /// </summary>
    /// <returns></returns>
    public static string GValiableCouponTypeSqlStr()
    {
        return string.Format("select ct.tid ,ct.ctype ,ct.typeDetails , cp.pname ,ct.score,ct.money ,ct.article  from couponType as ct inner join couponPlace as cp on ct.usedPlace=cp.pid where ct.displayed=1");
    }
    /// <summary>
    /// 根据vip卡号或者手机号获取该vip的积分
    /// </summary>
    /// <param name="vipIdOrMobile">卡号或者手机号</param>
    /// <param name="flag">标志手机号还是卡号</param>
    /// <returns></returns>
    public static string GVipPointsSqlStr(string vipID)
    {
        //if (flag == 0)//卡号
        //{
        //    return string.Format("select points from dbo.cardInfo where card_Id='{0}'",vipIdOrMobile);
        //}
        //else//手机号
        //{
        //    return string.Format("select points from dbo.cardInfo where userMobile='{0}'",vipIdOrMobile);
        //}
        return string.Format("select points from dbo.cardInfo where card_Id='{0}'", vipID);
    }
    /// <summary>
    /// 验证卡号是否被注册
    /// </summary>
    /// <param name="cardID">要验证的卡号</param>
    /// <returns></returns>
    public static string VVipCardIDRegistered(string cardID)
    {
        return string.Format("select COUNT(*) from dbo.cardInfo where card_Id='{0}'", cardID);
    }
    /// <summary>
    /// 验证卡号是否存在且未使用，且还没有过期
    /// </summary>
    /// <param name="cardID">需要验证的卡号</param>
    /// <returns></returns>
    public static string VVipCardIDExists(string cardID)
    {
        return string.Format("select endDate from dbo.VipSet where card_Id='{0}' and state=0 and endDate>getdate()", cardID);
    }
    /// <summary>
    /// 先判断vip卡表里面是否有数据（如果没有数据的话，那就填写卡号）
    /// </summary>
    /// <returns></returns>
    public static string VVipCardIDDataNull()
    {
        return string.Format("select COUNT(*) from dbo.VipSet");
    }

    /// <summary>
    /// 获取所有的vip卡类型信息
    /// </summary>
    /// <returns></returns>
    public static string GetVipCardTypeInfo()
    {
        //return string.Format("select TypeID,Discount,TypeName from VipCardTypeInfo");
        return string.Format("select distinct card_Type,card_Discount from VipSet");
    }
    /// <summary>
    /// 更新vip卡号信息变成已经被使用状态，这样就不能再被使用了
    /// </summary>
    /// <param name="cardId"></param>
    /// <returns></returns>
    public static string UpdateVipCardStatus(string cardId)
    {
        return string.Format("update vipset set state=1 where card_Id='{0}'",cardId);
    }
}