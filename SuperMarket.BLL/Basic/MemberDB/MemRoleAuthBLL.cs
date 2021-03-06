﻿using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemRoleAuth的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemRoleAuth的业务逻辑层。
	/// </summary>
	public class MemRoleAuthBLL
	{
	    #region 实例化
        public static MemRoleAuthBLL Instance
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
            internal static readonly MemRoleAuthBLL instance = new MemRoleAuthBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemRoleAuth，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRoleAuth">要添加的MemRoleAuth数据实体对象</param>
		public   int AddMemRoleAuth(MemRoleAuthEntity memRoleAuth)
		{
			  if (memRoleAuth.Id > 0)
            {
                return UpdateMemRoleAuth(memRoleAuth);
            }
          
            else if (MemRoleAuthBLL.Instance.IsExist(memRoleAuth))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemRoleAuthDA.Instance.AddMemRoleAuth(memRoleAuth);
            }
	 	}

		/// <summary>
		/// 更新一条MemRoleAuth记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRoleAuth">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemRoleAuth(MemRoleAuthEntity memRoleAuth)
		{
			return MemRoleAuthDA.Instance.UpdateMemRoleAuth(memRoleAuth);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemRoleAuthByKey(int id)
        {
            return MemRoleAuthDA.Instance.DeleteMemRoleAuthByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemRoleAuthDisabled()
        {
            return MemRoleAuthDA.Instance.DeleteMemRoleAuthDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemRoleAuthByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemRoleAuthDA.Instance.DeleteMemRoleAuthByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemRoleAuthByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemRoleAuthDA.Instance.DisableMemRoleAuthByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemRoleAuth实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemRoleAuth实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemRoleAuthEntity GetMemRoleAuth(int id)
		{
			return MemRoleAuthDA.Instance.GetMemRoleAuth(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemRoleAuthEntity> GetMemRoleAuthList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemRoleAuthDA.Instance.GetMemRoleAuthList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemRoleAuthAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemRoleAuthListKey";// SysCacheKey.MemRoleAuthListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemRoleAuthEntity> list = null;
                    list = MemRoleAuthDA.Instance.GetMemRoleAuthAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemRoleAuthEntity memRoleAuth)
        {
            return MemRoleAuthDA.Instance.ExistNum(memRoleAuth)>0;
        }
		
	}
}

