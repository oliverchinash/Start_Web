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
功能描述：AssetReCharge表的数据访问类。
创建时间：2016/8/4 11:02:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// AssetReChargeEntity的数据访问操作
	/// </summary>
	public partial class AssetReChargeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static AssetReChargeDA Instance
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
            internal static readonly AssetReChargeDA instance = new AssetReChargeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表AssetReCharge，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="assetReCharge">待插入的实体对象</param>
		public int AddAssetReCharge(AssetReChargeEntity entity)
		{
		   string sql=@"insert into AssetReCharge( [MemId],[Amt],[PayType],[BankCode],[TranSerialNum],[CardNo],[CreateTime],[IpAddress])VALUES
			            ( @MemId,@Amt,@PayType,@BankCode,@TranSerialNum,@CardNo,@CreateTime,@IpAddress);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Amt",  DbType.Decimal,entity.Amt);
			db.AddInParameter(cmd,"@PayType",  DbType.Int32,entity.PayType);
			db.AddInParameter(cmd,"@BankCode",  DbType.String,entity.BankCode);
			db.AddInParameter(cmd,"@TranSerialNum",  DbType.String,entity.TranSerialNum);
			db.AddInParameter(cmd,"@CardNo",  DbType.String,entity.CardNo);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@IpAddress",  DbType.String,entity.IpAddress);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="assetReCharge">待更新的实体对象</param>
		public   int UpdateAssetReCharge(AssetReChargeEntity entity)
		{
			string sql=@" UPDATE dbo.[AssetReCharge] SET
                       [MemId]=@MemId,[Amt]=@Amt,[PayType]=@PayType,[BankCode]=@BankCode,[TranSerialNum]=@TranSerialNum,[CardNo]=@CardNo,[CreateTime]=@CreateTime,[IpAddress]=@IpAddress
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Amt",  DbType.Decimal,entity.Amt);
			db.AddInParameter(cmd,"@PayType",  DbType.Int32,entity.PayType);
			db.AddInParameter(cmd,"@BankCode",  DbType.String,entity.BankCode);
			db.AddInParameter(cmd,"@TranSerialNum",  DbType.String,entity.TranSerialNum);
			db.AddInParameter(cmd,"@CardNo",  DbType.String,entity.CardNo);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@IpAddress",  DbType.String,entity.IpAddress);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteAssetReChargeByKey(int id)
	    {
			string sql=@"delete from AssetReCharge where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetReChargeDisabled()
        {
            string sql = @"delete from  AssetReCharge  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetReChargeByIds(string ids)
        {
            string sql = @"Delete from AssetReCharge  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetReChargeByIds(string ids)
        {
            string sql = @"Update   AssetReCharge set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   AssetReChargeEntity GetAssetReCharge(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[Amt],[PayType],[BankCode],[TranSerialNum],[CardNo],[CreateTime],[IpAddress]
							FROM
							dbo.[AssetReCharge] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		AssetReChargeEntity entity=new AssetReChargeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Amt=StringUtils.GetDbDecimal(reader["Amt"]);
					entity.PayType=StringUtils.GetDbInt(reader["PayType"]);
					entity.BankCode=StringUtils.GetDbString(reader["BankCode"]);
					entity.TranSerialNum=StringUtils.GetDbString(reader["TranSerialNum"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<AssetReChargeEntity> GetAssetReChargeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[Amt],[PayType],[BankCode],[TranSerialNum],[CardNo],[CreateTime],[IpAddress]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[Amt],[PayType],[BankCode],[TranSerialNum],[CardNo],[CreateTime],[IpAddress] from dbo.[AssetReCharge] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[AssetReCharge] with (nolock) ";
            IList<AssetReChargeEntity> entityList = new List< AssetReChargeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					AssetReChargeEntity entity=new AssetReChargeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Amt=StringUtils.GetDbDecimal(reader["Amt"]);
					entity.PayType=StringUtils.GetDbInt(reader["PayType"]);
					entity.BankCode=StringUtils.GetDbString(reader["BankCode"]);
					entity.TranSerialNum=StringUtils.GetDbString(reader["TranSerialNum"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
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
        public IList<AssetReChargeEntity> GetAssetReChargeAll()
        {

            string sql = @"SELECT    [Id],[MemId],[Amt],[PayType],[BankCode],[TranSerialNum],[CardNo],[CreateTime],[IpAddress] from dbo.[AssetReCharge] WITH(NOLOCK)	";
		    IList<AssetReChargeEntity> entityList = new List<AssetReChargeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   AssetReChargeEntity entity=new AssetReChargeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Amt=StringUtils.GetDbDecimal(reader["Amt"]);
					entity.PayType=StringUtils.GetDbInt(reader["PayType"]);
					entity.BankCode=StringUtils.GetDbString(reader["BankCode"]);
					entity.TranSerialNum=StringUtils.GetDbString(reader["TranSerialNum"]);
					entity.CardNo=StringUtils.GetDbString(reader["CardNo"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.IpAddress=StringUtils.GetDbString(reader["IpAddress"]);
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
        public int  ExistNum(AssetReChargeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[AssetReCharge] WITH(NOLOCK) ";
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
