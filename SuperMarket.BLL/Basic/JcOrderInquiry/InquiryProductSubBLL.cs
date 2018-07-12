﻿using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表InquiryProductSub的业务逻辑层。
创建时间：2017/8/26 21:30:16
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InquiryProductSub的业务逻辑层。
	/// </summary>
	public class InquiryProductSubBLL
	{
	    #region 实例化
        public static InquiryProductSubBLL Instance
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
            internal static readonly InquiryProductSubBLL instance = new InquiryProductSubBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表InquiryProductSub，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryProductSub">要添加的InquiryProductSub数据实体对象</param>
		public   int AddInquiryProductSub(InquiryProductSubEntity inquiryProductSub)
		{
		return InquiryProductSubDA.Instance.AddInquiryProductSub(inquiryProductSub);
         
	 	}
        public int CreateBindProductType(string ordercode,int proid,string productstypes)
        {
            return InquiryProductSubDA.Instance.CreateBindProductType(ordercode, proid ,productstypes);
        }
        /// <summary>
        /// 更新一条InquiryProductSub记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="inquiryProductSub">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateInquiryProductSub(InquiryProductSubEntity inquiryProductSub)
		{
			return InquiryProductSubDA.Instance.UpdateInquiryProductSub(inquiryProductSub);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryProductSubByKey(int id)
        {
            return InquiryProductSubDA.Instance.DeleteInquiryProductSubByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryProductSubDisabled()
        {
            return InquiryProductSubDA.Instance.DeleteInquiryProductSubDisabled();
        }
        /// <summary>
        /// 用户删除产品子条目
        /// </summary>
        /// <param name="code"></param>
        /// <param name="productid"></param>
        /// <param name="protypeid"></param>
        /// <returns></returns>
        public int DeleteInquiryProductSubByPro(string code,int productid,int protypeid)
        {
            return InquiryProductSubDA.Instance.DeleteInquiryProductSubByPro(code,   productid,   protypeid);
        }
        /// <summary>
        /// 供应商删除产品子条目
        /// </summary>
        /// <param name="code"></param>
        /// <param name="productid"></param>
        /// <param name="protypeid"></param>
        /// <returns></returns>
        public int DeleteInquiryProductSubByCG(string code, int productid, int protypeid,int cgmemid)
        {
            return InquiryProductSubDA.Instance.DeleteInquiryProductSubByCG(code, productid, protypeid, cgmemid);
        }
        
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryProductSubByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryProductSubDA.Instance.DeleteInquiryProductSubByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryProductSubByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryProductSubDA.Instance.DisableInquiryProductSubByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InquiryProductSub实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InquiryProductSub实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InquiryProductSubEntity GetInquiryProductSub(int id)
		{
			return InquiryProductSubDA.Instance.GetInquiryProductSub(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InquiryProductSubEntity> GetInquiryProductSubList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return InquiryProductSubDA.Instance.GetInquiryProductSubList(pageSize, pageIndex, ref recordCount);
        }
        public IList<InquiryProductSubEntity> GetInquiryProductSubAll(string ordercode,int proid, bool cache = false)
        {
            IList<InquiryProductSubEntity> list = null;

            if (cache)
            {
                string _cachekey = "GetInquiryProductSubAll_"+ ordercode +"_"+ proid;// SysCacheKey.InquiryProductSubListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = InquiryProductSubDA.Instance.GetInquiryProductSubAll(ordercode, proid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<InquiryProductSubEntity>)obj;
                }
            }
            else
            {
                list = InquiryProductSubDA.Instance.GetInquiryProductSubAll(ordercode, proid);

            }
            return list;

        }
     
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryProductSubEntity inquiryProductSub)
        {
            return InquiryProductSubDA.Instance.ExistNum(inquiryProductSub)>0;
        }
		
	}
}

