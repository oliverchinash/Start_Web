using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemRole的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemRole的业务逻辑层。
	/// </summary>
	public class MemRoleBLL
	{
	    #region 实例化
        public static MemRoleBLL Instance
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
            internal static readonly MemRoleBLL instance = new MemRoleBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemRole，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRole">要添加的MemRole数据实体对象</param>
		public   int AddMemRole(MemRoleEntity memRole)
		{
			  if (memRole.Id > 0)
            {
                return UpdateMemRole(memRole);
            }
				  else if (string.IsNullOrEmpty(memRole.RoleName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (MemRoleBLL.Instance.IsExist(memRole))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemRoleDA.Instance.AddMemRole(memRole);
            }
	 	}

		/// <summary>
		/// 更新一条MemRole记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRole">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemRole(MemRoleEntity memRole)
		{
			return MemRoleDA.Instance.UpdateMemRole(memRole);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemRoleByKey(int id)
        {
            return MemRoleDA.Instance.DeleteMemRoleByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemRoleDisabled()
        {
            return MemRoleDA.Instance.DeleteMemRoleDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemRoleByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemRoleDA.Instance.DeleteMemRoleByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemRoleByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemRoleDA.Instance.DisableMemRoleByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemRole实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemRole实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemRoleEntity GetMemRole(int id)
		{
			return MemRoleDA.Instance.GetMemRole(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemRoleEntity> GetMemRoleList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemRoleDA.Instance.GetMemRoleList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemRoleAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemRoleListKey";// SysCacheKey.MemRoleListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemRoleEntity> list = null;
                    list = MemRoleDA.Instance.GetMemRoleAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        public  IList<MemRoleEntity>   GetRoleAllByMemId(int id)
        {
       
                   return MemRoleDA.Instance.GetRoleAllByMemId(id);
                
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemRoleEntity memRole)
        {
            return MemRoleDA.Instance.ExistNum(memRole)>0;
        }
		
	}
}

