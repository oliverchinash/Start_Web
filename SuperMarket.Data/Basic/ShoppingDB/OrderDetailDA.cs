using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;
using SuperMarket.Model.Basic.VW.Shopping;

/*****************************************
功能描述：OrderDetail表的数据访问类。
创建时间：2016/8/6 10:38:19
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
    /// <summary>
    /// OrderDetailEntity的数据访问操作
    /// </summary>
    public partial class OrderDetailDA : BaseSuperMarketDB
    {
        #region 实例化
        public static OrderDetailDA Instance
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
            internal static readonly OrderDetailDA instance = new OrderDetailDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表OrderDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="orderDetail">待插入的实体对象</param>
        public int AddOrderDetail(OrderDetailEntity entity)
        {
            string sql = @"insert into OrderDetail( [IsDeliveried],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment],[HasReturn],[ReturnNum])VALUES
			            ( @IsDeliveried,@ProductId,@Spec1,@Spec2,@Spec3,@ProductName,@ProductPic,@DiscountId,@MarketPrice,@ActualPrice,@OrderCode,@Num,@TotalPrice,@TotalMarketPrice,@HasComment,@HasReturn,@ReturnNum);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@IsDeliveried", DbType.Int32, entity.IsDeliveried);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            db.AddInParameter(cmd, "@Spec3", DbType.String, entity.Spec3);
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
            db.AddInParameter(cmd, "@ProductPic", DbType.String, entity.ProductPic);
            db.AddInParameter(cmd, "@DiscountId", DbType.Int32, entity.DiscountId);
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, entity.MarketPrice);
            db.AddInParameter(cmd, "@ActualPrice", DbType.Decimal, entity.ActualPrice);
            db.AddInParameter(cmd, "@OrderCode", DbType.String, entity.OrderCode);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);
            db.AddInParameter(cmd, "@TotalPrice", DbType.Decimal, entity.TotalPrice);
            db.AddInParameter(cmd, "@TotalMarketPrice", DbType.Decimal, entity.TotalMarketPrice);
            db.AddInParameter(cmd, "@HasComment", DbType.Int32, entity.HasComment);
            db.AddInParameter(cmd, "@HasReturn", DbType.Int32, entity.HasReturn);
            db.AddInParameter(cmd, "@ReturnNum", DbType.Int32, entity.ReturnNum);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="orderDetail">待更新的实体对象</param>
        public int UpdateOrderDetail(OrderDetailEntity entity)
        {
            string sql = @" UPDATE dbo.[OrderDetail] SET
                       [IsDeliveried]=@IsDeliveried,[ProductId]=@ProductId,[Spec1]=@Spec1,[Spec2]=@Spec2,[Spec3]=@Spec3,[ProductName]=@ProductName,[ProductPic]=@ProductPic,[DiscountId]=@DiscountId,[MarketPrice]=@MarketPrice,[ActualPrice]=@ActualPrice,[OrderCode]=@OrderCode,[Num]=@Num,[TotalPrice]=@TotalPrice,[TotalMarketPrice]=@TotalMarketPrice,[HasComment]=@HasComment,[HasReturn]=@HasReturn,[ReturnNum]=@ReturnNum
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@IsDeliveried", DbType.Int32, entity.IsDeliveried);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@Spec1", DbType.String, entity.Spec1);
            db.AddInParameter(cmd, "@Spec2", DbType.String, entity.Spec2);
            db.AddInParameter(cmd, "@Spec3", DbType.String, entity.Spec3);
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName);
            db.AddInParameter(cmd, "@ProductPic", DbType.String, entity.ProductPic);
            db.AddInParameter(cmd, "@DiscountId", DbType.Int32, entity.DiscountId);
            db.AddInParameter(cmd, "@MarketPrice", DbType.Decimal, entity.MarketPrice);
            db.AddInParameter(cmd, "@ActualPrice", DbType.Decimal, entity.ActualPrice);
            db.AddInParameter(cmd, "@OrderCode", DbType.String, entity.OrderCode);
            db.AddInParameter(cmd, "@Num", DbType.Int32, entity.Num);
            db.AddInParameter(cmd, "@TotalPrice", DbType.Decimal, entity.TotalPrice);
            db.AddInParameter(cmd, "@TotalMarketPrice", DbType.Decimal, entity.TotalMarketPrice);
            db.AddInParameter(cmd, "@HasComment", DbType.Int32, entity.HasComment);
            db.AddInParameter(cmd, "@HasReturn", DbType.Int32, entity.HasReturn);
            db.AddInParameter(cmd, "@ReturnNum", DbType.Int32, entity.ReturnNum);
            return db.ExecuteNonQuery(cmd);
        }

        

        /// <summary>
        /// 根据主键值更新是否可以退换货。如果数据库不存在这条数据将返回0
        /// </summary>
        public int UpdateCanReturnbyId(int id,int canreturn)
        {
            string sql = @"Update dbo.OrderDetail set CanReturn=@CanReturn where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@CanReturn", DbType.Int32, canreturn);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderDetailByKey(int id)
        {
            string sql = @"delete from OrderDetail where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 设置不可退换货
        /// </summary>
        /// <returns></returns>
        public int SetCanReturnEqualsZero(int id)
        {
            string sql = @" Update OrderDetail set CanReturn=0 where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 获取指定订单下可退换货的子订单数目
        /// </summary>
        /// <returns></returns>
        public int GetRemainCanReturnNum(long ordercode)
        {
            string sql = @" select count(1) from OrderDetail where OrderCode=@OrderCode And CanReturn=1";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            object obj = db.ExecuteScalar(cmd);
            if (obj == null || obj == DBNull.Value) return 0;
            return StringUtils.GetDbInt(obj);
        }

        /// <summary>
        /// 获取指定订单下可退换货的子订单数目
        /// </summary>
        /// <returns></returns>
        public int UpdateOrderDetailReturnNum(int id,int returnnum)
        {
            string sql = @" Update dbo.[OrderDetail] set ReturnNum=@ReturnNum where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@ReturnNum", DbType.Int32, returnnum);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值更新发货状态。如果数据库不存在这条数据将返回0
        /// </summary>
        public int SetIsDelivered(int id)
        {
            string sql = @"Update dbo.[OrderDetail] set IsDeliveried=1 where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderDetailDisabled()
        {
            string sql = @"delete from  OrderDetail  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderDetailByIds(string ids)
        {
            string sql = @"Delete from OrderDetail  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderDetailByIds(string ids)
        {
            string sql = @"Update   OrderDetail set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }

        public OrderDetailEntity GetOrderDetailByOrderCode(string ordercode)
        {
            string sql = @"SELECT  [Id],[IsDeliveried],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment]
							FROM
							dbo.[OrderDetail] WITH(NOLOCK)	
							WHERE [OrderCode]=@OrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.String, ordercode);
            OrderDetailEntity entity = new OrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.IsDeliveried = StringUtils.GetDbInt(reader["IsDeliveried"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                }
            }
            return entity;
        }




        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public OrderDetailEntity GetOrderDetail(int id)
        {
            string sql = @"SELECT  [ReturnNum],[Id],[IsDeliveried],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],ProductPicSuffix,[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment]
							FROM
							dbo.[OrderDetail] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            OrderDetailEntity entity = new OrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.IsDeliveried = StringUtils.GetDbInt(reader["IsDeliveried"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);
                }
            }
            return entity;
        }
        public VWOrderDetailEntity GetVWOrderDetail(int id,int memid)
        {
            string sql = @"SELECT  b.[ReturnNum],b.[Price],B.[Id],a.CreateTime,a.PayType,[ProductId],ProductDetailId,[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],b.ProductPicSuffix,[DiscountId],[MarketPrice],[ActualPrice],b.[OrderCode],[Num],b.[TotalPrice],b.[TotalMarketPrice],[HasComment]
						,a.Status, c.AccepterName,
                          c.ProvinceId AS AccepterProvinceId,c.CityId AS AccepterCityId,c.Address AS AccepterAddress,c.MobilePhone AS AccepterPhone,c.Telephone
                            FROM dbo.[Order] A WITH(NOLOCK)	 INNER JOIN 
							dbo.[OrderDetail] B WITH(NOLOCK) ON A.Code=B.OrderCode
                            inner join dbo.OrderAddress c WITH(NOLOCK) ON a.Code=c.OrderCode 
							WHERE B.[Id]=@DetailId and A.MemId=@MemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@DetailId", DbType.Int32, id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            VWOrderDetailEntity entity = new VWOrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);  
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]); 
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]); 
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.AccepterProvinceId = StringUtils.GetDbInt(reader["AccepterProvinceId"]);
                    entity.AccepterCityId = StringUtils.GetDbInt(reader["AccepterCityId"]);
                    entity.AccepterAddress = StringUtils.GetDbString(reader["AccepterAddress"]);
                    entity.AccepterPhone = StringUtils.GetDbString(reader["AccepterPhone"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);

                }
            }
            return entity;
        }

        /// <summary>
        /// 设置已退货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SetOrderDetailHasReturn(int id)
        {
            string sql = @"Update dbo.OrderDetail set [HasReturn]=1 where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            int result = db.ExecuteNonQuery(cmd);
            return result;
        }



        public VWOrderDetailEntity GetVWOrderDetail(int id)
        {
            string sql = @"SELECT  B.[Id],a.CreateTime,[ProductId],ProductDetailId,[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],b.[TotalPrice],b.[TotalMarketPrice],[HasComment]
							FROM
						dbo.[Order] A WITH(NOLOCK)	 INNER JOIN 
							dbo.[OrderDetail] B WITH(NOLOCK)	 ON A.Code=B.OrderCode
							WHERE B.[Id]=@DetailId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@DetailId", DbType.Int32, id);
            VWOrderDetailEntity entity = new VWOrderDetailEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<OrderDetailEntity> GetOrderDetailByOrderRange(string ordercoderange)
        {
            string where = " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(ordercoderange))
            {
                where += " AND OrderCode in @OrderCodeRange";
            }
            string sql = @"SELECT top 50 [Id],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment]
							FROM
							dbo.[OrderDetail] WITH(NOLOCK)	
							" + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);

            if (!string.IsNullOrEmpty(ordercoderange))
            {
                db.AddInParameter(cmd, "@OrderCodeRange", DbType.String, "(" + ordercoderange + ")");
            }
            IList<OrderDetailEntity> entitylist = new List<OrderDetailEntity>();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailEntity entity = new OrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entitylist.Add(entity);
                }
            }
            return entitylist;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<OrderDetailEntity> GetOrderDetailList(int pagesize, int pageindex, ref int recordCount, int _memid, long ordercode, int _status, int hascomment)
        {
            string where = " WHERE 1=1 ";

            if (ordercode > 0)
            {
                where += " AND a.Code=@OrderCode ";
            }

            if (hascomment != -1)
            {
                where += " AND HasComment=@HasComment ";
            }
            if (_status != -1)
            {
                where += " AND a.Status=@OrderStatus ";
            }
            if (_memid != -1)
            {
                where += " AND a.MemId=@MemId ";
            }
            string sql = @"SELECT    OrderId, id,[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num], [TotalPrice], [TotalMarketPrice],[HasComment]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY b.Id desc) AS ROWNUMBER,
						   a.[Id] AS OrderId,b.id,[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],b.[TotalPrice],b.[TotalMarketPrice],[HasComment] from 
						dbo.[Order] a  WITH(NOLOCK)	 INNER JOIN  dbo.[OrderDetail] b WITH(NOLOCK)	 ON a.Code=b.OrderCode
						 " + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Order] a   with (nolock) inner join   dbo.[OrderDetail] b with (nolock) on a.Code=b.OrderCode " + where;
            IList<OrderDetailEntity> entityList = new List<OrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            }

            if (hascomment != -1)
            {
                db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
            }
            if (_status != -1)
            {
                db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, _status);

            }
            if (_memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, _memid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailEntity entity = new OrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (ordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            }

            if (hascomment != -1)
            {
                db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
            }
            if (_status != -1)
            {
                db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, _status);
            }
            if (_memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, _memid);
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
        public IList<OrderDetailEntity> GetSyncDetailsByCode(Int64 code)
        {
            IList<OrderDetailEntity> entityList = new List<OrderDetailEntity>();
            string sql = @"SELECT [Id]      ,[OrderCode]      ,[ProductDetailId]      ,[ProductId]      ,[ProductName]      ,[Spec1]      ,[Spec2]      ,[Spec3]      ,[ProductPic]      ,[ProductPicSuffix]      ,[DiscountId]      ,[Cost]      ,[Price]      ,[TradePrice]      ,[MarketPrice]      ,[ActualPrice]      ,[Num]      ,[TotalPrice]      ,[TotalMarketPrice]      ,[HasComment],[TransFeeType],[TransFee],[Weight],[Volume],[IsDeliveried],[PostDisCountId],[CanReturn],[HasReturn],[ReturnNum],[ProductType] FROM dbo.OrderDetail WHERE OrderCode=(
    SELECT TOP 1 Code FROM [dbo].[Order] where Code=@Code and  SyncCGStatus=@SyncCGStatus ORDER BY id ASC)";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code);
            db.AddInParameter(cmd, "@SyncCGStatus", DbType.Int32, (int)SyncCGStatus.Syncing);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailEntity entity = new OrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public int GetWaitEvaluateNum(int memid, int hascomment,int orderstyle)
        {
            string where = " where a.MemId=@MemId and a.Status = @Status and b.HasComment = @HasComment ";
            if(orderstyle>0)
            {
                where += " and a.OrderStyle=@OrderStyle ";
            }
            string sql = @"Select count(1) as Num from dbo.[Order] a   with (nolock) inner join   dbo.[OrderDetail] b with (nolock) on a.Code=b.OrderCode  "+ where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderStatus.Finished);
            db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
            if (orderstyle > 0)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle); 
            }
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<OrderDetailEntity> GetOrderDetailAll()
        {

            string sql = @"SELECT    [Id],[IsDeliveried],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment] from dbo.[OrderDetail] WITH(NOLOCK)	";
            IList<OrderDetailEntity> entityList = new List<OrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailEntity entity = new OrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.IsDeliveried = StringUtils.GetDbInt(reader["IsDeliveried"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
       
        /// <summary>
        /// 获取订单详情列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="orderdetailid"></param>
        /// <param name="hascomment"></param>
        /// <returns></returns>
        public IList<OrderDetailEntity> GetOrderDetailListOfAdmin(int pagesize, int pageindex, ref int recordCount, string productname, int hascomment,string ordercode)
        {
            string where = " WHERE 1=1 ";
            if (!string.IsNullOrEmpty(productname))
            {
                where += " AND ProductName like @ProductName";
            }
            if (!string.IsNullOrEmpty(ordercode))
            {
                where += " AND OrderCode = @OrderCode";
            }
            if (hascomment != -1)
            {
                where += " AND HasComment=@HasComment";
            }
            string sql = @"SELECT   [Id],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment],[CanReturn],[IsDeliveried]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ProductId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[OrderCode],[Num],[TotalPrice],[TotalMarketPrice],[HasComment],[CanReturn],[IsDeliveried] from dbo.[OrderDetail] WITH(NOLOCK)	
						" + where + ") as temp where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";


            string sql2 = @"Select count(1) from dbo.[OrderDetail] with (nolock) " + where;
            IList<OrderDetailEntity> entityList = new List<OrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (!string.IsNullOrEmpty(productname))
            {
                db.AddInParameter(cmd, "@ProductName", DbType.String, "%" + productname + "%");
            }

            if (!string.IsNullOrEmpty(ordercode))
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.String,  ordercode );
            }
            if (hascomment != -1)
            {
                db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailEntity entity = new OrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]);
                    entity.IsDeliveried = StringUtils.GetDbInt(reader["IsDeliveried"]);
                    entityList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(productname))
            {
                db.AddInParameter(cmd, "@ProductName", DbType.String, "%" + productname + "%");
            }

            if (!string.IsNullOrEmpty(ordercode))
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.String, ordercode);
            }

            if (hascomment != -1)
            {
                db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
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
        /// 判断当前节点是否已存在相同的
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int ExistNum(OrderDetailEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderDetail] WITH(NOLOCK) ";
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
        public int CommentHasEvaluate(int orderdetailid)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"update dbo.[OrderDetail] set HasComment=1 where id=@OrderDetailId  ";
            
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OrderDetailId", DbType.Int32, orderdetailid);
            
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public decimal GetTotalPrice(long ordercode, int producttype)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select Sum(TotalPrice) from dbo.[OrderDetail] WITH(NOLOCK) where OrderCode=@OrderCode and ProductType=@ProductType ";
            
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode); 
            db.AddInParameter(cmd, "@ProductType", DbType.Int16, producttype);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToDecimal(identity);
        }
        #endregion
        public IList<OrderDetailEntity> GetOrderDetailAllByOrder(int memid, long ordercode)
        {

            string sql = @"SELECT a.[TradePrice],a.[Id],[ProductId],[ProductDetailId],[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],a.[DiscountId],[MarketPrice],
                         [ActualPrice],[OrderCode],[Num],a.[TotalPrice],a.[TotalMarketPrice],a.[HasComment],a.[HasReturn],a.[ReturnNum] from dbo.[OrderDetail] a WITH(NOLOCK) inner join dbo.[Order] b WITH(NOLOCK)
                         on a.OrderCode=b.Code where b.Code=@OrderCode and b.MemId=@MemId";
            IList<OrderDetailEntity> entityList = new List<OrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailEntity entity = new OrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TradePrice = StringUtils.GetDbDecimal(reader["TradePrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entity.HasReturn = StringUtils.GetDbInt(reader["HasReturn"]);
                    entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);
                    entityList.Add(entity);
                }
            }
            return entityList;

        }
        public IList<VWOrderDetailEntity> GetDetailsByOrderCode(long ordercode,string keyword,int canreturn )
        {
            string where = "and 1=1 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                where += " and ProductName like @KeyWord";
            }
            if (canreturn!=-1)
            {
                where += " and CanReturn =@CanReturn";
            }
            string sql = @"SELECT a.[Id],[ProductId],ProductDetailId,[Spec1],[Spec2],[Spec3],[ProductName],[ProductPic],a.[DiscountId],[MarketPrice],a.[ProductPicSuffix],
                         [ActualPrice],[OrderCode],[Num],a.[TotalPrice],a.[TotalMarketPrice],a.[HasComment],a.[HasReturn],a.[ReturnNum],CanReturn from dbo.[OrderDetail] a WITH(NOLOCK)   where a.OrderCode=@OrderCode  " + where;

            IList<VWOrderDetailEntity> entityList = new List<VWOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@OrderCode", DbType.String, ordercode);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@KeyWord", DbType.String, keyword);
            }
            if (canreturn != -1)
            { 
                db.AddInParameter(cmd, "@CanReturn", DbType.Int32, canreturn);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWOrderDetailEntity entity = new VWOrderDetailEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);  
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]); 
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entity.HasReturn = StringUtils.GetDbInt(reader["HasReturn"]);
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]);
                    entityList.Add(entity);
                }
            }
            return entityList;

        }
        #endregion

        /// <summary>
        /// 获取我的退换货订单
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="memid"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>

        public IList<VWReturnEntity> GetMyReturnsList(int pagesize, int pageindex, ref int recordCount, int memid, string keyword)
        {
            string where = " where HasReturn=1 AND MemId=@MemId ";

            if (!string.IsNullOrEmpty(keyword))
            {
                where += " AND ProductName like @keyword ";
            }

            string sql = @"SELECT OrderCode,OrderDetailId,CreateTime,ProductPic,ProductName 
                         FROM
                         (Select ROW_NUMBER() OVER (Order By a.Id desc) as ROWNUMBER, a.Code as OrderCode,b.[Id] as OrderDetailId,a.CreateTime,b.ProductPic,b.ProductName from dbo.[Order] a left join dbo.[OrderDetail] b on a.Code=b.OrderCode
                          " + where + @") as temp where ROWNUMBER BETWEEN (@Pagesize*(@Pageindex-1)+1) AND @PageSize*@PageIndex ";

            IList<VWReturnEntity> entitylist = new List<VWReturnEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Pageindex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@Pagesize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "MemId", DbType.String, memid);
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWReturnEntity entity = new VWReturnEntity();
                    entity.OrderCode = StringUtils.GetDbString(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entitylist.Add(entity);
                }
            }

            string sql2 = "select count(1) from dbo.[Order] a left join dbo.[OrderDetail] b on a.Code=b.OrderCode " + where;
            cmd = db.GetSqlStringCommand(sql2);
            db.AddInParameter(cmd, "@Pageindex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@Pagesize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@MemId", DbType.String, memid);
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@keyword", DbType.String, "%" + keyword + "%");
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

            return entitylist;
        }
        /// <summary>
        /// 获取订单详情视图表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="keyword"></param>
        /// <param name="status"></param>
        /// <param name="term"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public IList<VWOrderDetailEntity> GetVWOrderDetailList(int pagesize, int pageindex, ref int recordCount, string keyword, int status, int term, int memid, int hascomment,int  _orderstyle)
        {
            string where = "WHERE  MemId=@MemId ";

            if (!string.IsNullOrEmpty(keyword))
            {
                where += " and code like @Code ";
            }
            if (status > 0)
            {
                where += " and Status = @Status ";
            }
            if (_orderstyle > 0)
            {
                where += " and  a.OrderStyle=@OrderStyle ";
            }
            if (hascomment != -1)
            {
                where += " AND HasComment=@HasComment ";
            }
            if (term >= 0)
            {
                switch (term)
                {
                    case (int)OrderTerm.Default:
                        where += " and  CreateTime > DATEADD(month, -3,GETDATE())  ";
                        break;
                    case (int)OrderTerm.YearThis:
                        where += " and  year(CreateTime)=year(GETDATE()) ";
                        break;
                    case (int)OrderTerm.YearLast:
                        where += " and  year(CreateTime) =  year(GETDATE())-1  ";
                        break;
                    case (int)OrderTerm.YearPrevious:
                        where += " and  year(CreateTime) =  year(GETDATE())-2  ";
                        break;
                }
            }


            string sql = @"SELECT ProductPicSuffix,OrderCode,AccepterName,OrderDetailId,CreateTime,ProductPic,ProductName,Num,TotalPrice,OrderType,OrderStyle,HasComment,Status,Spec1,Spec2,Spec3,HasReturn
                           FROM
                           (SELECT ROW_NUMBER() OVER (Order By a.Id desc,b.id desc) as ROWNUMBER,b.ProductPicSuffix,a.[Code] as OrderCode,b.[Id] as OrderDetailId,a.[CreateTime],b.[ProductPic],b.[ProductName],
                           b.[Num],c.AccepterName, b.[TotalPrice],a.[OrderType],a.OrderStyle,b.[HasComment],a.[Status],b.[Spec1],b.[Spec2],b.[Spec3],b.[HasReturn] from dbo.[Order] a  WITH(NOLOCK) INNER JOIN  OrderAddress c WITH(NOLOCK)  on a.Code=c.OrderCode inner join dbo.OrderDetail b  WITH(NOLOCK) on a.Code=b.OrderCode   "
                           + where + @")as temp
                           where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Order] a  WITH(NOLOCK)  inner join dbo.OrderDetail b  WITH(NOLOCK) on a.Code=b.OrderCode " + where;

            IList<VWOrderDetailEntity> entityList = new List<VWOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + keyword + "%");
            }
            if (status >= 0 && status != (int)OrderStatus.AllStatus)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (_orderstyle > 0)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, _orderstyle); 
            }
            if (hascomment != -1)
            {
                db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWOrderDetailEntity entity = new VWOrderDetailEntity();
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["OrderDetailId"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.HasComment = StringUtils.GetDbInt(reader["HasComment"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.HasReturn = StringUtils.GetDbInt(reader["HasReturn"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);
                    entityList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + keyword + "%");
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (_orderstyle > 0)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, _orderstyle);
            }
            if (hascomment != -1)
            {
                db.AddInParameter(cmd, "@HasComment", DbType.Int32, hascomment);
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


    }
}
