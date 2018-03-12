using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using SuperMarket.BLL.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表CGOrderTake的业务逻辑层。
创建时间：2016/12/31 9:41:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{

    /// <summary>
    /// 表CGOrderTake的业务逻辑层。
    /// </summary>
    public class CGOrderTakeBLL
    {
        #region 实例化
        public static CGOrderTakeBLL Instance
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
            internal static readonly CGOrderTakeBLL instance = new CGOrderTakeBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表CGOrderTake，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrderTake">要添加的CGOrderTake数据实体对象</param>
        public int AddCGOrderTake(CGOrderTakeEntity cGOrderTake)
        {
            if (cGOrderTake.Id > 0)
            {
                return UpdateCGOrderTake(cGOrderTake);
            }

            //else if (CGOrderTakeBLL.Instance.IsExist(cGOrderTake))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return CGOrderTakeDA.Instance.AddCGOrderTake(cGOrderTake);
            }
	 	}
        /// <summary>
        /// 供应商发货
        /// </summary>
        /// <param name="cagoucode"></param>
        /// <param name="memid"></param>
        /// <param name="wlcode"></param>
        /// <param name="wlcom"></param>
        /// <param name="dename"></param>
        /// <returns></returns>
        public int SendCGOrderTake(long cagoucode, int memid, string wlcode, string wlcom,string dename,string mobile)
        {
            return CGOrderTakeDA.Instance.SendCGOrderTake(cagoucode,   memid,   wlcode,   wlcom,   dename, mobile);

        }
        public int OrderRecivedVoucher(long cgordercode,int memid)
        {
            return CGOrderTakeDA.Instance.OrderRecivedVoucher(cgordercode,memid);

        }
        public int OrderRejectVoucher(long cgordercode, int memid)
        {
            return CGOrderTakeDA.Instance.OrderRejectVoucher(cgordercode, memid);

        }
        
        /// <summary>
        /// 更新一条CGOrderTake记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrderTake">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateCGOrderTake(CGOrderTakeEntity cGOrderTake)
		{
			return CGOrderTakeDA.Instance.UpdateCGOrderTake(cGOrderTake);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderTakeByKey(int id)
        {
            return CGOrderTakeDA.Instance.DeleteCGOrderTakeByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderTakeDisabled()
        {
            return CGOrderTakeDA.Instance.DeleteCGOrderTakeDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderTakeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderTakeDA.Instance.DeleteCGOrderTakeByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderTakeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderTakeDA.Instance.DisableCGOrderTakeByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个CGOrderTake实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGOrderTake实体</returns>
        /// <param name="columns">要返回的列</param>
        public CGOrderTakeEntity GetCGOrderTake(int id)
        {
            return CGOrderTakeDA.Instance.GetCGOrderTake(id);
        }

        /// <summary>
        /// 根据主键获取一个CGOrderTake实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGOrderTake实体</returns>
        /// <param name="columns">要返回的列</param>
        public CGOrderTakeEntity GetCGOrderTakeByCode(long code,int cgmemid)
        {
            return CGOrderTakeDA.Instance.GetCGOrderTakeByCode(code, cgmemid);
        }
      
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGOrderTakeEntity> GetCGOrderTakeList(int pageSize, int pageIndex, ref  int recordCount,string key,int memid)
        {
            return CGOrderTakeDA.Instance.GetCGOrderTakeList(pageSize, pageIndex, ref recordCount, key,   memid);
        }



        public IList<VWCGOrderReturnEntity> GetCGOrderSuppliers(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _pid = 0;
            long _orderCode = 0;

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (var item in wherelist)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.B2BOrderCode:
                            {
                                _orderCode = StringUtils.GetDbLong(item.CompareValue);
                                break;
                            }
                        case SearchFieldName.ProductId:
                            {
                                _pid = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                    }
                }
            }

            IList<VWCGOrderReturnEntity> entityList = CGOrderTakeDA.Instance.GetCGOrderTakeList(pageSize, pageIndex, ref recordCount, _pid, _orderCode);

            return entityList;
        }

        public async Task GetCGOrderTakeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "CGOrderTakeListKey";// SysCacheKey.CGOrderTakeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGOrderTakeEntity> list = null;
                    list = CGOrderTakeDA.Instance.GetCGOrderTakeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderTakeEntity cGOrderTake)
        {
            return CGOrderTakeDA.Instance.ExistNum(cGOrderTake) > 0;
        }

    }
}

