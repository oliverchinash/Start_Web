using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StaticClassesFound的业务逻辑层。
创建时间：2016/12/30 13:47:47
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表StaticClassesFound的业务逻辑层。
	/// </summary>
	public class StaticClassesFoundBLL
	{
	    #region 实例化
        public static StaticClassesFoundBLL Instance
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
            internal static readonly StaticClassesFoundBLL instance = new StaticClassesFoundBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StaticClassesFound，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="staticClassesFound">要添加的StaticClassesFound数据实体对象</param>
		public   int AddStaticClassesFound(StaticClassesFoundEntity staticClassesFound)
		{
			  if (staticClassesFound.Id > 0)
            {
                return UpdateStaticClassesFound(staticClassesFound);
            }
				  else if (string.IsNullOrEmpty(staticClassesFound.ClassName1))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(staticClassesFound.ClassName2))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(staticClassesFound.ClassName3))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(staticClassesFound.ClassName4))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (StaticClassesFoundBLL.Instance.IsExist(staticClassesFound))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return StaticClassesFoundDA.Instance.AddStaticClassesFound(staticClassesFound);
            }
	 	}

		/// <summary>
		/// 更新一条StaticClassesFound记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="staticClassesFound">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStaticClassesFound(StaticClassesFoundEntity staticClassesFound)
		{
			return StaticClassesFoundDA.Instance.UpdateStaticClassesFound(staticClassesFound);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStaticClassesFoundByKey(int id)
        {
            return StaticClassesFoundDA.Instance.DeleteStaticClassesFoundByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStaticClassesFoundDisabled()
        {
            return StaticClassesFoundDA.Instance.DeleteStaticClassesFoundDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStaticClassesFoundByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return StaticClassesFoundDA.Instance.DeleteStaticClassesFoundByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStaticClassesFoundByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return StaticClassesFoundDA.Instance.DisableStaticClassesFoundByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StaticClassesFound实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StaticClassesFound实体</returns>
		/// <param name="columns">要返回的列</param>
		public   StaticClassesFoundEntity GetStaticClassesFound(int id)
		{
			return StaticClassesFoundDA.Instance.GetStaticClassesFound(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<StaticClassesFoundEntity> GetStaticClassesFoundList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return StaticClassesFoundDA.Instance.GetStaticClassesFoundList(pageSize, pageIndex, ref recordCount);
        }
		public IList<StaticClassesFoundEntity> GetSubStaticByClassId(int classid,int level)
        {
            IList<StaticClassesFoundEntity> list = null;
            string _cachekey = "GetSubStaticClassByClassId_"+ classid+"_"+ level;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list = StaticClassesFoundDA.Instance.GetSubStaticByClassId(classid, level);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<StaticClassesFoundEntity>)obj;
            }
            return list;
        }
        public IList<StaticClassesFoundEntity> GetStaticClassesFoundAll(bool cache)
        {
            IList<StaticClassesFoundEntity> list = null;
            if (cache) { 
                string _cachekey = "GetStaticClassesFoundAll"; 
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                { 
                    list = StaticClassesFoundDA.Instance.GetStaticClassesFoundAll();
                    MemCache.AddCache(_cachekey, list);
                }
            }
            else
            {
                 
                list = StaticClassesFoundDA.Instance.GetStaticClassesFoundAll();
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(StaticClassesFoundEntity staticClassesFound)
        {
            return StaticClassesFoundDA.Instance.ExistNum(staticClassesFound)>0;
        }
		
	}
}

