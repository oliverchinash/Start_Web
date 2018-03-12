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
功能描述：ConfirmOrderProduct表的数据访问类。
创建时间：2017/10/12 14:15:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// ConfirmOrderProductEntity的数据访问操作
	/// </summary>
	public partial class ConfirmOrderProductDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ConfirmOrderProductDA Instance
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
            internal static readonly ConfirmOrderProductDA instance = new ConfirmOrderProductDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ConfirmOrderProduct，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="confirmOrderProduct">待插入的实体对象</param>
		public int AddConfirmOrderProduct(ConfirmOrderProductEntity entity)
		{
		   string sql=@"insert into ConfirmOrderProduct( [ConfirmOrderCode],[ProductCode],[ClassesId],[ClassesName],[ProductId],[ProductName],[ProductType],[ProductNum],[ProductUnitName],[Remark],[CreateManId],[CreateTime])VALUES
			            ( @ConfirmOrderCode,@ProductCode,@ClassesId,@ClassesName,@ProductId,@ProductName,@ProductType,@ProductNum,@ProductUnitName,@Remark,@CreateManId,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ConfirmOrderCode",  DbType.String,entity.ConfirmOrderCode);
			db.AddInParameter(cmd,"@ProductCode",  DbType.String,entity.ProductCode);
			db.AddInParameter(cmd,"@ClassesId",  DbType.Int32,entity.ClassesId);
			db.AddInParameter(cmd,"@ClassesName",  DbType.String,entity.ClassesName);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@ProductName",  DbType.String,entity.ProductName);
			db.AddInParameter(cmd,"@ProductType",  DbType.Int32,entity.ProductType);
			db.AddInParameter(cmd,"@ProductNum",  DbType.Int32,entity.ProductNum);
			db.AddInParameter(cmd,"@ProductUnitName",  DbType.String,entity.ProductUnitName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="confirmOrderProduct">待更新的实体对象</param>
		public   int UpdateConfirmOrderProduct(ConfirmOrderProductEntity entity)
		{
			string sql=@" UPDATE dbo.[ConfirmOrderProduct] SET
                       [ConfirmOrderCode]=@ConfirmOrderCode,[ProductCode]=@ProductCode,[ClassesId]=@ClassesId,[ClassesName]=@ClassesName,[ProductId]=@ProductId,[ProductName]=@ProductName,[ProductType]=@ProductType,[ProductNum]=@ProductNum,[ProductUnitName]=@ProductUnitName,[Remark]=@Remark,[CreateManId]=@CreateManId,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ConfirmOrderCode",  DbType.String,entity.ConfirmOrderCode);
			db.AddInParameter(cmd,"@ProductCode",  DbType.String,entity.ProductCode);
			db.AddInParameter(cmd,"@ClassesId",  DbType.Int32,entity.ClassesId);
			db.AddInParameter(cmd,"@ClassesName",  DbType.String,entity.ClassesName);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@ProductName",  DbType.String,entity.ProductName);
			db.AddInParameter(cmd,"@ProductType",  DbType.Int32,entity.ProductType);
			db.AddInParameter(cmd,"@ProductNum",  DbType.Int32,entity.ProductNum);
			db.AddInParameter(cmd,"@ProductUnitName",  DbType.String,entity.ProductUnitName);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@CreateManId",  DbType.Int32,entity.CreateManId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteConfirmOrderProductByKey(int id)
	    {
			string sql=@"delete from ConfirmOrderProduct where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfirmOrderProductDisabled()
        {
            string sql = @"delete from  ConfirmOrderProduct  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfirmOrderProductByIds(string ids)
        {
            string sql = @"Delete from ConfirmOrderProduct  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfirmOrderProductByIds(string ids)
        {
            string sql = @"Update   ConfirmOrderProduct set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ConfirmOrderProductEntity GetConfirmOrderProduct(int id)
		{
			string sql= @"SELECT  [Id],[ConfirmOrderCode],[ProductCode],[ClassesId],[ClassesName],[ProductId],[ProductName],[ProductType],[ProductNum],[ProductUnitName],[Remark],[CreateManId],[CreateTime],CGPrice,Price
							FROM
							dbo.[ConfirmOrderProduct] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ConfirmOrderProductEntity entity=new ConfirmOrderProductEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ConfirmOrderCode=StringUtils.GetDbString(reader["ConfirmOrderCode"]);
					entity.ProductCode=StringUtils.GetDbString(reader["ProductCode"]);
					entity.ClassesId=StringUtils.GetDbInt(reader["ClassesId"]);
					entity.ClassesName=StringUtils.GetDbString(reader["ClassesName"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
					entity.ProductType=StringUtils.GetDbInt(reader["ProductType"]);
					entity.ProductNum=StringUtils.GetDbInt(reader["ProductNum"]);
					entity.ProductUnitName=StringUtils.GetDbString(reader["ProductUnitName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);

                }
            }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ConfirmOrderProductEntity> GetConfirmOrderProductList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[ConfirmOrderCode],[ProductCode],[ClassesId],[ClassesName],[ProductId],[ProductName],[ProductType],[ProductNum],[ProductUnitName],[Remark],[CreateManId],[CreateTime],CGPrice,Price
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ConfirmOrderCode],[ProductCode],[ClassesId],[ClassesName],[ProductId],[ProductName],[ProductType],[ProductNum],[ProductUnitName],[Remark],[CreateManId],[CreateTime],CGPrice,Price from dbo.[ConfirmOrderProduct] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ConfirmOrderProduct] with (nolock) ";
            IList<ConfirmOrderProductEntity> entityList = new List< ConfirmOrderProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ConfirmOrderProductEntity entity=new ConfirmOrderProductEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ConfirmOrderCode=StringUtils.GetDbString(reader["ConfirmOrderCode"]);
					entity.ProductCode=StringUtils.GetDbString(reader["ProductCode"]);
					entity.ClassesId=StringUtils.GetDbInt(reader["ClassesId"]);
					entity.ClassesName=StringUtils.GetDbString(reader["ClassesName"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
					entity.ProductType=StringUtils.GetDbInt(reader["ProductType"]);
					entity.ProductNum=StringUtils.GetDbInt(reader["ProductNum"]);
					entity.ProductUnitName=StringUtils.GetDbString(reader["ProductUnitName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
                    entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
                    

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
        public IList<ConfirmOrderProductEntity> GetConfirmProductAllByCode(string code,int cgmemid)
        {
            string where = " where ConfirmOrderCode=@Code";
            if(cgmemid!=-1)
            {
                where += " and CGMemId=@CGMemId ";
            }
            string sql = @"SELECT    [Id], [ConfirmOrderCode],InquiryOrderCode,[ProductCode],[ClassesId],[ClassesName],[ProductId],[ProductName],[ProductType],[ProductNum],[ProductUnitName],[Remark],[CreateManId],[CreateTime],CGPrice,CGMemId,Price from dbo.[ConfirmOrderProduct] WITH(NOLOCK)	" + where+ " Order By ProductId,ProductType";

            IList<ConfirmOrderProductEntity> entityList = new List<ConfirmOrderProductEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.String, code);
            if (cgmemid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ConfirmOrderProductEntity entity=new ConfirmOrderProductEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ConfirmOrderCode = StringUtils.GetDbString(reader["ConfirmOrderCode"]);
					entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.ProductCode=StringUtils.GetDbString(reader["ProductCode"]);
					entity.ClassesId=StringUtils.GetDbInt(reader["ClassesId"]);
					entity.ClassesName=StringUtils.GetDbString(reader["ClassesName"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.ProductName=StringUtils.GetDbString(reader["ProductName"]);
					entity.ProductType=StringUtils.GetDbInt(reader["ProductType"]);
					entity.ProductNum=StringUtils.GetDbInt(reader["ProductNum"]);
					entity.ProductUnitName=StringUtils.GetDbString(reader["ProductUnitName"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.CreateManId=StringUtils.GetDbInt(reader["CreateManId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Price = StringUtils.GetDbDecimal(reader["Price"]);
					entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);


                    entityList.Add(entity);
                }
            } 
            return entityList;
        }
        public IList<VWConfirmOrderCGMemEntity> GetConfirmCGMemsByCode(string code, bool cache = true)
        {
        
            string sql = @"SELECT      [Id]
      ,[ConfirmOrderCode]
      ,[CGMemId]
      ,[CGTotalPrice]
      ,[SelectTime]
    ,HasInStock 
    ,RemarkByCGMem from [dbo].[ConfirmOrderCGMem] WITH(NOLOCK)	 where ConfirmOrderCode=@Code";

            IList<VWConfirmOrderCGMemEntity> entityList = new List<VWConfirmOrderCGMemEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Code", DbType.String, code);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWConfirmOrderCGMemEntity entity = new VWConfirmOrderCGMemEntity(); 
                    entity.ConfirmOrderCode = StringUtils.GetDbString(reader["ConfirmOrderCode"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);   
                    entity.SelectTime = StringUtils.GetDateTime(reader["SelectTime"]);   
                    entity.HasInStock = StringUtils.GetDbInt(reader["HasInStock"]);   
                    entity.RemarkByCGMem = StringUtils.GetDbString(reader["RemarkByCGMem"]);   
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
        public int  ExistNum(ConfirmOrderProductEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ConfirmOrderProduct] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (ClassesName=@ClassesName) ";
					     where = where+ "  (ProductName=@ProductName) ";
					     where = where+ "  (ProductUnitName=@ProductUnitName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (ClassesName=@ClassesName) ";
					     where = where+ " id<>@Id and  (ProductName=@ProductName) ";
					     where = where+ " id<>@Id and  (ProductUnitName=@ProductUnitName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@ClassesName", DbType.String, entity.ClassesName); 
					
            db.AddInParameter(cmd, "@ProductName", DbType.String, entity.ProductName); 
					
            db.AddInParameter(cmd, "@ProductUnitName", DbType.String, entity.ProductUnitName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
