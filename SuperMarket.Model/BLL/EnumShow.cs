using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace SuperMarket.Model
{ 
    public class EnumEntityShow
	{
      /// <summary>
        /// 返回类的实例
        /// </summary>
        public static EnumEntityShow Instance
        {
            get { return SingletonCreator.instance; }
        }

        /// <summary>
        /// 单例创建者
        /// </summary>
        class SingletonCreator
        {
            internal static readonly EnumEntityShow instance = new EnumEntityShow();
        }
        private EnumEntityShow() { } 
        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public  string GetEnumDes( Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return "";
        }


	}
}
