using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemRoleRelate的业务逻辑层。
创建时间：2016/8/1 17:46:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemRoleRelate的业务逻辑层。
	/// </summary>
	public class MemRoleRelateBLL
	{
	    #region 实例化
        public static MemRoleRelateBLL Instance
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
            internal static readonly MemRoleRelateBLL instance = new MemRoleRelateBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemRoleRelate，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRoleRelate">要添加的MemRoleRelate数据实体对象</param>
		public   int AddMemRoleRelate(MemRoleRelateEntity memRoleRelate)
		{
			  if (memRoleRelate.Id > 0)
            {
                return UpdateMemRoleRelate(memRoleRelate);
            }
          
            else if (MemRoleRelateBLL.Instance.IsExist(memRoleRelate))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemRoleRelateDA.Instance.AddMemRoleRelate(memRoleRelate);
            }
	 	}

		/// <summary>
		/// 更新一条MemRoleRelate记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRoleRelate">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemRoleRelate(MemRoleRelateEntity memRoleRelate)
		{
			return MemRoleRelateDA.Instance.UpdateMemRoleRelate(memRoleRelate);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemRoleRelateByKey(int id)
        {
            return MemRoleRelateDA.Instance.DeleteMemRoleRelateByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemRoleRelateDisabled()
        {
            return MemRoleRelateDA.Instance.DeleteMemRoleRelateDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemRoleRelateByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemRoleRelateDA.Instance.DeleteMemRoleRelateByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemRoleRelateByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemRoleRelateDA.Instance.DisableMemRoleRelateByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemRoleRelate实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemRoleRelate实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemRoleRelateEntity GetMemRoleRelate(int id)
		{
			return MemRoleRelateDA.Instance.GetMemRoleRelate(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemRoleRelateEntity> GetMemRoleRelateList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemRoleRelateDA.Instance.GetMemRoleRelateList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemRoleRelateAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemRoleRelateListKey";// SysCacheKey.MemRoleRelateListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemRoleRelateEntity> list = null;
                    list = MemRoleRelateDA.Instance.GetMemRoleRelateAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemRoleRelateEntity memRoleRelate)
        {
            return MemRoleRelateDA.Instance.ExistNum(memRoleRelate)>0;
        }
		
	}
}

