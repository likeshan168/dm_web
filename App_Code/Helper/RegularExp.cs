using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///RegularExp 的摘要说明
/// </summary>
public class RegularExp
{
    /// <summary>
    /// 姓名的正则表达式
    /// </summary>
    public const string NAME_REGULAR_EXP = @"^([A-Za-z]+|[\u4E00-\u9FA5])*$";
    /// <summary>
    /// 生日正则表达式
    /// </summary>
    public const string BRITHDAY_REGULAR_EXP = @"^(([0-9]{3}[1-9]|[0-9]{2}[1-9][0-9]{1}|[0-9]{1}[1-9][0-9]{2}|[1-9][0-9]{3})-(((0[13578]|1[02])-(0[1-9]|[12][0-9]|3[01]))|((0[469]|11)-(0[1-9]|[12][0-9]|30))|(02-(0[1-9]|[1][0-9]|2[0-8]))))|((([0-9]{2})(0[48]|[2468][048]|[13579][26])|((0[48]|[2468][048]|[3579][26])00))-02-29)";
    /// <summary>
    /// 邮箱正则表达式
    /// </summary>
    public const string MAIL_REGULAR_EXP = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
    /// <summary>
    /// 手机号码正则表达式
    /// </summary>
    public const string MOBILE_REGULAR_EXP=@"^(13)[0-9]{1}\d{8}|(15)[0-9]{1}\d{8}|(18)[6-9]{1}\d{8}";
    /// <summary>
    /// vip卡号的正则表达式
    /// </summary>
    public const string VIPCARDID_REGULAR_EXP = @"^[a-zA-Z0-9]{1,20}";
    /// <summary>
    /// vip姓名
    /// </summary>
    public static string VipName = string.Empty;
    /// <summary>
    /// vip积分
    /// </summary>
    public static string Points = string.Empty;

}