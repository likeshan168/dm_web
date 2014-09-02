using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.Web.Configuration;
using System.IO;

/// <summary>
/// 邮件群发主方法
/// </summary>
public class sendMail
{
    //static string SmtpServer = "smtp.ym.163.com";
    //static string FromUser = "xuwei@empox.cn";
    //static string FromPwd = "44513457";
    static string SmtpServer = WebConfigurationManager.AppSettings["SmtpServer"];
    static string FromUser = WebConfigurationManager.AppSettings["FromUser"];
    static string FromPwd = WebConfigurationManager.AppSettings["FromPwd"];
    static int Port = Int32.Parse(WebConfigurationManager.AppSettings["Port"]);
    static string ShowName = WebConfigurationManager.AppSettings["ShowName"];
    static string Subject = WebConfigurationManager.AppSettings["Subject"];
    public sendMail()
    {

    }
    public static void SendMail(string STo, string mailbody, int type)
    {
        string filePath = HttpContext.Current.Server.MapPath("~/EmailLog");
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromUser, ShowName);//发件人地址

            mailMessage.To.Add(STo);//设置收件人地址
            mailMessage.Subject = Subject;
            if (type == 0)
            {
                string body = mailbody;
                mailMessage.Body = body;
            }
            else
            {
                mailMessage.IsBodyHtml = true;
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailbody, null, "text/html");
                //AlternateView htmlView = AlternateView.CreateAlternateViewFromString("您好．我进行测试" + "<br><img src=cid:myimg>", null, "text/html");
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString("<img src=cid:myimg>", null, "text/html");
                LinkedResource linkSource = new LinkedResource("C:/EmpoxWebModel/mailModel/img/tmp.jpg");
                linkSource.ContentId = "myimg";//图片id，该图片也是以附件的方式发送的
                htmlView.LinkedResources.Add(linkSource);
                mailMessage.AlternateViews.Add(htmlView);

            }

            SmtpClient smtpclient = new SmtpClient();
            smtpclient.EnableSsl = false;
            smtpclient.Host = SmtpServer;
            smtpclient.Port = Port;
            smtpclient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpclient.EnableSsl = false;
            smtpclient.Credentials = new System.Net.NetworkCredential(FromUser, FromPwd);//发件人的用户名和密码

            

            smtpclient.Send(mailMessage);

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            if (!File.Exists(filePath + "/sendLog.txt"))
            {
                File.Create(filePath + "/sendLog.txt");
            }

            StreamWriter writer = File.AppendText(filePath + "/sendLog.txt");
            writer.Write("已发邮件：" + STo + " 发送时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n");
            writer.Flush();
            writer.Close();

        }
        catch (Exception ex)
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            if (!File.Exists(filePath + "/nosendLog.txt"))
            {
                File.Create(filePath + "/nosendLog.txt");
            }

            StreamWriter writer = File.AppendText(filePath + "/nosendLog.txt");
            writer.Write("未发邮件：" + STo + " 发送时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 错误："+ex.Message+"\r\n");
            writer.Flush();
            writer.Close();
        }

    }
}
