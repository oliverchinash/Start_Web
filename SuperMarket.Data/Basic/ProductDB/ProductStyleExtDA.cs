﻿using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;

/*****************************************
功能描述：ProductStyleExt表的数据访问类。
创建时间：2016/8/16 13:54:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductStyleExtEntity的数据访问操作
	/// </summary>
	public partial class ProductStyleExtDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductStyleExtDA Instance
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
            internal static readonly ProductStyleExtDA instance = new ProductStyleExtDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductStyleExt，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productStyleExt">待插入的实体对象</param>
		public int AddProductStyleExt(ProductStyleExtEntity entity)
		{
		   string sql= @"insert into ProductStyleExt( [StyleId],[SizeChart],ContentCms)VALUES
			            ( @StyleId,@SizeChart,@ContentCms);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@SizeChart",  DbType.String,entity.SizeChart);
			db.AddInParameter(cmd, "@ContentCms",  DbType.String,entity.ContentCms);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productStyleExt">待更新的实体对象</param>
		public   int UpdateProductStyleExt(ProductStyleExtEntity entity)
		{
			string sql= @" UPDATE dbo.[ProductStyleExt] SET
                       [StyleId]=@StyleId,[SizeChart]=@SizeChart,[ContentCms]=@ContentCms
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@SizeChart",  DbType.String,entity.SizeChart);
            db.AddInParameter(cmd, "@ContentCms", DbType.String, entity.ContentCms);
            return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductStyleExtByKey(int id)
	    {
			string sql=@"delete from ProductStyleExt where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductStyleExtDisabled()
        {
            string sql = @"delete from  ProductStyleExt  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductStyleExtByIds(string ids)
        {
            string sql = @"Delete from ProductStyleExt  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductStyleExtByIds(string ids)
        {
            string sql = @"Update   ProductStyleExt set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductStyleExtEntity GetProductStyleExt(int id)
		{
			string sql= @"SELECT  [Id],[StyleId],[SizeChart],ContentCms
							FROM
							dbo.[ProductStyleExt] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductStyleExtEntity entity=new ProductStyleExtEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.SizeChart=StringUtils.GetDbString(reader["SizeChart"]);
					entity.ContentCms = StringUtils.GetDbString(reader["ContentCms"]);
                }
   		    }
            return entity;
		}
        public ProductStyleExtEntity GetStyleExtByStyleId(int _styleid)
        {
            string sql = @"SELECT  [Id],[StyleId],[SizeChart],ContentCms
							FROM
							dbo.[ProductStyleExt] WITH(NOLOCK)	
							WHERE [StyleId]=@StyleId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@StyleId", DbType.Int32, _styleid);
            ProductStyleExtEntity entity = new ProductStyleExtEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.SizeChart = StringUtils.GetDbString(reader["SizeChart"]);
                    entity.ContentCms = StringUtils.GetDbString(reader["ContentCms"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ProductStyleExtEntity> GetProductStyleExtList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[StyleId],[SizeChart],ContentCms
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[StyleId],[SizeChart],ContentCms from dbo.[ProductStyleExt] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductStyleExt] with (nolock) ";
            IList<ProductStyleExtEntity> entityList = new List< ProductStyleExtEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductStyleExtEntity entity=new ProductStyleExtEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.SizeChart=StringUtils.GetDbString(reader["SizeChart"]);
					entity.ContentCms = StringUtils.GetDbString(reader["ContentCms"]);
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
        public IList<ProductStyleExtEntity> GetProductStyleExtAll()
        {

            string sql = @"SELECT    [Id],[StyleId],[SizeChart],ContentCms from dbo.[ProductStyleExt] WITH(NOLOCK)	";
		    IList<ProductStyleExtEntity> entityList = new List<ProductStyleExtEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductStyleExtEntity entity=new ProductStyleExtEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.SizeChart=StringUtils.GetDbString(reader["SizeChart"]);
					entity.ContentCms = StringUtils.GetDbString(reader["ContentCms"]);
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
        public int  ExistNum(ProductStyleExtEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductStyleExt] WITH(NOLOCK) where StyleId=@StyleId";
            
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@StyleId", DbType.Int32, entity.StyleId);
           
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
