using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public class VWCGSyncOrder
    {
        /// <summary>
        /// 通用的汽车配件与用品，拆分的时候过度
        /// </summary>
        public IList<int> TYClassIds
        {
            get;
            set;
        }

        /// <summary>
        /// 通用的汽车配件与用品，拆分的时候过度
        /// </summary>
        public IList<VWCGSyncProClass> TYClassPros
        {
            get;
            set;
        }
        /// <summary>
        /// 非通用的配件对应的分类列表（不重复）
        /// </summary>
        public IList<int> NotTYClasses
        {
            get;
            set;
        }
        /// <summary>
        /// 非通用的配件对应的分类明细数据
        /// </summary>
        public IList<VWCGSyncProClass> NotTYClassPros
        {
            get;
            set;
        }
        /// <summary>
        /// 非通用的配件对应的车型列表（不重复）
        /// </summary>
        public IList<int> NotTYCarTypes
        {
            get;
            set;
        }
        /// <summary>
        /// 非通用的配件对应的车型列表（不重复）
        /// </summary>
        public IList<VWCGSyncProCarType> NotTYProCarTypes
        {
            get;
            set;
        }
        /// <summary>
        /// 非通用的配件对应的车型明细，有可能重复对应多车型
        /// </summary>
        public IList<VWCGSyncProCarType> NotTYProCarTypesDetail
        {
            get;
            set;
        }
    }
    public class VWCGSyncProCarType
    {
        public int ProductId;
        public int CarTypeId1;
        public int CarTypeId2;
        public int CarTypeId3;
        public int CarTypeId4;

    }

    public class VWCGSyncProClass
    {
        public int ProductId;
        public int ClassId;  
    }

}
