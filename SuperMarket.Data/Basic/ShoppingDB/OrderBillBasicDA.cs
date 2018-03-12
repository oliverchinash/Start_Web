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
功能描述：OrderBillBasic表的数据访问类。
创建时间：2016/11/17 22:50:07
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// OrderBillBasicEntity的数据访问操作
	/// </summary>
	public partial class OrderBillBasicDA: BaseSuperMarketDB
    {
        #region 实例化
        public static OrderBillBasicDA Instance
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
            internal static readonly OrderBillBasicDA instance = new OrderBillBasicDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表OrderBillBasic，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderBillBasic">待插入的实体对象</param>
		public int AddOrderBillBasic(OrderBillBasicEntity entity)
		{
		   string sql=@"insert into OrderBillBasic( [OrderCode],[Title],[BillType],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime])VALUES
			            ( @OrderCode,@Title,@BillType,@CompanyName,@CompanyCode,@CompanyAddress,@CompanyPhone,@CompanyBank,@BankAccount,@ReceiverName,@ReceiverPhone,@ReceiverProvince,@ReceiverCity,@ReceiverAddress,@Status,@CreateTime,@UpdateTime,@CheckManId,@CheckTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@BillType",  DbType.Int32,entity.BillType);
			db.AddInParameter(cmd,"@CompanyName",  DbType.String,entity.CompanyName);
			db.AddInParameter(cmd,"@CompanyCode",  DbType.String,entity.CompanyCode);
			db.AddInParameter(cmd,"@CompanyAddress",  DbType.String,entity.CompanyAddress);
			db.AddInParameter(cmd,"@CompanyPhone",  DbType.String,entity.CompanyPhone);
			db.AddInParameter(cmd,"@CompanyBank",  DbType.String,entity.CompanyBank);
			db.AddInParameter(cmd,"@BankAccount",  DbType.String,entity.BankAccount);
			db.AddInParameter(cmd,"@ReceiverName",  DbType.String,entity.ReceiverName);
			db.AddInParameter(cmd,"@ReceiverPhone",  DbType.String,entity.ReceiverPhone);
			db.AddInParameter(cmd,"@ReceiverProvince",  DbType.Int32,entity.ReceiverProvince);
			db.AddInParameter(cmd,"@ReceiverCity",  DbType.Int32,entity.ReceiverCity);
			db.AddInParameter(cmd,"@ReceiverAddress",  DbType.String,entity.ReceiverAddress);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderBillBasic">待更新的实体对象</param>
		public   int UpdateOrderBillBasic(OrderBillBasicEntity entity)
		{
			string sql=@" UPDATE dbo.[OrderBillBasic] SET
                       [OrderCode]=@OrderCode,[Title]=@Title,[BillType]=@BillType,[CompanyName]=@CompanyName,[CompanyCode]=@CompanyCode,[CompanyAddress]=@CompanyAddress,[CompanyPhone]=@CompanyPhone,[CompanyBank]=@CompanyBank,[BankAccount]=@BankAccount,[ReceiverName]=@ReceiverName,[ReceiverPhone]=@ReceiverPhone,[ReceiverProvince]=@ReceiverProvince,[ReceiverCity]=@ReceiverCity,[ReceiverAddress]=@ReceiverAddress,[Status]=@Status,[CreateTime]=@CreateTime,[UpdateTime]=@UpdateTime,[CheckManId]=@CheckManId,[CheckTime]=@CheckTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@BillType",  DbType.Int32,entity.BillType);
			db.AddInParameter(cmd,"@CompanyName",  DbType.String,entity.CompanyName);
			db.AddInParameter(cmd,"@CompanyCode",  DbType.String,entity.CompanyCode);
			db.AddInParameter(cmd,"@CompanyAddress",  DbType.String,entity.CompanyAddress);
			db.AddInParameter(cmd,"@CompanyPhone",  DbType.String,entity.CompanyPhone);
			db.AddInParameter(cmd,"@CompanyBank",  DbType.String,entity.CompanyBank);
			db.AddInParameter(cmd,"@BankAccount",  DbType.String,entity.BankAccount);
			db.AddInParameter(cmd,"@ReceiverName",  DbType.String,entity.ReceiverName);
			db.AddInParameter(cmd,"@ReceiverPhone",  DbType.String,entity.ReceiverPhone);
			db.AddInParameter(cmd,"@ReceiverProvince",  DbType.Int32,entity.ReceiverProvince);
			db.AddInParameter(cmd,"@ReceiverCity",  DbType.Int32,entity.ReceiverCity);
			db.AddInParameter(cmd,"@ReceiverAddress",  DbType.String,entity.ReceiverAddress);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteOrderBillBasicByKey(int id)
	    {
			string sql=@"delete from OrderBillBasic where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderBillBasicDisabled()
        {
            string sql = @"delete from  OrderBillBasic  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderBillBasicByIds(string ids)
        {
            string sql = @"Delete from OrderBillBasic  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderBillBasicByIds(string ids)
        {
            string sql = @"Update   OrderBillBasic set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   OrderBillBasicEntity GetOrderBillBasic(int id)
		{
			string sql=@"SELECT  [Id],[OrderCode],[Title],[BillType],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime]
							FROM
							dbo.[OrderBillBasic] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		OrderBillBasicEntity entity=new OrderBillBasicEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.BillType=StringUtils.GetDbInt(reader["BillType"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyCode=StringUtils.GetDbString(reader["CompanyCode"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.CompanyBank=StringUtils.GetDbString(reader["CompanyBank"]);
					entity.BankAccount=StringUtils.GetDbString(reader["BankAccount"]);
					entity.ReceiverName=StringUtils.GetDbString(reader["ReceiverName"]);
					entity.ReceiverPhone=StringUtils.GetDbString(reader["ReceiverPhone"]);
					entity.ReceiverProvince=StringUtils.GetDbInt(reader["ReceiverProvince"]);
					entity.ReceiverCity=StringUtils.GetDbInt(reader["ReceiverCity"]);
					entity.ReceiverAddress=StringUtils.GetDbString(reader["ReceiverAddress"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
				}
   		    }
            return entity;
		}
        public OrderBillBasicEntity GetBillBasicByOrderCode(long code)
        {
            string sql = @"SELECT  [Id],[OrderCode],[Title],[BillType],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime]
							FROM
							dbo.[OrderBillBasic] WITH(NOLOCK)	
							WHERE [OrderCode]=@OrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, code);
            OrderBillBasicEntity entity = new OrderBillBasicEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.BillType = StringUtils.GetDbInt(reader["BillType"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyCode = StringUtils.GetDbString(reader["CompanyCode"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
                    entity.CompanyPhone = StringUtils.GetDbString(reader["CompanyPhone"]);
                    entity.CompanyBank = StringUtils.GetDbString(reader["CompanyBank"]);
                    entity.BankAccount = StringUtils.GetDbString(reader["BankAccount"]);
                    entity.ReceiverName = StringUtils.GetDbString(reader["ReceiverName"]);
                    entity.ReceiverPhone = StringUtils.GetDbString(reader["ReceiverPhone"]);
                    entity.ReceiverProvince = StringUtils.GetDbInt(reader["ReceiverProvince"]);
                    entity.ReceiverCity = StringUtils.GetDbInt(reader["ReceiverCity"]);
                    entity.ReceiverAddress = StringUtils.GetDbString(reader["ReceiverAddress"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<OrderBillBasicEntity> GetOrderBillBasicList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[OrderCode],[Title],[BillType],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OrderCode],[Title],[BillType],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime] from dbo.[OrderBillBasic] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[OrderBillBasic] with (nolock) ";
            IList<OrderBillBasicEntity> entityList = new List< OrderBillBasicEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					OrderBillBasicEntity entity=new OrderBillBasicEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.BillType=StringUtils.GetDbInt(reader["BillType"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyCode=StringUtils.GetDbString(reader["CompanyCode"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.CompanyBank=StringUtils.GetDbString(reader["CompanyBank"]);
					entity.BankAccount=StringUtils.GetDbString(reader["BankAccount"]);
					entity.ReceiverName=StringUtils.GetDbString(reader["ReceiverName"]);
					entity.ReceiverPhone=StringUtils.GetDbString(reader["ReceiverPhone"]);
					entity.ReceiverProvince=StringUtils.GetDbInt(reader["ReceiverProvince"]);
					entity.ReceiverCity=StringUtils.GetDbInt(reader["ReceiverCity"]);
					entity.ReceiverAddress=StringUtils.GetDbString(reader["ReceiverAddress"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public IList<OrderBillBasicEntity> GetOrderBillBasicAll()
        {

            string sql = @"SELECT    [Id],[OrderCode],[Title],[BillType],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime] from dbo.[OrderBillBasic] WITH(NOLOCK)	";
		    IList<OrderBillBasicEntity> entityList = new List<OrderBillBasicEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   OrderBillBasicEntity entity=new OrderBillBasicEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.BillType=StringUtils.GetDbInt(reader["BillType"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyCode=StringUtils.GetDbString(reader["CompanyCode"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.CompanyBank=StringUtils.GetDbString(reader["CompanyBank"]);
					entity.BankAccount=StringUtils.GetDbString(reader["BankAccount"]);
					entity.ReceiverName=StringUtils.GetDbString(reader["ReceiverName"]);
					entity.ReceiverPhone=StringUtils.GetDbString(reader["ReceiverPhone"]);
					entity.ReceiverProvince=StringUtils.GetDbInt(reader["ReceiverProvince"]);
					entity.ReceiverCity=StringUtils.GetDbInt(reader["ReceiverCity"]);
					entity.ReceiverAddress=StringUtils.GetDbString(reader["ReceiverAddress"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public int  ExistNum(OrderBillBasicEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderBillBasic] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CompanyName=@CompanyName) ";
					     where = where+ "  (ReceiverName=@ReceiverName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CompanyName=@CompanyName) ";
					     where = where+ " id<>@Id and  (ReceiverName=@ReceiverName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName); 
					
            db.AddInParameter(cmd, "@ReceiverName", DbType.String, entity.ReceiverName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
