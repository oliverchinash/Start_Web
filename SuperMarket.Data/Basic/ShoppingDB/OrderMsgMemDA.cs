﻿using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;

/*****************************************
功能描述：OrderMsgMem表的数据访问类。
创建时间：2016/8/4 11:02:01
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// OrderMsgMemEntity的数据访问操作
	/// </summary>
	public partial class OrderMsgMemDA: BaseSuperMarketDB
    {
        #region 实例化
        public static OrderMsgMemDA Instance
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
            internal static readonly OrderMsgMemDA instance = new OrderMsgMemDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表OrderMsgMem，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderMsgMem">待插入的实体对象</param>
		public int AddOrderMsgMem(OrderMsgMemEntity entity)
		{
		   string sql= @"insert into OrderMsgMem( [MemId],[TotalNum],[WaitPayNum],WaitSellerDeliverNum,[WaitReciveNum],[WaitCommentNum],[CancelNum])VALUES
			            ( @MemId,@TotalNum,@WaitPayNum,@WaitSellerDeliverNum,@WaitReciveNum,@WaitCommentNum,@CancelNum);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@TotalNum",  DbType.Int32,entity.TotalNum);
			db.AddInParameter(cmd,"@WaitPayNum",  DbType.Int32,entity.WaitPayNum);
			db.AddInParameter(cmd, "@WaitSellerDeliverNum",  DbType.Int32,entity.WaitSellerDeliverNum);
            db.AddInParameter(cmd,"@WaitReciveNum",  DbType.Int32,entity.WaitReciveNum);
			db.AddInParameter(cmd,"@WaitCommentNum",  DbType.Int32,entity.WaitCommentNum);
			db.AddInParameter(cmd,"@CancelNum",  DbType.Int32,entity.CancelNum);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderMsgMem">待更新的实体对象</param>
		public   int UpdateOrderMsgMem(OrderMsgMemEntity entity)
		{
			string sql= @" UPDATE dbo.[OrderMsgMem] SET
                       [MemId]=@MemId,[TotalNum]=@TotalNum,[WaitPayNum]=@WaitPayNum,[WaitSellerDeliverNum]=@WaitSellerDeliverNum,[WaitReciveNum]=@WaitReciveNum,[WaitCommentNum]=@WaitCommentNum,[CancelNum]=@CancelNum
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@TotalNum",  DbType.Int32,entity.TotalNum);
			db.AddInParameter(cmd,"@WaitPayNum",  DbType.Int32,entity.WaitPayNum);
            db.AddInParameter(cmd, "@WaitSellerDeliverNum", DbType.Int32, entity.WaitSellerDeliverNum);
            db.AddInParameter(cmd,"@WaitReciveNum",  DbType.Int32,entity.WaitReciveNum);
			db.AddInParameter(cmd,"@WaitCommentNum",  DbType.Int32,entity.WaitCommentNum);
			db.AddInParameter(cmd,"@CancelNum",  DbType.Int32,entity.CancelNum);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteOrderMsgMemByKey(int id)
	    {
			string sql=@"delete from OrderMsgMem where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderMsgMemDisabled()
        {
            string sql = @"delete from  OrderMsgMem  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderMsgMemByIds(string ids)
        {
            string sql = @"Delete from OrderMsgMem  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderMsgMemByIds(string ids)
        {
            string sql = @"Update   OrderMsgMem set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   OrderMsgMemEntity GetOrderMsgMem(int id)
		{
			string sql= @"SELECT  [Id],[MemId],[TotalNum],[WaitPayNum],WaitSellerDeliverNum,[WaitReciveNum],[WaitCommentNum],[CancelNum]
							FROM
							dbo.[OrderMsgMem] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		OrderMsgMemEntity entity=new OrderMsgMemEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.TotalNum=StringUtils.GetDbInt(reader["TotalNum"]);
					entity.WaitPayNum=StringUtils.GetDbInt(reader["WaitPayNum"]);
					entity.WaitSellerDeliverNum = StringUtils.GetDbInt(reader["WaitSellerDeliverNum"]); 
                    entity.WaitReciveNum=StringUtils.GetDbInt(reader["WaitReciveNum"]);
					entity.WaitCommentNum=StringUtils.GetDbInt(reader["WaitCommentNum"]);
					entity.CancelNum=StringUtils.GetDbInt(reader["CancelNum"]);
				}
   		    }
            return entity;
		}
        public VWOrderMsgEntity GetVWOrderMsgByMemId(int memid)
        {
            string sql = @"SELECT  [MemId],a.[TotalNum] AS PurchaseTotalNum,[WaitPayNum],[WaitSellerDeliverNum],[WaitReciveNum],[WaitCommentNum]
,[CancelNum]  FROM dbo.[OrderMsgMem]  a WITH(NOLOCK)  WHERE a.[MemId]=@MemId";

            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            VWOrderMsgEntity entity = new VWOrderMsgEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                { 
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]); 
                    entity.PurchaseTotalNum = StringUtils.GetDbInt(reader["PurchaseTotalNum"]); 
                    entity.WaitPayNum = StringUtils.GetDbInt(reader["WaitPayNum"]); 
                    entity.WaitSellerDeliverNum = StringUtils.GetDbInt(reader["WaitSellerDeliverNum"]);  
                    entity.WaitReciveNum = StringUtils.GetDbInt(reader["WaitReciveNum"]); 
                    entity.WaitCommentNum = StringUtils.GetDbInt(reader["WaitCommentNum"]); 
                    entity.CancelNum = StringUtils.GetDbInt(reader["CancelNum"]);  
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<OrderMsgMemEntity> GetOrderMsgMemList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[MemId],[TotalNum],[WaitPayNum],WaitSellerDeliverNum,[WaitReciveNum],[WaitCommentNum],[CancelNum]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[TotalNum],[WaitPayNum],WaitSellerDeliverNum,[WaitReciveNum],[WaitCommentNum],[CancelNum] from dbo.[OrderMsgMem] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[OrderMsgMem] with (nolock) ";
            IList<OrderMsgMemEntity> entityList = new List< OrderMsgMemEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					OrderMsgMemEntity entity=new OrderMsgMemEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.TotalNum=StringUtils.GetDbInt(reader["TotalNum"]);
					entity.WaitPayNum=StringUtils.GetDbInt(reader["WaitPayNum"]);
                    entity.WaitSellerDeliverNum = StringUtils.GetDbInt(reader["WaitSellerDeliverNum"]);
                    entity.WaitReciveNum=StringUtils.GetDbInt(reader["WaitReciveNum"]);
					entity.WaitCommentNum=StringUtils.GetDbInt(reader["WaitCommentNum"]);
					entity.CancelNum=StringUtils.GetDbInt(reader["CancelNum"]);
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
        public IList<OrderMsgMemEntity> GetOrderMsgMemAll()
        {

            string sql = @"SELECT    [Id],[MemId],[TotalNum],[WaitPayNum],WaitSellerDeliverNum,[WaitReciveNum],[WaitCommentNum],[CancelNum] from dbo.[OrderMsgMem] WITH(NOLOCK)	";
		    IList<OrderMsgMemEntity> entityList = new List<OrderMsgMemEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   OrderMsgMemEntity entity=new OrderMsgMemEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.TotalNum=StringUtils.GetDbInt(reader["TotalNum"]);
					entity.WaitPayNum=StringUtils.GetDbInt(reader["WaitPayNum"]);  
					entity.WaitSellerDeliverNum = StringUtils.GetDbInt(reader["WaitSellerDeliverNum"]); 

                    entity.WaitReciveNum=StringUtils.GetDbInt(reader["WaitReciveNum"]);
					entity.WaitCommentNum=StringUtils.GetDbInt(reader["WaitCommentNum"]);
					entity.CancelNum=StringUtils.GetDbInt(reader["CancelNum"]);
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
        public int  ExistNum(OrderMsgMemEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderMsgMem] WITH(NOLOCK) ";
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
