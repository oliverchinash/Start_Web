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
功能描述：SuggestDealLog表的数据访问类。
创建时间：2016/8/1 14:58:49
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// SuggestDealLogEntity的数据访问操作
	/// </summary>
	public partial class SuggestDealLogDA: BaseSuperMarketDB
    {
        #region 实例化
        public static SuggestDealLogDA Instance
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
            internal static readonly SuggestDealLogDA instance = new SuggestDealLogDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表SuggestDealLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="suggestDealLog">待插入的实体对象</param>
		public int AddSuggestDealLog(SuggestDealLogEntity entity)
		{
		   string sql=@"insert into SuggestDealLog( [SuggestId],[DealManId],[DealContent],[DealTime])VALUES
			            ( @SuggestId,@DealManId,@DealContent,@DealTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@SuggestId",  DbType.Int32,entity.SuggestId);
			db.AddInParameter(cmd,"@DealManId",  DbType.Int32,entity.DealManId);
			db.AddInParameter(cmd,"@DealContent",  DbType.String,entity.DealContent);
			db.AddInParameter(cmd,"@DealTime",  DbType.DateTime,entity.DealTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="suggestDealLog">待更新的实体对象</param>
		public   int UpdateSuggestDealLog(SuggestDealLogEntity entity)
		{
			string sql=@" UPDATE dbo.[SuggestDealLog] SET
                       [SuggestId]=@SuggestId,[DealManId]=@DealManId,[DealContent]=@DealContent,[DealTime]=@DealTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@SuggestId",  DbType.Int32,entity.SuggestId);
			db.AddInParameter(cmd,"@DealManId",  DbType.Int32,entity.DealManId);
			db.AddInParameter(cmd,"@DealContent",  DbType.String,entity.DealContent);
			db.AddInParameter(cmd,"@DealTime",  DbType.DateTime,entity.DealTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteSuggestDealLogByKey(int id)
	    {
			string sql=@"delete from SuggestDealLog where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSuggestDealLogDisabled()
        {
            string sql = @"delete from  SuggestDealLog  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSuggestDealLogByIds(string ids)
        {
            string sql = @"Delete from SuggestDealLog  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSuggestDealLogByIds(string ids)
        {
            string sql = @"Update   SuggestDealLog set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   SuggestDealLogEntity GetSuggestDealLog(int id)
		{
			string sql=@"SELECT  [Id],[SuggestId],[DealManId],[DealContent],[DealTime]
							FROM
							dbo.[SuggestDealLog] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		SuggestDealLogEntity entity=new SuggestDealLogEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SuggestId=StringUtils.GetDbInt(reader["SuggestId"]);
					entity.DealManId=StringUtils.GetDbInt(reader["DealManId"]);
					entity.DealContent=StringUtils.GetDbString(reader["DealContent"]);
					entity.DealTime=StringUtils.GetDbDateTime(reader["DealTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<SuggestDealLogEntity> GetSuggestDealLogList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[SuggestId],[DealManId],[DealContent],[DealTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[SuggestId],[DealManId],[DealContent],[DealTime] from dbo.[SuggestDealLog] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[SuggestDealLog] with (nolock) ";
            IList<SuggestDealLogEntity> entityList = new List< SuggestDealLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					SuggestDealLogEntity entity=new SuggestDealLogEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SuggestId=StringUtils.GetDbInt(reader["SuggestId"]);
					entity.DealManId=StringUtils.GetDbInt(reader["DealManId"]);
					entity.DealContent=StringUtils.GetDbString(reader["DealContent"]);
					entity.DealTime=StringUtils.GetDbDateTime(reader["DealTime"]);
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
        public IList<SuggestDealLogEntity> GetSuggestDealLogAll()
        {

            string sql = @"SELECT    [Id],[SuggestId],[DealManId],[DealContent],[DealTime] from dbo.[SuggestDealLog] WITH(NOLOCK)	";
		    IList<SuggestDealLogEntity> entityList = new List<SuggestDealLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   SuggestDealLogEntity entity=new SuggestDealLogEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SuggestId=StringUtils.GetDbInt(reader["SuggestId"]);
					entity.DealManId=StringUtils.GetDbInt(reader["DealManId"]);
					entity.DealContent=StringUtils.GetDbString(reader["DealContent"]);
					entity.DealTime=StringUtils.GetDbDateTime(reader["DealTime"]);
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
        public int  ExistNum(SuggestDealLogEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[SuggestDealLog] WITH(NOLOCK) ";
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
