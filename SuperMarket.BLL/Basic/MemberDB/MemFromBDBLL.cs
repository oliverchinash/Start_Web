using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemFromBD的业务逻辑层。
创建时间：2017/4/5 21:18:29
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemFromBD的业务逻辑层。
	/// </summary>
	public class MemFromBDBLL
	{
	    #region 实例化
        public static MemFromBDBLL Instance
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
            internal static readonly MemFromBDBLL instance = new MemFromBDBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemFromBD，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memFromBD">要添加的MemFromBD数据实体对象</param>
		public   int AddMemFromBD(MemFromBDEntity memFromBD)
		{
			if (memFromBD.Id > 0)
            {
                return UpdateMemFromBD(memFromBD);
            }
			else if (string.IsNullOrEmpty(memFromBD.CompanyName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
            else if (MemFromBDBLL.Instance.IsExist(memFromBD))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemFromBDDA.Instance.AddMemFromBD(memFromBD);
            }
	 	}

		/// <summary>
		/// 更新一条MemFromBD记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memFromBD">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemFromBD(MemFromBDEntity memFromBD)
		{
			return MemFromBDDA.Instance.UpdateMemFromBD(memFromBD);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemFromBDByKey(int id)
        {
            return MemFromBDDA.Instance.DeleteMemFromBDByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemFromBDDisabled()
        {
            return MemFromBDDA.Instance.DeleteMemFromBDDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemFromBDByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemFromBDDA.Instance.DeleteMemFromBDByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemFromBDByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemFromBDDA.Instance.DisableMemFromBDByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemFromBD实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemFromBD实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemFromBDEntity GetMemFromBD(int id)
		{
			return MemFromBDDA.Instance.GetMemFromBD(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemFromBDEntity> GetMemFromBDList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemFromBDDA.Instance.GetMemFromBDList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemFromBDAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemFromBDListKey";// SysCacheKey.MemFromBDListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<MemFromBDEntity> list = null;
                    list = MemFromBDDA.Instance.GetMemFromBDAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemFromBDEntity memFromBD)
        {
            return MemFromBDDA.Instance.ExistNum(memFromBD)>0;
        }
		
	}
}

