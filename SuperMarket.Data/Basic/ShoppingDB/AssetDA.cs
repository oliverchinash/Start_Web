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
功能描述：Asset表的数据访问类。
创建时间：2016/8/4 11:02:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// AssetEntity的数据访问操作
	/// </summary>
	public partial class AssetDA: BaseSuperMarketDB
    {
        #region 实例化
        public static AssetDA Instance
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
            internal static readonly AssetDA instance = new AssetDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Asset，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="asset">待插入的实体对象</param>
		public int AddAsset(AssetEntity entity)
		{
		   string sql=@"insert into Asset( [MemId],[BalanceAMT],[AvailableIntegral],[FreezingIntegral],[FreezingAMT])VALUES
			            ( @MemId,@BalanceAMT,@AvailableIntegral,@FreezingIntegral,@FreezingAMT);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@BalanceAMT",  DbType.Decimal,entity.BalanceAMT);
			db.AddInParameter(cmd,"@AvailableIntegral",  DbType.Int32,entity.AvailableIntegral);
			db.AddInParameter(cmd,"@FreezingIntegral",  DbType.Int32,entity.FreezingIntegral);
			db.AddInParameter(cmd,"@FreezingAMT",  DbType.Decimal,entity.FreezingAMT);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="asset">待更新的实体对象</param>
		public   int UpdateAsset(AssetEntity entity)
		{
			string sql=@" UPDATE dbo.[Asset] SET
                       [MemId]=@MemId,[BalanceAMT]=@BalanceAMT,[AvailableIntegral]=@AvailableIntegral,[FreezingIntegral]=@FreezingIntegral,[FreezingAMT]=@FreezingAMT
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@BalanceAMT",  DbType.Decimal,entity.BalanceAMT);
			db.AddInParameter(cmd,"@AvailableIntegral",  DbType.Int32,entity.AvailableIntegral);
			db.AddInParameter(cmd,"@FreezingIntegral",  DbType.Int32,entity.FreezingIntegral);
			db.AddInParameter(cmd,"@FreezingAMT",  DbType.Decimal,entity.FreezingAMT);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteAssetByKey(int id)
	    {
			string sql=@"delete from Asset where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetDisabled()
        {
            string sql = @"delete from  Asset  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetByIds(string ids)
        {
            string sql = @"Delete from Asset  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetByIds(string ids)
        {
            string sql = @"Update   Asset set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   AssetEntity GetAsset(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[BalanceAMT],[AvailableIntegral],[FreezingIntegral],[FreezingAMT]
							FROM
							dbo.[Asset] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		AssetEntity entity=new AssetEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.BalanceAMT=StringUtils.GetDbDecimal(reader["BalanceAMT"]);
					entity.AvailableIntegral=StringUtils.GetDbInt(reader["AvailableIntegral"]);
					entity.FreezingIntegral=StringUtils.GetDbInt(reader["FreezingIntegral"]);
					entity.FreezingAMT=StringUtils.GetDbDecimal(reader["FreezingAMT"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<AssetEntity> GetAssetList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[BalanceAMT],[AvailableIntegral],[FreezingIntegral],[FreezingAMT]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[BalanceAMT],[AvailableIntegral],[FreezingIntegral],[FreezingAMT] from dbo.[Asset] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[Asset] with (nolock) ";
            IList<AssetEntity> entityList = new List< AssetEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					AssetEntity entity=new AssetEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.BalanceAMT=StringUtils.GetDbDecimal(reader["BalanceAMT"]);
					entity.AvailableIntegral=StringUtils.GetDbInt(reader["AvailableIntegral"]);
					entity.FreezingIntegral=StringUtils.GetDbInt(reader["FreezingIntegral"]);
					entity.FreezingAMT=StringUtils.GetDbDecimal(reader["FreezingAMT"]);
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
        public IList<AssetEntity> GetAssetAll()
        {

            string sql = @"SELECT    [Id],[MemId],[BalanceAMT],[AvailableIntegral],[FreezingIntegral],[FreezingAMT] from dbo.[Asset] WITH(NOLOCK)	";
		    IList<AssetEntity> entityList = new List<AssetEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   AssetEntity entity=new AssetEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.BalanceAMT=StringUtils.GetDbDecimal(reader["BalanceAMT"]);
					entity.AvailableIntegral=StringUtils.GetDbInt(reader["AvailableIntegral"]);
					entity.FreezingIntegral=StringUtils.GetDbInt(reader["FreezingIntegral"]);
					entity.FreezingAMT=StringUtils.GetDbDecimal(reader["FreezingAMT"]);
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
        public int  ExistNum(AssetEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Asset] WITH(NOLOCK) ";
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

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public AssetEntity GetAssetByMemId(int memid)
        {
            string sql = @"SELECT  [Id],[MemId],[BalanceAMT],[AvailableIntegral],[FreezingIntegral],[FreezingAMT]
							FROM
							dbo.[Asset] WITH(NOLOCK)	
							WHERE [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            AssetEntity entity = new AssetEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.BalanceAMT = StringUtils.GetDbDecimal(reader["BalanceAMT"]);
                    entity.AvailableIntegral = StringUtils.GetDbInt(reader["AvailableIntegral"]);
                    entity.FreezingIntegral = StringUtils.GetDbInt(reader["FreezingIntegral"]);
                    entity.FreezingAMT = StringUtils.GetDbDecimal(reader["FreezingAMT"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public bool CheckIntegralEnough(int memid, int integral)
        {
            string sql = @"SELECT Count(1)	FROM dbo.[Integral] WITH(NOLOCK) WHERE [MemId]=@MemId and Status=1 and AvailableIntegral>=@AvailableIntegral";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@AvailableIntegral", DbType.Int32, integral);
            object identity = db.ExecuteScalar(cmd);
            if (StringUtils.GetDbInt(identity) > 0) return true;
            return false;
        }
    }
}
