﻿ /*****************************************
Table  Name:CGOrderSend
Create Time:2016/12/31 9:41:06
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CGOrderSend
	/// This entiy code is generated by codesmith. this entity is CGOrderSend.
	/// </summary>
	[Serializable()]
	public class ReportInquiryOrderEntity
    {
        #region Public Properties	
         
         
        /// <summary>
        /// 下单日期
        /// <summary>
        private string _CreateDate;
        public string CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                _CreateDate = value;
            }
        }

        /// <summary>
        /// 客户Id
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
        /// <summary>
        /// 客户公司名称
        /// <summary>
        private string _CompanyName;
        public string  CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }
        private string _MemName;
        public string MemName
        {
            get
            {
                return _MemName;
            }
            set
            {
                _MemName = value;
            }
        }
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
        /// 供应商联系人名称
        /// <summary>
        private string _CGMemName;
        public string CGMemName
        {
            get
            {
                return _CGMemName;
            }
            set
            {
                _CGMemName = value;
            }
        }

        /// <summary>
        /// 供应商公司名称
        /// <summary>
        private string _CGCompanyName;
        public string CGCompanyName
        {
            get
            {
                return _CGCompanyName;
            }
            set
            {
                _CGCompanyName = value;
            }
        }  

 /// <summary>
 /// 
 /// </summary>
        private decimal _CGTotalPrice;
        public decimal CGTotalPrice
        {
            get
            {
                return _CGTotalPrice;
            }
            set
            {
                _CGTotalPrice = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private decimal _TotalPrice;
        public decimal TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                _TotalPrice = value;
            }
        }
        private decimal _Profit;
        public decimal Profit
        {
            get
            {
                return _Profit;
            }
            set
            {
                _Profit = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private int _TotalNum;
        public int TotalNum
        {
            get
            {
                return _TotalNum;
            }
            set
            {
                _TotalNum = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private int _QuoteNum;
        public int QuoteNum
        {
            get
            {
                return _QuoteNum;
            }
            set
            {
                _QuoteNum = value;
            }
        }  /// <summary>
           /// 
           /// <summary>
        private decimal _QuoteRate;
        public decimal QuoteRate
        {
            get
            {
                return _QuoteRate;
            }
            set
            {
                _QuoteRate = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private int _CheckedNum;
        public int CheckedNum
        {
            get
            {
                return _CheckedNum;
            }
            set
            {
                _CheckedNum = value;
            }
        }  /// <summary>
           /// 
           /// <summary>
        private decimal _CheckedRate;
        public decimal CheckedRate
        {
            get
            {
                return _CheckedRate;
            }
            set
            {
                _CheckedRate = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private int _ConfirmNum;
        public int ConfirmNum
        {
            get
            {
                return _ConfirmNum;
            }
            set
            {
                _ConfirmNum = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private decimal _ConfirmRate;
        public decimal ConfirmRate
        {
            get
            {
                return _ConfirmRate;
            }
            set
            {
                _ConfirmRate = value;
            }
        }
        #endregion
    }
}
