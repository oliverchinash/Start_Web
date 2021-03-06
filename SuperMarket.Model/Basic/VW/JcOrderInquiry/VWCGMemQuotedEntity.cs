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
	public class VWCGMemQuotedEntity
    {
        #region Public Properties	
        /// <summary>
        /// 供应商报价登记表
        /// <summary>
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        /// <summary>
        /// 订单编号
        /// <summary>
        private string _InquiryOrderCode;
        public string InquiryOrderCode
        {
            get
            {
                return _InquiryOrderCode;
            }
            set
            {
                _InquiryOrderCode = value;
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
        /// 分配时间
        /// <summary>
        private DateTime _CreateTime;
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                _CreateTime = value;
            }
        }

        /// <summary>
        /// 发送微信提醒时间
        /// <summary>
        private DateTime _SendWeChatTime;
        public DateTime SendWeChatTime
        {
            get
            {
                return _SendWeChatTime;
            }
            set
            {
                _SendWeChatTime = value;
            }
        }

        /// <summary>
        /// 供应商阅读时间
        /// <summary>
        private int _HasSend;
        public int HasSend
        {
            get
            {
                return _HasSend;
            }
            set
            {
                _HasSend = value;
            }
        }
        /// <summary>
        /// 供应商阅读时间
        /// <summary>
        private int _HasRead;
        public int HasRead
        {
            get
            {
                return _HasRead;
            }
            set
            {
                _HasRead = value;
            }
        }
        /// <summary>
        /// 供应商阅读时间
        /// <summary>
        private DateTime _ReadTime;
        public DateTime ReadTime
        {
            get
            {
                return _ReadTime;
            }
            set
            {
                _ReadTime = value;
            }
        }
        /// <summary>
        /// 供应商阅读时间
        /// <summary>
        private int _HasQuote;
        public int HasQuote
        {
            get
            {
                return _HasQuote;
            }
            set
            {
                _HasQuote = value;
            }
        }
        /// <summary>
        /// 报价时间
        /// <summary>
        private DateTime _QuoteTime;
        public DateTime QuoteTime
        {
            get
            {
                return _QuoteTime;
            }
            set
            {
                _QuoteTime = value;
            }
        }

        /// <summary>
        /// 是否从横向件供应商找到的
        /// <summary>
        private int _FromClass;
        public int FromClass
        {
            get
            {
                return _FromClass;
            }
            set
            {
                _FromClass = value;
            }
        }

        /// <summary>
        /// 是否发送微信通知0未发送1已发送，2发送中
        /// <summary>
        private int _SendWeChatStatus;
        public int SendWeChatStatus
        {
            get
            {
                return _SendWeChatStatus;
            }
            set
            {
                _SendWeChatStatus = value;
            }
        }
        private int _HasInStock;
        public int HasInStock
        {
            get
            {
                return _HasInStock;
            }
            set
            {
                _HasInStock = value;
            }
        }
        public string InStockName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((QuoteInStock)HasInStock);
            }
        }
        private string _RemarkByCGMem;
        public string RemarkByCGMem
        {
            get
            {
                return _RemarkByCGMem;
            }
            set
            {
                _RemarkByCGMem = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private int _HasChecked;
        public int HasChecked
        {
            get
            {
                return _HasChecked;
            }
            set
            {
                _HasChecked = value;
            }
        }
        public string SendWeChatStatusName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((SendWeChatStatus)SendWeChatStatus);
            }

        }
        public VWStoreEntity CGMemStore;
        #endregion
    }
}
