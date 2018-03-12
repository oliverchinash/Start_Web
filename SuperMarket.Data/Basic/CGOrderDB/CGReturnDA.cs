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
功能描述：CGReturn表的数据访问类。
创建时间：2017/1/20 16:26:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGOrderDB
{
	/// <summary>
	/// CGReturnEntity的数据访问操作
	/// </summary>
	public partial class CGReturnDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGReturnDA Instance
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
            internal static readonly CGReturnDA instance = new CGReturnDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGReturn，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="cGReturn">待插入的实体对象</param>
		public int AddCGReturn(CGReturnEntity entity)
		{
		   string sql= @"
IF not  EXISTS(SELECT 1 FROM CGReturn where CGMemId=@CGMemId and B2BReturnXSId=@B2BReturnXSId)
begin
insert into CGReturn( [CGMemId],[B2BReturnXSId],[Status],ReturnNum,Remark)VALUES
			            ( @CGMemId,@B2BReturnXSId,@Status,@ReturnNum,@Remark); 	 
end
else
begin
UPDATE dbo.[CGReturn] SET  [CGMemId]=@CGMemId,[B2BReturnXSId]=@B2BReturnXSId, ReturnNum=@ReturnNum,Remark=@Remark where 
 CGMemId=@CGMemId and B2BReturnXSId=@B2BReturnXSId
end

";

	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@B2BReturnXSId",  DbType.Int32,entity.B2BReturnXSId);
			db.AddInParameter(cmd, "@ReturnNum",  DbType.Int32,entity.ReturnNum);
			db.AddInParameter(cmd, "@Remark",  DbType.Int32,entity.Remark);
            db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}


        public int SaveCGReturn(CGReturnEntity entity)
        {
            string sql = @"
IF not  EXISTS(SELECT 1 FROM CGReturn with(nolock) where CGMemId=@CGMemId and B2BReturnXSId=@B2BReturnXSId)
begin
insert into CGReturn( [CGMemId],[B2BReturnXSId],B2BNewOrderCode,ReturnType,[Status],ReturnNum,Remark,CGOrderCode,ProductId,ProvinceId,CityId,Address,ManName,Phone)VALUES
			            ( @CGMemId,@B2BReturnXSId,@B2BNewOrderCode,@ReturnType,@Status,@ReturnNum,@Remark,@CGOrderCode,@ProductId,@ProvinceId,@CityId,@Address,@ManName,@Phone); 	 
end
else
begin
UPDATE dbo.[CGReturn] SET  [CGMemId]=@CGMemId,B2BNewOrderCode=@B2BNewOrderCode,ReturnType=@ReturnType,[B2BReturnXSId]=@B2BReturnXSId, ReturnNum=@ReturnNum,Remark=@Remark,CGOrderCode=@CGOrderCode,ProductId=@ProductId,ProvinceId=@ProvinceId,CityId=@CityId,Address=@Address,ManName=@ManName,Phone=@Phone where 
 CGMemId=@CGMemId and B2BReturnXSId=@B2BReturnXSId
end

";

            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@B2BReturnXSId", DbType.Int32, entity.B2BReturnXSId);
            db.AddInParameter(cmd, "@ReturnNum", DbType.Int32, entity.ReturnNum);
            db.AddInParameter(cmd, "@B2BNewOrderCode", DbType.Int64, entity.B2BNewOrderCode);
            db.AddInParameter(cmd, "@ReturnType", DbType.Int32, entity.ReturnType);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, entity.CGOrderCode);
            db.AddInParameter(cmd, "@ProductId", DbType.Int32, entity.ProductId);
            db.AddInParameter(cmd, "@ProvinceId", DbType.Int32, entity.ProvinceId);
            db.AddInParameter(cmd, "@CityId", DbType.Int32, entity.CityId);
            db.AddInParameter(cmd, "@Address", DbType.String, entity.Address);
            db.AddInParameter(cmd, "@ManName", DbType.String, entity.ManName);
            db.AddInParameter(cmd, "@Phone", DbType.String, entity.Phone);
            db.AddInParameter(cmd, "@Status", DbType.Int32, entity.Status); 
  

            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="cGReturn">待更新的实体对象</param>
        public   int UpdateCGReturn(CGReturnEntity entity)
		{
			string sql=@" UPDATE dbo.[CGReturn] SET
                       [CGMemId]=@CGMemId,[B2BReturnXSId]=@B2BReturnXSId,[Status]=@Status
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CGMemId",  DbType.Int32,entity.CGMemId);
			db.AddInParameter(cmd,"@B2BReturnXSId",  DbType.Int32,entity.B2BReturnXSId);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
    	 	return db.ExecuteNonQuery(cmd);
		}

        public int NoteMsgToCGMems(int returnid)
        {
            string sql = @"  
UPDATE dbo.[CGReturn] SET [Status]=@Status  WHERE [B2BReturnXSId]=@B2BReturnXSId  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@B2BReturnXSId", DbType.Int32, returnid); 
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)CGReturnStatus.Accept); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public  int  DeleteCGReturnByKey(int id)
	    {
			string sql=@"delete from CGReturn where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGReturnDisabled()
        {
            string sql = @"delete from  CGReturn  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGReturnByIds(string ids)
        {
            string sql = @"Delete from CGReturn  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGReturnByIds(string ids)
        {
            string sql = @"Update   CGReturn set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGReturnEntity GetCGReturn(int id)
		{
			string sql=@"SELECT  [Id],[CGMemId],[B2BReturnXSId],[Status]
							FROM
							dbo.[CGReturn] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGReturnEntity entity=new CGReturnEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.B2BReturnXSId=StringUtils.GetDbInt(reader["B2BReturnXSId"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
				}
   		    }
            return entity;
		}
        public int ReBackRecived(long cgordercode, int rebackid, int memid)
        {
            string sql = @"Update   CGReturn set Status=@CGReturnStatus where CGOrderCode=@CGOrderCode and B2BReturnXSId=@B2BReturnXSId and CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@CGReturnStatus", DbType.Int32, (int)CGReturnStatus.HasRecived);
            db.AddInParameter(cmd, "@CGOrderCode", DbType.Int64, cgordercode);
            db.AddInParameter(cmd, "@B2BReturnXSId", DbType.Int32, rebackid);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }


        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGReturnEntity> GetCGReturnById(int id)
        {
            string sql = @"SELECT  [Id],[CGMemId],[B2BReturnXSId],[Status],[CGOrderCode],[ProductId],[ReturnNum],[ProvinceId],[CityId],[Phone],[ManName],[Address]
							FROM
							dbo.[CGReturn] WITH(NOLOCK)	
							WHERE [B2BReturnXSId]=@B2BReturnXSId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            IList<CGReturnEntity> entityList = new List<CGReturnEntity>();

            db.AddInParameter(cmd, "@B2BReturnXSId", DbType.Int32, id);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    CGReturnEntity entity = new CGReturnEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
                    entity.B2BReturnXSId = StringUtils.GetDbInt(reader["B2BReturnXSId"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);

                    entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
                    entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
                    entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
                    entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
                    entity.Phone = StringUtils.GetDbString(reader["Phone"]);
                    entity.ManName = StringUtils.GetDbString(reader["ManName"]);
                    entity.Address = StringUtils.GetDbString(reader["Address"]);

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
        public IList<VWCGReturnEntity> GetVWCGReturnList(int pagesize, int pageindex, ref  int recordCount ,string key ,int memid ,int _status)
		{
            string where = " where 1=1 ";
            if(!string.IsNullOrEmpty(key))
            {
                where += " and a.CGOrderCode LIKE @Key ";
            }
            if (memid!=-1)
            {
                where += " and a.CGMemId=@CGMemId ";
            }
            if (_status != -1)
            {
                where += " and a.Status=@Status ";
            }
            string sql= @"SELECT   [Id],[CGMemId]
                          ,[B2BReturnXSId]
                          ,[CGOrderCode]
                          ,[ProductId]
                          ,Num
                          ,[ReturnNum]
                          ,[Status]
                          ,[ProvinceId]
                          ,[Remark]
                          ,[CityId]
                          ,[Address]
                          ,[ManName]
                          ,[Phone]
                          ,[CreateTime],Name, CGPrice, CGTotalPrice
						                    FROM
						                    (SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						                     a.[Id],a.[CGMemId]
                          ,a.[B2BReturnXSId]
                          ,a.[CGOrderCode]
                          ,a.[ProductId]
                          ,b.[Num]
                          ,a.[ReturnNum]
                          ,a.[Status]
                          ,a.[ProvinceId]
                          ,a.[Remark]
                          ,a.[CityId]
                          ,a.[Address]
                          ,a.[ManName]
                          ,a.[Phone]
                          ,a.[CreateTime],b.Name,b.CGPrice,b.CGTotalPrice from dbo.[CGReturn] a  WITH(NOLOCK)	 inner join CGOrderDetail b WITH(NOLOCK) on a.CGOrderCode=b.CGOrderCode
                    and a.ProductId=b.ProductId " + where + @" ) as temp 
						                    where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGReturn] a with (nolock) "+where;
            IList<VWCGReturnEntity> entityList = new List<VWCGReturnEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(key))
            { 
                db.AddInParameter(cmd, "@Key", DbType.String, "%"+ key+"%");
            }
            if (memid != -1)
            { 
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (_status != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, _status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					VWCGReturnEntity entity=new VWCGReturnEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.B2BReturnXSId=StringUtils.GetDbInt(reader["B2BReturnXSId"]);
					entity.CGOrderCode = StringUtils.GetDbLong(reader["CGOrderCode"]);
					entity.ProductId = StringUtils.GetDbInt(reader["ProductId"]);
					entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Num = StringUtils.GetDbInt(reader["Num"]);
					entity.ReturnNum = StringUtils.GetDbInt(reader["ReturnNum"]);
					entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.ProvinceId = StringUtils.GetDbInt(reader["ProvinceId"]);
					entity.Remark = StringUtils.GetDbString(reader["Remark"]);
					entity.CityId = StringUtils.GetDbInt(reader["CityId"]);
					entity.Address = StringUtils.GetDbString(reader["Address"]);
					entity.ManName = StringUtils.GetDbString(reader["ManName"]);
					entity.Phone = StringUtils.GetDbString(reader["Phone"]);
					entity.CreateTime = StringUtils.GetDateTime(reader["CreateTime"]);
					entity.ManName = StringUtils.GetDbString(reader["ManName"]);
					entity.CGPrice = StringUtils.GetDbDecimal(reader["CGPrice"]);
					entity.CGTotalPrice = StringUtils.GetDbDecimal(reader["CGTotalPrice"]);

                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@Key", DbType.String, "%" + key + "%");
            }
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (_status != -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, _status);
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
		
		        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<CGReturnEntity> GetCGReturnAll(int b2breturnid)
        {

            string sql = @"SELECT    [Id],[CGMemId],[B2BReturnXSId],[Status] from dbo.[CGReturn] WITH(NOLOCK) where B2BReturnXSId=@B2BReturnXSId	";
		    IList<CGReturnEntity> entityList = new List<CGReturnEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@B2BReturnXSId", DbType.Int32, b2breturnid);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGReturnEntity entity=new CGReturnEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CGMemId=StringUtils.GetDbInt(reader["CGMemId"]);
					entity.B2BReturnXSId=StringUtils.GetDbInt(reader["B2BReturnXSId"]);
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
        public int  ExistNum(CGReturnEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGReturn] WITH(NOLOCK) ";
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
