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
功能描述：B2BNotice表的数据访问类。
创建时间：2016/12/19 18:15:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// B2BNoticeEntity的数据访问操作
	/// </summary>
	public partial class B2BNoticeDA: BaseSuperMarketDB
    {
        #region 实例化
        public static B2BNoticeDA Instance
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
            internal static readonly B2BNoticeDA instance = new B2BNoticeDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表B2BNotice，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="b2BNotice">待插入的实体对象</param>
		public int AddB2BNotice(B2BNoticeEntity entity)
		{
		   string sql= @"insert into B2BNotice([Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],SystemType,NoticeType)VALUES
			            (@Title,@NoticeContent,@CreateTime,@Sort,@ShowTime,@EndTime,@IsActive,@SystemType,@NoticeType);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			//db.AddInParameter(cmd, "@Id",  DbType.String,entity.Id);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
            db.AddInParameter(cmd,"@NoticeContent",  DbType.String,entity.NoticeContent);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ShowTime",  DbType.DateTime,entity.ShowTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd, "@IsActive",  DbType.Int16,entity.IsActive);
			db.AddInParameter(cmd, "@SystemType",  DbType.Int16,entity.SystemType);
			db.AddInParameter(cmd, "@NoticeType",  DbType.Int16,entity.NoticeType);
            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="b2BNotice">待更新的实体对象</param>
		public   int UpdateB2BNotice(B2BNoticeEntity entity)
		{
			string sql= @" UPDATE dbo.[B2BNotice] SET
                       [Title]=@Title,[NoticeContent]=@NoticeContent,[CreateTime]=@CreateTime,[Sort]=@Sort,[ShowTime]=@ShowTime,[EndTime]=@EndTime,SystemType=@SystemType,NoticeType=@NoticeType
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@NoticeContent",  DbType.String,entity.NoticeContent);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
			db.AddInParameter(cmd,"@Sort",  DbType.Int32,entity.Sort);
			db.AddInParameter(cmd,"@ShowTime",  DbType.DateTime,entity.ShowTime);
			db.AddInParameter(cmd,"@EndTime",  DbType.DateTime,entity.EndTime);
			db.AddInParameter(cmd,"@IsActive",  DbType.Int32,entity.IsActive); 
			db.AddInParameter(cmd, "@SystemType",  DbType.Int32,entity.SystemType);  
			db.AddInParameter(cmd, "@NoticeType",  DbType.Int32,entity.NoticeType);  

             return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteB2BNoticeByKey(int id)
	    {
			string sql=@"delete from B2BNotice where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteB2BNoticeDisabled()
        {
            string sql = @"delete from  B2BNotice  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteB2BNoticeByIds(string ids)
        {
            string sql = @"Delete from B2BNotice  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableB2BNoticeByIds(string ids)
        {
            string sql = @"Update   B2BNotice set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   B2BNoticeEntity GetB2BNotice(int id)
		{
			string sql= @"SELECT  [Id],[Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],IsHot
							FROM
							dbo.[B2BNotice] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		B2BNoticeEntity entity=new B2BNoticeEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.NoticeContent=StringUtils.GetDbString(reader["NoticeContent"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowTime=StringUtils.GetDbDateTime(reader["ShowTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
					entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                }
   		    }
            return entity;
		}
        public B2BNoticeEntity GetB2BNoticeByName(int systemtype, string name)
        {
            string sql = @"SELECT  [Id],[Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],IsHot
							FROM dbo.[B2BNotice] WITH(NOLOCK)	
							WHERE SysTemType=@SysTemType  and Title=@Title";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@SysTemType", DbType.Int32, systemtype);
            db.AddInParameter(cmd, "@Title", DbType.String, name);
            B2BNoticeEntity entity = new B2BNoticeEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.NoticeContent = StringUtils.GetDbString(reader["NoticeContent"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.ShowTime = StringUtils.GetDbDateTime(reader["ShowTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                }
            }
            return entity;
        }
        public IList<B2BNoticeEntity> GetB2BNoticeListTop4(int systemtype)
        {
            string sql = @"SELECT Top(5) [Id],[Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],IsHot
							FROM
							dbo.[B2BNotice] WITH(NOLOCK) where SysTemType=@SysTemType And IsActive=1 and NoticeType=1 Order By Sort desc";
 
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SysTemType", DbType.Int32, systemtype);

            IList<B2BNoticeEntity> entityList = new List<B2BNoticeEntity>();
                
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    B2BNoticeEntity entity = new B2BNoticeEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Title = StringUtils.GetDbString(reader["Title"]);
                    entity.NoticeContent = StringUtils.GetDbString(reader["NoticeContent"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.Sort = StringUtils.GetDbInt(reader["Sort"]);
                    entity.ShowTime = StringUtils.GetDbDateTime(reader["ShowTime"]);
                    entity.EndTime = StringUtils.GetDbDateTime(reader["EndTime"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]); 
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]); 
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
        public IList<B2BNoticeEntity> GetB2BNoticeList(int pagesize, int pageindex, ref int recordCount,string key,int _status,int _noticetype, int systype)
        {
            string where = " where 1=1 ";
            if (systype != -1)
            {
                where += " and SystemType=@SystemType";
            }
            if (!string.IsNullOrEmpty(key))
            {
                where += " and Title like @Key";
            }
            if (_status != -1)
            {
                where += " and IsActive=@IsActive";
            }
            if (_noticetype != -1)
            {
                where += " and NoticeType=@NoticeType";
            }
            string sql= @"SELECT   [Id],[Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],SystemType,IsHot
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],SystemType,IsHot from dbo.[B2BNotice] WITH(NOLOCK)	
						" + where+@" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[B2BNotice] with (nolock) "+ where;
            IList<B2BNoticeEntity> entityList = new List< B2BNoticeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (systype != -1)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, systype);
            }
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@Key", DbType.String, "%"+ key+"%");
            }
            if (_status != -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, _status); 
            }
            if (_noticetype != -1)
            {
                db.AddInParameter(cmd, "@NoticeType", DbType.Int32, _noticetype); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					B2BNoticeEntity entity=new B2BNoticeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.NoticeContent=StringUtils.GetDbString(reader["NoticeContent"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowTime=StringUtils.GetDbDateTime(reader["ShowTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]); 
					entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
                    entity.SystemType = StringUtils.GetDbInt(reader["SystemType"]); 

                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (systype != -1)
            {
                db.AddInParameter(cmd, "@SystemType", DbType.Int32, systype);
            }
            if (!string.IsNullOrEmpty(key))
            {
                db.AddInParameter(cmd, "@Key", DbType.String, "%" + key + "%");
            }
            if (_status != -1)
            {
                db.AddInParameter(cmd, "@IsActive", DbType.Int32, _status);
            }
            if (_noticetype != -1)
            {
                db.AddInParameter(cmd, "@NoticeType", DbType.Int32, _noticetype);
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
        public IList<B2BNoticeEntity> GetB2BNoticeAll()
        {

            string sql = @"SELECT    [Id],[Title],[NoticeContent],[CreateTime],[Sort],[ShowTime],[EndTime],[IsActive],IsHot from dbo.[B2BNotice] WITH(NOLOCK)	";
		    IList<B2BNoticeEntity> entityList = new List<B2BNoticeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   B2BNoticeEntity entity=new B2BNoticeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.Title=StringUtils.GetDbString(reader["Title"]);
					entity.NoticeContent=StringUtils.GetDbString(reader["NoticeContent"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.Sort=StringUtils.GetDbInt(reader["Sort"]);
					entity.ShowTime=StringUtils.GetDbDateTime(reader["ShowTime"]);
					entity.EndTime=StringUtils.GetDbDateTime(reader["EndTime"]);
					entity.IsActive=StringUtils.GetDbInt(reader["IsActive"]);
                    entity.IsHot = StringUtils.GetDbInt(reader["IsHot"]);
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
        public int  ExistNum(B2BNoticeEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[B2BNotice] WITH(NOLOCK) ";
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
