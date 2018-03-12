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
功能描述：ClassesMenuAD表的数据访问类。
创建时间：2017/6/15 15:25:42
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// ClassesMenuADEntity的数据访问操作
	/// </summary>
	public partial class ClassesMenuADDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ClassesMenuADDA Instance
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
            internal static readonly ClassesMenuADDA instance = new ClassesMenuADDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ClassesMenuAD，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="classesMenuAD">待插入的实体对象</param>
		public int AddClassesMenuAD(ClassesMenuADEntity entity)
		{
		   string sql=@"insert into ClassesMenuAD( [ClassId],[BrandOrProductId],[Sort],[ShowType],[BeginTime],[EndTime],[IsActive])VALUES
			            ( @ClassId,@BrandOrProductId,@Sort,@ShowType,@BeginTime,@EndTime,@IsActive);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandOrProductId",  DbType.Int32,entity.BrandOrProductId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ShowType",  DbType.Int32,entity.ShowType);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="classesMenuAD">待更新的实体对象</param>
		public   int UpdateClassesMenuAD(ClassesMenuADEntity entity)
		{
			string sql=@" UPDATE dbo.[ClassesMenuAD] SET
                       [ClassId]=@ClassId,[BrandOrProductId]=@BrandOrProductId,[Sort]=@Sort,[ShowType]=@ShowType,[BeginTime]=@BeginTime,[EndTime]=@EndTime,[IsActive]=@IsActive
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandOrProductId",  DbType.Int32,entity.BrandOrProductId);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ShowType",  DbType.Int32,entity.ShowType);
			db.AddInParameter(cmd,"@BeginTime",  DbType.DateTime,entity.BeginTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteClassesMenuADByKey(int id)
	    {
			string sql=@"delete from ClassesMenuAD where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassesMenuADDisabled()
        {
            string sql = @"delete from  ClassesMenuAD  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassesMenuADByIds(string ids)
        {
            string sql = @"Delete from ClassesMenuAD  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassesMenuADByIds(string ids)
        {
            string sql = @"Update   ClassesMenuAD set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ClassesMenuADEntity GetClassesMenuAD(int id)
		{
			string sql=@"SELECT  [Id],[ClassId],[BrandOrProductId],[Sort],[ShowType],[BeginTime],[EndTime],[IsActive]
							FROM
							dbo.[ClassesMenuAD] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ClassesMenuADEntity entity=new ClassesMenuADEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandOrProductId=StringUtils.GetDbInt(reader["BrandOrProductId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowType=StringUtils.GetDbInt(reader["ShowType"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ClassesMenuADEntity> GetClassesMenuADList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ClassId],[BrandOrProductId],[Sort],[ShowType],[BeginTime],[EndTime],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ClassId],[BrandOrProductId],[Sort],[ShowType],[BeginTime],[EndTime],[IsActive] from dbo.[ClassesMenuAD] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ClassesMenuAD] with (nolock) ";
            IList<ClassesMenuADEntity> entityList = new List< ClassesMenuADEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ClassesMenuADEntity entity=new ClassesMenuADEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandOrProductId=StringUtils.GetDbInt(reader["BrandOrProductId"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowType=StringUtils.GetDbInt(reader["ShowType"]);
					entity.BeginTime=StringUtils.GetDbDateTime(reader["BeginTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        /// 获取广告位品牌
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public IList<BrandEntity> GetBrandsByClassId(int classid)
        {
            string where = " where a.ShowType=1 and  b.[IsActive]=1   and a.[ClassId]=@ClassId  ";//ShowType=1代表品牌

            string sql = @"SELECT  b.[Id]
                                  ,b.[Code]
                                  ,b.[Name]
                                  ,b.[Title]
                                  ,b.[PYShort]
                                  ,b.[PYFull]
                                  ,b.[Sort]
                                  ,b.[IsActive]
                                  ,b.[IsHot]
                                  ,b.[ParentId]
                                  ,b.PicUrl
							FROM
							dbo.[ClassesMenuAD] a WITH(NOLOCK)	inner join  dbo.Brand b WITH(NOLOCK) on a.BrandId=b.Id
							 " + where + "  Order By a.[Sort] DESC ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
             
            IList<BrandEntity> entityList = new List<BrandEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BrandEntity entity = new BrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entityList.Add(entity);
                }
            }
            return entityList;

        }
        /// <summary>
        /// 获取广告位信息
        /// </summary>
        /// <param name="classid">分类id</param>
        /// <param name="showtype">广告类型：1品牌，2产品</param>
        /// <returns></returns>
        public IList<ClassesMenuADEntity> GetClassesMenuADAll(int classid,int showtype)
        {
            string sql = @"SELECT    [Id],[ClassId],[BrandOrProductId],[Sort],[ShowType],[BeginTime],[EndTime],[IsActive] from dbo.[ClassesMenuAD] WITH(NOLOCK)	
                        where ClassId=@ClassId and ShowType=@ShowType and IsActive=1 and BeginTime<=getdate() and EndTime>=getdate() Order By Sort Desc";
            IList<ClassesMenuADEntity> entityList = new List<ClassesMenuADEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            db.AddInParameter(cmd, "@ShowType", DbType.Int32, showtype);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassesMenuADEntity entity = new ClassesMenuADEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.BrandOrProductId = StringUtils.GetDbInt(reader["BrandOrProductId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.ShowType = StringUtils.GetDbInt(reader["ShowType"]);
                    entity.BeginTime = StringUtils.GetDbDateTime(reader["BeginTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(ClassesMenuADEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ClassesMenuAD] WITH(NOLOCK) ";
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
