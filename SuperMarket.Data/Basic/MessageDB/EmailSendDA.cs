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
功能描述：EmailSend表的数据访问类。
创建时间：2017/2/21 22:36:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// EmailSendEntity的数据访问操作
	/// </summary>
	public partial class EmailSendDA: BaseSuperMarketDB
    {
        #region 实例化
        public static EmailSendDA Instance
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
            internal static readonly EmailSendDA instance = new EmailSendDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表EmailSend，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="emailSend">待插入的实体对象</param>
		public int AddEmailSend(EmailSendEntity entity)
		{
		   string sql=@"insert into EmailSend( [Email],[Title],[Body],[CreateTime],[Status])VALUES
			            ( @Email,@Title,@Body,@CreateTime,@Status);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@Body",  DbType.String,entity.Body);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int OrderRemind(string ordercode)
        {
            string sql = @"EXEC [Proc_OrderRemind] @OrderCode";

            IList<BrandEntity> entityList = new List<BrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OrderCode", DbType.String, ordercode); 
           return db.ExecuteNonQuery(cmd); 
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="emailSend">待更新的实体对象</param>
        public   int UpdateEmailSend(EmailSendEntity entity)
		{
			string sql=@" UPDATE dbo.[EmailSend] SET
                       [Email]=@Email,[Title]=@Title,[Body]=@Body,[CreateTime]=@CreateTime,[SendTime]=@SendTime,[Status]=@Status
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Email",  DbType.String,entity.Email);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@Body",  DbType.String,entity.Body);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@SendTime",  DbType.DateTime,entity.SendTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int UpDateSendStatus(int id)
        {
            string sql = @" UPDATE dbo.[EmailSend] SET [SendTime]=getdate(),[Status]=1
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);  
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteEmailSendByKey(int id)
	    {
			string sql=@"delete from EmailSend where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteEmailSendDisabled()
        {
            string sql = @"delete from  EmailSend  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteEmailSendByIds(string ids)
        {
            string sql = @"Delete from EmailSend  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableEmailSendByIds(string ids)
        {
            string sql = @"Update   EmailSend set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   EmailSendEntity GetEmailSend(int id)
		{
			string sql=@"SELECT  [Id],[Email],[Title],[Body],[CreateTime],[SendTime],[Status]
							FROM
							dbo.[EmailSend] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		EmailSendEntity entity=new EmailSendEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.Body=StringUtils.GetDbString(reader["Body"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
				}
   		    }
            return entity;
		}
        public IList<EmailSendEntity> GetEmailWaitSend(int num)
        {
            string sql = @" SELECT top "+ num+@" [Id],[Email],[Title],[Body],[CreateTime],[SendTime],[Status] from dbo.[EmailSend] WITH(NOLOCK)	
						  where Status=0 order by id desc ";
             
            IList<EmailSendEntity> entityList = new List<EmailSendEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    EmailSendEntity entity = new EmailSendEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Email = StringUtils.GetDbString(reader["Email"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Body = StringUtils.GetDbString(reader["Body"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
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
        public   IList<EmailSendEntity> GetEmailSendList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Email],[Title],[Body],[CreateTime],[SendTime],[Status]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Email],[Title],[Body],[CreateTime],[SendTime],[Status] from dbo.[EmailSend] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[EmailSend] with (nolock) ";
            IList<EmailSendEntity> entityList = new List< EmailSendEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					EmailSendEntity entity=new EmailSendEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.Body=StringUtils.GetDbString(reader["Body"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
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
        public IList<EmailSendEntity> GetEmailSendAll()
        {

            string sql = @"SELECT    [Id],[Email],[Title],[Body],[CreateTime],[SendTime],[Status] from dbo.[EmailSend] WITH(NOLOCK)	";
		    IList<EmailSendEntity> entityList = new List<EmailSendEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   EmailSendEntity entity=new EmailSendEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Email=StringUtils.GetDbString(reader["Email"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.Body=StringUtils.GetDbString(reader["Body"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendTime=StringUtils.GetDbDateTime(reader["SendTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
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
        public int  ExistNum(EmailSendEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[EmailSend] WITH(NOLOCK) ";
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
