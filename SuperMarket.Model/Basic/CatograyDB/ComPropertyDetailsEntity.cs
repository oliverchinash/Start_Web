﻿ /*****************************************
Table  Name:ComPropertyDetails
Create Time:2016/10/31 13:00:04
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ComPropertyDetails
	/// This entiy code is generated by codesmith. this entity is ComPropertyDetails.
	/// </summary>
	[Serializable()]
	public class ComPropertyDetailsEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 通用属性内容明细：颜色表，等
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
		/// 颜色编码
		/// <summary>
		private  string _Code;
	 	public string Code
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
		/// 颜色名称
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
		/// 上级Id
		/// <summary>
		private  int _ParentId;
	 	public int ParentId
		{
			get
			{
				return _ParentId;
			}
			set
			{
				_ParentId=value;
			}
		}

	    /// <summary>
		/// 是否终极选取色调（色系->色调）
		/// <summary>
		private  int _IsEnd;
	 	public int IsEnd
		{
			get
			{
				return _IsEnd;
			}
			set
			{
				_IsEnd=value;
			}
		}

	    /// <summary>
		/// 色调图片路径
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
		/// 排序（倒序）
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
		/// 通用属性的Id
		/// <summary>
		private  int _ComPropertyId;
	 	public int ComPropertyId
		{
			get
			{
				return _ComPropertyId;
			}
			set
			{
				_ComPropertyId=value;
			}
		}

		#endregion				
	}
}
