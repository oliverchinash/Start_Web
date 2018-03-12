using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表InQuiryOrderFlow的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InQuiryOrderFlow的业务逻辑层。
	/// </summary>
	public class InQuiryOrderFlowBLL
	{
	    #region 实例化
        public static InQuiryOrderFlowBLL Instance
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
            internal static readonly InQuiryOrderFlowBLL instance = new InQuiryOrderFlowBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表InQuiryOrderFlow，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inQuiryOrderFlow">要添加的InQuiryOrderFlow数据实体对象</param>
		public   int AddInQuiryOrderFlow(InQuiryOrderFlowEntity inQuiryOrderFlow)
		{
			  if (inQuiryOrderFlow.Id > 0)
            {
                return UpdateInQuiryOrderFlow(inQuiryOrderFlow);
            }
				  else if (string.IsNullOrEmpty(inQuiryOrderFlow.MethodName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (InQuiryOrderFlowBLL.Instance.IsExist(inQuiryOrderFlow))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return InQuiryOrderFlowDA.Instance.AddInQuiryOrderFlow(inQuiryOrderFlow);
            }
	 	}

		/// <summary>
		/// 更新一条InQuiryOrderFlow记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inQuiryOrderFlow">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateInQuiryOrderFlow(InQuiryOrderFlowEntity inQuiryOrderFlow)
		{
			return InQuiryOrderFlowDA.Instance.UpdateInQuiryOrderFlow(inQuiryOrderFlow);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInQuiryOrderFlowByKey(int id)
        {
            return InQuiryOrderFlowDA.Instance.DeleteInQuiryOrderFlowByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInQuiryOrderFlowDisabled()
        {
            return InQuiryOrderFlowDA.Instance.DeleteInQuiryOrderFlowDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInQuiryOrderFlowByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InQuiryOrderFlowDA.Instance.DeleteInQuiryOrderFlowByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInQuiryOrderFlowByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InQuiryOrderFlowDA.Instance.DisableInQuiryOrderFlowByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InQuiryOrderFlow实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InQuiryOrderFlow实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InQuiryOrderFlowEntity GetInQuiryOrderFlow(int id)
		{
			return InQuiryOrderFlowDA.Instance.GetInQuiryOrderFlow(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InQuiryOrderFlowEntity> GetInQuiryOrderFlowList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return InQuiryOrderFlowDA.Instance.GetInQuiryOrderFlowList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetInQuiryOrderFlowAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="InQuiryOrderFlowListKey";// SysCacheKey.InQuiryOrderFlowListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<InQuiryOrderFlowEntity> list = null;
                    list = InQuiryOrderFlowDA.Instance.GetInQuiryOrderFlowAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InQuiryOrderFlowEntity inQuiryOrderFlow)
        {
            return InQuiryOrderFlowDA.Instance.ExistNum(inQuiryOrderFlow)>0;
        }
		
	}
}

