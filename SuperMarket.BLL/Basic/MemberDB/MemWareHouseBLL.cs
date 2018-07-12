using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemWareHouse的业务逻辑层。
创建时间：2017/8/1 22:17:17
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemWareHouse的业务逻辑层。
	/// </summary>
	public class MemWareHouseBLL
	{
	    #region 实例化
        public static MemWareHouseBLL Instance
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
            internal static readonly MemWareHouseBLL instance = new MemWareHouseBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemWareHouse，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memWareHouse">要添加的MemWareHouse数据实体对象</param>
		public   int AddMemWareHouse(MemWareHouseEntity memWareHouse)
		{    
                return MemWareHouseDA.Instance.AddMemWareHouse(memWareHouse);
            
	 	}

		/// <summary>
		/// 更新一条MemWareHouse记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memWareHouse">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemWareHouse(MemWareHouseEntity memWareHouse)
		{
			return MemWareHouseDA.Instance.UpdateMemWareHouse(memWareHouse);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemWareHouseByKey(int id)
        {
            return MemWareHouseDA.Instance.DeleteMemWareHouseByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemWareHouseDisabled()
        {
            return MemWareHouseDA.Instance.DeleteMemWareHouseDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemWareHouseByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemWareHouseDA.Instance.DeleteMemWareHouseByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemWareHouseByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemWareHouseDA.Instance.DisableMemWareHouseByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemWareHouse实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemWareHouse实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemWareHouseEntity GetMemWareHouse(int id)
		{
			return MemWareHouseDA.Instance.GetMemWareHouse(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemWareHouseEntity> GetMemWareHouseList(int pageSize, int pageIndex, ref  int recordCount,int memid)
        {
            return MemWareHouseDA.Instance.GetMemWareHouseList(pageSize, pageIndex, ref recordCount, memid);
        }
		
		public async Task GetMemWareHouseAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemWareHouseListKey";// SysCacheKey.MemWareHouseListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemWareHouseEntity> list = null;
                    list = MemWareHouseDA.Instance.GetMemWareHouseAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemWareHouseEntity memWareHouse)
        {
            return MemWareHouseDA.Instance.ExistNum(memWareHouse)>0;
        }
		
	}
}

