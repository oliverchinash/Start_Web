﻿ /*****************************************
Table  Name:Comment
Create Time:2016/9/8 13:30:10
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>Comment
	/// This entiy code is generated by codesmith. this entity is Comment.
	/// </summary>
	[Serializable()]
	public class CommentEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 评论表
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
		/// 对应产品ID
		/// <summary>
		private  int _ProductId;
	 	public int ProductId
		{
			get
			{
				return _ProductId;
			}
			set
			{
				_ProductId=value;
			}
		}
        /// <summary>
        /// 对应产品ID
        /// <summary>
        private int _ProductDetailId;
        public int ProductDetailId
        {
            get
            {
                return _ProductDetailId;
            }
            set
            {
                _ProductDetailId = value;
            }
        }


        public Int64 _OrderCode;
        public Int64 OrderCode
        {
            get
            {
                return _OrderCode;
            }
            set
            {
                _OrderCode = value;
            }
        }

        public int _OrderDetailId;
        public int OrderDetailId
        {
            get
            {
                return _OrderDetailId;
            }

            set
            {
                _OrderDetailId = value;
            }

        }
        public DateTime _OrderDate;
        public DateTime OrderDate
        {
            get
            {
                return _OrderDate;
            }

            set
            {
                _OrderDate = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private  string _ProductName;
	 	public string ProductName
		{
			get
			{
				return _ProductName;
			}
			set
			{
				_ProductName=value;
			}
		}

	    /// <summary>
		/// 产品星级
		/// <summary>
		private  int _ProductStar;
	 	public int ProductStar
		{
			get
			{
				return _ProductStar;
			}
			set
			{
				_ProductStar=value;
			}
		}
        /// <summary>
        /// 包装星级
        /// <summary>
        private int _PackStar;
        public int PackStar
        {
            get
            {
                return _PackStar;
            }
            set
            {
                _PackStar = value;
            }
        }
        
        /// <summary>
        /// 服务星级
        /// <summary>
        private  int _SeviceStar;
	 	public int SeviceStar
		{
			get
			{
				return _SeviceStar;
			}
			set
			{
				_SeviceStar=value;
			}
		}

	    /// <summary>
		/// 物流星级
		/// <summary>
		private  int _TrafficStar;
	 	public int TrafficStar
		{
			get
			{
				return _TrafficStar;
			}
			set
			{
				_TrafficStar=value;
			}
		}

	    /// <summary>
		/// 内容
		/// <summary>
		private  string _CommentContent;
	 	public string CommentContent
		{
			get
			{
				return _CommentContent;
			}
			set
			{
				_CommentContent=value;
			}
		}
        /// <summary>
        /// 内容
        /// <summary>
        private string _PicUrlContent;
        public string PicUrlContent
        {
            get
            {
                return _PicUrlContent;
            }
            set
            {
                _PicUrlContent = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private string _PicUrl ;
        public string PicUrl 
        {
            get
            {
                return _PicUrl ;
            }
            set
            {
                _PicUrl = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private string _PicSuffix;
        public string PicSuffix
        {
            get
            {
                return _PicSuffix;
            }
            set
            {
                _PicSuffix = value;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlSmall
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "Small." +  PicSuffix;
            }
        }
        /// <summary>
        /// 客户端种类：1PC，2安卓，3IOS
        /// <summary>
        private  int _ClientType;
	 	public int ClientType
		{
			get
			{
				return _ClientType;
			}
			set
			{
				_ClientType=value;
			}
		}

	    /// <summary>
		/// 评价里面是否有图片
		/// <summary>
		private  int _HasPic;
	 	public int HasPic
		{
			get
			{
				return _HasPic;
			}
			set
			{
				_HasPic=value;
			}
		}

	    /// <summary>
		/// 评论IP地址
		/// <summary>
		private  string _IpAddress;
	 	public string IpAddress
		{
			get
			{
				return _IpAddress;
			}
			set
			{
				_IpAddress=value;
			}
		}

	    /// <summary>
		/// 用户Id
		/// <summary>
		private  int _MemId;
	 	public int MemId
		{
			get
			{
				return _MemId;
			}
			set
			{
				_MemId=value;
			}
		}
        /// <summary>
        /// 用户Id
        /// <summary>
        private string _MemCode;
        public string MemCode
        {
            get
            {
                return _MemCode;
            }
            set
            {
                _MemCode = value;
            }
        }
        /// <summary>
        /// 用户Id
        /// <summary>
        private string _MemLevelName;
        public string MemLevelName
        {
            get
            {
                return _MemLevelName;
            }
            set
            {
                _MemLevelName = value;
            }
        }
        

        /// <summary>
        /// 评论时间
        /// <summary>
        private DateTime _CreateTime;
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
		/// 状态(0-未审核，1－审核发表，2－审核不发表)
		/// <summary>
		private  int _Status;
	 	public int Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _CheckManId;
	 	public int CheckManId
		{
			get
			{
				return _CheckManId;
			}
			set
			{
				_CheckManId=value;
			}
		}
        /// <summary>
        /// 是否追评
        /// <summary>
        private int _IsAdd;
        public int IsAdd
        {
            get
            {
                return _IsAdd;
            }
            set
            {
                _IsAdd = value;
            }
        }

        /// <summary>
        /// 审核时间
        /// <summary>
        private DateTime _CheckTime;
	 	public DateTime CheckTime
		{
			get
			{
				return _CheckTime;
			}
			set
			{
				_CheckTime=value;
			}
		}

	    /// <summary>
		/// 审核不通过原因
		/// <summary>
		private  string _ReplyContent;
	 	public string ReplyContent
        {
			get
			{
				return _ReplyContent;
			}
			set
			{
				_ReplyContent=value;
			}
		}

		#endregion				
	}
}