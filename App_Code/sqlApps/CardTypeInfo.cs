using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///vip卡类型信息类（李克善新加的）
/// </summary>
public class CardTypeInfo
{
    /// <summary>
    /// vip卡类型id
    /// </summary>
    public int TypeID { get; set; }
    /// <summary>
    /// vip卡类型名称
    /// </summary>
    public string TypeName { get; set; }
    /// <summary>
    /// 卡的折扣
    /// </summary>
    public float CardDiscount { get; set; }
    /// <summary>
    /// 卡的有效年限
    /// </summary>
    public int ValidityYears { get; set; }
}