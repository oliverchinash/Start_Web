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
功能描述：DicCoupons表的数据访问类。
创建时间：2017/3/25 18:44:47
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// DicCouponsEntity的数据访问操作
	/// </summary>
	public partial class DicCouponsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static DicCouponsDA Instance
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
            internal static readonly DicCouponsDA instance = new DicCouponsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表DicCoupons，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="dicCoupons">待插入的实体对象</param>
		public int AddDicCoupons(DicCouponsEntity entity)
		{
		   string sql=@"insert into DicCoupons( [Code],[Name],[CouponType],[CouponValue],[MinimumReqAmount],[ClassId],[BrandId],[Remark],[IsActive],[Sort],[EffectiveTime],[EffectiveUnit])VALUES
			            ( @Code,@Name,@CouponType,@CouponValue,@MinimumReqAmount,@ClassId,@BrandId,@Remark,@IsActive,@Sort,@EffectiveTime,@EffectiveUnit);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@CouponType",  DbType.Int32,entity.CouponType);
			db.AddInParameter(cmd,"@CouponValue",  DbType.Decimal,entity.CouponValue);
			db.AddInParameter(cmd,"@MinimumReqAmount",  DbType.Decimal,entity.MinimumReqAmount);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@EffectiveTime",  DbType.Int32,entity.EffectiveTime);
			db.AddInParameter(cmd,"@EffectiveUnit",  DbType.Int32,entity.EffectiveUnit);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="dicCoupons">待更新的实体对象</param>
		public   int UpdateDicCoupons(DicCouponsEntity entity)
		{
			string sql=@" UPDATE dbo.[DicCoupons] SET
                       [Code]=@Code,[Name]=@Name,[CouponType]=@CouponType,[CouponValue]=@CouponValue,[MinimumReqAmount]=@MinimumReqAmount,[ClassId]=@ClassId,[BrandId]=@BrandId,[Remark]=@Remark,[IsActive]=@IsActive,[Sort]=@Sort,[EffectiveTime]=@EffectiveTime,[EffectiveUnit]=@EffectiveUnit
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@CouponType",  DbType.Int32,entity.CouponType);
			db.AddInParameter(cmd,"@CouponValue",  DbType.Decimal,entity.CouponValue);
			db.AddInParameter(cmd,"@MinimumReqAmount",  DbType.Decimal,entity.MinimumReqAmount);
			db.AddInParameter(cmd,"@ClassId",  DbType.Int32,entity.ClassId);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@EffectiveTime",  DbType.Int32,entity.EffectiveTime);
			db.AddInParameter(cmd,"@EffectiveUnit",  DbType.Int32,entity.EffectiveUnit);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteDicCouponsByKey(int id)
	    {
			string sql=@"delete from DicCoupons where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicCouponsDisabled()
        {
            string sql = @"delete from  DicCoupons  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicCouponsByIds(string ids)
        {
            string sql = @"Delete from DicCoupons  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicCouponsByIds(string ids)
        {
            string sql = @"Update   DicCoupons set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   DicCouponsEntity GetDicCoupons(int id)
		{
			string sql= @"SELECT  [Id],[Code],[Name],[CouponType],[CouponValue],[MinimumReqAmount],[ClassId],[BrandId],[Remark],[IsActive],[Sort],[EffectiveTime],[EffectiveUnit],EffectiveType,EffectiveEndTime
							FROM
							dbo.[DicCoupons] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		DicCouponsEntity entity=new DicCouponsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.CouponType=StringUtils.GetDbInt(reader["CouponType"]);
					entity.CouponValue=StringUtils.GetDbDecimal(reader["CouponValue"]);
					entity.MinimumReqAmount=StringUtils.GetDbDecimal(reader["MinimumReqAmount"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.EffectiveTime=StringUtils.GetDbInt(reader["EffectiveTime"]);
                    entity.EffectiveUnit=StringUtils.GetDbInt(reader["EffectiveUnit"]);
                    entity.EffectiveType = StringUtils.GetDbInt(reader["EffectiveType"]);
                    entity.EffectiveEndTime = StringUtils.GetDbDateTime(reader["EffectiveEndTime"]);

                }
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<DicCouponsEntity> GetDicCouponsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[Code],[Name],[CouponType],[CouponValue],[MinimumReqAmount],[ClassId],[BrandId],[Remark],[IsActive],[Sort],[EffectiveTime],[EffectiveUnit],EffectiveType,EffectiveEndTime
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[CouponType],[CouponValue],[MinimumReqAmount],[ClassId],[BrandId],[Remark],[IsActive],[Sort],[EffectiveTime],[EffectiveUnit],EffectiveType,EffectiveEndTime from dbo.[DicCoupons] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[DicCoupons] with (nolock) ";
            IList<DicCouponsEntity> entityList = new List< DicCouponsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					DicCouponsEntity entity=new DicCouponsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.CouponType=StringUtils.GetDbInt(reader["CouponType"]);
					entity.CouponValue=StringUtils.GetDbDecimal(reader["CouponValue"]);
					entity.MinimumReqAmount=StringUtils.GetDbDecimal(reader["MinimumReqAmount"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.EffectiveTime=StringUtils.GetDbInt(reader["EffectiveTime"]);
					entity.EffectiveUnit=StringUtils.GetDbInt(reader["EffectiveUnit"]);
                    entity.EffectiveType = StringUtils.GetDbInt(reader["EffectiveType"]);
                    entity.EffectiveEndTime = StringUtils.GetDbDateTime(reader["EffectiveEndTime"]);
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
        public IList<DicCouponsEntity> GetDicCouponsAll()
        {

            string sql = @"SELECT    [Id],[Code],[Name],[CouponType],[CouponValue],[MinimumReqAmount],[ClassId],[BrandId],[Remark],[IsActive],[Sort],[EffectiveTime],[EffectiveUnit] from dbo.[DicCoupons] WITH(NOLOCK)	";
		    IList<DicCouponsEntity> entityList = new List<DicCouponsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   DicCouponsEntity entity=new DicCouponsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.CouponType=StringUtils.GetDbInt(reader["CouponType"]);
					entity.CouponValue=StringUtils.GetDbDecimal(reader["CouponValue"]);
					entity.MinimumReqAmount=StringUtils.GetDbDecimal(reader["MinimumReqAmount"]);
					entity.ClassId=StringUtils.GetDbInt(reader["ClassId"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.EffectiveTime=StringUtils.GetDbInt(reader["EffectiveTime"]);
					entity.EffectiveUnit=StringUtils.GetDbInt(reader["EffectiveUnit"]);
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
        public int  ExistNum(DicCouponsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[DicCoupons] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (Name=@Name) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (Name=@Name) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
