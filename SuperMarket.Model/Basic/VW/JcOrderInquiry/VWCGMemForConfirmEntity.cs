﻿ /*****************************************
Table  Name:CGMemQuoted
Create Time:2017/8/26 11:07:59
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CGMemQuoted
	/// This entiy code is generated by codesmith. this entity is CGMemQuoted.
	/// </summary>
	[Serializable()]
	public class VWCGMemForConfirmEntity
    {
        #region Public Properties	
        
        /// <summary>
        /// 供应商Id
        /// <summary>
        private int _CGMemId;
        public int CGMemId
        {
            get
            {
                return _CGMemId;
            }
            set
            {
                _CGMemId = value;
            }
        }
        /// <summary>
        /// 采购总价
        /// </summary>
        private decimal _CGPrice;
        public decimal CGPrice
        {
            get
            {
                return _CGPrice;
            }
            set
            {
                _CGPrice = value;
            }
        }
        #endregion
    }
}
