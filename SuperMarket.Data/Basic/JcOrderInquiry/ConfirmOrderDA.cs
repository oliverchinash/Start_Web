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
功能描述：ConfirmOrder表的数据访问类。
创建时间：2017/10/12 14:15:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// ConfirmOrderEntity的数据访问操作
	/// </summary>
	public partial class ConfirmOrderDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ConfirmOrderDA Instance
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
            internal static readonly ConfirmOrderDA instance = new ConfirmOrderDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ConfirmOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="confirmOrder">待插入的实体对象</param>
		public int AddConfirmOrder(ConfirmOrderEntity entity)
		{
		   string sql= @"insert into ConfirmOrder( [Code],[VinNo],[VinPic],[EngineModelNo],[EngineModelPic],[CarBrandId],[CarBrandName],[CarSeriesId],[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[NeedDay],[WLRemark],[CGTotalPrice],[TotalPrice],[MemId], [CreateManId],[CreateTime],[Status],[CancelReasonId],[CancelRemark])VALUES
			            ( @Code,@VinNo,@VinPic,@EngineModelNo,@EngineModelPic,@CarBrandId,@CarBrandName,@CarSeriesId,@CarSeriesName,@CarTypeModelId,@CarTypeModelName,@Remark,@NeedDay,@WLRemark,@CGTotalPrice,@TotalPrice,@MemId, @CreateManId,@CreateTime,@Status,@CancelReasonId,@CancelRemark);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@VinNo",  DbType.String,entity.VinNo);
			db.AddInParameter(cmd,"@VinPic",  DbType.String,entity.VinPic);
            db.AddInParameter(cmd,"@EngineModelNo", DbType.String, entity.EngineModelNo);
            db.AddInParameter(cmd,"@EngineModelPic", DbType.String, entity.EngineModelPic);
            db.AddInParameter(cmd,"@CarBrandId",  DbType.Int32,entity.CarBrandId);
			db.AddInParameter(cmd,"@CarBrandName",  DbType.String,entity.CarBrandName);
			db.AddInParameter(cmd,"@CarSeriesId",  DbType.Int32,entity.CarSeriesId);
			db.AddInParameter(cmd,"@CarSeriesName",  DbType.String,entity.CarSeriesName);
			db.AddInParameter(cmd,"@CarTypeModelId",  DbType.Int32,entity.CarTypeModelId);
			db.AddInParameter(cmd,"@CarTypeModelName",  DbType.String,entity.CarTypeModelName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@NeedDay",  DbType.Int32,entity.NeedDay);
			db.AddInParameter(cmd,"@WLRemark",  DbType.String,entity.WLRemark);
			db.AddInParameter(cmd,"@CGTotalPrice",  DbType.Decimal,entity.CGTotalPrice);
			db.AddInParameter(cmd,"@TotalPrice",  DbType.Decimal,entity.TotalPrice);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId); 
            db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CancelReasonId",  DbType.Int32,entity.CancelReasonId);
			db.AddInParameter(cmd,"@CancelRemark",  DbType.String,entity.CancelRemark);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="confirmOrder">待更新的实体对象</param>
		public   int UpdateConfirmOrder(ConfirmOrderEntity entity)
		{
			string sql= @" UPDATE dbo.[ConfirmOrder] SET
                       [Code]=@Code,[VinNo]=@VinNo,[VinPic]=@VinPic,[EngineModelNo]=@EngineModelNo,[EngineModelPic]=@EngineModelPic,[CarBrandId]=@CarBrandId,[CarBrandName]=@CarBrandName,[CarSeriesId]=@CarSeriesId,[CarSeriesName]=@CarSeriesName,[CarTypeModelId]=@CarTypeModelId,[CarTypeModelName]=@CarTypeModelName,[Remark]=@Remark,[NeedDay]=@NeedDay,[WLRemark]=@WLRemark,[CGTotalPrice]=@CGTotalPrice,[TotalPrice]=@TotalPrice,[MemId]=@MemId, [CreateManId]=@CreateManId,[CreateTime]=@CreateTime,[Status]=@Status,[CancelReasonId]=@CancelReasonId,[CancelRemark]=@CancelRemark
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@VinNo",  DbType.String,entity.VinNo);
			db.AddInParameter(cmd,"@VinPic",  DbType.String,entity.VinPic);
            db.AddInParameter(cmd, "@EngineModelNo", DbType.String, entity.EngineModelNo);
            db.AddInParameter(cmd, "@EngineModelPic", DbType.String, entity.EngineModelPic);
            db.AddInParameter(cmd,"@CarBrandId",  DbType.Int32,entity.CarBrandId);
			db.AddInParameter(cmd,"@CarBrandName",  DbType.String,entity.CarBrandName);
			db.AddInParameter(cmd,"@CarSeriesId",  DbType.Int32,entity.CarSeriesId);
			db.AddInParameter(cmd,"@CarSeriesName",  DbType.String,entity.CarSeriesName);
			db.AddInParameter(cmd,"@CarTypeModelId",  DbType.Int32,entity.CarTypeModelId);
			db.AddInParameter(cmd,"@CarTypeModelName",  DbType.String,entity.CarTypeModelName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@NeedDay",  DbType.Int32,entity.NeedDay);
			db.AddInParameter(cmd,"@WLRemark",  DbType.String,entity.WLRemark);
			db.AddInParameter(cmd,"@CGTotalPrice",  DbType.Decimal,entity.CGTotalPrice);
			db.AddInParameter(cmd,"@TotalPrice",  DbType.Decimal,entity.TotalPrice);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId); 
            db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			db.AddInParameter(cmd,"@CancelReasonId",  DbType.Int32,entity.CancelReasonId);
			db.AddInParameter(cmd,"@CancelRemark",  DbType.String,entity.CancelRemark);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteConfirmOrderByKey(int id)
	    {
			string sql=@"delete from ConfirmOrder where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfirmOrderDisabled()
        {
            string sql = @"delete from  ConfirmOrder  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfirmOrderByIds(string ids)
        {
            string sql = @"Delete from ConfirmOrder  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfirmOrderByIds(string ids)
        {
            string sql = @"Update   ConfirmOrder set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ConfirmOrderEntity GetConfirmOrder(int id)
		{
			string sql= @"SELECT  [Id],[Code],[VinNo],[VinPic],EngineModelNo,EngineModelPic,[CarBrandId],[CarBrandName],[CarSeriesId],[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[NeedDay],[WLRemark],[CGTotalPrice],[TotalPrice],[MemId], [CreateManId],[CreateTime],[Status],[CancelReasonId],[CancelRemark],ScopeType
							FROM
							dbo.[ConfirmOrder] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ConfirmOrderEntity entity=new ConfirmOrderEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.VinNo=StringUtils.GetDbString(reader["VinNo"]);
					entity.VinPic=StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);
                    entity.CarBrandId=StringUtils.GetDbInt(reader["CarBrandId"]);
					entity.CarBrandName=StringUtils.GetDbString(reader["CarBrandName"]);
					entity.CarSeriesId=StringUtils.GetDbInt(reader["CarSeriesId"]);
					entity.CarSeriesName=StringUtils.GetDbString(reader["CarSeriesName"]);
					entity.CarTypeModelId=StringUtils.GetDbInt(reader["CarTypeModelId"]);
					entity.CarTypeModelName=StringUtils.GetDbString(reader["CarTypeModelName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.NeedDay=StringUtils.GetDbInt(reader["NeedDay"]);
					entity.WLRemark=StringUtils.GetDbString(reader["WLRemark"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.TotalPrice=StringUtils.GetDbDecimal(reader["TotalPrice"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]); 
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ScopeType=StringUtils.GetDbInt(reader["ScopeType"]);
                    entity.CancelReasonId=StringUtils.GetDbInt(reader["CancelReasonId"]);
					entity.CancelRemark=StringUtils.GetDbString(reader["CancelRemark"]);
				}
   		    }
            return entity;
		}
        public ConfirmOrderEntity GetConfirmOrderByCode(string code)
        {
            string sql = @"SELECT  [Id],[Code],InquiryOrderCode,[VinNo],[VinPic],EngineModelNo,EngineModelPic,[CarBrandId],[CarBrandName],[CarSeriesId],[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[NeedDay],WLMemId,[WLRemark],[CGTotalPrice],[TotalPrice],[MemId], [CreateManId],[CreateTime],[Status],[CancelReasonId],[CancelRemark],ScopeType
							FROM
							dbo.[ConfirmOrder] WITH(NOLOCK)	
							WHERE [Code]=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, code);
            ConfirmOrderEntity entity = new ConfirmOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
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
                    entity.NeedDay = StringUtils.GetDbInt(reader["NeedDay"]);
                    entity.WLMemId = StringUtils.GetDbInt(reader["WLMemId"]);
                    entity.WLRemark = StringUtils.GetDbString(reader["WLRemark"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]); 
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
                    entity.CancelReasonId = StringUtils.GetDbInt(reader["CancelReasonId"]);
                    entity.CancelRemark = StringUtils.GetDbString(reader["CancelRemark"]);
                }
            }
            return entity;
        }

        public VWConfirmOrderEntity GetVWConfirmOrderByCode(string code)
        {
            string sql = @"SELECT  a.Id,a.[Code],a.InquiryOrderCode,a.[VinNo],a.VinPic,a.[EngineModelNo],a.EngineModelPic,a.CarBrandId,a.CarBrandName,a.CarSeriesId,a.CarSeriesName,[CarTypeModelId],[CarTypeModelName],
[Remark],[WLMemId],[WLRemark],NeedDay,CGTotalPrice,TotalPrice,[CreateManId],[CreateTime], a.[Status],a.StatusForMem,a.ScopeType, b.MemId, b.[MemName]
      ,b.[MemPhone]
      ,b.[CompanyAddress]
      ,b.[CompanyName] from dbo.[ConfirmOrder] a WITH(NOLOCK) INNER JOIN ConfirmOrderMember b WITH(NOLOCK) 
ON a.[InquiryOrderCode]=b.[InquiryOrderCode] 	
							WHERE a.[Code]=@Code";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@Code", DbType.String, code);
            VWConfirmOrderEntity entity = new VWConfirmOrderEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                { 
                    entity.ConfirmOrderId = StringUtils.GetDbInt(reader["Id"]);
                    entity.ConfirmOrderCode = StringUtils.GetDbString(reader["Code"]);
                    entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
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
                    entity.NeedDay = StringUtils.GetDbInt(reader["NeedDay"]);
                    entity.WLMemId = StringUtils.GetDbInt(reader["WLMemId"]);
                    entity.WLRemark = StringUtils.GetDbString(reader["WLRemark"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]); 
                    entity.MemName = StringUtils.GetDbString(reader["MemName"]);
                    entity.MemPhone = StringUtils.GetDbString(reader["MemPhone"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CreateManId = StringUtils.GetDbInt(reader["CreateManId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.StatusForMem = StringUtils.GetDbInt(reader["StatusForMem"]);
                    entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
                    //entity.CancelReasonId = StringUtils.GetDbInt(reader["CancelReasonId"]);
                    //entity.CancelRemark = StringUtils.GetDbString(reader["CancelRemark"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<VWConfirmOrderEntity> GetConfirmOrderList(int pagesize, int pageindex, ref  int recordCount, int memid, int cgmemid, int status, string key,string  inquirycode)
        {
            string where = " where 1=1 ";
            string innercgmemsql = "";
            if (memid != -1)
            {
                where += " and b.MemId=@MemId";
            }
            if (cgmemid != -1)
            {
                where += " and cgm.CGMemId=@CGMemId";
                innercgmemsql = " inner join ConfirmOrderCGMem cgm WITH(NOLOCK)  on a.[Code]=cgm.ConfirmOrderCode ";
            }
            if (status != -1)
            {
                where += " and a.Status=@Status";
            } 
            if (!string.IsNullOrEmpty(key))
            {
                where += " and a.Code like @Code";
            }
            if (!string.IsNullOrEmpty(inquirycode))
            {
                where += " and a.InquiryOrderCode like @InquiryOrderCode";
            }
            string sql= @"SELECT   [Code],InquiryOrderCode,[VinNo],VinPic,[EngineModelNo], EngineModelPic,CarBrandId,CarBrandName,CarSeriesId,CarSeriesName,[CarTypeModelId],[CarTypeModelName],[Remark],[WLRemark],NeedDay,CGTotalPrice,TotalPrice,[CreateManId],[CreateTime], [Status], StatusForMem,MemId,[MemName]
      ,[MemPhone]
      ,[CompanyAddress]
      ,[CompanyName] FROM 
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
a.[Code],a.InquiryOrderCode,a.[VinNo],a.VinPic,a.[EngineModelNo],a.EngineModelPic,a.CarBrandId,a.CarBrandName,a.CarSeriesId,a.CarSeriesName,[CarTypeModelId],[CarTypeModelName],[Remark],[WLRemark],NeedDay,a.CGTotalPrice,a.TotalPrice,[CreateManId],[CreateTime], a.[Status],a.StatusForMem, a.MemId, b.[MemName]
      ,b.[MemPhone]
      ,b.[CompanyAddress]
      ,b.[CompanyName] from dbo.[ConfirmOrder] a WITH(NOLOCK) INNER JOIN ConfirmOrderMember b WITH(NOLOCK) 
ON a.[Code]=b.[ConfirmOrderCode] " + innercgmemsql + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2= @"Select count(1) from dbo.[ConfirmOrder]  a WITH(NOLOCK) INNER JOIN ConfirmOrderMember b WITH(NOLOCK) 
ON a.[Code]=b.[ConfirmOrderCode] " + innercgmemsql + where;
            IList<VWConfirmOrderEntity> entityList = new List<VWConfirmOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid != -1)
            {
		      db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);  
            }
            if (cgmemid != -1)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            if (status != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Code", DbType.String,"%"+ key+"%");
            }
            if (!string.IsNullOrEmpty(inquirycode))
            { 
                db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, "%" + inquirycode + "%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWConfirmOrderEntity entity =new VWConfirmOrderEntity(); 
					entity.ConfirmOrderCode=StringUtils.GetDbString(reader["Code"]);
					entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.VinNo=StringUtils.GetDbString(reader["VinNo"]);
					entity.VinPic=StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);
                    entity.CarBrandId=StringUtils.GetDbInt(reader["CarBrandId"]);
					entity.CarBrandName=StringUtils.GetDbString(reader["CarBrandName"]);
					entity.CarSeriesId=StringUtils.GetDbInt(reader["CarSeriesId"]);
					entity.CarSeriesName=StringUtils.GetDbString(reader["CarSeriesName"]);
					entity.CarTypeModelId=StringUtils.GetDbInt(reader["CarTypeModelId"]);
					entity.CarTypeModelName=StringUtils.GetDbString(reader["CarTypeModelName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.NeedDay=StringUtils.GetDbInt(reader["NeedDay"]);
					entity.WLRemark=StringUtils.GetDbString(reader["WLRemark"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.TotalPrice=StringUtils.GetDbDecimal(reader["TotalPrice"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]); 
                    entity.MemName = StringUtils.GetDbString(reader["MemName"]);
					entity.MemPhone = StringUtils.GetDbString(reader["MemPhone"]);
					entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]); 
					entity.StatusForMem=StringUtils.GetDbInt(reader["StatusForMem"]);
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            }
            if (cgmemid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, "%" + key + "%");
            }
            if (!string.IsNullOrEmpty(inquirycode))
            {
                db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, "%" + inquirycode + "%");
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

        public IList<ReportInquiryOrderEntity> GetReportDaily(string startdate, string enddate, int status, int reporttype, int orderby)
        {
            string sql = "";
            string orderbystr = "";
            string where = " where a.CreateTime>@StartDate and a.CreateTime<@EndDate ";
            if (status != -1)
            {
                where += " and a.Status=@Status ";
            }
            if (reporttype == (int)ReportInquiryTypeEnum.CreateDate)
            {
                sql = @"SELECT CONVERT(VARCHAR(10),CreateTime,120) AS CreateDate,0 AS MemId,'' AS MemName, '' AS CompanyName,0 AS CGMemId,'' AS CGMemName,'' AS CGCompanyName,Sum(1) as TotalNum,SUM(CGTotalPrice) AS CGTotalPrice,SUM( TotalPrice ) AS TotalPrice ,SUM( TotalPrice ) -SUM(CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  WITH(NOLOCK) " + where + " GROUP BY CONVERT(VARCHAR(10),CreateTime,120)   ";
            }
            else if (reporttype == (int)ReportInquiryTypeEnum.Mem)
            {
                sql = @"SELECT CONVERT(VARCHAR(10),CreateTime,120) AS CreateDate,a.MemId AS MemId,b.MemName AS MemName,b.CompanyName AS  CompanyName, Sum(1) as TotalNum, SUM(CGTotalPrice) AS CGTotalPrice,SUM( TotalPrice ) AS TotalPrice ,SUM( TotalPrice ) -SUM(CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderMember b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode
  " + where + " GROUP BY CONVERT(VARCHAR(10),a.CreateTime,120),a.MemId,b.MemName,b.CompanyName ";
            }
            else if (reporttype == (int)ReportInquiryTypeEnum.CreateMonth)
            {
                sql = @"SELECT CONVERT(VARCHAR(7),CreateTime,120) AS CreateDate,0 AS MemId,'' AS MemName, '' AS CompanyName,0 AS CGMemId,'' AS CGMemName,'' AS CGCompanyName,Sum(1) as TotalNum,SUM(CGTotalPrice) AS CGTotalPrice,SUM( TotalPrice ) AS TotalPrice ,SUM( TotalPrice ) -SUM(CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  WITH(NOLOCK) " + where + " GROUP BY CONVERT(VARCHAR(7),CreateTime,120)   ";
            }
            else if (reporttype == (int)ReportInquiryTypeEnum.MemMonth)
            {
                sql = @"SELECT CONVERT(VARCHAR(7),CreateTime,120) AS CreateDate,a.MemId AS MemId,b.MemName AS MemName,b.CompanyName AS  CompanyName, Sum(1) as TotalNum, SUM(CGTotalPrice) AS CGTotalPrice,SUM( TotalPrice ) AS TotalPrice ,SUM( TotalPrice ) -SUM(CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderMember b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode
  " + where + " GROUP BY CONVERT(VARCHAR(7),a.CreateTime,120),a.MemId,b.MemName,b.CompanyName ";
            }
            else//默认
            {
                sql = @"SELECT CONVERT(VARCHAR(10),CreateTime,120) AS CreateDate,0 AS MemId,'' AS MemName, '' AS CompanyName,0 AS CGMemId,'' AS CGMemName,'' AS CGCompanyName,Sum(1) as TotalNum,SUM(CGTotalPrice) AS CGTotalPrice,SUM( TotalPrice ) AS TotalPrice ,SUM( TotalPrice ) -SUM(CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  WITH(NOLOCK) " + where + " GROUP BY CONVERT(VARCHAR(10),CreateTime,120)   ";
            }
            if (orderby == (int)ReportConfirmOrderEnum.TotalNum)
            {
                orderbystr = " Order By TotalNum desc";
            }
            else if (orderby == (int)ReportConfirmOrderEnum.TotalPrice)
            {
                orderbystr = " Order By TotalPrice desc";
            }
            else if (orderby == (int)ReportConfirmOrderEnum.Profit)
            {
                orderbystr = " Order By Profit desc";
            }
            else if (orderby == (int)ReportConfirmOrderEnum.CGTotalPrice)
            {
                orderbystr = " Order By CGTotalPrice desc";
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
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.Profit = StringUtils.GetDbDecimal(reader["Profit"]);
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
//                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,0 AS  CGMemId,Sum(1) as TotalNum,SUM(B.CGTotalPrice) AS CGTotalPrice,SUM(B.TotalPrice) AS TotalPrice,
// SUM(B.TotalPrice)-SUM(B.CGTotalPrice) AS Profit
//FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderCGMem b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode
//" + where + "  GROUP BY  CONVERT(VARCHAR(10),a.CreateTime,120)   ";
//              }
//            else
            if (reporttype == (int)ReportInquiryCGTypeEnum.Mem)
            {
                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,b.CGMemId AS  CGMemId,Sum(1) as TotalNum,SUM(B.CGTotalPrice) AS CGTotalPrice,SUM(B.TotalPrice) AS TotalPrice,
 SUM(B.TotalPrice)-SUM(B.CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderCGMem b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode 
" + where + " GROUP BY b.CGMemId,CONVERT(VARCHAR(10),a.CreateTime,120)   ";
            }
//            else if (reporttype == (int)ReportInquiryCGTypeEnum.CreateMonth)
//            {
//                sql = @"SELECT CONVERT(VARCHAR(7),a.CreateTime,120) AS CreateDate,0 AS  CGMemId,Sum(1) as TotalNum,SUM(B.CGTotalPrice) AS CGTotalPrice,SUM(B.TotalPrice) AS TotalPrice,
// SUM(B.TotalPrice)-SUM(B.CGTotalPrice) AS Profit
//FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderCGMem b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode
//" + where + "  GROUP BY  CONVERT(VARCHAR(7),a.CreateTime,120)    ";
//            }
            else if (reporttype == (int)ReportInquiryCGTypeEnum.MemMonth)
            {
                sql = @"SELECT CONVERT(VARCHAR(7),a.CreateTime,120) AS CreateDate,b.CGMemId AS  CGMemId,Sum(1) as TotalNum,SUM(B.CGTotalPrice) AS CGTotalPrice,SUM(B.TotalPrice) AS TotalPrice,
 SUM(B.TotalPrice)-SUM(B.CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderCGMem b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode 
" + where + " GROUP BY b.CGMemId,CONVERT(VARCHAR(7),a.CreateTime,120)  ";
            }
            else//默认
            {
                sql = @"SELECT CONVERT(VARCHAR(10),a.CreateTime,120) AS CreateDate,b.CGMemId AS  CGMemId,Sum(1) as TotalNum,SUM(B.CGTotalPrice) AS CGTotalPrice,SUM(B.TotalPrice) AS TotalPrice,
 SUM(B.TotalPrice)-SUM(B.CGTotalPrice) AS Profit
FROM   dbo.ConfirmOrder a  INNER JOIN dbo.ConfirmOrderCGMem b WITH(NOLOCK) ON a.Code=b.ConfirmOrderCode 
" + where + " GROUP BY b.CGMemId,CONVERT(VARCHAR(10),a.CreateTime,120)   ";
            }
            if (orderby == (int)ReportConfirmOrderEnum.TotalNum)
            {
                orderbystr = " Order By TotalNum desc";
            }
            else if (orderby == (int)ReportConfirmOrderEnum.TotalPrice)
            {
                orderbystr = " Order By TotalPrice desc";
            }
            else if (orderby == (int)ReportConfirmOrderEnum.Profit)
            {
                orderbystr = " Order By Profit desc";
            }
            else if (orderby == (int)ReportConfirmOrderEnum.CGTotalPrice)
            {
                orderbystr = " Order By CGTotalPrice desc";
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
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
                    entity.Profit = StringUtils.GetDbDecimal(reader["Profit"]);
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public int OrderDeliverySubmit(string code, string wlremark)
        {
            string sql = @" update ConfirmOrder set Status=@Status, StatusForMem=@StatusForMem,WLRemark=@WLRemark where Code=@ConfirmOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ConfirmOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@WLRemark", DbType.String, wlremark);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderConfirmStatusEnum.Finished);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, (int)ConfirmStatusForMemEnum.Finished);
            return db.ExecuteNonQuery(cmd);
        }
        public int OrderDeliveryAssign(string code, string wlremark, int deliverymemid)
        {
            string sql = @" update ConfirmOrder set Status=@Status, StatusForMem=@StatusForMem,WLRemark=@WLRemark,WLMemId=@WLMemId where Code=@ConfirmOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ConfirmOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@WLRemark", DbType.String, wlremark);
            db.AddInParameter(cmd, "@WLMemId", DbType.Int32, deliverymemid);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderConfirmStatusEnum.Delivering);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, (int)ConfirmStatusForMemEnum.Delivering);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 送货员不送货，还原订单
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wlremark"></param>
        /// <param name="deliverymemid"></param>
        /// <returns></returns>
        public int OrderDeliveryReback(string code, string wlremark, int deliverymemid)
        {
            string sql = @" update ConfirmOrder set Status=@Status, StatusForMem=@StatusForMem,WLRemark=@WLRemark,WLMemId=0 where Code=@ConfirmOrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ConfirmOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@WLRemark", DbType.String, wlremark); 
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)OrderConfirmStatusEnum.WaitDelivery);
            db.AddInParameter(cmd, "@StatusForMem", DbType.Int32, (int)ConfirmStatusForMemEnum.WaitDelivery);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ConfirmOrderEntity> GetConfirmOrderAll()
        {

            string sql = @"SELECT    [Id],[Code],[VinNo],[VinPic],[EngineModelNo],[EngineModelPic],[CarBrandId],[CarBrandName],[CarSeriesId],[CarSeriesName],[CarTypeModelId],[CarTypeModelName],[Remark],[NeedDay],[WLRemark],[CGTotalPrice],[TotalPrice],[MemId], [CreateManId],[CreateTime],[Status],[CancelReasonId],[CancelRemark],ScopeType from dbo.[ConfirmOrder] WITH(NOLOCK)	";
		    IList<ConfirmOrderEntity> entityList = new List<ConfirmOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ConfirmOrderEntity entity=new ConfirmOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.VinNo=StringUtils.GetDbString(reader["VinNo"]);
					entity.VinPic=StringUtils.GetDbString(reader["VinPic"]);
                    entity.EngineModelNo = StringUtils.GetDbString(reader["EngineModelNo"]);
                    entity.EngineModelPic = StringUtils.GetDbString(reader["EngineModelPic"]);
                    entity.CarBrandId=StringUtils.GetDbInt(reader["CarBrandId"]);
					entity.CarBrandName=StringUtils.GetDbString(reader["CarBrandName"]);
					entity.CarSeriesId=StringUtils.GetDbInt(reader["CarSeriesId"]);
					entity.CarSeriesName=StringUtils.GetDbString(reader["CarSeriesName"]);
					entity.CarTypeModelId=StringUtils.GetDbInt(reader["CarTypeModelId"]);
					entity.CarTypeModelName=StringUtils.GetDbString(reader["CarTypeModelName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.NeedDay=StringUtils.GetDbInt(reader["NeedDay"]);
					entity.WLRemark=StringUtils.GetDbString(reader["WLRemark"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.TotalPrice=StringUtils.GetDbDecimal(reader["TotalPrice"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]); 
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.ScopeType = StringUtils.GetDbInt(reader["ScopeType"]);
					entity.CancelReasonId=StringUtils.GetDbInt(reader["CancelReasonId"]);
					entity.CancelRemark=StringUtils.GetDbString(reader["CancelRemark"]);
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
        public int  ExistNum(ConfirmOrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ConfirmOrder] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CarBrandName=@CarBrandName) ";
					     where = where+ "  (CarSeriesName=@CarSeriesName) ";
					     where = where+ "  (CarTypeModelName=@CarTypeModelName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CarBrandName=@CarBrandName) ";
					     where = where+ " id<>@Id and  (CarSeriesName=@CarSeriesName) ";
					     where = where+ " id<>@Id and  (CarTypeModelName=@CarTypeModelName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CarBrandName", DbType.String, entity.CarBrandName); 
					
            db.AddInParameter(cmd, "@CarSeriesName", DbType.String, entity.CarSeriesName); 
					
            db.AddInParameter(cmd, "@CarTypeModelName", DbType.String, entity.CarTypeModelName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
