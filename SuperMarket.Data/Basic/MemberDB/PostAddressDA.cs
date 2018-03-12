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
功能描述：PostAddress表的数据访问类。
创建时间：2016/8/1 14:58:49
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// PostAddressEntity的数据访问操作
	/// </summary>
	public partial class PostAddressDA: BaseSuperMarketDB
    {
        #region 实例化
        public static PostAddressDA Instance
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
            internal static readonly PostAddressDA instance = new PostAddressDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表PostAddress，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="postAddress">待插入的实体对象</param>
		public int AddPostAddress(PostAddressEntity entity)
		{
		   string sql= @"insert into PostAddress( [MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort])VALUES
			            ( @MemId,@AccepterName,@CtType,@CountryCode,@CountryName,@ProvinceId,@CityId,@District,@Address,@Email,@Telephone,@MobilePhone,@IsDefault,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@AccepterName",  DbType.String,entity.AccepterName);
			db.AddInParameter(cmd,"@CtType",  DbType.Int32,entity.CtType);
			db.AddInParameter(cmd,"@CountryCode",  DbType.String,entity.CountryCode);
			db.AddInParameter(cmd,"@CountryName",  DbType.String,entity.CountryName);
			db.AddInParameter(cmd, "@ProvinceId",  DbType.Int32, entity.ProvinceId);
			db.AddInParameter(cmd, "@CityId",  DbType.Int32, entity.CityId);
			db.AddInParameter(cmd,"@District",  DbType.String,entity.District);
			db.AddInParameter(cmd,"@Address",  DbType.String,entity.Address);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@Telephone",  DbType.String,entity.Telephone);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@IsDefault",  DbType.Int32,entity.IsDefault);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
        

		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="postAddress">待更新的实体对象</param>
		public   int UpdatePostAddress(PostAddressEntity entity)
		{
			string sql= @" UPDATE dbo.[PostAddress] SET
                       [AccepterName]=@AccepterName,[CtType]=@CtType,[CountryCode]=@CountryCode,[CountryName]=@CountryName,[ProvinceId]=@ProvinceId,[CityId]=@CityId,[District]=@District,[Address]=@Address,[Email]=@Email,[Telephone]=@Telephone,[MobilePhone]=@MobilePhone,[IsDefault]=@IsDefault,[Sort]=@Sort
                       WHERE [Id]=@id and [MemId]=@MemId";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@AccepterName",  DbType.String,entity.AccepterName);
			db.AddInParameter(cmd,"@CtType",  DbType.Int32,entity.CtType);
			db.AddInParameter(cmd,"@CountryCode",  DbType.String,entity.CountryCode);
			db.AddInParameter(cmd,"@CountryName",  DbType.String,entity.CountryName);
			db.AddInParameter(cmd, "@ProvinceId",  DbType.String,entity.ProvinceId);
			db.AddInParameter(cmd, "@CityId",  DbType.String,entity.CityId);
			db.AddInParameter(cmd,"@District",  DbType.String,entity.District);
			db.AddInParameter(cmd,"@Address",  DbType.String,entity.Address);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@Telephone",  DbType.String,entity.Telephone);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@IsDefault",  DbType.Int32,entity.IsDefault);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}
        public int SetDefault(int id,int memid)
        {
            string sql = @"UPDATE dbo.[PostAddress] SET IsDefault=0 where  [MemId]=@MemId;UPDATE dbo.[PostAddress] SET IsDefault =1 where Id=@id and  [MemId]=@MemId;  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd,"@Id",DbType.Int32,id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
            /// <summary>
            /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
            /// </summary>
        public  int  DeletePostAddressByKey(int id, int memid)
	    {
			string sql= @"delete from PostAddress where  Id=@Id and  [MemId]=@MemId";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePostAddressDisabled()
        {
            string sql = @"delete from  PostAddress  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePostAddressByIds(string ids)
        {
            string sql = @"Delete from PostAddress  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);           
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePostAddressByIds(string ids)
        {
            string sql = @"Update   PostAddress set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }

        public int SetDefaultPostAddress(int id)
        {
            string sql = @"Update PostAddress set IsDefault=0 ;
            Update PostAddress set IsDefault=1 where id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        public PostAddressEntity GetDefaultAddress(int memid)
        {
            string sql = @"SELECT  TOP 1  [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort]
							FROM
							dbo.[PostAddress] WITH(NOLOCK)	
							WHERE [MemId]=@MemId Order By IsDefault DESC";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            PostAddressEntity entity = new PostAddressEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.CtType = StringUtils.GetDbInt(reader["CtType"]);
                    entity.CountryCode = StringUtils.GetDbString(reader["CountryCode"]);
                    entity.CountryName = StringUtils.GetDbString(reader["CountryName"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.Telephone = StringUtils.GetDbString(reader["Telephone"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   PostAddressEntity GetPostAddress(int id)
		{
			string sql= @"SELECT  [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort]
							FROM
							dbo.[PostAddress] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		PostAddressEntity entity=new PostAddressEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AccepterName=StringUtils.GetDbString(reader["AccepterName"]);
					entity.CtType=StringUtils.GetDbInt(reader["CtType"]);
					entity.CountryCode=StringUtils.GetDbString(reader["CountryCode"]);
					entity.CountryName=StringUtils.GetDbString(reader["CountryName"]);
					entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
					entity.District=StringUtils.GetDbString(reader["District"]);
					entity.Address=StringUtils.GetDbString(reader["Address"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}
        public PostAddressEntity GetDefaultPostAddress(int memid)
        {
            string sql = @"SELECT top 1  [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort]
							FROM
							dbo.[PostAddress] WITH(NOLOCK)	
							WHERE [MemId]=@MemId Order By  [IsDefault] desc,[Sort] desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            PostAddressEntity entity = new PostAddressEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.CtType = StringUtils.GetDbInt(reader["CtType"]);
                    entity.CountryCode = StringUtils.GetDbString(reader["CountryCode"]);
                    entity.CountryName = StringUtils.GetDbString(reader["CountryName"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.Telephone = StringUtils.GetDbString(reader["Telephone"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<PostAddressEntity> GetPostAddressList(int pagesize, int pageindex, ref  int recordCount ,int memid)//pagesize默认为5 
		{
            string where = " WHERE  MemId=@MemId ";

			string sql= @"SELECT [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort]
						 FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY IsDefault desc, Id desc) AS ROWNUMBER,
						 [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort] from dbo.[PostAddress] WITH(NOLOCK)	
						 " + where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[PostAddress] with (nolock) "+ where;
            IList<PostAddressEntity> entityList = new List< PostAddressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
		    db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					PostAddressEntity entity=new PostAddressEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AccepterName=StringUtils.GetDbString(reader["AccepterName"]);
					entity.CtType=StringUtils.GetDbInt(reader["CtType"]);
					entity.CountryCode=StringUtils.GetDbString(reader["CountryCode"]);
					entity.CountryName=StringUtils.GetDbString(reader["CountryName"]);
					entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
					entity.District=StringUtils.GetDbString(reader["District"]);
					entity.Address=StringUtils.GetDbString(reader["Address"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
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
        public IList<PostAddressEntity> GetPostAddressAll()
        {

            string sql = @"SELECT    [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort] from dbo.[PostAddress] WITH(NOLOCK)	";
		    IList<PostAddressEntity> entityList = new List<PostAddressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   PostAddressEntity entity=new PostAddressEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AccepterName=StringUtils.GetDbString(reader["AccepterName"]);
					entity.CtType=StringUtils.GetDbInt(reader["CtType"]);
					entity.CountryCode=StringUtils.GetDbString(reader["CountryCode"]);
					entity.CountryName=StringUtils.GetDbString(reader["CountryName"]);
					entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
					entity.District=StringUtils.GetDbString(reader["District"]);
					entity.Address=StringUtils.GetDbString(reader["Address"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
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
        public int  ExistNum(PostAddressEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PostAddress] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (AccepterName=@AccepterName) ";
					     where = where+ "  (CountryName=@CountryName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (AccepterName=@AccepterName) ";
					     where = where+ " id<>@Id and  (CountryName=@CountryName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@AccepterName", DbType.String, entity.AccepterName); 
					
            db.AddInParameter(cmd, "@CountryName", DbType.String, entity.CountryName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        public int GetNumForAddress(int memid)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PostAddress] WITH(NOLOCK) where MemId=@MemId "; 
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        public IList<PostAddressEntity> GetPostListByMemId(int memid)
        {
            string sql = @"SELECT  [Id],[MemId],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[District],[Address],[Email],[Telephone],[MobilePhone],[IsDefault],[Sort] from dbo.[PostAddress] WITH(NOLOCK)	WHERE [MemId]=@memid";
            IList<PostAddressEntity> entityList = new List<PostAddressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd,"@MemId",DbType.Int32,memid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    PostAddressEntity entity = new PostAddressEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.CtType = StringUtils.GetDbInt(reader["CtType"]);
                    entity.CountryCode = StringUtils.GetDbString(reader["CountryCode"]);
                    entity.CountryName = StringUtils.GetDbString(reader["CountryName"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.District = StringUtils.GetDbString(reader["District"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.Telephone = StringUtils.GetDbString(reader["Telephone"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entityList.Add(entity);
                }
            }
            return entityList ;
        }




        #endregion
        #endregion
    }
}
