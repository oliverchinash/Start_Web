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
功能描述：MemFromBD表的数据访问类。
创建时间：2017/4/5 21:18:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemFromBDEntity的数据访问操作
	/// </summary>
	public partial class MemFromBDDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemFromBDDA Instance
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
            internal static readonly MemFromBDDA instance = new MemFromBDDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemFromBD，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memFromBD">待插入的实体对象</param>
		public int AddMemFromBD(MemFromBDEntity entity)
		{
		   string sql=@"insert into MemFromBD( [CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[UnRegisterId],[MemId])VALUES
			            ( @CompanyName,@CompanyAddress,@CompanyPhone,@Tags,@Province,@City,@PositionLat,@PositionLng,@Title,@UnRegisterId,@MemId);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CompanyName",  DbType.String,entity.CompanyName);
			db.AddInParameter(cmd,"@CompanyAddress",  DbType.String,entity.CompanyAddress);
			db.AddInParameter(cmd,"@CompanyPhone",  DbType.String,entity.CompanyPhone);
			db.AddInParameter(cmd,"@Tags",  DbType.String,entity.Tags);
			db.AddInParameter(cmd,"@Province",  DbType.String,entity.Province);
			db.AddInParameter(cmd,"@City",  DbType.String,entity.City);
			db.AddInParameter(cmd,"@PositionLat",  DbType.String,entity.PositionLat);
			db.AddInParameter(cmd,"@PositionLng",  DbType.String,entity.PositionLng);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@UnRegisterId",  DbType.Int32,entity.UnRegisterId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memFromBD">待更新的实体对象</param>
		public   int UpdateMemFromBD(MemFromBDEntity entity)
		{
			string sql=@" UPDATE dbo.[MemFromBD] SET
                       [CompanyName]=@CompanyName,[CompanyAddress]=@CompanyAddress,[CompanyPhone]=@CompanyPhone,[Tags]=@Tags,[Province]=@Province,[City]=@City,[PositionLat]=@PositionLat,[PositionLng]=@PositionLng,[Title]=@Title,[UnRegisterId]=@UnRegisterId,[MemId]=@MemId
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CompanyName",  DbType.String,entity.CompanyName);
			db.AddInParameter(cmd,"@CompanyAddress",  DbType.String,entity.CompanyAddress);
			db.AddInParameter(cmd,"@CompanyPhone",  DbType.String,entity.CompanyPhone);
			db.AddInParameter(cmd,"@Tags",  DbType.String,entity.Tags);
			db.AddInParameter(cmd,"@Province",  DbType.String,entity.Province);
			db.AddInParameter(cmd,"@City",  DbType.String,entity.City);
			db.AddInParameter(cmd,"@PositionLat",  DbType.String,entity.PositionLat);
			db.AddInParameter(cmd,"@PositionLng",  DbType.String,entity.PositionLng);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@UnRegisterId",  DbType.Int32,entity.UnRegisterId);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemFromBDByKey(int id)
	    {
			string sql=@"delete from MemFromBD where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemFromBDDisabled()
        {
            string sql = @"delete from  MemFromBD  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemFromBDByIds(string ids)
        {
            string sql = @"Delete from MemFromBD  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemFromBDByIds(string ids)
        {
            string sql = @"Update   MemFromBD set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemFromBDEntity GetMemFromBD(int id)
		{
			string sql=@"SELECT  [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[UnRegisterId],[MemId]
							FROM
							dbo.[MemFromBD] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemFromBDEntity entity=new MemFromBDEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.Tags=StringUtils.GetDbString(reader["Tags"]);
					entity.Province=StringUtils.GetDbString(reader["Province"]);
					entity.City=StringUtils.GetDbString(reader["City"]);
					entity.PositionLat=StringUtils.GetDbString(reader["PositionLat"]);
					entity.PositionLng=StringUtils.GetDbString(reader["PositionLng"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.UnRegisterId=StringUtils.GetDbInt(reader["UnRegisterId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<MemFromBDEntity> GetMemFromBDList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[UnRegisterId],[MemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[UnRegisterId],[MemId] from dbo.[MemFromBD] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemFromBD] with (nolock) ";
            IList<MemFromBDEntity> entityList = new List< MemFromBDEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemFromBDEntity entity=new MemFromBDEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.Tags=StringUtils.GetDbString(reader["Tags"]);
					entity.Province=StringUtils.GetDbString(reader["Province"]);
					entity.City=StringUtils.GetDbString(reader["City"]);
					entity.PositionLat=StringUtils.GetDbString(reader["PositionLat"]);
					entity.PositionLng=StringUtils.GetDbString(reader["PositionLng"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.UnRegisterId=StringUtils.GetDbInt(reader["UnRegisterId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
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
        public IList<MemFromBDEntity> GetMemFromBDAll()
        {

            string sql = @"SELECT    [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[UnRegisterId],[MemId] from dbo.[MemFromBD] WITH(NOLOCK)	";
		    IList<MemFromBDEntity> entityList = new List<MemFromBDEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemFromBDEntity entity=new MemFromBDEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.Tags=StringUtils.GetDbString(reader["Tags"]);
					entity.Province=StringUtils.GetDbString(reader["Province"]);
					entity.City=StringUtils.GetDbString(reader["City"]);
					entity.PositionLat=StringUtils.GetDbString(reader["PositionLat"]);
					entity.PositionLng=StringUtils.GetDbString(reader["PositionLng"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.UnRegisterId=StringUtils.GetDbInt(reader["UnRegisterId"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
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
        public int  ExistNum(MemFromBDEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemFromBD] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CompanyName=@CompanyName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CompanyName=@CompanyName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
