using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///vid信息类（李克善新加的）
/// </summary>
public class CardInfo
{
    /// <summary>
    /// 卡号
    /// </summary>
    public string CardID { get; set; }
    /// <summary>
    /// 卡类型
    /// </summary>
    public int CardType { get; set; }
    /// <summary>
    /// 卡的折扣
    /// </summary>
    public float CardDiscount { get; set; }
    /// <summary>
    /// 持卡人姓名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 持卡人性别
    /// </summary>
    public string UserSex { get; set; }
    /// <summary>
    /// 称呼
    /// </summary>
    public string UserTitle { get; set; }
    /// <summary>
    /// 用户生日
    /// </summary>
    public string UserBirthday { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string UserPhone { get; set; }
    /// <summary>
    /// 手机号码
    /// </summary>
    public string UserMobile { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string UserEmail { get; set; }
    /// <summary>
    /// 证件号
    /// </summary>
    public string UserCode { get; set; }
    /// <summary>
    /// 邮编
    /// </summary>
    public string UserPost { get; set; }
    /// <summary>
    /// vip客户地址
    /// </summary>
    public string UserAddress { get; set; }
    /// <summary>
    /// 发卡店铺
    /// </summary>
    public string SendClient { get; set; }
    /// <summary>
    /// 发卡人
    /// </summary>
    public string SendMan { get; set; }
    /// <summary>
    /// vip卡的开始时间
    /// </summary>
    public string BeginDate { get; set; }
    /// <summary>
    /// vip卡的到期时间
    /// </summary>
    public string EndDate { get; set; }
    /// <summary>
    /// 积分
    /// </summary>
    public float Points { get; set; }
    /// <summary>
    /// vip卡密码
    /// </summary>
    public string pwd { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}