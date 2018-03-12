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
功能描述：Integral表的数据访问类。
创建时间：2016/12/1 14:30:42
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// IntegralEntity的数据访问操作
	/// </summary>
	public partial class IntegralDA: BaseSuperMarketDB
    {
        #region 实例化
        public static IntegralDA Instance
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
            internal static readonly IntegralDA instance = new IntegralDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Integral，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="integral">待插入的实体对象</param>
		public int AddIntegral(IntegralEntity entity)
		{
		   string sql=@"insert into Integral( [Id],[MemId],[AvailableIntegral],[FreezingIntegral],[Status])VALUES
			            ( @Id,@MemId,@AvailableIntegral,@FreezingIntegral,@Status)";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@AvailableIntegral",  DbType.Int32,entity.AvailableIntegral);
			db.AddInParameter(cmd,"@FreezingIntegral",  DbType.Int32,entity.FreezingIntegral);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			return db.ExecuteNonQuery(cmd);
 			
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="integral">待更新的实体对象</param>
		public   int UpdateIntegral(IntegralEntity entity)
		{
			string sql= @" UPDATE dbo.[Integral] SET
                       [Id]=@Id,[MemId]=@MemId,[AvailableIntegral]=@AvailableIntegral,[FreezingIntegral]=@FreezingIntegral,[Status]=@Status
                       WHERE [Id]=@id and  TimeStrampCode=@TimeStrampCode";
     
            DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@AvailableIntegral",  DbType.Int32,entity.AvailableIntegral);
			db.AddInParameter(cmd,"@FreezingIntegral",  DbType.Int32,entity.FreezingIntegral);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
            db.AddInParameter(cmd,"@TimeStrampCode", DbType.Binary, ByteUtils.GetByteFromString(entity.TimeStrampCode));
            return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteIntegralByKey(int id)
	    {
			string sql=@"delete from Integral where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIntegralDisabled()
        {
            string sql = @"delete from  Integral  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIntegralByIds(string ids)
        {
            string sql = @"Delete from Integral  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIntegralByIds(string ids)
        {
            string sql = @"Update   Integral set Status=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IntegralEntity GetIntegral(int id)
		{
			string sql= @"SELECT  [Id],[MemId],[AvailableIntegral],[FreezingIntegral],[Status],TimeStrampCode
							FROM
							dbo.[Integral] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		IntegralEntity entity=new IntegralEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AvailableIntegral=StringUtils.GetDbInt(reader["AvailableIntegral"]);
					entity.FreezingIntegral=StringUtils.GetDbInt(reader["FreezingIntegral"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					//entity.TimeStrampCode = StringUtils.GetDbByte(reader["TimeStrampCode"]);
                }
   		    }
         
            return entity;
		}
        public IntegralEntity GetIntegralByMemId(int memid)
        {
            string sql = @"SELECT  [Id],[MemId],[AvailableIntegral],[FreezingIntegral],[Status],TimeStrampCode
							FROM
							dbo.[Integral] WITH(NOLOCK)	
							WHERE [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            IntegralEntity entity = new IntegralEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.AvailableIntegral = StringUtils.GetDbInt(reader["AvailableIntegral"]);
                    entity.FreezingIntegral = StringUtils.GetDbInt(reader["FreezingIntegral"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.TimeStrampCode = ByteUtils.GetStringFromByte(reader["TimeStrampCode"] );
                }
            }
          
            return entity;
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<IntegralEntity> GetIntegralList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[MemId],[AvailableIntegral],[FreezingIntegral],[Status]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[AvailableIntegral],[FreezingIntegral],[Status] from dbo.[Integral] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[Integral] with (nolock) ";
            IList<IntegralEntity> entityList = new List< IntegralEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					IntegralEntity entity=new IntegralEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AvailableIntegral=StringUtils.GetDbInt(reader["AvailableIntegral"]);
					entity.FreezingIntegral=StringUtils.GetDbInt(reader["FreezingIntegral"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
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
        public IList<IntegralEntity> GetIntegralAll()
        {

            string sql = @"SELECT    [Id],[MemId],[AvailableIntegral],[FreezingIntegral],[Status] from dbo.[Integral] WITH(NOLOCK)	";
		    IList<IntegralEntity> entityList = new List<IntegralEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   IntegralEntity entity=new IntegralEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.AvailableIntegral=StringUtils.GetDbInt(reader["AvailableIntegral"]);
					entity.FreezingIntegral=StringUtils.GetDbInt(reader["FreezingIntegral"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
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
        public int  ExistNum(IntegralEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Integral] WITH(NOLOCK) ";
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
