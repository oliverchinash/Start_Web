using System;
using System.Data; 
using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表InquiryProduct的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InquiryProduct的业务逻辑层。
	/// </summary>
	public class InquiryProductBLL
	{
	    #region 实例化
        public static InquiryProductBLL Instance
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
            internal static readonly InquiryProductBLL instance = new InquiryProductBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表InquiryProduct，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="inquiryProduct">要添加的InquiryProduct数据实体对象</param>
        public int AddInquiryProduct(InquiryProductEntity inquiryProduct)
        {
            return InquiryProductDA.Instance.AddInquiryProduct(inquiryProduct); 
        }
        
        /// <summary>
        /// 更新一条InquiryProduct记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="inquiryProduct">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateInquiryProduct(InquiryProductEntity inquiryProduct)
		{
			return InquiryProductDA.Instance.UpdateInquiryProduct(inquiryProduct);
		}
        /// <summary>
        /// 用户提交配件信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="proid"></param>
        /// <param name="pronum"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int UpdateInquiryProduct(string code,int proid,int pronum,string unitname, string remark)
        {
            return InquiryProductDA.Instance.UpdateInquiryProduct(code,   proid,   pronum, unitname,  remark);
        }
        /// <summary>
        /// 供应商提交配件信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="proid"></param>
        /// <param name="pronum"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int UpdateInquiryProductByCG(string code, int proid, int pronum,string _unitname, string remark,int cgmemid)
        {
            return InquiryProductDA.Instance.UpdateInquiryProductByCG(code, proid, pronum, _unitname, remark, cgmemid);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryProductByKey(int id)
        {
            return InquiryProductDA.Instance.DeleteInquiryProductByKey(id);
        }
        /// <summary>
        /// 用户删除订单配件
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="proid"></param>
        /// <returns></returns>
        public int DelProductByProId(string ordercode,int proid)
        {
            return InquiryProductDA.Instance.DelProductByProId(ordercode, proid);
        }
        /// <summary>
        /// 供应商删除订单配件
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="proid"></param>
        /// <returns></returns>
        public int DelProductByProIdCG(string ordercode, int proid,int cgmemid)
        {
            return InquiryProductDA.Instance.DelProductByProIdCG(ordercode, proid,cgmemid);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryProductDisabled()
        {
            return InquiryProductDA.Instance.DeleteInquiryProductDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryProductByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryProductDA.Instance.DeleteInquiryProductByIds(idstr); 
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryProductById(string ordercode,int proid)
        {  
            return InquiryProductDA.Instance.DeleteInquiryProductById(ordercode, proid);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryProductByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryProductDA.Instance.DisableInquiryProductByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InquiryProduct实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InquiryProduct实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InquiryProductEntity GetInquiryProduct(int id)
		{
			return InquiryProductDA.Instance.GetInquiryProduct(id);			
		}
        public VWInquiryProductEntity GetVWInquiryProduct(int id,bool cache)
        {
            VWInquiryProductEntity vwproduct= InquiryProductDA.Instance.GetVWInquiryProduct(id);
            if(vwproduct!=null&& vwproduct.Id>0)
            {
                vwproduct.ProductSubList= InquiryProductSubBLL.Instance.GetInquiryProductSubAll(vwproduct.InquiryOrderCode, vwproduct.ProductId, false);
            }
            return vwproduct;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InquiryProductEntity> GetInquiryProductList(int pageSize, int pageIndex, ref  int recordCount )
        {
            return InquiryProductDA.Instance.GetInquiryProductList(pageSize, pageIndex, ref recordCount);
        }

        public IList<InquiryProductEntity> GetInquiryProductAll(string ordercode,bool cache=false)
        {
            IList<InquiryProductEntity> list = null;
            if(cache)
            {
                string _cachekey = "GetInquiryProductAll" + ordercode;// SysCacheKey.InquiryProductSubListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = InquiryProductDA.Instance.GetInquiryProductAll(ordercode);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<InquiryProductEntity>)obj;
                }
            }
            else { list = InquiryProductDA.Instance.GetInquiryProductAll(ordercode); }
                   
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryProductEntity inquiryProduct)
        {
            return InquiryProductDA.Instance.ExistNum(inquiryProduct)>0;
        }
		
	}
}

