﻿ /*****************************************
Table  Name:DicInquiryOrder
Create Time:2017/8/23 11:12:18
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>DicInquiryOrder
	/// This entiy code is generated by codesmith. this entity is DicInquiryOrder.
	/// </summary>
	[Serializable()]
	public class DicInquiryOrderEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 字典表
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
		/// 编号
		/// <summary>
		private  int _Code;
	 	public int Code
		{
			get
			{
				return _Code;
			}
			set
			{
				_Code=value;
			}
		}

	    /// <summary>
		/// 名称
		/// <summary>
		private  string _Name;
	 	public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name=value;
			}
		}
        /// <summary>
        /// 名称
        /// <summary>
        private int _ClassId;
        public int ClassId
        {
            get
            {
                return _ClassId;
            }
            set
            {
                _ClassId = value;
            }
        }

        /// <summary>
        /// 上级Id
        /// <summary>
        private int _ParentCode;
	 	public int ParentCode
        {
			get
			{
				return _ParentCode;
			}
			set
			{
                _ParentCode = value;
			}
		}
        /// <summary>
        /// 上级Id
        /// <summary>
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

        /// <summary>
        /// 枚举编码，每个枚举类型对应程序枚举值
        /// <summary>
        private string _MenuCode;
	 	public string MenuCode
		{
			get
			{
				return _MenuCode;
			}
			set
			{
				_MenuCode=value;
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

		#endregion				
	}
}
