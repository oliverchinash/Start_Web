using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表BasicSiteProDetails的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表BasicSiteProDetails的业务逻辑层。
	/// </summary>
	public class BasicSiteProDetailsBLL
	{
	    #region 实例化
        public static BasicSiteProDetailsBLL Instance
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
            internal static readonly BasicSiteProDetailsBLL instance = new BasicSiteProDetailsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表BasicSiteProDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="BasicSiteProDetails">要添加的BasicSiteProDetails数据实体对象</param>
		public   int AddBasicSiteProDetails(BasicSiteProDetailsEntity BasicSiteProDetails)
		{
			if (BasicSiteProDetails.Id > 0)
            {
                return UpdateBasicSiteProDetails(BasicSiteProDetails);
            }
		    else if (string.IsNullOrEmpty(BasicSiteProDetails.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	  
            else if (BasicSiteProDetailsBLL.Instance.IsExist(BasicSiteProDetails))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return BasicSiteProDetailsDA.Instance.AddBasicSiteProDetails(BasicSiteProDetails);
            }
	 	}

		/// <summary>
		/// 更新一条BasicSiteProDetails记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="BasicSiteProDetails">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateBasicSiteProDetails(BasicSiteProDetailsEntity BasicSiteProDetails)
		{
			return BasicSiteProDetailsDA.Instance.UpdateBasicSiteProDetails(BasicSiteProDetails);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteBasicSiteProDetailsByKey(int id)
        {
            return BasicSiteProDetailsDA.Instance.DeleteBasicSiteProDetailsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBasicSiteProDetailsDisabled()
        {
            return BasicSiteProDetailsDA.Instance.DeleteBasicSiteProDetailsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBasicSiteProDetailsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return BasicSiteProDetailsDA.Instance.DeleteBasicSiteProDetailsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBasicSiteProDetailsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return BasicSiteProDetailsDA.Instance.DisableBasicSiteProDetailsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个BasicSiteProDetails实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>BasicSiteProDetails实体</returns>
		/// <param name="columns">要返回的列</param>
		public   BasicSiteProDetailsEntity GetBasicSiteProDetails(int id,bool cache=false)
		{     BasicSiteProDetailsEntity list = null;
            if (cache)
            {
                string _cachekey = "GetBasicSiteProDetails_" + id;

                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = BasicSiteProDetailsDA.Instance.GetBasicSiteProDetails(id);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (BasicSiteProDetailsEntity)obj;
                }
            }
            else
            {
                list = BasicSiteProDetailsDA.Instance.GetBasicSiteProDetails(id);

            }
            return list;			
		}

        /// <summary>
        /// 根据分类属性Id,属性值获取属性值Id,没有就新增一个，并返回对应的值Id
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="properyid"></param>
        /// <param name="prodetailname"></param>
        /// <returns></returns>
        public int GetAndAddPropertDetailId(int properyid,string prodetailname)
        {
            return BasicSiteProDetailsDA.Instance.GetAndAddProPropertDetailId(properyid, prodetailname);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<BasicSiteProDetailsEntity> GetBasicSiteProDetailsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return BasicSiteProDetailsDA.Instance.GetBasicSiteProDetailsList(pageSize, pageIndex, ref recordCount);
        }
        public IList<BasicSiteProDetailsEntity> GetListByPropertyId(int propertyid, int parentid, bool cache = false)
        {
            IList<BasicSiteProDetailsEntity> list = null;
            if (cache)
            {
                string _cachekey = "BasicSiteProDetailsList_" + propertyid + parentid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = BasicSiteProDetailsDA.Instance.GetListByPropertyId(propertyid, parentid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<BasicSiteProDetailsEntity>)obj;
                }
            }
            else
            {
                list = BasicSiteProDetailsDA.Instance.GetListByPropertyId(propertyid, parentid);
            }

            return list;
        }
        public IList<BasicSiteProDetailsEntity> GetProDetailsBySiteId(int siteid,bool iscache=false)
        {
            IList<BasicSiteProDetailsEntity> list = null;
            if (!iscache)
            {
                list = BasicSiteProDetailsDA.Instance.GetProDetailsBySiteId(siteid);

            }
            else
            {

            string _cachekey = "GetProDetailsByClassId_" + siteid;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list = BasicSiteProDetailsDA.Instance.GetProDetailsBySiteId(siteid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<BasicSiteProDetailsEntity>)obj;
                }
            }
            return list;
        }
        public async Task GetBasicSiteProDetailsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="BasicSiteProDetailsListKey";// SysCacheKey.BasicSiteProDetailsListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<BasicSiteProDetailsEntity> list = null;
                    list = BasicSiteProDetailsDA.Instance.GetBasicSiteProDetailsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(BasicSiteProDetailsEntity BasicSiteProDetails)
        {
            return BasicSiteProDetailsDA.Instance.ExistNum(BasicSiteProDetails)>0;
        }
		
	}
}

