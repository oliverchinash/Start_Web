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
功能描述：InquiryOrder表的数据访问类。
创建时间：2017/8/23 11:12:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// InquiryOrderEntity的数据访问操作
	/// </summary>
	public partial class InquiryOrderDA: BaseSuperMarketDB
    {
        #region 实例化
        public static InquiryOrderDA Instance
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
            internal static readonly InquiryOrderDA instance = new InquiryOrderDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
	 
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inquiryOrder">待更新的实体对象</param>
		public   int UpdateInquiryOrder(InquiryOrderEntity entity)
		{
			string sql= @" UPDATE dbo.[InquiryOrder] SET
                       [Code] =@Code
                      ,[VinNo] =@VinNo
                      ,[VinPic] =@VinPic
                      ,[EngineModelNo] =@EngineModelNo
                      ,[EngineModelPic] =@EngineModelPic
                      ,[CarBrandId] =@CarBrandId
                      ,[CarBrandName] =@CarBrandName
                      ,[CarSeriesId] =@CarSeriesId
                      ,[CarSeriesName] =@CarSeriesName
                      ,[CarTypeModelId] =@CarTypeModelId
                      ,[CarTypeModelName] =@CarTypeModelName
                      ,[ProductTypeId] =@ProductTypeId
                      ,[Remark] =@Remark
                      ,[NeedDay] =@NeedDay
                      ,[WLRemark] =@WLRemark
                      ,[Status] =@Status
                      ,[StatusForMem] =@StatusForMem
                       WHERE [Id]=@Id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@VinNo",  DbType.String,entity.VinNo);
			db.AddInParameter(cmd, "@VinPic",  DbType.String,entity.VinPic);
            db.AddInParameter(cmd, "@EngineModelNo", DbType.String, entity.EngineModelNo);
            db.AddInParameter(cmd, "@EngineModelPic", DbType.String, entity.EngineModelPic);
            db.AddInParameter(cmd,"@CarBrandId", DbType.Int32, entity.CarBrandId);
            db.AddInParameter(cmd,"@CarBrandName", DbType.String, entity.CarBrandName);
            db.AddInParameter(cmd,"@CarSeriesId", DbType.Int32, entity.CarSeriesId);
            db.AddInParameter(cmd,"@CarSeriesName", DbType.String, entity.CarSeriesName);
            db.AddInParameter(cmd,"@CarTypeModelId",  DbType.Int32,entity.CarTypeModelId);
			db.AddInParameter(cmd,"@CarTypeModelName",  DbType.String,entity.CarTypeModelName);
            db.AddInParameter(cmd,"@ProductTypeId",  DbType.Int32,entity.ProductTypeId);
            db.AddInParameter(cmd,"@NeedDay",  DbType.Int32,entity.NeedDay);
            db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
            db.AddInParameter(cmd, "@WLRemark",  DbType.String,entity.WLRemark);
            db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32,entity.StatusForMem);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int UpdateQuoteStatus(string code,int quotestatus)
        {
            string sql = @" UPDATE dbo.[InquiryOrder] SET 
                       [QuoteStatus] =@QuoteStatus
                       WHERE [Code]=@Code ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@Code", DbType.String, code); 
            db.AddInParameter(cmd, "@QuoteStatus", DbType.Int32, quotestatus); 
            return db.ExecuteNonQuery(cmd);
        }
        public int QuotePriceSet(string code, string pricestr,int status,int statusformem)
        {
            string sql = @"
SELECT  * INTO #tempp  FROM dbo.fun_SplitStrToTable(@PriceStr, '|', '_')
 
 UPDATE a  SET [Price]=b.value3,Remark=b.value4,HasStock=b.value5 FROM InquiryProductSub a inner JOIN #tempp b ON 
 a.Id=b.value2   WHERE a.[InquiryOrderCode]=@InquiryOrderCode 

 Update InquiryOrder set Status=@Status,StatusForMem=@StatusForMem,PreQuoteTime=getdate() where Code=@InquiryOrderCode
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@PriceStr", DbType.String, pricestr);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, statusformem);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            return db.ExecuteNonQuery(cmd);
        }
        public int QuotePriceCheck(string code )
        {
            string sql = @" Update InquiryOrder set Status=@Status,StatusForMem=@StatusForMem,PreQuoteTime=getdate() where Code=@InquiryOrderCode ";
 
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@Status", DbType.Int16, (int)OrderInquiryStatusEnum.WaitAccept);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int16, (int)InquiryStatusForMemEnum.WaitAccept);
            return db.ExecuteNonQuery(cmd);
        }

        public IList<InquiryOrderEntity> GetInquiryOrderToCGQuote(int num)
        {
            string sql = @"SELECT TOP "+ num+ @" A.[Id]
      ,A.[Code]
      ,A.[VinNo]
      ,A.[VinPic]
      ,A.[EngineModelNo]
      ,A.[EngineModelPic]
      ,A.[CarBrandId]
      ,A.[CarBrandName]
      ,A.[CarSeriesId]
      ,A.[CarSeriesName]
      ,A.[CarTypeModelId]
      ,A.[CarTypeModelName]
      ,A.[ProductTypeId]
      ,A.[Remark]
      ,A.[NeedDay]
      ,A.[WLRemark] 
      ,A.[MemId] 
      ,A.[CheckManId] 
      ,A.[Status] 
      ,A.[StatusForMem] 
      ,A.[CreateTime]  into #temp
  FROM [dbo].[InquiryOrder] a  WITH(NOLOCK)   WHERE  a.QuoteStatus=@QuoteStatusWait ORDER BY ID DESC
update a set QuoteStatus=@Quoteing from  [dbo].[InquiryOrder] a inner join  #temp b on a.Code=b.Code 
select * from #temp   WITH(NOLOCK)
";
            IList<InquiryOrderEntity> entityList = new List<InquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@QuoteStatusWait", DbType.Int32, (int)QuoteStatusEnum.WaitSend);
            db.AddInParameter(cmd, "@Quoteing", DbType.Int32, (int)QuoteStatusEnum.Sendding);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    InquiryOrderEntity entity = new InquiryOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.VinNo = StringUtils.GetDbString(reader["VinNo"]);
                    entity.VinPic = StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);
                    entity.CarBrandId = StringUtils.GetDbInt(reader["CarBrandId"]);
                    entity.CarBrandName = StringUtils.GetDbString(reader["CarBrandName"]);
                    entity.CarSeriesId = StringUtils.GetDbInt(reader["CarSeriesId"]);
                    entity.CarSeriesName = StringUtils.GetDbString(reader["CarSeriesName"]);
                    entity.CarTypeModelId = StringUtils.GetDbInt(reader["CarTypeModelId"]);
                    entity.CarTypeModelName = StringUtils.GetDbString(reader["CarTypeModelName"]);
                    entity.ProductTypeId = StringUtils.GetDbInt(reader["ProductTypeId"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.WLRemark = StringUtils.GetDbString(reader["WLRemark"]);
                    entity.NeedDay = StringUtils.GetDbInt(reader["NeedDay"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]); 
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.StatusForMem = StringUtils.GetDbInt(reader["StatusForMem"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public int InquiryOrderEdit(VWInquiryOrderEntity inquiryOrder)
        {
            string sql = @" BEGIN TRAN OK 
begin
 IF ((NOT EXISTS(SELECT 1 FROM InquiryOrder WHERE  Code=@InquiryOrderCode)) or @InquiryOrderId=0)
BEGIN 
INSERT INTO [dbo].[InquiryOrder]
           ([Code]
           ,[VinNo]
           ,[CarTypeModelId]
           ,[CarTypeModelName] 
           ,[Remark]
           ,[CreateManId]
           ,[CreateTime] 
           ,[Status],[StatusForMem])
     VALUES
           (@InquiryOrderCode 
           ,@VinNo
           ,@CarTypeModelId
           ,@CarTypeModelName
           ,@Remark
           ,@CreateManId
           ,@CreateTime 
           ,@Status,@StatusForMem)
INSERT INTO  [dbo].[InquiryOrderMember]
           ([InquiryOrderCode]
           ,[MemId]
           ,[MemName]
           ,[MemPhone]
           ,[CompanyAddress]
           ,[CompanyName])
     VALUES
           (@InquiryOrderCode
           ,@MemId
           ,@MemName
           ,@MemPhone
           ,@CompanyAddress
           ,@CompanyName)
INSERT INTO [dbo].[InquiryOrderPics]
           ([InquiryOrderCode]
           ,[PicUrl]
           ,[Sort])
SELECT @InquiryOrderCode,id, 101-ROW_NUMBER() over(order by (select 0)) AS ROWNUM FROM DBO.fun_splitstr(@PicPaths,'|')   
end
ELSE IF  EXISTS(SELECT 1 FROM InquiryOrder WHERE Id=@InquiryOrderId and Code=@InquiryOrderCode)
BEGIN
update  [dbo].[InquiryOrder]
           set  [VinNo]=@VinNo
           ,[CarTypeModelId]=@CarTypeModelId
           ,[CarTypeModelName]=@CarTypeModelName
           ,[Remark]=@Remark  
           ,[CheckManId]=@CheckManId
           ,[CheckTime] =@CheckTime
           ,[Status]=@Status,[StatusForMem]=@StatusForMem where Code=@InquiryOrderCode and Id=@InquiryOrderId
      
update [dbo].[InquiryOrderMember]  set [InquiryOrderCode]=@InquiryOrderCode
           ,[MemId]=@MemId
           ,[MemName]=@MemName
           ,[MemPhone]=@MemPhone
           ,[CompanyAddress]=@CompanyAddress
           ,[CompanyName]=@CompanyName where InquiryOrderCode= @InquiryOrderCode
     SELECT @InquiryOrderCode as InquiryOrderCode,id as PicUrl, 101-ROW_NUMBER() 
over(order by (select 0)) AS Sort into #tempinquirypics FROM DBO.fun_splitstr(@PicPaths,'|')
--修改图片路径
update  a set PicUrl=b.PicUrl from InquiryOrderPics a with(nolock) inner join #tempinquirypics b with(nolock)  on 
a.Sort=b.Sort and a.InquiryOrderCode=b.InquiryOrderCode where a.InquiryOrderCode=@InquiryOrderCode
--删除图片路径
delete a from InquiryOrderPics a with(nolock) left join #tempinquirypics b with(nolock)  on 
a.Sort=b.Sort  and a.InquiryOrderCode=b.InquiryOrderCode where  b.InquiryOrderCode is null
--新增图片路径
insert into [dbo].[InquiryOrderPics]([InquiryOrderCode],[PicUrl],[Sort])
select b.InquiryOrderCode,b.PicUrl,b.Sort from InquiryOrderPics a with(nolock) right join #tempinquirypics b with(nolock)  on 
a.Sort=b.Sort  and a.InquiryOrderCode=b.InquiryOrderCode where  a.InquiryOrderCode is null
 
END 
    IF(@@ERROR<>0)
    BEGIN
        ROLLBACK TRAN OK;
        SELECT 0; 
    END
    ELSE 
    BEGIN                            
        COMMIT TRAN OK;  
            SELECT 1;
    END
END  
";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@InquiryOrderId", DbType.Int32, inquiryOrder.InquiryOrderId);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, inquiryOrder.InquiryOrderCode);
            db.AddInParameter(cmd, "@CarTypeModelId", DbType.Int32, inquiryOrder.CarTypeModelId);
            db.AddInParameter(cmd, "@VinNo", DbType.String, inquiryOrder.VinNo); 
            db.AddInParameter(cmd, "@CarTypeModelName", DbType.String, inquiryOrder.CarTypeModelName);
            db.AddInParameter(cmd, "@Remark", DbType.String, inquiryOrder.Remark);  
            db.AddInParameter(cmd, "@PicPaths", DbType.String, inquiryOrder.OrderPicPaths); 
            db.AddInParameter(cmd, "@CreateManId", DbType.Int32, inquiryOrder.CreateManId);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, inquiryOrder.CreateTime);
            db.AddInParameter(cmd, "@CheckManId", DbType.Int32, inquiryOrder.CheckManId);
            db.AddInParameter(cmd, "@CheckTime", DbType.DateTime, inquiryOrder.CheckTime);
            db.AddInParameter(cmd, "@Status", DbType.Int32, inquiryOrder.Status);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, inquiryOrder.StatusForMem);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, inquiryOrder.MemId);
            db.AddInParameter(cmd, "@MemName", DbType.String, inquiryOrder.MemName);
            db.AddInParameter(cmd, "@MemPhone", DbType.String, inquiryOrder.MemPhone);
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, inquiryOrder.CompanyAddress);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, inquiryOrder.CompanyName); 
            return db.ExecuteNonQuery(cmd);
        }

        public string InquiryOrderEdit1(InquiryOrderEntity order)
        {
            string sql = @" BEGIN TRAN OK 
begin
 IF  (NOT EXISTS(SELECT 1 FROM InquiryOrder WHERE  Code=@InquiryOrderCode and MemId=@MemId)) 
BEGIN 
INSERT INTO [dbo].[InquiryOrder]
           ([Code]
           ,[VinNo]
           ,VinPic   
           ,[EngineModelNo]
           ,EngineModelPic 
          ,[CarBrandId]
          ,[CarBrandName]
          ,[CarSeriesId]
          ,[CarSeriesName]
          ,[CarTypeModelId]
          ,[CarTypeModelName] 
            ,[MemId]
            ,CreateManId
           ,[Status],[StatusForMem],[CreateTime],ScopeType)
     VALUES
           (@InquiryOrderCode 
           ,@VinNo
           ,@VinPic
           ,@EngineModelNo
           ,@EngineModelPic
          ,@CarBrandId
          ,@CarBrandName
          ,@CarSeriesId
          ,@CarSeriesName
          ,@CarTypeModelId
          ,@CarTypeModelName
           ,@MemId
           ,@CreateManId
           ,@Status,@StatusForMem,getdate(),@ScopeType) 
end
ELSE  if  EXISTS(SELECT 1 FROM InquiryOrder WHERE  Code=@InquiryOrderCode and Status=@Status and CreateManId=@CreateManId)
BEGIN
update  [dbo].[InquiryOrder]
           set [VinNo]=@VinNo
              ,[VinPic]=@VinPic
              ,[EngineModelNo]=@EngineModelNo
              ,[EngineModelPic]=@EngineModelPic
              ,CarBrandId=@CarBrandId
              ,CarBrandName=@CarBrandName
              ,CarSeriesId=@CarSeriesId
              ,CarSeriesName=@CarSeriesName
              ,CarTypeModelId=@CarTypeModelId
              ,CarTypeModelName=@CarTypeModelName  
              ,StatusForMem=@StatusForMem
              ,ScopeType=@ScopeType
                where Code=@InquiryOrderCode  
        
END 
    IF(@@ERROR<>0)
    BEGIN
        ROLLBACK TRAN OK;  
select ''  
    END
    ELSE 
    BEGIN                            
        COMMIT TRAN OK; 
select @InquiryOrderCode  
    END
END  
";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, order.Code); 
            db.AddInParameter(cmd, "@VinNo", DbType.String, order.VinNo);
            db.AddInParameter(cmd, "@VinPic", DbType.String, order.VinPic);
            db.AddInParameter(cmd, "@EngineModelNo", DbType.String, order.EngineModelNo);
            db.AddInParameter(cmd, "@EngineModelPic", DbType.String, order.EngineModelPic);
            db.AddInParameter(cmd, "@CarBrandId", DbType.Int32, order.CarBrandId); 
            db.AddInParameter(cmd, "@CarBrandName", DbType.String, order.CarBrandName); 
            db.AddInParameter(cmd, "@CarSeriesId", DbType.Int32, order.CarSeriesId); 
            db.AddInParameter(cmd, "@CarSeriesName", DbType.String, order.CarSeriesName); 
            db.AddInParameter(cmd, "@CarTypeModelId", DbType.Int32, order.CarTypeModelId);
            db.AddInParameter(cmd, "@CarTypeModelName", DbType.String, order.CarTypeModelName);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, order.MemId);  
            db.AddInParameter(cmd, "@CreateManId", DbType.Int32, order.CreateManId);  
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderInquiryStatusEnum.Edit);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, (int)InquiryStatusForMemEnum.Edit);
            db.AddInParameter(cmd, "@ScopeType", DbType.Int32, order.ScopeType);
             object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return "";
            return identity.ToString();
        }
        public int InquiryOrderEdit2(string code, string carmodelid, string carmodelname, int memid)
        {
            string sql = @"  
update  [dbo].[InquiryOrder]
           set  [CarTypeModelId]=@CarTypeModelId 
           ,[CarTypeModelName]=@CarTypeModelName   where Code=@InquiryOrderCode and MemId=@MemId  and Status=@Status
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@CarTypeModelId", DbType.Int32, carmodelid);
            db.AddInParameter(cmd, "@CarTypeModelName", DbType.String, carmodelname);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderInquiryStatusEnum.Edit);
           return  db.ExecuteNonQuery(cmd);
         
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int InquiryOrderSubmit(string code)
        {
            string sql = @" update InquiryOrder set Status=@Status,StatusForMem=@StatusForMem where Code=@InquiryOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code); 
            db.AddInParameter(cmd, "@Status", DbType.Int32,(int)OrderInquiryStatusEnum.Submit); 
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32,(int)InquiryStatusForMemEnum.Submit); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="resonid"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int InquiryOrderCancel(string ordercode, int resonid, string remark)
        {
            string sql = @" update InquiryOrder set Status=@Status,StatusForMem=@StatusForMem  where Code=@InquiryOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode); 
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderInquiryStatusEnum.Cancel);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, (int)InquiryStatusForMemEnum.Cancel);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 订单接收
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int InquiryOrderAccept(string code)
        {
            string sql = @" update InquiryOrder set Status=@Status  where Code=@InquiryOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderInquiryStatusEnum.Checked); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 推订单给供应商报价
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int InquiryOrderToQuote(string code, int status, int statusformem)
        {
            string sql = @" update InquiryOrder set Status=@Status,StatusForMem=@StatusForMem where Code=@InquiryOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, statusformem);
            return db.ExecuteNonQuery(cmd);
        }
       
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteInquiryOrderByKey(string code)
	    {
			string sql=@"update    InquiryOrder set hasdel=1 where Code=@Code";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@Code", DbType.String, code); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryOrderDisabled()
        {
            string sql = @"delete from  InquiryOrder  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryOrderByIds(string ids)
        {
            string sql = @"Delete from InquiryOrder  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryOrderByIds(string ids)
        {
            string sql = @"Update   InquiryOrder set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 
        public InquiryOrderEntity GetInquiryOrderByCode(string code)
        {
            string sql = @"SELECT [Id]
                                  ,[Code]
                                  ,[VinNo]
                                  ,[VinPic]
                                  ,[EngineModelNo]
                                  ,[EngineModelPic]
                                  ,[CarBrandId]
                                  ,[CarBrandName]
                                  ,[CarSeriesId]
                                  ,[CarSeriesName]
                                  ,[CarTypeModelId]
                                  ,[CarTypeModelName]
                                  ,[ProductTypeId]
                                  ,[Remark]
                                  ,[NeedDay]
                                  ,[WLRemark]
                                  ,[CGTotalPrice]
                                  ,[TotalPrice]
                                  ,[MemId]
                                  ,[CreateManId]
                                  ,[CreateTime]
                                  ,[CheckManId]
                                  ,[CheckTime]
                                  ,[PreQuoteTime]
                                  ,[UserSubmitFirstTime]
                                  ,[SendUserEndTime]
                                  ,[UserSubmitEndTime]
                                  ,[DeliveryManId]
                                  ,[DeliveryTime]
                                  ,[FinishedTime]
                                  ,[Status]
                                  ,[StatusForMem],HasDel,ScopeType
							FROM
							dbo.[InquiryOrder] WITH(NOLOCK)	
							WHERE [Code]=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, code);
            InquiryOrderEntity entity = new InquiryOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.VinNo = StringUtils.GetDbString(reader["VinNo"]);
                    entity.VinPic = StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);

                    entity.NeedDay = StringUtils.GetDbInt(reader["NeedDay"]); 
                    entity.ProductTypeId = StringUtils.GetDbInt(reader["ProductTypeId"]); 
        entity.CarBrandId = StringUtils.GetDbInt(reader["CarBrandId"]);
                    entity.CarBrandName = StringUtils.GetDbString(reader["CarBrandName"]);
        entity.CarSeriesId = StringUtils.GetDbInt(reader["CarSeriesId"]);
                    entity.CarSeriesName = StringUtils.GetDbString(reader["CarSeriesName"]);
        entity.CarTypeModelId = StringUtils.GetDbInt(reader["CarTypeModelId"]);
                    entity.CarTypeModelName = StringUtils.GetDbString(reader["CarTypeModelName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.WLRemark = StringUtils.GetDbString(reader["WLRemark"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.PreQuoteTime = StringUtils.GetDbDateTime(reader["PreQuoteTime"]);
                    entity.UserSubmitFirstTime = StringUtils.GetDbDateTime(reader["UserSubmitFirstTime"]);
                    entity.SendUserEndTime = StringUtils.GetDbDateTime(reader["SendUserEndTime"]);
                    entity.UserSubmitEndTime = StringUtils.GetDbDateTime(reader["UserSubmitEndTime"]);
                    entity.DeliveryManId = StringUtils.GetDbInt(reader["DeliveryManId"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(reader["DeliveryTime"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.StatusForMem = StringUtils.GetDbInt(reader["StatusForMem"]);
                    entity.HasDel = StringUtils.GetDbInt(reader["HasDel"]); 
                    entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);  
                }
            }
            return entity;
        }


        public VWInquiryOrderEntity GetVWInquiryOrderByCode(string code)
        {
            string sql = @"SELECT a.Id as InquiryOrderId,  a.[Code],[VinNo],[VinPic],[EngineModelNo],[EngineModelPic],[CarBrandId],[CarBrandName],[CarSeriesId],[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[WLRemark],CGTotalPrice,TotalPrice,[CreateManId],[CreateTime],[CheckManId],[CheckTime],[PreQuoteTime],
                        [UserSubmitFirstTime],[SendUserEndTime],[UserSubmitEndTime],[DeliveryManId],[DeliveryTime],
                        [FinishedTime],a.[Status],a.[StatusForMem],a.ScopeType,b.MemId,b.[MemName]
                              ,b.[MemPhone]
                              ,b.[CompanyAddress]
                              ,b.[CompanyName] from dbo.[InquiryOrder] a WITH(NOLOCK) INNER JOIN InquiryOrderMember b WITH(NOLOCK) 
                        ON a.[Code]=b.[InquiryOrderCode]  WHERE  a.[Code]=@InquiryOrderCode ; 
";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            VWInquiryOrderEntity entity = new VWInquiryOrderEntity();
            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds.Tables.Count == 1)
            {
                DataTable _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    DataRow dr = _dt.Rows[0];
                    entity.InquiryOrderId = StringUtils.GetDbInt(dr["InquiryOrderId"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(dr["Code"]);
                    entity.VinNo = StringUtils.GetDbString(dr["VinNo"]);
                    entity.VinPic = StringUtils.GetDbString(dr["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(dr["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(dr["EngineModelPic"]);
                    entity.CarBrandId = StringUtils.GetDbInt(dr["CarBrandId"]);
                    entity.CarBrandName = StringUtils.GetDbString(dr["CarBrandName"]);
                    entity.CarSeriesId = StringUtils.GetDbInt(dr["CarSeriesId"]);
                    entity.CarSeriesName = StringUtils.GetDbString(dr["CarSeriesName"]);
                    entity.CarTypeModelId = StringUtils.GetDbInt(dr["CarTypeModelId"]);
                    entity.CarTypeModelName = StringUtils.GetDbString(dr["CarTypeModelName"]);
                    entity.Remark = StringUtils.GetDbString(dr["Remark"]);
                    entity.WLRemark = StringUtils.GetDbString(dr["WLRemark"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(dr["CGTotalPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(dr["TotalPrice"]);
                    entity.CreateManId = StringUtils.GetDbInt(dr["CreateManId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(dr["CreateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(dr["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(dr["CheckTime"]);
                    entity.PreQuoteTime = StringUtils.GetDbDateTime(dr["PreQuoteTime"]);
                    entity.UserSubmitFirstTime = StringUtils.GetDbDateTime(dr["UserSubmitFirstTime"]);
                    entity.SendUserEndTime = StringUtils.GetDbDateTime(dr["SendUserEndTime"]);
                    entity.UserSubmitEndTime = StringUtils.GetDbDateTime(dr["UserSubmitEndTime"]);
                    entity.DeliveryManId = StringUtils.GetDbInt(dr["DeliveryManId"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(dr["DeliveryTime"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(dr["FinishedTime"]);
                    entity.Status = StringUtils.GetDbInt(dr["Status"]);
                    entity.StatusForMem = StringUtils.GetDbInt(dr["StatusForMem"]);
                    entity.MemId = StringUtils.GetDbInt(dr["MemId"]);
                    entity.MemName = StringUtils.GetDbString(dr["MemName"]);
                    entity.MemPhone = StringUtils.GetDbString(dr["MemPhone"]);
                    entity.CompanyName = StringUtils.GetDbString(dr["CompanyName"]);
                    entity.CompanyAddress = StringUtils.GetDbString(dr["CompanyAddress"]);
                    entity.ScopeType = StringUtils.GetDbInt(dr["ScopeType"]);
                }
                 
            }
            return entity;
        }

        public VWInquiryOrderEntity GetInquiryOrderDataByCode(string code)
        {
            string sql = @"SELECT a.Id as InquiryOrderId,  a.[Code],a.[VinNo],a.[VinPic],a.EngineModelNo,a.EngineModelPic,a.[CarBrandId]
      ,a.[CarBrandName],a.[CarSeriesId],a.[CarSeriesName],a.[CarTypeModelId]
      ,a.[CarTypeModelName],a.[ProductTypeId],a.[NeedDay],a.ScopeType,[Remark],[WLRemark],CGTotalPrice,TotalPrice,[CreateManId],[CreateTime],[CheckManId],[CheckTime],[PreQuoteTime],
                        [UserSubmitFirstTime],[SendUserEndTime],[UserSubmitEndTime],[DeliveryManId],[DeliveryTime],
                        [FinishedTime],a.[Status],a.[StatusForMem],b.MemId,b.[MemName]
                              ,b.[MemPhone]
                              ,b.[CompanyAddress]
                              ,b.[CompanyName] from dbo.[InquiryOrder] a WITH(NOLOCK) INNER JOIN InquiryOrderMember b WITH(NOLOCK) 
                        ON a.[Code]=b.[InquiryOrderCode]  WHERE  a.[Code]=@InquiryOrderCode ; 
SELECT [Id],[InquiryOrderCode],[PicUrl],[Sort] from dbo.[InquiryOrderPics] WITH(NOLOCK)	where InquiryOrderCode=@InquiryOrderCode ;
SELECT    [Id],[InquiryOrderCode],[ProductCode],[ClassesId],ClassesName,ProductId,[ProductName],[ProductNum],[ProductUnitName],[Remark] from dbo.[InquiryProduct] WITH(NOLOCK) 	where InquiryOrderCode=@InquiryOrderCode ;	
SELECT    [Id],InquiryOrderCode,[InquiryProductId],[InquiryProductType],Price,HasStock,Remark from dbo.[InquiryProductSub] WITH(NOLOCK)	where InquiryOrderCode=@InquiryOrderCode Order By InquiryProductId,InquiryProductType
";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            VWInquiryOrderEntity entity = new VWInquiryOrderEntity();
            DataSet ds = db.ExecuteDataSet(cmd);

            if (ds.Tables.Count == 4)
            {
                DataTable _dt = new DataTable();
                _dt = ds.Tables[0];
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    DataRow dr = _dt.Rows[0];
                    entity.InquiryOrderId = StringUtils.GetDbInt(dr["InquiryOrderId"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(dr["Code"]);
                    entity.VinNo = StringUtils.GetDbString(dr["VinNo"]);
                    entity.VinPic = StringUtils.GetDbString(dr["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(dr["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(dr["EngineModelPic"]);
                    entity.CarBrandId = StringUtils.GetDbInt(dr["CarBrandId"]);
                    entity.CarBrandName = StringUtils.GetDbString(dr["CarBrandName"]);
                    entity.CarSeriesId = StringUtils.GetDbInt(dr["CarSeriesId"]);
                    entity.CarSeriesName = StringUtils.GetDbString(dr["CarSeriesName"]);
                    entity.CarTypeModelId = StringUtils.GetDbInt(dr["CarTypeModelId"]);
                    entity.CarTypeModelName = StringUtils.GetDbString(dr["CarTypeModelName"]);
                    entity.ProductTypeId = StringUtils.GetDbInt(dr["ProductTypeId"]);
                    entity.NeedDay = StringUtils.GetDbInt(dr["NeedDay"]);
                    entity.Remark = StringUtils.GetDbString(dr["Remark"]);
                    entity.WLRemark = StringUtils.GetDbString(dr["WLRemark"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(dr["CGTotalPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(dr["TotalPrice"]);
                    entity.CreateManId = StringUtils.GetDbInt(dr["CreateManId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(dr["CreateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(dr["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(dr["CheckTime"]);
                    entity.PreQuoteTime = StringUtils.GetDbDateTime(dr["PreQuoteTime"]);
                    entity.UserSubmitFirstTime = StringUtils.GetDbDateTime(dr["UserSubmitFirstTime"]);
                    entity.SendUserEndTime = StringUtils.GetDbDateTime(dr["SendUserEndTime"]);
                    entity.UserSubmitEndTime = StringUtils.GetDbDateTime(dr["UserSubmitEndTime"]);
                    entity.DeliveryManId = StringUtils.GetDbInt(dr["DeliveryManId"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(dr["DeliveryTime"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(dr["FinishedTime"]);
                    entity.Status = StringUtils.GetDbInt(dr["Status"]);
                    entity.StatusForMem = StringUtils.GetDbInt(dr["StatusForMem"]);
                    entity.MemId = StringUtils.GetDbInt(dr["MemId"]);
                    entity.MemName = StringUtils.GetDbString(dr["MemName"]);
                    entity.MemPhone = StringUtils.GetDbString(dr["MemPhone"]);
                    entity.CompanyName = StringUtils.GetDbString(dr["CompanyName"]);
                    entity.CompanyAddress = StringUtils.GetDbString(dr["CompanyAddress"]);
                    entity.ScopeType = StringUtils.GetDbInt(dr["ScopeType"]);
                }
                DataTable _dt2 = new DataTable();
                _dt2 = ds.Tables[1];
                if (_dt2 != null && _dt2.Rows.Count > 0)
                {
                    IList<InquiryOrderPicsEntity> picentityList = new List<InquiryOrderPicsEntity>();
                    foreach (DataRow dr2 in _dt2.Rows)
                    {
                        InquiryOrderPicsEntity picentity = new InquiryOrderPicsEntity();
                        picentity.Id = StringUtils.GetDbInt(dr2["Id"]);
                        picentity.InquiryOrderCode = StringUtils.GetDbString(dr2["InquiryOrderCode"]);
                        picentity.PicUrl = StringUtils.GetDbString(dr2["PicUrl"]);
                        picentity.Sort = StringUtils.GetDbInt(dr2["Sort"]);
                        picentityList.Add(picentity);
                    }
                    entity.OrderPics = picentityList;
                }
                DataTable _dt3 = new DataTable();
                _dt3 = ds.Tables[2];
                if (_dt3 != null && _dt3.Rows.Count > 0)
                {
                    IList<InquiryProductEntity> productentityList = new List<InquiryProductEntity>();
                    foreach (DataRow dr3 in _dt3.Rows)
                    {
                        InquiryProductEntity productentity = new InquiryProductEntity();
                        productentity.Id = StringUtils.GetDbInt(dr3["Id"]);
                        productentity.InquiryOrderCode = StringUtils.GetDbString(dr3["InquiryOrderCode"]);
                        productentity.ProductCode = StringUtils.GetDbString(dr3["ProductCode"]);
                        productentity.ClassesId = StringUtils.GetDbInt(dr3["ClassesId"]);
                        productentity.ClassesName = StringUtils.GetDbString(dr3["ClassesName"]);
                        productentity.ProductId = StringUtils.GetDbInt(dr3["ProductId"]);
                        productentity.ProductName = StringUtils.GetDbString(dr3["ProductName"]);
                        productentity.ProductNum = StringUtils.GetDbInt(dr3["ProductNum"]);
                        productentity.ProductUnitName = StringUtils.GetDbString(dr3["ProductUnitName"]);
                        productentity.Remark = StringUtils.GetDbString(dr3["Remark"]);
                        productentityList.Add(productentity);
                    }
                    entity.OrderProducts = productentityList;
                }
                DataTable _dt4 = new DataTable();
                _dt4 = ds.Tables[3];
                if (_dt4 != null && _dt4.Rows.Count > 0)
                {
                    IList<InquiryProductSubEntity> productsubentityList = new List<InquiryProductSubEntity>();
                    foreach (DataRow dr4 in _dt4.Rows)
                    {
                        InquiryProductSubEntity productsubentity = new InquiryProductSubEntity();
                        productsubentity.Id = StringUtils.GetDbInt(dr4["Id"]);
                        productsubentity.InquiryOrderCode = StringUtils.GetDbString(dr4["InquiryOrderCode"]);
                        productsubentity.InquiryProductId = StringUtils.GetDbInt(dr4["InquiryProductId"]);
                        productsubentity.InquiryProductType = StringUtils.GetDbInt(dr4["InquiryProductType"]);
                        productsubentity.Price = StringUtils.GetDbDecimal(dr4["Price"]);
                        productsubentity.HasStock = StringUtils.GetDbInt(dr4["HasStock"]);
                        productsubentity.Remark = StringUtils.GetDbString(dr4["Remark"]);
                        productsubentityList.Add(productsubentity);
                    }
                    entity.OrderProductSubs = productsubentityList;
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<VWInquiryOrderEntity> GetInquiryOrderList(int pagesize, int pageindex, ref  int recordCount,int memid, int status, int statusformem, string key)
		{
            string where = " where 1=1 and a.HasDel=0 ";
            if(memid!=-1)
            {
                where += " and b.MemId=@MemId";
            }
            if (status != -1)
            {
                where += " and a.Status=@Status";
            }
            if (statusformem != -1)
            {
                where += " and a.StatusForMem=@StatusForMem";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and a.Code like @Code";
            }
            string sql= @"SELECT   [Code],[VinNo], [VinPic], EngineModelNo, EngineModelPic,[CarBrandId]
      ,[CarBrandName],[CarSeriesId],[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[WLRemark],CGTotalPrice,TotalPrice,[CreateManId],[CreateTime],[CheckManId],[CheckTime],[PreQuoteTime],[UserSubmitFirstTime],[SendUserEndTime],[UserSubmitEndTime],[DeliveryManId],[DeliveryTime],[FinishedTime],[Status],[StatusForMem],MemId,[MemName]
      ,[MemPhone]
      ,[CompanyAddress]
      ,[CompanyName],ScopeType FROM (SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
a.[Code],a.[VinNo],a.[VinPic],a.EngineModelNo,a.EngineModelPic,a.[CarBrandId]
      ,a.[CarBrandName],a.[CarSeriesId],a.[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[WLRemark],CGTotalPrice,TotalPrice,[CreateManId],[CreateTime],  [CheckManId],[CheckTime],[PreQuoteTime],
                        [UserSubmitFirstTime],[SendUserEndTime],[UserSubmitEndTime],[DeliveryManId],[DeliveryTime],
                        [FinishedTime],a.[Status],a.[StatusForMem] , b.MemId,b.[MemName]
      ,b.[MemPhone]
      ,b.[CompanyAddress]
      ,b.[CompanyName],a.ScopeType 
from dbo.[InquiryOrder] a WITH(NOLOCK) INNER JOIN InquiryOrderMember b WITH(NOLOCK) 
ON a.[Code]=b.[InquiryOrderCode]   " + where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2= @"Select count(1) from dbo.[InquiryOrder] a WITH(NOLOCK) INNER JOIN InquiryOrderMember b WITH(NOLOCK) 
ON a.[Code]=b.[InquiryOrderCode] " + where;
            IList<VWInquiryOrderEntity> entityList = new List<VWInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid != -1)
            {
		    db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            }
            if (status != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);

            }
            if (statusformem != -1)
            {
                db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, statusformem); 
            }
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Code", DbType.String, "%"+ key + "%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWInquiryOrderEntity entity =new VWInquiryOrderEntity();
					 entity.InquiryOrderCode=StringUtils.GetDbString(reader["Code"]);
					entity.VinNo=StringUtils.GetDbString(reader["VinNo"]);
                    entity.VinPic = StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);
                    entity.CarBrandId = StringUtils.GetDbInt(reader["CarBrandId"]);
                    entity.CarBrandName = StringUtils.GetDbString(reader["CarBrandName"]);
                    entity.CarSeriesId = StringUtils.GetDbInt(reader["CarSeriesId"]);
                    entity.CarSeriesName = StringUtils.GetDbString(reader["CarSeriesName"]);
                    entity.CarTypeModelId=StringUtils.GetDbInt(reader["CarTypeModelId"]);
					entity.CarTypeModelName=StringUtils.GetDbString(reader["CarTypeModelName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.WLRemark=StringUtils.GetDbString(reader["WLRemark"]);
					entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.PreQuoteTime=StringUtils.GetDbDateTime(reader["PreQuoteTime"]);
					entity.UserSubmitFirstTime=StringUtils.GetDbDateTime(reader["UserSubmitFirstTime"]);
					entity.SendUserEndTime=StringUtils.GetDbDateTime(reader["SendUserEndTime"]);
					entity.UserSubmitEndTime=StringUtils.GetDbDateTime(reader["UserSubmitEndTime"]);
					entity.DeliveryManId=StringUtils.GetDbInt(reader["DeliveryManId"]);
					entity.DeliveryTime=StringUtils.GetDbDateTime(reader["DeliveryTime"]);
					entity.FinishedTime=StringUtils.GetDbDateTime(reader["FinishedTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.StatusForMem= StringUtils.GetDbInt(reader["StatusForMem"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.MemName=StringUtils.GetDbString(reader["MemName"]);
					entity.MemPhone=StringUtils.GetDbString(reader["MemPhone"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);

            }
            if (statusformem != -1)
            {
                db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, statusformem);
            }
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + key + "%");
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
        /// 供应商报价询价单列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="memid"></param>
        /// <param name="status"></param>
        /// <param name="statusformem"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<VWInquiryOrderEntity> GetInquiryListCG(int pagesize, int pageindex, ref int recordCount, int memid, int status, int statusformem, string key)
        {
            string where = " where 1=1 and a.HasDel=0 and b.HasCGDel=0";
            if (memid != -1)
            {
                where += " and b.CGMemId=@MemId";
            }
            if (status != -1)
            {
                where += " and a.Status=@Status";
            }
            if (statusformem != -1)
            {
                where += " and a.StatusForMem=@StatusForMem";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and a.Code like @Code";
            }
            string sql = @"SELECT   [Code],[VinNo], [VinPic], EngineModelNo, EngineModelPic, [CarBrandId]
      , [CarBrandName], [CarSeriesId], [CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[WLRemark],CGTotalPrice,TotalPrice,[CreateManId],[CreateTime],[CheckManId],[CheckTime],[PreQuoteTime],[UserSubmitFirstTime],[SendUserEndTime],[UserSubmitEndTime],[DeliveryManId],[DeliveryTime],[FinishedTime]
,[Status],StatusForMem,HasQuote,QuoteTime, ScopeType  FROM (SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
a.[Code],[VinNo],a.[VinPic],a.EngineModelNo,a.EngineModelPic,a.[CarBrandId]
      ,a.[CarBrandName],a.[CarSeriesId],a.[CarSeriesName],[CarTypeModelId],[CarTypeModelName],a.[Remark],a.[WLRemark],a.CGTotalPrice,a.TotalPrice,a.[CreateManId],a.[CreateTime],a.[CheckManId],a.[CheckTime],[PreQuoteTime],
[UserSubmitFirstTime],[SendUserEndTime],[UserSubmitEndTime],[DeliveryManId],[DeliveryTime],
[FinishedTime],a.[Status] ,a.[StatusForMem],b.HasQuote,b.QuoteTime,a.ScopeType  from dbo.[InquiryOrder] a WITH(NOLOCK) INNER JOIN CGMemQuoted b WITH(NOLOCK) 
ON a.[Code]=b.[InquiryOrderCode]  " + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[InquiryOrder] a WITH(NOLOCK) INNER JOIN CGMemQuoted b WITH(NOLOCK) 
ON a.[Code]=b.[InquiryOrderCode]  " + where;
            IList<VWInquiryOrderEntity> entityList = new List<VWInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (statusformem != -1)
            {
                db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, statusformem);
            }
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + key + "%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWInquiryOrderEntity entity = new VWInquiryOrderEntity();
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["Code"]);
                    entity.VinNo = StringUtils.GetDbString(reader["VinNo"]);
                    entity.VinPic = StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);
                    entity.CarBrandId = StringUtils.GetDbInt(reader["CarBrandId"]);
                    entity.CarBrandName = StringUtils.GetDbString(reader["CarBrandName"]);
                    entity.CarSeriesId = StringUtils.GetDbInt(reader["CarSeriesId"]);
                    entity.CarSeriesName = StringUtils.GetDbString(reader["CarSeriesName"]);
                    entity.CarTypeModelId = StringUtils.GetDbInt(reader["CarTypeModelId"]);
                    entity.CarTypeModelName = StringUtils.GetDbString(reader["CarTypeModelName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.WLRemark = StringUtils.GetDbString(reader["WLRemark"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);  
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]); 
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]); 
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.PreQuoteTime = StringUtils.GetDbDateTime(reader["PreQuoteTime"]);
                    entity.UserSubmitFirstTime = StringUtils.GetDbDateTime(reader["UserSubmitFirstTime"]);
                    entity.SendUserEndTime = StringUtils.GetDbDateTime(reader["SendUserEndTime"]);
                    entity.UserSubmitEndTime = StringUtils.GetDbDateTime(reader["UserSubmitEndTime"]);
                    entity.DeliveryManId = StringUtils.GetDbInt(reader["DeliveryManId"]);
                    entity.DeliveryTime = StringUtils.GetDbDateTime(reader["DeliveryTime"]);
                    entity.FinishedTime = StringUtils.GetDbDateTime(reader["FinishedTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.StatusForMem = StringUtils.GetDbInt(reader["StatusForMem"]);
                    entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);

            }
            if (statusformem != -1)
            {
                db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, statusformem);
            }
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + key + "%");
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


        public IList<ReportInquiryOrderEntity> GetReportDaily(string startdate, string enddate,int status,int reporttype,int orderby)
        {
            string sql = ""; 
            string orderbystr = "";
            string where = " where a.CreateTime>@StartDate and a.CreateTime<@EndDate ";
            if(status!=-1)
            {
                where += " and a.Status=@Status ";
            }
            if (reporttype==(int)ReportInquiryTypeEnum.CreateDate)
            {
                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,0 AS MemId,'' AS MemName, '' AS CompanyName, SUM(1) AS TotalNum,SUM( HasConfirm ) AS ConfirmNum,CAST(SUM( HasConfirm )*100 AS DECIMAL(10,3))/SUM(1) AS ConfirmRate 
FROM dbo.InquiryOrder a WITH(NOLOCK) " + where+ " GROUP BY CONVERT(VARCHAR(10), a.CreateTime, 120) ";
            }
            else if (reporttype == (int)ReportInquiryTypeEnum.Mem) 
            {
                sql = @"SELECT  CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,a.MemId AS MemId,b.MemName AS MemName,b.CompanyName AS  CompanyName,SUM(1) AS TotalNum,SUM( HasConfirm ) AS ConfirmNum,CAST(SUM( HasConfirm )*100 AS DECIMAL(10,3))/SUM(1) AS ConfirmRate 
 FROM dbo.InquiryOrder a WITH(NOLOCK) INNER JOIN dbo.InquiryOrderMember b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY CONVERT(VARCHAR(10),a.CreateTime,120),a.MemId,b.MemName,b.CompanyName ";
            }
            else if (reporttype == (int)ReportInquiryTypeEnum.CreateMonth)
            {
                sql = @"SELECT CONVERT(VARCHAR(7),a.CreateTime,120) AS CreateDate,0 AS MemId,'' AS MemName, '' AS CompanyName, SUM(1) AS TotalNum,SUM( HasConfirm ) AS ConfirmNum,CAST(SUM( HasConfirm )*100 AS DECIMAL(10,3))/SUM(1) AS ConfirmRate 
FROM dbo.InquiryOrder a WITH(NOLOCK) " + where + " GROUP BY CONVERT(VARCHAR(7), a.CreateTime, 120) ";
            }
            else if (reporttype == (int)ReportInquiryTypeEnum.MemMonth)
            {
                sql = @"SELECT  CONVERT(VARCHAR(7),a.CreateTime,120) AS CreateDate,a.MemId AS MemId,b.MemName AS MemName,b.CompanyName AS  CompanyName,SUM(1) AS TotalNum,SUM( HasConfirm ) AS ConfirmNum,CAST(SUM( HasConfirm )*100 AS DECIMAL(10,3))/SUM(1) AS ConfirmRate 
 FROM dbo.InquiryOrder a WITH(NOLOCK) INNER JOIN dbo.InquiryOrderMember b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY CONVERT(VARCHAR(7),a.CreateTime,120),a.MemId,b.MemName,b.CompanyName ";
            }
            else//默认
            {
                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,0 AS MemId,'' AS MemName, '' AS CompanyName, SUM(1) AS TotalNum,SUM( HasConfirm ) AS ConfirmNum,CAST(SUM( HasConfirm )*100 AS DECIMAL(10,3))/SUM(1) AS ConfirmRate 
FROM dbo.InquiryOrder a WITH(NOLOCK) " + where + " GROUP BY CONVERT(VARCHAR(10), a.CreateTime, 120) ";

            }
            if (orderby==(int)ReportInquiryOrderEnum.ConfirmNum)
            {
                orderbystr = " Order By ConfirmNum desc";
            }
            else if (orderby == (int)ReportInquiryOrderEnum.TotalNum)
            {
                orderbystr = " Order By TotalNum desc";
            }
            else if (orderby == (int)ReportInquiryOrderEnum.ConfirmRate)
            {
                orderbystr = " Order By ConfirmRate desc";
            }
            else
            { 
                orderbystr = " Order By CreateDate desc";
            }
            
            sql = sql + orderbystr;
            IList<ReportInquiryOrderEntity> entityList = new List<ReportInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            
                db.AddInParameter(cmd, "@StartDate", DbType.DateTime, startdate);
                db.AddInParameter(cmd, "@EndDate", DbType.DateTime, enddate);
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int16, status); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReportInquiryOrderEntity entity = new ReportInquiryOrderEntity(); 
                    entity.CreateDate = StringUtils.GetDbString(reader["CreateDate"]);
                    entity.MemName = StringUtils.GetDbString(reader["MemName"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.TotalNum = StringUtils.GetDbInt(reader["TotalNum"]);
                    entity.ConfirmNum = StringUtils.GetDbInt(reader["ConfirmNum"]);
                    entity.ConfirmRate = StringUtils.GetDbDecimal(reader["ConfirmRate"]); 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public IList<ReportInquiryOrderEntity> GetCGReportDaily(string startdate, string enddate, int status, int reporttype, int orderby)
        {
            string sql = "";
            string orderbystr = "";
            string where = " where a.CreateTime>@StartDate and a.CreateTime<@EndDate ";
            if (status != -1)
            {
                where += " and a.Status=@Status ";
            }
//            if (reporttype == (int)ReportInquiryCGTypeEnum.CreateDate)
//            {
//                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,0 AS  CGMemId,SUM(1) AS TotalNum,SUM(HasQuote) AS QuoteNum,SUM(HasChecked) AS CheckedNum
// , CAST(SUM(HasQuote) AS DECIMAL(10,3))/SUM(1) AS QuoteRate, CASE WHEN SUM(HasQuote)=0 THEN 0 else CAST(SUM(HasChecked) AS DECIMAL(10,3))/SUM(HasQuote) END AS CheckedRate
//FROM   dbo.InquiryOrder a  INNER JOIN dbo.CGMemQuoted b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY CONVERT(VARCHAR(10), a.CreateTime, 120) ";
//            }
//            else 
            if (reporttype == (int)ReportInquiryCGTypeEnum.Mem)
            {
                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,b.CGMemId AS  CGMemId,SUM(1) AS TotalNum,SUM(HasQuote) AS QuoteNum,SUM(HasChecked) AS CheckedNum
 , CAST(SUM(HasQuote) AS DECIMAL(10,3))/SUM(1) AS QuoteRate, CASE WHEN SUM(HasQuote)=0 THEN 0 else CAST(SUM(HasChecked) AS DECIMAL(10,3))/SUM(HasQuote) END AS CheckedRate
FROM   dbo.InquiryOrder a  INNER JOIN dbo.CGMemQuoted b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY b.CGMemId,CONVERT(VARCHAR(10), a.CreateTime, 120) ";
            }
//            else if (reporttype == (int)ReportInquiryCGTypeEnum.CreateMonth)
//            {
//                sql = @"SELECT CONVERT(VARCHAR(7),a.CreateTime,120) AS CreateDate,0 AS  CGMemId,SUM(1) AS TotalNum,SUM(HasQuote) AS QuoteNum,SUM(HasChecked) AS CheckedNum
// , CAST(SUM(HasQuote) AS DECIMAL(10,3))/SUM(1) AS QuoteRate, CASE WHEN SUM(HasQuote)=0 THEN 0 else CAST(SUM(HasChecked) AS DECIMAL(10,3))/SUM(HasQuote) END AS CheckedRate
//FROM   dbo.InquiryOrder a  INNER JOIN dbo.CGMemQuoted b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY CONVERT(VARCHAR(7), a.CreateTime, 120) ";
//            }
            else if (reporttype == (int)ReportInquiryCGTypeEnum.MemMonth)
            {
                sql = @"SELECT CONVERT(VARCHAR(7),a.CreateTime,120) AS CreateDate,b.CGMemId AS  CGMemId,SUM(1) AS TotalNum,SUM(HasQuote) AS QuoteNum,SUM(HasChecked) AS CheckedNum
 , CAST(SUM(HasQuote) AS DECIMAL(10,3))/SUM(1) AS QuoteRate, CASE WHEN SUM(HasQuote)=0 THEN 0 else CAST(SUM(HasChecked) AS DECIMAL(10,3))/SUM(HasQuote) END AS CheckedRate
FROM   dbo.InquiryOrder a  INNER JOIN dbo.CGMemQuoted b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY b.CGMemId,CONVERT(VARCHAR(7), a.CreateTime, 120) ";
            }
            else//默认
            {
                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,b.CGMemId AS  CGMemId,SUM(1) AS TotalNum,SUM(HasQuote) AS QuoteNum,SUM(HasChecked) AS CheckedNum
 , CAST(SUM(HasQuote) AS DECIMAL(10,3))/SUM(1) AS QuoteRate, CASE WHEN SUM(HasQuote)=0 THEN 0 else CAST(SUM(HasChecked) AS DECIMAL(10,3))/SUM(HasQuote) END AS CheckedRate
FROM   dbo.InquiryOrder a  INNER JOIN dbo.CGMemQuoted b WITH(NOLOCK) ON a.Code=b.InquiryOrderCode " + where + " GROUP BY b.CGMemId,CONVERT(VARCHAR(10), a.CreateTime, 120) ";
            }
            if (orderby == (int)ReportInquiryCGOrderEnum.TotalNum)
            {
                orderbystr = " Order By TotalNum desc";
            }
            else if (orderby == (int)ReportInquiryCGOrderEnum.CheckedNum)
            {
                orderbystr = " Order By CheckedNum desc";
            }
            else if (orderby == (int)ReportInquiryCGOrderEnum.QuoteNum)
            {
                orderbystr = " Order By QuoteNum desc";
            }
            else if (orderby == (int)ReportInquiryCGOrderEnum.QuoteRate)
            {
                orderbystr = " Order By QuoteRate desc";
            }
            else if (orderby == (int)ReportInquiryCGOrderEnum.CheckedRate)
            {
                orderbystr = " Order By CheckedRate desc";
            }
            else
            {
                orderbystr = " Order By CreateDate desc";
            }

            sql = sql + orderbystr;
            IList<ReportInquiryOrderEntity> entityList = new List<ReportInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@StartDate", DbType.DateTime, startdate);
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, enddate);
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int16, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReportInquiryOrderEntity entity = new ReportInquiryOrderEntity();
                    entity.CreateDate = StringUtils.GetDbString(reader["CreateDate"]); 
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.TotalNum = StringUtils.GetDbInt(reader["TotalNum"]);
                    entity.CheckedNum = StringUtils.GetDbInt(reader["CheckedNum"]);
                    entity.QuoteNum = StringUtils.GetDbInt(reader["QuoteNum"]);
                    entity.QuoteRate = StringUtils.GetDbDecimal(reader["QuoteRate"]);
                    entity.CheckedRate = StringUtils.GetDbDecimal(reader["CheckedRate"]);
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
        public int  ExistNum(InquiryOrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[InquiryOrder] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CarTypeModelName=@CarTypeModelName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CarTypeModelName=@CarTypeModelName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CarTypeModelName", DbType.String, entity.CarTypeModelName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
