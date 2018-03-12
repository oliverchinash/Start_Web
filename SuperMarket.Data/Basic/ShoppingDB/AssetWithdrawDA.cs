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
功能描述：AssetWithdraw表的数据访问类。
创建时间：2016/8/4 11:02:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// AssetWithdrawEntity的数据访问操作
	/// </summary>
	public partial class AssetWithdrawDA: BaseSuperMarketDB
    {
        #region 实例化
        public static AssetWithdrawDA Instance
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
            internal static readonly AssetWithdrawDA instance = new AssetWithdrawDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表AssetWithdraw，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="assetWithdraw">待插入的实体对象</param>
		public int AddAssetWithdraw(AssetWithdrawEntity entity)
		{
		   string sql=@"insert into AssetWithdraw( [MemId],[Amt],[CounterFee],[DrawType],[Status],[CreateTime],[DrawTime],[TranSerialNum],[CardNo],[BankCode],[BankName],[BankSubName],[CardName],[MobilePhone])VALUES
			            ( @MemId,@Amt,@CounterFee,@DrawType,@Status,@CreateTime,@DrawTime,@TranSerialNum,@CardNo,@BankCode,@BankName,@BankSubName,@CardName,@MobilePhone);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Amt",  DbType.Decimal,entity.Amt);
			db.AddInParameter(cmd,"@CounterFee",  DbType.Decimal,entity.CounterFee);
			db.AddInParameter(cmd,"@DrawType",  DbType.Int32,entity.DrawType);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@DrawTime",  DbType.DateTime,entity.DrawTime);
			db.AddInParameter(cmd,"@TranSerialNum",  DbType.String,entity.TranSerialNum);
			db.AddInParameter(cmd,"@CardNo",  DbType.String,entity.CardNo);
			db.AddInParameter(cmd,"@BankCode",  DbType.String,entity.BankCode);
			db.AddInParameter(cmd,"@BankName",  DbType.String,entity.BankName);
			db.AddInParameter(cmd,"@BankSubName",  DbType.String,entity.BankSubName);
			db.AddInParameter(cmd,"@CardName",  DbType.String,entity.CardName);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="assetWithdraw">待更新的实体对象</param>
		public   int UpdateAssetWithdraw(AssetWithdrawEntity entity)
		{
			string sql=@" UPDATE dbo.[AssetWithdraw] SET
                       [MemId]=@MemId,[Amt]=@Amt,[CounterFee]=@CounterFee,[DrawType]=@DrawType,[Status]=@Status,[CreateTime]=@CreateTime,[DrawTime]=@DrawTime,[TranSerialNum]=@TranSerialNum,[CardNo]=@CardNo,[BankCode]=@BankCode,[BankName]=@BankName,[BankSubName]=@BankSubName,[CardName]=@CardName,[MobilePhone]=@MobilePhone
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Amt",  DbType.Decimal,entity.Amt);
			db.AddInParameter(cmd,"@CounterFee",  DbType.Decimal,entity.CounterFee);
			db.AddInParameter(cmd,"@DrawType",  DbType.Int32,entity.DrawType);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@DrawTime",  DbType.DateTime,entity.DrawTime);
			db.AddInParameter(cmd,"@TranSerialNum",  DbType.String,entity.TranSerialNum);
			db.AddInParameter(cmd,"@CardNo",  DbType.String,entity.CardNo);
			db.AddInParameter(cmd,"@BankCode",  DbType.String,entity.BankCode);
			db.AddInParameter(cmd,"@BankName",  DbType.String,entity.BankName);
			db.AddInParameter(cmd,"@BankSubName",  DbType.String,entity.BankSubName);
			db.AddInParameter(cmd,"@CardName",  DbType.String,entity.CardName);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteAssetWithdrawByKey(int id)
	    {
			string sql=@"delete from AssetWithdraw where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetWithdrawDisabled()
        {
            string sql = @"delete from  AssetWithdraw  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetWithdrawByIds(string ids)
        {
            string sql = @"Delete from AssetWithdraw  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetWithdrawByIds(string ids)
        {
            string sql = @"Update   AssetWithdraw set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   AssetWithdrawEntity GetAssetWithdraw(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[Amt],[CounterFee],[DrawType],[Status],[CreateTime],[DrawTime],[TranSerialNum],[CardNo],[BankCode],[BankName],[BankSubName],[CardName],[MobilePhone]
							FROM
							dbo.[AssetWithdraw] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		AssetWithdrawEntity entity=new AssetWithdrawEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Amt=StringUtils.GetDbDecimal(reader["Amt"]);
					entity.CounterFee=StringUtils.GetDbDecimal(reader["CounterFee"]);
					entity.DrawType=StringUtils.GetDbInt(reader["DrawType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.DrawTime=StringUtils.GetDbDateTime(reader["DrawTime"]);
					entity.TranSerialNum=StringUtils.GetDbString(reader["TranSerialNum"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.BankCode=StringUtils.GetDbString(reader["BankCode"]);
					entity.BankName=StringUtils.GetDbString(reader["BankName"]);
					entity.BankSubName=StringUtils.GetDbString(reader["BankSubName"]);
					entity.CardName=StringUtils.GetDbString(reader["CardName"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<AssetWithdrawEntity> GetAssetWithdrawList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[Amt],[CounterFee],[DrawType],[Status],[CreateTime],[DrawTime],[TranSerialNum],[CardNo],[BankCode],[BankName],[BankSubName],[CardName],[MobilePhone]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[Amt],[CounterFee],[DrawType],[Status],[CreateTime],[DrawTime],[TranSerialNum],[CardNo],[BankCode],[BankName],[BankSubName],[CardName],[MobilePhone] from dbo.[AssetWithdraw] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[AssetWithdraw] with (nolock) ";
            IList<AssetWithdrawEntity> entityList = new List< AssetWithdrawEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					AssetWithdrawEntity entity=new AssetWithdrawEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Amt=StringUtils.GetDbDecimal(reader["Amt"]);
					entity.CounterFee=StringUtils.GetDbDecimal(reader["CounterFee"]);
					entity.DrawType=StringUtils.GetDbInt(reader["DrawType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.DrawTime=StringUtils.GetDbDateTime(reader["DrawTime"]);
					entity.TranSerialNum=StringUtils.GetDbString(reader["TranSerialNum"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.BankCode=StringUtils.GetDbString(reader["BankCode"]);
					entity.BankName=StringUtils.GetDbString(reader["BankName"]);
					entity.BankSubName=StringUtils.GetDbString(reader["BankSubName"]);
					entity.CardName=StringUtils.GetDbString(reader["CardName"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
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
        public IList<AssetWithdrawEntity> GetAssetWithdrawAll()
        {

            string sql = @"SELECT    [Id],[MemId],[Amt],[CounterFee],[DrawType],[Status],[CreateTime],[DrawTime],[TranSerialNum],[CardNo],[BankCode],[BankName],[BankSubName],[CardName],[MobilePhone] from dbo.[AssetWithdraw] WITH(NOLOCK)	";
		    IList<AssetWithdrawEntity> entityList = new List<AssetWithdrawEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   AssetWithdrawEntity entity=new AssetWithdrawEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Amt=StringUtils.GetDbDecimal(reader["Amt"]);
					entity.CounterFee=StringUtils.GetDbDecimal(reader["CounterFee"]);
					entity.DrawType=StringUtils.GetDbInt(reader["DrawType"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.DrawTime=StringUtils.GetDbDateTime(reader["DrawTime"]);
					entity.TranSerialNum=StringUtils.GetDbString(reader["TranSerialNum"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.BankCode=StringUtils.GetDbString(reader["BankCode"]);
					entity.BankName=StringUtils.GetDbString(reader["BankName"]);
					entity.BankSubName=StringUtils.GetDbString(reader["BankSubName"]);
					entity.CardName=StringUtils.GetDbString(reader["CardName"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
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
        public int  ExistNum(AssetWithdrawEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[AssetWithdraw] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (BankName=@BankName) ";
					     where = where+ "  (BankSubName=@BankSubName) ";
					     where = where+ "  (CardName=@CardName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (BankName=@BankName) ";
					     where = where+ " id<>@Id and  (BankSubName=@BankSubName) ";
					     where = where+ " id<>@Id and  (CardName=@CardName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@BankName", DbType.String, entity.BankName); 
					
            db.AddInParameter(cmd, "@BankSubName", DbType.String, entity.BankSubName); 
					
            db.AddInParameter(cmd, "@CardName", DbType.String, entity.CardName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
