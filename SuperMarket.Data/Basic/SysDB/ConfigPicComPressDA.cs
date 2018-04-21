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
功能描述：ConfigPicComPress表的数据访问类。
创建时间：2018/4/17 23:52:24
创 建 人：lgzh
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB
{
	/// <summary>
	/// ConfigPicComPressEntity的数据访问操作
	/// </summary>
	public partial class ConfigPicComPressDA: BaseSuperMarketDB
    {
        #region 实例化
        public static ConfigPicComPressDA Instance
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
            internal static readonly ConfigPicComPressDA instance = new ConfigPicComPressDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ConfigPicComPress，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="configPicComPress">待插入的实体对象</param>
		public int AddConfigPicComPress(ConfigPicComPressEntity entity)
		{
		   string sql=@"insert into ConfigPicComPress( [Title],[Width],[Height],[PicName],[PicNameType],[SourcePicType],[CreateTime])VALUES
			            ( @title,@width,@height,@picName,@picNameType,@sourcePicType,@createTime);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@Width",  DbType.Int32,entity.Width);
			db.AddInParameter(cmd,"@Height",  DbType.Int32,entity.Height);
			db.AddInParameter(cmd,"@PicName",  DbType.String,entity.PicName);
			db.AddInParameter(cmd,"@PicNameType",  DbType.Int32,entity.PicNameType);
			db.AddInParameter(cmd,"@SourcePicType",  DbType.Int32,entity.SourcePicType);
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
		/// <param name="configPicComPress">待更新的实体对象</param>
		public   int UpdateConfigPicComPress(ConfigPicComPressEntity entity)
		{
			string sql=@" UPDATE dbo.[ConfigPicComPress] SET
                       [Title]=@title,[Width]=@width,[Height]=@height,[PicName]=@picName,[PicNameType]=@picNameType,[SourcePicType]=@sourcePicType,[CreateTime]=@createTime
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@Title",  DbType.String,entity.Title);
			db.AddInParameter(cmd,"@Width",  DbType.Int32,entity.Width);
			db.AddInParameter(cmd,"@Height",  DbType.Int32,entity.Height);
			db.AddInParameter(cmd,"@PicName",  DbType.String,entity.PicName);
			db.AddInParameter(cmd,"@PicNameType",  DbType.Int32,entity.PicNameType);
			db.AddInParameter(cmd,"@SourcePicType",  DbType.Int32,entity.SourcePicType);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,entity.CreateTime);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteConfigPicComPressByKey(int id)
	    {
			string sql=@"delete from ConfigPicComPress where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   ConfigPicComPressEntity GetConfigPicComPress(int id)
		{
			string sql=@"SELECT  [Id],[Title],[Width],[Height],[PicName],[PicNameType],[SourcePicType],[CreateTime]
							FROM
							dbo.[ConfigPicComPress] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		ConfigPicComPressEntity entity=new ConfigPicComPressEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Title=StringUtils.GetDbString(reader["Title"]);;
					entity.Width=StringUtils.GetDbInt(reader["Width"]);;
					entity.Height=StringUtils.GetDbInt(reader["Height"]);;
					entity.PicName=StringUtils.GetDbString(reader["PicName"]);;
					entity.PicNameType=StringUtils.GetDbInt(reader["PicNameType"]);;
					entity.SourcePicType=StringUtils.GetDbInt(reader["SourcePicType"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<ConfigPicComPressEntity> GetConfigPicComPressList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[Title],[Width],[Height],[PicName],[PicNameType],[SourcePicType],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Title],[Width],[Height],[PicName],[PicNameType],[SourcePicType],[CreateTime] from dbo.[ConfigPicComPress] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[ConfigPicComPress] with (nolock) ";
            IList<ConfigPicComPressEntity> entityList = new List< ConfigPicComPressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					ConfigPicComPressEntity entity=new ConfigPicComPressEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);;
					entity.Title=StringUtils.GetDbString(reader["Title"]);;
					entity.Width=StringUtils.GetDbInt(reader["Width"]);;
					entity.Height=StringUtils.GetDbInt(reader["Height"]);;
					entity.PicName=StringUtils.GetDbString(reader["PicName"]);;
					entity.PicNameType=StringUtils.GetDbInt(reader["PicNameType"]);;
					entity.SourcePicType=StringUtils.GetDbInt(reader["SourcePicType"]);;
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);;
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
        public IList<ConfigPicComPressEntity> GetConfigListByPicSource(int source)
        {
            string sql = @" SELECT  
						 [Id],[Title],[Width],[Height],[PicName],[PicNameType],[SourcePicType],[CreateTime] from dbo.[ConfigPicComPress] WITH(NOLOCK)	
						WHERE  SourcePicType=@SourcePicType";
             
            IList<ConfigPicComPressEntity> entityList = new List<ConfigPicComPressEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@SourcePicType", DbType.Int32, source); 

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ConfigPicComPressEntity entity = new ConfigPicComPressEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]); ;
                    entity.Title = StringUtils.GetDbString(reader["Title"]); ;
                    entity.Width = StringUtils.GetDbInt(reader["Width"]); ;
                    entity.Height = StringUtils.GetDbInt(reader["Height"]); ;
                    entity.PicName = StringUtils.GetDbString(reader["PicName"]); ;
                    entity.PicNameType = StringUtils.GetDbInt(reader["PicNameType"]); ;
                    entity.SourcePicType = StringUtils.GetDbInt(reader["SourcePicType"]); ;
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]); ;
                }
            } 
            return entityList;
        }
        #endregion
        #endregion
    }
}
