using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    [Serializable()]
    public class VWProductStyleSpecEntity
    {
        #region Public Properties	
         
        /// <summary>
        /// 款式Id
        /// <summary>
        private int _StyleId;
        public int StyleId
        {
            get
            {
                return _StyleId;
            }
            set
            {
                _StyleId = value;
            }
        }

        /// <summary>
        /// 规格Id
        /// <summary>
        private int _SpecId;
        public int SpecId
        {
            get
            {
                return _SpecId;
            }
            set
            {
                _SpecId = value;
            }
        }

        /// <summary>
        /// 规格内容Id
        /// <summary>
        private int _SpecDetailId;
        public int SpecDetailId
        {
            get
            {
                return _SpecDetailId;
            }
            set
            {
                _SpecDetailId = value;
            }
        }
        /// <summary>
        /// 规格内容 
        /// <summary>
        private int _SpecDetailName;
        public int SpecDetailName
        {
            get
            {
                return _SpecDetailName;
            }
            set
            {
                _SpecDetailName = value;
            }
        }
        /// <summary>
        /// 规格内容排序
        /// <summary>
        private int _Sort;
        public int Sort
        {
            get
            {
                return _Sort;
            }
            set
            {
                _Sort = value;
            }
        }

        #endregion
    }
}
