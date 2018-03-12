﻿ /*****************************************
Table  Name:WeiXinQNews
Create Time:2017/7/15 14:16:47
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>WeiXinQNews
	/// This entiy code is generated by codesmith. this entity is WeiXinQNews.
	/// </summary>
	[Serializable()]
	public class WeiXinQNewsEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 微信群的文章记录表
		/// <summary>
		private  int _Id;
	 	public int Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id=value;
			}
		}

	    /// <summary>
		/// 标题
		/// <summary>
		private  string _Title;
	 	public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title=value;
			}
		}

	    /// <summary>
		/// 短标题
		/// <summary>
		private  string _ShortTitle;
	 	public string ShortTitle
		{
			get
			{
				return _ShortTitle;
			}
			set
			{
				_ShortTitle=value;
			}
		}

	    /// <summary>
		/// 内容
		/// <summary>
		private  string _NewsContent;
	 	public string NewsContent
		{
			get
			{
				return _NewsContent;
			}
			set
			{
				_NewsContent=value;
			}
		}

	    /// <summary>
		/// 建立时间
		/// <summary>
		private  DateTime _CreateTime;
	 	public DateTime CreateTime
		{
			get
			{
				return _CreateTime;
			}
			set
			{
				_CreateTime=value;
			}
		}

	    /// <summary>
		/// 是否已发布
		/// <summary>
		private  int _HasShow;
	 	public int HasShow
		{
			get
			{
				return _HasShow;
			}
			set
			{
				_HasShow=value;
			}
		}

	    /// <summary>
		/// 文章类型
		/// <summary>
		private  int _NewsType;
	 	public int NewsType
		{
			get
			{
				return _NewsType;
			}
			set
			{
				_NewsType=value;
			}
		}
        public string NewsTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((WeiXinNewsTypeEnum)NewsType);
            }
        }
        /// <summary>
        /// 来源网址
        /// <summary>
        private string _FromUrl;
        public string FromUrl
        {
            get
            {
                return _FromUrl;
            }
            set
            {
                _FromUrl = value;
            }
        }
        #endregion
    }
}