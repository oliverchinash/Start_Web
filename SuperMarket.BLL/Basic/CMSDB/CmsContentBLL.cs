using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表CmsContent的业务逻辑层。
创建时间：2016/9/1 15:35:32
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL
{

    /// <summary>
    /// 表CmsContent的业务逻辑层。
    /// </summary>
    public class CmsContentBLL
    {
        #region 实例化
        public static CmsContentBLL Instance
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
            internal static readonly CmsContentBLL instance = new CmsContentBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表CmsContent，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cmsContent">要添加的CmsContent数据实体对象</param>
        public int AddCmsContent(CmsContentEntity cmsContent)
        {
            if (cmsContent.Id > 0)
            {
                return UpdateCmsContent(cmsContent);
            }
            else
            {
                return CmsContentDA.Instance.AddCmsContent(cmsContent);
            }
        }

        /// <summary>
        /// 更新一条CmsContent记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cmsContent">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateCmsContent(CmsContentEntity cmsContent)
        {
            return CmsContentDA.Instance.UpdateCmsContent(cmsContent);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCmsContentByKey(int id)
        {
            return CmsContentDA.Instance.DeleteCmsContentByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCmsContentDisabled()
        {
            return CmsContentDA.Instance.DeleteCmsContentDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCmsContentByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CmsContentDA.Instance.DeleteCmsContentByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCmsContentByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CmsContentDA.Instance.DisableCmsContentByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个CmsContent实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CmsContent实体</returns>
        /// <param name="columns">要返回的列</param>
        public CmsContentEntity GetCmsContent(int id)
        {
            return CmsContentDA.Instance.GetCmsContent(id);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CmsContentEntity> GetCmsContentList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int cid = 0;
            string title =string.Empty;
            int ctype = 0;
            int status = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit _whereentity in wherelist)
                {
                    if (_whereentity.FieldName == "CId")
                    {
                        cid = StringUtils.GetDbInt(_whereentity.CompareValue);
                    }
                    else if (_whereentity.FieldName == "Title")
                    {
                        title = StringUtils.GetDbString(_whereentity.CompareValue);
                    }
                    else if (_whereentity.FieldName == "CType")
                    {
                        ctype = StringUtils.GetDbInt(_whereentity.CompareValue);
                    }
                    else if (_whereentity.FieldName == "Status")
                    {
                        status = StringUtils.GetDbInt(_whereentity.CompareValue);
                    }
                }
            }
            return CmsContentDA.Instance.GetCmsContentList(pageSize, pageIndex, ref recordCount, cid, title, ctype, status);
        }

        public async Task GetCmsContentAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "CmsContentListKey";// SysCacheKey.CmsContentListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CmsContentEntity> list = null;
                    list = CmsContentDA.Instance.GetCmsContentAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }

        public IList<CmsContentEntity> GetCmsContentAll(int i=0)
        {
            return CmsContentDA.Instance.GetCmsContentAll();
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CmsContentEntity cmsContent)
        {
            return CmsContentDA.Instance.ExistNum(cmsContent) > 0;
        }

    }
}

