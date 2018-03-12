﻿ /*****************************************
Table  Name:StylePics
Create Time:2016/8/16 13:54:29
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>StylePics
	/// This entiy code is generated by codesmith. this entity is StylePics.
	/// </summary>
	[Serializable()]
	public partial class ProductStylePicsEntity
    {
	    #region Public Properties	
	    /// <summary>
		/// 款式图片表
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
        /// 款式Id
        /// <summary>
        private int _ProductId;
        public int ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
            }
        }
        /// <summary>
             /// 款式Id
             /// <summary>
        private  int _StyleId;
	 	public int StyleId
		{
			get
			{
				return _StyleId;
			}
			set
			{
				_StyleId=value;
			}
		}

	    /// <summary>
		/// 图片路径
		/// <summary>
		private  string _PicUrl;
	 	public string PicUrl
		{
			get
			{
				return _PicUrl;
			}
			set
			{
				_PicUrl=value;
			}
		}
        /// <summary>
        /// 图片路径
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
        /// 横向大小
        /// <summary>
        private  int _Size_X;
	 	public int Size_X
		{
			get
			{
				return _Size_X;
			}
			set
			{
				_Size_X=value;
			}
		}

	    /// <summary>
		/// 纵向大小
		/// <summary>
		private  int _Size_Y;
	 	public int Size_Y
		{
			get
			{
				return _Size_Y;
			}
			set
			{
				_Size_Y=value;
			}
		}

	    /// <summary>
		/// 图片大小
		/// <summary>
		private  decimal _Weight;
	 	public decimal Weight
		{
			get
			{
				return _Weight;
			}
			set
			{
				_Weight=value;
			}
		}

	    /// <summary>
		/// 排序
		/// <summary>
		private  int _Sort;
	 	public int Sort
		{
			get
			{
				return _Sort;
			}
			set
			{
				_Sort=value;
			}
		}

	    /// <summary>
		/// 是否压缩
		/// <summary>
		private  int _HasCompress;
	 	public int HasCompress
		{
			get
			{
				return _HasCompress;
			}
			set
			{
				_HasCompress=value;
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
		/// 压缩时间
		/// <summary>
		private  string _CompressTime;
	 	public string CompressTime
		{
			get
			{
				return _CompressTime;
			}
			set
			{
				_CompressTime=value;
			}
		}

		#endregion				
	}
}
