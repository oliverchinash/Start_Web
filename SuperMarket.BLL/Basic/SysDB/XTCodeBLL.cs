using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表XTCode的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表XTCode的业务逻辑层。
	/// </summary>
	public class XTCodeBLL
	{
	    #region 实例化
        public static XTCodeBLL Instance
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
            internal static readonly XTCodeBLL instance = new XTCodeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表XTCode，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="xTCode">要添加的XTCode数据实体对象</param>
		public   int AddXTCode(XTCodeEntity xTCode)
		{
			  if (xTCode.Id > 0)
            {
                return UpdateXTCode(xTCode);
            }
				  else if (string.IsNullOrEmpty(xTCode.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (XTCodeBLL.Instance.IsExist(xTCode))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return XTCodeDA.Instance.AddXTCode(xTCode);
            }
	 	}

		/// <summary>
		/// 更新一条XTCode记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="xTCode">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateXTCode(XTCodeEntity xTCode)
		{
			return XTCodeDA.Instance.UpdateXTCode(xTCode);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteXTCodeByKey(int id)
        {
            return XTCodeDA.Instance.DeleteXTCodeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteXTCodeDisabled()
        {
            return XTCodeDA.Instance.DeleteXTCodeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteXTCodeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return XTCodeDA.Instance.DeleteXTCodeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableXTCodeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return XTCodeDA.Instance.DisableXTCodeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个XTCode实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>XTCode实体</returns>
		/// <param name="columns">要返回的列</param>
		public   XTCodeEntity GetXTCode(int id)
		{
			return XTCodeDA.Instance.GetXTCode(id);			
		}
        public string GetCodeFromProc(string codetype)
        {
            return XTCodeDA.Instance.GetCodeFromProc(codetype);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<XTCodeEntity> GetXTCodeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return XTCodeDA.Instance.GetXTCodeList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetXTCodeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="XTCodeListKey";// SysCacheKey.XTCodeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<XTCodeEntity> list = null;
                    list = XTCodeDA.Instance.GetXTCodeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(XTCodeEntity xTCode)
        {
            return XTCodeDA.Instance.ExistNum(xTCode)>0;
        }
		
	}
}

