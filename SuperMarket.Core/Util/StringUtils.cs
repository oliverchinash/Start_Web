using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Globalization;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Collections.Specialized;

namespace SuperMarket.Core.Util
{
    #region Check Type

    //"^\\d+$"　　//非负整数（正整数 + 0） 
    //"^[0-9]*[1-9][0-9]*$"　　//正整数 
    //"^((-\\d+)|(0+))$"　　//非正整数（负整数 + 0） 
    //"^-[0-9]*[1-9][0-9]*$"　　//负整数 
    //"^-?\\d+$"　　　　//整数
    //"^\\d+(\\.\\d+)?$"　　//非负浮点数（正浮点数 + 0） 
    //"^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$"　　//正浮点数 
    //"^((-\\d+(\\.\\d+)?)|(0+(\\.0+)?))$"　　//非正浮点数（负浮点数 + 0） 
    //"^(-(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*)))$"　　//负浮点数 
    //"^(-?\\d+)(\\.\\d+)?$"　　//浮点数 
    //"^[A-Za-z]+$"　　//由26个英文字母组成的字符串 
    //"^[A-Z]+$"　　//由26个英文字母的大写组成的字符串 
    //"^[a-z]+$"　　//由26个英文字母的小写组成的字符串 

    //"^[A-Za-z0-9]+$"　　//由数字和26个英文字母组成的字符串 
    //"^\\w+$"　　//由数字、26个英文字母或者下划线组成的字符串 
    //"^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$"　　　　//email地址 
    //"^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$"　　//url
    //@"^[0-9]+([,]{1}[0-9]+)*$";数字和逗号组成
    public enum CheckType
    {
        Nonnegative = 0,
        Positive = 1,
        NegativePlusZero = 2,
        Negative = 3,
        Integer = 4,

        PositiveFloatPlusZero = 5,
        PositiveFloat = 6,

        NegativeFloatPlusZero = 7,
        NegativeFloat = 8,

        Float = 9,

        EnglishChar = 10,
        EnglishCharLowerCase = 11,
        EnglishCharUpCase = 12,

        EnglishNumber = 13,
        EnglishNumberUnderline = 14,

        EmailAddress = 15,
        URL = 16,
        Intes = 17,
    }
    #endregion

    public class StringUtils
    {
        /// <summary>
        /// URL地址做为参数时需要处理URL为安全字符串
        /// </summary>
        /// <param name="url">http://www.baidu.com</param>
        /// <returns>http|2a|3f|3f.baidu.com</returns>
        public static string FormatUrl(string url)
        {
            try
            {
                url = System.Web.HttpContext.Current.Server.UrlEncode(url);
                url = url.Replace('%', '|');

                return url;
            }
            catch
            {
                return url;
            }
        }
        /// <summary>
        /// URL地址做为参数时需要处理URL为安全字符串
        /// </summary>
        /// <param name="url">http://www.baidu.com</param>
        /// <returns>http|2a|3f|3f.baidu.com</returns>
        public static string DeFormatUrl(string url)
        {
            try
            {
                url = url.Replace('|', '%');
                url = System.Web.HttpContext.Current.Server.UrlDecode(url);

                return url;
            }
            catch
            {
                return url;
            }
        }

        /// <summary>
        /// 根据正则表达式检查
        /// </summary>
        /// <param name="input"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool CheckStringFormat(string input, CheckType type)
        {
            Regex regex;
            switch (type)
            {
                case CheckType.Nonnegative:
                    regex = new Regex("^\\d+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.Positive:
                    regex = new Regex("^[0-9]*[1-9][0-9]*$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.NegativePlusZero:
                    regex = new Regex("^((-\\d+)|(0+))$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.Negative:
                    regex = new Regex("^-[0-9]*[1-9][0-9]*$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.Integer:
                    regex = new Regex("^-?\\d+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.PositiveFloatPlusZero:
                    regex = new Regex("^\\d+(\\.\\d+)?$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.PositiveFloat:
                    regex = new Regex("^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.NegativeFloatPlusZero:
                    regex = new Regex("^((-\\d+(\\.\\d+)?)|(0+(\\.0+)?))$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.NegativeFloat:
                    regex = new Regex("^(-(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*)))$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.Float:
                    regex = new Regex("^(-?\\d+)(\\.\\d+)?$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.EnglishChar:
                    regex = new Regex("^[A-Za-z]+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.EnglishCharUpCase:
                    regex = new Regex("^[A-Z]+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.EnglishCharLowerCase:
                    regex = new Regex("^[a-z]+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.EnglishNumber:
                    regex = new Regex("^[A-Za-z0-9]+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.EnglishNumberUnderline:
                    regex = new Regex("^\\w+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.EmailAddress:
                    regex = new Regex("^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$", RegexOptions.Compiled);
                    return regex.IsMatch(input);

                case CheckType.URL:
                    regex = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Compiled);

                    return regex.IsMatch(input);
                case CheckType.Intes:
                    regex = new Regex(@"^[0-9]+([,]{1}[0-9]+)*$", RegexOptions.Compiled); 
                    return regex.IsMatch(input);

                default:
                    return false;
            }
        }
        /// <summary>
        /// 替换HTML标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///转全角的函数(SBC case)
        ///任意字符串
        ///全角字符串
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }

        /// <summary>
        ///转半角的函数(DBC case)
        ///任意字符串
        ///半角字符串
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }

        /// <summary>
        /// 判断字符串中是否存在全角字符，汉字
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        public static int CheckExistSBC(string check)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = check.ToCharArray();

            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// 从首字符取被切字符串的定长字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubString(string input, int length)
        {
            if (string.IsNullOrEmpty(input)) return "";
            //双字节中文字符集ASSI编码范围
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            char[] stringChar = input.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;

            for (int i = 0; i < stringChar.Length; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }

                if (nLength >= length)
                {
                    break;
                }
            }
            //sb.Append("...");
            return sb.ToString();
        }

        /// <summary>
        /// 取用掩码替换字符串指定位置以后字符
        /// 如手机，地址，邮件地址等在Page上的显示
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetStringWithMark(string input, int fromPosition)
        {
            char[] stringChar = input.ToCharArray();
            //如果替换起始位置大于字符长度
            //返回原字符
            if (fromPosition >= stringChar.Length)
            {
                return input;
            }

            StringBuilder sb = new StringBuilder();
            int nLength = 0;

            for (int i = 0; i < stringChar.Length; i++)
            {
                if (nLength >= fromPosition)
                {
                    sb.Append("*");
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength++;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取URL内容　UTF8编码
        /// </summary>
        /// <param name="ContentURL">URL地址</param>
        /// <returns></returns>
        public static string GetContent(string ContentURL)
        {
            try
            {
                Encoding enc = Encoding.UTF8;
                //Encoding enc = Encoding.Default;
                Uri uri = new Uri(ContentURL);

                HttpWebRequest hwreq = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse hwrsp = (HttpWebResponse)hwreq.GetResponse();

                byte[] bts = new byte[(int)hwrsp.ContentLength];
                Stream s = hwrsp.GetResponseStream();
                for (int i = 0; i < bts.Length; )
                {
                    i += s.Read(bts, i, bts.Length - i);
                }
                string content = enc.GetString(bts);
                return content;
            }
            catch (Exception ex)
            {
                LogUtil.Log("StringUtil", (ex.InnerException == null ? ex.Message.ToString() : ex.InnerException.Message.ToString()));
                return "";
            }
        }


        /// <summary>
        /// 编码　默认编码
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string GBKUrlEncode(string k)
        {
            return System.Web.HttpUtility.UrlEncode(k, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 解码　默认编码
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public static string GBKUrlDecode(string k)
        {
            return System.Web.HttpUtility.UrlDecode(k, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 返回Bool类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetDbBool(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return false;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return bool.Parse(tempd);
            }
        }

        /// <summary>
        /// 转为Bool类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetDbBool(string text)
        {
            return GetDbBool(text, false);
        }
        /// <summary>
        /// 转为时间类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static DateTime SafeDateTime(string text, DateTime defaultValue)
        {
            DateTime time;
            if (DateTime.TryParse(text, out time))
            {
                defaultValue = time;
            }
            return defaultValue;
        }

        /// <summary>
        /// 转为Int类型 
        /// </summary>
        /// <param name="text"></param> 
        /// <returns></returns>
        public static int SafeInt(string text)
        {
            return SafeInt(text, 0);
        }

        /// <summary>
        /// 转为Bool类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetDbBool(string text, bool defaultValue)
        {
            bool flag;
            if (bool.TryParse(text, out flag))
            {
                defaultValue = flag;
            }
            return defaultValue;
        }

        /// <summary>
        /// 替换不安全字符
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string SafeCode(string Str)
        {
            string text1 = "" + Str;
            if (text1 != "")
            {
                text1 = text1.Replace("<", "&lt");
                text1 = text1.Replace(">", "&gt");
                //text1 = text1.Replace(",", "，");
                text1 = text1.Replace("'", "‘");
                text1 = text1.Replace("\"", "＂");
                text1 = text1.Replace("update", "");
                text1 = text1.Replace("insert", "");
                text1 = text1.Replace("delete", "");
                text1 = text1.Replace("--", "");
                text1 = text1.Replace("%", "");
                text1 = text1.Replace(";", "");
                //text1 = text1.Replace(",", "");

                text1 = text1.Replace("alert", "");
                text1 = text1.Replace("javascript", "");
            }
            return text1;
        }
        /// <summary>
        /// 替换不安全字符
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string SafeCodeSmall(string Str)
        {
            string text1 = "" + Str;
            if (text1 != "")
            {  
                text1 = text1.Replace("update", "");
                text1 = text1.Replace("insert", "");
                text1 = text1.Replace("delete", ""); 
                text1 = text1.Replace("alert", "");
                text1 = text1.Replace("javascript", "");
            }
            return text1;
        }

        /// <summary>
        /// 组合数组
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string ToDelimitedString(ICollection collection, string delimiter)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (collection is Hashtable)
            {
                foreach (object obj2 in ((Hashtable)collection).Keys)
                {
                    builder.Append(obj2.ToString() + delimiter);
                }
            }
            if (collection is ArrayList)
            {
                foreach (object obj3 in (ArrayList)collection)
                {
                    builder.Append(obj3.ToString() + delimiter);
                }
            }
            if (collection is string[])
            {
                foreach (string str in (string[])collection)
                {
                    builder.Append(str + delimiter);
                }
            }
            if (collection is MailAddressCollection)
            {
                foreach (MailAddress address in (MailAddressCollection)collection)
                {
                    builder.Append(address.Address + delimiter);
                }
            }
            return builder.ToString().TrimEnd(new char[] { Convert.ToChar(delimiter, CultureInfo.InvariantCulture) });
        }

        /// <summary>
        /// 返回整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetDbInt(object obj)
        {
            if (obj == null || obj == System.DBNull.Value  || obj.ToString() == "" || obj.ToString() == System.Boolean.FalseString )
            {
                return 0;
            }
            else if(obj.ToString() == System.Boolean.TrueString)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 返回整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetDbInt(string obj)
        {
            int temp;
            if (string.IsNullOrEmpty(obj))
            {
                return 0;
            }
            else if (!int.TryParse(obj.Replace(",", ""), out temp))
            {
                return 0;
            }
            return int.Parse(obj.Replace(",", ""));
        }

        /// <summary>
        /// 返回整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetDbInt(string obj, int defaultValue)
        {
            int temp;
            if (string.IsNullOrEmpty(obj))
            {
                return defaultValue;
            }
            else if (!int.TryParse(obj.Replace(",", ""), out temp))
            {
                return temp;
            }
            return int.Parse(obj.Replace(",", ""));
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDbString(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return String.Empty;
            }
            else
            {
                return obj.ToString().Trim();
            }
        }

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetDbString(object obj, string defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                return obj.ToString().Trim();
            }
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDbDateTime(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(obj);
            }
        }

        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDbDateTime(object obj, DateTime defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return defaultValue;
            }
            else
            {
                DateTime time;
                if (DateTime.TryParse(obj.ToString(), out time))
                {
                    defaultValue = time;
                }
                return defaultValue;
            }
        }

        /// <summary>
        /// 返回Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal GetDbDecimal(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return decimal.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Decimal GetDbDecimal(object obj, decimal defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return defaultValue;
            }
            else
            {
                string tempd = obj.ToString();
                return decimal.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Decimal GetDbDecimal(string obj)
        {
            decimal dec;
            if (obj == null || !decimal.TryParse(obj, out dec))
            {
                return 0;
            }
            else
            {
                return dec;
            }
        }

        /// <summary>
        /// 返回double类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetDbDouble(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return double.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回double类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double GetDbDouble(object obj, double defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return defaultValue;
            }
            else
            {
                string tempd = obj.ToString();
                return double.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回double类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetDbDouble(string obj)
        {
            double dec;
            if (obj == null || !double.TryParse(obj, out dec))
            {
                return 0;
            }
            else
            {
                return dec;
            }
        }

        /// <summary>
        /// 返回float类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float GetDbFloat(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return float.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回double类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float GetDbFloat(object obj, float defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return defaultValue;
            }
            else
            {
                string tempd = obj.ToString();
                return float.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回float类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static float GetDbFloat(string obj)
        {
            float dec;
            if (obj == null || !float.TryParse(obj, out dec))
            {
                return 0;
            }
            else
            {
                return dec;
            }
        }

        /// <summary>
        /// 返回Int16类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int16 GetDbInt16(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return Int16.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回Int16类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Int16 GetDbInt16(object obj, Int16 defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return defaultValue;
            }
            else
            {
                string tempd = obj.ToString();
                return Int16.Parse(tempd);
            }
        }

        /// <summary>
        /// 返回Int16类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int16 GetDbInt16(string obj)
        {
            Int16 dec;
            if (obj == null || !Int16.TryParse(obj, out dec))
            {
                return 0;
            }
            else
            {
                return dec;
            }
        }

        /// <summary>
        /// 返回byte类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte GetDbByte(object obj)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return 0;
            }
            else
            {
                string tempd = obj.ToString().TrimEnd('%');
                return byte.Parse(tempd);
            }
        }
        /// <summary>
        /// 返回Int16类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static byte GetDbByte(object obj, byte defaultValue)
        {
            if (obj == null || obj == System.DBNull.Value || obj.ToString() == "")
            {
                return defaultValue;
            }
            else
            {
                string tempd = obj.ToString();
                return byte.Parse(tempd);
            }
        }
        /// <summary>
        /// 返回Int16类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte GetDbByte(string obj)
        {
            byte dec;
            if (obj == null || !byte.TryParse(obj, out dec))
            {
                return 0;
            }
            else
            {
                return dec;
            }
        }
        /// <summary>
        /// 返回长整型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long GetDbLong(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
            {
                return 0;
            }
            else
            {
                long lId = 0;
                long.TryParse(obj.ToString(), out lId); 
                return lId;
            }
        }
        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(string date)
        {
            DateTime dt;
            if (DateTime.TryParse(date, out dt))
                return dt;
            return DateTime.MinValue;
        }
        /// <summary>
        /// 返回日期
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            DateTime dt;
            if (obj == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }


        /// <summary>
        /// 布尔类型转换为整型
        /// </summary>
        /// <param name="obj">object数据源</param>
        /// <returns></returns>
        public static int BoolToInt(object obj)
        {
            if (Convert.ToBoolean(obj) == true)
                return 1;
            else
                return 0;
        }


        /// <summary>
        /// 截取字符串　按字节计算
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="nBytes"></param>
        /// <param name="type">0 添加...</param>
        /// <returns></returns>
        public static string TrimByteStr(object obj, int nBytes, int type)
        {
            nBytes = nBytes * 2;
            if (obj == null)
            {
                return "";
            }
            if (nBytes <= 0)
                return "";

            if (nBytes % 2 != 0)
                nBytes++;

            byte[] blist = System.Text.Encoding.GetEncoding("Gb2312").GetBytes(obj.ToString());
            if (blist.Length > nBytes)
            {
                if (type == 0)
                {
                    return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes).Replace("?", "") + "...";
                }
                else
                {
                    return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes).Replace("?", "");
                }
            }
            else
            {
                return obj.ToString();
            }
            /*if (nBytes > blist.Length)
                nBytes = blist.Length;
            if (type == 0)
            {
                return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes) + "...";
            }
            else
            {
                return System.Text.Encoding.GetEncoding("Gb2312").GetString(blist, 0, nBytes);
            }*/
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="len"></param>
        /// <param name="type">0 添加...</param>
        /// <returns></returns>
        public static string TrimStr(object obj, int len, int type)
        {
            if (obj != null)
            {
                if (obj.ToString().Length > len)
                {
                    if (type == 0)
                    {
                        return obj.ToString().Substring(0, len) + "...";
                    }
                    else
                    {
                        return obj.ToString().Substring(0, len);
                    }
                }
                else
                {
                    return obj.ToString();
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 去除HTML代码
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string StripHT(object strHtml)
        {
            Regex regex = new Regex("<.+?>", RegexOptions.IgnoreCase);
            string strOutput = regex.Replace(strHtml.ToString(), "");
            return strOutput.Replace("&nbsp;", "");
        }
        /// <summary>
        /// 检查字符串是否是电子邮件地址
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsEmail(string request)
        {
            if (request == null)
                return false;
            Regex regex = new Regex(@"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return regex.IsMatch(request.Trim());
        }
        /// <summary>
        /// 值是否为手机号
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsMobile(string request)
        {
            //Regex reg = new Regex(@"^1(?:3|5|8)\d{9}$");
            Regex reg = new Regex(@"^1[3,5,8]{1}[0-9]{1}[0-9]{8}$");
            if (reg.IsMatch(request))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 转为Int类型 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int SafeInt(string text, int defaultValue)
        {
            int num;
            if (int.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }
        /// <summary>
        /// 转为Decimal类型
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal SafeDecimal(string text, decimal defaultValue)
        {
            decimal num;
            if (decimal.TryParse(text, out num))
            {
                defaultValue = num;
            }
            return defaultValue;
        }

        /// <summary>
        /// 检查字符串是否是日期
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsDateTimeAvailable(string request)
        {
            return IsDateTimeAvailable(request, DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// 检查字符串是否是指定的日期
        /// </summary>
        /// <param name="request"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns></returns>
        public static bool IsDateTimeAvailable(string request, DateTime minValue, DateTime maxValue)
        {
            DateTime req_Time;
            try
            {
                req_Time = DateTime.Parse(request);
            }
            catch
            {
                return false;
            }
            if (req_Time > maxValue || req_Time < minValue)
                return false;

            return true;
        }

        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string SafeStr(string text)
        {
            return SafeStr(text, string.Empty);
        }
        /// <summary>
        /// 转为字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string SafeStr(string text, string defaultValue)
        {
            if (String.IsNullOrEmpty(text))
            {
                return defaultValue;
            }
            return text.ToString();
        }
        /// <summary>  
        /// 返回本对象的Json序列化  
        /// </summary>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public static string ToJson(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj).Replace("\"", "'");
        }
        /// <summary>
        /// 验证身份证号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsUserIdentityNumAvailable(string id)
        {
            if (id == null)
                return false;
            id = id.Trim();

            Match match = Regex.Match(id, @"\d{18}|\d{17}\w");
            if (!match.Success)
                match = Regex.Match(id, @"\d{15}");
            if (!match.Success)
                return false;
            if (match.Value != id)
                return false;

            return true;
        }

        /// <summary>
        /// 生成随机密码
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRadomPassword(string chars, int length)
        {
            string temp = "";
            int charIndex;
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                charIndex = rnd.Next(chars.Length);
                temp += chars[charIndex];
            }

            return temp;
        }
        /// <summary>
        /// 生成随机数的种子
        /// </summary>
        /// <returns></returns>
        private static int getNewSeed()
        {
            byte[] rndBytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(rndBytes);
            return BitConverter.ToInt32(rndBytes, 0);
        }     /// <summary>
              /// 生成8位随机数
              /// </summary>
              /// <param name="length"></param>
              /// <returns></returns>
        static public string GetRandomString(int len)
        {
            string s = "123456789ABCDEFGHIJKLMNPQRSTUVWXYZ";
            string reValue = string.Empty;
            Random rnd = new Random(getNewSeed());
            while (reValue.Length < len)
            {
                string s1 = s[rnd.Next(0, s.Length)].ToString();
                if (reValue.IndexOf(s1) == -1) reValue += s1;
            }
            return reValue;
        }
        /// <summary>
        /// 替换延时加载图片
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string GetLazyImgSrc(string Content)
        {
            return Regex.Replace(Content, "(?<=<img[^>]*?) src=", " src=\"http://i2.mbscss.com/global/grey.gif\" data-original=", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 替换部分延时加载图片
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static string GetPartLazyImgSrc(string Content)
        {
            Content = Regex.Replace(Content, "(?<=<!--LazyBegin(\\d?)-->.*?<img[^>]*?) src=(?=.*?<!--LazyEnd\\1-->)", " src=\"http://i2.mbscss.com/global/grey.gif\" data-original=", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            return Content;
        }


        /// <summary>
        /// 将纯文本转换为Html代码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToHtmlString(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;

            System.Text.StringBuilder sb = new System.Text.StringBuilder(System.Web.HttpContext.Current.Server.HtmlEncode(text));

            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("\r\n", "<br>");
            sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }

        /// <summary>
        /// 将Html代码转换为纯文本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string ToTextString(string html)
        {
            if (string.IsNullOrEmpty(html)) return html;

            System.Text.StringBuilder sb = new System.Text.StringBuilder(System.Web.HttpContext.Current.Server.HtmlDecode(html));
            sb.Replace("<br>", "\r\n");
            sb.Replace("&nbsp;", " ");

            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            return sb.ToString();
        }

        /// <summary>
        /// 清除危险脚本
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetSafeHtml(string html)
        {
            //过滤<script></script>标记
            html = Regex.Replace(html, @"<script[\s\S]+</script *>", "", RegexOptions.IgnoreCase);
            //过滤href=javascript: (<A>) 属性
            html = Regex.Replace(html, @" href *= *[\s\S]*script *:", "", RegexOptions.IgnoreCase);
            //过滤其它控件的on...事件
            html = Regex.Replace(html, @" on[\s\S]*=", " _disibledevent=", RegexOptions.IgnoreCase);
            //过滤iframe
            html = Regex.Replace(html, @"<iframe[\s\S]+</iframe *>", "", RegexOptions.IgnoreCase);
            //过滤frameset
            html = Regex.Replace(html, @"<frameset[\s\S]+</frameset *>", "", RegexOptions.IgnoreCase);

            return html;
        }

        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="html">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string GetNoHtmlText(string html)
        {
            //删除HTML
            html = Regex.Replace(html, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"-->", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"<!--.*", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            html.Replace("<", "");
            html.Replace(">", "");
            html.Replace("\r\n", " ");

            return html;
        }
        /// <summary>
        /// 是否手持设备
        /// </summary>
        /// <param name="str_handset"></param>
        /// <returns></returns>
        public static bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,4,5,7,8]+\d{9}");
        }
        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="CString"></param>
        /// <returns></returns>
        public static bool IsChinaString(string CString)
        {
            bool BoolValue = false;
            for (int i = 0; i < CString.Length; i++)
            {
                if (Convert.ToInt32(Convert.ToChar(CString.Substring(i, 1))) < Convert.ToInt32(Convert.ToChar(128)))
                {
                    BoolValue = false;
                }
                else
                { 
                    return BoolValue = true;
                }
            } 
            return BoolValue;
        }


        /// <summary>
        /// 是否中文
        /// </summary>
        /// <param name="CString"></param>
        /// <returns></returns>
        public static IList<string> GetLuceneChinaStrAttr(string CString)
        {
            IList<string>  strsttr = new  List<string>();
            bool IsChina = false;
            bool IsChinaPre = false;
            string temp = "";
            int j = 0;//判断数组开头
            for (int i = 0; i < CString.Length; i++,j++)
            {
                
                string chartemp = CString.Substring(i, 1);
                if ((chartemp==" "&&!string.IsNullOrEmpty(temp)))
                {
                    if (IsChinaPre)
                    {
                        strsttr.Add(temp);
                    }
                    else
                    {
                        strsttr.Add(temp + "*");
                    }
                    temp = "";
                    j = 0;
                    continue;
                }
                if (Convert.ToInt32(Convert.ToChar(chartemp)) < Convert.ToInt32(Convert.ToChar(128)))
                {   
                    IsChina = false;
                }
                else
                {
                    IsChina = true;
                }
                //if (j==0|| IsChinaPre== IsChina)
                //{
                //    temp += CString.Substring(i, 1);
                //}
              
                if (j == 0 || (IsChinaPre== IsChina&&!IsChinaPre))
                {
                    temp += chartemp;
                }
                else 
                {
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (IsChinaPre)
                        {
                            strsttr.Add(temp);
                        }
                        else
                        {
                            strsttr.Add(temp + "*");
                        }
                        temp = chartemp;
                    }
                    else
                    {
                        temp = chartemp;
                    }
                }
                if(i== CString.Length-1)
                {
                    if (IsChinaPre)
                    {
                        strsttr.Add(temp);
                    }
                    else
                    {
                        strsttr.Add(temp + "*");
                    }
                }
                IsChinaPre = IsChina; 
            }
            return strsttr;
        }

        /// <summary>
        /// 解析网址
        /// </summary>
        /// <param name="url"></param>
        /// <param name="baseUrl"></param>
        /// <param name="nvc"></param>
        public static void ParseUrl(string url, out string baseUrl, out Dictionary<string, string> nvc)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            nvc = new Dictionary< string, string> ();
            baseUrl = "";
            if (url == "")
                return;
            int questionMarkIndex = url.IndexOf('?');
            if (questionMarkIndex == -1)
            {
                baseUrl = url;
                return;
            }
            baseUrl = url.Substring(0, questionMarkIndex);
            if (questionMarkIndex == url.Length - 1)
                return;
            string ps = url.Substring(questionMarkIndex + 1);
            // 开始分析参数对  
            Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
            MatchCollection mc = re.Matches(ps);
            foreach (Match m in mc)
            {
                nvc.Add(m.Result("$2").ToLower(), m.Result("$3"));
            }
        }


        public static string AppendParams(string url, Dictionary<string, string> paramsnew)
        {
            string[] urlattr = url.Split('#');
            string baseurlSearch = urlattr[0];
            string hash="";
            if (urlattr.Length>1)
            {
                hash = urlattr[1];
            }
            string baseurl = "";
            Dictionary<string, string> attrpa = new Dictionary<string, string>();

            ParseUrl(baseurlSearch, out baseurl, out attrpa);

            foreach (string key in paramsnew.Keys)
            {
                if (attrpa.ContainsKey(key))
                {
                    attrpa[key] = paramsnew[key];
                }
                else
                {
                    attrpa.Add(key, paramsnew[key]);
                }
            }
            baseurl += "?";
            foreach (string key in attrpa.Keys)
            {
                baseurl += "&" +key +"="+ attrpa[key];
            }
              
            if (!string.IsNullOrEmpty(hash)) {
                baseurl = baseurl + '#' + hash;
            }
            return baseurl;
        }
    }
}
