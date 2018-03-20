using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;

/*****************************************
功能描述：Store表的数据访问类。
创建时间：2016/8/11 13:43:28
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
    /// <summary>
    /// MemStoreEntity的数据访问操作
    /// </summary>
    public partial class MemStoreDA : BaseSuperMarketDB
    {
        #region 实例化
        public static MemStoreDA Instance
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
            internal static readonly MemStoreDA instance = new MemStoreDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Store，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="store">待插入的实体对象</param>
		public int AddStore( MemStoreEntity entity)
		{
		   string sql= @"
insert into MemStore( [MemId],[StoreName],[CompanyName],[LegalName],LegalMobilePhone,[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime])VALUES
			            ( @MemId,@StoreName,@CompanyName,@LegalName,@LegalMobilePhone,@LegalIdentityNo,@LegalIdentityPre,@LegalIdentityBeh,@MobilePhone,@ContactsManName,@Country,@ProvinceId,@CityId,@District,@Address,@Longitude,@Latitude,@LicenseNo,@LicensePath,@Status,@GradeLevel,@CreateTime);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@StoreName", DbType.String, entity.StoreName);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@LegalName", DbType.String, entity.LegalName);
            db.AddInParameter(cmd, "@LegalMobilePhone", DbType.String, entity.LegalMobilePhone);

            db.AddInParameter(cmd, "@LegalIdentityNo", DbType.String, entity.LegalIdentityNo);
            db.AddInParameter(cmd, "@LegalIdentityPre", DbType.String, entity.LegalIdentityPre);
            db.AddInParameter(cmd, "@LegalIdentityBeh", DbType.String, entity.LegalIdentityBeh);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, entity.MobilePhone);
            db.AddInParameter(cmd, "@ContactsManName", DbType.String, entity.ContactsManName);
            db.AddInParameter(cmd, "@Country", DbType.Int32, entity.Country);
            db.AddInParameter(cmd, "@ProvinceId", DbType.String, entity.ProvinceId);
            db.AddInParameter(cmd, "@CityId", DbType.String, entity.CityId);
            db.AddInParameter(cmd, "@District", DbType.String, entity.District);
            db.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, entity.Longitude);
            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, entity.Latitude);
            db.AddInParameter(cmd, "@LicenseNo", DbType.String, entity.LicenseNo);
            db.AddInParameter(cmd, "@LicensePath", DbType.String, entity.LicensePath);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@GradeLevel", DbType.Int32, entity.GradeLevel);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="store">待更新的实体对象</param>
        public int UpdateStore( MemStoreEntity entity)
        {
            string sql = @" UPDATE dbo.MemStore SET
                       [MemId]=@MemId,[StoreName]=@StoreName,[CompanyName]=@CompanyName,[LegalName]=@LegalName,LegalMobilePhone=@LegalMobilePhone,[LegalIdentityNo]=@LegalIdentityNo,[LegalIdentityPre]=@LegalIdentityPre,[LegalIdentityBeh]=@LegalIdentityBeh,[MobilePhone]=@MobilePhone,[ContactsManName]=@ContactsManName,[Country]=@Country,[ProvinceId]=@ProvinceId,[CityId]=@CityId,[District]=@District,[Address]=@Address,[Longitude]=@Longitude,[Latitude]=@Latitude,[LicenseNo]=@LicenseNo,[LicensePath]=@LicensePath,[Status]=@Status,[GradeLevel]=@GradeLevel,[CreateTime]=@CreateTime
                       WHERE [Id]=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@StoreName", DbType.String, entity.StoreName);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@LegalName", DbType.String, entity.LegalName);
            db.AddInParameter(cmd, "@LegalMobilePhone", DbType.String, entity.LegalMobilePhone);

            db.AddInParameter(cmd, "@LegalIdentityNo", DbType.String, entity.LegalIdentityNo);
            db.AddInParameter(cmd, "@LegalIdentityPre", DbType.String, entity.LegalIdentityPre);
            db.AddInParameter(cmd, "@LegalIdentityBeh", DbType.String, entity.LegalIdentityBeh);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, entity.MobilePhone);
            db.AddInParameter(cmd, "@ContactsManName", DbType.String, entity.ContactsManName);
            db.AddInParameter(cmd, "@Country", DbType.Int32, entity.Country);
            db.AddInParameter(cmd, "@ProvinceId", DbType.String, entity.ProvinceId);
            db.AddInParameter(cmd, "@CityId", DbType.String, entity.CityId);
            db.AddInParameter(cmd, "@District", DbType.String, entity.District);
            db.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, entity.Longitude);
            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, entity.Latitude);
            db.AddInParameter(cmd, "@LicenseNo", DbType.String, entity.LicenseNo);
            db.AddInParameter(cmd, "@LicensePath", DbType.String, entity.LicensePath);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@GradeLevel", DbType.Int32, entity.GradeLevel);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            return db.ExecuteNonQuery(cmd);
        }
       
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public MemStoreEntity GetStore(int id)
        {
            string sql = @"SELECT  [Id],[MemId],[StoreName],[CompanyName],[LegalName],LegalMobilePhone,[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
							FROM
							dbo.MemStore a WITH(NOLOCK)	 
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            MemStoreEntity entity = new MemStoreEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                }
            }
            return entity;
        }
        public MemStoreEntity GetStoreByNameAdd(string name, string address)
        {
            string sql = @"SELECT  [Id],[MemId],[StoreName],[CompanyName],[LegalName],LegalMobilePhone,[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
							FROM
							dbo.MemStore a WITH(NOLOCK)	 
							WHERE [CompanyName]=@CompanyName and Address=@Address";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CompanyName", DbType.String, name);
            db.AddInParameter(cmd, "@Address", DbType.String, address);
            MemStoreEntity entity = new MemStoreEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList< MemStoreEntity> GetStoreList(int pagesize, int pageindex, ref int recordCount, string legalName, string companyName, int Status,string memcode,string contractname,string contractphone,int _issupplier)
        {
            string where = " WHERE 1=1 ";

            if (!string.IsNullOrEmpty(legalName))
            {
                where += " AND a.LegalName Like @LegalName";
            }
            if (!string.IsNullOrEmpty(companyName))
            {
                where += " AND a.CompanyName Like @CompanyName";
            }
            if (Status != -1)
            {
                where += " AND a.[Status]=@Status";
            }
            if (!string.IsNullOrEmpty(memcode))
            {
                where += " AND b.MemCode Like @MemCode";
            }
            if (!string.IsNullOrEmpty(contractname))
            {
                where += " AND a.ContactsManName Like @ContactsManName";
            }
            if (!string.IsNullOrEmpty(contractphone))
            {
                where += " AND a.MobilePhone Like @MobilePhone";
            }
            if (_issupplier!=-1)
            {
                where += " AND b.IsSupplier = @IsSupplier";
            }
            string sql = @"SELECT   MemCode,[Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 b.MemCode,a.[Id],a.[MemId],a.[StoreName],a.[CompanyName],a.[LegalName],a.[LegalMobilePhone],a.[LegalIdentityNo],a.[LegalIdentityPre],a.[LegalIdentityBeh],a.[MobilePhone],a.[ContactsManName],a.[Country],a.[ProvinceId],a.[CityId],a.[District],a.[Address],a.[Longitude],a.[Latitude],a.[LicenseNo],a.[LicensePath],b.[Status],a.[GradeLevel],a.[CreateTime] from dbo.MemStore a WITH(NOLOCK)	
inner join dbo.Member b  WITH(NOLOCK) on a.MemId=b.Id 
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.MemStore a with (nolock) inner join dbo.Member b  WITH(NOLOCK) on a.MemId=b.Id  " + where;

            IList< MemStoreEntity> entityList = new List< MemStoreEntity>();

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!String.IsNullOrEmpty(legalName))
            {
                db.AddInParameter(cmd, "@LegalName", DbType.String, "%" + legalName + "%");
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + companyName + "%");
            } 
            if (Status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, Status);
            }
            if (!string.IsNullOrEmpty(memcode))
            { 
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%" + memcode + "%");
            }
            if (!string.IsNullOrEmpty(contractname))
            { 
                db.AddInParameter(cmd, "@ContactsManName", DbType.String, "%" + contractname + "%");
            }
            if (!string.IsNullOrEmpty(contractphone))
            { 
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + contractphone + "%");
            }
            if (_issupplier != -1)
            { 
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, _issupplier);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(legalName))
            {
                db.AddInParameter(cmd, "@legalName", DbType.String, "%" + legalName + "%");
            }

            if (!string.IsNullOrEmpty(companyName))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + companyName + "%");
            }

            if (Status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, Status);
            }
            if (!string.IsNullOrEmpty(memcode))
            {
                db.AddInParameter(cmd, "@MemCode", DbType.String, "%" + memcode + "%");
            }
            if (!string.IsNullOrEmpty(contractname))
            {
                db.AddInParameter(cmd, "@ContactsManName", DbType.String, "%" + contractname + "%");
            }
            if (!string.IsNullOrEmpty(contractphone))
            {
                db.AddInParameter(cmd, "@MobilePhone", DbType.String, "%" + contractphone + "%");
            }
            if (_issupplier != -1)
            {
                db.AddInParameter(cmd, "@IsSupplier", DbType.Int32, _issupplier);
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

        public IList< MemStoreEntity> GetStoreNoPositionList(int pagesize, int pageindex, ref int recordCount )
        {
            string where = " WHERE b.Status=1 and  c.Id is null ";
             
            string sql = @"SELECT   MemCode,[Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 b.MemCode,a.[Id],a.[MemId],a.[StoreName],a.[CompanyName],a.[LegalName],a.[LegalMobilePhone],a.[LegalIdentityNo],a.[LegalIdentityPre],a.[LegalIdentityBeh],a.[MobilePhone],a.[ContactsManName],a.[Country],a.[ProvinceId],a.[CityId],a.[District],a.[Address],a.[Longitude],a.[Latitude],a.[LicenseNo],a.[LicensePath],b.[Status],a.[GradeLevel],a.[CreateTime] from dbo.MemStore a WITH(NOLOCK)	
inner join dbo.Member b  WITH(NOLOCK) on a.MemId=b.Id  left join [MemFromBaidu] c  WITH(NOLOCK) on a.MemId=c.MemId
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.MemStore a with (nolock) 
inner join dbo.Member b  WITH(NOLOCK) on a.MemId=b.Id left join [MemFromBaidu] c  WITH(NOLOCK) on a.MemId=c.MemId " + where;

            IList< MemStoreEntity> entityList = new List< MemStoreEntity>();

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
             
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
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

        public IList<UnitStr> ShowHasRegister(string str)
        {
            IList<UnitStr> list = new List<UnitStr>();
            string sql = @"SELECT w.value1 FROM dbo.Member a WITH(NOLOCK)  INNER JOIN dbo.MemStore b WITH(NOLOCK)  ON a.status=1 AND a.id=b.Id INNER JOIN 
   dbo.fun_SplitStrToTable(@StrPostions,'|',',')  w ON b.Longitude=w.value2
   AND b.Latitude=w.value3 ";
             
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@StrPostions", DbType.String, str); 

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    UnitStr entity = new UnitStr();
                    entity.Key = StringUtils.GetDbString(reader["value1"]);
                    list.Add(entity);
                }
            }
            return list;
        }
        public IList< MemStoreEntity> GetStoreListByMems(string memids)
        {
            string sql = @"  SELECT  MemCode, [MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],A.[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],A.[Status],[GradeLevel]
 from dbo.MemStore A WITH(NOLOCK) INNER JOIN dbo.Member B  WITH(NOLOCK) ON A.MemId=B.Id where MemId in (" + memids + ")" ;
						  
            IList< MemStoreEntity> entityList = new List< MemStoreEntity>();

            DbCommand cmd = db.GetSqlStringCommand(sql);
           
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public VWShowStoreEntity GetNextStore(int preid)
        {
            VWShowStoreEntity entity = new VWShowStoreEntity();
            string where = " WHERE a.status=1  AND b.status=1 AND b.istest=0 ";
            if(preid>0)
            {
                where += " and a.Id<@StoreId ";
            }
            string sql = @"  SELECT TOP 1 a.id as MemStoreId,StoreName, [CompanyName],a.CreateTime
 from dbo.MemStore A WITH(NOLOCK)  INNER JOIN  dbo.Member b ON  a.MemId=b.Id "+ where + " ORDER BY a.id desc ";

            IList< MemStoreEntity> entityList = new List< MemStoreEntity>();

            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (preid > 0)
            {
                db.AddInParameter(cmd, "@StoreId", DbType.Int32, preid);
             }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.StoreId = StringUtils.GetDbInt(reader["StoreId"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CreateTime = StringUtils.GetDateTime(reader["CreateTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList< MemStoreEntity> GetStoreAll()
        {

            string sql = @"SELECT    [Id],[MemId],[StoreName],[CompanyName],[LegalName],LegalMobilePhone,[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime] from dbo.MemStore WITH(NOLOCK)	";
            IList< MemStoreEntity> entityList = new List< MemStoreEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);

                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int ExistNum( MemStoreEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.MemStore WITH(NOLOCK) ";
            string where = "where  ";
            if (entity.Id == 0)
            {
                where = where + "   (StoreName=@StoreName) ";

            }
            else
            {
                where = where + " id<>@Id and  (StoreName=@StoreName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }

            db.AddInParameter(cmd, "@StoreName", DbType.String, entity.StoreName);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int ExistNum(string name,string address)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.MemStore WITH(NOLOCK) ";
            string where = "where MemStoreName=@StoreName and  Address=@Address ";
           
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@StoreName", DbType.String, name);
            db.AddInParameter(cmd, "@Address", DbType.String, address);
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
        public VWStoreEntity GetVWStoreByMemId(int memid)
        {
            string sql = @" SELECT  a.[Id] AS MemId
      ,[MemCode],[LastLoginTime],[MemGrade],[StoreName],[CompanyName],[LegalName],[LegalIdentityNo],[LegalIdentityPre]
      ,[LegalIdentityBeh],c.[MobilePhone] AS MemStoreMobile,c.ContactsManName,[Country],[ProvinceId],[CityId]
      ,[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],c.[Status],[GradeLevel]
      ,c.[CreateTime],b.[MemName],b.[IdentityNo],b.[IdentityNoCheck],b.[Sex],[Birthday],[Telephone]
      ,b.[MobilePhone],[MobilePhoneCheck],b.[Email],[EmailCheck],b.[QQ],[QQCheck] ,[WeChat] 
      ,[IdentityPre],[IdentityBeh],[IdentityAutoName],[IdentityAutoNo],LegalMobilePhone
  FROM  [dbo].[Member] a WITH(NOLOCK) LEFT JOIN dbo.MemberInfo b  WITH(NOLOCK)
  ON a.id=b.MemId LEFT JOIN dbo.MemStore c WITH(NOLOCK) ON a.id=c.MemId   	
							WHERE a.[Id]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            VWStoreEntity entity = new VWStoreEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]);
                    entity.LastLoginTime = StringUtils.GetDbDateTime(reader["LastLoginTime"]);
                    entity.MemGrade = StringUtils.GetDbInt(reader["MemGrade"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.StoreMobile = StringUtils.GetDbString(reader["StoreMobile"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
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
                    entity.WeChat = StringUtils.GetDbString(reader["WeChat"]); 
                    entity.IdentityPre = StringUtils.GetDbString(reader["IdentityPre"]);
                    entity.IdentityBeh = StringUtils.GetDbString(reader["IdentityBeh"]);
                    entity.IdentityAutoName = StringUtils.GetDbString(reader["IdentityAutoName"]);
                    entity.IdentityAutoNo = StringUtils.GetDbString(reader["IdentityAutoNo"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);


                }
            }
            return entity;
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public MemStoreEntity GetStoreByMemId(int memid)
        {
            string sql = @"SELECT top 1  [Id],[MemId],[StoreName],[CompanyName],[LegalName],LegalMobilePhone,[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
							FROM
							dbo.MemStore WITH(NOLOCK)	
							WHERE [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            MemStoreEntity entity = new MemStoreEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据法人代表名称进行模糊查询
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="LegalName"></param>
        /// <returns></returns>
        public IList< MemStoreEntity> GetStoreByLegalName(int pagesize, int pageindex, ref int recordCount, string LegalName)
        {

            string sql1 = @"SELECT [Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
                          FROM 
                          (SELECT ROW_NUMBER() OVER (Order By Id desc) AS ROWNUMBER,
                          [Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],
                          [ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime] from dbo.MemStore WITH(NOLOCK) where [LegalName] Like @LegalName) as temp where ROWNUMBER BETWEEN (( @PageIndex - 1 ) * @PageSize + 1 ) AND @PageSize * @PageIndex";

            string sql2 = @"Select count(1) from dbo.MemStore with(nolock)";

            DbCommand cmd = db.GetSqlStringCommand(sql1);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@LegalName", DbType.String, "%" + LegalName + "%");

            IList< MemStoreEntity> entitylist = new List< MemStoreEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entitylist.Add(entity);
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

            return entitylist;
        }


        public IList< MemStoreEntity> GetStoreByCompanyName(int pagesize, int pageindex, ref int recordCount, string CompanyName)
        {

            string sql1 = @"SELECT [Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
                          FROM 
                          (SELECT ROW_NUMBER() OVER (Order By Id desc) AS ROWNUMBER,
                          [Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],
                          [ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime] from dbo.MemStore WITH(NOLOCK) where [CompanyName] Like @CompanyName) as temp where ROWNUMBER BETWEEN (( @PageIndex - 1 ) * @PageSize + 1 ) AND @PageSize * @PageIndex";

            string sql2 = @"Select count(1) from dbo.MemStore with(nolock)";

            DbCommand cmd = db.GetSqlStringCommand(sql1);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + CompanyName + "%");

            IList< MemStoreEntity> entitylist = new List< MemStoreEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entitylist.Add(entity);
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

            return entitylist;
        }

        public IList< MemStoreEntity> GetStoreByStatus(int pagesize, int pageindex, ref int recordCount, int Status)
        {

            string sql1 = @"SELECT [Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],[ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime]
                          FROM 
                          (SELECT ROW_NUMBER() OVER (Order By Id desc) AS ROWNUMBER,
                          [Id],[MemId],[StoreName],[CompanyName],[LegalName],[LegalMobilePhone],[LegalIdentityNo],[LegalIdentityPre],[LegalIdentityBeh],[MobilePhone],[ContactsManName],[Country],
                          [ProvinceId],[CityId],[District],[Address],[Longitude],[Latitude],[LicenseNo],[LicensePath],[Status],[GradeLevel],[CreateTime] from dbo.MemStore WITH(NOLOCK) where [Status] Like @Status) as temp where ROWNUMBER BETWEEN (( @PageIndex - 1 ) * @PageSize + 1 ) AND @PageSize * @PageIndex";

            string sql2 = @"Select count(1) from dbo.MemStore with(nolock)";

            DbCommand cmd = db.GetSqlStringCommand(sql1);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@Status", DbType.Int32, Status);

            IList< MemStoreEntity> entitylist = new List< MemStoreEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemStoreEntity entity = new MemStoreEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.LegalName = StringUtils.GetDbString(reader["LegalName"]);
                    entity.LegalMobilePhone = StringUtils.GetDbString(reader["LegalMobilePhone"]);
                    entity.LegalIdentityNo = StringUtils.GetDbString(reader["LegalIdentityNo"]);
                    entity.LegalIdentityPre = StringUtils.GetDbString(reader["LegalIdentityPre"]);
                    entity.LegalIdentityBeh = StringUtils.GetDbString(reader["LegalIdentityBeh"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.Country = StringUtils.GetDbInt(reader["Country"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Longitude = StringUtils.GetDbDecimal(reader["Longitude"]);
                    entity.Latitude = StringUtils.GetDbDecimal(reader["Latitude"]);
                    entity.LicenseNo = StringUtils.GetDbString(reader["LicenseNo"]);
                    entity.LicensePath = StringUtils.GetDbString(reader["LicensePath"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.GradeLevel = StringUtils.GetDbInt(reader["GradeLevel"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entitylist.Add(entity);
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

            return entitylist;
        }


        /// <summary>
        /// 检查手机号码是否唯一
        /// </summary>
        /// <returns></returns>
        public int CheckPhoneIsExist(string mobilePhone)
        {
            string sql = @"SELECT COUNT(1) FROM dbo.MemStore WITH(NOLOCK)  WHERE MobilePhone=@MobilePhone AND STATUS<>4";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, mobilePhone);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return StringUtils.GetDbInt(result);
        }

    }
}
