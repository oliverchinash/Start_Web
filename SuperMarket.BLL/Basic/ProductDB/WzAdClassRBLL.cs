using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WzAdClassR的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表WzAdClassR的业务逻辑层。
	/// </summary>
	public class WzAdClassRBLL
	{
	    #region 实例化
        public static WzAdClassRBLL Instance
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
            internal static readonly WzAdClassRBLL instance = new WzAdClassRBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WzAdClassR，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wzAdClassR">要添加的WzAdClassR数据实体对象</param>
		public   int AddWzAdClassR(WzAdClassREntity wzAdClassR)
		{
			  if (wzAdClassR.Id > 0)
            {
                return UpdateWzAdClassR(wzAdClassR);
            }
          
            else if (WzAdClassRBLL.Instance.IsExist(wzAdClassR))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return WzAdClassRDA.Instance.AddWzAdClassR(wzAdClassR);
            }
	 	}

		/// <summary>
		/// 更新一条WzAdClassR记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wzAdClassR">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWzAdClassR(WzAdClassREntity wzAdClassR)
		{
			return WzAdClassRDA.Instance.UpdateWzAdClassR(wzAdClassR);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWzAdClassRByKey(int id)
        {
            return WzAdClassRDA.Instance.DeleteWzAdClassRByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWzAdClassRDisabled()
        {
            return WzAdClassRDA.Instance.DeleteWzAdClassRDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWzAdClassRByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WzAdClassRDA.Instance.DeleteWzAdClassRByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWzAdClassRByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WzAdClassRDA.Instance.DisableWzAdClassRByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WzAdClassR实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WzAdClassR实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WzAdClassREntity GetWzAdClassR(int id)
		{
			return WzAdClassRDA.Instance.GetWzAdClassR(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WzAdClassREntity> GetWzAdClassRList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WzAdClassRDA.Instance.GetWzAdClassRList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetWzAdClassRAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WzAdClassRListKey";// SysCacheKey.WzAdClassRListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WzAdClassREntity> list = null;
                    list = WzAdClassRDA.Instance.GetWzAdClassRAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WzAdClassREntity wzAdClassR)
        {
            return WzAdClassRDA.Instance.ExistNum(wzAdClassR)>0;
        }
		
	}
}

