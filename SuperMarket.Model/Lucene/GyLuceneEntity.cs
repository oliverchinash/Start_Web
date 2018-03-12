/*****************************************
Table  Name:Product
Create Time:2016/8/23 16:49:59
Creator    :jc001
******************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

//?DataEntityattrbute
namespace SuperMarket.Model
{
    /// <summary>
    /// Gy_LuceneEntity实体类
    /// </summary>
    [Serializable]
    public class GyLuceneEntity
    {
        /// <summary>
        /// 数量
        /// </summary>
        public   int Count
        {
            get;
            set;
        }

        /// <summary>
        /// 搜索列表
        /// </summary>
        public   List<ProductLuceneEntity> ProductList
        {
            get;
            set;
        }
        /// <summary>
        /// 搜索列表
        /// </summary>
        public   List<int> ClassList
        {
            get;
            set;
        }
        /// <summary>
        /// 搜索列表
        /// </summary>
        public List<int> BrandList
        {
            get;
            set;
        }
    }
}
