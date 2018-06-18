using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    #region

    public enum SourcePicTypeEnum
    {
        [Description("默认产品图")]
        Product = 1,
         

    }
    /// <summary>
    /// 分类列表种类
    /// </summary>
    public enum ClassMenuTypeEnum
    {
        [Description("默认基础分类")]
        Default = 1,
        [Description("通用分类")]
        Normal = 2,
        [Description("PC端分类")]
        NormalPC = 3,
        [Description("手机端首页展示位分类")]
        HomeNavMobile = 4,

    }

    public enum SiteIdEnum
    {
        [Description("初始站点")]
        Tea = 1,
        [Description("玩具")]
        Toys = 2,
    }
    public enum ClassTypeEnum
    {
        [Description("基础分类")]
        Tea = 1001, 
    }
    #endregion
    /// <summary>
    /// 产品单位对应
    /// </summary>
    public enum UnitEnum
    {
        [Description("组")]
        ZU = 1,
        [Description("个")]
        GE = 2,
        [Description("件")]
        JIAN = 3,
        [Description("箱")]
        XIANG = 4,
        [Description("套")]
        TAO = 5,
        [Description("桶")]
        TONG = 6,
        [Description("盒")]
        HE = 7
    }
}
