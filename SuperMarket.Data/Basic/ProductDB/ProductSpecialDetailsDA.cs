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
功能描述：ProductSpecialDetails表的数据访问类。
创建时间：2017/5/16 11:30:29
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductSpecialDetailsEntity的数据访问操作
	/// </summary>
	public partial class ProductSpecialDetailsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductSpecialDetailsDA Instance
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
            internal static readonly ProductSpecialDetailsDA instance = new ProductSpecialDetailsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductSpecialDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productSpecialDetails">待插入的实体对象</param>
		public int AddProductSpecialDetails(ProductSpecialDetailsEntity entity)
		{
		   string sql=@"insert into ProductSpecialDetails( [SpecialId],[ProductDetailId],[Sort],[IsActive])VALUES
			            ( @SpecialId,@ProductDetailId,@Sort,@IsActive);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@SpecialId",  DbType.Int32,entity.SpecialId);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productSpecialDetails">待更新的实体对象</param>
		public   int UpdateProductSpecialDetails(ProductSpecialDetailsEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductSpecialDetails] SET
                       [SpecialId]=@SpecialId,[ProductDetailId]=@ProductDetailId,[Sort]=@Sort,[IsActive]=@IsActive
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@SpecialId",  DbType.Int32,entity.SpecialId);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductSpecialDetailsByKey(int id)
	    {
			string sql=@"delete from ProductSpecialDetails where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSpecialDetailsDisabled()
        {
            string sql = @"delete from  ProductSpecialDetails  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSpecialDetailsByIds(string ids)
        {
            string sql = @"Delete from ProductSpecialDetails  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSpecialDetailsByIds(string ids)
        {
            string sql = @"Update   ProductSpecialDetails set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductSpecialDetailsEntity GetProductSpecialDetails(int id)
		{
			string sql=@"SELECT  [Id],[SpecialId],[ProductDetailId],[Sort],[IsActive]
							FROM
							dbo.[ProductSpecialDetails] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductSpecialDetailsEntity entity=new ProductSpecialDetailsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SpecialId=StringUtils.GetDbInt(reader["SpecialId"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<VWProductSpecialDetailsEntity> GetProductSpecialDetailsList( int pageindex, int pagesize, ref  int recordCount,int specialid,int isactive  )
		{
            string where = " where 1=1 ";
            if(specialid > 0)
            {
                where += " and  SpecialId=@SpecialId ";
            }
            if (isactive > 0)
            {
                where += " and  IsActive=@IsActive ";
            }
            string sql=@"SELECT   [Id],[SpecialId],[ProductDetailId],[Sort],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Sort desc) AS ROWNUMBER,
						 [Id],[SpecialId],[ProductDetailId],[Sort],[IsActive] from dbo.[ProductSpecialDetails] WITH(NOLOCK)	
						"+ where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductSpecialDetails] with (nolock) "+ where;
            IList<VWProductSpecialDetailsEntity> entityList = new List<VWProductSpecialDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (specialid > 0)
            {
                db.AddInParameter(cmd, "@SpecialId", DbType.Int32, specialid);
            }
            if (isactive > 0)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductSpecialDetailsEntity entity =new VWProductSpecialDetailsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SpecialId=StringUtils.GetDbInt(reader["SpecialId"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (specialid > 0)
            {
                db.AddInParameter(cmd, "@SpecialId", DbType.Int32, specialid);
            }
            if (isactive > 0)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
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
        public IList<VWProductSpecialDetailsEntity> GetSpecialDetailsForMenuAD( int specialid, int num)
        {
            string where = " where 1=1 and  IsActive=1 ";
            if (specialid > 0)
            {
                where += " and  SpecialId=@SpecialId ";
            } 
            string sql = @"SELECT top "+ num+@" [Id],[SpecialId],[ProductDetailId],[Sort],[IsActive] from dbo.[ProductSpecialDetails] WITH(NOLOCK)	
						" + where + " ORDER BY Sort desc";
             
            IList<VWProductSpecialDetailsEntity> entityList = new List<VWProductSpecialDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (specialid > 0)
            {
                db.AddInParameter(cmd, "@SpecialId", DbType.Int32, specialid);
            }
            
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductSpecialDetailsEntity entity = new VWProductSpecialDetailsEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.SpecialId = StringUtils.GetDbInt(reader["SpecialId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
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
        public IList<ProductSpecialDetailsEntity> GetProductSpecialDetailsAll()
        {

            string sql = @"SELECT    [Id],[SpecialId],[ProductDetailId],[Sort],[IsActive] from dbo.[ProductSpecialDetails] WITH(NOLOCK)	";
		    IList<ProductSpecialDetailsEntity> entityList = new List<ProductSpecialDetailsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductSpecialDetailsEntity entity=new ProductSpecialDetailsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SpecialId=StringUtils.GetDbInt(reader["SpecialId"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(ProductSpecialDetailsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductSpecialDetails] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
				 
            }
            else
            {
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
