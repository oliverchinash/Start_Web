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
功能描述：XTCode表的数据访问类。
创建时间：2016/7/30 10:41:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
    /// <summary>
    /// XTCodeEntity的数据访问操作
    /// </summary>
    public partial class XTCodeDA : BaseSuperMarketDB
    {
        #region 实例化
        public static XTCodeDA Instance
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
            internal static readonly XTCodeDA instance = new XTCodeDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表XTCode，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="xTCode">待插入的实体对象</param>
        public int AddXTCode(XTCodeEntity entity)
        {
            string sql = @"insert into XTCode( [Code],[Name],[HasPre],[NeedDate],[NumLen],[FillChar])VALUES
			            ( @Code,@Name,@HasPre,@NeedDate,@NumLen,@FillChar);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@HasPre", DbType.Int32, entity.HasPre);
            db.AddInParameter(cmd, "@NeedDate", DbType.Int32, entity.NeedDate);
            db.AddInParameter(cmd, "@NumLen", DbType.Int32, entity.NumLen);
            db.AddInParameter(cmd, "@FillChar", DbType.String, entity.FillChar);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="xTCode">待更新的实体对象</param>
        public int UpdateXTCode(XTCodeEntity entity)
        {
            string sql = @" UPDATE dbo.[XTCode] SET
                       [Code]=@Code,[Name]=@Name,[HasPre]=@HasPre,[NeedDate]=@NeedDate,[NumLen]=@NumLen,[FillChar]=@FillChar
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@HasPre", DbType.Int32, entity.HasPre);
            db.AddInParameter(cmd, "@NeedDate", DbType.Int32, entity.NeedDate);
            db.AddInParameter(cmd, "@NumLen", DbType.Int32, entity.NumLen);
            db.AddInParameter(cmd, "@FillChar", DbType.String, entity.FillChar);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteXTCodeByKey(int id)
        {
            string sql = @"delete from XTCode where  and Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteXTCodeDisabled()
        {
            string sql = @"delete from  XTCode  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteXTCodeByIds(string ids)
        {
            string sql = @"Delete from XTCode  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableXTCodeByIds(string ids)
        {
            string sql = @"Update   XTCode set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public XTCodeEntity GetXTCode(int id)
        {
            string sql = @"SELECT  [Id],[Code],[Name],[HasPre],[NeedDate],[NumLen],[FillChar]
							FROM
							dbo.[XTCode] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            XTCodeEntity entity = new XTCodeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.HasPre = StringUtils.GetDbInt(reader["HasPre"]);
                    entity.NeedDate = StringUtils.GetDbInt(reader["NeedDate"]);
                    entity.NumLen = StringUtils.GetDbInt(reader["NumLen"]);
                    entity.FillChar = StringUtils.GetDbString(reader["FillChar"]);
                }
            }
            return entity;
        }
        public string GetCodeFromProc(string codetype)
        {
            string sql = @"EXEC sp_XT_GetSerialCode  @CodeType  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CodeType", DbType.String, codetype);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return "";
            return identity.ToString();
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<XTCodeEntity> GetXTCodeList(int pagesize, int pageindex, ref int recordCount)
        {
            string sql = @"SELECT   [Id],[Code],[Name],[HasPre],[NeedDate],[NumLen],[FillChar]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[HasPre],[NeedDate],[NumLen],[FillChar] from dbo.[XTCode] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[XTCode] with (nolock) ";
            IList<XTCodeEntity> entityList = new List<XTCodeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    XTCodeEntity entity = new XTCodeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.HasPre = StringUtils.GetDbInt(reader["HasPre"]);
                    entity.NeedDate = StringUtils.GetDbInt(reader["NeedDate"]);
                    entity.NumLen = StringUtils.GetDbInt(reader["NumLen"]);
                    entity.FillChar = StringUtils.GetDbString(reader["FillChar"]);
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
        public IList<XTCodeEntity> GetXTCodeAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[HasPre],[NeedDate],[NumLen],[FillChar] from dbo.[XTCode] WITH(NOLOCK)	";
            IList<XTCodeEntity> entityList = new List<XTCodeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    XTCodeEntity entity = new XTCodeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.HasPre = StringUtils.GetDbInt(reader["HasPre"]);
                    entity.NeedDate = StringUtils.GetDbInt(reader["NeedDate"]);
                    entity.NumLen = StringUtils.GetDbInt(reader["NumLen"]);
                    entity.FillChar = StringUtils.GetDbString(reader["FillChar"]);
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
        public int ExistNum(XTCodeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[XTCode] WITH(NOLOCK) ";
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
