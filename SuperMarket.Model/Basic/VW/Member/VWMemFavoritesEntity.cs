using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    [Serializable]

    /// <summary>
    /// 用于个人用户审核的视图
    /// </summary>
    public class VWMemFavoritesEntity
    {
        #region Public Properties	
        /// <summary>
        /// 我的收藏表
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
        /// 产品明细Id
        /// <summary>
        private int _ProductDetailId;
        public int ProductDetailId
        {
            get
            {
                return _ProductDetailId;
            }
            set
            {
                _ProductDetailId = value;
            }
        }
        public VWProductEntity ProductEntity;
        /// <summary>
        /// 用户Id
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
        /// 生成时间
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
        /// 是否有效，用户删除后变0
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
        private int _SystemId;
        public int SystemId
        {
            get
            {
                return _SystemId;
            }
            set
            {
                _SystemId = value;
            }
        }

        #endregion
    }
}
