using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XD.Job.Spider
{
    /// <summary>
    /// HandlerCity 的摘要说明
    /// </summary>
    public class HandlerCity : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            List<string> citys = new List<string>();
            context.Response.ContentType = "text/plain";
            HtmlWeb htmlweb = new HtmlWeb();
            HtmlDocument htmlDoc = htmlweb.Load("https://www.lagou.com/zhaopin/Python/?labelWords=label");
            HtmlNodeCollection hnc = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"filterCollapse\"]/div[1]/div[2]/li/div[2]/div/a");
            foreach (var item in hnc)
            {
                citys.Add(item.InnerText.Trim());
            }
            string json = JsonConvert.SerializeObject(citys);
            context.Response.Write(json);
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