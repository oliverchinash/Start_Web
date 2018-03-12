using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGOrderVoucher的业务逻辑层。
创建时间：2017/1/20 23:29:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{
	  
	/// <summary>
	/// 表CGOrderVoucher的业务逻辑层。
	/// </summary>
	public class CGOrderVoucherBLL
	{
	    #region 实例化
        public static CGOrderVoucherBLL Instance
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
            internal static readonly CGOrderVoucherBLL instance = new CGOrderVoucherBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGOrderVoucher，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrderVoucher">要添加的CGOrderVoucher数据实体对象</param>
		public   int AddCGOrderVoucher(CGOrderVoucherEntity cGOrderVoucher)
		{
			  if (cGOrderVoucher.Id > 0)
            {
                return UpdateCGOrderVoucher(cGOrderVoucher);
            }
          
            else if (CGOrderVoucherBLL.Instance.IsExist(cGOrderVoucher))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGOrderVoucherDA.Instance.AddCGOrderVoucher(cGOrderVoucher);
            }
	 	}

		/// <summary>
		/// 更新一条CGOrderVoucher记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrderVoucher">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGOrderVoucher(CGOrderVoucherEntity cGOrderVoucher)
		{
			return CGOrderVoucherDA.Instance.UpdateCGOrderVoucher(cGOrderVoucher);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderVoucherByKey(int id)
        {
            return CGOrderVoucherDA.Instance.DeleteCGOrderVoucherByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderVoucherDisabled()
        {
            return CGOrderVoucherDA.Instance.DeleteCGOrderVoucherDisabled();
        }
        public int DelCGOrderVoucher(int memid, long code, int vid)
        {
            return CGOrderVoucherDA.Instance.DelCGOrderVoucher(memid, code, vid);

        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderVoucherByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGOrderVoucherDA.Instance.DeleteCGOrderVoucherByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderVoucherByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderVoucherDA.Instance.DisableCGOrderVoucherByIds(idstr);
        }
        /// <summary>
        /// 上传收货凭证
        /// </summary>
        /// <param name="cgmemid"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public int UpLoadCGOrderVoucher(int cgmemid, long cgordercode,string paths,string delstrs,string remark)
        {
            return CGOrderVoucherDA.Instance.UpLoadCGOrderVoucher(cgmemid, cgordercode, paths, delstrs, remark);

        }
        /// <summary>
        /// 根据主键获取一个CGOrderVoucher实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGOrderVoucher实体</returns>
        /// <param name="columns">要返回的列</param>
        public   CGOrderVoucherEntity GetCGOrderVoucher(int id)
		{
			return CGOrderVoucherDA.Instance.GetCGOrderVoucher(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGOrderVoucherEntity> GetCGOrderVoucherList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGOrderVoucherDA.Instance.GetCGOrderVoucherList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<CGOrderVoucherEntity> GetCGOrderVoucherAll(long CODE)
        {
          
                    IList<CGOrderVoucherEntity> list = null;
                    list = CGOrderVoucherDA.Instance.GetCGOrderVoucherAll(CODE);
            return list;
        }

		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderVoucherEntity cGOrderVoucher)
        {
            return CGOrderVoucherDA.Instance.ExistNum(cGOrderVoucher)>0;
        }
		
	}
}

