using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表DicCoupons的业务逻辑层。
创建时间：2017/3/25 18:44:55
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表DicCoupons的业务逻辑层。
	/// </summary>
	public class DicCouponsBLL
	{
	    #region 实例化
        public static DicCouponsBLL Instance
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
            internal static readonly DicCouponsBLL instance = new DicCouponsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表DicCoupons，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="dicCoupons">要添加的DicCoupons数据实体对象</param>
		public   int AddDicCoupons(DicCouponsEntity dicCoupons)
		{
			  if (dicCoupons.Id > 0)
            {
                return UpdateDicCoupons(dicCoupons);
            }
				  else if (string.IsNullOrEmpty(dicCoupons.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (DicCouponsBLL.Instance.IsExist(dicCoupons))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return DicCouponsDA.Instance.AddDicCoupons(dicCoupons);
            }
	 	}

		/// <summary>
		/// 更新一条DicCoupons记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="dicCoupons">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateDicCoupons(DicCouponsEntity dicCoupons)
		{
			return DicCouponsDA.Instance.UpdateDicCoupons(dicCoupons);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDicCouponsByKey(int id)
        {
            return DicCouponsDA.Instance.DeleteDicCouponsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicCouponsDisabled()
        {
            return DicCouponsDA.Instance.DeleteDicCouponsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicCouponsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return DicCouponsDA.Instance.DeleteDicCouponsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicCouponsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return DicCouponsDA.Instance.DisableDicCouponsByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个DicCoupons实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>DicCoupons实体</returns>
        /// <param name="columns">要返回的列</param>
        public DicCouponsEntity GetDicCoupons(int id, bool iscache = true )
        {
            DicCouponsEntity entity = new DicCouponsEntity();
            if (iscache)
            {

                string _cachekey = "GetDicCoupons" + id;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    entity = DicCouponsDA.Instance.GetDicCoupons(id);
                    if (entity != null && entity.Id > 0)
                    {
                        if (entity.ClassId > 0)
                        {
                            entity.ClassName = ClassesFoundBLL.Instance.GetClassesFound(entity.ClassId).Name;
                        }
                        else
                        {
                            entity.ClassName = "不限";
                        }
                        if (entity.BrandId > 0)
                        {
                            entity.BrandName = ClassesFoundBLL.Instance.GetClassesFound(entity.BrandId).Name;
                        }
                        else
                        {
                            entity.BrandName = "不限";
                        }
                    }
                    MemCache.AddCache(_cachekey, entity);
                }
                else
                {
                    entity = (DicCouponsEntity)obj;
                }
            }
            else
            {
                entity = DicCouponsDA.Instance.GetDicCoupons(id);
                if (entity != null && entity.Id > 0)
                {
                    if (entity.ClassId > 0)
                    {
                        entity.ClassName = ClassesFoundBLL.Instance.GetClassesFound(entity.ClassId).Name;
                    }
                    else
                    {
                        entity.ClassName = "不限";
                    }
                    if (entity.BrandId > 0)
                    {
                        entity.BrandName = ClassesFoundBLL.Instance.GetClassesFound(entity.BrandId).Name;
                    }
                    else
                    {
                        entity.BrandName = "不限";
                    }
                }
            }
            return entity;

        }
    
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<DicCouponsEntity> GetDicCouponsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return DicCouponsDA.Instance.GetDicCouponsList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetDicCouponsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="DicCouponsListKey";// SysCacheKey.DicCouponsListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<DicCouponsEntity> list = null;
                    list = DicCouponsDA.Instance.GetDicCouponsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(DicCouponsEntity dicCoupons)
        {
            return DicCouponsDA.Instance.ExistNum(dicCoupons)>0;
        }
		
	}
}

