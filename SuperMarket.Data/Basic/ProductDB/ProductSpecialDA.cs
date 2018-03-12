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
功能描述：ProductSpecial表的数据访问类。
创建时间：2017/5/16 11:30:28
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductSpecialEntity的数据访问操作
	/// </summary>
	public partial class ProductSpecialDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductSpecialDA Instance
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
            internal static readonly ProductSpecialDA instance = new ProductSpecialDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductSpecial，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productSpecial">待插入的实体对象</param>
		public int AddProductSpecial(ProductSpecialEntity entity)
		{
		   string sql= @"insert into ProductSpecial( [ShortName],[FullName],[Remark],[EnPicUrl],[EnPicSuffix],[ExPicUrl],[ExPicSuffix],[Sort],[BeginTime],[EndTime],[IsActive],[SystemType],[SpecialType],RedirectUrl)VALUES
			            ( @ShortName,@FullName,@Remark,@EnPicUrl,@EnPicSuffix,@ExPicUrl,@ExPicSuffix,@Sort,@BeginTime,@EndTime,@IsActive,@SystemType,@SpecialType,@RedirectUrl);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ShortName",  DbType.String,entity.ShortName);
			db.AddInParameter(cmd,"@FullName",  DbType.String,entity.FullName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@EnPicUrl",  DbType.String,entity.EnPicUrl);
			db.AddInParameter(cmd,"@EnPicSuffix",  DbType.String,entity.EnPicSuffix);
			db.AddInParameter(cmd,"@ExPicUrl",  DbType.String,entity.ExPicUrl);
			db.AddInParameter(cmd,"@ExPicSuffix",  DbType.String,entity.ExPicSuffix);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@SystemType",  DbType.Int32,entity.SystemType);
			db.AddInParameter(cmd,"@SpecialType",  DbType.Int32,entity.SpecialType);  
			db.AddInParameter(cmd, "@RedirectUrl",  DbType.String, entity.RedirectUrl); 

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productSpecial">待更新的实体对象</param>
		public   int UpdateProductSpecial(ProductSpecialEntity entity)
		{
			string sql= @" UPDATE dbo.[ProductSpecial] SET
                       [ShortName]=@ShortName,[FullName]=@FullName,[Remark]=@Remark,[EnPicUrl]=@EnPicUrl,[EnPicSuffix]=@EnPicSuffix,[ExPicUrl]=@ExPicUrl,[ExPicSuffix]=@ExPicSuffix,[Sort]=@Sort,[BeginTime]=@BeginTime,[EndTime]=@EndTime,[IsActive]=@IsActive,[SystemType]=@SystemType,[SpecialType]=@SpecialType,RedirectUrl=@RedirectUrl
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ShortName",  DbType.String,entity.ShortName);
			db.AddInParameter(cmd,"@FullName",  DbType.String,entity.FullName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@EnPicUrl",  DbType.String,entity.EnPicUrl);
			db.AddInParameter(cmd,"@EnPicSuffix",  DbType.String,entity.EnPicSuffix);
			db.AddInParameter(cmd,"@ExPicUrl",  DbType.String,entity.ExPicUrl);
			db.AddInParameter(cmd,"@ExPicSuffix",  DbType.String,entity.ExPicSuffix);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@SystemType",  DbType.Int32,entity.SystemType);
			db.AddInParameter(cmd,"@SpecialType",  DbType.Int32,entity.SpecialType);  
			db.AddInParameter(cmd, "@RedirectUrl",  DbType.String, entity.RedirectUrl); 

             return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductSpecialByKey(int id)
	    {
			string sql=@"delete from ProductSpecial where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSpecialDisabled()
        {
            string sql = @"delete from  ProductSpecial  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSpecialByIds(string ids)
        {
            string sql = @"Delete from ProductSpecial  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSpecialByIds(string ids)
        {
            string sql = @"Update   ProductSpecial set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductSpecialEntity GetProductSpecial(int id)
		{
			string sql= @"SELECT  [Id],[ShortName],[FullName],[Remark],[EnPicUrl],[EnPicSuffix],[ExPicUrl],[ExPicSuffix],[Sort],[BeginTime],[EndTime],[IsActive],[SystemType],[SpecialType],RedirectUrl
							FROM
							dbo.[ProductSpecial] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductSpecialEntity entity=new ProductSpecialEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ShortName=StringUtils.GetDbString(reader["ShortName"]);
					entity.FullName=StringUtils.GetDbString(reader["FullName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.EnPicUrl=StringUtils.GetDbString(reader["EnPicUrl"]);
					entity.EnPicSuffix=StringUtils.GetDbString(reader["EnPicSuffix"]);
					entity.ExPicUrl=StringUtils.GetDbString(reader["ExPicUrl"]);
					entity.ExPicSuffix=StringUtils.GetDbString(reader["ExPicSuffix"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
					entity.SpecialType=StringUtils.GetDbInt(reader["SpecialType"]); 
					entity.RedirectUrl = StringUtils.GetDbString(reader["RedirectUrl"]); 

                }
   		    }
            return entity;
		}


        public ProductSpecialEntity GetProductSpecialByCode(string code)
        {
            string sql = @"SELECT  [Id],[ShortName],[FullName],[Remark],[EnPicUrl],[EnPicSuffix],[ExPicUrl],[ExPicSuffix],[Sort],[BeginTime],[EndTime],[IsActive],[SystemType],[SpecialType],RedirectUrl
							FROM
							dbo.[ProductSpecial] WITH(NOLOCK)	
							WHERE [Code]=@Code ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, code);
            ProductSpecialEntity entity = new ProductSpecialEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ShortName = StringUtils.GetDbString(reader["ShortName"]);
                    entity.FullName = StringUtils.GetDbString(reader["FullName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.EnPicUrl = StringUtils.GetDbString(reader["EnPicUrl"]);
                    entity.EnPicSuffix = StringUtils.GetDbString(reader["EnPicSuffix"]);
                    entity.ExPicUrl = StringUtils.GetDbString(reader["ExPicUrl"]);
                    entity.ExPicSuffix = StringUtils.GetDbString(reader["ExPicSuffix"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.BeginTime = StringUtils.GetDbDateTime(reader["BeginTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]);
                    entity.SpecialType = StringUtils.GetDbInt(reader["SpecialType"]);
                    entity.RedirectUrl = StringUtils.GetDbString(reader["RedirectUrl"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<ProductSpecialEntity> GetProductSpecialList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[ShortName],[FullName],[Remark],[EnPicUrl],[EnPicSuffix],[ExPicUrl],[ExPicSuffix],[Sort],[BeginTime],[EndTime],[IsActive],[SystemType],[SpecialType],RedirectUrl
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ShortName],[FullName],[Remark],[EnPicUrl],[EnPicSuffix],[ExPicUrl],[ExPicSuffix],[Sort],[BeginTime],[EndTime],[IsActive],[SystemType],[SpecialType],RedirectUrl from dbo.[ProductSpecial] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductSpecial] with (nolock) ";
            IList<ProductSpecialEntity> entityList = new List< ProductSpecialEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductSpecialEntity entity=new ProductSpecialEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ShortName=StringUtils.GetDbString(reader["ShortName"]);
					entity.FullName=StringUtils.GetDbString(reader["FullName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.EnPicUrl=StringUtils.GetDbString(reader["EnPicUrl"]);
					entity.EnPicSuffix=StringUtils.GetDbString(reader["EnPicSuffix"]);
					entity.ExPicUrl=StringUtils.GetDbString(reader["ExPicUrl"]);
					entity.ExPicSuffix=StringUtils.GetDbString(reader["ExPicSuffix"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
					entity.SpecialType=StringUtils.GetDbInt(reader["SpecialType"]);
                    entity.RedirectUrl = StringUtils.GetDbString(reader["RedirectUrl"]); 
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
        public IList<ProductSpecialEntity> GetProductSpecialAll()
        {

            string sql = @"SELECT    [Id],[ShortName],[FullName],[Remark],[EnPicUrl],[EnPicSuffix],[ExPicUrl],[ExPicSuffix],[Sort],[BeginTime],[EndTime],[IsActive],[SystemType],[SpecialType],RedirectUrl from dbo.[ProductSpecial] WITH(NOLOCK)	";
		    IList<ProductSpecialEntity> entityList = new List<ProductSpecialEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductSpecialEntity entity=new ProductSpecialEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ShortName=StringUtils.GetDbString(reader["ShortName"]);
					entity.FullName=StringUtils.GetDbString(reader["FullName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.EnPicUrl=StringUtils.GetDbString(reader["EnPicUrl"]);
					entity.EnPicSuffix=StringUtils.GetDbString(reader["EnPicSuffix"]);
					entity.ExPicUrl=StringUtils.GetDbString(reader["ExPicUrl"]);
					entity.ExPicSuffix=StringUtils.GetDbString(reader["ExPicSuffix"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
					entity.SpecialType=StringUtils.GetDbInt(reader["SpecialType"]);
                    entity.RedirectUrl = StringUtils.GetDbString(reader["RedirectUrl"]);
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
        public int  ExistNum(ProductSpecialEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductSpecial] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (ShortName=@ShortName) ";
					     where = where+ "  (FullName=@FullName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (ShortName=@ShortName) ";
					     where = where+ " id<>@Id and  (FullName=@FullName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@ShortName", DbType.String, entity.ShortName); 
					
            db.AddInParameter(cmd, "@FullName", DbType.String, entity.FullName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
