 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SuperMarket.Core.Safe
{
    public static class CryptMD5
    {
        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string str)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');//左边的2表示宽度 右边的表示填充字符
            }
            
            return ret.ToUpper();
        }
    }
}
