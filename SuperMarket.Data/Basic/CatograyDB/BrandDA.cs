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
功能描述：Brand表的数据访问类。
创建时间：2016/10/31 13:04:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.CatograyDB
{
	/// <summary>
	/// BrandEntity的数据访问操作
	/// </summary>
	public partial class BrandDA: BaseSuperMarketDB
    {
        #region 实例化
        public static BrandDA Instance
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
            internal static readonly BrandDA instance = new BrandDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表Brand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="brand">待插入的实体对象</param>
		public int AddBrand(BrandEntity entity)
		{
		   string sql= @"insert into Brand( [Code],[Name],[Title],[PYShort],[PYFull],[Sort],[IsActive],[IsHot],[ParentId],[Manufacturer])VALUES
			            ( @Code,@Name,@Title,dbo.fun_getPY(@Name),dbo.f_GetPinyin(@Name),@Sort,@IsActive,@IsHot,@ParentId,@Manufacturer);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			//db.AddInParameter(cmd,"@PYShort",  DbType.String,entity.PYShort);
			//db.AddInParameter(cmd,"@PYFull",  DbType.String,entity.PYFull);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@IsHot",  DbType.Int32,entity.IsHot);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
            db.AddInParameter(cmd, "@Manufacturer", DbType.String, entity.Manufacturer);

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="brand">待更新的实体对象</param>
		public   int UpdateBrand(BrandEntity entity)
		{
			string sql= @" UPDATE dbo.[Brand] SET
                       [Code]=@Code,[Name]=@Name,[Title]=@Title,[PYShort]=dbo.fun_getPY(@Name),[PYFull]=dbo.f_GetPinyin(@Name),[Sort]=@Sort,[IsActive]=@IsActive,[IsHot]=@IsHot,[ParentId]=@ParentId,[Manufacturer]=@Manufacturer,[PicUrl]=@PicUrl
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Code",  DbType.String,entity.Code);
			db.AddInParameter(cmd,"@Name",  DbType.String,entity.Name);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			//db.AddInParameter(cmd,"@PYShort",  DbType.String,entity.PYShort);
			//db.AddInParameter(cmd,"@PYFull",  DbType.String,entity.PYFull);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive);
			db.AddInParameter(cmd,"@IsHot",  DbType.Int32,entity.IsHot);
			db.AddInParameter(cmd,"@ParentId",  DbType.Int32,entity.ParentId);
            db.AddInParameter(cmd, "@Manufacturer", DbType.String, entity.Manufacturer);
            db.AddInParameter(cmd, "@PicUrl", DbType.String, entity.PicUrl);
            return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteBrandByKey(int id)
	    {
			string sql=@"delete from Brand where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBrandDisabled()
        {
            string sql = @"delete from  Brand  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBrandByIds(string ids)
        {
            string sql = @"Delete from Brand  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBrandByIds(string ids)
        {
            string sql = @"Update   Brand set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   BrandEntity GetBrand(int id)
		{
			string sql= @"SELECT  [Id],[Code],[Name],[Title],[PYShort],[PYFull],[Sort],[IsActive],[IsHot],[ParentId],[Manufacturer],PicUrl
							FROM
							dbo.[Brand] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		BrandEntity entity=new BrandEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.PYShort=StringUtils.GetDbString(reader["PYShort"]);
					entity.PYFull=StringUtils.GetDbString(reader["PYFull"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.IsHot=StringUtils.GetDbInt(reader["IsHot"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
                    entity.Manufacturer = StringUtils.GetDbString(reader["Manufacturer"]);
                    entity.PicUrl = StringUtils.GetDbString(reader["PicUrl"]);
                    
                }
            }
            return entity;
		}
        public BrandEntity GetBrandByName(string name)
        {
            string sql = @"SELECT  [Id],[Code],[Name],[Title],[PYShort],[PYFull],[Sort],[IsActive],[IsHot],[ParentId],[Manufacturer]
							FROM
							dbo.[Brand] WITH(NOLOCK)	
							WHERE [Name]=@Name";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@Name", DbType.String, name);
            BrandEntity entity = new BrandEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.Manufacturer = StringUtils.GetDbString(reader["Manufacturer"]); 
                }
            }
            return entity;
        }

        public IList<BrandEntity> GetBrandByKey(string classid, int pid, string key)
        {
            string sql = @"EXEC [Proc_GetBrandByKey] @ClassIdStr,@ParentId,@Key";

            IList<BrandEntity> entityList = new List<BrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@ClassIdStr", DbType.String, classid);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
            db.AddInParameter(cmd, "@Key", DbType.String, key);
        
            DataSet ds = db.ExecuteDataSet(cmd);
            DataTable _dt = new DataTable();
            if (ds.Tables.Count >0)
            {
                _dt = ds.Tables[0];
                foreach (DataRow dr in _dt.Rows)
                {
                    BrandEntity entity = new BrandEntity(); 
                    entity.Id = StringUtils.GetDbInt(dr["Id"]);
                    entity.Code = StringUtils.GetDbString(dr["Code"]);
                    entity.Name = StringUtils.GetDbString(dr["Name"]);
                    entity.Title = StringUtils.GetDbString(dr["Title"]);
                    entity.PYShort = StringUtils.GetDbString(dr["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(dr["PYFull"]);
                    entity.Sort = StringUtils.GetDbInt(dr["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(dr["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(dr["IsHot"]);
                    entity.ParentId = StringUtils.GetDbInt(dr["ParentId"]);
                    entity.Manufacturer = StringUtils.GetDbString(dr["Manufacturer"]);
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
        public IList<BrandEntity> GetBrandList(int pagesize, int pageindex, ref  int recordCount ,int ishot,string name)
		{
            string where = " WHERE  1=1 ";

            if (ishot > -1)
            {
                where += " And IsHot=@IsHot ";
            }

            if (!string.IsNullOrEmpty(name))
            {
                where += " And Name like @Name ";
            }

            string sql= @"SELECT   [Id],[Code],[Name],[Title],[PYShort],[PYFull],[Sort],[IsActive],[IsHot],[ParentId],[Manufacturer]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY a.Id desc) AS ROWNUMBER,
						 a.[Id],a.[Code],a.[Name],a.[Title],a.[PYShort],a.[PYFull],a.[Sort],a.[IsActive],a.[IsHot],a.[ParentId],a.[Manufacturer] from dbo.[Brand] a WITH(NOLOCK)	
						"+where+ @") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[Brand] a with (nolock) "+where;
            IList<BrandEntity> entityList = new List< BrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            if (ishot > -1)
            {
                db.AddInParameter(cmd, "@IsHot", DbType.Int32, ishot);
            }

            if (!string.IsNullOrEmpty(name))
            {
                db.AddInParameter(cmd, "@Name", DbType.String, "%"+name+"%");
            }


            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					BrandEntity entity=new BrandEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Code=StringUtils.GetDbString(reader["Code"]);
					entity.Name=StringUtils.GetDbString(reader["Name"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.PYShort=StringUtils.GetDbString(reader["PYShort"]);
					entity.PYFull=StringUtils.GetDbString(reader["PYFull"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.IsHot=StringUtils.GetDbInt(reader["IsHot"]);
					entity.ParentId=StringUtils.GetDbInt(reader["ParentId"]);
					entity.Manufacturer=StringUtils.GetDbString(reader["Manufacturer"]);
                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);

            if (ishot > -1)
            {
                db.AddInParameter(cmd, "@IsHot", DbType.Int32, ishot);
            }

            if (!string.IsNullOrEmpty(name))
            {
                db.AddInParameter(cmd, "@Name", DbType.String, "%" + name + "%");
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
        public IList<VWTreeBrandEntity> GetTreeBrandAll(int classid)
        {
            string where = " where a.IsActive=1 ";
            if (classid > 0)
            {
                where += " and b.ClassId=@ClassId";
            }

            string sql = "";
            if (classid > 0)
            {
                sql = @"SELECT a.[Id], a.[Name], a.[PYFirst] ,a.[Sort],a.[IsActive]  from dbo.[Brand] a WITH(NOLOCK) inner join ClassBrand b WITH(NOLOCK) on a.id=b.BrandId	" + where;
            }
            else
            {
                sql = @"SELECT a.[Id], a.[Name], a.[PYFirst] ,a.[Sort],a.[IsActive]  from dbo.[Brand] a WITH(NOLOCK) 	" + where;
            }
            IList<VWTreeBrandEntity> entityList = new List<VWTreeBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (classid > 0)
            {
                db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWTreeBrandEntity entity = new VWTreeBrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]); 
                    entity.Name = StringUtils.GetDbString(reader["Name"]); 
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]); 
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]); 
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
        public IList<VWTreeBrandEntity> GetBrandAllByClassStr(string classidstr)
        {
            string where = " where a.IsActive=1 ";
      
            string sql = "";
            if (!string.IsNullOrEmpty(classidstr)|| classidstr=="0")
            {
                sql = @"SELECT distinct a.[Id], a.[Name], a.[PYFirst] ,a.[Sort],a.[IsActive]  from dbo.[Brand] a WITH(NOLOCK) inner join ClassBrand b WITH(NOLOCK) on a.id=b.BrandId	inner join dbo.fun_splitstr('"+ classidstr + "','_') c on b.ClassId=c.Id" +where;
            }
            else
            {
                sql = @"SELECT distinct a.[Id], a.[Name], a.[PYFirst] ,a.[Sort],a.[IsActive]  from dbo.[Brand] a WITH(NOLOCK) 	" + where;
            }
            IList<VWTreeBrandEntity> entityList = new List<VWTreeBrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            //if (classid > 0)
            //{
            //    db.AddInParameter(cmd, "@ClassId", DbType.Int32, classid);
            //}
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    VWTreeBrandEntity entity = new VWTreeBrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.PYFirst = StringUtils.GetDbString(reader["PYFirst"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
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
        public IList<BrandEntity> GetBrandListByBrand(string brand)
        {
            string where = " where 1=1";

            if (!string.IsNullOrEmpty(brand))
            {
                where += " and Name like @Name";
            }
            string sql = @"SELECT  top 100  [Id],[Code],[Name],[Title],[PYShort],[PYFull],[Sort],[IsActive],[IsHot],[ParentId] from dbo.[Brand] WITH(NOLOCK)	"+where;
            IList<BrandEntity> entityList = new List<BrandEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(brand))
            {
                db.AddInParameter(cmd, "@Name",DbType.String,"%"+brand+"%");
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    BrandEntity entity = new BrandEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.PYShort = StringUtils.GetDbString(reader["PYShort"]);
                    entity.PYFull = StringUtils.GetDbString(reader["PYFull"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
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
        public int  ExistNum(BrandEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[Brand] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  Name=@Name ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  Name=@Name ";
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
