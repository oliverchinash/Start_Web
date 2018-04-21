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
功能描述：ProductProperties表的数据访问类。
创建时间：2018/4/14 16:58:58
创 建 人：lgzh
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductPropertiesEntity的数据访问操作
	/// </summary>
	public partial class ProductPropertiesDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ProductPropertiesDA Instance
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
            internal static readonly ProductPropertiesDA instance = new ProductPropertiesDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductProperties，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productProperties">待插入的实体对象</param>
		public int AddProductProperties(ProductPropertiesEntity entity)
		{
		   string sql=@"insert into ProductProperties( [ProductId],[Property1],[Property2],[Property3],[Property4],[Property5],[Property6],[Property7],[Property8],[Property9],[Property10])VALUES
			            ( @ProductId,@Property1,@Property2,@Property3,@Property4,@Property5,@Property6,@Property7,@Property8,@Property9,@Property10);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@Property1",  DbType.String,entity.Property1);
			db.AddInParameter(cmd,"@Property2",  DbType.String,entity.Property2);
			db.AddInParameter(cmd,"@Property3",  DbType.String,entity.Property3);
			db.AddInParameter(cmd,"@Property4",  DbType.String,entity.Property4);
			db.AddInParameter(cmd,"@Property5",  DbType.String,entity.Property5);
			db.AddInParameter(cmd,"@Property6",  DbType.String,entity.Property6);
			db.AddInParameter(cmd,"@Property7",  DbType.String,entity.Property7);
			db.AddInParameter(cmd,"@Property8",  DbType.String,entity.Property8);
			db.AddInParameter(cmd,"@Property9",  DbType.String,entity.Property9);
			db.AddInParameter(cmd,"@Property10",  DbType.String,entity.Property10);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productProperties">待更新的实体对象</param>
		public   int UpdateProductProperties(ProductPropertiesEntity entity)
		{
			string sql= @" UPDATE dbo.[ProductProperties] SET
                        [Property1]=@Property1,[Property2]=@Property2,[Property3]=@Property3,[Property4]=@Property4,[Property5]=@Property5,[Property6]=@Property6,[Property7]=@Property7,[Property8]=@Property8,[Property9]=@Property9,[Property10]=@Property10
                       WHERE [Id]=@Id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			 
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Property1",  DbType.String,entity.Property1);
			db.AddInParameter(cmd,"@Property2",  DbType.String,entity.Property2);
			db.AddInParameter(cmd,"@Property3",  DbType.String,entity.Property3);
			db.AddInParameter(cmd,"@Property4",  DbType.String,entity.Property4);
			db.AddInParameter(cmd,"@Property5",  DbType.String,entity.Property5);
			db.AddInParameter(cmd,"@Property6",  DbType.String,entity.Property6);
			db.AddInParameter(cmd,"@Property7",  DbType.String,entity.Property7);
			db.AddInParameter(cmd,"@Property8",  DbType.String,entity.Property8);
			db.AddInParameter(cmd,"@Property9",  DbType.String,entity.Property9);
			db.AddInParameter(cmd,"@Property10",  DbType.String,entity.Property10);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductPropertiesByKey(int id)
	    {
			string sql=@"delete from ProductProperties where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductPropertiesEntity GetProductProperties(int id)
		{
			string sql=@"SELECT  [Id],[ProductId],[Property1],[Property2],[Property3],[Property4],[Property5],[Property6],[Property7],[Property8],[Property9],[Property10]
							FROM
							dbo.[ProductProperties] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductPropertiesEntity entity=new ProductPropertiesEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);;
					entity.Property1=StringUtils.GetDbString(reader["Property1"]);;
					entity.Property2=StringUtils.GetDbString(reader["Property2"]);;
					entity.Property3=StringUtils.GetDbString(reader["Property3"]);;
					entity.Property4=StringUtils.GetDbString(reader["Property4"]);;
					entity.Property5=StringUtils.GetDbString(reader["Property5"]);;
					entity.Property6=StringUtils.GetDbString(reader["Property6"]);;
					entity.Property7=StringUtils.GetDbString(reader["Property7"]);;
					entity.Property8=StringUtils.GetDbString(reader["Property8"]);;
					entity.Property9=StringUtils.GetDbString(reader["Property9"]);;
					entity.Property10=StringUtils.GetDbString(reader["Property10"]);;
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductPropertiesEntity> GetProductPropertiesList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductId],[Property1],[Property2],[Property3],[Property4],[Property5],[Property6],[Property7],[Property8],[Property9],[Property10]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductId],[Property1],[Property2],[Property3],[Property4],[Property5],[Property6],[Property7],[Property8],[Property9],[Property10] from dbo.[ProductProperties] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductProperties] with (nolock) ";
            IList<ProductPropertiesEntity> entityList = new List< ProductPropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductPropertiesEntity entity=new ProductPropertiesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);;
					entity.Property1=StringUtils.GetDbString(reader["Property1"]);;
					entity.Property2=StringUtils.GetDbString(reader["Property2"]);;
					entity.Property3=StringUtils.GetDbString(reader["Property3"]);;
					entity.Property4=StringUtils.GetDbString(reader["Property4"]);;
					entity.Property5=StringUtils.GetDbString(reader["Property5"]);;
					entity.Property6=StringUtils.GetDbString(reader["Property6"]);;
					entity.Property7=StringUtils.GetDbString(reader["Property7"]);;
					entity.Property8=StringUtils.GetDbString(reader["Property8"]);;
					entity.Property9=StringUtils.GetDbString(reader["Property9"]);;
					entity.Property10=StringUtils.GetDbString(reader["Property10"]);;
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
		#endregion
		#endregion
	}
}
