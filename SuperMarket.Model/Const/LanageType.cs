using System.ComponentModel;

namespace SuperMarket.Model 
{
    public static class LanageType
    {
        /// <summary>
        /// 中文
        /// </summary>
        public const string Chinese = "0";
        /// <summary>
        /// 英文
        /// </summary>
        public const string English = "1";

    }
     
    /// <summary>
    /// 是否商家
    /// </summary>
    public enum IsStore
    {
        [Description("是")]
        Yes=1,
        [Description("否")]
        No=0
    }

    public enum B2BNavType
    {
        [Description("是")]
        Default = 1,
        [Description("否")]
        SpecialPrice = 0
    }


}
