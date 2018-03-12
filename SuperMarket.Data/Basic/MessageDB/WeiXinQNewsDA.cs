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
功能描述：WeiXinQNews表的数据访问类。
创建时间：2017/7/15 14:16:51
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// WeiXinQNewsEntity的数据访问操作
	/// </summary>
	public partial class WeiXinQNewsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WeiXinQNewsDA Instance
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
            internal static readonly WeiXinQNewsDA instance = new WeiXinQNewsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WeiXinQNews，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weiXinQNews">待插入的实体对象</param>
		public int AddWeiXinQNews(WeiXinQNewsEntity entity)
		{
		   string sql= @"insert into WeiXinQNews( [Title],[ShortTitle],[NewsContent],[CreateTime],[HasShow],[NewsType],FromUrl)VALUES
			            ( @Title,@ShortTitle,@NewsContent,@CreateTime,@HasShow,@NewsType,@FromUrl);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@ShortTitle",  DbType.String,entity.ShortTitle);
			db.AddInParameter(cmd,"@NewsContent",  DbType.String,entity.NewsContent);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@HasShow",  DbType.Int32,entity.HasShow);
			db.AddInParameter(cmd,"@NewsType",  DbType.Int32,entity.NewsType);
			db.AddInParameter(cmd, "@FromUrl",  DbType.String, entity.FromUrl);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weiXinQNews">待更新的实体对象</param>
		public   int UpdateWeiXinQNews(WeiXinQNewsEntity entity)
		{
			string sql= @" UPDATE dbo.[WeiXinQNews] SET
                       [Title]=@Title,[ShortTitle]=@ShortTitle,[NewsContent]=@NewsContent,[CreateTime]=@CreateTime,[HasShow]=@HasShow,[NewsType]=@NewsType,FromUrl=@FromUrl
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@ShortTitle",  DbType.String,entity.ShortTitle);
			db.AddInParameter(cmd,"@NewsContent",  DbType.String,entity.NewsContent);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@HasShow",  DbType.Int32,entity.HasShow);
			db.AddInParameter(cmd,"@NewsType",  DbType.Int32,entity.NewsType);
            db.AddInParameter(cmd, "@FromUrl", DbType.String, entity.FromUrl);
            return db.ExecuteNonQuery(cmd);
		}

        public int WeiXinQNewsPublish(int nid)
        {
            string sql = @" UPDATE dbo.[WeiXinQNews] SET [HasShow]=1 
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, nid); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteWeiXinQNewsByKey(int id)
	    {
			string sql=@"delete from WeiXinQNews where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeiXinQNewsDisabled()
        {
            string sql = @"delete from  WeiXinQNews  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeiXinQNewsByIds(string ids)
        {
            string sql = @"Delete from WeiXinQNews  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeiXinQNewsByIds(string ids)
        {
            string sql = @"Update   WeiXinQNews set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WeiXinQNewsEntity GetWeiXinQNews(int id)
		{
			string sql=@"SELECT  [Id],[Title],[ShortTitle],[NewsContent],[CreateTime],[HasShow],[NewsType],FromUrl
							FROM
							dbo.[WeiXinQNews] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WeiXinQNewsEntity entity=new WeiXinQNewsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.ShortTitle=StringUtils.GetDbString(reader["ShortTitle"]);
					entity.NewsContent=StringUtils.GetDbString(reader["NewsContent"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasShow=StringUtils.GetDbInt(reader["HasShow"]);
					entity.NewsType=StringUtils.GetDbInt(reader["NewsType"]);
					entity.FromUrl = StringUtils.GetDbString(reader["FromUrl"]);
                }
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<WeiXinQNewsEntity> GetWeiXinQNewsList(int pagesize, int pageindex, ref  int recordCount,int newstype, string key)
		{
            string where = " where 1=1 ";
            if (newstype > 0)
            {
                where += " and NewsType=@NewsType ";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and ShortTitle like @Key ";
            }

            string sql= @"SELECT   [Id],[Title],[ShortTitle],[NewsContent],[CreateTime],[HasShow],[NewsType],FromUrl
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Title],[ShortTitle],[NewsContent],[CreateTime],[HasShow],[NewsType],FromUrl from dbo.[WeiXinQNews] WITH(NOLOCK)	
						"+ where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WeiXinQNews] with (nolock) "+where;
            IList<WeiXinQNewsEntity> entityList = new List< WeiXinQNewsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (newstype > 0)
            { 
                db.AddInParameter(cmd, "@NewsType", DbType.Int32, newstype);
            }
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Key", DbType.String,"%"+ key+"%");
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WeiXinQNewsEntity entity=new WeiXinQNewsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.ShortTitle=StringUtils.GetDbString(reader["ShortTitle"]);
					entity.NewsContent=StringUtils.GetDbString(reader["NewsContent"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasShow=StringUtils.GetDbInt(reader["HasShow"]);
					entity.NewsType=StringUtils.GetDbInt(reader["NewsType"]); 
                    entity.FromUrl = StringUtils.GetDbString(reader["FromUrl"]); 
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (newstype > 0)
            {
                db.AddInParameter(cmd, "@NewsType", DbType.Int32, newstype);
            }
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@Key", DbType.String, "%" + key + "%");
            }
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
        public IList<WeiXinQNewsEntity> GetWeiXinQNewsAll()
        {

            string sql = @"SELECT    [Id],[Title],[ShortTitle],[NewsContent],[CreateTime],[HasShow],[NewsType] from dbo.[WeiXinQNews] WITH(NOLOCK)	";
		    IList<WeiXinQNewsEntity> entityList = new List<WeiXinQNewsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WeiXinQNewsEntity entity=new WeiXinQNewsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.ShortTitle=StringUtils.GetDbString(reader["ShortTitle"]);
					entity.NewsContent=StringUtils.GetDbString(reader["NewsContent"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasShow=StringUtils.GetDbInt(reader["HasShow"]);
					entity.NewsType=StringUtils.GetDbInt(reader["NewsType"]);
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
        public int  ExistNum(WeiXinQNewsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WeiXinQNews] WITH(NOLOCK) ";
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
        public bool IsExistTitle(string title)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WeiXinQNews] WITH(NOLOCK) where [Title]=@Title ";
            
            DbCommand cmd = db.GetSqlStringCommand(sql);
     
                db.AddInParameter(cmd, "@Title", DbType.String, title);
             
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return false;
            return Convert.ToInt32(identity)>0;
        }







        #endregion
        #endregion
    }
}
