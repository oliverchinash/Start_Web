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
功能描述：ProductFine表的数据访问类。
创建时间：2017/4/19 23:17:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductFineEntity的数据访问操作
	/// </summary>
	public partial class ProductFineDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductFineDA Instance
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
            internal static readonly ProductFineDA instance = new ProductFineDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductFine，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productFine">待插入的实体对象</param>
		public int AddProductFine(ProductFineEntity entity)
		{
		   string sql=@"insert into ProductFine( [ProductDetailId],[Sort],[BeginTime],[EndTime],[IsActive],[FineModuleId])VALUES
			            ( @ProductDetailId,@Sort,@BeginTime,@EndTime,@IsActive,@FineModuleId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@FineModuleId",  DbType.Int32,entity.FineModuleId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productFine">待更新的实体对象</param>
		public   int UpdateProductFine(ProductFineEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductFine] SET
                       [ProductDetailId]=@ProductDetailId,[Sort]=@Sort,[BeginTime]=@BeginTime,[EndTime]=@EndTime,[IsActive]=@IsActive,[FineModuleId]=@FineModuleId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@FineModuleId",  DbType.Int32,entity.FineModuleId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductFineByKey(int id)
	    {
			string sql=@"delete from ProductFine where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductFineDisabled()
        {
            string sql = @"delete from  ProductFine  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductFineByIds(string ids)
        {
            string sql = @"Delete from ProductFine  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductFineByIds(string ids)
        {
            string sql = @"Update   ProductFine set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductFineEntity GetProductFine(int id)
		{
			string sql=@"SELECT  [Id],[ProductDetailId],[Sort],[BeginTime],[EndTime],[IsActive],[FineModuleId]
							FROM
							dbo.[ProductFine] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductFineEntity entity=new ProductFineEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.FineModuleId=StringUtils.GetDbInt(reader["FineModuleId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<VWProductFineEntity> GetProductFineList(int pagesize, int pageindex, ref  int recordCount,int finetype )
		{
            string where = " where 1=1 ";
            if(finetype!=-1)
            {
                where += " and FineModuleId=@FineModuleId ";
            }
			string sql=@"SELECT   [Id],[ProductDetailId],[Sort],[BeginTime],[EndTime],[IsActive],[FineModuleId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[Sort],[BeginTime],[EndTime],[IsActive],[FineModuleId] from dbo.[ProductFine] WITH(NOLOCK)	
						"+ where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductFine] with (nolock) "+ where;
            IList<VWProductFineEntity> entityList = new List<VWProductFineEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (finetype != -1)
            {
                db.AddInParameter(cmd, "@FineModuleId", DbType.Int32, finetype); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					VWProductFineEntity entity=new VWProductFineEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.FineModuleId=StringUtils.GetDbInt(reader["FineModuleId"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (finetype != -1)
            {
                db.AddInParameter(cmd, "@FineModuleId", DbType.Int32, finetype);
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
        public IList<VWProductFineEntity> GetProductFineTopNum(int finetype,int num)
        {
            string where = " where 1=1 ";
            if (finetype != -1)
            {
                where += " and FineModuleId=@FineModuleId ";
            }
            string sql = @"SELECT    top "+ num + @"
						 [Id],[ProductDetailId],[Sort],[BeginTime],[EndTime],[IsActive],[FineModuleId] from dbo.[ProductFine] WITH(NOLOCK)	
						" + where +  " Order By Sort Desc ";

             IList<VWProductFineEntity> entityList = new List<VWProductFineEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (finetype != -1)
            {
                db.AddInParameter(cmd, "@FineModuleId", DbType.Int32, finetype);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductFineEntity entity = new VWProductFineEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.BeginTime = StringUtils.GetDbDateTime(reader["BeginTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.FineModuleId = StringUtils.GetDbInt(reader["FineModuleId"]);
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
        public IList<ProductFineEntity> GetProductFineAll()
        {

            string sql = @"SELECT    [Id],[ProductDetailId],[Sort],[BeginTime],[EndTime],[IsActive],[FineModuleId] from dbo.[ProductFine] WITH(NOLOCK)	";
		    IList<ProductFineEntity> entityList = new List<ProductFineEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductFineEntity entity=new ProductFineEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.FineModuleId=StringUtils.GetDbInt(reader["FineModuleId"]);
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
        public int  ExistNum(ProductFineEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductFine] WITH(NOLOCK) ";
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
