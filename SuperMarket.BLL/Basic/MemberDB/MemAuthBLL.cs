using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemAuth的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemAuth的业务逻辑层。
	/// </summary>
	public class MemAuthBLL
	{
	    #region 实例化
        public static MemAuthBLL Instance
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
            internal static readonly MemAuthBLL instance = new MemAuthBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemAuth，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memAuth">要添加的MemAuth数据实体对象</param>
		public   int AddMemAuth(MemAuthEntity memAuth)
		{
			  if (memAuth.Id > 0)
            {
                return UpdateMemAuth(memAuth);
            }
				  else if (string.IsNullOrEmpty(memAuth.AuthName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (MemAuthBLL.Instance.IsExist(memAuth))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemAuthDA.Instance.AddMemAuth(memAuth);
            }
	 	}

		/// <summary>
		/// 更新一条MemAuth记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memAuth">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemAuth(MemAuthEntity memAuth)
		{
			return MemAuthDA.Instance.UpdateMemAuth(memAuth);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemAuthByKey(int id)
        {
            return MemAuthDA.Instance.DeleteMemAuthByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemAuthDisabled()
        {
            return MemAuthDA.Instance.DeleteMemAuthDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemAuthByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemAuthDA.Instance.DeleteMemAuthByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemAuthByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemAuthDA.Instance.DisableMemAuthByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemAuth实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemAuth实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemAuthEntity GetMemAuth(int id)
		{
			return MemAuthDA.Instance.GetMemAuth(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemAuthEntity> GetMemAuthList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemAuthDA.Instance.GetMemAuthList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemAuthAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemAuthListKey";// SysCacheKey.MemAuthListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemAuthEntity> list = null;
                    list = MemAuthDA.Instance.GetMemAuthAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }

        public IList<MemAuthEntity> GetAuthAllByRoleIds(int memid)
        {
           
                return MemAuthDA.Instance.GetAuthAllByRoleIds(memid);
                  
        }
        
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemAuthEntity memAuth)
        {
            return MemAuthDA.Instance.ExistNum(memAuth)>0;
        }
		
	}
}

