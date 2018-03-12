﻿ /*****************************************
Table  Name:CmsTemStyle
Create Time:2016/10/31 13:28:24
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{
    /// <summary>CmsTemProduct
    /// This entiy code is generated by codesmith. this entity is CmsTemStyle.
    /// </summary>
    [Serializable()]
	public class CmsTemProductEntity
    {
	    #region Public Properties	
	    /// <summary>
		/// CMS模板对应产品款式表
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
		/// 内容Id
		/// <summary>
		private  int _CmsContenttId;
	 	public int CmsContenttId
		{
			get
			{
				return _CmsContenttId;
			}
			set
			{
				_CmsContenttId=value;
			}
		}

	 //   /// <summary>
		///// 款式Id
		///// <summary>
		//private  int _StyleId;
	 //	public int StyleId
		//{
		//	get
		//	{
		//		return _StyleId;
		//	}
		//	set
		//	{
		//		_StyleId=value;
		//	}
		//}

        /// <summary>
        /// 产品Id
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
        /// 排序
        /// <summary>
        private int _Sort;
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
		/// 添加人员
		/// <summary>
		private  int _CreateManId;
	 	public int CreateManId
		{
			get
			{
				return _CreateManId;
			}
			set
			{
				_CreateManId=value;
			}
		}

		#endregion				
	}
}
