using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Collections;
using System.Text;
using System.Data;

public partial class DM_VIP_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                DataTable dt = new VipClientOperate().GetVipCardTypeInfo();
                if (dt != null && dt.Rows.Count > 0)
                {
                    selectCardType.Items.Add(new ListItem("--请选择--", "0"));
                    int i = 1;
                    foreach (DataRow dr in dt.Rows)
                    {
                        selectCardType.Items.Add(new ListItem(dr[0].ToString() + "[折扣:" + dr[1].ToString() + "]", i.ToString()));
                        i++;
                    }
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "vipcardtypenull", "if(confirm('没有vip卡类型信息，是否去下载vip制卡信息')) location.href='DataDownload.aspx';", true);
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "exception", "alert('" + ex.Message + "')", true);
            }
        }
    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        try
        {
            string cardId = txtCardID.Text.Trim();
            string cardType = selectCardType.Items[selectCardType.SelectedIndex].Text;


            string userName = txtUserName.Text.Trim();
            string vipGender = rdbMale.Checked ? "男" : "女";
            string mobile = txtMobile.Text.Trim();
            string birthday = txtBirthday.Text.Trim();
            string email = txtEmail.Text.Trim();
            object endDate = null;
            /*以下是对用户的输入进行验证用的*/
            VipClientOperate VCOperate = new VipClientOperate();
            /*首先判断vip卡表里面是否有数据*/
            #region 验证卡号输入是否正确

            if (!string.IsNullOrEmpty(cardId))
            {
                if (VCOperate.ValidateVipCardDataNull() > 0)//如果vip卡表里有数据，那么就去验证输入的卡号是否存在表中
                {


                    if ((endDate = VCOperate.ValidateVipCardIDExists(cardId)) == null)//如果输入的卡号不存在，那么就表示输入的卡号不正确，那就不能进行注册
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "tip1", "alert('卡号输入不正确！请核对后重新输入。')", true);
                        return;
                    }
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "cardidNull", "alert('卡号不能为空！请输入正确额卡号。')", true);
                return;
            }

            #endregion


            #region 验证是否选择了卡类型
            if (cardType == "--请选择--")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "tip2", "alert('请选择卡类型，再进行注册!')", true);
                return;
            }
            #endregion
            string[] disStr = cardType.Split(':');

            float discount = float.Parse(disStr[1].TrimEnd(']'));

            cardType = disStr[0].Split('[')[0];
            if (string.IsNullOrEmpty(userName))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "userNameNull", "alert('姓名不能为空，请输入姓名!')", true);
                return;
            }


            #region 验证手机号是否正确
            Regex regex = new Regex(RegularExp.MOBILE_REGULAR_EXP);
            if (!string.IsNullOrEmpty(mobile))
            {
                if (!regex.IsMatch(mobile))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "tip3", "alert('输入的手机号码不正确，请重新输入！')", true);
                    return;
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "mobileNull", "alert('输入的手机号码不正确，请重新输入！')", true);
                return;
            }

            #endregion

            #region 验证生日的格式是否正确
            if (birthday.Length != 0)//因为生日不是必须的项，所以只要在其输入日期的情况下，我们才对其进行格式的验证，否则就不需要进行验证啦
            {
                regex = new Regex(RegularExp.BRITHDAY_REGULAR_EXP);
                if (!regex.IsMatch(birthday))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "tip4", "alert('生日格式输入不正确！')", true);
                    return;
                }
            }

            #endregion


            #region 邮箱进行格式的验证
            if (email.Length != 0)//邮箱和生日是一样的，都不是必须的项，因此呢？只有在填写了邮箱之后，我们才对其格式进行验证，否则是不会进行验证的。
            {
                regex = new Regex(RegularExp.MAIL_REGULAR_EXP);
                if (!regex.IsMatch(email))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "tip5", "alert('邮箱格式输入不正确，请重新输入！')", true);
                    return;
                }
            }
            #endregion



            //VipSet  endDate  2012-02-04
            #region 在所有的验证都通过之后,就进行输入的写入，将信息插入到cardInfo表中去。
            VipClientInfo vipClientInfo = new VipClientInfo()
            {
                VipID = cardId,
                VipName = userName,
                VipGender = vipGender,
                VipBirthday = birthday,
                VipMobile = mobile,
                VipPhone = mobile,
                VipEmail = email,
                BeginDate = DateTime.Now.ToString("yyyy-MM-dd"),
                EndDate = endDate.ToString(),
                VipCardType = cardType,
                VipCardDiscount = discount
            };

            ArrayList ar = new ArrayList();
            ar.Add(SqlStrHelper.RVipClientInfoSqlStr(vipClientInfo));//将vip信息插入到数据库中去
            ar.Add(SqlStrHelper.UpdateVipCardStatus(cardId));

            int pri = (int)new SQL_Operate().sqlExecuteScalar(SqlStrHelper.GPRISqlStr("开卡"));
            //尊敬的顾客[姓名]您好,您已注册成为我公司会员,卡号为[卡号],初始密码为[密码],谢谢光临。
            string msgContent = string.Format("尊敬的顾客{0}您好,您已注册成为我公司会员,卡号为{1},初始密码为{2},谢谢光临。", userName, cardId, "888888");
            ar.Add(SqlStrHelper.ASendDataSqlStr(mobile, "开卡", msgContent, pri));//将短信信息插入到数据库中准备发送给客户，是用来通知客户vip卡号已经注册成功！
            new SQL_Operate().sqlExcuteNonQuery(ar, true);

            #region 将新增的vip资料必须要同步到erp软件当中去
            DateTime newDT = new DateTime(2010, 1, 1);
            TimeSpan sp = DateTime.Now - newDT;
            double li = sp.TotalMilliseconds;
            byte[] dataRes = Encoding.Default.GetBytes(cardId);
            string sql = "insert into TempStor(SessionId,data,flag,companyid,databag) values('"
                + li.ToString() + "','0x35',0,'" + memberStatic.companyID + "',@b)";
            DataTable dt = new DataTable();
            dt.Columns.Add("data", typeof(string));
            dt.Columns.Add("value", typeof(object));
            dt.Columns.Add("type", typeof(string));
            dt.Rows.Add(new object[] { "@b", dataRes, "binary" });

            new SQL_Operate().sqlExcuteNonQuery(sql, dt);

            ClientScript.RegisterClientScriptBlock(this.GetType(), "regSucc", "alert('Vip注册成功!')", true);
            #endregion
            #endregion
        }
        catch (Exception ex)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "regFailed", "alert('Vip注册成功!'+'" + ex.Message + "')", true);
        }

    }
}