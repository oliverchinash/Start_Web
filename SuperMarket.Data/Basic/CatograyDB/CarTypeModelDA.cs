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
功能描述：CarTypeModel表的数据访问类。
创建时间：2017/4/21 11:46:48
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// CarTypeModelEntity的数据访问操作
	/// </summary>
	public partial class CarTypeModelDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CarTypeModelDA Instance
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
            internal static readonly CarTypeModelDA instance = new CarTypeModelDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CarTypeModel，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="carTypeModel">待插入的实体对象</param>
		public int AddCarTypeModel(CarTypeModelEntity entity)
		{
		   string sql=@"insert into CarTypeModel( [Code],[ParentCode],[BoxType],[Capacity],[CarYear],[ModelName],[SeriesId],[Status],[Engine])VALUES
			            ( @Code,@ParentCode,@BoxType,@Capacity,@CarYear,@ModelName,@SeriesId,@Status,@Engine);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@ParentCode",  DbType.String,entity.ParentCode);
			db.AddInParameter(cmd,"@BoxType",  DbType.String,entity.BoxType);
			db.AddInParameter(cmd,"@Capacity",  DbType.String,entity.Capacity);
			db.AddInParameter(cmd,"@CarYear",  DbType.Int32,entity.CarYear);
			db.AddInParameter(cmd,"@ModelName",  DbType.String,entity.ModelName);
			db.AddInParameter(cmd,"@SeriesId",  DbType.Int32,entity.SeriesId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@Engine",  DbType.String,entity.Engine);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="carTypeModel">待更新的实体对象</param>
		public   int UpdateCarTypeModel(CarTypeModelEntity entity)
		{
			string sql=@" UPDATE dbo.[CarTypeModel] SET
                       [Code]=@Code,[ParentCode]=@ParentCode,[BoxType]=@BoxType,[Capacity]=@Capacity,[CarYear]=@CarYear,[ModelName]=@ModelName,[SeriesId]=@SeriesId,[Status]=@Status,[Engine]=@Engine
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@ParentCode",  DbType.String,entity.ParentCode);
			db.AddInParameter(cmd,"@BoxType",  DbType.String,entity.BoxType);
			db.AddInParameter(cmd,"@Capacity",  DbType.String,entity.Capacity);
			db.AddInParameter(cmd,"@CarYear",  DbType.Int32,entity.CarYear);
			db.AddInParameter(cmd,"@ModelName",  DbType.String,entity.ModelName);
			db.AddInParameter(cmd,"@SeriesId",  DbType.Int32,entity.SeriesId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@Engine",  DbType.String,entity.Engine);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCarTypeModelByKey(int id)
	    {
			string sql=@"delete from CarTypeModel where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeModelDisabled()
        {
            string sql = @"delete from  CarTypeModel  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeModelByIds(string ids)
        {
            string sql = @"Delete from CarTypeModel  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeModelByIds(string ids)
        {
            string sql = @"Update   CarTypeModel set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CarTypeModelEntity GetCarTypeModel(int id)
		{
			string sql=@"SELECT  [Id],[Code],[ParentCode],[BoxType],[Capacity],[CarYear],[ModelName],[SeriesId],[Status],[Engine]
							FROM
							dbo.[CarTypeModel] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CarTypeModelEntity entity=new CarTypeModelEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.ParentCode=StringUtils.GetDbString(reader["ParentCode"]);
					entity.BoxType=StringUtils.GetDbString(reader["BoxType"]);
					entity.Capacity=StringUtils.GetDbString(reader["Capacity"]);
					entity.CarYear=StringUtils.GetDbInt(reader["CarYear"]);
					entity.ModelName=StringUtils.GetDbString(reader["ModelName"]);
					entity.SeriesId=StringUtils.GetDbInt(reader["SeriesId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.Engine=StringUtils.GetDbString(reader["Engine"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<CarTypeModelEntity> GetCarTypeModelList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[ParentCode],[BoxType],[Capacity],[CarYear],[ModelName],[SeriesId],[Status],[Engine]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[ParentCode],[BoxType],[Capacity],[CarYear],[ModelName],[SeriesId],[Status],[Engine] from dbo.[CarTypeModel] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CarTypeModel] with (nolock) ";
            IList<CarTypeModelEntity> entityList = new List< CarTypeModelEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CarTypeModelEntity entity=new CarTypeModelEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.ParentCode=StringUtils.GetDbString(reader["ParentCode"]);
					entity.BoxType=StringUtils.GetDbString(reader["BoxType"]);
					entity.Capacity=StringUtils.GetDbString(reader["Capacity"]);
					entity.CarYear=StringUtils.GetDbInt(reader["CarYear"]);
					entity.ModelName=StringUtils.GetDbString(reader["ModelName"]);
					entity.SeriesId=StringUtils.GetDbInt(reader["SeriesId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.Engine=StringUtils.GetDbString(reader["Engine"]);
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
        public IList<CarTypeModelEntity> GetCarTypeModelAll(int seriesid)
        {
            
            string sql = @"SELECT    [Id],[Code],[ParentCode],[BoxType],[Capacity],[CarYear],[ModelName],[SeriesId],[Status],[Engine] from dbo.[CarTypeModel] WITH(NOLOCK)	where SeriesId=@SeriesId and Status=1 Order By ModelName  ";
		    IList<CarTypeModelEntity> entityList = new List<CarTypeModelEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SeriesId", DbType.Int32, seriesid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CarTypeModelEntity entity=new CarTypeModelEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.ParentCode=StringUtils.GetDbString(reader["ParentCode"]);
					entity.BoxType=StringUtils.GetDbString(reader["BoxType"]);
					entity.Capacity=StringUtils.GetDbString(reader["Capacity"]);
					entity.CarYear=StringUtils.GetDbInt(reader["CarYear"]);
					entity.ModelName=StringUtils.GetDbString(reader["ModelName"]);
					entity.SeriesId=StringUtils.GetDbInt(reader["SeriesId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.Engine=StringUtils.GetDbString(reader["Engine"]);
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
        public int  ExistNum(CarTypeModelEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CarTypeModel] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (ModelName=@ModelName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (ModelName=@ModelName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@ModelName", DbType.String, entity.ModelName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
