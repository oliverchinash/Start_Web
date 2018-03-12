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
功能描述：ProductStyleCar表的数据访问类。
创建时间：2016/8/19 14:49:31
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductStyleCarEntity的数据访问操作
	/// </summary>
	public partial class ProductStyleCarDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductStyleCarDA Instance
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
            internal static readonly ProductStyleCarDA instance = new ProductStyleCarDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductStyleCar，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productStyleCar">待插入的实体对象</param>
		public int AddProductStyleCar(ProductStyleCarEntity entity)
		{
		   string sql=@"insert into ProductStyleCar( [StyleId],[BrandCar1],[BrandCar2],[BrandCar3],[BrandCar4],[CreateTime])VALUES
			            ( @StyleId,@BrandCar1,@BrandCar2,@BrandCar3,@BrandCar4,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@BrandCar1",  DbType.Int32,entity.BrandCar1);
			db.AddInParameter(cmd,"@BrandCar2",  DbType.Int32,entity.BrandCar2);
			db.AddInParameter(cmd,"@BrandCar3",  DbType.Int32,entity.BrandCar3);
			db.AddInParameter(cmd,"@BrandCar4",  DbType.Int32,entity.BrandCar4);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productStyleCar">待更新的实体对象</param>
		public   int UpdateProductStyleCar(ProductStyleCarEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductStyleCar] SET
                       [StyleId]=@StyleId,[BrandCar1]=@BrandCar1,[BrandCar2]=@BrandCar2,[BrandCar3]=@BrandCar3,[BrandCar4]=@BrandCar4,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@BrandCar1",  DbType.Int32,entity.BrandCar1);
			db.AddInParameter(cmd,"@BrandCar2",  DbType.Int32,entity.BrandCar2);
			db.AddInParameter(cmd,"@BrandCar3",  DbType.Int32,entity.BrandCar3);
			db.AddInParameter(cmd,"@BrandCar4",  DbType.Int32,entity.BrandCar4);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductStyleCarByKey(int id)
	    {
			string sql=@"delete from ProductStyleCar where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductStyleCarDisabled()
        {
            string sql = @"delete from  ProductStyleCar  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductStyleCarByIds(string ids)
        {
            string sql = @"Delete from ProductStyleCar  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductStyleCarByIds(string ids)
        {
            string sql = @"Update   ProductStyleCar set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductStyleCarEntity GetProductStyleCar(int id)
		{
			string sql=@"SELECT  [Id],[StyleId],[BrandCar1],[BrandCar2],[BrandCar3],[BrandCar4],[CreateTime]
							FROM
							dbo.[ProductStyleCar] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductStyleCarEntity entity=new ProductStyleCarEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.BrandCar1=StringUtils.GetDbInt(reader["BrandCar1"]);
					entity.BrandCar2=StringUtils.GetDbInt(reader["BrandCar2"]);
					entity.BrandCar3=StringUtils.GetDbInt(reader["BrandCar3"]);
					entity.BrandCar4=StringUtils.GetDbInt(reader["BrandCar4"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductStyleCarEntity> GetProductStyleCarList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[StyleId],[BrandCar1],[BrandCar2],[BrandCar3],[BrandCar4],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[StyleId],[BrandCar1],[BrandCar2],[BrandCar3],[BrandCar4],[CreateTime] from dbo.[ProductStyleCar] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductStyleCar] with (nolock) ";
            IList<ProductStyleCarEntity> entityList = new List< ProductStyleCarEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductStyleCarEntity entity=new ProductStyleCarEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.BrandCar1=StringUtils.GetDbInt(reader["BrandCar1"]);
					entity.BrandCar2=StringUtils.GetDbInt(reader["BrandCar2"]);
					entity.BrandCar3=StringUtils.GetDbInt(reader["BrandCar3"]);
					entity.BrandCar4=StringUtils.GetDbInt(reader["BrandCar4"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<ProductStyleCarEntity> GetListByStyleId(int styleid)
        {
            string sql = @"SELECT    [Id],[StyleId],[BrandCar1],[BrandCar2],[BrandCar3],[BrandCar4],[CreateTime] from dbo.[ProductStyleCar] WITH(NOLOCK) where StyleId=@StyleId	";
            IList<ProductStyleCarEntity> entityList = new List<ProductStyleCarEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@StyleId", DbType.Int32, styleid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductStyleCarEntity entity = new ProductStyleCarEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.BrandCar1 = StringUtils.GetDbInt(reader["BrandCar1"]);
                    entity.BrandCar2 = StringUtils.GetDbInt(reader["BrandCar2"]);
                    entity.BrandCar3 = StringUtils.GetDbInt(reader["BrandCar3"]);
                    entity.BrandCar4 = StringUtils.GetDbInt(reader["BrandCar4"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<ProductStyleCarEntity> GetProductStyleCarAll()
        {

            string sql = @"SELECT    [Id],[StyleId],[BrandCar1],[BrandCar2],[BrandCar3],[BrandCar4],[CreateTime] from dbo.[ProductStyleCar] WITH(NOLOCK)	";
		    IList<ProductStyleCarEntity> entityList = new List<ProductStyleCarEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductStyleCarEntity entity=new ProductStyleCarEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.BrandCar1=StringUtils.GetDbInt(reader["BrandCar1"]);
					entity.BrandCar2=StringUtils.GetDbInt(reader["BrandCar2"]);
					entity.BrandCar3=StringUtils.GetDbInt(reader["BrandCar3"]);
					entity.BrandCar4=StringUtils.GetDbInt(reader["BrandCar4"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(ProductStyleCarEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductStyleCar] WITH(NOLOCK) ";
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
