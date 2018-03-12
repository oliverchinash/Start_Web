using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public class VWClassSpecEntity
    {
        #region Public Properties	
        /// <summary>
        /// 分类包含的规格记录表
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
        /// 属性名称
        /// <summary>
        private string _Name;
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(ClassSpecName) ? SpecName : ClassSpecName;
            }
        }

        /// <summary>
        /// 属性名称
        /// <summary>
        private string _ClassSpecName;
        public string ClassSpecName
        {
            get
            {
                return _ClassSpecName;
            }
            set
            {
                _ClassSpecName = value;
            }
        }
        /// <summary>
        /// 属性名称
        /// <summary>
        private string _SpecName;
        public string SpecName
        {
            get
            {
                return _SpecName;
            }
            set
            {
                _SpecName = value;
            }
        }
        /// <summary>
        /// 分类Id
        /// <summary>
        private int _ClassFoundId;
        public int ClassId
        {
            get
            {
                return _ClassFoundId;
            }
            set
            {
                _ClassFoundId = value;
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
        /// 排序
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

        /// <summary>
        /// 是否有效
        /// <summary>
        private int _IsActive;
        public int IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private int _RootSpecId;
        public int RootSpecId
        {
            get
            {
                return _RootSpecId;
            }
            set
            {
                _RootSpecId = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private int _IsEnd;
        public int IsEnd
        {
            get
            {
                return _IsEnd;
            }
            set
            {
                _IsEnd = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private int _ParentSpecId;
        public int ParentSpecId
        {
            get
            {
                return _ParentSpecId;
            }
            set
            {
                _ParentSpecId = value;
            }
        }
        #endregion
    }
}
