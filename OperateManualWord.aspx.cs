using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
public partial class OperateManualWord : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginState"] == null || Session["LoginState"].ToString() != "login")
            Response.Redirect("Login.aspx");
        else
        {
            string path = Server.MapPath("~/dm.doc");
            FileInfo file = new FileInfo(path);
            FileStream myfs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] filedata = new byte[file.Length];
            myfs.Read(filedata, 0, (int)file.Length);
            myfs.Close();
            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            Response.Charset = "GB2312";
            Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
            Response.AddHeader("Content-Length", file.Length.ToString());
            Response.ContentType = "application/ms-word";
            Response.Flush();
            Response.BinaryWrite(filedata);
            Response.End();


        }
    }
}