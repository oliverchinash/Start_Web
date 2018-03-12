using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
   
    /// <summary>
    /// 微信信息获取方式
    /// </summary>
    public enum WeChatStatus
    {
        [Description("简易信息")]
        ShortInfo = 1,
        [Description("全部信息")]
        FullInfo = 2  
    }
    /// <summary>
    /// 微信信息获取方式
    /// </summary>
    public enum WeChatNavigationEnum
    {
        [Description("简易信息")]
        ShortInfo = 1,
        [Description("全部信息")]
        FullInfo = 2
    }
    public enum SendWeChatStatus
    {
        [Description("待发送")]
        WaitSend =0,
        [Description("已发送")]
        HasSend = 1, 
        [Description("发送中")]
         Sending = 2,
        [Description("发送失败")]
         SendFail = 3,
        [Description("已阅读")]
        HasRead = 4,
        [Description("已报价")]
        HasQuote = 5,
        [Description("报价关闭")]
        Close = 99
    }
    public enum WeChatUrTypeEnum
    {
        [Description("常用导航")]
        Normal = 1,
        [Description("临时导航")]
        Temp = 2,
    }

}
