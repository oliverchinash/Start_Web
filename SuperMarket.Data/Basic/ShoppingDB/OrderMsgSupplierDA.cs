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
功能描述：OrderMsgStore表的数据访问类。
创建时间：2016/8/4 11:02:01
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// OrderMsgStoreEntity的数据访问操作
	/// </summary>
	public partial class OrderMsgStoreDA: BaseSuperMarketDB
    {
        #region 实例化
        public static OrderMsgStoreDA Instance
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
            internal static readonly OrderMsgStoreDA instance = new OrderMsgStoreDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表OrderMsgStore，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderMsgStore">待插入的实体对象</param>
		public int AddOrderMsgStore(OrderMsgStoreEntity entity)
		{
		   string sql=@"insert into OrderMsgStore( [SellerId],[TotallNum],[WaitDealNum],[WaitDeliverNum],[ReturnNum],[ExchangeNum],[FinishedNum],[RejectNum])VALUES
			            ( @SellerId,@TotallNum,@WaitDealNum,@WaitDeliverNum,@ReturnNum,@ExchangeNum,@FinishedNum,@RejectNum);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@SellerId",  DbType.Int32,entity.SellerId);
			db.AddInParameter(cmd,"@TotallNum",  DbType.Int32,entity.TotallNum);
			db.AddInParameter(cmd,"@WaitDealNum",  DbType.Int32,entity.WaitDealNum);
			db.AddInParameter(cmd,"@WaitDeliverNum",  DbType.Int32,entity.WaitDeliverNum);
			db.AddInParameter(cmd,"@ReturnNum",  DbType.Int32,entity.ReturnNum);
			db.AddInParameter(cmd,"@ExchangeNum",  DbType.Int32,entity.ExchangeNum);
			db.AddInParameter(cmd,"@FinishedNum",  DbType.Int32,entity.FinishedNum);
			db.AddInParameter(cmd,"@RejectNum",  DbType.Int32,entity.RejectNum);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderMsgStore">待更新的实体对象</param>
		public   int UpdateOrderMsgStore(OrderMsgStoreEntity entity)
		{
			string sql=@" UPDATE dbo.[OrderMsgStore] SET
                       [SellerId]=@SellerId,[TotallNum]=@TotallNum,[WaitDealNum]=@WaitDealNum,[WaitDeliverNum]=@WaitDeliverNum,[ReturnNum]=@ReturnNum,[ExchangeNum]=@ExchangeNum,[FinishedNum]=@FinishedNum,[RejectNum]=@RejectNum
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@SellerId",  DbType.Int32,entity.SellerId);
			db.AddInParameter(cmd,"@TotallNum",  DbType.Int32,entity.TotallNum);
			db.AddInParameter(cmd,"@WaitDealNum",  DbType.Int32,entity.WaitDealNum);
			db.AddInParameter(cmd,"@WaitDeliverNum",  DbType.Int32,entity.WaitDeliverNum);
			db.AddInParameter(cmd,"@ReturnNum",  DbType.Int32,entity.ReturnNum);
			db.AddInParameter(cmd,"@ExchangeNum",  DbType.Int32,entity.ExchangeNum);
			db.AddInParameter(cmd,"@FinishedNum",  DbType.Int32,entity.FinishedNum);
			db.AddInParameter(cmd,"@RejectNum",  DbType.Int32,entity.RejectNum);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteOrderMsgStoreByKey(int id)
	    {
			string sql=@"delete from OrderMsgStore where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderMsgStoreDisabled()
        {
            string sql = @"delete from  OrderMsgStore  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderMsgStoreByIds(string ids)
        {
            string sql = @"Delete from OrderMsgStore  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderMsgStoreByIds(string ids)
        {
            string sql = @"Update   OrderMsgStore set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   OrderMsgStoreEntity GetOrderMsgStore(int id)
		{
			string sql=@"SELECT  [Id],[SellerId],[TotallNum],[WaitDealNum],[WaitDeliverNum],[ReturnNum],[ExchangeNum],[FinishedNum],[RejectNum]
							FROM
							dbo.[OrderMsgStore] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		OrderMsgStoreEntity entity=new OrderMsgStoreEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SellerId=StringUtils.GetDbInt(reader["SellerId"]);
					entity.TotallNum=StringUtils.GetDbInt(reader["TotallNum"]);
					entity.WaitDealNum=StringUtils.GetDbInt(reader["WaitDealNum"]);
					entity.WaitDeliverNum=StringUtils.GetDbInt(reader["WaitDeliverNum"]);
					entity.ReturnNum=StringUtils.GetDbInt(reader["ReturnNum"]);
					entity.ExchangeNum=StringUtils.GetDbInt(reader["ExchangeNum"]);
					entity.FinishedNum=StringUtils.GetDbInt(reader["FinishedNum"]);
					entity.RejectNum=StringUtils.GetDbInt(reader["RejectNum"]);
				}
   		    }
            return entity;
		}
        public OrderMsgStoreEntity GetOrderMsgBySellerId(int sellerid)
        {
            string sql = @"SELECT  [Id],[SellerId],[TotallNum],[WaitDealNum],[WaitDeliverNum],[ReturnNum],[ExchangeNum],[FinishedNum],[RejectNum]
							FROM
							dbo.[OrderMsgStore] WITH(NOLOCK)	
							WHERE [SellerId]=@SellerId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@SellerId", DbType.Int32, sellerid);
            OrderMsgStoreEntity entity = new OrderMsgStoreEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.SellerId = StringUtils.GetDbInt(reader["SellerId"]);
                    entity.TotallNum = StringUtils.GetDbInt(reader["TotallNum"]);
                    entity.WaitDealNum = StringUtils.GetDbInt(reader["WaitDealNum"]);
                    entity.WaitDeliverNum = StringUtils.GetDbInt(reader["WaitDeliverNum"]);
                    entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);
                    entity.ExchangeNum = StringUtils.GetDbInt(reader["ExchangeNum"]);
                    entity.FinishedNum = StringUtils.GetDbInt(reader["FinishedNum"]);
                    entity.RejectNum = StringUtils.GetDbInt(reader["RejectNum"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<OrderMsgStoreEntity> GetOrderMsgStoreList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[SellerId],[TotallNum],[WaitDealNum],[WaitDeliverNum],[ReturnNum],[ExchangeNum],[FinishedNum],[RejectNum]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[SellerId],[TotallNum],[WaitDealNum],[WaitDeliverNum],[ReturnNum],[ExchangeNum],[FinishedNum],[RejectNum] from dbo.[OrderMsgStore] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[OrderMsgStore] with (nolock) ";
            IList<OrderMsgStoreEntity> entityList = new List< OrderMsgStoreEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					OrderMsgStoreEntity entity=new OrderMsgStoreEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SellerId=StringUtils.GetDbInt(reader["SellerId"]);
					entity.TotallNum=StringUtils.GetDbInt(reader["TotallNum"]);
					entity.WaitDealNum=StringUtils.GetDbInt(reader["WaitDealNum"]);
					entity.WaitDeliverNum=StringUtils.GetDbInt(reader["WaitDeliverNum"]);
					entity.ReturnNum=StringUtils.GetDbInt(reader["ReturnNum"]);
					entity.ExchangeNum=StringUtils.GetDbInt(reader["ExchangeNum"]);
					entity.FinishedNum=StringUtils.GetDbInt(reader["FinishedNum"]);
					entity.RejectNum=StringUtils.GetDbInt(reader["RejectNum"]);
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
        public IList<OrderMsgStoreEntity> GetOrderMsgStoreAll()
        {

            string sql = @"SELECT    [Id],[SellerId],[TotallNum],[WaitDealNum],[WaitDeliverNum],[ReturnNum],[ExchangeNum],[FinishedNum],[RejectNum] from dbo.[OrderMsgStore] WITH(NOLOCK)	";
		    IList<OrderMsgStoreEntity> entityList = new List<OrderMsgStoreEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   OrderMsgStoreEntity entity=new OrderMsgStoreEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SellerId=StringUtils.GetDbInt(reader["SellerId"]);
					entity.TotallNum=StringUtils.GetDbInt(reader["TotallNum"]);
					entity.WaitDealNum=StringUtils.GetDbInt(reader["WaitDealNum"]);
					entity.WaitDeliverNum=StringUtils.GetDbInt(reader["WaitDeliverNum"]);
					entity.ReturnNum=StringUtils.GetDbInt(reader["ReturnNum"]);
					entity.ExchangeNum=StringUtils.GetDbInt(reader["ExchangeNum"]);
					entity.FinishedNum=StringUtils.GetDbInt(reader["FinishedNum"]);
					entity.RejectNum=StringUtils.GetDbInt(reader["RejectNum"]);
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
        public int  ExistNum(OrderMsgStoreEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderMsgStore] WITH(NOLOCK) ";
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
