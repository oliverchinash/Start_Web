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
功能描述：ConfirmOrderCGMem表的数据访问类。
创建时间：2017/10/18 16:28:43
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// ConfirmOrderCGMemEntity的数据访问操作
	/// </summary>
	public partial class ConfirmOrderCGMemDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ConfirmOrderCGMemDA Instance
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
            internal static readonly ConfirmOrderCGMemDA instance = new ConfirmOrderCGMemDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ConfirmOrderCGMem，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="confirmOrderCGMem">待插入的实体对象</param>
		public int AddConfirmOrderCGMem(ConfirmOrderCGMemEntity entity)
		{
		   string sql=@"insert into ConfirmOrderCGMem( [ConfirmOrderCode],[CGMemId],[CGTotalPrice],[SelectTime])VALUES
			            ( @ConfirmOrderCode,@CGMemId,@CGTotalPrice,@SelectTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ConfirmOrderCode",  DbType.String,entity.ConfirmOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CGTotalPrice",  DbType.Decimal,entity.CGTotalPrice);
			db.AddInParameter(cmd,"@SelectTime",  DbType.DateTime,entity.SelectTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="confirmOrderCGMem">待更新的实体对象</param>
		public   int UpdateConfirmOrderCGMem(ConfirmOrderCGMemEntity entity)
		{
			string sql=@" UPDATE dbo.[ConfirmOrderCGMem] SET
                       [ConfirmOrderCode]=@ConfirmOrderCode,[CGMemId]=@CGMemId,[CGTotalPrice]=@CGTotalPrice,[SelectTime]=@SelectTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ConfirmOrderCode",  DbType.String,entity.ConfirmOrderCode);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@CGTotalPrice",  DbType.Decimal,entity.CGTotalPrice);
			db.AddInParameter(cmd,"@SelectTime",  DbType.DateTime,entity.SelectTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteConfirmOrderCGMemByKey(int id)
	    {
			string sql=@"delete from ConfirmOrderCGMem where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfirmOrderCGMemDisabled()
        {
            string sql = @"delete from  ConfirmOrderCGMem  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfirmOrderCGMemByIds(string ids)
        {
            string sql = @"Delete from ConfirmOrderCGMem  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfirmOrderCGMemByIds(string ids)
        {
            string sql = @"Update   ConfirmOrderCGMem set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ConfirmOrderCGMemEntity GetConfirmOrderCGMem(int id)
		{
			string sql=@"SELECT  [Id],[ConfirmOrderCode],[CGMemId],[CGTotalPrice],[SelectTime]
							FROM
							dbo.[ConfirmOrderCGMem] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ConfirmOrderCGMemEntity entity=new ConfirmOrderCGMemEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ConfirmOrderCode=StringUtils.GetDbString(reader["ConfirmOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.SelectTime=StringUtils.GetDbDateTime(reader["SelectTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ConfirmOrderCGMemEntity> GetConfirmOrderCGMemList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[ConfirmOrderCode],[CGMemId],[CGTotalPrice],[SelectTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ConfirmOrderCode],[CGMemId],[CGTotalPrice],[SelectTime] from dbo.[ConfirmOrderCGMem] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ConfirmOrderCGMem] with (nolock) ";
            IList<ConfirmOrderCGMemEntity> entityList = new List< ConfirmOrderCGMemEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ConfirmOrderCGMemEntity entity=new ConfirmOrderCGMemEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ConfirmOrderCode=StringUtils.GetDbString(reader["ConfirmOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.SelectTime=StringUtils.GetDbDateTime(reader["SelectTime"]);
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
        public IList<ConfirmOrderCGMemEntity> GetConfirmOrderCGMemAll()
        {

            string sql = @"SELECT    [Id],[ConfirmOrderCode],[CGMemId],[CGTotalPrice],[SelectTime] from dbo.[ConfirmOrderCGMem] WITH(NOLOCK)	";
		    IList<ConfirmOrderCGMemEntity> entityList = new List<ConfirmOrderCGMemEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   ConfirmOrderCGMemEntity entity=new ConfirmOrderCGMemEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ConfirmOrderCode=StringUtils.GetDbString(reader["ConfirmOrderCode"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.CGTotalPrice=StringUtils.GetDbDecimal(reader["CGTotalPrice"]);
					entity.SelectTime=StringUtils.GetDbDateTime(reader["SelectTime"]);
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
        public int  ExistNum(ConfirmOrderCGMemEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ConfirmOrderCGMem] WITH(NOLOCK) ";
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
