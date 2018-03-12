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
功能描述：DicInquiryOrder表的数据访问类。
创建时间：2017/8/23 11:12:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// DicInquiryOrderEntity的数据访问操作
	/// </summary>
	public partial class DicInquiryOrderDA: BaseSuperMarketDB
    {
        #region 实例化
        public static DicInquiryOrderDA Instance
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
            internal static readonly DicInquiryOrderDA instance = new DicInquiryOrderDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表DicInquiryOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="dicInquiryOrder">待插入的实体对象</param>
		public int AddDicInquiryOrder(DicInquiryOrderEntity entity)
		{
		   string sql=@"insert into DicInquiryOrder( [Code],[Name],[ParentId],[MenuCode],[IsActive])VALUES
			            ( @Code,@Name,@ParentId,@MenuCode,@IsActive);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.Int32,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@MenuCode",  DbType.String,entity.MenuCode);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="dicInquiryOrder">待更新的实体对象</param>
		public   int UpdateDicInquiryOrder(DicInquiryOrderEntity entity)
		{
			string sql=@" UPDATE dbo.[DicInquiryOrder] SET
                       [Code]=@Code,[Name]=@Name,[ParentId]=@ParentId,[MenuCode]=@MenuCode,[IsActive]=@IsActive
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.Int32,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
			db.AddInParameter(cmd,"@MenuCode",  DbType.String,entity.MenuCode);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteDicInquiryOrderByKey(int id)
	    {
			string sql=@"delete from DicInquiryOrder where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicInquiryOrderDisabled()
        {
            string sql = @"delete from  DicInquiryOrder  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicInquiryOrderByIds(string ids)
        {
            string sql = @"Delete from DicInquiryOrder  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicInquiryOrderByIds(string ids)
        {
            string sql = @"Update   DicInquiryOrder set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   DicInquiryOrderEntity GetDicInquiryOrder(int id)
		{
			string sql=@"SELECT  [Id],[Code],[Name],[ParentId],[MenuCode],[IsActive]
							FROM
							dbo.[DicInquiryOrder] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		DicInquiryOrderEntity entity=new DicInquiryOrderEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbInt(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.MenuCode=StringUtils.GetDbString(reader["MenuCode"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<DicInquiryOrderEntity> GetDicInquiryOrderList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Code],[Name],[ParentId],[MenuCode],[IsActive]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[ParentId],[MenuCode],[IsActive] from dbo.[DicInquiryOrder] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[DicInquiryOrder] with (nolock) ";
            IList<DicInquiryOrderEntity> entityList = new List< DicInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					DicInquiryOrderEntity entity=new DicInquiryOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbInt(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.MenuCode=StringUtils.GetDbString(reader["MenuCode"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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

        public IList<DicInquiryOrderEntity> GetDicInquiryOrderShow(int parentid, int classid)
        {
            string where = " where 1=1 ";
            if(parentid>0)
            {
                where += " and ParentId=@ParentId ";
            }
            if (classid > 0)
            {
                where += " and ClassId=@ClassId ";
            }
            string sql = @" select top 50 [Id],[Code],[Name],[ParentId],ClassId,[MenuCode],[IsActive] from dbo.[DicInquiryOrder] WITH(NOLOCK)	
					 "+ where + " Order By Name asc ";

              IList<DicInquiryOrderEntity> entityList = new List<DicInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (parentid > 0)
            { 
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, parentid);
            }
            if (classid > 0)
            { 
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DicInquiryOrderEntity entity = new DicInquiryOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbInt(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.MenuCode = StringUtils.GetDbString(reader["MenuCode"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entityList.Add(entity);
                }
            } 
            return entityList;
        }

        public IList<DicInquiryOrderEntity> GetDicFromCode(string parentcode, string menucode, bool cache = true)
        { 
    
            string sql = @" select top 50 [Id],[Code],[Name],[ParentId],[ParentCode],ClassId,[MenuCode],[IsActive] from dbo.[DicInquiryOrder] WITH(NOLOCK)	
					 where  ParentId=(select top 1 id from dbo.[DicInquiryOrder] WITH(NOLOCK) where  MenuCode=@MenuCode and Code=@Code)  Order By Name asc ";

            IList<DicInquiryOrderEntity> entityList = new List<DicInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
                db.AddInParameter(cmd, "@Code", DbType.String, parentcode);
                db.AddInParameter(cmd, "@MenuCode", DbType.String, menucode);
             
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DicInquiryOrderEntity entity = new DicInquiryOrderEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbInt(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.ClassId = StringUtils.GetDbInt(reader["ClassId"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.ParentCode = StringUtils.GetDbInt(reader["ParentCode"]);
                    entity.MenuCode = StringUtils.GetDbString(reader["MenuCode"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
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
        public IList<DicInquiryOrderEntity> GetInquiryDicAllByMenuCode(string menucode)
        {

            string sql = @"SELECT    [Id],[Code],[Name],[ParentId],[MenuCode],[IsActive] from dbo.[DicInquiryOrder] WITH(NOLOCK)	where MenuCode=@MenuCode and isactive=1";
		    IList<DicInquiryOrderEntity> entityList = new List<DicInquiryOrderEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MenuCode", DbType.String, menucode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   DicInquiryOrderEntity entity=new DicInquiryOrderEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbInt(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.MenuCode=StringUtils.GetDbString(reader["MenuCode"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
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
        public int  ExistNum(DicInquiryOrderEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[DicInquiryOrder] WITH(NOLOCK) ";
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
