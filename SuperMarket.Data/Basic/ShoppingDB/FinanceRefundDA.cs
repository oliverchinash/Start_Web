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
功能描述：FinanceRefund表的数据访问类。
创建时间：2017/1/16 12:07:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// FinanceRefundEntity的数据访问操作
	/// </summary>
	public partial class FinanceRefundDA: BaseSuperMarketDB
    {
        #region 实例化
        public static FinanceRefundDA Instance
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
            internal static readonly FinanceRefundDA instance = new FinanceRefundDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表FinanceRefund，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="financeRefund">待插入的实体对象</param>
		public int AddFinanceRefund(FinanceRefundEntity entity)
		{
		   string sql= @"insert into FinanceRefund( [ReturnXSId],[OrderCode],[PayTradeNo],[RebackFee],[Description],[CreateTime],[Status],[FinanceDealTime],[DealManId])VALUES
			            ( @ReturnXSId,@OrderCode,@PayTradeNo,@RebackFee,@Description,@CreateTime,@Status,@FinanceDealTime,@DealManId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@PayTradeNo",  DbType.String,entity.PayTradeNo);
			db.AddInParameter(cmd,"@RebackFee",  DbType.Decimal,entity.RebackFee);
			db.AddInParameter(cmd,"@Description",  DbType.String,entity.Description);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@FinanceDealTime",  DbType.DateTime,entity.FinanceDealTime);
			db.AddInParameter(cmd,"@DealManId",  DbType.Int32,entity.DealManId);
			db.AddInParameter(cmd, "@ReturnXSId",  DbType.Int32,entity.ReturnXSId);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="financeRefund">待更新的实体对象</param>
		public   int UpdateFinanceRefund(FinanceRefundEntity entity)
		{
			string sql= @" UPDATE dbo.[FinanceRefund] SET
                       [ReturnXSId]=@ReturnXSId,[OrderCode]=@OrderCode,[PayTradeNo]=@PayTradeNo,[RebackFee]=@RebackFee,[Description]=@Description,[CreateTime]=@CreateTime,[Status]=@Status,[FinanceDealTime]=@FinanceDealTime,[DealManId]=@DealManId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@PayTradeNo",  DbType.String,entity.PayTradeNo);
			db.AddInParameter(cmd,"@RebackFee",  DbType.Decimal,entity.RebackFee);
			db.AddInParameter(cmd,"@Description",  DbType.String,entity.Description);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@FinanceDealTime",  DbType.DateTime,entity.FinanceDealTime);
			db.AddInParameter(cmd,"@DealManId",  DbType.Int32,entity.DealManId);
			db.AddInParameter(cmd, "@ReturnXSId",  DbType.Int32,entity.ReturnXSId);
            return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteFinanceRefundByKey(int id)
	    {
			string sql=@"delete from FinanceRefund where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}

        public int AddFinanceRefund(int returnXSId)
        {
            string sql = @"INSERT INTO [JcShoppingDB].[dbo].[FinanceRefund]
								([OrderCode]
								,[PayTradeNo]
								,[RebackFee]
								,[ActRebackFee]
								,[Description]
								,[ReturnXSId]
								,[CreateTime]
								,[Status] )	
						SELECT a.ReturnOrderCode,b.tradeno,a.Price*a.Num,0,'退款原因:退货',a.Id,GETDATE(),0 
						FROM DBO.ReturnXS A INNER JOIN DBO.PayAliResultOrder B ON A.ReturnOrderCode=B.outtradeno
						WHERE a.Id= @returnXSId;
                        SELECT SCOPE_IDENTITY() 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@returnXSId", DbType.Int32, returnXSId);
            return db.ExecuteNonQuery(cmd);
        }

         /// <summary>
         /// 删除失效记录，默认保留2个月
         /// </summary>
         /// <returns></returns>
        public int DeleteFinanceRefundDisabled()
        {
            string sql = @"delete from  FinanceRefund  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteFinanceRefundByIds(string ids)
        {
            string sql = @"Delete from FinanceRefund  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableFinanceRefundByIds(string ids)
        {
            string sql = @"Update   FinanceRefund set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   FinanceRefundEntity GetFinanceRefund(int id)
		{
			string sql= @"SELECT  [Id],[OrderCode],[PayTradeNo],[RebackFee],ActRebackFee,ReturnXSId,[Description],[CreateTime],[Status],[FinanceDealTime],[DealManId]
				,ReBackIntegral,IntegralStatus,ActReBackIntegral			FROM
							dbo.[FinanceRefund] WITH(NOLOCK)		
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		FinanceRefundEntity entity=new FinanceRefundEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.PayTradeNo=StringUtils.GetDbString(reader["PayTradeNo"]); 
                    entity.RebackFee=StringUtils.GetDbDecimal(reader["RebackFee"]);
					entity.Description=StringUtils.GetDbString(reader["Description"]);
					entity.ActRebackFee = StringUtils.GetDbDecimal(reader["ActRebackFee"]);
					entity.ReturnXSId = StringUtils.GetDbInt(reader["ReturnXSId"]);
                    entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ActReBackIntegral = StringUtils.GetDbInt(reader["ActReBackIntegral"]);
					entity.ReBackIntegral = StringUtils.GetDbInt(reader["ReBackIntegral"]);
					entity.IntegralStatus = StringUtils.GetDbInt(reader["IntegralStatus"]);
                    entity.FinanceDealTime=StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
					entity.DealManId=StringUtils.GetDbInt(reader["DealManId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<FinanceRefundEntity> GetFinanceRefundList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[OrderCode],[PayTradeNo],[RebackFee],[Description],[CreateTime],[Status],[FinanceDealTime],[DealManId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OrderCode],[PayTradeNo],[RebackFee],[Description],[CreateTime],[Status],[FinanceDealTime],[DealManId] from dbo.[FinanceRefund] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[FinanceRefund] with (nolock) ";
            IList<FinanceRefundEntity> entityList = new List< FinanceRefundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					FinanceRefundEntity entity=new FinanceRefundEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.PayTradeNo=StringUtils.GetDbString(reader["PayTradeNo"]);
					entity.RebackFee=StringUtils.GetDbDecimal(reader["RebackFee"]);
					entity.Description=StringUtils.GetDbString(reader["Description"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.FinanceDealTime=StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
					entity.DealManId=StringUtils.GetDbInt(reader["DealManId"]);
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

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWFinanceRefundEntity> GetVWFinanceRefundList(int pagesize, int pageindex, ref int recordCount, long ordercode)
        {
            string where = " where 1=1 ";
            if(ordercode>0)
            {
                where += " and a.OrderCode=@OrderCode ";
            }
            string sql = @"SELECT    [Id],[OrderCode],[PayTradeNo],[RebackFee],[Description],[CreateTime],[Status],[FinanceDealTime],[DealManId],
  PayPrice, TotalPrice, Integral,HasReBackFee,ActReBackIntegral,ActRebackFee
 FROM (SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],[OrderCode],[PayTradeNo],a.[RebackFee],[Description],a.[CreateTime],a.[Status],[FinanceDealTime],
						 [DealManId],c.totalfee AS PayPrice,b.TotalPrice,b.Integral,b.ReBackFee AS HasReBackFee,a.ActRebackFee,a.ActReBackIntegral from dbo.[FinanceRefund] A WITH(NOLOCK) INNER JOIN dbo.[Order] B WITH(NOLOCK) 
						 ON A.[OrderCode]=B.Code INNER JOIN dbo.PayAliResultOrder c ON b.Code=c.outtradeno
						" + where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[FinanceRefund] a with (nolock) "+ where;
            IList<VWFinanceRefundEntity> entityList = new List<VWFinanceRefundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (ordercode > 0)
            { 
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWFinanceRefundEntity entity = new VWFinanceRefundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.PayTradeNo = StringUtils.GetDbString(reader["PayTradeNo"]);
                    entity.RebackFee = StringUtils.GetDbDecimal(reader["RebackFee"]);
                    entity.HasReBackFee = StringUtils.GetDbDecimal(reader["HasReBackFee"]);
                    entity.ActRebackFee = StringUtils.GetDbDecimal(reader["ActRebackFee"]);
                    entity.ActReBackIntegral = StringUtils.GetDbDecimal(reader["ActReBackIntegral"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.OrderPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.Description = StringUtils.GetDbString(reader["Description"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.FinanceDealTime = StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
                    entity.DealManId = StringUtils.GetDbInt(reader["DealManId"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
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
        public IList<FinanceRefundEntity> GetFinanceRefundAll()
        {

            string sql = @"SELECT    [Id],[OrderCode],[PayTradeNo],[RebackFee],[Description],[CreateTime],[Status],[FinanceDealTime],[DealManId] from dbo.[FinanceRefund] WITH(NOLOCK)	";
		    IList<FinanceRefundEntity> entityList = new List<FinanceRefundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   FinanceRefundEntity entity=new FinanceRefundEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.PayTradeNo=StringUtils.GetDbString(reader["PayTradeNo"]);
					entity.RebackFee=StringUtils.GetDbDecimal(reader["RebackFee"]);
					entity.Description=StringUtils.GetDbString(reader["Description"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.FinanceDealTime=StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
					entity.DealManId=StringUtils.GetDbInt(reader["DealManId"]);
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
        public int  ExistNum(FinanceRefundEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[FinanceRefund] WITH(NOLOCK) ";
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
