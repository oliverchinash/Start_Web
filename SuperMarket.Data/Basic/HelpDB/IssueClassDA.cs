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
功能描述：IssueClass表的数据访问类。
创建时间：2016/10/8 13:48:13
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.HelpDB
{
	/// <summary>
	/// IssueClassEntity的数据访问操作
	/// </summary>
	public partial class IssueClassDA: BaseSuperMarketDB
    {
        #region 实例化
        public static IssueClassDA Instance
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
            internal static readonly IssueClassDA instance = new IssueClassDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表IssueClass，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="issueClass">待插入的实体对象</param>
		public int AddIssueClass(IssueClassEntity entity)
		{
		   string sql= @"insert into IssueClass( [ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType)VALUES
			            ( @ClassName,@ParentId,@Sort,@UrlMethod,@HasContent,@IsActive,@SystemType);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassName",  DbType.String,entity.ClassName);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@UrlMethod",  DbType.Int32,entity.UrlMethod);
			db.AddInParameter(cmd,"@HasContent",  DbType.Int32,entity.HasContent);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);  
			db.AddInParameter(cmd, "@SystemType",  DbType.Int32,entity.SystemType); 

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="issueClass">待更新的实体对象</param>
		public   int UpdateIssueClass(IssueClassEntity entity)
		{
			string sql= @" UPDATE dbo.[IssueClass] SET
                       [ClassName]=@ClassName,[ParentId]=@ParentId,[Sort]=@Sort,[UrlMethod]=@UrlMethod,[HasContent]=@HasContent,[IsActive]=@IsActive
                    ,SystemType=@SystemType   WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassName",  DbType.String,entity.ClassName);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@UrlMethod",  DbType.Int32,entity.UrlMethod);
			db.AddInParameter(cmd,"@HasContent",  DbType.Int32,entity.HasContent);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd, "@SystemType",  DbType.Int32,entity.SystemType);
            return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteIssueClassByKey(int id)
	    {
			string sql=@"delete from IssueClass where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIssueClassDisabled()
        {
            string sql = @"delete from  IssueClass  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIssueClassByIds(string ids)
        {
            string sql = @"Delete from IssueClass  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIssueClassByIds(string ids)
        {
            string sql = @"Update   IssueClass set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IssueClassEntity GetIssueClass(int id)
		{
			string sql= @"SELECT  [Id],[ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType
							FROM
							dbo.[IssueClass] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		IssueClassEntity entity=new IssueClassEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassName=StringUtils.GetDbString(reader["ClassName"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.UrlMethod=StringUtils.GetDbInt(reader["UrlMethod"]);
					entity.HasContent=StringUtils.GetDbInt(reader["HasContent"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);  
					entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]); 

                }
   		    }
            return entity;
		}

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IssueClassEntity GetIssueClassByPid(int pid)
        {
            string sql = @"SELECT  top (1) [Id],[ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType
							FROM
							dbo.[IssueClass] WITH(NOLOCK)	
							WHERE [ParentId]=@ParentId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
            IssueClassEntity entity = new IssueClassEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassName = StringUtils.GetDbString(reader["ClassName"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.UrlMethod = StringUtils.GetDbInt(reader["UrlMethod"]);
                    entity.HasContent = StringUtils.GetDbInt(reader["HasContent"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]);

                }
            }
            return entity;
        }
        

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<IssueClassEntity> GetIssueClassList(int pagesize, int pageindex, ref  int recordCount ,int systype)
		{
            string where = " where 1=1 ";
            if(systype!=-1)
            {
                where += " and SystemType=@SystemType";
            }
			string sql= @"SELECT   [Id],[ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType from dbo.[IssueClass] WITH(NOLOCK)	
						"+ where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[IssueClass] with (nolock) "+ where;
            IList<IssueClassEntity> entityList = new List< IssueClassEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (systype != -1)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, systype); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					IssueClassEntity entity=new IssueClassEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassName=StringUtils.GetDbString(reader["ClassName"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.UrlMethod=StringUtils.GetDbInt(reader["UrlMethod"]);
					entity.HasContent=StringUtils.GetDbInt(reader["HasContent"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);  
					entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]); 

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
        public IList<IssueClassEntity> GetIssueClassAll()
        {

            string sql = @"SELECT    [Id],[ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType from dbo.[IssueClass] WITH(NOLOCK)	";
		    IList<IssueClassEntity> entityList = new List<IssueClassEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   IssueClassEntity entity=new IssueClassEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassName=StringUtils.GetDbString(reader["ClassName"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.UrlMethod=StringUtils.GetDbInt(reader["UrlMethod"]);
					entity.HasContent=StringUtils.GetDbInt(reader["HasContent"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);  
					entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]); 

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
        public IList<IssueClassEntity> GetIssueClassByParentId(int parentid)
        {

            string sql = @"SELECT    [Id],[ClassName],[ParentId],[Sort],[UrlMethod],[HasContent],[IsActive],SystemType from dbo.[IssueClass] WITH(NOLOCK)	where ParentId=@ParentId";
            IList<IssueClassEntity> entityList = new List<IssueClassEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    IssueClassEntity entity = new IssueClassEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassName = StringUtils.GetDbString(reader["ClassName"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.UrlMethod = StringUtils.GetDbInt(reader["UrlMethod"]);
                    entity.HasContent = StringUtils.GetDbInt(reader["HasContent"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);  
                    entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]); 
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
        public int  ExistNum(IssueClassEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[IssueClass] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (ClassName=@ClassName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (ClassName=@ClassName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@ClassName", DbType.String, entity.ClassName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
