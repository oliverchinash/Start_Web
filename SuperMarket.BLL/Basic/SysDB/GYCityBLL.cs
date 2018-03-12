using System;
using System.Data;
using System.Linq;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using SuperMarket.Model.Const;
using SuperMarket.Core.Util; 
using SuperMarket.Core;
using SuperMarket.Model.Cache;
using System.Threading.Tasks;
using SuperMarket.Model.Common;
using SuperMarket.Core.Json;

/*****************************************
功能描述：表GYCity的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表GYCity的业务逻辑层。
	/// </summary>
	public class GYCityBLL
	{
	    #region 实例化
        public static GYCityBLL Instance
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
            internal static readonly GYCityBLL instance = new GYCityBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表GYCity，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYCity">要添加的GYCity数据实体对象</param>
		public   int AddGYCity(GYCityEntity gYCity)
		{
			  if (gYCity.Id > 0)
            {
                return UpdateGYCity(gYCity);
            }
				  else if (string.IsNullOrEmpty(gYCity.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (GYCityBLL.Instance.IsExist(gYCity))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return GYCityDA.Instance.AddGYCity(gYCity);
            }
	 	}

		/// <summary>
		/// 更新一条GYCity记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYCity">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateGYCity(GYCityEntity gYCity)
		{
			return GYCityDA.Instance.UpdateGYCity(gYCity);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteGYCityByKey(int id)
        {
            return GYCityDA.Instance.DeleteGYCityByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteGYCityDisabled()
        {
            return GYCityDA.Instance.DeleteGYCityDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteGYCityByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return GYCityDA.Instance.DeleteGYCityByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableGYCityByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return GYCityDA.Instance.DisableGYCityByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个GYCity实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>GYCity实体</returns>
		/// <param name="columns">要返回的列</param>
		public   GYCityEntity GetGYCity(int id)
		{
			return GYCityDA.Instance.GetGYCity(id);			
		}

        public GYCityEntity GetGYCityByCode(string code)
        {
            return GYCityDA.Instance.GetGYCityByCode(code);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<GYCityEntity> GetGYCityList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return GYCityDA.Instance.GetGYCityList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetGYCityAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="GYCityListKey";// SysCacheKey.GYCityListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<GYCityEntity> list = null;
                    list = GYCityDA.Instance.GetGYCityAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        public IList<GYCityEntity> GetGYCityAllByPid(string pcode)
        {
          
                string _cachekey = "GYCityListKey";// SysCacheKey.GYCityListKey;
                object _objcache = MemCache.GetCache(_cachekey);
             IList<GYCityEntity> listall = null;
            if (_objcache == null)
            { 
                listall = GYCityDA.Instance.GetGYCityAllByPid(pcode);
                return listall;
            }
            else
            {
                listall = (IList<GYCityEntity>)_objcache;
                var templist = from c in listall
                               where c.ParentCode== pcode
                               orderby c.Sort descending ,  c.Name
                               select c;
                return templist.ToList<GYCityEntity>();
            }
            
        }

        public string GetGYCityJsonByPid(string pcode)
        {
            string _cachekey = "GetGYCityJsonByPid_"+ pcode;// SysCacheKey.GYCityListKey;
            object _objcache = MemCache.GetCache(_cachekey);
            string liststr = "";
            if (_objcache == null)
            {
                IList<GYCityEntity> list = GetGYCityAllByPid(pcode);
                var listfilter = list.Select(
                   p => new
                   {
                       Code = p.Code,
                       Name = p.Name
                   });
                  liststr = JsonJC.ObjectToJson(listfilter);
            }
            else
            {
                liststr = (string)_objcache;
            }
            return liststr;
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(GYCityEntity gYCity)
        {
            return GYCityDA.Instance.ExistNum(gYCity)>0;
        }
		
	}
}

