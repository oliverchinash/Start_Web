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
功能描述：IntegralChange表的数据访问类。
创建时间：2016/12/1 14:30:43
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// IntegralChangeEntity的数据访问操作
	/// </summary>
	public partial class IntegralChangeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static IntegralChangeDA Instance
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
            internal static readonly IntegralChangeDA instance = new IntegralChangeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表IntegralChange，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="integralChange">待插入的实体对象</param>
		public int AddIntegralChange(IntegralChangeEntity entity)
		{
		   string sql=@"insert into IntegralChange( [MemId],[ChangeNum],[BalanceChangeNum],[AvailableChangeNum],[FreezingChangeNum],[ChangeType],[ChangeReason],[ChangeTime])VALUES
			            ( @MemId,@ChangeNum,@BalanceChangeNum,@AvailableChangeNum,@FreezingChangeNum,@ChangeType,@ChangeReason,@ChangeTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@ChangeNum",  DbType.Int32,entity.ChangeNum);
			db.AddInParameter(cmd,"@BalanceChangeNum",  DbType.Int32,entity.BalanceChangeNum);
			db.AddInParameter(cmd,"@AvailableChangeNum",  DbType.Int32,entity.AvailableChangeNum);
			db.AddInParameter(cmd,"@FreezingChangeNum",  DbType.Int32,entity.FreezingChangeNum);
			db.AddInParameter(cmd,"@ChangeType",  DbType.Int32,entity.ChangeType);
			db.AddInParameter(cmd,"@ChangeReason",  DbType.String,entity.ChangeReason);
			db.AddInParameter(cmd,"@ChangeTime",  DbType.DateTime,entity.ChangeTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="integralChange">待更新的实体对象</param>
		public   int UpdateIntegralChange(IntegralChangeEntity entity)
		{
			string sql=@" UPDATE dbo.[IntegralChange] SET
                       [MemId]=@MemId,[ChangeNum]=@ChangeNum,[BalanceChangeNum]=@BalanceChangeNum,[AvailableChangeNum]=@AvailableChangeNum,[FreezingChangeNum]=@FreezingChangeNum,[ChangeType]=@ChangeType,[ChangeReason]=@ChangeReason,[ChangeTime]=@ChangeTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@ChangeNum",  DbType.Int32,entity.ChangeNum);
			db.AddInParameter(cmd,"@BalanceChangeNum",  DbType.Int32,entity.BalanceChangeNum);
			db.AddInParameter(cmd,"@AvailableChangeNum",  DbType.Int32,entity.AvailableChangeNum);
			db.AddInParameter(cmd,"@FreezingChangeNum",  DbType.Int32,entity.FreezingChangeNum);
			db.AddInParameter(cmd,"@ChangeType",  DbType.Int32,entity.ChangeType);
			db.AddInParameter(cmd,"@ChangeReason",  DbType.String,entity.ChangeReason);
			db.AddInParameter(cmd,"@ChangeTime",  DbType.DateTime,entity.ChangeTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteIntegralChangeByKey(int id)
	    {
			string sql=@"delete from IntegralChange where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIntegralChangeDisabled()
        {
            string sql = @"delete from  IntegralChange  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIntegralChangeByIds(string ids)
        {
            string sql = @"Delete from IntegralChange  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIntegralChangeByIds(string ids)
        {
            string sql = @"Update   IntegralChange set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IntegralChangeEntity GetIntegralChange(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[ChangeNum],[BalanceChangeNum],[AvailableChangeNum],[FreezingChangeNum],[ChangeType],[ChangeReason],[ChangeTime]
							FROM
							dbo.[IntegralChange] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		IntegralChangeEntity entity=new IntegralChangeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.ChangeNum=StringUtils.GetDbInt(reader["ChangeNum"]);
					entity.BalanceChangeNum=StringUtils.GetDbInt(reader["BalanceChangeNum"]);
					entity.AvailableChangeNum=StringUtils.GetDbInt(reader["AvailableChangeNum"]);
					entity.FreezingChangeNum=StringUtils.GetDbInt(reader["FreezingChangeNum"]);
					entity.ChangeType=StringUtils.GetDbInt(reader["ChangeType"]);
					entity.ChangeReason=StringUtils.GetDbString(reader["ChangeReason"]);
					entity.ChangeTime=StringUtils.GetDbDateTime(reader["ChangeTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<IntegralChangeEntity> GetIntegralChangeList(int pagesize, int pageindex, ref  int recordCount ,int memid)
		{
            string where = " where MemId=@MemId ";

			string sql=@"SELECT   [Id],[MemId],[ChangeNum],[BalanceChangeNum],[AvailableChangeNum],[FreezingChangeNum],[ChangeType],[ChangeReason],[ChangeTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[ChangeNum],[BalanceChangeNum],[AvailableChangeNum],[FreezingChangeNum],[ChangeType],[ChangeReason],[ChangeTime] from dbo.[IntegralChange] WITH(NOLOCK)	
						"+ where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[IntegralChange] with (nolock) "+ where;
            IList<IntegralChangeEntity> entityList = new List< IntegralChangeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
		    db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					IntegralChangeEntity entity=new IntegralChangeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.ChangeNum=StringUtils.GetDbInt(reader["ChangeNum"]);
					entity.BalanceChangeNum=StringUtils.GetDbInt(reader["BalanceChangeNum"]);
					entity.AvailableChangeNum=StringUtils.GetDbInt(reader["AvailableChangeNum"]);
					entity.FreezingChangeNum=StringUtils.GetDbInt(reader["FreezingChangeNum"]);
					entity.ChangeType=StringUtils.GetDbInt(reader["ChangeType"]);
					entity.ChangeReason=StringUtils.GetDbString(reader["ChangeReason"]);
					entity.ChangeTime=StringUtils.GetDbDateTime(reader["ChangeTime"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
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
        public IList<IntegralChangeEntity> GetIntegralChangeAll()
        {

            string sql = @"SELECT    [Id],[MemId],[ChangeNum],[BalanceChangeNum],[AvailableChangeNum],[FreezingChangeNum],[ChangeType],[ChangeReason],[ChangeTime] from dbo.[IntegralChange] WITH(NOLOCK)	";
		    IList<IntegralChangeEntity> entityList = new List<IntegralChangeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   IntegralChangeEntity entity=new IntegralChangeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.ChangeNum=StringUtils.GetDbInt(reader["ChangeNum"]);
					entity.BalanceChangeNum=StringUtils.GetDbInt(reader["BalanceChangeNum"]);
					entity.AvailableChangeNum=StringUtils.GetDbInt(reader["AvailableChangeNum"]);
					entity.FreezingChangeNum=StringUtils.GetDbInt(reader["FreezingChangeNum"]);
					entity.ChangeType=StringUtils.GetDbInt(reader["ChangeType"]);
					entity.ChangeReason=StringUtils.GetDbString(reader["ChangeReason"]);
					entity.ChangeTime=StringUtils.GetDbDateTime(reader["ChangeTime"]);
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
        public int  ExistNum(IntegralChangeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[IntegralChange] WITH(NOLOCK) ";
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
