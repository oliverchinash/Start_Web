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
功能描述：InquiryOrderPics表的数据访问类。
创建时间：2017/8/23 11:12:06
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.JcOrderInquiry
{
	/// <summary>
	/// InquiryOrderPicsEntity的数据访问操作
	/// </summary>
	public partial class InquiryOrderPicsDA: BaseSuperMarketDB
    {
        #region 实例化
        public static InquiryOrderPicsDA Instance
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
            internal static readonly InquiryOrderPicsDA instance = new InquiryOrderPicsDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表InquiryOrderPics，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="inquiryOrderPics">待插入的实体对象</param>
		public int AddInquiryOrderPics(InquiryOrderPicsEntity entity)
		{
		   string sql=@"insert into InquiryOrderPics( [InquiryOrderCode],[PicUrl],[Sort])VALUES
			            ( @InquiryOrderCode,@PicUrl,@Sort);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
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
		/// <param name="inquiryOrderPics">待更新的实体对象</param>
		public   int UpdateInquiryOrderPics(InquiryOrderPicsEntity entity)
		{
			string sql=@" UPDATE dbo.[InquiryOrderPics] SET
                       [InquiryOrderCode]=@InquiryOrderCode,[PicUrl]=@PicUrl,[Sort]=@Sort
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@InquiryOrderCode",  DbType.String,entity.InquiryOrderCode);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteInquiryOrderPicsByKey(int id)
	    {
			string sql=@"delete from InquiryOrderPics where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
        public int DeletePicsFromOrder(string code,int id)
        {
            string sql = @"delete from InquiryOrderPics where InquiryOrderCode=@InquiryOrderCode and Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, code);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryOrderPicsDisabled()
        {
            string sql = @"delete from  InquiryOrderPics  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryOrderPicsByIds(string ids)
        {
            string sql = @"Delete from InquiryOrderPics  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryOrderPicsByIds(string ids)
        {
            string sql = @"Update   InquiryOrderPics set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   InquiryOrderPicsEntity GetInquiryOrderPics(int id)
		{
			string sql=@"SELECT  [Id],[InquiryOrderCode],[PicUrl],[Sort]
							FROM
							dbo.[InquiryOrderPics] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		InquiryOrderPicsEntity entity=new InquiryOrderPicsEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
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
		public   IList<InquiryOrderPicsEntity> GetInquiryOrderPicsList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[InquiryOrderCode],[PicUrl],[Sort]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[InquiryOrderCode],[PicUrl],[Sort] from dbo.[InquiryOrderPics] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[InquiryOrderPics] with (nolock) ";
            IList<InquiryOrderPicsEntity> entityList = new List< InquiryOrderPicsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					InquiryOrderPicsEntity entity=new InquiryOrderPicsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
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
        public IList<InquiryOrderPicsEntity> GetInquiryOrderPicsAllByCode(string ordercode)
        { 
            string sql = @"SELECT [Id],[InquiryOrderCode],[PicUrl],[Sort] from dbo.[InquiryOrderPics] WITH(NOLOCK)	where InquiryOrderCode=@InquiryOrderCode ";
		    IList<InquiryOrderPicsEntity> entityList = new List<InquiryOrderPicsEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@InquiryOrderCode", DbType.String, ordercode);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   InquiryOrderPicsEntity entity=new InquiryOrderPicsEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.InquiryOrderCode=StringUtils.GetDbString(reader["InquiryOrderCode"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
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
        public int  ExistNum(InquiryOrderPicsEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[InquiryOrderPics] WITH(NOLOCK) ";
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
