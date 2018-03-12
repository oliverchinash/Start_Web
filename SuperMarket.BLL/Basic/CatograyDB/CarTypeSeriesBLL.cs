using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using System.Collections;

/*****************************************
功能描述：表CarTypeSeries的业务逻辑层。
创建时间：2017/4/21 11:46:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表CarTypeSeries的业务逻辑层。
	/// </summary>
	public class CarTypeSeriesBLL
	{
	    #region 实例化
        public static CarTypeSeriesBLL Instance
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
            internal static readonly CarTypeSeriesBLL instance = new CarTypeSeriesBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CarTypeSeries，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeSeries">要添加的CarTypeSeries数据实体对象</param>
		public   int AddCarTypeSeries(CarTypeSeriesEntity carTypeSeries)
		{
			  if (carTypeSeries.Id > 0)
            {
                return UpdateCarTypeSeries(carTypeSeries);
            }
				  else if (string.IsNullOrEmpty(carTypeSeries.FullName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(carTypeSeries.ShortName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CarTypeSeriesBLL.Instance.IsExist(carTypeSeries))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CarTypeSeriesDA.Instance.AddCarTypeSeries(carTypeSeries);
            }
	 	}

		/// <summary>
		/// 更新一条CarTypeSeries记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeSeries">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCarTypeSeries(CarTypeSeriesEntity carTypeSeries)
		{
			return CarTypeSeriesDA.Instance.UpdateCarTypeSeries(carTypeSeries);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCarTypeSeriesByKey(int id)
        {
            return CarTypeSeriesDA.Instance.DeleteCarTypeSeriesByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeSeriesDisabled()
        {
            return CarTypeSeriesDA.Instance.DeleteCarTypeSeriesDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeSeriesByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CarTypeSeriesDA.Instance.DeleteCarTypeSeriesByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeSeriesByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CarTypeSeriesDA.Instance.DisableCarTypeSeriesByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CarTypeSeries实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CarTypeSeries实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CarTypeSeriesEntity GetCarTypeSeries(int id)
		{
			return CarTypeSeriesDA.Instance.GetCarTypeSeries(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeSeriesEntity> GetCarTypeSeriesList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CarTypeSeriesDA.Instance.GetCarTypeSeriesList(pageSize, pageIndex, ref recordCount);
        }

        public IList<VWTreeCTSeriesEntity> GetTreeCTSeries(int parentbrandid)
        {
            IList<VWTreeCTSeriesEntity> resultlist = new List<VWTreeCTSeriesEntity>();
            string _cachekey = "GetTreeCTSeries_"+ parentbrandid;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                IList<CarTypeBrandEntity> list = null;
                list = CarTypeBrandDA.Instance.GetCarTypeBrandAll(parentbrandid,1);
                if (list != null && list.Count > 0)
                {       
                    foreach (CarTypeBrandEntity entity in list)
                    {
                        IList<CarTypeSeriesEntity> seri = CarTypeSeriesBLL.Instance.GetCarTypeSeriesAll(-1,entity.Id);
                        VWTreeCTSeriesEntity addentity = new VWTreeCTSeriesEntity();
                        addentity.Name = entity.BrandName;
                        addentity.Children = seri;
                        resultlist.Add(addentity);
                    } 
                    MemCache.AddCache(_cachekey, resultlist);
                }
            }
            else
            {
                resultlist = (IList<VWTreeCTSeriesEntity>)obj;
            }
            return resultlist;
        }
        public IList<VWTreeCTSeriesEntity> GetTreeCTSeriesBySubBrand(int  subbrandid)
        {
            IList<VWTreeCTSeriesEntity> resultlist = new List<VWTreeCTSeriesEntity>();
            string _cachekey = "GetTreeCTSeriesBySubBrand_" + subbrandid;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                IList<CarTypeBrandEntity> list = new List<CarTypeBrandEntity>();
                CarTypeBrandEntity brandentity = CarTypeBrandDA.Instance.GetCarTypeBrand(subbrandid);
                if (brandentity != null && brandentity.Id > 0) list.Add(brandentity); 
                if (list != null && list.Count > 0)
                {
                    foreach (CarTypeBrandEntity entity in list)
                    {
                        IList<CarTypeSeriesEntity> seri = CarTypeSeriesBLL.Instance.GetCarTypeSeriesAll(-1, entity.Id);
                        VWTreeCTSeriesEntity addentity = new VWTreeCTSeriesEntity();
                        addentity.Name = entity.BrandName;
                        addentity.Children = seri;
                        resultlist.Add(addentity);
                    }
                    MemCache.AddCache(_cachekey, resultlist);
                }
            }
            else
            {
                resultlist = (IList<VWTreeCTSeriesEntity>)obj;
            }
            return resultlist;
        }


        public IList<CarTypeSeriesEntity> GetCarTypeSeriesAll(int standbrandid,int ParentBrandId)
        {
            IList<CarTypeSeriesEntity> list = null;
            string _cachekey = "GetCarTypeSeriesAll_"+ standbrandid + "_"+ ParentBrandId;// SysCacheKey.CarTypeSeriesListKey;
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
                list = CarTypeSeriesDA.Instance.GetCarTypeSeriesAll(standbrandid, ParentBrandId);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<CarTypeSeriesEntity>)obj;
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CarTypeSeriesEntity carTypeSeries)
        {
            return CarTypeSeriesDA.Instance.ExistNum(carTypeSeries)>0;
        }
		
	}
}

