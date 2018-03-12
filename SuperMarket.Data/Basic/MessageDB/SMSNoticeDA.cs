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
功能描述：SMSNotice表的数据访问类。
创建时间：2017/1/18 11:56:51
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// SMSNoticeEntity的数据访问操作
	/// </summary>
	public partial class SMSNoticeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static SMSNoticeDA Instance
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
            internal static readonly SMSNoticeDA instance = new SMSNoticeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表SMSNotice，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sMSNotice">待插入的实体对象</param>
		public int AddSMSNotice(SMSNoticeEntity entity)
		{
		   string sql= @"insert into SMSNotice( [CreateTime],[MobilePhone],[SMSContent],[Status],[SystemType])VALUES
			            ( getdate(),@MobilePhone,@SMSContent,@Status,@SystemType);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@SMSContent",  DbType.String,entity.SMSContent);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status); 
            db.AddInParameter(cmd,"@SystemType",  DbType.Int32,entity.SystemType);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddSMSNoticeByPhone(string phone, string body)
        {
            string sql = @"insert into SMSNotice( [CreateTime],[MobilePhone],[SMSContent],[Status],[SystemType])VALUES
			            ( getdate(),@MobilePhone,@SMSContent,0,@SystemType);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MobilePhone", DbType.String, phone);
            db.AddInParameter(cmd, "@SMSContent", DbType.String, body); 
            db.AddInParameter(cmd, "@SystemType", DbType.Int32,  (int)SystemType.Purchase);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="sMSNotice">待更新的实体对象</param>
        public   int UpdateSMSNotice(SMSNoticeEntity entity)
		{
			string sql= @" UPDATE dbo.[SMSNotice] SET
                       [MobilePhone]=@MobilePhone,[SMSContent]=@SMSContent,[Status]=@Status,[SendTime]=@SendTime,[SystemType]=@SystemType,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@SMSContent",  DbType.String,entity.SMSContent);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@SendTime",  DbType.DateTime,entity.SendTime);
			db.AddInParameter(cmd,"@SystemType",  DbType.Int32,entity.SystemType);
			db.AddInParameter(cmd, "@CreateTime",  DbType.DateTime,entity.CreateTime);
            return db.ExecuteNonQuery(cmd);
		}
        public int SMSNoticeSendAccess(SMSNoticeEntity entity)
        {
            string sql = @" UPDATE dbo.[SMSNotice] SET [Status]=@Status,[SendTime]=getdate() ,ReturnMsg=@ReturnMsg,SendProvider=@SendProvider
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id); 
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)SMSNoticeStatus.HasSend); 
            db.AddInParameter(cmd, "@SendProvider", DbType.String, entity.SendProvider);
            db.AddInParameter(cmd, "@ReturnMsg", DbType.String, entity.ReturnMsg);
            return db.ExecuteNonQuery(cmd);
        }
        public int SMSNoticeSendError(int id, string msg)
        {
            string sql = @" UPDATE dbo.[SMSNotice] SET [Status]=@Status,[SendTime]=getdate() ,ReturnMsg=@ReturnMsg
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)SMSNoticeStatus.SendError);
            db.AddInParameter(cmd, "@ReturnMsg", DbType.String, msg);
            return db.ExecuteNonQuery(cmd);
        }
        
        public int SMSNoticeAdd(string mobile, string msg,int systemtype)
        {
            string sql = @"insert into SMSNotice( [CreateTime],[MobilePhone],[SMSContent],[Status],[SystemType])VALUES
			            ( getdate(),@MobilePhone,@SMSContent,0,@SystemType);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, mobile);
            db.AddInParameter(cmd, "@SMSContent", DbType.String, msg);   
            db.AddInParameter(cmd, "@SystemType", DbType.Int32, systemtype);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int  DeleteSMSNoticeByKey(int id)
	    {
			string sql=@"delete from SMSNotice where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSMSNoticeDisabled()
        {
            string sql = @"delete from  SMSNotice  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSMSNoticeByIds(string ids)
        {
            string sql = @"Delete from SMSNotice  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSMSNoticeByIds(string ids)
        {
            string sql = @"Update   SMSNotice set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   SMSNoticeEntity GetSMSNotice(int id)
		{
			string sql=@"SELECT  [Id],[MobilePhone],[SMSContent],[Status],[SendTime],[SystemType]
							FROM
							dbo.[SMSNotice] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		SMSNoticeEntity entity=new SMSNoticeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.SMSContent=StringUtils.GetDbString(reader["SMSContent"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<SMSNoticeEntity> GetSMSNoticeList(int pagesize, int pageindex, ref  int recordCount,int systemType,int status)
		{
            string where = " WHERE  1=1 ";

            if (systemType > 0)
            {
                where += " And SystemType=@SystemType";
            }

            if (status > -1)
            {
                where += " And Status=@Status";
            }

			string sql= @"SELECT   [Id],[MobilePhone],[SMSContent],[Status],[SendTime],[SystemType],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MobilePhone],[SMSContent],[Status],[SendTime],[SystemType],[CreateTime] from dbo.[SMSNotice] WITH(NOLOCK)	
						" + where+ @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[SMSNotice] with (nolock) "+where ;
            IList<SMSNoticeEntity> entityList = new List< SMSNoticeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (systemType>0)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, systemType);
            }

            if (status>-1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					SMSNoticeEntity entity=new SMSNoticeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.SMSContent=StringUtils.GetDbString(reader["SMSContent"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (systemType > 0)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, systemType);
            }

            if (status > -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
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
        public IList<SMSNoticeEntity> GetSMSList(int num,int status)
        {
            string sql = @" SELECT top "+ num + @" [Id],[MobilePhone],[SMSContent],[Status],[SendTime],[SystemType] from dbo.[SMSNotice] WITH(NOLOCK)	
						WHERE Status= @Status ";
             
            IList<SMSNoticeEntity> entityList = new List<SMSNoticeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status); 

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SMSNoticeEntity entity = new SMSNoticeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.SMSContent = StringUtils.GetDbString(reader["SMSContent"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<SMSNoticeEntity> GetSMSWaitSend(int Num_i)
        {
            string sql = @" SELECT  top "+ Num_i + @" [Id],[MobilePhone],[SMSContent],[Status],[SendTime],[SystemType],CreateTime from dbo.[SMSNotice] WITH(NOLOCK)	
						WHERE Status= @Status  order by id desc";

            IList<SMSNoticeEntity> entityList = new List<SMSNoticeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)SMSNoticeStatus.WaitSend);
            //db.AddInParameter(cmd, "@Num", DbType.Int32, Num_i);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    SMSNoticeEntity entity = new SMSNoticeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.SMSContent = StringUtils.GetDbString(reader["SMSContent"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]); 
                    entity.CreateTime = StringUtils.GetDateTime(reader["CreateTime"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<SMSNoticeEntity> GetSMSNoticeAll()
        {

            string sql = @"SELECT    [Id],[MobilePhone],[SMSContent],[Status],[SendTime],[SystemType] from dbo.[SMSNotice] WITH(NOLOCK)	";
		    IList<SMSNoticeEntity> entityList = new List<SMSNoticeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   SMSNoticeEntity entity=new SMSNoticeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.SMSContent=StringUtils.GetDbString(reader["SMSContent"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.SystemType=StringUtils.GetDbInt(reader["SystemType"]);
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
        public int  ExistNum(SMSNoticeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[SMSNotice] WITH(NOLOCK) ";
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
