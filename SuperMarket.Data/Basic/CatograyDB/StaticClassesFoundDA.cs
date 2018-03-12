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
功能描述：StaticClassesFound表的数据访问类。
创建时间：2016/12/30 13:47:43
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// StaticClassesFoundEntity的数据访问操作
	/// </summary>
	public partial class StaticClassesFoundDA: BaseSuperMarketDB
    {
        #region 实例化
        public static StaticClassesFoundDA Instance
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
            internal static readonly StaticClassesFoundDA instance = new StaticClassesFoundDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StaticClassesFound，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="staticClassesFound">待插入的实体对象</param>
		public int AddStaticClassesFound(StaticClassesFoundEntity entity)
		{
		   string sql=@"insert into StaticClassesFound( [ClassId1],[ClassName1],[ClassId2],[ClassName2],[ClassId3],[ClassName3],[ClassId4],[ClassName4],[ClassEndId])VALUES
			            ( @ClassId1,@ClassName1,@ClassId2,@ClassName2,@ClassId3,@ClassName3,@ClassId4,@ClassName4,@ClassEndId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassId1",  DbType.Int32,entity.ClassId1);
			db.AddInParameter(cmd,"@ClassName1",  DbType.String,entity.ClassName1);
			db.AddInParameter(cmd,"@ClassId2",  DbType.Int32,entity.ClassId2);
			db.AddInParameter(cmd,"@ClassName2",  DbType.String,entity.ClassName2);
			db.AddInParameter(cmd,"@ClassId3",  DbType.Int32,entity.ClassId3);
			db.AddInParameter(cmd,"@ClassName3",  DbType.String,entity.ClassName3);
			db.AddInParameter(cmd,"@ClassId4",  DbType.Int32,entity.ClassId4);
			db.AddInParameter(cmd,"@ClassName4",  DbType.String,entity.ClassName4);
			db.AddInParameter(cmd,"@ClassEndId",  DbType.Int32,entity.ClassEndId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="staticClassesFound">待更新的实体对象</param>
		public   int UpdateStaticClassesFound(StaticClassesFoundEntity entity)
		{
			string sql=@" UPDATE dbo.[StaticClassesFound] SET
                       [ClassId1]=@ClassId1,[ClassName1]=@ClassName1,[ClassId2]=@ClassId2,[ClassName2]=@ClassName2,[ClassId3]=@ClassId3,[ClassName3]=@ClassName3,[ClassId4]=@ClassId4,[ClassName4]=@ClassName4,[ClassEndId]=@ClassEndId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassId1",  DbType.Int32,entity.ClassId1);
			db.AddInParameter(cmd,"@ClassName1",  DbType.String,entity.ClassName1);
			db.AddInParameter(cmd,"@ClassId2",  DbType.Int32,entity.ClassId2);
			db.AddInParameter(cmd,"@ClassName2",  DbType.String,entity.ClassName2);
			db.AddInParameter(cmd,"@ClassId3",  DbType.Int32,entity.ClassId3);
			db.AddInParameter(cmd,"@ClassName3",  DbType.String,entity.ClassName3);
			db.AddInParameter(cmd,"@ClassId4",  DbType.Int32,entity.ClassId4);
			db.AddInParameter(cmd,"@ClassName4",  DbType.String,entity.ClassName4);
			db.AddInParameter(cmd,"@ClassEndId",  DbType.Int32,entity.ClassEndId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteStaticClassesFoundByKey(int id)
	    {
			string sql=@"delete from StaticClassesFound where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStaticClassesFoundDisabled()
        {
            string sql = @"delete from  StaticClassesFound  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStaticClassesFoundByIds(string ids)
        {
            string sql = @"Delete from StaticClassesFound  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStaticClassesFoundByIds(string ids)
        {
            string sql = @"Update   StaticClassesFound set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   StaticClassesFoundEntity GetStaticClassesFound(int id)
		{
			string sql=@"SELECT  [Id],[ClassId1],[ClassName1],[ClassId2],[ClassName2],[ClassId3],[ClassName3],[ClassId4],[ClassName4],[ClassEndId]
							FROM
							dbo.[StaticClassesFound] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		StaticClassesFoundEntity entity=new StaticClassesFoundEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId1=StringUtils.GetDbInt(reader["ClassId1"]);
					entity.ClassName1=StringUtils.GetDbString(reader["ClassName1"]);
					entity.ClassId2=StringUtils.GetDbInt(reader["ClassId2"]);
					entity.ClassName2=StringUtils.GetDbString(reader["ClassName2"]);
					entity.ClassId3=StringUtils.GetDbInt(reader["ClassId3"]);
					entity.ClassName3=StringUtils.GetDbString(reader["ClassName3"]);
					entity.ClassId4=StringUtils.GetDbInt(reader["ClassId4"]);
					entity.ClassName4=StringUtils.GetDbString(reader["ClassName4"]);
					entity.ClassEndId=StringUtils.GetDbInt(reader["ClassEndId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<StaticClassesFoundEntity> GetStaticClassesFoundList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ClassId1],[ClassName1],[ClassId2],[ClassName2],[ClassId3],[ClassName3],[ClassId4],[ClassName4],[ClassEndId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ClassId1],[ClassName1],[ClassId2],[ClassName2],[ClassId3],[ClassName3],[ClassId4],[ClassName4],[ClassEndId] from dbo.[StaticClassesFound] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[StaticClassesFound] with (nolock) ";
            IList<StaticClassesFoundEntity> entityList = new List< StaticClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					StaticClassesFoundEntity entity=new StaticClassesFoundEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId1=StringUtils.GetDbInt(reader["ClassId1"]);
					entity.ClassName1=StringUtils.GetDbString(reader["ClassName1"]);
					entity.ClassId2=StringUtils.GetDbInt(reader["ClassId2"]);
					entity.ClassName2=StringUtils.GetDbString(reader["ClassName2"]);
					entity.ClassId3=StringUtils.GetDbInt(reader["ClassId3"]);
					entity.ClassName3=StringUtils.GetDbString(reader["ClassName3"]);
					entity.ClassId4=StringUtils.GetDbInt(reader["ClassId4"]);
					entity.ClassName4=StringUtils.GetDbString(reader["ClassName4"]);
					entity.ClassEndId=StringUtils.GetDbInt(reader["ClassEndId"]);
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
        public IList<StaticClassesFoundEntity> GetSubStaticByClassId(int classid, int level)
        {
            string where = " where 1=1 ";
            if (classid > 0)
            { 
                if (level == 0 || level == 1)
                {
                    where += " and ClassId1=@ClassId";
                }
                else if (level == 2)
                {
                    where += " and ClassId2=@ClassId";
                }
                else if (level == 3)
                {
                    where += " and ClassId3=@ClassId";
                }
                else if (level == 4)
                {
                    where += " and ClassId4=@ClassId";
                }
            }

            string sql = @"SELECT    [Id],[ClassId1],[ClassName1],[ClassId2],[ClassName2],[ClassId3],[ClassName3],[ClassId4],[ClassName4],[ClassEndId],ClassType from dbo.[StaticClassesFound] WITH(NOLOCK)	"+where;
            IList<StaticClassesFoundEntity> entityList = new List<StaticClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    StaticClassesFoundEntity entity = new StaticClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId1 = StringUtils.GetDbInt(reader["ClassId1"]);
                    entity.ClassName1 = StringUtils.GetDbString(reader["ClassName1"]);
                    entity.ClassId2 = StringUtils.GetDbInt(reader["ClassId2"]);
                    entity.ClassName2 = StringUtils.GetDbString(reader["ClassName2"]);
                    entity.ClassId3 = StringUtils.GetDbInt(reader["ClassId3"]);
                    entity.ClassName3 = StringUtils.GetDbString(reader["ClassName3"]);
                    entity.ClassId4 = StringUtils.GetDbInt(reader["ClassId4"]);
                    entity.ClassName4 = StringUtils.GetDbString(reader["ClassName4"]);
                    entity.ClassEndId = StringUtils.GetDbInt(reader["ClassEndId"]);
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);

                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<StaticClassesFoundEntity> GetStaticClassesFoundAll()
        {

            string sql = @"SELECT    [Id],[ClassId1],[ClassName1],[ClassId2],[ClassName2],[ClassId3],[ClassName3],[ClassId4],[ClassName4],[ClassEndId],ClassType from dbo.[StaticClassesFound] WITH(NOLOCK)	";
		    IList<StaticClassesFoundEntity> entityList = new List<StaticClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   StaticClassesFoundEntity entity=new StaticClassesFoundEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId1=StringUtils.GetDbInt(reader["ClassId1"]);
					entity.ClassName1=StringUtils.GetDbString(reader["ClassName1"]);
					entity.ClassId2=StringUtils.GetDbInt(reader["ClassId2"]);
					entity.ClassName2=StringUtils.GetDbString(reader["ClassName2"]);
					entity.ClassId3=StringUtils.GetDbInt(reader["ClassId3"]);
					entity.ClassName3=StringUtils.GetDbString(reader["ClassName3"]);
					entity.ClassId4=StringUtils.GetDbInt(reader["ClassId4"]);
					entity.ClassName4=StringUtils.GetDbString(reader["ClassName4"]);
					entity.ClassEndId=StringUtils.GetDbInt(reader["ClassEndId"]); 
					entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);  

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
        public int  ExistNum(StaticClassesFoundEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[StaticClassesFound] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (ClassName1=@ClassName1) ";
					     where = where+ "  (ClassName2=@ClassName2) ";
					     where = where+ "  (ClassName3=@ClassName3) ";
					     where = where+ "  (ClassName4=@ClassName4) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (ClassName1=@ClassName1) ";
					     where = where+ " id<>@Id and  (ClassName2=@ClassName2) ";
					     where = where+ " id<>@Id and  (ClassName3=@ClassName3) ";
					     where = where+ " id<>@Id and  (ClassName4=@ClassName4) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@ClassName1", DbType.String, entity.ClassName1); 
					
            db.AddInParameter(cmd, "@ClassName2", DbType.String, entity.ClassName2); 
					
            db.AddInParameter(cmd, "@ClassName3", DbType.String, entity.ClassName3); 
					
            db.AddInParameter(cmd, "@ClassName4", DbType.String, entity.ClassName4); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
