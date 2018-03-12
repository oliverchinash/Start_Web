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
功能描述：InquiryRecords表的数据访问类。
创建时间：2017/7/4 15:23:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CommentDB 
{
	/// <summary>
	/// InquiryRecordsEntity的数据访问操作
	/// </summary>
	public partial class InquiryRecordsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static InquiryRecordsDA Instance
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
            internal static readonly InquiryRecordsDA instance = new InquiryRecordsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表InquiryRecords，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inquiryRecords">待插入的实体对象</param>
		public int AddInquiryRecords(InquiryRecordsEntity entity)
		{
		   string sql=@"insert into InquiryRecords( [MemId],[MobilePhone],[Remark],[ProductDetailId],[CreatTime],[Status],[ReplyContent])VALUES
			            ( @MemId,@MobilePhone,@Remark,@ProductDetailId,@CreatTime,@Status,@ReplyContent);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@CreatTime",  DbType.DateTime,entity.CreatTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@ReplyContent",  DbType.String,entity.ReplyContent);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inquiryRecords">待更新的实体对象</param>
		public   int UpdateInquiryRecords(InquiryRecordsEntity entity)
		{
			string sql=@" UPDATE dbo.[InquiryRecords] SET
                       [MemId]=@MemId,[MobilePhone]=@MobilePhone,[Remark]=@Remark,[ProductDetailId]=@ProductDetailId,[CreatTime]=@CreatTime,[Status]=@Status,[ReplyContent]=@ReplyContent
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@CreatTime",  DbType.DateTime,entity.CreatTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@ReplyContent",  DbType.String,entity.ReplyContent);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteInquiryRecordsByKey(int id)
	    {
			string sql=@"delete from InquiryRecords where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryRecordsDisabled()
        {
            string sql = @"delete from  InquiryRecords  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryRecordsByIds(string ids)
        {
            string sql = @"Delete from InquiryRecords  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryRecordsByIds(string ids)
        {
            string sql = @"Update   InquiryRecords set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   InquiryRecordsEntity GetInquiryRecords(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[MobilePhone],[Remark],[ProductDetailId],[CreatTime],[Status],[ReplyContent]
							FROM
							dbo.[InquiryRecords] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		InquiryRecordsEntity entity=new InquiryRecordsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.CreatTime=StringUtils.GetDbDateTime(reader["CreatTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ReplyContent=StringUtils.GetDbString(reader["ReplyContent"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<InquiryRecordsEntity> GetInquiryRecordsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[MobilePhone],[Remark],[ProductDetailId],[CreatTime],[Status],[ReplyContent]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[MobilePhone],[Remark],[ProductDetailId],[CreatTime],[Status],[ReplyContent] from dbo.[InquiryRecords] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[InquiryRecords] with (nolock) ";
            IList<InquiryRecordsEntity> entityList = new List< InquiryRecordsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					InquiryRecordsEntity entity=new InquiryRecordsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.CreatTime=StringUtils.GetDbDateTime(reader["CreatTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ReplyContent=StringUtils.GetDbString(reader["ReplyContent"]);
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
        public IList<InquiryRecordsEntity> GetInquiryRecordsAll()
        {

            string sql = @"SELECT    [Id],[MemId],[MobilePhone],[Remark],[ProductDetailId],[CreatTime],[Status],[ReplyContent] from dbo.[InquiryRecords] WITH(NOLOCK)	";
		    IList<InquiryRecordsEntity> entityList = new List<InquiryRecordsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   InquiryRecordsEntity entity=new InquiryRecordsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.CreatTime=StringUtils.GetDbDateTime(reader["CreatTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ReplyContent=StringUtils.GetDbString(reader["ReplyContent"]);
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
        public int  ExistNum(InquiryRecordsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[InquiryRecords] WITH(NOLOCK) ";
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
