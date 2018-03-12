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
功能描述：SysUser表的数据访问类。
创建时间：2016/7/30 10:41:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
	/// <summary>
	/// SysUserEntity的数据访问操作
	/// </summary>
	public partial class SysUserDA: BaseSuperMarketDB
    {
        #region 实例化
        public static SysUserDA Instance
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
            internal static readonly SysUserDA instance = new SysUserDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表SysUser，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sysUser">待插入的实体对象</param>
		public int AddSysUser(SysUserEntity entity)
		{
		   string sql=@"insert into SysUser( [UserCode],[UserName],[PassWord],[IsActive],[CreateTime],[UserLevel])VALUES
			            ( @UserCode,@UserName,@PassWord,@IsActive,@CreateTime,@UserLevel);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@UserCode",  DbType.String,entity.UserCode);
			db.AddInParameter(cmd,"@UserName",  DbType.String,entity.UserName);
			db.AddInParameter(cmd,"@PassWord",  DbType.String, CryptMD5.Encrypt(entity.PassWord));
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UserLevel",  DbType.Int32,entity.UserLevel);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sysUser">待更新的实体对象</param>
		public   int UpdateSysUser(SysUserEntity entity)
		{
			string sql=@" UPDATE dbo.[SysUser] SET
                       [UserCode]=@UserCode,[UserName]=@UserName,[PassWord]=@PassWord,[IsActive]=@IsActive,[CreateTime]=@CreateTime,[UserLevel]=@UserLevel
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@UserCode",  DbType.String,entity.UserCode);
			db.AddInParameter(cmd,"@UserName",  DbType.String,entity.UserName);
			db.AddInParameter(cmd,"@PassWord",  DbType.String,entity.PassWord);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UserLevel",  DbType.Int32,entity.UserLevel);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteSysUserByKey(int id)
	    {
			string sql=@"delete from SysUser where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSysUserDisabled()
        {
            string sql = @"delete from  SysUser  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSysUserByIds(string ids)
        {
            string sql = @"Delete from SysUser  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSysUserByIds(string ids)
        {
            string sql = @"Update   SysUser set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   SysUserEntity GetSysUser(int id)
		{
			string sql=@"SELECT  [Id],[UserCode],[UserName],[PassWord],[IsActive],[CreateTime],[UserLevel]
							FROM
							dbo.[SysUser] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		SysUserEntity entity=new SysUserEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.UserCode=StringUtils.GetDbString(reader["UserCode"]);
					entity.UserName=StringUtils.GetDbString(reader["UserName"]);
					entity.PassWord=StringUtils.GetDbString(reader["PassWord"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UserLevel=StringUtils.GetDbInt(reader["UserLevel"]);
				}
   		    }
            return entity;
		}
        public SysUserEntity GetUserByUserCode(string code)
        {
            string sql = @"SELECT  [Id],[UserCode],[UserName],[PassWord],[IsActive],[CreateTime],[UserLevel]
							FROM
							dbo.[SysUser] WITH(NOLOCK)	
							WHERE [UserCode]=@UserCode and IsActive=1";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@UserCode", DbType.String, code);
            SysUserEntity entity = new SysUserEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.UserCode = StringUtils.GetDbString(reader["UserCode"]);
                    entity.UserName = StringUtils.GetDbString(reader["UserName"]);
                    entity.PassWord = StringUtils.GetDbString(reader["PassWord"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UserLevel = StringUtils.GetDbInt(reader["UserLevel"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<SysUserEntity> GetSysUserList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[UserCode],[UserName],[PassWord],[IsActive],[CreateTime],[UserLevel]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[UserCode],[UserName],[PassWord],[IsActive],[CreateTime],[UserLevel] from dbo.[SysUser] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[SysUser] with (nolock) ";
            IList<SysUserEntity> entityList = new List< SysUserEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					SysUserEntity entity=new SysUserEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.UserCode=StringUtils.GetDbString(reader["UserCode"]);
					entity.UserName=StringUtils.GetDbString(reader["UserName"]);
					entity.PassWord=StringUtils.GetDbString(reader["PassWord"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UserLevel=StringUtils.GetDbInt(reader["UserLevel"]);
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
        public IList<SysUserEntity> GetSysUserAll()
        {

            string sql = @"SELECT    [Id],[UserCode],[UserName],[PassWord],[IsActive],[CreateTime],[UserLevel] from dbo.[SysUser] WITH(NOLOCK)	";
		    IList<SysUserEntity> entityList = new List<SysUserEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   SysUserEntity entity=new SysUserEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.UserCode=StringUtils.GetDbString(reader["UserCode"]);
					entity.UserName=StringUtils.GetDbString(reader["UserName"]);
					entity.PassWord=StringUtils.GetDbString(reader["PassWord"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UserLevel=StringUtils.GetDbInt(reader["UserLevel"]);
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
        public int  ExistNum(SysUserEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[SysUser] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (UserCode=@UserCode) "; 
            }
            else
            {
					     where = where+ " id<>@Id and  (UserCode=@UserCode) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@UserCode", DbType.String, entity.UserCode); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
