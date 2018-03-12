﻿ /*****************************************
Table  Name:ProductStyleExt
Create Time:2016/8/16 13:54:29
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductStyleExt
	/// This entiy code is generated by codesmith. this entity is ProductStyleExt.
	/// </summary>
	[Serializable()]
	public class ProductStyleExtEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 款式扩张表
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
		/// 尺码对照表HTML
		/// <summary>
		private  string _SizeChart;
	 	public string SizeChart
		{
			get
			{
				return _SizeChart;
			}
			set
			{
				_SizeChart=value;
			}
		}
        private string _ContentCms;
        public string ContentCms
        {
            get
            {
                return _ContentCms;
            }
            set
            {
                _ContentCms = value;
            }
        }
        #endregion
    }
}