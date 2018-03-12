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
功能描述：MemBillCom表的数据访问类。
创建时间：2016/11/17 22:48:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemBillComEntity的数据访问操作
	/// </summary>
	public partial class MemBillComDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemBillComDA Instance
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
            internal static readonly MemBillComDA instance = new MemBillComDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemBillCom，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memBillCom">待插入的实体对象</param>
		public int AddMemBillCom(MemBillComEntity entity)
		{
		   string sql= @"
update MemBillCom set IsDefault=0 where MemId=@MemId 
insert into MemBillCom( [MemId],[Title],[CreateTime],[Sort],[IsDefault],Status)VALUES
			            ( @MemId,@Title,@CreateTime,@Sort,@IsDefault,@Status);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,DateTime.Now.ToString());
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,1);
            db.AddInParameter(cmd,"@IsDefault",  DbType.Int32,entity.IsDefault);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memBillCom">待更新的实体对象</param>
		public   int UpdateMemBillCom(MemBillComEntity entity)
		{
			string sql= @"update MemBillCom set IsDefault=0 where MemId=@MemId 
UPDATE dbo.[MemBillCom] SET [Title]=@Title, UpdateTime=@UpdateTime,[IsDefault]=@IsDefault
                       WHERE [Id]=@Id and [MemId]=@MemId";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@IsDefault",  DbType.Int32,entity.IsDefault);
            db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,DateTime.Now.ToString()); 
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemBillComByKey(int id)
	    {
			string sql=@"delete from MemBillCom where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemBillComDisabled()
        {
            string sql = @"delete from  MemBillCom  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        public int SetDefault(int id, int memid)
        {
            string sql = @"UPDATE dbo.[MemBillCom] SET IsDefault=0 where  [MemId]=@MemId;UPDATE dbo.[MemBillCom] SET IsDefault =1 where Id=@id and  [MemId]=@MemId;  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteBillComByKey(int id, int memid)
        {
            string sql = @"UPDATE dbo.[MemBillCom] SET Status=0,UpdateTime=getdate()  where  Id=@Id and  [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemBillComByIds(string ids)
        {
            string sql = @"Delete from MemBillCom  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBillComByIds(string ids)
        {
            string sql = @"Update   MemBillCom set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemBillComEntity GetMemBillCom(int id)
		{
			string sql=@"SELECT  [Id],[MemId],[Title],[CreateTime],[Sort],[IsDefault]
							FROM
							dbo.[MemBillCom] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemBillComEntity entity=new MemBillComEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<MemBillComEntity> GetMemBillComList(int pagesize, int pageindex, ref  int recordCount,int memid )
		{
            //只取有效的
            string where = " where Status=1 ";
            if (memid > 0)
            {
                where += " and MemId=@MemId";
            }
			string sql=@"SELECT   [Id],[MemId],[Title],[CreateTime],[Sort],[IsDefault]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[Title],[CreateTime],[Sort],[IsDefault] from dbo.[MemBillCom] WITH(NOLOCK)	
						"+ where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemBillCom] with (nolock) "+where;
            IList<MemBillComEntity> entityList = new List< MemBillComEntity>();
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
					MemBillComEntity entity=new MemBillComEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
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
        public IList<MemBillComEntity> GetMemBillComAll()
        {

            string sql = @"SELECT    [Id],[MemId],[Title],[CreateTime],[Sort],[IsDefault] from dbo.[MemBillCom] WITH(NOLOCK)	";
		    IList<MemBillComEntity> entityList = new List<MemBillComEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemBillComEntity entity=new MemBillComEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
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
        public int  ExistNum(MemBillComEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemBillCom] WITH(NOLOCK) ";
            string where = "where Title=@Title";
          
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
         
                db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
          
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
