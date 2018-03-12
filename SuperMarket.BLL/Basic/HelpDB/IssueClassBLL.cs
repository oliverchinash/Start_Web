using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.HelpDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表IssueClass的业务逻辑层。
创建时间：2016/10/8 13:48:17
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.HelpDB
{
	  
	/// <summary>
	/// 表IssueClass的业务逻辑层。
	/// </summary>
	public class IssueClassBLL
	{
	    #region 实例化
        public static IssueClassBLL Instance
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
            internal static readonly IssueClassBLL instance = new IssueClassBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表IssueClass，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="issueClass">要添加的IssueClass数据实体对象</param>
		public   int AddIssueClass(IssueClassEntity issueClass)
		{
			  if (issueClass.Id > 0)
            {
                return UpdateIssueClass(issueClass);
            }
				  else if (string.IsNullOrEmpty(issueClass.ClassName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (IssueClassBLL.Instance.IsExist(issueClass))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return IssueClassDA.Instance.AddIssueClass(issueClass);
            }
	 	}

		/// <summary>
		/// 更新一条IssueClass记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="issueClass">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateIssueClass(IssueClassEntity issueClass)
		{
			return IssueClassDA.Instance.UpdateIssueClass(issueClass);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteIssueClassByKey(int id)
        {
            return IssueClassDA.Instance.DeleteIssueClassByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIssueClassDisabled()
        {
            return IssueClassDA.Instance.DeleteIssueClassDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIssueClassByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return IssueClassDA.Instance.DeleteIssueClassByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIssueClassByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return IssueClassDA.Instance.DisableIssueClassByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个IssueClass实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>IssueClass实体</returns>
		/// <param name="columns">要返回的列</param>
		public   IssueClassEntity GetIssueClass(int id)
		{
			return IssueClassDA.Instance.GetIssueClass(id);			
		}
        /// <summary>
		/// 根据主键获取一个IssueClass实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>IssueClass实体</returns>
		/// <param name="columns">要返回的列</param>
		public IssueClassEntity GetIssueClassByPid(int pid)
        {
            return IssueClassDA.Instance.GetIssueClassByPid(pid);
        }

        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<IssueClassEntity> GetIssueClassList(int pageSize, int pageIndex, ref  int recordCount,int systype=1)
        {
            return IssueClassDA.Instance.GetIssueClassList(pageSize, pageIndex, ref recordCount, systype);
        }
		
		public async Task GetIssueClassAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="IssueClassListKey";// SysCacheKey.IssueClassListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<IssueClassEntity> list = null;
                    list = IssueClassDA.Instance.GetIssueClassAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }

        public IList<IssueClassEntity> GetIssueClassAllNoCache()
        {
            return IssueClassDA.Instance.GetIssueClassAll();
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(IssueClassEntity issueClass)
        {
            return IssueClassDA.Instance.ExistNum(issueClass)>0;
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public IList<IssueClassEntity> GetIssueClassByParentId(int parentid)
        {
            return IssueClassDA.Instance.GetIssueClassByParentId(parentid);
       }
    }
}

