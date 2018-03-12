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
功能描述：Coupons表的数据访问类。
创建时间：2017/3/25 18:44:15
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// MemCouponsEntity的数据访问操作
	/// </summary>
	public partial class MemCouponsDA : BaseSuperMarketDB
    {
        #region 实例化
        public static MemCouponsDA Instance
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
            internal static readonly MemCouponsDA instance = new MemCouponsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Coupons，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="coupons">待插入的实体对象</param>
		public int AddMemCoupons(MemCouponsEntity entity)
		{
		   string sql= @"insert into MemCoupons( [CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort],Status)VALUES
			            ( @CouponsId,@MemId,@CreateTime,@EndTime,@Num,@Sort,@Status);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CouponsId",  DbType.Int32,entity.CouponsId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd, "@Status",  DbType.Int32,entity.Status);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="coupons">待更新的实体对象</param>
		public   int UpdateMemCoupons(MemCouponsEntity entity)
		{
			string sql= @" UPDATE dbo.[MemCoupons] SET
                       [CouponsId]=@CouponsId,[MemId]=@MemId,[CreateTime]=@CreateTime,[EndTime]=@EndTime,[Num]=@Num,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CouponsId",  DbType.Int32,entity.CouponsId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@Num",  DbType.Int32,entity.Num);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int DeleteMemCouponsByKey(int id)
	    {
			string sql= @"delete from MemCoupons where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemCouponsDisabled()
        {
            string sql = @"delete from  MemCoupons  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemCouponsByIds(string ids)
        {
            string sql = @"Delete from MemCoupons  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemCouponsByIds(string ids)
        {
            string sql = @"Update   MemCoupons set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemCouponsEntity GetMemCoupons(int id)
		{
			string sql= @"SELECT  [Id],[CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort]
							FROM
							dbo.[MemCoupons] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemCouponsEntity entity=new MemCouponsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CouponsId=StringUtils.GetDbInt(reader["CouponsId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<MemCouponsEntity> GetMemCouponsList(int pageindex, int pagesize,  ref  int recordCount, int status, int memid)
		{
            string where = " where 1=1 ";
            if(status==1)
            {
                where += " and Status=1 and EndTime>getdate() and createTime<getdate() ";
            }
            else if (status == 2)
            {
                where += " and Status=2 ";
            }
            else if (status == 3)
            {
                where += " and Status=1 and EndTime<getdate()";
            }
            else
            {
                where += " and 1=0 ";
            }
            if (memid > 0)
            {
                where += " and MemId=@MemId";
            } 
            string sql= @"SELECT   [Id],[CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort],Status
						FROM (SELECT ROW_NUMBER() OVER (ORDER BY EndTime  asc) AS ROWNUMBER,
						 [Id],[CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort],Status from dbo.[MemCoupons] WITH(NOLOCK)	
						" + where + @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemCoupons] with (nolock) "+ where;
            IList<MemCouponsEntity> entityList = new List< MemCouponsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid > 0)
            { 
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemCouponsEntity entity=new MemCouponsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CouponsId=StringUtils.GetDbInt(reader["CouponsId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
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

        public IList<MemCouponsEntity> GetCanUseCoupons(int memid)
        {
             
            string sql = @"SELECT DISTINCT [CouponsId],Sort from dbo.[MemCoupons] WITH(NOLOCK)WHERE STATUS =1 AND endtime> GETDATE()   and MemId=@MemId Order by Sort desc";

             IList<MemCouponsEntity> entityList = new List<MemCouponsEntity>();
             DbCommand cmd = db.GetSqlStringCommand(sql);
             db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
           
             using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemCouponsEntity entity = new MemCouponsEntity(); 
                    entity.CouponsId = StringUtils.GetDbInt(reader["CouponsId"]); 
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]); 
                    entityList.Add(entity);
                }
            }
         
            return entityList;
        }
        public int GetCanUseCouponsNum(int memid )
        {

            string sql = @"SELECT count(1)  from dbo.[MemCoupons] WITH(NOLOCK)WHERE STATUS =1 AND endtime> GETDATE()   and MemId=@MemId  ";

            IList<MemCouponsEntity> entityList = new List<MemCouponsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
      
        }

        public MemCouponsEntity GetByCouponIdAndMem(int couponid, int memid)
        {
            string sql = @"SELECT top 1 [Id],[CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort],Status from dbo.[MemCoupons] WITH(NOLOCK)WHERE STATUS =1 AND endtime> GETDATE()   and MemId=@MemId and CouponsId=@CouponsId Order by EndTime  asc";

            MemCouponsEntity entity = new MemCouponsEntity();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@CouponsId", DbType.Int32, couponid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                { 
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CouponsId = StringUtils.GetDbInt(reader["CouponsId"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }

            return entity;
        }
        public MemCouponsEntity GetByMemCouponId(int memid,int memcouponid)
        {
            string sql = @"SELECT  [Id],[CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort],Status
							FROM
							dbo.[MemCoupons] WITH(NOLOCK)	
							WHERE [Id]=@id and MemId=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, memcouponid);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            MemCouponsEntity entity = new MemCouponsEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CouponsId = StringUtils.GetDbInt(reader["CouponsId"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                }
            }
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<MemCouponsEntity> GetMemCouponsAll()
        {

            string sql = @"SELECT    [Id],[CouponsId],[MemId],[CreateTime],[EndTime],[Num],[Sort] from dbo.[MemCoupons] WITH(NOLOCK)	";
		    IList<MemCouponsEntity> entityList = new List<MemCouponsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemCouponsEntity entity=new MemCouponsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CouponsId=StringUtils.GetDbInt(reader["CouponsId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.Num=StringUtils.GetDbInt(reader["Num"]);
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
        public int  ExistNum(MemCouponsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemCoupons] WITH(NOLOCK) ";
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
