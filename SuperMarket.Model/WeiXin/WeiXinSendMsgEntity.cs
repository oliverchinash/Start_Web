using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    /// <summary>
    /// 发送消息参数
    /// </summary>
    public class WeiXinSendMsgEntity
    {
        /// <summary>
        /// 接收者openid
        /// </summary>
        public string touser;
        /// <summary>
        /// 模板ID
        /// </summary>
        public string template_id;
        /// <summary>
        /// 模板跳转链接
        /// </summary>
        public string url;
        /// <summary>
        /// 跳小程序所需数据，不需跳小程序可不用传该数据
        /// </summary>
        public MiniProgramEntity miniprogram;
        public Object data;  
    }

    public class MiniProgramEntity
    {
        public string appid;
        public string pagepath;
    }
    public class WeiXinUnitEntity
    { 
        public string value;
        public string color;
    }
    public class WeiXinInquiryEntity
    {
        public WeiXinUnitEntity first;
        public WeiXinUnitEntity tradeDateTime;
        public WeiXinUnitEntity orderType;
        public WeiXinUnitEntity customerInfo;
        public WeiXinUnitEntity orderItemName;
        public WeiXinUnitEntity orderItemData;
        public WeiXinUnitEntity remark;
    }
    #region 微信客服通知对象

    public class WeiXinCustomerMsgtypeEnum
    {
        public const string text = "text";
        public const string image = "image";
        public const string voice = "voice";
        public const string video = "video";
        public const string music = "music";
        public const string news = "news";
    }
    /// <summary>
    /// 微信客服通知对象
    /// </summary>
    public class WeiXinCustomerEntity
    {
        public string touser;
        public string msgtype;
        public WeiXinCustomerTextEntity text;
        public WeiXinCustomerImageEntity image;
        public WeiXinCustomerVoiceEntity voice;
        public WeiXinCustomerVideoEntity video;
        public WeiXinCustomerMusicEntity music;
        public WeiXinCustomerNewsEntity news;
    }
    public class WeiXinCustomerTextEntity
    {
        public string content;
    }
    public class WeiXinCustomerImageEntity
    {
        public string media_id;
    }
    public class WeiXinCustomerVoiceEntity
    {
        public string media_id;
    }
    public class WeiXinCustomerVideoEntity
    {
        public string media_id;
        public string thumb_media_id;
        public string title;
        public string description;
    }
    public class WeiXinCustomerMusicEntity
    {
        public string title;
        public string description;
        public string musicurl;
        public string hqmusicurl;
        public string thumb_media_id;
         
    }
    public class WeiXinCustomerNewsEntity
    {
        public IList<WeiXinCustomerArticleEntity> articles; 

    }
    public class WeiXinCustomerArticleEntity
    {
        public string title;
        public string description;
        public string url;
        public string picurl; 
    }
    #endregion
}