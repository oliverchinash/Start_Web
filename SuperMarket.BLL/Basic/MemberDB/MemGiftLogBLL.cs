﻿using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemGiftLog的业务逻辑层。
创建时间：2017/6/8 14:41:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemGiftLog的业务逻辑层。
	/// </summary>
	public class MemGiftLogBLL
	{
	    #region 实例化
        public static MemGiftLogBLL Instance
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
            internal static readonly MemGiftLogBLL instance = new MemGiftLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemGiftLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memGiftLog">要添加的MemGiftLog数据实体对象</param>
		public   int AddMemGiftLog(MemGiftLogEntity memGiftLog)
		{ 
                return MemGiftLogDA.Instance.AddMemGiftLog(memGiftLog);
         
	 	}

		/// <summary>
		/// 更新一条MemGiftLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memGiftLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemGiftLog(MemGiftLogEntity memGiftLog)
		{
			return MemGiftLogDA.Instance.UpdateMemGiftLog(memGiftLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemGiftLogByKey(int id)
        {
            return MemGiftLogDA.Instance.DeleteMemGiftLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemGiftLogDisabled()
        {
            return MemGiftLogDA.Instance.DeleteMemGiftLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemGiftLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemGiftLogDA.Instance.DeleteMemGiftLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemGiftLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemGiftLogDA.Instance.DisableMemGiftLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemGiftLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemGiftLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemGiftLogEntity GetMemGiftLog(int id)
		{
			return MemGiftLogDA.Instance.GetMemGiftLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemGiftLogEntity> GetMemGiftLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemGiftLogDA.Instance.GetMemGiftLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemGiftLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemGiftLogListKey";// SysCacheKey.MemGiftLogListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<MemGiftLogEntity> list = null;
                    list = MemGiftLogDA.Instance.GetMemGiftLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemGiftLogEntity memGiftLog)
        {
            return MemGiftLogDA.Instance.ExistNum(memGiftLog)>0;
        }
		
	}
}

