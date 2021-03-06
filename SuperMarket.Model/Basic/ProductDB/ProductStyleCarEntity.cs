﻿ /*****************************************
Table  Name:ProductStyleCar
Create Time:2016/8/19 14:49:24
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductStyleCar
	/// This entiy code is generated by codesmith. this entity is ProductStyleCar.
	/// </summary>
	[Serializable()]
	public partial class ProductStyleCarEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 款式适合车型记录表
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
		/// 车型品牌1级：乘用车，商用车。。。BrandCar
		/// <summary>
		private  int _BrandCar1;
	 	public int BrandCar1
		{
			get
			{
				return _BrandCar1;
			}
			set
			{
				_BrandCar1=value;
			}
		}

	    /// <summary>
		/// 车品牌2：奥迪。。。
		/// <summary>
		private  int _BrandCar2;
	 	public int BrandCar2
		{
			get
			{
				return _BrandCar2;
			}
			set
			{
				_BrandCar2=value;
			}
		}

	    /// <summary>
		/// 车系3级：Q5...
		/// <summary>
		private  int _BrandCar3;
	 	public int BrandCar3
		{
			get
			{
				return _BrandCar3;
			}
			set
			{
				_BrandCar3=value;
			}
		}

	    /// <summary>
		/// 车型4级：2015 豪华型。。。
		/// <summary>
		private  int _BrandCar4;
	 	public int BrandCar4
		{
			get
			{
				return _BrandCar4;
			}
			set
			{
				_BrandCar4=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  DateTime _CreateTime;
	 	public DateTime CreateTime
		{
			get
			{
				return _CreateTime;
			}
			set
			{
				_CreateTime=value;
			}
		}

		#endregion				
	}
}
