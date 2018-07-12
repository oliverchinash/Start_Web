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
功能描述：ProductFineModule表的数据访问类。
创建时间：2018/7/11 18:06:20
创 建 人：lgzh
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductFineModuleEntity的数据访问操作
	/// </summary>
	public partial class ProductFineModuleDA : BaseSuperMarketDB
{
        #region 实例化
        public static ProductFineModuleDA Instance
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
            internal static readonly ProductFineModuleDA instance = new ProductFineModuleDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductFineModule，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productFineModule">待插入的实体对象</param>
		public int AddProductFineModule(ProductFineModuleEntity entity)
		{
		   string sql=@"insert into ProductFineModule( [Name],[Sort],[PicUrl],[Status],[ModuleType],[SiteId])VALUES
			            ( @name,@sort,@picUrl,@status,@moduleType,@siteId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@ModuleType",  DbType.Int32,entity.ModuleType);
			db.AddInParameter(cmd,"@SiteId",  DbType.Int32,entity.SiteId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productFineModule">待更新的实体对象</param>
		public   int UpdateProductFineModule(ProductFineModuleEntity entity)
		{
			string sql=@" UPDATE dbo.[ProductFineModule] SET
                       [Name]=@name,[Sort]=@sort,[PicUrl]=@picUrl,[Status]=@status,[ModuleType]=@moduleType,[SiteId]=@siteId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@ModuleType",  DbType.Int32,entity.ModuleType);
			db.AddInParameter(cmd,"@SiteId",  DbType.Int32,entity.SiteId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductFineModuleByKey(int id)
	    {
			string sql=@"delete from ProductFineModule where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductFineModuleEntity GetProductFineModule(int id)
		{
			string sql=@"SELECT  [Id],[Name],[Sort],[PicUrl],[Status],[ModuleType],[SiteId]
							FROM
							dbo.[ProductFineModule] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductFineModuleEntity entity=new ProductFineModuleEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Name=StringUtils.GetDbString(reader["Name"]);;
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);;
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
					entity.ModuleType=StringUtils.GetDbInt(reader["ModuleType"]);;
					entity.SiteId=StringUtils.GetDbInt(reader["SiteId"]);;
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductFineModuleEntity> GetProductFineModuleList(int siteid, int moduletype, int status  )
		{
           string where = " where SiteId=@SiteId and  ModuleType=@ModuleType and Status=@Status ";

            string sql=@" SELECT  
						 [Id],[Name],[Sort],[PicUrl],[Status],[ModuleType],[SiteId] from dbo.[ProductFineModule] WITH(NOLOCK)	
						"+ where  + " Order By Sort desc " ;
             
            IList<ProductFineModuleEntity> entityList = new List< ProductFineModuleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
		    db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
		    db.AddInParameter(cmd, "@Status", DbType.Int32, status);
		    db.AddInParameter(cmd, "@ModuleType", DbType.Int32, moduletype);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductFineModuleEntity entity=new ProductFineModuleEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Name=StringUtils.GetDbString(reader["Name"]);;
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);;
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
					entity.ModuleType=StringUtils.GetDbInt(reader["ModuleType"]);;
					entity.SiteId=StringUtils.GetDbInt(reader["SiteId"]);;
                    entityList.Add(entity);

                }
			 }
			 
            return entityList;
     	}
		#endregion
		#endregion
	}
}
