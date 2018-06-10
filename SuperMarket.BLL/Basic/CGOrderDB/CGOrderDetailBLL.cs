using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;
using SuperMarket.BLL.CatograyDB;

/*****************************************
功能描述：表CGOrderDetail的业务逻辑层。
创建时间：2016/12/31 9:41:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{
	  
	/// <summary>
	/// 表CGOrderDetail的业务逻辑层。
	/// </summary>
	public class CGOrderDetailBLL
	{
	    #region 实例化
        public static CGOrderDetailBLL Instance
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
            internal static readonly CGOrderDetailBLL instance = new CGOrderDetailBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGOrderDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrderDetail">要添加的CGOrderDetail数据实体对象</param>
		public   int AddCGOrderDetail(CGOrderDetailEntity cGOrderDetail)
		{
			  if (cGOrderDetail.Id > 0)
            {
                return UpdateCGOrderDetail(cGOrderDetail);
            }
				  else if (string.IsNullOrEmpty(cGOrderDetail.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (CGOrderDetailBLL.Instance.IsExist(cGOrderDetail))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGOrderDetailDA.Instance.AddCGOrderDetail(cGOrderDetail);
            }
	 	}

		/// <summary>
		/// 更新一条CGOrderDetail记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrderDetail">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGOrderDetail(CGOrderDetailEntity cGOrderDetail)
		{
			return CGOrderDetailDA.Instance.UpdateCGOrderDetail(cGOrderDetail);
		}

        
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderDetailByKey(int id)
        {
            return CGOrderDetailDA.Instance.DeleteCGOrderDetailByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderDetailDisabled()
        {
            return CGOrderDetailDA.Instance.DeleteCGOrderDetailDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderDetailByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGOrderDetailDA.Instance.DeleteCGOrderDetailByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderDetailByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderDetailDA.Instance.DisableCGOrderDetailByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGOrderDetail实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGOrderDetail实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGOrderDetailEntity GetCGOrderDetail(int id)
		{
			return CGOrderDetailDA.Instance.GetCGOrderDetail(id);			
		}

        /// <summary>
        /// 根据主键获取一个CGOrderDetail实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGOrderDetail实体</returns>
        /// <param name="columns">要返回的列</param>
        public IList<CGOrderDetailEntity> GetCGOrderDetailListByOrderCode(long code)
        {
            IList<CGOrderDetailEntity> list= CGOrderDetailDA.Instance.GetCGOrderDetailListByOrderCode(code);
            foreach(CGOrderDetailEntity entity in list)
            {
                if(entity.Unit>0)
                { 
                    entity.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(entity.Unit).Name;
                }
                else
                { 
                    entity.UnitName ="件";
                }
            }
            return list;
        }

        /// <summary>
		/// 根据主键获取一个CGOrderDetail实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGOrderDetail实体</returns>
		/// <param name="columns">要返回的列</param>
		public IList<CGOrderDetailEntity> GetCGOrderDetailByCode(long code)
        {
            return CGOrderDetailDA.Instance.GetCGOrderDetailByCode(code);
        }

        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGOrderDetailEntity> GetCGOrderDetailList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            string _productName = string.Empty;
            long _orderCode = 0;

            if (wherelist!=null&&wherelist.Count>0)
            {
                foreach (var item in wherelist)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.ProductName:
                            {
                                _productName = StringUtils.GetDbString(item.CompareValue);
                                break;
                            }

                        case SearchFieldName.OrderCode:
                            {
                                _orderCode = StringUtils.GetDbLong(item.CompareValue);
                                break;
                            }

                    }
                }
            }

            return CGOrderDetailDA.Instance.GetCGOrderDetailList(pageSize, pageIndex, ref recordCount,_productName,_orderCode);
        }
		
		public IList<CGOrderDetailEntity> GetCGOrderDetailAllByCode(long code)
        { 
            IList<CGOrderDetailEntity> list =CGOrderDetailDA.Instance.GetCGOrderDetailByCode(code);
            foreach (CGOrderDetailEntity entity in list)
            {
                if (entity.Unit > 0)
                {
                    entity.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(entity.Unit).Name;
                }
                else
                {
                    entity.UnitName = "件";
                }
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderDetailEntity cGOrderDetail)
        {
            return CGOrderDetailDA.Instance.ExistNum(cGOrderDetail)>0;
        }
		
	}
}

