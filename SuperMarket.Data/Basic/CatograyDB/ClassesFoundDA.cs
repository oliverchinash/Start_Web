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
功能描述：ClassesFound表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
    /// <summary>
    /// ClassesFoundEntity的数据访问操作
    /// </summary>
    public partial class ClassesFoundDA : BaseSuperMarketDB
    {
        #region 实例化
        public static ClassesFoundDA Instance
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
            internal static readonly ClassesFoundDA instance = new ClassesFoundDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
   //     /// <summary>
   //     /// 插入一条记录到表ClassesFound，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
   //     /// </summary>
   //     /// <param name="db">数据库操作对象</param>
   //     /// <param name="classesFound">待插入的实体对象</param>
   //     public int AddClassesFound(ClassesFoundEntity entity)
   //     {
   //         string sql = @"insert into ClassesFound( [Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[HasSpecific],[SpecificTitle],[ParentId],[StyleEnd],[HasProduct])VALUES
			//            ( @Code,@Name,@PYFirst,@PYShort,@PYFull,@AdId,@Sort,@IsActive,@IsHot,@CreateTime,@UpdateTime,@ClassLevel,@IsEnd,@HasProperties,@HasSpecific,@SpecificTitle,@ParentId,@StyleEnd,@HasProduct);
			//SELECT SCOPE_IDENTITY();";
   //         DbCommand cmd = db.GetSqlStringCommand(sql);

   //         db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
   //         db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
   //         db.AddInParameter(cmd, "@PYFirst", DbType.String, entity.PYFirst);
   //         db.AddInParameter(cmd, "@PYShort", DbType.String, entity.PYShort);
   //         db.AddInParameter(cmd, "@PYFull", DbType.String, entity.PYFull);
   //         db.AddInParameter(cmd, "@AdId", DbType.Int32, entity.AdId);
   //         db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
   //         db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
   //         db.AddInParameter(cmd, "@IsHot", DbType.Int32, entity.IsHot);
   //         db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
   //         db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
   //         db.AddInParameter(cmd, "@ClassLevel", DbType.Int32, entity.ClassLevel);
   //         db.AddInParameter(cmd, "@IsEnd", DbType.Int32, entity.IsEnd);
   //         db.AddInParameter(cmd, "@HasProperties", DbType.Int32, entity.HasProperties);
   //         db.AddInParameter(cmd, "@ParentId", DbType.Int32, entity.ParentId);
   //         object identity = db.ExecuteScalar(cmd);
   //         if (identity == null || identity == DBNull.Value) return 0;
   //         return Convert.ToInt32(identity);
   //     }

        /// <summary>
        /// 插入一条记录到表ClassesFound，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="classesFound">待插入的实体对象</param>
        public int AddClassesFound(ClassesFoundEntity entity)
        {
            string sql = @"insert into ClassesFound( [Code],[Name],[FullName],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[ParentId],[HasProperties],[PropertiesClassId],[HasProduct],[SiteId],ClassType)VALUES
			            ( @Code,@Name,@FullName,@PYFirst,@PYShort,@PYFull,@AdId,@Sort,@IsActive,@IsHot,@CreateTime,@UpdateTime,@ClassLevel,@IsEnd,@ParentId,@HasProperties,@PropertiesClassId,@HasProduct,@SiteId,@ClassType);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@FullName", DbType.String, entity.FullName);
            db.AddInParameter(cmd, "@PYFirst", DbType.String, entity.PYFirst);
            db.AddInParameter(cmd, "@PYShort", DbType.String, entity.PYShort);
            db.AddInParameter(cmd, "@PYFull", DbType.String, entity.PYFull);
            db.AddInParameter(cmd, "@AdId", DbType.Int32, entity.AdId);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            db.AddInParameter(cmd, "@IsHot", DbType.Int32, entity.IsHot);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
            db.AddInParameter(cmd, "@ClassLevel", DbType.Int32, entity.ClassLevel);
            db.AddInParameter(cmd, "@IsEnd", DbType.Int32, entity.IsEnd);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, entity.ParentId);
            db.AddInParameter(cmd, "@HasProperties", DbType.Int32, entity.HasProperties);
            db.AddInParameter(cmd, "@PropertiesClassId", DbType.Int32, entity.PropertiesClassId);
            db.AddInParameter(cmd, "@HasProduct", DbType.Int32, entity.HasProduct);
            db.AddInParameter(cmd, "@SiteId", DbType.Int32, entity.SiteId); 
            db.AddInParameter(cmd, "@ClassType", DbType.Int32, entity.ClassType); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="classesFound">待更新的实体对象</param>
        public int UpdateClassesFound(ClassesFoundEntity entity)
        {
            string sql = @" UPDATE dbo.[ClassesFound] SET
                       [Code]=@Code,[Name]=@Name,[FullName]=@FullName,[PYFirst]=@PYFirst,[PYShort]=@PYShort,[PYFull]=@PYFull,[AdId]=@AdId,[Sort]=@Sort,[IsActive]=@IsActive,[IsHot]=@IsHot,[CreateTime]=@CreateTime,[UpdateTime]=@UpdateTime,[ClassLevel]=@ClassLevel,[IsEnd]=@IsEnd,[HasProperties]=@HasProperties, [ParentId]=@ParentId, [ClassType]=@ClassType 
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@FullName", DbType.String, entity.FullName);
            db.AddInParameter(cmd, "@PYFirst", DbType.String, entity.PYFirst);
            db.AddInParameter(cmd, "@PYShort", DbType.String, entity.PYShort);
            db.AddInParameter(cmd, "@PYFull", DbType.String, entity.PYFull);
            db.AddInParameter(cmd, "@AdId", DbType.Int32, entity.AdId);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            db.AddInParameter(cmd, "@IsHot", DbType.Int32, entity.IsHot);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, entity.UpdateTime);
            db.AddInParameter(cmd, "@ClassLevel", DbType.Int32, entity.ClassLevel);
            db.AddInParameter(cmd, "@IsEnd", DbType.Int32, entity.IsEnd);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, entity.ParentId);
            db.AddInParameter(cmd, "@HasProperties", DbType.Int32, entity.HasProperties);
            db.AddInParameter(cmd, "@PropertiesClassId", DbType.Int32, entity.PropertiesClassId);
            db.AddInParameter(cmd, "@HasProduct", DbType.Int32, entity.HasProduct);
            db.AddInParameter(cmd, "@ClassType", DbType.Int32, entity.ClassType);

            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassesFoundByKey(int id)
        {
            string sql = @"delete from ClassesFound where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }



        /// <summary>
        ///  
        /// </summary>
        public int DeleteClassesFoundByParentId(int parentid)
        {
            string sql = @"delete from ClassesFound where ParentId=@ParentId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassesFoundDisabled()
        {
            string sql = @"delete from  ClassesFound  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassesFoundByIds(string ids)
        {
            string sql = @"Delete from ClassesFound  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassesFoundByIds(string ids)
        {
            string sql = @"Update   ClassesFound set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 做生效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int EnableClassesFoundByIds(string ids)
        {
            string sql = @"Update   ClassesFound set IsActive=1  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public ClassesFoundEntity GetClassesFound(int id)
        {
            string sql = @"SELECT  [Id],[Code],[FullName],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[HasProduct],PropertiesClassId, [ParentId],
RedirectClassId,ClassType,SiteId  FROM dbo.[ClassesFound] WITH(NOLOCK)	 WHERE [Id]=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            ClassesFoundEntity entity = new ClassesFoundEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.FullName = StringUtils.GetDbString(reader["FullName"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.PropertiesClassId = StringUtils.GetDbInt(reader["PropertiesClassId"]); 
                    
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.RedirectClassId = StringUtils.GetDbInt(reader["RedirectClassId"]); 
                    entity.SiteId = StringUtils.GetDbInt(reader["SiteId"]); 

                }
            }
            return entity;
        }
        public ClassesFoundEntity GetClassesFoundByName(string name, int classtype,int ClassMenuType,int isend)
        {
            string sql = @"
   
 IF ( SELECT    COUNT(1)
      FROM      dbo.[ClassesFound] WITH ( NOLOCK )
      WHERE     [Name] = @Name
                AND IsActive = 1
                AND RedirectClassId = 0 
                AND IsEnd = @IsEnd
                AND ClassType = @ClassType
                AND ClassMenuType = @ClassMenuType
    ) = 1 
    BEGIN    
        SELECT  [Id] ,
                [Code] ,
                [FullName] ,
                [Name] ,
                [PYFirst] ,
                [PYShort] ,
                [PYFull] ,
                [AdId] ,
                [Sort] ,
                [IsActive] ,
                [IsHot] ,
                [CreateTime] ,
                [UpdateTime] ,
                [ClassLevel] ,
                [IsEnd] ,
                [HasProperties] ,
                [HasProduct] ,
                PropertiesClassId ,
                [ParentId] ,
                ClassType ,
                RedirectClassId
        FROM    dbo.[ClassesFound] WITH ( NOLOCK )
        WHERE   [Name] = @Name
                AND IsActive = 1
                AND RedirectClassId = 0
                AND IsEnd = @IsEnd
                AND ClassType = @ClassType
                AND ClassMenuType = @ClassMenuType
    END
 ELSE 
    BEGIN  
        SELECT  [Id] ,
                [Code] ,
                [FullName] ,
                [Name] ,
                [PYFirst] ,
                [PYShort] ,
                [PYFull] ,
                [AdId] ,
                [Sort] ,
                [IsActive] ,
                [IsHot] ,
                [CreateTime] ,
                [UpdateTime] ,
                [ClassLevel] ,
                [IsEnd] ,
                [HasProperties] ,
                [HasProduct] ,
                PropertiesClassId ,
                [ParentId] ,
                ClassType ,
                RedirectClassId
        FROM    dbo.[ClassesFound] WITH ( NOLOCK )
        WHERE   [FullName] = @Name
                AND IsActive = 1
                AND RedirectClassId = 0
                AND IsEnd = @IsEnd
                AND ClassType = @ClassType
                AND ClassMenuType = @ClassMenuType
    END
        ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Name", DbType.String, name);
            db.AddInParameter(cmd, "@ClassType", DbType.String, classtype);
            db.AddInParameter(cmd, "@IsEnd", DbType.Int16, isend);
           
            db.AddInParameter(cmd, "@ClassMenuType", DbType.String, ClassMenuType);
            ClassesFoundEntity entity = new ClassesFoundEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.FullName = StringUtils.GetDbString(reader["FullName"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.PropertiesClassId = StringUtils.GetDbInt(reader["PropertiesClassId"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);
                    entity.RedirectClassId = StringUtils.GetDbInt(reader["RedirectClassId"]);

                }
            }
            return entity;
        }
 
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ClassesFoundEntity> GetClassesFoundList(int pagesize, int pageindex, ref int recordCount, int level, string name,int parentid,int isactive,int classtype,int classmenutype,int siteid)
        { 
            
            string where = "WHERE  1=1";
            if(siteid > 0)
            {
                where += " and   SiteId=@SiteId "; 
            } 
            if (level > 0)
            {
                where += " And ClassLevel=@ClassLevel";
            }
            if (isactive > -1)
            {
                where += " And IsActive=@IsActive";
            }
            if (parentid>-1)
            {
                where += " And ParentId=@ParentId";
            }
            if (name != string.Empty)
            {
                where += " And Name like @Name";
            }
            if (classtype !=-1)
            {
                where += " And ClassType = @ClassType";
            }
            if (classmenutype != -1)
            {
                where += " And ClassMenuType = @ClassMenuType";
            }
            string sql = @"SELECT   [Id],[Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties] ,[ParentId],ClassType 
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[ParentId],ClassType  from dbo.[ClassesFound] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[ClassesFound] with (nolock) " + where;
            IList<ClassesFoundEntity> entityList = new List<ClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (siteid > 0)
            {
                db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            }
            if (level > 0)
            {
                db.AddInParameter(cmd, "@ClassLevel", DbType.Int32, level);
            }
            if (isactive > -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            if (parentid > -1)
            {
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            }
            if (name != string.Empty)
            {
                db.AddInParameter(cmd, "@Name", DbType.String, "%" + name + "%");
            }
            if (classtype != -1)
            { 
                db.AddInParameter(cmd, "@ClassType", DbType.Int32, classtype);
            }
            if (classmenutype != -1)
            {
                db.AddInParameter(cmd, "@ClassMenuType", DbType.Int32, classmenutype); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassesFoundEntity entity = new ClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);  
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]); 
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (siteid > 0)
            {
                db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            } 
            if (level > 0)
            {
                db.AddInParameter(cmd, "@ClassLevel", DbType.Int32, level);
            }
            if (isactive > -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            if (parentid > -1)
            {
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            }
            if (name != string.Empty)
            {
                db.AddInParameter(cmd, "@Name", DbType.String, "%" + name + "%");
            }
            if (classtype != -1)
            {
                db.AddInParameter(cmd, "@ClassType", DbType.Int32, classtype);
            }
            if (classmenutype != -1)
            {
                db.AddInParameter(cmd, "@ClassMenuType", DbType.Int32, classmenutype);
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
        /// 根据分类所属类型，如普通，导航，首页导航等
        /// </summary>
        /// <param name="classnenutype"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public IList<VWClassesFoundEntity> GetClassMenuAll(int siteid,   int parentid  )
        {
            string where = " where 1=1 and IsActive=1";
            if(siteid>0)
            {
                where += " and SiteId=@SiteId";
            } 
            if (parentid != -1)
            {
                where += " and ParentId=@ParentId";
            } 
            string sql = @"SELECT    [Id],[Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[ParentId],PropertiesClassId,HasProduct, RedirectClassId  from dbo.[ClassesFound] WITH(NOLOCK)	" + where + " Order By parentid ,Sort desc";
            IList<VWClassesFoundEntity> entityList = new List<VWClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (siteid != -1)
            {
                db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            } 
            if (parentid != -1)
            {
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid); 
            } 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWClassesFoundEntity entity = new VWClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.PropertiesClassId = StringUtils.GetDbInt(reader["PropertiesClassId"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]); 
                    entity.RedirectClassId = StringUtils.GetDbInt(reader["RedirectClassId"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public IList<ClassesFoundEntity> GetClassesAllByPId(int pid, int level, int siteid)
        {
            string where = " where 1=1 and IsActive=1";

            if (pid > 0)
            {
                where += " and ParentId=@ParentId";
            }
            else
            {
                if (pid == 0)
                {
                    where += " and ParentId=0";
                }
                if (siteid != -1)
                {

                    where += " and SiteId=@SiteId";
                }
            }
            string sql = @"SELECT    [Id],[Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[ParentId],PropertiesClassId,HasProduct,ClassType ,RedirectClassId  from dbo.[ClassesFound] WITH(NOLOCK)	" + where+ " Order By Sort desc" ;
            IList<ClassesFoundEntity> entityList = new List<ClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);

            if (pid > 0)
            {
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
            }
            else
            { 
                if (pid <= 0 && siteid != -1)
                {
                    db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
                }
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassesFoundEntity entity = new ClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]); 
                    entity.PropertiesClassId = StringUtils.GetDbInt(reader["PropertiesClassId"]); 
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);  
                    entity.ClassType = StringUtils.GetDbInt(reader["ClassType"]);  
                    entity.RedirectClassId = StringUtils.GetDbInt(reader["RedirectClassId"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<ClassesFoundEntity> GetClassesAllByBrandId(int siteid,int brandid)
        {
            string where = " where  a.BrandId=@BrandId ";
            if(siteid > 0)
            {
                where += " and b.SiteId=@SiteId ";
            }
            string sql = @" SELECT b.[Id],[Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],b.[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[ParentId],PropertiesClassId,HasProduct,ClassType
                            from ClassBrand a  WITH(NOLOCK)	 inner join  dbo.[ClassesFound] b WITH(NOLOCK) ON a.ClassId=b.Id  "+ where;
            IList<ClassesFoundEntity> entityList = new List<ClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (brandid != -1)
            {
                db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            }
            if (siteid > 0)
            { 
                db.AddInParameter(cmd, "@SiteId", DbType.Int32, siteid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassesFoundEntity entity = new ClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.PropertiesClassId = StringUtils.GetDbInt(reader["PropertiesClassId"]);
                    entity.HasProduct = StringUtils.GetDbInt(reader["HasProduct"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
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
        public IList<ClassesFoundEntity> GetClassesFoundAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[PYFirst],[PYShort],[PYFull],[AdId],[Sort],[IsActive],[IsHot],[CreateTime],[UpdateTime],[ClassLevel],[IsEnd],[HasProperties],[HasSpecific],[SpecificTitle],[ParentId],[StyleEnd],[HasProduct] from dbo.[ClassesFound] WITH(NOLOCK)	";
            IList<ClassesFoundEntity> entityList = new List<ClassesFoundEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ClassesFoundEntity entity = new ClassesFoundEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.AdId = StringUtils.GetDbInt(reader["AdId"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.ClassLevel = StringUtils.GetDbInt(reader["ClassLevel"]);
                    entity.IsEnd = StringUtils.GetDbInt(reader["IsEnd"]);
                    entity.HasProperties = StringUtils.GetDbInt(reader["HasProperties"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
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
        public int ExistNum(ClassesFoundEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ClassesFound] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "  (Name=@Name) ";

            }
            else
            {
                where = where + " id<>@Id and  (Name=@Name) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }

            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }








        #endregion
        #endregion
    }
}
