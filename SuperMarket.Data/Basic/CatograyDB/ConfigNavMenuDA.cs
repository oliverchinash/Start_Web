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
功能描述：ConfigNavMenu表的数据访问类。
创建时间：2017/4/21 15:47:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// ConfigNavMenuEntity的数据访问操作
	/// </summary>
	public partial class ConfigNavMenuDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ConfigNavMenuDA Instance
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
            internal static readonly ConfigNavMenuDA instance = new ConfigNavMenuDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ConfigNavMenu，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="configNavMenu">待插入的实体对象</param>
		public int AddConfigNavMenu(ConfigNavMenuEntity entity)
		{
		   string sql=@"insert into ConfigNavMenu( [ClassId],[BrandId],[UrlPath],[ImagePath],[Name],[ParentId],[NavMenuType],[Sort],[ShowMethod])VALUES
			            ( @ClassId,@BrandId,@UrlPath,@ImagePath,@Name,@ParentId,@NavMenuType,@Sort,@ShowMethod);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@UrlPath",  DbType.String,entity.UrlPath);
			db.AddInParameter(cmd,"@ImagePath",  DbType.String,entity.ImagePath);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@NavMenuType",  DbType.Int32,entity.NavMenuType);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ShowMethod",  DbType.Int32,entity.ShowMethod);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="configNavMenu">待更新的实体对象</param>
		public   int UpdateConfigNavMenu(ConfigNavMenuEntity entity)
		{
			string sql=@" UPDATE dbo.[ConfigNavMenu] SET
                       [ClassId]=@ClassId,[BrandId]=@BrandId,[UrlPath]=@UrlPath,[ImagePath]=@ImagePath,[Name]=@Name,[ParentId]=@ParentId,[NavMenuType]=@NavMenuType,[Sort]=@Sort,[ShowMethod]=@ShowMethod
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@UrlPath",  DbType.String,entity.UrlPath);
			db.AddInParameter(cmd,"@ImagePath",  DbType.String,entity.ImagePath);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@NavMenuType",  DbType.Int32,entity.NavMenuType);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ShowMethod",  DbType.Int32,entity.ShowMethod);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteConfigNavMenuByKey(int id)
	    {
			string sql=@"delete from ConfigNavMenu where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfigNavMenuDisabled()
        {
            string sql = @"delete from  ConfigNavMenu  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfigNavMenuByIds(string ids)
        {
            string sql = @"Delete from ConfigNavMenu  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfigNavMenuByIds(string ids)
        {
            string sql = @"Update   ConfigNavMenu set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ConfigNavMenuEntity GetConfigNavMenu(int id)
		{
			string sql=@"SELECT  [Id],[ClassId],[BrandId],[UrlPath],[ImagePath],[Name],[ParentId],[NavMenuType],[Sort],[ShowMethod]
							FROM
							dbo.[ConfigNavMenu] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ConfigNavMenuEntity entity=new ConfigNavMenuEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.UrlPath=StringUtils.GetDbString(reader["UrlPath"]);
					entity.ImagePath=StringUtils.GetDbString(reader["ImagePath"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.NavMenuType=StringUtils.GetDbInt(reader["NavMenuType"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowMethod=StringUtils.GetDbInt(reader["ShowMethod"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ConfigNavMenuEntity> GetConfigNavMenuList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ClassId],[BrandId],[UrlPath],[ImagePath],[Name],[ParentId],[NavMenuType],[Sort],[ShowMethod]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ClassId],[BrandId],[UrlPath],[ImagePath],[Name],[ParentId],[NavMenuType],[Sort],[ShowMethod] from dbo.[ConfigNavMenu] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ConfigNavMenu] with (nolock) ";
            IList<ConfigNavMenuEntity> entityList = new List< ConfigNavMenuEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ConfigNavMenuEntity entity=new ConfigNavMenuEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.UrlPath=StringUtils.GetDbString(reader["UrlPath"]);
					entity.ImagePath=StringUtils.GetDbString(reader["ImagePath"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.NavMenuType=StringUtils.GetDbInt(reader["NavMenuType"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowMethod=StringUtils.GetDbInt(reader["ShowMethod"]);
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
        public IList<VWConfigNavMenuEntity> GetConfigNavMenuAll(int navtype)
        {
            string where = " where 1=1 ";
            if(navtype!=-1)
            {
                where += " and NavMenuType=@NavMenuType ";
            }
            string sql = @"SELECT    [Id],[ClassId],[BrandId],[UrlPath],[ImagePath],[Name],[ParentId],[NavMenuType],[Sort],[ShowMethod] from dbo.[ConfigNavMenu] WITH(NOLOCK)	"+ where +" Order by Sort desc" ;
		    IList<VWConfigNavMenuEntity> entityList = new List<VWConfigNavMenuEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (navtype != -1)
            {
                db.AddInParameter(cmd, "@NavMenuType", DbType.Int32, navtype);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWConfigNavMenuEntity entity =new VWConfigNavMenuEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.UrlPath=StringUtils.GetDbString(reader["UrlPath"]);
					entity.ImagePath=StringUtils.GetDbString(reader["ImagePath"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.NavMenuType=StringUtils.GetDbInt(reader["NavMenuType"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowMethod=StringUtils.GetDbInt(reader["ShowMethod"]);
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
        public int  ExistNum(ConfigNavMenuEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ConfigNavMenu] WITH(NOLOCK) ";
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
