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
功能描述：StylePics表的数据访问类。
创建时间：2016/8/16 13:54:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// ProductStylePicsEntity的数据访问操作
	/// </summary>
	public partial class ProductStylePicsDA : BaseSuperMarketDB
	{
        #region 实例化
        public static ProductStylePicsDA Instance
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
            internal static readonly ProductStylePicsDA instance = new ProductStylePicsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StylePics，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="stylePics">待插入的实体对象</param>
		public int AddStylePics(ProductStylePicsEntity entity)
		{
		   string sql=@"insert into [ProductStylePics]( [StyleId],[PicUrl],[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],[CompressTime])VALUES
			            ( @StyleId,@PicUrl,@SizeX,@SizeY,@Weight,@Sort,@HasCompress,@CreateTime,@CompressTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl); 
            db.AddInParameter(cmd,"@Size_X",  DbType.Int32,entity.Size_X);
			db.AddInParameter(cmd,"@Size_Y",  DbType.Int32,entity.Size_Y);
			db.AddInParameter(cmd,"@Weight",  DbType.Decimal,entity.Weight);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@HasCompress",  DbType.Int32,entity.HasCompress);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CompressTime",  DbType.String,entity.CompressTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddProcStylePics(int styleid, int productid, string picstrs,string picdetailstr)
        {
            string sql = @"EXEC Proc_ProductPicsEdit @StyleId,@ProductId,@ProductPicsStr,@DescripPicsStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@StyleId", DbType.Int32, styleid);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@ProductPicsStr", DbType.String, picstrs); 
            db.AddInParameter(cmd, "@DescripPicsStr", DbType.String, picdetailstr);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="stylePics">待更新的实体对象</param>
        public   int UpdateStylePics(ProductStylePicsEntity entity)
		{
			string sql= @" UPDATE dbo.[ProductStylePics] SET
                       [StyleId]=@StyleId,[PicUrl]=substring(@PicUrl,1,(LEN(@PicUrl)-CHARINDEX('.',REVERSE(@PicUrl))) ),
PicSuffix=REVERSE(SUBSTRING(REVERSE(@PicUrl),1,CHARINDEX('.',REVERSE(@PicUrl))-1)),[Size_X]=@SizeX,[Size_Y]=@SizeY,[Weight]=@Weight,[Sort]=@Sort,[HasCompress]=@HasCompress,[CreateTime]=@CreateTime,[CompressTime]=@CompressTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@Size_X",  DbType.Int32,entity.Size_X);
			db.AddInParameter(cmd,"@Size_Y",  DbType.Int32,entity.Size_Y);
			db.AddInParameter(cmd,"@Weight",  DbType.Decimal,entity.Weight);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@HasCompress",  DbType.Int32,entity.HasCompress);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CompressTime",  DbType.String,entity.CompressTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteStylePicsByKey(int id)
	    {
			string sql=@"delete from StylePics where    Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
        public int ComPressComplete(string allids, string successids, string failids)
        {

            string sql = @"Update   ProductStylePics set HasCompress=1 ,CompressTime=GETDATE()  where HasCompress=2 AND  Id in (" + successids + @");
                Update ProductStylePics set HasCompress = 3, CompressTime = GETDATE()  where HasCompress=2 AND  Id in (" + failids + @");
                 Update ProductStylePics set HasCompress =0, CompressTime = GETDATE()  where HasCompress=2 AND  Id in (" + allids + "); ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);

        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStylePicsDisabled()
        {
            string sql = @"delete from  StylePics  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStylePicsByIds(string ids)
        {
            string sql = @"Delete from StylePics  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStylePicsByIds(string ids)
        {
            string sql = @"Update   StylePics set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ProductStylePicsEntity GetStylePics(int id)
		{
			string sql= @"SELECT  [Id],[StyleId],[PicUrl],PicSuffix,[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],[CompressTime]
							FROM
							dbo.[ProductStylePics] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ProductStylePicsEntity entity=new ProductStylePicsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Size_X=StringUtils.GetDbInt(reader["Size_X"]);
					entity.Size_Y=StringUtils.GetDbInt(reader["Size_Y"]);
					entity.Weight=StringUtils.GetDbDecimal(reader["Weight"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.HasCompress=StringUtils.GetDbInt(reader["HasCompress"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CompressTime=StringUtils.GetDbString(reader["CompressTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ProductStylePicsEntity> GetStylePicsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[StyleId],[PicUrl],PicSuffix,[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],[CompressTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[StyleId],[PicUrl],PicSuffix,[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],[CompressTime] from dbo.[ProductStylePics] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ProductStylePics] with (nolock) ";
            IList<ProductStylePicsEntity> entityList = new List< ProductStylePicsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ProductStylePicsEntity entity=new ProductStylePicsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);  
					entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);  
                    entity.Size_X=StringUtils.GetDbInt(reader["Size_X"]);
					entity.Size_Y=StringUtils.GetDbInt(reader["Size_Y"]);
					entity.Weight=StringUtils.GetDbDecimal(reader["Weight"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.HasCompress=StringUtils.GetDbInt(reader["HasCompress"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CompressTime=StringUtils.GetDbString(reader["CompressTime"]);
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

        public IList<ProductStylePicsEntity> GetStylePicsNoComPress(int num)
        {
            string sql = @"SELECT   TOP "+ num+ @" [Id],[StyleId],[PicUrl],PicSuffix,[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],
[CompressTime] INTO #temp from dbo.[ProductStylePics] WITH(NOLOCK)
where  HasCompress=0 ORDER BY CreateTime DESC 
UPDATE a SET [HasCompress]=2, CompressTime=GETDATE() FROM  dbo.[ProductStylePics] a  
, #temp b WHERE a.id=b.Id 
SELECT * FROM #temp
 ";
            IList<ProductStylePicsEntity> entityList = new List<ProductStylePicsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ProductStylePicsEntity entity = new ProductStylePicsEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Size_X = StringUtils.GetDbInt(reader["Size_X"]);
                    entity.Size_Y = StringUtils.GetDbInt(reader["Size_Y"]);
                    entity.Weight = StringUtils.GetDbDecimal(reader["Weight"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.HasCompress = StringUtils.GetDbInt(reader["HasCompress"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CompressTime = StringUtils.GetDbString(reader["CompressTime"]);
                    entityList.Add(entity);
                }
            }
            return entityList;

        }

        public IList<ProductStylePicsEntity> GetListPics( int productid )
        {
            IList<ProductStylePicsEntity> entityList = new List<ProductStylePicsEntity>();
            if (productid > 0  )
            {
                string sql = @"SELECT    [Id],[ProductId],[StyleId],[PicUrl],PicSuffix,[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],[CompressTime] from dbo.[ProductStylePics] WITH(NOLOCK)
                               where  ProductId=@ProductId Order By Sort Desc";
                DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
                using (IDataReader reader = db.ExecuteReader(cmd))
                {
                    while (reader.Read())
                    {
                        ProductStylePicsEntity entity = new ProductStylePicsEntity();
                        entity.Id = StringUtils.GetDbInt(reader["Id"]);
                        entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                        entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                        entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);  
                        entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]); 
                        entity.Size_X = StringUtils.GetDbInt(reader["Size_X"]);
                        entity.Size_Y = StringUtils.GetDbInt(reader["Size_Y"]);
                        entity.Weight = StringUtils.GetDbDecimal(reader["Weight"]);
                        entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                        entity.HasCompress = StringUtils.GetDbInt(reader["HasCompress"]);
                        entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                        entity.CompressTime = StringUtils.GetDbString(reader["CompressTime"]);
                        entityList.Add(entity);
                    }
                }
            } 
            return entityList;

        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ProductStylePicsEntity> GetStylePicsAll()
        {

            string sql = @"SELECT    [Id],[StyleId],[PicUrl],PicSuffix,[Size_X],[Size_Y],[Weight],[Sort],[HasCompress],[CreateTime],[CompressTime] from dbo.[ProductStylePics] WITH(NOLOCK)	";
		    IList<ProductStylePicsEntity> entityList = new List<ProductStylePicsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ProductStylePicsEntity entity=new ProductStylePicsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]); 
					entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);  

                    entity.Size_X=StringUtils.GetDbInt(reader["Size_X"]);
					entity.Size_Y=StringUtils.GetDbInt(reader["Size_Y"]);
					entity.Weight=StringUtils.GetDbDecimal(reader["Weight"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.HasCompress=StringUtils.GetDbInt(reader["HasCompress"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CompressTime=StringUtils.GetDbString(reader["CompressTime"]);
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
        public int  ExistNum(ProductStylePicsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ProductStylePics] WITH(NOLOCK) ";
            string where = "where "; 
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
