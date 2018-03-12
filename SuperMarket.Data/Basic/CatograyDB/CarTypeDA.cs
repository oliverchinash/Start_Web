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
功能描述：CarType表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// CarTypeEntity的数据访问操作
	/// </summary>
	public partial class CarTypeDA : BaseSuperMarketDB
    {
        #region 实例化
        public static CarTypeDA Instance
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
            internal static readonly CarTypeDA instance = new CarTypeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CarType，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="CarType">待插入的实体对象</param>
		public int AddCarType(CarTypeEntity entity)
		{
		   string sql=@"insert into CarType( [Code],[Name],[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort])VALUES
			            ( @Code,@Name,@Year,@ParentId,@IsActive,@CarTypeLevel,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@Year",  DbType.String,entity.Year);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@CarTypeLevel",  DbType.Int32,entity.CarTypeLevel);
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
		/// <param name="CarType">待更新的实体对象</param>
		public   int UpdateCarType(CarTypeEntity entity)
		{
			string sql=@" UPDATE dbo.[CarType] SET
                       [Code]=@Code,[Name]=@Name,[Year]=@Year,[ParentId]=@ParentId,[IsActive]=@IsActive,[CarTypeLevel]=@CarTypeLevel,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@Year",  DbType.String,entity.Year);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@CarTypeLevel",  DbType.Int32,entity.CarTypeLevel);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCarTypeByKey(int id)
	    {
			string sql=@"delete from CarType where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCarTypeDisabled()
        {
            string sql = @"delete from  CarType  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCarTypeByIds(string ids)
        {
            string sql = @"Delete from CarType  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCarTypeByIds(string ids)
        {
            string sql = @"Update   CarType set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CarTypeEntity GetCarType(int id)
		{
			string sql= @"SELECT  [Id],[Code],[Name],[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort],RedirectCarTypeId,ClassType,IsStandard
							FROM
							dbo.[CarType] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CarTypeEntity entity=new CarTypeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.Year=StringUtils.GetDbString(reader["Year"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CarTypeLevel=StringUtils.GetDbInt(reader["CarTypeLevel"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.RedirectCarTypeId = StringUtils.GetDbInt(reader["RedirectCarTypeId"]);
					entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);
					entity.IsStandard = StringUtils.GetDbInt(reader["IsStandard"]);
                }
   		    }
            return entity;
		}
        public IList<CarTypeEntity> GetListByParent(int pid,int status )
        {
            string where = " where 1=1 ";
            if(pid!=-1)
            {
                where += " and ParentId=@ParentId  ";
            }
            if (status != -1)
            {
                where += " and IsActive=@IsActive  ";
            }
            string sql = @"SELECT    [Id],[Code],[Name],FirstPY,[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort],IsHot,RedirectCarTypeId,ClassType,IsStandard from dbo.[CarType] WITH(NOLOCK) " + where+"  Order By Year asc,Sort DESC";
            IList<CarTypeEntity> entityList = new List<CarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (pid != -1)
            { 
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
            }
            if (status != -1)
            { 
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CarTypeEntity entity = new CarTypeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.FirstPY = StringUtils.GetDbString(reader["FirstPY"]); 
                    entity.Year = StringUtils.GetDbString(reader["Year"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.CarTypeLevel = StringUtils.GetDbInt(reader["CarTypeLevel"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.RedirectCarTypeId = StringUtils.GetDbInt(reader["RedirectCarTypeId"]);
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);
                    entity.IsStandard = StringUtils.GetDbInt(reader["IsStandard"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public IList<CarTypeEntity> GetListYearByParent(int pid)
        {
            string sql = @"SELECT  distinct [Year]  from dbo.[CarType] WITH(NOLOCK) where
ParentId=@ParentId order by Year ";
            IList<CarTypeEntity> entityList = new List<CarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CarTypeEntity entity = new CarTypeEntity(); 
                    entity.Year = StringUtils.GetDbString(reader["Year"]); 
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
        public IList<CarTypeEntity> GetCarTypeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[Name],[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort] from dbo.[CarType] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CarType] with (nolock) ";
            IList<CarTypeEntity> entityList = new List< CarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CarTypeEntity entity=new CarTypeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.Year=StringUtils.GetDbString(reader["Year"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CarTypeLevel=StringUtils.GetDbInt(reader["CarTypeLevel"]);
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

        public IList<CarTypeEntity> GetCarTypeAll(int menuid,int level,int isstandard,int parentid)
        {
            string where = " where IsActive=1 ";
            if(menuid>0)
            {
                where += " and ClassType=@ClassType ";
            }
            if (level !=-1)
            {
                where += " and CarTypeLevel=@CarTypeLevel ";
            }
            if (isstandard !=-1)
            {
                where += " and IsStandard=@IsStandard ";
            }
            if (parentid != -1)
            {
                where += " and ParentId=@ParentId ";
            }
            string sql = @"SELECT    [Id],[Code],[Name],[PicUrl],[FirstPY],[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort],IsHot,CarTypeClassId,ClassType,IsStandard,RedirectCarTypeId from dbo.[CarType] WITH(NOLOCK)	" + where + "  Order by Sort desc";
            IList<CarTypeEntity> entityList = new List<CarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (menuid > 0)
            { 
                db.AddInParameter(cmd, "@ClassType", DbType.Int32, menuid);
            }
            if (level != -1)
            { 
                db.AddInParameter(cmd, "@CarTypeLevel", DbType.Int32, level);
            }
            if (isstandard != -1)
            { 
                db.AddInParameter(cmd, "@IsStandard", DbType.Int32, isstandard);
            }
            if (parentid != -1)
            { 
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CarTypeEntity entity = new CarTypeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.FirstPY = StringUtils.GetDbString(reader["FirstPY"]);
                    entity.Year = StringUtils.GetDbString(reader["Year"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.CarTypeLevel = StringUtils.GetDbInt(reader["CarTypeLevel"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);  
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]); 
                    entity.CarTypeClassId = StringUtils.GetDbInt(reader["CarTypeClassId"]);
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);
                    entity.IsStandard = StringUtils.GetDbInt(reader["IsStandard"]);
                    entity.RedirectCarTypeId = StringUtils.GetDbInt(reader["RedirectCarTypeId"]);
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
        public IList<CarTypeEntity> GetCarTypeAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[Year],[ParentId],[IsActive],[CarTypeLevel],[Sort] from dbo.[CarType] WITH(NOLOCK)	";
		    IList<CarTypeEntity> entityList = new List<CarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CarTypeEntity entity=new CarTypeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.Year=StringUtils.GetDbString(reader["Year"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CarTypeLevel=StringUtils.GetDbInt(reader["CarTypeLevel"]);
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
        public int  ExistNum(CarTypeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CarType] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (Name=@Name) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (Name=@Name) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
