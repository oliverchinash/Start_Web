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
功能描述：PayTrade表的数据访问类。
创建时间：2016/10/9 10:39:34
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// PayTradeEntity的数据访问操作
	/// </summary>
	public partial class PayTradeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static PayTradeDA Instance
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
            internal static readonly PayTradeDA instance = new PayTradeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表PayTrade，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payTrade">待插入的实体对象</param>
		public int AddPayTrade(PayTradeEntity entity)
		{
		   string sql= @"insert into PayTrade( [PayCode],[OrderCode],[OrderAmount],[PayBankCode],[TransCode],[MobilePhone],CreateTime)VALUES
			            ( @PayCode,@OrderCode,@OrderAmount,@PayBankCode,@TransCode,@MobilePhone,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@PayCode",  DbType.String,entity.PayCode);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@OrderAmount",  DbType.Decimal,entity.OrderAmount);
			db.AddInParameter(cmd,"@PayBankCode",  DbType.String,entity.PayBankCode);
			db.AddInParameter(cmd,"@TransCode",  DbType.String,entity.TransCode);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd, "@CreateTime",  DbType.DateTime,DateTime.Now);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payTrade">待更新的实体对象</param>
		public   int UpdatePayTrade(PayTradeEntity entity)
		{
			string sql=@" UPDATE dbo.[PayTrade] SET
                       [PayCode]=@PayCode,[OrderCode]=@OrderCode,[OrderAmount]=@OrderAmount,[PayBankCode]=@PayBankCode,[TransCode]=@TransCode,[MobilePhone]=@MobilePhone
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@PayCode",  DbType.String,entity.PayCode);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@OrderAmount",  DbType.Decimal,entity.OrderAmount);
			db.AddInParameter(cmd,"@PayBankCode",  DbType.String,entity.PayBankCode);
			db.AddInParameter(cmd,"@TransCode",  DbType.String,entity.TransCode);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeletePayTradeByKey(int id)
	    {
			string sql=@"delete from PayTrade where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayTradeDisabled()
        {
            string sql = @"delete from  PayTrade  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayTradeByIds(string ids)
        {
            string sql = @"Delete from PayTrade  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayTradeByIds(string ids)
        {
            string sql = @"Update   PayTrade set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   PayTradeEntity GetPayTrade(int id)
		{
			string sql=@"SELECT  [Id],[PayCode],[OrderCode],[OrderAmount],[PayBankCode],[TransCode],[MobilePhone]
							FROM
							dbo.[PayTrade] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		PayTradeEntity entity=new PayTradeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.PayCode=StringUtils.GetDbString(reader["PayCode"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.OrderAmount=StringUtils.GetDbDecimal(reader["OrderAmount"]);
					entity.PayBankCode=StringUtils.GetDbString(reader["PayBankCode"]);
					entity.TransCode=StringUtils.GetDbString(reader["TransCode"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
				}
   		    }
            return entity;
		}
        public PayTradeEntity GetPayTradeByOrder(Int64 paycode)
        {
            string sql = @"SELECT  [Id],[PayCode],[OrderCode],[OrderAmount],[PayBankCode],[TransCode],[MobilePhone],CreateTime
							FROM
							dbo.[PayTrade] WITH(NOLOCK)	
							WHERE [PayCode]=@PayCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PayCode", DbType.String, paycode);
            PayTradeEntity entity = new PayTradeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.PayCode = StringUtils.GetDbString(reader["PayCode"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderAmount = StringUtils.GetDbDecimal(reader["OrderAmount"]);
                    entity.PayBankCode = StringUtils.GetDbString(reader["PayBankCode"]);
                    entity.TransCode = StringUtils.GetDbString(reader["TransCode"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<PayTradeEntity> GetPayTradeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[PayCode],[OrderCode],[OrderAmount],[PayBankCode],[TransCode],[MobilePhone]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[PayCode],[OrderCode],[OrderAmount],[PayBankCode],[TransCode],[MobilePhone] from dbo.[PayTrade] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[PayTrade] with (nolock) ";
            IList<PayTradeEntity> entityList = new List< PayTradeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					PayTradeEntity entity=new PayTradeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.PayCode=StringUtils.GetDbString(reader["PayCode"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.OrderAmount=StringUtils.GetDbDecimal(reader["OrderAmount"]);
					entity.PayBankCode=StringUtils.GetDbString(reader["PayBankCode"]);
					entity.TransCode=StringUtils.GetDbString(reader["TransCode"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
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
        public IList<PayTradeEntity> GetPayTradeAll()
        {

            string sql = @"SELECT    [Id],[PayCode],[OrderCode],[OrderAmount],[PayBankCode],[TransCode],[MobilePhone] from dbo.[PayTrade] WITH(NOLOCK)	";
		    IList<PayTradeEntity> entityList = new List<PayTradeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   PayTradeEntity entity=new PayTradeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.PayCode=StringUtils.GetDbString(reader["PayCode"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.OrderAmount=StringUtils.GetDbDecimal(reader["OrderAmount"]);
					entity.PayBankCode=StringUtils.GetDbString(reader["PayBankCode"]);
					entity.TransCode=StringUtils.GetDbString(reader["TransCode"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
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
        public int  ExistNum(PayTradeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PayTrade] WITH(NOLOCK) WHERE PayCode=@PayCode"; 
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@PayCode", DbType.String, entity.PayCode);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
