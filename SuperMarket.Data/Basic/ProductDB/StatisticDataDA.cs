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
功能描述：StatisticData表的数据访问类。
创建时间：2016/8/16 13:54:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// StatisticDataEntity的数据访问操作
	/// </summary>
	public partial class StatisticDataDA: BaseSuperMarketDB
	{
        #region 实例化
        public static StatisticDataDA Instance
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
            internal static readonly StatisticDataDA instance = new StatisticDataDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StatisticData，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticData">待插入的实体对象</param>
		public int AddStatisticData(StatisticDataEntity entity)
		{
		   string sql=@"insert into StatisticData( [SellerId],[TotalNum],[PcNum],[MobileNum],[SDataType],[CreateDate])VALUES
			            ( @SellerId,@TotalNum,@PcNum,@MobileNum,@SDataType,@CreateDate);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@SellerId",  DbType.Int32,entity.SellerId);
			db.AddInParameter(cmd,"@TotalNum",  DbType.Int32,entity.TotalNum);
			db.AddInParameter(cmd,"@PcNum",  DbType.Int32,entity.PcNum);
			db.AddInParameter(cmd,"@MobileNum",  DbType.Int32,entity.MobileNum);
			db.AddInParameter(cmd,"@SDataType",  DbType.Int32,entity.SDataType);
			db.AddInParameter(cmd,"@CreateDate",  DbType.Object,entity.CreateDate);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticData">待更新的实体对象</param>
		public   int UpdateStatisticData(StatisticDataEntity entity)
		{
			string sql=@" UPDATE dbo.[StatisticData] SET
                       [SellerId]=@SellerId,[TotalNum]=@TotalNum,[PcNum]=@PcNum,[MobileNum]=@MobileNum,[SDataType]=@SDataType,[CreateDate]=@CreateDate
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@SellerId",  DbType.Int32,entity.SellerId);
			db.AddInParameter(cmd,"@TotalNum",  DbType.Int32,entity.TotalNum);
			db.AddInParameter(cmd,"@PcNum",  DbType.Int32,entity.PcNum);
			db.AddInParameter(cmd,"@MobileNum",  DbType.Int32,entity.MobileNum);
			db.AddInParameter(cmd,"@SDataType",  DbType.Int32,entity.SDataType);
			db.AddInParameter(cmd,"@CreateDate",  DbType.Object,entity.CreateDate);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteStatisticDataByKey(int id)
	    {
			string sql=@"delete from StatisticData where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticDataDisabled()
        {
            string sql = @"delete from  StatisticData  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticDataByIds(string ids)
        {
            string sql = @"Delete from StatisticData  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticDataByIds(string ids)
        {
            string sql = @"Update   StatisticData set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   StatisticDataEntity GetStatisticData(int id)
		{
			string sql=@"SELECT  [Id],[SellerId],[TotalNum],[PcNum],[MobileNum],[SDataType],[CreateDate]
							FROM
							dbo.[StatisticData] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		StatisticDataEntity entity=new StatisticDataEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SellerId=StringUtils.GetDbInt(reader["SellerId"]);
					entity.TotalNum=StringUtils.GetDbInt(reader["TotalNum"]);
					entity.PcNum=StringUtils.GetDbInt(reader["PcNum"]);
					entity.MobileNum=StringUtils.GetDbInt(reader["MobileNum"]);
					entity.SDataType=StringUtils.GetDbInt(reader["SDataType"]);
					entity.CreateDate=StringUtils.GetDbString(reader["CreateDate"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<StatisticDataEntity> GetStatisticDataList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[SellerId],[TotalNum],[PcNum],[MobileNum],[SDataType],[CreateDate]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[SellerId],[TotalNum],[PcNum],[MobileNum],[SDataType],[CreateDate] from dbo.[StatisticData] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[StatisticData] with (nolock) ";
            IList<StatisticDataEntity> entityList = new List< StatisticDataEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					StatisticDataEntity entity=new StatisticDataEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SellerId=StringUtils.GetDbInt(reader["SellerId"]);
					entity.TotalNum=StringUtils.GetDbInt(reader["TotalNum"]);
					entity.PcNum=StringUtils.GetDbInt(reader["PcNum"]);
					entity.MobileNum=StringUtils.GetDbInt(reader["MobileNum"]);
					entity.SDataType=StringUtils.GetDbInt(reader["SDataType"]);
					entity.CreateDate=StringUtils.GetDbString(reader["CreateDate"]);
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
        public IList<StatisticDataEntity> GetStatisticDataAll()
        {

            string sql = @"SELECT    [Id],[SellerId],[TotalNum],[PcNum],[MobileNum],[SDataType],[CreateDate] from dbo.[StatisticData] WITH(NOLOCK)	";
		    IList<StatisticDataEntity> entityList = new List<StatisticDataEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   StatisticDataEntity entity=new StatisticDataEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.SellerId=StringUtils.GetDbInt(reader["SellerId"]);
					entity.TotalNum=StringUtils.GetDbInt(reader["TotalNum"]);
					entity.PcNum=StringUtils.GetDbInt(reader["PcNum"]);
					entity.MobileNum=StringUtils.GetDbInt(reader["MobileNum"]);
					entity.SDataType=StringUtils.GetDbInt(reader["SDataType"]);
					entity.CreateDate=StringUtils.GetDbString(reader["CreateDate"]);
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
        public int  ExistNum(StatisticDataEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[StatisticData] WITH(NOLOCK) ";
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
