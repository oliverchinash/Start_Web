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
功能描述：InquiryProductSub表的数据访问类。
创建时间：2017/8/26 21:30:22
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// InquiryProductSubEntity的数据访问操作
	/// </summary>
	public partial class InquiryProductSubDA: BaseSuperMarketDB
    {
        #region 实例化
        public static InquiryProductSubDA Instance
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
            internal static readonly InquiryProductSubDA instance = new InquiryProductSubDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表InquiryProductSub，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inquiryProductSub">待插入的实体对象</param>
		public int AddInquiryProductSub(InquiryProductSubEntity entity)
		{
		   string sql= @"
IF NOT EXISTS(SELECT 1 FROM InquiryProductSub WHERE InquiryOrderCode=@InquiryOrderCode AND [InquiryProductId]=@InquiryProductId and [InquiryProductType]=@InquiryProductType)
BEGIN
insert into InquiryProductSub(InquiryOrderCode, [InquiryProductId],[InquiryProductType],CreateManId,CreateTime)VALUES
			            (@InquiryOrderCode, @InquiryProductId,@InquiryProductType,@CreateManId,getdate());
			SELECT SCOPE_IDENTITY();
END	
else
begin
  SELECT id FROM InquiryProductSub WHERE InquiryOrderCode=@InquiryOrderCode AND [InquiryProductId]=@InquiryProductId and  [InquiryProductType]=@InquiryProductType
end

";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd, "@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@InquiryProductId", DbType.Int32,entity.InquiryProductId);
			db.AddInParameter(cmd,"@InquiryProductType",  DbType.Int32,entity.InquiryProductType);
			db.AddInParameter(cmd, "@CreateManId",  DbType.Int32,entity.CreateManId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int CreateBindProductType(string ordercode,int productid, string productstypes)
        {
            string sql = @"
delete from InquiryProductSub where InquiryProductId=@InquiryProductId
insert into InquiryProductSub(InquiryOrderCode, [InquiryProductId],[InquiryProductType])
 SELECT @InquiryOrderCode,@InquiryProductId,Id FROM dbo.fun_splitstr(@ProductTypeS,'|')";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@InquiryProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            db.AddInParameter(cmd, "@ProductTypeS", DbType.String, productstypes);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="inquiryProductSub">待更新的实体对象</param>
        public   int UpdateInquiryProductSub(InquiryProductSubEntity entity)
		{
			string sql=@" UPDATE dbo.[InquiryProductSub] SET
                       [InquiryProductId]=@InquiryProductId,[InquiryProductType]=@InquiryProductType
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@InquiryProductId",  DbType.Int32,entity.InquiryProductId);
			db.AddInParameter(cmd,"@InquiryProductType",  DbType.Int32,entity.InquiryProductType);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteInquiryProductSubByKey(int id)
	    {
			string sql=@"delete from InquiryProductSub where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryProductSubDisabled()
        {
            string sql = @"delete from  InquiryProductSub  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        public int DeleteInquiryProductSubByPro(string code, int productid, int protypeid)
        {
            string sql = @"delete from  InquiryProductSub  where  InquiryOrderCode=@InquiryOrderCode and [InquiryProductId]=@InquiryProductId and [InquiryProductType]=@InquiryProductType ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@InquiryProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@InquiryProductType", DbType.Int32, protypeid);
            return db.ExecuteNonQuery(cmd);
        }
        public int DeleteInquiryProductSubByCG(string code, int productid, int protypeid, int cgmemid)
        {
            string sql = @"delete from  InquiryProductSub  where  InquiryOrderCode=@InquiryOrderCode and [InquiryProductId]=@InquiryProductId and [InquiryProductType]=@InquiryProductType and CreateManId=@CreateManId  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@InquiryProductId", DbType.Int32, productid);
            db.AddInParameter(cmd, "@InquiryProductType", DbType.Int32, protypeid);
            db.AddInParameter(cmd, "@CreateManId", DbType.Int32, cgmemid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryProductSubByIds(string ids)
        {
            string sql = @"Delete from InquiryProductSub  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryProductSubByIds(string ids)
        {
            string sql = @"Update   InquiryProductSub set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   InquiryProductSubEntity GetInquiryProductSub(int id)
		{
			string sql=@"SELECT  [Id],[InquiryProductId],[InquiryProductType]
							FROM
							dbo.[InquiryProductSub] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		InquiryProductSubEntity entity=new InquiryProductSubEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryProductId=StringUtils.GetDbInt(reader["InquiryProductId"]);
					entity.InquiryProductType=StringUtils.GetDbInt(reader["InquiryProductType"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<InquiryProductSubEntity> GetInquiryProductSubList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[InquiryProductId],[InquiryProductType]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[InquiryProductId],[InquiryProductType] from dbo.[InquiryProductSub] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[InquiryProductSub] with (nolock) ";
            IList<InquiryProductSubEntity> entityList = new List< InquiryProductSubEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					InquiryProductSubEntity entity=new InquiryProductSubEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryProductId=StringUtils.GetDbInt(reader["InquiryProductId"]);
					entity.InquiryProductType=StringUtils.GetDbInt(reader["InquiryProductType"]);
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
        public IList<InquiryProductSubEntity> GetInquiryProductSubAll(string ordercode,int  proid)
        {
            string where = " where 1=1 ";
            if(!string.IsNullOrEmpty(ordercode))
            {
                where += " and InquiryOrderCode=@InquiryOrderCode ";
            }
            if (proid>0)
            {
                where += " and InquiryProductId=@InquiryProductId ";
            }
            string sql = @"SELECT    [Id],InquiryOrderCode,[InquiryProductId],[InquiryProductType] from dbo.[InquiryProductSub] WITH(NOLOCK)	"+where+" Order By InquiryProductId,InquiryProductType";
		    IList<InquiryProductSubEntity> entityList = new List<InquiryProductSubEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(ordercode))
            {
                db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode); 
            }
            if (proid > 0)
            {
                db.AddInParameter(cmd, "@InquiryProductId", DbType.Int32, proid);  
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   InquiryProductSubEntity entity=new InquiryProductSubEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode = StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.InquiryProductId=StringUtils.GetDbInt(reader["InquiryProductId"]);
                    entity.InquiryProductType=StringUtils.GetDbInt(reader["InquiryProductType"]);
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
        public int  ExistNum(InquiryProductSubEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[InquiryProductSub] WITH(NOLOCK) ";
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
