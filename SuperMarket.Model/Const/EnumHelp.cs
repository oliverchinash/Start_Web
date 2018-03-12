using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Model.Const
{

    /// <summary>
    /// 枚举的显示名称
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumShowNameAttribute : Attribute
    {
        private string showName;//字段

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName//属性
        {
            get
            {
                return this.showName;
            }
        }

        /// <summary>
        /// 构造枚举的显示名称
        /// </summary>
        /// <param name="showName">显示名称</param>
        public EnumShowNameAttribute(string showName)//构造函数
        {
            this.showName = showName;
        }
    }

    public class EnumHelp
    {
        private static Dictionary<string, Dictionary<string, string>> _EnumList = new Dictionary<string, Dictionary<string, string>>(); //枚举缓存池

        /// <summary>
        /// 将枚举转换成Dictionary<int, string>
        /// Dictionary中，key为枚举项对应的int值；value为：若定义了EnumShowName属性，则取它，否则取name
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<string, string> EnumToDictionary(Type enumType)
        {
            string keyName = enumType.FullName;

            if (!_EnumList.ContainsKey(keyName))
            {
                Dictionary<string, string> list = new Dictionary<string, string>();

                foreach (string i in Enum.GetValues(enumType))
                {
                    string name = Enum.GetName(enumType, i);

                    //取显示名称
                    string showName = string.Empty;

                    object[] atts = enumType.GetField(name).GetCustomAttributes(typeof(EnumShowNameAttribute), false);
                    if (atts.Length > 0) showName = ((EnumShowNameAttribute)atts[0]).ShowName;

                    list.Add(i, string.IsNullOrEmpty(showName) ? name : showName);
                }

                object syncObj = new object();

                if (!_EnumList.ContainsKey(keyName))
                {
                    lock (syncObj)
                    {
                        if (!_EnumList.ContainsKey(keyName))
                        {
                            _EnumList.Add(keyName, list);
                        }
                    }
                }
            }

            return _EnumList[keyName];
        }

        /// <summary>
        /// 获取枚举值对应的显示名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="intValue">枚举项对应的int值</param>
        /// <returns></returns>
        public static string GetEnumShowName(Type enumType, string intValue)
        {
            return EnumToDictionary(enumType)[intValue];
        }
    }

}
