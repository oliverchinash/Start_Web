using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using System.Linq;

/*****************************************
功能描述：表BasicSiteProperties的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表BasicSiteProperties的业务逻辑层。
	/// </summary>
	public class BasicSitePropertiesBLL
	{
	    #region 实例化
        public static BasicSitePropertiesBLL Instance
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
            internal static readonly BasicSitePropertiesBLL instance = new BasicSitePropertiesBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表BasicSiteProperties，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="BasicSiteProperties">要添加的BasicSiteProperties数据实体对象</param>
		public   int AddBasicSiteProperties(BasicSitePropertiesEntity BasicSiteProperties)
		{
			  if (BasicSiteProperties.Id > 0)
            {
                return UpdateBasicSiteProperties(BasicSiteProperties);
            }
		    else if (string.IsNullOrEmpty(BasicSiteProperties.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (BasicSitePropertiesBLL.Instance.IsExist(BasicSiteProperties))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return BasicSitePropertiesDA.Instance.AddBasicSiteProperties(BasicSiteProperties);
            }
	 	}

		/// <summary>
		/// 更新一条BasicSiteProperties记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="BasicSiteProperties">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateBasicSiteProperties(BasicSitePropertiesEntity BasicSiteProperties)
		{
			return BasicSitePropertiesDA.Instance.UpdateBasicSiteProperties(BasicSiteProperties);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteBasicSitePropertiesByKey(int id)
        {
            return BasicSitePropertiesDA.Instance.DeleteBasicSitePropertiesByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBasicSitePropertiesDisabled()
        {
            return BasicSitePropertiesDA.Instance.DeleteBasicSitePropertiesDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBasicSitePropertiesByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return BasicSitePropertiesDA.Instance.DeleteBasicSitePropertiesByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBasicSitePropertiesByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return BasicSitePropertiesDA.Instance.DisableBasicSitePropertiesByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个BasicSiteProperties实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>BasicSiteProperties实体</returns>
        /// <param name="columns">要返回的列</param>
        public BasicSitePropertiesEntity GetBasicSiteProperties(int id,bool cache=false)
        {
            BasicSitePropertiesEntity list = null;
            if (cache)
            {
                string _cachekey = "GetBasicSiteProperties_" + id;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = BasicSitePropertiesDA.Instance.GetBasicSiteProperties(id);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (BasicSitePropertiesEntity)obj;
                }
            }
            else
            {
                list = BasicSitePropertiesDA.Instance.GetBasicSiteProperties(id);
            }
            return list;
        } 
        public int ProcBindProperties(int classid, string propertiesstr)
        {
            return BasicSitePropertiesDA.Instance.ProcBindProperties(classid,  propertiesstr);

        }
        public int ProcGetProperties(int classid, string propertiesstr)
        {
            return BasicSitePropertiesDA.Instance.ProcGetProperties(classid, propertiesstr);

        }
        /// <summary>
        /// 根据分类Id 和属性名称获取对应属性iD
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="propertiesstr"></param>
        /// <returns></returns>
        public int GetPropertiesId(int classid, string propertiesstr)
        {
            return BasicSitePropertiesDA.Instance.GetPropertiesId(classid, propertiesstr); 
        }
        public int BindProperties(int classid, string propertname,int sort)
        {
            return BasicSitePropertiesDA.Instance.BindProperties(classid, propertname,sort);

        }
        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<BasicSitePropertiesEntity> GetBasicSitePropertiesList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return BasicSitePropertiesDA.Instance.GetBasicSitePropertiesList(pageSize, pageIndex, ref recordCount);
        }    
        /// <summary>
               /// 根据分类id 获取对应的分类产品属性
               /// </summary>
               /// <param name="classid"></param>
               /// <param name="pid"></param>
               /// <returns></returns> 
        public IList<BasicSitePropertiesEntity> GetListBySiteId(int siteid,int parentid,bool cache=false)
        {
            IList<BasicSitePropertiesEntity> _objlistall = null;
            if (cache)
            {
                string _cachekey = "BasicSitePropertiesList_" + siteid + "_" + parentid;// SysCacheKey.VWBasicSitePropertiesListKey;
                object _objcache = MemCache.GetCache(_cachekey);
                if (_objcache == null)
                {
                    _objlistall = BasicSitePropertiesDA.Instance.GetListBySiteId(siteid, parentid);
                }
                else
                {
                    _objlistall = (List<BasicSitePropertiesEntity>)_objcache;
                }
                MemCache.AddCache(_cachekey, _objlistall);
            }
            else
            {
                _objlistall = BasicSitePropertiesDA.Instance.GetListBySiteId(siteid, parentid);

            }
            return _objlistall; 
        }

        /// <summary>
        /// 根据分类id 获取对应的分类产品属性
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="pid"></param>
        /// <returns></returns> 
        public IList<BasicSitePropertiesEntity> GetPropertiesBySiteId(int siteid, bool cache=false )
        {
            IList<BasicSitePropertiesEntity> list= GetListBySiteId(siteid, 0, cache);
            var objlist = list.Where(p => p.IsSpec == 0);
            if(objlist!=null)
            {
                return objlist.ToList<BasicSitePropertiesEntity>();
            }
            return null;
        }
        public async Task GetBasicSitePropertiesAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="BasicSitePropertiesListKey";// SysCacheKey.BasicSitePropertiesListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<BasicSitePropertiesEntity> list = null;
                    list = BasicSitePropertiesDA.Instance.GetBasicSitePropertiesAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(BasicSitePropertiesEntity BasicSiteProperties)
        {
            return BasicSitePropertiesDA.Instance.ExistNum(BasicSiteProperties)>0;
        }
		
	}
}

