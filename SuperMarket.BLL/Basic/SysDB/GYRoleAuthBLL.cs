using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表GYRoleAuth的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表GYRoleAuth的业务逻辑层。
	/// </summary>
	public class GYRoleAuthBLL
	{
	    #region 实例化
        public static GYRoleAuthBLL Instance
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
            internal static readonly GYRoleAuthBLL instance = new GYRoleAuthBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表GYRoleAuth，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYRoleAuth">要添加的GYRoleAuth数据实体对象</param>
		public   int AddGYRoleAuth(GYRoleAuthEntity gYRoleAuth)
		{
			  if (gYRoleAuth.Id > 0)
            {
                return UpdateGYRoleAuth(gYRoleAuth);
            }
          
            else if (GYRoleAuthBLL.Instance.IsExist(gYRoleAuth))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return GYRoleAuthDA.Instance.AddGYRoleAuth(gYRoleAuth);
            }
	 	}

		/// <summary>
		/// 更新一条GYRoleAuth记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYRoleAuth">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateGYRoleAuth(GYRoleAuthEntity gYRoleAuth)
		{
			return GYRoleAuthDA.Instance.UpdateGYRoleAuth(gYRoleAuth);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteGYRoleAuthByKey(int id)
        {
            return GYRoleAuthDA.Instance.DeleteGYRoleAuthByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteGYRoleAuthDisabled()
        {
            return GYRoleAuthDA.Instance.DeleteGYRoleAuthDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteGYRoleAuthByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return GYRoleAuthDA.Instance.DeleteGYRoleAuthByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableGYRoleAuthByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return GYRoleAuthDA.Instance.DisableGYRoleAuthByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个GYRoleAuth实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>GYRoleAuth实体</returns>
		/// <param name="columns">要返回的列</param>
		public   GYRoleAuthEntity GetGYRoleAuth(int id)
		{
			return GYRoleAuthDA.Instance.GetGYRoleAuth(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<GYRoleAuthEntity> GetGYRoleAuthList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return GYRoleAuthDA.Instance.GetGYRoleAuthList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetGYRoleAuthAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="GYRoleAuthListKey";// SysCacheKey.GYRoleAuthListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<GYRoleAuthEntity> list = null;
                    list = GYRoleAuthDA.Instance.GetGYRoleAuthAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(GYRoleAuthEntity gYRoleAuth)
        {
            return GYRoleAuthDA.Instance.ExistNum(gYRoleAuth)>0;
        }
		
	}
}

