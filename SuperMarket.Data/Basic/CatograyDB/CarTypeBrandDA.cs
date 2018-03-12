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
功能描述：CarTypeBrand表的数据访问类。
创建时间：2017/4/21 11:46:48
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// CarTypeBrandEntity的数据访问操作
	/// </summary>
	public partial class CarTypeBrandDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CarTypeBrandDA Instance
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
            internal static readonly CarTypeBrandDA instance = new CarTypeBrandDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CarTypeBrand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="carTypeBrand">待插入的实体对象</param>
		public int AddCarTypeBrand(CarTypeBrandEntity entity)
		{
		   string sql=@"insert into CarTypeBrand( [Code],[BrandName],[PYFirst],[PicUrl],[IsActive],[ParentCode],[ParentId],[IsStandardBrand],[Sort])VALUES
			            ( @Code,@BrandName,@PYFirst,@PicUrl,@IsActive,@ParentCode,@ParentId,@IsStandardBrand,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@BrandName",  DbType.String,entity.BrandName);
			db.AddInParameter(cmd,"@PYFirst",  DbType.String,entity.PYFirst);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@ParentCode",  DbType.String,entity.ParentCode);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsStandardBrand",  DbType.Int32,entity.IsStandardBrand);
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
		/// <param name="carTypeBrand">待更新的实体对象</param>
		public   int UpdateCarTypeBrand(CarTypeBrandEntity entity)
		{
			string sql=@" UPDATE dbo.[CarTypeBrand] SET
                       [Code]=@Code,[BrandName]=@BrandName,[PYFirst]=@PYFirst,[PicUrl]=@PicUrl,[IsActive]=@IsActive,[ParentCode]=@ParentCode,[ParentId]=@ParentId,[IsStandardBrand]=@IsStandardBrand,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@BrandName",  DbType.String,entity.BrandName);
			db.AddInParameter(cmd,"@PYFirst",  DbType.String,entity.PYFirst);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@ParentCode",  DbType.String,entity.ParentCode);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsStandardBrand",  DbType.Int32,entity.IsStandardBrand);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCarTypeBrandByKey(int id)
	    {
			string sql=@"delete from CarTypeBrand where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeBrandDisabled()
        {
            string sql = @"delete from  CarTypeBrand  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeBrandByIds(string ids)
        {
            string sql = @"Delete from CarTypeBrand  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeBrandByIds(string ids)
        {
            string sql = @"Update   CarTypeBrand set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CarTypeBrandEntity GetCarTypeBrand(int id)
		{
			string sql=@"SELECT  [Id],[Code],[BrandName],[PYFirst],[PicUrl],[IsActive],[ParentCode],[ParentId],[IsStandardBrand],[Sort]
							FROM
							dbo.[CarTypeBrand] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CarTypeBrandEntity entity=new CarTypeBrandEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.BrandName=StringUtils.GetDbString(reader["BrandName"]);
					entity.PYFirst=StringUtils.GetDbString(reader["PYFirst"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.ParentCode=StringUtils.GetDbString(reader["ParentCode"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsStandardBrand=StringUtils.GetDbInt(reader["IsStandardBrand"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}
        public CarTypeBrandEntity GetParentByName(string name)
        {
            string sql = @"SELECT  [Id],[Code],[BrandName],[PYFirst],[PicUrl],[IsActive],[ParentCode],[ParentId],[IsStandardBrand],[Sort]
							FROM
							dbo.[CarTypeBrand] WITH(NOLOCK)	
							WHERE [Id]=(Select top 1 ParentId from 	dbo.[CarTypeBrand] WITH(NOLOCK)	 where BrandName=@BrandName and ParentId>0)  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@BrandName", DbType.String, name);
            CarTypeBrandEntity entity = new CarTypeBrandEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.BrandName = StringUtils.GetDbString(reader["BrandName"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.ParentCode = StringUtils.GetDbString(reader["ParentCode"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsStandardBrand = StringUtils.GetDbInt(reader["IsStandardBrand"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<CarTypeBrandEntity> GetCarTypeBrandList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[BrandName],[PYFirst],[PicUrl],[IsActive],[ParentCode],[ParentId],[IsStandardBrand],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[BrandName],[PYFirst],[PicUrl],[IsActive],[ParentCode],[ParentId],[IsStandardBrand],[Sort] from dbo.[CarTypeBrand] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CarTypeBrand] with (nolock) ";
            IList<CarTypeBrandEntity> entityList = new List< CarTypeBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CarTypeBrandEntity entity=new CarTypeBrandEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.BrandName=StringUtils.GetDbString(reader["BrandName"]);
					entity.PYFirst=StringUtils.GetDbString(reader["PYFirst"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.ParentCode=StringUtils.GetDbString(reader["ParentCode"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsStandardBrand=StringUtils.GetDbInt(reader["IsStandardBrand"]);
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
        public IList<CarTypeBrandEntity> GetCarTypeBrandAll(int parentid,int isactive,int isstand=-1)
        {
            string where = " where 1=1 ";
            if (parentid > 0)
            {
                where += " and ParentId=@ParentId ";
            }
            if (isactive !=-1)
            {
                where += " and IsActive=@IsActive ";
            }
            if (isstand != -1)
            {
                where += " and IsStandardBrand=@IsStandardBrand ";
            }
            string sql = @"SELECT    [Id],[Code],[BrandName],[PYFirst],[PicUrl],[IsActive],[IsHot],[ParentCode],[ParentId],[IsStandardBrand],[Sort] from dbo.[CarTypeBrand] WITH(NOLOCK)	" + where + " Order By Sort desc,BrandName asc ";
		    IList<CarTypeBrandEntity> entityList = new List<CarTypeBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); if (parentid > 0)
            { 
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            }
            if (isactive != -1)
            { 
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            if (isstand != -1)
            { 
                db.AddInParameter(cmd, "@IsStandardBrand", DbType.Int32, isstand);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CarTypeBrandEntity entity=new CarTypeBrandEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.BrandName=StringUtils.GetDbString(reader["BrandName"]);
					entity.PYFirst=StringUtils.GetDbString(reader["PYFirst"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.ParentCode=StringUtils.GetDbString(reader["ParentCode"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsStandardBrand=StringUtils.GetDbInt(reader["IsStandardBrand"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				    entityList.Add(entity);
                }
            } 
            return entityList;
        }
        /// <summary>
        /// 获取标准车型有效品牌
        /// </summary>
        /// <returns></returns>
        public IList<VWTreeCTBrandEntity> GetStandardTreeCTB(int isstand)
        {
            string where = " where  IsActive=1  ";
            if(isstand!=-1)
            {
                where = " where IsStandardBrand=@IsStandardBrand ";
            }
            string sql = @"SELECT    [Id], [BrandName],[PYFirst],[PicUrl],Sort,ParentId,IsStandardBrand from dbo.[CarTypeBrand] WITH(NOLOCK) " + where+" Order By Sort  	";
            IList<VWTreeCTBrandEntity> entityList = new List<VWTreeCTBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (isstand != -1)
            { 
                db.AddInParameter(cmd, "@IsStandardBrand", DbType.Int32, isstand);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWTreeCTBrandEntity entity = new VWTreeCTBrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]); 
                    entity.Name = StringUtils.GetDbString(reader["BrandName"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsStandardBrand = StringUtils.GetDbInt(reader["IsStandardBrand"]);
                    
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
        public int  ExistNum(CarTypeBrandEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CarTypeBrand] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (BrandName=@BrandName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (BrandName=@BrandName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@BrandName", DbType.String, entity.BrandName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
