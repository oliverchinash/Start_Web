using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemBillCom的业务逻辑层。
创建时间：2016/11/17 22:48:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemBillCom的业务逻辑层。
	/// </summary>
	public class MemBillComBLL
	{
	    #region 实例化
        public static MemBillComBLL Instance
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
            internal static readonly MemBillComBLL instance = new MemBillComBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemBillCom，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memBillCom">要添加的MemBillCom数据实体对象</param>
		public   int AddMemBillCom(MemBillComEntity memBillCom)
		{
			  if (memBillCom.Id > 0)
            {
                return UpdateMemBillCom(memBillCom);
            } 
            else if (MemBillComBLL.Instance.IsExist(memBillCom))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemBillComDA.Instance.AddMemBillCom(memBillCom);
            }
	 	}

		/// <summary>
		/// 更新一条MemBillCom记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memBillCom">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemBillCom(MemBillComEntity memBillCom)
        {
            if (memBillCom.Id == 0)
            {
                return AddMemBillCom(memBillCom);
            } 
            return MemBillComDA.Instance.UpdateMemBillCom(memBillCom);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemBillComByKey(int id)
        {
            return MemBillComDA.Instance.DeleteMemBillComByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemBillComDisabled()
        {
            return MemBillComDA.Instance.DeleteMemBillComDisabled();
        }
        public int SetDefault(int billid, int memid)
        {
            return MemBillComDA.Instance.SetDefault(billid, memid);
        }
        public int DeleteBillComByKey(int id, int memid)
        {
            return MemBillComDA.Instance.DeleteBillComByKey(id, memid); 
        }

        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemBillComByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemBillComDA.Instance.DeleteMemBillComByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBillComByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemBillComDA.Instance.DisableMemBillComByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemBillCom实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemBillCom实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemBillComEntity GetMemBillCom(int id)
		{
			return MemBillComDA.Instance.GetMemBillCom(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemBillComEntity> GetMemBillComList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemBillComDA.Instance.GetMemBillComList(pageSize, pageIndex, ref recordCount,0);
        }
        public IList<MemBillComEntity> GetMemBillComListByMemId(int pageSize, int pageIndex, ref int recordCount,int memid)
        {
            return MemBillComDA.Instance.GetMemBillComList(pageSize, pageIndex, ref recordCount, memid);
        }

        public async Task GetMemBillComAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemBillComListKey";// SysCacheKey.MemBillComListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemBillComEntity> list = null;
                    list = MemBillComDA.Instance.GetMemBillComAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemBillComEntity memBillCom)
        {
            return MemBillComDA.Instance.ExistNum(memBillCom)>0;
        }
		
	}
}

