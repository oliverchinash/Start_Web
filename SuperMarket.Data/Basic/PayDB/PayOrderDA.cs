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
功能描述：PayOrder表的数据访问类。
创建时间：2017/11/16 8:34:30
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.PayDB
{
	/// <summary>
	/// PayOrderEntity的数据访问操作
	/// </summary>
	public partial class PayOrderDA: BaseSuperMarketDB
    {
        #region 实例化
        public static PayOrderDA Instance
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
            internal static readonly PayOrderDA instance = new PayOrderDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表PayOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payOrder">待插入的实体对象</param>
		public int AddPayOrder(PayOrderEntity entity)
		{
		   string sql= @"insert into PayOrder( [PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],ExternalCode )VALUES
			            ( @PayOrderCode,@SysOrderCode,@SysType,@NeedPayPrice,@PayMethod,@CreateTime,@CreateManId,@Status,@ExternalCode );
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@PayOrderCode",  DbType.String,entity.PayOrderCode);
			db.AddInParameter(cmd,"@SysOrderCode",  DbType.String,entity.SysOrderCode);
			db.AddInParameter(cmd,"@SysType",  DbType.Int32,entity.SysType);
			db.AddInParameter(cmd,"@NeedPayPrice",  DbType.Decimal,entity.NeedPayPrice);
			db.AddInParameter(cmd,"@PayMethod",  DbType.Int32,entity.PayMethod);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status); 
			db.AddInParameter(cmd, "@ExternalCode",  DbType.String, entity.ExternalCode);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payOrder">待更新的实体对象</param>
		public   int UpdatePayOrder(PayOrderEntity entity)
		{
			string sql=@" UPDATE dbo.[PayOrder] SET
                       [PayOrderCode]=@PayOrderCode,[SysOrderCode]=@SysOrderCode,[SysType]=@SysType,[NeedPayPrice]=@NeedPayPrice,[PayMethod]=@PayMethod,[CreateTime]=@CreateTime,[CreateManId]=@CreateManId,[Status]=@Status,[PayTime]=@PayTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@PayOrderCode",  DbType.String,entity.PayOrderCode);
			db.AddInParameter(cmd,"@SysOrderCode",  DbType.String,entity.SysOrderCode);
			db.AddInParameter(cmd,"@SysType",  DbType.Int32,entity.SysType);
			db.AddInParameter(cmd,"@NeedPayPrice",  DbType.Decimal,entity.NeedPayPrice);
			db.AddInParameter(cmd,"@PayMethod",  DbType.Int32,entity.PayMethod);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@PayTime",  DbType.DateTime,entity.PayTime);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int PayTypeUpdate(string paycode, int paytype,int memid)
        {
            string sql = @" UPDATE dbo.[PayOrder] SET PayMethod=@PayMethod 
                       WHERE [PayOrderCode]=@PayOrderCode and Status=0  and CreateManId=@CreateManId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);  
            db.AddInParameter(cmd, "@PayMethod", DbType.Int32, paytype);
            db.AddInParameter(cmd, "@PayOrderCode", DbType.String, paycode);
            db.AddInParameter(cmd, "@CreateManId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }

        public int RecivedPaySuccess(VWPayOrderEntity entity)
        {
            string sql = @" UPDATE dbo.[PayOrder] SET
                      [Status]=@Status,[PayTime]=@PayTime,ExternalCode=@ExternalCode,PayPrice=@PayPrice
                       WHERE [Id]=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);  
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@PayTime", DbType.DateTime, entity.PayTime);
            db.AddInParameter(cmd, "@PayPrice", DbType.Decimal, entity.PayPrice);
            db.AddInParameter(cmd, "@ExternalCode", DbType.String, entity.ExternalCode);
            return db.ExecuteNonQuery(cmd);
        }
        
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int  DeletePayOrderByKey(int id)
	    {
			string sql=@"delete from PayOrder where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayOrderDisabled()
        {
            string sql = @"delete from  PayOrder  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayOrderByIds(string ids)
        {
            string sql = @"Delete from PayOrder  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayOrderByIds(string ids)
        {
            string sql = @"Update   PayOrder set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   PayOrderEntity GetPayOrder(int id)
		{
			string sql=@"SELECT  [Id],[PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],[PayTime]
							FROM
							dbo.[PayOrder] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		PayOrderEntity entity=new PayOrderEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.PayOrderCode=StringUtils.GetDbString(reader["PayOrderCode"]);;
					entity.SysOrderCode=StringUtils.GetDbString(reader["SysOrderCode"]);;
					entity.SysType=StringUtils.GetDbInt(reader["SysType"]);;
					entity.NeedPayPrice=StringUtils.GetDbDecimal(reader["NeedPayPrice"]);;
					entity.PayMethod=StringUtils.GetDbInt(reader["PayMethod"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
					entity.PayTime=StringUtils.GetDbDateTime(reader["PayTime"]);;
				}
   		    }
            return entity;
		}

        public VWPayOrderEntity GetVWPayOrderByPayCode(string payordercode)
        {
            string sql = @"SELECT  [Id],[PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],[PayTime],ExternalCode,PayPrice
							FROM
							dbo.[PayOrder] WITH(NOLOCK)	
							WHERE [PayOrderCode]=@PayOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PayOrderCode", DbType.String, payordercode);
            VWPayOrderEntity entity = new VWPayOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.PayOrderCode = StringUtils.GetDbString(reader["PayOrderCode"]);
                    entity.SysOrderCode = StringUtils.GetDbString(reader["SysOrderCode"]);
                    entity.SysType = StringUtils.GetDbInt(reader["SysType"]);
                    entity.NeedPayPrice = StringUtils.GetDbDecimal(reader["NeedPayPrice"]);
                    entity.PayMethod = StringUtils.GetDbInt(reader["PayMethod"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.ExternalCode = StringUtils.GetDbString(reader["ExternalCode"]);  
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]); 
                }
            }
            return entity;
        }

        public  PayOrderEntity GetPayOrderBySysCode(int systype, string syscode,int paytype)
        {
            string sql = @"SELECT  [Id],[PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],[PayTime]
							FROM
							dbo.[PayOrder] WITH(NOLOCK)	
							WHERE [SysOrderCode]=@SysOrderCode and SysType=@SysType and PayMethod=@PayMethod ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@SysOrderCode", DbType.String, syscode);
            db.AddInParameter(cmd, "@SysType", DbType.Int32, systype);
            db.AddInParameter(cmd, "@PayMethod", DbType.Int32, paytype);
            PayOrderEntity entity = new PayOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.PayOrderCode = StringUtils.GetDbString(reader["PayOrderCode"]);
                    entity.SysOrderCode = StringUtils.GetDbString(reader["SysOrderCode"]);
                    entity.SysType = StringUtils.GetDbInt(reader["SysType"]);
                    entity.NeedPayPrice = StringUtils.GetDbDecimal(reader["NeedPayPrice"]);
                    entity.PayMethod = StringUtils.GetDbInt(reader["PayMethod"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<VWPayOrderEntity> GetVWPayOrderList(int pagesize, int pageindex, ref  int recordCount,string paycode,string syscode,int paymethod,int systype)
		{
            string where = " where 1=1 ";
            if(!string.IsNullOrEmpty(paycode))
            {
                where += " and PayOrderCode=@PayOrderCode ";
            }
            if (!string.IsNullOrEmpty(syscode))
            {
                where += " and SysOrderCode=@SysOrderCode ";
            }
            if (paymethod>0)
            {
                where += " and PayMethod=@PayMethod ";
            }
            if (systype > 0)
            {
                where += " and SysType=@SysType ";
            }
            string sql= @"SELECT   [Id],[PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],[PayTime],[PayPrice],[ExternalCode]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],[PayTime],[PayPrice],[ExternalCode] from dbo.[PayOrder] WITH(NOLOCK)	
						" + where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[PayOrder] with (nolock) "+where;
            IList<VWPayOrderEntity> entityList = new List<VWPayOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(paycode))
            { 
                db.AddInParameter(cmd, "@PayOrderCode", DbType.String, paycode);
            }
            if (!string.IsNullOrEmpty(syscode))
            {
                db.AddInParameter(cmd, "@SysOrderCode", DbType.String, syscode); 
            }
            if (paymethod > 0)
            {
                db.AddInParameter(cmd, "@PayMethod", DbType.Int32, paymethod);  
            }
            if (systype > 0)
            {
                db.AddInParameter(cmd, "@SysType", DbType.Int32, systype); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					VWPayOrderEntity entity=new VWPayOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.PayOrderCode=StringUtils.GetDbString(reader["PayOrderCode"]);;
					entity.SysOrderCode=StringUtils.GetDbString(reader["SysOrderCode"]);;
					entity.SysType=StringUtils.GetDbInt(reader["SysType"]);;
					entity.NeedPayPrice=StringUtils.GetDbDecimal(reader["NeedPayPrice"]);;
					entity.PayMethod=StringUtils.GetDbInt(reader["PayMethod"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
					entity.PayTime=StringUtils.GetDbDateTime(reader["PayTime"]);;
					entity.PayPrice=StringUtils.GetDbDecimal(reader["PayPrice"]);;
					entity.ExternalCode=StringUtils.GetDbString(reader["ExternalCode"]); ;
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(paycode))
            {
                db.AddInParameter(cmd, "@PayOrderCode", DbType.String, paycode);
            }
            if (!string.IsNullOrEmpty(syscode))
            {
                db.AddInParameter(cmd, "@SysOrderCode", DbType.String, syscode);
            }
            if (paymethod > 0)
            {
                db.AddInParameter(cmd, "@PayMethod", DbType.Int32, paymethod);
            }
            if (systype > 0)
            {
                db.AddInParameter(cmd, "@SysType", DbType.Int32, systype);
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
		
		        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<PayOrderEntity> GetPayOrderAll()
        {

            string sql = @"SELECT    [Id],[PayOrderCode],[SysOrderCode],[SysType],[NeedPayPrice],[PayMethod],[CreateTime],[CreateManId],[Status],[PayTime] from dbo.[PayOrder] WITH(NOLOCK)	";
		    IList<PayOrderEntity> entityList = new List<PayOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   PayOrderEntity entity=new PayOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.PayOrderCode=StringUtils.GetDbString(reader["PayOrderCode"]);;
					entity.SysOrderCode=StringUtils.GetDbString(reader["SysOrderCode"]);;
					entity.SysType=StringUtils.GetDbInt(reader["SysType"]);;
					entity.NeedPayPrice=StringUtils.GetDbDecimal(reader["NeedPayPrice"]);;
					entity.PayMethod=StringUtils.GetDbInt(reader["PayMethod"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
					entity.PayTime=StringUtils.GetDbDateTime(reader["PayTime"]);;
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
        public int  ExistNum(PayOrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PayOrder] WITH(NOLOCK) ";
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
