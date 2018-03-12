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
功能描述：CGProductsLongitude表的数据访问类。
创建时间：2016/12/28 15:59:45
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGProductDB
{
	/// <summary>
	/// CGProductsLongitudeEntity的数据访问操作
	/// </summary>
	public partial class CGProductsLongitudeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGProductsLongitudeDA Instance
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
            internal static readonly CGProductsLongitudeDA instance = new CGProductsLongitudeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGProductsLongitude，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productsLongitude">待插入的实体对象</param>
		public int AddCGProductsLongitude(CGProductsLongitudeEntity entity)
		{
		   string sql= @"insert into CGProductsLongitude( [Id],[CarTypeId1],[CarTypeId2],[CarTypeId3],[CarTypeId4],HasChecked,IsEffective,[CreateTime])VALUES
			            ( @Id,@CarTypeId1,@CarTypeId2,@CarTypeId3,@CarTypeId4,@HasChecked,@IsEffective,@CreateTime)";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@CarTypeId1",  DbType.Int32,entity.CarTypeId1);
			db.AddInParameter(cmd,"@CarTypeId2",  DbType.Int32,entity.CarTypeId2);
			db.AddInParameter(cmd,"@CarTypeId3",  DbType.Int32,entity.CarTypeId3);
			db.AddInParameter(cmd,"@CarTypeId4",  DbType.Int32,entity.CarTypeId4);
			db.AddInParameter(cmd, "@HasChecked",  DbType.Int32,entity.HasChecked);  
			db.AddInParameter(cmd, "@IsEffective",  DbType.Int32,entity.IsEffective);  

            db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			return db.ExecuteNonQuery(cmd);
 			
		}
        public int ProcAddProductsL(int memid, int cartypeid1, int cartypeid2, int cartypeid3, int cartypeid4, string cartypename1, string cartypename2, string cartypename3, string cartypename4 )
        {
            string sql = @"EXEC [Proc_AddProductL] @MemId,@CarTypeId1,@CarTypeId2,@CarTypeId3,@CarTypeId4,@CarTypeName1,@CarTypeName2,@CarTypeName3,@CarTypeName4";

            IList<BrandEntity> entityList = new List<BrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@CarTypeId1", DbType.Int32, cartypeid1);
            db.AddInParameter(cmd, "@CarTypeId2", DbType.Int32, cartypeid2);
            db.AddInParameter(cmd, "@CarTypeId3", DbType.Int32, cartypeid3);
            db.AddInParameter(cmd, "@CarTypeId4", DbType.Int32, cartypeid4);
            db.AddInParameter(cmd, "@CarTypeName1", DbType.String, cartypename1);
            db.AddInParameter(cmd, "@CarTypeName2", DbType.String, cartypename2);
            db.AddInParameter(cmd, "@CarTypeName3", DbType.String, cartypename3);
            db.AddInParameter(cmd, "@CarTypeName4", DbType.String, cartypename4);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);

        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="productsLongitude">待更新的实体对象</param>
        public   int UpdateCGProductsLongitude(CGProductsLongitudeEntity entity)
		{
			string sql= @" UPDATE dbo.[CGProductsLongitude] SET
                       [Id]=@Id,[CarTypeId1]=@CarTypeId1,[CarTypeId2]=@CarTypeId2,[CarTypeId3]=@CarTypeId3,[CarTypeId4]=@CarTypeId4,[HasChecked]=@HasChecked,IsEffective=@IsEffective,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@CarTypeId1",  DbType.Int32,entity.CarTypeId1);
			db.AddInParameter(cmd,"@CarTypeId2",  DbType.Int32,entity.CarTypeId2);
			db.AddInParameter(cmd,"@CarTypeId3",  DbType.Int32,entity.CarTypeId3);
			db.AddInParameter(cmd,"@CarTypeId4",  DbType.Int32,entity.CarTypeId4);
			db.AddInParameter(cmd, "@HasChecked",  DbType.Int32,entity.HasChecked);
			db.AddInParameter(cmd, "@IsEffective",  DbType.Int32,entity.IsEffective);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGProductsLongitudeByKey(int id)
	    {
			string sql=@"delete from CGProductsLongitude where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductsLongitudeDisabled()
        {
            string sql = @"delete from  CGProductsLongitude  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductsLongitudeByIds(string ids)
        {
            string sql = @"Delete from CGProductsLongitude  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductsLongitudeByIds(string ids)
        {
            string sql = @"Update   CGProductsLongitude set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGProductsLongitudeEntity GetCGProductsLongitude(int id)
		{
			string sql=@"SELECT  [Id],[CarTypeId1],[CarTypeId2],[CarTypeId3],[CarTypeId4],HasChecked,IsEffective,[CreateTime]
							FROM
							dbo.[CGProductsLongitude] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGProductsLongitudeEntity entity=new CGProductsLongitudeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CarTypeId1=StringUtils.GetDbInt(reader["CarTypeId1"]);
					entity.CarTypeId2=StringUtils.GetDbInt(reader["CarTypeId2"]);
					entity.CarTypeId3=StringUtils.GetDbInt(reader["CarTypeId3"]);
					entity.CarTypeId4=StringUtils.GetDbInt(reader["CarTypeId4"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]);
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
		public   IList<CGProductsLongitudeEntity> GetCGProductsLongitudeList(int pageindex, int pagesize,  ref  int recordCount,int memid,int haschecked,int iseffect )
        {
            string where = " where 1=1 ";
            if (memid != -1)
            {
                where += " and CGMemId=@CGMemId ";
            }
            if (haschecked != -1)
            {
                where += " and HasChecked=@HasChecked ";
            }
            if (iseffect != -1)
            {
                where += " and IsEffective=@IsEffective ";
            }
            string sql= @"SELECT    [Id]
      ,[CarTypeId1]
      ,[CarTypeId2]
      ,[CarTypeId3]
      ,[CarTypeId4]
      ,[CarTypeName1]
      ,[CarTypeName2]
      ,[CarTypeName3]
      ,[CarTypeName4]
      ,HasChecked,IsEffective
      ,[CreateTime]
      ,[CGMemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
					  [Id]
      ,[CarTypeId1]
      ,[CarTypeId2]
      ,[CarTypeId3]
      ,[CarTypeId4]
      ,[CarTypeName1]
      ,[CarTypeName2]
      ,[CarTypeName3]
      ,[CarTypeName4]
      ,HasChecked,IsEffective
      ,[CreateTime]
      ,[CGMemId]
  FROM [JcCGProductDB].[dbo].[CGProductsLongitude] WITH(NOLOCK)	" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2= @"Select count(1) from dbo.[CGProductsLongitude] with (nolock) "  + where ;
            IList<CGProductsLongitudeEntity> entityList = new List< CGProductsLongitudeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (haschecked != -1)
            { 
                db.AddInParameter(cmd, "@HasChecked", DbType.Int32, haschecked);
            }
            if (iseffect != -1)
            {
                db.AddInParameter(cmd, "@IsEffective", DbType.Int32, iseffect); 
            } 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					CGProductsLongitudeEntity entity=new CGProductsLongitudeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CarTypeId1=StringUtils.GetDbInt(reader["CarTypeId1"]);
					entity.CarTypeId2=StringUtils.GetDbInt(reader["CarTypeId2"]);
					entity.CarTypeId3=StringUtils.GetDbInt(reader["CarTypeId3"]);
					entity.CarTypeId4=StringUtils.GetDbInt(reader["CarTypeId4"]);
					entity.CarTypeName1 = StringUtils.GetDbString(reader["CarTypeName1"]);
					entity.CarTypeName2 = StringUtils.GetDbString(reader["CarTypeName2"]);
					entity.CarTypeName3 = StringUtils.GetDbString(reader["CarTypeName3"]);
					entity.CarTypeName4 = StringUtils.GetDbString(reader["CarTypeName4"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]);
                    entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2); if (memid != -1)
            {
                db.AddInParameter(cmd, "@CGMemId", DbType.Int32, memid);
            }
            if (haschecked != -1)
            {
                db.AddInParameter(cmd, "@HasChecked", DbType.Int32, haschecked);
            }
            if (iseffect != -1)
            {
                db.AddInParameter(cmd, "@IsEffective", DbType.Int32, iseffect);
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
        public IList<CGProductsLongitudeEntity> GetCGProductsLongitudeAll()
        {

            string sql = @"SELECT    [Id],[CarTypeId1],[CarTypeId2],[CarTypeId3],[CarTypeId4],HasChecked,IsEffective,[CreateTime] from dbo.[CGProductsLongitude] WITH(NOLOCK)	";
		    IList<CGProductsLongitudeEntity> entityList = new List<CGProductsLongitudeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGProductsLongitudeEntity entity=new CGProductsLongitudeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.CarTypeId1=StringUtils.GetDbInt(reader["CarTypeId1"]);
					entity.CarTypeId2=StringUtils.GetDbInt(reader["CarTypeId2"]);
					entity.CarTypeId3=StringUtils.GetDbInt(reader["CarTypeId3"]);
					entity.CarTypeId4=StringUtils.GetDbInt(reader["CarTypeId4"]);
					entity.HasChecked=StringUtils.GetDbInt(reader["HasChecked"]);
					entity.IsEffective=StringUtils.GetDbInt(reader["IsEffective"]);
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
        public int  ExistNum(CGProductsLongitudeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGProductsLongitude] WITH(NOLOCK) ";
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
