using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表PartsProductFound的业务逻辑层。
创建时间：2017/9/4 10:28:41
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表PartsProductFound的业务逻辑层。
	/// </summary>
	public class PartsProductFoundBLL
	{
	    #region 实例化
        public static PartsProductFoundBLL Instance
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
            internal static readonly PartsProductFoundBLL instance = new PartsProductFoundBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PartsProductFound，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="partsProductFound">要添加的PartsProductFound数据实体对象</param>
		public   int AddPartsProductFound(PartsProductFoundEntity partsProductFound)
		{
			  if (partsProductFound.Id > 0)
            {
                return UpdatePartsProductFound(partsProductFound);
            }
				  else if (string.IsNullOrEmpty(partsProductFound.ProductName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(partsProductFound.ProductFullName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (PartsProductFoundBLL.Instance.IsExist(partsProductFound))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return PartsProductFoundDA.Instance.AddPartsProductFound(partsProductFound);
            }
	 	}
        public int AddPartsByName(string proname,int _scopeid,int _scopetype)
        {
            return PartsProductFoundDA.Instance.AddPartsByName(proname, _scopeid, _scopetype);
        }
        /// <summary>
        /// 更新一条PartsProductFound记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="partsProductFound">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdatePartsProductFound(PartsProductFoundEntity partsProductFound)
		{
			return PartsProductFoundDA.Instance.UpdatePartsProductFound(partsProductFound);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePartsProductFoundByKey(int id)
        {
            return PartsProductFoundDA.Instance.DeletePartsProductFoundByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePartsProductFoundDisabled()
        {
            return PartsProductFoundDA.Instance.DeletePartsProductFoundDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePartsProductFoundByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PartsProductFoundDA.Instance.DeletePartsProductFoundByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePartsProductFoundByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PartsProductFoundDA.Instance.DisablePartsProductFoundByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PartsProductFound实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PartsProductFound实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PartsProductFoundEntity GetPartsProductFound(int id)
		{
			return PartsProductFoundDA.Instance.GetPartsProductFound(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PartsProductFoundEntity> GetPartsProductFoundList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return PartsProductFoundDA.Instance.GetPartsProductFoundList(pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获取显示的部分要选择的产品列表
        /// </summary>
        /// <param name="py"></param>
        /// <returns></returns>
        public IList<PartsProductFoundEntity> GetPartsProductFoundShow(string key,int scopetype,int scopeid,bool cache )
        {
            IList<PartsProductFoundEntity> list = new List<PartsProductFoundEntity>();
            string _cachekey = "GetPartsProductFoundShow_" + key+"_"+ scopetype+"_"+ scopeid;// SysCacheKey.PartsProductFoundListKey;
            if (cache)
            {
                object obj = MemCache.GetCache(_cachekey);
                if (obj != null)
                {
                    list = (IList<PartsProductFoundEntity>)obj;
                }
            }
            if (list == null || list.Count == 0)
            {
                list = PartsProductFoundDA.Instance.GetPartsProductFoundShow(key, scopetype, scopeid, SearchMethod.PY);
                if ((list == null || list.Count < 50)&&!string.IsNullOrEmpty( key))
                {
                    IList<PartsProductFoundEntity> list2 = PartsProductFoundDA.Instance.GetPartsProductFoundShow(key, scopetype, scopeid, SearchMethod.PingYing);
                    if (list2 != null && list2.Count > 0)
                    {
                        if (list == null) list = list2;
                        else
                        {
                            for (int i = 0; i < 50 - list.Count && i < list2.Count - 1; i++)
                            {
                                PartsProductFoundEntity entity = list2[i];
                                list.Add(entity);
                            }
                        }
                    }
                }
                if ((list == null || list.Count == 0) && !string.IsNullOrEmpty(key))
                {
                    IList<PartsProductFoundEntity> list3 = PartsProductFoundDA.Instance.GetPartsProductFoundShow(key, scopetype, scopeid, SearchMethod.Name);
                    if (list3 != null && list3.Count > 0)
                    {
                        if (list == null) list = list3;
                        else
                        {
                            for (int i = 0; i < 50 - list.Count && i < list3.Count - 1; i++)
                            {
                                PartsProductFoundEntity entity = list3[i];
                                list.Add(entity);
                            }
                        }
                    }
                }
                if (cache)
                {
                    MemCache.AddCache(_cachekey, list);
                }
            }
            return list;
        }
        public async Task GetPartsProductFoundAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="PartsProductFoundListKey";// SysCacheKey.PartsProductFoundListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PartsProductFoundEntity> list = null;
                    list = PartsProductFoundDA.Instance.GetPartsProductFoundAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PartsProductFoundEntity partsProductFound)
        {
            return PartsProductFoundDA.Instance.ExistNum(partsProductFound)>0;
        }
		
	}
}

