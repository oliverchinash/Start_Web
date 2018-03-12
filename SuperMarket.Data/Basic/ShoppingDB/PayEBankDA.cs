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
功能描述：PayEBank表的数据访问类。
创建时间：2016/10/9 10:39:34
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// PayEBankEntity的数据访问操作
	/// </summary>
	public partial class PayEBankDA: BaseSuperMarketDB
    {
        #region 实例化
        public static PayEBankDA Instance
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
            internal static readonly PayEBankDA instance = new PayEBankDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表PayEBank，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payEBank">待插入的实体对象</param>
		public int AddPayEBank(PayEBankEntity entity)
		{
		   string sql=@"insert into PayEBank( [EBankCode],[EBankName],[FormMethod],[FormAction],[ResponseURL],[PrivateKey],[PublicKey],[MerchantCode],[EmailAddress])VALUES
			            ( @EBankCode,@EBankName,@FormMethod,@FormAction,@ResponseURL,@PrivateKey,@PublicKey,@MerchantCode,@EmailAddress);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@EBankCode",  DbType.String,entity.EBankCode);
			db.AddInParameter(cmd,"@EBankName",  DbType.String,entity.EBankName);
			db.AddInParameter(cmd,"@FormMethod",  DbType.String,entity.FormMethod);
			db.AddInParameter(cmd,"@FormAction",  DbType.String,entity.FormAction);
			db.AddInParameter(cmd,"@ResponseURL",  DbType.String,entity.ResponseURL);
			db.AddInParameter(cmd,"@PrivateKey",  DbType.String,entity.PrivateKey);
			db.AddInParameter(cmd,"@PublicKey",  DbType.String,entity.PublicKey);
			db.AddInParameter(cmd,"@MerchantCode",  DbType.String,entity.MerchantCode);
			db.AddInParameter(cmd,"@EmailAddress",  DbType.String,entity.EmailAddress);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="payEBank">待更新的实体对象</param>
		public   int UpdatePayEBank(PayEBankEntity entity)
		{
			string sql=@" UPDATE dbo.[PayEBank] SET
                       [EBankCode]=@EBankCode,[EBankName]=@EBankName,[FormMethod]=@FormMethod,[FormAction]=@FormAction,[ResponseURL]=@ResponseURL,[PrivateKey]=@PrivateKey,[PublicKey]=@PublicKey,[MerchantCode]=@MerchantCode,[EmailAddress]=@EmailAddress
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@EBankCode",  DbType.String,entity.EBankCode);
			db.AddInParameter(cmd,"@EBankName",  DbType.String,entity.EBankName);
			db.AddInParameter(cmd,"@FormMethod",  DbType.String,entity.FormMethod);
			db.AddInParameter(cmd,"@FormAction",  DbType.String,entity.FormAction);
			db.AddInParameter(cmd,"@ResponseURL",  DbType.String,entity.ResponseURL);
			db.AddInParameter(cmd,"@PrivateKey",  DbType.String,entity.PrivateKey);
			db.AddInParameter(cmd,"@PublicKey",  DbType.String,entity.PublicKey);
			db.AddInParameter(cmd,"@MerchantCode",  DbType.String,entity.MerchantCode);
			db.AddInParameter(cmd,"@EmailAddress",  DbType.String,entity.EmailAddress);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeletePayEBankByKey(int id)
	    {
			string sql=@"delete from PayEBank where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayEBankDisabled()
        {
            string sql = @"delete from  PayEBank  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayEBankByIds(string ids)
        {
            string sql = @"Delete from PayEBank  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayEBankByIds(string ids)
        {
            string sql = @"Update   PayEBank set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   PayEBankEntity GetPayEBank(int id)
		{
			string sql=@"SELECT  [Id],[EBankCode],[EBankName],[FormMethod],[FormAction],[ResponseURL],[PrivateKey],[PublicKey],[MerchantCode],[EmailAddress]
							FROM
							dbo.[PayEBank] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		PayEBankEntity entity=new PayEBankEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.EBankCode=StringUtils.GetDbString(reader["EBankCode"]);
					entity.EBankName=StringUtils.GetDbString(reader["EBankName"]);
					entity.FormMethod=StringUtils.GetDbString(reader["FormMethod"]);
					entity.FormAction=StringUtils.GetDbString(reader["FormAction"]);
					entity.ResponseURL=StringUtils.GetDbString(reader["ResponseURL"]);
					entity.PrivateKey=StringUtils.GetDbString(reader["PrivateKey"]);
					entity.PublicKey=StringUtils.GetDbString(reader["PublicKey"]);
					entity.MerchantCode=StringUtils.GetDbString(reader["MerchantCode"]);
					entity.EmailAddress=StringUtils.GetDbString(reader["EmailAddress"]);
				}
   		    }
            return entity;
		}
        public PayEBankEntity GetPayEBankByPayType(int paytype)
        {
            string sql = @"SELECT  [Id],[EBankCode],[EBankName],[FormMethod],[FormAction],[ResponseURL],[PrivateKey],[PublicKey],[MerchantCode],[EmailAddress]
							FROM
							dbo.[PayEBank] WITH(NOLOCK)	
							WHERE [PayType]=@PayType";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@PayType", DbType.Int32, paytype);
            PayEBankEntity entity = new PayEBankEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.EBankCode = StringUtils.GetDbString(reader["EBankCode"]);
                    entity.EBankName = StringUtils.GetDbString(reader["EBankName"]);
                    entity.FormMethod = StringUtils.GetDbString(reader["FormMethod"]);
                    entity.FormAction = StringUtils.GetDbString(reader["FormAction"]);
                    entity.ResponseURL = StringUtils.GetDbString(reader["ResponseURL"]);
                    entity.PrivateKey = StringUtils.GetDbString(reader["PrivateKey"]);
                    entity.PublicKey = StringUtils.GetDbString(reader["PublicKey"]);
                    entity.MerchantCode = StringUtils.GetDbString(reader["MerchantCode"]);
                    entity.EmailAddress = StringUtils.GetDbString(reader["EmailAddress"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<PayEBankEntity> GetPayEBankList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[EBankCode],[EBankName],[FormMethod],[FormAction],[ResponseURL],[PrivateKey],[PublicKey],[MerchantCode],[EmailAddress]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[EBankCode],[EBankName],[FormMethod],[FormAction],[ResponseURL],[PrivateKey],[PublicKey],[MerchantCode],[EmailAddress] from dbo.[PayEBank] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[PayEBank] with (nolock) ";
            IList<PayEBankEntity> entityList = new List< PayEBankEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					PayEBankEntity entity=new PayEBankEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.EBankCode=StringUtils.GetDbString(reader["EBankCode"]);
					entity.EBankName=StringUtils.GetDbString(reader["EBankName"]);
					entity.FormMethod=StringUtils.GetDbString(reader["FormMethod"]);
					entity.FormAction=StringUtils.GetDbString(reader["FormAction"]);
					entity.ResponseURL=StringUtils.GetDbString(reader["ResponseURL"]);
					entity.PrivateKey=StringUtils.GetDbString(reader["PrivateKey"]);
					entity.PublicKey=StringUtils.GetDbString(reader["PublicKey"]);
					entity.MerchantCode=StringUtils.GetDbString(reader["MerchantCode"]);
					entity.EmailAddress=StringUtils.GetDbString(reader["EmailAddress"]);
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
        public IList<PayEBankEntity> GetPayEBankAll()
        {

            string sql = @"SELECT    [Id],[EBankCode],[EBankName],[FormMethod],[FormAction],[ResponseURL],[PrivateKey],[PublicKey],[MerchantCode],[EmailAddress] from dbo.[PayEBank] WITH(NOLOCK)	";
		    IList<PayEBankEntity> entityList = new List<PayEBankEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   PayEBankEntity entity=new PayEBankEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.EBankCode=StringUtils.GetDbString(reader["EBankCode"]);
					entity.EBankName=StringUtils.GetDbString(reader["EBankName"]);
					entity.FormMethod=StringUtils.GetDbString(reader["FormMethod"]);
					entity.FormAction=StringUtils.GetDbString(reader["FormAction"]);
					entity.ResponseURL=StringUtils.GetDbString(reader["ResponseURL"]);
					entity.PrivateKey=StringUtils.GetDbString(reader["PrivateKey"]);
					entity.PublicKey=StringUtils.GetDbString(reader["PublicKey"]);
					entity.MerchantCode=StringUtils.GetDbString(reader["MerchantCode"]);
					entity.EmailAddress=StringUtils.GetDbString(reader["EmailAddress"]);
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
        public int  ExistNum(PayEBankEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[PayEBank] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (EBankName=@EBankName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (EBankName=@EBankName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@EBankName", DbType.String, entity.EBankName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
