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
功能描述：InQuiryOrderFlow表的数据访问类。
创建时间：2017/8/23 11:12:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// InQuiryOrderFlowEntity的数据访问操作
	/// </summary>
	public partial class InQuiryOrderFlowDA: BaseSuperMarketDB
    {
        #region 实例化
        public static InQuiryOrderFlowDA Instance
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
            internal static readonly InQuiryOrderFlowDA instance = new InQuiryOrderFlowDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表InQuiryOrderFlow，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inQuiryOrderFlow">待插入的实体对象</param>
		public int AddInQuiryOrderFlow(InQuiryOrderFlowEntity entity)
		{
		   string sql=@"insert into InQuiryOrderFlow( [Id],[InquiryOrderCode],[Remark],[MethodName],[CreateManId],[CreateTime])VALUES
			            ( @Id,@InquiryOrderCode,@Remark,@MethodName,@CreateManId,@CreateTime)";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@MethodName",  DbType.String,entity.MethodName);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			return db.ExecuteNonQuery(cmd);
 			
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inQuiryOrderFlow">待更新的实体对象</param>
		public   int UpdateInQuiryOrderFlow(InQuiryOrderFlowEntity entity)
		{
			string sql=@" UPDATE dbo.[InQuiryOrderFlow] SET
                       [Id]=@Id,[InquiryOrderCode]=@InquiryOrderCode,[Remark]=@Remark,[MethodName]=@MethodName,[CreateManId]=@CreateManId,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@MethodName",  DbType.String,entity.MethodName);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteInQuiryOrderFlowByKey(int id)
	    {
			string sql=@"delete from InQuiryOrderFlow where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInQuiryOrderFlowDisabled()
        {
            string sql = @"delete from  InQuiryOrderFlow  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInQuiryOrderFlowByIds(string ids)
        {
            string sql = @"Delete from InQuiryOrderFlow  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInQuiryOrderFlowByIds(string ids)
        {
            string sql = @"Update   InQuiryOrderFlow set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   InQuiryOrderFlowEntity GetInQuiryOrderFlow(int id)
		{
			string sql=@"SELECT  [Id],[InquiryOrderCode],[Remark],[MethodName],[CreateManId],[CreateTime]
							FROM
							dbo.[InQuiryOrderFlow] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		InQuiryOrderFlowEntity entity=new InQuiryOrderFlowEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.MethodName=StringUtils.GetDbString(reader["MethodName"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<InQuiryOrderFlowEntity> GetInQuiryOrderFlowList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[InquiryOrderCode],[Remark],[MethodName],[CreateManId],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[InquiryOrderCode],[Remark],[MethodName],[CreateManId],[CreateTime] from dbo.[InQuiryOrderFlow] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[InQuiryOrderFlow] with (nolock) ";
            IList<InQuiryOrderFlowEntity> entityList = new List< InQuiryOrderFlowEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					InQuiryOrderFlowEntity entity=new InQuiryOrderFlowEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.MethodName=StringUtils.GetDbString(reader["MethodName"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<InQuiryOrderFlowEntity> GetInQuiryOrderFlowAll()
        {

            string sql = @"SELECT    [Id],[InquiryOrderCode],[Remark],[MethodName],[CreateManId],[CreateTime] from dbo.[InQuiryOrderFlow] WITH(NOLOCK)	";
		    IList<InQuiryOrderFlowEntity> entityList = new List<InQuiryOrderFlowEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   InQuiryOrderFlowEntity entity=new InQuiryOrderFlowEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.MethodName=StringUtils.GetDbString(reader["MethodName"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(InQuiryOrderFlowEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[InQuiryOrderFlow] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (MethodName=@MethodName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (MethodName=@MethodName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@MethodName", DbType.String, entity.MethodName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
