using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StaticCarType的业务逻辑层。
创建时间：2016/12/30 13:47:47
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表StaticCarType的业务逻辑层。
	/// </summary>
	public class StaticCarTypeBLL
	{
	    #region 实例化
        public static StaticCarTypeBLL Instance
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
            internal static readonly StaticCarTypeBLL instance = new StaticCarTypeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StaticCarType，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="staticCarType">要添加的StaticCarType数据实体对象</param>
		public   int AddStaticCarType(StaticCarTypeEntity staticCarType)
		{
			  if (staticCarType.Id > 0)
            {
                return UpdateStaticCarType(staticCarType);
            }
				  else if (string.IsNullOrEmpty(staticCarType.CarTypeName1))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(staticCarType.CarTypeName2))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(staticCarType.CarTypeName3))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(staticCarType.CarTypeName4))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (StaticCarTypeBLL.Instance.IsExist(staticCarType))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return StaticCarTypeDA.Instance.AddStaticCarType(staticCarType);
            }
	 	}

		/// <summary>
		/// 更新一条StaticCarType记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="staticCarType">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStaticCarType(StaticCarTypeEntity staticCarType)
		{
			return StaticCarTypeDA.Instance.UpdateStaticCarType(staticCarType);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStaticCarTypeByKey(int id)
        {
            return StaticCarTypeDA.Instance.DeleteStaticCarTypeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStaticCarTypeDisabled()
        {
            return StaticCarTypeDA.Instance.DeleteStaticCarTypeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStaticCarTypeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return StaticCarTypeDA.Instance.DeleteStaticCarTypeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStaticCarTypeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return StaticCarTypeDA.Instance.DisableStaticCarTypeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StaticCarType实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StaticCarType实体</returns>
		/// <param name="columns">要返回的列</param>
		public   StaticCarTypeEntity GetStaticCarType(int id)
		{
			return StaticCarTypeDA.Instance.GetStaticCarType(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<StaticCarTypeEntity> GetStaticCarTypeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return StaticCarTypeDA.Instance.GetStaticCarTypeList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetStaticCarTypeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="StaticCarTypeListKey";// SysCacheKey.StaticCarTypeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<StaticCarTypeEntity> list = null;
                    list = StaticCarTypeDA.Instance.GetStaticCarTypeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(StaticCarTypeEntity staticCarType)
        {
            return StaticCarTypeDA.Instance.ExistNum(staticCarType)>0;
        }
		
	}
}

