﻿ /*****************************************
Table  Name:PartsProductFound
Create Time:2017/9/4 10:28:30
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>PartsProductFound
	/// This entiy code is generated by codesmith. this entity is PartsProductFound.
	/// </summary>
	[Serializable()]
	public class PartsProductFoundEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 配件产品选择基础表
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
		/// 产品名称
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
		/// 
		/// <summary>
		private  string _ProductFullName;
	 	public string ProductFullName
		{
			get
			{
				return _ProductFullName;
			}
			set
			{
				_ProductFullName=value;
			}
		}

	    /// <summary>
		/// 映射基础分类Id
		/// <summary>
		private  int _ClassFoundId;
	 	public int ClassFoundId
		{
			get
			{
				return _ClassFoundId;
			}
			set
			{
				_ClassFoundId=value;
			}
		}

	    /// <summary>
		/// 基础配件产品Id
		/// <summary>
		private  int _ProductFoundId;
	 	public int ProductFoundId
		{
			get
			{
				return _ProductFoundId;
			}
			set
			{
				_ProductFoundId=value;
			}
		}

	    /// <summary>
		/// 首字母
		/// <summary>
		private  string _PYFirst;
	 	public string PYFirst
		{
			get
			{
				return _PYFirst;
			}
			set
			{
				_PYFirst=value;
			}
		}

	    /// <summary>
		/// 全拼
		/// <summary>
		private  string _PingYingFull;
	 	public string PingYingFull
		{
			get
			{
				return _PingYingFull;
			}
			set
			{
				_PingYingFull=value;
			}
		}

	    /// <summary>
		/// 是否有效
		/// <summary>
		private  int _IsActive;
	 	public int IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				_IsActive=value;
			}
		}

	    /// <summary>
		/// 是否热门
		/// <summary>
		private  int _IsHot;
	 	public int IsHot
		{
			get
			{
				return _IsHot;
			}
			set
			{
				_IsHot=value;
			}
		}

	    /// <summary>
		/// 
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
        /// 
        /// <summary>
        private int _ScopeId;
        public int ScopeId
        {
            get
            {
                return _ScopeId;
            }
            set
            {
                _ScopeId = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private int _ScopeType;
        public int ScopeType
        {
            get
            {
                return _ScopeType;
            }
            set
            {
                _ScopeType = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private string _ProductUnitName;
        public string ProductUnitName
        {
            get
            {
                return _ProductUnitName;
            }
            set
            {
                _ProductUnitName = value;
            }
        }
        #endregion
    }
}
