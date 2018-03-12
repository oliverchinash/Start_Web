using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.HelpDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表IssueContent的业务逻辑层。
创建时间：2016/10/8 13:48:17
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.HelpDB
{

    /// <summary>
    /// 表IssueContent的业务逻辑层。
    /// </summary>
    public class IssueContentBLL
    {
        #region 实例化
        public static IssueContentBLL Instance
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
            internal static readonly IssueContentBLL instance = new IssueContentBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表IssueContent，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="issueContent">要添加的IssueContent数据实体对象</param>
        public int AddIssueContent(IssueContentEntity issueContent)
        {
            if (issueContent.Id > 0)
            {
                return UpdateIssueContent(issueContent);
            }

            else if (IssueContentBLL.Instance.IsExist(issueContent))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return IssueContentDA.Instance.AddIssueContent(issueContent);
            }
        }

        /// <summary>
        /// 更新一条IssueContent记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="issueContent">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateIssueContent(IssueContentEntity issueContent)
        {
            return IssueContentDA.Instance.UpdateIssueContent(issueContent);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteIssueContentByKey(int id)
        {
            return IssueContentDA.Instance.DeleteIssueContentByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIssueContentDisabled()
        {
            return IssueContentDA.Instance.DeleteIssueContentDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIssueContentByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return IssueContentDA.Instance.DeleteIssueContentByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIssueContentByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return IssueContentDA.Instance.DisableIssueContentByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个IssueContent实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>IssueContent实体</returns>
        /// <param name="columns">要返回的列</param>
        public IssueContentEntity GetIssueContent(int id)
        {
            return IssueContentDA.Instance.GetIssueContent(id);
        }
        public IssueContentEntity GetIssueContentByName(string title)
        {
            return IssueContentDA.Instance.GetIssueContentByName(title);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<IssueContentEntity> GetIssueContentList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _id = 0;
            string _issuetitle = string.Empty;
            int _isactive = -1;
            int _classid = 0;
            int _systype = -1;

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.Id:
                            {
                                _id = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.IssueTitle:
                            {
                                _issuetitle = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.IsActive:
                            {
                                _isactive = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.ClassId:
                            {
                                _classid = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.SystemType:
                            {
                                _systype = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                    }
                }
            }
            return IssueContentDA.Instance.GetIssueContentList(pageSize, pageIndex, ref recordCount, _id, _issuetitle, _isactive,_classid, _systype);
        }

        public async Task GetIssueContentAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "IssueContentListKey";// SysCacheKey.IssueContentListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<IssueContentEntity> list = null;
                    list = IssueContentDA.Instance.GetIssueContentAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(IssueContentEntity issueContent)
        {
            return IssueContentDA.Instance.ExistNum(issueContent) > 0;
        }

    }
}

