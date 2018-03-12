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
功能描述：CGOrderOfferSub表的数据访问类。
创建时间：2017/2/5 18:00:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
	/// <summary>
	/// CGOrderOfferSubEntity的数据访问操作
	/// </summary>
	public partial class CGOrderOfferSubDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderOfferSubDA Instance
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
            internal static readonly CGOrderOfferSubDA instance = new CGOrderOfferSubDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGOrderOfferSub，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGOrderOfferSub">待插入的实体对象</param>
		public int AddCGOrderOfferSub(CGOrderOfferSubEntity entity)
		{
		   string sql=@"insert into CGOrderOfferSub( [OfferId],[CGOrderCode],[CGMemId],[ProductId],[CGPrice],[Num],[CGTotalPrice])VALUES
			            ( @OfferId,@CGOrderCode,@CGMemId,@ProductId,@CGPrice,@Num,@CGTotalPrice);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@OfferId",  DbType.Int32,entity.OfferId);
			db.AddInParameter(cmd,"@CGOrderCode",  DbType.Int64,entity.CGOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@CGPrice",  DbType.Decimal,entity.CGPrice);
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@CGTotalPrice",  DbType.Decimal,entity.CGTotalPrice);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGOrderOfferSub">待更新的实体对象</param>
		public   int UpdateCGOrderOfferSub(CGOrderOfferSubEntity entity)
		{
			string sql=@" UPDATE dbo.[CGOrderOfferSub] SET
                       [OfferId]=@OfferId,[CGOrderCode]=@CGOrderCode,[CGMemId]=@CGMemId,[ProductId]=@ProductId,[CGPrice]=@CGPrice,[Num]=@Num,[CGTotalPrice]=@CGTotalPrice
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OfferId",  DbType.Int32,entity.OfferId);
			db.AddInParameter(cmd,"@CGOrderCode",  DbType.Int64,entity.CGOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@ProductId",  DbType.Int32,entity.ProductId);
			db.AddInParameter(cmd,"@CGPrice",  DbType.Decimal,entity.CGPrice);
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@CGTotalPrice",  DbType.Decimal,entity.CGTotalPrice);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGOrderOfferSubByKey(int id)
	    {
			string sql=@"delete from CGOrderOfferSub where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderOfferSubDisabled()
        {
            string sql = @"delete from  CGOrderOfferSub  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderOfferSubByIds(string ids)
        {
            string sql = @"Delete from CGOrderOfferSub  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderOfferSubByIds(string ids)
        {
            string sql = @"Update   CGOrderOfferSub set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGOrderOfferSubEntity GetCGOrderOfferSub(int id)
		{
			string sql=@"SELECT  [Id],[OfferId],[CGOrderCode],[CGMemId],[ProductId],[CGPrice],[Num],[CGTotalPrice]
							FROM
							dbo.[CGOrderOfferSub] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGOrderOfferSubEntity entity=new CGOrderOfferSubEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OfferId=StringUtils.GetDbInt(reader["OfferId"]);
					entity.CGOrderCode=StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.CGPrice=StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
				}
   		    }
            return entity;
		}
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<VWCGOrderOfferSubEntity> GetVWCGOrderOfferSubS(int cgmemid,long orderCode)
        {
            string sql = @"SELECT   b.[CGOrderCode],b.[ProductId],[PicUrl],[PicSuffix],[Name],b.CGPrice,[Title],[Spec1],[Spec2],[Spec3],b.[Num],a.Unit 
							FROM
							dbo.[CGOrderDetail] a WITH(NOLOCK) inner join  dbo.[CGOrderOfferSub] b WITH(NOLOCK)	
                            on a.CGOrderCode=b.CGOrderCode and a.ProductId=b.ProductId   
							WHERE b.[CGOrderCode]=@CGOrderCode and  b.CGMemId=@CGMemId  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            IList<VWCGOrderOfferSubEntity> entityList = new List<VWCGOrderOfferSubEntity>();
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, orderCode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWCGOrderOfferSubEntity entity = new VWCGOrderOfferSubEntity();
                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    entity.PicSuffix = StringUtils.GetDbString(reader["PicSuffix"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.Spec1 = StringUtils.GetDbString(reader["Spec1"]);
                    entity.Spec2 = StringUtils.GetDbString(reader["Spec2"]);
                    entity.Spec3 = StringUtils.GetDbString(reader["Spec3"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);  
                    entity.Unit = StringUtils.GetDbInt(reader["Unit"]);  
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
        public   IList<CGOrderOfferSubEntity> GetCGOrderOfferSubList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[OfferId],[CGOrderCode],[CGMemId],[ProductId],[CGPrice],[Num],[CGTotalPrice]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OfferId],[CGOrderCode],[CGMemId],[ProductId],[CGPrice],[Num],[CGTotalPrice] from dbo.[CGOrderOfferSub] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGOrderOfferSub] with (nolock) ";
            IList<CGOrderOfferSubEntity> entityList = new List< CGOrderOfferSubEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGOrderOfferSubEntity entity=new CGOrderOfferSubEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OfferId=StringUtils.GetDbInt(reader["OfferId"]);
					entity.CGOrderCode=StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.CGPrice=StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
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
        public IList<CGOrderOfferSubEntity> GetCGOrderOfferSubAll()
        {

            string sql = @"SELECT    [Id],[OfferId],[CGOrderCode],[CGMemId],[ProductId],[CGPrice],[Num],[CGTotalPrice] from dbo.[CGOrderOfferSub] WITH(NOLOCK)	";
		    IList<CGOrderOfferSubEntity> entityList = new List<CGOrderOfferSubEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGOrderOfferSubEntity entity=new CGOrderOfferSubEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OfferId=StringUtils.GetDbInt(reader["OfferId"]);
					entity.CGOrderCode=StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.ProductId=StringUtils.GetDbInt(reader["ProductId"]);
					entity.CGPrice=StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
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
        public int  ExistNum(CGOrderOfferSubEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderOfferSub] WITH(NOLOCK) ";
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
