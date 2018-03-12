using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{ 

   [Serializable()]
    public class VWPayAliResultEntity
    {
        #region Public Properties	
        /// <summary>
        /// 支付宝通知信息保存
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
        /// 
        /// <summary>
        private string _Buyeremail;
        public string Buyeremail
        {
            get
            {
                return _Buyeremail;
            }
            set
            {
                _Buyeremail = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Buyerid;
        public string Buyerid
        {
            get
            {
                return _Buyerid;
            }
            set
            {
                _Buyerid = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Issuccess;
        public string Issuccess
        {
            get
            {
                return _Issuccess;
            }
            set
            {
                _Issuccess = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Notifytime;
        public string Notifytime
        {
            get
            {
                return _Notifytime;
            }
            set
            {
                _Notifytime = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Notifytype;
        public string Notifytype
        {
            get
            {
                return _Notifytype;
            }
            set
            {
                _Notifytype = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Outtradeno;
        public string Outtradeno
        {
            get
            {
                return _Outtradeno;
            }
            set
            {
                _Outtradeno = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Paymenttype;
        public string Paymenttype
        {
            get
            {
                return _Paymenttype;
            }
            set
            {
                _Paymenttype = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Selleremail;
        public string Selleremail
        {
            get
            {
                return _Selleremail;
            }
            set
            {
                _Selleremail = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Sellerid;
        public string Sellerid
        {
            get
            {
                return _Sellerid;
            }
            set
            {
                _Sellerid = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Subject;
        public string Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                _Subject = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private decimal _Totalfee;
        public decimal Totalfee
        {
            get
            {
                return _Totalfee;
            }
            set
            {
                _Totalfee = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private decimal _ReBackFee;
        public decimal ReBackFee
        {
            get
            {
                return _ReBackFee;
            }
            set
            {
                _ReBackFee = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private string _Tradeno;
        public string Tradeno
        {
            get
            {
                return _Tradeno;
            }
            set
            {
                _Tradeno = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _Tradestatus;
        public string Tradestatus
        {
            get
            {
                return _Tradestatus;
            }
            set
            {
                _Tradestatus = value;
            }
        }

        /// <summary>
        /// 
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
        /// 已处理
        /// <summary>
        private int _HasDeal;
        public int HasDeal
        {
            get
            {
                return _HasDeal;
            }
            set
            {
                _HasDeal = value;
            }
        }

        #endregion

        public string AccepterName
        {
            get;
            set;
        }
        public string MobilePhone
        {
            get;
            set;
        }
        public decimal ActPrice
        {
            get;
            set;
        }
        public int OrderStatus
        {
            get;
            set;
        }
    }
}
