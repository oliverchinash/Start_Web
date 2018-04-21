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
功能描述：ClassBrand表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
    /// <summary>
    /// ClassBrandEntity的数据访问操作
    /// </summary>
    public partial class ClassBrandDA : BaseSuperMarketDB
    {
        #region 实例化
        public static ClassBrandDA Instance
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
            internal static readonly ClassBrandDA instance = new ClassBrandDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表ClassBrand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="classBrand">待插入的实体对象</param>
        public int AddClassBrand(ClassBrandEntity entity)
        {
            string sql = @"insert into ClassBrand( [ClassId],[BrandId],[Sort],Status)VALUES
			            ( @ClassId,@BrandId,@Sort,@Status);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="classBrand">待更新的实体对象</param>
        public int UpdateClassBrand(ClassBrandEntity entity)
        {
            string sql = @" UPDATE dbo.[ClassBrand] SET
                       [ClassId]=@ClassId,[BrandId]=@BrandId,[Sort]=@Sort
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassBrandByKey(int id)
        {
            string sql = @"delete from ClassBrand where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassBrandByBrandId(int brandid)
        {
            string sql = @"delete from ClassBrand where BrandId=@BrandId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassBrandDisabled()
        {
            string sql = @"delete from  ClassBrand  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassBrandByIds(string ids)
        {
            string sql = @"Delete from ClassBrand  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassBrandByIds(string ids)
        {
            string sql = @"Update   ClassBrand set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public ClassBrandEntity GetClassBrand(int id)
        {
            string sql = @"SELECT  [Id],[ClassId],[BrandId],[Sort]
							FROM
							dbo.[ClassBrand] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            ClassBrandEntity entity = new ClassBrandEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }
        public IList<BrandEntity> GetBrandByClass(int classid )
        {
            string where = " where b.[IsActive]=1 ";
            if (classid != -1)
            {
                where += " and a.[ClassId]=@ClassId";
            } 
            string sql = @"SELECT  b.[Id]
                                  ,b.[Code]
                                  ,b.[Name]
                                  ,b.[Title]
                                  ,b.[PYShort]
                                  ,b.[PYFull]
                                  ,b.[Sort]
                                  ,b.[IsActive]
                                  ,b.[IsHot] 
							FROM
							dbo.[ClassBrand] a WITH(NOLOCK)	inner join  dbo.Brand b WITH(NOLOCK) on a.BrandId=b.Id
							 " + where + "  Order By b.[Sort] DESC ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (classid != -1)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            } 
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
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
      
        public BrandEntity GetBrandByCB(int classid, int brandid)
        {
            string where = " where 1=1 ";
            if (classid != -1)
            {
                where += " and a.[ClassId]=@ClassId";
            }
            if (brandid != -1)
            {
                where += " and a.[BrandId]=@BrandId";
            }
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
							FROM
							dbo.[ClassBrand] a WITH(NOLOCK)	inner join  dbo.Brand b WITH(NOLOCK) on a.BrandId=b.Id
							 " + where + "  Order By b.[Sort] DESC ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (classid != -1)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (brandid != -1)
            {
                db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            }
             BrandEntity entity = new  BrandEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                { 
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
                 }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ClassBrandEntity> GetClassBrandList(int pagesize, int pageindex, ref int recordCount, int classid)
        {
            string where = " WHERE  1=1 ";

            if (classid > 0)
            {
                where+= "  and ClassId=@ClassId ";
             
            }

            string sql = @"SELECT   [Id],[ClassId],[BrandId],[Sort],[Name],[Manufacturer]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],a.[ClassId],a.[BrandId],a.[Sort] ,b.[Name],b.[Manufacturer] from dbo.[ClassBrand] a WITH(NOLOCK)	
						 inner join dbo.[Brand] b WITH(NOLOCK) on a.BrandId = b.Id " + where +@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[ClassBrand] a with (nolock) " + where;
            IList<ClassBrandEntity> entityList = new List<ClassBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassBrandEntity entity = new ClassBrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    if (classid > 0)
                    {
                        entity.BrandName = StringUtils.GetDbString(reader["Name"]);
                        entity.Manufacturer = StringUtils.GetDbString(reader["Manufacturer"]);
                    }
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
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
        public IList<ClassBrandEntity> GetClassBrandAll()
        {

            string sql = @"SELECT    [Id],[ClassId],[BrandId],[Sort] from dbo.[ClassBrand] WITH(NOLOCK)	";
            IList<ClassBrandEntity> entityList = new List<ClassBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassBrandEntity entity = new ClassBrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
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
        public int ExistNum(ClassBrandEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ClassBrand] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where += " BrandId=@BrandId and [ClassId]=@ClassId ";
            }
            else
            {
                where += " Id<>@Id and BrandId=@BrandId and [ClassId]=@ClassId ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, entity.BrandId);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
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
