using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;
using System.Data.SqlClient;

/*****************************************
功能描述：ReturnXS表的数据访问类。
创建时间：2016/9/22 11:23:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
    /// <summary>
    /// ReturnXSEntity的数据访问操作
    /// </summary>
    public partial class ReturnXSDA : BaseSuperMarketDB
    {
        #region 实例化
        public static ReturnXSDA Instance
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
            internal static readonly ReturnXSDA instance = new ReturnXSDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表ReturnXS，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="returnXS">待插入的实体对象</param>
        public int AddReturnXS(ReturnXSEntity entity)
        {
            string sql = @"insert into ReturnXS([PayType], [ProductPic],[ProductPicSuffix],[ProvinceId],[CityId],[GetTime],[GetAddress],[AcceptName],[AcceptPhone],  [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId])VALUES
			            (@PayType,@ProductPic, @ProductPicSuffix,@ProvinceId,@CityId,@GetTime,@GetAddress,@AcceptName,@AcceptPhone,@ProductId,@ReturnOrderCode,@ReturnOrderDetailId,@ProductName,@Num,@Price,@ReturnType,@NewOrderCode,@ReturnDescription,@ReturnReason,@CreateTime,@Status,@MemId,@RejectReason,@CheckManId);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@ReturnOrderCode", DbType.Int64, entity.ReturnOrderCode);
            db.AddInParameter(cmd, "@ReturnOrderDetailId", DbType.Int32, entity.ReturnOrderDetailId);
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);
            db.AddInParameter(cmd, "@Price", DbType.Decimal, entity.Price);
            db.AddInParameter(cmd, "@ReturnType", DbType.Int32, entity.ReturnType);

            db.AddInParameter(cmd, "@NewOrderCode", DbType.String, entity.NewOrderCode);
            db.AddInParameter(cmd, "@ReturnDescription", DbType.String, entity.ReturnDescription);
            db.AddInParameter(cmd, "@ReturnReason", DbType.String, entity.ReturnReason);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@RejectReason", DbType.String, entity.RejectReason);
            db.AddInParameter(cmd, "@CheckManId", DbType.String, entity.CheckManId);
            //db.AddInParameter(cmd, "@CheckTime", DbType.DateTime, entity.CheckTime);

            db.AddInParameter(cmd, "@ProductPic", DbType.String, entity.ProductPic);
            db.AddInParameter(cmd, "@ProductPicSuffix", DbType.String, entity.ProductPicSuffix);
            db.AddInParameter(cmd, "@PayType", DbType.Int32, entity.PayType);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, entity.ProvinceId);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, entity.CityId);
            db.AddInParameter(cmd, "@GetTime", DbType.DateTime, entity.GetTime);
            db.AddInParameter(cmd, "@GetAddress", DbType.String, entity.GetAddress);
            db.AddInParameter(cmd, "@AcceptName", DbType.String, entity.AcceptName);
            db.AddInParameter(cmd, "@AcceptPhone", DbType.String, entity.AcceptPhone);



            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="returnXS">待更新的实体对象</param>
        public int UpdateReturnXS(ReturnXSEntity entity)
        {
            string sql = @" UPDATE dbo.[ReturnXS] SET
                       [PayType]=@PayType,ProvinceId=@ProvinceId,CityId=@CityId,GetTime=@GetTime,GetAddress=@GetAddress,AcceptName=@AcceptName,AcceptPhone=@AcceptPhone,ProductPicSuffix=@ProductPicSuffix,     [ProductId]=@ProductId,[ReturnOrderCode]=@ReturnOrderCode,[ReturnOrderDetailId]=@ReturnOrderDetailId,[ProductName]=@ProductName,[Num]=@Num,[Price]=@Price,[ReturnType]=@ReturnType,[NewOrderCode]=@NewOrderCode,[ReturnDescription]=@ReturnDescription,[ReturnReason]=@ReturnReason,[CreateTime]=@CreateTime,[Status]=@Status,[MemId]=@MemId,[RejectReason]=@RejectReason,[CheckManId]=@CheckManId,[CheckTime]=@CheckTime
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@ReturnOrderCode", DbType.Int64, entity.ReturnOrderCode);
            db.AddInParameter(cmd, "@ReturnOrderDetailId", DbType.Int32, entity.ReturnOrderDetailId);
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);
            db.AddInParameter(cmd, "@Price", DbType.Decimal, entity.Price);
            db.AddInParameter(cmd, "@ReturnType", DbType.Int32, entity.ReturnType);

            db.AddInParameter(cmd, "@NewOrderCode", DbType.String, entity.NewOrderCode);
            db.AddInParameter(cmd, "@ReturnDescription", DbType.String, entity.ReturnDescription);
            db.AddInParameter(cmd, "@ReturnReason", DbType.String, entity.ReturnReason);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@RejectReason", DbType.String, entity.RejectReason);
            db.AddInParameter(cmd, "@CheckManId", DbType.String, entity.CheckManId);
            db.AddInParameter(cmd, "@CheckTime", DbType.DateTime, entity.CheckTime);

            db.AddInParameter(cmd, "@ProductPicSuffix", DbType.String, entity.ProductPicSuffix);
            db.AddInParameter(cmd, "@PayType", DbType.Int32, entity.PayType);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, entity.ProvinceId);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, entity.CityId);
            db.AddInParameter(cmd, "@GetTime", DbType.DateTime, entity.GetTime);
            db.AddInParameter(cmd, "@GetAddress", DbType.String, entity.GetAddress);
            db.AddInParameter(cmd, "@AcceptName", DbType.String, entity.AcceptName);
            db.AddInParameter(cmd, "@AcceptPhone", DbType.String, entity.AcceptPhone);


            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="returnXS">待更新的实体对象</param>
        public int UpdateReturnXSStatus(int returnId,int status,int oldstatus)
        {
            string where = " where [Id]=@id ";
            if(oldstatus!=-1)
            {
                where += " and  [Status]=@StatusOld ";
            }
            string sql = @" UPDATE dbo.[ReturnXS] SET
                         [Status]=@Status  " + where;
             
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, returnId  );
            db.AddInParameter(cmd, "@Status", DbType.Int32, status); 
            if (oldstatus != -1)
            {
                db.AddInParameter(cmd, "@StatusOld", DbType.Int32, oldstatus);
            }
            return db.ExecuteNonQuery(cmd);
        }

        public int UpdateReturnXSNewOrderCode(int returnId, string newOrderCode,int status)
        {
            string sql = @" UPDATE dbo.[ReturnXS] SET
                         [NewOrderCode]=@NewOrderCode,Status=@Status
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, returnId);
            db.AddInParameter(cmd, "@NewOrderCode", DbType.String, newOrderCode);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);


            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="returnXS">待更新的实体对象</param>
        public int UpdateReturnXSByReturnOrderCode(long returnOrderCode)
        {
            string sql = @" UPDATE dbo.[ReturnXS] SET Status=@Status where ReturnOrderCode=@ReturnOrderCode ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ReturnOrderCode", DbType.Int64, returnOrderCode);
            db.AddInParameter(cmd, "@Status", DbType.Int32, ReturnOrderStatus.Complete);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteReturnXSByKey(int id)
        {
            string sql = @"delete from ReturnXS where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }

        
        /// <summary>
        /// 退换货
        /// </summary>
        public int ReturnGoods(int memid, long orderCode,int orderDetailId,int returnNum, int _returntype, string _returnreason, string _acceptname, string _acceptphone, int _provinceid, int _cityid, string _getaddress,int receiveType)
        {
            string sql = @"EXEC [dbo].[Proc_ReturnGoods] @MemId,@OrderCode,@OrderDetailId,@CurrentReturnNum,@ReturnType,@ReturnReason,@AcceptName,@AcceptPhone,@ProvinceId,@CityId,@Address,@ReceiveType";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, orderCode);
            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, orderDetailId);
            db.AddInParameter(cmd, "@CurrentReturnNum", DbType.Int32, returnNum);
            db.AddInParameter(cmd, "@ReturnType", DbType.Int32, _returntype);
            db.AddInParameter(cmd, "@ReturnReason", DbType.String, _returnreason);  
            db.AddInParameter(cmd, "@AcceptName", DbType.String, _acceptname);
            db.AddInParameter(cmd, "@AcceptPhone", DbType.String, _acceptphone);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, _provinceid);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, _cityid); 
            db.AddInParameter(cmd, "@Address", DbType.String, _getaddress);
            db.AddInParameter(cmd, "@ReceiveType", DbType.Int32, receiveType);
            object obj=db.ExecuteScalar(cmd);
            if (obj == null || obj == DBNull.Value) return 0;
            return StringUtils.GetDbInt(obj);
        }

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteReturnXSDisabled()
        {
            string sql = @"delete from  ReturnXS  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteReturnXSByIds(string ids)
        {
            string sql = @"Delete from ReturnXS  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableReturnXSByIds(string ids)
        {
            string sql = @"Update   ReturnXS set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public ReturnXSEntity GetReturnXS(int id,int memid)
        {
            string where = " where [Id]=@id ";
            if (memid!=-1)
            {
                where += " and MemId=@MemId ";
            }
            string sql = @"SELECT  [Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime],ProductPic,[ProductPicSuffix],[ProvinceId],[CityId],[GetTime],[GetAddress],[AcceptName],[AcceptPhone],[PayType]
							FROM
							dbo.[ReturnXS] WITH(NOLOCK)	 "+ where;
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            ReturnXSEntity entity = new ReturnXSEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ReturnOrderCode = StringUtils.GetDbLong(reader["ReturnOrderCode"]);
                    entity.ReturnOrderDetailId = StringUtils.GetDbInt(reader["ReturnOrderDetailId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.ReturnType = StringUtils.GetDbInt(reader["ReturnType"]);

                    entity.NewOrderCode = StringUtils.GetDbString(reader["NewOrderCode"]);
                    entity.ReturnDescription = StringUtils.GetDbString(reader["ReturnDescription"]);
                    entity.ReturnReason = StringUtils.GetDbString(reader["ReturnReason"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.RejectReason = StringUtils.GetDbString(reader["RejectReason"]);
                    entity.CheckManId = StringUtils.GetDbString(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.GetTime = StringUtils.GetDbDateTime(reader["GetTime"]);
                    entity.GetAddress = StringUtils.GetDbString(reader["GetAddress"]);
                    entity.AcceptName = StringUtils.GetDbString(reader["AcceptName"]);
                    entity.AcceptPhone = StringUtils.GetDbString(reader["AcceptPhone"]);
                }
            }
            return entity;
        }


        public bool ReturnNumEqDist(int id)
        {
            string sql = @"SELECT 1 FROM 	dbo.ReturnXS WHERE id= @ReturnId
AND Num=(
SELECT SUM(ISNULL(ReturnNum,0)) FROM 	dbo.ReturnXSWL WHERE ReturnId= @ReturnId)";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, id);
            object obj = db.ExecuteScalar(cmd);
            if (obj == null || obj == DBNull.Value) return false;
            return StringUtils.GetDbInt(obj)==1;

        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ReturnXSEntity> GetReturnXSList(int pagesize, int pageindex, ref int recordCount, int memid, int returntype, string keyword, int term, int status)
        {
            string where = " WHERE  1=1 ";

            if (memid > 0)
            {
                where += " AND MemId=@MemId ";
            }

            if (returntype > 0)
            {
                where += " AND ReturnType=@ReturnType";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                where += " AND (ReturnOrderCode like @KeyWord)";
            }
            if (term >-1)
            {
                switch (term)
                {
                    case (int)ReturnOrderTerm.ThreeMonth:
                        {
                            where += " AND CreateTime>DATEADD(mm,-3,getdate())";
                            break;
                        }
                    case (int)ReturnOrderTerm.HalfYear:
                        {
                            where += " AND CreateTime>DATEADD(mm,-6,getdate())";
                            break;
                        }
                    case (int)ReturnOrderTerm.ThisYear:
                        {
                            where += " AND year(CreateTime)=year(getdate())";
                            break;
                        }
                    case (int)ReturnOrderTerm.LastYear:
                        {
                            where += " AND year(CreateTime)=year(getdate())-1";
                            break;
                        }
                    case (int)ReturnOrderTerm.PreviousYear:
                        {
                            where += " AND year(CreateTime)=year(getdate())-2";
                            break;
                        }
                }
            }
            if (status >-1)
            {
                where += " AND Status=@Status";
            }

            string sql = @"SELECT  [AahamaReciveType],[PayType],[ProductPicSuffix],[ProvinceId],[CityId],[GetTime],[GetAddress],[AcceptName],[AcceptPhone], [Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY id desc) AS ROWNUMBER,
						 [AahamaReciveType],[PayType],[ProductPicSuffix],[ProvinceId],[CityId],[GetTime],[GetAddress],[AcceptName],[AcceptPhone], [Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status]
                        ,[MemId],[RejectReason],[CheckManId],[CheckTime] 
                            from dbo.[ReturnXS] WITH(NOLOCK)  
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[ReturnXS] with (nolock) "+where;
            IList<ReturnXSEntity> entityList = new List<ReturnXSEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (returntype > 0)
            {
                db.AddInParameter(cmd, "@ReturnType", DbType.Int32, returntype);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@KeyWord", DbType.String, "%" + keyword + "%");
            }
            if (status>-1)
            {
                db.AddInParameter(cmd, "@Status", DbType.String, status);
            }
       
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReturnXSEntity entity = new ReturnXSEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ReturnOrderCode = StringUtils.GetDbLong(reader["ReturnOrderCode"]);
                    entity.ReturnOrderDetailId = StringUtils.GetDbInt(reader["ReturnOrderDetailId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.ReturnType = StringUtils.GetDbInt(reader["ReturnType"]);

                    entity.NewOrderCode = StringUtils.GetDbString(reader["NewOrderCode"]);
                    entity.ReturnDescription = StringUtils.GetDbString(reader["ReturnDescription"]);
                    entity.ReturnReason = StringUtils.GetDbString(reader["ReturnReason"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.RejectReason = StringUtils.GetDbString(reader["RejectReason"]);
                    entity.CheckManId = StringUtils.GetDbString(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);

                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.GetTime = StringUtils.GetDbDateTime(reader["GetTime"]);
                    entity.GetAddress = StringUtils.GetDbString(reader["GetAddress"]);
                    entity.AcceptName = StringUtils.GetDbString(reader["AcceptName"]);
                    entity.AcceptPhone = StringUtils.GetDbString(reader["AcceptPhone"]);
                    entity.AahamaReciveType = StringUtils.GetDbInt(reader["AahamaReciveType"]);

                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (returntype > 0)
            {
                db.AddInParameter(cmd, "@ReturnType", DbType.Int32, returntype);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@KeyWord", DbType.String, "%" + keyword + "%");
            }
            if (status > -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.String, status);
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
















        public IList<ReturnXSEntity> GetReturnXSListByMemId(int pagesize, int pageindex, ref int RecordCount, int MemId)
        {
            string sql = @"SELECT   [AahamaReciveType],[Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [AahamaReciveType],[Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime] from dbo.[ReturnXS] WITH(NOLOCK)	
						WHERE  MemId=@MemId ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[ReturnXS] with (nolock) ";
            IList<ReturnXSEntity> entityList = new List<ReturnXSEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, MemId);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReturnXSEntity entity = new ReturnXSEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ReturnOrderCode = StringUtils.GetDbLong(reader["ReturnOrderCode"]);
                    entity.ReturnOrderDetailId = StringUtils.GetDbInt(reader["ReturnOrderDetailId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.ReturnType = StringUtils.GetDbInt(reader["ReturnType"]);

                    entity.NewOrderCode = StringUtils.GetDbString(reader["NewOrderCode"]);
                    entity.ReturnDescription = StringUtils.GetDbString(reader["ReturnDescription"]);
                    entity.ReturnReason = StringUtils.GetDbString(reader["ReturnReason"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.RejectReason = StringUtils.GetDbString(reader["RejectReason"]);
                    entity.CheckManId = StringUtils.GetDbString(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.AahamaReciveType = StringUtils.GetDbInt(reader["AahamaReciveType"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    RecordCount = StringUtils.GetDbInt(reader[0]);
                }
                else
                {
                    RecordCount = 0;
                }
            }
            return entityList;
        }


        public IList<ReturnXSEntity> GetReturnXSListByReturnType(int pagesize, int pageindex, ref int recordCount, int returnType)
        {
            string sql = @"SELECT   [Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime] from dbo.[ReturnXS] WITH(NOLOCK)	
						WHERE  ReturnType=@ReturnType ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[ReturnXS] with (nolock) ";
            IList<ReturnXSEntity> entityList = new List<ReturnXSEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@ReturnType", DbType.Int32, returnType);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReturnXSEntity entity = new ReturnXSEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ReturnOrderCode = StringUtils.GetDbLong(reader["ReturnOrderCode"]);
                    entity.ReturnOrderDetailId = StringUtils.GetDbInt(reader["ReturnOrderDetailId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.ReturnType = StringUtils.GetDbInt(reader["ReturnType"]);

                    entity.NewOrderCode = StringUtils.GetDbString(reader["NewOrderCode"]);
                    entity.ReturnDescription = StringUtils.GetDbString(reader["ReturnDescription"]);
                    entity.ReturnReason = StringUtils.GetDbString(reader["ReturnReason"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.RejectReason = StringUtils.GetDbString(reader["RejectReason"]);
                    entity.CheckManId = StringUtils.GetDbString(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public IList<ReturnXSEntity> GetReturnXSAll()
        {

            string sql = @"SELECT    [Id], [ProductId],[ReturnOrderCode],[ReturnOrderDetailId],[ProductName],[Num],[Price],[ReturnType],[NewOrderCode],[ReturnDescription],[ReturnReason],[CreateTime],[Status],[MemId],[RejectReason],[CheckManId],[CheckTime] from dbo.[ReturnXS] WITH(NOLOCK)	";
            IList<ReturnXSEntity> entityList = new List<ReturnXSEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReturnXSEntity entity = new ReturnXSEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ReturnOrderCode = StringUtils.GetDbLong(reader["ReturnOrderCode"]);
                    entity.ReturnOrderDetailId = StringUtils.GetDbInt(reader["ReturnOrderDetailId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.ReturnType = StringUtils.GetDbInt(reader["ReturnType"]);

                    entity.NewOrderCode = StringUtils.GetDbString(reader["NewOrderCode"]);
                    entity.ReturnDescription = StringUtils.GetDbString(reader["ReturnDescription"]);
                    entity.ReturnReason = StringUtils.GetDbString(reader["ReturnReason"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.RejectReason = StringUtils.GetDbString(reader["RejectReason"]);
                    entity.CheckManId = StringUtils.GetDbString(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public int ExistNum(ReturnXSEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ReturnXS] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "  (ProductName=@ProductName) ";

            }
            else
            {
                where = where + " id<>@Id and  (ProductName=@ProductName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }

            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }








        #endregion
        #endregion
    }
}
