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
功能描述：ProductMenu表的数据访问类。
创建时间：2017/2/15 9:13:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductMenuEntity的数据访问操作
	/// </summary>
	public partial class ProductMenuDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductMenuDA Instance
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
            internal static readonly ProductMenuDA instance = new ProductMenuDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductMenu，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productMenu">待插入的实体对象</param>
		public int AddProductMenu(ProductMenuEntity entity)
		{
		   string sql=@"insert into ProductMenu( [ProductDetailId],[ClassId],[BrandId],[MenuType],[Sort],[ProductSmallPicUrl],[ProductPicUrl],[CreateTime],[BeginTime],[EndTime])VALUES
			            ( @ProductDetailId,@ClassId,@BrandId,@MenuType,@Sort,@ProductSmallPicUrl,@ProductPicUrl,@CreateTime,@BeginTime,@EndTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@MenuType",  DbType.Int32,entity.MenuType);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ProductSmallPicUrl",  DbType.String,entity.ProductSmallPicUrl);
			db.AddInParameter(cmd,"@ProductPicUrl",  DbType.String,entity.ProductPicUrl);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productMenu">待更新的实体对象</param>
		public   int UpdateProductMenu(ProductMenuEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductMenu] SET
                       [ProductDetailId]=@ProductDetailId,[ClassId]=@ClassId,[BrandId]=@BrandId,[MenuType]=@MenuType,[Sort]=@Sort,[ProductSmallPicUrl]=@ProductSmallPicUrl,[ProductPicUrl]=@ProductPicUrl,[CreateTime]=@CreateTime,[BeginTime]=@BeginTime,[EndTime]=@EndTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@MenuType",  DbType.Int32,entity.MenuType);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ProductSmallPicUrl",  DbType.String,entity.ProductSmallPicUrl);
			db.AddInParameter(cmd,"@ProductPicUrl",  DbType.String,entity.ProductPicUrl);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductMenuByKey(int id)
	    {
			string sql=@"delete from ProductMenu where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductMenuDisabled()
        {
            string sql = @"delete from  ProductMenu  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductMenuByIds(string ids)
        {
            string sql = @"Delete from ProductMenu  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductMenuByIds(string ids)
        {
            string sql = @"Update   ProductMenu set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductMenuEntity GetProductMenu(int id)
		{
			string sql=@"SELECT  [Id],[ProductDetailId],[ClassId],[BrandId],[MenuType],[Sort],[ProductSmallPicUrl],[ProductPicUrl],[CreateTime],[BeginTime],[EndTime]
							FROM
							dbo.[ProductMenu] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductMenuEntity entity=new ProductMenuEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.MenuType=StringUtils.GetDbInt(reader["MenuType"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ProductSmallPicUrl=StringUtils.GetDbString(reader["ProductSmallPicUrl"]);
					entity.ProductPicUrl=StringUtils.GetDbString(reader["ProductPicUrl"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductMenuEntity> GetProductMenuList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductDetailId],[ClassId],[BrandId],[MenuType],[Sort],[ProductSmallPicUrl],[ProductPicUrl],[CreateTime],[BeginTime],[EndTime]
						FROM (SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[ClassId],[BrandId],[MenuType],[Sort],[ProductSmallPicUrl],[ProductPicUrl],[CreateTime],[BeginTime],[EndTime] from dbo.[ProductMenu] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductMenu] with (nolock) ";
            IList<ProductMenuEntity> entityList = new List< ProductMenuEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductMenuEntity entity=new ProductMenuEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.MenuType=StringUtils.GetDbInt(reader["MenuType"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ProductSmallPicUrl=StringUtils.GetDbString(reader["ProductSmallPicUrl"]);
					entity.ProductPicUrl=StringUtils.GetDbString(reader["ProductPicUrl"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
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
        public IList<VWProductMenuEntity> GetProductMenuAll(int menutype,int isactive)
        {
            string where = " where 1=1 ";
            if(menutype!=-1)
            {
                where += " and MenuType=@MenuType ";
            }
            if (isactive != -1)
            {
                where += " and IsActive=@IsActive ";
            }
            if(menutype==(int)ProductMenuType.BPProduct)
            { 
                where += " and BeginTime<=getdate() and EndTime>=getdate() ";
            }
            string sql = @"SELECT    [Id],[ProductDetailId],[ClassId],[BrandId],Name,ShortTitle,[MenuType],[Sort],[ProductSmallPicUrl],[ProductPicUrl],[CreateTime],[BeginTime],[EndTime] from dbo.[ProductMenu] WITH(NOLOCK)	" + where+ " order by Sort desc";

            IList<VWProductMenuEntity> entityList = new List<VWProductMenuEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (menutype != -1)
            { 
                db.AddInParameter(cmd, "@MenuType", DbType.Int32, menutype);
            }
            if (isactive != -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWProductMenuEntity entity =new VWProductMenuEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]); 
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.Name = StringUtils.GetDbString(reader["Name"]); 
					entity.ShortTitle = StringUtils.GetDbString(reader["ShortTitle"]);  
                    entity.MenuType=StringUtils.GetDbInt(reader["MenuType"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ProductSmallPicUrl=StringUtils.GetDbString(reader["ProductSmallPicUrl"]);
					entity.ProductPicUrl=StringUtils.GetDbString(reader["ProductPicUrl"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
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
        public int  ExistNum(ProductMenuEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductMenu] WITH(NOLOCK) ";
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
