using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace SuperMarket.Core.WebService
{
    public class Restparam
    {
        public string Key;
        public string Value;
    }
    public class RestService
    {
        public static string HttpGet(string url, IList<Restparam> listparam)
        {
            StringBuilder paramz = new StringBuilder();
            if (listparam != null && listparam.Count > 0)
            { 
            foreach (Restparam entity in listparam)
            {
                paramz.Append(entity.Key);
                paramz.Append("=");
                paramz.Append(HttpUtility.UrlEncode(entity.Value));
                paramz.Append("&");
            }
        }
            url += "?" + paramz.ToString();
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }
        public static string HttpPostParam(string url, IList<Restparam> listparam)
        {
            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded"; 
            StringBuilder paramz = new StringBuilder();
            if (listparam != null && listparam.Count > 0)
            {
                foreach (Restparam entity in listparam)
                {
                    paramz.Append(entity.Key);
                    paramz.Append("=");
                    paramz.Append(HttpUtility.UrlEncode(entity.Value));
                    paramz.Append("&");
                }
            } 
            byte[] formData = UTF8Encoding.UTF8.GetBytes(paramz.ToString());
            req.ContentLength = formData.Length; 
            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            } 
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;
        }


        public static string HttpPost(string url ,string param )
        {

            HttpWebRequest req = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded"; 
            byte[] formData = UTF8Encoding.UTF8.GetBytes(param);
            req.ContentLength = formData.Length;
            using (Stream post = req.GetRequestStream())
            {
                post.Write(formData, 0, formData.Length);
            }
            string result = null;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
            }
            return result;



            //WebRequest req = WebRequest.Create(new Uri("服务地址"));
            //req.Method = "POST";
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes("参数值");
            //req.ContentType = "applicationson";
            //req.ContentLength = bytes.Length;
            //string str = string.Empty;
            //using (Stream postStream = req.GetRequestStream())
            //{
            //    postStream.Write(bytes, 0, bytes.Length);
            //}
            //using (WebResponse hwr = req.GetResponse())
            //{
            //    using (StreamReader st = new StreamReader(hwr.GetResponseStream(), System.Text.Encoding.UTF8))
            //    {
            //        str = HttpUtility.UrlDecode(st.ReadToEnd());
            //    }
            //}
            //if (!string.IsNullOrEmpty(str))
            //    return str;
            //else
            //    return null;
        }


    }
   
}
