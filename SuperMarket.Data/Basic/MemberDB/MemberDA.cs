using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;
using SuperMarket.Model.Account;

/*****************************************
功能描述：Member表的数据访问类。
创建时间：2016/8/9 10:45:15
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
    /// <summary>
    /// MemberEntity的数据访问操作
    /// </summary>
    public partial class MemberDA : BaseSuperMarketDB
    {
        #region 实例化
        public static MemberDA Instance
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
            internal static readonly MemberDA instance = new MemberDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生


        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemberByKey(int id)
        {
            string sql = @"delete from Member where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemberDisabled()
        {
            string sql = @"delete from  Member  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemberByIds(string ids)
        {
            string sql = @"Delete from Member  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemberByIds(string ids)
        {
            string sql = @"Update   Member set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public MemberEntity GetMember(int id)
        {
            string sql = @"SELECT  [TimeStampTab],[Id],[MemCode],MemNikeName,[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],[IsSupplier],[IsSysUser],[Status],[MemGrade],[RecommendCode]
							FROM
							dbo.[Member] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            MemberEntity entity = new MemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.MemNikeName = StringUtils.GetDbString(reader["MemNikeName"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]); 
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.TimeStampTab =  ByteUtils.GetStringFromByte(reader["TimeStampTab"]);
                }
            }
            return entity;
        }
        public VWMemberEntity GetVWMember(int memid)
        {
            string sql = @"SELECT   a.[Id] as MemId,[MemCode],a.[Email],a.[WeChat],a.[QQ],a.[MobilePhone],
a.[PassWord],a.MemNikeName, [LoginNum], [IsStore],a.[Status],
[MemGrade],[RecommendCode],a.TimeStampTab  from				dbo.[Member] a WITH(NOLOCK) 
 WHERE a.[Id]=@MemId
 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            VWMemberEntity entity = new VWMemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]); 
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);  
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);  
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.MemNikeName = StringUtils.GetDbString(reader["MemNikeName"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);  
                }
            }
            return entity;
        }

        public VWMemberEntity GetVWMemberByPhone(string phone)
        {
            string sql = @"SELECT   a.[Id] as MemId,[MemCode],a.[Email],a.[WeChat],a.[QQ],a.[MobilePhone],
a.[PassWord],a.MemNikeName, [LoginNum], [IsStore],a.[Status],
[MemGrade],[RecommendCode],a.TimeStampTab  from				dbo.[Member] a WITH(NOLOCK)  
 WHERE a.[MobilePhone]=@MobilePhone
 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MobilePhone", DbType.String, phone);
            VWMemberEntity entity = new VWMemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.MemNikeName = StringUtils.GetDbString(reader["MemNikeName"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }
            return entity;
        }
        public VWMemberEntity GetVWMemberByWeChat(string wechatcode)
        {
            string sql = @"SELECT   a.[Id] as MemId,[MemCode],a.[Email],a.[WeChat],a.[QQ],a.[MobilePhone],
a.[PassWord],a.MemNikeName, [LoginNum], [IsStore],a.[Status],
[MemGrade],[RecommendCode],a.TimeStampTab  from				dbo.[Member] a WITH(NOLOCK) 
 WHERE a.[WeChat]=@WeChat
 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@WeChat", DbType.String, wechatcode);
            VWMemberEntity entity = new VWMemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.MemNikeName = StringUtils.GetDbString(reader["MemNikeName"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);

                }
            }
            return entity;
        }

        /// <summary>
        /// 查是否已注册手机号
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool RegisterPhoneExist(string phone)
        { 
            string sql  = @"Select count(1) from dbo.[Member] with (nolock) where MobilePhone=@MobilePhone";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, phone);
            return StringUtils.GetDbInt(db.ExecuteScalar(cmd)) > 0; 
        }
        /// <summary>
        /// 查是否有会员的手机号，审核通过的
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public bool MemPhoneExist(string phone)
        {
            string sql = @"Select count(1) from dbo.[Member] with (nolock) where MobilePhone=@MobilePhone and Status=1 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, phone);
            return StringUtils.GetDbInt(db.ExecuteScalar(cmd)) > 0;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<MemberEntity> GetMemberList(int pagesize, int pageindex, ref int recordCount, string _memCode, int active, string _companyName, string _mobilePhone, int issupplier)
        {
            string where = " where 1=1 ";
            if(!string.IsNullOrEmpty(_memCode))
            {
                where += " and MemCode like @MemCode ";
            }
            if (active!=-1)
            {
                where += " and Status= @Status ";
            }
            if (!string.IsNullOrEmpty(_mobilePhone))
            {
                where += " and MobilePhone like @MobilePhone ";
            }
            if (issupplier!=-1)
            {
                where += " and IsSupplier=@IsSupplier ";
            }
            string sql = @"SELECT   [Id],[MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],[Status],[MemGrade],[RecommendCode]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],[Status],[MemGrade],[RecommendCode] from dbo.[Member] WITH(NOLOCK)	
						"+ where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Member] with (nolock) " + where ;
            IList<MemberEntity> entityList = new List<MemberEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(_memCode))
            { 
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%"+ _memCode + "%");
            }
            if (active != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, active);
            }
            if (!string.IsNullOrEmpty(_mobilePhone))
            { 
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + _mobilePhone + "%");
            }
            if (issupplier != -1)
            { 
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, issupplier);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemberEntity entity = new MemberEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(_memCode))
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%" + _memCode + "%");
            }
            if (active != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, active);
            }
            if (!string.IsNullOrEmpty(_mobilePhone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + _mobilePhone + "%");
            }
            if (issupplier != -1)
            {
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, issupplier);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    recordCount = StringUtils.GetDbInt(reader[0]);
                }
                else
                {
                    recordCount = 0;
                }
            }
            return entityList;
        }
        public IList<VWMemberEntity> GetVWMemberList(int pagesize, int pageindex, ref int recordCount, string _memCode, int status, string _companyName, string _mobilePhone, int issupplier, int isstore, int issys,string memname,string brandname)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(_memCode))
            {
                where += " and a.MemCode like @MemCode ";
            }
            if (status != -1)
            {
                where += " and a.Status= @Status ";
            }
            if (!string.IsNullOrEmpty(_companyName))
            {
                where += " and b.CompanyName like @CompanyName ";
            } 
            if (!string.IsNullOrEmpty(_mobilePhone))
            {
                where += " and a.MobilePhone like @MobilePhone ";
            }
            if (issupplier != -1)
            {
                where += " and a.IsSupplier=@IsSupplier ";
            }
            if (isstore != -1)
            {
                where += " and a.IsStore=@IsStore ";
            }
            if (issys != -1)
            {
                where += " and a.IsSysUser=@IsSysUser ";
            }
            string brandstr = "";
            if (!string.IsNullOrEmpty(brandname))
            {
                brandstr += " INNER  JOIN dbo.MemCGScope memscope  WITH(NOLOCK) ON a.id=memscope.CGMemId ";
                where += " and  memscope.BrandName LIKE @BrandName ";
            }
            string sql = @"SELECT  MemId,  StoreId,[MemCode],[Email],[QQ],[WeChat], [MobilePhone], [CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore], [Status],[MemGrade],[RecommendCode] 
				,[StoreName]  ,[CompanyName],[LegalName],[LegalMobilePhone], [ContactsManName]
      ,[Country],[ProvinceId], [StoreType],[CityId],[District],[Address],  [GradeLevel],TimeStampTab
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id] AS MemId,b.Id AS StoreId,[MemCode],[Email],[QQ],[WeChat],a.[MobilePhone] ,a.[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],a.[Status],[MemGrade],[RecommendCode] 
				,[StoreName]  ,[CompanyName],[LegalName],[LegalMobilePhone], [ContactsManName]
      ,[Country],[ProvinceId],b.[StoreType],[CityId],[District],[Address],  [GradeLevel],a.TimeStampTab 	from dbo.[Member] a WITH(NOLOCK) INNER JOIN dbo.MemStore b ON a.id=b.MemId  "+ brandstr+ where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Member]   a WITH(NOLOCK) INNER JOIN dbo.MemStore b ON a.id=b.MemId " + brandstr   + where;
            IList<VWMemberEntity> entityList = new List<VWMemberEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(_memCode))
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%" + _memCode + "%");
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (!string.IsNullOrEmpty(_companyName))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + _companyName + "%");
              }
            if (!string.IsNullOrEmpty(_mobilePhone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + _mobilePhone + "%");
            }
            if (issupplier != -1)
            {
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, issupplier);
            }
            if (isstore != -1)
            {
                db.AddInParameter(cmd, "@IsStore", DbType.Int32, isstore); 
            }
            if (issys != -1)
            {
                db.AddInParameter(cmd, "@IsSysUser", DbType.Int32, issys); 
            }
            if (!string.IsNullOrEmpty(brandname))
            {
                db.AddInParameter(cmd, "@BrandName", DbType.String, brandname); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemberEntity entity = new VWMemberEntity();
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]); 
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]); 
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["Address"]);
                    entity.CompanyCityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CompanyType = StringUtils.GetDbInt(reader["StoreType"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.ContactsMobile = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.TimeStampTab = ByteUtils.GetStringFromByte(reader["TimeStampTab"]);

                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(_memCode))
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%" + _memCode + "%");
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (!string.IsNullOrEmpty(_companyName))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + _companyName + "%");
            }
            if (!string.IsNullOrEmpty(_mobilePhone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + _mobilePhone + "%");
            }
            if (issupplier != -1)
            {
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, issupplier);
            }
            if (isstore != -1)
            {
                db.AddInParameter(cmd, "@IsStore", DbType.Int32, isstore);
            }
            if (issys != -1)
            {
                db.AddInParameter(cmd, "@IsSysUser", DbType.Int32, issys);
            }
            if (!string.IsNullOrEmpty(brandname))
            {
                db.AddInParameter(cmd, "@BrandName", DbType.String, brandname);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    recordCount = StringUtils.GetDbInt(reader[0]);
                }
                else
                {
                    recordCount = 0;
                }
            }
            return entityList;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<VWMemAutoTemplete> GetVWMemAutoTemplete(string contactname, string contactphone, string companyname)
        {
            string where = " where a.Status=1  ";
            if (!string.IsNullOrEmpty(contactname))
            {
                where += " and b.ContactsManName like @ContactsManName ";
            }
            if (!string.IsNullOrEmpty(contactphone))
            {
                where += " and a.MobilePhone like @MobilePhone ";
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                where += " and b.CompanyName like @CompanyName ";
            }
            string sql = @"SELECT    A.[Id] AS MemId, A.[MobilePhone],b.CompanyName,b.Address,b.ContactsManName
from dbo.[Member] A WITH(NOLOCK) LEFT JOIN dbo.MemStore B WITH(NOLOCK) ON A.Id=B.MemId  "+ where;
            IList<VWMemAutoTemplete> entityList = new List<VWMemAutoTemplete>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(contactname))
            {
                db.AddInParameter(cmd, "@ContactsManName", DbType.String, "%" + contactname + "%");
            }
            if (!string.IsNullOrEmpty(contactphone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + contactphone + "%");
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + companyname + "%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemAutoTemplete entity = new VWMemAutoTemplete();
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["Address"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<VWMemAutoTemplete> GetMemBasicInfoList(int pagesize, int pageindex, ref int recordCount, string contactname, string contactphone, string companyname, int issys, int issupplier, int isstore)
        {
            string where = " where a.Status=1  ";
            if (!string.IsNullOrEmpty(contactname))
            {
                where += " and b.ContactsManName like @ContactsManName ";
            }
            if (!string.IsNullOrEmpty(contactphone))
            {
                where += " and a.MobilePhone like @MobilePhone ";
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                where += " and b.CompanyName like @CompanyName ";
            }
            if(issys!=-1)
            { 
                where += " and a.IsSysUser=@IsSysUser ";
            }
            if (issupplier != -1)
            {
                where += " and a.IsSupplier=@IsSupplier ";
            }
            if (isstore != -1)
            {
                where += " and a.IsStore=@IsStore ";
            }
            string sql = @"SELECT MemId,MobilePhone,CompanyName, Address, ContactsManName    FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,   A.[Id] AS MemId, A.[MobilePhone],b.CompanyName,b.Address,b.ContactsManName
from dbo.[Member] A WITH(NOLOCK) LEFT JOIN dbo.MemStore B WITH(NOLOCK) ON A.Id=B.MemId  " + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
            string sql2 = @"Select count(1) from dbo.[Member] a with (nolock) LEFT JOIN dbo.MemStore B WITH(NOLOCK) ON A.Id=B.MemId  " + where;
            IList<VWMemAutoTemplete> entityList = new List<VWMemAutoTemplete>();
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(contactname))
            {
                db.AddInParameter(cmd, "@ContactsManName", DbType.String, "%" + contactname + "%");
            }
            if (!string.IsNullOrEmpty(contactphone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + contactphone + "%");
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + companyname + "%");
            }
            if (issys != -1)
            {
                db.AddInParameter(cmd, "@IsSysUser", DbType.Int32, issys); 
            }
            if (issupplier != -1)
            { 
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, issupplier);
            }
            if (isstore != -1)
            {
                db.AddInParameter(cmd, "@IsStore", DbType.Int32, isstore); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemAutoTemplete entity = new VWMemAutoTemplete();
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["Address"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(contactname))
            {
                db.AddInParameter(cmd, "@ContactsManName", DbType.String, "%" + contactname + "%");
            }
            if (!string.IsNullOrEmpty(contactphone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + contactphone + "%");
            }
            if (!string.IsNullOrEmpty(companyname))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + companyname + "%");
            }
            if (issys != -1)
            {
                db.AddInParameter(cmd, "@IsSysUser", DbType.Int32, issys);
            }
            if (issupplier != -1)
            {
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, issupplier);
            }
            if (isstore != -1)
            {
                db.AddInParameter(cmd, "@IsStore", DbType.Int32, isstore);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    recordCount = StringUtils.GetDbInt(reader[0]);
                }
                else
                {
                    recordCount = 0;
                }
            }
            return entityList;
        }

        #endregion
        #endregion
        /// <summary>
        /// 插入一条记录到表Member，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="member">待插入的实体对象</param>
        public int AddMember(MemberEntity entity)
        {
            string sql = @"insert into Member(  [MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],IsSysUser,[IsSupplier],[Status],[MemGrade],[RecommendCode])VALUES
			            (  @MemCode,@Email,@QQ,@WeChat,@MobilePhone,@PassWord,@CreateTime,@LastLoginTime,@LoginNum,@CreateIp,@CreateClientType,@IsStore,@IsSysUser,@IsSupplier,@Status,@MemGrade,@RecommendCode);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemCode", DbType.String, entity.MemCode);
            db.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
            db.AddInParameter(cmd, "@QQ", DbType.String, entity.QQ);
            db.AddInParameter(cmd, "@WeChat", DbType.String, entity.WeChat);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, entity.MobilePhone);
            db.AddInParameter(cmd, "@PassWord", DbType.String, CryptMD5.Encrypt(entity.PassWord));
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@LastLoginTime", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@LoginNum", DbType.Int32, entity.LoginNum);
            db.AddInParameter(cmd, "@CreateIp", DbType.String, entity.CreateIp);
            db.AddInParameter(cmd, "@CreateClientType", DbType.Int32, entity.CreateClientType);
            db.AddInParameter(cmd, "@IsSysUser", DbType.Int32, entity.IsSysUser);
            db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, entity.IsSupplier);
            db.AddInParameter(cmd, "@IsStore", DbType.Int32, entity.IsStore);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@MemGrade", DbType.Int32, entity.MemGrade);
            db.AddInParameter(cmd, "@RecommendCode", DbType.String, entity.RecommendCode); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="member">待更新的实体对象</param>
        public int UpdateMember(MemberEntity entity)
        {
            string sql = @"  UPDATE dbo.[Member] SET [MemCode]=@MemCode,[Email]=@Email,[MemNikeName]=@MemNikeName,[QQ]=@QQ,[WeChat]=@WeChat,[MobilePhone]=@MobilePhone,[PassWord]=@PassWord, [LastLoginTime]=@LastLoginTime,[LoginNum]=@LoginNum, [IsStore]=@IsStore,[IsSupplier]=@IsSupplier,[IsSysUser]=@IsSysUser,[Status]=@Status,[MemGrade]=@MemGrade,[RecommendCode]=@RecommendCode
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@MemCode", DbType.String, entity.MemCode);
            db.AddInParameter(cmd, "@Email", DbType.String, entity.Email);
            db.AddInParameter(cmd, "@MemNikeName", DbType.String, entity.MemNikeName);
            db.AddInParameter(cmd, "@QQ", DbType.String, entity.QQ);
            db.AddInParameter(cmd, "@WeChat", DbType.String, entity.WeChat);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, entity.MobilePhone);
            db.AddInParameter(cmd, "@PassWord", DbType.String, entity.PassWord); 
            db.AddInParameter(cmd, "@LastLoginTime", DbType.DateTime, entity.LastLoginTime);
            db.AddInParameter(cmd, "@LoginNum", DbType.Int32, entity.LoginNum); 
            db.AddInParameter(cmd, "@IsStore", DbType.Int32, entity.IsStore);
            db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, entity.IsSupplier);
            db.AddInParameter(cmd, "@IsSysUser", DbType.Int32, entity.IsSysUser);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@MemGrade", DbType.Int32, entity.MemGrade);
            db.AddInParameter(cmd, "@RecommendCode", DbType.String, entity.RecommendCode); 

            return db.ExecuteNonQuery(cmd);
        }

        public int BindMemWeChat(int memid, string WeChatunionid, string timestramp)
        {
            string sql = @"  UPDATE dbo.[Member] SET  [WeChat]=@WeChat 
                       WHERE [Id]=@MemId and  TimeStampTab=@TimeStampTab ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@WeChat", DbType.String, WeChatunionid); 
            db.AddInParameter(cmd, "@TimeStampTab", DbType.Binary, ByteUtils.GetByteFromString(timestramp));
            return db.ExecuteNonQuery(cmd);
        }
        public int BindMemWeChatRelease(int memid)
        {
            string sql = @"  UPDATE dbo.[Member] SET  [WeChat]='' 
                       WHERE [Id]=@MemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);  
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <returns></returns>
        public int UpdateMemberSatus(int id, int status)
        {
            string sql = @" UPDATE DBO.[MEMBER] SET STATUS=@STATUS WHERE ID=@ID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, status);
            return db.ExecuteNonQuery(cmd);
        }

        public int StoreBasicMsgUpdate(VWMemberEntity member)
        {
            string sql = @"INSERT INTO [MemberInfoLog]
           ( [LogId]
           ,[MemId]
           ,[Nickname]
           ,[HeadPicUrl]
           ,[MemName]
           ,[IdentityNo]
           ,[IdentityNoCheck]
           ,[Sex]
           ,[Birthday]
           ,[Telephone]
           ,[MobilePhone]
           ,[MobilePhoneCheck]
           ,[Email]
           ,[EmailCheck]
           ,[QQ]
           ,[QQCheck]
           ,[WeChat]
            
           ,[IdentityPre]
           ,[IdentityBeh]
           ,[IdentityAutoName]
           ,[IdentityAutoNo]
           ,[UpdateReason]
           ,[UpdateTime]
           ,[UpdateManId])
   SELECT [Id]
           ,[MemId]
           ,[Nickname]
           ,[HeadPicUrl]
           ,[MemName]
           ,[IdentityNo]
           ,[IdentityNoCheck]
           ,[Sex]
           ,[Birthday]
           ,[Telephone]
           ,[MobilePhone]
           ,[MobilePhoneCheck]
           ,[Email]
           ,[EmailCheck]
           ,[QQ]
           ,[QQCheck]
           ,[WeChat] 
           ,[IdentityPre]
           ,[IdentityBeh]
           ,[IdentityAutoName]
           ,[IdentityAutoNo]
           ,'修改基础信息'
           ,getdate()
           ,@MemId FROM dbo.MemberInfo  WITH(NOLOCK)  WHERE MemId=@MemId 
INSERT INTO [JcMemberDB].[dbo].[StoreLog]
           ([LogId]
           ,[MemId]
           ,[StoreName]
           ,[CompanyName]
           ,[LegalName]
           ,[LegalMobilePhone]
           ,[LegalIdentityNo]
           ,[LegalIdentityPre]
           ,[LegalIdentityBeh]
           ,[MobilePhone]
           ,[ContactsManName]
           ,[Country]
           ,[ProvinceId]
           ,[StoreType]
           ,[CityId]
           ,[District]
           ,[Address]
           ,[Longitude]
           ,[Latitude]
           ,[LicenseNo]
           ,[LicensePath]
           ,[Status]
           ,[GradeLevel]
           ,[CreateTime]
           ,[CheckManId]
           ,[CheckTime]
           ,[UpdateReason]
           ,[UpdateTime]
           ,[UpdateManId])
    SELECT [Id]
           ,[MemId]
           ,[StoreName]
           ,[CompanyName]
           ,[LegalName]
           ,[LegalMobilePhone]
           ,[LegalIdentityNo]
           ,[LegalIdentityPre]
           ,[LegalIdentityBeh]
           ,[MobilePhone]
           ,[ContactsManName]
           ,[Country]
           ,[ProvinceId]
           ,[StoreType]
           ,[CityId]
           ,[District]
           ,[Address]
           ,[Longitude]
           ,[Latitude]
           ,[LicenseNo]
           ,[LicensePath]
           ,[Status]
           ,[GradeLevel]
           ,[CreateTime]
           ,[CheckManId]
           ,[CheckTime]
          ,'修改基础信息'
           ,getdate()
           ,@MemId FROM dbo.MemStore WITH(NOLOCK) WHERE MemId=@MemId
UPDATE dbo.[MemberInfo] SET [Status]=@NewStatus,MemName=@MemName,MobilePhone=@MobilePhone,Email=@Email
WHERE [MemId]=@MemId
update dbo.MemStore set CompanyName=@CompanyName,StoreType=@StoreType,ProvinceId=@ProvinceId,CityId=@CityId,Address=@Address WHERE [MemId]=@MemId
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, member.MemId);
            db.AddInParameter(cmd, "@NewStatus", DbType.Int32, (int)StoreStatus.WaitChecked);
            db.AddInParameter(cmd, "@MemName", DbType.String, member.ContactsManName);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, member.ContactsMobile);
            db.AddInParameter(cmd, "@Email", DbType.String, member.ContactsEmail);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, member.CompanyName);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, member.CompanyProvinceId);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, member.CompanyCityId);
            db.AddInParameter(cmd, "@Address", DbType.String, member.CompanyAddress);
            db.AddInParameter(cmd, "@StoreType", DbType.Int32, member.CompanyType);

            return db.ExecuteNonQuery(cmd);
        }

        public int MemberBasicMsgUpdate(VWMemberEntity member)
        {
            string sql = @"INSERT INTO [MemberInfoLog]
           ( [LogId]
           ,[MemId]
           ,[Nickname]
           ,[HeadPicUrl]
           ,[MemName]
           ,[IdentityNo]
           ,[IdentityNoCheck]
           ,[Sex]
           ,[Birthday]
           ,[Telephone]
           ,[MobilePhone]
           ,[MobilePhoneCheck]
           ,[Email]
           ,[EmailCheck]
           ,[QQ]
           ,[QQCheck]
           ,[WeChat] 
           ,[IdentityPre]
           ,[IdentityBeh]
           ,[IdentityAutoName]
           ,[IdentityAutoNo]
           ,[UpdateReason]
           ,[UpdateTime]
           ,[UpdateManId])
   SELECT [Id]
           ,[MemId]
           ,[Nickname]
           ,[HeadPicUrl]
           ,[MemName]
           ,[IdentityNo]
           ,[IdentityNoCheck]
           ,[Sex]
           ,[Birthday]
           ,[Telephone]
           ,[MobilePhone]
           ,[MobilePhoneCheck]
           ,[Email]
           ,[EmailCheck]
           ,[QQ]
           ,[QQCheck]
           ,[WeChat] 
           ,[IdentityPre]
           ,[IdentityBeh]
           ,[IdentityAutoName]
           ,[IdentityAutoNo]
           ,'修改基础信息'
           ,getdate()
           ,@MemId FROM dbo.MemberInfo  WITH(NOLOCK)  WHERE MemId=@MemId 
 
UPDATE dbo.[MemberInfo] SET [Status]=@NewStatus,MemName=@MemName,MobilePhone=@MobilePhone,Email=@Email,IdentityNo=@IdentityNo
WHERE [MemId]=@MemId 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, member.MemId);
            db.AddInParameter(cmd, "@NewStatus", DbType.Int32, (int)StoreStatus.WaitChecked);
            db.AddInParameter(cmd, "@MemName", DbType.String, member.ContactsManName);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, member.ContactsMobile);
            db.AddInParameter(cmd, "@Email", DbType.String, member.ContactsEmail);
            db.AddInParameter(cmd, "@IdentityNo", DbType.String, member.IdentityNo);

            return db.ExecuteNonQuery(cmd);
        }
        public int BasicMsgUpdate(VWMemberEntity member)
        {
            string sql = @" 
UPDATE dbo.[Member] SET [MemNikeName]=@MemNikeName 
WHERE [MemId]=@MemId 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, member.MemId);
            db.AddInParameter(cmd, "@MemNikeName", DbType.String, member.MemNikeName);

            return db.ExecuteNonQuery(cmd);
        }
        public int RegisterCompanyProc(VWMemberEntity entity)
        {
            string sql = @"EXEC Proc_StoreMemRegister  @MemCode,@PassWord,@CompanyName,@CompanyProvinceId,@CompanyCityId,@CompanyAddress,
@IsSupplier,@CompanyType,@ContactsManName,@ContactsMobile,@RecommendCode,@CreateClientType,@CreateIp,@ContactsEmail,@LicensePath";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemCode", DbType.String, entity.MemCode);
            db.AddInParameter(cmd, "@PassWord", DbType.String, CryptMD5.Encrypt(entity.PassWord));
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@CompanyProvinceId", DbType.Int32, entity.CompanyProvinceId);
            db.AddInParameter(cmd, "@CompanyCityId", DbType.Int32, entity.CompanyCityId);
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, entity.CompanyAddress);
            db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, entity.IsSupplier);
            db.AddInParameter(cmd, "@CompanyType", DbType.Int32, entity.CompanyType);
            db.AddInParameter(cmd, "@ContactsManName", DbType.String, entity.ContactsManName);
            db.AddInParameter(cmd, "@ContactsMobile", DbType.String, entity.ContactsMobile);
            db.AddInParameter(cmd, "@RecommendCode", DbType.String, entity.RecommendCode);
            db.AddInParameter(cmd, "@CreateClientType", DbType.String, entity.CreateClientType);
            db.AddInParameter(cmd, "@CreateIp", DbType.String, entity.CreateIp);
            db.AddInParameter(cmd, "@ContactsEmail", DbType.String, entity.ContactsEmail);
            db.AddInParameter(cmd, "@LicensePath", DbType.String, entity.LicensePath);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }

        public int RegisterProc(MemberEntity mem, MemberInfoEntity meminfo, MemStoreEntity store)
        {
            string sql = @"  BEGIN TRAN  
 UPDATE  dbo.Member SET Status=@MemStatus,MemCode=@MemCode WHERE   id = @MemId
IF EXISTS ( SELECT  1
                            FROM    dbo.MemberInfo
                            WHERE   MemId = @MemId ) 
                    BEGIN
 UPDATE  dbo.MemberInfo SET Nickname = @ContactsManName ,
                        MemName = @ContactsManName ,
                        MobilePhone = @ContactsMobile  
                WHERE   MemId = @MemId
end
else
begin
  INSERT  INTO dbo.MemberInfo
                        ( MemId ,
                          Nickname ,
                          HeadPicUrl ,
                          MemName ,
                          IdentityNo ,
                          IdentityNoCheck ,
                          Sex ,
                          Birthday ,
                          Telephone ,
                          MobilePhone ,
                          MobilePhoneCheck ,
                          Email ,
                          EmailCheck ,
                          QQ ,
                          QQCheck , 
                          IdentityPre ,
                          IdentityBeh ,
                          IdentityAutoName ,
                          IdentityAutoNo
	                  )
                VALUES  ( @MemId , -- MemId - int
                          @ContactsManName , -- Nickname - varchar(50)
                          '' , -- HeadPicUrl - varchar(200)
                          @ContactsManName , -- MemName - varchar(50)
                          '' , -- IdentityNo - varchar(30)
                          0 , -- IdentityNoCheck - int
                          0 , -- Sex - int
                          NULL , -- Birthday - datetime
                          '' , -- Telephone - varchar(30)
                          @ContactsMobile , -- MobilePhone - varchar(20)
                          1 , -- MobilePhoneCheck - int
                          '' , -- Email - varchar(50)
                          0 , -- EmailCheck - int
                          '' , -- QQ - varchar(20)
                          0 , -- QQCheck - int 
                          '' , -- IdentityPre - varchar(100)
                          '' , -- IdentityBeh - varchar(100)
                          '' , -- IdentityAutoName - varchar(50)
                          ''  -- IdentityAutoNo - varchar(30)
	                  )
	                 
end 
 IF EXISTS ( SELECT  1  FROM  dbo.MemStore  WHERE   MemId = @MemId ) 
                    BEGIN
                        UPDATE  dbo.MemStore
                        SET     StoreName = @CompanyName ,
                                CompanyName = @CompanyName ,
                                MobilePhone = @ContactsMobile ,
                                ContactsManName = @ContactsManName ,
                                ProvinceId = @CompanyProvinceId ,
                                CityId = @CompanyCityId ,
                                StoreType = @CompanyType ,
                                Address = @CompanyAddress 
                        WHERE   MemId = @MemId 
                    END
                ELSE 
                    BEGIN 
                        INSERT  INTO dbo.MemStore
                                ( MemId ,
                                  StoreName ,
                                  CompanyName ,
                                  LegalName ,
                                  LegalMobilePhone ,
                                  LegalIdentityNo ,
                                  LegalIdentityPre ,
                                  LegalIdentityBeh ,
                                  MobilePhone ,
                                  ContactsManName ,
                                  Country ,
                                  ProvinceId ,
                                  CityId ,
                                  StoreType ,
                                  District ,
                                  Address ,
                                  Longitude ,
                                  Latitude ,
                                  LicenseNo ,
                                  LicensePath ,
                                  Status ,
                                  GradeLevel ,
                                  CreateTime ,
                                  CheckManId ,
                                  CheckTime
	                          )
                        VALUES  ( @MemId , -- MemberId - int
                                  @CompanyName , -- StoreName - varchar(200) 
                                  @CompanyName , -- CompanyName - varchar(50)
                                  '' , -- LegalName - varchar(50)
                                  '' , -- LegalMobilePhone - varchar(50)
                                  '' , -- LegalIdentityNo - varchar(50)
                                  '' , -- LegalIdentityPre - varchar(200)
                                  '' , -- LegalIdentityBeh - varchar(200)
                                  @ContactsMobile , -- MobilePhone - varchar(20)
                                  @ContactsManName , -- ContactsManName - varchar(50)
                                  0 , -- Country - int
                                  @CompanyProvinceId , -- ProvinceId - int
                                  @CompanyCityId , -- CityId - int
                                  @CompanyType ,
                                  0 , -- District - int
                                  @CompanyAddress , -- Address - varchar(200)
                                  NULL , -- Longitude - decimal
                                  NULL , -- Latitude - decimal
                                  '' , -- LicenseNo - varchar(50)
                                  '' , -- LicensePath - varchar(200)
                                  10 , -- Status - int
                                  0 , -- GradeLevel - int
                                  GETDATE() , -- CreateTime - datetime
                                  0 ,
                                  NULL  -- CheckTime - datetime
	                          )
  INSERT  INTO [dbo].[PostAddress]
                        ( [MemId] ,
                          [AccepterName] ,
                          [CtType] ,
                          [CountryCode] ,
                          [CountryName] ,
                          [ProvinceId] ,
                          [CityId] ,
                          [District] ,
                          [Address] ,
                          [Email] ,
                          [Telephone] ,
                          [MobilePhone] ,
                          [IsDefault] ,
                          [Sort]
                        )
                VALUES  ( @MemId ,
                          @ContactsManName ,
                          0 ,
                          '' ,
                          '中国' ,
                          @CompanyProvinceId ,
                          @CompanyCityId ,
                          '' ,
                          @CompanyAddress ,
                          '' ,
                          '' ,
                          @ContactsMobile ,
                          1 ,
                          0
                        )            
                    END
                 SELECT  @MemId
	   
        IF @@error <> 0 
            BEGIN  
                SELECT  0
                ROLLBACK   TRAN   
            END      
        ELSE 
            COMMIT  TRAN 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemStatus", DbType.Int32, (int)MemberStatus.Register2);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, mem.Id);
            db.AddInParameter(cmd, "@MemCode", DbType.String, mem.MemCode);
            db.AddInParameter(cmd, "@ContactsManName", DbType.String, meminfo.MemName);
            db.AddInParameter(cmd, "@ContactsMobile", DbType.String, meminfo.MobilePhone);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, store.CompanyName);
            db.AddInParameter(cmd, "@CompanyProvinceId", DbType.Int32, store.ProvinceId);
            db.AddInParameter(cmd, "@CompanyCityId", DbType.Int32, store.CityId);
            db.AddInParameter(cmd, "@CompanyType", DbType.Int32, store.StoreType);
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, store.Address);
            
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 修改执照路径
        /// </summary>
        /// <param name="memid"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int RegisterCompanyLicense(int memid, string path)
        {
            string sql = @" 
UPDATE dbo.Member SET  Status=@MemStatus WHERE  Id=@MemId 
UPDATE dbo.MemStore SET  LicensePath=@LicensePath  WHERE MemId=@MemId   ";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@LicensePath", DbType.String, path);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            db.AddInParameter(cmd, "@MemStatus", DbType.Int32, (int)MemberStatus.WaitCheck); 
            return db.ExecuteNonQuery(cmd);
        }
        public int UpdateLastLoginDate(MemLoginLogEntity _log)
        {
            string sql = @" UPDATE dbo.[Member] SET  [LastLoginTime]=@LoginTime,LoginNum=LoginNum+1 WHERE [Id]=@MemId; 
             insert into MemLoginLog( [MemId],[LoginIP],[ClientType],[LoginTime])VALUES
			            ( @MemId,@LoginIP,@ClientType,@LoginTime);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, _log.MemId);
            db.AddInParameter(cmd, "@LoginIP", DbType.String, _log.LoginIP);
            db.AddInParameter(cmd, "@ClientType", DbType.Int32, _log.ClientType);
            db.AddInParameter(cmd, "@LoginTime", DbType.DateTime, _log.LoginTime);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public MemberEntity GetMemberByCode(string code)
        {
            string sql = @"SELECT  [TimeStampTab],[Id],[MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],[Status],[MemGrade],[RecommendCode]
							FROM
							dbo.[Member] WITH(NOLOCK)	
							WHERE [MemCode]=@MemCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemCode", DbType.String, code);
            MemberEntity entity = new MemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.TimeStampTab =  ByteUtils.GetStringFromByte(reader["TimeStampTab"]);
                }
            }
            return entity;
        }


        public IList<MemberEntity> GetMemByRoleCode(string rolecode)
        {
            string sql = @"SELECT   a.[Id],a.[MemCode],a.[Email],a.[QQ],a.[WeChat],a.[MobilePhone],a.[CreateTime],
  a.IsSysUser,a.IsSupplier,a.[IsStore],a.[Status],a.[MemGrade] 
							FROM
							dbo.[Member] a WITH(NOLOCK)	 inner join MemRoleRelate b WITH(NOLOCK) on a.Id=b.MemId
inner join MemRole c  WITH(NOLOCK)	 on b.RoleId=c.Id 
							WHERE c.[RoleCode]=@RoleCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@RoleCode", DbType.String, rolecode);
            IList<MemberEntity> list = new List<MemberEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemberEntity entity = new MemberEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]); 
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);  
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]);
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    list.Add(entity);
                }
            }
            return list;
        }
        public IList<VWMemberEntity> GetMemByAuthCode(string authcode)
        {
            string sql = @"SELECT distinct  a.[Id],a.[MemCode],a.[Email],a.[QQ],a.[WeChat],a.[MobilePhone],a.[CreateTime],
  a.IsSysUser,a.IsSupplier,a.[IsStore],a.[Status],a.[MemGrade],s.CompanyName,s.Address 
							FROM dbo.[Member] a WITH(NOLOCK)	 inner join MemRoleRelate b WITH(NOLOCK) on a.Id=b.MemId
inner join MemRoleAuth c  WITH(NOLOCK)	 on b.RoleId=c.RoleId inner join  MemAuth d  WITH(NOLOCK)	on c.AuthId=d.Id 
inner join Store s   WITH(NOLOCK) on a.id=s.MemId
							WHERE d.[AuthCode]=@AuthCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@AuthCode", DbType.String, authcode);
            IList<VWMemberEntity> list = new List<VWMemberEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemberEntity entity = new VWMemberEntity();
                    entity.MemId = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]);
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["Address"]);
                    list.Add(entity);
                }
            }
            return list;
        }
        public MemberEntity GetMemByMethod(string code, LoginMethodEnum method)
        {
            string where = " where 1=1 ";
            if (method == LoginMethodEnum.Code)
            {
                where += " and a.[MemCode]=@MemCode ";
            }
            else if (method == LoginMethodEnum.MobilePhone)
            {
                where += " and a.[MobilePhone] =@MobilePhone ";
            }
            else if (method == LoginMethodEnum.WeChat)
            {
                where += " and a.[WeChat] =@WeChat";
            }
            else if (method == LoginMethodEnum.MemId)
            {
                where += " and a.[Id] =@MemId";
            }
            string sql = @"SELECT  [Id],[MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],[StoreType],IsSupplier,IsSysUser,[Status],[MemGrade],[RecommendCode]
							,TimeStampTab FROM
							dbo.[Member] a WITH(NOLOCK)	 
							 " + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (method == LoginMethodEnum.Code)
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, code);
            }
            else if (method == LoginMethodEnum.MobilePhone)
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, code);
            }
            else if (method == LoginMethodEnum.WeChat)
            {
                db.AddInParameter(cmd, "@WeChat", DbType.String, code);
            }
            else if (method == LoginMethodEnum.MemId)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, code);
            }
            MemberEntity entity = new MemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]); 
                    entity.StoreType = StringUtils.GetDbInt(reader["StoreType"]);  
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]);
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.TimeStampTab = ByteUtils.GetStringFromByte(reader["TimeStampTab"]);
                }
            }
            return entity;
        }
        public MemberEntity GetMemByMobile(string mobile)
        {
            string sql = @"SELECT Top(1) [TimeStampTab],[Id],[MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],[IsSupplier],[IsSysUser],[Status],[MemGrade],[RecommendCode]
							FROM
							dbo.[Member] WITH(NOLOCK)	
							WHERE [MobilePhone]=@MobilePhone";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MobilePhone", DbType.String, mobile);
            MemberEntity entity = new MemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]);
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.TimeStampTab =  ByteUtils.GetStringFromByte(reader["TimeStampTab"]);
                }
            }
            return entity;
        }

        public MemberEntity GetLoginMemByMobile(string mobile)
        {
            string sql = @"SELECT Top(1) [TimeStampTab],[Id],[MemCode],[Email],[QQ],[WeChat],[MobilePhone],[PassWord],[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],IsSupplier,[Status],[MemGrade],[RecommendCode]
							FROM
							dbo.[Member] WITH(NOLOCK)	
							WHERE [MobilePhone]=@MobilePhone";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MobilePhone", DbType.String, mobile);
            MemberEntity entity = new MemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.TimeStampTab =  ByteUtils.GetStringFromByte(reader["TimeStampTab"]);
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]);
                    
                }
            }
            return entity;
        }

        


        /// <summary>
        /// 检查手机号码是否唯一
        /// </summary>
        /// <returns></returns>
        public int CheckPhoneIsExist(string mobilePhone)
        {
            string sql = @"SELECT COUNT(1) FROM DBO.MEMBER WHERE MobilePhone=@MobilePhone AND STATUS<>0";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, mobilePhone);

            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return StringUtils.GetDbInt(result);
        }
        public Dictionary<string, string> GetAuthByMemId(int memid)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string sql = @"SELECT  DISTINCT  [AuthCode]
      ,[AuthName] 
  FROM  [dbo].[MemAuth] a WITH(NOLOCK) INNER JOIN dbo.MemRoleAuth b WITH(NOLOCK) ON a.id=b.AuthId INNER JOIN 
dbo.MemRoleRelate c WITH(NOLOCK) ON b.RoleId=c.MemId WHERE c.MemId=@MemId
  AND a.IsActive=1 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    dic.Add(StringUtils.GetDbString(reader["AuthCode"]), StringUtils.GetDbString(reader["AuthName"]));
                }
            }
            return dic;
        }

        public MemberLoginEntity GetLoginMemByCode(string code)
        {
            string sql = @"SELECT  a.[Id] as MemId,[MemCode], [PassWord],IsSupplier,IsSysUser, [IsStore],a.StoreType,a.[Status],[MemGrade],a.MemNikeName 
							FROM
							dbo.[Member] a WITH(NOLOCK)	 
							WHERE [MemCode]=@MemCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemCode", DbType.String, code);
            MemberLoginEntity entity = new MemberLoginEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);  
                    entity.StoreType = StringUtils.GetDbInt(reader["StoreType"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.NickName = StringUtils.GetDbString(reader["MemNikeName"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);  
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]); 
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]); 
                }
            }
            return entity;
        }
        public MemberLoginEntity GetLoginMemByMethod(string code, LoginMethodEnum method)
        {
            string where = " where 1=1 ";
            if(method== LoginMethodEnum.Code)
            {
                where += " and a.[MemCode]=@MemCode ";
            }
            else if (method == LoginMethodEnum.MobilePhone)
            {
                where += " and a.[MobilePhone] =@MobilePhone ";
            }
            else if (method == LoginMethodEnum.WeChat)
            {
                where += " and a.[WeChat] =@WeChat";
            }
            else if (method == LoginMethodEnum.MemId)
            {
                where += " and a.[Id] =@MemId";
            }
            else if (method == LoginMethodEnum.TempCode)
            {
                where += " and a.[TimeStampTab] =@TimeStampTab";

            }
            string sql = @"SELECT  a.[Id] as MemId,[MemCode], WeChat,[PassWord],IsSupplier, IsSysUser,[IsStore],a.StoreType,a.[Status],[MemGrade],MemNikeName,TimeStampTab 
							FROM
							dbo.[Member] a WITH(NOLOCK) 
							 " + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (method == LoginMethodEnum.Code)
            { 
                db.AddInParameter(cmd, "@MemCode", DbType.String, code);
            }
            else if (method == LoginMethodEnum.MobilePhone)
            { 
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, code);
            }
            else if (method == LoginMethodEnum.WeChat)
            {
                db.AddInParameter(cmd, "@WeChat", DbType.String, code);
            }
            else if (method == LoginMethodEnum.MemId)
            { 
                db.AddInParameter(cmd, "@MemId", DbType.Int32, code);
            }
            else if (method == LoginMethodEnum.TempCode)
            {
                  string ssstemp = CryptDES.DESDecrypt(code);
                  db.AddInParameter(cmd, "@TimeStampTab", DbType.Binary, ByteUtils.GetByteFromString(ssstemp));
             }
            MemberLoginEntity entity = new MemberLoginEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]);
                    entity.StoreType = StringUtils.GetDbInt(reader["StoreType"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.NickName = StringUtils.GetDbString(reader["MemNikeName"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]); 
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]); 
                    entity.TimeStampTab = ByteUtils.GetStringFromByte(reader["TimeStampTab"]);
                }
            }
            return entity;
        }

        public MemberLoginEntity GetLoginMemByPhone(string phonecode)
        {
            string sql = @"SELECT top 1 a.[Id] as MemId,[MemCode], [PassWord],IsSupplier,IsSysUser, [IsStore],a.StoreType,a.[Status],[MemGrade],a.MemNikeName 
							FROM
							dbo.[Member] a WITH(NOLOCK)	 
							WHERE a.[MobilePhone] =@MobilePhone";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MobilePhone", DbType.String, phonecode);
            MemberLoginEntity entity = new MemberLoginEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);  
                    entity.StoreType = StringUtils.GetDbInt(reader["StoreType"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.NickName = StringUtils.GetDbString(reader["MemNikeName"]);  
                    entity.IsSysUser = StringUtils.GetDbInt(reader["IsSysUser"]); 
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);  
                    entity.IsSupplier = StringUtils.GetDbInt(reader["IsSupplier"]); 
                }
            }
            return entity;
        }




        /// <summary>
        /// 判断当前节点是否已存在相同的
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int ExistNum(MemberEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Member] WITH(NOLOCK) ";
            string where = "where 1=1 ";
            if (entity.Id == 0)
            {
                where = where + " and MemCode=@MemCode ";
            }
            else
            {
                where = where + " and Id<>@Id and  MemCode=@MemCode ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
            db.AddInParameter(cmd, "@MemCode", DbType.String, entity.MemCode);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public bool MemberCodeExist(string membercode)
        {
            string sql = @"Select count(1) from dbo.[Member] WITH(NOLOCK) where MemCode=@MemCode ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemCode", DbType.String, membercode);
            return (StringUtils.GetDbInt(db.ExecuteScalar(cmd)) > 0);
        }

        public int CheckSupplier(int memId, int memberStatus, int storeId, int storeStatus, int billId, int billStatus, int checkmanid)
        {
            string sql = @"
                            BEGIN TRAN OK
 
		                    BEGIN
		
			                    UPDATE dbo.MemStore SET Status=@StoreStatus,CheckTime=GETDATE(),CheckManId=@CheckManId WHERE Id=@StoreId;
			                    UPDATE DBO.Member SET Status=@MemberStatus WHERE Id=@MemId;
			                    UPDATE DBO.MemBillVAT SET Status=@BillStatus WHERE Id=@BillId;
			 
                                IF(@@ERROR<>0)
                                     BEGIN
                                             ROLLBACK TRAN ok;
                                             SELECT 0;
                                     END
                                ELSE
                                     BEGIN
                                             COMMIT TRAN ok;
                                             SELECT 1;
                                     END
			
		                   END
			
";

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memId);
            db.AddInParameter(cmd, "@MemberStatus", DbType.Int32, memberStatus);
            db.AddInParameter(cmd, "@StoreId", DbType.Int32, storeId);
            db.AddInParameter(cmd, "@StoreStatus", DbType.Int32, storeStatus);
            db.AddInParameter(cmd, "@BillId", DbType.Int32, billId);
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, billStatus);
            db.AddInParameter(cmd, "@CheckManId", DbType.Int32, checkmanid);


            return StringUtils.GetDbInt(db.ExecuteNonQuery(cmd));


        }

    }
}
