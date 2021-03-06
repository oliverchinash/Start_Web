﻿using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CmsTemStyle的业务逻辑层。
创建时间：2016/9/1 15:35:32
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL
{
	  
	/// <summary>
	/// 表CmsTemStyle的业务逻辑层。
	/// </summary>
	public class CmsTemProductBLL
	{
	    #region 实例化
        public static CmsTemProductBLL Instance
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
            internal static readonly CmsTemProductBLL instance = new CmsTemProductBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CmsTemStyle，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cmsTemStyle">要添加的CmsTemStyle数据实体对象</param>
		public   int AddCmsTemStyle(CmsTemProductEntity cmsTemProduct)
		{
			  if (cmsTemProduct.Id > 0)
            {
                return UpdateCmsTemStyle(cmsTemProduct);
            }
          
            else if (CmsTemProductBLL.Instance.IsExist(cmsTemProduct))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CmsTemProductDA.Instance.AddCmsTemProduct(cmsTemProduct);
            }
	 	}

		/// <summary>
		/// 更新一条CmsTemStyle记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cmsTemStyle">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCmsTemStyle(CmsTemProductEntity cmsTemProduct)
		{
			return CmsTemProductDA.Instance.UpdateCmsTemProduct(cmsTemProduct);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCmsTemProductByKey(int id)
        {
            return CmsTemProductDA.Instance.DeleteCmsTemProductByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCmsTemProductDisabled()
        {
            return CmsTemProductDA.Instance.DeleteCmsTemProductDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCmsTemProductByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CmsTemProductDA.Instance.DeleteCmsTemProductByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCmsTemProductByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CmsTemProductDA.Instance.DisableCmsTemProductByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CmsTemStyle实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CmsTemStyle实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CmsTemProductEntity GetCmsTemProduct(int id)
		{
			return CmsTemProductDA.Instance.GetCmsTemProduct(id);			
		}

        public int AddCmsProc(string str,int cmsid)
        {
            return CmsTemProductDA.Instance.AddCmsProc(str, cmsid);
        }

          ///// <summary>
          ///// 获得数据列表
          ///// </summary>
        public IList<CmsTemProductEntity> GetCmsTemProductList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CmsTemProductDA.Instance.GetCmsTemProductList(pageSize, pageIndex, ref recordCount);
        }
        public IList<VWProductEntity> GetCmsTemProductByCmsid(int cmsid)
        {
            return CmsTemProductDA.Instance.GetCmsTemProductByCmsid(cmsid);

        }
        public async Task GetCmsTemProductAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "CmsTemProductListKey";// SysCacheKey.CmsTemProductListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CmsTemProductEntity> list = null;
                    list = CmsTemProductDA.Instance.GetCmsTemProductAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CmsTemProductEntity cmsTemProduct)
        {
            return CmsTemProductDA.Instance.ExistNum(cmsTemProduct) >0;
        }
		
	}
}

