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
功能描述：Suggest表的数据访问类。
创建时间：2016/8/1 14:58:49
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemSuggestEntity的数据访问操作
	/// </summary>
	public partial class MemSuggestDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemSuggestDA Instance
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
            internal static readonly MemSuggestDA instance = new MemSuggestDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Suggest，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="suggest">待插入的实体对象</param>
		public int AddSuggest(MemSuggestEntity entity)
		{
		   string sql=@"insert into MemSuggest( [SuggestType],[Title],[MobilePhone],[ManName],[Email],[SuggestContent],[IpAddress],[ClientType],[Status],[CreateTime])VALUES
			            ( @SuggestType,@Title,@MobilePhone,@ManName,@Email,@SuggestContent,@IpAddress,@ClientType,@Status,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@SuggestType",  DbType.Int32,entity.SuggestType);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@ManName",  DbType.String,entity.ManName);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@SuggestContent",  DbType.String,entity.SuggestContent);
			db.AddInParameter(cmd,"@IpAddress",  DbType.String,entity.IpAddress);
			db.AddInParameter(cmd,"@ClientType",  DbType.Int32,entity.ClientType);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="suggest">待更新的实体对象</param>
		public   int UpdateSuggest(MemSuggestEntity entity)
		{
			string sql=@" UPDATE dbo.[MemSuggest] SET
                       [SuggestType]=@SuggestType,[Title]=@Title,[MobilePhone]=@MobilePhone,[ManName]=@ManName,[Email]=@Email,[SuggestContent]=@SuggestContent,[IpAddress]=@IpAddress,[ClientType]=@ClientType,[Status]=@Status,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@SuggestType",  DbType.Int32,entity.SuggestType);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@ManName",  DbType.String,entity.ManName);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@SuggestContent",  DbType.String,entity.SuggestContent);
			db.AddInParameter(cmd,"@IpAddress",  DbType.String,entity.IpAddress);
			db.AddInParameter(cmd,"@ClientType",  DbType.Int32,entity.ClientType);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteSuggestByKey(int id)
	    {
			string sql=@"delete from MemSuggest where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSuggestDisabled()
        {
            string sql = @"delete from  MemSuggest  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSuggestByIds(string ids)
        {
            string sql = @"Delete from MemSuggest  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSuggestByIds(string ids)
        {
            string sql = @"Update   MemSuggest set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemSuggestEntity GetSuggest(int id)
		{
			string sql=@"SELECT  [Id],[SuggestType],[Title],[MobilePhone],[ManName],[Email],[SuggestContent],[IpAddress],[ClientType],[Status],[CreateTime]
							FROM
							dbo.[MemSuggest] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemSuggestEntity entity=new MemSuggestEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SuggestType=StringUtils.GetDbInt(reader["SuggestType"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.ManName=StringUtils.GetDbString(reader["ManName"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.SuggestContent=StringUtils.GetDbString(reader["SuggestContent"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
					entity.ClientType=StringUtils.GetDbInt(reader["ClientType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<MemSuggestEntity> GetSuggestList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[SuggestType],[Title],[MobilePhone],[ManName],[Email],[SuggestContent],[IpAddress],[ClientType],[Status],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[SuggestType],[Title],[MobilePhone],[ManName],[Email],[SuggestContent],[IpAddress],[ClientType],[Status],[CreateTime] from dbo.[MemSuggest] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemSuggest] with (nolock) ";
            IList<MemSuggestEntity> entityList = new List< MemSuggestEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemSuggestEntity entity=new MemSuggestEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SuggestType=StringUtils.GetDbInt(reader["SuggestType"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.ManName=StringUtils.GetDbString(reader["ManName"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.SuggestContent=StringUtils.GetDbString(reader["SuggestContent"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
					entity.ClientType=StringUtils.GetDbInt(reader["ClientType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<MemSuggestEntity> GetSuggestAll()
        {

            string sql = @"SELECT    [Id],[SuggestType],[Title],[MobilePhone],[ManName],[Email],[SuggestContent],[IpAddress],[ClientType],[Status],[CreateTime] from dbo.[MemSuggest] WITH(NOLOCK)	";
		    IList<MemSuggestEntity> entityList = new List<MemSuggestEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemSuggestEntity entity=new MemSuggestEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SuggestType=StringUtils.GetDbInt(reader["SuggestType"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.ManName=StringUtils.GetDbString(reader["ManName"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.SuggestContent=StringUtils.GetDbString(reader["SuggestContent"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
					entity.ClientType=StringUtils.GetDbInt(reader["ClientType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(MemSuggestEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemSuggest] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (ManName=@ManName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (ManName=@ManName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@ManName", DbType.String, entity.ManName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
