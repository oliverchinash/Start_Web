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
功能描述：BasicSiteProperties表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// BasicSitePropertiesEntity的数据访问操作
	/// </summary>
	public partial class BasicSitePropertiesDA: BaseSuperMarketDB
    {
        #region 实例化
        public static BasicSitePropertiesDA Instance
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
            internal static readonly BasicSitePropertiesDA instance = new BasicSitePropertiesDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表BasicSiteProperties，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="BasicSiteProperties">待插入的实体对象</param>
		public int AddBasicSiteProperties(BasicSitePropertiesEntity entity)
		{
		   string sql= @"insert into BasicSiteProperties( [Code],[Name],[SiteId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId])VALUES
			            ( @Code,@Name,@SiteId,@ParentId,@IsActive,@Sort,@RootPropertyId,@CanInput,@IsSpec,@ComPropertyId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd, "@SiteId",  DbType.Int32,entity.SiteId);
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
		/// <param name="BasicSiteProperties">待更新的实体对象</param>
		public   int UpdateBasicSiteProperties(BasicSitePropertiesEntity entity)
		{
			string sql= @" UPDATE dbo.[BasicSiteProperties] SET
                       [Code]=@Code,[Name]=@Name,[SiteId]=@SiteId,[ParentId]=@ParentId,[IsActive]=@IsActive,[Sort]=@Sort,[RootPropertyId]=@RootPropertyId,[CanInput]=@CanInput,[IsSpec]=@IsSpec,[ComPropertyId]=@ComPropertyId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd, "@SiteId",  DbType.Int32,entity.SiteId);
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
		public  int  DeleteBasicSitePropertiesByKey(int id)
	    {
			string sql=@"delete from BasicSiteProperties where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBasicSitePropertiesDisabled()
        {
            string sql = @"delete from  BasicSiteProperties  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBasicSitePropertiesByIds(string ids)
        {
            string sql = @"Delete from BasicSiteProperties  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBasicSitePropertiesByIds(string ids)
        {
            string sql = @"Update   BasicSiteProperties set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   BasicSitePropertiesEntity GetBasicSiteProperties(int id)
		{
			string sql= @"SELECT  [Id],[Code],[Name],[SiteId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId]
							FROM
							dbo.[BasicSiteProperties] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		BasicSitePropertiesEntity entity=new BasicSitePropertiesEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);
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
            string sql = @"EXEC Proc_BasicSitePropertiesEdit @ProperClassId, @PropertiesStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProperClassId", DbType.Int32, properclassid);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, strproperties);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int ProcGetProperties(int siteid, string propertiesstr)
        {
            string sql = @"EXEC Proc_GetPropertiesIdBindClass @SiteId, @PropertiesStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            db.AddInParameter(cmd, "@PropertiesStr", DbType.String, propertiesstr);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int  GetPropertiesId(int siteid, string propertiesstr)
        {
            string sql = @"SELECT Id FROM dbo.BasicSiteProperties WHERE SiteId=@SiteId AND name=@Name";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            db.AddInParameter(cmd, "@Name", DbType.String, propertiesstr);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int BindProperties(int siteid, string propertname,int sort)
        {
            string sql = @"IF NOT EXISTS (SELECT 1 FROM dbo.BasicSiteProperties WHERE SiteId=@SiteId AND name=@Name )
BEGIN 
 INSERT INTO   dbo.BasicSiteProperties
         ( Code ,
           Name ,
           SiteId ,
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
           @SiteId , -- ClassId - int
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

            db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
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
        public   IList<BasicSitePropertiesEntity> GetBasicSitePropertiesList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[Code],[Name],[SiteId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[SiteId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId] from dbo.[BasicSiteProperties] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[BasicSiteProperties] with (nolock) ";
            IList<BasicSitePropertiesEntity> entityList = new List< BasicSitePropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					BasicSitePropertiesEntity entity=new BasicSitePropertiesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);
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
        public IList<BasicSitePropertiesEntity> GetListBySiteId(int siteid,int parentid)
        {
            string sql = @"SELECT    [Id],[Code],[Name],[SiteId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId] from dbo.[BasicSiteProperties] WITH(NOLOCK)
where SiteId=@SiteId and ParentId=@ParentId Order by Sort desc";
            IList<BasicSitePropertiesEntity> entityList = new List<BasicSitePropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BasicSitePropertiesEntity entity = new BasicSitePropertiesEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);
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
        public IList<BasicSitePropertiesEntity> GetBasicSitePropertiesAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[SiteId],[ParentId],[IsActive],[Sort],[RootPropertyId],[CanInput],[IsSpec],[ComPropertyId] from dbo.[BasicSiteProperties] WITH(NOLOCK)	";
		    IList<BasicSitePropertiesEntity> entityList = new List<BasicSitePropertiesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   BasicSitePropertiesEntity entity=new BasicSitePropertiesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]);
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
        public int  ExistNum(BasicSitePropertiesEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[BasicSiteProperties] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
				  where = where+ "  (Name=@Name) and SiteId=@SiteId ";
				 
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
            db.AddInParameter(cmd, "@SiteId", DbType.Int32, entity.SiteId); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
