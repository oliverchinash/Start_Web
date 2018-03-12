﻿ /*****************************************
Table  Name:GYCity
Create Time:2016/7/30 10:41:22
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>GYCity
	/// This entiy code is generated by codesmith. this entity is GYCity.
	/// </summary>
	[Serializable()]
	public class GYCityEntity
	{
	    #region Public Properties	
		// 
	 	public int Id
		{
			get;
			set;
		}

		// 编码
	 	public string Code
		{
			get;
			set;
		}

		// 名称
	 	public string Name
		{
			get;
			set;
		}

		// 省份编码
	 	public string ParentCode
		{
			get;
			set;
		}

		// 是否有效
	 	public int IsActive
		{
			get;
			set;
		}

		// 拼音首字母
	 	public string PYFirst
		{
			get;
			set;
		}

		// 全拼
	 	public string PYFull
		{
			get;
			set;
		}

		// 失效时间
	 	public DateTime DisableTime
		{
			get;
			set;
		}
        // 失效时间
        public int Sort 
        {
            get;
            set;
        }
        #endregion
    }
}
