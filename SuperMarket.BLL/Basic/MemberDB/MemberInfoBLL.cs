using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Basic.VW.Member;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表MemberInfo的业务逻辑层。
创建时间：2016/8/8 15:27:48
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{

    /// <summary>
    /// 表MemberInfo的业务逻辑层。
    /// </summary>
    public class MemberInfoBLL
    {
        #region 实例化
        public static MemberInfoBLL Instance
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
            internal static readonly MemberInfoBLL instance = new MemberInfoBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表MemberInfo，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="memberInfo">要添加的MemberInfo数据实体对象</param>
        public int AddMemberInfo(MemberInfoEntity memberInfo)
        {
            if (memberInfo.Id > 0)
            {
                return UpdateMemberInfo(memberInfo);
            } 
            else
            {
                return MemberInfoDA.Instance.AddMemberInfo(memberInfo);
            }
        }

        /// <summary>
        /// 更新一条MemberInfo记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="memberInfo">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateMemberInfo(MemberInfoEntity memberInfo)
        {
            if (memberInfo.Id == 0)
            {
                return AddMemberInfo(memberInfo);
            }
            return MemberInfoDA.Instance.UpdateMemberInfo(memberInfo);
        }
        public int UpdateShowHead(int memid, string picpath)
        {
            return MemberInfoDA.Instance.UpdateShowHead(memid, picpath);
        }


        /// <summary>
        /// 获取用于审核个人用户信息的视图
        /// </summary>
        /// <returns></returns>
        public IList<VWMemberEntity2> GetVWMemberInfo(int pageIndex,int pageSize,ref int recordCount,IList<ConditionUnit> whereList)
        {
            string _memCode = string.Empty;
            int _status = -1;

            if (whereList != null && whereList.Count > 0)
            {
                foreach (var item in whereList)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.MemCode:
                            {
                                _memCode = StringUtils.GetDbString(item.CompareValue);
                            }
                            break;
                        case SearchFieldName.CheckStatus:
                            {
                                _status = StringUtils.GetDbInt(item.CompareValue);
                            }
                            break;
                    }
 
                }
            }

            return MemberInfoDA.Instance.GetVWMemberInfo(pageIndex, pageSize, ref recordCount, _memCode,_status);
        }


        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemberInfoByKey(int id)
        {
            return MemberInfoDA.Instance.DeleteMemberInfoByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemberInfoDisabled()
        {
            return MemberInfoDA.Instance.DeleteMemberInfoDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemberInfoByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemberInfoDA.Instance.DeleteMemberInfoByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemberInfoByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemberInfoDA.Instance.DisableMemberInfoByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个MemberInfo实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>MemberInfo实体</returns>
        /// <param name="columns">要返回的列</param>
        public MemberInfoEntity GetMemberInfo(int id)
        {
            return MemberInfoDA.Instance.GetMemberInfo(id);
        }
        /// <summary>
		/// 根据主键获取一个MemberInfo实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemberInfo实体</returns>
		/// <param name="columns">要返回的列</param>
		public MemberInfoEntity GetMemberInfoByMemId(int memid)
        {
            return MemberInfoDA.Instance.GetMemberInfoByMemId(memid);
        }
        /// <summary>
        /// 根据主键获取一个MemberInfo实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>MemberInfo实体</returns>
        /// <param name="columns">要返回的列</param>
        public VWMemberEntity GetVWMemberInfoByMemId(int memid)
        {
            return MemberInfoDA.Instance.GetVWMemberInfoByMemId(memid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemberInfoEntity> GetMemberInfoList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            return MemberInfoDA.Instance.GetMemberInfoList(pageSize, pageIndex, ref recordCount);
        }
     public string   GetNickNameByMemId(int memid)
        {
            string nickname = "";
            string _cachekey = "GetNickNameByMemId"+ memid;// SysCacheKey.MemberInfoListKey;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                MemberInfoEntity  entity = null;
                entity = MemberInfoDA.Instance.GetMemberInfoByMemId(memid);
                nickname = entity.Nickname;
                MemCache.AddCache(_cachekey, nickname);
            }
            else
            {
                nickname = obj.ToString();
            }
            return nickname;
        }
        public async Task GetMemberInfoAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "MemberInfoListKey";// SysCacheKey.MemberInfoListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemberInfoEntity> list = null;
                    list = MemberInfoDA.Instance.GetMemberInfoAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemberInfoEntity memberInfo)
        {
            return MemberInfoDA.Instance.ExistNum(memberInfo) > 0;
        }

    }
}

