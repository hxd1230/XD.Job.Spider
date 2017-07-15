using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace XD.Job.Spider
{
    /// <summary>
    /// HandlerAjax 的摘要说明
    /// </summary>
    public class HandlerAjax : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int pageIndex = int.Parse(context.Request["pageIndex"]);
            string city = context.Request["city"];
            city = context.Server.UrlEncode(city);
            string keyWord = context.Request["kd"];
            context.Response.ContentType = "text/plain";
            string result = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://www.lagou.com/jobs/positionAjax.json?px=default&city={0}&needAddtionalResult=false", city));
            request.Referer = string.Format("https://www.lagou.com/jobs/list_Python?px=default&city={0}", city);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.115 Safari/537.36";
            request.Host = "www.lagou.com";
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Headers.Add("X-Anit-Forge-Code", "0");
            request.Headers.Add("X-Anit-Forge-Token", null);
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Method = "POST";
            string str = string.Format("first={0}&pn={1}&kd={2}", true, pageIndex, keyWord);
            byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(str);
            request.ContentLength = data.Length;
            Stream outStream = request.GetRequestStream();
            outStream.Write(data, 0, data.Length);
            outStream.Flush();
            outStream.Close();
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}