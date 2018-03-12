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
功能描述：表CarTypeBrand的业务逻辑层。
创建时间：2017/4/21 11:46:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表CarTypeBrand的业务逻辑层。
	/// </summary>
	public class CarTypeBrandBLL
	{
	    #region 实例化
        public static CarTypeBrandBLL Instance
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
            internal static readonly CarTypeBrandBLL instance = new CarTypeBrandBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CarTypeBrand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeBrand">要添加的CarTypeBrand数据实体对象</param>
		public   int AddCarTypeBrand(CarTypeBrandEntity carTypeBrand)
		{
			  if (carTypeBrand.Id > 0)
            {
                return UpdateCarTypeBrand(carTypeBrand);
            }
				  else if (string.IsNullOrEmpty(carTypeBrand.BrandName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CarTypeBrandBLL.Instance.IsExist(carTypeBrand))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CarTypeBrandDA.Instance.AddCarTypeBrand(carTypeBrand);
            }
	 	}

		/// <summary>
		/// 更新一条CarTypeBrand记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="carTypeBrand">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCarTypeBrand(CarTypeBrandEntity carTypeBrand)
		{
			return CarTypeBrandDA.Instance.UpdateCarTypeBrand(carTypeBrand);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCarTypeBrandByKey(int id)
        {
            return CarTypeBrandDA.Instance.DeleteCarTypeBrandByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeBrandDisabled()
        {
            return CarTypeBrandDA.Instance.DeleteCarTypeBrandDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeBrandByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CarTypeBrandDA.Instance.DeleteCarTypeBrandByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeBrandByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CarTypeBrandDA.Instance.DisableCarTypeBrandByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CarTypeBrand实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CarTypeBrand实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CarTypeBrandEntity GetCarTypeBrand(int id)
		{
			return CarTypeBrandDA.Instance.GetCarTypeBrand(id);			
		}
        public CarTypeBrandEntity GetParentByName(string name)
        {
            return CarTypeBrandDA.Instance.GetParentByName(name);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CarTypeBrandEntity> GetCarTypeBrandList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CarTypeBrandDA.Instance.GetCarTypeBrandList(pageSize, pageIndex, ref recordCount);
        }
		public IList<VWTreeCTBrandEntity> GetTreeCarTypeBrand(int isstand = 1)
        {
            IList<VWTreeCTBrandEntity> resultlist = new List<VWTreeCTBrandEntity>();
            string _cachekey = "GetTreeCarTypeBrand_"+ isstand;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                IList<VWTreeCTBrandEntity> list = null;
                list = CarTypeBrandDA.Instance.GetStandardTreeCTB(isstand);
                if (list != null && list.Count > 0)
                {
                    //string[] firstletter = new Array();
                    ArrayList firstletter = new ArrayList();
                    foreach (VWTreeCTBrandEntity entity in list)
                    {
                        if (!firstletter.Contains(entity.PYFirst))
                        {
                            firstletter.Add(entity.PYFirst);
                        }
                        firstletter.Sort();
                    }
                    foreach (string letter in firstletter)
                    {
                        VWTreeCTBrandEntity entity = new VWTreeCTBrandEntity();
                        entity.PYFirst = letter;
                        resultlist.Add(entity);
                    }
                    foreach (VWTreeCTBrandEntity entity in list)
                    {
                        foreach (VWTreeCTBrandEntity entity2 in resultlist)
                        {
                            if (entity.PYFirst == entity2.PYFirst)
                            {
                                if (entity2.Children == null)
                                {
                                    entity2.Children = new List<VWTreeCTBrandEntity>();
                                }
                                entity2.Children.Add(entity);
                                break;
                            }

                        }


                    }

                }
                MemCache.AddCache(_cachekey, resultlist);
            }
            else
            {
                resultlist = (IList<VWTreeCTBrandEntity>)obj;
            }
            return resultlist;
        }

        public Dictionary<string, IList<VWTreeCTBrandEntity>> GetCarTypeDic()
        {
            Dictionary<string, IList<VWTreeCTBrandEntity>> firstletterlist = new Dictionary<string, IList<VWTreeCTBrandEntity>>();
            Dictionary<int, IList<VWTreeCTBrandEntity>> sublist = new Dictionary<int, IList<VWTreeCTBrandEntity>>();
            string _cachekey = "GetCarTypeDic";
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                IList<VWTreeCTBrandEntity> list = null;
                list = CarTypeBrandDA.Instance.GetStandardTreeCTB(-1);
                if (list != null && list.Count > 0)
                {
                    //string[] firstletter = new Array(); 
                    foreach (VWTreeCTBrandEntity entity in list)
                    {
                        if (entity.IsStandardBrand == 1)
                        {
                            if (!firstletterlist.ContainsKey(entity.PYFirst))
                            {
                                firstletterlist.Add(entity.PYFirst, new List<VWTreeCTBrandEntity>());
                            }
                            firstletterlist[entity.PYFirst].Add(entity);
                            if (!sublist.ContainsKey(entity.Id))
                            {
                                sublist.Add(entity.Id, new List<VWTreeCTBrandEntity>());
                            }
                            entity.Children = sublist[entity.Id];
                        } 
                    }
                    foreach (VWTreeCTBrandEntity entity in list)
                    {
                        if (entity.ParentId > 0)
                        {
                            if (!sublist.ContainsKey(entity.ParentId))
                            {
                                sublist.Add(entity.ParentId, new List<VWTreeCTBrandEntity>());
                            }
                            sublist[entity.ParentId].Add(entity);
                        }
                    }
                }
                MemCache.AddCache(_cachekey, firstletterlist);
            }
            else
            {
                firstletterlist = (Dictionary<string, IList<VWTreeCTBrandEntity>>)obj;
            }
            return firstletterlist;
        }
        /// <summary>
        /// 获取有效的品牌字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,IList<CarTypeBrandEntity>> GetTreeCarTypeBrandDic()
        {
            Dictionary<string, IList<CarTypeBrandEntity>> dislist = new Dictionary<string, IList<CarTypeBrandEntity>>();
            IList<CarTypeBrandEntity> list = CarTypeBrandDA.Instance.GetCarTypeBrandAll(0, 1,1);
            if(list!=null&& list.Count>0)
            {
                foreach(CarTypeBrandEntity entity in list)
                {
                    if(entity.IsHot==1)
                    {
                        if(!dislist.ContainsKey("Hot"))
                        {
                            dislist.Add("Hot", new List<CarTypeBrandEntity>());
                        }
                        dislist["Hot"].Add(entity);
                    }
                    if (!dislist.ContainsKey(entity.PYFirst))
                    {
                        dislist.Add(entity.PYFirst, new List<CarTypeBrandEntity>());
                    }
                    dislist[entity.PYFirst].Add(entity);
                }
            }
            return dislist;
        }
        /// <summary>
        /// 获取所有子集品牌
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public IList<CarTypeBrandEntity> GetCarTypeBrandAll(int parentid,bool cache=true)
        {
            IList<CarTypeBrandEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetCarTypeBrandAll_" + parentid;// SysCacheKey.CarTypeBrandListKey;
                object obj = MemCache.GetCache(_cachekey);  
                if (obj == null)
                {
                    list = CarTypeBrandDA.Instance.GetCarTypeBrandAll(parentid,1);
                    MemCache.AddCache(_cachekey, list);
                }
            }
            else
            {
                list = CarTypeBrandDA.Instance.GetCarTypeBrandAll(parentid, 1);
            }
            return list; 
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CarTypeBrandEntity carTypeBrand)
        {
            return CarTypeBrandDA.Instance.ExistNum(carTypeBrand)>0;
        }
		
	}
}

