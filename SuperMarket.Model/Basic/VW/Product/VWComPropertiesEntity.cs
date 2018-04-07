using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public class VWComPropertiesEntity
    {
        /// <summary>
        /// 所有的产品属性表
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
        /// 产品属性编码
        /// <summary>
        private string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        /// <summary>
        /// 产品属性名称
        /// <summary>
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// 父级属性
        /// <summary>
        private int _ParentId;
        public int ParentId
        {
            get
            {
                return _ParentId;
            }
            set
            {
                _ParentId = value;
            }
        }

        /// <summary>
        /// 
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
        private int _PropertyClassType;
        public int PropertyClassType
        {
            get
            {
                return _PropertyClassType;
            }
            set
            {
                _PropertyClassType = value;
            }
        }
        private int _IsSpec;
        public int IsSpec
        {
            get
            {
                return _IsSpec;
            }
            set
            {
                _IsSpec = value;
            }
        }
        public IList<BasicSiteProDetailsEntity> Details; 
    }
}
