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
功能描述：CGProductsTransverse表的数据访问类。
创建时间：2016/12/28 15:59:45
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CGProductDB
{
	/// <summary>
	/// CGProductsTransverseEntity的数据访问操作
	/// </summary>
	public partial class CGProductsTransverseDA: BaseSuperMarketDB
    {
        #region 实例化
        public static CGProductsTransverseDA Instance
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
            internal static readonly CGProductsTransverseDA instance = new CGProductsTransverseDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表CGProductsTransverse，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="productsTransverse">待插入的实体对象</param>
		public int AddCGProductsTransverse(CGProductsTransverseEntity entity)
		{
		   string sql= @"insert into CGProductsTransverse( [ClassId1],[ClassId2],[ClassId3],[ClassId4],[BrandId],HasChecked,IsEffective,[CreateTime])VALUES
			            ( @ClassId1,@ClassId2,@ClassId3,@ClassId4,@BrandId,@HasChecked,@IsEffective,@CreateTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@ClassId1",  DbType.Int32,entity.ClassId1);
			db.AddInParameter(cmd,"@ClassId2",  DbType.Int32,entity.ClassId2);
			db.AddInParameter(cmd,"@ClassId3",  DbType.Int32,entity.ClassId3);
			db.AddInParameter(cmd,"@ClassId4",  DbType.Int32,entity.ClassId4);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@HasChecked",  DbType.Int32,entity.HasChecked);
			db.AddInParameter(cmd,"@IsEffective",  DbType.Int32,entity.IsEffective); 
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
		/// <param name="productsTransverse">待更新的实体对象</param>
		public   int UpdateCGProductsTransverse(CGProductsTransverseEntity entity)
		{
			string sql= @" UPDATE dbo.[CGProductsTransverse] SET
                       [ClassId1]=@ClassId1,[ClassId2]=@ClassId2,[ClassId3]=@ClassId3,[ClassId4]=@ClassId4,[BrandId]=@BrandId,[HasChecked]=@HasChecked,[IsEffective]=@IsEffective,[CreateTime]=@CreateTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@ClassId1",  DbType.Int32,entity.ClassId1);
			db.AddInParameter(cmd,"@ClassId2",  DbType.Int32,entity.ClassId2);
			db.AddInParameter(cmd,"@ClassId3",  DbType.Int32,entity.ClassId3);
			db.AddInParameter(cmd,"@ClassId4",  DbType.Int32,entity.ClassId4);
			db.AddInParameter(cmd,"@BrandId",  DbType.Int32,entity.BrandId);
			db.AddInParameter(cmd,"@HasChecked",  DbType.Int32,entity.HasChecked);
			db.AddInParameter(cmd,"@IsEffective",  DbType.Int32,entity.IsEffective); 
            db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteCGProductsTransverseByKey(int id)
	    {
			string sql=@"delete from CGProductsTransverse where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductsTransverseDisabled()
        {
            string sql = @"delete from  CGProductsTransverse  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductsTransverseByIds(string ids)
        {
            string sql = @"Delete from CGProductsTransverse  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductsTransverseByIds(string ids)
        {
            string sql = @"Update   CGProductsTransverse set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   CGProductsTransverseEntity GetCGProductsTransverse(int id)
		{
			string sql=@"SELECT  [Id],[ClassId1],[ClassId2],[ClassId3],[ClassId4],[BrandId],HasChecked,IsEffective,[CreateTime]
							FROM
							dbo.[CGProductsTransverse] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		CGProductsTransverseEntity entity=new CGProductsTransverseEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId1=StringUtils.GetDbInt(reader["ClassId1"]);
					entity.ClassId2=StringUtils.GetDbInt(reader["ClassId2"]);
					entity.ClassId3=StringUtils.GetDbInt(reader["ClassId3"]);
					entity.ClassId4=StringUtils.GetDbInt(reader["ClassId4"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]); 
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]);
                    entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
				}
   		    }
            return entity;
		}
        public int ProcAddProductsT(int memid, int classid1, int classid2, int classid3, int classid4, string classname1, string classname2, string classname3, string classname4,int brandid,string brandname)
        {
            string sql = @"EXEC [Proc_AddProductT] @MemId,@ClassId1,@ClassId2,@ClassId3,@ClassId4,@ClassName1,@ClassName2,@ClassName3,@ClassName4,@BrandId,@BrandName";

            IList<BrandEntity> entityList = new List<BrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@ClassId1", DbType.Int32, classid1);
            db.AddInParameter(cmd, "@ClassId2", DbType.Int32, classid2);
            db.AddInParameter(cmd, "@ClassId3", DbType.Int32, classid3);
            db.AddInParameter(cmd, "@ClassId4", DbType.Int32, classid4);
            db.AddInParameter(cmd, "@BrandId", DbType.Int32, brandid);
            db.AddInParameter(cmd, "@BrandName", DbType.String, brandname);
            db.AddInParameter(cmd, "@ClassName1", DbType.String, classname1);
            db.AddInParameter(cmd, "@ClassName2", DbType.String, classname2);
            db.AddInParameter(cmd, "@ClassName3", DbType.String, classname3);
            db.AddInParameter(cmd, "@ClassName4", DbType.String, classname4);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<CGProductsTransverseEntity> GetCGProductsTransverseList( int pageindex, int pagesize, ref  int recordCount,int memid,int haschecked,int iseffect)
		{
            string where = " where 1=1 ";
            if(memid!=-1)
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

            string sql = @"SELECT   [Id]
      ,[ClassId1]
      ,[ClassId2]
      ,[ClassId3]
      ,[ClassId4]
      ,[BrandId]
      ,[ClassName1]
      ,[ClassName2]
      ,[ClassName3]
      ,[ClassName4]
      ,[BrandName]
      ,HasChecked,IsEffective
      ,[CreateTime]
      ,[CGMemId]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
				  [Id]
      ,[ClassId1]
      ,[ClassId2]
      ,[ClassId3]
      ,[ClassId4]
      ,[BrandId]
      ,[ClassName1]
      ,[ClassName2]
      ,[ClassName3]
      ,[ClassName4]
      ,[BrandName]
      ,HasChecked,IsEffective
      ,[CreateTime]
      ,[CGMemId]
  FROM  [dbo].[CGProductsTransverse] WITH(NOLOCK)	" + where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[CGProductsTransverse] with (nolock) "+ where;
            IList<CGProductsTransverseEntity> entityList = new List< CGProductsTransverseEntity>();
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
					CGProductsTransverseEntity entity=new CGProductsTransverseEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId1=StringUtils.GetDbInt(reader["ClassId1"]);
					entity.ClassId2=StringUtils.GetDbInt(reader["ClassId2"]);
					entity.ClassId3=StringUtils.GetDbInt(reader["ClassId3"]);
					entity.ClassId4=StringUtils.GetDbInt(reader["ClassId4"]);
					entity.ClassName1 = StringUtils.GetDbString(reader["ClassName1"]);
					entity.ClassName2 = StringUtils.GetDbString(reader["ClassName2"]);
					entity.ClassName3 = StringUtils.GetDbString(reader["ClassName3"]);
					entity.ClassName4 = StringUtils.GetDbString(reader["ClassName4"]);
					entity.BrandName = StringUtils.GetDbString(reader["BrandName"]);
                    entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
                    entity.CGMemId = StringUtils.GetDbInt(reader["CGMemId"]);
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
        public IList<CGProductsTransverseEntity> GetCGProductsTransverseAll()
        {

            string sql = @"SELECT    [Id],[ClassId1],[ClassId2],[ClassId3],[ClassId4],[BrandId],HasChecked,IsEffective,[CreateTime] from dbo.[CGProductsTransverse] WITH(NOLOCK)	";
		    IList<CGProductsTransverseEntity> entityList = new List<CGProductsTransverseEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   CGProductsTransverseEntity entity=new CGProductsTransverseEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.ClassId1=StringUtils.GetDbInt(reader["ClassId1"]);
					entity.ClassId2=StringUtils.GetDbInt(reader["ClassId2"]);
					entity.ClassId3=StringUtils.GetDbInt(reader["ClassId3"]);
					entity.ClassId4=StringUtils.GetDbInt(reader["ClassId4"]);
					entity.BrandId=StringUtils.GetDbInt(reader["BrandId"]);
                    entity.HasChecked = StringUtils.GetDbInt(reader["HasChecked"]);
                    entity.IsEffective = StringUtils.GetDbInt(reader["IsEffective"]);
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
        public int  ExistNum(CGProductsTransverseEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[CGProductsTransverse] WITH(NOLOCK) ";
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
