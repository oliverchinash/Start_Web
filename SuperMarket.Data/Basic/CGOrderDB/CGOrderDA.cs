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
功能描述：CGOrder表的数据访问类。
创建时间：2016/12/31 9:40:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
    /// <summary>
    /// CGOrderEntity的数据访问操作
    /// </summary>
    public partial class CGOrderDA : BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderDA Instance
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
            internal static readonly CGOrderDA instance = new CGOrderDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表CGOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGOrder">待插入的实体对象</param>
        public int AddCGOrder(CGOrderEntity entity)
        {
            string sql = @"insert into CGOrder( [Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark])VALUES
			            ( @Code,@Title,@SourceCode,@CreateTime, @TransFee,@Status, @SendTime,@Remark);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, entity.Code);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@SourceCode", DbType.Int64, entity.SourceCode);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime); 
            db.AddInParameter(cmd, "@TransFee", DbType.Decimal, entity.TransFee);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status); 
            db.AddInParameter(cmd, "@SendTime", DbType.DateTime, entity.SendTime);
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
        /// <param name="cGOrder">待更新的实体对象</param>
        public int UpdateCGOrder(CGOrderEntity entity)
        {
            string sql = @" UPDATE dbo.[CGOrder] SET
                       [Code]=@Code,[Title]=@Title,[SourceCode]=@SourceCode,[CreateTime]=@CreateTime, [TransFee]=@TransFee,[Status]=@Status, [SendTime]=@SendTime,[Remark]=@Remark
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@Code", DbType.Int64, entity.Code);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@SourceCode", DbType.Int64, entity.SourceCode);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime); 
            db.AddInParameter(cmd, "@TransFee", DbType.Decimal, entity.TransFee);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status); 
            db.AddInParameter(cmd, "@SendTime", DbType.DateTime, entity.SendTime);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            return db.ExecuteNonQuery(cmd);
        }

        public int CGOrderRecived(long b2bordercode)
        {
            string sql = @" 
update dbo.[CGOrder] set Status=@CGOrderStatusNew  WHERE [SourceCode]=@Code  
update a set Status=@CGOrderTakeNew from  dbo.[CGOrderTake] a inner join  dbo.[CGOrder] b on a.CGOrderCode=b.Code   WHERE b.[SourceCode]=@Code  

";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, b2bordercode);
            db.AddInParameter(cmd, "@CGOrderStatusNew", DbType.Int32, (int)CGOrderStatus.Finished);
            db.AddInParameter(cmd, "@CGOrderTakeNew", DbType.Int32, (int)CGOrderTake.HasRecived);
            return db.ExecuteNonQuery(cmd);
        }
        public int CGOrderCancel(long b2bordercode)
        {
            string sql = @" 
begin tran 
SELECT Code,Status INTO #temp FROM dbo.CGOrder WHERE SourceCode=@B2BCode 
UPDATE a SET IsClose=1  FROM dbo.CGOrderSend a INNER JOIN #temp b
ON a.CGOrderCode=b.Code 
UPDATE a SET Status=@TakeOrderCancel  FROM dbo.CGOrderTake a INNER JOIN #temp b
ON a.CGOrderCode=b.Code 
UPDATE a SET BeforeCancelStatus=a.STATUS,STATUS=@CGOrderCancel FROM dbo.CGOrder a INNER JOIN #temp b
ON a.Code=b.Code
commit tran
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@B2BCode", DbType.Int64, b2bordercode);
            db.AddInParameter(cmd, "@CGOrderCancel", DbType.Int32, (int)CGOrderStatus.Cancel);
            db.AddInParameter(cmd, "@TakeOrderCancel", DbType.Int32, (int)CGOrderTake.Cancel);
            return db.ExecuteNonQuery(cmd);
        }

        public int CGOrderCancelXuQiu(string b2bordercodestrs)
        {
            string sql = @"  
  Update a  set Status=@NewStatus  from   dbo.CGOrder a inner  join  dbo.fun_splitstr(@CGOrderCodes, '|') b
   ON a.code=b.ID    where Status=@OldStatus 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCodes", DbType.String, b2bordercodestrs);
            db.AddInParameter(cmd, "@NewStatus", DbType.Int32, (int)CGOrderStatus.Cancel);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int32, (int)CGOrderStatus.WaitRecived);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CGOrderEntity GetCGOrderByOrderCode(long orderCode)
        {
            string sql = @"SELECT  [Id],[Code],ExpressCom,[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark]
							FROM
							dbo.[CGOrder] WITH(NOLOCK)	
							WHERE [Code]=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, orderCode);
            CGOrderEntity entity = new CGOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);  
                    entity.ExpressCom = StringUtils.GetDbInt(reader["ExpressCom"]); 
                }
            }
            return entity;
        }

        public VWCGOrderEntity GetVWCGOrderByOrderCode(long _orderCode)
        {
            string sql = @"SELECT  a.[Id],a.[Code],a.ExpressCom,a.[Title],a.[SourceCode],a.[CreateTime], [TransFee],a.[Status], a.[SendTime],a.[Remark],
b.[ReceiptName]
      ,b.[ReceiptProvince]
      ,b.[ReceiptCity]
      ,b.[ReceiptTown]
      ,b.[ReceiptAddress]
      ,b.[ReceiptPhone]
							FROM
							dbo.[CGOrder] a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] b  WITH(NOLOCK)
on a.Code=b.CGOrderCode
							WHERE [Code]=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, _orderCode);
            VWCGOrderEntity entity = new VWCGOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.ExpressCom = StringUtils.GetDbInt(reader["ExpressCom"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]);
                }
            }
            return entity;
        }
        public long B2BOrderCanRecived(long cgorderCode)
        {
            string sql = @"
                        DECLARE @B2BCode BIGINT
                        SELECT @B2BCode=SourceCode FROM  [CGOrder] WHERE CODE=@CGOrderCode
                        IF EXISTS(SELECT 1 FROM [CGOrder] WHERE SourceCode=@B2BCode AND STATUS<>@OrderFinishedStatus)
                        BEGIN
                        SELECT 0
                        END
                        ELSE
                        begin
                        SELECT @B2BCode
                        end
                        ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgorderCode); 
            db.AddInParameter(cmd, "@OrderFinishedStatus", DbType.Int64, CGOrderStatus.Finished); 
            return StringUtils.GetDbLong(db.ExecuteScalar(cmd));
          

        }

        public int GetCGOrderNumByB2BCode(long _b2borderCode)
        {
            string sql = @"Select count(1) from dbo.[CGOrder] with (nolock) where SourceCode=@SourceCode";
            IList<CGOrderEntity> entityList = new List<CGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SourceCode", DbType.Int64, _b2borderCode);
            int recordCount;
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
            return recordCount;
        }
        public IList<CGOrderEntity> GetCGOrderBySourceCode(long sourceCode)
        {
            string sql = @"SELECT  [Id],[Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark],OrderStyle
							FROM
							dbo.[CGOrder] WITH(NOLOCK)	
							WHERE [SourceCode]=@SourceCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceCode);

            IList<CGOrderEntity> entityList = new List<CGOrderEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderEntity entity = new CGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.OrderStyle = StringUtils.GetDbInt(reader["OrderStyle"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public IList<VWCGOrderEntity> GetVWCGOrderBySourceCode(long sourceCode)
        {
            string sql = @"SELECT  a.[Id],a.[Code],a.ExpressCom,a.[Title],a.[SourceCode],a.[CreateTime], [TransFee],a.[Status], a.[SendTime],a.[Remark],
b.[ReceiptName]
      ,b.[ReceiptProvince]
      ,b.[ReceiptCity]
      ,b.[ReceiptTown]
      ,b.[ReceiptAddress]
      ,b.[ReceiptPhone]
							FROM
							dbo.[CGOrder] a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] b  WITH(NOLOCK)
on a.Code=b.CGOrderCode
							WHERE [SourceCode]=@SourceCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceCode);

            IList<VWCGOrderEntity> entityList = new List<VWCGOrderEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderEntity entity = new VWCGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderByKey(int id)
        {
            string sql = @"delete from CGOrder where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderDisabled()
        {
            string sql = @"delete from  CGOrder  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderByIds(string ids)
        {
            string sql = @"Delete from CGOrder  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderByIds(string ids)
        {
            string sql = @"Update   CGOrder set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CGOrderEntity GetCGOrder(int id)
        {
            string sql = @"SELECT  [Id],[Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark]
							FROM
							dbo.[CGOrder] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            CGOrderEntity entity = new CGOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                }
            }
            return entity;
        }
        public VWCGOrderEntity GetVWCGOrder(int id)
        {
            string sql = @"SELECT   a.[Id],a.[Code],a.[Title],a.[SourceCode],a.[CreateTime],a.[TransFee],a.[Status],  a.[SendTime],a.[Remark],
b.[ReceiptName],b.[ReceiptProvince],b.[ReceiptCity],b.[ReceiptTown],b.[ReceiptAddress]
      ,b.[ReceiptPhone] 	FROM
							dbo.[CGOrder] a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] b  WITH(NOLOCK)
on a.Code=b.CGOrderCode
							WHERE a.[Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            VWCGOrderEntity entity = new VWCGOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGOrderEntity> GetCGOrderList(int pagesize, int pageindex, ref int recordCount, int status,int orderstyle, long sourceCode, string keyword)
        {

            string where = " where 1=1";

            if (!string.IsNullOrEmpty(keyword))
            {
                where += " And Code like @keyword";
            }
            if (orderstyle != -1)
            {
                where += " And OrderStyle=@OrderStyle";
            }
            if (status != -1)
            {
                where += " And Status=@Status";
            }

            if (sourceCode>0)
            {
                where += " And SourceCode = @SourceCode";
            }

            string sql = @"SELECT   [Id],[Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status],  [SendTime],[Remark] from dbo.[CGOrder] WITH(NOLOCK)	
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrder] with (nolock) " + where;
            IList<CGOrderEntity> entityList = new List<CGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (orderstyle != -1)
            { 
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            if (sourceCode>0)
            {
                db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceCode);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderEntity entity = new CGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (orderstyle != -1)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle);
           }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            if (sourceCode>0)
            {
                db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceCode);
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

        public IList<VWCGOrderEntity> GetVWCGOrderList(int pagesize, int pageindex, ref int recordCount,int cgmemid, int status, int orderstyle, long sourceCode, string keyword )
        {

            string where = " where 1=1";
            
          
            if (!string.IsNullOrEmpty(keyword))
            {
                where += " And Code like @keyword";
            }
            if (cgmemid != -1)
            {
                where += " And CGMemId=@CGMemId";
            }
            if (orderstyle != -1)
            {
                where += " And OrderStyle=@OrderStyle";
            }
            if (status != -1)
            {
                where += " And Status=@Status";
            } 
            if (sourceCode > 0)
            {
                where += " And SourceCode = @SourceCode";
            }

            string sql = @"SELECT   [Id],[Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark],[ReceiptName]
      , [ReceiptProvince]
      , [ReceiptCity]
      , [ReceiptTown]
      , [ReceiptAddress]
      , [ReceiptPhone]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],a.[Code],a.[Title],a.[SourceCode],a.[CreateTime],a.[TransFee],a.[Status],  a.[SendTime],a.[Remark],
b.[ReceiptName]
      ,b.[ReceiptProvince]
      ,b.[ReceiptCity]
      ,b.[ReceiptTown]
      ,b.[ReceiptAddress]
      ,b.[ReceiptPhone]
							FROM
							dbo.[CGOrder] a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] b  WITH(NOLOCK)
on a.Code=b.CGOrderCode

						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrder] a with(nolock) inner join dbo.[CGOrderAddress] b  WITH(NOLOCK)
on a.Code=b.CGOrderCode " + where;
            IList<VWCGOrderEntity> entityList = new List<VWCGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (cgmemid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);  
            }
            if (orderstyle != -1)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle); 
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            if (sourceCode > 0)
            {
                db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceCode);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderEntity entity = new VWCGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }
            if (cgmemid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            if (orderstyle != -1)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            if (sourceCode > 0)
            {
                db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceCode);
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

        public CGOrderEntity GetCGOrderByCode(long cgcode, int memid)
        {
             CGOrderEntity entity = new  CGOrderEntity();
            string where = " where Code=@Code ";
            if (memid != -1)
            {
                where += "and CGMemId=@CGMemId ";
            }
            string sql = @" SELECT  a.[Id],a.[Code],CGMemId,a.[Title],a.[SourceCode],a.[CreateTime],a.[TransFee],a.[Status],  a.[SendTime],a.[Remark] 
                      
							FROM
							dbo.[CGOrder] a  WITH(NOLOCK)   " + where;

            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, cgcode);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }


            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]); 

                }
            }

            return entity;
        }

        public VWCGOrderEntity GetVWCGOrderByCode(long cgcode, int memid)
        {
            VWCGOrderEntity entity = new VWCGOrderEntity();
            string where = " where Code=@Code ";
            if (memid != -1)
            {
                where += "and CGMemId=@CGMemId ";
            }
            string sql = @" SELECT  a.[Id],a.[Code],a.[Title],a.[SourceCode],a.[CreateTime],a.[TransFee],a.[Status],  a.[SendTime],a.[Remark],
                          b.[ReceiptName]
                          ,b.[ReceiptProvince]
                          ,b.[ReceiptCity]
                          ,b.[ReceiptTown]
                          ,b.[ReceiptAddress]
                          ,b.[ReceiptPhone]
							FROM
							dbo.[CGOrder] a  WITH(NOLOCK)	inner join dbo.[CGOrderAddress] b  WITH(NOLOCK)
on a.Code=b.CGOrderCode  "+where;
              
            DbCommand cmd = db.GetSqlStringCommand(sql);
          
                db.AddInParameter(cmd, "@Code", DbType.Int64, cgcode);
            if (memid != -1)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
           

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.ReceiptName = StringUtils.GetDbString(reader["ReceiptName"]);
                    entity.ReceiptProvince = StringUtils.GetDbInt(reader["ReceiptProvince"]);
                    entity.ReceiptCity = StringUtils.GetDbInt(reader["ReceiptCity"]);
                    entity.ReceiptTown = StringUtils.GetDbInt(reader["ReceiptTown"]);
                    entity.ReceiptAddress = StringUtils.GetDbString(reader["ReceiptAddress"]);
                    entity.ReceiptPhone = StringUtils.GetDbString(reader["ReceiptPhone"]);
     
                }
            }
          
            return entity;
        }
        public IList<ReportCGOrderEntity> GetReportsGH(int year, int mon, int day)
        {
            string where = " where 1=1 ";
            string strdatebegin = "";
            string strdateend = "";
            if (year>0)
            {
                strdatebegin += year; 
                if(mon>0)
                {
                    strdatebegin += "-" + mon;
                    if(day>0)
                    {
                        strdateend  = strdatebegin+"-" + (day+1);
                        strdatebegin += "-" + day;
                    }
                    else
                    {
                        strdateend += year+"-"+(mon + 1) + "-01";
                        strdatebegin += "-1";
                    } 
                }
                else
                {
                    strdatebegin+= "-1-1";
                    strdateend += (year + 1)+"-01-01";
                }
            } 
           if(!string.IsNullOrEmpty(strdatebegin)&& !string.IsNullOrEmpty(strdateend))
            {
                where += " and  a.CreateTime >@CreateTimeBegin  and a.CreateTime<@CreateTimeEnd";
            }

            string sql = @"SELECT a.CreateTime,a.SourceCode as B2BCode,a.code as CGCode,b.CGMemId ,bs.Name as ProductName,bs.Num,BS.CGPrice,BS.Price as B2BPrice,A.Status  FROM dbo.CGOrder a WITH(NOLOCK) INNER JOIN dbo.CGOrderTake b  WITH(NOLOCK) ON a.Code=b.CGOrderCode
INNER JOIN dbo.CGOrderDetail bs WITH(NOLOCK) ON b.CGOrderCode=bs.CGOrderCode " + where;
             
            IList<ReportCGOrderEntity> entityList = new List<ReportCGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(strdatebegin) && !string.IsNullOrEmpty(strdateend))
            { 
                db.AddInParameter(cmd, "@CreateTimeBegin", DbType.String, strdatebegin);
                db.AddInParameter(cmd, "@CreateTimeEnd", DbType.String, strdateend);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReportCGOrderEntity entity = new ReportCGOrderEntity();
                    entity.B2BCode = StringUtils.GetDbLong(reader["B2BCode"]);
                    entity.CGCode = StringUtils.GetDbLong(reader["CGCode"]);
                    entity.B2BPrice = StringUtils.GetDbDecimal(reader["B2BPrice"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CgMemId = StringUtils.GetDbInt(reader["CgMemId"]);
                    entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    
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
        public IList<CGOrderEntity> GetCGOrderAll()
        {

            string sql = @"SELECT    [Id],[Code],[Title],[SourceCode],[CreateTime], [TransFee],[Status], [SendTime],[Remark] from dbo.[CGOrder] WITH(NOLOCK)	";
            IList<CGOrderEntity> entityList = new List<CGOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderEntity entity = new CGOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.SourceCode = StringUtils.GetDbLong(reader["SourceCode"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); 
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.SendTime = StringUtils.GetDbDateTime(reader["SendTime"]);
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
        public int ExistNum(CGOrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrder] WITH(NOLOCK) ";
            string where = "where   (Code=@Code) "; 
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
             db.AddInParameter(cmd, "@Code", DbType.Int32, entity.Code); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }


        /// <summary>
        /// 拆分订单
        /// </summary>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public int SplitCGOrder(long sourceOrderCode, string orderDetailIdArr)
        {
            string sql = "EXEC Proc_SplitCGOrder @SourceCode,@OrderDetailsIdStr";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SourceCode", DbType.Int64, sourceOrderCode);
            db.AddInParameter(cmd, "@OrderDetailsIdStr", DbType.String, orderDetailIdArr);
            object result = db.ExecuteScalar(cmd);
            if (result == null || result == DBNull.Value) return 0;
            return (int)result;
        }



        /// <summary>
        /// 更新采购单状态
        /// </summary>
        /// <returns></returns>
        public int UpdateCGOrderStatus(long CGOrderCode,int memid,int status)
        {
            string where = " where Code=@Code ";
            if(memid>0)
            {
                where += " and CGMemId=@CGMemId";
            }
            string sql = " Update dbo.CGOrder set Status=@Status  "+ where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "Code", DbType.Int64, CGOrderCode);
            db.AddInParameter(cmd, "Status", DbType.Int32, status);
            if (memid > 0)
            {
            db.AddInParameter(cmd, "CGMemId", DbType.Int32, memid); 
            }
            return db.ExecuteNonQuery(cmd);
        }

        #endregion
        #endregion
    }
}
