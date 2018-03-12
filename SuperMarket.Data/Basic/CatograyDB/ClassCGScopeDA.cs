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
功能描述：ClassCGScope表的数据访问类。
创建时间：2017/10/25 11:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// ClassCGScopeEntity的数据访问操作
	/// </summary>
	public partial class ClassCGScopeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ClassCGScopeDA Instance
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
            internal static readonly ClassCGScopeDA instance = new ClassCGScopeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ClassCGScope，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="classCGScope">待插入的实体对象</param>
		public int AddClassCGScope(ClassCGScopeEntity entity)
		{
		   string sql= @"insert into ClassCGScope( [ClassId],[ScopeClassName],[IsActive],[Sort],ParentId,IsRoot,ScopeType)VALUES
			            ( @ClassId,@ScopeClassName,@IsActive,@Sort,@ParentId,@IsRoot,@ScopeType);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ScopeClassName",  DbType.String,entity.ScopeClassName);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd, "@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd, "@IsRoot",  DbType.Int32,entity.IsRoot);
			db.AddInParameter(cmd, "@ScopeType",  DbType.Int32,entity.ScopeType);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="classCGScope">待更新的实体对象</param>
		public   int UpdateClassCGScope(ClassCGScopeEntity entity)
		{
			string sql=@" UPDATE dbo.[ClassCGScope] SET
                       [ClassId]=@ClassId,[ScopeClassName]=@ScopeClassName,[IsActive]=@IsActive,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ScopeClassName",  DbType.String,entity.ScopeClassName);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteClassCGScopeByKey(int id)
	    {
			string sql=@"delete from ClassCGScope where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassCGScopeDisabled()
        {
            string sql = @"delete from  ClassCGScope  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassCGScopeByIds(string ids)
        {
            string sql = @"Delete from ClassCGScope  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassCGScopeByIds(string ids)
        {
            string sql = @"Update   ClassCGScope set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ClassCGScopeEntity GetClassCGScope(int id)
		{
			string sql= @"SELECT  [Id],[ClassId],[ScopeClassName],[IsActive],[Sort],ParentId,IsRoot,ScopeType
							FROM
							dbo.[ClassCGScope] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ClassCGScopeEntity entity=new ClassCGScopeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ScopeClassName=StringUtils.GetDbString(reader["ScopeClassName"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]); 
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsRoot = StringUtils.GetDbInt(reader["IsRoot"]);
                    entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
                }
   		    }
            return entity;
		}

        public ClassCGScopeEntity GetClassCGScopeByName(string name)
        {
            string sql = @"SELECT top 1 [Id],[ClassId],[ScopeClassName],[IsActive],[Sort],ParentId,IsRoot,ScopeType
							FROM
							dbo.[ClassCGScope] WITH(NOLOCK)	
							WHERE [ScopeClassName]=@ScopeClassName Order By Sort Desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ScopeClassName", DbType.String, name);
            ClassCGScopeEntity entity = new ClassCGScopeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.ScopeClassName = StringUtils.GetDbString(reader["ScopeClassName"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.IsRoot = StringUtils.GetDbInt(reader["IsRoot"]);
                    entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
                }
            }
            return entity;
        }

        public ClassCGScopeEntity GetParentByScopeName(string name)
        {
            string sql = @"SELECT  [Id],[ClassId],[ScopeClassName],[IsActive],[Sort],ParentId,IsRoot
							FROM
							dbo.[ClassCGScope] WITH(NOLOCK)	
							WHERE [Id]=(Select top 1 ParentId from dbo.[ClassCGScope] WITH(NOLOCK)	 where ScopeClassName=@ScopeClassName and ParentId>0 )";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ScopeClassName", DbType.String, name);
            ClassCGScopeEntity entity = new ClassCGScopeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.ScopeClassName = StringUtils.GetDbString(reader["ScopeClassName"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
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
        public   IList<ClassCGScopeEntity> GetClassCGScopeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ClassId],[ScopeClassName],[IsActive],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ClassId],[ScopeClassName],[IsActive],[Sort] from dbo.[ClassCGScope] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ClassCGScope] with (nolock) ";
            IList<ClassCGScopeEntity> entityList = new List< ClassCGScopeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ClassCGScopeEntity entity=new ClassCGScopeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ScopeClassName=StringUtils.GetDbString(reader["ScopeClassName"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public IList<VWClassCGScopeEntity> GetClassCGScopeAll(int scopetype,int isroot,int isactive)
        {
            string where = " where 1=1 ";
            if (scopetype != -1)
            {
                where += " and ScopeType=@ScopeType ";
            }
            if (isroot!=-1)
            {
                where += " and IsRoot=@IsRoot ";
            }
            if (isactive != -1)
            {
                where += " and IsActive=@IsActive ";
            }
            string sql = @"SELECT    [Id],[ClassId],[ScopeClassName],[IsActive],[Sort],ParentId,IsRoot,ScopeType from dbo.[ClassCGScope] WITH(NOLOCK)	"+ where + " Order by Sort desc";
		    IList<VWClassCGScopeEntity> entityList = new List<VWClassCGScopeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (scopetype != -1)
            {
                db.AddInParameter(cmd, "@ScopeType", DbType.Int32, scopetype);
            }
            if (isroot != -1)
            {
		    db.AddInParameter(cmd, "@IsRoot", DbType.Int32, isroot); 
            }
            if (isactive != -1)
            {
		    db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);  
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWClassCGScopeEntity entity =new VWClassCGScopeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ScopeClassName=StringUtils.GetDbString(reader["ScopeClassName"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
					entity.IsRoot = StringUtils.GetDbInt(reader["IsRoot"]);
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
        public int  ExistNum(ClassCGScopeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ClassCGScope] WITH(NOLOCK) ";
            string where = "where    ScopeClassName=@ScopeClassName  AND ParentId=@ParentId  AND ScopeType=@ScopeType  "; 
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);  
            db.AddInParameter(cmd, "@ScopeClassName", DbType.String, entity.ScopeClassName); 
            db.AddInParameter(cmd, "@ParentId", DbType.String, entity.ParentId); 
            db.AddInParameter(cmd, "@ScopeType", DbType.String, entity.ScopeType);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
