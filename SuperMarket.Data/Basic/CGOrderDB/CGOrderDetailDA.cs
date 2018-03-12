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
功能描述：CGOrderDetail表的数据访问类。
创建时间：2016/12/31 9:40:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
    /// <summary>
    /// CGOrderDetailEntity的数据访问操作
    /// </summary>
    public partial class CGOrderDetailDA : BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderDetailDA Instance
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
            internal static readonly CGOrderDetailDA instance = new CGOrderDetailDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表CGOrderDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGOrderDetail">待插入的实体对象</param>
        public int AddCGOrderDetail(CGOrderDetailEntity entity)
        {
            string sql = @"insert into CGOrderDetail( [CGOrderCode],[ProductId],[PicUrl],[PicSuffix],[Name],[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark])VALUES
			            ( @CGOrderCode,@ProductId,@PicUrl,@PicSuffix,@Name,@Title,@Spec1,@Spec2,@Spec3,@Num,@Remark);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            db.AddInParameter(cmd, "@PicSuffix", DbType.String, entity.PicSuffix);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            db.AddInParameter(cmd, "@Spec3", DbType.String, entity.Spec3);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGOrderDetail">待更新的实体对象</param>
        public int UpdateCGOrderDetail(CGOrderDetailEntity entity)
        {
            string sql = @" UPDATE dbo.[CGOrderDetail] SET
                       [CGOrderCode]=@CGOrderCode,[ProductId]=@ProductId,[PicUrl]=@PicUrl,[PicSuffix]=@PicSuffix,[Name]=@Name,[Title]=@Title,[Spec1]=@Spec1,[Spec2]=@Spec2,[Spec3]=@Spec3,[Num]=@Num,[Remark]=@Remark
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            db.AddInParameter(cmd, "@PicSuffix", DbType.String, entity.PicSuffix);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            db.AddInParameter(cmd, "@Spec3", DbType.String, entity.Spec3);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderDetailByKey(int id)
        {
            string sql = @"delete from CGOrderDetail where  and Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderDetailDisabled()
        {
            string sql = @"delete from  CGOrderDetail  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderDetailByIds(string ids)
        {
            string sql = @"Delete from CGOrderDetail  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderDetailByIds(string ids)
        {
            string sql = @"Update   CGOrderDetail set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CGOrderDetailEntity GetCGOrderDetail(int id)
        {
            string sql = @"SELECT  [Id],[CGOrderCode],[ProductId],[PicUrl],[PicSuffix],[Name],[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark]
							FROM
							dbo.[CGOrderDetail] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            CGOrderDetailEntity entity = new CGOrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGOrderDetailEntity> GetCGOrderDetailListByOrderCode(long orderCode)
        {
            string sql = @"SELECT  [Id],[CGOrderCode],[ProductId],[Price],[PicUrl],[PicSuffix],[Name],[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark],Unit
							FROM
							dbo.[CGOrderDetail] WITH(NOLOCK)	
							WHERE [CGOrderCode]=@CGOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            IList<CGOrderDetailEntity> entityList = new List<CGOrderDetailEntity>();

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, orderCode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderDetailEntity entity = new CGOrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);  
                    entity.Unit = StringUtils.GetDbInt(reader["Unit"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }


        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGOrderDetailEntity> GetCGOrderDetailByCode(long orderCode)
        {
            string sql = @"SELECT  [Id],[CGOrderCode],[ProductId],[ProductCode],[PicUrl],[PicSuffix],[Name],Price,[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark],CGPrice,CGTotalPrice
							FROM
							dbo.[CGOrderDetail] WITH(NOLOCK)	
							WHERE [CGOrderCode]=@CGOrderCode ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            IList<CGOrderDetailEntity> entityList = new List<CGOrderDetailEntity>();
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, orderCode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderDetailEntity entity = new CGOrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductCode = StringUtils.GetDbString(reader["ProductCode"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
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
        public IList<CGOrderDetailEntity> GetCGOrderDetailList(int pagesize, int pageindex, ref int recordCount, string productName, long orderCode)
        {
            string where = "WHERE 1=1";

            if (!string.IsNullOrEmpty(productName))
            {
                where += " And Name like @Name";
            }

            if (orderCode > 0)
            {
                where += " And CGOrderCode like @CGOrderCode";
            }

            string sql = @"SELECT   [Id],[CGOrderCode],[ProductId],[PicUrl],[PicSuffix],[Name],[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGOrderCode],[ProductId],[PicUrl],[PicSuffix],[Name],[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark] from dbo.[CGOrderDetail] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrderDetail] with (nolock) " + where;
            IList<CGOrderDetailEntity> entityList = new List<CGOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@Name",DbType.String,productName);
            }

            if (orderCode > 0)
            {
                db.AddInParameter(cmd, "CGOrderCode", DbType.Int64, orderCode);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderDetailEntity entity = new CGOrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(productName))
            {
                db.AddInParameter(cmd, "@Name", DbType.String, productName);
            }

            if (orderCode > 0)
            {
                db.AddInParameter(cmd, "CGOrderCode", DbType.Int64, orderCode);
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
        public IList<CGOrderDetailEntity> GetCGOrderDetailAll()
        {

            string sql = @"SELECT    [Id],[CGOrderCode],[ProductId],[PicUrl],[PicSuffix],[Name],[Title],[Spec1],[Spec2],[Spec3],[Num],[Remark] from dbo.[CGOrderDetail] WITH(NOLOCK)	";
            IList<CGOrderDetailEntity> entityList = new List<CGOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderDetailEntity entity = new CGOrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
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
        public int ExistNum(CGOrderDetailEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderDetail] WITH(NOLOCK) ";
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
