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
功能描述：ProductStyle表的数据访问类。
创建时间：2016/8/23 16:50:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
    /// <summary>
    /// ProductStyleEntity的数据访问操作
    /// </summary>
    public partial class ProductStyleDA : BaseSuperMarketDB
    {
        #region 实例化
        public static ProductStyleDA Instance
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
            internal static readonly ProductStyleDA instance = new ProductStyleDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表ProductStyle，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="productStyle">待插入的实体对象</param>
        public int AddProductStyle(ProductStyleEntity entity)
        {
            string sql = @"insert into ProductStyle( [AdTitle],[Title],[BrandId],[PicUrl],[DefaultProductId],[ClassId],[HasHtml],HasProduct, CreateTime,[PicSuffix],Status)VALUES
			            ( @AdTitle,@Title,@BrandId,@PicUrl,@DefaultProductId,@ClassId,@HasHtml,@HasProduct, @CreateTime,@PicSuffix,@Status);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            db.AddInParameter(cmd, "@DefaultProductId", DbType.Int32, entity.DefaultProductId);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@HasHtml", DbType.Int32, entity.HasHtml);
            db.AddInParameter(cmd, "@HasProduct", DbType.Int32, entity.HasProduct);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
            db.AddInParameter(cmd, "@PicSuffix", DbType.String, entity.PicSuffix);  
            db.AddInParameter(cmd, "@Status", DbType.String, entity.Status); 

            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToInt32(identity);
        }
        public int AddProductProc(ProcProductStyleEntity entity)
        {
            string sql = @"EXEC Proc_ProductEdit  @AdTitle,@Title,@BrandId,@Price,@MarketPrice,@PicUrl,@DefaultProductId,@ClassId,@HasHtml,@Cost,@TradePrice,@SellerId,@Num, 
@ContentCms,@StyleId,@StylePics,@StylePropertys,@BrandCars
			 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@Price", DbType.Decimal, entity.Price);
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, entity.MarketPrice);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            db.AddInParameter(cmd, "@DefaultProductId", DbType.Int32, entity.DefaultProductId);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@HasHtml", DbType.Int32, entity.HasHtml);
            db.AddInParameter(cmd, "@Cost", DbType.Decimal, entity.Cost);
            db.AddInParameter(cmd, "@TradePrice", DbType.Decimal, entity.TradePrice);
            db.AddInParameter(cmd, "@SellerId", DbType.Int32, entity.SellerId);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num); 
            db.AddInParameter(cmd, "@ContentCms", DbType.String, entity.ContentCms);
            db.AddInParameter(cmd, "@StyleId", DbType.Int32, entity.StyleId);
            db.AddInParameter(cmd, "@BrandCars", DbType.String, entity.BrandCars);
            db.AddInParameter(cmd, "@StylePics", DbType.String, entity.StylePics);
            db.AddInParameter(cmd, "@StylePropertys", DbType.String, entity.StylePropertys);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        public int AddProductStyleProc(VWProductStyleEntity _entity)
        {
            string sql = @"EXEC Proc_ProductStyleEdit  @AdTitle,@Title,@BrandId,@ClassId,@PicUrl,@HasHtml, @ContentCms,@StyleId,@StylePics
			 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@AdTitle", DbType.String, _entity.AdTitle);
            db.AddInParameter(cmd, "@Title", DbType.String, _entity.Title);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, _entity.BrandId);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, _entity.PicUrl);
            db.AddInParameter(cmd, "@HasHtml", DbType.Int32, _entity.HasHtml);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, _entity.ClassId); 
            db.AddInParameter(cmd, "@ContentCms", DbType.String, _entity.ContentCms);
            db.AddInParameter(cmd, "@StyleId", DbType.Int32, _entity.StyleId);
            db.AddInParameter(cmd, "@StylePics", DbType.String, _entity.StylePicsStr);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="productStyle">待更新的实体对象</param>
        public int UpdateProductStyle(ProductStyleEntity entity)
        {
            string sql = @" UPDATE dbo.[ProductStyle] SET
                       [AdTitle]=@AdTitle,[Title]=@Title,[BrandId]=@BrandId,[Price]=@Price,[PicSuffix]=@PicSuffix,[MarketPrice]=@MarketPrice,[PicUrl]=@PicUrl,[DefaultProductId]=@DefaultProductId,[ClassId]=@ClassId,[HasHtml]=@HasHtml,Cost=@Cost,TradePrice=@TradePrice,SellerId=@SellerId,Num=@Num, HasProduct=@HasProduct,SaleNum=@SaleNum
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@Price", DbType.Decimal, entity.Price);
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, entity.MarketPrice);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            db.AddInParameter(cmd, "@PicSuffix", DbType.String, entity.PicSuffix);
            db.AddInParameter(cmd, "@DefaultProductId", DbType.Int32, entity.DefaultProductId);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@HasHtml", DbType.Int32, entity.HasHtml);
            db.AddInParameter(cmd, "@Cost", DbType.Decimal, entity.Cost);
            db.AddInParameter(cmd, "@TradePrice", DbType.Decimal, entity.TradePrice);
            db.AddInParameter(cmd, "@SellerId", DbType.Int32, entity.SellerId);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num); 
            db.AddInParameter(cmd, "@HasProduct", DbType.Int32, entity.HasProduct);
            db.AddInParameter(cmd, "@SaleNum", DbType.Int32, entity.SaleNum);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductStyleByKey(int id)
        {
            string sql = @"delete from ProductStyle where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductStyleDisabled()
        {
            string sql = @"delete from  ProductStyle  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductStyleByIds(string ids)
        {
            string sql = @"Delete from ProductStyle  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductStyleByIds(string ids)
        {
            string sql = @"Update   ProductStyle set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public VWProductStyleEntity GetProductStyle(int id)
        {
            string sql = @"SELECT  [Id] as StyleId,[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice],[Num], [HasProduct],[SaleNum]
							FROM
							dbo.[ProductStyle] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            VWProductStyleEntity entity = new VWProductStyleEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]); 
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);

                }
            }
            return entity;
        }

        public VWProductStyleEntity GetProduct(int id)
        {
            string sql = @"SELECT  a.[Id] as ProductId,[AdTitle],[Title],[BrandId],b.[Price],[MarketPrice],[PicURL],[PicSuffix],[ClassId],[Cost],b.[TradePrice],[Num],b.[SaleNum]
							FROM
							dbo.[Product] a WITH(NOLOCK) inner join dbo.[ProductDetail] b WITH (NOLOCK) on a.[Id]=b.[ProductId] 
							WHERE a.[Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            VWProductStyleEntity entity = new VWProductStyleEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicURL"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);

                }
            }
            return entity;
        }
        public ProductStyleEntity GetProductStyleByName(int classid, int brandid, string adtitle)
        {
            string sql = @"SELECT  [Id] as StyleId,[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice],[Num], [HasProduct],[SaleNum]
							FROM
							dbo.[ProductStyle] WITH(NOLOCK)	
							WHERE [BrandId]=@BrandId and ClassId=@ClassId and AdTitle=@AdTitle";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            db.AddInParameter(cmd, "@AdTitle", DbType.String, adtitle);
            ProductStyleEntity entity = new ProductStyleEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]); 
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);

                }
            }
            return entity;
        }
        public ProductStyleEntity GetProductStyleByName(  string adtitle)
        {
            string sql = @"SELECT  [Id] as StyleId,[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice],[Num], [HasProduct],[SaleNum]
							FROM
							dbo.[ProductStyle] WITH(NOLOCK)	
							WHERE  AdTitle=@AdTitle";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@AdTitle", DbType.String, adtitle);
            ProductStyleEntity entity = new ProductStyleEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]); 
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);

                }
            }
            return entity;
        }
        public IList<OrderDetailPreTempEntity> GetOrderProducts(string productdetails)
        {
            string sql = @"EXEC [Proc_GetOrderCreate] @ProductDetails";

            IList<OrderDetailPreTempEntity> entityList = new List<OrderDetailPreTempEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductDetails", DbType.String, productdetails);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable _dt = new DataTable();
            if (ds.Tables.Count == 1)
            {
                _dt = ds.Tables[0];
                foreach (DataRow dr in _dt.Rows)
                {
                    OrderDetailPreTempEntity entity = new OrderDetailPreTempEntity();
                    entity.ProductDetailId = StringUtils.GetDbInt(dr["ProductDetailId"]);
                    entity.BrandId = StringUtils.GetDbInt(dr["BrandId"]);
                    entity.ClassId = StringUtils.GetDbInt(dr["ClassId"]);
                    entity.ProductId = StringUtils.GetDbInt(dr["ProductId"]);
                    entity.ProductCode = StringUtils.GetDbString(dr["ProductCode"]);
                    entity.ProductName = StringUtils.GetDbString(dr["ProductName"]);
                    entity.Spec1 = StringUtils.GetDbString(dr["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(dr["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(dr["Spec3"]);
                    entity.ProductPic = StringUtils.GetDbString(dr["ProductPic"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(dr["ProductPicSuffix"]);
                    entity.Cost = StringUtils.GetDbDecimal(dr["Cost"]);
                    entity.Price = StringUtils.GetDbDecimal(dr["Price"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(dr["TradePrice"]);  
                    entity.DealerPrice = StringUtils.GetDbDecimal(dr["DealerPrice"]); 
                    entity.MarketPrice = StringUtils.GetDbDecimal(dr["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(dr["ActualPrice"]);
                    entity.Num = StringUtils.GetDbInt(dr["Num"]);
                    entity.TransFeeType = StringUtils.GetDbInt(dr["TransFeeType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(dr["TransFee"]);
                    entity.ProductType = StringUtils.GetDbInt(dr["ProductType"]);
                    entity.StockNum = StringUtils.GetDbInt(dr["StockNum"]);
                    entity.Unit = StringUtils.GetDbInt(dr["Unit"]);
                    entity.IsBP = StringUtils.GetDbInt(dr["IsBP"]);
                    entity.CGMemId = StringUtils.GetDbInt(dr["CGMemId"]);
                    entity.IsAhmTake = StringUtils.GetDbInt(dr["IsAhmTake"]);

                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public int ProductsToOrder(string productdetails)
        {
            string sql = @"EXEC [Proc_ProductToOrder] @ProductDetails"; 
            IList<OrderDetailPreTempEntity> entityList = new List<OrderDetailPreTempEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductDetails", DbType.String, productdetails);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public bool ProductsEnough(string productdetails)
        {
            
                //id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"SELECT COUNT(1)
                FROM    dbo.fun_SplitStrToTable(@ProductDetails, '|', '_') a LEFT JOIN
                dbo.ProductDetail b ON a.value1 = b.id AND a.value2 <= b.StockNum  WHERE B.ID IS NULL ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductDetails", DbType.String, productdetails);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return true;
            return Convert.ToInt32(identity)==0;

        }
//        public VWProductStyleEntity GetStyleIdByProperties(int _classid, int _brandid, string properties)
//        {
//            string sql = @"SELECT  [Id] as StyleId,[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice], [Num], HasProduct,SaleNum
//							,PropertiesContent FROM
//							dbo.[ProductStyle] a WITH(NOLOCK) inner join  ProductStyleExt b  WITH(NOLOCK) 	
//on a.id=b.styleid WHERE a.[ClassId]=@ClassId and a.BrandId=@BrandId and  b.PropertiesContent=@PropertiesContent";
//            DbCommand cmd = db.GetSqlStringCommand(sql);
//            db.AddInParameter(cmd, "@ClassId", DbType.Int32, _classid);
//            db.AddInParameter(cmd, "@BrandId", DbType.Int32, _brandid);
//            db.AddInParameter(cmd, "@PropertiesContent", DbType.String, properties);
//            VWProductStyleEntity entity = new VWProductStyleEntity();
//            using (IDataReader reader = db.ExecuteReader(cmd))
//            {
//                if (reader.Read())
//                {
//                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
//                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
//                    entity.Title = StringUtils.GetDbString(reader["Title"]);
//                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
//                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
//                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
//                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
//                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
//                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
//                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
//                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
//                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
//                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
//                    entity.Num = StringUtils.GetDbInt(reader["Num"]); 
//                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
//                    entity.PropertiesContent = StringUtils.GetDbString(reader["PropertiesContent"]);
//                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);

//                }
//            }
//            return entity;
//        }
//        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ProductStyleEntity> GetProductStyleList(int pagesize, int pageindex, ref int recordCount, int classid, int brandid, int orderby ,string stylename)
        {

            //string sql = @"EXEC [Proc_GetListStyleByPage] @ClassId,@BrandId,@Properties,@PageIndex,@PageSize,@OrderByType";
            string where = " where 1=1";
            string orderbystr = "  Order by Id desc ";
            if (classid > 0)
            {
                where += " and ClassId=@ClassId ";
            }
            if (brandid > 0)
            {
                where += " and BrandId=@BrandId ";
            }
            if (!string.IsNullOrEmpty(stylename))
            {
                where += " and AdTitle like @AdTitle";
            }
            if (orderby == (int)OrderByType.NameAsc)
            {
                orderbystr = " Order by AdTitle asc";
            }

            string sql = @"SELECT [Id],[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],
                                         [DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice],
                         [Num],HasProduct,SaleNum
                         FROM
                         (SELECT ROW_NUMBER() OVER(" + orderbystr + @") AS ROWNUMBER, 
                         [Id]  ,[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice], [Num],HasProduct,SaleNum
						 from dbo.[ProductStyle] WITH(NOLOCK) " + where + @" ) as temp
                         where rownumber BETWEEN((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
            IList<ProductStyleEntity> entityList = new List<ProductStyleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (brandid > 0)
            {
                db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            }
            if (!string.IsNullOrEmpty(stylename))
            {
                db.AddInParameter(cmd, "@AdTitle",DbType.String,"%"+stylename+"%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductStyleEntity entity = new ProductStyleEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]); 
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);
                    entityList.Add(entity);
                }
            }

            string sql2 = @"Select count(1) from dbo.[ProductStyle] with (nolock) " + where;
            cmd = db.GetSqlStringCommand(sql2); if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (brandid > 0)
            {
                db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            }
            if (!string.IsNullOrEmpty(stylename))
            {
                db.AddInParameter(cmd,"@AdTitle",DbType.String,"%"+stylename+"%");
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
        /// 根据类型ID获取产品样式列表
        /// </summary>
        /// <param name="ClassId"></param>
        /// <returns></returns>
        public IList<ProductStyleEntity> GetProductStyleByClassFoundId(int ClassId)
        {
            string sql = @"SELECT    [Id],[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],[Cost],[TradePrice],[SellerId],[Num],HasProduct,SaleNum from dbo.[ProductStyle] WITH(NOLOCK) WHERE [ClassId]=@ClassId ";
            IList<ProductStyleEntity> entityList = new List<ProductStyleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, ClassId);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductStyleEntity entity = new ProductStyleEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.SellerId = StringUtils.GetDbInt(reader["SellerId"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);
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
        public IList<ProductStyleEntity> GetProductStyleAll()
        {

            string sql = @"SELECT    [Id],[AdTitle],[Title],[BrandId],[Price],[MarketPrice],[PicUrl],[PicSuffix],[DefaultProductId],[ClassId],[HasHtml],Cost,TradePrice,SellerId,Num,HasProduct,SaleNum from dbo.[ProductStyle] WITH(NOLOCK)	";
            IList<ProductStyleEntity> entityList = new List<ProductStyleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductStyleEntity entity = new ProductStyleEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.DefaultProductId = StringUtils.GetDbInt(reader["DefaultProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.HasHtml = StringUtils.GetDbInt(reader["HasHtml"]);
                    entity.Cost = StringUtils.GetDbInt(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.SellerId = StringUtils.GetDbInt(reader["SellerId"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);
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
        public int ExistNum(ProductStyleEntity entity)
        {
            //id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductStyle] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "  (BrandId=@BrandId) and ClassId=@ClassId and AdTitle=@AdTitle ";

            }
            else
            {
                where = where + " id<>@Id and  (BrandId=@BrandId)  and ClassId=@ClassId and AdTitle=@AdTitle ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }

            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        public int ExistTitleNum(string adtitle)
        {
            //id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductStyle] WITH(NOLOCK) where  AdTitle=@AdTitle "; 
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@AdTitle", DbType.String, adtitle);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }







        #endregion
        #endregion
    }
}
