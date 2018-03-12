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
功能描述：CGProductStock表的数据访问类。
创建时间：2017/1/17 11:53:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGProductDB
{
	/// <summary>
	/// CGProductStockEntity的数据访问操作
	/// </summary>
	public partial class CGProductStockDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGProductStockDA Instance
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
            internal static readonly CGProductStockDA instance = new CGProductStockDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGProductStock，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGProductStock">待插入的实体对象</param>
		public int AddCGProductStock(CGProductStockEntity entity)
		{
		   string sql=@"insert into CGProductStock( [CGMemId],[ProductId],[SuggestPrice],[StockNum],[Status] )VALUES
			            ( @CGMemId,@ProductId,@SuggestPrice,@StockNum,@Status );
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@SuggestPrice",  DbType.Decimal,entity.SuggestPrice);
			db.AddInParameter(cmd,"@StockNum",  DbType.Int32,entity.StockNum);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);  
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGProductStock">待更新的实体对象</param>
		public   int UpdateCGProductStock(CGProductStockEntity entity)
		{
			string sql=@" UPDATE dbo.[CGProductStock] SET
                       [CGMemId]=@CGMemId,[ProductId]=@ProductId,[SuggestPrice]=@SuggestPrice,[StockNum]=@StockNum,[Status]=@Status 
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@SuggestPrice",  DbType.Decimal,entity.SuggestPrice);
			db.AddInParameter(cmd,"@StockNum",  DbType.Int32,entity.StockNum);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);  
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGProductStockByKey(int id)
	    {
			string sql=@"delete from CGProductStock where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductStockDisabled()
        {
            string sql = @"delete from  CGProductStock  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductStockByIds(string  ids )
        {
            string sql = @"delete  from  CGProductStock where Status=0 and Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStockByProIds(string ids, int memid)
        {
            string sql = @"Update   CGProductStock set Status=0  where ProductId in (" + ids + ") and CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGProductStockEntity GetCGProductStock(int id)
		{
			string sql=@"SELECT  [Id],[CGMemId],[ProductId],[SuggestPrice],[StockNum],[Status]  
							FROM
							dbo.[CGProductStock] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGProductStockEntity entity=new CGProductStockEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.SuggestPrice=StringUtils.GetDbDecimal(reader["SuggestPrice"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]); 
				}
   		    }
            return entity;
		}

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<VWCGProductStockEntity> GetVWCGProductStockList(int pagesize, int pageindex, ref int recordCount, int memid, int status, int classid, int brandid, string key)
        {
            string where = " where 1=1 ";
            if (memid > 0)
            {
                where += " and CGMemId=@CGMemId ";
            }
            if (status > 0)
            {
                where += " and Status=@Status ";
            }
            if (classid > 0)
            {
                where += " and ClassId=@ClassId ";
            }
            if (brandid > 0)
            {
                where += " and BrandId=@BrandId ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and ProductSelfName like @ProductSelfName ";
            }
            string sql = @"SELECT   [Id],[CGMemId],[ProductId],[ProductCode],[ProductSelfName],[SuggestPrice],[StockNum],[Status] 
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGMemId],[ProductId],[ProductCode],[ProductSelfName],[SuggestPrice],[StockNum],[Status]  from dbo.[CGProductStock] WITH(NOLOCK)	
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGProductStock] with (nolock) " + where;
            IList<VWCGProductStockEntity> entityList = new List<VWCGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, memid);
            }
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (brandid > 0)
            {
                db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            }
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@ProductSelfName", DbType.String, "%" + key + "%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGProductStockEntity entity = new VWCGProductStockEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductCode = StringUtils.GetDbString(reader["ProductCode"]);
                    entity.ProductSelfName = StringUtils.GetDbString(reader["ProductSelfName"]);
                    entity.SuggestPrice = StringUtils.GetDbDecimal(reader["SuggestPrice"]);
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, memid);
            }
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (brandid > 0)
            {
                db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            }
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@ProductSelfName", DbType.String, "%" + key + "%");
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
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
       public IList<CGProductStockEntity> GetCGProductStockList(int pagesize, int pageindex, ref int recordCount, int _memid, int _status)
        {
            string where = " where 1=1 ";
            if(_status!=-1)
            {
                where += " and Status=@Status ";
            }
            string sql = @"SELECT   [Id],[CGMemId],[ProductId],[SuggestPrice],[StockNum],[Status] 
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGMemId],[ProductId],[SuggestPrice],[StockNum],[Status]  from dbo.[CGProductStock] WITH(NOLOCK)	
						" +where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGProductStock] with (nolock) "+ where;
            IList<CGProductStockEntity> entityList = new List<CGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (_status != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, _status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGProductStockEntity entity = new CGProductStockEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.SuggestPrice = StringUtils.GetDbDecimal(reader["SuggestPrice"]);
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2); if (_status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, _status);
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
        public IList<CGProductStockEntity> GetCGStockByProIds(string products, int memid)
        {
            string sql = @"SELECT a.* FROM dbo.CGProductStock a INNER JOIN 
(SELECT * FROM dbo.fun_splitstr(@ProductsStr,'|')) b ON a.ProductId=b.ID WHERE a.CGMemId=@CGMemId"; 
            IList<CGProductStockEntity> entityList = new List<CGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ProductsStr", DbType.String, products);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGProductStockEntity entity = new CGProductStockEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.SuggestPrice = StringUtils.GetDbDecimal(reader["SuggestPrice"]);
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public int AcceptCGStockByProIds(string productids, int memid)
        {
            string sql = @"
begin tran 

SELECT [CGMemId]
      ,[ProductId]
      ,[SuggestPrice]
      ,[StockNum]
      ,Status AS [Status]
      ,[ProvinceId]
      ,[CityId] INTO #temp FROM dbo.CGProductStockApp a INNER JOIN 
(SELECT * FROM dbo.fun_splitstr(@ProductsStr,'|')) b ON a.ProductId=b.ID WHERE a.CGMemId=@CGMemId and Status=@Status
INSERT INTO [JcCGProductDB].[dbo].[CGProductStock]
           ([CGMemId]
           ,[ProductId]
           ,[SuggestPrice]
           ,[StockNum]
           ,[Status]
           ,[ProvinceId]
           ,[CityId] )
           SELECT [CGMemId]
      ,[ProductId]
      ,[SuggestPrice]
      ,[StockNum]
      ,@NewStatus AS [Status]
      ,[ProvinceId]
      ,[CityId] FROM #temp;
delete a from [dbo].[CGProductStockApp] a inner join #temp b on a.ProductId=b.ProductId where a.CGMemId=@CGMemId
commit tran ";
            IList<CGProductStockEntity> entityList = new List<CGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ProductsStr", DbType.String, productids);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)CGProductAppStatus.Apping);
            db.AddInParameter(cmd, "@NewStatus", DbType.Int32, (int)CGProductAppStatus.Accept);

            return db.ExecuteNonQuery(cmd);
        }
        public int AcceptCGStockByMemId(int memid)
        {
            string sql = @"
begin tran 

SELECT [CGMemId]
      ,[ProductId]
      ,[SuggestPrice]
      ,[StockNum]
      ,Status AS [Status]
      ,[ProvinceId]
      ,[CityId] INTO #temp FROM dbo.CGProductStockApp a  WHERE a.CGMemId=@CGMemId and Status=@Status
INSERT INTO [JcCGProductDB].[dbo].[CGProductStock]
           ([CGMemId]
           ,[ProductId]
           ,[SuggestPrice]
           ,[StockNum]
           ,[Status]
           ,[ProvinceId]
           ,[CityId] )
           SELECT [CGMemId]
      ,[ProductId]
      ,[SuggestPrice]
      ,[StockNum]
      ,@NewStatus AS [Status]
      ,[ProvinceId]
      ,[CityId] FROM #temp
delete a  from dbo.CGProductStockApp a  inner join #temp b on a.ProductId=b.ProductId and a.CGMemId=@CGMemId
commit tran ";
            IList<CGProductStockEntity> entityList = new List<CGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid); 
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)CGProductAppStatus.Apping);
            db.AddInParameter(cmd, "@NewStatus", DbType.Int32, (int)CGProductAppStatus.Accept);

            return db.ExecuteNonQuery(cmd);
        }
        public int RejectCGStockByProIds(string productids, int memid)
        {
            string sql = @"UPDATE a SET [Status]=@Status FROM    dbo.CGProductStockApp a INNER JOIN 
(SELECT * FROM dbo.fun_splitstr(@ProductsStr,'|')) b ON a.ProductId=b.ID WHERE a.CGMemId=@CGMemId";
            IList<CGProductStockEntity> entityList = new List<CGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ProductsStr", DbType.String, productids);
            db.AddInParameter(cmd, "@Status", DbType.Int32, CGProductAppStatus.Reject);

            return db.ExecuteNonQuery(cmd);
        }
        public int ProcEditCGStock(string products, int memid)
        {
            string sql = @"EXEC Proc_ProductsEdit @ProductStr, @CGMemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductStr", DbType.String, products);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGProductStockEntity> GetCGProductStockAll()
        {

            string sql = @"SELECT    [Id],[CGMemId],[ProductId],[SuggestPrice],[StockNum],[Status]  from dbo.[CGProductStock] WITH(NOLOCK)	";
		    IList<CGProductStockEntity> entityList = new List<CGProductStockEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGProductStockEntity entity=new CGProductStockEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.SuggestPrice=StringUtils.GetDbDecimal(reader["SuggestPrice"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]); 
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
        public int  ExistNum(CGProductStockEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGProductStock] WITH(NOLOCK) ";
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
