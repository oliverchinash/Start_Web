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
功能描述：StatisticProductSpec表的数据访问类。
创建时间：2016/11/10 16:04:27
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ProductDB
{
	/// <summary>
	/// StatisticProductSpecEntity的数据访问操作
	/// </summary>
	public partial class StatisticProductSpecDA: BaseSuperMarketDB
    {
        #region 实例化
        public static StatisticProductSpecDA Instance
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
            internal static readonly StatisticProductSpecDA instance = new StatisticProductSpecDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StatisticProductSpec，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticProductSpec">待插入的实体对象</param>
		public int AddStatisticProductSpec(StatisticProductSpecEntity entity)
		{
		   string sql=@"insert into StatisticProductSpec( [StyleId],[Spec1Merge],[Spec2Merge],[Spec3Merge],[CreateTime],[UpdateTime])VALUES
			            ( @StyleId,@Spec1Merge,@Spec2Merge,@Spec3Merge,@CreateTime,@UpdateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@Spec1Merge",  DbType.String,entity.Spec1Merge);
			db.AddInParameter(cmd,"@Spec2Merge",  DbType.String,entity.Spec2Merge);
			db.AddInParameter(cmd,"@Spec3Merge",  DbType.String,entity.Spec3Merge);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticProductSpec">待更新的实体对象</param>
		public   int UpdateStatisticProductSpec(StatisticProductSpecEntity entity)
		{
			string sql=@" UPDATE dbo.[StatisticProductSpec] SET
                       [StyleId]=@StyleId,[Spec1Merge]=@Spec1Merge,[Spec2Merge]=@Spec2Merge,[Spec3Merge]=@Spec3Merge,[CreateTime]=@CreateTime,[UpdateTime]=@UpdateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@Spec1Merge",  DbType.String,entity.Spec1Merge);
			db.AddInParameter(cmd,"@Spec2Merge",  DbType.String,entity.Spec2Merge);
			db.AddInParameter(cmd,"@Spec3Merge",  DbType.String,entity.Spec3Merge);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,entity.UpdateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteStatisticProductSpecByKey(int id)
	    {
			string sql=@"delete from StatisticProductSpec where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticProductSpecDisabled()
        {
            string sql = @"delete from  StatisticProductSpec  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticProductSpecByIds(string ids)
        {
            string sql = @"Delete from StatisticProductSpec  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticProductSpecByIds(string ids)
        {
            string sql = @"Update   StatisticProductSpec set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   StatisticProductSpecEntity GetStatisticProductSpec(int id)
		{
			string sql=@"SELECT  [Id],[StyleId],[Spec1Merge],[Spec2Merge],[Spec3Merge],[CreateTime],[UpdateTime]
							FROM
							dbo.[StatisticProductSpec] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		StatisticProductSpecEntity entity=new StatisticProductSpecEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.Spec1Merge=StringUtils.GetDbString(reader["Spec1Merge"]);
					entity.Spec2Merge=StringUtils.GetDbString(reader["Spec2Merge"]);
					entity.Spec3Merge=StringUtils.GetDbString(reader["Spec3Merge"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
				}
   		    }
            return entity;
		}
        public StatisticProductSpecEntity GetSpecsByStyleId(int styleid)
        {
            string sql = @"SELECT  top 1 [Id],[StyleId],[Spec1Merge],[Spec2Merge],[Spec3Merge],[CreateTime],[UpdateTime]
							FROM
							dbo.[StatisticProductSpec] WITH(NOLOCK)	
							WHERE [StyleId]=@StyleId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@StyleId", DbType.Int32, styleid);
            StatisticProductSpecEntity entity = new StatisticProductSpecEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.Spec1Merge = StringUtils.GetDbString(reader["Spec1Merge"]);
                    entity.Spec2Merge = StringUtils.GetDbString(reader["Spec2Merge"]);
                    entity.Spec3Merge = StringUtils.GetDbString(reader["Spec3Merge"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<StatisticProductSpecEntity> GetStatisticProductSpecList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[StyleId],[Spec1Merge],[Spec2Merge],[Spec3Merge],[CreateTime],[UpdateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[StyleId],[Spec1Merge],[Spec2Merge],[Spec3Merge],[CreateTime],[UpdateTime] from dbo.[StatisticProductSpec] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[StatisticProductSpec] with (nolock) ";
            IList<StatisticProductSpecEntity> entityList = new List< StatisticProductSpecEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					StatisticProductSpecEntity entity=new StatisticProductSpecEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.Spec1Merge=StringUtils.GetDbString(reader["Spec1Merge"]);
					entity.Spec2Merge=StringUtils.GetDbString(reader["Spec2Merge"]);
					entity.Spec3Merge=StringUtils.GetDbString(reader["Spec3Merge"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
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
        public IList<StatisticProductSpecEntity> GetStatisticProductSpecAll()
        {

            string sql = @"SELECT    [Id],[StyleId],[Spec1Merge],[Spec2Merge],[Spec3Merge],[CreateTime],[UpdateTime] from dbo.[StatisticProductSpec] WITH(NOLOCK)	";
		    IList<StatisticProductSpecEntity> entityList = new List<StatisticProductSpecEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   StatisticProductSpecEntity entity=new StatisticProductSpecEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.Spec1Merge=StringUtils.GetDbString(reader["Spec1Merge"]);
					entity.Spec2Merge=StringUtils.GetDbString(reader["Spec2Merge"]);
					entity.Spec3Merge=StringUtils.GetDbString(reader["Spec3Merge"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
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
        public int  ExistNum(StatisticProductSpecEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[StatisticProductSpec] WITH(NOLOCK) ";
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
