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
功能描述：MemWeChatMsg表的数据访问类。
创建时间：2017/8/16 17:20:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemWeChatMsgEntity的数据访问操作
	/// </summary>
	public partial class MemWeChatMsgDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemWeChatMsgDA Instance
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
            internal static readonly MemWeChatMsgDA instance = new MemWeChatMsgDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemWeChatMsg，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memWeChatMsg">待插入的实体对象</param>
		public int AddMemWeChatMsg(MemWeChatMsgEntity entity)
		{
		   string sql=@"insert into MemWeChatMsg( [UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status])VALUES
			            ( @UnionId,@OpenId,@NickName,@Sex,@Language,@City,@Province,@Country,@HeadimgUrl,@AppId,@Status);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@UnionId",  DbType.String,entity.UnionId);
			db.AddInParameter(cmd,"@OpenId",  DbType.String,entity.OpenId);
			db.AddInParameter(cmd,"@NickName",  DbType.String,entity.NickName);
			db.AddInParameter(cmd,"@Sex",  DbType.Int32,entity.Sex);
			db.AddInParameter(cmd,"@Language",  DbType.String,entity.Language);
			db.AddInParameter(cmd,"@City",  DbType.String,entity.City);
			db.AddInParameter(cmd,"@Province",  DbType.String,entity.Province);
			db.AddInParameter(cmd,"@Country",  DbType.String,entity.Country);
			db.AddInParameter(cmd,"@HeadimgUrl",  DbType.String,entity.HeadimgUrl);
			db.AddInParameter(cmd,"@AppId",  DbType.String,entity.AppId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memWeChatMsg">待更新的实体对象</param>
		public   int UpdateMemWeChatMsg(MemWeChatMsgEntity entity)
		{
			string sql=@" UPDATE dbo.[MemWeChatMsg] SET
                       [UnionId]=@UnionId,[OpenId]=@OpenId,[NickName]=@NickName,[Sex]=@Sex,[Language]=@Language,[City]=@City,[Province]=@Province,[Country]=@Country,[HeadimgUrl]=@HeadimgUrl,[AppId]=@AppId,[Status]=@Status
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@UnionId",  DbType.String,entity.UnionId);
			db.AddInParameter(cmd,"@OpenId",  DbType.String,entity.OpenId);
			db.AddInParameter(cmd,"@NickName",  DbType.String,entity.NickName);
			db.AddInParameter(cmd,"@Sex",  DbType.Int32,entity.Sex);
			db.AddInParameter(cmd,"@Language",  DbType.String,entity.Language);
			db.AddInParameter(cmd,"@City",  DbType.String,entity.City);
			db.AddInParameter(cmd,"@Province",  DbType.String,entity.Province);
			db.AddInParameter(cmd,"@Country",  DbType.String,entity.Country);
			db.AddInParameter(cmd,"@HeadimgUrl",  DbType.String,entity.HeadimgUrl);
			db.AddInParameter(cmd,"@AppId",  DbType.String,entity.AppId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemWeChatMsgByKey(int id)
	    {
			string sql=@"delete from MemWeChatMsg where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemWeChatMsgDisabled()
        {
            string sql = @"delete from  MemWeChatMsg  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemWeChatMsgByIds(string ids)
        {
            string sql = @"Delete from MemWeChatMsg  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemWeChatMsgByIds(string ids)
        {
            string sql = @"Update   MemWeChatMsg set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemWeChatMsgEntity GetMemWeChatMsg(int id)
		{
			string sql=@"SELECT  [Id],[UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status]
							FROM
							dbo.[MemWeChatMsg] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemWeChatMsgEntity entity=new MemWeChatMsgEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.UnionId=StringUtils.GetDbString(reader["UnionId"]);
					entity.OpenId=StringUtils.GetDbString(reader["OpenId"]);
					entity.NickName=StringUtils.GetDbString(reader["NickName"]);
					entity.Sex=StringUtils.GetDbInt(reader["Sex"]);
					entity.Language=StringUtils.GetDbString(reader["Language"]);
					entity.City=StringUtils.GetDbString(reader["City"]);
					entity.Province=StringUtils.GetDbString(reader["Province"]);
					entity.Country=StringUtils.GetDbString(reader["Country"]);
					entity.HeadimgUrl=StringUtils.GetDbString(reader["HeadimgUrl"]);
					entity.AppId=StringUtils.GetDbString(reader["AppId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
				}
   		    }
            return entity;
		}
        public MemWeChatMsgEntity GetMsgByAppUnionId(string appid, string unionid)
        {
            string sql = @"SELECT  [Id],[UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status]
							FROM
							dbo.[MemWeChatMsg] WITH(NOLOCK)	
							WHERE [UnionId]=@UnionId and AppId=@AppId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@AppId", DbType.String, appid);
            db.AddInParameter(cmd, "@UnionId", DbType.String, unionid);
            MemWeChatMsgEntity entity = new MemWeChatMsgEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.UnionId = StringUtils.GetDbString(reader["UnionId"]);
                    entity.OpenId = StringUtils.GetDbString(reader["OpenId"]);
                    entity.NickName = StringUtils.GetDbString(reader["NickName"]);
                    entity.Sex = StringUtils.GetDbInt(reader["Sex"]);
                    entity.Language = StringUtils.GetDbString(reader["Language"]);
                    entity.City = StringUtils.GetDbString(reader["City"]);
                    entity.Province = StringUtils.GetDbString(reader["Province"]);
                    entity.Country = StringUtils.GetDbString(reader["Country"]);
                    entity.HeadimgUrl = StringUtils.GetDbString(reader["HeadimgUrl"]);
                    entity.AppId = StringUtils.GetDbString(reader["AppId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 微信登录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int WeChatLogin(MemWeChatMsgEntity entity)
        {
            string sql = @"
if EXISTS( SELECT 1 FROM MemWeChatMsg WHERE UnionId=@UnionId AND AppId=@AppId)
BEGIN
--如果没有获取过基本信息，此次获取的是基本信息，则完善信息
if(@Status=2 and EXISTS(SELECT 1 FROM MemWeChatMsg WHERE UnionId=@UnionId AND AppId=@AppId and Status=1))
begin 
UPDATE MemWeChatMsg SET  [NickName]=@NickName,[Sex]=@Sex,[Language]=@Language,[City]=@City,[Province]=@Province,
[Country]=@Country,[HeadimgUrl]=@HeadimgUrl, [Status]=@Status,loginNum=loginNum+1 WHERE UnionId=@UnionId AND AppId=@AppId
end
else
begin
UPDATE MemWeChatMsg SET loginNum=loginNum+1 WHERE UnionId=@UnionId AND AppId=@AppId
end
end
ELSE
BEGIN
insert into MemWeChatMsg( [UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status])VALUES
			            ( @UnionId,@OpenId,@NickName,@Sex,@Language,@City,@Province,@Country,@HeadimgUrl,@AppId,@Status) 
end";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@UnionId", DbType.String, entity.UnionId);
            db.AddInParameter(cmd, "@OpenId", DbType.String, entity.OpenId);
            db.AddInParameter(cmd, "@NickName", DbType.String, entity.NickName);
            db.AddInParameter(cmd, "@Sex", DbType.Int32, entity.Sex);
            db.AddInParameter(cmd, "@Language", DbType.String, entity.Language);
            db.AddInParameter(cmd, "@City", DbType.String, entity.City);
            db.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
            db.AddInParameter(cmd, "@Country", DbType.String, entity.Country);
            db.AddInParameter(cmd, "@HeadimgUrl", DbType.String, entity.HeadimgUrl);
            db.AddInParameter(cmd, "@AppId", DbType.String, entity.AppId);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<MemWeChatMsgEntity> GetMemWeChatMsgList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status] from dbo.[MemWeChatMsg] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemWeChatMsg] with (nolock) ";
            IList<MemWeChatMsgEntity> entityList = new List< MemWeChatMsgEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemWeChatMsgEntity entity=new MemWeChatMsgEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.UnionId=StringUtils.GetDbString(reader["UnionId"]);
					entity.OpenId=StringUtils.GetDbString(reader["OpenId"]);
					entity.NickName=StringUtils.GetDbString(reader["NickName"]);
					entity.Sex=StringUtils.GetDbInt(reader["Sex"]);
					entity.Language=StringUtils.GetDbString(reader["Language"]);
					entity.City=StringUtils.GetDbString(reader["City"]);
					entity.Province=StringUtils.GetDbString(reader["Province"]);
					entity.Country=StringUtils.GetDbString(reader["Country"]);
					entity.HeadimgUrl=StringUtils.GetDbString(reader["HeadimgUrl"]);
					entity.AppId=StringUtils.GetDbString(reader["AppId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
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
        public IList<MemWeChatMsgEntity> GetMemWeChatMsgAll()
        {

            string sql = @"SELECT    [Id],[UnionId],[OpenId],[NickName],[Sex],[Language],[City],[Province],[Country],[HeadimgUrl],[AppId],[Status] from dbo.[MemWeChatMsg] WITH(NOLOCK)	";
		    IList<MemWeChatMsgEntity> entityList = new List<MemWeChatMsgEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemWeChatMsgEntity entity=new MemWeChatMsgEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.UnionId=StringUtils.GetDbString(reader["UnionId"]);
					entity.OpenId=StringUtils.GetDbString(reader["OpenId"]);
					entity.NickName=StringUtils.GetDbString(reader["NickName"]);
					entity.Sex=StringUtils.GetDbInt(reader["Sex"]);
					entity.Language=StringUtils.GetDbString(reader["Language"]);
					entity.City=StringUtils.GetDbString(reader["City"]);
					entity.Province=StringUtils.GetDbString(reader["Province"]);
					entity.Country=StringUtils.GetDbString(reader["Country"]);
					entity.HeadimgUrl=StringUtils.GetDbString(reader["HeadimgUrl"]);
					entity.AppId=StringUtils.GetDbString(reader["AppId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
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
        public int  ExistNum(MemWeChatMsgEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemWeChatMsg] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (NickName=@NickName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (NickName=@NickName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@NickName", DbType.String, entity.NickName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
