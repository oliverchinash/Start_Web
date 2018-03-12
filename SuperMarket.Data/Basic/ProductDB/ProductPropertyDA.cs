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
功能描述：ProductProperty表的数据访问类。
创建时间：2016/10/31 16:28:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductPropertyEntity的数据访问操作
	/// </summary>
	public partial class ProductPropertyDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductPropertyDA Instance
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
            internal static readonly ProductPropertyDA instance = new ProductPropertyDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductProperty，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productProperty">待插入的实体对象</param>
		public int AddProductProperty(ProductPropertyEntity entity)
		{
		   string sql=@"insert into ProductProperty( [PropertyId],[PropertyName],[PropertyDetailId],[ProductId],[Sort],[IsSpec])VALUES
			            ( @PropertyId,@PropertyName,@PropertyDetailId,@ProductId,@Sort,@IsSpec);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@PropertyId",  DbType.Int32,entity.PropertyId);
			db.AddInParameter(cmd,"@PropertyName",  DbType.String,entity.PropertyName);
			db.AddInParameter(cmd,"@PropertyDetailId",  DbType.Int32,entity.PropertyDetailId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsSpec",  DbType.Int32,entity.IsSpec);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int ProcAddProperties(int properclassid,int productid,string strproperties)
        {
            string sql = @"EXEC Proc_ProductPropertiesEdit @ProperClassId,@ProductId,@PropertiesStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@ProperClassId", DbType.Int32, properclassid);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, strproperties); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="productProperty">待更新的实体对象</param>
        public   int UpdateProductProperty(ProductPropertyEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductProperty] SET
                       [PropertyId]=@PropertyId,[PropertyName]=@PropertyName,[PropertyDetailId]=@PropertyDetailId,[ProductId]=@ProductId,[Sort]=@Sort,[IsSpec]=@IsSpec
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@PropertyId",  DbType.Int32,entity.PropertyId);
			db.AddInParameter(cmd,"@PropertyName",  DbType.String,entity.PropertyName);
			db.AddInParameter(cmd,"@PropertyDetailId",  DbType.Int32,entity.PropertyDetailId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsSpec",  DbType.Int32,entity.IsSpec);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int EditProductProperty(int productid, int propertyid, int propertdetailid)
        {
            string sql = @"
                         DECLARE @prodid INT 
                        IF NOT EXISTS (SELECT 1 FROM dbo.[ProductProperty] WHERE PropertyId=@PropertyId AND [ProductId]=@ProductId )
                                BEGIN 
             insert into ProductProperty([PropertyId] 
           ,[PropertyDetailId]
           ,[ProductId]  )
     VALUES
           (@PropertyId 
           ,@PropertyDetailId 
           ,@ProductId  )               
                          END
                          ELSE
                          BEGIN
                              update ProductProperty set PropertyDetailId=@PropertyDetailId where PropertyId=@PropertyId AND [ProductId]=@ProductId  
                          END
                          SELECT id from ProductProperty where PropertyId=@PropertyId AND [ProductId]=@ProductId and PropertyDetailId=@PropertyDetailId 
                        ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PropertyId", DbType.Int32, propertyid);
            db.AddInParameter(cmd, "@PropertyDetailId", DbType.Int32, propertdetailid);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteProductPropertyByKey(int id)
	    {
			string sql=@"delete from ProductProperty where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductPropertyDisabled()
        {
            string sql = @"delete from  ProductProperty  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductPropertyByIds(string ids)
        {
            string sql = @"Delete from ProductProperty  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductPropertyByIds(string ids)
        {
            string sql = @"Update   ProductProperty set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductPropertyEntity GetProductProperty(int id)
		{
			string sql=@"SELECT  [Id],[PropertyId],[PropertyName],[PropertyDetailId],[ProductId],[Sort],[IsSpec]
							FROM
							dbo.[ProductProperty] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductPropertyEntity entity=new ProductPropertyEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.PropertyId=StringUtils.GetDbInt(reader["PropertyId"]);
					entity.PropertyName=StringUtils.GetDbString(reader["PropertyName"]);
					entity.PropertyDetailId=StringUtils.GetDbInt(reader["PropertyDetailId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsSpec=StringUtils.GetDbInt(reader["IsSpec"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductPropertyEntity> GetProductPropertyList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[PropertyId],[PropertyName],[PropertyDetailId],[ProductId],[Sort],[IsSpec]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[PropertyId],[PropertyName],[PropertyDetailId],[ProductId],[Sort],[IsSpec] from dbo.[ProductProperty] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductProperty] with (nolock) ";
            IList<ProductPropertyEntity> entityList = new List< ProductPropertyEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductPropertyEntity entity=new ProductPropertyEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.PropertyId=StringUtils.GetDbInt(reader["PropertyId"]);
					entity.PropertyName=StringUtils.GetDbString(reader["PropertyName"]);
					entity.PropertyDetailId=StringUtils.GetDbInt(reader["PropertyDetailId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsSpec=StringUtils.GetDbInt(reader["IsSpec"]);
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
        public IList<ProductPropertyEntity> GetPropertyByProductId(int productid)
        {
            string sql = @"SELECT    [Id],[PropertyId],[PropertyName],[PropertyDetailId],[ProductId],[Sort],[IsSpec] from dbo.[ProductProperty] WITH(NOLOCK)
where ProductId=@ProductId ";
            IList<ProductPropertyEntity> entityList = new List<ProductPropertyEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductPropertyEntity entity = new ProductPropertyEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.PropertyId = StringUtils.GetDbInt(reader["PropertyId"]);
                    entity.PropertyName = StringUtils.GetDbString(reader["PropertyName"]);
                    entity.PropertyDetailId = StringUtils.GetDbInt(reader["PropertyDetailId"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsSpec = StringUtils.GetDbInt(reader["IsSpec"]);
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
        public IList<ProductPropertyEntity> GetProductPropertyAll()
        {

            string sql = @"SELECT    [Id],[PropertyId],[PropertyName],[PropertyDetailId],[ProductId],[Sort],[IsSpec] from dbo.[ProductProperty] WITH(NOLOCK)	";
		    IList<ProductPropertyEntity> entityList = new List<ProductPropertyEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductPropertyEntity entity=new ProductPropertyEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.PropertyId=StringUtils.GetDbInt(reader["PropertyId"]);
					entity.PropertyName=StringUtils.GetDbString(reader["PropertyName"]);
					entity.PropertyDetailId=StringUtils.GetDbInt(reader["PropertyDetailId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsSpec=StringUtils.GetDbInt(reader["IsSpec"]);
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
        public int  ExistNum(ProductPropertyEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductProperty] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (PropertyName=@PropertyName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (PropertyName=@PropertyName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@PropertyName", DbType.String, entity.PropertyName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
