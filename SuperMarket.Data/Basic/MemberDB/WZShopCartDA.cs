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
功能描述：WZShopCart表的数据访问类。
创建时间：2016/9/18 12:34:45
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// WZShopCartEntity的数据访问操作
	/// </summary>
	public partial class WZShopCartDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WZShopCartDA Instance
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
            internal static readonly WZShopCartDA instance = new WZShopCartDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WZShopCart，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="wZShopCart">待插入的实体对象</param>
		public int AddWZShopCart(WZShopCartEntity entity)
		{
		   string sql= @"insert into WZShopCart( [MemId],[BuyDate],[EndDate],[AddDate],[CookieValue],[CookieValueXuQiu],[CookieId])VALUES
			            ( @MemId,@BuyDate,@EndDate,@AddDate,@CookieValue,@CookieValueXuQiu,@CookieId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@BuyDate",  DbType.DateTime,entity.BuyDate);
			db.AddInParameter(cmd,"@EndDate",  DbType.DateTime,entity.EndDate);
			db.AddInParameter(cmd,"@AddDate",  DbType.DateTime,entity.AddDate);
			db.AddInParameter(cmd,"@CookieValue",  DbType.String,entity.CookieValue);
			db.AddInParameter(cmd, "@CookieValueXuQiu",  DbType.String,entity.CookieValueXuQiu);
            db.AddInParameter(cmd,"@CookieId",  DbType.String,entity.CookieId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="wZShopCart">待更新的实体对象</param>
		public   int UpdateWZShopCart(WZShopCartEntity entity)
		{
			string sql= @" UPDATE dbo.[WZShopCart] SET [BuyDate]=@BuyDate,[EndDate]=@EndDate,[AddDate]=@AddDate,[CookieValue]=@CookieValue, [CookieId]=@CookieId
                       WHERE [MemId]=@MemId";
		    DbCommand cmd = db.GetSqlStringCommand(sql); 
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@BuyDate",  DbType.DateTime,entity.BuyDate);
			db.AddInParameter(cmd,"@EndDate",  DbType.DateTime,entity.EndDate);
			db.AddInParameter(cmd,"@AddDate",  DbType.DateTime,entity.AddDate);
			db.AddInParameter(cmd,"@CookieValue",  DbType.String,entity.CookieValue); 
            db.AddInParameter(cmd,"@CookieId",  DbType.String,entity.CookieId);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int UpdateWZShopCartXuQiu(WZShopCartEntity entity)
             
        {
            string sql = @" UPDATE dbo.[WZShopCart] SET [BuyDate]=@BuyDate,[EndDate]=@EndDate,[CookieValueXuQiu]=@CookieValueXuQiu 
                       WHERE [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@BuyDate", DbType.DateTime, entity.BuyDate);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, entity.EndDate); 
            db.AddInParameter(cmd, "@CookieValueXuQiu", DbType.String, entity.CookieValueXuQiu); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int  DeleteWZShopCartByKey(int id)
	    {
			string sql=@"delete from WZShopCart where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWZShopCartDisabled()
        {
            string sql = @"delete from  WZShopCart  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWZShopCartByIds(string ids)
        {
            string sql = @"Delete from WZShopCart  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWZShopCartByIds(string ids)
        {
            string sql = @"Update   WZShopCart set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WZShopCartEntity GetWZShopCart(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[BuyDate],[EndDate],[AddDate],[CookieValue],[CookieId]
							FROM
							dbo.[WZShopCart] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WZShopCartEntity entity=new WZShopCartEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.BuyDate=StringUtils.GetDbDateTime(reader["BuyDate"]);
					entity.EndDate=StringUtils.GetDbDateTime(reader["EndDate"]);
					entity.AddDate=StringUtils.GetDbDateTime(reader["AddDate"]);
					entity.CookieValue=StringUtils.GetDbString(reader["CookieValue"]);
					entity.CookieId=StringUtils.GetDbString(reader["CookieId"]);
				}
   		    }
            return entity;
		}
        public WZShopCartEntity GetCartCookie(int memberid)
        {
            string sql = @"SELECT top 1  [Id],[MemId],[BuyDate],[EndDate],[AddDate],[CookieValue],[CookieValueXuQiu],[CookieId]
							FROM
							dbo.[WZShopCart] WITH(NOLOCK)	
							WHERE [MemId]=@MemId order by id desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memberid);
            WZShopCartEntity entity = new WZShopCartEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.BuyDate = StringUtils.GetDbDateTime(reader["BuyDate"]);
                    entity.EndDate = StringUtils.GetDbDateTime(reader["EndDate"]);
                    entity.AddDate = StringUtils.GetDbDateTime(reader["AddDate"]);
                    entity.CookieValue = StringUtils.GetDbString(reader["CookieValue"]);
                    entity.CookieValueXuQiu = StringUtils.GetDbString(reader["CookieValueXuQiu"]);
                    entity.CookieId = StringUtils.GetDbString(reader["CookieId"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<WZShopCartEntity> GetWZShopCartList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[BuyDate],[EndDate],[AddDate],[CookieValue],[CookieId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[BuyDate],[EndDate],[AddDate],[CookieValue],[CookieId] from dbo.[WZShopCart] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WZShopCart] with (nolock) ";
            IList<WZShopCartEntity> entityList = new List< WZShopCartEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WZShopCartEntity entity=new WZShopCartEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.BuyDate=StringUtils.GetDbDateTime(reader["BuyDate"]);
					entity.EndDate=StringUtils.GetDbDateTime(reader["EndDate"]);
					entity.AddDate=StringUtils.GetDbDateTime(reader["AddDate"]);
					entity.CookieValue=StringUtils.GetDbString(reader["CookieValue"]);
					entity.CookieId=StringUtils.GetDbString(reader["CookieId"]);
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
        public IList<WZShopCartEntity> GetWZShopCartAll()
        {

            string sql = @"SELECT    [Id],[MemId],[BuyDate],[EndDate],[AddDate],[CookieValue],[CookieId] from dbo.[WZShopCart] WITH(NOLOCK)	";
		    IList<WZShopCartEntity> entityList = new List<WZShopCartEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WZShopCartEntity entity=new WZShopCartEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.BuyDate=StringUtils.GetDbDateTime(reader["BuyDate"]);
					entity.EndDate=StringUtils.GetDbDateTime(reader["EndDate"]);
					entity.AddDate=StringUtils.GetDbDateTime(reader["AddDate"]);
					entity.CookieValue=StringUtils.GetDbString(reader["CookieValue"]);
					entity.CookieId=StringUtils.GetDbString(reader["CookieId"]);
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
        public int  ExistNum(WZShopCartEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @" Select count(1) from dbo.[WZShopCart] WITH(NOLOCK) WHERE MemId=@MemId ";
        
            DbCommand cmd = db.GetSqlStringCommand(sql); 
             db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
          
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
