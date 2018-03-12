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
功能描述：MemProductClick表的数据访问类。
创建时间：2017/5/5 9:14:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// MemProductClickEntity的数据访问操作
	/// </summary>
	public partial class MemProductClickDA : BaseSuperMarketDB
    {
        #region 实例化
        public static MemProductClickDA Instance
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
            internal static readonly MemProductClickDA instance = new MemProductClickDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemProductClick，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="MemProductClick">待插入的实体对象</param>
		public int AddMemProductClick(MemProductClickEntity entity)
		{
		   string sql= @" 
insert into MemProductClick( [ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId],ClickIp)VALUES
			            ( @ProductDetailId,@MemId,getdate(),@ClickNum,@SystemId,@ClickIp);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductDetailId",  DbType.Int32,entity.ProductDetailId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId); 
			db.AddInParameter(cmd,"@ClickNum",  DbType.Int32,entity.ClickNum);
			db.AddInParameter(cmd,"@SystemId",  DbType.Int32,entity.SystemId);  
			db.AddInParameter(cmd, "@ClickIp",  DbType.String,entity.ClickIp); 

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="MemProductClick">待更新的实体对象</param>
		public   int UpdateMemProductClick(MemProductClickEntity entity)
		{
			string sql=@" UPDATE dbo.[MemProductClick] SET
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
		public  int  DeleteMemProductClickByKey(int id)
	    {
			string sql=@"delete from MemProductClick where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemProductClickDisabled()
        {
            string sql = @"delete from  MemProductClick  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemProductClickByIds(string ids)
        {
            string sql = @"Delete from MemProductClick  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemProductClickByIds(string ids)
        {
            string sql = @"Update   MemProductClick set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemProductClickEntity GetMemProductClick(int id)
		{
			string sql=@"SELECT  [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId]
							FROM
							dbo.[MemProductClick] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemProductClickEntity entity=new MemProductClickEntity();
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
		public   IList<MemProductClickEntity> GetMemProductClickList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId] from dbo.[MemProductClick] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemProductClick] with (nolock) ";
            IList<MemProductClickEntity> entityList = new List< MemProductClickEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemProductClickEntity entity=new MemProductClickEntity();
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
		
		        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<MemProductClickEntity> GetMemProductClickAll()
        {

            string sql = @"SELECT    [Id],[ProductDetailId],[MemId],[CreateTime],[ClickNum],[SystemId] from dbo.[MemProductClick] WITH(NOLOCK)	";
		    IList<MemProductClickEntity> entityList = new List<MemProductClickEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemProductClickEntity entity=new MemProductClickEntity();
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
        public int  ExistNum(MemProductClickEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemProductClick] WITH(NOLOCK) ";
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
