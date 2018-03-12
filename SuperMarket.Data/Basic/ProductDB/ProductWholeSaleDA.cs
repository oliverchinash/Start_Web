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
功能描述：ProductWholeSale表的数据访问类。
创建时间：2016/8/16 13:54:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductWholeSaleEntity的数据访问操作
	/// </summary>
	public partial class ProductWholeSaleDA: BaseSuperMarketDB
	{
        #region 实例化
        public static ProductWholeSaleDA Instance
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
            internal static readonly ProductWholeSaleDA instance = new ProductWholeSaleDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductWholeSale，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productWholeSale">待插入的实体对象</param>
		public int AddProductWholeSale(ProductWholeSaleEntity entity)
		{
		   string sql=@"insert into ProductWholeSale( [ProductId],[BatchNum1],[Price1],[BatchNum2],[Price2],[BatchNum3],[Price3])VALUES
			            ( @ProductId,@BatchNum1,@Price1,@BatchNum2,@Price2,@BatchNum3,@Price3);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@BatchNum1",  DbType.Int32,entity.BatchNum1);
			db.AddInParameter(cmd,"@Price1",  DbType.Decimal,entity.Price1);
			db.AddInParameter(cmd,"@BatchNum2",  DbType.Int32,entity.BatchNum2);
			db.AddInParameter(cmd,"@Price2",  DbType.Decimal,entity.Price2);
			db.AddInParameter(cmd,"@BatchNum3",  DbType.Int32,entity.BatchNum3);
			db.AddInParameter(cmd,"@Price3",  DbType.Decimal,entity.Price3);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productWholeSale">待更新的实体对象</param>
		public   int UpdateProductWholeSale(ProductWholeSaleEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductWholeSale] SET
                       [ProductId]=@ProductId,[BatchNum1]=@BatchNum1,[Price1]=@Price1,[BatchNum2]=@BatchNum2,[Price2]=@Price2,[BatchNum3]=@BatchNum3,[Price3]=@Price3
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@BatchNum1",  DbType.Int32,entity.BatchNum1);
			db.AddInParameter(cmd,"@Price1",  DbType.Decimal,entity.Price1);
			db.AddInParameter(cmd,"@BatchNum2",  DbType.Int32,entity.BatchNum2);
			db.AddInParameter(cmd,"@Price2",  DbType.Decimal,entity.Price2);
			db.AddInParameter(cmd,"@BatchNum3",  DbType.Int32,entity.BatchNum3);
			db.AddInParameter(cmd,"@Price3",  DbType.Decimal,entity.Price3);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductWholeSaleByKey(int id)
	    {
			string sql=@"delete from ProductWholeSale where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductWholeSaleDisabled()
        {
            string sql = @"delete from  ProductWholeSale  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductWholeSaleByIds(string ids)
        {
            string sql = @"Delete from ProductWholeSale  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductWholeSaleByIds(string ids)
        {
            string sql = @"Update   ProductWholeSale set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductWholeSaleEntity GetProductWholeSale(int id)
		{
			string sql=@"SELECT  [Id],[ProductId],[BatchNum1],[Price1],[BatchNum2],[Price2],[BatchNum3],[Price3]
							FROM
							dbo.[ProductWholeSale] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductWholeSaleEntity entity=new ProductWholeSaleEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.BatchNum1=StringUtils.GetDbInt(reader["BatchNum1"]);
					entity.Price1=StringUtils.GetDbDecimal(reader["Price1"]);
					entity.BatchNum2=StringUtils.GetDbInt(reader["BatchNum2"]);
					entity.Price2=StringUtils.GetDbDecimal(reader["Price2"]);
					entity.BatchNum3=StringUtils.GetDbInt(reader["BatchNum3"]);
					entity.Price3=StringUtils.GetDbDecimal(reader["Price3"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductWholeSaleEntity> GetProductWholeSaleList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductId],[BatchNum1],[Price1],[BatchNum2],[Price2],[BatchNum3],[Price3]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductId],[BatchNum1],[Price1],[BatchNum2],[Price2],[BatchNum3],[Price3] from dbo.[ProductWholeSale] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductWholeSale] with (nolock) ";
            IList<ProductWholeSaleEntity> entityList = new List< ProductWholeSaleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductWholeSaleEntity entity=new ProductWholeSaleEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.BatchNum1=StringUtils.GetDbInt(reader["BatchNum1"]);
					entity.Price1=StringUtils.GetDbDecimal(reader["Price1"]);
					entity.BatchNum2=StringUtils.GetDbInt(reader["BatchNum2"]);
					entity.Price2=StringUtils.GetDbDecimal(reader["Price2"]);
					entity.BatchNum3=StringUtils.GetDbInt(reader["BatchNum3"]);
					entity.Price3=StringUtils.GetDbDecimal(reader["Price3"]);
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
        public IList<ProductWholeSaleEntity> GetProductWholeSaleAll()
        {

            string sql = @"SELECT    [Id],[ProductId],[BatchNum1],[Price1],[BatchNum2],[Price2],[BatchNum3],[Price3] from dbo.[ProductWholeSale] WITH(NOLOCK)	";
		    IList<ProductWholeSaleEntity> entityList = new List<ProductWholeSaleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductWholeSaleEntity entity=new ProductWholeSaleEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.BatchNum1=StringUtils.GetDbInt(reader["BatchNum1"]);
					entity.Price1=StringUtils.GetDbDecimal(reader["Price1"]);
					entity.BatchNum2=StringUtils.GetDbInt(reader["BatchNum2"]);
					entity.Price2=StringUtils.GetDbDecimal(reader["Price2"]);
					entity.BatchNum3=StringUtils.GetDbInt(reader["BatchNum3"]);
					entity.Price3=StringUtils.GetDbDecimal(reader["Price3"]);
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
        public int  ExistNum(ProductWholeSaleEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductWholeSale] WITH(NOLOCK) ";
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
