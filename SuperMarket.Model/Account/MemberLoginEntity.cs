using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Account
{
    [Serializable]
    public  class MemberLoginEntity
    {  
        /// <summary>
        /// 
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
        /// 账号
        /// <summary>
        private string _MemCode;
        public string MemCode
        {
            get
            {
                return _MemCode;
            }
            set
            {
                _MemCode = value;
            }
        }
        /// <summary>
        /// 账号
        /// <summary>
        private string _NickName;
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                _NickName = value;
            }
        }
        /// <summary>
        /// 
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
        private int _MemGrade;
        public int MemGrade
        {
            get
            {
                return _MemGrade;
            }
            set
            {
                _MemGrade = value;
            }
        }
        /// <summary>
        /// 是否供应商
        /// </summary>
        private int _IsSupplier;
        public int IsSupplier
        {
            get
            {
                return _IsSupplier;
            }
            set
            {
                _IsSupplier = value;
            }
        }
        /// <summary>
        /// 是否供应商
        /// </summary>
        private int _IsSysUser;
        public int IsSysUser
        {
            get
            {
                return _IsSysUser;
            }
            set
            {
                _IsSysUser = value;
            }
        }
        private int _IsStore;
        public int IsStore
        {
            get
            {
                return _IsStore;
            }
            set
            {
                _IsStore = value;
            }
        }
        private int _StoreType;
        public int StoreType
        {
            get
            {
                return _StoreType;
            }
            set
            {
                _StoreType = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private string _PassWord;
        public string PassWord
        {
            get
            {
                return _PassWord;
            }
            set
            {
                _PassWord = value;
            }
        }
        private string _WeChat;
        public string WeChat
        {
            get
            {
                return _WeChat;
            }
            set
            {
                _WeChat = value;
            }
        }
        private string _TimeStampTab;
        public string TimeStampTab
        {
            get
            {
                return _TimeStampTab;
            }
            set
            {
                _TimeStampTab = value;
            }
        }
        

    }
}
