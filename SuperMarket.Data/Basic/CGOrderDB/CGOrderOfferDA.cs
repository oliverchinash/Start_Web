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
功能描述：CGOrderOffer表的数据访问类。
创建时间：2016/12/31 9:40:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
    /// <summary>
    /// CGOrderOfferEntity的数据访问操作
    /// </summary>
    public partial class CGOrderOfferDA : BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderOfferDA Instance
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
            internal static readonly CGOrderOfferDA instance = new CGOrderOfferDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表CGOrderOffer，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGOrderOffer">待插入的实体对象</param>
        public int AddCGOrderOffer(CGOrderOfferEntity entity)
        {
            string sql = @"insert into CGOrderOffer( [CGOrderCode],[CGMemId],[QuotedPrice],[QuotedTime],[Status],[QuotedTransFee],[QuotedTotalPrice])VALUES
			            ( @CGOrderCode,@CGMemId,@QuotedPrice,@QuotedTime,@Status,@QuotedTransFee,@QuotedTotalPrice);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@QuotedTransFee", DbType.Decimal, entity.QuotedTransFee);
            db.AddInParameter(cmd, "@QuotedPrice", DbType.Decimal, entity.QuotedPrice);
            db.AddInParameter(cmd, "@QuotedTotalPrice", DbType.Decimal, entity.QuotedTotalPrice);
            db.AddInParameter(cmd, "@QuotedTime", DbType.DateTime, entity.QuotedTime);
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
        /// <param name="cGOrderOffer">待更新的实体对象</param>
        public int UpdateCGOrderOffer(CGOrderOfferEntity entity)
        {
            string sql = @" UPDATE dbo.[CGOrderOffer] SET
                       [CGOrderCode]=@CGOrderCode,[CGMemId]=@CGMemId,[QuotedPrice]=@QuotedPrice,[QuotedTime]=@QuotedTime,[Status]=@Status,[QuotedTransFee]=@QuotedTransFee,[QuotedTotalPrice]=@QuotedTotalPrice
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@QuotedTransFee", DbType.Decimal, entity.QuotedTransFee);
            db.AddInParameter(cmd, "@QuotedPrice", DbType.Decimal, entity.QuotedPrice);
            db.AddInParameter(cmd, "@QuotedTotalPrice", DbType.Decimal, entity.QuotedTotalPrice);
            db.AddInParameter(cmd, "@QuotedTime", DbType.DateTime, entity.QuotedTime);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderOfferByKey(int id)
        {
            string sql = @"delete from CGOrderOffer where  and Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderOfferDisabled()
        {
            string sql = @"delete from  CGOrderOffer  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderOfferByIds(string ids)
        {
            string sql = @"Delete from CGOrderOffer  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderOfferByIds(string ids)
        {
            string sql = @"Update   CGOrderOffer set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CGOrderOfferEntity GetCGOrderOfferByMemidAndCode(int memid, long code)
        {
            string sql = @"SELECT  top (1) [Id],[CGOrderCode],[CGMemId],[QuotedPrice],[QuotedTime],[Status],[QuotedTransFee],[QuotedTotalPrice],CreateTime
							FROM
							dbo.[CGOrderOffer] WITH(NOLOCK)	
							WHERE [CGMemId]=@CGMemId and [CGOrderCode]=@CGOrderCode Order By QuotedTime desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, code);

            CGOrderOfferEntity entity = new CGOrderOfferEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.QuotedTransFee = StringUtils.GetDbDecimal(reader["QuotedTransFee"]);
                    entity.QuotedPrice = StringUtils.GetDbDecimal(reader["QuotedPrice"]);
                    entity.QuotedTotalPrice = StringUtils.GetDbDecimal(reader["QuotedTotalPrice"]);
                    entity.QuotedTime = StringUtils.GetDbDateTime(reader["QuotedTime"]); 
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }
            return entity;
        }


        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CGOrderOfferEntity GetCGOrderOffer(int id)
        {
            string sql = @"SELECT  [Id],[CGOrderCode],[CGMemId],[QuotedPrice],[QuotedTime],[Status]
							FROM
							dbo.[CGOrderOffer] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            CGOrderOfferEntity entity = new CGOrderOfferEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.QuotedPrice = StringUtils.GetDbDecimal(reader["QuotedPrice"]);
                    entity.QuotedTime = StringUtils.GetDbDateTime(reader["QuotedTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGOrderOfferEntity> GetCGOrderOfferList(int pagesize, int pageindex, ref int recordCount, int status, int memid)
        {
            string where = " WHERE  1=1 ";

            if (status > 0)
            {
                where += " And Status=@Status";
            }

            if (memid > 0)
            {
                where += " And CGMemId=@CGMemId";

            }

            string sql = @"SELECT   [Id],[CGOrderCode],[CGMemId],[QuotedPrice],[QuotedTime],[Status]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGOrderCode],[CGMemId],[QuotedPrice],[QuotedTime],[Status] from dbo.[CGOrderOffer] WITH(NOLOCK)	
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrderOffer] with (nolock) " + where;
            IList<CGOrderOfferEntity> entityList = new List<CGOrderOfferEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            if (memid > 0)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);

            }


            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderOfferEntity entity = new CGOrderOfferEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.QuotedPrice = StringUtils.GetDbDecimal(reader["QuotedPrice"]);
                    entity.QuotedTime = StringUtils.GetDbDateTime(reader["QuotedTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            if (memid > 0)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);

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
        public IList<CGOrderOfferEntity> GetCGOrderOfferAll()
        {

            string sql = @"SELECT    [Id],[CGOrderCode],[CGMemId],[QuotedPrice],[QuotedTime],[Status] from dbo.[CGOrderOffer] WITH(NOLOCK)	";
            IList<CGOrderOfferEntity> entityList = new List<CGOrderOfferEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderOfferEntity entity = new CGOrderOfferEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.QuotedPrice = StringUtils.GetDbDecimal(reader["QuotedPrice"]);
                    entity.QuotedTime = StringUtils.GetDbDateTime(reader["QuotedTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
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
        public int ExistNum(CGOrderOfferEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderOffer] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where += " CGOrderCode=@CGOrderCode And CGMemId=@CGMemId";
            }
            else
            {
                where += " Id<>@Id and CGOrderCode=@CGOrderCode And CGMemId=@CGMemId";

            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
                db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
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
