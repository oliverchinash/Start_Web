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
功能描述：SMSNoticeLog表的数据访问类。
创建时间：2017/1/18 14:05:14
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// SMSNoticeLogEntity的数据访问操作
	/// </summary>
	public partial class SMSNoticeLogDA: BaseSuperMarketDB
    {
        #region 实例化
        public static SMSNoticeLogDA Instance
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
            internal static readonly SMSNoticeLogDA instance = new SMSNoticeLogDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表SMSNoticeLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sMSNoticeLog">待插入的实体对象</param>
		public int AddSMSNoticeLog(SMSNoticeLogEntity entity)
		{
		   string sql= @"insert into SMSNoticeLog( [LogId],[MobilePhone],[SMSContent],[Status],[CreateTime],[SendTime],[SystemType],[ReturnMsg],SendProvider)VALUES
			            ( @LogId,@MobilePhone,@SMSContent,@Status,@CreateTime,@SendTime,@SystemType,@ReturnMsg,@SendProvider);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@LogId",  DbType.Int32,entity.LogId);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@SMSContent",  DbType.String,entity.SMSContent);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@SendTime",  DbType.DateTime,entity.SendTime);
			db.AddInParameter(cmd,"@SystemType",  DbType.Int32,entity.SystemType);
			db.AddInParameter(cmd,"@ReturnMsg",  DbType.String,entity.ReturnMsg);
			db.AddInParameter(cmd, "@SendProvider",  DbType.String,entity.SendProvider);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sMSNoticeLog">待更新的实体对象</param>
		public   int UpdateSMSNoticeLog(SMSNoticeLogEntity entity)
		{
			string sql=@" UPDATE dbo.[SMSNoticeLog] SET
                       [LogId]=@LogId,[MobilePhone]=@MobilePhone,[SMSContent]=@SMSContent,[Status]=@Status,[CreateTime]=@CreateTime,[SendTime]=@SendTime,[SystemType]=@SystemType,[ReturnMsg]=@ReturnMsg
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@LogId",  DbType.Int32,entity.LogId);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@SMSContent",  DbType.String,entity.SMSContent);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@SendTime",  DbType.DateTime,entity.SendTime);
			db.AddInParameter(cmd,"@SystemType",  DbType.Int32,entity.SystemType);
			db.AddInParameter(cmd,"@ReturnMsg",  DbType.String,entity.ReturnMsg);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteSMSNoticeLogByKey(int id)
	    {
			string sql=@"delete from SMSNoticeLog where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSMSNoticeLogDisabled()
        {
            string sql = @"delete from  SMSNoticeLog  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSMSNoticeLogByIds(string ids)
        {
            string sql = @"Delete from SMSNoticeLog  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSMSNoticeLogByIds(string ids)
        {
            string sql = @"Update   SMSNoticeLog set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   SMSNoticeLogEntity GetSMSNoticeLog(int id)
		{
			string sql=@"SELECT  [Id],[LogId],[MobilePhone],[SMSContent],[Status],[CreateTime],[SendTime],[SystemType],[ReturnMsg]
							FROM
							dbo.[SMSNoticeLog] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		SMSNoticeLogEntity entity=new SMSNoticeLogEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.LogId=StringUtils.GetDbInt(reader["LogId"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.SMSContent=StringUtils.GetDbString(reader["SMSContent"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
					entity.ReturnMsg=StringUtils.GetDbString(reader["ReturnMsg"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<SMSNoticeLogEntity> GetSMSNoticeLogList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[LogId],[MobilePhone],[SMSContent],[Status],[CreateTime],[SendTime],[SystemType],[ReturnMsg]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[LogId],[MobilePhone],[SMSContent],[Status],[CreateTime],[SendTime],[SystemType],[ReturnMsg] from dbo.[SMSNoticeLog] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[SMSNoticeLog] with (nolock) ";
            IList<SMSNoticeLogEntity> entityList = new List< SMSNoticeLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					SMSNoticeLogEntity entity=new SMSNoticeLogEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.LogId=StringUtils.GetDbInt(reader["LogId"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.SMSContent=StringUtils.GetDbString(reader["SMSContent"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
					entity.ReturnMsg=StringUtils.GetDbString(reader["ReturnMsg"]);
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
        public IList<SMSNoticeLogEntity> GetSMSNoticeLogAll()
        {

            string sql = @"SELECT    [Id],[LogId],[MobilePhone],[SMSContent],[Status],[CreateTime],[SendTime],[SystemType],[ReturnMsg] from dbo.[SMSNoticeLog] WITH(NOLOCK)	";
		    IList<SMSNoticeLogEntity> entityList = new List<SMSNoticeLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   SMSNoticeLogEntity entity=new SMSNoticeLogEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.LogId=StringUtils.GetDbInt(reader["LogId"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.SMSContent=StringUtils.GetDbString(reader["SMSContent"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
					entity.ReturnMsg=StringUtils.GetDbString(reader["ReturnMsg"]);
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
        public int  ExistNum(SMSNoticeLogEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[SMSNoticeLog] WITH(NOLOCK) ";
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
