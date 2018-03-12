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
using System.Linq;

/*****************************************
功能描述：Order表的数据访问类。
创建时间：2016/8/6 11:59:30
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
    /// <summary>
    /// OrderEntity的数据访问操作
    /// </summary>
    public partial class OrderDA : BaseSuperMarketDB
    {
        #region 实例化
        public static OrderDA Instance
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
            internal static readonly OrderDA instance = new OrderDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表Order，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="order">待插入的实体对象</param>
        public int AddOrder(OrderEntity entity)
        {
            string sql = @"insert into Order( [ReBackFee],[CanReturn],[Code],[OrderVisualCode],[MemId],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime])VALUES
			            ( ,@ReBackFee,@CanReturn,@Code,@OrderVisualCode,@MemId,@IsStore,@Status,@CreateTime,@OrderType,@TransFee,@NeedDeliver,@Integral,@TotalPrice,@TotalMarketPrice,@DealTime,@DeliverTime,@ReciveTime,@MakeBill,@FinishedTime);
			SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@OrderVisualCode", DbType.Int64, entity.OrderVisualCode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@IsStore", DbType.String, entity.IsStore);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@OrderType", DbType.Int32, entity.OrderType);
            db.AddInParameter(cmd, "@TransFee", DbType.Decimal, entity.TransFee);
            db.AddInParameter(cmd, "@NeedDeliver", DbType.Int32, entity.NeedDeliver);
            db.AddInParameter(cmd, "@Integral", DbType.Int32, entity.Integral);
            db.AddInParameter(cmd, "@TotalPrice", DbType.Decimal, entity.TotalPrice);
            db.AddInParameter(cmd, "@TotalMarketPrice", DbType.Decimal, entity.TotalMarketPrice);
            db.AddInParameter(cmd, "@DealTime", DbType.DateTime, entity.DealTime);
            db.AddInParameter(cmd, "@DeliverTime", DbType.DateTime, entity.DeliverTime);
            db.AddInParameter(cmd, "@ReciveTime", DbType.DateTime, entity.ReciveTime);
            db.AddInParameter(cmd, "@MakeBill", DbType.Int32, entity.MakeBill);
            db.AddInParameter(cmd, "@FinishedTime", DbType.DateTime, entity.FinishedTime);
            db.AddInParameter(cmd, "@CanReturn", DbType.Int32, entity.CanReturn);
            db.AddInParameter(cmd, "@ReBackFee", DbType.Decimal, entity.ReBackFee);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="order">待更新的实体对象</param>
        public int UpdateOrder(OrderEntity entity)
        {
            string sql = @" UPDATE dbo.[Order] SET
                       [ReBackFee]=@ReBackFee,[CanReturn]=@CanReturn,[Code]=@Code,[OrderVisualCode]=@OrderVisualCode,[MemId]=@MemId,[IsStore]=@IsStore,[Status]=@Status,[CreateTime]=@CreateTime,[OrderType]=@OrderType,[TransFee]=@TransFee,[NeedDeliver]=@NeedDeliver,[Integral]=@Integral,[TotalPrice]=@TotalPrice,[TotalMarketPrice]=@TotalMarketPrice,[DealTime]=@DealTime,[DeliverTime]=@DeliverTime,[ReciveTime]=@ReciveTime,[MakeBill]=@MakeBill,[FinishedTime]=@FinishedTime
                       WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@OrderVisualCode", DbType.Int64, entity.OrderVisualCode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@IsStore", DbType.String, entity.IsStore);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@OrderType", DbType.Int32, entity.OrderType);
            db.AddInParameter(cmd, "@TransFee", DbType.Decimal, entity.TransFee);
            db.AddInParameter(cmd, "@NeedDeliver", DbType.Int32, entity.NeedDeliver);
            db.AddInParameter(cmd, "@Integral", DbType.Int32, entity.Integral);
            db.AddInParameter(cmd, "@TotalPrice", DbType.Decimal, entity.TotalPrice);
            db.AddInParameter(cmd, "@TotalMarketPrice", DbType.Decimal, entity.TotalMarketPrice);
            db.AddInParameter(cmd, "@DealTime", DbType.DateTime, entity.DealTime);
            db.AddInParameter(cmd, "@DeliverTime", DbType.DateTime, entity.DeliverTime);
            db.AddInParameter(cmd, "@ReciveTime", DbType.DateTime, entity.ReciveTime);
            db.AddInParameter(cmd, "@MakeBill", DbType.Int32, entity.MakeBill);
            db.AddInParameter(cmd, "@FinishedTime", DbType.DateTime, entity.FinishedTime);
            db.AddInParameter(cmd, "@CanReturn", DbType.Int32, entity.CanReturn);
            db.AddInParameter(cmd, "@ReBackFee", DbType.Decimal, entity.ReBackFee);

            return db.ExecuteNonQuery(cmd);
        }
        public int UpdateOrderStatus(long ordercode, OrderStatus oldstatus, OrderStatus newstatus)
        {
            string sql = @" UPDATE dbo.[Order] SET [Status]=@Status,DealTime=getdate() WHERE [Code]=@Code and Status=@OldStatus";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.String, ordercode);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)newstatus);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int32, (int)oldstatus);
            return db.ExecuteNonQuery(cmd);
        }
        public int OrderFinaceSubmit(string ordercode, int memid)
        {
            
            string sql = @"
UPDATE dbo.[Order] SET [FinancialStatus]=@FinancialStatus,FinancialCheckManId=@FinancialCheckManId,FinancialCheckTime=Getdate() WHERE [Code]=@Code and Status=@OldStatus
and FinancialStatus=@OldFiSatus;
UPDATE dbo.[Order] SET [Status]=@Status,DealTime=getdate() WHERE [Code]=@Code and Status=@OldStatus";
            //第二句等采购平台上线后交给供应商打印发货单时
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@FinancialStatus", DbType.Int32, (int)SuperMarket.Model.FinancialStatus.Checked);
            db.AddInParameter(cmd, "@OldFiSatus", DbType.Int32, (int)SuperMarket.Model.FinancialStatus.WaitChecked);
            db.AddInParameter(cmd, "@Code", DbType.String, ordercode);
            db.AddInParameter(cmd, "@FinancialCheckManId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)SuperMarket.Model.OrderStatus.WaitDeliver);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int32, (int)SuperMarket.Model.OrderStatus.WaitDeal);
            return db.ExecuteNonQuery(cmd);
        }

        public int OrderCancelXuQiu(long ordercode, string timecode, string reason, int memid, int oldstatus)
        {
            string sql = @" 
UPDATE dbo.[Order] SET  BeforeCancelStatus=[Status],[Status]=@Status,CancelTime=getdate(),ReasonBack=@ReasonBack
WHERE [Code]=@Code and Status=@OldStatus and MemId=@MemId and TimeStampCode=@TimeStampCode
 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ReasonBack", DbType.String, reason);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Cancel);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)oldstatus);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="memid"></param>
        /// <param name="_oldstatus"></param>
        /// <param name="_newstatus"></param>
        /// <returns></returns>
        public int OrderCancel(long ordercode, string timecode, string reason, int memid, int oldstatus)
        {
            string sql = @"
begin tran 
UPDATE dbo.[Order] SET  BeforeCancelStatus=[Status],[Status]=@Status,CancelTime=getdate(),ReasonBack=@ReasonBack
WHERE [Code]=@Code and Status=@OldStatus and MemId=@MemId and TimeStampCode=@TimeStampCode
DECLARE @integral INT 
DECLARE @memcouponid INT 
SELECT @integral=Integral,@memcouponid=MemCouponsId FROM dbo.[Order] WHERE code=@Code
 
if @integral is not null and @integral>0
begin

UPDATE  DBO.Integral set AvailableIntegral=AvailableIntegral+@integral   WHERE MemId=@MemId 
INSERT  INTO dbo.IntegralChange
                        ( MemId ,
                          ChangeNum ,
                          BalanceChangeNum ,
                          AvailableChangeNum ,
                          FreezingChangeNum ,
                          TotalChangeNum ,
                          ChangeType ,
                          ChangeReason ,
                          OrderCode ,
                          ChangeTime,
                          HasDeal 
                        )
                VALUES  ( @MemId , -- MemId - int
                          @integral  , -- ChangeNum - int
                          0 , -- BalanceChangeNum - int
                          @integral , -- AvailableChangeNum - int
                          0 , -- FreezingChangeNum - int
                          @integral , -- TotalChangeNum - int
                          5 , -- ChangeType - int取消订单退还
                          '取消订单退还 订单号' +  CAST(@Code AS VARCHAR) ,
                         @Code,
                         getdate(),
                         1
                        ) 
end
if @memcouponid is not null and @memcouponid>0
begin
  update  dbo.[MemCoupons] set Status=1 where id=@memcouponid and status=2  --1表示有效，2表示已使用
end

if @@error!=0
begin
   ROLLBACK tran
end
else 
begin
commit tran
end 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ReasonBack", DbType.String, reason);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Cancel);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)oldstatus);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }
       /// <summary>
        /// 取消订单。交钱后的取消
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="memid"></param>
        /// <param name="_oldstatus"></param>
        /// <param name="_newstatus"></param>
        /// <returns></returns>
        public int OrderCancel2(long ordercode, string timecode, string reason, int memid, int oldstatus)
        {
            string sql = @"IF EXISTS ( SELECT  1
            FROM    dbo.[Order]
            WHERE   [Code] = @Code
                    AND Status = @OldStatus
                    AND MemId = @MemId
                    AND FinancialStatus = @OldFinancialStatus
                    AND TimeStampCode = @TimeStampCode ) 
    BEGIN 
        BEGIN TRAN
        UPDATE  dbo.[Order]
        SET      BeforeCancelStatus=[Status],[Status] = @Status ,
                CancelTime = GETDATE() ,
                ReasonBack = @ReasonBack ,
                FinancialStatus = @FinancialStatus
        WHERE   [Code] = @Code
                AND Status = @OldStatus
                AND MemId = @MemId
                AND FinancialStatus = @OldFinancialStatus
                AND TimeStampCode = @TimeStampCode ;
   DECLARE @integral INT  
        SELECT  @integral = Integral 
        FROM    dbo.[Order]
        WHERE   code = @Code
        INSERT  INTO [FinanceRefund]
                ( [OrderCode] ,
                  [PayTradeNo] ,
                  [RebackFee] ,
                  ReBackIntegral ,ActReBackIntegral, 
                  [Description] ,
                  [CreateTime] ,
                  [Status] ,
                  IntegralStatus
                )
                SELECT  outtradeno ,
                        tradeno ,
                        totalfee ,
                        b.Integral , isnull(@integral,0), 
                        '取消订单退款' ,
                        GETDATE() ,
                        0 ,
                        1
                FROM    dbo.PayAliResultOrder a
                        INNER JOIN dbo.[Order] b ON a.outtradeno = b.Code
                WHERE   outtradeno = @Code
 
if @integral is not null and @integral>0
begin

UPDATE  DBO.Integral set AvailableIntegral=AvailableIntegral+@integral   WHERE MemId=@MemId 
INSERT  INTO dbo.IntegralChange
                        ( MemId ,
                          ChangeNum ,
                          BalanceChangeNum ,
                          AvailableChangeNum ,
                          FreezingChangeNum ,
                          TotalChangeNum ,
                          ChangeType ,
                          ChangeReason ,
                          OrderCode ,
                          ChangeTime,
                          HasDeal 
                        )
                VALUES  ( @MemId , -- MemId - int
                          @integral  , -- ChangeNum - int
                          0 , -- BalanceChangeNum - int
                          @integral , -- AvailableChangeNum - int
                          0 , -- FreezingChangeNum - int
                          @integral , -- TotalChangeNum - int
                          5 , -- ChangeType - int取消订单退还
                          '取消订单退还 订单号' +  CAST(@Code AS VARCHAR) ,
                         @Code,
                         getdate(),
                         1
                        ) 
end


        COMMIT TRAN
    END 


";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ReasonBack", DbType.String, reason);
            db.AddInParameter(cmd, "@FinancialStatus", DbType.Int32, (int)SuperMarket.Model.FinancialStatus.WaitReback);
            db.AddInParameter(cmd, "@OldFinancialStatus", DbType.Int32, (int)SuperMarket.Model.FinancialStatus.WaitChecked);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Cancel);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)oldstatus);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 取消订单。收款确认后取消
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="memid"></param>
        /// <param name="_oldstatus"></param>
        /// <param name="_newstatus"></param>
        /// <returns></returns>
        public int OrderCancel3(long ordercode, string timecode, string reason, int memid, int oldstatus)
        {
            string sql = @"IF EXISTS ( SELECT  1
            FROM    dbo.[Order]
            WHERE   [Code] = @Code
                    AND Status = @OldStatus
                    AND MemId = @MemId 
                    AND TimeStampCode = @TimeStampCode ) 
    BEGIN 
        BEGIN TRAN
        UPDATE  dbo.[Order]
        SET     BeforeCancelStatus=[Status],
[Status] = @Status , 
                CancelTime = GETDATE() ,
                ReasonBack = @ReasonBack  
        WHERE   [Code] = @Code
                AND Status = @OldStatus
                AND MemId = @MemId 
                AND TimeStampCode = @TimeStampCode ; 
        COMMIT TRAN
    END 


";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ReasonBack", DbType.String, reason);  
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.CancelApp);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)oldstatus);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 取消订单通过
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="timecode"></param>
        /// <returns></returns>
        public int OrderCancelCheckPass(long ordercode, string timecode)
        {
            string sql = @"IF EXISTS ( SELECT  1
            FROM    dbo.[Order]
            WHERE   [Code] = @Code
                    AND Status = @OldStatus 
                    AND TimeStampCode = @TimeStampCode ) 
    BEGIN 
        BEGIN TRAN
        UPDATE  dbo.[Order]
        SET     [Status] = @Status  
        WHERE   [Code] = @Code
                AND Status = @OldStatus 
                AND TimeStampCode = @TimeStampCode ;

        INSERT  INTO [FinanceRefund]
                ( [OrderCode] ,
                  [PayTradeNo] ,
                  [RebackFee] ,
                  ReBackIntegral ,
                  [Description] ,
                  [CreateTime] ,
                  [Status] ,
                  IntegralStatus
                )
                SELECT  outtradeno ,
                        tradeno ,
                        totalfee ,
                        b.Integral ,
                        '取消订单退款' ,
                        GETDATE() ,
                        0 ,
                        0
                FROM    dbo.PayAliResultOrder a
                        INNER JOIN dbo.[Order] b ON a.outtradeno = b.Code
                WHERE   outtradeno = @Code  
DECLARE @memcouponid int 
DECLARE @MemId int 
SELECT  @MemId=MemId,@memcouponid=MemCouponsId FROM dbo.[Order] WHERE code=@Code

if @memcouponid is not null and @memcouponid>0
begin
  update  dbo.[MemCoupons] set Status=1 where id=@memcouponid and status=2  --1表示有效，2表示已使用
end
   COMMIT TRAN
    END  
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);    
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Cancel); 
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)OrderStatus.CancelApp);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 拒绝取消订单
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="timecode"></param>
        /// <returns></returns>
        public int OrderCancelCheckReject(long ordercode, string timecode)
        {
            string sql = @"IF EXISTS ( SELECT  1
            FROM    dbo.[Order]
            WHERE   [Code] = @Code
                    AND Status = @OldStatus 
                    AND TimeStampCode = @TimeStampCode ) 
    BEGIN 
        BEGIN TRAN
        UPDATE  dbo.[Order]
        SET     [Status] =BeforeCancelStatus 
        WHERE   [Code] = @Code
                AND Status = @OldStatus 
                AND TimeStampCode = @TimeStampCode ;
 
        COMMIT TRAN
    END 


";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Cancel);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)OrderStatus.CancelApp);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }

        public int RebackIntegral(long ordercode,string timecode,int refundid,int rebackintegral,int memid)
        {
            string sql = @"IF EXISTS (SELECT  1 FROM    dbo.[Order] a inner join [FinanceRefund] b on a.Code=b.OrderCode
            WHERE   a.[Code] = @Code  AND a.TimeStampCode = @TimeStampCode and b.id=@RefundId  and Integral-ReBackFeeIntegral>=@integral
                    AND MemId = @MemId) 
    BEGIN 
        BEGIN TRAN
        UPDATE  dbo.[Order]
        SET    ReBackFeeIntegral=ReBackFeeIntegral+@integral
        WHERE   [Code] = @Code 
                AND TimeStampCode = @TimeStampCode ;
        update FinanceRefund set ActReBackIntegral=ActReBackIntegral+@integral,
IntegralStatus=@IntegralStatus WHERE Id=@RefundId 
       
                UPDATE  DBO.Integral
                SET     AvailableIntegral = AvailableIntegral + @integral
                WHERE   MemId = @MemId 
                INSERT  INTO dbo.IntegralChange
                        ( MemId ,
                          ChangeNum ,
                          BalanceChangeNum ,
                          AvailableChangeNum ,
                          FreezingChangeNum ,
                          TotalChangeNum ,
                          ChangeType ,
                          ChangeReason ,
                          ChangeTime
                        )
                VALUES  ( @MemId , -- MemId - int
                          @integral , -- ChangeNum - int
                          0 , -- BalanceChangeNum - int
                          @integral , -- AvailableChangeNum - int
                          0 , -- FreezingChangeNum - int
                          @integral * -1 , -- TotalChangeNum - int
                          1 , -- ChangeType - int
                          '取消订单退还 订单号' + CAST(@Code AS VARCHAR) ,
                          GETDATE()
                        ) 
         


        COMMIT TRAN
    END 


";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            db.AddInParameter(cmd, "@IntegralStatus", DbType.Int32, (int)SuperMarket.Model.ReBackIntegralStatus.HasReback); 
            db.AddInParameter(cmd, "@integral", DbType.Int32, rebackintegral); 
            db.AddInParameter(cmd, "@RefundId", DbType.Int32, refundid);
            db.AddInParameter(cmd, "@TimeStampCode", DbType.Binary, ByteUtils.GetByteFromString(timecode));
            return db.ExecuteNonQuery(cmd);
        }
        public int SendOrder(long ordercode)
        {
            string sql = @" UPDATE dbo.[Order] SET [Status]=@Status,DeliverTime=getdate() where Code=@Code 
and DeliverTime is null
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode); 
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.WaitRecive);
            return db.ExecuteNonQuery(cmd);
        }
        public int OrderRecived(long code,  int memid)
        {
            string sql = @" UPDATE dbo.[Order] SET [Status]=@Status,ReciveTime=getdate() where Code=@Code and Status=@OldStatus
and MemId=@MemId
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Finished);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)OrderStatus.WaitRecive); 
            return db.ExecuteNonQuery(cmd);
        }
        public int EditOrderCouponsNum(long code, int num)
        {
            string sql = @" UPDATE dbo.[Order] SET [ProvideCouponsNum]=@ProvideCouponsNum  where Code=@Code and ProvideCouponsNum=0 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code); 
            db.AddInParameter(cmd, "@ProvideCouponsNum", DbType.Int32, num);
            return db.ExecuteNonQuery(cmd);
        }
        public int OrderRecived(long code )
        {
            string sql = @" UPDATE dbo.[Order] SET [Status]=@Status,ReciveTime=getdate() where Code=@Code and Status=@OldStatus
 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code); 
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderStatus.Finished);
            db.AddInParameter(cmd, "@OldStatus", DbType.Int16, (int)OrderStatus.WaitRecive);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderByKey(int id)
        {
            string sql = @"delete from Order where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderDisabled()
        {
            string sql = @"delete from  Order  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int ActivateOrder(int id)
        {
            string sql = @"UPDATE DBO.[ORDER] SET CANRETURN=1 WHERE ID=@ID";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ID", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 设置订单不可退换货
        /// </summary>
        /// <returns></returns>
        public int SetCanReturnEqualsZero(long ordercode)
        {
            string sql = " Update dbo.[Order] set CanReturn=0 where Code=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            return db.ExecuteNonQuery(cmd);
        }
        


        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderByIds(string ids)
        {
            string sql = @"Delete from Order  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderByIds(string ids)
        {
            string sql = @"Update   Order set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }


        public int UpdateOrderByCode(Int64 code, int status,decimal reBackFee=0.00M)
        {
            string _setItem = string.Empty;
            if (reBackFee > 0)
            {
                _setItem = ",ReBackFee=@ReBackFee";
            }
            string sql = @"Update dbo.[Order] set [Status]=@Status "+ _setItem + " where [Code]=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (reBackFee>0)
            {
                db.AddInParameter(cmd, "@ReBackFee", DbType.Decimal, reBackFee);
            }
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code);
            return db.ExecuteNonQuery(cmd);
        }

        public IList<OrderEntity> GetNeedSyncOrder()
        {
            IList<OrderEntity> entityList = new List<OrderEntity>();
            string sql = @" SELECT TOP 10 Code into #temp FROM [dbo].[Order] where SyncCGStatus=@SyncCGStatus ORDER BY id ASC 
update a set SyncCGStatus=@SyncING from [dbo].[Order] a inner join  #temp b where a.Code=b.Code and a.SyncCGStatus=@SyncCGStatus
SELECT  [Id],[Code],[OrderVisualCode],[MemId],[IsStore],[MemLevel],[Status],[OrderType],TransFee,[NeedDeliver],[Integral]
      ,[PayPrice],[TotalPrice],[TotalMarketPrice],[PreDisCountPrice],[IntegralFee],[ActPrice],[ReBackFee],[CreateTime],[PayTime],[DealTime],[DeliverTime],[ReciveTime],[FinishedTime],[CancelTime],[MakeBill],[Remark],[CanReturn],[PayType],[ReasonBack],[TimeStampCode],[UpdateTime]
      ,[SyncCGStatus] from [dbo].[Order] a WITH(NOLOCK) inner join  #temp b  WITH(NOLOCK) ON A.Code=B.Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SyncCGStatus", DbType.Int32, (int)SyncCGStatus.WaitSync);
            db.AddInParameter(cmd, "@SyncING", DbType.Int64, (int)SyncCGStatus.Syncing);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderEntity entity = new OrderEntity(); 
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.PreDisCountPrice = StringUtils.GetDbDecimal(reader["PreDisCountPrice"]);
                    entity.IntegralFee = StringUtils.GetDbDecimal(reader["IntegralFee"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.CancelTime = StringUtils.GetDbDateTime(reader["CancelTime"]);
                    entity.ReasonBack = StringUtils.GetDbString(reader["ReasonBack"]);
                    entity.TimeStampCode = ByteUtils.GetStringFromByte(reader["TimeStampCode"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public int UpdateOrderByOrderId(int orderid, int status)
        {
            string sql = @"Update dbo.[Order] set [Status]=@Status where [Id]=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            db.AddInParameter(cmd, "@Id", DbType.String, orderid);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public OrderEntity GetOrder(int id)
        {
            string sql = @"SELECT  ,[CanReturn],[Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime]
							FROM
							dbo.[Order] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            OrderEntity entity = new OrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);

                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]);
                    entity.ReBackFee = StringUtils.GetDbDecimal(reader["ReBackFee"]);
                }
            }
            return entity;
        }
        public OrderEntity GetOrderByCode(long code)
        {
            string sql = @"SELECT  top 1 a.[Id],[Code]
                          ,[OrderVisualCode]
                          ,[MemId]
                          ,[IsStore]
                          ,[MemLevel] 
                          ,[Status]
                          ,[Status]
,FinancialStatus 
,FinancialCheckTime
                          ,[CreateTime]
                          ,[OrderType]
                          ,[TransFee]
                          ,[NeedDeliver]
                          ,[Integral]
,ReBackFeeIntegral
                          ,[ActPrice]
                          ,[PayPrice]
                          ,[TotalPrice]
                          ,[TotalMarketPrice]
                          ,[PreDisCountPrice]
                          ,[IntegralFee]
                          ,[DealTime]
                          ,[DeliverTime]
                          ,[ReciveTime]
                          ,[MakeBill]
                          ,[FinishedTime]
                          ,[Remark]
                          ,[PayType],CancelTime,ReasonBack,TimeStampCode,BeforeCancelStatus,OrderStyle 	FROM
						   dbo.[Order] a WITH(NOLOCK) 
						   WHERE a.[Code]=@Code order by a.id DESC";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, code);
            OrderEntity entity = new OrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.FinancialStatus = StringUtils.GetDbInt(reader["FinancialStatus"]); 
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.ReBackFeeIntegral = StringUtils.GetDbInt(reader["ReBackFeeIntegral"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.PreDisCountPrice = StringUtils.GetDbDecimal(reader["PreDisCountPrice"]);
                    entity.IntegralFee = StringUtils.GetDbDecimal(reader["IntegralFee"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.CancelTime = StringUtils.GetDbDateTime(reader["CancelTime"]);
                    entity.ReasonBack = StringUtils.GetDbString(reader["ReasonBack"]);  
                    entity.BeforeCancelStatus = StringUtils.GetDbInt(reader["BeforeCancelStatus"]);
                    entity.TimeStampCode = ByteUtils.GetStringFromByte(reader["TimeStampCode"]);
                    entity.OrderStyle = StringUtils.GetDbInt(reader["OrderStyle"]);
                    
                }
            }
            return entity;
        }
        public VWOrderEntity GetVWOrderByCode(Int64 code)
        {
            string sql = @"SELECT  top 1 a.[Id],[Code]
                          ,[OrderVisualCode]
                          ,[MemId]
                          ,[IsStore]
                          ,[MemLevel] 
                          ,a.[Status]
                          ,a.[CreateTime]
                          ,[OrderType]
                          ,[TransFee]
                          ,[NeedDeliver]
                          ,[Integral]
                          ,[ActPrice]
                          ,[PayPrice]
                          ,[TotalPrice]
                          ,[TotalMarketPrice]
                          ,[PreDisCountPrice]
                          ,DisCountFee
                          ,CouponsFee
                          ,MemCouponsId
                          ,[IntegralFee]
                          ,PayTime
                          ,[DealTime]
                          ,[DeliverTime]
                          ,[ReciveTime]
                          ,[MakeBill]
                          ,[FinishedTime]
                          ,[Remark]
                          ,[PayType],
                          b.AccepterName,
                          b.ProvinceId AS AccepterProvinceId,b.CityId AS AccepterCityId,b.Address AS AccepterAddress,b.MobilePhone AS AccepterPhone,b.Telephone,
                          c.BillType,c.CompanyName AS BillName,c.CompanyAddress AS BillAddress,c.CompanyPhone
                          AS BillPhone,c.CompanyBank AS BillBank,c.BankAccount AS BillAccount,a.PayConfirmCode,a.CanReturn	FROM
						   dbo.[Order] a WITH(NOLOCK)	INNER JOIN 
						    dbo.OrderAddress b WITH(NOLOCK) ON a.Code=b.OrderCode inner join 
						    dbo.OrderBillBasic c WITH(NOLOCK) ON a.code=c.OrderCode 
						   WHERE a.[Code]=@Code order by a.id DESC";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, code);
            VWOrderEntity entity = new VWOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.PayConfirmCode = StringUtils.GetDbString(reader["PayConfirmCode"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);  
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]); 
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.PreDisCountPrice = StringUtils.GetDbDecimal(reader["PreDisCountPrice"]);
                    entity.DisCountFee = StringUtils.GetDbDecimal(reader["DisCountFee"]);
                    entity.CouponsFee = StringUtils.GetDbDecimal(reader["CouponsFee"]); 
                    entity.IntegralFee = StringUtils.GetDbDecimal(reader["IntegralFee"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.AccepterProvinceId = StringUtils.GetDbInt(reader["AccepterProvinceId"]);
                    entity.AccepterCityId = StringUtils.GetDbInt(reader["AccepterCityId"]);
                    entity.AccepterAddress = StringUtils.GetDbString(reader["AccepterAddress"]);
                    entity.AccepterPhone = StringUtils.GetDbString(reader["AccepterPhone"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.BillType = StringUtils.GetDbInt(reader["BillType"]);
                    entity.BillName = StringUtils.GetDbString(reader["BillName"]);
                    entity.BillAddress = StringUtils.GetDbString(reader["BillAddress"]);
                    entity.BillPhone = StringUtils.GetDbString(reader["BillPhone"]);
                    entity.BillBank = StringUtils.GetDbString(reader["BillBank"]);
                    entity.BillAccount = StringUtils.GetDbString(reader["BillAccount"]); 
                    entity.MemCouponsId = StringUtils.GetDbInt(reader["MemCouponsId"]);  
                }
            }
            return entity;
        }
        public VWOrderEntity GetVWOrderByCode(Int64 code, int memid)
        {
            string sql = @"SELECT  top 1 a.[Id],[Code]
                          ,[OrderVisualCode]
                          ,[MemId]
                          ,[IsStore]
                          ,[MemLevel] 
                          ,a.[Status]
                          ,a.[CreateTime]
                          ,[OrderType]
                          ,[TransFee]
                          ,[NeedDeliver]
                          ,[Integral]
                          ,[ActPrice]
                          ,[PayPrice]
                          ,[TotalPrice]
                          ,[TotalMarketPrice]
                          ,[PreDisCountPrice]
                          ,DisCountFee
                          ,CouponsFee
                          ,[IntegralFee]
                          ,a.ExpressCom
                          ,PayTime
                          ,[DealTime]
                          ,[DeliverTime]
                          ,[ReciveTime],CancelTime
                          ,[MakeBill]
                          ,[FinishedTime]
                          ,[Remark]
                          ,[PayType],
                          b.AccepterName,
                          b.ProvinceId AS AccepterProvinceId,b.CityId AS AccepterCityId,b.Address AS AccepterAddress,b.MobilePhone AS AccepterPhone,b.Telephone,
                          c.BillType,c.CompanyName AS BillName,c.CompanyAddress AS BillAddress,c.CompanyPhone
                          AS BillPhone,c.CompanyBank AS BillBank,c.BankAccount AS BillAccount,a.CanReturn,a.MemCouponsId,a.OrderStyle	FROM
						   dbo.[Order] a WITH(NOLOCK)	INNER JOIN 
						    dbo.OrderAddress b WITH(NOLOCK) ON a.Code=b.OrderCode inner join 
						    dbo.OrderBillBasic c WITH(NOLOCK) ON a.code=c.OrderCode 
						   WHERE a.[Code]=@Code And a.MemId=@MemId order by a.id DESC";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, code);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            VWOrderEntity entity = new VWOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.PreDisCountPrice = StringUtils.GetDbDecimal(reader["PreDisCountPrice"]);
                    entity.DisCountFee = StringUtils.GetDbDecimal(reader["DisCountFee"]);  
                    entity.CouponsFee = StringUtils.GetDbDecimal(reader["CouponsFee"]); 
                    entity.IntegralFee = StringUtils.GetDbDecimal(reader["IntegralFee"]);
                    entity.PayTime = StringUtils.GetDbDateTime(reader["PayTime"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]); 
                    entity.CancelTime = StringUtils.GetDbDateTime(reader["CancelTime"]); 
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.AccepterProvinceId = StringUtils.GetDbInt(reader["AccepterProvinceId"]);
                    entity.AccepterCityId = StringUtils.GetDbInt(reader["AccepterCityId"]);
                    entity.AccepterAddress = StringUtils.GetDbString(reader["AccepterAddress"]);
                    entity.AccepterPhone = StringUtils.GetDbString(reader["AccepterPhone"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.BillType = StringUtils.GetDbInt(reader["BillType"]);
                    entity.BillName = StringUtils.GetDbString(reader["BillName"]);
                    entity.BillAddress = StringUtils.GetDbString(reader["BillAddress"]);
                    entity.BillPhone = StringUtils.GetDbString(reader["BillPhone"]);
                    entity.BillBank = StringUtils.GetDbString(reader["BillBank"]);
                    entity.BillAccount = StringUtils.GetDbString(reader["BillAccount"]);  
                    entity.ExpressCom = StringUtils.GetDbInt(reader["ExpressCom"]);
                    entity.MemCouponsId = StringUtils.GetDbInt(reader["MemCouponsId"]);
                    entity.OrderStyle = StringUtils.GetDbInt(reader["OrderStyle"]);
                }
            }
            return entity;
        }

        public int PayFinished(long ordercode, AssetReChargeEntity _asset)
        {
            string sql = @"EXEC [Proc_PayFinished]     @OrderCode,
    @Amt,@MemId,@PayType,@BankCode  ,@CardNo ,@TranSerialNum ,@CreateTime ,@IpAddress ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            db.AddInParameter(cmd, "@Amt", DbType.Decimal, _asset.Amt);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, _asset.MemId);
            db.AddInParameter(cmd, "@PayType", DbType.Int16, _asset.PayType);
            db.AddInParameter(cmd, "@BankCode", DbType.String, _asset.BankCode);
            db.AddInParameter(cmd, "@CardNo", DbType.String, _asset.CardNo);
            db.AddInParameter(cmd, "@TranSerialNum", DbType.String, _asset.TranSerialNum);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, _asset.CreateTime);
            db.AddInParameter(cmd, "@IpAddress", DbType.String, _asset.IpAddress);

            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }

        public int PayFinishedForOrder(long ordercode,decimal amt)
        {
            string sql = @"EXEC [Proc_PayFinishedForOrder]     @OrderCode,@Amt ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode); 
            db.AddInParameter(cmd, "@Amt", DbType.Decimal, amt);

            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }

        public int PayMethodUpdate(long ordercode, int memid, int paymethod)
        {
            string sql = @" Update dbo.[Order] set PayType=@PayType where Code=@Code and MemId=@MemId and Status=@WaitPayStatus";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PayType", DbType.Int32, paymethod);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@WaitPayStatus", DbType.Int32, (int)OrderStatus.WaitPay);
            db.AddInParameter(cmd, "@Code", DbType.Int64, ordercode);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 更新退款金额
        /// </summary>
        /// <returns></returns>
        public int UpdateOrderRebackFee(long orderCode,decimal rebackFee)
        {
            string sql = @" Update dbo.[Order] set RebackFee=RebackFee+@RebackFee where Code=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@RebackFee", DbType.Decimal, rebackFee);
            db.AddInParameter(cmd, "@Code", DbType.Int64, orderCode);
            return db.ExecuteNonQuery(cmd);
        }

        public string CreateOrder(VWOrderEntity order, OrderAddressEntity _address, OrderBillBasicEntity _billentity)
        {
            string preorderdetailsstr = "";
            foreach (VWOrderDetailEntity entity in order.Details)
            {
                preorderdetailsstr += "|" + entity.OrderDetailId;
            }
            preorderdetailsstr = preorderdetailsstr.TrimStart('|');
            string sql = @"EXEC [Proc_CreateOrder]   
                            @Code   ,
                            @OrderVisualCode,
                            @CreateTime   , 
                            @IntegralFee   ,
                            @IsStore   ,
                            @MemId   ,
                            @Integral   ,
                            @MemLevel   ,
                            @NeedDeliver   ,
                            @OrderType   ,
                            @ActPrice   ,
                            @PayPrice   ,
                            @PayType   ,
                            @ExpressCom,
                            @TransFee   ,
                            @PreDisCountPrice,
                            @DisCountFee,
                            @PreOrderCode ,
                            @PreOrderDetailIds ,
                            @Remark   , 
                            @Status   ,
                            @TotalMarketPrice   ,
                            @TotalPrice   ,
                            @AccepterName  ,
                            @Address   ,
                            @CityId   ,
                            @ProvinceId  ,
                            @MobilePhone ,
                            @BillId, 
                            @BillType, 
                            @BillBankAccount, 
                            @BillCompanyAddress,
                            @BillCompanyBank,
                            @BillCompanyCode,
                            @BillCompanyName,
                            @BillCompanyPhone,
                            @BillReceiverAddress,
                            @BillReceiverCity,
                            @BillReceiverName,
                            @BillReceiverPhone,
                            @BillReceiverProvince,
                            @BillTitle,
                            @Status,
                            @PayConfirmCode,
                            @CouponsFee,
                            @MemCouponsId,
                            @OrderStyle
";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, order.Code);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, order.CreateTime);
            db.AddInParameter(cmd, "@IntegralFee", DbType.Decimal, order.IntegralFee);
            db.AddInParameter(cmd, "@IsStore", DbType.Int16, order.IsStore);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, order.MemId);
            db.AddInParameter(cmd, "@Integral", DbType.Int32, order.Integral);
            db.AddInParameter(cmd, "@MemLevel", DbType.Int32, order.MemLevel);
            db.AddInParameter(cmd, "@NeedDeliver", DbType.Int32, order.NeedDeliver);
            db.AddInParameter(cmd, "@OrderType", DbType.Int32, order.OrderType);
            db.AddInParameter(cmd, "@OrderVisualCode", DbType.Int64, order.OrderVisualCode);
            db.AddInParameter(cmd, "@ActPrice", DbType.Decimal, order.ActPrice);
            db.AddInParameter(cmd, "@PayPrice", DbType.Decimal, order.PayPrice);
            db.AddInParameter(cmd, "@PayType", DbType.Int32, order.PayType);
            db.AddInParameter(cmd, "@ExpressCom", DbType.Int32, order.ExpressCom);
            db.AddInParameter(cmd, "@TransFee", DbType.Decimal, order.TransFee);
            db.AddInParameter(cmd, "@PreDisCountPrice", DbType.Decimal, order.PreDisCountPrice);
            db.AddInParameter(cmd, "@DisCountFee", DbType.Decimal, order.DisCountFee);
            db.AddInParameter(cmd, "@PreOrderCode", DbType.Int64, order.PreOrderCode);
            db.AddInParameter(cmd, "@PreOrderDetailIds", DbType.String, preorderdetailsstr);
            db.AddInParameter(cmd, "@Remark", DbType.String, order.Remark);
            db.AddInParameter(cmd, "@Status", DbType.Int32, order.Status);
            db.AddInParameter(cmd, "@TotalMarketPrice", DbType.Decimal, order.TotalMarketPrice);
            db.AddInParameter(cmd, "@TotalPrice", DbType.Decimal, order.TotalPrice);
            db.AddInParameter(cmd, "@AccepterName", DbType.String, _address.AccepterName);
            db.AddInParameter(cmd, "@Address", DbType.String, _address.Address);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, _address.CityId);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, _address.ProvinceId);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, _address.MobilePhone);
            db.AddInParameter(cmd, "@BillId", DbType.Int32, _billentity.BillId);
            db.AddInParameter(cmd, "@BillType", DbType.Int32, _billentity.BillType);
            db.AddInParameter(cmd, "@BillBankAccount", DbType.String, _billentity.BankAccount);
            db.AddInParameter(cmd, "@BillCompanyAddress", DbType.String, _billentity.CompanyAddress);
            db.AddInParameter(cmd, "@BillCompanyBank", DbType.String, _billentity.CompanyBank);
            db.AddInParameter(cmd, "@BillCompanyCode", DbType.String, _billentity.CompanyCode);
            db.AddInParameter(cmd, "@BillCompanyName", DbType.String, _billentity.CompanyName);
            db.AddInParameter(cmd, "@BillCompanyPhone", DbType.String, _billentity.CompanyPhone);
            db.AddInParameter(cmd, "@BillReceiverAddress", DbType.String, _billentity.ReceiverAddress);
            db.AddInParameter(cmd, "@BillReceiverCity", DbType.Int32, _billentity.ReceiverCity);
            db.AddInParameter(cmd, "@BillReceiverName", DbType.String, _billentity.ReceiverName);
            db.AddInParameter(cmd, "@BillReceiverPhone", DbType.String, _billentity.ReceiverPhone);
            db.AddInParameter(cmd, "@BillReceiverProvince", DbType.Int32, _billentity.ReceiverProvince);
            db.AddInParameter(cmd, "@BillTitle", DbType.String, _billentity.Title);
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, _billentity.Status);
            db.AddInParameter(cmd, "@PayConfirmCode", DbType.String, order.PayConfirmCode);
            db.AddInParameter(cmd, "@CouponsFee", DbType.Decimal, order.CouponsFee);
            db.AddInParameter(cmd, "@MemCouponsId", DbType.Int32, order.MemCouponsId);
            db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, order.OrderStyle);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return "";
            return identity.ToString();

        }
        public string CreateOrderXuQiu(VWOrderEntity order, OrderAddressEntity _address, OrderBillBasicEntity _billentity)
        {
            string preorderdetailsstr = "";
            foreach (VWOrderDetailEntity entity in order.Details)
            {
                preorderdetailsstr += "|" + entity.OrderDetailId;
            }
            preorderdetailsstr= preorderdetailsstr.TrimStart('|');
            string sql = @"EXEC [Proc_CreateOrderXuQiu]   
                            @Code   ,
                            @OrderVisualCode   ,
                            @CreateTime   , 
                            @IntegralFee   ,
                            @IsStore   ,
                            @MemId   ,
                            @Integral   ,
                            @MemLevel   ,
                            @NeedDeliver   ,
                            @OrderType   ,
                            @ActPrice   ,
                            @PayPrice   ,
                            @PayType   ,
                            @ExpressCom,
                            @TransFee   ,
                            @PreDisCountPrice,
                            @DisCountFee,
                            @PreOrderCode ,
                            @PreOrderDetailIds ,
                            @Remark   , 
                            @Status   ,
                            @TotalMarketPrice   ,
                            @TotalPrice   ,
                            @AccepterName  ,
                            @Address   ,
                            @CityId   ,
                            @ProvinceId  ,
                            @MobilePhone ,
                            @BillId, 
                            @BillType, 
                            @BillBankAccount, 
                            @BillCompanyAddress,
                            @BillCompanyBank,
                            @BillCompanyCode,
                            @BillCompanyName,
                            @BillCompanyPhone,
                            @BillReceiverAddress,
                            @BillReceiverCity,
                            @BillReceiverName,
                            @BillReceiverPhone,
                            @BillReceiverProvince,
                            @BillTitle,
                            @Status,
                            @PayConfirmCode,
                            @CouponsFee,
                            @MemCouponsId,
                            @OrderStyle
";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.Int64, order.Code);
            db.AddInParameter(cmd, "@OrderVisualCode", DbType.Int64, order.OrderVisualCode);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, order.CreateTime);
            db.AddInParameter(cmd, "@IntegralFee", DbType.Decimal, order.IntegralFee);
            db.AddInParameter(cmd, "@IsStore", DbType.Int16, order.IsStore);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, order.MemId);
            db.AddInParameter(cmd, "@Integral", DbType.Int32, order.Integral);
            db.AddInParameter(cmd, "@MemLevel", DbType.Int32, order.MemLevel);
            db.AddInParameter(cmd, "@NeedDeliver", DbType.Int32, order.NeedDeliver);
            db.AddInParameter(cmd, "@OrderType", DbType.Int32, order.OrderType);
            db.AddInParameter(cmd, "@ActPrice", DbType.Decimal, order.ActPrice);
            db.AddInParameter(cmd, "@PayPrice", DbType.Decimal, order.PayPrice);
            db.AddInParameter(cmd, "@PayType", DbType.Int32, order.PayType);
            db.AddInParameter(cmd, "@ExpressCom", DbType.Int32, order.ExpressCom);
            db.AddInParameter(cmd, "@TransFee", DbType.Decimal, order.TransFee);
            db.AddInParameter(cmd, "@PreDisCountPrice", DbType.Decimal, order.PreDisCountPrice);
            db.AddInParameter(cmd, "@DisCountFee", DbType.Decimal, order.DisCountFee);
            db.AddInParameter(cmd, "@PreOrderCode", DbType.Int64, order.PreOrderCode);
            db.AddInParameter(cmd, "@PreOrderDetailIds", DbType.String, preorderdetailsstr);
            db.AddInParameter(cmd, "@Remark", DbType.String, order.Remark);
            db.AddInParameter(cmd, "@Status", DbType.Int32, order.Status);
            db.AddInParameter(cmd, "@TotalMarketPrice", DbType.Decimal, order.TotalMarketPrice);
            db.AddInParameter(cmd, "@TotalPrice", DbType.Decimal, order.TotalPrice);
            db.AddInParameter(cmd, "@AccepterName", DbType.String, _address.AccepterName);
            db.AddInParameter(cmd, "@Address", DbType.String, _address.Address);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, _address.CityId);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, _address.ProvinceId);
            db.AddInParameter(cmd, "@MobilePhone", DbType.String, _address.MobilePhone);
            db.AddInParameter(cmd, "@BillId", DbType.Int32, _billentity.BillId);
            db.AddInParameter(cmd, "@BillType", DbType.Int32, _billentity.BillType);
            db.AddInParameter(cmd, "@BillBankAccount", DbType.String, _billentity.BankAccount);
            db.AddInParameter(cmd, "@BillCompanyAddress", DbType.String, _billentity.CompanyAddress);
            db.AddInParameter(cmd, "@BillCompanyBank", DbType.String, _billentity.CompanyBank);
            db.AddInParameter(cmd, "@BillCompanyCode", DbType.String, _billentity.CompanyCode);
            db.AddInParameter(cmd, "@BillCompanyName", DbType.String, _billentity.CompanyName);
            db.AddInParameter(cmd, "@BillCompanyPhone", DbType.String, _billentity.CompanyPhone);
            db.AddInParameter(cmd, "@BillReceiverAddress", DbType.String, _billentity.ReceiverAddress);
            db.AddInParameter(cmd, "@BillReceiverCity", DbType.Int32, _billentity.ReceiverCity);
            db.AddInParameter(cmd, "@BillReceiverName", DbType.String, _billentity.ReceiverName);
            db.AddInParameter(cmd, "@BillReceiverPhone", DbType.String, _billentity.ReceiverPhone);
            db.AddInParameter(cmd, "@BillReceiverProvince", DbType.Int32, _billentity.ReceiverProvince);
            db.AddInParameter(cmd, "@BillTitle", DbType.String, _billentity.Title);
            db.AddInParameter(cmd, "@BillStatus", DbType.Int32, _billentity.Status);
            db.AddInParameter(cmd, "@PayConfirmCode", DbType.String, order.PayConfirmCode);
            db.AddInParameter(cmd, "@CouponsFee", DbType.Decimal, order.CouponsFee);
            db.AddInParameter(cmd, "@MemCouponsId", DbType.Int32, order.MemCouponsId);
            db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, order.OrderStyle);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return "";
            return identity.ToString();

        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<OrderEntity> GetOrderList(int pagesize, int pageindex, ref int recordCount)
        {
            string sql = @"SELECT   [Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime] from dbo.[Order] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Order] with (nolock) ";
            IList<OrderEntity> entityList = new List<OrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderEntity entity = new OrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);

                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
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

        public IList<VWOrderEntity> GetOrdersByMemIdTop3(int memid)
        {
            string sql = @" SELECT  top 3 	b.Code,b.Id, b.Status,b.ActPrice,b.PayType,b.CreateTime,b.MemId,b.CanReturn,
c.AccepterName from  dbo.[Order] b WITH(NOLOCK) 
inner join OrderAddress c  WITH(NOLOCK)  on b.Code=c.OrderCode
    WHERE b.MemId=@MemId  order by  id desc ";

            IList<VWOrderEntity> entityList = new List<VWOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWOrderEntity entity = new VWOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);  
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]); 
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.CreateTime = StringUtils.GetDateTime(reader["CreateTime"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.PayType = StringUtils.GetDbInt(reader["PayType"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }


        public decimal GetPriceByVisualCode(long visualcode,int memid)
        {
            decimal amount = 0;
            string where = " where 1=1 ";
            if(visualcode>0)
            {
                where += " and  OrderVisualCode=@OrderVisualCode "; 
            }
            if(memid!=-1)
            {
                where += " and  MemId=@MemId ";
            }
            string sql  = " select SUM(ActPrice) from dbo.[Order] a   "+where;

            DbCommand cmd = db.GetSqlStringCommand(sql );
            if (visualcode > 0)
            { 
                db.AddInParameter(cmd, "@OrderVisualCode", DbType.Int64, visualcode);
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            }
             
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    amount = StringUtils.GetDbDecimal(reader[0]);
                }
                else
                {
                    amount = 0;
                }
            }
            return amount;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<OrderEntity> GetOrderList(int pagesize, int pageindex, ref int recordCount, string keyword, int status, int term, int memid)
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

            if (term >= 0)
            {
                switch (term)
                {
                    case (int)OrderTerm.Default:
                        where += " and CreateTime > DATEADD(month, -3,GETDATE())  ";
                        break;
                    case (int)OrderTerm.YearThis:
                        where += " and  year(CreateTime)=year(GETDATE())  ";
                        break;
                    case (int)OrderTerm.YearLast:
                        where += " and  year(CreateTime) =  year(GETDATE())-1  ";
                        break;
                    case (int)OrderTerm.YearPrevious:
                        where += " and  year(CreateTime) =  year(GETDATE())-2  ";
                        break;
                }
            }


            string sql = @"SELECT [Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[MakeBill],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[MakeBill],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice] from dbo.[Order] WITH(NOLOCK)	
						" + where + @") as temp 
						where ROWNUMBER BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Order] with (nolock) " + where;

            IList<OrderEntity> entityList = new List<OrderEntity>();
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
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderEntity entity = new OrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entityList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + keyword + "%");
            }
            if (status >= 0 && status != (int)OrderStatus.AllStatus)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
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
        public IList<VWOrderEntity> GetVWOrderList(int pagesize, int pageindex, ref int recordCount, string keyword, int status, int term, int memid,int isreturn,long orderCode,long  visualordercode,int orderstyle)
        {
            string where = " WHERE  a.MemId=@MemId ";

            if (orderCode>0      )
            {
                where += " AND a.code = @OrderCode";
            }
            if (visualordercode > 0)
            {
                where += " AND a.OrderVisualCode = @OrderVisualCode";
            }
            
            if (!string.IsNullOrEmpty(keyword))
            {
                where += " AND (a.code like @KeyWord OR a.OrderVisualCode like @KeyWord )";
            } 
            if (isreturn == 1)
            {
                where += "  and a.CanReturn=1 and A.Status in (4,5) ";
            }
            else
            {
                
            }
            if (status == -1)
            {
                where += " and A.Status in (2,3,4,5) ";
            }
            else if (status > 0)
            {
                where += " and A.Status = @Status ";
            }
            if (term >= 0)
            {
                switch (term)
                {
                    case (int)OrderTerm.Default:
                        where += " and  a.CreateTime > DATEADD(month, -3,GETDATE())  ";
                        break;
                    case (int)OrderTerm.YearThis:
                        where += " and  year(a.CreateTime) = year(GETDATE()) ";
                        break;
                    case (int)OrderTerm.YearLast:
                        where += " and  year(a.CreateTime) =  year(GETDATE())-1  ";
                        break;
                    case (int)OrderTerm.YearPrevious:
                        where += " and  year(a.CreateTime) =  year(GETDATE())-2  ";
                        break;
                }
            }
            if (orderstyle != -1)
            {
                where += " and A.OrderStyle=@OrderStyle ";
            }


            string sql = @"SELECT   [MakeBill],[Id],[Code],OrderVisualCode,[CreateTime], [OrderType],[Status],CanReturn,FinancialStatus,TransFee,Integral,PayPrice,TotalPrice,ActPrice,AccepterName,MemId,OrderStyle
                           FROM
                          (SELECT ROW_NUMBER() OVER (Order By a.id desc) as ROWNUMBER,a.[MakeBill],a.[Id],a.[Code],a.OrderVisualCode,[CreateTime], [OrderType],[Status],CanReturn,FinancialStatus,A.TransFee,Integral,PayPrice,A.TotalPrice,ActPrice 
                          ,b.AccepterName,a.MemId,a.OrderStyle
                           from dbo.[Order] a WITH(nolock)  INNER JOIN dbo.OrderAddress b  WITH(nolock) ON a.Code=b.OrderCode " + where + @") as temp 
						   where ROWNUMBER BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize ";

            string sql2 = @"Select count(A.Code) from dbo.[Order] a with (nolock) INNER JOIN dbo.OrderAddress b  WITH(nolock) ON a.Code=b.OrderCode " + where;

            IList<VWOrderEntity> orderList = new List<VWOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            if (orderCode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, orderCode);
            }
            if (visualordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, visualordercode); 
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@KeyWord", DbType.String, "%" + keyword + "%");
            }
            if (status >0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (orderstyle != -1)
            { 
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWOrderEntity entity = new VWOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);  
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]);
                    entity.FinancialStatus = StringUtils.GetDbInt(reader["FinancialStatus"]); 
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.OrderStyle = StringUtils.GetDbInt(reader["OrderStyle"]);
                    orderList.Add(entity);
                }
            }

            cmd = db.GetSqlStringCommand(sql2);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            if (orderCode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, orderCode);
            }
            if (visualordercode > 0)
            {
                db.AddInParameter(cmd, "@OrderCode", DbType.Int64, visualordercode);
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@KeyWord", DbType.String, "%" + keyword + "%");
            }

            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (orderstyle != -1)
            {
                db.AddInParameter(cmd, "@OrderStyle", DbType.Int32, orderstyle);
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

            return orderList;
        }

        public VWOrderMsgEntity GetOrderMsgByMemId(int memid)
        {
            VWOrderMsgEntity _entity = new VWOrderMsgEntity();

            _entity.MemId = memid;
            _entity.CancelNum = 0;
            _entity.WaitPayNum = 0;
            _entity.WaitReciveNum = 0;
            _entity.WaitDeliverNum = 0;
            _entity.WaitCommentNum = 0;

            string sql = @"SELECT  Status,COUNT(1) as Num FROM dbo.[Order] a WITH(nolock)  WHERE MemId=@MemId GROUP BY Status ";

            IList<OrderEntity> entityList = new List<OrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    if (StringUtils.GetDbInt(reader["Status"]) == (int)OrderStatus.WaitPay)
                    {
                        _entity.WaitPayNum = StringUtils.GetDbInt(reader["Num"]);
                    }
                    else if (StringUtils.GetDbInt(reader["Status"]) == (int)OrderStatus.WaitRecive)
                    {
                        _entity.WaitReciveNum = StringUtils.GetDbInt(reader["Num"]);
                    }
                    else if (StringUtils.GetDbInt(reader["Status"]) == (int)OrderStatus.Cancel)
                    {
                        _entity.CancelNum = StringUtils.GetDbInt(reader["Num"]);
                    }
                }
            }

            sql = @"SELECT  COUNT(1) as Num FROM dbo.[Order] a WITH(nolock) inner join  
                    dbo.OrderDetail b  WITH(nolock) ON a.Code=b.OrderCode
                    WHERE a.MemId=@MemId AND a.Status=@Status AND b.HasComment=0  ";

            DbCommand cmd2 = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd2, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd2, "@Status", DbType.Int32, (int)OrderStatus.Finished);
            using (IDataReader reader = db.ExecuteReader(cmd2))
            {
                if (reader.Read())
                {
                    _entity.WaitCommentNum = StringUtils.GetDbInt(reader["Num"]);

                }
            }
            return _entity;
        }

        public IList<OrderEntity> GetOrderListByStatus(int pagesize, int pageindex, ref int recordCount, int status)
        {
            string sql = @"SELECT   [Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[OrderVisualCode],[MemId],[MemLevel],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime] from dbo.[Order] WITH(NOLOCK)	
						WHERE  Status=@Status ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[Order] with (nolock) ";
            IList<OrderEntity> entityList = new List<OrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderEntity entity = new OrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);

                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
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
        public IList<OrderEntity> GetOrderAll()
        {

            string sql = @"SELECT    [Id],[Code],[OrderVisualCode],[MemId],[IsStore],[Status],[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],[DeliverTime],[ReciveTime],[MakeBill],[FinishedTime] from dbo.[Order] WITH(NOLOCK)	";
            IList<OrderEntity> entityList = new List<OrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderEntity entity = new OrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);

                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
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
        public int ExistNum(OrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Order] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "  (IsStore=@IsStore) ";

            }
            else
            {
                where = where + " id<>@Id and  (IsStore=@IsStore) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }

            db.AddInParameter(cmd, "@IsStore", DbType.String, entity.IsStore);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        #endregion
        #endregion


        public IList<VWOrderDetailEntity> GetVWOrderDetailList(int pagesize, int pageindex, ref int recordCount, int memid, string keyword, int term, int hascomment)
        {
            string where = " WHERE a.MemId=@MemId ";

            if (!string.IsNullOrEmpty(keyword))
            {
                where += " AND a.Code like @Code";
            }

            if (term != -2)
            {
                switch (term)
                {
                    case (int)OrderTerm.OneMonth:
                        {
                            where += " AND CreateTime > DATEADD(Month,-1,GETDATE()) ";
                            break;
                        }
                    case (int)OrderTerm.Default:
                        {
                            where += " AND CreateTime > DATEADD(Month,-3,GETDATE()) ";
                            break;
                        }
                    case (int)OrderTerm.YearThis:
                        {
                            where += " AND  year(CreateTime) = year(GETDATE()) ";
                            break;
                        }
                    case (int)OrderTerm.YearLast:
                        {
                            where += " AND year(CreateTime) = year(GETDATE())-1 ";
                            break;
                        }
                    case (int)OrderTerm.YearPrevious:
                        {
                            where += " AND year(CreateTime) = year(GETDATE())-2 ";
                            break;
                        }
                }
            }

            if (hascomment != -1)
            {
                where += " AND b.HasComment=@HasComment ";
            }


            string sql = @"SELECT [Id],[OrderCode],[ProductName] ,[Num] ,[AccepterName] ,[TotalPrice] ,[ProductId],ProductDetailId, [Spec1], [Spec2], [Spec3]
                         FROM
                        (SELECT ROW_NUMBER() Over(Order by a.Id) as ROWNUMBER ,b.[Id],b.[OrderCode],b.[ProductName] ,b.[Num] ,c.[AccepterName] ,b.[TotalPrice],b.[ProductId],b.ProductDetailId,b.[Spec1],b.[Spec2],b.[Spec3] from dbo.[Order] a left join dbo.[OrderDetail] b on a.Id=b.OrderCode left join dbo.[OrderAddress] c on a.Id = c.OrderCode
                         " + where + @") as temp 
						where ROWNUMBER BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            IList<VWOrderDetailEntity> entitylist = new List<VWOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Pagesize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@Pageindex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + keyword + "%");
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
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entitylist.Add(entity);
                }
            }

            string sql2 = " select count(1) from dbo.[Order] a left join dbo.[OrderDetail] b on a.Id=b.OrderCode " + where;

            cmd = db.GetSqlStringCommand(sql2);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            if (string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + keyword + "%");
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

            return entitylist;

        }

        /// <summary>
        /// 获取订单管理列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="OrderCode"></param>
        /// <param name="term"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<OrderEntity> GetManageOrderList(int pagesize, int pageindex, ref int recordCount, string OrderCode, int term, int status)
        {
            string where = " WHERE 1=1 ";

            if (!string.IsNullOrEmpty(OrderCode))
            {
                where += " AND Code like @Code ";
            }

            if (term > -2)
            {
                switch (term)
                {
                    case (int)OrderTerm.OneMonth:
                        {
                            where += " AND CreateTime >DATEADD(month,-1,GETDATE()) ";
                            break;
                        }
                    case (int)OrderTerm.Default:
                        {
                            where += " AND CreateTime >DATEADD(month,-3,GETDATE()) ";
                            break;
                        }
                    case (int)OrderTerm.YearThis:
                        {
                            where += " AND year(CreateTime) =year(GETDATE()) ";
                            break;
                        }
                    case (int)OrderTerm.YearLast:
                        {
                            where += " AND year(CreateTime) =year(GETDATE())-1 ";
                            break;
                        }
                    case (int)OrderTerm.YearPrevious:
                        {
                            where += " AND year(CreateTime) =year(GETDATE())-2 ";
                            break;
                        }
                }
            }

            if (status > 0)
            {
                where += " AND Status=@Status ";
            }

            string sql = @" SELECT [CanReturn],[Id],[Code],[OrderVisualCode],[MemId],[IsStore],[Status],FinancialStatus,[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],
                        [DeliverTime],[ReciveTime],[MakeBill],[FinishedTime],[MemLevel],PayPrice,ActPrice
                        FROM
                       (SELECT ROW_NUMBER() OVER (ORDER BY Id DESC) AS ROWNUMBER ,[CanReturn],[Id],[Code],[OrderVisualCode],[MemId],[IsStore],[Status],FinancialStatus,[CreateTime],[OrderType],TransFee,[NeedDeliver],[Integral],[TotalPrice],[TotalMarketPrice],[DealTime],
                        [DeliverTime],[ReciveTime],[MakeBill],[FinishedTime],[MemLevel],PayPrice,ActPrice  from dbo.[Order]
                        " + where + @") as temp WHERE ROWNUMBER BETWEEN (@Pagesize*(@Pageindex-1)+1) AND (@Pagesize*@Pageindex)";

            IList<OrderEntity> entitylist = new List<OrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Pagesize", DbType.Int32, pagesize);
            db.AddInParameter(cmd, "@Pageindex", DbType.Int32, pageindex);
            if (!string.IsNullOrEmpty(OrderCode))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + OrderCode + "%");
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderEntity entity = new OrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.OrderVisualCode = StringUtils.GetDbLong(reader["OrderVisualCode"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]); 
                    entity.IsStore = StringUtils.GetDbInt(reader["IsStore"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.FinancialStatus = StringUtils.GetDbInt(reader["FinancialStatus"]);  
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.OrderType = StringUtils.GetDbInt(reader["OrderType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.NeedDeliver = StringUtils.GetDbInt(reader["NeedDeliver"]);
                    entity.Integral = StringUtils.GetDbInt(reader["Integral"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.DealTime = StringUtils.GetDbDateTime(reader["DealTime"]);
                    entity.DeliverTime = StringUtils.GetDbDateTime(reader["DeliverTime"]);
                    entity.ReciveTime = StringUtils.GetDbDateTime(reader["ReciveTime"]);
                    entity.MakeBill = StringUtils.GetDbInt(reader["MakeBill"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.MemLevel = StringUtils.GetDbInt(reader["MemLevel"]);
                    entity.FinishedTime = StringUtils.GetDateTime(reader["FinishedTime"]); 
                    entity.CanReturn = StringUtils.GetDbInt(reader["CanReturn"]);
                    entity.PayPrice = StringUtils.GetDbDecimal(reader["PayPrice"]); 
                    entity.ActPrice = StringUtils.GetDbDecimal(reader["ActPrice"]); 
                    entitylist.Add(entity);
                }
            }

            string sql2 = "SELECT count(1) from dbo.[Order] " + where;

            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(OrderCode))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, OrderCode);
            }

            if (status > 0)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
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
    }
}
