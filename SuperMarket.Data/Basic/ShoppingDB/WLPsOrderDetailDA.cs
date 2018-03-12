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
功能描述：WLPsOrderDetail表的数据访问类。
创建时间：2016/12/1 21:48:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// WLPsOrderDetailEntity的数据访问操作
	/// </summary>
	public partial class WLPsOrderDetailDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WLPsOrderDetailDA Instance
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
            internal static readonly WLPsOrderDetailDA instance = new WLPsOrderDetailDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WLPsOrderDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="wLPsOrderDetail">待插入的实体对象</param>
		public int AddWLPsOrderDetail(WLPsOrderDetailEntity entity)
		{
		   string sql=@"insert into WLPsOrderDetail( [OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId])VALUES
			            ( @OrderCode,@OrderDetailId,@SendManName,@SendManPhone,@Remark,@TransferCode,@WLCompanyId,@HasBill,@Status,@CreateTime,@CreateManId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@OrderDetailId",  DbType.Int32,entity.OrderDetailId);
			db.AddInParameter(cmd,"@SendManName",  DbType.String,entity.SendManName);
			db.AddInParameter(cmd,"@SendManPhone",  DbType.String,entity.SendManPhone);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@TransferCode",  DbType.String,entity.TransferCode);
			db.AddInParameter(cmd,"@WLCompanyId",  DbType.Int32,entity.WLCompanyId);
			db.AddInParameter(cmd,"@HasBill",  DbType.Int32,entity.HasBill);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
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
		/// <param name="wLPsOrderDetail">待更新的实体对象</param>
		public   int UpdateWLPsOrderDetail(WLPsOrderDetailEntity entity)
		{
			string sql=@" UPDATE dbo.[WLPsOrderDetail] SET
                       [OrderCode]=@OrderCode,[OrderDetailId]=@OrderDetailId,[SendManName]=@SendManName,[SendManPhone]=@SendManPhone,[Remark]=@Remark,[TransferCode]=@TransferCode,[WLCompanyId]=@WLCompanyId,[HasBill]=@HasBill,[Status]=@Status,[CreateTime]=@CreateTime,[CreateManId]=@CreateManId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int64,entity.OrderCode);
			db.AddInParameter(cmd,"@OrderDetailId",  DbType.Int32,entity.OrderDetailId);
			db.AddInParameter(cmd,"@SendManName",  DbType.String,entity.SendManName);
			db.AddInParameter(cmd,"@SendManPhone",  DbType.String,entity.SendManPhone);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@TransferCode",  DbType.String,entity.TransferCode);
			db.AddInParameter(cmd,"@WLCompanyId",  DbType.Int32,entity.WLCompanyId);
			db.AddInParameter(cmd,"@HasBill",  DbType.Int32,entity.HasBill);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteWLPsOrderDetailByKey(int id)
	    {
			string sql=@"delete from WLPsOrderDetail where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWLPsOrderDetailDisabled()
        {
            string sql = @"delete from  WLPsOrderDetail  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWLPsOrderDetailByIds(string ids)
        {
            string sql = @"Delete from WLPsOrderDetail  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWLPsOrderDetailByIds(string ids)
        {
            string sql = @"Update   WLPsOrderDetail set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WLPsOrderDetailEntity GetWLPsOrderDetail(int id)
		{
			string sql=@"SELECT  [Id],[OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId]
							FROM
							dbo.[WLPsOrderDetail] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WLPsOrderDetailEntity entity=new WLPsOrderDetailEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.OrderDetailId=StringUtils.GetDbInt(reader["OrderDetailId"]);
					entity.SendManName=StringUtils.GetDbString(reader["SendManName"]);
					entity.SendManPhone=StringUtils.GetDbString(reader["SendManPhone"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.TransferCode=StringUtils.GetDbString(reader["TransferCode"]);
					entity.WLCompanyId=StringUtils.GetDbInt(reader["WLCompanyId"]);
					entity.HasBill=StringUtils.GetDbInt(reader["HasBill"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
				}
   		    }
            return entity;
		}

        /// <summary>
        /// 根据orderdetailid读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public WLPsOrderDetailEntity GetWLPsOrderDetailByODId(int orderdetailid)
        {
            string sql = @"SELECT  [Id],[OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId]
							FROM
							dbo.[WLPsOrderDetail] WITH(NOLOCK)	
							WHERE [OrderDetailId]=@OrderDetailId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, orderdetailid);
            WLPsOrderDetailEntity entity = new WLPsOrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.SendManName = StringUtils.GetDbString(reader["SendManName"]);
                    entity.SendManPhone = StringUtils.GetDbString(reader["SendManPhone"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.TransferCode = StringUtils.GetDbString(reader["TransferCode"]);
                    entity.WLCompanyId = StringUtils.GetDbInt(reader["WLCompanyId"]);
                    entity.HasBill = StringUtils.GetDbInt(reader["HasBill"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                }
            }
            return entity;
        }

        public WLPsOrderDetailEntity GetWLByDetailId(int detailid)
        {
            string sql = @"SELECT top 1  [Id],[OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId]
							FROM
							dbo.[WLPsOrderDetail] WITH(NOLOCK)	
							WHERE [OrderDetailId]=@OrderDetailId and Status=1";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, detailid);
            WLPsOrderDetailEntity entity = new WLPsOrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.SendManName = StringUtils.GetDbString(reader["SendManName"]);
                    entity.SendManPhone = StringUtils.GetDbString(reader["SendManPhone"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.TransferCode = StringUtils.GetDbString(reader["TransferCode"]);
                    entity.WLCompanyId = StringUtils.GetDbInt(reader["WLCompanyId"]);
                    entity.HasBill = StringUtils.GetDbInt(reader["HasBill"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public   IList<WLPsOrderDetailEntity> GetWLPsOrderDetailList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId] from dbo.[WLPsOrderDetail] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WLPsOrderDetail] with (nolock) ";
            IList<WLPsOrderDetailEntity> entityList = new List< WLPsOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WLPsOrderDetailEntity entity=new WLPsOrderDetailEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.OrderDetailId=StringUtils.GetDbInt(reader["OrderDetailId"]);
					entity.SendManName=StringUtils.GetDbString(reader["SendManName"]);
					entity.SendManPhone=StringUtils.GetDbString(reader["SendManPhone"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.TransferCode=StringUtils.GetDbString(reader["TransferCode"]);
					entity.WLCompanyId=StringUtils.GetDbInt(reader["WLCompanyId"]);
					entity.HasBill=StringUtils.GetDbInt(reader["HasBill"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<WLPsOrderDetailEntity> GetWLPsOrderDetailAll()
        {

            string sql = @"SELECT    [Id],[OrderCode],[OrderDetailId],[SendManName],[SendManPhone],[Remark],[TransferCode],[WLCompanyId],[HasBill],[Status],[CreateTime],[CreateManId] from dbo.[WLPsOrderDetail] WITH(NOLOCK)	";
		    IList<WLPsOrderDetailEntity> entityList = new List<WLPsOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WLPsOrderDetailEntity entity=new WLPsOrderDetailEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbLong(reader["OrderCode"]);
					entity.OrderDetailId=StringUtils.GetDbInt(reader["OrderDetailId"]);
					entity.SendManName=StringUtils.GetDbString(reader["SendManName"]);
					entity.SendManPhone=StringUtils.GetDbString(reader["SendManPhone"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.TransferCode=StringUtils.GetDbString(reader["TransferCode"]);
					entity.WLCompanyId=StringUtils.GetDbInt(reader["WLCompanyId"]);
					entity.HasBill=StringUtils.GetDbInt(reader["HasBill"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(WLPsOrderDetailEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WLPsOrderDetail] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (TransferCode=@TransferCode) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (TransferCode=@TransferCode) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@TransferCode", DbType.String, entity.TransferCode); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
