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
功能描述：MemBrowseLog表的数据访问类。
创建时间：2017/5/5 9:14:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemBrowseLogEntity的数据访问操作
	/// </summary>
	public partial class MemBrowseLogDA : BaseSuperMarketDB
    {
        #region 实例化
        public static MemBrowseLogDA Instance
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
            internal static readonly MemBrowseLogDA instance = new MemBrowseLogDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemBrowseLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="MemBrowseLog">待插入的实体对象</param>
		public int AddMemBrowseLog(MemBrowseLogEntity entity)
		{
		   string sql= @" IF EXISTS ( SELECT  1
            FROM    dbo.MemBrowseLog
            WHERE   [MemId] = @MemId
                    AND ProductDetailId = @ProductDetailId ) 
    BEGIN 
        UPDATE  [dbo].[MemBrowseLog]
        SET     [UpdateTime] = GETDATE() ,
                [ClickNum] = ClickNum + 1 ,
                [SystemId] = @SystemId
        WHERE   [ProductDetailId] = @ProductDetailId
                AND [MemId] = @MemId
    END
ELSE 
    BEGIN 
        INSERT  INTO [dbo].[MemBrowseLog]
                ( [ProductDetailId] ,
                  [MemId] ,
                  [CreateTime] ,
                  [UpdateTime] ,
                  [ClickNum] ,
                  [SystemId]
                )
        VALUES  ( @ProductDetailId ,
                  @MemId ,
                  GETDATE() ,
                  GETDATE() ,
                  0 ,
                  @SystemId
                )
    END";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);  
			db.AddInParameter(cmd,"@SystemId",  DbType.Int32,entity.SystemId);   

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddMemBrowseLogStr(string MemBrowsesStr,int memid,int systemid)
        {

            string sql = @"UPDATE a SET [UpdateTime]=getdate(),[ClickNum]=[ClickNum]+1 FROM   MemBrowseLog a RIGHT JOIN dbo.fun_splitstr(@MemBrowsesStr  ,',') b ON a.ProductDetailId=b.ID
                            WHERE a.MemId=@MemId AND a.id IS NOT NULL 
                            INSERT INTO [JcMemberDB].[dbo].[MemBrowseLog]([ProductDetailId],[MemId],[CreateTime],[UpdateTime],[ClickNum],[SystemId] )
                            SELECT b.id,@MemId,GETDATE(),GETDATE(),1,@SystemId FROM   MemBrowseLog a RIGHT JOIN dbo.fun_splitstr(@MemBrowsesStr ,',') b ON a.ProductDetailId=b.ID
                            WHERE a.id IS NULL
                             ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemBrowsesStr", DbType.String, MemBrowsesStr);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@SystemId", DbType.Int32, systemid);

           return db.ExecuteNonQuery(cmd);
         
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="MemBrowseLog">待更新的实体对象</param>
        public   int UpdateMemBrowseLog(MemBrowseLogEntity entity)
		{
			string sql=@" UPDATE dbo.[MemBrowseLog] SET
                       [ProductDetailId]=@ProductDetailId,[MemId]=@MemId,[CreateTime]=@CreateTime,[ClickNum]=@ClickNum,[SystemId]=@SystemId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@ClickNum",  DbType.Int32,entity.ClickNum);
			db.AddInParameter(cmd,"@SystemId",  DbType.Int32,entity.SystemId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemBrowseLogByKey(int id)
	    {
			string sql=@"delete from MemBrowseLog where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemBrowseLogDisabled()
        {
            string sql = @"delete from  MemBrowseLog  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemBrowseLogByIds(string ids)
        {
            string sql = @"Delete from MemBrowseLog  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBrowseLogByIds(string ids)
        {
            string sql = @"Update   MemBrowseLog set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemBrowseLogEntity GetMemBrowseLog(int id)
		{
			string sql=@"SELECT  [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId]
							FROM
							dbo.[MemBrowseLog] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemBrowseLogEntity entity=new MemBrowseLogEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.ClickNum=StringUtils.GetDbInt(reader["ClickNum"]);
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
		public   IList<MemBrowseLogEntity> GetMemBrowseLogList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId] from dbo.[MemBrowseLog] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemBrowseLog] with (nolock) ";
            IList<MemBrowseLogEntity> entityList = new List< MemBrowseLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemBrowseLogEntity entity=new MemBrowseLogEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.ClickNum=StringUtils.GetDbInt(reader["ClickNum"]);
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


        public IList<VWMemBrowseLogEntity> GetVWMemFavoritesList(int pageindex, int pagesize, ref int recordCount, int memid )
        {
            string where = " where 1=1 ";
            if (memid > 0)
            {
                where += " and MemId=@MemId ";
            } 
            string sql = @"SELECT   [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY CreateTime desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId] from dbo.[MemBrowseLog] WITH(NOLOCK)	
						"+where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[MemBrowseLog] with (nolock) "+ where;
            IList<VWMemBrowseLogEntity> entityList = new List<VWMemBrowseLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWMemBrowseLogEntity entity = new VWMemBrowseLogEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.ClickNum = StringUtils.GetDbInt(reader["ClickNum"]);
                    entity.SystemId = StringUtils.GetDbInt(reader["SystemId"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
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
        public IList<MemBrowseLogEntity> GetMemBrowseLogAll()
        {

            string sql = @"SELECT    [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId] from dbo.[MemBrowseLog] WITH(NOLOCK)	";
		    IList<MemBrowseLogEntity> entityList = new List<MemBrowseLogEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemBrowseLogEntity entity=new MemBrowseLogEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ProductDetailId=StringUtils.GetDbInt(reader["ProductDetailId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.ClickNum=StringUtils.GetDbInt(reader["ClickNum"]);
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
        public int  ExistNum(MemBrowseLogEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemBrowseLog] WITH(NOLOCK) ";
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
