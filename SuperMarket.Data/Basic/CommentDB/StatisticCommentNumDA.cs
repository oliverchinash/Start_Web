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
功能描述：StatisticCommentNum表的数据访问类。
创建时间：2016/11/11 23:30:51
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CommentDB
{
	/// <summary>
	/// StatisticCommentNumEntity的数据访问操作
	/// </summary>
	public partial class StatisticCommentNumDA: BaseSuperMarketDB
    {
        #region 实例化
        public static StatisticCommentNumDA Instance
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
            internal static readonly StatisticCommentNumDA instance = new StatisticCommentNumDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StatisticCommentNum，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticCommentNum">待插入的实体对象</param>
		public int AddStatisticCommentNum(StatisticCommentNumEntity entity)
		{
		   string sql=@"insert into StatisticCommentNum( [ProductId],[StyleId],[FavNum],[GeneralNum],[BadNum])VALUES
			            ( @ProductId,@StyleId,@FavNum,@GeneralNum,@BadNum);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@StyleId",  DbType.Int32,entity.StyleId);
			db.AddInParameter(cmd,"@FavNum",  DbType.Int32,entity.FavNum);
			db.AddInParameter(cmd,"@GeneralNum",  DbType.Int32,entity.GeneralNum);
			db.AddInParameter(cmd,"@BadNum",  DbType.Int32,entity.BadNum);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="statisticCommentNum">待更新的实体对象</param>
		public   int UpdateCommentContent(int id,string content)
		{
			string sql= @" UPDATE dbo.[StatisticCommentNum] SET CommentContent=@CommentContent
                       WHERE [Id]=@id and CommentContent=''";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32, id);
			db.AddInParameter(cmd, "@CommentContent",  DbType.String, content); 
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int UpdateStatisticCommentNum(StatisticCommentNumEntity entity)
        {
            string sql = @" UPDATE dbo.[StatisticCommentNum] SET
                       [ProductId]=@ProductId,[StyleId]=@StyleId,[FavNum]=@FavNum,[GeneralNum]=@GeneralNum,[BadNum]=@BadNum
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@StyleId", DbType.Int32, entity.StyleId);
            db.AddInParameter(cmd, "@FavNum", DbType.Int32, entity.FavNum);
            db.AddInParameter(cmd, "@GeneralNum", DbType.Int32, entity.GeneralNum);
            db.AddInParameter(cmd, "@BadNum", DbType.Int32, entity.BadNum);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteStatisticCommentNumByKey(int id)
	    {
			string sql=@"delete from StatisticCommentNum where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticCommentNumDisabled()
        {
            string sql = @"delete from  StatisticCommentNum  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticCommentNumByIds(string ids)
        {
            string sql = @"Delete from StatisticCommentNum  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticCommentNumByIds(string ids)
        {
            string sql = @"Update   StatisticCommentNum set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   StatisticCommentNumEntity GetStatisticCommentNum(int id)
		{
			string sql=@"SELECT  [Id],[ProductId],[StyleId],[FavNum],[GeneralNum],[BadNum]
							FROM
							dbo.[StatisticCommentNum] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		StatisticCommentNumEntity entity=new StatisticCommentNumEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.FavNum=StringUtils.GetDbInt(reader["FavNum"]);
					entity.GeneralNum=StringUtils.GetDbInt(reader["GeneralNum"]);
					entity.BadNum=StringUtils.GetDbInt(reader["BadNum"]);
				}
   		    }
            return entity;
		}
        public StatisticCommentNumEntity GetCommentNumByProductId(int productid)
        {
            string sql = @"SELECT top 1 [Id],[ProductId],[StyleId],[FavNum],[GeneralNum],[BadNum],[FavRate],[GeneralRate],[BadRate],AllNum,CommentContent
							FROM
							dbo.[StatisticCommentNum] WITH(NOLOCK)	
							WHERE [ProductId]=@ProductId order by id desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductId", DbType.Int32, productid);
            StatisticCommentNumEntity entity = new StatisticCommentNumEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.StyleId = StringUtils.GetDbInt(reader["StyleId"]);
                    entity.AllNum = StringUtils.GetDbInt(reader["AllNum"]);
                    entity.FavNum = StringUtils.GetDbInt(reader["FavNum"]);
                    entity.GeneralNum = StringUtils.GetDbInt(reader["GeneralNum"]);
                    entity.BadNum = StringUtils.GetDbInt(reader["BadNum"]);
                    entity.FavRate = StringUtils.GetDbInt(reader["FavRate"]);
                    entity.GeneralRate = StringUtils.GetDbInt(reader["GeneralRate"]);
                    entity.BadRate = StringUtils.GetDbInt(reader["BadRate"]);
                    entity.CommentContent = StringUtils.GetDbString(reader["CommentContent"]);  
                }
            }
            return entity;
            
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<StatisticCommentNumEntity> GetStatisticCommentNumList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductId],[StyleId],[FavNum],[GeneralNum],[BadNum]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductId],[StyleId],[FavNum],[GeneralNum],[BadNum] from dbo.[StatisticCommentNum] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[StatisticCommentNum] with (nolock) ";
            IList<StatisticCommentNumEntity> entityList = new List< StatisticCommentNumEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					StatisticCommentNumEntity entity=new StatisticCommentNumEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.FavNum=StringUtils.GetDbInt(reader["FavNum"]);
					entity.GeneralNum=StringUtils.GetDbInt(reader["GeneralNum"]);
					entity.BadNum=StringUtils.GetDbInt(reader["BadNum"]);
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
        public IList<StatisticCommentNumEntity> GetStatisticCommentNumAll()
        {

            string sql = @"SELECT    [Id],[ProductId],[StyleId],[FavNum],[GeneralNum],[BadNum] from dbo.[StatisticCommentNum] WITH(NOLOCK)	";
		    IList<StatisticCommentNumEntity> entityList = new List<StatisticCommentNumEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   StatisticCommentNumEntity entity=new StatisticCommentNumEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.StyleId=StringUtils.GetDbInt(reader["StyleId"]);
					entity.FavNum=StringUtils.GetDbInt(reader["FavNum"]);
					entity.GeneralNum=StringUtils.GetDbInt(reader["GeneralNum"]);
					entity.BadNum=StringUtils.GetDbInt(reader["BadNum"]);
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
        public int  ExistNum(StatisticCommentNumEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[StatisticCommentNum] WITH(NOLOCK) ";
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
