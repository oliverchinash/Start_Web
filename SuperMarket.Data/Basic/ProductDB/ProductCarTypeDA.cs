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
功能描述：ProductCarType表的数据访问类。
创建时间：2016/10/31 16:28:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductCarTypeEntity的数据访问操作
	/// </summary>
	public partial class ProductCarTypeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductCarTypeDA Instance
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
            internal static readonly ProductCarTypeDA instance = new ProductCarTypeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductCarType，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productCarType">待插入的实体对象</param>
		public int AddProductCarType(ProductCarTypeEntity entity)
		{
		   string sql=@"insert into ProductCarType( [ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort])VALUES
			            ( @ProductId,@CarType1,@CarType2,@CarType3,@CarType4,@CreateTime,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@CarType1",  DbType.Int32,entity.CarType1);
			db.AddInParameter(cmd,"@CarType2",  DbType.Int32,entity.CarType2);
			db.AddInParameter(cmd,"@CarType3",  DbType.Int32,entity.CarType3);
			db.AddInParameter(cmd,"@CarType4",  DbType.Int32,entity.CarType4);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productCarType">待更新的实体对象</param>
		public   int UpdateProductCarType(ProductCarTypeEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductCarType] SET
                       [ProductId]=@ProductId,[CarType1]=@CarType1,[CarType2]=@CarType2,[CarType3]=@CarType3,[CarType4]=@CarType4,[CreateTime]=@CreateTime,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@CarType1",  DbType.Int32,entity.CarType1);
			db.AddInParameter(cmd,"@CarType2",  DbType.Int32,entity.CarType2);
			db.AddInParameter(cmd,"@CarType3",  DbType.Int32,entity.CarType3);
			db.AddInParameter(cmd,"@CarType4",  DbType.Int32,entity.CarType4);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}	
        
        
        public int ExecProcProductCarTypeAdd(ProductCarTypeEntity entity)
        {
            int _result = 0;
            string sql = @"Exec [dbo].[Proc_ProductCarTypeAdd] @ProductId,@CarType1,@CarType2,@CarType3,@CarType4";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@CarType1", DbType.Int32, entity.CarType1);
            db.AddInParameter(cmd, "@CarType2", DbType.Int32, entity.CarType2);
            db.AddInParameter(cmd, "@CarType3", DbType.Int32, entity.CarType3);
            db.AddInParameter(cmd, "@CarType4", DbType.Int32, entity.CarType4);
            _result=StringUtils.GetDbInt(db.ExecuteScalar(cmd));
            return _result;


        }
            /// <summary>
            /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
            /// </summary>
        public  int  DeleteProductCarTypeByKey(int id)
	    {
			string sql=@"delete from ProductCarType where    Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductCarTypeDisabled()
        {
            string sql = @"delete from  ProductCarType  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductCarTypeByIds(string ids)
        {
            string sql = @"Delete from ProductCarType  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductCarTypeByIds(string ids)
        {
            string sql = @"Update   ProductCarType set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductCarTypeEntity GetProductCarType(int id)
		{
			string sql=@"SELECT  [Id],[ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort]
							FROM
							dbo.[ProductCarType] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductCarTypeEntity entity=new ProductCarTypeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.CarType1=StringUtils.GetDbInt(reader["CarType1"]);
					entity.CarType2=StringUtils.GetDbInt(reader["CarType2"]);
					entity.CarType3=StringUtils.GetDbInt(reader["CarType3"]);
					entity.CarType4=StringUtils.GetDbInt(reader["CarType4"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}


        public ProductCarTypeEntity GetProductCarTypeByPid(int pid)
        {
            string sql = @"SELECT  [Id],[ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort]
							FROM
							dbo.[ProductCarType] WITH(NOLOCK)	
							WHERE [ProductId]=@ProductId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductId", DbType.Int32, pid);
            ProductCarTypeEntity entity = new ProductCarTypeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.CarType1 = StringUtils.GetDbInt(reader["CarType1"]);
                    entity.CarType2 = StringUtils.GetDbInt(reader["CarType2"]);
                    entity.CarType3 = StringUtils.GetDbInt(reader["CarType3"]);
                    entity.CarType4 = StringUtils.GetDbInt(reader["CarType4"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }
        public int ProcEditProductCarType(int productid,string staticids)
        {
            int _result = 0;
            string sql = @"Exec [dbo].[Proc_ProductCarTypeEdit] @ProductId,@StrStaticIds";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@StrStaticIds", DbType.String, staticids); 
            _result = StringUtils.GetDbInt(db.ExecuteScalar(cmd));
            return _result;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ProductCarTypeEntity> GetProductCarTypeList(int pagesize, int pageindex, ref  int recordCount,int pId)
		{
            string where = " where 1=1";
            if (pId > 0)
            {
                where += " And ProductId=@ProductId"; 
            }
			string sql=@"SELECT   [Id],[ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id asc) AS ROWNUMBER,
						 [Id],[ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort] from dbo.[ProductCarType] WITH(NOLOCK)	
						 "+where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductCarType] with (nolock) "+where;
            IList<ProductCarTypeEntity> entityList = new List< ProductCarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (pId > 0)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, pId);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductCarTypeEntity entity=new ProductCarTypeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.CarType1=StringUtils.GetDbInt(reader["CarType1"]);
					entity.CarType2=StringUtils.GetDbInt(reader["CarType2"]);
					entity.CarType3=StringUtils.GetDbInt(reader["CarType3"]);
					entity.CarType4=StringUtils.GetDbInt(reader["CarType4"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (pId > 0)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, pId);
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
        public IList<ProductCarTypeEntity> GetListByProductId(int productid)
        {
            string sql = @"SELECT    [Id],[ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort] from dbo.[ProductCarType] WITH(NOLOCK)
where ProductId=@ProductId ";
            IList<ProductCarTypeEntity> entityList = new List<ProductCarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductCarTypeEntity entity = new ProductCarTypeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.CarType1 = StringUtils.GetDbInt(reader["CarType1"]);
                    entity.CarType2 = StringUtils.GetDbInt(reader["CarType2"]);
                    entity.CarType3 = StringUtils.GetDbInt(reader["CarType3"]);
                    entity.CarType4 = StringUtils.GetDbInt(reader["CarType4"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
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
        public IList<ProductCarTypeEntity> GetProductCarTypeAll()
        {

            string sql = @"SELECT    [Id],[ProductId],[CarType1],[CarType2],[CarType3],[CarType4],[CreateTime],[Sort] from dbo.[ProductCarType] WITH(NOLOCK)	";
		    IList<ProductCarTypeEntity> entityList = new List<ProductCarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductCarTypeEntity entity=new ProductCarTypeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.CarType1=StringUtils.GetDbInt(reader["CarType1"]);
					entity.CarType2=StringUtils.GetDbInt(reader["CarType2"]);
					entity.CarType3=StringUtils.GetDbInt(reader["CarType3"]);
					entity.CarType4=StringUtils.GetDbInt(reader["CarType4"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
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
        public int  ExistNum(ProductCarTypeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductCarType] WITH(NOLOCK) ";
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
