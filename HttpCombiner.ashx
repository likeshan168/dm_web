<%@ WebHandler Language="C#" Class="HttpCombiner" %>

using System;
using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Web.Configuration;
public class HttpCombiner : IHttpHandler
{
    /*
     *这个是为了提升客户端网页的性能而设计的，让css以及javascript强制在浏览器端进行缓冲，以减少
     *对服务端的请求，从而提高性能
     */
    private const bool DO_GZIP = true;
    private readonly static TimeSpan CACHE_DURATION = TimeSpan.FromDays(30);
    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        string setName = request.QueryString["s"] ?? string.Empty;
        string contentType = request.QueryString["t"] ?? string.Empty;
        string version = request.QueryString["v"] ?? string.Empty;
        bool isCompressed = DO_GZIP && this.CanGzip(request);
        UTF8Encoding encoding = new UTF8Encoding(false);
        if (!this.WriteFromCache(context, setName, version, isCompressed, contentType))
        {
            using (MemoryStream ms = new MemoryStream(5000))
            {
                using (Stream writer = isCompressed ? (Stream)(new GZipStream(ms, CompressionMode.Compress)) : ms)
                {
                    string setDefinition = WebConfigurationManager.AppSettings[setName] ?? string.Empty;//这个就是去web.config中获取文件的
                    string[] FileNames = setDefinition.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string scriptFileName in FileNames)
                    {
                        byte[] fileBytes = this.GetFileBytes(context, scriptFileName.Trim(), encoding);
                        writer.Write(fileBytes, 0, fileBytes.Length);
                    }
                    writer.Close();

                }
                byte[] responseBytes = ms.ToArray();
                context.Cache.Insert(GetCacheKey(setName, version, isCompressed), responseBytes, null, System.Web.Caching.Cache.NoAbsoluteExpiration, CACHE_DURATION);
                this.WriteBytes(responseBytes, context, isCompressed, contentType);
            }
        }
    }
    /// <summary>
    /// 判断浏览器是否支持压缩
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    private bool CanGzip(HttpRequest request)
    {
        string acceptEncoding = request.Headers["Accept-Encoding"];
        if (!string.IsNullOrEmpty(acceptEncoding) && (acceptEncoding.Contains("gzip")) || acceptEncoding.Contains("deflate"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// 从缓存中获取数据
    /// </summary>
    /// <param name="context"></param>
    /// <param name="setName"></param>
    /// <param name="version"></param>
    /// <param name="isCompressed"></param>
    /// <param name="contentType"></param>
    /// <returns></returns>
    private bool WriteFromCache(HttpContext context, string setName, string version, bool isCompressed, string contentType)
    {
        byte[] responseBytes = context.Cache[GetCacheKey(setName, version, isCompressed)] as byte[];
        if (null == responseBytes || 0 == responseBytes.Length)
        {
            return false;
        }
        this.WriteBytes(responseBytes, context, isCompressed, contentType);
        return true;
    }
    /// <summary>
    /// 获取key
    /// </summary>
    /// <param name="setName"></param>
    /// <param name="version"></param>
    /// <param name="isCompressed"></param>
    /// <returns></returns>
    private string GetCacheKey(string setName, string version, bool isCompressed)
    {
        return "HttpCombiner." + setName + "." + version + "." + isCompressed;
    }
    /// <summary>
    /// 写入客户端缓存
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="context"></param>
    /// <param name="isCompressed"></param>
    /// <param name="contentType"></param>
    private void WriteBytes(byte[] bytes, HttpContext context, bool isCompressed, string contentType)
    {
        HttpResponse response = context.Response;
        response.AppendHeader("Content-Length", bytes.Length.ToString());
        response.ContentType = contentType;
        if (isCompressed)
        {
            response.AppendHeader("Content-Encoding", "gzip");
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetExpires(DateTime.Now.Add(CACHE_DURATION));
            context.Response.Cache.SetMaxAge(CACHE_DURATION);
            context.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
            response.OutputStream.Write(bytes, 0, bytes.Length);
            response.Flush();
        }
    }
    /// <summary>
    /// 获取文件数据
    /// </summary>
    /// <param name="context"></param>
    /// <param name="virtualPath"></param>
    /// <param name="endcoding"></param>
    /// <returns></returns>
    private byte[] GetFileBytes(HttpContext context, string virtualPath, Encoding endcoding)
    {
        if (virtualPath.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadData(virtualPath);
            }
        }
        else
        {
            string physicalPath = context.Server.MapPath(virtualPath);
            byte[] bytes = File.ReadAllBytes(physicalPath);
            return bytes;
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