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
功能描述：BankInfo表的数据访问类。
创建时间：2016/8/1 14:58:49
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// BankInfoEntity的数据访问操作
	/// </summary>
	public partial class BankInfoDA: BaseSuperMarketDB
    {
        #region 实例化
        public static BankInfoDA Instance
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
            internal static readonly BankInfoDA instance = new BankInfoDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表BankInfo，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="bankInfo">待插入的实体对象</param>
		public int AddBankInfo(BankInfoEntity entity)
		{
		   string sql=@"insert into BankInfo( [MemId],[AccountType],[CardNo],[CardName],[BankName],[BankSubName],[MobilePhone],[IsDeaultDraw],[IsActive])VALUES
			            ( @MemId,@AccountType,@CardNo,@CardName,@BankName,@BankSubName,@MobilePhone,@IsDeaultDraw,@IsActive);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@AccountType",  DbType.Int32,entity.AccountType);
			db.AddInParameter(cmd,"@CardNo",  DbType.String,entity.CardNo);
			db.AddInParameter(cmd,"@CardName",  DbType.String,entity.CardName);
			db.AddInParameter(cmd,"@BankName",  DbType.String,entity.BankName);
			db.AddInParameter(cmd,"@BankSubName",  DbType.String,entity.BankSubName);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@IsDeaultDraw",  DbType.Int32,entity.IsDeaultDraw);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="bankInfo">待更新的实体对象</param>
		public   int UpdateBankInfo(BankInfoEntity entity)
		{
			string sql=@" UPDATE dbo.[BankInfo] SET
                       [MemId]=@MemId,[AccountType]=@AccountType,[CardNo]=@CardNo,[CardName]=@CardName,[BankName]=@BankName,[BankSubName]=@BankSubName,[MobilePhone]=@MobilePhone,[IsDeaultDraw]=@IsDeaultDraw,[IsActive]=@IsActive
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@AccountType",  DbType.Int32,entity.AccountType);
			db.AddInParameter(cmd,"@CardNo",  DbType.String,entity.CardNo);
			db.AddInParameter(cmd,"@CardName",  DbType.String,entity.CardName);
			db.AddInParameter(cmd,"@BankName",  DbType.String,entity.BankName);
			db.AddInParameter(cmd,"@BankSubName",  DbType.String,entity.BankSubName);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@IsDeaultDraw",  DbType.Int32,entity.IsDeaultDraw);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteBankInfoByKey(int id)
	    {
			string sql=@"delete from BankInfo where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBankInfoDisabled()
        {
            string sql = @"delete from  BankInfo  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBankInfoByIds(string ids)
        {
            string sql = @"Delete from BankInfo  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBankInfoByIds(string ids)
        {
            string sql = @"Update   BankInfo set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   BankInfoEntity GetBankInfo(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[AccountType],[CardNo],[CardName],[BankName],[BankSubName],[MobilePhone],[IsDeaultDraw],[IsActive]
							FROM
							dbo.[BankInfo] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		BankInfoEntity entity=new BankInfoEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AccountType=StringUtils.GetDbInt(reader["AccountType"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.CardName=StringUtils.GetDbString(reader["CardName"]);
					entity.BankName=StringUtils.GetDbString(reader["BankName"]);
					entity.BankSubName=StringUtils.GetDbString(reader["BankSubName"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDeaultDraw=StringUtils.GetDbInt(reader["IsDeaultDraw"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<BankInfoEntity> GetBankInfoList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[AccountType],[CardNo],[CardName],[BankName],[BankSubName],[MobilePhone],[IsDeaultDraw],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[AccountType],[CardNo],[CardName],[BankName],[BankSubName],[MobilePhone],[IsDeaultDraw],[IsActive] from dbo.[BankInfo] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[BankInfo] with (nolock) ";
            IList<BankInfoEntity> entityList = new List< BankInfoEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					BankInfoEntity entity=new BankInfoEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AccountType=StringUtils.GetDbInt(reader["AccountType"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.CardName=StringUtils.GetDbString(reader["CardName"]);
					entity.BankName=StringUtils.GetDbString(reader["BankName"]);
					entity.BankSubName=StringUtils.GetDbString(reader["BankSubName"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDeaultDraw=StringUtils.GetDbInt(reader["IsDeaultDraw"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public IList<BankInfoEntity> GetBankInfoAll()
        {

            string sql = @"SELECT    [Id],[MemId],[AccountType],[CardNo],[CardName],[BankName],[BankSubName],[MobilePhone],[IsDeaultDraw],[IsActive] from dbo.[BankInfo] WITH(NOLOCK)	";
		    IList<BankInfoEntity> entityList = new List<BankInfoEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   BankInfoEntity entity=new BankInfoEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AccountType=StringUtils.GetDbInt(reader["AccountType"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.CardName=StringUtils.GetDbString(reader["CardName"]);
					entity.BankName=StringUtils.GetDbString(reader["BankName"]);
					entity.BankSubName=StringUtils.GetDbString(reader["BankSubName"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDeaultDraw=StringUtils.GetDbInt(reader["IsDeaultDraw"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(BankInfoEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[BankInfo] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CardName=@CardName) ";
					     where = where+ "  (BankName=@BankName) ";
					     where = where+ "  (BankSubName=@BankSubName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CardName=@CardName) ";
					     where = where+ " id<>@Id and  (BankName=@BankName) ";
					     where = where+ " id<>@Id and  (BankSubName=@BankSubName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CardName", DbType.String, entity.CardName); 
					
            db.AddInParameter(cmd, "@BankName", DbType.String, entity.BankName); 
					
            db.AddInParameter(cmd, "@BankSubName", DbType.String, entity.BankSubName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
