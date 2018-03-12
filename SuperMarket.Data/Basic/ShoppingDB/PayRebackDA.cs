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
功能描述：PayReback表的数据访问类。
创建时间：2016/12/9 14:09:54
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
    /// <summary>
    /// PayRebackEntity的数据访问操作
    /// </summary>
    public partial class PayRebackDA : BaseSuperMarketDB
    {
        #region 实例化
        public static PayRebackDA Instance
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
            internal static readonly PayRebackDA instance = new PayRebackDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表PayReback，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="payReback">待插入的实体对象</param>
        public int AddPayReback(PayRebackEntity entity)
        {
            string sql = @"insert into PayReback( [OrderCode],[BatchNo],[TradeNoLog],FinanceRefundId,[RebackFee],[Remack],[CreateTime],[RebackTime],[Status],[ReBackManId])VALUES
			            ( @OrderCode,@BatchNo,@TradeNoLog,@FinanceRefundId,@RebackFee,@Remack,@CreateTime,@RebackTime,@Status,@ReBackManId);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, entity.OrderCode);
            db.AddInParameter(cmd, "@BatchNo", DbType.Int64, entity.BatchNo);
            db.AddInParameter(cmd, "@TradeNoLog", DbType.String, entity.TradeNoLog);
            db.AddInParameter(cmd, "@RebackFee", DbType.Decimal, entity.RebackFee);
            db.AddInParameter(cmd, "@Remack", DbType.String, entity.Remack);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@RebackTime", DbType.DateTime, entity.RebackTime);
            db.AddInParameter(cmd, "@FinanceRefundId", DbType.Int32, entity.FinanceRefundId);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@ReBackManId", DbType.Int32, entity.ReBackManId);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="payReback">待更新的实体对象</param>
        public int UpdatePayReback(PayRebackEntity entity)
        {
            string sql = @" UPDATE dbo.[PayReback] SET
                       [OrderCode]=@OrderCode,[BatchNo]=@BatchNo,[TradeNoLog]=@TradeNoLog,[RebackFee]=@RebackFee,[Remack]=@Remack,[CreateTime]=@CreateTime,[RebackTime]=@RebackTime,[Status]=@Status,[ReBackManId]=@ReBackManId
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, entity.OrderCode);
            db.AddInParameter(cmd, "@BatchNo", DbType.Int64, entity.BatchNo);
            db.AddInParameter(cmd, "@TradeNoLog", DbType.String, entity.TradeNoLog);
            db.AddInParameter(cmd, "@RebackFee", DbType.Decimal, entity.RebackFee);
            db.AddInParameter(cmd, "@Remack", DbType.String, entity.Remack);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@RebackTime", DbType.DateTime, entity.RebackTime);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@ReBackManId", DbType.Int32, entity.ReBackManId);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="payReback">待更新的实体对象</param>
        public int UpdatePayRebackByTradeNoLog(string tradeNoLog, decimal rebackFee)
        {
            string sql = @"Update dbo.[PayReback] set RebackFee=@RebackFee,RebackTime=getdate(),Status=1 where TradeNoLog=@TradeNoLog";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@TradeNoLog", DbType.String, tradeNoLog);
            db.AddInParameter(cmd, "@RebackFee", DbType.Decimal, rebackFee);
            return db.ExecuteNonQuery(cmd);
        }



        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayRebackByKey(int id)
        {
            string sql = @"delete from PayReback where    Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        public int ProcPayBackRecive(string code,string num,string details)
        {
            string sql = @"EXEC Proc_PayRebackRecive @RebackBatchCode, @Num,@Details";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@RebackBatchCode", DbType.String, code);
            db.AddInParameter(cmd, "@Num", DbType.Int32, num);
            db.AddInParameter(cmd, "@Details", DbType.String, details);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayRebackDisabled()
        {
            string sql = @"delete from  PayReback  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayRebackByIds(string ids)
        {
            string sql = @"Delete from PayReback  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayRebackByIds(string ids)
        {
            string sql = @"Update   PayReback set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public PayRebackEntity GetPayReback(int id)
        {
            string sql = @"SELECT  [Id],[OrderCode],[BatchNo],[TradeNoLog],[RebackFee],[Remack],[CreateTime],[RebackTime],[Status],[ReBackManId]
							FROM
							dbo.[PayReback] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            PayRebackEntity entity = new PayRebackEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.BatchNo = StringUtils.GetDbLong(reader["BatchNo"]);
                    entity.TradeNoLog = StringUtils.GetDbString(reader["TradeNoLog"]);
                    entity.RebackFee = StringUtils.GetDbDecimal(reader["RebackFee"]);
                    entity.Remack = StringUtils.GetDbString(reader["Remack"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.RebackTime = StringUtils.GetDbDateTime(reader["RebackTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ReBackManId = StringUtils.GetDbInt(reader["ReBackManId"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public PayRebackEntity GetPayRebackByTradeNoLog(string tradeNoLog)
        {
            string sql = @"SELECT  [Id],[OrderCode],[BatchNo],[TradeNoLog],[RebackFee],[Remack],[CreateTime],[RebackTime],[Status],[ReBackManId]
							FROM
							dbo.[PayReback] WITH(NOLOCK)	
							WHERE [TradeNoLog]=@TradeNoLog";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@TradeNoLog", DbType.String, tradeNoLog);
            PayRebackEntity entity = new PayRebackEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.BatchNo = StringUtils.GetDbLong(reader["BatchNo"]);
                    entity.TradeNoLog = StringUtils.GetDbString(reader["TradeNoLog"]);
                    entity.RebackFee = StringUtils.GetDbDecimal(reader["RebackFee"]);
                    entity.Remack = StringUtils.GetDbString(reader["Remack"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.RebackTime = StringUtils.GetDbDateTime(reader["RebackTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ReBackManId = StringUtils.GetDbInt(reader["ReBackManId"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<PayRebackEntity> GetPayRebackList(int pagesize, int pageindex, ref int recordCount, string _batchno, int status)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(_batchno))
            {
                where += " and BatchNo=@BatchNo ";
            }
            if (status != -1)
            {
                where += " and Status=@Status ";
            }
            string sql = @"SELECT   [Id],[OrderCode],[BatchNo],[TradeNoLog],[RebackFee],[Remack],[CreateTime],[RebackTime],[Status],[ReBackManId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OrderCode],[BatchNo],[TradeNoLog],[RebackFee],[Remack],[CreateTime],[RebackTime],[Status],[ReBackManId] from dbo.[PayReback] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[PayReback] with (nolock) " + where;
            IList<PayRebackEntity> entityList = new List<PayRebackEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(_batchno))
            {
                db.AddInParameter(cmd, "@BatchNo", DbType.String, _batchno);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int16, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    PayRebackEntity entity = new PayRebackEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.BatchNo = StringUtils.GetDbLong(reader["BatchNo"]);
                    entity.TradeNoLog = StringUtils.GetDbString(reader["TradeNoLog"]);
                    entity.RebackFee = StringUtils.GetDbDecimal(reader["RebackFee"]);
                    entity.Remack = StringUtils.GetDbString(reader["Remack"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.RebackTime = StringUtils.GetDbDateTime(reader["RebackTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ReBackManId = StringUtils.GetDbInt(reader["ReBackManId"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(_batchno))
            {
                db.AddInParameter(cmd, "@BatchNo", DbType.String, _batchno);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int16, status);
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
        public IList<string> GetBatchTodayWait()
        {
            IList<string> _list = new List<string>();

            string sql = @"SELECT DISTINCT BatchNo from dbo.[PayReback] WHERE  CreateTime>@CreateTime order by BatchNo desc ";

            IList<PayRebackEntity> entityList = new List<PayRebackEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd"));
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    _list.Add(StringUtils.GetDbString(reader["BatchNo"]));
                }
            }
            return _list;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<PayRebackEntity> GetPayRebackAll(string batchno)
        {

            string sql = @"SELECT  [Id],[OrderCode],[BatchNo],[TradeNoLog],[RebackFee],[Remack],[CreateTime],[RebackTime],[Status],[ReBackManId] from dbo.[PayReback] WITH(NOLOCK)	
                           where BatchNo=@BatchNo and status=0";
            IList<PayRebackEntity> entityList = new List<PayRebackEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@BatchNo", DbType.String, batchno);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    PayRebackEntity entity = new PayRebackEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.BatchNo = StringUtils.GetDbLong(reader["BatchNo"]);
                    entity.TradeNoLog = StringUtils.GetDbString(reader["TradeNoLog"]);
                    entity.RebackFee = StringUtils.GetDbDecimal(reader["RebackFee"]);
                    entity.Remack = StringUtils.GetDbString(reader["Remack"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.RebackTime = StringUtils.GetDbDateTime(reader["RebackTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ReBackManId = StringUtils.GetDbInt(reader["ReBackManId"]);
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
        public int ExistNum(PayRebackEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PayReback] WITH(NOLOCK) WHERE (FinanceRefundId = @FinanceRefundId AND Status = 1)
or FinanceRefundId = @FinanceRefundId AND BatchNo = @BatchNo";

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FinanceRefundId", DbType.Int64, entity.FinanceRefundId);
            db.AddInParameter(cmd, "@BatchNo", DbType.String, entity.BatchNo); 
            object identity = db.ExecuteScalar(cmd);
            int result = StringUtils.GetDbInt(identity);
            return result;
        }








        #endregion
        #endregion
    }
}
