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
功能描述：WeChatResult表的数据访问类。
创建时间：2017/11/29 10:53:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.PayDB
{
	/// <summary>
	/// WeChatResultEntity的数据访问操作
	/// </summary>
	public partial class WeChatResultDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WeChatResultDA Instance
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
            internal static readonly WeChatResultDA instance = new WeChatResultDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WeChatResult，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weChatResult">待插入的实体对象</param>
		public int AddWeChatResult(WeChatResultEntity entity)
		{
		   string sql=@"insert into WeChatResult( [appid],[bank_type],[cash_fee],[fee_type],[is_subscribe],[mch_id],[nonce_str],[openid],[out_trade_no],[result_code],[return_code],[sign],[time_end],[total_fee],[trade_type],[transaction_id],[CreateTime])VALUES
			            ( @Appid,@BankType,@CashFee,@FeeType,@IsSubscribe,@MchId,@NonceStr,@Openid,@OutTradeNo,@ResultCode,@ReturnCode,@Sign,@TimeEnd,@TotalFee,@TradeType,@TransactionId,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Appid",  DbType.String,entity.Appid);
			db.AddInParameter(cmd, "@BankType",  DbType.String,entity.Bank_type);
			db.AddInParameter(cmd, "@CashFee",  DbType.String,entity.Cash_fee);
			db.AddInParameter(cmd, "@FeeType",  DbType.String,entity.Fee_type);
			db.AddInParameter(cmd, "@IsSubscribe",  DbType.String,entity.Is_subscribe);
			db.AddInParameter(cmd, "@MchId",  DbType.String,entity.Mch_id);
			db.AddInParameter(cmd, "@NonceStr",  DbType.String,entity.Nonce_str);
			db.AddInParameter(cmd,"@Openid",  DbType.String,entity.Openid);
			db.AddInParameter(cmd, "@OutTradeNo",  DbType.String,entity.Out_trade_no);
			db.AddInParameter(cmd, "@ResultCode",  DbType.String,entity.Result_code);
			db.AddInParameter(cmd, "@ReturnCode",  DbType.String,entity.Return_code);
			db.AddInParameter(cmd,"@Sign",  DbType.String,entity.Sign);
			db.AddInParameter(cmd, "@TimeEnd",  DbType.String,entity.Time_end);
			db.AddInParameter(cmd, "@TotalFee",  DbType.String,entity.Total_fee);
			db.AddInParameter(cmd, "@TradeType",  DbType.String,entity.Trade_type);
			db.AddInParameter(cmd, "@TransactionId",  DbType.String,entity.Transaction_id);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weChatResult">待更新的实体对象</param>
		public   int UpdateWeChatResult(WeChatResultEntity entity)
		{
			string sql=@" UPDATE dbo.[WeChatResult] SET
                       [appid]=@Appid,[bank_type]=@BankType,[cash_fee]=@CashFee,[fee_type]=@FeeType,[is_subscribe]=@IsSubscribe,[mch_id]=@MchId,[nonce_str]=@NonceStr,[openid]=@Openid,[out_trade_no]=@OutTradeNo,[result_code]=@ResultCode,[return_code]=@ReturnCode,[sign]=@Sign,[time_end]=@TimeEnd,[total_fee]=@TotalFee,[trade_type]=@TradeType,[transaction_id]=@TransactionId,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
            db.AddInParameter(cmd, "@Appid", DbType.String, entity.Appid);
            db.AddInParameter(cmd, "@BankType", DbType.String, entity.Bank_type);
            db.AddInParameter(cmd, "@CashFee", DbType.String, entity.Cash_fee);
            db.AddInParameter(cmd, "@FeeType", DbType.String, entity.Fee_type);
            db.AddInParameter(cmd, "@IsSubscribe", DbType.String, entity.Is_subscribe);
            db.AddInParameter(cmd, "@MchId", DbType.String, entity.Mch_id);
            db.AddInParameter(cmd, "@NonceStr", DbType.String, entity.Nonce_str);
            db.AddInParameter(cmd, "@Openid", DbType.String, entity.Openid);
            db.AddInParameter(cmd, "@OutTradeNo", DbType.String, entity.Out_trade_no);
            db.AddInParameter(cmd, "@ResultCode", DbType.String, entity.Result_code);
            db.AddInParameter(cmd, "@ReturnCode", DbType.String, entity.Return_code);
            db.AddInParameter(cmd, "@Sign", DbType.String, entity.Sign);
            db.AddInParameter(cmd, "@TimeEnd", DbType.String, entity.Time_end);
            db.AddInParameter(cmd, "@TotalFee", DbType.String, entity.Total_fee);
            db.AddInParameter(cmd, "@TradeType", DbType.String, entity.Trade_type);
            db.AddInParameter(cmd, "@TransactionId", DbType.String, entity.Transaction_id);
            db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteWeChatResultByKey(int id)
	    {
			string sql=@"delete from WeChatResult where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeChatResultDisabled()
        {
            string sql = @"delete from  WeChatResult  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeChatResultByIds(string ids)
        {
            string sql = @"Delete from WeChatResult  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeChatResultByIds(string ids)
        {
            string sql = @"Update   WeChatResult set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WeChatResultEntity GetWeChatResult(int id)
		{
			string sql=@"SELECT  [Id],[appid],[bank_type],[cash_fee],[fee_type],[is_subscribe],[mch_id],[nonce_str],[openid],[out_trade_no],[result_code],[return_code],[sign],[time_end],[total_fee],[trade_type],[transaction_id],[CreateTime]
							FROM
							dbo.[WeChatResult] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WeChatResultEntity entity=new WeChatResultEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Appid=StringUtils.GetDbString(reader["Appid"]);;
					entity.Bank_type=StringUtils.GetDbString(reader["Bank_type"]);;
					entity.Cash_fee=StringUtils.GetDbString(reader["Cash_fee"]);;
					entity.Fee_type=StringUtils.GetDbString(reader["Fee_type"]);;
					entity.Is_subscribe=StringUtils.GetDbString(reader["Is_subscribe"]);;
					entity.Mch_id=StringUtils.GetDbString(reader["Mch_id"]);;
					entity.Nonce_str=StringUtils.GetDbString(reader["Nonce_str"]);;
					entity.Openid=StringUtils.GetDbString(reader["Openid"]);;
					entity.Out_trade_no=StringUtils.GetDbString(reader["Out_trade_no"]);;
					entity.Result_code=StringUtils.GetDbString(reader["Result_code"]);;
					entity.Return_code=StringUtils.GetDbString(reader["Return_code"]);;
					entity.Sign=StringUtils.GetDbString(reader["Sign"]);;
					entity.Time_end=StringUtils.GetDbString(reader["Time_end"]);;
					entity.Total_fee=StringUtils.GetDbString(reader["Total_fee"]);;
					entity.Trade_type=StringUtils.GetDbString(reader["Trade_type"]);;
					entity.Transaction_id=StringUtils.GetDbString(reader["Transaction_id"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<WeChatResultEntity> GetWeChatResultList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[appid],[bank_type],[cash_fee],[fee_type],[is_subscribe],[mch_id],[nonce_str],[openid],[out_trade_no],[result_code],[return_code],[sign],[time_end],[total_fee],[trade_type],[transaction_id],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[appid],[bank_type],[cash_fee],[fee_type],[is_subscribe],[mch_id],[nonce_str],[openid],[out_trade_no],[result_code],[return_code],[sign],[time_end],[total_fee],[trade_type],[transaction_id],[CreateTime] from dbo.[WeChatResult] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WeChatResult] with (nolock) ";
            IList<WeChatResultEntity> entityList = new List< WeChatResultEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WeChatResultEntity entity=new WeChatResultEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Appid=StringUtils.GetDbString(reader["Appid"]);;
					entity.Bank_type=StringUtils.GetDbString(reader["Bank_type"]);;
					entity.Cash_fee=StringUtils.GetDbString(reader["Cash_fee"]);;
					entity.Fee_type=StringUtils.GetDbString(reader["Fee_type"]);;
					entity.Is_subscribe=StringUtils.GetDbString(reader["Is_subscribe"]);;
					entity.Mch_id=StringUtils.GetDbString(reader["Mch_id"]);;
					entity.Nonce_str=StringUtils.GetDbString(reader["Nonce_str"]);;
					entity.Openid=StringUtils.GetDbString(reader["Openid"]);;
					entity.Out_trade_no=StringUtils.GetDbString(reader["Out_trade_no"]);;
					entity.Result_code=StringUtils.GetDbString(reader["Result_code"]);;
					entity.Return_code=StringUtils.GetDbString(reader["Return_code"]);;
					entity.Sign=StringUtils.GetDbString(reader["Sign"]);;
					entity.Time_end=StringUtils.GetDbString(reader["Time_end"]);;
					entity.Total_fee=StringUtils.GetDbString(reader["Total_fee"]);;
					entity.Trade_type=StringUtils.GetDbString(reader["Trade_type"]);;
					entity.Transaction_id=StringUtils.GetDbString(reader["Transaction_id"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
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
        public IList<WeChatResultEntity> GetWeChatResultAll()
        {

            string sql = @"SELECT    [Id],[appid],[bank_type],[cash_fee],[fee_type],[is_subscribe],[mch_id],[nonce_str],[openid],[out_trade_no],[result_code],[return_code],[sign],[time_end],[total_fee],[trade_type],[transaction_id],[CreateTime] from dbo.[WeChatResult] WITH(NOLOCK)	";
		    IList<WeChatResultEntity> entityList = new List<WeChatResultEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WeChatResultEntity entity=new WeChatResultEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Appid=StringUtils.GetDbString(reader["Appid"]);;
					entity.Bank_type=StringUtils.GetDbString(reader["Bank_type"]);;
					entity.Cash_fee=StringUtils.GetDbString(reader["Cash_fee"]);;
					entity.Fee_type=StringUtils.GetDbString(reader["Fee_type"]);;
					entity.Is_subscribe=StringUtils.GetDbString(reader["Is_subscribe"]);;
					entity.Mch_id=StringUtils.GetDbString(reader["Mch_id"]);;
					entity.Nonce_str=StringUtils.GetDbString(reader["Nonce_str"]);;
					entity.Openid=StringUtils.GetDbString(reader["Openid"]);;
					entity.Out_trade_no=StringUtils.GetDbString(reader["Out_trade_no"]);;
					entity.Result_code=StringUtils.GetDbString(reader["Result_code"]);;
					entity.Return_code=StringUtils.GetDbString(reader["Return_code"]);;
					entity.Sign=StringUtils.GetDbString(reader["Sign"]);;
					entity.Time_end=StringUtils.GetDbString(reader["Time_end"]);;
					entity.Total_fee=StringUtils.GetDbString(reader["Total_fee"]);;
					entity.Trade_type=StringUtils.GetDbString(reader["Trade_type"]);;
					entity.Transaction_id=StringUtils.GetDbString(reader["Transaction_id"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
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
        public int  ExistNum(WeChatResultEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WeChatResult] WITH(NOLOCK) ";
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
