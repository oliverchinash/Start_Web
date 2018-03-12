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
功能描述：ProductBaoPin表的数据访问类。
创建时间：2017/4/15 15:37:36
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductBaoPinEntity的数据访问操作
	/// </summary>
	public partial class ProductBaoPinDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductBaoPinDA Instance
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
            internal static readonly ProductBaoPinDA instance = new ProductBaoPinDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductBaoPin，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productBaoPin">待插入的实体对象</param>
		public int AddProductBaoPin(ProductBaoPinEntity entity)
		{
		   string sql=@"insert into ProductBaoPin( [ProductDetailId],[Name],[BeginTime],[EndTime],[IsActive],[CreateTime])VALUES
			            ( @ProductDetailId,@Name,@BeginTime,@EndTime,@IsActive,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
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
		/// <param name="productBaoPin">待更新的实体对象</param>
		public   int UpdateProductBaoPin(ProductBaoPinEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductBaoPin] SET
                       [ProductDetailId]=@ProductDetailId,[Name]=@Name,[BeginTime]=@BeginTime,[EndTime]=@EndTime,[IsActive]=@IsActive,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductBaoPinByKey(int id)
	    {
			string sql=@"delete from ProductBaoPin where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductBaoPinDisabled()
        {
            string sql = @"delete from  ProductBaoPin  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductBaoPinByIds(string ids)
        {
            string sql = @"Delete from ProductBaoPin  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductBaoPinByIds(string ids)
        {
            string sql = @"Update   ProductBaoPin set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductBaoPinEntity GetProductBaoPin(int id)
		{
			string sql=@"SELECT  [Id],[ProductDetailId],[Name],[BeginTime],[EndTime],[IsActive],[CreateTime]
							FROM
							dbo.[ProductBaoPin] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductBaoPinEntity entity=new ProductBaoPinEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
		public   IList<VWProductBaoPinEntity> GetProductBaoPinList(int pagesize, int pageindex, ref  int recordCount,int active,int showproduct )
		{
            string where = " where  1=1 ";   
            if (active!=-1)
            {
                if (active == 1)
                {
                    where += " and IsActive=@IsActive and  BeginTime<= getdate() and EndTime>= getdate()";
                }
                else
                { 
                    where += " and IsActive=@IsActive  ";
                }
            }
            if (showproduct != -1)
            {
                where += " and ShowProduct=@ShowProduct ";
            }
            string sql=@"SELECT   [Id],[ProductDetailId],[Name],[BeginTime],[EndTime],[IsActive],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[Name],[BeginTime],[EndTime],[IsActive],[CreateTime] from dbo.[ProductBaoPin] WITH(NOLOCK)	
						"+ where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductBaoPin] with (nolock) "+ where;
            IList<VWProductBaoPinEntity> entityList = new List<VWProductBaoPinEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (active != -1)
            { 
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, active);
            }
            if (showproduct != -1)
            {
                db.AddInParameter(cmd, "@ShowProduct", DbType.Int32, showproduct); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductBaoPinEntity entity =new VWProductBaoPinEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (active != -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, active);
            }
            if (showproduct != -1)
            {
                db.AddInParameter(cmd, "@ShowProduct", DbType.Int32, showproduct);
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
        public IList<VWProductBaoPinEntity> GetProductBaoPinShowList()
        {

            string sql = @"SELECT    [Id],[ProductDetailId],[Name],[BeginTime],[EndTime],[IsActive],[CreateTime] from dbo.[ProductBaoPin] WITH(NOLOCK) where IsActive =1 and BeginTime<=getdate() and EndTime>=getdate() and ShowProduct=1 Order By Sort desc	";
		    IList<VWProductBaoPinEntity> entityList = new List<VWProductBaoPinEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductBaoPinEntity entity =new VWProductBaoPinEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(ProductBaoPinEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductBaoPin] WITH(NOLOCK) ";
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
