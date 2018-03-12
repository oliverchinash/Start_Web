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
功能描述：ProductSimilarHot表的数据访问类。
创建时间：2016/9/8 15:01:47
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductSimilarHotEntity的数据访问操作
	/// </summary>
	public partial class ProductSimilarHotDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductSimilarHotDA Instance
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
            internal static readonly ProductSimilarHotDA instance = new ProductSimilarHotDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductSimilarHot，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productSimilarHot">待插入的实体对象</param>
		public int AddProductSimilarHot(ProductSimilarHotEntity entity)
		{
		   string sql=@"insert into ProductSimilarHot( [ClassId],[ProductId],[SalesNum],[Sort])VALUES
			            ( @ClassId,@ProductId,@SalesNum,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@SalesNum",  DbType.Int32,entity.SalesNum);
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
		/// <param name="productSimilarHot">待更新的实体对象</param>
		public   int UpdateProductSimilarHot(ProductSimilarHotEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductSimilarHot] SET
                       [ClassId]=@ClassId,[ProductId]=@ProductId,[SalesNum]=@SalesNum,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@SalesNum",  DbType.Int32,entity.SalesNum);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductSimilarHotByKey(int id)
	    {
			string sql=@"delete from ProductSimilarHot where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSimilarHotDisabled()
        {
            string sql = @"delete from  ProductSimilarHot  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSimilarHotByIds(string ids)
        {
            string sql = @"Delete from ProductSimilarHot  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSimilarHotByIds(string ids)
        {
            string sql = @"Update   ProductSimilarHot set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public ProductSimilarHotEntity  GetProductSimilarHot(int id)
		{
			string sql=@"SELECT  [Id],[ClassId],[ProductId],[SalesNum],[Sort]
							FROM
							dbo.[ProductSimilarHot] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductSimilarHotEntity entity=new ProductSimilarHotEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.SalesNum=StringUtils.GetDbInt(reader["SalesNum"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}
        public IList<VWProductEntity> GetHotProducts(int classid, int size,int producttype=1)
        {
            string sql = @"SELECT  top " + size + @"  a.ProductId,[AdTitle],[Title],[BrandId],pd.[Price],[MarketPrice],[PicUrl]
,a.[ClassId], [Cost],pd.[TradePrice], pd.[TransFee],
pd.[TransFeeType], a.Sort from dbo.[ProductSimilarHot] a WITH(NOLOCK)	
INNER JOIN dbo.[Product]  b WITH(NOLOCK)  ON a.ProductId=b.Id AND a.ClassId=@ClassId 
inner join dbo.[ProductDetail] pd WITH(NOLOCK) on b.Id=pd.ProductId and pd.ProductType=@ProductType
						 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            db.AddInParameter(cmd, "@ProductType", DbType.Int32, producttype);
            IList<VWProductEntity> list = new List<VWProductEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    VWProductEntity entity = new VWProductEntity();
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.AdTitle = StringUtils.GetDbString(reader["AdTitle"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                      entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                      entity.Cost = StringUtils.GetDbDecimal(reader["Cost"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.TransFeeType = StringUtils.GetDbInt(reader["TransFeeType"]);
                     list.Add(entity);
                }
            }
            return list;
        }

        public IList<VWProductSimilarHotEntity> GetHotProductsByClassId(int classid,int num )
        {
            string where = " where 1=1 ";
            if(classid>=0)
            {
                where += " and a.ClassId =@ClassId ";
            }
            string sql = @"SELECT top "+ num + @" a.SalesNum, ProductDetailId
						FROM dbo.[ProductSimilarHot] a WITH(NOLOCK)	  
							 " + where+" ORDER BY a.Sort desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if(classid>=0)
            {
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            } 
            IList<VWProductSimilarHotEntity> list = new List<VWProductSimilarHotEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductSimilarHotEntity entity = new VWProductSimilarHotEntity(); 
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);  
                    entity.SalesNum = StringUtils.GetDbInt(reader["SalesNum"]);
                    list.Add(entity);
                }
            }
            return list;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<ProductSimilarHotEntity> GetProductSimilarHotList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ClassId],[ProductId],[SalesNum],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ClassId],[ProductId],[SalesNum],[Sort] from dbo.[ProductSimilarHot] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductSimilarHot] with (nolock) ";
            IList<ProductSimilarHotEntity> entityList = new List< ProductSimilarHotEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductSimilarHotEntity entity=new ProductSimilarHotEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.SalesNum=StringUtils.GetDbInt(reader["SalesNum"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
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
        public IList<ProductSimilarHotEntity> GetProductSimilarHotAll()
        {

            string sql = @"SELECT    [Id],[ClassId],[ProductId],[SalesNum],[Sort] from dbo.[ProductSimilarHot] WITH(NOLOCK)	";
		    IList<ProductSimilarHotEntity> entityList = new List<ProductSimilarHotEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductSimilarHotEntity entity=new ProductSimilarHotEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.SalesNum=StringUtils.GetDbInt(reader["SalesNum"]);
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
        public int  ExistNum(ProductSimilarHotEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductSimilarHot] WITH(NOLOCK) ";
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
