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
功能描述：GYAuth表的数据访问类。
创建时间：2016/7/30 10:41:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
	/// <summary>
	/// GYAuthEntity的数据访问操作
	/// </summary>
	public partial class GYAuthDA: BaseSuperMarketDB
    {
        #region 实例化
        public static GYAuthDA Instance
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
            internal static readonly GYAuthDA instance = new GYAuthDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表GYAuth，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="gYAuth">待插入的实体对象</param>
		public int AddGYAuth(GYAuthEntity entity)
		{
		   string sql=@"insert into GYAuth( [AuthCode],[AuthName],[IsActive],[Remark],[CreateTime],[UpdateTime],[UpdateManId])VALUES
			            ( @AuthCode,@AuthName,@IsActive,@Remark,@CreateTime,@UpdateTime,@UpdateManId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@AuthCode",  DbType.String,entity.AuthCode);
			db.AddInParameter(cmd,"@AuthName",  DbType.String,entity.AuthName);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			db.AddInParameter(cmd,"@UpdateManId",  DbType.Int32,entity.UpdateManId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="gYAuth">待更新的实体对象</param>
		public   int UpdateGYAuth(GYAuthEntity entity)
		{
			string sql=@" UPDATE dbo.[GYAuth] SET
                       [AuthCode]=@AuthCode,[AuthName]=@AuthName,[IsActive]=@IsActive,[Remark]=@Remark,[CreateTime]=@CreateTime,[UpdateTime]=@UpdateTime,[UpdateManId]=@UpdateManId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@AuthCode",  DbType.String,entity.AuthCode);
			db.AddInParameter(cmd,"@AuthName",  DbType.String,entity.AuthName);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			db.AddInParameter(cmd,"@UpdateManId",  DbType.Int32,entity.UpdateManId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteGYAuthByKey(int id)
	    {
			string sql=@"delete from GYAuth where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteGYAuthDisabled()
        {
            string sql = @"delete from  GYAuth  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteGYAuthByIds(string ids)
        {
            string sql = @"Delete from GYAuth  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableGYAuthByIds(string ids)
        {
            string sql = @"Update   GYAuth set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   GYAuthEntity GetGYAuth(int id)
		{
			string sql=@"SELECT  [Id],[AuthCode],[AuthName],[IsActive],[Remark],[CreateTime],[UpdateTime],[UpdateManId]
							FROM
							dbo.[GYAuth] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		GYAuthEntity entity=new GYAuthEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.AuthCode=StringUtils.GetDbString(reader["AuthCode"]);
					entity.AuthName=StringUtils.GetDbString(reader["AuthName"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.UpdateManId=StringUtils.GetDbInt(reader["UpdateManId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<GYAuthEntity> GetGYAuthList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[AuthCode],[AuthName],[IsActive],[Remark],[CreateTime],[UpdateTime],[UpdateManId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[AuthCode],[AuthName],[IsActive],[Remark],[CreateTime],[UpdateTime],[UpdateManId] from dbo.[GYAuth] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[GYAuth] with (nolock) ";
            IList<GYAuthEntity> entityList = new List< GYAuthEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					GYAuthEntity entity=new GYAuthEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.AuthCode=StringUtils.GetDbString(reader["AuthCode"]);
					entity.AuthName=StringUtils.GetDbString(reader["AuthName"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.UpdateManId=StringUtils.GetDbInt(reader["UpdateManId"]);
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
        public IList<GYAuthEntity> GetGYAuthAll()
        {

            string sql = @"SELECT    [Id],[AuthCode],[AuthName],[IsActive],[Remark],[CreateTime],[UpdateTime],[UpdateManId] from dbo.[GYAuth] WITH(NOLOCK)	";
		    IList<GYAuthEntity> entityList = new List<GYAuthEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   GYAuthEntity entity=new GYAuthEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.AuthCode=StringUtils.GetDbString(reader["AuthCode"]);
					entity.AuthName=StringUtils.GetDbString(reader["AuthName"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.UpdateManId=StringUtils.GetDbInt(reader["UpdateManId"]);
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
        public int  ExistNum(GYAuthEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[GYAuth] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (AuthName=@AuthName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (AuthName=@AuthName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@AuthName", DbType.String, entity.AuthName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
