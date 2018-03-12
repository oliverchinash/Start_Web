﻿ /*****************************************
Table  Name:AssetReCharge
Create Time:2016/8/4 11:01:53
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>AssetReCharge
	/// This entiy code is generated by codesmith. this entity is AssetReCharge.
	/// </summary>
	[Serializable()]
	public class AssetReChargeEntity
	{
	    #region Public Properties	
		// 充值历史记录表
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

		// 充值账户ID
		private  int _MemId;
	 	public int MemId
		{
			get
			{
				return _MemId;
			}
			set
			{
				_MemId=value;
			}
		}

		// 充值金额
		private  decimal _Amt;
	 	public decimal Amt
		{
			get
			{
				return _Amt;
			}
			set
			{
				_Amt=value;
			}
		}

		// 充值方式（1支付宝，2银联，3微信支付）
		private  int _PayType;
	 	public int PayType
		{
			get
			{
				return _PayType;
			}
			set
			{
                _PayType = value;
			}
		}

		// 银行编码
		private  string _BankCode;
	 	public string BankCode
		{
			get
			{
				return _BankCode;
			}
			set
			{
				_BankCode=value;
			}
		}

		// 交易序列号
		private  string _TranSerialNum;
	 	public string TranSerialNum
		{
			get
			{
				return _TranSerialNum;
			}
			set
			{
				_TranSerialNum=value;
			}
		}

		// 银行卡编号/微信账号/支付宝账号
		private  string _CardNo;
	 	public string CardNo
		{
			get
			{
				return _CardNo;
			}
			set
			{
				_CardNo=value;
			}
		}

		// 充值
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

		// 
		private  string _IpAddress;
	 	public string IpAddress
		{
			get
			{
				return _IpAddress;
			}
			set
			{
				_IpAddress=value;
			}
		} 
        private string _Remark;
        public string Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }
        #endregion
    }
}
