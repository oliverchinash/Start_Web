using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProCertifacate的业务逻辑层。
创建时间：2017/1/5 14:30:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProCertifacate的业务逻辑层。
	/// </summary>
	public class CGProCertifacateBLL
	{
	    #region 实例化
        public static CGProCertifacateBLL Instance
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
            internal static readonly CGProCertifacateBLL instance = new CGProCertifacateBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProCertifacate，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGProCertifacate">要添加的CGProCertifacate数据实体对象</param>
		public   int AddCGProCertifacate(CGProCertifacateEntity cGProCertifacate)
		{
			  if (cGProCertifacate.Id > 0)
            {
                return UpdateCGProCertifacate(cGProCertifacate);
            }
				  else if (string.IsNullOrEmpty(cGProCertifacate.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CGProCertifacateBLL.Instance.IsExist(cGProCertifacate))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProCertifacateDA.Instance.AddCGProCertifacate(cGProCertifacate);
            }
	 	}

		/// <summary>
		/// 更新一条CGProCertifacate记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGProCertifacate">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGProCertifacate(CGProCertifacateEntity cGProCertifacate)
		{
			return CGProCertifacateDA.Instance.UpdateCGProCertifacate(cGProCertifacate);
		}
        public int UpdateProductLicense(int cgmemid,string paths)
        {
            return CGProCertifacateDA.Instance.UpdateProductLicense(cgmemid, paths);

        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProCertifacateByKey(int id)
        {
            return CGProCertifacateDA.Instance.DeleteCGProCertifacateByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProCertifacateDisabled()
        {
            return CGProCertifacateDA.Instance.DeleteCGProCertifacateDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProCertifacateByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProCertifacateDA.Instance.DeleteCGProCertifacateByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProCertifacateByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProCertifacateDA.Instance.DisableCGProCertifacateByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGProCertifacate实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProCertifacate实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProCertifacateEntity GetCGProCertifacate(int id)
		{
			return CGProCertifacateDA.Instance.GetCGProCertifacate(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGProCertifacateEntity> GetCGProCertifacateList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGProCertifacateDA.Instance.GetCGProCertifacateList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<CGProCertifacateEntity> GetCGProCertifacateAll(int memid)
        {
              IList<CGProCertifacateEntity> list = null;
                    list = CGProCertifacateDA.Instance.GetCGProCertifacateAll(memid);
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProCertifacateEntity cGProCertifacate)
        {
            return CGProCertifacateDA.Instance.ExistNum(cGProCertifacate)>0;
        }
		
	}
}

