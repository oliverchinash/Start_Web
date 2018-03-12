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
功能描述：WeChatNavigation表的数据访问类。
创建时间：2017/8/16 16:21:41
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
	/// <summary>
	/// WeChatNavigationEntity的数据访问操作
	/// </summary>
	public partial class WeChatNavigationDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WeChatNavigationDA Instance
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
            internal static readonly WeChatNavigationDA instance = new WeChatNavigationDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WeChatNavigation，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weChatNavigation">待插入的实体对象</param>
		public int AddWeChatNavigation(WeChatNavigationEntity entity)
		{
		   string sql= @"insert into WeChatNavigation( [WeChatNavCode],[RedirectUrl],[WeChatUrl],WeChatUrlType,[Remark])VALUES
			            ( @WeChatNavCode,@RedirectUrl,@WeChatUrl,@WeChatUrlType,@Remark);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@WeChatNavCode",  DbType.Int32,entity.WeChatNavCode);
			db.AddInParameter(cmd,"@RedirectUrl",  DbType.String,entity.RedirectUrl);
			db.AddInParameter(cmd, "@WeChatUrl",  DbType.String,entity.WeChatUrl);
			db.AddInParameter(cmd, "@WeChatUrlType",  DbType.String,entity.WeChatUrlType);
            db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weChatNavigation">待更新的实体对象</param>
		public   int UpdateWeChatNavigation(WeChatNavigationEntity entity)
		{
			string sql= @" UPDATE dbo.[WeChatNavigation] SET
                       [WeChatNavCode]=@WeChatNavCode,[RedirectUrl]=@RedirectUrl,[WeChatUrl]=@WeChatUrl,[WeChatUrlType]=@WeChatUrlType,[Remark]=@Remark
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@WeChatNavCode",  DbType.Int32,entity.WeChatNavCode);
			db.AddInParameter(cmd,"@RedirectUrl",  DbType.String,entity.RedirectUrl);
			db.AddInParameter(cmd, "@WeChatUrl",  DbType.String,entity.WeChatUrl);
			db.AddInParameter(cmd, "@WeChatUrlType",  DbType.String,entity.WeChatUrlType);
            db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteWeChatNavigationByKey(int id)
	    {
			string sql=@"delete from WeChatNavigation where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeChatNavigationDisabled()
        {
            string sql = @"delete from  WeChatNavigation  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeChatNavigationByIds(string ids)
        {
            string sql = @"Delete from WeChatNavigation  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeChatNavigationByIds(string ids)
        {
            string sql = @"Update   WeChatNavigation set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WeChatNavigationEntity GetWeChatNavigation(int id)
		{
			string sql= @"SELECT  [Id],[WeChatNavCode],[RedirectUrl],[WeChatUrl],[Remark]
							FROM
							dbo.[WeChatNavigation] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WeChatNavigationEntity entity=new WeChatNavigationEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.WeChatNavCode=StringUtils.GetDbInt(reader["WeChatNavCode"]);
					entity.RedirectUrl=StringUtils.GetDbString(reader["RedirectUrl"]);
					entity.WeChatUrl = StringUtils.GetDbString(reader["WeChatUrl"]);
                    entity.Remark=StringUtils.GetDbString(reader["Remark"]);
				}
   		    }
            return entity;
		}
        public WeChatNavigationEntity GetNavigationByCode(int code)
        {
            string sql = @"SELECT  [Id],[WeChatNavCode],[RedirectUrl],[Remark]
							FROM
							dbo.[WeChatNavigation] WITH(NOLOCK)	
							WHERE [WeChatNavCode]=@WeChatNavCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@WeChatNavCode", DbType.String, code);
            WeChatNavigationEntity entity = new WeChatNavigationEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.WeChatNavCode = StringUtils.GetDbInt(reader["WeChatNavCode"]);
                    entity.RedirectUrl = StringUtils.GetDbString(reader["RedirectUrl"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                }
            }
            return entity;
        }
        public WeChatNavigationEntity GetNavigationByUrl(string url)
        {
            string sql = @"SELECT  [Id],[WeChatNavCode],[RedirectUrl],[WeChatUrl],[Remark]
							FROM
							dbo.[WeChatNavigation] WITH(NOLOCK)	
							WHERE [RedirectUrl]=@RedirectUrl";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@RedirectUrl", DbType.String, url);
            WeChatNavigationEntity entity = new WeChatNavigationEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.WeChatNavCode = StringUtils.GetDbInt(reader["WeChatNavCode"]);
                    entity.RedirectUrl = StringUtils.GetDbString(reader["RedirectUrl"]); 
                    entity.WeChatUrl = StringUtils.GetDbString(reader["WeChatUrl"]); 
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                }
            }
            return entity;
        }

        public int GetIdByUrl(string url)
        {
            string sql = @"IF EXISTS(SELECT 1 FROM dbo.[WeChatNavigation] WITH(NOLOCK) WHERE RedirectUrl=@RedirectUrl)
                        BEGIN 
                          SELECT id FROM dbo.[WeChatNavigation] WITH(NOLOCK) WHERE RedirectUrl=@RedirectUrl
                        END
                        ELSE
                        begin
                          INSERT INTO  dbo.[WeChatNavigation](WeChatNavCode,RedirectUrl,Remark)
                          VALUES('',@RedirectUrl,'')
                          SELECT @@IDENTITY
                        END";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@RedirectUrl", DbType.String, url);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<WeChatNavigationEntity> GetWeChatNavigationList(int pagesize, int pageindex, ref  int recordCount,string key)
		{
            string where = " where 1=1 ";
            if(!string.IsNullOrEmpty(key))
            {
                where += " and RedirectUrl like @RedirectUrl ";
            }
			string sql= @"SELECT   [Id],[WeChatNavCode],[RedirectUrl],[WeChatUrl],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[WeChatNavCode],[RedirectUrl],WeChatUrl,[Remark] from dbo.[WeChatNavigation] WITH(NOLOCK)	
					 "+ where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WeChatNavigation] with (nolock) "+where;
            IList<WeChatNavigationEntity> entityList = new List< WeChatNavigationEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(key))
            {
		    db.AddInParameter(cmd, "@RedirectUrl", DbType.String, "%"+ key+"%"); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WeChatNavigationEntity entity=new WeChatNavigationEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.WeChatNavCode=StringUtils.GetDbInt(reader["WeChatNavCode"]);
					entity.RedirectUrl=StringUtils.GetDbString(reader["RedirectUrl"]);
					entity.WeChatUrl = StringUtils.GetDbString(reader["WeChatUrl"]);
                    entity.Remark=StringUtils.GetDbString(reader["Remark"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@RedirectUrl", DbType.String, "%" + key + "%");
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
        public IList<WeChatNavigationEntity> GetWeChatNavigationAll()
        {

            string sql = @"SELECT    [Id],[WeChatNavCode],[RedirectUrl],[Remark] from dbo.[WeChatNavigation] WITH(NOLOCK)	";
		    IList<WeChatNavigationEntity> entityList = new List<WeChatNavigationEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WeChatNavigationEntity entity=new WeChatNavigationEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.WeChatNavCode=StringUtils.GetDbInt(reader["WeChatNavCode"]);
					entity.RedirectUrl=StringUtils.GetDbString(reader["RedirectUrl"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
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
        public int  ExistNum(WeChatNavigationEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WeChatNavigation] WITH(NOLOCK) ";
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
