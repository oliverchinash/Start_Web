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
功能描述：CGProCertifacate表的数据访问类。
创建时间：2017/1/5 14:30:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGProductDB
{
	/// <summary>
	/// CGProCertifacateEntity的数据访问操作
	/// </summary>
	public partial class CGProCertifacateDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGProCertifacateDA Instance
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
            internal static readonly CGProCertifacateDA instance = new CGProCertifacateDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGProCertifacate，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGProCertifacate">待插入的实体对象</param>
		public int AddCGProCertifacate(CGProCertifacateEntity entity)
		{
		   string sql= @"insert into CGProCertifacate( [CGMemId],[Name],[PicUrl],[PicSuffix],[CreateTime],[HasChecked],[IsEffective],[CheckManId],[CheckTime])VALUES
			            ( @CGMemId,@Name,@PicUrl,@PicSuffix,@CreateTime,@HasChecked,@IsEffective,@CheckManId,@CheckTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@PicSuffix",  DbType.String,entity.PicSuffix);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd, "@HasChecked",  DbType.Int32,entity.HasChecked);
			db.AddInParameter(cmd, "@IsEffective",  DbType.Int32,entity.IsEffective);
            db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGProCertifacate">待更新的实体对象</param>
		public   int UpdateCGProCertifacate(CGProCertifacateEntity entity)
		{
			string sql= @" UPDATE dbo.[CGProCertifacate] SET
                       [CGMemId]=@CGMemId,[Name]=@Name,[PicUrl]=@PicUrl,[PicSuffix]=@PicSuffix,[CreateTime]=@CreateTime,[HasChecked]=@HasChecked,[IsEffective]=@IsEffective,[CheckManId]=@CheckManId,[CheckTime]=@CheckTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@PicSuffix",  DbType.String,entity.PicSuffix);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd, "@HasChecked",  DbType.Int32,entity.HasChecked);
			db.AddInParameter(cmd, "@IsEffective",  DbType.Int32,entity.IsEffective);
			db.AddInParameter(cmd,"@CheckManId",  DbType.Int32,entity.CheckManId);
			db.AddInParameter(cmd,"@CheckTime",  DbType.DateTime,entity.CheckTime);
    	 	return db.ExecuteNonQuery(cmd);
		}
        /// <summary>
        /// 更新未审核的产品资质
        /// </summary>
        /// <param name="cgmemid"></param>
        /// <param name="paths"></param>
        /// <returns></returns>
        public int UpdateProductLicense(int cgmemid, string paths)
        {
            string sql = @"EXEC Proc_ProductCerEdit @CGMemId, @PicPaths";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            db.AddInParameter(cmd, "@PicPaths", DbType.String, paths);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteCGProCertifacateByKey(int id)
	    {
			string sql=@"delete from CGProCertifacate where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProCertifacateDisabled()
        {
            string sql = @"delete from  CGProCertifacate  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProCertifacateByIds(string ids)
        {
            string sql = @"Delete from CGProCertifacate  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProCertifacateByIds(string ids)
        {
            string sql = @"Update   CGProCertifacate set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGProCertifacateEntity GetCGProCertifacate(int id)
		{
			string sql= @"SELECT  [Id],[CGMemId],[Name],[PicUrl],[PicSuffix],[CreateTime],[HasChecked],[IsEffective],[CheckManId],[CheckTime]
							FROM
							dbo.[CGProCertifacate] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGProCertifacateEntity entity=new CGProCertifacateEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
					entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<CGProCertifacateEntity> GetCGProCertifacateList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql= @"SELECT   [Id],[CGMemId],[Name],[PicUrl],[PicSuffix],[CreateTime],[HasChecked],IsEffective,[CheckManId],[CheckTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGMemId],[Name],[PicUrl],[PicSuffix],[CreateTime],[HasChecked],IsEffective,[CheckManId],[CheckTime] from dbo.[CGProCertifacate] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGProCertifacate] with (nolock) ";
            IList<CGProCertifacateEntity> entityList = new List< CGProCertifacateEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGProCertifacateEntity entity=new CGProCertifacateEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]); 
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public IList<CGProCertifacateEntity> GetCGProCertifacateAll(int memid)
        {

            string sql = @"SELECT    [Id],[CGMemId],[Name],[PicUrl],[PicSuffix],[CreateTime],[HasChecked],[IsEffective],[CheckManId],[CheckTime] from dbo.[CGProCertifacate] WITH(NOLOCK) Where CGMemId=@CGMemId	order by id desc";
		    IList<CGProCertifacateEntity> entityList = new List<CGProCertifacateEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGProCertifacateEntity entity=new CGProCertifacateEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
					entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public int  ExistNum(CGProCertifacateEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGProCertifacate] WITH(NOLOCK) ";
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
