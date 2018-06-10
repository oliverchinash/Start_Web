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

/*****************************************
功能描述：表CGOrderOffer的业务逻辑层。
创建时间：2016/12/31 9:41:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{

    /// <summary>
    /// 表CGOrderOffer的业务逻辑层。
    /// </summary>
    public class CGOrderOfferBLL
    {
        #region 实例化
        public static CGOrderOfferBLL Instance
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
            internal static readonly CGOrderOfferBLL instance = new CGOrderOfferBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表CGOrderOffer，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrderOffer">要添加的CGOrderOffer数据实体对象</param>
        public int AddCGOrderOffer(CGOrderOfferEntity cGOrderOffer)
        {
            if (cGOrderOffer.Id > 0)
            {
                return UpdateCGOrderOffer(cGOrderOffer);
            }
            //else if (CGOrderOfferBLL.Instance.IsExist(cGOrderOffer))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return CGOrderOfferDA.Instance.AddCGOrderOffer(cGOrderOffer);
            }
        }

        /// <summary>
        /// 更新一条CGOrderOffer记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrderOffer">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateCGOrderOffer(CGOrderOfferEntity cGOrderOffer)
        {
            return CGOrderOfferDA.Instance.UpdateCGOrderOffer(cGOrderOffer);
        }


        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public CGOrderOfferEntity GetCGOrderOfferByMemidAndCode(int memid, long code)
        {
            return CGOrderOfferDA.Instance.GetCGOrderOfferByMemidAndCode(memid, code);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderOfferByKey(int id)
        {
            return CGOrderOfferDA.Instance.DeleteCGOrderOfferByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderOfferDisabled()
        {
            return CGOrderOfferDA.Instance.DeleteCGOrderOfferDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderOfferByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderOfferDA.Instance.DeleteCGOrderOfferByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderOfferByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderOfferDA.Instance.DisableCGOrderOfferByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个CGOrderOffer实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGOrderOffer实体</returns>
        /// <param name="columns">要返回的列</param>
        public CGOrderOfferEntity GetCGOrderOffer(int id)
        {
            return CGOrderOfferDA.Instance.GetCGOrderOffer(id);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGOrderOfferEntity> GetCGOrderOfferList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _status = -1;
            int _memid = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (var item in wherelist)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.Status:
                            {
                                _status = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                        case SearchFieldName.MemId:
                            {
                                _memid = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                    }
                }
            }

            return CGOrderOfferDA.Instance.GetCGOrderOfferList(pageSize, pageIndex, ref recordCount, _status, _memid);
        }

        public async Task GetCGOrderOfferAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "CGOrderOfferListKey";// SysCacheKey.CGOrderOfferListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGOrderOfferEntity> list = null;
                    list = CGOrderOfferDA.Instance.GetCGOrderOfferAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderOfferEntity cGOrderOffer)
        {
            return CGOrderOfferDA.Instance.ExistNum(cGOrderOffer) > 0;
        }

    }
}

