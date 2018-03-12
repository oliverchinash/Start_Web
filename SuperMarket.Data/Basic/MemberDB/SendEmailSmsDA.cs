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
功能描述：SendEmailSms表的数据访问类。
创建时间：2016/8/2 14:16:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// SendEmailSmsEntity的数据访问操作
	/// </summary>
	public partial class SendEmailSmsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static SendEmailSmsDA Instance
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
            internal static readonly SendEmailSmsDA instance = new SendEmailSmsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表SendEmailSms，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sendEmailSms">待插入的实体对象</param>
		public int AddSendEmailSms(SendEmailSmsEntity entity)
		{
		   string sql=@"insert into SendEmailSms( [Title],[SendTo],[CreateTime],[CreateBy],[Status],[MemCode],[EmailContent],[PriorIndex])VALUES
			            ( @Title,@SendTo,@CreateTime,@CreateBy,@Status,@MemCode,@EmailContent,@PriorIndex);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@SendTo",  DbType.String,entity.SendTo);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CreateBy",  DbType.String,entity.CreateBy);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@MemCode",  DbType.String,entity.MemCode);
			db.AddInParameter(cmd,"@EmailContent",  DbType.String,entity.EmailContent);
			db.AddInParameter(cmd,"@PriorIndex",  DbType.Int32,entity.PriorIndex);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="sendEmailSms">待更新的实体对象</param>
		public   int UpdateSendEmailSms(SendEmailSmsEntity entity)
		{
			string sql=@" UPDATE dbo.[SendEmailSms] SET
                       [Title]=@Title,[SendTo]=@SendTo,[CreateTime]=@CreateTime,[CreateBy]=@CreateBy,[Status]=@Status,[MemCode]=@MemCode,[EmailContent]=@EmailContent,[PriorIndex]=@PriorIndex
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@SendTo",  DbType.String,entity.SendTo);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CreateBy",  DbType.String,entity.CreateBy);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@MemCode",  DbType.String,entity.MemCode);
			db.AddInParameter(cmd,"@EmailContent",  DbType.String,entity.EmailContent);
			db.AddInParameter(cmd,"@PriorIndex",  DbType.Int32,entity.PriorIndex);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteSendEmailSmsByKey(int id)
	    {
			string sql=@"delete from SendEmailSms where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSendEmailSmsDisabled()
        {
            string sql = @"delete from  SendEmailSms  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSendEmailSmsByIds(string ids)
        {
            string sql = @"Delete from SendEmailSms  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSendEmailSmsByIds(string ids)
        {
            string sql = @"Update   SendEmailSms set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   SendEmailSmsEntity GetSendEmailSms(int id)
		{
			string sql=@"SELECT  [Id],[Title],[SendTo],[CreateTime],[CreateBy],[Status],[MemCode],[EmailContent],[PriorIndex]
							FROM
							dbo.[SendEmailSms] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		SendEmailSmsEntity entity=new SendEmailSmsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.SendTo=StringUtils.GetDbString(reader["SendTo"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateBy=StringUtils.GetDbString(reader["CreateBy"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.MemCode=StringUtils.GetDbString(reader["MemCode"]);
					entity.EmailContent=StringUtils.GetDbString(reader["EmailContent"]);
					entity.PriorIndex=StringUtils.GetDbInt(reader["PriorIndex"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<SendEmailSmsEntity> GetSendEmailSmsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Title],[SendTo],[CreateTime],[CreateBy],[Status],[MemCode],[EmailContent],[PriorIndex]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Title],[SendTo],[CreateTime],[CreateBy],[Status],[MemCode],[EmailContent],[PriorIndex] from dbo.[SendEmailSms] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[SendEmailSms] with (nolock) ";
            IList<SendEmailSmsEntity> entityList = new List< SendEmailSmsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					SendEmailSmsEntity entity=new SendEmailSmsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.SendTo=StringUtils.GetDbString(reader["SendTo"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateBy=StringUtils.GetDbString(reader["CreateBy"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.MemCode=StringUtils.GetDbString(reader["MemCode"]);
					entity.EmailContent=StringUtils.GetDbString(reader["EmailContent"]);
					entity.PriorIndex=StringUtils.GetDbInt(reader["PriorIndex"]);
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
        public IList<SendEmailSmsEntity> GetSendEmailSmsAll()
        {

            string sql = @"SELECT    [Id],[Title],[SendTo],[CreateTime],[CreateBy],[Status],[MemCode],[EmailContent],[PriorIndex] from dbo.[SendEmailSms] WITH(NOLOCK)	";
		    IList<SendEmailSmsEntity> entityList = new List<SendEmailSmsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   SendEmailSmsEntity entity=new SendEmailSmsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.SendTo=StringUtils.GetDbString(reader["SendTo"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateBy=StringUtils.GetDbString(reader["CreateBy"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.MemCode=StringUtils.GetDbString(reader["MemCode"]);
					entity.EmailContent=StringUtils.GetDbString(reader["EmailContent"]);
					entity.PriorIndex=StringUtils.GetDbInt(reader["PriorIndex"]);
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
        public int  ExistNum(SendEmailSmsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[SendEmailSms] WITH(NOLOCK) ";
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
