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
功能描述：CacheConfig表的数据访问类。
创建时间：2016/7/30 10:41:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
	/// <summary>
	/// CacheConfigEntity的数据访问操作
	/// </summary>
	public partial class CacheConfigDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CacheConfigDA Instance
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
            internal static readonly CacheConfigDA instance = new CacheConfigDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CacheConfig，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cacheConfig">待插入的实体对象</param>
		public int AddCacheConfig(CacheConfigEntity entity)
		{
		   string sql=@"insert into CacheConfig( [CacheKey],[Version],[CreateTime],[UpdateTime],[UpdateMan],[Remark],[HasExpend],[IsActive],[DisableTime])VALUES
			            ( @CacheKey,@Version,@CreateTime,@UpdateTime,@UpdateMan,@Remark,@HasExpend,@IsActive,@DisableTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CacheKey",  DbType.String,entity.CacheKey);
			db.AddInParameter(cmd,"@Version",  DbType.Int32,entity.Version);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			db.AddInParameter(cmd,"@UpdateMan",  DbType.Int32,entity.UpdateMan);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@HasExpend",  DbType.Int32,entity.HasExpend);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@DisableTime",  DbType.DateTime,entity.DisableTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cacheConfig">待更新的实体对象</param>
		public   int UpdateCacheConfig(CacheConfigEntity entity)
		{
			string sql=@" UPDATE dbo.[CacheConfig] SET
                       [CacheKey]=@CacheKey,[Version]=@Version,[CreateTime]=@CreateTime,[UpdateTime]=@UpdateTime,[UpdateMan]=@UpdateMan,[Remark]=@Remark,[HasExpend]=@HasExpend,[IsActive]=@IsActive,[DisableTime]=@DisableTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CacheKey",  DbType.String,entity.CacheKey);
			db.AddInParameter(cmd,"@Version",  DbType.Int32,entity.Version);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			db.AddInParameter(cmd,"@UpdateMan",  DbType.Int32,entity.UpdateMan);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@HasExpend",  DbType.Int32,entity.HasExpend);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@DisableTime",  DbType.DateTime,entity.DisableTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCacheConfigByKey(int id)
	    {
			string sql=@"delete from CacheConfig where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCacheConfigDisabled()
        {
            string sql = @"delete from  CacheConfig  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCacheConfigByIds(string ids)
        {
            string sql = @"Delete from CacheConfig  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCacheConfigByIds(string ids)
        {
            string sql = @"Update   CacheConfig set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CacheConfigEntity GetCacheConfig(int id)
		{
			string sql=@"SELECT  [Id],[CacheKey],[Version],[CreateTime],[UpdateTime],[UpdateMan],[Remark],[HasExpend],[IsActive],[DisableTime]
							FROM
							dbo.[CacheConfig] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CacheConfigEntity entity=new CacheConfigEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CacheKey=StringUtils.GetDbString(reader["CacheKey"]);
					entity.Version=StringUtils.GetDbInt(reader["Version"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.UpdateMan=StringUtils.GetDbInt(reader["UpdateMan"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.HasExpend=StringUtils.GetDbInt(reader["HasExpend"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.DisableTime=StringUtils.GetDbDateTime(reader["DisableTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<CacheConfigEntity> GetCacheConfigList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[CacheKey],[Version],[CreateTime],[UpdateTime],[UpdateMan],[Remark],[HasExpend],[IsActive],[DisableTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CacheKey],[Version],[CreateTime],[UpdateTime],[UpdateMan],[Remark],[HasExpend],[IsActive],[DisableTime] from dbo.[CacheConfig] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CacheConfig] with (nolock) ";
            IList<CacheConfigEntity> entityList = new List< CacheConfigEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CacheConfigEntity entity=new CacheConfigEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CacheKey=StringUtils.GetDbString(reader["CacheKey"]);
					entity.Version=StringUtils.GetDbInt(reader["Version"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.UpdateMan=StringUtils.GetDbInt(reader["UpdateMan"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.HasExpend=StringUtils.GetDbInt(reader["HasExpend"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.DisableTime=StringUtils.GetDbDateTime(reader["DisableTime"]);
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
        public IList<CacheConfigEntity> GetCacheConfigAll()
        {

            string sql = @"SELECT    [Id],[CacheKey],[Version],[CreateTime],[UpdateTime],[UpdateMan],[Remark],[HasExpend],[IsActive],[DisableTime] from dbo.[CacheConfig] WITH(NOLOCK)	";
		    IList<CacheConfigEntity> entityList = new List<CacheConfigEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CacheConfigEntity entity=new CacheConfigEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CacheKey=StringUtils.GetDbString(reader["CacheKey"]);
					entity.Version=StringUtils.GetDbInt(reader["Version"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.UpdateMan=StringUtils.GetDbInt(reader["UpdateMan"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.HasExpend=StringUtils.GetDbInt(reader["HasExpend"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.DisableTime=StringUtils.GetDbDateTime(reader["DisableTime"]);
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
        public int  ExistNum(CacheConfigEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CacheConfig] WITH(NOLOCK) ";
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
