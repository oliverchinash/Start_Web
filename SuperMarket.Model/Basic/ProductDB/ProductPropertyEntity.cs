﻿ /*****************************************
Table  Name:ProductProperty
Create Time:2016/10/31 16:27:55
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductProperty
	/// This entiy code is generated by codesmith. this entity is ProductProperty.
	/// </summary>
	[Serializable()]
	public class ProductPropertyEntity
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
		/// 
		/// <summary>
		private  string _PropertyName;
	 	public string PropertyName
		{
			get
			{
				return _PropertyName;
			}
			set
			{
				_PropertyName=value;
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
        ///  
        /// <summary>
        private string _PropertyDetailName;
        public string PropertyDetailName
        {
            get
            {
                return _PropertyDetailName;
            }
            set
            {
                _PropertyDetailName = value;
            }
        }
        /// <summary>
        /// 款式ID
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
		/// 是否规格，从分类带过来
		/// <summary>
		private  int _IsSpec;
	 	public int IsSpec
		{
			get
			{
				return _IsSpec;
			}
			set
			{
				_IsSpec=value;
			}
		}

		#endregion				
	}
}
