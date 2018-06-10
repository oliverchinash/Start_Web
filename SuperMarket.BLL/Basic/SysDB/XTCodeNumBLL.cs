using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表XTCodeNum的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表XTCodeNum的业务逻辑层。
	/// </summary>
	public class XTCodeNumBLL
	{
	    #region 实例化
        public static XTCodeNumBLL Instance
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
            internal static readonly XTCodeNumBLL instance = new XTCodeNumBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表XTCodeNum，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="xTCodeNum">要添加的XTCodeNum数据实体对象</param>
		public   int AddXTCodeNum(XTCodeNumEntity xTCodeNum)
		{
			  if (xTCodeNum.Id > 0)
            {
                return UpdateXTCodeNum(xTCodeNum);
            }
          
            else if (XTCodeNumBLL.Instance.IsExist(xTCodeNum))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return XTCodeNumDA.Instance.AddXTCodeNum(xTCodeNum);
            }
	 	}

		/// <summary>
		/// 更新一条XTCodeNum记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="xTCodeNum">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateXTCodeNum(XTCodeNumEntity xTCodeNum)
		{
			return XTCodeNumDA.Instance.UpdateXTCodeNum(xTCodeNum);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteXTCodeNumByKey(int id)
        {
            return XTCodeNumDA.Instance.DeleteXTCodeNumByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteXTCodeNumDisabled()
        {
            return XTCodeNumDA.Instance.DeleteXTCodeNumDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteXTCodeNumByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return XTCodeNumDA.Instance.DeleteXTCodeNumByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableXTCodeNumByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return XTCodeNumDA.Instance.DisableXTCodeNumByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个XTCodeNum实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>XTCodeNum实体</returns>
		/// <param name="columns">要返回的列</param>
		public   XTCodeNumEntity GetXTCodeNum(int id)
		{
			return XTCodeNumDA.Instance.GetXTCodeNum(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<XTCodeNumEntity> GetXTCodeNumList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return XTCodeNumDA.Instance.GetXTCodeNumList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetXTCodeNumAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="XTCodeNumListKey";// SysCacheKey.XTCodeNumListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<XTCodeNumEntity> list = null;
                    list = XTCodeNumDA.Instance.GetXTCodeNumAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(XTCodeNumEntity xTCodeNum)
        {
            return XTCodeNumDA.Instance.ExistNum(xTCodeNum)>0;
        }
		
	}
}

