using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SysMsg的业务逻辑层。
创建时间：2016/8/8 10:22:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表SysMsg的业务逻辑层。
	/// </summary>
	public class SysMsgBLL
	{
	    #region 实例化
        public static SysMsgBLL Instance
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
            internal static readonly SysMsgBLL instance = new SysMsgBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SysMsg，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sysMsg">要添加的SysMsg数据实体对象</param>
		public   int AddSysMsg(SysMsgEntity sysMsg)
		{
			  if (sysMsg.Id > 0)
            {
                return UpdateSysMsg(sysMsg);
            }
          
            else if (SysMsgBLL.Instance.IsExist(sysMsg))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return SysMsgDA.Instance.AddSysMsg(sysMsg);
            }
	 	}

		/// <summary>
		/// 更新一条SysMsg记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sysMsg">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSysMsg(SysMsgEntity sysMsg)
		{
			return SysMsgDA.Instance.UpdateSysMsg(sysMsg);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSysMsgByKey(int id)
        {
            return SysMsgDA.Instance.DeleteSysMsgByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSysMsgDisabled()
        {
            return SysMsgDA.Instance.DeleteSysMsgDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSysMsgByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SysMsgDA.Instance.DeleteSysMsgByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSysMsgByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SysMsgDA.Instance.DisableSysMsgByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SysMsg实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SysMsg实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SysMsgEntity GetSysMsg(int id)
		{
			return SysMsgDA.Instance.GetSysMsg(id);			
		}
        public int GetNoReadNumByMemId(int memid)
        {
            return SysMsgDA.Instance.GetNoReadNumByMemId(memid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SysMsgEntity> GetSysMsgList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SysMsgDA.Instance.GetSysMsgList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSysMsgAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SysMsgListKey";// SysCacheKey.SysMsgListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<SysMsgEntity> list = null;
                    list = SysMsgDA.Instance.GetSysMsgAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SysMsgEntity sysMsg)
        {
            return SysMsgDA.Instance.ExistNum(sysMsg)>0;
        }
		
	}
}

