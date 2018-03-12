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
功能描述：MemFromBaidu表的数据访问类。
创建时间：2017/4/5 15:18:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemFromBaiduEntity的数据访问操作
	/// </summary>
	public partial class MemFromBaiduDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemFromBaiduDA Instance
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
            internal static readonly MemFromBaiduDA instance = new MemFromBaiduDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemFromBaidu，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memFromBaidu">待插入的实体对象</param>
		public int AddMemFromBaidu(MemFromBaiduEntity entity)
		{
		   string sql=@"insert into MemFromBaidu( [CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi])VALUES
			            ( @CompanyName,@CompanyAddress,@CompanyPhone,@Tags,@Province,@City,@PositionLat,@PositionLng,@Title,@CenterPosition,@JuLi);
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
			db.AddInParameter(cmd,"@CenterPosition",  DbType.String,entity.CenterPosition);
			db.AddInParameter(cmd,"@JuLi",  DbType.String,entity.JuLi);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
        public int AddOrEditMemFromBaidu(MemFromBaiduEntity entity)
        {
            string sql = @"
IF EXISTS(SELECT 1 FROM  MemFromBaidu WHERE [CompanyName]=@CompanyName AND   [CompanyAddress]=@CompanyAddress)
BEGIN 
 UPDATE MemFromBaidu SET [PositionLat]=@PositionLat ,[PositionLng]=@PositionLng,UpdateTime=getdate()  WHERE [CompanyName]=@CompanyName AND   [CompanyAddress]=@CompanyAddress
END
ELSE
BEGIN
insert into MemFromBaidu( [CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi],UpdateTime)VALUES
			            ( @CompanyName,@CompanyAddress,@CompanyPhone,@Tags,@Province,@City,@PositionLat,@PositionLng,@Title,@CenterPosition,@JuLi,getdate());
 END 
insert into MemFromBaiduTemp( [CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi],UpdateTime)VALUES
			            ( @CompanyName,@CompanyAddress,@CompanyPhone,@Tags,@Province,@City,@PositionLat,@PositionLng,@Title,@CenterPosition,@JuLi,getdate());

 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, entity.CompanyAddress);
            db.AddInParameter(cmd, "@CompanyPhone", DbType.String, entity.CompanyPhone);
            db.AddInParameter(cmd, "@Tags", DbType.String, entity.Tags);
            db.AddInParameter(cmd, "@Province", DbType.String, entity.Province);
            db.AddInParameter(cmd, "@City", DbType.String, entity.City);
            db.AddInParameter(cmd, "@PositionLat", DbType.String, entity.PositionLat);
            db.AddInParameter(cmd, "@PositionLng", DbType.String, entity.PositionLng);
            db.AddInParameter(cmd, "@Title", DbType.String, entity.Title);
            db.AddInParameter(cmd, "@CenterPosition", DbType.String, entity.CenterPosition);
            db.AddInParameter(cmd, "@JuLi", DbType.String, entity.JuLi);
            return  db.ExecuteNonQuery(cmd); 
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="memFromBaidu">待更新的实体对象</param>
        public   int UpdateMemFromBaidu(MemFromBaiduEntity entity)
		{
			string sql=@" UPDATE dbo.[MemFromBaidu] SET
                       [CompanyName]=@CompanyName,[CompanyAddress]=@CompanyAddress,[CompanyPhone]=@CompanyPhone,[Tags]=@Tags,[Province]=@Province,[City]=@City,[PositionLat]=@PositionLat,[PositionLng]=@PositionLng,[Title]=@Title,[CenterPosition]=@CenterPosition,[JuLi]=@JuLi
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
			db.AddInParameter(cmd,"@CenterPosition",  DbType.String,entity.CenterPosition);
			db.AddInParameter(cmd,"@JuLi",  DbType.String,entity.JuLi);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteMemFromBaiduByKey(int id)
	    {
			string sql=@"delete from MemFromBaidu where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemFromBaiduDisabled()
        {
            string sql = @"delete from  MemFromBaidu  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemFromBaiduByIds(string ids)
        {
            string sql = @"Delete from MemFromBaidu  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        public int SetPrintCodeUnRegister(string posstrs, Int64 code)
        {
            string sql = @"update a set PrintCode=@PrintCode  FROM MemFromBaidu a inner JOIN    dbo.fun_SplitStrToTable(@StrPostions, '|', ',')  w 
ON a.PositionLng = w.value2 AND a.PositionLat = w.value3";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@StrPostions", DbType.String, posstrs);
            db.AddInParameter(cmd, "@PrintCode", DbType.Int64, code);
            return db.ExecuteNonQuery(cmd); 
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemFromBaiduByIds(string ids)
        {
            string sql = @"Update   MemFromBaidu set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   MemFromBaiduEntity GetMemFromBaidu(int id)
		{
			string sql=@"SELECT  [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi]
							FROM
							dbo.[MemFromBaidu] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		MemFromBaiduEntity entity=new MemFromBaiduEntity();
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
					entity.CenterPosition=StringUtils.GetDbString(reader["CenterPosition"]);
					entity.JuLi=StringUtils.GetDbString(reader["JuLi"]);
				}
   		    }
            return entity;
		}
        public int SetMemberPosition(int memid, decimal posilng, decimal posilat, string title,string address)
        {
            string sql = @" 
UPDATE dbo.Store SET Latitude=@Latitude ,Longitude=@Longitude WHERE MemId=@MemId 
IF EXISTS(SELECT 1 FROM dbo.MemFromBaidu a WITH(NOLOCK)  inner join dbo.Store b WITH(NOLOCK)   on a.MemId=b.MemId and a.CompanyName=b.CompanyName and a.CompanyAddress=b.Address WHERE  a.MemId=@MemId   )
BEGIN 
 delete a  from  dbo.MemFromBaidu a inner join dbo.Store b on a.MemId=b.MemId and a.CompanyName=b.CompanyName and a.CompanyAddress=b.Address WHERE  a.MemId=@MemId 
 UPDATE  dbo.MemFromBaidu SET  MemId=0  WHERE  MemId=@MemId 
END
else IF EXISTS(SELECT 1 FROM dbo.MemFromBaidu a  WHERE  MemId=@MemId   )
BEGIN 
  UPDATE  dbo.MemFromBaidu SET  MemId=0  WHERE  MemId=@MemId 
END 
IF EXISTS(SELECT 1 FROM dbo.MemFromBaidu WHERE PositionLat=@Latitude AND PositionLng=@Longitude)
BEGIN 
 UPDATE  dbo.MemFromBaidu SET MemId=@MemId WHERE PositionLat=@Latitude AND PositionLng=@Longitude
END
ELSE IF EXISTS(SELECT 1 FROM  dbo.MemFromBaidu WHERE CompanyName=@Title AND  CompanyAddress=@Address)
BEGIN 
 UPDATE  dbo.MemFromBaidu SET MemId=@MemId WHERE CompanyName=@Title AND  CompanyAddress=@Address
END
ELSE
BEGIN
INSERT INTO  dbo.MemFromBaidu
        ( CompanyName ,
          CompanyAddress ,
          CompanyPhone ,
          Tags ,
          Province ,
          City ,
          PositionLat ,
          PositionLng ,
          Title ,
          CenterPosition ,
          JuLi,MemId
        )
 SELECT  case when isnull(@Title,'')='' then  CompanyName else @Title end ,case when isnull(@Address,'')='' then  Address else @Address end ,
MobilePhone,'',b.Name,c.Name,Latitude,Longitude,'','','',MemId FROM dbo.Store a WITH(NOLOCK)   INNER JOIN SysDB.Dbo.GYProvince b
 ON a.ProvinceId=b.Code INNER JOIN SysDB.Dbo.GYCity c ON a.CityId=c.Code WHERE a.MemId=@MemId
END 

 ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@Latitude", DbType.Decimal, posilat);
            db.AddInParameter(cmd, "@Longitude", DbType.Decimal, posilng);
            db.AddInParameter(cmd, "@Title", DbType.String, title);
            db.AddInParameter(cmd, "@Address", DbType.String, address);

            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<MemFromBaiduEntity> GetMemFromBaiduList(int pagesize, int pageindex, ref  int recordCount, decimal lngmax, decimal latmax, decimal lngmin, decimal latmin)
		{
            string where = " where 1=1 ";
            if(lngmax>0&& lngmin>0)
            {
                where += " and PositionLng>@PositionLngMin and PositionLng<@PositionLngMax ";
            }
            if (latmax > 0 && latmin > 0)
            {
                where += " and PositionLat>@PositionLatMin and PositionLat<@PositionLatMax ";
            }

            string sql= @"SELECT   [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi],MemId
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi],MemId from dbo.[MemFromBaidu] WITH(NOLOCK)	
						" + where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[MemFromBaidu] with (nolock) "+ where ;
            IList<MemFromBaiduEntity> entityList = new List< MemFromBaiduEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (lngmax > 0 && lngmin > 0)
            {
		    db.AddInParameter(cmd, "@PositionLngMin", DbType.Decimal, lngmin);
		    db.AddInParameter(cmd, "@PositionLngMax", DbType.Decimal, lngmax); 
            }
            if (latmax > 0 && latmin > 0)
            { 
                db.AddInParameter(cmd, "@PositionLatMin", DbType.Decimal, latmin);
                db.AddInParameter(cmd, "@PositionLatMax", DbType.Decimal, latmax);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemFromBaiduEntity entity=new MemFromBaiduEntity();
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
					entity.CenterPosition=StringUtils.GetDbString(reader["CenterPosition"]);
					entity.JuLi=StringUtils.GetDbString(reader["JuLi"]);
					entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (lngmax > 0 && lngmin > 0)
            {
                db.AddInParameter(cmd, "@PositionLngMin", DbType.Decimal, lngmin);
                db.AddInParameter(cmd, "@PositionLngMax", DbType.Decimal, lngmax);
            }
            if (latmax > 0 && latmin > 0)
            {
                db.AddInParameter(cmd, "@PositionLatMin", DbType.Decimal, latmin);
                db.AddInParameter(cmd, "@PositionLatMax", DbType.Decimal, latmax);
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
        public IList<MemFromBaiduEntity> GetMemFromBaiduList(int pagesize, int pageindex, ref int recordCount, Int64 code)
        {
            string where = " where 1=1 ";
            if (code > 0)
            {
                where += " and PrintCode=@PrintCode ";
            }
           

            string sql = @"SELECT   [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi],MemId
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi],MemId from dbo.[MemFromBaidu] WITH(NOLOCK)	
						" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[MemFromBaidu] with (nolock) " + where;
            IList<MemFromBaiduEntity> entityList = new List<MemFromBaiduEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (code > 0)
            {
            db.AddInParameter(cmd, "@PrintCode", DbType.Int64, code); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemFromBaiduEntity entity = new MemFromBaiduEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
                    entity.CompanyPhone = StringUtils.GetDbString(reader["CompanyPhone"]);
                    entity.Tags = StringUtils.GetDbString(reader["Tags"]);
                    entity.Province = StringUtils.GetDbString(reader["Province"]);
                    entity.City = StringUtils.GetDbString(reader["City"]);
                    entity.PositionLat = StringUtils.GetDbString(reader["PositionLat"]);
                    entity.PositionLng = StringUtils.GetDbString(reader["PositionLng"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.CenterPosition = StringUtils.GetDbString(reader["CenterPosition"]);
                    entity.JuLi = StringUtils.GetDbString(reader["JuLi"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (code > 0)
            {
                db.AddInParameter(cmd, "@PrintCode", DbType.Int64, code);
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
        public IList<MemFromBaiduEntity> GetMemFromBaiduAll(Int64 code)
        {

            string sql = @"SELECT    [Id],[CompanyName],[CompanyAddress],[CompanyPhone],[Tags],[Province],[City],[PositionLat],[PositionLng],[Title],[CenterPosition],[JuLi] from dbo.[MemFromBaidu] WITH(NOLOCK) where PrintCode=@PrintCode	";
		    IList<MemFromBaiduEntity> entityList = new List<MemFromBaiduEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PrintCode", DbType.Int64, code);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemFromBaiduEntity entity=new MemFromBaiduEntity();
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
					entity.CenterPosition=StringUtils.GetDbString(reader["CenterPosition"]);
					entity.JuLi=StringUtils.GetDbString(reader["JuLi"]);
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
        public int  ExistNum(MemFromBaiduEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemFromBaidu] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CompanyName=@CompanyName) and  CompanyAddress=@CompanyAddress";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CompanyName=@CompanyName and  CompanyAddress=@CompanyAddress) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName); 
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, entity.CompanyAddress);
            object identity = db.ExecuteScalar(cmd);

            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
