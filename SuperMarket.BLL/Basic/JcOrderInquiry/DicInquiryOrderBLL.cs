using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表DicInquiryOrder的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表DicInquiryOrder的业务逻辑层。
	/// </summary>
	public class DicInquiryOrderBLL
	{
	    #region 实例化
        public static DicInquiryOrderBLL Instance
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
            internal static readonly DicInquiryOrderBLL instance = new DicInquiryOrderBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表DicInquiryOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="dicInquiryOrder">要添加的DicInquiryOrder数据实体对象</param>
		public   int AddDicInquiryOrder(DicInquiryOrderEntity dicInquiryOrder)
		{
			  if (dicInquiryOrder.Id > 0)
            {
                return UpdateDicInquiryOrder(dicInquiryOrder);
            }
				  else if (string.IsNullOrEmpty(dicInquiryOrder.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (DicInquiryOrderBLL.Instance.IsExist(dicInquiryOrder))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return DicInquiryOrderDA.Instance.AddDicInquiryOrder(dicInquiryOrder);
            }
	 	}

		/// <summary>
		/// 更新一条DicInquiryOrder记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="dicInquiryOrder">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateDicInquiryOrder(DicInquiryOrderEntity dicInquiryOrder)
		{
			return DicInquiryOrderDA.Instance.UpdateDicInquiryOrder(dicInquiryOrder);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDicInquiryOrderByKey(int id)
        {
            return DicInquiryOrderDA.Instance.DeleteDicInquiryOrderByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicInquiryOrderDisabled()
        {
            return DicInquiryOrderDA.Instance.DeleteDicInquiryOrderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicInquiryOrderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return DicInquiryOrderDA.Instance.DeleteDicInquiryOrderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicInquiryOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return DicInquiryOrderDA.Instance.DisableDicInquiryOrderByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个DicInquiryOrder实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>DicInquiryOrder实体</returns>
		/// <param name="columns">要返回的列</param>
		public   DicInquiryOrderEntity GetDicInquiryOrder(int id)
		{
			return DicInquiryOrderDA.Instance.GetDicInquiryOrder(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<DicInquiryOrderEntity> GetDicInquiryOrderList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return DicInquiryOrderDA.Instance.GetDicInquiryOrderList(pageSize, pageIndex, ref recordCount);
        }
        public IList<DicInquiryOrderEntity> GetDicInquiryOrderShow(int parentid,int classid, bool cache = true)
        {
            IList<DicInquiryOrderEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetDicInquiryOrderShow_" + parentid+"_"+ classid; 
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                { 
                    list = DicInquiryOrderDA.Instance.GetDicInquiryOrderShow(parentid, classid);
                    MemCache.AddCache(_cachekey, list);
                }
            }
            else
            {
                list = DicInquiryOrderDA.Instance.GetDicInquiryOrderShow(parentid, classid);
            }
            return list;
        }
        public IList<DicInquiryOrderEntity> GetDicFromCode(string parentcode,string menucode, bool cache = true)
        {
            IList<DicInquiryOrderEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetDicFromCode_" + parentcode + "_" + menucode;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = DicInquiryOrderDA.Instance.GetDicFromCode(parentcode, menucode);
                    MemCache.AddCache(_cachekey, list);
                }
            }
            else
            {
                list = DicInquiryOrderDA.Instance.GetDicFromCode(parentcode, menucode);
            }
            return list;
        }
        public IList<DicInquiryOrderEntity> GetInquiryDicAllByMenuCode(string menucode,bool cache=false)
        {
            IList<DicInquiryOrderEntity> list = null;
        if(cache)
            {
                string _cachekey = "GetInquiryDicAllByMenuCode"+ menucode;// SysCacheKey.DicInquiryOrderListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                   
                    list = DicInquiryOrderDA.Instance.GetInquiryDicAllByMenuCode(menucode);
                    MemCache.AddCache(_cachekey, list);
                } 
            }
            else
            {
                list = DicInquiryOrderDA.Instance.GetInquiryDicAllByMenuCode(menucode);
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(DicInquiryOrderEntity dicInquiryOrder)
        {
            return DicInquiryOrderDA.Instance.ExistNum(dicInquiryOrder)>0;
        }
		
	}
}

