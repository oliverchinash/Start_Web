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
功能描述：BasicSiteProDetails表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// BasicSiteProDetailsEntity的数据访问操作
	/// </summary>
	public partial class BasicSiteProDetailsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static BasicSiteProDetailsDA Instance
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
            internal static readonly BasicSiteProDetailsDA instance = new BasicSiteProDetailsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表BasicSiteProDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="BasicSiteProDetails">待插入的实体对象</param>
		public int AddBasicSiteProDetails(BasicSiteProDetailsEntity entity)
		{
		   string sql=@"insert into BasicSiteProDetails( [Code],[Name],[PicUrl],[PropertyId],[Status],[ParentId],[Active],[Sort])VALUES
			            ( @Code,@Name,@PicUrl,@PropertyId,@Status,@ParentId,@Active,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@PropertyId",  DbType.Int32,entity.PropertyId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@Active",  DbType.Int32,entity.Active);
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
		/// <param name="BasicSiteProDetails">待更新的实体对象</param>
		public   int UpdateBasicSiteProDetails(BasicSiteProDetailsEntity entity)
		{
			string sql=@" UPDATE dbo.[BasicSiteProDetails] SET
                       [Code]=@Code,[Name]=@Name,[PicUrl]=@PicUrl,[PropertyId]=@PropertyId,[Status]=@Status,[ParentId]=@ParentId,[Active]=@Active,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@PropertyId",  DbType.Int32,entity.PropertyId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@Active",  DbType.Int32,entity.Active);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteBasicSiteProDetailsByKey(int id)
	    {
			string sql=@"delete from BasicSiteProDetails where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBasicSiteProDetailsDisabled()
        {
            string sql = @"delete from  BasicSiteProDetails  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBasicSiteProDetailsByIds(string ids)
        {
            string sql = @"Delete from BasicSiteProDetails  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBasicSiteProDetailsByIds(string ids)
        {
            string sql = @"Update   BasicSiteProDetails set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   BasicSiteProDetailsEntity GetBasicSiteProDetails(int id)
		{
			string sql=@"SELECT  [Id],[Code],[Name],[PicUrl],[PropertyId],[Status],[ParentId],[Active],[Sort]
							FROM
							dbo.[BasicSiteProDetails] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		BasicSiteProDetailsEntity entity=new BasicSiteProDetailsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PropertyId=StringUtils.GetDbInt(reader["PropertyId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Active=StringUtils.GetDbInt(reader["Active"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}

        public int GetAndAddProPropertDetailId(int properyid, string prodetailname)
        {
            string sql = @"
                         DECLARE @prodid INT 
                        IF NOT EXISTS (SELECT 1 FROM dbo.BasicSiteProDetails WHERE PropertyId=@PropertyId AND Name=@Name )
                                                    BEGIN 
                                                     INSERT INTO [JcCatograyDB].[dbo].[BasicSiteProDetails]
                                                               ([Code]
                                                               ,[Name]
                                                               ,[PicUrl]
                                                               ,[PropertyId]
                                                               ,[Status]
                                                               ,[ParentId]
                                                               ,[Active]
                                                               ,[Sort])
                                                         VALUES(''
                                                               ,@Name
                                                               ,''
                                                               ,@PropertyId
                                                               ,1
                                                               ,0
                                                               ,1
                                                               ,0)
                                                               SET @prodid=@@IDENTITY
                          END
                          ELSE
                          BEGIN
                          SELECT @prodid=Id FROM dbo.BasicSiteProDetails WHERE PropertyId=@PropertyId AND Name=@Name
                          END
                          SELECT @prodid
                        ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PropertyId", DbType.Int32, properyid);
            db.AddInParameter(cmd, "@Name", DbType.String, prodetailname);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<BasicSiteProDetailsEntity> GetBasicSiteProDetailsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[Name],[PicUrl],[PropertyId],[Status],[ParentId],[Active],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[PicUrl],[PropertyId],[Status],[ParentId],[Active],[Sort] from dbo.[BasicSiteProDetails] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[BasicSiteProDetails] with (nolock) ";
            IList<BasicSiteProDetailsEntity> entityList = new List< BasicSiteProDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					BasicSiteProDetailsEntity entity=new BasicSiteProDetailsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PropertyId=StringUtils.GetDbInt(reader["PropertyId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Active=StringUtils.GetDbInt(reader["Active"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
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
        public IList<BasicSiteProDetailsEntity> GetListByPropertyId(int propertyid, int parentid)
        {
            string sql = @"SELECT    [Id],[Code],[Name],[PicUrl],[PropertyId],[Status],[ParentId],[Active],[Sort] from dbo.[BasicSiteProDetails]
WITH(NOLOCK)	where PropertyId=@PropertyId and ParentId=@ParentId";
            IList<BasicSiteProDetailsEntity> entityList = new List<BasicSiteProDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PropertyId", DbType.Int32, propertyid);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BasicSiteProDetailsEntity entity = new BasicSiteProDetailsEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PropertyId = StringUtils.GetDbInt(reader["PropertyId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.Active = StringUtils.GetDbInt(reader["Active"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<BasicSiteProDetailsEntity> GetProDetailsBySiteId(int siteid)
        {
            string sql = @"SELECT    a.[Id],a.[Code],a.[Name],a.[PicUrl],a.[PropertyId],a.[Status],a.[ParentId],a.[Active],a.[Sort] from dbo.[BasicSiteProDetails] a
WITH(NOLOCK)	INNER JOIN dbo.BasicSiteProperties b WITH(NOLOCK) ON a.PropertyId=b.Id
WHERE b.SiteId=@SiteId AND Active=1 Order By a.PropertyId,a.[Sort] desc";
            IList<BasicSiteProDetailsEntity> entityList = new List<BasicSiteProDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BasicSiteProDetailsEntity entity = new BasicSiteProDetailsEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PropertyId = StringUtils.GetDbInt(reader["PropertyId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.Active = StringUtils.GetDbInt(reader["Active"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<BasicSiteProDetailsEntity> GetBasicSiteProDetailsAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[PicUrl],[PropertyId],[Status],[ParentId],[Active],[Sort] from dbo.[BasicSiteProDetails] WITH(NOLOCK)	";
		    IList<BasicSiteProDetailsEntity> entityList = new List<BasicSiteProDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   BasicSiteProDetailsEntity entity=new BasicSiteProDetailsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PropertyId=StringUtils.GetDbInt(reader["PropertyId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Active=StringUtils.GetDbInt(reader["Active"]);
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
        public int  ExistNum(BasicSiteProDetailsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[BasicSiteProDetails] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (Name=@Name) and  PropertyId=@PropertyId ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (Name=@Name and  PropertyId=@PropertyId) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name); 
            db.AddInParameter(cmd, "@PropertyId", DbType.Int32, entity.PropertyId);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
