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
功能描述：PayOrderReback表的数据访问类。
创建时间：2017/11/29 16:19:53
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.PayDB
{
	/// <summary>
	/// PayOrderRebackEntity的数据访问操作
	/// </summary>
	public partial class PayOrderRebackDA: BaseSuperMarketDB
    {
        #region 实例化
        public static PayOrderRebackDA Instance
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
            internal static readonly PayOrderRebackDA instance = new PayOrderRebackDA();
        }
        #endregion 
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表PayOrderReback，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payOrderReback">待插入的实体对象</param>
		public int AddPayOrderReback(PayOrderRebackEntity entity)
		{
		   string sql= @"insert into PayOrderReback( [PayRebackCode],[PayOrderCode],[SysOrderCode],[SysType],[PayMethod],[ExternalCode],TotalPrice,[RebackPrice],[RebackRemark],[CreateTime],[Status])VALUES
			            ( @PayRebackCode,@PayOrderCode,@SysOrderCode,@SysType,@PayMethod,@ExternalCode,@TotalPrice,@RebackPrice,@RebackRemark,@CreateTime,@Status);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@PayRebackCode",  DbType.String,entity.PayRebackCode);
			db.AddInParameter(cmd,"@PayOrderCode",  DbType.String,entity.PayOrderCode);
			db.AddInParameter(cmd,"@SysOrderCode",  DbType.String,entity.SysOrderCode);
			db.AddInParameter(cmd,"@SysType",  DbType.Int32,entity.SysType);
			db.AddInParameter(cmd,"@PayMethod",  DbType.Int32,entity.PayMethod);
			db.AddInParameter(cmd,"@ExternalCode",  DbType.String,entity.ExternalCode);
			db.AddInParameter(cmd, "@TotalPrice",  DbType.Decimal,entity.TotalPrice);
			db.AddInParameter(cmd,"@RebackPrice",  DbType.Decimal,entity.RebackPrice);
			db.AddInParameter(cmd,"@RebackRemark",  DbType.String,entity.RebackRemark);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payOrderReback">待更新的实体对象</param>
		public   int UpdatePayOrderReback(PayOrderRebackEntity entity)
		{
			string sql=@" UPDATE dbo.[PayOrderReback] SET
                       [PayRebackCode]=@PayRebackCode,[PayOrderCode]=@PayOrderCode,[SysOrderCode]=@SysOrderCode,[SysType]=@SysType,[PayMethod]=@PayMethod,[ExternalCode]=@ExternalCode,[RebackPrice]=@RebackPrice,[RebackRemark]=@RebackRemark,[CreateTime]=@CreateTime,[Status]=@Status
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@PayRebackCode",  DbType.String,entity.PayRebackCode);
			db.AddInParameter(cmd,"@PayOrderCode",  DbType.String,entity.PayOrderCode);
			db.AddInParameter(cmd,"@SysOrderCode",  DbType.String,entity.SysOrderCode);
			db.AddInParameter(cmd,"@SysType",  DbType.Int32,entity.SysType);
			db.AddInParameter(cmd,"@PayMethod",  DbType.Int32,entity.PayMethod);
			db.AddInParameter(cmd,"@ExternalCode",  DbType.String,entity.ExternalCode);
			db.AddInParameter(cmd,"@RebackPrice",  DbType.Decimal,entity.RebackPrice);
			db.AddInParameter(cmd,"@RebackRemark",  DbType.String,entity.RebackRemark);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeletePayOrderRebackByKey(int id)
	    {
			string sql=@"delete from PayOrderReback where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayOrderRebackDisabled()
        {
            string sql = @"delete from  PayOrderReback  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayOrderRebackByIds(string ids)
        {
            string sql = @"Delete from PayOrderReback  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayOrderRebackByIds(string ids)
        {
            string sql = @"Update   PayOrderReback set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   PayOrderRebackEntity GetPayOrderReback(int id)
		{
			string sql=@"SELECT  [Id],[PayRebackCode],[PayOrderCode],[SysOrderCode],[SysType],[PayMethod],[ExternalCode],[RebackPrice],[RebackRemark],[CreateTime],[Status]
							FROM
							dbo.[PayOrderReback] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		PayOrderRebackEntity entity=new PayOrderRebackEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.PayRebackCode=StringUtils.GetDbString(reader["PayRebackCode"]);;
					entity.PayOrderCode=StringUtils.GetDbString(reader["PayOrderCode"]);;
					entity.SysOrderCode=StringUtils.GetDbString(reader["SysOrderCode"]);;
					entity.SysType=StringUtils.GetDbInt(reader["SysType"]);;
					entity.PayMethod=StringUtils.GetDbInt(reader["PayMethod"]);;
					entity.ExternalCode=StringUtils.GetDbString(reader["ExternalCode"]);;
					entity.RebackPrice=StringUtils.GetDbDecimal(reader["RebackPrice"]);;
					entity.RebackRemark=StringUtils.GetDbString(reader["RebackRemark"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<PayOrderRebackEntity> GetPayOrderRebackList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[PayRebackCode],[PayOrderCode],[SysOrderCode],[SysType],[PayMethod],[ExternalCode],[RebackPrice],[RebackRemark],[CreateTime],[Status]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[PayRebackCode],[PayOrderCode],[SysOrderCode],[SysType],[PayMethod],[ExternalCode],[RebackPrice],[RebackRemark],[CreateTime],[Status] from dbo.[PayOrderReback] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[PayOrderReback] with (nolock) ";
            IList<PayOrderRebackEntity> entityList = new List< PayOrderRebackEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					PayOrderRebackEntity entity=new PayOrderRebackEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.PayRebackCode=StringUtils.GetDbString(reader["PayRebackCode"]);;
					entity.PayOrderCode=StringUtils.GetDbString(reader["PayOrderCode"]);;
					entity.SysOrderCode=StringUtils.GetDbString(reader["SysOrderCode"]);;
					entity.SysType=StringUtils.GetDbInt(reader["SysType"]);;
					entity.PayMethod=StringUtils.GetDbInt(reader["PayMethod"]);;
					entity.ExternalCode=StringUtils.GetDbString(reader["ExternalCode"]);;
					entity.RebackPrice=StringUtils.GetDbDecimal(reader["RebackPrice"]);;
					entity.RebackRemark=StringUtils.GetDbString(reader["RebackRemark"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
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
        public IList<PayOrderRebackEntity> GetRebackByPayCode(string paycode)
        { 
            string sql = @"SELECT    [Id],[PayRebackCode],[PayOrderCode],[SysOrderCode],[SysType],[PayMethod],[ExternalCode],[RebackPrice],[RebackRemark],[CreateTime],[Status] from dbo.[PayOrderReback] WITH(NOLOCK) 
                           where PayOrderCode=@PayOrderCode AND Status=1 ";
		    IList<PayOrderRebackEntity> entityList = new List<PayOrderRebackEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PayOrderCode", DbType.String, paycode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   PayOrderRebackEntity entity=new PayOrderRebackEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.PayRebackCode=StringUtils.GetDbString(reader["PayRebackCode"]);;
					entity.PayOrderCode=StringUtils.GetDbString(reader["PayOrderCode"]);;
					entity.SysOrderCode=StringUtils.GetDbString(reader["SysOrderCode"]);;
					entity.SysType=StringUtils.GetDbInt(reader["SysType"]);;
					entity.PayMethod=StringUtils.GetDbInt(reader["PayMethod"]);;
					entity.ExternalCode=StringUtils.GetDbString(reader["ExternalCode"]);;
					entity.RebackPrice=StringUtils.GetDbDecimal(reader["RebackPrice"]);;
					entity.RebackRemark=StringUtils.GetDbString(reader["RebackRemark"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
					entity.Status=StringUtils.GetDbInt(reader["Status"]);;
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
        public int  ExistNum(PayOrderRebackEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PayOrderReback] WITH(NOLOCK) ";
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
	}
}
