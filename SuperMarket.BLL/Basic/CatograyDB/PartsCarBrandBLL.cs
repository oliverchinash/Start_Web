using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表PartsCarBrand的业务逻辑层。
创建时间：2017/9/4 10:28:41
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表PartsCarBrand的业务逻辑层。
	/// </summary>
	public class PartsCarBrandBLL
	{
	    #region 实例化
        public static PartsCarBrandBLL Instance
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
            internal static readonly PartsCarBrandBLL instance = new PartsCarBrandBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PartsCarBrand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="partsCarBrand">要添加的PartsCarBrand数据实体对象</param>
		public   int AddPartsCarBrand(PartsCarBrandEntity partsCarBrand)
		{
			  if (partsCarBrand.Id > 0)
            {
                return UpdatePartsCarBrand(partsCarBrand);
            }
          
            else if (PartsCarBrandBLL.Instance.IsExist(partsCarBrand))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return PartsCarBrandDA.Instance.AddPartsCarBrand(partsCarBrand);
            }
	 	}

		/// <summary>
		/// 更新一条PartsCarBrand记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="partsCarBrand">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdatePartsCarBrand(PartsCarBrandEntity partsCarBrand)
		{
			return PartsCarBrandDA.Instance.UpdatePartsCarBrand(partsCarBrand);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePartsCarBrandByKey(int id)
        {
            return PartsCarBrandDA.Instance.DeletePartsCarBrandByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePartsCarBrandDisabled()
        {
            return PartsCarBrandDA.Instance.DeletePartsCarBrandDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePartsCarBrandByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PartsCarBrandDA.Instance.DeletePartsCarBrandByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePartsCarBrandByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PartsCarBrandDA.Instance.DisablePartsCarBrandByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PartsCarBrand实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PartsCarBrand实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PartsCarBrandEntity GetPartsCarBrand(int id)
		{
			return PartsCarBrandDA.Instance.GetPartsCarBrand(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PartsCarBrandEntity> GetPartsCarBrandList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return PartsCarBrandDA.Instance.GetPartsCarBrandList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetPartsCarBrandAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="PartsCarBrandListKey";// SysCacheKey.PartsCarBrandListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PartsCarBrandEntity> list = null;
                    list = PartsCarBrandDA.Instance.GetPartsCarBrandAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PartsCarBrandEntity partsCarBrand)
        {
            return PartsCarBrandDA.Instance.ExistNum(partsCarBrand)>0;
        }
		
	}
}

