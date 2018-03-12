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
功能描述：CGOrderSend表的数据访问类。
创建时间：2016/12/31 9:40:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
	/// <summary>
	/// CGOrderSendEntity的数据访问操作
	/// </summary>
	public partial class CGOrderSendDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderSendDA Instance
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
            internal static readonly CGOrderSendDA instance = new CGOrderSendDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGOrderSend，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGOrderSend">待插入的实体对象</param>
		public int AddCGOrderSend(CGOrderSendEntity entity)
		{
		   string sql= @"insert into CGOrderSend( [CGOrderCode],[CGMemId],[CreateTime],[HasRead],[ReadTime],[IsClose],[HasSendSMS])VALUES
			            ( @CGOrderCode,@CGMemId,@CreateTime,@HasRead,@ReadTime,0,0);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGOrderCode",  DbType.Int64,entity.CGOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@HasRead",  DbType.Int32,entity.HasRead);
			db.AddInParameter(cmd,"@ReadTime",  DbType.DateTime,entity.ReadTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGOrderSend">待更新的实体对象</param>
		public   int UpdateCGOrderSend(CGOrderSendEntity entity)
		{
			string sql=@" UPDATE dbo.[CGOrderSend] SET
                       [CGOrderCode]=@CGOrderCode,[CGMemId]=@CGMemId,[CreateTime]=@CreateTime,[HasRead]=@HasRead,[ReadTime]=@ReadTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGOrderCode",  DbType.Int64,entity.CGOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@HasRead",  DbType.Int32,entity.HasRead);
			db.AddInParameter(cmd,"@ReadTime",  DbType.DateTime,entity.ReadTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGOrderSendByKey(int id)
	    {
			string sql=@"delete from CGOrderSend where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderSendDisabled()
        {
            string sql = @"delete from  CGOrderSend  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 供应山报价， 
        /// </summary>
        /// <param name="cgcode"></param>
        /// <param name="price"></param>
        /// <param name="  fee"></param>
        /// <param name="_PriceStr"></param>
        /// <param name="cgmemid"></param>
        /// <returns></returns>
        public int OrderQuotationToOffer(long cgcode, decimal price, decimal transfee,string _PriceStr ,int cgmemid)
        {
            string sql = @"
   BEGIN TRAN 
 SELECT B.CGOrderCode ,
        @CGMemId AS CGMemId ,
        B.ProductId ,
        CAST(a.value2 AS DECIMAL(12, 3)) AS Price ,
        b.Price AS LimitPrice ,
        B.Num
 INTO   #temp
 FROM   dbo.fun_SplitStrToTable(@PriceStr, '|', '_') a
        INNER JOIN dbo.CGOrderDetail b WITH ( NOLOCK ) ON a.value1 = b.ProductId
                                                          AND b.CGOrderCode = @CGOrderCode
 WHERE  CAST(a.value2 AS DECIMAL(12, 3)) > 0
 IF ( SELECT    COUNT(1)
      FROM      #temp
      WHERE     LimitPrice >= Price
    ) = ( SELECT    COUNT(1)
          FROM      #temp
        ) 
    BEGIN
 

        IF NOT EXISTS ( SELECT  1
                        FROM    dbo.CGOrderOffer WITH ( NOLOCK )
                        WHERE   CGOrderCode = @CGOrderCode
                                AND CGMemId = @CGMemId ) 
            BEGIN

                IF ( SELECT COUNT(1)
                     FROM   #temp
                   ) = ( SELECT COUNT(1)
                         FROM   dbo.CGOrderDetail WITH ( NOLOCK )
                         WHERE  CGOrderCode = @CGOrderCode
                       )
                    AND ( SELECT    COUNT(1)
                          FROM      dbo.CGOrderSend WITH ( NOLOCK )
                          WHERE     CGOrderCode = @CGOrderCode
                                    AND CGMemId = @CGMemId
                        ) > 0 
                    BEGIN
                        DECLARE @offerid INT 
                        DECLARE @SumPrice DECIMAL(15, 3)
                        SELECT  @SumPrice = SUM(Price * Num)
                        FROM    #temp
 
                        INSERT  INTO dbo.CGOrderOffer
                                ( CGOrderCode ,
                                  CGMemId ,
                                  QuotedTransFee ,
                                  QuotedPrice ,
                                  QuotedTotalPrice ,
                                  Status ,
                                  CreateTime
                                        
                                )
                                SELECT  @CGOrderCode , -- CGOrderCode - bigint
                                        CGMemId , -- CGMemId - int
                                        0 , -- QuotedTransFee - decimal
                                        @SumPrice , -- QuotedPrice - decimal
                                        @SumPrice , -- QuotedTotalPrice - decimal 
                                        0 , -- Status - int
                                        GETDATE()
                                FROM    dbo.CGOrderSend WITH ( NOLOCK )
                                WHERE   CGOrderCode = @CGOrderCode
                                        AND CGMemId = @CGMemId

                        SET @offerid = SCOPE_IDENTITY() 
                        INSERT  INTO [JcCGOrder].[dbo].[CGOrderOfferSub]
                                ( [OfferId] ,
                                  [CGOrderCode] ,
                                  [CGMemId] ,
                                  [ProductId] ,
                                  [CGPrice] ,
                                  [Num] 
                                )
                                SELECT  @offerid ,
                                        CGOrderCode ,
                                        @CGMemId ,
                                        ProductId ,
                                        Price ,
                                        Num
                                FROM    #temp                             
                        DELETE  FROM dbo.CGOrderSend
                        WHERE   CGOrderCode = @CGOrderCode
                                AND CGMemId = @CGMemId
--前期不进行比价，直接做个比较派单
                    EXEC [dbo].[Proc_OrderTake]
                    select -1--报价成功
                    END
                               
            END  
            else
            begin

            select -2204--报价失败，已报价
            end 
    END   
    else
    begin
        select -2205--报价失败，超过限价
    end
 COMMIT TRAN";
            //已经报价，修改报价
//            else if  EXISTS(SELECT 1 FROM dbo.CGOrderOffer WITH(NOLOCK) WHERE CGOrderCode=@CGOrderCode AND CGMemId=@CGMemId and Status=@OrderWait)
//BEGIN
//SELECT B.CGOrderCode,@CGMemId AS  CGMemId,B.ProductId,value2 AS Price,B.Num INTO #temp2 FROM dbo.fun_SplitStrToTable(@PriceStr,'|','_') a
//inner JOIN dbo.CGOrderDetail b  WITH(NOLOCK)  ON a.value1=b.ProductId and b.CGOrderCode=@CGOrderCode
//UPDATE A SET CGPrice=B.Price FROM [dbo].[CGOrderOfferSub] A INNER JOIN #temp2 B ON A.CGOrderCode=B.CGOrderCode AND A.CGMemId=B.CGMemId AND A.ProductId=B.ProductId
// DECLARE @SumPrice2 decimal(15,3)
//select @SumPrice2=sum(CGPrice*Num) from   [dbo].[CGOrderOfferSub]   WITH(NOLOCK)  WHERE CGOrderCode=@CGOrderCode AND CGMemId=@CGMemId
// update CGOrderOffer set QuotedPrice=@SumPrice2,QuotedTotalPrice=@SumPrice2  WHERE CGOrderCode=@CGOrderCode AND CGMemId=@CGMemId
//end
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgcode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            db.AddInParameter(cmd, "@PriceStr", DbType.String, _PriceStr);
            db.AddInParameter(cmd, "@OrderWait", DbType.Int32, (int)CGOrderOfferStatus.OrderWait); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return (int)CommonStatus.Fail;
            return Convert.ToInt32(identity) ;
        }

        public bool HasQuotation(long cgcode, int cgmemid)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderOffer] WITH(NOLOCK)  WHERE CGOrderCode=@CGOrderCode AND CGMemId=@CGMemId ";

            DbCommand cmd = db.GetSqlStringCommand(sql); 

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgcode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return false;
            return Convert.ToInt32(identity)>0;
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderSendByIds(string ids)
        {
            string sql = @"Delete from CGOrderSend  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderSendByIds(string ids)
        {
            string sql = @"Update   CGOrderSend set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGOrderSendEntity GetCGOrderSend(int id)
		{
			string sql=@"SELECT  [Id],[CGOrderCode],[CGMemId],[CreateTime],[HasRead],[ReadTime]
							FROM
							dbo.[CGOrderSend] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGOrderSendEntity entity=new CGOrderSendEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGOrderCode=StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasRead=StringUtils.GetDbInt(reader["HasRead"]);
					entity.ReadTime=StringUtils.GetDbDateTime(reader["ReadTime"]);
				}
   		    }
            return entity;
		}
        public CGOrderSendEntity GetCGOrderSendByCode(long code, int memid)
        {
            string sql = @"SELECT  [Id],[CGOrderCode],[CGMemId],[CreateTime],[HasRead],[ReadTime]
							FROM
							dbo.[CGOrderSend] WITH(NOLOCK)	
							WHERE [CGOrderCode]=@CGOrderCode and CGMemId=@CGMemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, code);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            CGOrderSendEntity entity = new CGOrderSendEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.HasRead = StringUtils.GetDbInt(reader["HasRead"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param> 
        public   IList<VWCGOrderEntity> GetCGOrderSendList(int pagesize, int pageindex, ref  int recordCount,string keyword,int memid,int isclose)
		{  
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                where += " And (a.Code like @keyword  ) ";
            }
            if(memid!=-1)
            {
                where += " And b.CGMemId=@CGMemId ";
            }
            if (isclose != -1)
            {
                where += " And b.IsClose=@IsClose ";
            }
            string sql = @"SELECT   [ExpressCom],[Id],[Code],[Title],[SourceCode],[CreateTime],[ReceiptName],[ReceiptProvince],[ReceiptCity],[ReceiptTown],[ReceiptAddress],[ReceiptPhone], [Status],[SendTime],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,a.[ExpressCom],
						 a.[Id],a.[Code],a.[Title],a.[SourceCode],a.[CreateTime],c.[ReceiptName],c.[ReceiptProvince],c.[ReceiptCity],c.[ReceiptTown],c.[ReceiptAddress],
c.[ReceiptPhone], a.[Status],a.[SendTime],a.[Remark] from dbo.[CGOrder] a WITH(NOLOCK)	
inner join dbo.[CGOrderAddress] c  WITH(NOLOCK)
on a.Code=c.CGOrderCode
inner join  dbo.[CGOrderSend] b WITH(NOLOCK) on a.Code=b.CGOrderCode 
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrder] a WITH(NOLOCK)	
inner join dbo.[CGOrderAddress] c  WITH(NOLOCK)
on a.Code=c.CGOrderCode
inner join  dbo.[CGOrderSend] b WITH(NOLOCK) on a.Code=b.CGOrderCode  " + where;
            IList<VWCGOrderEntity> entityList = new List<VWCGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (memid != -1)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (isclose != -1)
            { 
                db.AddInParameter(cmd, "@IsClose", DbType.Int32, isclose);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderEntity entity = new VWCGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.ExpressCom = StringUtils.GetDbInt(reader["ExpressCom"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (isclose != -1)
            {
                db.AddInParameter(cmd, "@IsClose", DbType.Int32, isclose);
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
        public IList<VWCGOrderEntity> GetCGOrderOfferList(int pagesize, int pageindex, ref int recordCount, string keyword, int memid,int status)
        {
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                where += " And a.Title like @keyword ";
            }
            if (memid != -1)
            {
                where += " And b.CGMemId=@CGMemId ";
            }
            if (status > 0)
            {
                where += "And b.Status=@Status";
            }
            string sql = @"SELECT   [Id],[Code],[Title],[SourceCode],[CreateTime],[ReceiptName],[ReceiptProvince],[ReceiptCity],[ReceiptTown],[ReceiptAddress],[ReceiptPhone], [Status],[SendTime],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],a.[Code],a.[Title],a.[SourceCode],a.[CreateTime],c.[ReceiptName],c.[ReceiptProvince],c.[ReceiptCity],c.[ReceiptTown],c.[ReceiptAddress],
c.[ReceiptPhone], a.[Status],a.[SendTime],a.[Remark] from dbo.[CGOrder]  a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] c  WITH(NOLOCK)
on a.Code=c.CGOrderCode	
inner join  dbo.[CGOrderOffer] b WITH(NOLOCK) on a.Code=b.CGOrderCode 
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrder]  a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] c  WITH(NOLOCK)
on a.Code=c.CGOrderCode	 inner join  dbo.[CGOrderOffer] b WITH(NOLOCK) on a.Code=b.CGOrderCode  " + where;
            IList<VWCGOrderEntity> entityList = new List<VWCGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderEntity entity = new VWCGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
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
        public IList<VWCGOrderEntity> GetCGOrderTakeList(int pagesize, int pageindex, ref int recordCount, string keyword, int memid)
        {
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(keyword))
            {
                where += " And a.Title like @keyword ";
            }
            if (memid != -1)
            {
                where += " And b.CGMemId=@CGMemId ";
            }
             
            string sql = @"SELECT   [Id],[Code],[Title],[SourceCode],[CreateTime],[ReceiptName],[ReceiptProvince],[ReceiptCity],[ReceiptTown],[ReceiptAddress],[ReceiptPhone], [Status], [SendTime],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],a.[Code],a.[Title],a.[SourceCode],a.[CreateTime],c.[ReceiptName],c.[ReceiptProvince],c.[ReceiptCity],c.[ReceiptTown],c.[ReceiptAddress],
c.[ReceiptPhone], a.[Status],a.[SendTime],a.[Remark] from dbo.[CGOrder]  a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] c  WITH(NOLOCK)
on a.Code=c.CGOrderCode	
inner join  dbo.[CGOrderTake] b WITH(NOLOCK) on a.Code=b.CGOrderCode 
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrder] a WITH(NOLOCK) inner join  dbo.[CGOrderTake] b WITH(NOLOCK) on a.Code=b.CGOrderCode  " + where;
            IList<VWCGOrderEntity> entityList = new List<VWCGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderEntity entity = new VWCGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
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
        public IList<CGOrderSendEntity> GetCGOrderSendAll()
        {

            string sql = @"SELECT    [Id],[CGOrderCode],[CGMemId],[CreateTime],[HasRead],[ReadTime] from dbo.[CGOrderSend] WITH(NOLOCK)	";
		    IList<CGOrderSendEntity> entityList = new List<CGOrderSendEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGOrderSendEntity entity=new CGOrderSendEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGOrderCode=StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasRead=StringUtils.GetDbInt(reader["HasRead"]);
					entity.ReadTime=StringUtils.GetDbDateTime(reader["ReadTime"]);
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
        public int  ExistNum(CGOrderSendEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderSend] WITH(NOLOCK) ";
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
     
		 
        /// <summary>
        /// 人工派单给供应商
        /// </summary>
        /// <param name="cgordercode"></param>
        /// <param name="cgmemid"></param>
        /// <returns></returns>
        public int SendCGOrderToCGMem(long cgordercode, int cgmemid,int memid)
        {
            string sql = @"
                        IF  (SELECT COUNT(1) FROM DBO.CGOrderSend WHERE CGOrderCode=@CGOrderCode and IsClose=0 )=0
                        BEGIN
                            BEGIN TRAN OK
                            BEGIN 
                                if (SELECT COUNT(1) FROM DBO.CGOrderSend WHERE CGOrderCode=@CGOrderCode and CGMemId=@CGMemId )>0
                                begin
                                    update CGOrderSend set IsClose=0,HasSendSMS=0 where CGOrderCode=@CGOrderCode and CGMemId=@CGMemId
                                    Update dbo.CGOrder set Status = @Status,OfferNum=1,SendTime=GETDATE()  where Code = @CGOrderCode AND  Status=@UnderLineStatus;
                                end
                                else
                                begin
                                    INSERT INTO CGOrderSend( [CGOrderCode],[CGMemId],[CreateTime], [HasRead],[IsClose],[HasSendSMS],CreateManId)  
                                    SELECT Code,@CGMemId,GETDATE(), 0,0,0,@ManId from dbo.CGOrder WHERE Code=@CGOrderCode AND  Status=@UnderLineStatus;

                                    Update dbo.CGOrder set Status = @Status,OfferNum=1,SendTime=GETDATE() where Code = @CGOrderCode AND  Status=@UnderLineStatus;
                                  
                                END
                            END
                            if(@@ROWCOUNT>0)
                            begin
                                SELECT 1;
                            end
                            else
                            begin    
                                SELECT 0;
                            end 
                            IF(@@ERROR<>0)
                            BEGIN
                                ROLLBACK TRAN OK;
                                SELECT 0; 
                            END
                            ELSE 
                            BEGIN
                                COMMIT TRAN OK; 
                            END
                        END
 
                         ";

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ManId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgordercode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            db.AddInParameter(cmd, "@UnderLineStatus", DbType.Int32, (int)CGOrderStatus.Line); 
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)CGOrderStatus.WaitBJ);

            object _result= db.ExecuteScalar(cmd);
            if (_result == null || _result == DBNull.Value) return 0;
            return (int)_result;
            
        }

        /// <summary>
        /// 获取待报价
        /// </summary>
        /// <returns></returns>
        public int GetCGOrderSendOfWaitBJByMemId(int memId)
        {
            string sql = @"SELECT COUNT(1) FROM [JcCGOrder].[dbo].[CGOrderSend] where [CGMemId]=@CGMemId and [IsClose]=0";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memId);
            object _result = db.ExecuteScalar(cmd);
            if (_result == null || _result ==DBNull.Value) return 0;
            return (int)_result;
        }


        #endregion
        #endregion
    }
}
