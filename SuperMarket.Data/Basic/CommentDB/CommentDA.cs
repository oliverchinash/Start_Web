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
功能描述：Comment表的数据访问类。
创建时间：2016/9/8 13:30:15
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CommentDB
{
	/// <summary>
	/// CommentEntity的数据访问操作
	/// </summary>
	public partial class CommentDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CommentDA Instance
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
            internal static readonly CommentDA instance = new CommentDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Comment，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="comment">待插入的实体对象</param>
		public int AddComment(CommentEntity entity)
		{
		   string sql= @"insert into Comment( [ProductId], [OrderCode],[OrderDetailId],[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent])VALUES
			            ( @ProductId, @OrderCode,@OrderDetailId,@ProductName,@ProductStar,@SeviceStar,@TrafficStar,@CommentContent,@ClientType,@HasPic,@IpAddress,@MemId,@CreateTime,@Status,@CheckManId,@CheckTime,@ReplyContent);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId); 
            db.AddInParameter(cmd, "@OrderCode", DbType.String, entity.OrderCode);
            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, entity.OrderDetailId);
            db.AddInParameter(cmd,"@ProductName",  DbType.String,entity.ProductName);
			db.AddInParameter(cmd,"@ProductStar",  DbType.Int32,entity.ProductStar);
			db.AddInParameter(cmd,"@SeviceStar",  DbType.Int32,entity.SeviceStar);
			db.AddInParameter(cmd,"@TrafficStar",  DbType.Int32,entity.TrafficStar);
			db.AddInParameter(cmd,"@CommentContent",  DbType.String,entity.CommentContent);
			db.AddInParameter(cmd,"@ClientType",  DbType.Int32,entity.ClientType);
			db.AddInParameter(cmd,"@HasPic",  DbType.Int32,entity.HasPic);
			db.AddInParameter(cmd,"@IpAddress",  DbType.String,entity.IpAddress);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
			db.AddInParameter(cmd,"@ReplyContent",  DbType.String,entity.ReplyContent);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public int CreateCommentProc(CommentEntity entity)
        {
 
            string sql = @"EXEC Proc_CommentCreate  @ProductId  , @ProductDetailId  ,@ProductName  ,
    @OrderCode  ,
    @OrderDetailId ,
    @OrderDate ,
    @ProductStar ,
    @SeviceStar  ,
    @PackStar  ,
    @TrafficStar  ,
    @CommentContent  ,
    @PicUrlContent ,
    @ClientType ,
    @IpAddress   ,
    @MemId   ,
    @MemCode  ,
    @MemLevelName  ,
    @PicUrl  ,
    @PicSuffix  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@ProductDetailId", DbType.Int32, entity.ProductDetailId);
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, entity.OrderCode);
            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, entity.OrderDetailId);
            db.AddInParameter(cmd, "@OrderDate", DbType.DateTime, entity.OrderDate);
            db.AddInParameter(cmd, "@ProductStar", DbType.Int32, entity.ProductStar);
            db.AddInParameter(cmd, "@SeviceStar", DbType.Int32, entity.SeviceStar);
            db.AddInParameter(cmd, "@PackStar", DbType.Int32, entity.PackStar);
            db.AddInParameter(cmd, "@TrafficStar", DbType.Int32, entity.TrafficStar);
            db.AddInParameter(cmd, "@CommentContent", DbType.String, entity.CommentContent);
            db.AddInParameter(cmd, "@PicUrlContent", DbType.String, entity.PicUrlContent);
            db.AddInParameter(cmd, "@ClientType", DbType.Int32, entity.ClientType);
            db.AddInParameter(cmd, "@IpAddress", DbType.String, entity.IpAddress);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@MemCode", DbType.String, entity.MemCode);
            db.AddInParameter(cmd, "@MemLevelName", DbType.String, entity.MemLevelName);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            db.AddInParameter(cmd, "@PicSuffix", DbType.String, entity.PicSuffix);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
 
    }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="comment">待更新的实体对象</param>
        public   int UpdateComment(CommentEntity entity)
		{
			string sql= @" UPDATE dbo.[Comment] SET
                       [ProductId]=@ProductId, [OrderCode]=@OrderCode,[OrderDetailId]=@OrderDetailId,[ProductName]=@ProductName,[ProductStar]=@ProductStar,[SeviceStar]=@SeviceStar,[TrafficStar]=@TrafficStar,[CommentContent]=@CommentContent,[ClientType]=@ClientType,[HasPic]=@HasPic,[IpAddress]=@IpAddress,[MemId]=@MemId,[CreateTime]=@CreateTime,[Status]=@Status,[CheckManId]=@CheckManId,[CheckTime]=@CheckTime,[ReplyContent]=@ReplyContent
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
            db.AddInParameter(cmd, "@OrderCode", DbType.String, entity.OrderCode);
            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, entity.OrderDetailId); 
			db.AddInParameter(cmd,"@ProductName",  DbType.String,entity.ProductName);
			db.AddInParameter(cmd,"@ProductStar",  DbType.Int32,entity.ProductStar);
			db.AddInParameter(cmd,"@SeviceStar",  DbType.Int32,entity.SeviceStar);
			db.AddInParameter(cmd,"@TrafficStar",  DbType.Int32,entity.TrafficStar);
			db.AddInParameter(cmd,"@CommentContent",  DbType.String,entity.CommentContent);
			db.AddInParameter(cmd,"@ClientType",  DbType.Int32,entity.ClientType);
			db.AddInParameter(cmd,"@HasPic",  DbType.Int32,entity.HasPic);
			db.AddInParameter(cmd,"@IpAddress",  DbType.String,entity.IpAddress);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
			db.AddInParameter(cmd,"@ReplyContent",  DbType.String,entity.ReplyContent);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCommentByKey(int id)
	    {
			string sql=@"delete from Comment where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}

        /// <summary>
        /// 更新评论审核状态
        /// </summary>
        /// <returns></returns>
        public int UpdateCommentStatus(int id,int status)
        {
            string sql = @"Update Comment set Status=@Status Where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32,id);
            db.AddInParameter(cmd, "@Status",DbType.Int32,status);
            return db.ExecuteNonQuery(cmd);
        }
         /// <summary>
         /// 删除失效记录，默认保留2个月
         /// </summary>
         /// <returns></returns>
        public int DeleteCommentDisabled()
        {
            string sql = @"delete from  Comment  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCommentByIds(string ids)
        {
            string sql = @"Delete from Comment  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCommentByIds(string ids)
        {
            string sql = @"Update   Comment set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CommentEntity GetComment(int id)
		{
			string sql= @"SELECT  [Id],[ProductId], [OrderCode],[OrderDetailId],[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent]
							FROM
							dbo.[Comment] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CommentEntity entity=new CommentEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]); 
                    entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
					entity.ProductStar=StringUtils.GetDbInt(reader["ProductStar"]);
					entity.SeviceStar=StringUtils.GetDbInt(reader["SeviceStar"]);
					entity.TrafficStar=StringUtils.GetDbInt(reader["TrafficStar"]);
					entity.CommentContent=StringUtils.GetDbString(reader["CommentContent"]);
					entity.ClientType=StringUtils.GetDbInt(reader["ClientType"]);
					entity.HasPic=StringUtils.GetDbInt(reader["HasPic"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.ReplyContent=StringUtils.GetDbString(reader["ReplyContent"]);
				}
   		    }
            return entity;
		}

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CommentEntity GetCommentByOrderDetailId(int orderdetailid)
        {
            string sql = @"SELECT [PicUrl],[PicSuffix] ,[PackStar],[Id],[ProductId], [OrderCode],[OrderDetailId],[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent]
							FROM
							dbo.[Comment] WITH(NOLOCK)	
							WHERE [OrderDetailId]=@OrderDetailId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, orderdetailid);
            CommentEntity entity = new CommentEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.PackStar = StringUtils.GetDbInt(reader["PackStar"]);
                    entity.ProductStar = StringUtils.GetDbInt(reader["ProductStar"]);
                    entity.SeviceStar = StringUtils.GetDbInt(reader["SeviceStar"]);
                    entity.TrafficStar = StringUtils.GetDbInt(reader["TrafficStar"]);
                    entity.CommentContent = StringUtils.GetDbString(reader["CommentContent"]);
                    entity.ClientType = StringUtils.GetDbInt(reader["ClientType"]);
                    entity.HasPic = StringUtils.GetDbInt(reader["HasPic"]);
                    entity.IpAddress = StringUtils.GetDbString(reader["IpAddress"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.ReplyContent = StringUtils.GetDbString(reader["ReplyContent"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]); 
                }
            }
            return entity;
        }


        /// <summary>
        /// 依据订单详情号获取评论详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public IList<CommentEntity> GetCommentByODId(int id,int memid)
        { 
            string sql = @"SELECT  [Id],[ProductId],[OrderCode],[OrderDetailId],PicUrl,PicSuffix,[ProductName],[ProductStar],[SeviceStar],[TrafficStar],PackStar,[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent]
							FROM
							dbo.[Comment] WITH(NOLOCK)	
							WHERE [OrderDetailId]=@OrderDetailId and MemId=@MenId order by id";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            IList<CommentEntity> entityList = new List<CommentEntity>(); 
            db.AddInParameter(cmd, "@MenId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, id);
            
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CommentEntity entity = new CommentEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]); 
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductStar = StringUtils.GetDbInt(reader["ProductStar"]);
                    entity.SeviceStar = StringUtils.GetDbInt(reader["SeviceStar"]);
                    entity.PackStar = StringUtils.GetDbInt(reader["PackStar"]);
                    entity.TrafficStar = StringUtils.GetDbInt(reader["TrafficStar"]);
                    entity.CommentContent = StringUtils.GetDbString(reader["CommentContent"]);
                    entity.ClientType = StringUtils.GetDbInt(reader["ClientType"]);
                    entity.HasPic = StringUtils.GetDbInt(reader["HasPic"]);
                    entity.IpAddress = StringUtils.GetDbString(reader["IpAddress"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.ReplyContent = StringUtils.GetDbString(reader["ReplyContent"]); 
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]); 
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);  
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CommentEntity> GetCommentList(int pagesize, int pageindex, ref int recordCount, int productid,int memid,int isadd)
        {
            string where = "where 1=1 ";
            if (productid != -1)
            {
                where += " and ProductId=@ProductId";
            }
            if (memid != -1)
            {
                where += " and MemId=@MemId";
            }
            if (isadd != -1)
            {
                where += " and IsAdd=@IsAdd";
            }
            string sql = @"SELECT [PicUrl],[PicSuffix],[Id],[ProductId],[ProductDetailId], [OrderCode],[OrderDetailId],[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [PicUrl],[PicSuffix],[Id],[ProductId],[ProductDetailId], [OrderCode],[OrderDetailId],[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent] from dbo.[Comment]  
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Comment] with (nolock) " + where;
            IList<CommentEntity> entityList = new List<CommentEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (productid != -1)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (isadd != -1)
            {
                db.AddInParameter(cmd, "@IsAdd", DbType.Int32, isadd); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CommentEntity entity = new CommentEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]); 
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductStar = StringUtils.GetDbInt(reader["ProductStar"]);
                    entity.SeviceStar = StringUtils.GetDbInt(reader["SeviceStar"]);
                    entity.TrafficStar = StringUtils.GetDbInt(reader["TrafficStar"]);
                    entity.CommentContent = StringUtils.GetDbString(reader["CommentContent"]);
                    entity.ClientType = StringUtils.GetDbInt(reader["ClientType"]);
                    entity.HasPic = StringUtils.GetDbInt(reader["HasPic"]);
                    entity.IpAddress = StringUtils.GetDbString(reader["IpAddress"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.ReplyContent = StringUtils.GetDbString(reader["ReplyContent"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (productid != -1)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (isadd != -1)
            {
                db.AddInParameter(cmd, "@IsAdd", DbType.Int32, isadd);
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
        public IList<CommentEntity> GetListByProductId(int pagesize, int pageindex, ref int recordCount, int productid)
        {
            string where = "where 1=1 and Status=1 and IsAdd=0 ";
            if (productid != -1)
            {
                where += " and ProductId=@ProductId ";
            }
            string sql = @"SELECT   [Id],[ProductId], [OrderCode],[OrderDetailId],OrderDate,[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],MemCode,MemLevelName,[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductId], [OrderCode],[OrderDetailId],OrderDate,[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],MemCode,MemLevelName,[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent] from dbo.[Comment] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Comment] with (nolock) " + where;
            IList<CommentEntity> entityList = new List<CommentEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (productid != -1)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CommentEntity entity = new CommentEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]); 
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.OrderDate = StringUtils.GetDateTime(reader["OrderDate"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductStar = StringUtils.GetDbInt(reader["ProductStar"]);
                    entity.SeviceStar = StringUtils.GetDbInt(reader["SeviceStar"]);
                    entity.TrafficStar = StringUtils.GetDbInt(reader["TrafficStar"]);
                    entity.CommentContent = StringUtils.GetDbString(reader["CommentContent"]);  
                    entity.ClientType = StringUtils.GetDbInt(reader["ClientType"]);
                    entity.HasPic = StringUtils.GetDbInt(reader["HasPic"]);
                    entity.IpAddress = StringUtils.GetDbString(reader["IpAddress"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemCode = StringUtils.GetDbString(reader["MemCode"]); 
                    entity.MemLevelName = StringUtils.GetDbString(reader["MemLevelName"]);  
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.ReplyContent = StringUtils.GetDbString(reader["ReplyContent"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (productid != -1)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
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
        public IList<CommentEntity> GetCommentAll()
        {

            string sql = @"SELECT [Id],[ProductId], [OrderCode],[OrderDetailId],[ProductName],[ProductStar],[SeviceStar],[TrafficStar],[CommentContent],[ClientType],[HasPic],[IpAddress],[MemId],[CreateTime],[Status],[CheckManId],[CheckTime],[ReplyContent] from dbo.[Comment] WITH(NOLOCK)	";
		    IList<CommentEntity> entityList = new List<CommentEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CommentEntity entity=new CommentEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]); 
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
					entity.ProductStar=StringUtils.GetDbInt(reader["ProductStar"]);
					entity.SeviceStar=StringUtils.GetDbInt(reader["SeviceStar"]);
					entity.TrafficStar=StringUtils.GetDbInt(reader["TrafficStar"]);
					entity.CommentContent=StringUtils.GetDbString(reader["CommentContent"]);
					entity.ClientType=StringUtils.GetDbInt(reader["ClientType"]);
					entity.HasPic=StringUtils.GetDbInt(reader["HasPic"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.ReplyContent=StringUtils.GetDbString(reader["ReplyContent"]);
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
        public int  ExistNum(CommentEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Comment] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "[ProductId]=@ProductId";//即针对一个Id一个StyleId只能有一条评论
            }
            else
            {
                where = where + "[Id]<>@Id and [ProductId]=@ProductId";
            } 
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId); 
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
