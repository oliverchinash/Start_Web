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
功能描述：CmsTemplet表的数据访问类。
创建时间：2016/8/16 13:54:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data
{
    /// <summary>
    /// CmsTempletEntity的数据访问操作
    /// </summary>
    public partial class CmsTempletDA : BaseSuperMarketDB
    {
        #region 实例化
        public static CmsTempletDA Instance
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
            internal static readonly CmsTempletDA instance = new CmsTempletDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表CmsTemplet，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cmsTemplet">待插入的实体对象</param>
        public int AddCmsTemplet(CmsTempletEntity entity)
        {
            string sql = @"insert into CmsTemplet( [Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive])VALUES
			            ( @Title,@TempletContent,@ProductNum,@CMSType,@CreateDate,@IsActive);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@TempletContent", DbType.String, entity.TempletContent);
            db.AddInParameter(cmd, "@ProductNum", DbType.Int32, entity.ProductNum);
            db.AddInParameter(cmd, "@CMSType", DbType.Int32, entity.CMSType);
            db.AddInParameter(cmd, "@CreateDate", DbType.DateTime, entity.CreateDate);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cmsTemplet">待更新的实体对象</param>
        public int UpdateCmsTemplet(CmsTempletEntity entity)
        {
            string sql = @" UPDATE dbo.[CmsTemplet] SET
                       [Title]=@Title,[TempletContent]=@TempletContent,[ProductNum]=@ProductNum,[CMSType]=@CMSType,[CreateDate]=@CreateDate,[IsActive]=@IsActive
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@TempletContent", DbType.String, entity.TempletContent);
            db.AddInParameter(cmd, "@ProductNum", DbType.Int32, entity.ProductNum);
            db.AddInParameter(cmd, "@CMSType", DbType.Int32, entity.CMSType);
            db.AddInParameter(cmd, "@CreateDate", DbType.DateTime, entity.CreateDate);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCmsTempletByKey(int id)
        {
            string sql = @"delete from CmsTemplet where  and Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCmsTempletDisabled()
        {
            string sql = @"delete from  CmsTemplet  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCmsTempletByIds(string ids)
        {
            string sql = @"Delete from CmsTemplet  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCmsTempletByIds(string ids)
        {
            string sql = @"Update   CmsTemplet set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CmsTempletEntity GetCmsTemplet(int id)
        {
            string sql = @"SELECT  [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive]
							FROM
							dbo.[CmsTemplet] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            CmsTempletEntity entity = new CmsTempletEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.TempletContent = StringUtils.GetDbString(reader["TempletContent"]);
                    entity.ProductNum = StringUtils.GetDbInt(reader["ProductNum"]);
                    entity.CMSType = StringUtils.GetDbInt(reader["CMSType"]);
                    entity.CreateDate = StringUtils.GetDbDateTime(reader["CreateDate"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                }
            }
            return entity;
        }

        public CmsTempletEntity GetCmsTempletByTitle(string title)
        {
            string sql = @"SELECT  [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive]
							FROM
							dbo.[CmsTemplet] WITH(NOLOCK)	
							WHERE [Title]=@title";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Title", DbType.String, title);
            CmsTempletEntity entity = new CmsTempletEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.TempletContent = StringUtils.GetDbString(reader["TempletContent"]);
                    entity.ProductNum = StringUtils.GetDbInt(reader["ProductNum"]);
                    entity.CMSType = StringUtils.GetDbInt(reader["CMSType"]);
                    entity.CreateDate = StringUtils.GetDbDateTime(reader["CreateDate"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                }
            }
            return entity;
        }

        public IList<CmsTempletEntity> GetCmsTempletListBySearchCondition(string title, int cmstype, int isactive)
        {
            string sql = @"SELECT [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive] 
                         FROM
                         dbo.[CmsTemplet] WITH(NOLOCK)
                         WHERE [CMSType]=@cmstype AND [IsActive]=@isactive AND [Title] like @title";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CMSType", DbType.Int32, cmstype);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            db.AddInParameter(cmd, "@Title", DbType.String, "%" + title + "%");
            IList<CmsTempletEntity> _entitylist = new List<CmsTempletEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CmsTempletEntity _entity = new CmsTempletEntity();
                    _entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    _entity.Title = StringUtils.GetDbString(reader["Title"]);
                    _entity.TempletContent = StringUtils.GetDbString(reader["TempletContent"]);
                    _entity.ProductNum = StringUtils.GetDbInt(reader["ProductNum"]);
                    _entity.CMSType = StringUtils.GetDbInt(reader["CMStype"]);
                    _entity.CreateDate = StringUtils.GetDbDateTime(reader["CreateDate"]);
                    _entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    _entitylist.Add(_entity);
                }
            }
            return _entitylist;

        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CmsTempletEntity> GetCmsTempletList(int pagesize, int pageindex, ref int recordCount, string searchkey, int cmstype, int isactive, string sortkeyword)
        {
            string where = " where 1=1 ";
            string where2 = "";
            if (!string.IsNullOrEmpty(searchkey))
            {
                where += " and Title like @Title ";
            }
            if (cmstype > 0)
            {
                where += " and CMSType=@CMSType ";
            }
            if (isactive > 0)
            {
                where += " and IsActive=@IsActive ";
            }
            if (!string.IsNullOrEmpty(sortkeyword))
            {
                where2 = " order by " + sortkeyword + " desc";
            }
            string sql = @"SELECT   [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive] from dbo.[CmsTemplet] WITH(NOLOCK)	
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize" + where2;

            string sql2 = @"Select count(1) from dbo.[CmsTemplet] with (nolock) " + where;
            IList<CmsTempletEntity> entityList = new List<CmsTempletEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(searchkey))
            {
                db.AddInParameter(cmd, "@Title", DbType.String, "%" + searchkey + "%");
            }
            if (cmstype > 0)
            {
                db.AddInParameter(cmd, "@CMSType", DbType.Int32, cmstype);
            }
            if (isactive > 0)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, isactive);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CmsTempletEntity entity = new CmsTempletEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.TempletContent = StringUtils.GetDbString(reader["TempletContent"]);
                    entity.ProductNum = StringUtils.GetDbInt(reader["ProductNum"]);
                    entity.CMSType = StringUtils.GetDbInt(reader["CMSType"]);
                    entity.CreateDate = StringUtils.GetDbDateTime(reader["CreateDate"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(searchkey))
            {
                db.AddInParameter(cmd, "@Title", DbType.String, "%" + searchkey + "%");
            }
            if (cmstype > 0)
            {
                db.AddInParameter(cmd, "@CMSType", DbType.Int32, cmstype);
            }
            if (isactive > 0)
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
        public IList<CmsTempletEntity> GetCmsTempletAll()
        {

            string sql = @"SELECT    [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive] from dbo.[CmsTemplet] WITH(NOLOCK)	";
            IList<CmsTempletEntity> entityList = new List<CmsTempletEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CmsTempletEntity entity = new CmsTempletEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.TempletContent = StringUtils.GetDbString(reader["TempletContent"]);
                    entity.ProductNum = StringUtils.GetDbInt(reader["ProductNum"]);
                    entity.CMSType = StringUtils.GetDbInt(reader["CMSType"]);
                    entity.CreateDate = StringUtils.GetDbDateTime(reader["CreateDate"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<CmsTempletEntity> GetCmsTempletListByType(int cmstype)
        {

            string sql = @"SELECT    [Id],[Title],[TempletContent],[ProductNum],[CMSType],[CreateDate],[IsActive] from dbo.[CmsTemplet] WITH(NOLOCK) WHERE [CMSType]=@cmstype AND IsActive=1";
            IList<CmsTempletEntity> entityList = new List<CmsTempletEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CMSType", DbType.Int32, cmstype);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CmsTempletEntity entity = new CmsTempletEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.TempletContent = StringUtils.GetDbString(reader["TempletContent"]);
                    entity.ProductNum = StringUtils.GetDbInt(reader["ProductNum"]);
                    entity.CMSType = StringUtils.GetDbInt(reader["CMSType"]);
                    entity.CreateDate = StringUtils.GetDbDateTime(reader["CreateDate"]);
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
        public int ExistNum(CmsTempletEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CmsTemplet] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "Title=@Title";
            }
            else
            {
                where = where + "Id<>@Id and Title=@Title";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }


        //public void CmsTempletSort()
        //{
        //    string sql = "select * from dbo.CmsTemplet order by IsActive desc,CreateDate desc";
        //    DbCommand cmd = db.GetSqlStringCommand(sql);
        //    db.ExecuteReader(cmd);
        //}





        #endregion
        #endregion
    }
}
