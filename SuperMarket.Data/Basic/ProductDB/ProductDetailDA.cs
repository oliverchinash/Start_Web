using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;
using SuperMarket.Model.Basic.VW.Product;

/*****************************************
功能描述：ProductDetail表的数据访问类。
创建时间：2016/12/15 10:17:01
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductDetailEntity的数据访问操作
	/// </summary>
	public partial class ProductDetailDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductDetailDA Instance
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
            internal static readonly ProductDetailDA instance = new ProductDetailDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productDetail">待插入的实体对象</param>
		public int AddProductDetail(ProductDetailEntity entity)
		{
		   string sql= @"insert into ProductDetail( [ProductId],[Price],[TradePrice],[StockNum],[SaleNum],[ProductType],[Status],IsBP,Sort)VALUES
			            ( @ProductId,@Price,@TradePrice,@StockNum,@SaleNum,@ProductType,@Status,@IsBP,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@Price",  DbType.Decimal,entity.Price);
			db.AddInParameter(cmd,"@TradePrice",  DbType.Decimal,entity.TradePrice);
			db.AddInParameter(cmd,"@StockNum",  DbType.Int32,entity.StockNum);
			db.AddInParameter(cmd,"@SaleNum",  DbType.Int32,entity.SaleNum);
			db.AddInParameter(cmd,"@ProductType",  DbType.Int32,entity.ProductType);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);  
			db.AddInParameter(cmd, "@IsBP",  DbType.Int32,entity.IsBP); 
			db.AddInParameter(cmd, "@Sort",  DbType.Int32,entity.Sort);

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productDetail">待更新的实体对象</param>
		public   int UpdateProductDetail(ProductDetailEntity entity)
		{
			string sql= @" UPDATE dbo.[ProductDetail] SET
                       [ProductId]=@ProductId,[Price]=@Price,IsBP=@IsBP,[TradePrice]=@TradePrice,[DealerPrice]=@DealerPrice,[StockNum]=@StockNum,[SaleNum]=@SaleNum,[ProductType]=@ProductType,[Status]=@Status
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@Price",  DbType.Decimal,entity.Price);
			db.AddInParameter(cmd,"@TradePrice",  DbType.Decimal,entity.TradePrice);
			db.AddInParameter(cmd, "@DealerPrice",  DbType.Decimal,entity.DealerPrice);
			db.AddInParameter(cmd,"@StockNum",  DbType.Int32,entity.StockNum);
			db.AddInParameter(cmd,"@SaleNum",  DbType.Int32,entity.SaleNum);
			db.AddInParameter(cmd,"@ProductType",  DbType.Int32,entity.ProductType);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd, "@IsBP",  DbType.Int32,entity.IsBP);
            return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductDetailByKey(int id)
	    {
			string sql=@"delete from ProductDetail where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductDetailDisabled()
        {
            string sql = @"delete from  ProductDetail  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <returns></returns>
        public int UpdatePDetailStatus(int id,int status)
        {
            string sql = @"UPDATE DBO.[PRODUCTDETAIL] SET STATUS=@STATUS WHERE ID=@ID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@STATUS", DbType.Int32, status);
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }


         /// <summary>
         /// 做失效处理
         /// </summary>
         /// <param name="ids"></param>
         /// <returns></returns>
        public int DeleteProductDetailByIds(string ids)
        {
            string sql = @"Delete from ProductDetail  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductDetailByIds(string ids)
        {
            string sql = @"Update   ProductDetail set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductDetailEntity GetProductDetail(int id)
		{
			string sql=@"SELECT  [Id],[ProductId],[Price],[TradePrice],[StockNum],[SaleNum],[ProductType],[Status],[TimeStampCode]
							FROM
							dbo.[ProductDetail] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductDetailEntity entity=new ProductDetailEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.Price=StringUtils.GetDbDecimal(reader["Price"]);
					entity.TradePrice=StringUtils.GetDbDecimal(reader["TradePrice"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.SaleNum=StringUtils.GetDbInt(reader["SaleNum"]);
					entity.ProductType=StringUtils.GetDbInt(reader["ProductType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.TimeStampCode=StringUtils.GetDbString(reader["TimeStampCode"]);
				}
   		    }
            return entity;
		}
        public ProductDetailEntity GetProductDetailByPId(int productid, int producttype)
        {
            string sql = @"SELECT  [Id],[ProductId],IsBP,[Price],[TradePrice],[StockNum],[SaleNum],[ProductType],[Status],[TimeStampCode]
							FROM
							dbo.[ProductDetail] WITH(NOLOCK)	
							WHERE [ProductId]=@ProductId and ProductType=@ProductType";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@ProductType", DbType.Int32, producttype);
            ProductDetailEntity entity = new ProductDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);
                    entity.ProductType = StringUtils.GetDbInt(reader["ProductType"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.IsBP = StringUtils.GetDbInt(reader["IsBP"]);
                    entity.TimeStampCode = StringUtils.GetDbString(reader["TimeStampCode"]);
                }
            }
            return entity;
        }

        public int ReleaseStock(string productdetails)
        {
            string sql = @"UPDATE a SET StockNum=StockNum+b.value2 FROM  DBO.[Productdetail] a  INNER JOIN  dbo.fun_SplitStrToTable(@ProductDetails, '|', '_') b
   ON a.Id=b.value1  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductDetails", DbType.String, productdetails); 
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<ProductDetailEntity> GetProductDetailList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductId],[Price],[TradePrice],[StockNum],[SaleNum],[ProductType],[Status],[TimeStampCode]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductId],[Price],[TradePrice],[StockNum],[SaleNum],[ProductType],[Status],[TimeStampCode] from dbo.[ProductDetail] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductDetail] with (nolock) ";
            IList<ProductDetailEntity> entityList = new List< ProductDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductDetailEntity entity=new ProductDetailEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.Price=StringUtils.GetDbDecimal(reader["Price"]);
					entity.TradePrice=StringUtils.GetDbDecimal(reader["TradePrice"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.SaleNum=StringUtils.GetDbInt(reader["SaleNum"]);
					entity.ProductType=StringUtils.GetDbInt(reader["ProductType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.TimeStampCode=StringUtils.GetDbString(reader["TimeStampCode"]);
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
        /// 获取普通产品列表
        /// </summary>
        /// <returns></returns>
        public IList<VWConProductEntity> GetConProductList(int pageSize,int pageIndex,ref int recordCount,int productType,string productName)
        {
            string where = "  WHERE 1=1  ";
            if (productType > 0)
            {
                where += "  AND B.PRODUCTTYPE = @PRODUCTTYPE ";
            }
            if (!string.IsNullOrEmpty(productName))
            {
                where += " AND A.NAME like @NAME ";
            }
            string sql = @" SELECT ID,PRODUCTID,TITLE,adTITLE,NAME,PRICE,TRADEPRICE,SALENUM ,PRODUCTTYPE,STATUS,StockNum FROM 
                         (SELECT ROW_NUMBER() OVER (ORDER BY B.ID DESC) AS ROWNUMBER ,B.ID,A.ID AS PRODUCTID,A.TITLE,A.adTITLE,A.NAME,B.PRICE,B.TRADEPRICE,B.SALENUM,B.PRODUCTTYPE,B.STATUS,b.StockNum 
                          FROM DBO.[PRODUCT] A WITH(NOLOCK) INNER JOIN DBO.[PRODUCTDETAIL] B WITH(NOLOCK) ON A.ID=B.PRODUCTID " + where + @") AS TEMP
                          WHERE ROWNUMBER BETWEEN ((@PAGEINDEX-1)*@PAGESIZE)+1 AND @PAGEINDEX*@PAGESIZE";


            string sql2 = @" SELECT COUNT(1) FROM DBO.[PRODUCT] A INNER JOIN DBO.[PRODUCTDETAIL] B ON A.ID=B.PRODUCTID"+where;

            DbCommand cmd = db.GetSqlStringCommand(sql);
            IList<VWConProductEntity> entityList = new List<VWConProductEntity>();
            db.AddInParameter(cmd, "@PAGEINDEX", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PAGESIZE", DbType.Int32, pageSize);
            if (productType > 0)
            {
                db.AddInParameter(cmd, "@PRODUCTTYPE", DbType.Int32, productType);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@NAME", DbType.String, "%" +productName+"%");
            }

            using (IDataReader reader=db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWConProductEntity entity = new VWConProductEntity();
                    entity.Id = StringUtils.GetDbInt(reader["ID"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["PRODUCTID"]);
                    entity.Title = StringUtils.GetDbString(reader["TITLE"]);
                    entity.ADTitle = StringUtils.GetDbString(reader["adTITLE"]);
                    entity.Name = StringUtils.GetDbString(reader["NAME"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["PRICE"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TRADEPRICE"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SALENUM"]); 
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);  
                    entity.ProductType = StringUtils.GetDbInt(reader["PRODUCTTYPE"]);
                    entity.Status = StringUtils.GetDbInt(reader["STATUS"]);
                    entityList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);

            if (productType > 0)
            {
                db.AddInParameter(cmd, "@PRODUCTTYPE", DbType.Int32, productType);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@NAME", DbType.String, "%" + productName + "%");
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
        /// 获取特价产品列表
        /// </summary>
        /// <returns></returns>
        public IList<VWSpeProductEntity> GetSpeProductList(int pageSize, int pageIndex, ref int recordCount, int productType, string productName)
        {
            string where = "  WHERE 1=1  ";
            if (productType > 0)
            {
                where += "  AND B.PRODUCTTYPE = @PRODUCTTYPE ";
            }
            if (!string.IsNullOrEmpty(productName))
            {
                where += " AND A.NAME like @NAME ";
            }
            string sql = @" SELECT StockNum,ID,PRODUCTID,TITLE,NAME,PRICE,TRADEPRICE,SALENUM ,PRODUCTTYPE,STATUS FROM 
                         (SELECT ROW_NUMBER() OVER (ORDER BY B.ID DESC) AS ROWNUMBER ,B.StockNum,B.ID,A.ID AS PRODUCTID,A.TITLE,A.NAME,B.PRICE,B.TRADEPRICE,B.SALENUM,B.PRODUCTTYPE,B.STATUS 
                          FROM DBO.[PRODUCT] A WITH(NOLOCK) INNER JOIN DBO.[PRODUCTDETAIL] B WITH(NOLOCK) ON A.ID=B.PRODUCTID " + where + @") AS TEMP
                          WHERE ROWNUMBER BETWEEN ((@PAGEINDEX-1)*@PAGESIZE)+1 AND @PAGEINDEX*@PAGESIZE";


            string sql2 = @" SELECT COUNT(1) FROM DBO.[PRODUCT] A INNER JOIN DBO.[PRODUCTDETAIL] B ON A.ID=B.PRODUCTID" + where;

            DbCommand cmd = db.GetSqlStringCommand(sql);
            IList<VWSpeProductEntity> entityList = new List<VWSpeProductEntity>();
            db.AddInParameter(cmd, "@PAGEINDEX", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PAGESIZE", DbType.Int32, pageSize);
            if (productType > 0)
            {
                db.AddInParameter(cmd, "@PRODUCTTYPE", DbType.Int32, productType);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@NAME", DbType.String, "%" + productName + "%");
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWSpeProductEntity entity = new VWSpeProductEntity();
                    entity.Id = StringUtils.GetDbInt(reader["ID"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["PRODUCTID"]);
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);
                    entity.Title = StringUtils.GetDbString(reader["TITLE"]);
                    entity.Name = StringUtils.GetDbString(reader["NAME"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["PRICE"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TRADEPRICE"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SALENUM"]);
                    entity.ProductType = StringUtils.GetDbInt(reader["PRODUCTTYPE"]);
                    entity.Status = StringUtils.GetDbInt(reader["STATUS"]);
                    entityList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);

            if (productType > 0)
            {
                db.AddInParameter(cmd, "@PRODUCTTYPE", DbType.Int32, productType);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@NAME", DbType.String, "%" + productName + "%");
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
        public IList<ProductDetailEntity> GetProductDetailAll()
        {

            string sql = @"SELECT    [Id],[ProductId],[Price],[TradePrice],[StockNum],[SaleNum],[ProductType],[Status],[TimeStampCode] from dbo.[ProductDetail] WITH(NOLOCK)	";
		    IList<ProductDetailEntity> entityList = new List<ProductDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductDetailEntity entity=new ProductDetailEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.Price=StringUtils.GetDbDecimal(reader["Price"]);
					entity.TradePrice=StringUtils.GetDbDecimal(reader["TradePrice"]);
					entity.StockNum=StringUtils.GetDbInt(reader["StockNum"]);
					entity.SaleNum=StringUtils.GetDbInt(reader["SaleNum"]);
					entity.ProductType=StringUtils.GetDbInt(reader["ProductType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.TimeStampCode=StringUtils.GetDbString(reader["TimeStampCode"]);
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
        public int  ExistNum(ProductDetailEntity entity)
        {

            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductDetail] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where += " ProductId=@ProductId AND ProductType=@ProductType";
            }
            else
            {
                where += " Id<>@Id AND ProductId=@ProductId AND ProductType=@ProductType";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@ProductType", DbType.Int32, entity.ProductType);

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
