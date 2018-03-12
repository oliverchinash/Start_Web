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
功能描述：PayAliResult表的数据访问类。
创建时间：2016/10/10 16:27:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// PayAliResultEntity的数据访问操作
	/// </summary>
	public partial class PayAliResultDA: BaseSuperMarketDB
    {
        #region 实例化
        public static PayAliResultDA Instance
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
            internal static readonly PayAliResultDA instance = new PayAliResultDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表PayAliResult，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payAliResult">待插入的实体对象</param>
		public int AddPayAliResult(PayAliResultEntity entity)
		{
		   string sql=@"insert into PayAliResult( [buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],[CreateTime],[HasDeal])VALUES
			            ( @Buyeremail,@Buyerid,@Issuccess,@Notifytime,@Notifytype,@Outtradeno,@Paymenttype,@Selleremail,@Sellerid,@Subject,@Totalfee,@Tradeno,@Tradestatus,@CreateTime,@HasDeal);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Buyeremail",  DbType.String,entity.Buyeremail);
			db.AddInParameter(cmd,"@Buyerid",  DbType.String,entity.Buyerid);
			db.AddInParameter(cmd,"@Issuccess",  DbType.String,entity.Issuccess);
			db.AddInParameter(cmd,"@Notifytime",  DbType.String,entity.Notifytime);
			db.AddInParameter(cmd,"@Notifytype",  DbType.String,entity.Notifytype);
			db.AddInParameter(cmd,"@Outtradeno",  DbType.String,entity.Outtradeno);
			db.AddInParameter(cmd,"@Paymenttype",  DbType.String,entity.Paymenttype);
			db.AddInParameter(cmd,"@Selleremail",  DbType.String,entity.Selleremail);
			db.AddInParameter(cmd,"@Sellerid",  DbType.String,entity.Sellerid);
			db.AddInParameter(cmd,"@Subject",  DbType.String,entity.Subject);
			db.AddInParameter(cmd,"@Totalfee",  DbType.String,entity.Totalfee);
			db.AddInParameter(cmd,"@Tradeno",  DbType.String,entity.Tradeno);
			db.AddInParameter(cmd,"@Tradestatus",  DbType.String,entity.Tradestatus);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@HasDeal",  DbType.Int32,entity.HasDeal);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payAliResult">待更新的实体对象</param>
		public   int UpdatePayAliResult(PayAliResultEntity entity)
		{
			string sql=@" UPDATE dbo.[PayAliResult] SET
                       [buyeremail]=@Buyeremail,[buyerid]=@Buyerid,[issuccess]=@Issuccess,[notifytime]=@Notifytime,[notifytype]=@Notifytype,[outtradeno]=@Outtradeno,[paymenttype]=@Paymenttype,[selleremail]=@Selleremail,[sellerid]=@Sellerid,[subject]=@Subject,[totalfee]=@Totalfee,[tradeno]=@Tradeno,[tradestatus]=@Tradestatus,[CreateTime]=@CreateTime,[HasDeal]=@HasDeal
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Buyeremail",  DbType.String,entity.Buyeremail);
			db.AddInParameter(cmd,"@Buyerid",  DbType.String,entity.Buyerid);
			db.AddInParameter(cmd,"@Issuccess",  DbType.String,entity.Issuccess);
			db.AddInParameter(cmd,"@Notifytime",  DbType.String,entity.Notifytime);
			db.AddInParameter(cmd,"@Notifytype",  DbType.String,entity.Notifytype);
			db.AddInParameter(cmd,"@Outtradeno",  DbType.String,entity.Outtradeno);
			db.AddInParameter(cmd,"@Paymenttype",  DbType.String,entity.Paymenttype);
			db.AddInParameter(cmd,"@Selleremail",  DbType.String,entity.Selleremail);
			db.AddInParameter(cmd,"@Sellerid",  DbType.String,entity.Sellerid);
			db.AddInParameter(cmd,"@Subject",  DbType.String,entity.Subject);
			db.AddInParameter(cmd,"@Totalfee",  DbType.String,entity.Totalfee);
			db.AddInParameter(cmd,"@Tradeno",  DbType.String,entity.Tradeno);
			db.AddInParameter(cmd,"@Tradestatus",  DbType.String,entity.Tradestatus);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@HasDeal",  DbType.Int32,entity.HasDeal);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeletePayAliResultByKey(int id)
	    {
			string sql=@"delete from PayAliResult where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayAliResultDisabled()
        {
            string sql = @"delete from  PayAliResult  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayAliResultByIds(string ids)
        {
            string sql = @"Delete from PayAliResult  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayAliResultByIds(string ids)
        {
            string sql = @"Update   PayAliResult set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   PayAliResultEntity GetPayAliResult(int id)
		{
			string sql=@"SELECT  [Id],[buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],[CreateTime],[HasDeal]
							FROM
							dbo.[PayAliResult] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		PayAliResultEntity entity=new PayAliResultEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Buyeremail=StringUtils.GetDbString(reader["Buyeremail"]);
					entity.Buyerid=StringUtils.GetDbString(reader["Buyerid"]);
					entity.Issuccess=StringUtils.GetDbString(reader["Issuccess"]);
					entity.Notifytime=StringUtils.GetDbString(reader["Notifytime"]);
					entity.Notifytype=StringUtils.GetDbString(reader["Notifytype"]);
					entity.Outtradeno=StringUtils.GetDbString(reader["Outtradeno"]);
					entity.Paymenttype=StringUtils.GetDbString(reader["Paymenttype"]);
					entity.Selleremail=StringUtils.GetDbString(reader["Selleremail"]);
					entity.Sellerid=StringUtils.GetDbString(reader["Sellerid"]);
					entity.Subject=StringUtils.GetDbString(reader["Subject"]);
					entity.Totalfee=StringUtils.GetDbString(reader["Totalfee"]);
					entity.Tradeno=StringUtils.GetDbString(reader["Tradeno"]);
					entity.Tradestatus=StringUtils.GetDbString(reader["Tradestatus"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasDeal=StringUtils.GetDbInt(reader["HasDeal"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<PayAliResultEntity> GetPayAliResultList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],[CreateTime],[HasDeal]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],[CreateTime],[HasDeal] from dbo.[PayAliResult] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[PayAliResult] with (nolock) ";
            IList<PayAliResultEntity> entityList = new List< PayAliResultEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					PayAliResultEntity entity=new PayAliResultEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Buyeremail=StringUtils.GetDbString(reader["Buyeremail"]);
					entity.Buyerid=StringUtils.GetDbString(reader["Buyerid"]);
					entity.Issuccess=StringUtils.GetDbString(reader["Issuccess"]);
					entity.Notifytime=StringUtils.GetDbString(reader["Notifytime"]);
					entity.Notifytype=StringUtils.GetDbString(reader["Notifytype"]);
					entity.Outtradeno=StringUtils.GetDbString(reader["Outtradeno"]);
					entity.Paymenttype=StringUtils.GetDbString(reader["Paymenttype"]);
					entity.Selleremail=StringUtils.GetDbString(reader["Selleremail"]);
					entity.Sellerid=StringUtils.GetDbString(reader["Sellerid"]);
					entity.Subject=StringUtils.GetDbString(reader["Subject"]);
					entity.Totalfee=StringUtils.GetDbString(reader["Totalfee"]);
					entity.Tradeno=StringUtils.GetDbString(reader["Tradeno"]);
					entity.Tradestatus=StringUtils.GetDbString(reader["Tradestatus"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasDeal=StringUtils.GetDbInt(reader["HasDeal"]);
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
        public IList<VWPayAliResultEntity> GetVWPayAliResultList(int pagesize, int pageindex, ref int recordCount,long ordercode)
        {
            string where = " where 1=1 ";
            if(ordercode>0)
            {
                where += " and a.outtradeno=@OrderCode ";
            }
            string sql = @"SELECT   [Id],[buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],[CreateTime],[HasDeal]
						,AccepterName, MobilePhone, ActPrice, OrderStatus,ReBackFee FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],[buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],a.[CreateTime],[HasDeal]
 ,b.AccepterName,b.MobilePhone,c.ActPrice,c.Status AS OrderStatus,c.ReBackFee
						 from dbo.[PayAliResultOrder] a WITH(NOLOCK)	
						INNER JOIN dbo.[OrderAddress] b WITH(NOLOCK) ON a.outtradeno=b.OrderCode  
						INNER JOIN dbo.[Order] c WITH(NOLOCK) ON a.outtradeno=c.Code " + where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[PayAliResultOrder] a with(nolock) inner join dbo.[OrderAddress] b with(nolock)  on a.[outtradeno]=b.[OrderCode]  inner join dbo.[Order] c with (nolock) on a.[outtradeno]=c.[Code]" + where;
            IList<VWPayAliResultEntity> entityList = new List<VWPayAliResultEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWPayAliResultEntity entity = new VWPayAliResultEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Buyeremail = StringUtils.GetDbString(reader["Buyeremail"]);
                    entity.Buyerid = StringUtils.GetDbString(reader["Buyerid"]);
                    entity.Issuccess = StringUtils.GetDbString(reader["Issuccess"]);
                    entity.Notifytime = StringUtils.GetDbString(reader["Notifytime"]);
                    entity.Notifytype = StringUtils.GetDbString(reader["Notifytype"]);
                    entity.Outtradeno = StringUtils.GetDbString(reader["Outtradeno"]);
                    entity.Paymenttype = StringUtils.GetDbString(reader["Paymenttype"]);
                    entity.Selleremail = StringUtils.GetDbString(reader["Selleremail"]);
                    entity.Sellerid = StringUtils.GetDbString(reader["Sellerid"]);
                    entity.Subject = StringUtils.GetDbString(reader["Subject"]);
                    entity.Totalfee = StringUtils.GetDbDecimal(reader["Totalfee"]);
                    entity.ReBackFee = StringUtils.GetDbDecimal(reader["ReBackFee"]);
                    entity.Tradeno = StringUtils.GetDbString(reader["Tradeno"]);
                    entity.Tradestatus = StringUtils.GetDbString(reader["Tradestatus"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.HasDeal = StringUtils.GetDbInt(reader["HasDeal"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);  
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]); 
                    entity.OrderStatus = StringUtils.GetDbInt(reader["OrderStatus"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
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
        public IList<PayAliResultEntity> GetPayAliResultAll()
        {

            string sql = @"SELECT    [Id],[buyeremail],[buyerid],[issuccess],[notifytime],[notifytype],[outtradeno],[paymenttype],[selleremail],[sellerid],[subject],[totalfee],[tradeno],[tradestatus],[CreateTime],[HasDeal] from dbo.[PayAliResult] WITH(NOLOCK)	";
		    IList<PayAliResultEntity> entityList = new List<PayAliResultEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   PayAliResultEntity entity=new PayAliResultEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Buyeremail=StringUtils.GetDbString(reader["Buyeremail"]);
					entity.Buyerid=StringUtils.GetDbString(reader["Buyerid"]);
					entity.Issuccess=StringUtils.GetDbString(reader["Issuccess"]);
					entity.Notifytime=StringUtils.GetDbString(reader["Notifytime"]);
					entity.Notifytype=StringUtils.GetDbString(reader["Notifytype"]);
					entity.Outtradeno=StringUtils.GetDbString(reader["Outtradeno"]);
					entity.Paymenttype=StringUtils.GetDbString(reader["Paymenttype"]);
					entity.Selleremail=StringUtils.GetDbString(reader["Selleremail"]);
					entity.Sellerid=StringUtils.GetDbString(reader["Sellerid"]);
					entity.Subject=StringUtils.GetDbString(reader["Subject"]);
					entity.Totalfee=StringUtils.GetDbString(reader["Totalfee"]);
					entity.Tradeno=StringUtils.GetDbString(reader["Tradeno"]);
					entity.Tradestatus=StringUtils.GetDbString(reader["Tradestatus"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasDeal=StringUtils.GetDbInt(reader["HasDeal"]);
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
        public int  ExistNum(PayAliResultEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PayAliResult] WITH(NOLOCK) ";
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
