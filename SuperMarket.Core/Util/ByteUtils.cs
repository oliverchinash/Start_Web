using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.Util
{
    public class ByteUtils
    {
        public static string GetStringFromByte(object timestramp)
        {
            byte[] btTsArray = timestramp as byte[];
            string strTimeSpan =  BitConverter.ToString(btTsArray,0).Replace("-","") ;
            return strTimeSpan;
        }
        public static long GetLongFromByte(object timestramp)
        {
            byte[] btTsArray = timestramp as byte[];
            long strTimeSpan =  BitConverter.ToInt64(btTsArray, 0);
            return strTimeSpan;
        }
        public static byte[] GetByteFromString(string hexText)
        {
            byte[] buffer2;
     
                byte[] buffer = null;
                string hexString = hexText.Replace("0x", ""); 
                int num = 0;
                int num2 = hexString.Length / 2;
                buffer = new byte[num2];
                for (int i = 0; i < buffer.Length; i++)
                {
                    string s = new string(new char[] { hexString[num], hexString[num + 1] });
                    buffer[i] = byte.Parse(s, NumberStyles.HexNumber);
                    num += 2;
                }
                buffer2 = buffer;
       
            return buffer2;
        }
    }
}
