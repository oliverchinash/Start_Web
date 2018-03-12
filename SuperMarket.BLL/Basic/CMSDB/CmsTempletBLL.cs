using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表CmsTemplet的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL
{

    /// <summary>
    /// 表CmsTemplet的业务逻辑层。
    /// </summary>
    public class CmsTempletBLL
    {
        #region 实例化
        public static CmsTempletBLL Instance
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
            internal static readonly CmsTempletBLL instance = new CmsTempletBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表CmsTemplet，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cmsTemplet">要添加的CmsTemplet数据实体对象</param>
        public int AddCmsTemplet(CmsTempletEntity cmsTemplet)
        {
            if (cmsTemplet.Id > 0)
            {
                return UpdateCmsTemplet(cmsTemplet);
            }

            else if (IsExist(cmsTemplet))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CmsTempletDA.Instance.AddCmsTemplet(cmsTemplet);
            }
        }

        public int NewAddCmsTemplet(CmsTempletEntity cmsTemplet)
        {
            return CmsTempletDA.Instance.AddCmsTemplet(cmsTemplet);
        }

        /// <summary>
        /// 更新一条CmsTemplet记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cmsTemplet">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateCmsTemplet(CmsTempletEntity cmsTemplet)
        {
            return CmsTempletDA.Instance.UpdateCmsTemplet(cmsTemplet);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCmsTempletByKey(int id)
        {
            return CmsTempletDA.Instance.DeleteCmsTempletByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCmsTempletDisabled()
        {
            return CmsTempletDA.Instance.DeleteCmsTempletDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCmsTempletByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CmsTempletDA.Instance.DeleteCmsTempletByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCmsTempletByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CmsTempletDA.Instance.DisableCmsTempletByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个CmsTemplet实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CmsTemplet实体</returns>
        /// <param name="columns">要返回的列</param>
        public CmsTempletEntity GetCmsTemplet(int id)
        {
            return CmsTempletDA.Instance.GetCmsTemplet(id);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CmsTempletEntity> GetCmsTempletList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _searchkey = "";
            int _cmstype = 0;
            int _isactive = -1;
            string _sortKeyWord = "";
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.SeachDefault:
                            {
                                _searchkey = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.CmsType:
                            {
                                _cmstype = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.IsActive:
                            {
                                _isactive = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.SortKeyWord:
                            {
                                _sortKeyWord = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                    }
                }
            }

            return CmsTempletDA.Instance.GetCmsTempletList(pageSize, pageIndex, ref recordCount, _searchkey, _cmstype, _isactive, _sortKeyWord);
        }

        public async Task GetCmsTempletAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "CmsTempletListKey";// SysCacheKey.CmsTempletListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CmsTempletEntity> list = null;
                    list = CmsTempletDA.Instance.GetCmsTempletAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }

        public IList<CmsTempletEntity> GetCmsTempletAll(int i=0)
        {
            return CmsTempletDA.Instance.GetCmsTempletAll(); 
        }

        public IList<CmsTempletEntity> NewGetCmsTempletAll()
        {
            return CmsTempletDA.Instance.GetCmsTempletAll();
        }

        public IList<CmsTempletEntity> GetCmsTempletListByType(int cmstype)
        {
            return CmsTempletDA.Instance.GetCmsTempletListByType(cmstype);
        }

        public CmsTempletEntity GetCmsTempletByTitle(string title)
        {
            return CmsTempletDA.Instance.GetCmsTempletByTitle(title);
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CmsTempletEntity cmsTemplet)
        {
            return CmsTempletDA.Instance.ExistNum(cmsTemplet) > 0;
        }




    }
}

