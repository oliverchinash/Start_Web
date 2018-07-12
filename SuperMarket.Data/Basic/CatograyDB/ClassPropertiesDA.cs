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
功能描述：ClassProperties表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// ClassPropertiesEntity的数据访问操作
	/// </summary>
	public partial class ClassPropertiesDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ClassPropertiesDA Instance
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
            internal static readonly ClassPropertiesDA instance = new ClassPropertiesDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ClassProperties，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="ClassProperties">待插入的实体对象</param>
		public int AddClassProperties(ClassPropertiesEntity entity)
		{
		   string sql= @"insert into ClassProperties( [Code],[Name],[ClassId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId])VALUES
			            ( @Code,@Name,@ClassId,@ParentId,@IsActive,@Sort,@RootPropertyId,@CanInput,@IsSpec,@ComPropertyId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd, "@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@RootPropertyId",  DbType.Int32,entity.RootPropertyId);
			db.AddInParameter(cmd,"@CanInput",  DbType.Int32,entity.CanInput);
			db.AddInParameter(cmd,"@IsSpec",  DbType.Int32,entity.IsSpec);
			db.AddInParameter(cmd,"@ComPropertyId",  DbType.Int32,entity.ComPropertyId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="ClassProperties">待更新的实体对象</param>
		public   int UpdateClassProperties(ClassPropertiesEntity entity)
		{
			string sql= @" UPDATE dbo.[ClassProperties] SET
                       [Code]=@Code,[Name]=@Name,[ClassId]=@ClassId,[ParentId]=@ParentId,[IsActive]=@IsActive,[Sort]=@Sort,[RootPropertyId]=@RootPropertyId,[CanInput]=@CanInput,[IsSpec]=@IsSpec,[ComPropertyId]=@ComPropertyId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd, "@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@RootPropertyId",  DbType.Int32,entity.RootPropertyId);
			db.AddInParameter(cmd,"@CanInput",  DbType.Int32,entity.CanInput);
			db.AddInParameter(cmd,"@IsSpec",  DbType.Int32,entity.IsSpec);
			db.AddInParameter(cmd,"@ComPropertyId",  DbType.Int32,entity.ComPropertyId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteClassPropertiesByKey(int id)
	    {
			string sql=@"delete from ClassProperties where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassPropertiesDisabled()
        {
            string sql = @"delete from  ClassProperties  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassPropertiesByIds(string ids)
        {
            string sql = @"Delete from ClassProperties  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassPropertiesByIds(string ids)
        {
            string sql = @"Update   ClassProperties set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ClassPropertiesEntity GetClassProperties(int id)
		{
			string sql= @"SELECT  [Id],[Code],[Name],[ClassId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId]
							FROM
							dbo.[ClassProperties] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ClassPropertiesEntity entity=new ClassPropertiesEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.RootPropertyId=StringUtils.GetDbInt(reader["RootPropertyId"]);
					entity.CanInput=StringUtils.GetDbInt(reader["CanInput"]);
					entity.IsSpec=StringUtils.GetDbInt(reader["IsSpec"]);
					entity.ComPropertyId=StringUtils.GetDbInt(reader["ComPropertyId"]);
				}
   		    }
            return entity;
		}
        public int ProcBindProperties(int properclassid, string strproperties)
        {
            string sql = @"EXEC Proc_ClassPropertiesEdit @ProperClassId, @PropertiesStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProperClassId", DbType.Int32, properclassid);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, strproperties);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int ProcGetProperties(int ClassId, string propertiesstr)
        {
            string sql = @"EXEC Proc_GetPropertiesIdBindClass @ClassId, @PropertiesStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassId", DbType.Int32, ClassId);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, propertiesstr);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int  GetPropertiesId(int ClassId, string propertiesstr)
        {
            string sql = @"SELECT Id FROM dbo.ClassProperties WHERE ClassId=@ClassId AND name=@Name";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassId", DbType.Int32, ClassId);
            db.AddInParameter(cmd, "@Name", DbType.String, propertiesstr);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int BindProperties(int ClassId, string propertname,int sort)
        {
            string sql = @"IF NOT EXISTS (SELECT 1 FROM dbo.ClassProperties WHERE ClassId=@ClassId AND name=@Name )
BEGIN 
 INSERT INTO   dbo.ClassProperties
         ( Code ,
           Name ,
           ClassId ,
           ParentId ,
           IsActive ,
           Sort ,
           RootPropertyId ,
           CanInput ,
           IsSpec ,
           ComPropertyId ,
           IsEnd
         )
 VALUES  ( '' , -- Code - varchar(20)
           @Name , -- Name - varchar(50)
           @ClassId , -- ClassId - int
           0 , -- ParentId - int
           1 , -- IsActive - int
           @Sort , -- Sort - int
           0 , -- RootPropertyId - int
           1 , -- CanInput - int
           0 , -- IsSpec - int
           0 , -- ComPropertyId - int
           1  -- IsEnd - int
         )
END";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassId", DbType.Int32, ClassId);
            db.AddInParameter(cmd, "@Name", DbType.String, propertname);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, sort);
            return  db.ExecuteNonQuery(cmd);
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="pid"></param>
        /// <returns></returns> 
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<ClassPropertiesEntity> GetClassPropertiesList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[Code],[Name],[ClassId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[ClassId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId] from dbo.[ClassProperties] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ClassProperties] with (nolock) ";
            IList<ClassPropertiesEntity> entityList = new List< ClassPropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ClassPropertiesEntity entity=new ClassPropertiesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.RootPropertyId=StringUtils.GetDbInt(reader["RootPropertyId"]);
					entity.CanInput=StringUtils.GetDbInt(reader["CanInput"]);
					entity.IsSpec=StringUtils.GetDbInt(reader["IsSpec"]);
					entity.ComPropertyId=StringUtils.GetDbInt(reader["ComPropertyId"]);
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
        public IList<ClassPropertiesEntity> GetListByClassId(int ClassId )
        {
            string sql = @"SELECT    [Id],[Code],[Name],[ClassId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId] from dbo.[ClassProperties] WITH(NOLOCK)
where ClassId=@ClassId   Order by Sort desc";
            IList<ClassPropertiesEntity> entityList = new List<ClassPropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, ClassId); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassPropertiesEntity entity = new ClassPropertiesEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.RootPropertyId = StringUtils.GetDbInt(reader["RootPropertyId"]);
                    entity.CanInput = StringUtils.GetDbInt(reader["CanInput"]);
                    entity.IsSpec = StringUtils.GetDbInt(reader["IsSpec"]);
                    entity.ComPropertyId = StringUtils.GetDbInt(reader["ComPropertyId"]);
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
        public IList<ClassPropertiesEntity> GetClassPropertiesAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[ClassId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId] from dbo.[ClassProperties] WITH(NOLOCK)	";
		    IList<ClassPropertiesEntity> entityList = new List<ClassPropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ClassPropertiesEntity entity=new ClassPropertiesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.RootPropertyId=StringUtils.GetDbInt(reader["RootPropertyId"]);
					entity.CanInput=StringUtils.GetDbInt(reader["CanInput"]);
					entity.IsSpec=StringUtils.GetDbInt(reader["IsSpec"]);
					entity.ComPropertyId=StringUtils.GetDbInt(reader["ComPropertyId"]);
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
        public int  ExistNum(ClassPropertiesEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ClassProperties] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
				  where = where+ "  (Name=@Name) and ClassId=@ClassId ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (Name=@Name and ClassId=@ClassId) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name); 
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
