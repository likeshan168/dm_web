<%@ WebHandler Language="C#" Class="ValidateVipCardID" %>

using System;
using System.Web;

public class ValidateVipCardID : IHttpHandler
{
    
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";


        try
        {
            string cardID = context.Request.Params["cardID"];
            int type;
            if (Int32.TryParse(context.Request.Params["type"].ToString(), out type))
            {
                if (type == 0)//验证卡号是否已经被注册，这一般用于没有制好卡的情况
                {
                    if (new VipClientOperate().ValidateVipCardIDRegistered(cardID) > 0)
                    {
                        context.Response.Write("Y");
                    }
                    else
                    {
                        context.Response.Write("N");
                    }
                }
                else if (type == 1)//验证卡号是否存在，这一般用于是先制好卡的情况
                {
                    if (new VipClientOperate().ValidateVipCardDataNull() > 0)//先判断vip卡表里面是否有数据
                    {
                        if (new VipClientOperate().ValidateVipCardIDExists(cardID)==null)
                        {
                            context.Response.Write("N");//说明卡号存在且未使用，且未过期
                        }
                        else
                        {
                            context.Response.Write("Y");//说明卡号不存在或不可用
                        }
                    }
                    else//如果vip卡表里面没有数据，那么就按照输入的卡号进行保存(那么这里至少也应该要验证一下卡的位数才好的)
                    {
                        context.Response.Write("Y");
                    }
                }
                else
                {
                    context.Response.Write("禁止非法注册！");
                }

            }

        }
        catch (Exception ex)
        {
            context.Response.Write(ex.Message);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}