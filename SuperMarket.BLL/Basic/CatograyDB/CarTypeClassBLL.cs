using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CarTypeClass的业务逻辑层。
创建时间：2017/1/1 10:55:44
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表CarTypeClass的业务逻辑层。
	/// </summary>
	public class CarTypeClassBLL
	{
	    #region 实例化
        public static CarTypeClassBLL Instance
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
            internal static readonly CarTypeClassBLL instance = new CarTypeClassBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CarTypeClass，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeClass">要添加的CarTypeClass数据实体对象</param>
		public   int AddCarTypeClass(CarTypeClassEntity carTypeClass)
		{
			  if (carTypeClass.Id > 0)
            {
                return UpdateCarTypeClass(carTypeClass);
            }
				  else if (string.IsNullOrEmpty(carTypeClass.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CarTypeClassBLL.Instance.IsExist(carTypeClass))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CarTypeClassDA.Instance.AddCarTypeClass(carTypeClass);
            }
	 	}

		/// <summary>
		/// 更新一条CarTypeClass记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeClass">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCarTypeClass(CarTypeClassEntity carTypeClass)
		{
			return CarTypeClassDA.Instance.UpdateCarTypeClass(carTypeClass);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCarTypeClassByKey(int id)
        {
            return CarTypeClassDA.Instance.DeleteCarTypeClassByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeClassDisabled()
        {
            return CarTypeClassDA.Instance.DeleteCarTypeClassDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeClassByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CarTypeClassDA.Instance.DeleteCarTypeClassByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeClassByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CarTypeClassDA.Instance.DisableCarTypeClassByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CarTypeClass实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CarTypeClass实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CarTypeClassEntity GetCarTypeClass(int id)
		{
			return CarTypeClassDA.Instance.GetCarTypeClass(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeClassEntity> GetCarTypeClassList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CarTypeClassDA.Instance.GetCarTypeClassList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<CarTypeClassEntity> GetCarTypeClassAll()
        {
            IList<CarTypeClassEntity> list = null;

            string _cachekey = "GetCarTypeClassAll"; 
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                   list = CarTypeClassDA.Instance.GetCarTypeClassAll();
                    MemCache.AddCache(_cachekey, list);
                }
            else
            {
                list = (IList<CarTypeClassEntity>)obj;

            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CarTypeClassEntity carTypeClass)
        {
            return CarTypeClassDA.Instance.ExistNum(carTypeClass)>0;
        }
		
	}
}

