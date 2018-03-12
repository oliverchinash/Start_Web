using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.MemberDB;

/*****************************************
功能描述：表ConfirmOrderProduct的业务逻辑层。
创建时间：2017/10/12 14:15:20
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表ConfirmOrderProduct的业务逻辑层。
	/// </summary>
	public class ConfirmOrderProductBLL
	{
	    #region 实例化
        public static ConfirmOrderProductBLL Instance
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
            internal static readonly ConfirmOrderProductBLL instance = new ConfirmOrderProductBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ConfirmOrderProduct，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="confirmOrderProduct">要添加的ConfirmOrderProduct数据实体对象</param>
		public   int AddConfirmOrderProduct(ConfirmOrderProductEntity confirmOrderProduct)
		{
			  if (confirmOrderProduct.Id > 0)
            {
                return UpdateConfirmOrderProduct(confirmOrderProduct);
            }
				  else if (string.IsNullOrEmpty(confirmOrderProduct.ClassesName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(confirmOrderProduct.ProductName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(confirmOrderProduct.ProductUnitName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ConfirmOrderProductBLL.Instance.IsExist(confirmOrderProduct))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ConfirmOrderProductDA.Instance.AddConfirmOrderProduct(confirmOrderProduct);
            }
	 	}

		/// <summary>
		/// 更新一条ConfirmOrderProduct记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="confirmOrderProduct">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateConfirmOrderProduct(ConfirmOrderProductEntity confirmOrderProduct)
		{
			return ConfirmOrderProductDA.Instance.UpdateConfirmOrderProduct(confirmOrderProduct);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteConfirmOrderProductByKey(int id)
        {
            return ConfirmOrderProductDA.Instance.DeleteConfirmOrderProductByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfirmOrderProductDisabled()
        {
            return ConfirmOrderProductDA.Instance.DeleteConfirmOrderProductDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfirmOrderProductByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ConfirmOrderProductDA.Instance.DeleteConfirmOrderProductByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfirmOrderProductByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ConfirmOrderProductDA.Instance.DisableConfirmOrderProductByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ConfirmOrderProduct实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ConfirmOrderProduct实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ConfirmOrderProductEntity GetConfirmOrderProduct(int id)
		{
			return ConfirmOrderProductDA.Instance.GetConfirmOrderProduct(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ConfirmOrderProductEntity> GetConfirmOrderProductList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ConfirmOrderProductDA.Instance.GetConfirmOrderProductList(pageSize, pageIndex, ref recordCount);
        }
		/// <summary>
        /// 获取订单产品列表
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cache"></param>
        /// <returns></returns>
		public IList<ConfirmOrderProductEntity> GetConfirmProductAllByCode(string code,int cgmemid,bool cache=true)
        {
            IList<ConfirmOrderProductEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetConfirmProductAllByCode_"+ code+"_"+ cgmemid;// SysCacheKey.ConfirmOrderProductListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = ConfirmOrderProductDA.Instance.GetConfirmProductAllByCode(code, cgmemid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<ConfirmOrderProductEntity>)obj; 
                }
            }
            else
            {
                list = ConfirmOrderProductDA.Instance.GetConfirmProductAllByCode(code, cgmemid);

            }
            return list;

        }

        public IList<VWConfirmOrderCGMemEntity> GetConfirmCGMemsByCode(string code, bool cache = true)
        {
            IList<VWConfirmOrderCGMemEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetConfirmCGMemsByCode_" + code;// SysCacheKey.ConfirmOrderProductListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = ConfirmOrderProductDA.Instance.GetConfirmCGMemsByCode(code);
                    if (list != null && list.Count > 0)
                    {
                        foreach (VWConfirmOrderCGMemEntity cgmem in list)
                        {
                            cgmem.member = MemberBLL.Instance.GetVWMember(cgmem.CGMemId);
                        }
                    }
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<VWConfirmOrderCGMemEntity>)obj;
                }
            }
            else
            {
                list = ConfirmOrderProductDA.Instance.GetConfirmCGMemsByCode(code);
                if (list != null && list.Count > 0)
                {
                    foreach (VWConfirmOrderCGMemEntity cgmem in list)
                    {
                        cgmem.member = MemberBLL.Instance.GetVWMember(cgmem.CGMemId);
                    }
                }
            }
            return list;

        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ConfirmOrderProductEntity confirmOrderProduct)
        {
            return ConfirmOrderProductDA.Instance.ExistNum(confirmOrderProduct)>0;
        }
		
	}
}

