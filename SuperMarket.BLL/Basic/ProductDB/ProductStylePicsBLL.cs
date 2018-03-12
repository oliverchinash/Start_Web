using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StylePics的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{

    /// <summary>
    /// 表StylePics的业务逻辑层。
    /// </summary>
    public class ProductStylePicsBLL
    {
        #region 实例化
        public static ProductStylePicsBLL Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly ProductStylePicsBLL instance = new ProductStylePicsBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表StylePics，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="stylePics">要添加的StylePics数据实体对象</param>
        public int AddStylePics(ProductStylePicsEntity stylePics)
        {
            if (stylePics.Id > 0)
            {
                return UpdateStylePics(stylePics);
            }

            else if (ProductStylePicsBLL.Instance.IsExist(stylePics))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductStylePicsDA.Instance.AddStylePics(stylePics);
            }
        }
        public int AddProcStylePics(int styleid,int productid,string picstrs,string picdetailstr)
        {
            return ProductStylePicsDA.Instance.AddProcStylePics(styleid, productid, picstrs, picdetailstr);
        } 
        /// <summary>
        /// 更新一条StylePics记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="stylePics">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateStylePics(ProductStylePicsEntity stylePics)
        {
            return ProductStylePicsDA.Instance.UpdateStylePics(stylePics);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStylePicsByKey(int id)
        {
            return ProductStylePicsDA.Instance.DeleteStylePicsByKey(id);
        }
        /// <summary>
        /// 压缩完成，反馈结果
        /// </summary>
        /// <param name="allids"></param>
        /// <param name="successids"></param>
        /// <param name="failids"></param>
        /// <returns></returns>
        public int ComPressComplete(string allids, string successids, string failids)
        {
            return ProductStylePicsDA.Instance.ComPressComplete(allids, successids, failids);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStylePicsDisabled()
        {
            return ProductStylePicsDA.Instance.DeleteStylePicsDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStylePicsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductStylePicsDA.Instance.DeleteStylePicsByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStylePicsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductStylePicsDA.Instance.DisableStylePicsByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个StylePics实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>StylePics实体</returns>
        /// <param name="columns">要返回的列</param>
        public ProductStylePicsEntity GetStylePics(int id)
        {
            return ProductStylePicsDA.Instance.GetStylePics(id);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductStylePicsEntity> GetStylePicsList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            return ProductStylePicsDA.Instance.GetStylePicsList(pageSize, pageIndex, ref recordCount);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductStylePicsEntity> GetStylePicsNoComPress(int num)
        {
            return ProductStylePicsDA.Instance.GetStylePicsNoComPress(num);

        }
        public IList<ProductStylePicsEntity> GetListByStyleId(int styleid)
        {
            IList<ProductStylePicsEntity> list = null;
            list = ProductStylePicsDA.Instance.GetListPics(styleid,0,(int)ProductPicShowType.Combine); 
            return list; 
        }
        public IList<ProductStylePicsEntity> GetListPicsByProductId(int productid)
        {
            IList<ProductStylePicsEntity> list = null;
            list = ProductStylePicsDA.Instance.GetListPics(0, productid, (int)ProductPicShowType.Combine);
            return list;
        }
        public IList<ProductStylePicsEntity> GetListPics(int styleid,int productid,int showpictype)
        {
            IList<ProductStylePicsEntity> list = null;
            string _cachekey = "GetProductDetailPics_"+ styleid+"_"+ productid+"_"+ showpictype;// SysCacheKey.GetProductDetailPics_;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list = ProductStylePicsDA.Instance.GetListPics(styleid, productid, showpictype); 
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<ProductStylePicsEntity>)obj;
            }

            return list;
        }
       
        public async Task GetStylePicsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "StylePicsListKey";// SysCacheKey.StylePicsListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductStylePicsEntity> list = null;
                    list = ProductStylePicsDA.Instance.GetStylePicsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductStylePicsEntity stylePics)
        {
            return ProductStylePicsDA.Instance.ExistNum(stylePics) > 0;
        }


    }
}

