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
功能描述：CGMemQuoted表的数据访问类。
创建时间：2017/8/26 11:08:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// CGMemQuotedEntity的数据访问操作
	/// </summary>
	public partial class CGMemQuotedDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGMemQuotedDA Instance
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
            internal static readonly CGMemQuotedDA instance = new CGMemQuotedDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGMemQuoted，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGMemQuoted">待插入的实体对象</param>
		public int AddCGMemQuoted(CGMemQuotedEntity entity)
		{
		   string sql=@"insert into CGMemQuoted( [InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],[QuoteTime],[FromClass],[SendWeChatStatus])VALUES
			            ( @InquiryOrderCode,@CGMemId,@CreateTime,@SendWeChatTime,@ReadTime,@QuoteTime,@FromClass,@SendWeChatStatus);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@SendWeChatTime",  DbType.DateTime,entity.SendWeChatTime);
			db.AddInParameter(cmd,"@ReadTime",  DbType.DateTime,entity.ReadTime);
			db.AddInParameter(cmd,"@QuoteTime",  DbType.DateTime,entity.QuoteTime);
			db.AddInParameter(cmd,"@FromClass",  DbType.Int32,entity.FromClass);
			db.AddInParameter(cmd,"@SendWeChatStatus",  DbType.Int32,entity.SendWeChatStatus);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddInquiryToCGMemQuoted(string ordercode,string memids)
        {
            string sql = @"insert into CGMemQuoted( [InquiryOrderCode],[CGMemId],[CreateTime], [SendWeChatStatus])
SELECT @InquiryOrderCode,a.Id,GETDATE(),@Status FROM dbo.fun_splitstr(@MemIds,',') a LEFT JOIN CGMemQuoted b ON a.id=b.CGMemId AND b.InquiryOrderCode=@InquiryOrderCode WHERE b.CGMemId  IS null AND A.ID>0    
		 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@MemIds", DbType.String, memids);
            db.AddInParameter(cmd, "@Status", DbType.Int32,(int) SendWeChatStatus.WaitSend);
            return db.ExecuteNonQuery(cmd); 
        }
        public int SelectInquiryOrderCGMem(string ordercode, int memid)
        {
            string sql = @" 
		update CGMemQuoted set HasChecked=0 where InquiryOrderCode=@InquiryOrderCode 
update CGMemQuoted set HasChecked=1 where InquiryOrderCode=@InquiryOrderCode and CGMemId=@CGMemId
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@CGMemId", DbType.String, memid); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 旧版选择供应商
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="confirmcode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int SelectConfirmOrderCGMem(string ordercode,string confirmcode, int memid)
        {
            string sql = @" 
		update CGMemQuoted set HasChecked=0 where InquiryOrderCode=@InquiryOrderCode 
update CGMemQuoted set HasChecked=1 where InquiryOrderCode=@InquiryOrderCode and CGMemId=@CGMemId 
UPDATE a SET CGPrice=c.CGPrice FROM   dbo.[ConfirmOrderProduct] a  WITH(NOLOCK)  
INNER JOIN dbo.CGQuotedPrice  c WITH(NOLOCK)  ON c.InquiryOrderCode=@InquiryOrderCode AND a.ProductId=c.InquiryProductId AND a.ProductType=c.InquiryProductType
WHERE  ConfirmOrderCode=@ConfirmOrderCode and c.InquiryOrderCode=@InquiryOrderCode AND c.CGMemId=@CGMemId 
DECLARE @cgtotalprice DECIMAL(12,2)
SELECT @cgtotalprice=SUM(cgprice) FROM [ConfirmOrderProduct] WHERE ConfirmOrderCode=@ConfirmOrderCode
update ConfirmOrder set CGtOTALPRICE=@cgtotalprice,CGMemId=@CGMemId,Status=@ConfirmStatus  WHERE Code=@ConfirmOrderCode
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@ConfirmOrderCode", DbType.String, confirmcode);
            db.AddInParameter(cmd, "@CGMemId", DbType.String, memid);
            db.AddInParameter(cmd, "@ConfirmStatus", DbType.Int32, (int)OrderConfirmStatusEnum.WaitDelivery);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 新版选择供应商，支持多个供应商
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="confirmcode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int SelConfirmOrderCGMem(string ordercode, string confirmcode, string memprices)
        {
               string sql = @"  
SELECT  *  INTO #tempprice FROM  dbo.fun_SplitStrToTable(@ProductPriceStr, '|', '_') 
UPDATE a SET CGPrice=c.CGPrice,CGMemId=c.CGMemId FROM   dbo.[ConfirmOrderProduct] a  WITH(NOLOCK)  
INNER JOIN dbo.CGQuotedPrice  c WITH(NOLOCK)  ON c.InquiryOrderCode=@InquiryOrderCode AND a.ProductId=c.InquiryProductId AND a.ProductType=c.InquiryProductType
inner join #tempprice tp on c.Id=tp.value2  WHERE  ConfirmOrderCode=@ConfirmOrderCode and c.CGMemId=tp.value1
if EXISTS(select top 1 1 from [dbo].[ConfirmOrderCGMem] where ConfirmOrderCode=@ConfirmOrderCode)
begin
delete from [dbo].[ConfirmOrderCGMem] where ConfirmOrderCode=@ConfirmOrderCode
end

INSERT INTO  [dbo].[ConfirmOrderCGMem]
           ([ConfirmOrderCode]
,InquiryOrderCode
           ,[CGMemId]
           ,CGTotalPrice
           ,[SelectTime])
 select @ConfirmOrderCode,@InquiryOrderCode,CGMemId,Sum(CGPrice),getdate() from  [ConfirmOrderProduct] WHERE ConfirmOrderCode=@ConfirmOrderCode  AND CGMemId >0 group by CGMemId
  
 UPDATE a SET HasInStock=b.HasInStock,RemarkByCGMem=b.RemarkByCGMem from  [ConfirmOrderCGMem] a inner join CGMemQuoted b on a.InquiryOrderCode=b.InquiryOrderCode and 
a.CGMemId=b.CGMemId where a.ConfirmOrderCode=@ConfirmOrderCode
  
DECLARE @cgtotalprice DECIMAL(12,2)
SELECT @cgtotalprice=SUM(cgprice) FROM [ConfirmOrderProduct] WHERE ConfirmOrderCode=@ConfirmOrderCode
update ConfirmOrder set CGtOTALPRICE=@cgtotalprice, Status=@ConfirmStatus  WHERE Code=@ConfirmOrderCode
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@ConfirmOrderCode", DbType.String, confirmcode);
            db.AddInParameter(cmd, "@ProductPriceStr", DbType.String, memprices);
            db.AddInParameter(cmd, "@ConfirmStatus", DbType.Int32, (int)OrderConfirmStatusEnum.WaitDelivery);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGMemQuoted">待更新的实体对象</param>
        public int UpdateCGMemQuoted(CGMemQuotedEntity entity)
		{
			string sql=@" UPDATE dbo.[CGMemQuoted] SET
                       [InquiryOrderCode]=@InquiryOrderCode,[CGMemId]=@CGMemId,[CreateTime]=@CreateTime,[SendWeChatTime]=@SendWeChatTime,[ReadTime]=@ReadTime,[QuoteTime]=@QuoteTime,[FromClass]=@FromClass,[SendWeChatStatus]=@SendWeChatStatus
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@SendWeChatTime",  DbType.DateTime,entity.SendWeChatTime);
			db.AddInParameter(cmd,"@ReadTime",  DbType.DateTime,entity.ReadTime);
			db.AddInParameter(cmd,"@QuoteTime",  DbType.DateTime,entity.QuoteTime);
			db.AddInParameter(cmd,"@FromClass",  DbType.Int32,entity.FromClass);
			db.AddInParameter(cmd,"@SendWeChatStatus",  DbType.Int32,entity.SendWeChatStatus);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int QuotedCloseByCode(string ordercode)
        {
            string sql = @" UPDATE dbo.[CGMemQuoted] SET   [SendWeChatStatus]=@SendWeChatStatus
                       WHERE [InquiryOrderCode]=@InquiryOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@SendWeChatStatus", DbType.Int32, (int)SendWeChatStatus.Close); 
            return db.ExecuteNonQuery(cmd);
        }
        public int CGQuotedSend(int cgmemid, string ordercode)
        {
            string sql = @" UPDATE dbo.[CGMemQuoted] SET   HasSend=1 ,[SendWeChatStatus]=@SendWeChatStatus,SendWeChatTime=getdate()
                       WHERE [InquiryOrderCode]=@InquiryOrderCode and CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@SendWeChatStatus", DbType.Int32, (int)SendWeChatStatus.HasSend);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            return db.ExecuteNonQuery(cmd);
        }
        public int CGQuotedFinished(int cgmemid, string ordercode)
        {
            string sql = @" UPDATE dbo.[CGMemQuoted] SET   HasQuote=1 ,[SendWeChatStatus]=@SendWeChatStatus,QuoteTime=getdate()
                       WHERE [InquiryOrderCode]=@InquiryOrderCode and CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@SendWeChatStatus", DbType.Int32, (int)SendWeChatStatus.HasQuote);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteCGMemQuotedByKey(int id)
	    {
			string sql=@"delete from CGMemQuoted where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		/// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGMemQuotedDisabled(string code,int memid)
        {
            string sql = @"update   CGMemQuoted set  HasCGDel=1  where InquiryOrderCode=@InquiryOrderCode and CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGMemQuotedByIds(string ids)
        {
            string sql = @"Delete from CGMemQuoted  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGMemQuotedByIds(string ids)
        {
            string sql = @"Update   CGMemQuoted set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGMemQuotedEntity GetCGMemQuoted(int id)
		{
			string sql= @"SELECT  [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus]
							FROM
							dbo.[CGMemQuoted] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGMemQuotedEntity entity=new CGMemQuotedEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendWeChatTime=StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
					entity.ReadTime=StringUtils.GetDbDateTime(reader["ReadTime"]);
					entity.HasQuote=StringUtils.GetDbInt(reader["HasQuote"]);
					entity.QuoteTime= StringUtils.GetDbDateTime(reader["QuoteTime"]);
					entity.FromClass=StringUtils.GetDbInt(reader["FromClass"]);
					entity.SendWeChatStatus=StringUtils.GetDbInt(reader["SendWeChatStatus"]);
				}
   		    }
            return entity;
		}


        public CGMemQuotedEntity GetCGMemQuotedByCode(string code, int memid)
        {
            string sql = @"SELECT  [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus],HasInStock,RemarkByCGMem
							FROM
							dbo.[CGMemQuoted] WITH(NOLOCK)	
							WHERE [InquiryOrderCode]=@InquiryOrderCode and CGMemId=@CGMemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            CGMemQuotedEntity entity = new CGMemQuotedEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entity.HasInStock = StringUtils.GetDbInt(reader["HasInStock"]);
                    entity.RemarkByCGMem = StringUtils.GetDbString(reader["RemarkByCGMem"]);
                }
            }
            return entity;
        }

        

        public CGMemQuotedEntity GetCGMemHasCheckedByCode(string code)
        {
            string sql = @"SELECT top 1  [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus],HasInStock,RemarkByCGMem
							FROM
							dbo.[CGMemQuoted] WITH(NOLOCK)	
							WHERE [InquiryOrderCode]=@InquiryOrderCode and HasChecked=1 order by id desc";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code); 
            CGMemQuotedEntity entity = new CGMemQuotedEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entity.HasInStock = StringUtils.GetDbInt(reader["HasInStock"]);
                    entity.RemarkByCGMem = StringUtils.GetDbString(reader["RemarkByCGMem"]);
                }
            }
            return entity;
        }
        public string GetQuotedCGMemByCode(string code)
        {
            string memids = "";
            string sql = @"SELECT [CGMemId]  FROM
							dbo.[CGMemQuoted] WITH(NOLOCK)	where InquiryOrderCode=@InquiryOrderCode ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            CGMemQuotedEntity entity = new CGMemQuotedEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    memids += StringUtils.GetDbInt(reader["CGMemId"]) + ","; 
                }
            }
            return memids;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<CGMemQuotedEntity> GetCGMemQuotedList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus] from dbo.[CGMemQuoted] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGMemQuoted] with (nolock) ";
            IList<CGMemQuotedEntity> entityList = new List< CGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGMemQuotedEntity entity=new CGMemQuotedEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendWeChatTime=StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
					entity.ReadTime=StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime=StringUtils.GetDbDateTime(reader["QuoteTime"]);
					entity.FromClass=StringUtils.GetDbInt(reader["FromClass"]);
					entity.SendWeChatStatus=StringUtils.GetDbInt(reader["SendWeChatStatus"]);
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
        public IList<VWCGMemQuotedEntity> GetInquiryCGMemQuotedList(int pagesize, int pageindex, ref int recordCount, string ordercode, int hasread, int hasquote,int status,int cgmemid)
        {
            string where = " where 1=1 ";
            if (!string.IsNullOrEmpty(ordercode))
                   { where += " and InquiryOrderCode=@InquiryOrderCode "; }
            if (hasread != -1)
            { where += " and HasRead=@HasRead "; }
            if (hasquote != -1)
            { where += " and HasQuote=@HasQuote "; }
             if( cgmemid>0)
            {
                where += " and CGMemId=@CGMemId ";
            }
            if (status > 0)
            {
                where += " and SendWeChatStatus=@SendWeChatStatus ";
            }


            string sql = @"SELECT   [Id],[InquiryOrderCode],[CGMemId],HasSend,HasRead,HasQuote,[CreateTime],[SendWeChatTime],[ReadTime],[QuoteTime],[FromClass],[SendWeChatStatus]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[InquiryOrderCode],[CGMemId],HasSend,HasRead,HasQuote,[CreateTime],[SendWeChatTime],[ReadTime],[QuoteTime],[FromClass],[SendWeChatStatus] from dbo.[CGMemQuoted] WITH(NOLOCK)	
						" + where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[CGMemQuoted] with (nolock) "+where ;
            IList<VWCGMemQuotedEntity> entityList = new List<VWCGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(ordercode))
            {  
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            }
            if (hasread != -1)
            {  
                db.AddInParameter(cmd, "@HasRead", DbType.Int32, hasread);
            }
            if (hasquote != -1)
            { 
                db.AddInParameter(cmd, "@HasQuote", DbType.Int32, hasquote);
            }
            if (cgmemid > 0)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            if (status > 0)
            { 
                db.AddInParameter(cmd, "@SendWeChatStatus", DbType.Int32, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGMemQuotedEntity entity = new VWCGMemQuotedEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]); 
                    entity.HasSend = StringUtils.GetDbInt(reader["HasSend"]);  
                    entity.HasRead = StringUtils.GetDbInt(reader["HasRead"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);  
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(ordercode))
            {
                db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            }
            if (hasread != -1)
            {
                db.AddInParameter(cmd, "@HasRead", DbType.Int32, hasread);
            }
            if (hasquote != -1)
            {
                db.AddInParameter(cmd, "@HasQuote", DbType.Int32, hasquote);
            }
            if (cgmemid > 0)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            if (status > 0)
            {
                db.AddInParameter(cmd, "@SendWeChatStatus", DbType.Int32, status);
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
        public IList<CGMemQuotedEntity> GetCGMemQuotedListSend(int num)
        {
            string sql = @" SELECT top "+ num + @" [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],
[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus] into #temp from dbo.[CGMemQuoted] WITH(NOLOCK)	
						WHERE  SendWeChatStatus=@WaitSendStatus order by id desc
update a set Status=@SendingStatus from  dbo.[CGMemQuoted] a inner join #temp b on a.Id=b.Id where a.InquiryOrderCode=b.InquiryOrderCode;
drop table #temp;";
             
            IList<CGMemQuotedEntity> entityList = new List<CGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);  
		    db.AddInParameter(cmd, "@SendingStatus", DbType.Int32, (int)SendWeChatStatus.Sending);
		    db.AddInParameter(cmd, "@WaitSendStatus", DbType.Int32, (int)SendWeChatStatus.WaitSend);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGMemQuotedEntity entity = new CGMemQuotedEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entityList.Add(entity);
                }
            } 
            return entityList;
        }

        public IList<CGMemQuotedEntity> GetCGMemQuotedNeedSend(string code)
        {
            string sql = @"SELECT  [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],
[ReadTime],HasQuote,HasSend,[QuoteTime],[FromClass],[SendWeChatStatus] from  dbo.[CGMemQuoted]  WITH(NOLOCK)	
						WHERE  InquiryOrderCode=@InquiryOrderCode  order by id desc "; 
            IList<CGMemQuotedEntity> entityList = new List<CGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGMemQuotedEntity entity = new CGMemQuotedEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.HasSend = StringUtils.GetDbInt(reader["HasSend"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public int GetCGMemQuotedByStatus(string ordercode, int status)
        {
            string sql = @"Select count(1) from dbo.[CGMemQuoted] WITH(NOLOCK) where InquiryOrderCode=@InquiryOrderCode and SendWeChatStatus=@SendWeChatStatus";
            
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@SendWeChatStatus", DbType.Int16, status);
            
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public int GetCGMemNotQuoted(string ordercode)
        {
            string sql = @"Select count(1) from dbo.[CGMemQuoted] WITH(NOLOCK) where InquiryOrderCode=@InquiryOrderCode and HasQuote=0";

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        public IList<CGMemQuotedEntity> GetCGMemQuotedAllByCode(string ordercode)
        {

            string sql = @" SELECT [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],
[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus],HasChecked,HasInStock,RemarkByCGMem  from dbo.[CGMemQuoted] WITH(NOLOCK)	
						WHERE  InquiryOrderCode=@InquiryOrderCode order by CGMemId desc
 "; 
            IList<CGMemQuotedEntity> entityList = new List<CGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGMemQuotedEntity entity = new CGMemQuotedEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.HasInStock = StringUtils.GetDbInt(reader["HasInStock"]);
                    entity.RemarkByCGMem = StringUtils.GetDbString(reader["RemarkByCGMem"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }
        public IList<VWCGMemQuotedEntity> GetVWCGMemQuotedAllByCode(string ordercode)
        {

            string sql = @" SELECT [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],
[ReadTime],[QuoteTime],[FromClass],[SendWeChatStatus],HasChecked,HasInStock,RemarkByCGMem  from dbo.[CGMemQuoted] WITH(NOLOCK)	
						WHERE  InquiryOrderCode=@InquiryOrderCode order by CGMemId desc
 ";
            IList<VWCGMemQuotedEntity> entityList = new List<VWCGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGMemQuotedEntity entity = new VWCGMemQuotedEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.HasInStock = StringUtils.GetDbInt(reader["HasInStock"]);
                    entity.RemarkByCGMem = StringUtils.GetDbString(reader["RemarkByCGMem"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public CGMemQuotedEntity GetHasCheckedCGMem(string ordercode)
        {
            string sql = @" SELECT top 1 [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],
[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus],HasChecked  from dbo.[CGMemQuoted] WITH(NOLOCK)	
						WHERE  InquiryOrderCode=@InquiryOrderCode and HasChecked=1 order by Id  desc
 ";
             CGMemQuotedEntity entity = new  CGMemQuotedEntity();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                { 
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.SendWeChatTime = StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
                    entity.ReadTime = StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime = StringUtils.GetDbDateTime(reader["QuoteTime"]);
                    entity.FromClass = StringUtils.GetDbInt(reader["FromClass"]);
                    entity.SendWeChatStatus = StringUtils.GetDbInt(reader["SendWeChatStatus"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
               
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGMemQuotedEntity> GetCGMemQuotedAll()
        {

            string sql = @"SELECT    [Id],[InquiryOrderCode],[CGMemId],[CreateTime],[SendWeChatTime],[ReadTime],HasQuote,[QuoteTime],[FromClass],[SendWeChatStatus] from dbo.[CGMemQuoted] WITH(NOLOCK)	";
		    IList<CGMemQuotedEntity> entityList = new List<CGMemQuotedEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGMemQuotedEntity entity=new CGMemQuotedEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.SendWeChatTime=StringUtils.GetDbDateTime(reader["SendWeChatTime"]);
					entity.ReadTime=StringUtils.GetDbDateTime(reader["ReadTime"]);
                    entity.HasQuote = StringUtils.GetDbInt(reader["HasQuote"]);
                    entity.QuoteTime=StringUtils.GetDbDateTime(reader["QuoteTime"]);
					entity.FromClass=StringUtils.GetDbInt(reader["FromClass"]);
					entity.SendWeChatStatus=StringUtils.GetDbInt(reader["SendWeChatStatus"]);
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
        public int  ExistNum(CGMemQuotedEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGMemQuoted] WITH(NOLOCK) ";
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
