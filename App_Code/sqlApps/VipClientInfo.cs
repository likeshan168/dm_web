using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///VipClientInfo 的摘要说明
/// </summary>
public class VipClientInfo
{
    /// <summary>
    /// vip姓名
    /// </summary>
    public string vipName = string.Empty;
    /// <summary>
    /// vip卡号
    /// </summary>
    public string vipID = string.Empty;
    /// <summary>
    /// vip称呼
    /// </summary>
    public string vipTitle = string.Empty;
    /// <summary>
    /// vip手机号
    /// </summary>
    public string vipMobile = string.Empty;
    /// <summary>
    /// vip联系电话
    /// </summary>
    public string vipPhone = string.Empty;
    /// <summary>
    /// vip密码
    /// </summary>
    public string vipPwd = "888888";
    /// <summary>
    /// vip性别
    /// </summary>
    public string vipGender = string.Empty;
    /// <summary>
    /// vip生日
    /// </summary>
    public string vipBirthday = string.Empty;
    /// <summary>
    /// vip邮箱
    /// </summary>
    public string vipEmail = string.Empty;
    /// <summary>
    /// vip省份证号码
    /// </summary>
    public string vipCode = string.Empty;
    /// <summary>
    /// vip的联系地址
    /// </summary>
    public string vipAddress = string.Empty;
    /// <summary>
    /// vip邮编
    /// </summary>
    public string vipPost = string.Empty;
    /// <summary>
    /// vip卡的启用日期
    /// </summary>
    public string beginDate = string.Empty;
    /// <summary>
    /// vip卡的截止日期
    /// </summary>
    public string endDate = string.Empty;
    /// <summary>
    /// vip卡类型
    /// </summary>
    public string cardType = string.Empty;
    /// <summary>
    /// vip卡折扣
    /// </summary>
    public float cardDiscount;

    /// <summary>
    /// vip姓名
    /// </summary>
    public string VipName
    {
        get { return this.vipName; }
        set { this.vipName = value; }
    }
    /// <summary>
    /// vip卡号
    /// </summary>
    public string VipID { get; set; }
    /// <summary>
    /// vip称呼
    /// </summary>
    public string VipTitle
    {
        get { return this.vipTitle; }
        set { this.vipTitle = value; }
    }
    /// <summary>
    /// vip手机号
    /// </summary>
    public string VipMobile
    {
        get { return this.vipMobile; }
        set { this.vipMobile = value; }
    }
    /// <summary>
    /// 座机号码
    /// </summary>
    public string VipPhone
    {
        get { return this.vipPhone; }
        set { this.vipPhone = value; }
    }
    /// <summary>
    /// vip密码
    /// </summary>
    public string VipPwd
    {
        get { return this.vipPwd; }
        set { this.vipPwd = value; }
    }
    /// <summary>
    /// vip性别
    /// </summary>
    public string VipGender
    {
        get { return this.vipGender; }
        set { this.vipGender = value; }
    }
    /// <summary>
    /// vip生日
    /// </summary>
    public string VipBirthday { get; set; }
    /// <summary>
    /// vip邮箱
    /// </summary>
    public string VipEmail
    {
        get { return this.vipEmail; }
        set { this.vipEmail = value; }
    }
    /// <summary>
    /// vip身份证号
    /// </summary>
    public string VipCode
    {
        get { return this.vipCode; }
        set { this.vipCode = value; }
    }
    /// <summary>
    /// vip地址
    /// </summary>
    public string VipAddress
    {
        get { return this.vipAddress; }
        set { this.vipAddress = value; }
    }
    /// <summary>
    /// vip邮编
    /// </summary>
    public string VipPost
    {
        get { return this.vipPost; }
        set { this.vipPost = value; }
    }
    /// <summary>
    /// vip卡开始时间
    /// </summary>
    public string BeginDate
    {
        get { return this.beginDate; }
        set { this.beginDate = value; }
    }
    /// <summary>
    /// vip卡截止时间
    /// </summary>
    public string EndDate
    {
        get { return this.endDate; }
        set { this.endDate = value; }
    }
    /// <summary>
    /// vip卡类型（默认用1）
    /// </summary>
    public string VipCardType
    {
        get { return this.cardType; }
        set { this.cardType = value; }
    }
    /// <summary>
    /// vip卡折扣（默认用89）
    /// </summary>
    public float VipCardDiscount
    {
        get { return this.cardDiscount; }
        set { this.cardDiscount = value; }
    }

}
