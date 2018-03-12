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
功能描述：SuggestRecord表的数据访问类。
创建时间：2017/5/9 11:53:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// SuggestRecordEntity的数据访问操作
	/// </summary>
	public partial class SuggestRecordDA: BaseSuperMarketDB
    {
        #region 实例化
        public static SuggestRecordDA Instance
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
            internal static readonly SuggestRecordDA instance = new SuggestRecordDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表SuggestRecord，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="suggestRecord">待插入的实体对象</param>
		public int AddSuggestRecord(SuggestRecordEntity entity)
		{
		   string sql=@"insert into SuggestRecord( [MemId],[ProductNum],[ProductPrice],[KFService],[WLService],[SuggestDetail],[HasCheck],[HasRecived],[CreateTime])VALUES
			            ( @MemId,@ProductNum,@ProductPrice,@KFService,@WLService,@SuggestDetail,0,0,getdate());
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@ProductNum",  DbType.String,entity.ProductNum);
			db.AddInParameter(cmd,"@ProductPrice",  DbType.String,entity.ProductPrice);
			db.AddInParameter(cmd,"@KFService",  DbType.String,entity.KFService);
			db.AddInParameter(cmd,"@WLService",  DbType.String,entity.WLService);
			db.AddInParameter(cmd,"@SuggestDetail",  DbType.String,entity.SuggestDetail);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="suggestRecord">待更新的实体对象</param>
		public   int UpdateSuggestRecord(SuggestRecordEntity entity)
		{
			string sql=@" UPDATE dbo.[SuggestRecord] SET
                       [MemId]=@MemId,[ProductNum]=@ProductNum,[ProductPrice]=@ProductPrice,[KFService]=@KFService,[WLService]=@WLService,[SuggestDetail]=@SuggestDetail,[HasCheck]=@HasCheck,[HasRecived]=@HasRecived,[CreateTime]=@CreateTime,[CheckTime]=@CheckTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@ProductNum",  DbType.String,entity.ProductNum);
			db.AddInParameter(cmd,"@ProductPrice",  DbType.String,entity.ProductPrice);
			db.AddInParameter(cmd,"@KFService",  DbType.String,entity.KFService);
			db.AddInParameter(cmd,"@WLService",  DbType.String,entity.WLService);
			db.AddInParameter(cmd,"@SuggestDetail",  DbType.String,entity.SuggestDetail);
			db.AddInParameter(cmd,"@HasCheck",  DbType.Int32,entity.HasCheck);
			db.AddInParameter(cmd,"@HasRecived",  DbType.Int32,entity.HasRecived);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteSuggestRecordByKey(int id)
	    {
			string sql=@"delete from SuggestRecord where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSuggestRecordDisabled()
        {
            string sql = @"delete from  SuggestRecord  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSuggestRecordByIds(string ids)
        {
            string sql = @"Delete from SuggestRecord  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSuggestRecordByIds(string ids)
        {
            string sql = @"Update   SuggestRecord set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   SuggestRecordEntity GetSuggestRecord(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[ProductNum],[ProductPrice],[KFService],[WLService],[SuggestDetail],[HasCheck],[HasRecived],[CreateTime],[CheckTime]
							FROM
							dbo.[SuggestRecord] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		SuggestRecordEntity entity=new SuggestRecordEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.ProductNum=StringUtils.GetDbString(reader["ProductNum"]);
					entity.ProductPrice=StringUtils.GetDbString(reader["ProductPrice"]);
					entity.KFService=StringUtils.GetDbString(reader["KFService"]);
					entity.WLService=StringUtils.GetDbString(reader["WLService"]);
					entity.SuggestDetail=StringUtils.GetDbString(reader["SuggestDetail"]);
					entity.HasCheck=StringUtils.GetDbInt(reader["HasCheck"]);
					entity.HasRecived=StringUtils.GetDbInt(reader["HasRecived"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<SuggestRecordEntity> GetSuggestRecordList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[ProductNum],[ProductPrice],[KFService],[WLService],[SuggestDetail],[HasCheck],[HasRecived],[CreateTime],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[ProductNum],[ProductPrice],[KFService],[WLService],[SuggestDetail],[HasCheck],[HasRecived],[CreateTime],[CheckTime] from dbo.[SuggestRecord] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[SuggestRecord] with (nolock) ";
            IList<SuggestRecordEntity> entityList = new List< SuggestRecordEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					SuggestRecordEntity entity=new SuggestRecordEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.ProductNum=StringUtils.GetDbString(reader["ProductNum"]);
					entity.ProductPrice=StringUtils.GetDbString(reader["ProductPrice"]);
					entity.KFService=StringUtils.GetDbString(reader["KFService"]);
					entity.WLService=StringUtils.GetDbString(reader["WLService"]);
					entity.SuggestDetail=StringUtils.GetDbString(reader["SuggestDetail"]);
					entity.HasCheck=StringUtils.GetDbInt(reader["HasCheck"]);
					entity.HasRecived=StringUtils.GetDbInt(reader["HasRecived"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public IList<SuggestRecordEntity> GetSuggestRecordAll()
        {

            string sql = @"SELECT    [Id],[MemId],[ProductNum],[ProductPrice],[KFService],[WLService],[SuggestDetail],[HasCheck],[HasRecived],[CreateTime],[CheckTime] from dbo.[SuggestRecord] WITH(NOLOCK)	";
		    IList<SuggestRecordEntity> entityList = new List<SuggestRecordEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   SuggestRecordEntity entity=new SuggestRecordEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.ProductNum=StringUtils.GetDbString(reader["ProductNum"]);
					entity.ProductPrice=StringUtils.GetDbString(reader["ProductPrice"]);
					entity.KFService=StringUtils.GetDbString(reader["KFService"]);
					entity.WLService=StringUtils.GetDbString(reader["WLService"]);
					entity.SuggestDetail=StringUtils.GetDbString(reader["SuggestDetail"]);
					entity.HasCheck=StringUtils.GetDbInt(reader["HasCheck"]);
					entity.HasRecived=StringUtils.GetDbInt(reader["HasRecived"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public int  ExistNum(SuggestRecordEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[SuggestRecord] WITH(NOLOCK) ";
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
