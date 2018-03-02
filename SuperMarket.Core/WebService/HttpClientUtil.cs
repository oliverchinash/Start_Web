using SuperMarket.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.WebService
{
    public class HttpClientUtil
    {
      
        // REST @GET 方法，根据泛型自动转换成实体，支持List<T>
        public static string doGetMethodToObj(string methodUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(methodUrl);
            request.Method = "get";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse(); 
            string json = getResponseString(response);
            return json;
        }

        // 将 HttpWebResponse 返回结果转换成 string
        private static string getResponseString(HttpWebResponse response)
        {
            string json = null;
            using (StreamReader reader = new StreamReader(response.GetResponseStream(),Encoding.GetEncoding("UTF-8")))
            {
                json = reader.ReadToEnd();
            }
            return json;
        }

        // 获取异常信息
        private static string getRestErrorMessage(HttpWebResponse errorResponse)
        {
            string errorhtml = getResponseString(errorResponse);
            string errorkey = "spi.UnhandledException:";
            errorhtml = errorhtml.Substring(errorhtml.IndexOf(errorkey) + errorkey.Length);
            errorhtml = errorhtml.Substring(0, errorhtml.IndexOf("\n"));
            return errorhtml;
        }

        // REST @POST 方法
        public static string doPostMethodToObj(string metodUrl, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(metodUrl);
            request.Method = "post";
            request.ContentType = "application/json;charset=UTF-8";
            var stream = request.GetRequestStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(jsonBody);
                writer.Flush();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json = getResponseString(response);
            return json;
        }

        // REST @PUT 方法
        public static string doPutMethod(string metodUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(metodUrl);
            request.Method = "put";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                return reader.ReadToEnd();
            }
        }

        // REST @PUT 方法，带发送内容主体
        public static string doPutMethodToObj(string metodUrl, string jsonBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(metodUrl);
            request.Method = "put";
            request.ContentType = "application/json;charset=UTF-8";
            var stream = request.GetRequestStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(jsonBody);
                writer.Flush();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string json = getResponseString(response);
            return json;
        }

        // REST @DELETE 方法
        public static bool doDeleteMethod(string metodUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(metodUrl);
            request.Method = "delete";
            request.ContentType = "application/json;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("UTF-8")))
            {
                string responseString = reader.ReadToEnd();
                if (responseString.Equals("1"))
                {
                    return true;
                }
                return false;
            }
        }

        //根据实体，反射遍历实体所有属性，动态生成DataTable
        public static DataTable toDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                FieldInfo[] fieldInfos = list[0].GetType().GetFields();
                foreach (FieldInfo fi in fieldInfos)
                {
                    if (fi.Name.Length > 4 && fi.Name.LastIndexOf("Date") == fi.Name.Length - 4)
                    {
                        result.Columns.Add(fi.Name, "".GetType());
                        continue;
                    }
                    if (fi.Name.Length > 4 && fi.Name.LastIndexOf("Time") == fi.Name.Length - 4)
                    {
                        result.Columns.Add(fi.Name, "".GetType());
                        continue;
                    }
                    if (fi.Name.IndexOf("imagepath") >= 0)
                    {
                        result.Columns.Add(fi.Name, Image.FromFile("1.jpg").GetType());
                        continue;
                    }
                    result.Columns.Add(fi.Name, fi.FieldType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (FieldInfo fi in fieldInfos)
                    {
                        object obj = fi.GetValue(list[i]);
                        if (null == obj)
                        {
                            tempList.Add(obj);
                            continue;
                        }
                        if (fi.Name.Length > 4 && fi.Name.LastIndexOf("Date") == fi.Name.Length - 4)
                        {
                            int dateInt = (int)obj;
                            if (0 == dateInt)
                            {
                                tempList.Add("");
                                continue;
                            }
                            obj = DateTimeUtil.convertIntDatetime(dateInt).ToShortDateString();
                        }
                        if (fi.Name.Length > 4 && fi.Name.LastIndexOf("Time") == fi.Name.Length - 4)
                        {
                            int dateInt = int.Parse(obj.ToString());
                            if (0 == dateInt)
                            {
                                tempList.Add("");
                                continue;
                            }
                            obj = DateTimeUtil.convertIntDatetime(dateInt).ToString();
                        }
                        if (fi.Name.IndexOf("imagepath") >= 0)
                        {
                            if (null == obj)
                            {
                                tempList.Add("");
                                continue;
                            }
                            WebClient myWebClient = new WebClient();
                            MemoryStream ms = new MemoryStream(myWebClient.DownloadData( obj.ToString()));
                            obj = Image.FromStream(ms);
                        }
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

    }
}
