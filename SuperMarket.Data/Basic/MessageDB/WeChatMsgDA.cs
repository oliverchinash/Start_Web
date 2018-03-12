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
功能描述：WeChatMsg表的数据访问类。
创建时间：2017/8/26 16:39:06
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MessageDB
{
	/// <summary>
	/// WeChatMsgEntity的数据访问操作
	/// </summary>
	public partial class WeChatMsgDA: BaseSuperMarketDB
    {
        #region 实例化
        public static WeChatMsgDA Instance
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
            internal static readonly WeChatMsgDA instance = new WeChatMsgDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表WeChatMsg，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weChatMsg">待插入的实体对象</param>
		public int AddWeChatMsg(WeChatMsgEntity entity)
		{
		   string sql=@"insert into WeChatMsg( [WeChatOpenId],[TemplateIid],[WeChatUrl],[ParamStr],[Result],[Remark])VALUES
			            ( @WeChatOpenId,@TemplateIid,@WeChatUrl,@ParamStr,@Result,@Remark);
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@WeChatOpenId",  DbType.String,entity.WeChatOpenId);
			db.AddInParameter(cmd,"@TemplateIid",  DbType.String,entity.TemplateIid);
			db.AddInParameter(cmd,"@WeChatUrl",  DbType.String,entity.WeChatUrl);
			db.AddInParameter(cmd,"@ParamStr",  DbType.String,entity.ParamStr);
			db.AddInParameter(cmd,"@Result",  DbType.String,entity.Result);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
			object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="weChatMsg">待更新的实体对象</param>
		public   int UpdateWeChatMsg(WeChatMsgEntity entity)
		{
			string sql=@" UPDATE dbo.[WeChatMsg] SET
                       [WeChatOpenId]=@WeChatOpenId,[TemplateIid]=@TemplateIid,[WeChatUrl]=@WeChatUrl,[ParamStr]=@ParamStr,[Result]=@Result,[Remark]=@Remark
                       WHERE [Id]=@id";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd,"@Id",  DbType.Int32,entity.Id);
			db.AddInParameter(cmd,"@WeChatOpenId",  DbType.String,entity.WeChatOpenId);
			db.AddInParameter(cmd,"@TemplateIid",  DbType.String,entity.TemplateIid);
			db.AddInParameter(cmd,"@WeChatUrl",  DbType.String,entity.WeChatUrl);
			db.AddInParameter(cmd,"@ParamStr",  DbType.String,entity.ParamStr);
			db.AddInParameter(cmd,"@Result",  DbType.String,entity.Result);
			db.AddInParameter(cmd,"@Remark",  DbType.String,entity.Remark);
    	 	return db.ExecuteNonQuery(cmd);
		}		
			/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteWeChatMsgByKey(int id)
	    {
			string sql=@"delete from WeChatMsg where  and Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeChatMsgDisabled()
        {
            string sql = @"delete from  WeChatMsg  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeChatMsgByIds(string ids)
        {
            string sql = @"Delete from WeChatMsg  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeChatMsgByIds(string ids)
        {
            string sql = @"Update   WeChatMsg set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		/// <summary>
		/// 根据主键值读取记录。如果数据库不存在这条数据将返回null
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   WeChatMsgEntity GetWeChatMsg(int id)
		{
			string sql=@"SELECT  [Id],[WeChatOpenId],[TemplateIid],[WeChatUrl],[ParamStr],[Result],[Remark]
							FROM
							dbo.[WeChatMsg] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd,"@Id", DbType.Int32,id);
    		WeChatMsgEntity entity=new WeChatMsgEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.WeChatOpenId=StringUtils.GetDbString(reader["WeChatOpenId"]);
					entity.TemplateIid=StringUtils.GetDbString(reader["TemplateIid"]);
					entity.WeChatUrl=StringUtils.GetDbString(reader["WeChatUrl"]);
					entity.ParamStr=StringUtils.GetDbString(reader["ParamStr"]);
					entity.Result=StringUtils.GetDbString(reader["Result"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
				}
   		    }
            return entity;
		}
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<WeChatMsgEntity> GetWeChatMsgList(int pagesize, int pageindex, ref  int recordCount )
		{
			string sql=@"SELECT   [Id],[WeChatOpenId],[TemplateIid],[WeChatUrl],[ParamStr],[Result],[Remark]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[WeChatOpenId],[TemplateIid],[WeChatUrl],[ParamStr],[Result],[Remark] from dbo.[WeChatMsg] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2=@"Select count(1) from dbo.[WeChatMsg] with (nolock) ";
            IList<WeChatMsgEntity> entityList = new List< WeChatMsgEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					WeChatMsgEntity entity=new WeChatMsgEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.WeChatOpenId=StringUtils.GetDbString(reader["WeChatOpenId"]);
					entity.TemplateIid=StringUtils.GetDbString(reader["TemplateIid"]);
					entity.WeChatUrl=StringUtils.GetDbString(reader["WeChatUrl"]);
					entity.ParamStr=StringUtils.GetDbString(reader["ParamStr"]);
					entity.Result=StringUtils.GetDbString(reader["Result"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
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
        public IList<WeChatMsgEntity> GetWeChatMsgAll()
        {

            string sql = @"SELECT    [Id],[WeChatOpenId],[TemplateIid],[WeChatUrl],[ParamStr],[Result],[Remark] from dbo.[WeChatMsg] WITH(NOLOCK)	";
		    IList<WeChatMsgEntity> entityList = new List<WeChatMsgEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   WeChatMsgEntity entity=new WeChatMsgEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.WeChatOpenId=StringUtils.GetDbString(reader["WeChatOpenId"]);
					entity.TemplateIid=StringUtils.GetDbString(reader["TemplateIid"]);
					entity.WeChatUrl=StringUtils.GetDbString(reader["WeChatUrl"]);
					entity.ParamStr=StringUtils.GetDbString(reader["ParamStr"]);
					entity.Result=StringUtils.GetDbString(reader["Result"]);
					entity.Remark=StringUtils.GetDbString(reader["Remark"]);
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
        public int  ExistNum(WeChatMsgEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[WeChatMsg] WITH(NOLOCK) ";
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
