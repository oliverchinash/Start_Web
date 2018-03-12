﻿ /*****************************************
Table  Name:CarTypeModel
Create Time:2017/4/21 11:46:53
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CarTypeModel
	/// This entiy code is generated by codesmith. this entity is CarTypeModel.
	/// </summary>
	[Serializable()]
	public class CarTypeModelEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 车型记录表
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
		/// 
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
		/// 
		/// <summary>
		private  string _ParentCode;
	 	public string ParentCode
		{
			get
			{
				return _ParentCode;
			}
			set
			{
				_ParentCode=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _BoxType;
	 	public string BoxType
		{
			get
			{
				return _BoxType;
			}
			set
			{
				_BoxType=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _Capacity;
	 	public string Capacity
		{
			get
			{
				return _Capacity;
			}
			set
			{
				_Capacity=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _CarYear;
	 	public int CarYear
		{
			get
			{
				return _CarYear;
			}
			set
			{
				_CarYear=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _ModelName;
	 	public string ModelName
		{
			get
			{
				return _ModelName;
			}
			set
			{
				_ModelName=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _SeriesId;
	 	public int SeriesId
		{
			get
			{
				return _SeriesId;
			}
			set
			{
				_SeriesId=value;
			}
		}

	    /// <summary>
		/// 
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
		private  string _Engine;
	 	public string Engine
		{
			get
			{
				return _Engine;
			}
			set
			{
				_Engine=value;
			}
		}

		#endregion				
	}
}
