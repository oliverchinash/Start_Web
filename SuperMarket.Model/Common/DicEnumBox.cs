using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    #region
    public enum WebSiteEnum
    {
        [Description("茶")]
        Tea = 1,
        [Description("玩具")]
        Toys = 2,
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
