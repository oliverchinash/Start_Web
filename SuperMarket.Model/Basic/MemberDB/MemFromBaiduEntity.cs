﻿ /*****************************************
Table  Name:MemFromBaidu
Create Time:2017/4/5 15:17:57
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>MemFromBaidu
	/// This entiy code is generated by codesmith. this entity is MemFromBaidu.
	/// </summary>
	[Serializable()]
	public class MemFromBaiduEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 
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
		private  string _CompanyName;
	 	public string CompanyName
		{
			get
			{
				return _CompanyName;
			}
			set
			{
				_CompanyName=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _CompanyAddress;
	 	public string CompanyAddress
		{
			get
			{
				return _CompanyAddress;
			}
			set
			{
				_CompanyAddress=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _CompanyPhone;
	 	public string CompanyPhone
		{
			get
			{
				return _CompanyPhone;
			}
			set
			{
				_CompanyPhone=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _Tags;
	 	public string Tags
		{
			get
			{
				return _Tags;
			}
			set
			{
				_Tags=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _Province;
	 	public string Province
		{
			get
			{
				return _Province;
			}
			set
			{
				_Province=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _City;
	 	public string City
		{
			get
			{
				return _City;
			}
			set
			{
				_City=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _PositionLat;
	 	public string PositionLat
		{
			get
			{
				return _PositionLat;
			}
			set
			{
				_PositionLat=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _PositionLng;
	 	public string PositionLng
		{
			get
			{
				return _PositionLng;
			}
			set
			{
				_PositionLng=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _Title;
	 	public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _CenterPosition;
	 	public string CenterPosition
		{
			get
			{
				return _CenterPosition;
			}
			set
			{
				_CenterPosition=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _JuLi;
	 	public string JuLi
		{
			get
			{
				return _JuLi;
			}
			set
			{
				_JuLi=value;
			}
		}
        /// <summary>
        /// 
        /// <summary>
        private int _MemId;
        public int MemId
        {
            get
            {
                return _MemId;
            }
            set
            {
                _MemId = value;
            }
        }
        
        #endregion
    }
}
