using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表DicUnitEnum的业务逻辑层。
创建时间：2017/3/15 0:41:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表DicUnitEnum的业务逻辑层。
	/// </summary>
	public class DicUnitEnumBLL
	{
	    #region 实例化
        public static DicUnitEnumBLL Instance
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
            internal static readonly DicUnitEnumBLL instance = new DicUnitEnumBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表DicUnitEnum，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="dicUnitEnum">要添加的DicUnitEnum数据实体对象</param>
		public   int AddDicUnitEnum(DicUnitEnumEntity dicUnitEnum)
		{
			  if (dicUnitEnum.Id > 0)
            {
                return UpdateDicUnitEnum(dicUnitEnum);
            } 
            else
            {
                return DicUnitEnumDA.Instance.AddDicUnitEnum(dicUnitEnum);
            }
	 	}

		/// <summary>
		/// 更新一条DicUnitEnum记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="dicUnitEnum">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateDicUnitEnum(DicUnitEnumEntity dicUnitEnum)
		{
			return DicUnitEnumDA.Instance.UpdateDicUnitEnum(dicUnitEnum);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDicUnitEnumByKey(int id)
        {
            return DicUnitEnumDA.Instance.DeleteDicUnitEnumByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicUnitEnumDisabled()
        {
            return DicUnitEnumDA.Instance.DeleteDicUnitEnumDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicUnitEnumByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return DicUnitEnumDA.Instance.DeleteDicUnitEnumByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicUnitEnumByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return DicUnitEnumDA.Instance.DisableDicUnitEnumByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个DicUnitEnum实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>DicUnitEnum实体</returns>
		/// <param name="columns">要返回的列</param>
		public   DicUnitEnumEntity GetDicUnitEnum(int id,bool iscache=false)
        {
            DicUnitEnumEntity entity = new DicUnitEnumEntity();
            if (!iscache)
            {
                entity = DicUnitEnumDA.Instance.GetDicUnitEnum(id);

            }
            else
            {
            string _cachekey = "GetDicUnitEnum"+ id;// SysCacheKey.DicUnitEnumListKey;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                entity= DicUnitEnumDA.Instance.GetDicUnitEnum(id);
                MemCache.AddCache(_cachekey, entity);
            }
            else
            {
                entity = (DicUnitEnumEntity)obj;
            }
            }
          
            return entity;

        }
        public int         GetDicUnitEnumByName(string name)
        {
         
            return DicUnitEnumDA.Instance.GetDicUnitEnumByName(name);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<DicUnitEnumEntity> GetDicUnitEnumList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return DicUnitEnumDA.Instance.GetDicUnitEnumList(pageSize, pageIndex, ref recordCount);
        }

        public IList<DicUnitEnumEntity> GetDicUnitEnumAll()
        {
                IList<DicUnitEnumEntity> list = null;
                string _cachekey = "GetDicUnitEnumAll";// SysCacheKey.DicUnitEnumListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                 
                    list = DicUnitEnumDA.Instance.GetDicUnitEnumAll();
                    MemCache.AddCache(_cachekey, list);
                }
           else
            {
                list = (IList<DicUnitEnumEntity>)obj;
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(DicUnitEnumEntity dicUnitEnum)
        {
            return DicUnitEnumDA.Instance.ExistNum(dicUnitEnum)>0;
        }
		
	}
}

