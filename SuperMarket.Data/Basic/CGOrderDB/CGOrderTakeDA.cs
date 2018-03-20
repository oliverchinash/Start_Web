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
功能描述：CGOrderTake表的数据访问类。
创建时间：2016/12/31 9:40:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
    /// <summary>
    /// CGOrderTakeEntity的数据访问操作
    /// </summary>
    public partial class CGOrderTakeDA : BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderTakeDA Instance
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
            internal static readonly CGOrderTakeDA instance = new CGOrderTakeDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表CGOrderTake，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGOrderTake">待插入的实体对象</param>
        public int AddCGOrderTake(CGOrderTakeEntity entity)
        {
            string sql = @"insert into CGOrderTake(WLCode,[WLComName],[DeliveryManName],[DeliveryManPhone],[CGOrderCode],[CGMemId],[TakeTime],[TakePrice],[PrintNoteTime],[DeliveryTime],[VoucherTime],[VoucherUrl],[PayTime],[PayMemId])VALUES
			            (@WLCode,@WLComName,@DeliveryManName,@DeliveryManPhone,@CGOrderCode,@CGMemId,@TakeTime,@TakePrice,@PrintNoteTime,@DeliveryTime,@VoucherTime,@VoucherUrl,@PayTime,@PayMemId);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@TakeTime", DbType.DateTime, entity.TakeTime = DateTime.Now);
            db.AddInParameter(cmd, "@TakePrice", DbType.Decimal, entity.TakePrice);
            db.AddInParameter(cmd, "@PrintNoteTime", DbType.DateTime, entity.PrintNoteTime = DateTime.Now);
            db.AddInParameter(cmd, "@DeliveryTime", DbType.DateTime, entity.DeliveryTime = DateTime.Now);
            db.AddInParameter(cmd, "@VoucherTime", DbType.DateTime, entity.VoucherTime = DateTime.Now);
            db.AddInParameter(cmd, "@VoucherUrl", DbType.String, entity.VoucherUrl);
            db.AddInParameter(cmd, "@PayTime", DbType.DateTime, entity.PayTime = DateTime.Now);
            db.AddInParameter(cmd, "@PayMemId", DbType.Int32, entity.PayMemId);
            db.AddInParameter(cmd, "@DeliveryManName", DbType.String, entity.DeliveryManName);
            db.AddInParameter(cmd, "@DeliveryManPhone", DbType.String, entity.DeliveryManPhone);
            db.AddInParameter(cmd, "@WLComName", DbType.String, entity.WLComName);
            db.AddInParameter(cmd, "@WLCode", DbType.String, entity.WLCode);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int SendCGOrderTake(long cagoucode, int memid, string wlcode, string wlcom, string dename, string mobile)
        {
            string sql = @" UPDATE dbo.[CGOrderTake] SET
                       [DeliveryManName]=@DeliveryManName,[DeliveryManPhone]=@DeliveryManPhone,[WLComName]=@WLComName,[WLCode]=@WLCode,   [DeliveryTime]=getdate(),Status=@Status
                       WHERE [CGMemId]=@CGMemId and CGOrderCode=@CGOrderCode 
UPDATE dbo.[CGOrder] SET Status=@CGOrderStatusNew where   Code=@CGOrderCode and Status= @CGOrderStatusOld

";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cagoucode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@WLComName", DbType.String, wlcom);
            db.AddInParameter(cmd, "@WLCode", DbType.String, wlcode);
            db.AddInParameter(cmd, "@DeliveryManName", DbType.String, dename);
            db.AddInParameter(cmd, "@DeliveryManPhone", DbType.String, mobile);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)CGOrderTake.HasSend);
            db.AddInParameter(cmd, "@CGOrderStatusNew", DbType.Int16, (int)CGOrderStatus.HasDelivery);
            db.AddInParameter(cmd, "@CGOrderStatusOld", DbType.Int16, (int)CGOrderStatus.HasTake);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 收货凭证审核通过
        /// </summary>
        /// <param name="cgordercode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int OrderRecivedVoucher(long cgordercode, int memid)
        {
            string sql = @"begin tran
                        UPDATE CGOrder set Status=@OrderStatus  where  Code=@CGOrderCode  and Status=@OldOrderStatus
                        UPDATE dbo.[CGOrderTake] SET  Status=@Status,VoucherTime=getdate() WHERE  CGOrderCode=@CGOrderCode and Status=@OldStatus
                        UPDATE CGOrderVoucher set CheckTime=getdate(),Status=1,CheckManId=@MemId where CGOrderCode=@CGOrderCode and status=0
                        commit tran
                        ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgordercode);
            db.AddInParameter(cmd, "@OldOrderStatus", DbType.Int16, (int)CGOrderStatus.UploadRecivered);
            db.AddInParameter(cmd, "@OrderStatus", DbType.Int16, (int)CGOrderStatus.Finished);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)CGOrderTake.HasRecived);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)CGOrderTake.WaitCheckCer);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 收货凭证审核不通过
        /// </summary>
        /// <param name="cgordercode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int OrderRejectVoucher(long cgordercode, int memid)
        {
            string sql = @"begin tran
                        update CGOrder set Status=@OldOrderStatus where Code=@CGOrderCode and status=@CGOrderStatus
                        update CGOrderTake set Status=@OldTakeStatus  where CGOrderCode=@CGOrderCode and status=@CGTakeStatus
                        UPDATE CGOrderVoucher set CheckTime=getdate(),Status=0,CheckManId=@MemId where CGOrderCode=@CGOrderCode 
                        commit tran
                        ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgordercode);  
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);  
            db.AddInParameter(cmd, "@CGOrderStatus", DbType.Int32, (int)CGOrderStatus.UploadRecivered);
            db.AddInParameter(cmd, "@OldOrderStatus", DbType.Int32, (int)CGOrderStatus.HasDelivery);
            db.AddInParameter(cmd, "@CGTakeStatus", DbType.Int32, (int)CGOrderTake.WaitCheckCer);
            db.AddInParameter(cmd, "@OldTakeStatus", DbType.Int32, (int)CGOrderTake.HasSend);

            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGOrderTake">待更新的实体对象</param>
        public int UpdateCGOrderTake(CGOrderTakeEntity entity)
        {
            string sql = @" UPDATE dbo.[CGOrderTake] SET
                       [DeliveryManName]=@DeliveryManName,[DeliveryManPhone]=@DeliveryManPhone,[WLComName]=@WLComName,[CGOrderCode]=@CGOrderCode,[CGMemId]=@CGMemId,[TakeTime]=@TakeTime,[TakePrice]=@TakePrice,[PrintNoteTime]=@PrintNoteTime,[DeliveryTime]=@DeliveryTime,[VoucherTime]=@VoucherTime,[VoucherUrl]=@VoucherUrl,[PayTime]=@PayTime,[PayMemId]=@PayMemId
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@TakeTime", DbType.DateTime, entity.TakeTime);
            db.AddInParameter(cmd, "@TakePrice", DbType.Decimal, entity.TakePrice);
            db.AddInParameter(cmd, "@PrintNoteTime", DbType.DateTime, entity.PrintNoteTime);
            db.AddInParameter(cmd, "@DeliveryTime", DbType.DateTime, entity.DeliveryTime);
            db.AddInParameter(cmd, "@VoucherTime", DbType.DateTime, entity.VoucherTime);
            db.AddInParameter(cmd, "@VoucherUrl", DbType.String, entity.VoucherUrl);
            db.AddInParameter(cmd, "@PayMemId", DbType.Int32, entity.PayMemId);
            db.AddInParameter(cmd, "@WLComName", DbType.String, entity.WLComName);
            db.AddInParameter(cmd, "@DeliveryManName", DbType.String, entity.DeliveryManName);
            db.AddInParameter(cmd, "@DeliveryManPhone", DbType.String, entity.DeliveryManPhone);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderTakeByKey(int id)
        {
            string sql = @"delete from CGOrderTake where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderTakeDisabled()
        {
            string sql = @"delete from  CGOrderTake  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderTakeByIds(string ids)
        {
            string sql = @"Delete from CGOrderTake  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderTakeByIds(string ids)
        {
            string sql = @"Update   CGOrderTake set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public CGOrderTakeEntity GetCGOrderTake(int id)
        {
            string sql = @"SELECT  [Id],[CGOrderCode],[CGMemId],[TakeTime],[TakePrice],[PrintNoteTime],[DeliveryTime],[VoucherTime],[VoucherUrl],[PayTime],[PayMemId],Status
							FROM
							dbo.[CGOrderTake] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            CGOrderTakeEntity entity = new CGOrderTakeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.TakeTime = StringUtils.GetDbDateTime(reader["TakeTime"]);
                    entity.TakePrice = StringUtils.GetDbDecimal(reader["TakePrice"]);
                    entity.PrintNoteTime = StringUtils.GetDbDateTime(reader["PrintNoteTime"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(reader["DeliveryTime"]);
                    entity.VoucherTime = StringUtils.GetDbDateTime(reader["VoucherTime"]);
                    entity.VoucherUrl = StringUtils.GetDbString(reader["VoucherUrl"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.PayMemId = StringUtils.GetDbInt(reader["PayMemId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }
            return entity;
        }


        public CGOrderTakeEntity GetCGOrderTakeByCode(long code,int cgmemid)
        {
            string where = "where [CGOrderCode]=@CGOrderCode "; 
            if(cgmemid!=-1)
            {
                where += " and CGMemId=@CGMemId ";
            }
            string sql = @"SELECT  [DeliveryManName],[DeliveryManPhone],WLCode,[WLComName],[Id],[CGOrderCode],[CGMemId],[TakeTime],[TakePrice],[PrintNoteTime],[DeliveryTime],[VoucherTime],[VoucherUrl],[PayTime],[PayMemId],Status,Remark
							FROM
							dbo.[CGOrderTake] WITH(NOLOCK)	
						 "+ where;
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, code);
            if (cgmemid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            CGOrderTakeEntity entity = new CGOrderTakeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.TakeTime = StringUtils.GetDbDateTime(reader["TakeTime"]);
                    entity.TakePrice = StringUtils.GetDbDecimal(reader["TakePrice"]);
                    entity.PrintNoteTime = StringUtils.GetDbDateTime(reader["PrintNoteTime"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(reader["DeliveryTime"]);
                    entity.VoucherTime = StringUtils.GetDbDateTime(reader["VoucherTime"]);
                    entity.VoucherUrl = StringUtils.GetDbString(reader["VoucherUrl"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.PayMemId = StringUtils.GetDbInt(reader["PayMemId"]);
                    entity.DeliveryManName = StringUtils.GetDbString(reader["DeliveryManName"]);
                    entity.DeliveryManPhone = StringUtils.GetDbString(reader["DeliveryManPhone"]);
                    entity.WLComName = StringUtils.GetDbString(reader["WLComName"]);
                    entity.WLCode = StringUtils.GetDbString(reader["WLCode"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]); 
                }
            }
            return entity;
        }


        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGOrderTakeEntity> GetCGOrderTakeList(int pagesize, int pageindex, ref int recordCount, string code, int memid)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(code))
            {
                where += " and CGOrderCode like @CGOrderCode";
            }
            if (memid != -1)
            {
                where += " and CGMemId=@CGMemId";
            }
            string sql = @"SELECT   [Id],[CGOrderCode],[CGMemId],[TakeTime],[TakePrice],[PrintNoteTime],[DeliveryTime],[VoucherTime],[VoucherUrl],[PayTime],[PayMemId],Status
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGOrderCode],[CGMemId],[TakeTime],[TakePrice],[PrintNoteTime],[DeliveryTime],[VoucherTime],[VoucherUrl],[PayTime],[PayMemId],Status from dbo.[CGOrderTake] WITH(NOLOCK)	
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGOrderTake] with (nolock) " + where;

            IList<CGOrderTakeEntity> entityList = new List<CGOrderTakeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(code))
            {
                db.AddInParameter(cmd, "@CGOrderCode", DbType.String, "%" + code + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderTakeEntity entity = new CGOrderTakeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.TakeTime = StringUtils.GetDbDateTime(reader["TakeTime"]);
                    entity.TakePrice = StringUtils.GetDbDecimal(reader["TakePrice"]);
                    entity.PrintNoteTime = StringUtils.GetDbDateTime(reader["PrintNoteTime"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(reader["DeliveryTime"]);
                    entity.VoucherTime = StringUtils.GetDbDateTime(reader["VoucherTime"]);
                    entity.VoucherUrl = StringUtils.GetDbString(reader["VoucherUrl"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.PayMemId = StringUtils.GetDbInt(reader["PayMemId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(code))
            {
                db.AddInParameter(cmd, "@CGOrderCode", DbType.String, "%" + code + "%");
            }
            if (memid != -1)
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


        public IList<VWCGOrderReturnEntity> GetCGOrderTakeList(int pagesize, int pageindex, ref int recordCount, int pid, long B2BOrderCode)
        {

            string where = " WHERE  1=1";

            if (pid > 0)
            {
                where += " and c.ProductId=@ProductId";
            }

            if (B2BOrderCode > 0)
            {
                where += " and a.Code=@B2BOrderCode";
            }

            string sql = @"SELECT a.Code AS B2BOrderCode,b.Code AS CGOrderCode,d.CGMemId,c.ProductId,c.Num,cgr.ReturnNum,
mem.StoreName,mem.ContactsManName,mem.MobilePhone,mem.Address,mem.ProvinceId,mem.CityId
 FROM dbo.B2BOrder  a WITH(NOLOCK) INNER JOIN dbo.CGOrder b  WITH(NOLOCK)  
ON a.Code=b.SourceCode  INNER JOIN dbo.CGOrderDetail c  WITH(NOLOCK)  ON b.Code=
c.CGOrderCode INNER JOIN dbo.CGOrderTake d  WITH(NOLOCK)  ON c.CGOrderCode=d.CGOrderCode
INNER JOIN  JcCGMember.dbo.MemStore mem  WITH(NOLOCK)  ON d.CGMemId=mem.MemId
left JOIN dbo.CGReturn cgr  WITH(NOLOCK) ON d.CGMemId=cgr.CGMemId  AND B.cODE=cgr.CGOrderCode
" + where;



            string sql2 = @"Select count(1) FROM dbo.B2BOrder  a WITH(NOLOCK) INNER JOIN dbo.CGOrder b  WITH(NOLOCK)  
ON a.Code=b.SourceCode  INNER JOIN dbo.CGOrderDetail c  WITH(NOLOCK)  ON b.Code=
c.CGOrderCode INNER JOIN dbo.CGOrderTake d  WITH(NOLOCK)  ON c.CGOrderCode=d.CGOrderCode
INNER JOIN  JcCGMember.dbo.MemStore mem  WITH(NOLOCK)  ON d.CGMemId=mem.MemId  " + where;

            IList<VWCGOrderReturnEntity> entityList = new List<VWCGOrderReturnEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (pid > 0)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, pid);
            }

            if (B2BOrderCode > 0)
            {
                db.AddInParameter(cmd, "@B2BOrderCode", DbType.Int64, B2BOrderCode);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderReturnEntity entity = new VWCGOrderReturnEntity();
                    entity.B2BOrderCode = StringUtils.GetDbLong(reader["B2BOrderCode"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);
                    entity.StoreName = StringUtils.GetDbString(reader["StoreName"]);
                    entity.ContactsManName = StringUtils.GetDbString(reader["ContactsManName"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);

            if (pid > 0)
            {
                db.AddInParameter(cmd, "@ProductId", DbType.Int32, pid);

            }

            if (B2BOrderCode > 0)
            {
                db.AddInParameter(cmd, "@B2BOrderCode", DbType.Int64, B2BOrderCode);

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
        public IList<CGOrderTakeEntity> GetCGOrderTakeAll()
        {

            string sql = @"SELECT    [Id],[CGOrderCode],[CGMemId],[TakeTime],[TakePrice],[PrintNoteTime],[DeliveryTime],[VoucherTime],[VoucherUrl],[PayTime],[PayMemId] from dbo.[CGOrderTake] WITH(NOLOCK)	";
            IList<CGOrderTakeEntity> entityList = new List<CGOrderTakeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGOrderTakeEntity entity = new CGOrderTakeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.TakeTime = StringUtils.GetDbDateTime(reader["TakeTime"]);
                    entity.TakePrice = StringUtils.GetDbDecimal(reader["TakePrice"]);
                    entity.PrintNoteTime = StringUtils.GetDbDateTime(reader["PrintNoteTime"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(reader["DeliveryTime"]);
                    entity.VoucherTime = StringUtils.GetDbDateTime(reader["VoucherTime"]);
                    entity.VoucherUrl = StringUtils.GetDbString(reader["VoucherUrl"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.PayMemId = StringUtils.GetDbInt(reader["PayMemId"]);
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
        public int ExistNum(CGOrderTakeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderTake] WITH(NOLOCK) ";
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
