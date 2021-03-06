﻿ /*****************************************
Table  Name:ClassCGScope
Create Time:2017/10/25 11:04:53
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ClassCGScope
	/// This entiy code is generated by codesmith. this entity is ClassCGScope.
	/// </summary>
	[Serializable()]
	public class ClassCGScopeEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 产品经营范围记录表
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
		/// 与classfund表的串接Id
		/// <summary>
		private  int _ClassId;
	 	public int ClassId
		{
			get
			{
				return _ClassId;
			}
			set
			{
				_ClassId=value;
			}
		}

	    /// <summary>
		/// 范围名称
		/// <summary>
		private  string _ScopeClassName;
	 	public string ScopeClassName
		{
			get
			{
				return _ScopeClassName;
			}
			set
			{
				_ScopeClassName=value;
			}
		}

	    /// <summary>
		/// 有效性
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
        private int _ParentId;
        public int ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                _ParentId = value;
            }
        }
        private int _IsRoot;
        public int IsRoot
        {
            get
            {
                return _IsRoot;
            }
            set
            {
                _IsRoot = value;
            }
        }
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
        #endregion
    }
}
