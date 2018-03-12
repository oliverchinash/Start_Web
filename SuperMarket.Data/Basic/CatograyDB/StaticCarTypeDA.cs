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
功能描述：StaticCarType表的数据访问类。
创建时间：2016/12/30 13:47:42
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// StaticCarTypeEntity的数据访问操作
	/// </summary>
	public partial class StaticCarTypeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static StaticCarTypeDA Instance
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
            internal static readonly StaticCarTypeDA instance = new StaticCarTypeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表StaticCarType，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="staticCarType">待插入的实体对象</param>
		public int AddStaticCarType(StaticCarTypeEntity entity)
		{
		   string sql=@"insert into StaticCarType( [CarTypeId1],[CarTypeName1],[CarTypeId2],[CarTypeName2],[CarTypeId3],[CarTypeName3],[CarTypeId4],[CarTypeName4],[CarTypeClassId])VALUES
			            ( @CarTypeId1,@CarTypeName1,@CarTypeId2,@CarTypeName2,@CarTypeId3,@CarTypeName3,@CarTypeId4,@CarTypeName4,@CarTypeClassId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CarTypeId1",  DbType.Int32,entity.CarTypeId1);
			db.AddInParameter(cmd,"@CarTypeName1",  DbType.String,entity.CarTypeName1);
			db.AddInParameter(cmd,"@CarTypeId2",  DbType.Int32,entity.CarTypeId2);
			db.AddInParameter(cmd,"@CarTypeName2",  DbType.String,entity.CarTypeName2);
			db.AddInParameter(cmd,"@CarTypeId3",  DbType.Int32,entity.CarTypeId3);
			db.AddInParameter(cmd,"@CarTypeName3",  DbType.String,entity.CarTypeName3);
			db.AddInParameter(cmd,"@CarTypeId4",  DbType.Int32,entity.CarTypeId4);
			db.AddInParameter(cmd,"@CarTypeName4",  DbType.String,entity.CarTypeName4);
			db.AddInParameter(cmd,"@CarTypeClassId",  DbType.Int32,entity.CarTypeClassId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="staticCarType">待更新的实体对象</param>
		public   int UpdateStaticCarType(StaticCarTypeEntity entity)
		{
			string sql=@" UPDATE dbo.[StaticCarType] SET
                       [CarTypeId1]=@CarTypeId1,[CarTypeName1]=@CarTypeName1,[CarTypeId2]=@CarTypeId2,[CarTypeName2]=@CarTypeName2,[CarTypeId3]=@CarTypeId3,[CarTypeName3]=@CarTypeName3,[CarTypeId4]=@CarTypeId4,[CarTypeName4]=@CarTypeName4,[CarTypeClassId]=@CarTypeClassId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CarTypeId1",  DbType.Int32,entity.CarTypeId1);
			db.AddInParameter(cmd,"@CarTypeName1",  DbType.String,entity.CarTypeName1);
			db.AddInParameter(cmd,"@CarTypeId2",  DbType.Int32,entity.CarTypeId2);
			db.AddInParameter(cmd,"@CarTypeName2",  DbType.String,entity.CarTypeName2);
			db.AddInParameter(cmd,"@CarTypeId3",  DbType.Int32,entity.CarTypeId3);
			db.AddInParameter(cmd,"@CarTypeName3",  DbType.String,entity.CarTypeName3);
			db.AddInParameter(cmd,"@CarTypeId4",  DbType.Int32,entity.CarTypeId4);
			db.AddInParameter(cmd,"@CarTypeName4",  DbType.String,entity.CarTypeName4);
			db.AddInParameter(cmd,"@CarTypeClassId",  DbType.Int32,entity.CarTypeClassId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteStaticCarTypeByKey(int id)
	    {
			string sql=@"delete from StaticCarType where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStaticCarTypeDisabled()
        {
            string sql = @"delete from  StaticCarType  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStaticCarTypeByIds(string ids)
        {
            string sql = @"Delete from StaticCarType  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStaticCarTypeByIds(string ids)
        {
            string sql = @"Update   StaticCarType set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   StaticCarTypeEntity GetStaticCarType(int id)
		{
			string sql=@"SELECT  [Id],[CarTypeId1],[CarTypeName1],[CarTypeId2],[CarTypeName2],[CarTypeId3],[CarTypeName3],[CarTypeId4],[CarTypeName4],[CarTypeClassId]
							FROM
							dbo.[StaticCarType] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		StaticCarTypeEntity entity=new StaticCarTypeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CarTypeId1=StringUtils.GetDbInt(reader["CarTypeId1"]);
					entity.CarTypeName1=StringUtils.GetDbString(reader["CarTypeName1"]);
					entity.CarTypeId2=StringUtils.GetDbInt(reader["CarTypeId2"]);
					entity.CarTypeName2=StringUtils.GetDbString(reader["CarTypeName2"]);
					entity.CarTypeId3=StringUtils.GetDbInt(reader["CarTypeId3"]);
					entity.CarTypeName3=StringUtils.GetDbString(reader["CarTypeName3"]);
					entity.CarTypeId4=StringUtils.GetDbInt(reader["CarTypeId4"]);
					entity.CarTypeName4=StringUtils.GetDbString(reader["CarTypeName4"]);
					entity.CarTypeClassId=StringUtils.GetDbInt(reader["CarTypeClassId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<StaticCarTypeEntity> GetStaticCarTypeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[CarTypeId1],[CarTypeName1],[CarTypeId2],[CarTypeName2],[CarTypeId3],[CarTypeName3],[CarTypeId4],[CarTypeName4],[CarTypeClassId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CarTypeId1],[CarTypeName1],[CarTypeId2],[CarTypeName2],[CarTypeId3],[CarTypeName3],[CarTypeId4],[CarTypeName4],[CarTypeClassId] from dbo.[StaticCarType] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[StaticCarType] with (nolock) ";
            IList<StaticCarTypeEntity> entityList = new List< StaticCarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					StaticCarTypeEntity entity=new StaticCarTypeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CarTypeId1=StringUtils.GetDbInt(reader["CarTypeId1"]);
					entity.CarTypeName1=StringUtils.GetDbString(reader["CarTypeName1"]);
					entity.CarTypeId2=StringUtils.GetDbInt(reader["CarTypeId2"]);
					entity.CarTypeName2=StringUtils.GetDbString(reader["CarTypeName2"]);
					entity.CarTypeId3=StringUtils.GetDbInt(reader["CarTypeId3"]);
					entity.CarTypeName3=StringUtils.GetDbString(reader["CarTypeName3"]);
					entity.CarTypeId4=StringUtils.GetDbInt(reader["CarTypeId4"]);
					entity.CarTypeName4=StringUtils.GetDbString(reader["CarTypeName4"]);
					entity.CarTypeClassId=StringUtils.GetDbInt(reader["CarTypeClassId"]);
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
        public IList<StaticCarTypeEntity> GetStaticCarTypeAll()
        {

            string sql = @"SELECT    [Id],[CarTypeId1],[CarTypeName1],[CarTypeId2],[CarTypeName2],[CarTypeId3],[CarTypeName3],[CarTypeId4],[CarTypeName4],[CarTypeClassId] from dbo.[StaticCarType] WITH(NOLOCK)	";
		    IList<StaticCarTypeEntity> entityList = new List<StaticCarTypeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   StaticCarTypeEntity entity=new StaticCarTypeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CarTypeId1=StringUtils.GetDbInt(reader["CarTypeId1"]);
					entity.CarTypeName1=StringUtils.GetDbString(reader["CarTypeName1"]);
					entity.CarTypeId2=StringUtils.GetDbInt(reader["CarTypeId2"]);
					entity.CarTypeName2=StringUtils.GetDbString(reader["CarTypeName2"]);
					entity.CarTypeId3=StringUtils.GetDbInt(reader["CarTypeId3"]);
					entity.CarTypeName3=StringUtils.GetDbString(reader["CarTypeName3"]);
					entity.CarTypeId4=StringUtils.GetDbInt(reader["CarTypeId4"]);
					entity.CarTypeName4=StringUtils.GetDbString(reader["CarTypeName4"]);
					entity.CarTypeClassId=StringUtils.GetDbInt(reader["CarTypeClassId"]);
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
        public int  ExistNum(StaticCarTypeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[StaticCarType] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CarTypeName1=@CarTypeName1) ";
					     where = where+ "  (CarTypeName2=@CarTypeName2) ";
					     where = where+ "  (CarTypeName3=@CarTypeName3) ";
					     where = where+ "  (CarTypeName4=@CarTypeName4) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CarTypeName1=@CarTypeName1) ";
					     where = where+ " id<>@Id and  (CarTypeName2=@CarTypeName2) ";
					     where = where+ " id<>@Id and  (CarTypeName3=@CarTypeName3) ";
					     where = where+ " id<>@Id and  (CarTypeName4=@CarTypeName4) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CarTypeName1", DbType.String, entity.CarTypeName1); 
					
            db.AddInParameter(cmd, "@CarTypeName2", DbType.String, entity.CarTypeName2); 
					
            db.AddInParameter(cmd, "@CarTypeName3", DbType.String, entity.CarTypeName3); 
					
            db.AddInParameter(cmd, "@CarTypeName4", DbType.String, entity.CarTypeName4); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
