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
功能描述：OrderPayLineConfirm表的数据访问类。
创建时间：2017/1/16 12:07:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// OrderPayLineConfirmEntity的数据访问操作
	/// </summary>
	public partial class OrderPayLineConfirmDA: BaseSuperMarketDB
    {
        #region 实例化
        public static OrderPayLineConfirmDA Instance
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
            internal static readonly OrderPayLineConfirmDA instance = new OrderPayLineConfirmDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表OrderPayLineConfirm，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderPayLineConfirm">待插入的实体对象</param>
		public int AddOrderPayLineConfirm(OrderPayLineConfirmEntity entity)
		{
            string sql = @"
DECLARE @idenid INT 
insert into OrderPayLineConfirm( [OrderCode],[Reason],[CreateTime],[CreateManId],[Status])VALUES
			            ( @OrderCode,@Reason,GETDATE(),@CreateManId,@Status); 
SET @idenid=SCOPE_IDENTITY(); 
            update dbo.[Order] set FinancialStatus=@WaitConfirm  where Code=@OrderCode and FinancialStatus=@FinanceDefault ;
			SELECT @idenid ";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@Reason",  DbType.String,entity.Reason); 
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId); 
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd, "@WaitConfirm",  DbType.Int32, (int)SuperMarket.Model.FinancialStatus.WaitChecked);
			db.AddInParameter(cmd, "@FinanceDefault",  DbType.Int32,(int)SuperMarket.Model.FinancialStatus.Default);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderPayLineConfirm">待更新的实体对象</param>
		public   int UpdateOrderPayLineConfirm(OrderPayLineConfirmEntity entity)
		{
			string sql=@" UPDATE dbo.[OrderPayLineConfirm] SET
                       [OrderCode]=@OrderCode,[Reason]=@Reason,[CreateTime]=@CreateTime,[CreateManId]=@CreateManId,[FinanceDealManId]=@FinanceDealManId,[FinanceDealTime]=@FinanceDealTime,[Status]=@Status
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@Reason",  DbType.String,entity.Reason);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@FinanceDealManId",  DbType.Int32,entity.FinanceDealManId);
			db.AddInParameter(cmd,"@FinanceDealTime",  DbType.DateTime,entity.FinanceDealTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteOrderPayLineConfirmByKey(int id)
	    {
			string sql=@"delete from OrderPayLineConfirm where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderPayLineConfirmDisabled()
        {
            string sql = @"delete from  OrderPayLineConfirm  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderPayLineConfirmByIds(string ids)
        {
            string sql = @"Delete from OrderPayLineConfirm  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderPayLineConfirmByIds(string ids)
        {
            string sql = @"Update   OrderPayLineConfirm set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   OrderPayLineConfirmEntity GetPayLineConfirm(int id,long ordercode)
		{
			string sql= @"SELECT  [Id],[OrderCode],[Reason],[CreateTime],[CreateManId],[FinanceDealManId],[FinanceDealTime],[Status]
							FROM
							dbo.[OrderPayLineConfirm] WITH(NOLOCK)	
							WHERE [Id]=@id and OrderCode=@OrderCode and Status=@Status";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
			db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
			db.AddInParameter(cmd, "@Status", DbType.Int32, (int)PayLineConfirm.Default);
            OrderPayLineConfirmEntity entity=new OrderPayLineConfirmEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.Reason=StringUtils.GetDbString(reader["Reason"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.FinanceDealManId=StringUtils.GetDbInt(reader["FinanceDealManId"]);
					entity.FinanceDealTime=StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
				}
   		    }
            return entity;
		}
        public int ConfirmRecived(int id,long ordercode,int sysmemid)
        {
            string sql = @"Update   OrderPayLineConfirm set Status=@ConfirmStatus,FinanceDealManId=@FinanceDealManId,FinanceDealTime=getdate()  where Id =@ConfirmId and OrderCode=@OrderCode;
update dbo.[Order] set FinancialStatus=@FinancialStatus,Status=@NewStatus,PayTime=getdate(),DealTime=getdate() where Code =@OrderCode and Status=@OldStatus and FinancialStatus=@OldFinanceStatus;
 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ConfirmId", DbType.Int32, id);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@ConfirmStatus", DbType.Int32, (int)PayLineConfirm.Pass);
            db.AddInParameter(cmd, "@FinancialStatus", DbType.Int32, (int)FinancialStatus.Checked);
            db.AddInParameter(cmd, "@FinanceDealManId", DbType.Int32, sysmemid);
            db.AddInParameter(cmd, "@NewStatus", DbType.Int32, (int)OrderStatus.WaitDeliver);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int32, (int)OrderStatus.WaitPay);
            db.AddInParameter(cmd, "@OldFinanceStatus", DbType.Int32, (int)FinancialStatus.WaitChecked);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<OrderPayLineConfirmEntity> GetOrderPayLineConfirmList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[OrderCode],[Reason],[CreateTime],[CreateManId],[FinanceDealManId],[FinanceDealTime],[Status]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OrderCode],[Reason],[CreateTime],[CreateManId],[FinanceDealManId],[FinanceDealTime],[Status] from dbo.[OrderPayLineConfirm] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[OrderPayLineConfirm] with (nolock) ";
            IList<OrderPayLineConfirmEntity> entityList = new List< OrderPayLineConfirmEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					OrderPayLineConfirmEntity entity=new OrderPayLineConfirmEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.Reason=StringUtils.GetDbString(reader["Reason"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.FinanceDealManId=StringUtils.GetDbInt(reader["FinanceDealManId"]);
					entity.FinanceDealTime=StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
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

        public IList<VWOrderPayLineConfirm> GetVWPayLineConfirmList(int pagesize, int pageindex, ref int recordCount, long ordercode, string payconfirmcode)
        {
            string where = " where 1=1 ";
            if(ordercode>0)
            {
                where += " and a.OrderCode=@OrderCode ";
            }
            if (!string.IsNullOrEmpty(payconfirmcode ))
            {
                where += " and b.PayConfirmCode like @PayConfirmCode ";
            }
            string sql = @"SELECT   [Id],[OrderCode],[Reason],[CreateTime],[CreateManId],[FinanceDealManId],[FinanceDealTime],[Status],ActPrice, PayConfirmCode
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,  A.*,B.ActPrice,B.PayConfirmCode  FROM dbo.OrderPayLineConfirm A  WITH(NOLOCK) INNER JOIN dbo.[Order] B  WITH(NOLOCK) ON A.OrderCode=B.Code	
						" + where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[OrderPayLineConfirm] with (nolock)  "+where;
            IList<VWOrderPayLineConfirm> entityList = new List<VWOrderPayLineConfirm>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode); 
            }
            if (!string.IsNullOrEmpty(payconfirmcode))
            { 
                db.AddInParameter(cmd, "@PayConfirmCode", DbType.String, "%"+payconfirmcode+"%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWOrderPayLineConfirm entity = new VWOrderPayLineConfirm();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Reason = StringUtils.GetDbString(reader["Reason"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.FinanceDealManId = StringUtils.GetDbInt(reader["FinanceDealManId"]);
                    entity.FinanceDealTime = StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.PayConfirmCode = StringUtils.GetDbString(reader["PayConfirmCode"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            }
            if (!string.IsNullOrEmpty(payconfirmcode))
            {
                db.AddInParameter(cmd, "@PayConfirmCode", DbType.String, "%" + payconfirmcode + "%");
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
        public IList<OrderPayLineConfirmEntity> GetOrderPayLineConfirmAll()
        {

            string sql = @"SELECT    [Id],[OrderCode],[Reason],[CreateTime],[CreateManId],[FinanceDealManId],[FinanceDealTime],[Status] from dbo.[OrderPayLineConfirm] WITH(NOLOCK)	";
		    IList<OrderPayLineConfirmEntity> entityList = new List<OrderPayLineConfirmEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   OrderPayLineConfirmEntity entity=new OrderPayLineConfirmEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.Reason=StringUtils.GetDbString(reader["Reason"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.FinanceDealManId=StringUtils.GetDbInt(reader["FinanceDealManId"]);
					entity.FinanceDealTime=StringUtils.GetDbDateTime(reader["FinanceDealTime"]);
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
        public int  ExistNum(long ordercode,int status)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderPayLineConfirm] WITH(NOLOCK) ";
            string where = "where OrderCode=@OrderCode ";
            if (status != -1)
            {
                where += " and Status=@Status ";
            }
              sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            if (status != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
