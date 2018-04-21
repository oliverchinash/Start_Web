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
功能描述：Product表的数据访问类。
创建时间：2016/10/31 16:28:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductEntity的数据访问操作
	/// </summary>
	public partial class ProductDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductDA Instance
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
            internal static readonly ProductDA instance = new ProductDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Product，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="product">待插入的实体对象</param>
		public int AddProduct(ProductEntity entity)
		{
		   string sql= @"insert into Product( [Code],[Title],[AdTitle], [ClassId],[BrandId], [Num],[Spec1],[Spec2],[Spec3],[ShowPicType],[PicUrl],PicSuffix,[Retail],[Wholesale],[Status],IsOEM,Sort  )VALUES
			            ( @Code,@Title,@AdTitle, @ClassId,@BrandId, @Num,@Spec1,@Spec2,@Spec3,@ShowPicType,@PicUrl,@PicSuffix,@Retail,@Wholesale,@Status,@IsOEM,@Sort );
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@AdTitle",  DbType.String,entity.AdTitle); 
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId); 
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@Spec1",  DbType.String,entity.Spec1);
			db.AddInParameter(cmd,"@Spec2",  DbType.String,entity.Spec2);
			db.AddInParameter(cmd,"@Spec3",  DbType.String,entity.Spec3);
            db.AddInParameter(cmd,"@ShowPicType",  DbType.Int32,entity.ShowPicType);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd, "@PicSuffix",  DbType.String,entity.PicSuffix);
            db.AddInParameter(cmd,"@Retail",  DbType.Int32,entity.Retail);
			db.AddInParameter(cmd,"@Wholesale",  DbType.Int32,entity.Wholesale);  
			db.AddInParameter(cmd, "@Status",  DbType.Int32,entity.Status);  
			db.AddInParameter(cmd, "@Sort",  DbType.Int32, entity.Sort);

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddProductProc(ProcProductEntity  entity)
        { 
            string sql = @"EXEC Proc_ProductEditNew  @Code,@AdTitle,@Title,@GrossWeight,@PlaceOrigin,@Name,@BrandId,@BrandName,@ProductId,@Spec1,
    @Spec2,@Unit,@ClassId, @CreateManId,@Num ,@SiteId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@GrossWeight", DbType.Decimal, entity.GrossWeight);
            db.AddInParameter(cmd, "@PlaceOrigin", DbType.String, entity.PlaceOrigin);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId); 
            db.AddInParameter(cmd, "@BrandName", DbType.String, entity.BrandName);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            db.AddInParameter(cmd, "@Unit", DbType.Int32, entity.Unit);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId); 
            db.AddInParameter(cmd, "@CreateManId", DbType.Int32, entity.CreateManId);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);   
            db.AddInParameter(cmd, "@SiteId", DbType.Int32, entity.SiteId);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
     
    }


        /// <summary>
        ///  赋值
        /// </summary>
        /// <returns></returns>
        public int Assignment(ref VWProductEntity VWEntity,int id)
        {
            string sql = @" select * from dbo.[Product] WITH (NOLOCK) where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            using (IDataReader reader=db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    VWEntity.Id = StringUtils.GetDbInt(reader["Id"]);
                    VWEntity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    VWEntity.Title = StringUtils.GetDbString(reader["Title"]);
                    VWEntity.Code = StringUtils.GetDbString(reader["Code"]);
                    VWEntity.Name = StringUtils.GetDbString(reader["Name"]); 
                    VWEntity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    VWEntity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    VWEntity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    VWEntity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);
                    VWEntity.GrossWeight = StringUtils.GetDbDecimal(reader["GrossWeight"]);
                    VWEntity.PlaceOrigin = StringUtils.GetDbString(reader["PlaceOrigin"]);
                    VWEntity.ShowPicType = StringUtils.GetDbInt(reader["ShowPicType"]);
                    VWEntity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    VWEntity.BrandId = StringUtils.GetDbInt(reader["BrandId"]); 

                    return 1;
                }
            }
            return 0;
        }


        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ChangeProductStatus(int id,int status)
        {
            string sql = @"Update dbo.[Product] Set Status=@Status where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd,"@Status",DbType.Int32,status);
            db.AddInParameter(cmd,"@Id",DbType.Int32,id);
            return db.ExecuteNonQuery(cmd);
        }

    /// <summary>
    /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
    /// 如果数据库有数据被更新了则返回True，否则返回False
    /// </summary>
    /// <param name="db">数据库操作对象</param>
    /// <param name="product">待更新的实体对象</param>
        public   int UpdateProduct(ProductEntity entity)
		{
			string sql= @" UPDATE dbo.[Product] SET
                       [Code]=@Code,[Title]=@Title,[Name]=@Name,[AdTitle]=@AdTitle,  [Num]=@Num,[Spec1]=@Spec1,[Spec2]=@Spec2,  [Retail]=@Retail,[Wholesale]=@Wholesale 
,[Status]=@Status,Unit=@Unit,PackUnit=@PackUnit,PackNum=@PackNum
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
            db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@AdTitle",  DbType.String,entity.AdTitle);  
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@Spec1",  DbType.String,entity.Spec1);
			db.AddInParameter(cmd,"@Spec2",  DbType.String,entity.Spec2); 
			db.AddInParameter(cmd,"@Retail",  DbType.Int32,entity.Retail);
			db.AddInParameter(cmd,"@Wholesale",  DbType.Int32,entity.Wholesale); 
			db.AddInParameter(cmd, "@Status",  DbType.Int32,entity.Status); 
			db.AddInParameter(cmd, "@Unit",  DbType.Int32, entity.Unit);
			db.AddInParameter(cmd, "@PackUnit",  DbType.Int32, entity.PackUnit);
			db.AddInParameter(cmd, "@PackNum",  DbType.Int32, entity.PackNum);
            return db.ExecuteNonQuery(cmd);
		}
        public int UpdateProductName(ProductEntity entity)
        {
            string sql = @" UPDATE dbo.[Product] SET
                        [AdTitle]=@AdTitle 
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
             db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
           
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteProductByKey(int id)
	    {
			string sql=@"delete from Product where   Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductDisabled()
        {
            string sql = @"delete from  Product  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductByIds(string ids)
        {
            string sql = @"Delete from Product  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductByIds(string ids)
        {
            string sql = @"Update   Product set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductEntity GetProduct(int id)
		{
			string sql= @"SELECT  *
  FROM  [dbo].[Product] WITH(NOLOCK)	
							WHERE [Id]=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductEntity entity=new ProductEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]); 
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]); 
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ShowPicType = StringUtils.GetDbInt(reader["ShowPicType"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.CarTypeRelated = StringUtils.GetDbInt(reader["CarTypeRelated"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.Retail = StringUtils.GetDbInt(reader["Retail"]);
                    entity.Wholesale = StringUtils.GetDbInt(reader["Wholesale"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.GrossWeight = StringUtils.GetDbDecimal(reader["GrossWeight"]);
                    entity.PlaceOrigin = StringUtils.GetDbString(reader["PlaceOrigin"]);
                    entity.IsOEM = StringUtils.GetDbInt(reader["IsOEM"]);
                    entity.Unit = StringUtils.GetDbInt(reader["Unit"]);
                   
    }
   		    }
            return entity;
		}

        public VWCGSyncOrder SplitOrder(string proids)
        {
            VWCGSyncOrder result = new VWCGSyncOrder();
            result.TYClassIds = new List<int>();
            result.TYClassPros = new List<VWCGSyncProClass>();
            result.NotTYClasses = new List<int>();
            result.NotTYClassPros = new List<VWCGSyncProClass>();
            result.NotTYCarTypes = new List<int>();
            result.NotTYProCarTypes = new List<VWCGSyncProCarType>();
            result.NotTYProCarTypesDetail = new List<VWCGSyncProCarType>();
            string sql = @"EXEC [Proc_SplitOrderByProducts] @Products ";

            IList<VWProductEntity> entityList = new List<VWProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Products", DbType.String, proids);
            DataSet ds = db.ExecuteDataSet(cmd);
             if (ds.Tables.Count == 7)
            {
                DataTable _dt0 = ds.Tables[0];
                foreach (DataRow dr in _dt0.Rows)
                {
                    result.TYClassIds.Add( StringUtils.GetDbInt(dr["ClassId"]));  
                }
                DataTable _dt1 = ds.Tables[1];
                foreach (DataRow dr in _dt1.Rows)
                {
                    VWCGSyncProClass tyclassproentity = new VWCGSyncProClass();
                    tyclassproentity.ClassId = StringUtils.GetDbInt(dr["ClassId"]);
                    tyclassproentity.ProductId = StringUtils.GetDbInt(dr["ProductId"]);
                    result.TYClassPros.Add(tyclassproentity);
                }
                DataTable _dt2= ds.Tables[2];
                foreach (DataRow dr in _dt2.Rows)
                {
                    result.NotTYCarTypes.Add(StringUtils.GetDbInt(dr["CarTypeId2"]));
                }
                DataTable _dt3 = ds.Tables[3];
                foreach (DataRow dr in _dt3.Rows)
                {
                    VWCGSyncProCarType entity = new VWCGSyncProCarType();
                    entity.ProductId = StringUtils.GetDbInt(dr["ProductId"]); 
                    entity.CarTypeId2 = StringUtils.GetDbInt(dr["CarTypeId2"]); 
                    result.NotTYProCarTypes.Add(entity);
                }
                DataTable _dt4 = ds.Tables[4];
                foreach (DataRow dr in _dt4.Rows)
                {
                    result.NotTYClasses.Add(StringUtils.GetDbInt(dr["ClassId"]));
                }
                DataTable _dt5 = ds.Tables[5];
                foreach (DataRow dr in _dt5.Rows)
                {
                    VWCGSyncProClass classentity = new VWCGSyncProClass();
                    classentity.ClassId = StringUtils.GetDbInt(dr["ClassId"]);
                    classentity.ProductId = StringUtils.GetDbInt(dr["ProductId"]);
                    result.NotTYClassPros.Add(classentity);
                }
                DataTable _dt6 = ds.Tables[6];
                foreach (DataRow dr in _dt6.Rows)
                {
                    VWCGSyncProCarType entity = new VWCGSyncProCarType();
                    entity.ProductId = StringUtils.GetDbInt(dr["ProductId"]);
                    entity.CarTypeId1 = StringUtils.GetDbInt(dr["CarTypeId1"]);
                    entity.CarTypeId2 = StringUtils.GetDbInt(dr["CarTypeId2"]);
                    entity.CarTypeId3 = StringUtils.GetDbInt(dr["CarTypeId3"]);
                    entity.CarTypeId4 = StringUtils.GetDbInt(dr["CarTypeId4"]);
                    result.NotTYProCarTypesDetail.Add(entity);
                }
            } 
            return result;
        }
        public VWProductEntity GetProVWByDetailId(int productdetailid)
        {
            string sql = @"SELECT pd.Id as ProductDetailId,a.Id as  ProductId,a.Code, a.[AdTitle] ,a.[Title] , a.[BrandId],a.[Cost],
                            pd.[Price],a.[MarketPrice],pd.IsBP,
                            pd.[TradePrice]  ,pd.[DealerPrice] ,pd.CGMemId,pd.IsAhmTake ,pd.JiShiSong,pd.TransFeeType ,pd.TransFee ,a.[PicUrl],a.PicSuffix  ,a.[ClassId],pd.[StockNum] ,a.[Status] ,pd.[Status] as ProductDetailStatus ,pd.StockNum,pd.SaleNum,a.Sort,a.Unit,a.Spec1,a.Spec2,a.ShowPicType,a.Retail,a.Wholesale,
                            a.Name,a.GrossWeight,a.PlaceOrigin,a.SiteId,ProductType,pe.DetailDescrip AS  ContentCms 
                            FROM 		dbo.[Product] a  WITH(NOLOCK) inner join ProductDetail pd WITH(NOLOCK) on a.id=pd.ProductId  left join  ProductExt pe WITH(NOLOCK) on a.Id=pe.ProductId  WHERE pd.Id=@ProductDetailId   ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@ProductDetailId", DbType.Int32, productdetailid);
            VWProductEntity entity = new VWProductEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.GrossWeight = StringUtils.GetDbDecimal(reader["GrossWeight"]);
                    entity.PlaceOrigin = StringUtils.GetDbString(reader["PlaceOrigin"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]); 
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.DealerPrice = StringUtils.GetDbDecimal(reader["DealerPrice"]);
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.StockNum = StringUtils.GetDbInt(reader["StockNum"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.ShowPicType = StringUtils.GetDbInt(reader["ShowPicType"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);

                    entity.Retail = StringUtils.GetDbInt(reader["Retail"]);
                    entity.Wholesale = StringUtils.GetDbInt(reader["Wholesale"]);
                    entity.TransFeeType = StringUtils.GetDbInt(reader["TransFeeType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.Unit = StringUtils.GetDbInt(reader["Unit"]);
                    entity.SaleNum = StringUtils.GetDbInt(reader["SaleNum"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]) & StringUtils.GetDbInt(reader["ProductDetailStatus"]);  
                    entity.IsBP = StringUtils.GetDbInt(reader["IsBP"]);  
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.IsAhmTake = StringUtils.GetDbInt(reader["IsAhmTake"]); 
                    entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);   
                    entity.ProductType = StringUtils.GetDbInt(reader["ProductType"]);
                    entity.ContentCms = StringUtils.GetDbString(reader["ContentCms"]);
                }
            }
            return entity;
        }

        public VWProductEntity GetProductVW(int productid )
        {
            string sql = @" SELECT  a.Id as  ProductId,a.Code, a.[AdTitle] ,a.[Title] ,  a.[BrandId], a.[BrandName],a.[Cost],
 a.[MarketPrice],  a.[PicUrl],a.PicSuffix  ,a.[ClassId],pd.[StockNum] ,pd.TradePrice ,a.[Status] , a.Sort,a.Unit,a.Spec1,a.Spec2,a.ShowPicType,a.Retail,a.Wholesale,
a.Name,a.GrossWeight,a.PlaceOrigin,a.SiteId,pp.Id as PropertyId, pp.Property1, pp.Property2,pp.Property3,pp.Property4,pp.Property5,pp.Property6,pp.Property7,pp.Property8,pp.Property9,pp.Property10, pe.DetailDescrip AS  ContentCms 
FROM  dbo.[Product] a  WITH(NOLOCK)  left join ProductDetail pd WITH(NOLOCK) on a.id=pd.ProductId
  left join ProductProperties pp WITH(NOLOCK) on a.id=pp.ProductId
left join ProductExt pe WITH(NOLOCK) on a.Id=pe.ProductId 
WHERE a.Id=@ProductId   ";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);  
            VWProductEntity entity = new VWProductEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                { 
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);  
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.GrossWeight = StringUtils.GetDbDecimal(reader["GrossWeight"]);
                    entity.PlaceOrigin = StringUtils.GetDbString(reader["PlaceOrigin"]); 
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]); 
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]); 
                    entity.BrandName = StringUtils.GetDbString(reader["BrandName"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]); 
                    entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]); 
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.ShowPicType = StringUtils.GetDbInt(reader["ShowPicType"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]); 
                    entity.Retail = StringUtils.GetDbInt(reader["Retail"]);
                    entity.Wholesale = StringUtils.GetDbInt(reader["Wholesale"]);  
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.Unit = StringUtils.GetDbInt(reader["Unit"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]); 
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]); 
                    entity.ContentCms = StringUtils.GetDbString(reader["ContentCms"]); 
                    entity.PropertyId = StringUtils.GetDbInt(reader["PropertyId"]);
                    entity.Property1 = StringUtils.GetDbString(reader["Property1"]);
                    entity.Property2 = StringUtils.GetDbString(reader["Property2"]); 
                    entity.Property3 = StringUtils.GetDbString(reader["Property3"]); 
                    entity.Property4 = StringUtils.GetDbString(reader["Property4"]); 
                    entity.Property5 = StringUtils.GetDbString(reader["Property5"]); 
                    entity.Property6 = StringUtils.GetDbString(reader["Property6"]); 
                    entity.Property7 = StringUtils.GetDbString(reader["Property7"]); 
                    entity.Property8 = StringUtils.GetDbString(reader["Property8"]); 
                    entity.Property9 = StringUtils.GetDbString(reader["Property9"]); 
                    entity.Property10 = StringUtils.GetDbString(reader["Property10"]);  
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<VWProductEntity> GetProductListFormProc(int pagesize, int pageindex, ref int recordCount, string classidstr, int brandid, string propertstr, int orderbytype,   string key,  int status)
        {
            string sql = @"EXEC [Proc_GetListProduct] @ClassIdStr,@BrandId,@PropertiesStr,@PageIndex,@PageSize,@OrderByType, @SearchKey,@Status  ";

            IList<VWProductEntity> entityList = new List<VWProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@ClassIdStr", DbType.String, classidstr);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, propertstr);
            db.AddInParameter(cmd, "@OrderByType", DbType.Int32, orderbytype); 
            db.AddInParameter(cmd, "@SearchKey", DbType.String, key);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);  
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable _dt = new DataTable();
            if (ds.Tables.Count == 2)
            {
                recordCount = StringUtils.GetDbInt(ds.Tables[0].Rows[0][0]);
                _dt = ds.Tables[1];
                foreach (DataRow dr in _dt.Rows)
                {
                    VWProductEntity entity = new VWProductEntity();
                    entity.SiteId = StringUtils.GetDbInt(dr["SiteId"]);
                    entity.AdTitle = StringUtils.GetDbString(dr["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(dr["Title"]); 
                    entity.BrandId = StringUtils.GetDbInt(dr["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(dr["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(dr["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(dr["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(dr["PicSuffix"]);
                    entity.ProductId = StringUtils.GetDbInt(dr["ProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(dr["ClassId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(dr["ProductDetailId"]);
                    entity.Cost = StringUtils.GetDbDecimal(dr["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(dr["TradePrice"]);
                    entity.DealerPrice = StringUtils.GetDbDecimal(dr["DealerPrice"]);
                    entity.StockNum = StringUtils.GetDbInt(dr["StockNum"]);
                    entity.TransFee = StringUtils.GetDbDecimal(dr["TransFee"]);
                    entity.TransFeeType = StringUtils.GetDbInt(dr["TransFeeType"]);
                    entity.SaleNum = StringUtils.GetDbInt(dr["SaleNum"]);
                    entity.StockNum = StringUtils.GetDbInt(dr["StockNum"]); 
                    entity.IsBP = StringUtils.GetDbInt(dr["IsBP"]);
                    entity.Unit = StringUtils.GetDbInt(dr["Unit"]);  
                    entity.CGMemId = StringUtils.GetDbInt(dr["CGMemId"]);   
                    entity.Property1 = StringUtils.GetDbString(dr["Property1"]);
                    entity.Property2 = StringUtils.GetDbString(dr["Property2"]);
                    entity.Property3 = StringUtils.GetDbString(dr["Property3"]);
                    entity.Property4 = StringUtils.GetDbString(dr["Property4"]);
                    entity.Property5 = StringUtils.GetDbString(dr["Property5"]);
                    entity.Property6 = StringUtils.GetDbString(dr["Property6"]);
                    entity.Property7 = StringUtils.GetDbString(dr["Property7"]);
                    entity.Property8 = StringUtils.GetDbString(dr["Property8"]);
                    entity.Property9 = StringUtils.GetDbString(dr["Property9"]);
                    entity.Property10 = StringUtils.GetDbString(dr["Property10"]);
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
        public IList<VWProductEntity> GetProductListProcCYC(int pagesize, int pageindex, ref int recordCount, string classidstr, int brandid, string propertstr, int orderbytype, int producttype, int cartype,  int jishi, int status)
        {
            string sql = @"EXEC [Proc_GetListByPageCYC] @ClassIdStr,@BrandId,@PropertiesStr,@PageIndex,@PageSize,@OrderByType,@ProductType,@CarType,@Status,@JiShiSong";

            IList<VWProductEntity> entityList = new List<VWProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@ClassIdStr", DbType.String, classidstr);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, propertstr);
            db.AddInParameter(cmd, "@OrderByType", DbType.Int32, orderbytype);
            db.AddInParameter(cmd, "@ProductType", DbType.Int32, producttype);
            db.AddInParameter(cmd, "@CarType", DbType.Int32, cartype);  
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            db.AddInParameter(cmd, "@JiShiSong", DbType.Int32, jishi);
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable _dt = new DataTable();
            if (ds.Tables.Count == 2)
            {
                recordCount = StringUtils.GetDbInt(ds.Tables[0].Rows[0][0]);
                _dt = ds.Tables[1];
                foreach (DataRow dr in _dt.Rows)
                {
                    VWProductEntity entity = new VWProductEntity();
                    entity.SiteId = StringUtils.GetDbInt(dr["SiteId"]);
                    entity.AdTitle = StringUtils.GetDbString(dr["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(dr["Title"]); 
                    entity.BrandId = StringUtils.GetDbInt(dr["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(dr["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(dr["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(dr["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(dr["PicSuffix"]);
                    entity.ProductId = StringUtils.GetDbInt(dr["ProductId"]);
                    entity.ClassId = StringUtils.GetDbInt(dr["ClassId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(dr["ProductDetailId"]);
                    entity.Cost = StringUtils.GetDbDecimal(dr["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(dr["TradePrice"]);
                    entity.DealerPrice = StringUtils.GetDbDecimal(dr["DealerPrice"]);
                    entity.StockNum = StringUtils.GetDbInt(dr["StockNum"]);
                    entity.TransFee = StringUtils.GetDbDecimal(dr["TransFee"]);
                    entity.TransFeeType = StringUtils.GetDbInt(dr["TransFeeType"]);
                    entity.SaleNum = StringUtils.GetDbInt(dr["SaleNum"]);
                    entity.StockNum = StringUtils.GetDbInt(dr["StockNum"]);
                    entity.ProductType = StringUtils.GetDbInt(dr["ProductType"]);
                    entity.IsBP = StringUtils.GetDbInt(dr["IsBP"]);
                    entity.Unit = StringUtils.GetDbInt(dr["Unit"]);
                    entity.ListShowMethod = StringUtils.GetDbInt(dr["ListShowMethod"]);
                    entity.CGMemId = StringUtils.GetDbInt(dr["CGMemId"]);
                    entity.IsAhmTake = StringUtils.GetDbInt(dr["IsAhmTake"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;

        }


         public IList<ProductEntity> GetProductList(int pagesize, int pageindex, ref int recordCount,string productName ,string classidstr,int _siteid )
        {
            string where = " where  1=1 "; 
            string innerstr = " ";
            if(!string.IsNullOrEmpty(classidstr))
            {
                innerstr = " inner join dbo.fun_splitstr(@ClassIdStr,',') b  	on a.ClassId=b.Id ";
            }
            if (!string.IsNullOrEmpty(productName))
            {
                where += " and AdTitle like @AdTitle ";
            }  
            if(_siteid>0)
            {
                where += " and SiteId=@SiteId ";

            }
            string sql = @"SELECT   *
            			FROM
            			(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
            			 a.* from dbo.[Product] a WITH(NOLOCK)	
              			" + innerstr + where+@") as temp 
            			where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Product] a with (nolock) "+ innerstr + where;
            IList<ProductEntity> entityList = new List<ProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(classidstr))
            {
                db.AddInParameter(cmd, "@ClassIdStr", DbType.String, classidstr); 
            }
            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@AdTitle",DbType.String,"%"+ productName + "%");
            }
            if (_siteid > 0)
            { 
                db.AddInParameter(cmd, "@SiteId", DbType.Int32, _siteid);

            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductEntity entity = new ProductEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);  
                    entity.Unit = StringUtils.GetDbInt(reader["Unit"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.ShowPicType = StringUtils.GetDbInt(reader["ShowPicType"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Retail = StringUtils.GetDbInt(reader["Retail"]);
                    entity.Wholesale = StringUtils.GetDbInt(reader["Wholesale"]);  
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(classidstr))
            {
                db.AddInParameter(cmd, "@ClassIdStr", DbType.String, classidstr);
            }
            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@AdTitle", DbType.String, "%" + productName + "%");
            }


            if (_siteid > 0)
            {
                db.AddInParameter(cmd, "@SiteId", DbType.Int32, _siteid);

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
        public DataSet GetDataTableByClassId(int classid ,int producttypeid, int productstatus)
        {
            string sql = @"
SELECT cf.name AS ClassName,ps.AdTitle AS StyleName,b.name AS BrandName, ISNULL(pe.Manufacturer,'') AS FacName,ISNULL(a.Name,'') AS ProductName,pe.SpecModel AS SpecModel,a.Code,
pe.OEMPartNos,ISNULL(due.NAME,'') AS UnitName,ISNULL(due2.NAME,'') AS PackUnitName,CAST (a.PackNum AS VARCHAR) AS PackNum,pe.SpecPacking AS SpecPacking,pe.APPModels AS AppCarModel,a.AdTitle,CASE WHEN IsOEM=1 THEN 'OEM' ELSE '品牌' END  AS OEMType,
Spec1,Spec2,CAST (a.Cost as  VARCHAR) AS CostPrice,CAST (pd.TradePrice AS VARCHAR) AS TradePrice,CAST (pd.DealerPrice AS VARCHAR) AS DealerPrice,
CASE WHEN pd.ProductType=1 THEN '常规' WHEN pd.ProductType=2 THEN '特价' END AS PriceType ,CAST (pd.StockNum AS VARCHAR) AS StockNum,CAST (a.id  AS VARCHAR) AS ProductId,
CAST(pd.TransFee AS VARCHAR) AS TransFee,CAST(pd.TransFeeType AS VARCHAR) AS TransFeeType,case when pd.Status=0 then '已下架' when a.Status=0 then '已下架' else '已处理' End AS  OrperateType,'' AS ReturnNMsg
 FROM  dbo.Product  a WITH(NOLOCK) INNER JOIN  JcCatograyDB.dbo.Brand
b  WITH(NOLOCK) ON a.BrandId=b.Id  INNER JOIN dbo.ProductStyle ps WITH(NOLOCK) ON a.StyleId=ps.Id
LEFT JOIN dbo.ProductDetail pd WITH(NOLOCK) ON a.Id=pd.ProductId
INNER JOIN JcCatograyDB.dbo.ClassesFound cf WITH(NOLOCK) ON a.classId=cf.Id
LEFT JOIN JcCatograyDB.dbo.DicUnitEnum due  WITH(NOLOCK) ON a.Unit=due.ID
LEFT JOIN JcCatograyDB.dbo.DicUnitEnum due2  WITH(NOLOCK) ON a.PackUnit=due2.ID
LEFT JOIN  dbo.ProductExt pe  WITH(NOLOCK) ON a.Id=pe.ProductId
 WHERE a.ClassId=@ClassId    	";
            if (producttypeid > 0)
            {
                sql += " and pd.ProductType=@ProductType ";
            }
            if (productstatus != -1)
            {
                sql += " and a.status=@Status ";
            }
            if (productstatus > 0)
            {
                sql += " and pd.status=@Status ";
            }
            IList<VWProductEntity> entityList = new List<VWProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            if (producttypeid > 0)
            { 
                db.AddInParameter(cmd, "@ProductType", DbType.Int32, producttypeid);
            }
            if (productstatus != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, productstatus);
            } 
            DataSet ds = db.ExecuteDataSet(cmd);
            
            return ds;
        }
        public IList<VWProductEntity> GetListSpecsByStyleId(int styleid,int producttype,int cgmemid)
        {
            string sql = @"SELECT    a.[Id] as ProductId,a.[SiteId], [Spec1],[Spec2],[Spec3],PicUrl,PicSuffix,b.Id AS ProductDetailId  from
dbo.[Product] a WITH(NOLOCK)   inner join dbo.[ProductDetail] b  WITH(NOLOCK) on a.Id=b.ProductId
where a.StyleId=@StyleId  and b.ProductType=@ProductType and b.CGMemId=@CGMemId and a.Status=1  and b.Status=1";
            IList<VWProductEntity> entityList = new List<VWProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@StyleId", DbType.Int32, styleid);
            db.AddInParameter(cmd, "@ProductType", DbType.Int32, producttype);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
           
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductEntity entity = new VWProductEntity();
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]); 
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]); 
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<ClassesFoundEntity> GetClassesListAll(int classtypeid,int producttypeid,int productstatus)
        {
            string sql = @"SELECT DISTINCT b.* FROM JcCatograyDB.dbo.ClassesFound  b WITH(NOLOCK) LEFT JOIN 
dbo.[Product] a WITH(NOLOCK)  ON b.id=a.ClassId  LEFT JOIN 
dbo.[ProductDetail] pd WITH(NOLOCK)  ON a.id=pd.ProductId 
WHERE ClassType=@ClassType   ";
            if (producttypeid > 0)
            {
                sql += " and pd.ProductType=@ProductType ";
            }
            if (productstatus != -1)
            {
                sql += " and a.status=@Status "; 
            }
            if (productstatus >0)
            {
                sql += " and pd.status=@Status ";
            }
            IList<ClassesFoundEntity> entityList = new List<ClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassType", DbType.Int32, classtypeid);
            if (producttypeid > 0)
            { 
                db.AddInParameter(cmd, "@ProductType", DbType.Int32, producttypeid);
            }
            if (productstatus != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, productstatus);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassesFoundEntity entity = new ClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.FullName = StringUtils.GetDbString(reader["FullName"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.PropertiesClassId = StringUtils.GetDbInt(reader["PropertiesClassId"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);
                    entity.RedirectClassId = StringUtils.GetDbInt(reader["RedirectClassId"]);
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
        public IList<ProductEntity> GetProductAll()
        {

            string sql = @"SELECT   * from dbo.[Product] a WITH(NOLOCK)	
 	";
		    IList<ProductEntity> entityList = new List<ProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductEntity entity=new ProductEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.AdTitle=StringUtils.GetDbString(reader["AdTitle"]);  
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.Spec1=StringUtils.GetDbString(reader["Spec1"]);
					entity.Spec2=StringUtils.GetDbString(reader["Spec2"]);
					entity.ShowPicType=StringUtils.GetDbInt(reader["ShowPicType"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.Retail=StringUtils.GetDbInt(reader["Retail"]);
					entity.Wholesale=StringUtils.GetDbInt(reader["Wholesale"]); 
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
        public int  ExistNum(ProductEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Product] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "  (BrandId=@BrandId) and ClassId=@ClassId and AdTitle=@AdTitle "; 
            }
            else
            {
                where = where + " id<>@Id and  (BrandId=@BrandId)  and ClassId=@ClassId and AdTitle=@AdTitle ";
            }
            int i = 0;
            if(!string.IsNullOrEmpty(entity.Spec1))
            {
                where = where + " and  (Spec1=@Spec1)   "; 
            }
            if (!string.IsNullOrEmpty(entity.Spec2))
            {
                where = where + " and  (Spec2=@Spec2)   ";
            }
            if (!string.IsNullOrEmpty(entity.Spec3))
            {
                where = where + " and  (Spec3=@Spec3)   ";
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
            if (!string.IsNullOrEmpty(entity.Spec1))
            {
                db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            }
            if (!string.IsNullOrEmpty(entity.Spec2))
            {
                db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            }
            if (!string.IsNullOrEmpty(entity.Spec3))
            {
                db.AddInParameter(cmd, "@Spec3", DbType.String, entity.Spec3);
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        public int GetProductIdByName(ProductEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select top 1 Id from dbo.[Product] WITH(NOLOCK) ";
            string where = "where ";
    
                where = where + "  (BrandId=@BrandId) and ClassId=@ClassId and AdTitle=@AdTitle ";
          
            if (!string.IsNullOrEmpty(entity.Spec1))
            {
                where = where + " and  (Spec1=@Spec1)   ";
            }
            if (!string.IsNullOrEmpty(entity.Spec2))
            {
                where = where + " and  (Spec2=@Spec2)   ";
            }
            if (!string.IsNullOrEmpty(entity.Spec3))
            {
                where = where + " and  (Spec3=@Spec3)   ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
     
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@AdTitle", DbType.String, entity.AdTitle);
            if (!string.IsNullOrEmpty(entity.Spec1))
            {
                db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            }
            if (!string.IsNullOrEmpty(entity.Spec2))
            {
                db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            }
            if (!string.IsNullOrEmpty(entity.Spec3))
            {
                db.AddInParameter(cmd, "@Spec3", DbType.String, entity.Spec3);
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }






        #endregion
        #endregion
    }
}
