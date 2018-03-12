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
功能描述：OrderAddress表的数据访问类。
创建时间：2016/8/4 11:02:01
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// OrderAddressEntity的数据访问操作
	/// </summary>
	public partial class OrderAddressDA: BaseSuperMarketDB
    {
        #region 实例化
        public static OrderAddressDA Instance
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
            internal static readonly OrderAddressDA instance = new OrderAddressDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表OrderAddress，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderAddress">待插入的实体对象</param>
		public int AddOrderAddress(OrderAddressEntity entity)
		{
		   string sql= @"insert into OrderAddress( [OrderCode],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[DistrictIdId],[Address],[Postcode],[Telephone],[MobilePhone],[IsDefault],[Sort])VALUES
			            ( @OrderCode,@AccepterName,@CtType,@CountryCode,@CountryName,@ProvinceId,@CityId,@DistrictId,@Address,@Postcode,@Telephone,@MobilePhone,@IsDefault,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int32,entity.OrderCode);
			db.AddInParameter(cmd,"@AccepterName",  DbType.String,entity.AccepterName);
			db.AddInParameter(cmd,"@CtType",  DbType.Int32,entity.CtType);
			db.AddInParameter(cmd,"@CountryCode",  DbType.String,entity.CountryCode);
			db.AddInParameter(cmd,"@CountryName",  DbType.String,entity.CountryName);
			db.AddInParameter(cmd,"@ProvinceId",  DbType.String,entity.ProvinceId);
			db.AddInParameter(cmd,"@CityId",  DbType.String,entity.CityId);
			db.AddInParameter(cmd, "@DistrictIdId",  DbType.String,entity.DistrictId);
			db.AddInParameter(cmd,"@Address",  DbType.String,entity.Address);
			db.AddInParameter(cmd,"@Postcode",  DbType.String,entity.Postcode);
			db.AddInParameter(cmd,"@Telephone",  DbType.String,entity.Telephone);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@IsDefault",  DbType.Int32,entity.IsDefault);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="orderAddress">待更新的实体对象</param>
		public   int UpdateOrderAddress(OrderAddressEntity entity)
		{
			string sql=@" UPDATE dbo.[OrderAddress] SET
                       [OrderCode]=@OrderCode,[AccepterName]=@AccepterName,[CtType]=@CtType,[CountryCode]=@CountryCode,[CountryName]=@CountryName,[ProvinceId]=@ProvinceId,[CityId]=@CityId,[DistrictId]=@DistrictId,[Address]=@Address,[Postcode]=@Postcode,[Telephone]=@Telephone,[MobilePhone]=@MobilePhone,[IsDefault]=@IsDefault,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@OrderCode",  DbType.Int32,entity.OrderCode);
			db.AddInParameter(cmd,"@AccepterName",  DbType.String,entity.AccepterName);
			db.AddInParameter(cmd,"@CtType",  DbType.Int32,entity.CtType);
			db.AddInParameter(cmd,"@CountryCode",  DbType.String,entity.CountryCode);
			db.AddInParameter(cmd,"@CountryName",  DbType.String,entity.CountryName);
			db.AddInParameter(cmd,"@ProvinceId",  DbType.String,entity.ProvinceId);
			db.AddInParameter(cmd,"@CityId",  DbType.String,entity.CityId);
			db.AddInParameter(cmd,"@DistrictId",  DbType.String,entity.DistrictId);
			db.AddInParameter(cmd,"@Address",  DbType.String,entity.Address);
			db.AddInParameter(cmd,"@Postcode",  DbType.String,entity.Postcode);
			db.AddInParameter(cmd,"@Telephone",  DbType.String,entity.Telephone);
			db.AddInParameter(cmd,"@MobilePhone",  DbType.String,entity.MobilePhone);
			db.AddInParameter(cmd,"@IsDefault",  DbType.Int32,entity.IsDefault);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteOrderAddressByKey(int id)
	    {
			string sql=@"delete from OrderAddress where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderAddressDisabled()
        {
            string sql = @"delete from  OrderAddress  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderAddressByIds(string ids)
        {
            string sql = @"Delete from OrderAddress  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderAddressByIds(string ids)
        {
            string sql = @"Update   OrderAddress set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   OrderAddressEntity GetOrderAddress(int id)
		{
			string sql=@"SELECT  [Id],[OrderCode],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[DistrictId],[Address],[Postcode],[Telephone],[MobilePhone],[IsDefault],[Sort]
							FROM
							dbo.[OrderAddress] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		OrderAddressEntity entity=new OrderAddressEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbInt(reader["OrderCode"]);
					entity.AccepterName=StringUtils.GetDbString(reader["AccepterName"]);
					entity.CtType=StringUtils.GetDbInt(reader["CtType"]);
					entity.CountryCode=StringUtils.GetDbString(reader["CountryCode"]);
					entity.CountryName=StringUtils.GetDbString(reader["CountryName"]);
					entity.ProvinceId=StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId=StringUtils.GetDbInt(reader["CityId"]);
					entity.DistrictId=StringUtils.GetDbString(reader["DistrictId"]);
					entity.Address=StringUtils.GetDbString(reader["Address"]);
					entity.Postcode=StringUtils.GetDbString(reader["Postcode"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}

        /// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public OrderAddressEntity GetOrderAddressByOrderCode(long ordercode)
        {
            string sql = @"SELECT  [Id],[OrderCode],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[DistrictId],[Address],[Postcode],[Telephone],[MobilePhone],[IsDefault],[Sort]
							FROM
							dbo.[OrderAddress] WITH(NOLOCK)	
							WHERE [OrderCode]=@OrderCode";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@OrderCode", DbType.Int64, ordercode);
            OrderAddressEntity entity = new OrderAddressEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.OrderCode = StringUtils.GetDbLong(reader["OrderCode"]);
                    entity.AccepterName = StringUtils.GetDbString(reader["AccepterName"]);
                    entity.CtType = StringUtils.GetDbInt(reader["CtType"]);
                    entity.CountryCode = StringUtils.GetDbString(reader["CountryCode"]);
                    entity.CountryName = StringUtils.GetDbString(reader["CountryName"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.DistrictId = StringUtils.GetDbString(reader["DistrictId"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);
                    entity.Postcode = StringUtils.GetDbString(reader["Postcode"]);
                    entity.Telephone = StringUtils.GetDbString(reader["Telephone"]);
                    entity.MobilePhone = StringUtils.GetDbString(reader["MobilePhone"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<OrderAddressEntity> GetOrderAddressList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[OrderCode],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[DistrictId],[Address],[Postcode],[Telephone],[MobilePhone],[IsDefault],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[OrderCode],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[DistrictId],[Address],[Postcode],[Telephone],[MobilePhone],[IsDefault],[Sort] from dbo.[OrderAddress] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[OrderAddress] with (nolock) ";
            IList<OrderAddressEntity> entityList = new List< OrderAddressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					OrderAddressEntity entity=new OrderAddressEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbInt(reader["OrderCode"]);
					entity.AccepterName=StringUtils.GetDbString(reader["AccepterName"]);
					entity.CtType=StringUtils.GetDbInt(reader["CtType"]);
					entity.CountryCode=StringUtils.GetDbString(reader["CountryCode"]);
					entity.CountryName=StringUtils.GetDbString(reader["CountryName"]);
					entity.ProvinceId=StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId=StringUtils.GetDbInt(reader["CityId"]);
					entity.DistrictId=StringUtils.GetDbString(reader["DistrictId"]);
					entity.Address=StringUtils.GetDbString(reader["Address"]);
					entity.Postcode=StringUtils.GetDbString(reader["Postcode"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
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
        public IList<OrderAddressEntity> GetOrderAddressAll()
        {

            string sql = @"SELECT    [Id],[OrderCode],[AccepterName],[CtType],[CountryCode],[CountryName],[ProvinceId],[CityId],[DistrictId],[Address],[Postcode],[Telephone],[MobilePhone],[IsDefault],[Sort] from dbo.[OrderAddress] WITH(NOLOCK)	";
		    IList<OrderAddressEntity> entityList = new List<OrderAddressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   OrderAddressEntity entity=new OrderAddressEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.OrderCode=StringUtils.GetDbInt(reader["OrderCode"]);
					entity.AccepterName=StringUtils.GetDbString(reader["AccepterName"]);
					entity.CtType=StringUtils.GetDbInt(reader["CtType"]);
					entity.CountryCode=StringUtils.GetDbString(reader["CountryCode"]);
					entity.CountryName=StringUtils.GetDbString(reader["CountryName"]);
					entity.ProvinceId=StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.CityId=StringUtils.GetDbInt(reader["CityId"]);
					entity.DistrictId=StringUtils.GetDbString(reader["DistrictId"]);
					entity.Address=StringUtils.GetDbString(reader["Address"]);
					entity.Postcode=StringUtils.GetDbString(reader["Postcode"]);
					entity.Telephone=StringUtils.GetDbString(reader["Telephone"]);
					entity.MobilePhone=StringUtils.GetDbString(reader["MobilePhone"]);
					entity.IsDefault=StringUtils.GetDbInt(reader["IsDefault"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
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
        public int  ExistNum(OrderAddressEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[OrderAddress] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (AccepterName=@AccepterName) ";
					     where = where+ "  (CountryName=@CountryName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (AccepterName=@AccepterName) ";
					     where = where+ " id<>@Id and  (CountryName=@CountryName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@AccepterName", DbType.String, entity.AccepterName); 
					
            db.AddInParameter(cmd, "@CountryName", DbType.String, entity.CountryName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
