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
功能描述：CGProductStockApp表的数据访问类。
创建时间：2017/1/17 11:53:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGProductDB
{
	/// <summary>
	/// CGProductStockAppEntity的数据访问操作
	/// </summary>
	public partial class CGProductStockAppDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGProductStockAppDA Instance
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
            internal static readonly CGProductStockAppDA instance = new CGProductStockAppDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGProductStockApp，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGProductStockApp">待插入的实体对象</param>
		public int AddCGProductStockApp(CGProductStockAppEntity entity)
		{
		   string sql=@"insert into CGProductStockApp( [CGMemId],[ProductId],[StockNum],[Status],[CreateTime],[CheckTime],[CheckManId],[ProvinceId],[CityId])VALUES
			            ( @CGMemId,@ProductId,@StockNum,@Status,@CreateTime,@CheckTime,@CheckManId,@ProvinceId,@CityId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@StockNum",  DbType.Int32,entity.StockNum);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@ProvinceId",  DbType.Int32,entity.ProvinceId);
			db.AddInParameter(cmd,"@CityId",  DbType.Int32,entity.CityId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGProductStockApp">待更新的实体对象</param>
		public   int UpdateCGProductStockApp(CGProductStockAppEntity entity)
		{
			string sql=@" UPDATE dbo.[CGProductStockApp] SET
                       [CGMemId]=@CGMemId,[ProductId]=@ProductId,[StockNum]=@StockNum,[Status]=@Status,[CreateTime]=@CreateTime,[CheckTime]=@CheckTime,[CheckManId]=@CheckManId,[ProvinceId]=@ProvinceId,[CityId]=@CityId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@StockNum",  DbType.Int32,entity.StockNum);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@ProvinceId",  DbType.Int32,entity.ProvinceId);
			db.AddInParameter(cmd,"@CityId",  DbType.Int32,entity.CityId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGProductStockAppByKey(int id)
	    {
			string sql=@"delete from CGProductStockApp where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductStockAppDisabled()
        {
            string sql = @"delete from  CGProductStockApp  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductStockAppByIds(string ids,int memid)
        {
            string sql = @"Delete from CGProductStockApp  where ProductId in (" + ids + ") and CGMemId=@CGMemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductStockAppByIds(string ids)
        {
            string sql = @"Update   CGProductStockApp set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGProductStockAppEntity GetCGProductStockApp(int id)
		{
			string sql=@"SELECT  [Id],[CGMemId],[ProductId],[StockNum],[Status],[CreateTime],[CheckTime],[CheckManId],[ProvinceId],[CityId]
							FROM
							dbo.[CGProductStockApp] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGProductStockAppEntity entity=new CGProductStockAppEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.ProvinceId=StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId=StringUtils.GetDbInt(reader["CityId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<CGProductStockAppEntity> GetCGProductStockAppList(int pagesize, int pageindex, ref  int recordCount,int memid,int _status)
		{
            string where = " where 1=1 ";
            if(memid!=-1)
            {
                where += " and CGMemId=@CGMemId ";

            }
            if (_status != -1)
            {
                where += " and Status=@Status ";

            }
            string sql= @"SELECT   [Id],[CGMemId],[ProductId],[StockNum],SuggestPrice,[Status],[CreateTime],[CheckTime],[CheckManId],[ProvinceId],[CityId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGMemId],[ProductId],[StockNum],SuggestPrice,[Status],[CreateTime],[CheckTime],[CheckManId],[ProvinceId],[CityId] from dbo.[CGProductStockApp] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGProductStockApp] with (nolock) "+ where;
            IList<CGProductStockAppEntity> entityList = new List< CGProductStockAppEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid != -1)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);

            }
            if (_status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, _status); 

            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGProductStockAppEntity entity=new CGProductStockAppEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]); 
					entity.SuggestPrice = StringUtils.GetDbDecimal(reader["SuggestPrice"]);  

                    entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.ProvinceId=StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId=StringUtils.GetDbInt(reader["CityId"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2); if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);

            }
            if (_status != -1)
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

        public IList<int> GetCGMemIdList(int pagesize, int pageindex, ref int recordCount,int status)
        {
            string sql = @"
SELECT DISTINCT CGMemId INTO #temp FROM dbo.[CGProductStockApp] WITH(NOLOCK)  WHERE STATUS=@Status 

SELECT    [CGMemId] 
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY CGMemId desc) AS ROWNUMBER,
						  [CGMemId]  from #temp WITH(NOLOCK)   ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select COUNT(DISTINCT  CGMemId) from dbo.[CGProductStockApp] with (nolock)  where   STATUS=@Status  ";
            IList<int> entityList = new List<int>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    int memid = StringUtils.GetDbInt(reader["CGMemId"]);
                    entityList.Add(memid);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
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
        public int ProcAddCGStockApp(string products,int memid)
        {
            string sql = @"EXEC Proc_AddProductToApp @ProductStr, @CGMemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductStr", DbType.String, products);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
           return  db.ExecuteNonQuery(cmd);
    
        }
        public int ProcEditCGStockApp(string products, int memid)
        {
            string sql = @"EXEC Proc_ProductsAppEdit @ProductStr, @CGMemId";
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
        public IList<CGProductStockAppEntity> GetCGProductStockAppAll()
        {

            string sql = @"SELECT    [Id],[CGMemId],[ProductId],[StockNum],[Status],[CreateTime],[CheckTime],[CheckManId],[ProvinceId],[CityId] from dbo.[CGProductStockApp] WITH(NOLOCK)	";
		    IList<CGProductStockAppEntity> entityList = new List<CGProductStockAppEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGProductStockAppEntity entity=new CGProductStockAppEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.ProvinceId=StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId=StringUtils.GetDbInt(reader["CityId"]);
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
        public int  ExistNum(CGProductStockAppEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGProductStockApp] WITH(NOLOCK) ";
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
