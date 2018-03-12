using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SysUser的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表SysUser的业务逻辑层。
	/// </summary>
	public class SysUserBLL
	{
	    #region 实例化
        public static SysUserBLL Instance
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
            internal static readonly SysUserBLL instance = new SysUserBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SysUser，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sysUser">要添加的SysUser数据实体对象</param>
		public   int AddSysUser(SysUserEntity sysUser)
		{
			  if (sysUser.Id > 0)
            {
                return UpdateSysUser(sysUser);
            } 
            else if (SysUserBLL.Instance.IsExist(sysUser))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return SysUserDA.Instance.AddSysUser(sysUser);
            }
	 	}

		/// <summary>
		/// 更新一条SysUser记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sysUser">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSysUser(SysUserEntity sysUser)
		{
			return SysUserDA.Instance.UpdateSysUser(sysUser);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSysUserByKey(int id)
        {
            return SysUserDA.Instance.DeleteSysUserByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSysUserDisabled()
        {
            return SysUserDA.Instance.DeleteSysUserDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSysUserByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SysUserDA.Instance.DeleteSysUserByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSysUserByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SysUserDA.Instance.DisableSysUserByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SysUser实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SysUser实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SysUserEntity GetSysUser(int id)
		{
			return SysUserDA.Instance.GetSysUser(id);			
		}
        public SysUserEntity GetUserByUserCode(string code)
        {
            return SysUserDA.Instance.GetUserByUserCode(code);
        }
        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SysUserEntity> GetSysUserList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SysUserDA.Instance.GetSysUserList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSysUserAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SysUserListKey";// SysCacheKey.SysUserListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<SysUserEntity> list = null;
                    list = SysUserDA.Instance.GetSysUserAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SysUserEntity sysUser)
        {
            return SysUserDA.Instance.ExistNum(sysUser)>0;
        }
		
	}
}

