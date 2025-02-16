﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace Yushen.Util
{

    /// <summary> 
    /// 有关HTTP/HTTPS请求的辅助类 lzpong
    /// 默认 UTF-8 编码
    /// </summary> 
    public class HTTPS
    {

        public HTTPS()
        {
            //spt = SecurityProtocolType.Tls;
            requestEncoding = Encoding.GetEncoding("UTF-8");
            requestUserAgent = DefaultUserAgent;
        }

        /// <summary>
        /// SecurityProtocolType
        /// 如果https出现"基础连接..."或"ssl/tls..."错误,更改此属性; 也可能需要升级 .net 已支持更新的安全协议
        /// 不指定,使之自动协商/适应, 避免指定的版本与服务器不一样反而连不上
        /// </summary>
        //public static SecurityProtocolType spt { get; set; }

        //设置编码   默认编码是UTF-8
        public static Encoding requestEncoding { get; set; }

        //设置编码
        public static void SetEncoding(string endcode) { requestEncoding = Encoding.GetEncoding(endcode); }

        private static string requestUserAgent = DefaultUserAgent;

        private static readonly string DefaultUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";

        //设置浏览器版本
        public static void SetUserAgent(string userAgent = null)
        {
            if (!String.IsNullOrWhiteSpace(userAgent)) requestUserAgent = userAgent; else requestUserAgent = DefaultUserAgent;
        }

        /// <summary>
        /// 创建GET方式的HTTP/HTTPS请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>
        /// <returns></returns>
        public static HttpWebResponse CreateGet(string url, int? timeout = null, string userAgent = null, CookieCollection cookies = null)
        {
            return Create(url, null, "GET", requestEncoding, timeout, cookies.ToString());
        }

        /// <summary>
        /// 创建POST方式的HTTP/HTTPS请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="datas">随同请求POST的数据</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="headers">随同HTTP请求发送的headers信息，如Cookie等</param>
        /// <returns></returns>
        public static HttpWebResponse CreatePost(string url, string datas = null, int? timeout = null, string headers = null)
        {
            return Create(url, datas, "POST", requestEncoding, timeout, headers);
        }

        /// <summary>
        /// 创建HTTP/HTTPS请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="datas">随同请求POST的参数名称及参数值字典</param>
        /// <param name="timeout">请求的超时时间</param>
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>
        /// <param name="headers">随同HTTP请求发送的headers信息，如Cookie等</param>
        /// <param name="AllowAutoRedirect">请求应自动跟随 Internet 资源的重定向响应，则为 true，否则为 false。默认值为 true。</param>
        /// <returns></returns>
        public static HttpWebResponse Create(string url, string datas = null, string method = "GET", Encoding requestEncoding = null, int? timeout = null, string headers = null, bool AllowAutoRedirect = true)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            if (requestEncoding == null)
                requestEncoding = Encoding.GetEncoding("UTF-8");

            HttpWebRequest request = null;
            //如果是发送HTTPS请求 
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //ServicePointManager.SecurityProtocol = spt; //不指定,使之自动协商/适应, 避免指定的版本与服务器不一样反而连不上
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
                request = WebRequest.Create(url) as HttpWebRequest;

            request.Method = method;
            request.UserAgent = requestUserAgent;
            request.AllowAutoRedirect = AllowAutoRedirect;

            if (timeout.HasValue)
                request.Timeout = timeout.Value;

            if (!string.IsNullOrWhiteSpace(headers))
            {
                string[] arr1 = headers.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in arr1)
                {
                    string[] arr2 = s.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                    if (arr2[0] == "Referer")
                        request.Referer = arr2[1];
                    else if (arr2[0] == "User-Agent")
                        request.UserAgent = arr2[1];
                    else if (arr2[0] == "Accept")
                        request.Accept = arr2[1];
                    else if (arr2[0] == "Range")
                    {
                        string[] arr3 = arr2[1].Split('-');
                        long f = long.Parse(arr3[0]);
                        long t = 0;
                        if (arr3.Length > 1)
                            t = long.Parse(arr3[1]);
                        if (t < f)
                            request.AddRange(f, t);
                        else
                            request.AddRange(f);
                    }
                    else
                        request.Headers.Add(arr2[0], arr2[1]);
                }
            }
            //如果需要POST数据 
            if (method == "POST")
            {
                request.ContentType = "application/x-www-form-urlencoded";
                if (!String.IsNullOrWhiteSpace(datas))
                {
                    byte[] data = requestEncoding.GetBytes(datas);
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        /// <summary>
        /// GET 方法请求 获取文本内容
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="headers">随同HTTP请求发送的headers信息，如Cookie等</param>
        ///
        public static string Get(string url, string heads = null, string encoding = null)
        {
            HttpWebResponse rq = Create("GET", url, null, null, null, heads);
            Stream rs = rq.GetResponseStream();
            int bt;
            MemoryStream mm = new MemoryStream(128);
            while ((bt = rs.ReadByte()) > -1)
            {
                mm.WriteByte((byte)bt);
            }
            string data = Encoding.UTF8.GetString(mm.GetBuffer());//默认 utf8 编码
            if (rq.ContentType.IndexOf("html") > -1) //网页内容
            {
                int n = data.IndexOf("content-type");
                if (n > 0)
                {//
                    n = data.IndexOf("charset", n);
                    if (n > 0) //去网页内部指明的编码
                    {
                        int m = data.IndexOf("\"", n + 1);
                        n = data.IndexOf("=", n) + 1;
                        string c = data.Substring(n, m - n).Trim();
                        data = Encoding.GetEncoding(c).GetString(mm.GetBuffer());
                    }
                }
            }
            else if (!string.IsNullOrWhiteSpace(rq.CharacterSet) && rq.ContentType.IndexOf(';') > 0) //非网页
            {
                data = Encoding.GetEncoding(rq.CharacterSet).GetString(mm.GetBuffer());
            }
            return data.Replace("\0", "");//要去掉末尾的'\0'
        }
    }
}