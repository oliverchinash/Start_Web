using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ComProperty的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ComProperty的业务逻辑层。
	/// </summary>
	public class ComPropertyBLL
	{
	    #region 实例化
        public static ComPropertyBLL Instance
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
            internal static readonly ComPropertyBLL instance = new ComPropertyBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ComProperty，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="comProperty">要添加的ComProperty数据实体对象</param>
		public   int AddComProperty(ComPropertyEntity comProperty)
		{
			  if (comProperty.Id > 0)
            {
                return UpdateComProperty(comProperty);
            }
				  else if (string.IsNullOrEmpty(comProperty.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ComPropertyBLL.Instance.IsExist(comProperty))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ComPropertyDA.Instance.AddComProperty(comProperty);
            }
	 	}

		/// <summary>
		/// 更新一条ComProperty记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="comProperty">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateComProperty(ComPropertyEntity comProperty)
		{
			return ComPropertyDA.Instance.UpdateComProperty(comProperty);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteComPropertyByKey(int id)
        {
            return ComPropertyDA.Instance.DeleteComPropertyByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteComPropertyDisabled()
        {
            return ComPropertyDA.Instance.DeleteComPropertyDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteComPropertyByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ComPropertyDA.Instance.DeleteComPropertyByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableComPropertyByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ComPropertyDA.Instance.DisableComPropertyByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ComProperty实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ComProperty实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ComPropertyEntity GetComProperty(int id)
		{
			return ComPropertyDA.Instance.GetComProperty(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ComPropertyEntity> GetComPropertyList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ComPropertyDA.Instance.GetComPropertyList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetComPropertyAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ComPropertyListKey";// SysCacheKey.ComPropertyListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ComPropertyEntity> list = null;
                    list = ComPropertyDA.Instance.GetComPropertyAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ComPropertyEntity comProperty)
        {
            return ComPropertyDA.Instance.ExistNum(comProperty)>0;
        }
		
	}
}

