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
功能描述：MemRole表的数据访问类。
创建时间：2016/8/1 14:58:49
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemRoleEntity的数据访问操作
	/// </summary>
	public partial class MemRoleDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemRoleDA Instance
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
            internal static readonly MemRoleDA instance = new MemRoleDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemRole，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memRole">待插入的实体对象</param>
		public int AddMemRole(MemRoleEntity entity)
		{
		   string sql=@"insert into MemRole( [RoleCode],[RoleName],[Remark],[IsActive])VALUES
			            ( @RoleCode,@RoleName,@Remark,@IsActive);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@RoleCode",  DbType.String,entity.RoleCode);
			db.AddInParameter(cmd,"@RoleName",  DbType.String,entity.RoleName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
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
		/// <param name="memRole">待更新的实体对象</param>
		public   int UpdateMemRole(MemRoleEntity entity)
		{
			string sql=@" UPDATE dbo.[MemRole] SET
                       [RoleCode]=@RoleCode,[RoleName]=@RoleName,[Remark]=@Remark,[IsActive]=@IsActive
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@RoleCode",  DbType.String,entity.RoleCode);
			db.AddInParameter(cmd,"@RoleName",  DbType.String,entity.RoleName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemRoleByKey(int id)
	    {
			string sql=@"delete from MemRole where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemRoleDisabled()
        {
            string sql = @"delete from  MemRole  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemRoleByIds(string ids)
        {
            string sql = @"Delete from MemRole  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemRoleByIds(string ids)
        {
            string sql = @"Update   MemRole set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemRoleEntity GetMemRole(int id)
		{
			string sql=@"SELECT  [Id],[RoleCode],[RoleName],[Remark],[IsActive]
							FROM
							dbo.[MemRole] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemRoleEntity entity=new MemRoleEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleCode=StringUtils.GetDbString(reader["RoleCode"]);
					entity.RoleName=StringUtils.GetDbString(reader["RoleName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
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
		public   IList<MemRoleEntity> GetMemRoleList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[RoleCode],[RoleName],[Remark],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[RoleCode],[RoleName],[Remark],[IsActive] from dbo.[MemRole] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemRole] with (nolock) ";
            IList<MemRoleEntity> entityList = new List< MemRoleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemRoleEntity entity=new MemRoleEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleCode=StringUtils.GetDbString(reader["RoleCode"]);
					entity.RoleName=StringUtils.GetDbString(reader["RoleName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
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
        public IList<MemRoleEntity> GetMemRoleAll()
        {

            string sql = @"SELECT    [Id],[RoleCode],[RoleName],[Remark],[IsActive] from dbo.[MemRole] WITH(NOLOCK)	";
		    IList<MemRoleEntity> entityList = new List<MemRoleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemRoleEntity entity=new MemRoleEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleCode=StringUtils.GetDbString(reader["RoleCode"]);
					entity.RoleName=StringUtils.GetDbString(reader["RoleName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				    entityList.Add(entity);
                }
            } 
            return entityList;
        }
        public IList<MemRoleEntity> GetRoleAllByMemId(int id)
        {
            string sql = @"SELECT    a.[Id],[RoleCode],[RoleName],[Remark],[IsActive] from dbo.[MemRole]  a
WITH(NOLOCK)  INNER JOIN dbo.MemRoleRelate b WITH(NOLOCK)  ON a.id=b.Roleid
WHERE B.MEMid=@MemId	";
            IList<MemRoleEntity> entityList = new List<MemRoleEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, id);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemRoleEntity entity = new MemRoleEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.RoleCode = StringUtils.GetDbString(reader["RoleCode"]);
                    entity.RoleName = StringUtils.GetDbString(reader["RoleName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(MemRoleEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemRole] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (RoleName=@RoleName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (RoleName=@RoleName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@RoleName", DbType.String, entity.RoleName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
