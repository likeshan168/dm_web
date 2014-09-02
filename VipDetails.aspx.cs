using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using RBKJEmpox_Management_System.Models;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;

public partial class VipDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Session["VipName"] == null)//如果两个有一个为null就从新登录
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "tip1", "location.href='VipClientLogin.aspx'", true);
                return;
            }
            try
            {

                string countID = (string)Cache["vipCardID"];//就以卡号进行后去vip资料的判断
                DataBind(countID);
            }

            catch (Exception ex)
            {
                Log4netHelper.InvokeErrorLog(MethodBase.GetCurrentMethod().DeclaringType, "获取vip详细资料时出错!", ex);
                ClientScript.RegisterClientScriptBlock(GetType(), "tip2", "alert('请先注册！');location.href='VipClientRegister.aspx'", true);
                return;
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string vipName = txtUserName.Text.Trim(),
               vipTitle = txtTitle.Text.Trim(),
               vipPwd = txtPassword.Text.Trim(),
               vipGender = rbSex1.Checked ? "男" : "女",
               vipBirthday = txtBirthday.Text.Trim(),
               vipMobile = txtMobile.Text.Trim(),
               vipPhone = txtPhone.Text.Trim(),
               vipEmail = txtEmail.Text.Trim(),
               vipCode = txtPasNo.Text.Trim(),
               vipPost = txtPost.Text.Trim(),
               vipAddress = txtAddress.Text.Trim();
        if (string.IsNullOrWhiteSpace(vipName) || string.IsNullOrWhiteSpace(vipPwd) || string.IsNullOrWhiteSpace(vipBirthday) || string.IsNullOrWhiteSpace(vipMobile) || string.IsNullOrWhiteSpace(vipEmail))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "err1", "alert('请将必要信息填写完整！')");
            return;
        }

        Regex regex = new Regex(RegularExp.NAME_REGULAR_EXP);
        if (!regex.IsMatch(vipName))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "err2", "alert('输入姓名不符合要求！')");
            return;
        }
        regex = new Regex(RegularExp.BRITHDAY_REGULAR_EXP);
        if (!regex.IsMatch(vipBirthday))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "err3", "alert('出生日期不符合规则！')");
            return;
        }
        regex = new Regex(RegularExp.MOBILE_REGULAR_EXP);
        if (!regex.IsMatch(vipMobile))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "err4", "alert('输入手机号码不正确！')");
            return;
        }
        regex = new Regex(RegularExp.MAIL_REGULAR_EXP);
        if (!regex.IsMatch(vipEmail))
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "err5", "alert('输入邮箱格式不正确！')");
            return;
        }
        //验证通过了之后就进行vip客户资料的更新
        VipClientInfo vipclientInfo = new VipClientInfo()
        {
            VipName = vipName,
            VipTitle = vipTitle,
            VipPwd = vipPwd,
            VipGender = vipGender,
            VipBirthday = vipBirthday,
            VipMobile = vipMobile,
            VipPhone = vipPhone,
            VipEmail = vipEmail,
            VipCode = vipCode,
            VipPost = vipPost,
            VipAddress = vipAddress
        };
        try
        {

            if (Cache["vipCardID"] != null)
            {
                string vipID = (string)Cache["vipCardID"];
                if (new VipClientOperate().UpdateVipCardInfo(vipclientInfo, vipID) > 0)
                {//说明更新成功 
                    #region 将修改过的资料反映到erp软件中去
                    SQL_Operate so = new SQL_Operate();
                    DateTime newDT = new DateTime(2010, 1, 1);
                    TimeSpan sp;
                    double li;
                    byte[] tt;
                    string sql;
                    DataTable dt;
                    /*以下是更新vip用户名称*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userName" + (char)22 + vipclientInfo.VipName + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);

                    /*以下是更新vip用户的称呼*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userTitle" + (char)22 + vipclientInfo.VipTitle + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    /*以下是更新vip用户的密码*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("pwd" + (char)22 + vipclientInfo.VipPwd + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    /*以下是更新vip用户的性别*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userSex" + (char)22 + vipclientInfo.VipGender + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);

                    /*以下是更新vip用户的生日*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userBirthday" + (char)22 + vipclientInfo.VipBirthday + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    /*以下是更新vip用户的手机*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userMobile" + (char)22 + vipclientInfo.VipMobile + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    /*以下是更新vip用户的电话*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userPhone" + (char)22 + vipclientInfo.VipPhone + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);

                    /*以下是更新vip用户的邮箱*/
                    sp = DateTime.Now - newDT;
                    li = sp.TotalMilliseconds;
                    tt = Encoding.Default.GetBytes("userEmail" + (char)22 + vipclientInfo.VipEmail + (char)22 + vipID);
                    sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                       + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                    dt = new DataTable();
                    dt.Columns.Add("data", typeof(string));
                    dt.Columns.Add("value", typeof(object));
                    dt.Columns.Add("type", typeof(string));
                    dt.Rows.Add(new object[] { "@b", tt, "binary" });
                    so.sqlExcuteNonQuery(sql, dt);
                    /*以下是更新vip用户的省份证*/
                    if (!string.IsNullOrEmpty(vipclientInfo.VipCode))
                    {
                        sp = DateTime.Now - newDT;
                        li = sp.TotalMilliseconds;
                        tt = Encoding.Default.GetBytes("userCode" + (char)22 + vipclientInfo.VipCode + (char)22 + vipID);
                        sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                           + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                        dt = new DataTable();
                        dt.Columns.Add("data", typeof(string));
                        dt.Columns.Add("value", typeof(object));
                        dt.Columns.Add("type", typeof(string));
                        dt.Rows.Add(new object[] { "@b", tt, "binary" });
                        so.sqlExcuteNonQuery(sql, dt);
                    }


                    /*以下是更新vip用户的邮编*/
                    if (!string.IsNullOrEmpty(vipclientInfo.VipPost))
                    {
                        sp = DateTime.Now - newDT;
                        li = sp.TotalMilliseconds;
                        tt = Encoding.Default.GetBytes("userPost" + (char)22 + vipclientInfo.VipPost + (char)22 + vipID);
                        sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                           + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                        dt = new DataTable();
                        dt.Columns.Add("data", typeof(string));
                        dt.Columns.Add("value", typeof(object));
                        dt.Columns.Add("type", typeof(string));
                        dt.Rows.Add(new object[] { "@b", tt, "binary" });
                        so.sqlExcuteNonQuery(sql, dt);
                    }


                    /*以下是更新vip用户的地址*/
                    if (!string.IsNullOrEmpty(vipclientInfo.VipAddress))
                    {
                        sp = DateTime.Now - newDT;
                        li = sp.TotalMilliseconds;
                        tt = Encoding.Default.GetBytes("userAddress" + (char)22 + vipclientInfo.VipAddress + (char)22 + vipID);
                        sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                           + li.ToString() + "','0x34',0,'" + memberStatic.companyID + "',@b)";//vip资料批量更新就是讲更新的结果反映到erp软件中去，操作码是0x34即52
                        dt = new DataTable();
                        dt.Columns.Add("data", typeof(string));
                        dt.Columns.Add("value", typeof(object));
                        dt.Columns.Add("type", typeof(string));
                        dt.Rows.Add(new object[] { "@b", tt, "binary" });
                        so.sqlExcuteNonQuery(sql, dt);
                    }

                    #endregion
                    ClientScript.RegisterClientScriptBlock(GetType(), "msg1", "alert('更新成功');location.href='LoginSuccess.aspx'",
 true);

                }
                else
                {//更新不成功 
                    ClientScript.RegisterClientScriptBlock(GetType(), "err6", "alert('更新失败，请稍后再试！')", true);
                    return;
                }

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "err7", "location.href='VipClientLogin.aspx'", true);
                return;
            }

        }
        catch (Exception ex)
        {
            Log4netHelper.InvokeErrorLog(MethodBase.GetCurrentMethod().DeclaringType, "更新vip客户资料失败", ex);
            ClientScript.RegisterClientScriptBlock(GetType(), "err8", "alert('" + ex.Message + "')", true);
        }

    }
    /// <summary>
    /// 根据vip卡号或者手机号获取vip客户资料
    /// </summary>
    private void DataBind(string countID)
    {
        #region//通过vip卡号或者手机号获取vip客户资料信息

        DataTable dt = new VipClientOperate().GetVipClientInfoByIDOrMobile(countID);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataRow dr = dt.Rows[0];
            txtCardID.Text = dr["card_Id"].ToString();
            txtCardNo.Text = dr["card_Type"].ToString();
            txtBeginDate.Text = dr["beginDate"].ToString();
            txtEndDate.Text = dr["endDate"].ToString();
            txtPoints.Text = dr["points"].ToString();
            txtUserName.Text = dr["userName"].ToString();
            txtTitle.Text = dr["userTitle"].ToString();
            txtPassword.Text = dr["pwd"].ToString();
            //txtPassword2.Text = dr["pwd"].ToString();
            rbSex1.Checked = dr["userSex"].ToString() == "男" ? true : false;
            txtBirthday.Text = dr["userBirthday"].ToString();
            txtMobile.Text = dr["userMobile"].ToString();
            txtPhone.Text = dr["userPhone"].ToString();
            txtEmail.Text = dr["userEmail"].ToString();
            txtPasNo.Text = dr["userCode"].ToString();
            txtPost.Text = dr["userPost"].ToString();
            txtAddress.Text = dr["userAddress"].ToString();
        }
        else
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "tip3", "location.href='VipClientLogin.aspx'", true);
        }
        #endregion
    }
}