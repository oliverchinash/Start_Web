using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ReturnXS的业务逻辑层。
创建时间：2016/9/22 11:23:23
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{

    /// <summary>
    /// 表ReturnXS的业务逻辑层。
    /// </summary>
    public class ReturnXSBLL
    {
        #region 实例化
        public static ReturnXSBLL Instance
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
            internal static readonly ReturnXSBLL instance = new ReturnXSBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表ReturnXS，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="returnXS">要添加的ReturnXS数据实体对象</param>
        public int AddReturnXS(ReturnXSEntity returnXS)
        {
            if (returnXS.Id > 0)
            {
                return UpdateReturnXS(returnXS);
            }
            else if (string.IsNullOrEmpty(returnXS.ProductName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }

            //else if (ReturnXSBLL.Instance.IsExist(returnXS))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return ReturnXSDA.Instance.AddReturnXS(returnXS);
            }
        }

        /// <summary>
        /// 更新一条ReturnXS记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="returnXS">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateReturnXS(ReturnXSEntity returnXS)
        {
            return ReturnXSDA.Instance.UpdateReturnXS(returnXS);
        }

        /// <summary>
        /// 更新一条ReturnXS记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="returnXS">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateReturnXSStatus(int id, int status,int oldstatus=-1)
        {
            return ReturnXSDA.Instance.UpdateReturnXSStatus(id, status, oldstatus);
        }

        public int UpdateReturnXSNewOrderCode(int id, string newOrderCode,int status)
        {
            return ReturnXSDA.Instance.UpdateReturnXSNewOrderCode(id, newOrderCode,status);
        }

        /// <summary>
        /// 更新一条ReturnXS记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="returnXS">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateReturnXSByReturnOrderCode(long returnOrderCode)
        {
            return ReturnXSDA.Instance.UpdateReturnXSByReturnOrderCode(returnOrderCode);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteReturnXSByKey(int id)
        {
            return ReturnXSDA.Instance.DeleteReturnXSByKey(id);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int ReturnGoods(int memid, long orderCode, int orderDetailId, int returnNum, int _returntype, string _returnreason, string _acceptname, string _acceptphone, int _provinceid, int _cityid, string _getaddress, int receiveType)
        {
            return ReturnXSDA.Instance.ReturnGoods(memid, orderCode, orderDetailId, returnNum, _returntype, _returnreason, _acceptname, _acceptphone, _provinceid, _cityid, _getaddress, receiveType);
        }

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteReturnXSDisabled()
        {
            return ReturnXSDA.Instance.DeleteReturnXSDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteReturnXSByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ReturnXSDA.Instance.DeleteReturnXSByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableReturnXSByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ReturnXSDA.Instance.DisableReturnXSByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个ReturnXS实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ReturnXS实体</returns>
        /// <param name="columns">要返回的列</param>
        public ReturnXSEntity GetReturnXS(int id, int memid)
        {
            return ReturnXSDA.Instance.GetReturnXS(id, memid);
        }
        /// <summary>
        /// 判断退货数量是否等于分配数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ReturnNumEqDist(int id)
        {
            return ReturnXSDA.Instance.ReturnNumEqDist(id );

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ReturnXSEntity> GetReturnXSList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _keyword = string.Empty;
            int _status = -1;
            int _term = -1;
            int _returntype = 0;
            int _memid = 0; 

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.SeachDefault:
                            {
                                _keyword = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.ReturnOrderStatus:
                            {
                                _status = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.ReturnOrderTerm:
                            {
                                _term = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.ReturnType:
                            {
                                _returntype = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.MemId:
                            {
                                _memid = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                    }

                }
            }
            return ReturnXSDA.Instance.GetReturnXSList(pageSize, pageIndex, ref recordCount, _memid, _returntype, _keyword, _term, _status );
        }


        public async Task GetReturnXSAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "ReturnXSListKey";// SysCacheKey.ReturnXSListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ReturnXSEntity> list = null;
                    list = ReturnXSDA.Instance.GetReturnXSAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ReturnXSEntity returnXS)
        {
            return ReturnXSDA.Instance.ExistNum(returnXS) > 0;
        }



    }
}

