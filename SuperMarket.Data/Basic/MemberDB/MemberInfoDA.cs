using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;
using SuperMarket.Model.Basic.VW.Member;

/*****************************************
功能描述：MemberInfo表的数据访问类。
创建时间：2016/8/9 10:45:15
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemberInfoEntity的数据访问操作
	/// </summary>
	public partial class MemberInfoDA : BaseSuperMarketDB
    {
        #region 实例化
        public static MemberInfoDA Instance
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
            internal static readonly MemberInfoDA instance = new MemberInfoDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemberInfo，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memberInfo">待插入的实体对象</param>
		public int AddMemberInfo(MemberInfoEntity entity)
		{
		   string sql=@"insert into MemberInfo( [MemId],[Nickname],[MemName],[IdentityNo],[IdentityNoCheck],[Sex],[Birthday],[Telephone],[MobilePhone],[MobilePhoneCheck],[Email],[EmailCheck],[QQ],[QQCheck],  [IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo])VALUES
			            ( @MemId,@Nickname,@MemName,@IdentityNo,@IdentityNoCheck,@Sex,@Birthday,@Telephone,@MobilePhone,@MobilePhoneCheck,@Email,@EmailCheck,@QQ,@QQCheck, @IdentityPre,@IdentityBeh,@IdentityAutoName,@IdentityAutoNo);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Nickname",  DbType.String,entity.Nickname);
			db.AddInParameter(cmd,"@MemName",  DbType.String,entity.MemName);
			db.AddInParameter(cmd,"@IdentityNo",  DbType.String,entity.IdentityNo);
			db.AddInParameter(cmd,"@IdentityNoCheck",  DbType.Int32,entity.IdentityNoCheck);
			db.AddInParameter(cmd,"@Sex",  DbType.Int32,entity.Sex);
			db.AddInParameter(cmd,"@Birthday",  DbType.DateTime, (entity.Birthday == null || entity.Birthday < DateTime.Now.AddYears(-100)) ? "1901-01-01" : entity.Birthday.ToString());
			db.AddInParameter(cmd,"@Telephone",  DbType.String,entity.Telephone);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@MobilePhoneCheck",  DbType.Int32,entity.MobilePhoneCheck);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@EmailCheck",  DbType.Int32,entity.EmailCheck);
			db.AddInParameter(cmd,"@QQ",  DbType.String,entity.QQ);
			db.AddInParameter(cmd,"@QQCheck",  DbType.Int32,entity.QQCheck); 
			db.AddInParameter(cmd,"@IdentityPre",  DbType.String,entity.IdentityPre);
			db.AddInParameter(cmd,"@IdentityBeh",  DbType.String,entity.IdentityBeh);
			db.AddInParameter(cmd,"@IdentityAutoName",  DbType.String,entity.IdentityAutoName);
			db.AddInParameter(cmd,"@IdentityAutoNo",  DbType.String,entity.IdentityAutoNo);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memberInfo">待更新的实体对象</param>
		public   int UpdateMemberInfo(MemberInfoEntity entity)
		{
			string sql= @" UPDATE dbo.[MemberInfo] SET
                       [MemId]=@MemId,[Nickname]=@Nickname,[MemName]=@MemName,[IdentityNo]=@IdentityNo,[IdentityNoCheck]=@IdentityNoCheck,[Sex]=@Sex,[Birthday]=@Birthday,[Telephone]=@Telephone,[MobilePhone]=@MobilePhone,[MobilePhoneCheck]=@MobilePhoneCheck,[Email]=@Email,[EmailCheck]=@EmailCheck,[QQ]=@QQ,[QQCheck]=@QQCheck,[IdentityPre]=@IdentityPre,[IdentityBeh]=@IdentityBeh,[IdentityAutoName]=@IdentityAutoName,[IdentityAutoNo]=@IdentityAutoNo,[HeadPicUrl]=@HeadPicUrl
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Nickname",  DbType.String,entity.Nickname);
			db.AddInParameter(cmd,"@MemName",  DbType.String,entity.MemName);
			db.AddInParameter(cmd,"@IdentityNo",  DbType.String,entity.IdentityNo);
			db.AddInParameter(cmd,"@IdentityNoCheck",  DbType.Int32,entity.IdentityNoCheck);
			db.AddInParameter(cmd,"@Sex",  DbType.Int32,entity.Sex);
            db.AddInParameter(cmd,"@Birthday", DbType.DateTime, (entity.Birthday == null|| entity.Birthday < DateTime.Now.AddYears(-100))?"1901-01-01": entity.Birthday.ToString());
			db.AddInParameter(cmd,"@Telephone",  DbType.String,entity.Telephone);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@MobilePhoneCheck",  DbType.Int32,entity.MobilePhoneCheck);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@EmailCheck",  DbType.Int32,entity.EmailCheck);
			db.AddInParameter(cmd,"@QQ",  DbType.String,entity.QQ);
			db.AddInParameter(cmd,"@QQCheck",  DbType.Int32,entity.QQCheck); 
			db.AddInParameter(cmd,"@IdentityPre",  DbType.String,entity.IdentityPre);
			db.AddInParameter(cmd,"@IdentityBeh",  DbType.String,entity.IdentityBeh);
			db.AddInParameter(cmd,"@IdentityAutoName",  DbType.String,entity.IdentityAutoName);
			db.AddInParameter(cmd,"@IdentityAutoNo",  DbType.String,entity.IdentityAutoNo);
			db.AddInParameter(cmd, "@HeadPicUrl",  DbType.String,entity.HeadPicUrl);
    	 	return db.ExecuteNonQuery(cmd);
		}
        public int UpdateShowHead(int memid,string picpath)
        {
            string sql = @" UPDATE dbo.[MemberInfo] SET  [HeadPicUrl]=@HeadPicUrl
                       WHERE [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@HeadPicUrl", DbType.String, picpath);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
           
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int  DeleteMemberInfoByKey(int id)
	    {
			string sql=@"delete from MemberInfo where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemberInfoDisabled()
        {
            string sql = @"delete from  MemberInfo  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemberInfoByIds(string ids)
        {
            string sql = @"Delete from MemberInfo  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemberInfoByIds(string ids)
        {
            string sql = @"Update   MemberInfo set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemberInfoEntity GetMemberInfo(int id)
		{
			string sql= @"SELECT  [Id],[MemId],[Nickname],[MemName],[IdentityNo],[IdentityNoCheck],[Sex],[Birthday],[Telephone],[MobilePhone],[MobilePhoneCheck],[Email],[EmailCheck],[QQ],[QQCheck],[IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo],HeadPicUrl
							FROM
							dbo.[MemberInfo] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemberInfoEntity entity=new MemberInfoEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Nickname=StringUtils.GetDbString(reader["Nickname"]);
					entity.MemName=StringUtils.GetDbString(reader["MemName"]);
					entity.IdentityNo=StringUtils.GetDbString(reader["IdentityNo"]);
					entity.IdentityNoCheck=StringUtils.GetDbInt(reader["IdentityNoCheck"]);
					entity.Sex=StringUtils.GetDbInt(reader["Sex"]);
					entity.Birthday=StringUtils.GetDbDateTime(reader["Birthday"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.MobilePhoneCheck=StringUtils.GetDbInt(reader["MobilePhoneCheck"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.EmailCheck=StringUtils.GetDbInt(reader["EmailCheck"]);
					entity.QQ=StringUtils.GetDbString(reader["QQ"]);
					entity.QQCheck=StringUtils.GetDbInt(reader["QQCheck"]); 
					entity.IdentityPre=StringUtils.GetDbString(reader["IdentityPre"]);
					entity.IdentityBeh=StringUtils.GetDbString(reader["IdentityBeh"]);
					entity.IdentityAutoName=StringUtils.GetDbString(reader["IdentityAutoName"]);
					entity.IdentityAutoNo=StringUtils.GetDbString(reader["IdentityAutoNo"]);
					entity.HeadPicUrl = StringUtils.GetDbString(reader["HeadPicUrl"]);
                }
   		    }
            return entity;
		}
      
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<MemberInfoEntity> GetMemberInfoList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[Nickname],[MemName],[IdentityNo],[IdentityNoCheck],[Sex],[Birthday],[Telephone],[MobilePhone],[MobilePhoneCheck],[Email],[EmailCheck],[QQ],[QQCheck], [IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[Nickname],[MemName],[IdentityNo],[IdentityNoCheck],[Sex],[Birthday],[Telephone],[MobilePhone],[MobilePhoneCheck],[Email],[EmailCheck],[QQ],[QQCheck], [IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo] from dbo.[MemberInfo] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemberInfo] with (nolock) ";
            IList<MemberInfoEntity> entityList = new List< MemberInfoEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemberInfoEntity entity=new MemberInfoEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Nickname=StringUtils.GetDbString(reader["Nickname"]);
					entity.MemName=StringUtils.GetDbString(reader["MemName"]);
					entity.IdentityNo=StringUtils.GetDbString(reader["IdentityNo"]);
					entity.IdentityNoCheck=StringUtils.GetDbInt(reader["IdentityNoCheck"]);
					entity.Sex=StringUtils.GetDbInt(reader["Sex"]);
					entity.Birthday=StringUtils.GetDbDateTime(reader["Birthday"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.MobilePhoneCheck=StringUtils.GetDbInt(reader["MobilePhoneCheck"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.EmailCheck=StringUtils.GetDbInt(reader["EmailCheck"]);
					entity.QQ=StringUtils.GetDbString(reader["QQ"]);
					entity.QQCheck=StringUtils.GetDbInt(reader["QQCheck"]); 
					entity.IdentityPre=StringUtils.GetDbString(reader["IdentityPre"]);
					entity.IdentityBeh=StringUtils.GetDbString(reader["IdentityBeh"]);
					entity.IdentityAutoName=StringUtils.GetDbString(reader["IdentityAutoName"]);
					entity.IdentityAutoNo=StringUtils.GetDbString(reader["IdentityAutoNo"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
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
        public IList<MemberInfoEntity> GetMemberInfoAll()
        {

            string sql = @"SELECT    [Id],[MemId],[Nickname],[MemName],[IdentityNo],[IdentityNoCheck],[Sex],[Birthday],[Telephone],[MobilePhone],[MobilePhoneCheck],[Email],[EmailCheck],[QQ],[QQCheck],[WebChart],[WebChartCheck],[IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo] from dbo.[MemberInfo] WITH(NOLOCK)	";
		    IList<MemberInfoEntity> entityList = new List<MemberInfoEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemberInfoEntity entity=new MemberInfoEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Nickname=StringUtils.GetDbString(reader["Nickname"]);
					entity.MemName=StringUtils.GetDbString(reader["MemName"]);
					entity.IdentityNo=StringUtils.GetDbString(reader["IdentityNo"]);
					entity.IdentityNoCheck=StringUtils.GetDbInt(reader["IdentityNoCheck"]);
					entity.Sex=StringUtils.GetDbInt(reader["Sex"]);
					entity.Birthday=StringUtils.GetDbDateTime(reader["Birthday"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.MobilePhoneCheck=StringUtils.GetDbInt(reader["MobilePhoneCheck"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.EmailCheck=StringUtils.GetDbInt(reader["EmailCheck"]);
					entity.QQ=StringUtils.GetDbString(reader["QQ"]);
					entity.QQCheck=StringUtils.GetDbInt(reader["QQCheck"]);
					entity.WebChart=StringUtils.GetDbString(reader["WebChart"]);
					entity.WebChartCheck=StringUtils.GetDbInt(reader["WebChartCheck"]);
					entity.IdentityPre=StringUtils.GetDbString(reader["IdentityPre"]);
					entity.IdentityBeh=StringUtils.GetDbString(reader["IdentityBeh"]);
					entity.IdentityAutoName=StringUtils.GetDbString(reader["IdentityAutoName"]);
					entity.IdentityAutoNo=StringUtils.GetDbString(reader["IdentityAutoNo"]);
				    entityList.Add(entity);
                }
            } 
            return entityList;
        }
        
		    /// <summary>
        /// 判断当前节点是否已存在相同的
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int  ExistNum(MemberInfoEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemberInfo] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (MemName=@MemName) ";
					     where = where+ "  (IdentityAutoName=@IdentityAutoName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (MemName=@MemName) ";
					     where = where+ " id<>@Id and  (IdentityAutoName=@IdentityAutoName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@MemName", DbType.String, entity.MemName); 
					
            db.AddInParameter(cmd, "@IdentityAutoName", DbType.String, entity.IdentityAutoName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }








        #endregion
        #endregion

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public VWMemberEntity GetVWMemberInfoByMemId(int memid)
        {
            string sql = @"SELECT   a.[MemCode],a.[PassWord],a.[CreateTime],[LastLoginTime],[LoginNum],[CreateIp],[CreateClientType],[IsStore],a.[Status],MemGrade,RecommendCode,
a.Id AS [MemId],NickName,[MemName],[IdentityNo],IdentityNoCheck,[Telephone],b.[MobilePhone],[MobilePhoneCheck],b.[Email],[EmailCheck],b.[QQ],[QQCheck], 
c.GradeLevel,b.HeadPicUrl,c.CompanyName
							FROM
							dbo.[Member] a WITH(NOLOCK)  left join 
							dbo.[MemberInfo]  b WITH(NOLOCK) ON a.id=b.MemId	left join 
							dbo.MemStore  c WITH(NOLOCK) ON a.id=c.MemId
							WHERE a.[Id]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            VWMemberEntity entity = new VWMemberEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.LoginNum = StringUtils.GetDbInt(reader["LoginNum"]);
                    entity.CreateIp = StringUtils.GetDbString(reader["CreateIp"]);
                    entity.CreateClientType = StringUtils.GetDbInt(reader["CreateClientType"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.HeadPicUrl = StringUtils.GetDbString(reader["HeadPicUrl"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);

                    entity.RecommendCode = StringUtils.GetDbString(reader["RecommendCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemName = StringUtils.GetDbString(reader["MemName"]);
                    entity.IdentityNo = StringUtils.GetDbString(reader["IdentityNo"]);
                    entity.IdentityNoCheck = StringUtils.GetDbInt(reader["IdentityNoCheck"]);
                    entity.Telephone = StringUtils.GetDbString(reader["Telephone"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.MobilePhoneCheck = StringUtils.GetDbInt(reader["MobilePhoneCheck"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.EmailCheck = StringUtils.GetDbInt(reader["EmailCheck"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.QQCheck = StringUtils.GetDbInt(reader["QQCheck"]); 

                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                }
            }
            return entity;
        }

        public MemberInfoEntity GetMemberInfoByMemId(int memid)
        {
            string sql = @"SELECT  [Id],[MemId],[Nickname],[MemName],[IdentityNo],[IdentityNoCheck],[Sex],[Birthday],[Telephone],[MobilePhone],[MobilePhoneCheck],[Email],[EmailCheck],[QQ],[QQCheck],[WebChart],[WebChartCheck],[IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo],HeadPicUrl
							FROM
							dbo.[MemberInfo] WITH(NOLOCK)	
							WHERE [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            MemberInfoEntity entity = new MemberInfoEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.Nickname = StringUtils.GetDbString(reader["Nickname"]);
                    entity.MemName = StringUtils.GetDbString(reader["MemName"]);
                    entity.IdentityNo = StringUtils.GetDbString(reader["IdentityNo"]);
                    entity.IdentityNoCheck = StringUtils.GetDbInt(reader["IdentityNoCheck"]);
                    entity.Sex = StringUtils.GetDbInt(reader["Sex"]);
                    entity.Birthday = StringUtils.GetDbDateTime(reader["Birthday"]);
                    entity.Telephone = StringUtils.GetDbString(reader["Telephone"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.MobilePhoneCheck = StringUtils.GetDbInt(reader["MobilePhoneCheck"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.EmailCheck = StringUtils.GetDbInt(reader["EmailCheck"]);
                    entity.QQ = StringUtils.GetDbString(reader["QQ"]);
                    entity.QQCheck = StringUtils.GetDbInt(reader["QQCheck"]);
                    entity.WebChart = StringUtils.GetDbString(reader["WebChart"]);
                    entity.WebChartCheck = StringUtils.GetDbInt(reader["WebChartCheck"]);
                    entity.IdentityPre = StringUtils.GetDbString(reader["IdentityPre"]);
                    entity.IdentityBeh = StringUtils.GetDbString(reader["IdentityBeh"]);
                    entity.IdentityAutoName = StringUtils.GetDbString(reader["IdentityAutoName"]);
                    entity.IdentityAutoNo = StringUtils.GetDbString(reader["IdentityAutoNo"]);
                    entity.HeadPicUrl = StringUtils.GetDbString(reader["HeadPicUrl"]);
                }
            }
            return entity;
        }



        /// <summary>
        /// 获取用于个人用户审核视图
        /// </summary>
        /// <returns></returns>
        public IList<VWMemberEntity2> GetVWMemberInfo(int pageIndex,int pageSize,ref int recordCount,string memCode,int status)
        {
            string where = " where IsStore=0 ";
            if (!string.IsNullOrEmpty(memCode))
            {
                where += "  and MemCode like @MemCode";
            }
            if (status>0)
            {
                where += "  and a.Status=@Status";
            }

            string sql = @" SELECT Id,MemCode ,IdentityPre,IdentityBeh,Status FROM 
                           (Select ROW_NUMBER() OVER (Order By a.Id desc) as ROWNUMBER , a.Id,a.MemCode,b.IdentityPre,b.IdentityBeh,a.[Status] From dbo.[Member] a with (nolock) 
                            inner join dbo.[MemberInfo] b with (nolock) on a.Id=b.MemId "+where+@") as temp
                            WHERE ROWNUMBER BETWEEN ((@PAGEINDEX-1)*@PAGESIZE+1) AND (@PAGEINDEX*@PAGESIZE)";

            string sql2 = @"SELECT COUNT(1) FROM DBO.[MEMBER] A WITH (NOLOCK) INNER JOIN DBO.[MEMBERINFO] B WITH (NOLOCK) ON A.ID=B.MEMID" + where;

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PAGEINDEX", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PAGESIZE", DbType.Int32, pageSize);
            if (!string.IsNullOrEmpty(memCode))
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%"+memCode+"%");
            }
            if (status>0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            IList<VWMemberEntity2> entityList = new List<VWMemberEntity2>();

            using (IDataReader reader=db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemberEntity2 entity = new VWMemberEntity2();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.IDPre = StringUtils.GetDbString(reader["IdentityPre"]);
                    entity.IDBeh = StringUtils.GetDbString(reader["IdentityBeh"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entityList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(memCode))
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%" + memCode + "%");
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
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

    }
}
