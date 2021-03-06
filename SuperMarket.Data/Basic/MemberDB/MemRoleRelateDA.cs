﻿using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;

/*****************************************
功能描述：MemRoleRelate表的数据访问类。
创建时间：2016/8/1 17:46:30
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemRoleRelateEntity的数据访问操作
	/// </summary>
	public partial class MemRoleRelateDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemRoleRelateDA Instance
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
            internal static readonly MemRoleRelateDA instance = new MemRoleRelateDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemRoleRelate，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memRoleRelate">待插入的实体对象</param>
		public int AddMemRoleRelate(MemRoleRelateEntity entity)
		{
		   string sql=@"insert into MemRoleRelate( [RoleId],[MemId])VALUES
			            ( @RoleId,@MemId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@RoleId",  DbType.Int32,entity.RoleId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddRolesToMem(int memid,string roleids)
        {
            string sql = @"
delete from MemRoleRelate where MemId=@MemId
insert into MemRoleRelate( [RoleId],[MemId])
SELECT Id,@MemId FROM dbo.fun_splitstr(@RoleStr,'_') WHERE id>0
";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@RoleStr", DbType.String, roleids);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="memRoleRelate">待更新的实体对象</param>
        public   int UpdateMemRoleRelate(MemRoleRelateEntity entity)
		{
			string sql=@" UPDATE dbo.[MemRoleRelate] SET
                       [RoleId]=@RoleId,[MemId]=@MemId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@RoleId",  DbType.Int32,entity.RoleId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemRoleRelateByKey(int id)
	    {
			string sql=@"delete from MemRoleRelate where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemRoleRelateDisabled()
        {
            string sql = @"delete from  MemRoleRelate  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemRoleRelateByIds(string ids)
        {
            string sql = @"Delete from MemRoleRelate  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemRoleRelateByIds(string ids)
        {
            string sql = @"Update   MemRoleRelate set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemRoleRelateEntity GetMemRoleRelate(int id)
		{
			string sql=@"SELECT  [Id],[RoleId],[MemId]
							FROM
							dbo.[MemRoleRelate] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemRoleRelateEntity entity=new MemRoleRelateEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleId=StringUtils.GetDbInt(reader["RoleId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<MemRoleRelateEntity> GetMemRoleRelateList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[RoleId],[MemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[RoleId],[MemId] from dbo.[MemRoleRelate] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemRoleRelate] with (nolock) ";
            IList<MemRoleRelateEntity> entityList = new List< MemRoleRelateEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemRoleRelateEntity entity=new MemRoleRelateEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleId=StringUtils.GetDbInt(reader["RoleId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
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
        public IList<MemRoleRelateEntity> GetMemRoleRelateAll()
        {

            string sql = @"SELECT    [Id],[RoleId],[MemId] from dbo.[MemRoleRelate] WITH(NOLOCK)	";
		    IList<MemRoleRelateEntity> entityList = new List<MemRoleRelateEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemRoleRelateEntity entity=new MemRoleRelateEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.RoleId=StringUtils.GetDbInt(reader["RoleId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
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
        public int  ExistNum(MemRoleRelateEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemRoleRelate] WITH(NOLOCK) ";
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
