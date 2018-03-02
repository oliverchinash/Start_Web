using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel; 
namespace SuperMarket.Core.Enums
{
    public class EnumShow
    {

        /// <summary>
        /// 返回类的实例
        /// </summary>
        public static EnumShow Instance
        {
            get { return SingletonCreator.instance; }
        }


        /// <summary>
        /// 单例创建者
        /// </summary>
        class SingletonCreator
        {
            internal static readonly EnumShow instance = new EnumShow();
        }
        private EnumShow() { }

        /// <summary>
        /// 获取描述信息 
        /// </summary>
        /// <param name="en">枚举</param>
        /// <returns></returns>
        public string GetEnumDes(Enum en)
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
        public IList<EnumUnitEntity> GetListByEnum<T>()
        {
            IList<EnumUnitEntity> list = new List<EnumUnitEntity>();
            Type type = typeof(T);
            foreach (int myCode in Enum.GetValues(type))
            {
                EnumUnitEntity entity = new EnumUnitEntity();
                entity.EnumValue = myCode;
                entity.EnumName = Enum.GetName(type, myCode);//获取名称
                MemberInfo[] memInfo = type.GetMember(entity.EnumName);
                if (memInfo != null && memInfo.Length > 0)
                {
                    object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                    if (attrs != null && attrs.Length > 0)
                        entity.EnumDes = ((DescriptionAttribute)attrs[0]).Description;
                }
                list.Add(entity);
            }
            return list;
        }

    }
}
