using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CarType的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表CarType的业务逻辑层。
	/// </summary>
	public class CarTypeBLL
    {
	    #region 实例化
        public static CarTypeBLL Instance
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
            internal static readonly CarTypeBLL instance = new CarTypeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CarType，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="CarType">要添加的CarType数据实体对象</param>
		public   int AddCarType(CarTypeEntity CarType)
		{
			  if (CarType.Id > 0)
            {
                return UpdateCarType(CarType);
            }
				  else if (string.IsNullOrEmpty(CarType.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CarTypeBLL.Instance.IsExist(CarType))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CarTypeDA.Instance.AddCarType(CarType);
            }
	 	}

		/// <summary>
		/// 更新一条CarType记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="CarType">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCarType(CarTypeEntity CarType)
		{
			return CarTypeDA.Instance.UpdateCarType(CarType);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCarTypeByKey(int id)
        {
            return CarTypeDA.Instance.DeleteCarTypeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeDisabled()
        {
            return CarTypeDA.Instance.DeleteCarTypeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CarTypeDA.Instance.DeleteCarTypeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CarTypeDA.Instance.DisableCarTypeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CarType实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CarType实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CarTypeEntity GetCarType(int id)
        {
            string _cachekey = "GetCarType_" + id;
            object obj = MemCache.GetCache(_cachekey);
            CarTypeEntity  entity = new CarTypeEntity();
            if (obj == null)
            { 
                entity= CarTypeDA.Instance.GetCarType(id);
                MemCache.AddCache(_cachekey, entity);
            }
            else
            {
                entity = (CarTypeEntity)obj;
            }
            return entity; 		
		}
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeEntity> GetListByParent(int  pid,int status=1)
        {
            string _cachekey = "CarTypeList_"+ pid+ "_"+status;
            object obj = MemCache.GetCache(_cachekey);
            IList<CarTypeEntity> list = null;
            if (obj == null)
            {
                list = CarTypeDA.Instance.GetListByParent(pid, status);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<CarTypeEntity>)obj;
            }
            return list; 
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeEntity> GetListYearByParent(int pid)
        {
            string _cachekey = "CarTypeYearList_" + pid;
            object obj = MemCache.GetCache(_cachekey);
            IList<CarTypeEntity> list = null;
            if (obj == null)
            {
                list = CarTypeDA.Instance.GetListYearByParent(pid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<CarTypeEntity>)obj;
            }
            return list;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeEntity> GetCarTypeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CarTypeDA.Instance.GetCarTypeList(pageSize, pageIndex, ref recordCount);
        }
        public IList<CarTypeEntity> GetCarTypeAll(int menuid, int level, int isstandard, int parentid)
        {
            string _cachekey = "GetCarTypeAll_"+ menuid+"_"+ level+ "_" + isstandard + "_"+ parentid; 
            object obj = MemCache.GetCache(_cachekey);  
            IList<CarTypeEntity> list = null;
            if (obj == null)
            { 
         list = CarTypeDA.Instance.GetCarTypeAll(  menuid,   level,   isstandard,   parentid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<CarTypeEntity>)obj;
            }
            return list;
        }
        public async Task GetCarTypeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CarTypeListKey";// SysCacheKey.CarTypeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CarTypeEntity> list = null;
                    list = CarTypeDA.Instance.GetCarTypeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CarTypeEntity CarType)
        {
            return CarTypeDA.Instance.ExistNum(CarType)>0;
        }
		
	}
}

