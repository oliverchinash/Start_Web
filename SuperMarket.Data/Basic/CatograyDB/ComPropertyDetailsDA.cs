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
功能描述：ComPropertyDetails表的数据访问类。
创建时间：2016/10/31 13:05:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// ComPropertyDetailsEntity的数据访问操作
	/// </summary>
	public partial class ComPropertyDetailsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ComPropertyDetailsDA Instance
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
            internal static readonly ComPropertyDetailsDA instance = new ComPropertyDetailsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ComPropertyDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="comPropertyDetails">待插入的实体对象</param>
		public int AddComPropertyDetails(ComPropertyDetailsEntity entity)
		{
		   string sql=@"insert into ComPropertyDetails( [Code],[Name],[ParentId],[IsEnd],[PicUrl],[Sort],[ComPropertyId])VALUES
			            ( @Code,@Name,@ParentId,@IsEnd,@PicUrl,@Sort,@ComPropertyId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsEnd",  DbType.Int32,entity.IsEnd);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ComPropertyId",  DbType.Int32,entity.ComPropertyId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="comPropertyDetails">待更新的实体对象</param>
		public   int UpdateComPropertyDetails(ComPropertyDetailsEntity entity)
		{
			string sql=@" UPDATE dbo.[ComPropertyDetails] SET
                       [Code]=@Code,[Name]=@Name,[ParentId]=@ParentId,[IsEnd]=@IsEnd,[PicUrl]=@PicUrl,[Sort]=@Sort,[ComPropertyId]=@ComPropertyId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsEnd",  DbType.Int32,entity.IsEnd);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ComPropertyId",  DbType.Int32,entity.ComPropertyId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteComPropertyDetailsByKey(int id)
	    {
			string sql=@"delete from ComPropertyDetails where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteComPropertyDetailsDisabled()
        {
            string sql = @"delete from  ComPropertyDetails  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteComPropertyDetailsByIds(string ids)
        {
            string sql = @"Delete from ComPropertyDetails  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableComPropertyDetailsByIds(string ids)
        {
            string sql = @"Update   ComPropertyDetails set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ComPropertyDetailsEntity GetComPropertyDetails(int id)
		{
			string sql=@"SELECT  [Id],[Code],[Name],[ParentId],[IsEnd],[PicUrl],[Sort],[ComPropertyId]
							FROM
							dbo.[ComPropertyDetails] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ComPropertyDetailsEntity entity=new ComPropertyDetailsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsEnd=StringUtils.GetDbInt(reader["IsEnd"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ComPropertyId=StringUtils.GetDbInt(reader["ComPropertyId"]);
				}
   		    }
            return entity;
		}
        public IList<ComPropertyDetailsEntity> GetListByPropertyId(int propertyid, int pid)
        {
            string sql = @"SELECT  [Id],[Code],[Name],[ParentId],[IsEnd],[PicUrl],[Sort],[ComPropertyId]
							FROM
							dbo.[ComPropertyDetails] WITH(NOLOCK)	
							WHERE [ComPropertyId]=@PropertyId and ParentId=@ParentId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PropertyId", DbType.Int32, propertyid);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
            IList<ComPropertyDetailsEntity> entityList = new List<ComPropertyDetailsEntity>(); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ComPropertyDetailsEntity entity = new ComPropertyDetailsEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.ComPropertyId = StringUtils.GetDbInt(reader["ComPropertyId"]);
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
        public   IList<ComPropertyDetailsEntity> GetComPropertyDetailsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[Name],[ParentId],[IsEnd],[PicUrl],[Sort],[ComPropertyId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[ParentId],[IsEnd],[PicUrl],[Sort],[ComPropertyId] from dbo.[ComPropertyDetails] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ComPropertyDetails] with (nolock) ";
            IList<ComPropertyDetailsEntity> entityList = new List< ComPropertyDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ComPropertyDetailsEntity entity=new ComPropertyDetailsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsEnd=StringUtils.GetDbInt(reader["IsEnd"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ComPropertyId=StringUtils.GetDbInt(reader["ComPropertyId"]);
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
        public IList<ComPropertyDetailsEntity> GetComPropertyDetailsAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[ParentId],[IsEnd],[PicUrl],[Sort],[ComPropertyId] from dbo.[ComPropertyDetails] WITH(NOLOCK)	";
		    IList<ComPropertyDetailsEntity> entityList = new List<ComPropertyDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ComPropertyDetailsEntity entity=new ComPropertyDetailsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsEnd=StringUtils.GetDbInt(reader["IsEnd"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ComPropertyId=StringUtils.GetDbInt(reader["ComPropertyId"]);
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
        public int  ExistNum(ComPropertyDetailsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ComPropertyDetails] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (Name=@Name) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (Name=@Name) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
