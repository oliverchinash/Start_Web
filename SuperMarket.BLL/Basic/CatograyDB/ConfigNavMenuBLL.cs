using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ConfigNavMenu的业务逻辑层。
创建时间：2017/4/21 15:47:07
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ConfigNavMenu的业务逻辑层。
	/// </summary>
	public class ConfigNavMenuBLL
	{
	    #region 实例化
        public static ConfigNavMenuBLL Instance
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
            internal static readonly ConfigNavMenuBLL instance = new ConfigNavMenuBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ConfigNavMenu，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="configNavMenu">要添加的ConfigNavMenu数据实体对象</param>
		public   int AddConfigNavMenu(ConfigNavMenuEntity configNavMenu)
		{
			  if (configNavMenu.Id > 0)
            {
                return UpdateConfigNavMenu(configNavMenu);
            }
				  else if (string.IsNullOrEmpty(configNavMenu.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ConfigNavMenuBLL.Instance.IsExist(configNavMenu))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ConfigNavMenuDA.Instance.AddConfigNavMenu(configNavMenu);
            }
	 	}

		/// <summary>
		/// 更新一条ConfigNavMenu记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="configNavMenu">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateConfigNavMenu(ConfigNavMenuEntity configNavMenu)
		{
			return ConfigNavMenuDA.Instance.UpdateConfigNavMenu(configNavMenu);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteConfigNavMenuByKey(int id)
        {
            return ConfigNavMenuDA.Instance.DeleteConfigNavMenuByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfigNavMenuDisabled()
        {
            return ConfigNavMenuDA.Instance.DeleteConfigNavMenuDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfigNavMenuByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ConfigNavMenuDA.Instance.DeleteConfigNavMenuByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfigNavMenuByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ConfigNavMenuDA.Instance.DisableConfigNavMenuByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ConfigNavMenu实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ConfigNavMenu实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ConfigNavMenuEntity GetConfigNavMenu(int id)
		{
			return ConfigNavMenuDA.Instance.GetConfigNavMenu(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ConfigNavMenuEntity> GetConfigNavMenuList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ConfigNavMenuDA.Instance.GetConfigNavMenuList(pageSize, pageIndex, ref recordCount);
        }

        public IList<VWConfigNavMenuEntity> GetConfigNavMenuAll(int navtype)
        {

            IList<VWConfigNavMenuEntity> resultlist = new List<VWConfigNavMenuEntity>(); 
            string _cachekey = "GetConfigNavMenuAll_" + navtype;// SysCacheKey.ConfigNavMenuListKey;
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
                IList<VWConfigNavMenuEntity> list = null ;
                list = ConfigNavMenuDA.Instance.GetConfigNavMenuAll(navtype);
                if (list != null && list.Count > 0)
                {
                    foreach (VWConfigNavMenuEntity entity in list)
                    { 
                        if(entity.ParentId==0)
                        { 
                            resultlist.Add(entity);
                        }
                        else
                        {
                            foreach (VWConfigNavMenuEntity entity2 in list)
                            {
                                if (entity.ParentId == entity2.Id)
                                {
                                    if (entity2.Children == null)
                                    {
                                        entity2.Children = new List<VWConfigNavMenuEntity>();
                                    }
                                    entity2.Children.Add(entity);
                                }
                            }
                        }
                    }
                   
                }
                MemCache.AddCache(_cachekey, resultlist);
            }
            else
            {
                resultlist = (IList<VWConfigNavMenuEntity>)obj;
            }
            return resultlist;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ConfigNavMenuEntity configNavMenu)
        {
            return ConfigNavMenuDA.Instance.ExistNum(configNavMenu)>0;
        }
		
	}
}

