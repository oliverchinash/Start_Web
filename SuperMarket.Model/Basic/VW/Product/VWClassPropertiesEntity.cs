using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public partial class VWClassPropertiesEntity
    {

        #region Public Properties	
  
        /// <summary>
        /// 属性名称
        /// <summary>
        private string _Name;
        public string Name
        {
            get
            {
                return string.IsNullOrEmpty(ClassPropertyName)? PropertyName : ClassPropertyName;
            } 
        }

        /// <summary>
        /// 基础分类3级Id
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
        /// 属性Id
        /// <summary>
        private int _PropertyId;
        public int PropertyId
        {
            get
            {
                return _PropertyId;
            }
            set
            {
                _PropertyId = value;
            }
        }
        /// <summary>
        /// 属性名称
        /// <summary>
        private string _ClassPropertyName;
        public string ClassPropertyName
        {
            get
            {
                return _ClassPropertyName;
            }
            set
            {
                _ClassPropertyName = value;
            }
        }
        /// <summary>
        /// 属性名称
        /// <summary>
        private string _PropertyName;
        public string PropertyName
        {
            get
            {
                return _PropertyName;
            }
            set
            {
                _PropertyName = value;
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
        /// 属性根节点，没有层级关系的以自身代替
        /// <summary>
        private int _RootPropertyId;
        public int RootPropertyId
        {
            get
            {
                return _RootPropertyId;
            }
            set
            {
                _RootPropertyId = value;
            }
        }
        /// <summary>
        /// 父级属性
        /// <summary>
        private int _ParentPropertyId;
        public int ParentPropertyId
        {
            get
            {
                return _ParentPropertyId;
            }
            set
            {
                _ParentPropertyId = value;
            }
        }
        /// <summary>
        /// 是否叶子结点
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
        /// 属性类别（方便做特殊处理）
        /// <summary>
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
        /// <summary>
        /// 是否规格
        /// <summary>
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
        /// <summary>
        /// 属性默认选择值
        /// <summary>
        private int _PropertyDetailId;
        public int PropertyDetailId
        {
            get
            {
                return _PropertyDetailId;
            }
            set
            {
                _PropertyDetailId = value;
            }
        }
        /// <summary>
        /// 属性默认选择值
        /// <summary>
        private string _PropertyDetailName;
        public string PropertyDetailName
        {
            get
            {
                return _PropertyDetailName;
            }
            set
            {
                _PropertyDetailName = value;
            }
        }
        /// <summary>
        /// 在后台是否只能单选添加
        /// <summary>
        private int _HasProduct;
        public int HasProduct
        {
            get
            {
                return _HasProduct;
            }
            set
            {
                _HasProduct = value;
            }
        }
        #endregion
    }
}
