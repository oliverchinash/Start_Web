﻿ /*****************************************
Table  Name:ClassBrand
Create Time:2016/10/31 13:00:03
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ClassBrand
	/// This entiy code is generated by codesmith. this entity is ClassBrand.
	/// </summary>
	[Serializable()]
	public class ClassBrandEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 品牌分类关联表
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
		/// 分类Id
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
		/// 品牌Id
		/// <summary>
		private  int _BrandId;
	 	public int BrandId
		{
			get
			{
				return _BrandId;
			}
			set
			{
				_BrandId=value;
			}
		}
        /// <summary>
        /// 排序
        /// <summary>
        private int _Status;
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
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
        /// 品牌名称
        /// <summary>
        private string _BrandName;
        public string BrandName
        {
            get
            {
                return _BrandName;
            }
            set
            {
                _BrandName = value;
            }
        }

        /// <summary>
        /// 生产商
        /// <summary>
        private string _Manufacturer;
        public string Manufacturer
        {
            get
            {
                return _Manufacturer;
            }
            set
            {
                _Manufacturer = value;
            }
        }

        #endregion
    }
}
