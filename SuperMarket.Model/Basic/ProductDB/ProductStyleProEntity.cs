﻿ /*****************************************
Table  Name:ProductStylePro
Create Time:2016/8/23 16:49:59
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductStylePro
	/// This entiy code is generated by codesmith. this entity is ProductStylePro.
	/// </summary>
	[Serializable()]
	public partial class ProductStyleProEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 款式属性记录表
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
		/// 款式属性名称Id
		/// <summary>
		private  int _PropertyId;
	 	public int PropertyId
		{
			get
			{
				return _PropertyId;
			}
			set
			{
				_PropertyId=value;
			}
		}

	    /// <summary>
		/// 属性值Id
		/// <summary>
		private  int _PropertyDetailId;
	 	public int PropertyDetailId
		{
			get
			{
				return _PropertyDetailId;
			}
			set
			{
				_PropertyDetailId=value;
			}
		}

	    /// <summary>
		/// 款式ID
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
        /// 显示属性名
        /// <summary>
        private string _PropertyName;
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                _PropertyName = value;
            }
        }
        /// <summary>
        /// 显示属性名
        /// <summary>
        private int _IsSpec;
        public int IsSpec
        {
            get
            {
                return _IsSpec;
            }
            set
            {
                _IsSpec = value;
            }
        }
        
        #endregion
    }
}
