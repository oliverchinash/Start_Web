using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表GYAuth的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表GYAuth的业务逻辑层。
	/// </summary>
	public class GYAuthBLL
	{
	    #region 实例化
        public static GYAuthBLL Instance
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
            internal static readonly GYAuthBLL instance = new GYAuthBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表GYAuth，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYAuth">要添加的GYAuth数据实体对象</param>
		public   int AddGYAuth(GYAuthEntity gYAuth)
		{
			  if (gYAuth.Id > 0)
            {
                return UpdateGYAuth(gYAuth);
            }
				  else if (string.IsNullOrEmpty(gYAuth.AuthName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (GYAuthBLL.Instance.IsExist(gYAuth))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return GYAuthDA.Instance.AddGYAuth(gYAuth);
            }
	 	}

		/// <summary>
		/// 更新一条GYAuth记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYAuth">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateGYAuth(GYAuthEntity gYAuth)
		{
			return GYAuthDA.Instance.UpdateGYAuth(gYAuth);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteGYAuthByKey(int id)
        {
            return GYAuthDA.Instance.DeleteGYAuthByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteGYAuthDisabled()
        {
            return GYAuthDA.Instance.DeleteGYAuthDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteGYAuthByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return GYAuthDA.Instance.DeleteGYAuthByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableGYAuthByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return GYAuthDA.Instance.DisableGYAuthByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个GYAuth实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>GYAuth实体</returns>
		/// <param name="columns">要返回的列</param>
		public   GYAuthEntity GetGYAuth(int id)
		{
			return GYAuthDA.Instance.GetGYAuth(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<GYAuthEntity> GetGYAuthList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return GYAuthDA.Instance.GetGYAuthList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetGYAuthAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="GYAuthListKey";// SysCacheKey.GYAuthListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<GYAuthEntity> list = null;
                    list = GYAuthDA.Instance.GetGYAuthAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(GYAuthEntity gYAuth)
        {
            return GYAuthDA.Instance.ExistNum(gYAuth)>0;
        }
		
	}
}

