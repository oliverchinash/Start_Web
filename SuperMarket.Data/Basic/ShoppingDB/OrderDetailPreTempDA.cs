using System;
using System.Data;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using SuperMarket.Core.Util;
using SuperMarket.Core.Safe;
using System.Data.Common;
using SuperMarket.Model;
using System.Text;

/*****************************************
功能描述：OrderDetailPreTemp表的数据访问类。
创建时间：2016/9/20 17:14:48
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// OrderDetailPreTempEntity的数据访问操作
	/// </summary>
	public partial class OrderDetailPreTempDA: BaseSuperMarketDB
    {
        #region 实例化
        public static OrderDetailPreTempDA Instance
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
            internal static readonly OrderDetailPreTempDA instance = new OrderDetailPreTempDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表OrderDetailPreTemp，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderDetailPreTemp">待插入的实体对象</param>
		public int AddOrderDetailPreTemp(OrderDetailPreTempEntity entity)
		{
		   string sql= @"insert into OrderDetailPreTemp( [Code],[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[DiscountId],[MarketPrice],[ActualPrice], [Num],[TotalPrice],[TotalMarketPrice],[CreateTime])VALUES
			            ( @Code,@ProductId,@ProductName,@Spec1,@Spec2,@Spec3,@ProductPic,@DiscountId,@MarketPrice,@ActualPrice,@Num,@TotalPrice,@TotalMarketPrice,@CreateTime)";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId); 
			db.AddInParameter(cmd, "@ProductName",  DbType.String,entity.ProductName);
			db.AddInParameter(cmd, "@Spec1",  DbType.String,entity.Spec1);
			db.AddInParameter(cmd, "@Spec2",  DbType.String,entity.Spec2);
			db.AddInParameter(cmd, "@Spec3",  DbType.String,entity.Spec3);
			db.AddInParameter(cmd,"@ProductPic",  DbType.String,entity.ProductPic);
			db.AddInParameter(cmd,"@DiscountId",  DbType.Int32,entity.DiscountId);
			db.AddInParameter(cmd,"@MarketPrice",  DbType.Decimal,entity.MarketPrice);
			db.AddInParameter(cmd,"@ActualPrice",  DbType.Decimal,entity.ActualPrice); 
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@TotalPrice",  DbType.Decimal,entity.TotalPrice);
			db.AddInParameter(cmd,"@TotalMarketPrice",  DbType.Decimal,entity.TotalMarketPrice);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			return db.ExecuteNonQuery(cmd);
 			
		}
        public int AddOrderPreList(IList<OrderDetailPreTempEntity> list)
        {
            int result = 0;
            if(list!=null&& list.Count>0)
            {
                string sql = @"insert into OrderDetailPreTemp( [Code],[ProductDetailId],BrandId,ClassId,[ProductId],[ProductCode],[ProductName],Spec1,Spec2,Spec3,[ProductPic],ProductPicSuffix,[Cost],[Price],[TradePrice],DealerPrice,[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime],TransFeeType,TransFee,ProductType,Status,Remark,Unit,IsBP,CGMemId,IsAhmTake) VALUES";
                StringBuilder sqlvalues = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    OrderDetailPreTempEntity _entity = list[i];
                    if (sqlvalues.Length > 0)
                    {
                        sqlvalues.Append(",");
                    }
                    sqlvalues.Append("(");
                    sqlvalues.Append(_entity.Code);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.ProductDetailId);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.BrandId);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.ClassId);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.ProductId);
                    sqlvalues.Append(",'");
                    sqlvalues.Append(_entity.ProductCode);
                    sqlvalues.Append("','");
                    sqlvalues.Append(_entity.ProductName);
                    sqlvalues.Append("','");
                    sqlvalues.Append(_entity.Spec1);
                    sqlvalues.Append("','");
                    sqlvalues.Append(_entity.Spec2);
                    sqlvalues.Append("','");
                    sqlvalues.Append(_entity.Spec3);
                    sqlvalues.Append("','");
                    sqlvalues.Append(_entity.ProductPic);
                    sqlvalues.Append("','");
                    sqlvalues.Append(_entity.ProductPicSuffix); 
                    sqlvalues.Append("',");
                    sqlvalues.Append(_entity.Cost);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.Price);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.TradePrice);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.DealerPrice);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.MarketPrice);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.ActualPrice); 
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.Num);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.TotalPrice); 
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.TotalMarketPrice);
                    sqlvalues.Append(",'");
                    sqlvalues.Append(_entity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    sqlvalues.Append("',");
                    sqlvalues.Append(_entity.TransFeeType);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.TransFee);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.ProductType);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.Status);
                    sqlvalues.Append(",'");
                    sqlvalues.Append(_entity.Remark);
                    sqlvalues.Append("',");
                    sqlvalues.Append(_entity.Unit);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.IsBP);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.CGMemId);
                    sqlvalues.Append(",");
                    sqlvalues.Append(_entity.IsAhmTake); 
                    sqlvalues.Append(")");
                    if((i+1)%10==0||i== list.Count-1)
                    {
                        DbCommand cmd = db.GetSqlStringCommand(sql + sqlvalues.ToString()); 
                        result+= db.ExecuteNonQuery(cmd);
                        sqlvalues.Clear();
                    } 
                } 
            }
            return result;
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="orderDetailPreTemp">待更新的实体对象</param>
        public   int UpdateOrderDetailPreTemp(OrderDetailPreTempEntity entity)
		{
			string sql=@" UPDATE dbo.[OrderDetailPreTemp] SET
                       [Code]=@Code,[ProductId]=@ProductId,[ProductName]=@ProductName,[ProductPic]=@ProductPic,[DiscountId]=@DiscountId,[MarketPrice]=@MarketPrice,[ActualPrice]=@ActualPrice,[Num]=@Num,[TotalPrice]=@TotalPrice,[TotalMarketPrice]=@TotalMarketPrice,[CreateTime]=@CreateTime
                       WHERE [Code]=@code";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId); 
			db.AddInParameter(cmd,"@ProductName",  DbType.String,entity.ProductName);
			db.AddInParameter(cmd,"@ProductPic",  DbType.String,entity.ProductPic);
			db.AddInParameter(cmd,"@DiscountId",  DbType.Int32,entity.DiscountId);
			db.AddInParameter(cmd,"@MarketPrice",  DbType.Decimal,entity.MarketPrice);
			db.AddInParameter(cmd,"@ActualPrice",  DbType.Decimal,entity.ActualPrice);
			
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@TotalPrice",  DbType.Decimal,entity.TotalPrice);
			db.AddInParameter(cmd,"@TotalMarketPrice",  DbType.Decimal,entity.TotalMarketPrice);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteOrderDetailPreTempByKey(string code)
	    {
			string sql=@"delete from OrderDetailPreTemp where  and Code=@Code";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Code", DbType.String,code); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderDetailPreTempDisabled()
        {
            string sql = @"delete from  OrderDetailPreTemp  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderDetailPreTempByIds(string ids)
        {
            string sql = @"Delete from OrderDetailPreTemp  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderDetailPreTempByIds(string ids)
        {
            string sql = @"Update   OrderDetailPreTemp set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   OrderDetailPreTempEntity GetOrderDetailPreTemp(string code)
		{
			string sql=@"SELECT  [Code],[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime]
							FROM
							dbo.[OrderDetailPreTemp] WITH(NOLOCK)	
							WHERE [Code]=@code";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Code", DbType.String,code);
    		OrderDetailPreTempEntity entity=new OrderDetailPreTempEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Code=StringUtils.GetDbLong(reader["Code"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
                    entity.Spec1=StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2=StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3=StringUtils.GetDbString(reader["Spec3"]);
					entity.ProductPic=StringUtils.GetDbString(reader["ProductPic"]);
					entity.DiscountId=StringUtils.GetDbInt(reader["DiscountId"]);
					entity.MarketPrice=StringUtils.GetDbDecimal(reader["MarketPrice"]);
					entity.ActualPrice=StringUtils.GetDbDecimal(reader["ActualPrice"]);
					
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.TotalPrice=StringUtils.GetDbDecimal(reader["TotalPrice"]);
					entity.TotalMarketPrice=StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<OrderDetailPreTempEntity> GetOrderDetailPreTempList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Code],[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Code desc) AS ROWNUMBER,
						 [Code],[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime] from dbo.[OrderDetailPreTemp] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[OrderDetailPreTemp] with (nolock) ";
            IList<OrderDetailPreTempEntity> entityList = new List< OrderDetailPreTempEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					OrderDetailPreTempEntity entity=new OrderDetailPreTempEntity();
					entity.Code=StringUtils.GetDbLong(reader["Code"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					
					entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
                    entity.Spec1=StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2=StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3=StringUtils.GetDbString(reader["Spec3"]);
					entity.ProductPic=StringUtils.GetDbString(reader["ProductPic"]);
					entity.DiscountId=StringUtils.GetDbInt(reader["DiscountId"]);
					entity.MarketPrice=StringUtils.GetDbDecimal(reader["MarketPrice"]);
					entity.ActualPrice=StringUtils.GetDbDecimal(reader["ActualPrice"]);
					
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.TotalPrice=StringUtils.GetDbDecimal(reader["TotalPrice"]);
					entity.TotalMarketPrice=StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<OrderDetailPreTempEntity> GetOrderPreTempByCode(Int64 code,int status)
        {
            string where = " where Code=@Code ";
            if(status!=-1)
            {
                where += " and Status=@Status";
            }
            string sql = @"SELECT    [Code],[ProductDetailId],BrandId,ClassId,[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[ProductPicSuffix],[DiscountId],[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime],TransFeeType,TransFee,ProductType,Status,Remark,CGMemId,IsAhmTake from dbo.[OrderDetailPreTemp] WITH(NOLOCK)
" + where;
            IList<OrderDetailPreTempEntity> entityList = new List<OrderDetailPreTempEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code);
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int64, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    OrderDetailPreTempEntity entity = new OrderDetailPreTempEntity();
                    entity.Code = StringUtils.GetDbLong(reader["Code"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]); 
                    entity.BrandId = StringUtils.GetDbInt(reader["BrandId"]);  
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);  
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);  
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);
                    entity.DiscountId = StringUtils.GetDbInt(reader["DiscountId"]);
                    entity.MarketPrice = StringUtils.GetDbDecimal(reader["MarketPrice"]);
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]); 
                    entity.TransFeeType = StringUtils.GetDbInt(reader["TransFeeType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]);
                    entity.ProductType = StringUtils.GetDbInt(reader["ProductType"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]);
                    entity.TotalMarketPrice = StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.IsAhmTake = StringUtils.GetDbInt(reader["IsAhmTake"]);
                 
                    entityList.Add(entity);
                }
            }
            return entityList;
        }

        public IList<VWOrderDetailEntity> GetOrderDetailsByCode(long code,int status=-1)
        {
            string where = " where Code=@Code ";
            if (status != -1)
            {
                where += " and Status=@Status";
            }
            string sql = @"SELECT  Id as PreTempDetailsId,  [Code],[ProductDetailId],BrandId,ClassId,[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[ProductPicSuffix],[DiscountId],[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime],TransFeeType,TransFee,ProductType,Status,Remark,CGMemId,IsAhmTake from dbo.[OrderDetailPreTemp] WITH(NOLOCK)
" + where;
            IList<VWOrderDetailEntity> entityList = new List<VWOrderDetailEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.Int64, code);
            if (status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int64, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWOrderDetailEntity entity = new VWOrderDetailEntity();
                    entity.OrderDetailId = StringUtils.GetDbInt(reader["PreTempDetailsId"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["Code"]);
                    entity.ProductDetailId = StringUtils.GetDbInt(reader["ProductDetailId"]); 
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ProductName = StringUtils.GetDbString(reader["ProductName"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.ProductPic = StringUtils.GetDbString(reader["ProductPic"]);
                    entity.ProductPicSuffix = StringUtils.GetDbString(reader["ProductPicSuffix"]);  
                    entity.ActualPrice = StringUtils.GetDbDecimal(reader["ActualPrice"]);
                    entity.TransFeeType = StringUtils.GetDbInt(reader["TransFeeType"]);
                    entity.TransFee = StringUtils.GetDbDecimal(reader["TransFee"]); 
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]); 
                    entity.TotalPrice = StringUtils.GetDbDecimal(reader["TotalPrice"]); 
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.IsAhmTake = StringUtils.GetDbInt(reader["IsAhmTake"]);

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
        public IList<OrderDetailPreTempEntity> GetOrderDetailPreTempAll()
        {

            string sql = @"SELECT    [Code],[ProductId],[ProductName],Spec1,Spec2,Spec3,[ProductPic],[DiscountId],[MarketPrice],[ActualPrice],[Num],[TotalPrice],[TotalMarketPrice],[CreateTime] from dbo.[OrderDetailPreTemp] WITH(NOLOCK)	";
		    IList<OrderDetailPreTempEntity> entityList = new List<OrderDetailPreTempEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   OrderDetailPreTempEntity entity=new OrderDetailPreTempEntity();
					entity.Code=StringUtils.GetDbLong(reader["Code"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					
					entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
					entity.ProductPic=StringUtils.GetDbString(reader["ProductPic"]);
					entity.DiscountId=StringUtils.GetDbInt(reader["DiscountId"]);
					entity.MarketPrice=StringUtils.GetDbDecimal(reader["MarketPrice"]);
					entity.ActualPrice=StringUtils.GetDbDecimal(reader["ActualPrice"]);
					
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.TotalPrice=StringUtils.GetDbDecimal(reader["TotalPrice"]);
					entity.TotalMarketPrice=StringUtils.GetDbDecimal(reader["TotalMarketPrice"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(OrderDetailPreTempEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderDetailPreTemp] WITH(NOLOCK) ";
            string where = "  ";
           
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
           
					
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
