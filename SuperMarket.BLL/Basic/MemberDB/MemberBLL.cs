using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Account;
using SuperMarket.BLL.SysDB;

/*****************************************
功能描述：表Member的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表Member的业务逻辑层。
	/// </summary>
	public class MemberBLL
	{
	    #region 实例化
        public static MemberBLL Instance
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
            internal static readonly MemberBLL instance = new MemberBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表Member，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="member">要添加的Member数据实体对象</param>
		public   int AddMember(MemberEntity member)
		{
			  return MemberDA.Instance.AddMember(member);
	 	}

		/// <summary>
		/// 更新一条Member记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="member">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMember(MemberEntity member)
		{
			return MemberDA.Instance.UpdateMember(member);
		}
        public int BindMemWeChat(int memid,string wechatunionid,string timestramp)
        {
            return MemberDA.Instance.BindMemWeChat(memid,   wechatunionid,   timestramp);
        }
        public int BindMemWeChatRelease(int memid )
        {
            return MemberDA.Instance.BindMemWeChatRelease(memid );
        }
        /// <summary>
        /// 企业用户基础信息修改， 影响到状态的信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int StoreBasicMsgUpdate(VWMemberEntity member)
        {
			return MemberDA.Instance.StoreBasicMsgUpdate(member);
        }
        /// <summary>
        /// 个人用户基础信息修改，影响到状态的信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int MemberBasicMsgUpdate(VWMemberEntity member)
        {
            return MemberDA.Instance.MemberBasicMsgUpdate(member);
        }
        /// <summary>
        /// 基础信息修改，不影响到状态的信息 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public int BasicMsgUpdate(VWMemberEntity member)
        {
            return MemberDA.Instance.BasicMsgUpdate(member);
        }
        public int RegisterCompanyProc(VWMemberEntity entity)
        {
            return MemberDA.Instance.RegisterCompanyProc(entity);
        }
        public int RegisterProc( MemberEntity mem,MemberInfoEntity meminfo,MemStoreEntity store)
        {
            return MemberDA.Instance.RegisterProc(mem, meminfo, store);
        }
        public int RegisterCompanyLicense(int memid,string path) {
            return MemberDA.Instance.RegisterCompanyLicense(memid, path);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int UpdateMemberSatus(int id,int status)
        {
            return MemberDA.Instance.UpdateMemberSatus(id,status);
        }

        /// <summary>
        /// 修改最后登录时间
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public int UpdateLastLoginDate(MemLoginLogEntity _log)
        {
            return MemberDA.Instance.UpdateLastLoginDate(_log);
        }
        
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemberByKey(int id)
        {
            return MemberDA.Instance.DeleteMemberByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemberDisabled()
        {
            return MemberDA.Instance.DeleteMemberDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemberByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemberDA.Instance.DeleteMemberByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemberByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemberDA.Instance.DisableMemberByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个Member实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Member实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemberEntity GetMember(int id)
		{
			return MemberDA.Instance.GetMember(id);			
		}
        public VWMemberEntity GetVWMember(int memid)
        {

            VWMemberEntity entity= MemberDA.Instance.GetVWMember(memid);
            if (entity != null)
            {
              
                if (entity.CompanyCityId > 0)
                {
                    entity.CompanyCityName = GYCityBLL.Instance.GetGYCityByCode(entity.CompanyCityId.ToString()).Name;
                }
            }
            return entity;
        }
        public VWMemberEntity GetVWMemberByPhone(string phone)
        {
            return MemberDA.Instance.GetVWMemberByPhone(phone);
        }
        public VWMemberEntity GetVWMemberByWeChat(string wechatunioncode)
        {
            return MemberDA.Instance.GetVWMemberByWeChat(wechatunioncode);
        }
        public bool RegisterPhoneExist(string phone)
        {
            return MemberDA.Instance.RegisterPhoneExist(phone); 
        }
        public bool MemPhoneExist(string phone)
        {
            return MemberDA.Instance.MemPhoneExist(phone);
        }
        /// <summary>
		/// 根据主键获取一个Member实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Member实体</returns>
		/// <param name="columns">要返回的列</param>
		public MemberEntity GetMemberByCode(string  code)
        {
            return MemberDA.Instance.GetMemberByCode(code);
        }
        /// <summary>
        /// 获取所有对应角色下的成员
        /// </summary>
        /// <param name="rolecode"></param>
        /// <returns></returns>
        public IList<MemberEntity> GetMemByRoleCode(string rolecode)
        {
            return MemberDA.Instance.GetMemByRoleCode(rolecode);
        }
        public IList<VWMemberEntity> GetMemByAuthCode(string authcode)
        {
            return MemberDA.Instance.GetMemByAuthCode(authcode);
        }
        public MemberLoginEntity GetLoginMemByCode(string code)
        {
            return MemberDA.Instance.GetLoginMemByCode(code);
        }
        public MemberLoginEntity GetLoginMemByMethod(string code, LoginMethodEnum method)
        {
            return MemberDA.Instance.GetLoginMemByMethod(code, method);
        }
        public MemberLoginEntity GetLoginMemByPhone(string phonecode)
        {
            return MemberDA.Instance.GetLoginMemByPhone(phonecode);
        }

        public MemberEntity GetMemByMobile(string mobile)
        {
            return MemberDA.Instance.GetMemByMobile(mobile);
        }
      
        public MemberEntity GetMemByMethod(string code, LoginMethodEnum method)
        {
            return MemberDA.Instance.GetMemByMethod(code, method);

        }
        public int CheckPhoneIsExist(string mobile)
        {
            return MemberDA.Instance.CheckPhoneIsExist(mobile);
        }
        #region 用户权限
        public Dictionary<string,string> GetAuthByMemId(int memid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string _cachekey = "GetAuthByMemId_"+ memid;// SysCacheKey.MemRoleListKey;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                dic= MemberDA.Instance.GetAuthByMemId(memid);
                MemCache.AddCache(_cachekey, dic);
            }
            else
            {
                dic = (Dictionary<string, string>)obj;
            }
            return dic;
        }

        public int UpdateRolesToMemId(  int _memid,string roleids)
        {
            int raid = MemRoleRelateDA.Instance.AddRolesToMem(_memid, roleids);
            if(raid>0)
            {
                MemCache.RemoveCache("GetAuthByMemId_" + _memid);
            }
            return raid;
        }

        #endregion
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemberEntity> GetMemberList(int pageSize, int pageIndex, ref  int recordCount,string _memCode, int  Active, string _companyName, string _mobilePhone,int issupplier)
        {
            return MemberDA.Instance.GetMemberList(pageSize, pageIndex, ref recordCount,_memCode, Active, _companyName, _mobilePhone, issupplier);
        }
        public IList<VWMemberEntity> GetVWMemberList(int pageSize, int pageIndex, ref int recordCount, string _memCode, int status, string _companyName, string _mobilePhone, int issupplier,int isstore,int issys,string memname,string brandname)
        {
            return MemberDA.Instance.GetVWMemberList(pageSize, pageIndex, ref recordCount, _memCode, status, _companyName, _mobilePhone, issupplier, isstore, issys, memname,brandname);
        }
     
        /// <summary>
        /// 取得所有的用户
        /// </summary>
        /// <param name="contactname"></param>
        /// <param name="contactphone"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public IList<VWMemAutoTemplete> GetVWMemAutoTemplete(string contactname,string contactphone,string companyname)
        { 
            IList<VWMemAutoTemplete> list = null;
            list = MemberDA.Instance.GetVWMemAutoTemplete(contactname,   contactphone,   companyname);
            return list;
        }
        /// <summary>
        /// 取得分页用户数据,只有几个关键数据
        /// </summary>
        /// <param name="contactname"></param>
        /// <param name="contactphone"></param>
        /// <param name="companyname"></param>
        /// <returns></returns>
        public IList<VWMemAutoTemplete> GetMemBasicInfoList(int pageSize, int pageIndex, ref int recordCount, string contactname, string contactphone, string companyname,int issys,int issupplier,int isstore)
        {
            IList<VWMemAutoTemplete> list = null;
            list = MemberDA.Instance.GetMemBasicInfoList(  pageSize,   pageIndex, ref   recordCount,   contactname,   contactphone,   companyname,   issys,   issupplier,   isstore);
            return list;
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemberEntity member)
        {
            return MemberDA.Instance.ExistNum(member)>0;
        }
        public bool MemberCodeExist(string membercode)
        {
            return MemberDA.Instance.MemberCodeExist(membercode)  ;
        } 
        /// <summary>
        /// 审核注册
        /// </summary>
        /// <param name="memId"></param>
        /// <param name="memberStatus"></param>
        /// <param name="storeId"></param>
        /// <param name="storeStatus"></param>
        /// <param name="billId"></param>
        /// <param name="billStatus"></param>
        /// <returns></returns>
        public int CheckSupplier(int memId,int memberStatus,int storeId,int storeStatus,int billId,int billStatus,int checkmanid)
        {
            return MemberDA.Instance.CheckSupplier(memId, memberStatus, storeId, storeStatus, billId, billStatus, checkmanid);
        }


        public MemberEntity GetLoginMemByMobile(string mobilePhone)
        {
            return MemberDA.Instance.GetLoginMemByMobile(mobilePhone);
        }
    }
}

