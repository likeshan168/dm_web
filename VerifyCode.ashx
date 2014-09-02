<%@ WebHandler Language="C#" Class="VerifyCode" %>

using System;
using System.Web;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web.SessionState;
public class VerifyCode : IHttpHandler, IRequiresSessionState
{

    /*
     *这个是产生验证码的程序
     */
    public void ProcessRequest(HttpContext context)
    {

        string[] strArray = { "0", "1", "2", "3", "4", "5", "6", "7",
                              "8", "9", "a", "b", "c", "d", "e", "f",
                              "g", "h", "i", "j", "k", "l", "m", "n",
                              "o", "p", "q", "r", "s", "t", "u", "v",
                              "w", "x", "y", "z","A","B","C","D","E",
                              "F","G","H","I","J","K","L","M","N","O",
                              "P","Q","R","S","T","U","V","W","X","Y","Z" };
        Random rnd = new Random();
        string strCode = string.Empty;
        for (int i = 0; i < 4; i++)
        {
            int index = rnd.Next(62);
            strCode += strArray[index];

        }

        context.Session["code"] = strCode;//这个是为和客户端提交过来的输入的验证码进行比较，看是否输入正确
        HttpContext.Current.Session["code"] = strCode;
        CreateImage(strCode);//这个是调用下面生成验证码图片的函数

    }
    /// <summary>
    /// 获取随机颜色
    /// </summary>
    /// <returns></returns>
    private System.Drawing.Color GetRandomColor()
    {
        Random rnd = new Random((int)unchecked(DateTime.Now.Ticks));
        int int_Rde = rnd.Next(256);
        int int_Green = rnd.Next(256);
        int int_Blue = (int_Rde + int_Green) > 400 ? 0 : 400 - int_Rde - int_Green;
        int_Blue = int_Blue > 255 ? 255 : int_Blue;
        return System.Drawing.Color.FromArgb(int_Rde, int_Green, int_Blue);
    }
    private void CreateImage(string strCode)
    {
        int imgWidth = strCode.Length * 22;//
        Random rnd = new Random();
        System.Drawing.Bitmap bitmap = new Bitmap(imgWidth, 36);//36
        Graphics g = Graphics.FromImage(bitmap);
        g.Clear(Color.White);
        drawLine(g, bitmap, rnd);//画随机线
        g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, bitmap.Width - 1, bitmap.Height - 1);//画边框


        for (int i = 0; i < strCode.Length; i++)
        {
            Matrix x = new Matrix();
            x.Shear((float)rnd.Next(0, 300) / 1000 - 0.25f, (float)rnd.Next(0, 100) / 1000 - 0.05f);
            g.Transform = x;
            string str_char = strCode.Substring(i, 1);
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
            Point pointer = new Point(i * 22 + 1 + rnd.Next(3), 1 + rnd.Next(13));
            string[] fonts = new string[] { "Helvetica", "Geneva", "sans-serif", "Verdana", "Times New Roman", "Courier New", "Arial" };
            Font font = new Font(fonts[rnd.Next(fonts.Length - 1)], rnd.Next(14, 18), FontStyle.Bold);
            g.DrawString(str_char, font, brush, pointer);//写入字符串
        }
        drawPoint(bitmap, rnd);//随机画点
        MemoryStream ms = new MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ContentType = "image/png";
        HttpContext.Current.Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        bitmap.Dispose();
        HttpContext.Current.Response.End();

    }
    /// <summary>
    /// 在图片上画点
    /// </summary>
    /// <param name="map"></param>
    /// <param name="rnd"></param>
    private void drawPoint(Bitmap map, Random rnd)
    {
        for (int i = 0; i < 100; i++)
        {
            int x = rnd.Next(map.Width);
            int y = rnd.Next(map.Height);
            map.SetPixel(x, y, Color.FromArgb(rnd.Next()));
        }
    }
    /// <summary>
    /// 随机画线
    /// </summary>
    /// <param name="g"></param>
    /// <param name="map"></param>
    /// <param name="rnd"></param>
    private void drawLine(Graphics g, Bitmap map, Random rnd)
    {
        for (int i = 0; i < 30; i++)
        {
            int x1 = rnd.Next(map.Width);
            int y1 = rnd.Next(map.Height);
            int x2 = rnd.Next(map.Width);
            int y2 = rnd.Next(map.Height);
            g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
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