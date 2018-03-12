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
功能描述：CGOrderVoucher表的数据访问类。
创建时间：2017/1/20 23:29:07
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
	/// <summary>
	/// CGOrderVoucherEntity的数据访问操作
	/// </summary>
	public partial class CGOrderVoucherDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGOrderVoucherDA Instance
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
            internal static readonly CGOrderVoucherDA instance = new CGOrderVoucherDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGOrderVoucher，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGOrderVoucher">待插入的实体对象</param>
		public int AddCGOrderVoucher(CGOrderVoucherEntity entity)
		{
		   string sql=@"insert into CGOrderVoucher( [CGOrderCode],[PicUrl],[PicSuffix],[CreateTime])VALUES
			            ( @CGOrderCode,@PicUrl,@PicSuffix,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGOrderCode",  DbType.Int32,entity.CGOrderCode);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@PicSuffix",  DbType.String,entity.PicSuffix);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGOrderVoucher">待更新的实体对象</param>
		public   int UpdateCGOrderVoucher(CGOrderVoucherEntity entity)
		{
			string sql=@" UPDATE dbo.[CGOrderVoucher] SET
                       [CGOrderCode]=@CGOrderCode,[PicUrl]=@PicUrl,[PicSuffix]=@PicSuffix,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGOrderCode",  DbType.Int32,entity.CGOrderCode);
			db.AddInParameter(cmd,"@PicUrl",  DbType.String,entity.PicUrl);
			db.AddInParameter(cmd,"@PicSuffix",  DbType.String,entity.PicSuffix);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGOrderVoucherByKey(int id)
	    {
			string sql=@"delete from CGOrderVoucher where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderVoucherDisabled()
        {
            string sql = @"delete from  CGOrderVoucher  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderVoucherByIds(string ids)
        {
            string sql = @"Delete from CGOrderVoucher  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        public int DelCGOrderVoucher(int memid, long code, int vid)
        {
            string sql = @"Delete a  from CGOrderVoucher  a inner join CGOrderTake b on a.CGOrderCode=b.CGOrderCode 
where a.Id=@Id and  a.[CGOrderCode]=@CGOrderCode and b.CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, vid);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, code);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderVoucherByIds(string ids)
        {
            string sql = @"Update   CGOrderVoucher set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 上传收货凭证
        /// </summary>
        /// <param name="cgmemid"></param>
        /// <param name="cgordercode"></param>
        /// <param name="paths"></param>
        /// <param name="delstrs"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public int UpLoadCGOrderVoucher(int cgmemid, long cgordercode, string paths,string delstrs,string remark)
        {
            string sql = @" 

SELECT  id ,
                value1 AS CerId ,
                SUBSTRING(value2, 1,
                          ( LEN(value2) - CHARINDEX('.', REVERSE(value2)) )) AS PicUrl ,
                REVERSE(SUBSTRING(REVERSE(value2), 1,
                                  CHARINDEX('.', REVERSE(value2)) - 1)) AS PicSuffix
        INTO    #temppics
        FROM    dbo.fun_SplitStrToTable(@PicPaths, '|', '_')
  SELECT Id as PicId  into #tempdelpics FROM dbo.fun_splitstr(@DelPics,'|')
IF EXISTS(SELECT 1 FROM dbo.CGOrderTake  b with(nolock) where  b.CGOrderCode=@CGOrderCode and b.CGMemId=@CGMemId and Status=@OldTakeStatus)
begin
begin tran
        DELETE FROM a FROM CGOrderVoucher  a INNER JOIN #tempdelpics b ON a.id=b.PicId
        INSERT  INTO  dbo.CGOrderVoucher
                (  CGOrderCode,
                  [PicUrl] ,
                  [PicSuffix] ,
                  [CreateTime]  
                 )
                SELECT  @CGOrderCode , 
                        PicUrl ,
                        PicSuffix ,
                        GETDATE()   
                FROM    #temppics a with(nolock)  where a.CerId = 0 
        update CGOrder set Status=@CGOrderStatus where Code=@CGOrderCode and status=@OldOrderStatus
        update CGOrderTake set Status=@CGTakeStatus,Remark=@Remark where CGOrderCode=@CGOrderCode and status=@OldTakeStatus 
commit tran
end
";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgordercode);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            db.AddInParameter(cmd, "@PicPaths", DbType.String, paths);
            db.AddInParameter(cmd, "@DelPics", DbType.String, delstrs);
            db.AddInParameter(cmd, "@CGOrderStatus", DbType.Int32, (int)CGOrderStatus.UploadRecivered);
            db.AddInParameter(cmd, "@OldOrderStatus", DbType.Int32, (int)CGOrderStatus.HasDelivery);
            db.AddInParameter(cmd, "@CGTakeStatus", DbType.Int32, (int)CGOrderTake.WaitCheckCer);
            db.AddInParameter(cmd, "@OldTakeStatus", DbType.Int32, (int)CGOrderTake.HasSend);
            db.AddInParameter(cmd, "@Remark", DbType.String, remark);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   CGOrderVoucherEntity GetCGOrderVoucher(int id)
		{
			string sql=@"SELECT  [Id],[CGOrderCode],[PicUrl],[PicSuffix],[CreateTime]
							FROM
							dbo.[CGOrderVoucher] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGOrderVoucherEntity entity=new CGOrderVoucherEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGOrderCode=StringUtils.GetDbInt(reader["CGOrderCode"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<CGOrderVoucherEntity> GetCGOrderVoucherList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[CGOrderCode],[PicUrl],[PicSuffix],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CGOrderCode],[PicUrl],[PicSuffix],[CreateTime] from dbo.[CGOrderVoucher] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGOrderVoucher] with (nolock) ";
            IList<CGOrderVoucherEntity> entityList = new List< CGOrderVoucherEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGOrderVoucherEntity entity=new CGOrderVoucherEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGOrderCode=StringUtils.GetDbInt(reader["CGOrderCode"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public IList<CGOrderVoucherEntity> GetCGOrderVoucherAll(long code)
        {

            string sql = @"SELECT    [Id],[CGOrderCode],[PicUrl],[PicSuffix],[CreateTime] from dbo.[CGOrderVoucher] WITH(NOLOCK) where CGOrderCode=@CGOrderCode	";
		    IList<CGOrderVoucherEntity> entityList = new List<CGOrderVoucherEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, code);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGOrderVoucherEntity entity=new CGOrderVoucherEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGOrderCode=StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.PicUrl=StringUtils.GetDbString(reader["PicUrl"]);
					entity.PicSuffix=StringUtils.GetDbString(reader["PicSuffix"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
        public int  ExistNum(CGOrderVoucherEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGOrderVoucher] WITH(NOLOCK) ";
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
