using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class VipClientLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string vipID = txtName.Text.Trim();
        string vipPwd = txtPwd.Text.Trim();
        string code = txtVerifyCode.Text.Trim();
        if (string.IsNullOrWhiteSpace(vipID) || string.IsNullOrWhiteSpace(vipPwd))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "infoNullErr", "alert('卡号/手机或者密码不能为空！')", true);
            return;
        }
        if (string.IsNullOrWhiteSpace(code))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "codeNullErr", "alert('验证码不能为空！')", true);
            return;
        }
        else
        {
            if (Session["code"] != null)
            {
                if (string.Compare(code, Session["code"].ToString(), true) != 0)
                {
                    ClientScript.RegisterClientScriptBlock(GetType(), "codeErr", "alert('验证码输入错误')", true);
                    return;
                }
            }
        }

        try
        {
            
            DataTable dt = new VipClientOperate().ValidateVipClientLogOn(vipID, vipPwd);

            if (dt != null && dt.Rows.Count > 0)//登录成功之后
            {
                DataRow dr = dt.Rows[0];
                Session["VipName"] = dr[0].ToString();//vip姓名
                Cache["vipCardID"] = dr[1].ToString();//vip卡号(这个是从数据库中取出来的，不是用户填写的)
                ClientScript.RegisterClientScriptBlock(GetType(), "vipSuccMsg", "location.href='LoginSuccess.aspx'", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "vipErrMsg", "alert('用户名或密码有误，请核对后再试！')", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "ExceErr", "alert('" + ex.Message + "')", true);
            return;
        }

    }


}