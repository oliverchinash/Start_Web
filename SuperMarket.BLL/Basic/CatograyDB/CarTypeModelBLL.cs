using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CarTypeModel的业务逻辑层。
创建时间：2017/4/21 11:46:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表CarTypeModel的业务逻辑层。
	/// </summary>
	public class CarTypeModelBLL
	{
	    #region 实例化
        public static CarTypeModelBLL Instance
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
            internal static readonly CarTypeModelBLL instance = new CarTypeModelBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CarTypeModel，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeModel">要添加的CarTypeModel数据实体对象</param>
		public   int AddCarTypeModel(CarTypeModelEntity carTypeModel)
		{
			  if (carTypeModel.Id > 0)
            {
                return UpdateCarTypeModel(carTypeModel);
            }
				  else if (string.IsNullOrEmpty(carTypeModel.ModelName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CarTypeModelBLL.Instance.IsExist(carTypeModel))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CarTypeModelDA.Instance.AddCarTypeModel(carTypeModel);
            }
	 	}

		/// <summary>
		/// 更新一条CarTypeModel记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeModel">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCarTypeModel(CarTypeModelEntity carTypeModel)
		{
			return CarTypeModelDA.Instance.UpdateCarTypeModel(carTypeModel);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCarTypeModelByKey(int id)
        {
            return CarTypeModelDA.Instance.DeleteCarTypeModelByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeModelDisabled()
        {
            return CarTypeModelDA.Instance.DeleteCarTypeModelDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeModelByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CarTypeModelDA.Instance.DeleteCarTypeModelByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeModelByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CarTypeModelDA.Instance.DisableCarTypeModelByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CarTypeModel实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CarTypeModel实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CarTypeModelEntity GetCarTypeModel(int id)
		{
			return CarTypeModelDA.Instance.GetCarTypeModel(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeModelEntity> GetCarTypeModelList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CarTypeModelDA.Instance.GetCarTypeModelList(pageSize, pageIndex, ref recordCount);
        }
        public  VWTreeCTModelEntity  GetTreeCTModel(int seriesid)
        {
            VWTreeCTModelEntity  resultlist = null;
            string _cachekey = "GetTreeCTModel_" + seriesid;
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
                IList<CarTypeModelEntity> list =CarTypeModelDA.Instance.GetCarTypeModelAll(seriesid);
                if(list!=null&& list.Count>0)
                {
                    List<string> liststryear = new List<string>();
                    List<string> liststrcapacity = new List<string>();
                    foreach (CarTypeModelEntity entity in list)
                    {
                        if(!liststryear.Contains(entity.CarYear.ToString()))
                        {
                            liststryear.Add(entity.CarYear.ToString());
                        }
                        if (!liststrcapacity.Contains(entity.Capacity ))
                        {
                            liststrcapacity.Add(entity.Capacity);
                        }
                    }
                    liststryear.Sort();
                  liststrcapacity.Sort();
                    
                  resultlist = new  VWTreeCTModelEntity();
                    resultlist.Capacitys = liststrcapacity;
                    resultlist.CarYears = liststryear;
                    resultlist.Children = list;
                    MemCache.AddCache(_cachekey, resultlist);
                }
            }
            else
            {
                resultlist = ( VWTreeCTModelEntity )obj;
            }
            return resultlist;
        }
		
		public IList<CarTypeModelEntity> GetCarTypeModelAll(int seriesid,bool cache=false)
        {
            IList<CarTypeModelEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetCarTypeModelAll_" + seriesid;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = CarTypeModelDA.Instance.GetCarTypeModelAll(seriesid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<CarTypeModelEntity>)obj;
                }
            }
            else
            { 
                list = CarTypeModelDA.Instance.GetCarTypeModelAll(seriesid);
            }
           
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CarTypeModelEntity carTypeModel)
        {
            return CarTypeModelDA.Instance.ExistNum(carTypeModel)>0;
        }
		
	}
}

