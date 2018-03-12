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
功能描述：GYRoleAuth表的数据访问类。
创建时间：2016/7/30 10:41:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
	/// <summary>
	/// GYRoleAuthEntity的数据访问操作
	/// </summary>
	public partial class GYRoleAuthDA: BaseSuperMarketDB
    {
        #region 实例化
        public static GYRoleAuthDA Instance
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
            internal static readonly GYRoleAuthDA instance = new GYRoleAuthDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表GYRoleAuth，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="gYRoleAuth">待插入的实体对象</param>
		public int AddGYRoleAuth(GYRoleAuthEntity entity)
		{
		   string sql=@"insert into GYRoleAuth( [RoleId],[AuthId])VALUES
			            ( @RoleId,@AuthId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@RoleId",  DbType.Int32,entity.RoleId);
			db.AddInParameter(cmd,"@AuthId",  DbType.Int32,entity.AuthId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="gYRoleAuth">待更新的实体对象</param>
		public   int UpdateGYRoleAuth(GYRoleAuthEntity entity)
		{
			string sql=@" UPDATE dbo.[GYRoleAuth] SET
                       [RoleId]=@RoleId,[AuthId]=@AuthId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@RoleId",  DbType.Int32,entity.RoleId);
			db.AddInParameter(cmd,"@AuthId",  DbType.Int32,entity.AuthId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteGYRoleAuthByKey(int id)
	    {
			string sql=@"delete from GYRoleAuth where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteGYRoleAuthDisabled()
        {
            string sql = @"delete from  GYRoleAuth  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteGYRoleAuthByIds(string ids)
        {
            string sql = @"Delete from GYRoleAuth  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableGYRoleAuthByIds(string ids)
        {
            string sql = @"Update   GYRoleAuth set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   GYRoleAuthEntity GetGYRoleAuth(int id)
		{
			string sql=@"SELECT  [Id],[RoleId],[AuthId]
							FROM
							dbo.[GYRoleAuth] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		GYRoleAuthEntity entity=new GYRoleAuthEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleId=StringUtils.GetDbInt(reader["RoleId"]);
					entity.AuthId=StringUtils.GetDbInt(reader["AuthId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<GYRoleAuthEntity> GetGYRoleAuthList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[RoleId],[AuthId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[RoleId],[AuthId] from dbo.[GYRoleAuth] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[GYRoleAuth] with (nolock) ";
            IList<GYRoleAuthEntity> entityList = new List< GYRoleAuthEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					GYRoleAuthEntity entity=new GYRoleAuthEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleId=StringUtils.GetDbInt(reader["RoleId"]);
					entity.AuthId=StringUtils.GetDbInt(reader["AuthId"]);
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
        public IList<GYRoleAuthEntity> GetGYRoleAuthAll()
        {

            string sql = @"SELECT    [Id],[RoleId],[AuthId] from dbo.[GYRoleAuth] WITH(NOLOCK)	";
		    IList<GYRoleAuthEntity> entityList = new List<GYRoleAuthEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   GYRoleAuthEntity entity=new GYRoleAuthEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleId=StringUtils.GetDbInt(reader["RoleId"]);
					entity.AuthId=StringUtils.GetDbInt(reader["AuthId"]);
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
        public int  ExistNum(GYRoleAuthEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[GYRoleAuth] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
				 
            }
            else
            {
            }
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
