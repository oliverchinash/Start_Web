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
功能描述：MemFavorites表的数据访问类。
创建时间：2017/5/5 9:14:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemFavoritesEntity的数据访问操作
	/// </summary>
	public partial class MemFavoritesDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemFavoritesDA Instance
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
            internal static readonly MemFavoritesDA instance = new MemFavoritesDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemFavorites，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memFavorites">待插入的实体对象</param>
		public MemFavoritesEntity AddMemFavorites(MemFavoritesEntity entity)
		{
		   string sql= @"IF ( SELECT COUNT(1)
     FROM   MemFavorites
     WHERE  MemId = @MemId
            AND ProductDetailId = @ProductDetailId
   ) > 0 
    BEGIN
        UPDATE  MemFavorites
        SET     IsActive = 1 ,
                updatetime = GETDATE()
        WHERE   MemId = @MemId
                AND ProductDetailId = @ProductDetailId
 
    END
ELSE 
    BEGIN 
 
        INSERT  INTO MemFavorites
                ( [ProductDetailId] ,
                  [MemId] ,
                  [CreateTime] ,
                  UpdateTime ,
                  [IsActive] ,
                  [SystemId]
                )
        VALUES  ( @ProductDetailId ,
                  @MemId ,
                  GETDATE() ,
                  GETDATE() ,
                  @IsActive ,
                  @SystemId
                ) 

    END

SELECT * FROM MemFavorites  WHERE MemId=@MemId AND ProductDetailId=@ProductDetailId
";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId); 
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@SystemId",  DbType.Int32,entity.SystemId);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.SystemId = StringUtils.GetDbInt(reader["SystemId"]);
                }
            }
            return entity;
        }
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memFavorites">待更新的实体对象</param>
		public   int UpdateMemFavorites(MemFavoritesEntity entity)
		{
			string sql=@" UPDATE dbo.[MemFavorites] SET
                       [ProductDetailId]=@ProductDetailId,[MemId]=@MemId,[CreateTime]=@CreateTime,[IsActive]=@IsActive,[SystemId]=@SystemId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@SystemId",  DbType.Int32,entity.SystemId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemFavoritesByKey(int id)
	    {
			string sql=@"delete from MemFavorites where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemFavoritesDisabled()
        {
            string sql = @"delete from  MemFavorites  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemFavoritesByIds(string ids)
        {
            string sql = @"Delete from MemFavorites  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemFavoritesByIds(string productdetailsid,int memid,int systemid)
        {
            string where = " where ProductDetailId in (" + productdetailsid + ") ";
            if(systemid>0)
            {
                where += " and SystemId=@SystemId ";
            }
            if (memid !=-1)
            {
                where += " and MemId=@MemId ";
            }
            string sql = @"Update   MemFavorites set IsActive=0  "+where;
           
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (systemid > 0)
            {
                db.AddInParameter(cmd, "@SystemId", DbType.Int32, systemid);
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            return db.ExecuteNonQuery(cmd);
        }

        public int DisableMemFavoritesAll(int memid)
        {
            string where = " where 1=1 ";
       
            if (memid != -1)
            {
                where += " and MemId=@MemId ";
            }
            string sql = @"Update   MemFavorites set IsActive=0  " + where;

            DbCommand cmd = db.GetSqlStringCommand(sql);
            
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            return db.ExecuteNonQuery(cmd);
        }

        public int GetMemFavoritesNum(int memid)
        {
            string where = " where IsActive=1 ";

            if (memid != -1)
            {
                where += " and MemId=@MemId ";
            }
            string sql = @"select Count(1) from    MemFavorites   " + where; 
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   MemFavoritesEntity GetMemFavorites(int id)
		{
			string sql=@"SELECT  [Id],[ProductDetailId],[MemId],[CreateTime],[IsActive],[SystemId]
							FROM
							dbo.[MemFavorites] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemFavoritesEntity entity=new MemFavoritesEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.SystemId=StringUtils.GetDbInt(reader["SystemId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<MemFavoritesEntity> GetMemFavoritesList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductDetailId],[MemId],[CreateTime],[IsActive],[SystemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[MemId],[CreateTime],[IsActive],[SystemId] from dbo.[MemFavorites] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemFavorites] with (nolock) ";
            IList<MemFavoritesEntity> entityList = new List< MemFavoritesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemFavoritesEntity entity=new MemFavoritesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.SystemId=StringUtils.GetDbInt(reader["SystemId"]);
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

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWMemFavoritesEntity> GetVWMemFavoritesList(int pageindex, int pagesize, ref int recordCount, int memid, int isactive)
        {
            string where = " where 1=1 ";
            if(memid>0)
            {
                where += " and MemId=@MemId ";
            }
            if (isactive !=-1)
            {
                where += " and IsActive=@IsActive ";
            }
            string sql = @"SELECT   [Id],[ProductDetailId],[MemId],[CreateTime],[UpdateTime],[IsActive],[SystemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY UpdateTime desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[MemId],[CreateTime],UpdateTime,[IsActive],[SystemId] from dbo.[MemFavorites] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[MemFavorites] with (nolock) "+ where;
            IList<VWMemFavoritesEntity> entityList = new List<VWMemFavoritesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid > 0)
            { 
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (isactive != -1)
            { 
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemFavoritesEntity entity = new VWMemFavoritesEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.SystemId = StringUtils.GetDbInt(reader["SystemId"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (isactive != -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
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
        public IList<MemFavoritesEntity> GetMemFavoritesAll()
        {

            string sql = @"SELECT    [Id],[ProductDetailId],[MemId],[CreateTime],[IsActive],[SystemId] from dbo.[MemFavorites] WITH(NOLOCK)	";
		    IList<MemFavoritesEntity> entityList = new List<MemFavoritesEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemFavoritesEntity entity=new MemFavoritesEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.SystemId=StringUtils.GetDbInt(reader["SystemId"]);
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
        public int  ExistNum(int productdetailid,int memid)
        {
            string where = " where MemId=@MemId ";
            if(productdetailid!=-1)
            {
                where += " and ProductDetailId=@ProductDetailId ";
            }
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemFavorites] WITH(NOLOCK) "+ where;
             
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            if (productdetailid != -1)
            {
                db.AddInParameter(cmd, "@ProductDetailId", DbType.Int32, productdetailid);
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
