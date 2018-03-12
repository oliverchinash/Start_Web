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
功能描述：MemCGScope表的数据访问类。
创建时间：2017/9/22 15:02:36
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemCGScopeEntity的数据访问操作
	/// </summary>
	public partial class MemCGScopeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemCGScopeDA Instance
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
            internal static readonly MemCGScopeDA instance = new MemCGScopeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemCGScope，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memCGScope">待插入的实体对象</param>
		public int AddMemCGScope(MemCGScopeEntity entity)
		{
		   string sql= @"insert into MemCGScope( [CGMemId],[BrandId],[BrandName],[ClassId],[ClassName],ScopeType,[CreateManId],[CreateTime])VALUES
			            ( @CGMemId,@BrandId,@BrandName,@ClassId,@ClassName,@ScopeType,@CreateManId,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@BrandName",  DbType.String,entity.BrandName);
			db.AddInParameter(cmd,"@ScopeType",  DbType.Int32, entity.ScopeType);
            db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ClassName",  DbType.String,entity.ClassName);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memCGScope">待更新的实体对象</param>
		public   int UpdateMemCGScope(MemCGScopeEntity entity)
		{
			string sql=@" UPDATE dbo.[MemCGScope] SET
                       [CGMemId]=@CGMemId,[BrandId]=@BrandId,[BrandName]=@BrandName,[ClassId]=@ClassId,[ClassName]=@ClassName,[CreateManId]=@CreateManId,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@BrandName",  DbType.String,entity.BrandName);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@ClassName",  DbType.String,entity.ClassName);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemCGScopeByKey(int id,int cgmemid)
	    {
			string sql= @"delete from MemCGScope where Id=@Id and CGMemId=@CGMemId";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
		    db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemCGScopeDisabled()
        {
            string sql = @"delete from  MemCGScope  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemCGScopeByIds(string ids)
        {
            string sql = @"Delete from MemCGScope  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemCGScopeByIds(string ids)
        {
            string sql = @"Update   MemCGScope set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemCGScopeEntity GetMemCGScope(int id)
		{
			string sql= @"SELECT  [Id],[CGMemId],[BrandId],[BrandName],[ClassId],[ClassName],ScopeType,[CreateManId],[CreateTime]
							FROM
							dbo.[MemCGScope] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemCGScopeEntity entity=new MemCGScopeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.BrandName=StringUtils.GetDbString(reader["BrandName"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ClassName=StringUtils.GetDbString(reader["ClassName"]);
					entity.ScopeType=StringUtils.GetDbInt(reader["ScopeType"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
        /// <summary>
        /// 获取车型件供应商前两个,派单供报价 
        /// </summary>
        /// <param name="carbrandid"></param>
        /// <param name="carbrandname"></param>
        /// <returns></returns>
        public string GetCGMemIdsByCarBrand(string carbrandname,string oldmemids)
        {
            string memids = "";
            string sql = @"SELECT  [CGMemId],row_number() OVER (PARTITION BY [CGMemId] ORDER BY   LevelSort desc,TakeSort desc,QuoteSort desc,
                          ReadSort desc,NoteSort DESC)  AS RowNum ,
                          LevelSort  ,TakeSort  ,QuoteSort  ,
                          ReadSort  ,NoteSort  into #tempbrand
							FROM
						  dbo.[MemCGScope] a WITH(NOLOCK) inner join dbo.fun_splitstr(@BrandNames,'|') n on a.BrandName=n.ID  LEFT JOIN dbo.fun_splitstr(@OldMemIdStr,',') b ON a.[CGMemId]=b.ID	where IsActive=1  AND b.Id IS null 
select top 4 [CGMemId] from #tempbrand where RowNum=1 order by LevelSort desc,TakeSort desc,QuoteSort desc,
                          ReadSort desc,NoteSort desc
";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@BrandNames", DbType.String, carbrandname);
            db.AddInParameter(cmd, "@OldMemIdStr", DbType.String, oldmemids);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    memids += StringUtils.GetDbInt(reader["CGMemId"])+",";  
                }
            }
            return memids;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<MemCGScopeEntity> GetMemCGScopeList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[CGMemId],[BrandId],[BrandName],[ClassId],[ClassName],ScopeType,[CreateManId],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGMemId],[BrandId],[BrandName],[ClassId],[ClassName],ScopeType,[CreateManId],[CreateTime] from dbo.[MemCGScope] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemCGScope] with (nolock) ";
            IList<MemCGScopeEntity> entityList = new List< MemCGScopeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemCGScopeEntity entity=new MemCGScopeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.BrandName=StringUtils.GetDbString(reader["BrandName"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ClassName=StringUtils.GetDbString(reader["ClassName"]);
					entity.ScopeType=StringUtils.GetDbInt(reader["ScopeType"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<MemCGScopeEntity> GetMemCGScopeAllByMemId(int memid)
        {
            string where = " where 1=1 ";
            if(memid>0)
            {
                where += " and CGMemId=@CGMemId ";
            }
            string sql = @"SELECT    [Id],[CGMemId],[BrandId],[BrandName],[ClassId],[ClassName],ScopeType,[CreateManId],[CreateTime] from dbo.[MemCGScope] WITH(NOLOCK)	" + where;
		    IList<MemCGScopeEntity> entityList = new List<MemCGScopeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (memid > 0)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemCGScopeEntity entity=new MemCGScopeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.BrandName=StringUtils.GetDbString(reader["BrandName"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.ClassName=StringUtils.GetDbString(reader["ClassName"]);
					entity.ScopeType=StringUtils.GetDbInt(reader["ScopeType"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(MemCGScopeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemCGScope] WITH(NOLOCK) where BrandName=@BrandName and ClassName=@ClassName and CGMemId=@CGMemId  "; 
            DbCommand cmd = db.GetSqlStringCommand(sql);  
            db.AddInParameter(cmd, "@BrandName", DbType.String, entity.BrandName);  
            db.AddInParameter(cmd, "@ClassName", DbType.String, entity.ClassName); 
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
