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
功能描述：AssetChange表的数据访问类。
创建时间：2016/8/7 17:12:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// AssetChangeEntity的数据访问操作
	/// </summary>
	public partial class AssetChangeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static AssetChangeDA Instance
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
            internal static readonly AssetChangeDA instance = new AssetChangeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表AssetChange，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="assetChange">待插入的实体对象</param>
		public int AddAssetChange(AssetChangeEntity entity)
		{
		   string sql=@"insert into AssetChange( [MemId],[OperateType],[ReChargeAmt],[PayAmt],[Reason],[CreateTime])VALUES
			            ( @MemId,@OperateType,@ReChargeAmt,@PayAmt,@Reason,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@OperateType",  DbType.Int32,entity.OperateType);
			db.AddInParameter(cmd,"@ReChargeAmt",  DbType.Decimal,entity.ReChargeAmt);
			db.AddInParameter(cmd,"@PayAmt",  DbType.Decimal,entity.PayAmt);
			db.AddInParameter(cmd,"@Reason",  DbType.String,entity.Reason);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="assetChange">待更新的实体对象</param>
		public   int UpdateAssetChange(AssetChangeEntity entity)
		{
			string sql=@" UPDATE dbo.[AssetChange] SET
                       [MemId]=@MemId,[OperateType]=@OperateType,[ReChargeAmt]=@ReChargeAmt,[PayAmt]=@PayAmt,[Reason]=@Reason,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@OperateType",  DbType.Int32,entity.OperateType);
			db.AddInParameter(cmd,"@ReChargeAmt",  DbType.Decimal,entity.ReChargeAmt);
			db.AddInParameter(cmd,"@PayAmt",  DbType.Decimal,entity.PayAmt);
			db.AddInParameter(cmd,"@Reason",  DbType.String,entity.Reason);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteAssetChangeByKey(int id)
	    {
			string sql=@"delete from AssetChange where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetChangeDisabled()
        {
            string sql = @"delete from  AssetChange  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetChangeByIds(string ids)
        {
            string sql = @"Delete from AssetChange  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetChangeByIds(string ids)
        {
            string sql = @"Update   AssetChange set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   AssetChangeEntity GetAssetChange(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[OperateType],[ReChargeAmt],[PayAmt],[Reason],[CreateTime]
							FROM
							dbo.[AssetChange] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		AssetChangeEntity entity=new AssetChangeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.OperateType=StringUtils.GetDbInt(reader["OperateType"]);
					entity.ReChargeAmt=StringUtils.GetDbDecimal(reader["ReChargeAmt"]);
					entity.PayAmt=StringUtils.GetDbDecimal(reader["PayAmt"]);
					entity.Reason=StringUtils.GetDbString(reader["Reason"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
	  
		
		        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<AssetChangeEntity> GetAssetChangeAll()
        {

            string sql = @"SELECT    [Id],[MemId],[OperateType],[ReChargeAmt],[PayAmt],[Reason],[CreateTime] from dbo.[AssetChange] WITH(NOLOCK)	";
		    IList<AssetChangeEntity> entityList = new List<AssetChangeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   AssetChangeEntity entity=new AssetChangeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.OperateType=StringUtils.GetDbInt(reader["OperateType"]);
					entity.ReChargeAmt=StringUtils.GetDbDecimal(reader["ReChargeAmt"]);
					entity.PayAmt=StringUtils.GetDbDecimal(reader["PayAmt"]);
					entity.Reason=StringUtils.GetDbString(reader["Reason"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(AssetChangeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[AssetChange] WITH(NOLOCK) ";
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
