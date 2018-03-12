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
功能描述：StatisticTrade表的数据访问类。
创建时间：2017/6/17 11:02:13
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// StatisticTradeEntity的数据访问操作
	/// </summary>
	public partial class StatisticTradeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static StatisticTradeDA Instance
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
            internal static readonly StatisticTradeDA instance = new StatisticTradeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StatisticTrade，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticTrade">待插入的实体对象</param>
		public int AddStatisticTrade(StatisticTradeEntity entity)
		{
		   string sql=@"insert into StatisticTrade( [StatisticDate],[TradeAmount],[TradeNum])VALUES
			            ( @StatisticDate,@TradeAmount,@TradeNum);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@StatisticDate",  DbType.DateTime,entity.StatisticDate);
			db.AddInParameter(cmd,"@TradeAmount",  DbType.Decimal,entity.TradeAmount);
			db.AddInParameter(cmd,"@TradeNum",  DbType.Int32,entity.TradeNum);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticTrade">待更新的实体对象</param>
		public   int UpdateStatisticTrade(StatisticTradeEntity entity)
		{
			string sql=@" UPDATE dbo.[StatisticTrade] SET
                       [StatisticDate]=@StatisticDate,[TradeAmount]=@TradeAmount,[TradeNum]=@TradeNum
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@StatisticDate",  DbType.DateTime,entity.StatisticDate);
			db.AddInParameter(cmd,"@TradeAmount",  DbType.Decimal,entity.TradeAmount);
			db.AddInParameter(cmd,"@TradeNum",  DbType.Int32,entity.TradeNum);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteStatisticTradeByKey(int id)
	    {
			string sql=@"delete from StatisticTrade where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticTradeDisabled()
        {
            string sql = @"delete from  StatisticTrade  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticTradeByIds(string ids)
        {
            string sql = @"Delete from StatisticTrade  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticTradeByIds(string ids)
        {
            string sql = @"Update   StatisticTrade set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   StatisticTradeEntity GetStatisticTrade(int id)
		{
			string sql=@"SELECT  [Id],[StatisticDate],[TradeAmount],[TradeNum]
							FROM
							dbo.[StatisticTrade] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		StatisticTradeEntity entity=new StatisticTradeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StatisticDate=StringUtils.GetDbDateTime(reader["StatisticDate"]);
					entity.TradeAmount=StringUtils.GetDbDecimal(reader["TradeAmount"]);
					entity.TradeNum=StringUtils.GetDbInt(reader["TradeNum"]);
				}
   		    }
            return entity;
		}

        public StatisticTradeEntity GetStatisticTradeToday()
        {
            string sql = @"SELECT  [Id],[StatisticDate],[TradeAmount],[TradeNum]
							FROM  dbo.[StatisticTrade] WITH(NOLOCK)	
							WHERE [StatisticDate]=@StatisticDate";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@StatisticDate", DbType.DateTime, DateTime.Now.Date);
            StatisticTradeEntity entity = new StatisticTradeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.StatisticDate = StringUtils.GetDbDateTime(reader["StatisticDate"]);
                    entity.TradeAmount = StringUtils.GetDbDecimal(reader["TradeAmount"]);
                    entity.TradeNum = StringUtils.GetDbInt(reader["TradeNum"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<StatisticTradeEntity> GetStatisticTradeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[StatisticDate],[TradeAmount],[TradeNum]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[StatisticDate],[TradeAmount],[TradeNum] from dbo.[StatisticTrade] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[StatisticTrade] with (nolock) ";
            IList<StatisticTradeEntity> entityList = new List< StatisticTradeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					StatisticTradeEntity entity=new StatisticTradeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StatisticDate=StringUtils.GetDbDateTime(reader["StatisticDate"]);
					entity.TradeAmount=StringUtils.GetDbDecimal(reader["TradeAmount"]);
					entity.TradeNum=StringUtils.GetDbInt(reader["TradeNum"]);
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
        public IList<StatisticTradeEntity> GetStatisticTradeAll()
        {

            string sql = @"SELECT    [Id],[StatisticDate],[TradeAmount],[TradeNum] from dbo.[StatisticTrade] WITH(NOLOCK)	";
		    IList<StatisticTradeEntity> entityList = new List<StatisticTradeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   StatisticTradeEntity entity=new StatisticTradeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StatisticDate=StringUtils.GetDbDateTime(reader["StatisticDate"]);
					entity.TradeAmount=StringUtils.GetDbDecimal(reader["TradeAmount"]);
					entity.TradeNum=StringUtils.GetDbInt(reader["TradeNum"]);
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
        public int  ExistNum(StatisticTradeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[StatisticTrade] WITH(NOLOCK) ";
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
