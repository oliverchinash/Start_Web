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
功能描述：IssueContent表的数据访问类。
创建时间：2016/10/8 13:48:13
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.HelpDB
{
    /// <summary>
    /// IssueContentEntity的数据访问操作
    /// </summary>
    public partial class IssueContentDA : BaseSuperMarketDB
    {
        #region 实例化
        public static IssueContentDA Instance
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
            internal static readonly IssueContentDA instance = new IssueContentDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表IssueContent，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="issueContent">待插入的实体对象</param>
        public int AddIssueContent(IssueContentEntity entity)
        {
            string sql = @"insert into IssueContent( [ClassId],[IssueTitle],[IssueContent],[IsActive],[Sort])VALUES
			            ( @ClassId,@IssueTitle,@IssueContent,@IsActive,@Sort);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@IssueTitle", DbType.String, entity.IssueTitle);
            db.AddInParameter(cmd, "@IssueContent", DbType.String, entity.IssueContent);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="issueContent">待更新的实体对象</param>
        public int UpdateIssueContent(IssueContentEntity entity)
        {
            string sql = @" UPDATE dbo.[IssueContent] SET
                       [ClassId]=@ClassId,[IssueTitle]=@IssueTitle,[IssueContent]=@IssueContent,[IsActive]=@IsActive,[Sort]=@Sort
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@ClassId", DbType.Int32, entity.ClassId);
            db.AddInParameter(cmd, "@IssueTitle", DbType.String, entity.IssueTitle);
            db.AddInParameter(cmd, "@IssueContent", DbType.String, entity.IssueContent);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            db.AddInParameter(cmd, "@Sort", DbType.Int32, entity.Sort);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteIssueContentByKey(int id)
        {
            string sql = @"delete from IssueContent where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIssueContentDisabled()
        {
            string sql = @"delete from  IssueContent  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIssueContentByIds(string ids)
        {
            string sql = @"Delete from IssueContent  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIssueContentByIds(string ids)
        {
            string sql = @"Update   IssueContent set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IssueContentEntity GetIssueContent(int id)
        {
            string sql = @"SELECT  [Id],[ClassId],[IssueTitle],[IssueContent],[IsActive],[Sort]
							FROM
							dbo.[IssueContent] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            IssueContentEntity entity = new IssueContentEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.IssueTitle = StringUtils.GetDbString(reader["IssueTitle"]);
                    entity.IssueContent = StringUtils.GetDbString(reader["IssueContent"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }


        public IssueContentEntity GetIssueContentByName(string title)
        {
            string sql = @"SELECT  [Id],[ClassId],[IssueTitle],[IssueContent],[IsActive],[Sort]
							FROM
							dbo.[IssueContent] WITH(NOLOCK)	
							WHERE [IssueTitle]=@IssueTitle";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@IssueTitle", DbType.String, title);
            IssueContentEntity entity = new IssueContentEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.IssueTitle = StringUtils.GetDbString(reader["IssueTitle"]);
                    entity.IssueContent = StringUtils.GetDbString(reader["IssueContent"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }


        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<IssueContentEntity> GetIssueContentList(int pagesize, int pageindex, ref int recordCount, int id, string issuetitle, int isactive,int classid,int _systype)
        {
            string where = "WHERE  1=1";
            if (id > 0)
            {
                where += " AND a.Id=@Id";
            }
            if (!string.IsNullOrEmpty(issuetitle))
            {
                where += " AND a.IssueTitle like @IssueTitle";
            }
            if (isactive > -1)
            {
                where += " AND a.IsActive=@IsActive";
            }
            if (classid>0)
            {
                where += " AND ClassId=@ClassId";
            }
            if (_systype !=-1)
            {
                where += " AND a.[SystemType]=@SystemType";
            }
            
            string sql = @"SELECT   [Id],[ClassId],ClassName,[IssueTitle],[IssueContent],[IsActive],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.[Sort] desc) AS ROWNUMBER,
						 a.[Id],a.[ClassId],b.ClassName,a.[IssueTitle],a.[IssueContent],a.[IsActive],a.[Sort] from dbo.[IssueContent] a WITH(NOLOCK)
inner  join IssueClass b  WITH(NOLOCK) on a.ClassId=b.id	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[IssueContent] a with (nolock) " + where;
            IList<IssueContentEntity> entityList = new List<IssueContentEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            }
            if ( !string.IsNullOrEmpty(issuetitle))
            {
                db.AddInParameter(cmd, "@IssueTitle", DbType.String, "%" + issuetitle + "%");
            }
            if (isactive > -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (_systype != -1)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, _systype);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    IssueContentEntity entity = new IssueContentEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.ClassName= StringUtils.GetDbString(reader["ClassName"]);
                    entity.IssueTitle = StringUtils.GetDbString(reader["IssueTitle"]);
                    entity.IssueContent = StringUtils.GetDbString(reader["IssueContent"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            }
            if (!string.IsNullOrEmpty(issuetitle))
            {
                db.AddInParameter(cmd, "@IssueTitle", DbType.String, "%" + issuetitle + "%");
            }
            if (isactive > -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            if (_systype != -1)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, _systype);
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
        public IList<IssueContentEntity> GetIssueContentAll()
        {

            string sql = @"SELECT    [Id],[ClassId],[IssueTitle],[IssueContent],[IsActive],[Sort] from dbo.[IssueContent] WITH(NOLOCK)	";
            IList<IssueContentEntity> entityList = new List<IssueContentEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    IssueContentEntity entity = new IssueContentEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.IssueTitle = StringUtils.GetDbString(reader["IssueTitle"]);
                    entity.IssueContent = StringUtils.GetDbString(reader["IssueContent"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
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
        public int ExistNum(IssueContentEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[IssueContent] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where += " IssueTitle=@IssueTitle";
            }
            else
            {
                where += " Id!=@Id AND IssueTitle=@IssueTitle";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@IssueTitle", DbType.String, entity.IssueTitle);
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
