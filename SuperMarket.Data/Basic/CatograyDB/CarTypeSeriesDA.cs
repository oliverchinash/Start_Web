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
功能描述：CarTypeSeries表的数据访问类。
创建时间：2017/4/21 11:46:48
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// CarTypeSeriesEntity的数据访问操作
	/// </summary>
	public partial class CarTypeSeriesDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CarTypeSeriesDA Instance
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
            internal static readonly CarTypeSeriesDA instance = new CarTypeSeriesDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CarTypeSeries，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="carTypeSeries">待插入的实体对象</param>
		public int AddCarTypeSeries(CarTypeSeriesEntity entity)
		{
		   string sql=@"insert into CarTypeSeries( [Code],[FullName],[ShortName],[StandardBrandId],[ParentBrandId],[Sort],[IsActive])VALUES
			            ( @Code,@FullName,@ShortName,@StandardBrandId,@ParentBrandId,@Sort,@IsActive);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@FullName",  DbType.String,entity.FullName);
			db.AddInParameter(cmd,"@ShortName",  DbType.String,entity.ShortName);
			db.AddInParameter(cmd,"@StandardBrandId",  DbType.Int32,entity.StandardBrandId);
			db.AddInParameter(cmd,"@ParentBrandId",  DbType.Int32,entity.ParentBrandId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="carTypeSeries">待更新的实体对象</param>
		public   int UpdateCarTypeSeries(CarTypeSeriesEntity entity)
		{
			string sql=@" UPDATE dbo.[CarTypeSeries] SET
                       [Code]=@Code,[FullName]=@FullName,[ShortName]=@ShortName,[StandardBrandId]=@StandardBrandId,[ParentBrandId]=@ParentBrandId,[Sort]=@Sort,[IsActive]=@IsActive
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@FullName",  DbType.String,entity.FullName);
			db.AddInParameter(cmd,"@ShortName",  DbType.String,entity.ShortName);
			db.AddInParameter(cmd,"@StandardBrandId",  DbType.Int32,entity.StandardBrandId);
			db.AddInParameter(cmd,"@ParentBrandId",  DbType.Int32,entity.ParentBrandId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCarTypeSeriesByKey(int id)
	    {
			string sql=@"delete from CarTypeSeries where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeSeriesDisabled()
        {
            string sql = @"delete from  CarTypeSeries  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeSeriesByIds(string ids)
        {
            string sql = @"Delete from CarTypeSeries  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeSeriesByIds(string ids)
        {
            string sql = @"Update   CarTypeSeries set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CarTypeSeriesEntity GetCarTypeSeries(int id)
		{
			string sql=@"SELECT  [Id],[Code],[FullName],[ShortName],[StandardBrandId],[ParentBrandId],[Sort],[IsActive]
							FROM
							dbo.[CarTypeSeries] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CarTypeSeriesEntity entity=new CarTypeSeriesEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.FullName=StringUtils.GetDbString(reader["FullName"]);
					entity.ShortName=StringUtils.GetDbString(reader["ShortName"]);
					entity.StandardBrandId=StringUtils.GetDbInt(reader["StandardBrandId"]);
					entity.ParentBrandId=StringUtils.GetDbInt(reader["ParentBrandId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<CarTypeSeriesEntity> GetCarTypeSeriesList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[FullName],[ShortName],[StandardBrandId],[ParentBrandId],[Sort],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[FullName],[ShortName],[StandardBrandId],[ParentBrandId],[Sort],[IsActive] from dbo.[CarTypeSeries] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CarTypeSeries] with (nolock) ";
            IList<CarTypeSeriesEntity> entityList = new List< CarTypeSeriesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CarTypeSeriesEntity entity=new CarTypeSeriesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.FullName=StringUtils.GetDbString(reader["FullName"]);
					entity.ShortName=StringUtils.GetDbString(reader["ShortName"]);
					entity.StandardBrandId=StringUtils.GetDbInt(reader["StandardBrandId"]);
					entity.ParentBrandId=StringUtils.GetDbInt(reader["ParentBrandId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public IList<CarTypeSeriesEntity> GetCarTypeSeriesAll(int standbrandid, int parentbrandid )
        {
            string where = " where 1=1 ";
            if(parentbrandid>0)
            {
                where += " and ParentBrandId=@ParentBrandId ";
            }
            if (standbrandid > 0)
            {
                where += " and StandardBrandId=@StandardBrandId ";
            }
            where += " and IsActive=1 ";
           
            string sql = @"SELECT [Id],[Code],[FullName],[ShortName],[StandardBrandId],[ParentBrandId],[Sort],[IsActive] from dbo.[CarTypeSeries] WITH(NOLOCK)	"+ where+ " Order By FullName asc ";

            IList<CarTypeSeriesEntity> entityList = new List<CarTypeSeriesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (parentbrandid > 0)
            { 
                db.AddInParameter(cmd, "@ParentBrandId", DbType.Int32, parentbrandid);
            }
            if (standbrandid > 0)
            {
                db.AddInParameter(cmd, "@StandardBrandId", DbType.Int32, standbrandid); 
            }
             
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CarTypeSeriesEntity entity=new CarTypeSeriesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.FullName=StringUtils.GetDbString(reader["FullName"]);
					entity.ShortName=StringUtils.GetDbString(reader["ShortName"]);
					entity.StandardBrandId=StringUtils.GetDbInt(reader["StandardBrandId"]);
					entity.ParentBrandId=StringUtils.GetDbInt(reader["ParentBrandId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(CarTypeSeriesEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CarTypeSeries] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (FullName=@FullName) ";
					     where = where+ "  (ShortName=@ShortName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (FullName=@FullName) ";
					     where = where+ " id<>@Id and  (ShortName=@ShortName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@FullName", DbType.String, entity.FullName); 
					
            db.AddInParameter(cmd, "@ShortName", DbType.String, entity.ShortName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
