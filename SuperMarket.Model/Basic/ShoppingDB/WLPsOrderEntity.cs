﻿ /*****************************************
Table  Name:WLPsOrder
Create Time:2016/9/19 11:40:02
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>WLPsOrder
	/// This entiy code is generated by codesmith. this entity is WLPsOrder.
	/// </summary>
	[Serializable()]
	public class WLPsOrderEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 订单物流信息
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
        /// 订单编号
        /// </summary>
        private int _OrderId;
        public int OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                _OrderId = value;
            }
        }

	    /// <summary>
		/// 订单编码
		/// <summary>
		private  long _OrderCode;
	 	public long OrderCode
		{
			get
			{
				return _OrderCode;
			}
			set
			{
				_OrderCode=value;
			}
		}

	    /// <summary>
		/// 发货单号码
		/// <summary>
		private  string _ConsignCode;
	 	public string ConsignCode
		{
			get
			{
				return _ConsignCode;
			}
			set
			{
				_ConsignCode=value;
			}
		}

	    /// <summary>
		/// 物流公司编号
		/// <summary>
		private  string _TransferCode;
	 	public string TransferCode
		{
			get
			{
				return _TransferCode;
			}
			set
			{
				_TransferCode=value;
			}
		}

	    /// <summary>
		/// 配送单号
		/// <summary>
		private  string _PsBillCode;
	 	public string PsBillCode
		{
			get
			{
				return _PsBillCode;
			}
			set
			{
				_PsBillCode=value;
			}
		}

	    /// <summary>
		/// 出库日期
		/// <summary>
		private  DateTime _OutTime;
	 	public DateTime OutTime
		{
			get
			{
				return _OutTime;
			}
			set
			{
				_OutTime=value;
			}
		}

	    /// <summary>
		///  配送结果(0-失败,1-成功,2-部分成功,3-干线到达,4-异常,5-配送中)
		/// <summary>
		private  int _PsResult;
	 	public int PsResult
		{
			get
			{
				return _PsResult;
			}
			set
			{
				_PsResult=value;
			}
		}

	    /// <summary>
		/// 维护人员Id
		/// <summary>
		private  int _CreateManId;
	 	public int CreateManId
		{
			get
			{
				return _CreateManId;
			}
			set
			{
				_CreateManId=value;
			}
		}
        /// <summary>
        /// 送货员姓名
        /// <summary>
        private string _SendManName;
        public string SendManName
        {
            get
            {
                return _SendManName;
            }
            set
            {
                _SendManName = value;
            }
        }
        /// <summary>
        /// 送货员电话
        /// <summary>
        private string _SendManPhone;
        public string SendManPhone
        {
            get
            {
                return _SendManPhone;
            }
            set
            {
                _SendManPhone = value;
            }
        }
        #endregion
    }
}
