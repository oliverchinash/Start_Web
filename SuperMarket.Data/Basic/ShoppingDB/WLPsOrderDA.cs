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
功能描述：WLPsOrder表的数据访问类。
创建时间：2016/9/19 11:39:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// WLPsOrderEntity的数据访问操作
	/// </summary>
	public partial class WLPsOrderDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WLPsOrderDA Instance
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
            internal static readonly WLPsOrderDA instance = new WLPsOrderDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WLPsOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="wLPsOrder">待插入的实体对象</param>
		public int AddWLPsOrder(WLPsOrderEntity entity)
		{
		   string sql= @"insert into WLPsOrder( [OrderId],[OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId])VALUES
			            (@OrderId,@OrderCode,@ConsignCode,@TransferCode,@PsBillCode,@OutTime,@PsResult,@CreateManId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd, "@OrderId",  DbType.Int32,entity.OrderId);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
            db.AddInParameter(cmd,"@ConsignCode",  DbType.String,entity.ConsignCode);
			db.AddInParameter(cmd,"@TransferCode",  DbType.String,entity.TransferCode);
			db.AddInParameter(cmd,"@PsBillCode",  DbType.String,entity.PsBillCode);
			db.AddInParameter(cmd,"@OutTime",  DbType.DateTime,entity.OutTime);
			db.AddInParameter(cmd,"@PsResult",  DbType.Int32,entity.PsResult);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="wLPsOrder">待更新的实体对象</param>
		public   int UpdateWLPsOrder(WLPsOrderEntity entity)
		{
			string sql=@" UPDATE dbo.[WLPsOrder] SET
                       [OrderCode]=@OrderCode,[ConsignCode]=@ConsignCode,[TransferCode]=@TransferCode,[PsBillCode]=@PsBillCode,[OutTime]=@OutTime,[PsResult]=@PsResult,[CreateManId]=@CreateManId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OrderCode",  DbType.String,entity.OrderCode);
			db.AddInParameter(cmd,"@ConsignCode",  DbType.String,entity.ConsignCode);
			db.AddInParameter(cmd,"@TransferCode",  DbType.String,entity.TransferCode);
			db.AddInParameter(cmd,"@PsBillCode",  DbType.String,entity.PsBillCode);
			db.AddInParameter(cmd,"@OutTime",  DbType.DateTime,entity.OutTime);
			db.AddInParameter(cmd,"@PsResult",  DbType.Int32,entity.PsResult);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteWLPsOrderByKey(int id)
	    {
			string sql=@"delete from WLPsOrder where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWLPsOrderDisabled()
        {
            string sql = @"delete from  WLPsOrder  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWLPsOrderByIds(string ids)
        {
            string sql = @"Delete from WLPsOrder  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWLPsOrderByIds(string ids)
        {
            string sql = @"Update   WLPsOrder set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WLPsOrderEntity GetWLPsOrder(int id)
		{
			string sql=@"SELECT  [Id],[OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId]
							FROM
							dbo.[WLPsOrder] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WLPsOrderEntity entity=new WLPsOrderEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.ConsignCode=StringUtils.GetDbString(reader["ConsignCode"]);
					entity.TransferCode=StringUtils.GetDbString(reader["TransferCode"]);
					entity.PsBillCode=StringUtils.GetDbString(reader["PsBillCode"]);
					entity.OutTime=StringUtils.GetDbDateTime(reader["OutTime"]);
					entity.PsResult=StringUtils.GetDbInt(reader["PsResult"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
				}
   		    }
            return entity;
		}
        

        public WLPsOrderEntity GetWLPsOrderByCode(Int64 ordercode)
        {
            string sql = @"SELECT  [Id],[OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId],SendManName,SendManPhone
							FROM
							dbo.[WLPsOrder] WITH(NOLOCK)	
							WHERE [OrderCode]=@OrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            WLPsOrderEntity entity = new WLPsOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]); 
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.ConsignCode = StringUtils.GetDbString(reader["ConsignCode"]);
                    entity.TransferCode = StringUtils.GetDbString(reader["TransferCode"]);
                    entity.PsBillCode = StringUtils.GetDbString(reader["PsBillCode"]);
                    entity.OutTime = StringUtils.GetDbDateTime(reader["OutTime"]);
                    entity.PsResult = StringUtils.GetDbInt(reader["PsResult"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.SendManName = StringUtils.GetDbString(reader["SendManName"]);
                    entity.SendManPhone = StringUtils.GetDbString(reader["SendManPhone"]);
                }
            }
            return entity;
        }

        public WLPsOrderEntity GetWLPsOrderByOrderCode(string ordercode)
        {
            string sql = @"SELECT  [Id],[OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId]
							FROM
							dbo.[WLPsOrder] WITH(NOLOCK)	
							WHERE [OrderCode]=@OrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.String, ordercode);
            WLPsOrderEntity entity = new WLPsOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.ConsignCode = StringUtils.GetDbString(reader["ConsignCode"]);
                    entity.TransferCode = StringUtils.GetDbString(reader["TransferCode"]);
                    entity.PsBillCode = StringUtils.GetDbString(reader["PsBillCode"]);
                    entity.OutTime = StringUtils.GetDbDateTime(reader["OutTime"]);
                    entity.PsResult = StringUtils.GetDbInt(reader["PsResult"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<WLPsOrderEntity> GetWLPsOrderList(int pagesize, int pageindex, ref  int recordCount)
		{
			string sql=@"SELECT   [Id], [OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id], [OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId] from dbo.[WLPsOrder] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WLPsOrder] with (nolock) ";
            IList<WLPsOrderEntity> entityList = new List< WLPsOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WLPsOrderEntity entity=new WLPsOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]); 
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.ConsignCode=StringUtils.GetDbString(reader["ConsignCode"]);
					entity.TransferCode=StringUtils.GetDbString(reader["TransferCode"]);
					entity.PsBillCode=StringUtils.GetDbString(reader["PsBillCode"]);
					entity.OutTime=StringUtils.GetDbDateTime(reader["OutTime"]);
					entity.PsResult=StringUtils.GetDbInt(reader["PsResult"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
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
        public IList<WLPsOrderEntity> GetWLPsOrderAll()
        {

            string sql = @"SELECT    [Id], [OrderCode],[ConsignCode],[TransferCode],[PsBillCode],[OutTime],[PsResult],[CreateManId] from dbo.[WLPsOrder] WITH(NOLOCK)	";
		    IList<WLPsOrderEntity> entityList = new List<WLPsOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WLPsOrderEntity entity=new WLPsOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]); 
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.ConsignCode=StringUtils.GetDbString(reader["ConsignCode"]);
					entity.TransferCode=StringUtils.GetDbString(reader["TransferCode"]);
					entity.PsBillCode=StringUtils.GetDbString(reader["PsBillCode"]);
					entity.OutTime=StringUtils.GetDbDateTime(reader["OutTime"]);
					entity.PsResult=StringUtils.GetDbInt(reader["PsResult"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
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
        public int  ExistNum(WLPsOrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WLPsOrder] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where += " OrderId=@OrderId";
            }
            else
            {
                where += " OrderId=@OrderId and Id<>@Id";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderId", DbType.Int32,entity.OrderId);
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
