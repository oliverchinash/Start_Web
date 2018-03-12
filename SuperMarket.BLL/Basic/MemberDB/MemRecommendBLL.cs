using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemRecommend的业务逻辑层。
创建时间：2016/8/2 11:56:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemRecommend的业务逻辑层。
	/// </summary>
	public class MemRecommendBLL
	{
	    #region 实例化
        public static MemRecommendBLL Instance
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
            internal static readonly MemRecommendBLL instance = new MemRecommendBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemRecommend，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRecommend">要添加的MemRecommend数据实体对象</param>
		public   int AddMemRecommend(MemRecommendEntity memRecommend)
		{
			  if (memRecommend.Id > 0)
            {
                return UpdateMemRecommend(memRecommend);
            }
          
            else if (MemRecommendBLL.Instance.IsExist(memRecommend))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemRecommendDA.Instance.AddMemRecommend(memRecommend);
            }
	 	}

		/// <summary>
		/// 更新一条MemRecommend记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memRecommend">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemRecommend(MemRecommendEntity memRecommend)
		{
			return MemRecommendDA.Instance.UpdateMemRecommend(memRecommend);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemRecommendByKey(int id)
        {
            return MemRecommendDA.Instance.DeleteMemRecommendByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemRecommendDisabled()
        {
            return MemRecommendDA.Instance.DeleteMemRecommendDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemRecommendByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemRecommendDA.Instance.DeleteMemRecommendByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemRecommendByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemRecommendDA.Instance.DisableMemRecommendByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemRecommend实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemRecommend实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemRecommendEntity GetMemRecommend(int id)
		{
			return MemRecommendDA.Instance.GetMemRecommend(id);			
		}
        public MemRecommendEntity GetMemRecommendByCode(string code)
        {
            return MemRecommendDA.Instance.GetMemRecommendByCode(code);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemRecommendEntity> GetMemRecommendList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemRecommendDA.Instance.GetMemRecommendList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemRecommendAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemRecommendListKey";// SysCacheKey.MemRecommendListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemRecommendEntity> list = null;
                    list = MemRecommendDA.Instance.GetMemRecommendAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemRecommendEntity memRecommend)
        {
            return MemRecommendDA.Instance.ExistNum(memRecommend)>0;
        }
		
	}
}

