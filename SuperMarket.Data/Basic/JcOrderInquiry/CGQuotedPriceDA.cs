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
功能描述：CGQuotedPrice表的数据访问类。
创建时间：2017/8/23 11:12:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// CGQuotedPriceEntity的数据访问操作
	/// </summary>
	public partial class CGQuotedPriceDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGQuotedPriceDA Instance
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
            internal static readonly CGQuotedPriceDA instance = new CGQuotedPriceDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGQuotedPrice，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGQuotedPrice">待插入的实体对象</param>
		public int AddCGQuotedPrice(CGQuotedPriceEntity entity)
		{
		   string sql=@"insert into CGQuotedPrice( [InquiryOrderCode],[InquiryProductId],[CGMemId],[CGPrice],[Price],[InquiryProductType],[InquiryProductDes],[Remark],[HasSelected],[QuotedTime],[CheckMemId],[CheckTime])VALUES
			            ( @InquiryOrderCode,@InquiryProductId,@CGMemId,@CGPrice,@Price,@InquiryProductType,@InquiryProductDes,@Remark,@HasSelected,@QuotedTime,@CheckMemId,@CheckTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@InquiryProductId",  DbType.Int32,entity.InquiryProductId);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CGPrice",  DbType.Decimal,entity.CGPrice);
			db.AddInParameter(cmd,"@Price",  DbType.Decimal,entity.Price);
			db.AddInParameter(cmd,"@InquiryProductType",  DbType.Int32,entity.InquiryProductType);
			db.AddInParameter(cmd,"@InquiryProductDes",  DbType.String,entity.InquiryProductDes);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@HasSelected",  DbType.Int32,entity.HasSelected);
			db.AddInParameter(cmd,"@QuotedTime",  DbType.DateTime,entity.QuotedTime);
			db.AddInParameter(cmd,"@CheckMemId",  DbType.Int32,entity.CheckMemId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.String,entity.CheckTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}

        public int QuotedPriceCG(string ordercode, int hasinstock, string cgmemremark, int cgmemid,string prices)
        {
            string sql = @"
SELECT  *
INTO    #tempp
FROM    dbo.fun_SplitStrToTable(@PriceStr, '|', '_')
IF   EXISTS(SELECT TOP 1  1 FROM CGQuotedPrice WHERE CGMemId=@CGMemId AND [InquiryOrderCode]=@InquiryOrderCode)
BEGIN
  UPDATE a  SET [CGPrice]=b.value2,Price=b.value3 ,[InquiryProductDes]=b.value4 FROM CGQuotedPrice a inner JOIN #tempp b ON  a.InquiryProductSubId=b.value1 WHERE a.[InquiryOrderCode]=@InquiryOrderCode AND a.CGMemId=@CGMemId 
  delete b  FROM CGQuotedPrice a inner JOIN #tempp b ON  a.InquiryProductSubId=b.value1 WHERE a.[InquiryOrderCode]=@InquiryOrderCode AND a.CGMemId=@CGMemId 

END
              
INSERT  INTO CGQuotedPrice
        ( [InquiryOrderCode] ,
          [InquiryProductId] ,
          InquiryProductSubId ,
          [CGMemId] ,
          [CGPrice] ,
          [Price] ,
          [InquiryProductType] ,
          [InquiryProductDes] ,
          [Remark] ,
          [HasSelected] ,
          [QuotedTime]
        )
        SELECT  b.InquiryOrderCode ,
                b.InquiryProductId ,
                b.Id ,
                @CGMemId ,
                a.Value2 ,
                a.Value3 ,
                b.InquiryProductType ,
                a.Value4 ,
                '' ,
                0 ,
                GETDATE()
        FROM    #tempp a WITH ( NOLOCK )
                inner JOIN InquiryProductSub b WITH ( NOLOCK ) ON a.value1 = b.Id  WHERE b.[InquiryOrderCode]=@InquiryOrderCode 
  
IF EXISTS ( SELECT  1
            FROM    CGMemQuoted
            WHERE   CGMemId = @CGMemId
                    AND InquiryOrderCode = @InquiryOrderCode ) 
    BEGIN 
        UPDATE  CGMemQuoted
        SET     HasQuote=1,QuoteTime = GETDATE(),HasInStock=@HasInStock,RemarkByCGMem=@RemarkByCGMem
        WHERE   CGMemId = @CGMemId
                AND InquiryOrderCode = @InquiryOrderCode
 
    END
ELSE 
    BEGIN
        INSERT  INTO dbo.CGMemQuoted
                ( InquiryOrderCode ,
                  CGMemId , 
                  CreateTime ,
                  HasQuote,
                  HasInStock,
                  RemarkByCGMem,
                  SendWeChatTime ,
                  ReadTime ,
                  QuoteTime ,
                  FromClass ,
                  SendWeChatStatus
                )
        VALUES  ( @InquiryOrderCode ,
                  @CGMemId ,
                  GETDATE() ,
                  1,
                  @HasInStock,
                  @RemarkByCGMem ,
                  GETDATE() ,
                  GETDATE() ,
                  GETDATE() ,
                  0 ,
                  1
                )
    END
DROP TABLE #tempp

";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            db.AddInParameter(cmd, "@PriceStr", DbType.String, prices);
            db.AddInParameter(cmd, "@RemarkByCGMem", DbType.String, cgmemremark);
            db.AddInParameter(cmd, "@HasInStock", DbType.String, hasinstock);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            return db.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGQuotedPrice">待更新的实体对象</param>
        public   int UpdateCGQuotedPrice(CGQuotedPriceEntity entity)
		{
			string sql=@" UPDATE dbo.[CGQuotedPrice] SET
                       [InquiryOrderCode]=@InquiryOrderCode,[InquiryProductId]=@InquiryProductId,[CGMemId]=@CGMemId,[CGPrice]=@CGPrice,[Price]=@Price,[InquiryProductType]=@InquiryProductType,[InquiryProductDes]=@InquiryProductDes,[Remark]=@Remark,[HasSelected]=@HasSelected,[QuotedTime]=@QuotedTime,[CheckMemId]=@CheckMemId,[CheckTime]=@CheckTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@InquiryProductId",  DbType.Int32,entity.InquiryProductId);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CGPrice",  DbType.Decimal,entity.CGPrice);
			db.AddInParameter(cmd,"@Price",  DbType.Decimal,entity.Price);
			db.AddInParameter(cmd,"@InquiryProductType",  DbType.Int32,entity.InquiryProductType);
			db.AddInParameter(cmd,"@InquiryProductDes",  DbType.String,entity.InquiryProductDes);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@HasSelected",  DbType.Int32,entity.HasSelected);
			db.AddInParameter(cmd,"@QuotedTime",  DbType.DateTime,entity.QuotedTime);
			db.AddInParameter(cmd,"@CheckMemId",  DbType.Int32,entity.CheckMemId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.String,entity.CheckTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGQuotedPriceByKey(int id)
	    {
			string sql=@"delete from CGQuotedPrice where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGQuotedPriceDisabled()
        {
            string sql = @"delete from  CGQuotedPrice  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGQuotedPriceByIds(string ids)
        {
            string sql = @"Delete from CGQuotedPrice  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGQuotedPriceByIds(string ids)
        {
            string sql = @"Update   CGQuotedPrice set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGQuotedPriceEntity GetCGQuotedPrice(int id)
		{
			string sql=@"SELECT  [Id],[InquiryOrderCode],[InquiryProductId],[CGMemId],[CGPrice],[Price],[InquiryProductType],[InquiryProductDes],[Remark],[HasSelected],[QuotedTime],[CheckMemId],[CheckTime]
							FROM
							dbo.[CGQuotedPrice] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGQuotedPriceEntity entity=new CGQuotedPriceEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.InquiryProductId=StringUtils.GetDbInt(reader["InquiryProductId"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CGPrice=StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Price=StringUtils.GetDbDecimal(reader["Price"]);
					entity.InquiryProductType=StringUtils.GetDbInt(reader["InquiryProductType"]);
					entity.InquiryProductDes=StringUtils.GetDbString(reader["InquiryProductDes"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.HasSelected=StringUtils.GetDbInt(reader["HasSelected"]);
					entity.QuotedTime=StringUtils.GetDbDateTime(reader["QuotedTime"]);
					entity.CheckMemId=StringUtils.GetDbInt(reader["CheckMemId"]);
					entity.CheckTime=StringUtils.GetDbString(reader["CheckTime"]);
				}
   		    }
            return entity;
		}
        public int UserSelectQuote(string ordercode, string confirmcode, string productselectstr)
        {
//string sql = @" SELECT *  INTO #tempp FROM  dbo.fun_SplitStrToTable(@ProductSelectStr, '|', '_')
//update a set HasSelected=1 from CGQuotedPrice a inner join #tempp b on a.Id=b.value2  where a.InquiryOrderCode=@InquiryOrderCode

//  DECLARE @cgtotalprice decimal(12,2)
//   DECLARE  @totalprice decimal(12,2) 
//select @cgtotalprice=sum(CGPrice),@totalprice= sum(Price) FROM CGQuotedPrice WHERE  HasSelected=1  AND InquiryOrderCode=@InquiryOrderCode
//update InquiryOrder set Status=@Status,CGTotalPrice=@cgtotalprice,TotalPrice=@totalprice where Code=@InquiryOrderCode
//";
string sql = @" SELECT  *  INTO #tempp FROM  dbo.fun_SplitStrToTable(@ProductSelectStr, '|', '_')
 
DECLARE  @totalprice decimal(12,2) 
select @totalprice= sum(Price) FROM dbo.InquiryProductSub a inner join #tempp b on a.Id=b.value2  WHERE   InquiryOrderCode=@InquiryOrderCode
update InquiryOrder set Status=@InquiryStatus,HasConfirm=1,StatusForMem=@InquiryStatusForMem  where Code=@InquiryOrderCode
INSERT INTO [JcOrderInquiry].[dbo].[ConfirmOrder]
           ([Code]
           ,[InquiryOrderCode]
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
           ,[Remark]
           ,[NeedDay] 
           ,[MemId]
           ,[CreateManId]
           ,[CreateTime]
           ,[Status]
           ,[StatusForMem]  
           ,[TotalPrice],ScopeType ) 
SELECT @ConfirmOrderCode, [Code] 
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
           ,[Remark]
           ,[NeedDay] 
           ,[MemId]
           ,[MemId]
           ,GETDATE()
           ,@ConfirmStatus
           ,@StatusForMem 
            ,@totalprice,ScopeType
            FROM [dbo].[InquiryOrder] where Code=@InquiryOrderCode
INSERT INTO  [dbo].[ConfirmOrderProduct]
           ([ConfirmOrderCode]
           ,[InquiryOrderCode]
           ,[ProductCode] 
           ,[ProductId]
           ,[ProductName]
           ,[ProductType]
           ,[ProductNum]
           ,[ProductUnitName]
           ,[Remark] 
           ,[CreateTime] 
           ,[Price])
SELECT @ConfirmOrderCode,@InquiryOrderCode,b.[ProductCode] 
           ,b.[ProductId]
           ,b.[ProductName]
           ,a.InquiryProductType
           ,b.[ProductNum]
           ,b.[ProductUnitName]
           ,b.[Remark] 
           ,GETDATE()
           ,a.[Price] FROM dbo.InquiryProductSub a with(nolock) INNER JOIN dbo.InquiryProduct b with(nolock) ON a.InquiryProductId=b.ProductId AND a.InquiryOrderCode=b.InquiryOrderCode 
inner join #tempp c on b.Id=c.value1 and a.id=c.value2 where  b.InquiryOrderCode=@InquiryOrderCode
INSERT INTO [JcOrderInquiry].[dbo].[ConfirmOrderMember]
           ([ConfirmOrderCode]
           ,[InquiryOrderCode]
           ,[MemId]
           ,[MemName]
           ,[MemPhone]
           ,[CompanyAddress]
           ,[CompanyName])
 SELECT   @ConfirmOrderCode,[InquiryOrderCode]
      ,[MemId]
      ,[MemName]
      ,[MemPhone]
      ,[CompanyAddress]
      ,[CompanyName]
  FROM [JcOrderInquiry].[dbo].[InquiryOrderMember] where InquiryOrderCode=@InquiryOrderCode
";
            //            update a set SelectProductType=c.InquiryProductType,Price=c.price from InquiryProduct a inner join #tempp b on a.Id=b.value1 inner join CGQuotedPrice c 
            //on b.value2=c.id where a.InquiryOrderCode=@InquiryOrderCode
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ProductSelectStr", DbType.String, productselectstr);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@ConfirmOrderCode", DbType.String, confirmcode);
            db.AddInParameter(cmd, "@ConfirmStatus", DbType.Int16, (int)OrderConfirmStatusEnum.WaitSelectCGMem);//付款环节暂时省掉
            db.AddInParameter(cmd, "@InquiryStatus", DbType.Int16, (int)OrderInquiryStatusEnum.HasAccept); //选择产品已产生订单后
            db.AddInParameter(cmd, "@InquiryStatusForMem", DbType.Int16, (int)InquiryStatusForMemEnum.Finished); //选择产品已产生订单后
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int16, (int)ConfirmStatusForMemEnum.WaitDelivery);//付款环节暂时省掉
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 选择询价虚拟供应商
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="prices"></param>
        /// <returns></returns>
        public int InquirySelectQuote(string ordercode, string prices)
        {
            string sql = @" SELECT  *  INTO #tempprice FROM  dbo.fun_SplitStrToTable(@ProductPriceStr, '|', '_') 
 update CGQuotedPrice set HasSelected=0  where InquiryOrderCode=@InquiryOrderCode
 update a set HasSelected=1 from   CGQuotedPrice a inner join #tempprice b on a.CGMemId=b.value1 and a.Id=b.value2 where a.InquiryOrderCode=@InquiryOrderCode
";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode); 
            db.AddInParameter(cmd, "@ProductPriceStr", DbType.String, prices);//付款环节暂时省掉
             return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<CGQuotedPriceEntity> GetCGQuotedPriceList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[InquiryOrderCode],[InquiryProductId],[CGMemId],[CGPrice],[Price],[InquiryProductType],[InquiryProductDes],[Remark],[HasSelected],[QuotedTime],[CheckMemId],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[InquiryOrderCode],[InquiryProductId],[CGMemId],[CGPrice],[Price],[InquiryProductType],[InquiryProductDes],[Remark],[HasSelected],[QuotedTime],[CheckMemId],[CheckTime] from dbo.[CGQuotedPrice] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGQuotedPrice] with (nolock) ";
            IList<CGQuotedPriceEntity> entityList = new List< CGQuotedPriceEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGQuotedPriceEntity entity=new CGQuotedPriceEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.InquiryProductId=StringUtils.GetDbInt(reader["InquiryProductId"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CGPrice=StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Price=StringUtils.GetDbDecimal(reader["Price"]);
					entity.InquiryProductType=StringUtils.GetDbInt(reader["InquiryProductType"]);
					entity.InquiryProductDes=StringUtils.GetDbString(reader["InquiryProductDes"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.HasSelected=StringUtils.GetDbInt(reader["HasSelected"]);
					entity.QuotedTime=StringUtils.GetDbDateTime(reader["QuotedTime"]);
					entity.CheckMemId=StringUtils.GetDbInt(reader["CheckMemId"]);
					entity.CheckTime=StringUtils.GetDbString(reader["CheckTime"]);
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
        public IList<CGQuotedPriceEntity> GetCGQuotedPriceAll(string ordercode,int cgmemid,bool hascgprice, bool hasprice, bool hasselect)
        {
            string  where = " where 1=1 ";
            if (!string.IsNullOrEmpty(ordercode))
            {
                where += " and InquiryOrderCode=@InquiryOrderCode ";
            }
            if (cgmemid != -1)
            {
                where += " and CGMemId=@CGMemId ";
            }
            if(hascgprice)
            {
                where += " and CGPrice>0 ";
            }
            if (hasprice)
            {
                where += " and Price>0 ";
            }
            if (hasselect)
            {
                where += " and HasSelected=1 ";
            }
            string sql = @"SELECT    [Id],[InquiryOrderCode],[InquiryProductId],[InquiryProductSubId],[CGMemId],[CGPrice],[Price],[InquiryProductType],[InquiryProductDes],[Remark],[HasSelected],[QuotedTime],[CheckMemId],
[CheckTime] from dbo.[CGQuotedPrice] WITH(NOLOCK)   	" + where;
		    IList<CGQuotedPriceEntity> entityList = new List<CGQuotedPriceEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(ordercode))
            {
                db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            }
            if (cgmemid != -1)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGQuotedPriceEntity entity=new CGQuotedPriceEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.InquiryProductId=StringUtils.GetDbInt(reader["InquiryProductId"]);
					entity.InquiryProductSubId = StringUtils.GetDbInt(reader["InquiryProductSubId"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CGPrice=StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Price=StringUtils.GetDbDecimal(reader["Price"]);
					entity.InquiryProductType=StringUtils.GetDbInt(reader["InquiryProductType"]);
					entity.InquiryProductDes=StringUtils.GetDbString(reader["InquiryProductDes"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.HasSelected=StringUtils.GetDbInt(reader["HasSelected"]);
					entity.QuotedTime=StringUtils.GetDbDateTime(reader["QuotedTime"]);
					entity.CheckMemId=StringUtils.GetDbInt(reader["CheckMemId"]);
					entity.CheckTime=StringUtils.GetDbString(reader["CheckTime"]);
				    entityList.Add(entity);
                }
            } 
            return entityList;
        }
        public IList<VWCGQuotedPriceEntity> GetVWCGQuotedPriceAll(string ordercode, int cgmemid, bool hascgprice, bool hasprice = false, bool hasselected = false)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(ordercode))
            {
                where += " and InquiryOrderCode=@InquiryOrderCode ";
            }
            if (cgmemid != -1)
            {
                where += " and CGMemId=@CGMemId ";
            }
            if (hascgprice)
            {
                where += " and CGPrice>0 ";
            }
            if (hasprice)
            {
                where += " and Price>0 ";
            }
            if (hasselected)
            {
                where += " and HasSelected=1 ";
            }
            string sql = @"SELECT    a.[Id],a.[InquiryOrderCode],a.[InquiryProductId],a.[InquiryProductSubId],a.[CGMemId],a.[CGPrice],a.[Price],
a.[InquiryProductType],a.[InquiryProductDes],a.[Remark],a.[HasSelected],a.[QuotedTime],a.[CheckMemId],
a.[CheckTime] from dbo.[CGQuotedPrice] A WITH(NOLOCK) 	" + where;
            IList<VWCGQuotedPriceEntity> entityList = new List<VWCGQuotedPriceEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(ordercode))
            {
                db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            }
            if (cgmemid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGQuotedPriceEntity entity = new VWCGQuotedPriceEntity(); 
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.InquiryProductId = StringUtils.GetDbInt(reader["InquiryProductId"]);
                    entity.InquiryProductSubId = StringUtils.GetDbInt(reader["InquiryProductSubId"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    entity.InquiryProductType = StringUtils.GetDbInt(reader["InquiryProductType"]);
                    entity.InquiryProductDes = StringUtils.GetDbString(reader["InquiryProductDes"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.HasSelected = StringUtils.GetDbInt(reader["HasSelected"]);
                    entity.QuotedTime = StringUtils.GetDbDateTime(reader["QuotedTime"]);
                    entity.CheckMemId = StringUtils.GetDbInt(reader["CheckMemId"]);
                    entity.CheckTime = StringUtils.GetDbString(reader["CheckTime"]);
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
        public int  ExistNum(CGQuotedPriceEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGQuotedPrice] WITH(NOLOCK) ";
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
