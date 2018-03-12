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
功能描述：MemBillVAT表的数据访问类。
创建时间：2016/11/17 22:48:00
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.MemberDB
{
	/// <summary>
	/// MemBillVATEntity的数据访问操作
	/// </summary>
	public partial class MemBillVATDA: BaseSuperMarketDB
    {
        #region 实例化
        public static MemBillVATDA Instance
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
            internal static readonly MemBillVATDA instance = new MemBillVATDA();
        }
        #endregion
		#region 代码生成
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表MemBillVAT，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memBillVAT">待插入的实体对象</param>
		public int AddMemBillVAT(MemBillVATEntity entity)
		{
		   string sql= @" 
insert into MemBillVAT( [MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],IsDefault,BillType )VALUES
			            ( @MemId,@CompanyName,@CompanyCode,@CompanyAddress,@CompanyPhone,@CompanyBank,@BankAccount,@ReceiverName,@ReceiverPhone,@ReceiverProvince,@ReceiverCity,@ReceiverAddress,@Status,@CreateTime,@UpdateTime,@IsDefault,@BillType );
			SELECT SCOPE_IDENTITY();";
	        DbCommand cmd = db.GetSqlStringCommand(sql);
	   
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
			db.AddInParameter(cmd,"@CompanyName",  DbType.String,entity.CompanyName);
			db.AddInParameter(cmd,"@CompanyCode",  DbType.String,entity.CompanyCode);
			db.AddInParameter(cmd,"@CompanyAddress",  DbType.String,entity.CompanyAddress);
			db.AddInParameter(cmd,"@CompanyPhone",  DbType.String,entity.CompanyPhone);
			db.AddInParameter(cmd,"@CompanyBank",  DbType.String,entity.CompanyBank);
			db.AddInParameter(cmd,"@BankAccount",  DbType.String,entity.BankAccount);
			db.AddInParameter(cmd,"@ReceiverName",  DbType.String,entity.ReceiverName);
			db.AddInParameter(cmd,"@ReceiverPhone",  DbType.String,entity.ReceiverPhone);
			db.AddInParameter(cmd,"@ReceiverProvince",  DbType.Int32,entity.ReceiverProvince);
			db.AddInParameter(cmd,"@ReceiverCity",  DbType.Int32,entity.ReceiverCity);
			db.AddInParameter(cmd,"@ReceiverAddress",  DbType.String,entity.ReceiverAddress);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,0);
			db.AddInParameter(cmd,"@IsDefault",  DbType.Int32, 0);
			db.AddInParameter(cmd,"@BillType",  DbType.Int32, entity.BillType);
			db.AddInParameter(cmd,"@CreateTime",  DbType.DateTime,DateTime.Now.ToString()); 
			db.AddInParameter(cmd,"@UpdateTime",  DbType.DateTime,DateTime.Now.ToString());
            

            object identity = db.ExecuteScalar(cmd); 
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
		}
		
		/// <summary>
		/// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
		/// 如果数据库有数据被更新了则返回True，否则返回False
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="memBillVAT">待更新的实体对象</param>
		public   int UpdateMemBillVAT(MemBillVATEntity entity)
		{
			string sql= @" UPDATE dbo.[MemBillVAT] SET
                       [CompanyName]=@CompanyName,[CompanyCode]=@CompanyCode,[CompanyAddress]=@CompanyAddress,[CompanyPhone]=@CompanyPhone,[CompanyBank]=@CompanyBank,[BankAccount]=@BankAccount,[ReceiverName]=@ReceiverName,[ReceiverPhone]=@ReceiverPhone,[ReceiverProvince]=@ReceiverProvince,[ReceiverCity]=@ReceiverCity,[ReceiverAddress]=@ReceiverAddress,[Status]=@Status
                       WHERE [BillType]=@BillType and [MemId]=@MemId";
		    DbCommand cmd = db.GetSqlStringCommand(sql);
			
			db.AddInParameter(cmd, "@BillType",  DbType.Int32,entity.BillType);
			db.AddInParameter(cmd,"@MemId",  DbType.Int32,entity.MemId);
            db.AddInParameter(cmd,"@CompanyName",  DbType.String,entity.CompanyName);
			db.AddInParameter(cmd,"@CompanyCode",  DbType.String,entity.CompanyCode);
			db.AddInParameter(cmd,"@CompanyAddress",  DbType.String,entity.CompanyAddress);
			db.AddInParameter(cmd,"@CompanyPhone",  DbType.String,entity.CompanyPhone);
			db.AddInParameter(cmd,"@CompanyBank",  DbType.String,entity.CompanyBank);
			db.AddInParameter(cmd,"@BankAccount",  DbType.String,entity.BankAccount);
			db.AddInParameter(cmd,"@ReceiverName",  DbType.String,entity.ReceiverName);
			db.AddInParameter(cmd,"@ReceiverPhone",  DbType.String,entity.ReceiverPhone);
			db.AddInParameter(cmd,"@ReceiverProvince",  DbType.Int32,entity.ReceiverProvince);
			db.AddInParameter(cmd,"@ReceiverCity",  DbType.Int32,entity.ReceiverCity);
			db.AddInParameter(cmd,"@ReceiverAddress",  DbType.String,entity.ReceiverAddress);
			db.AddInParameter(cmd,"@Status",  DbType.Int32,entity.Status);
            if (db.ExecuteNonQuery(cmd) > 0)
            {
                return entity.Id;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 更新状态
        /// </summary>
        /// <returns></returns>
        public int UpdateMemBillVATStatus(int id,int status)
        {
            string sql = @" Update dbo.[MemBillVAT] SET Status=@Status WHERE Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }


        public int UpdateMemBillVATMsg(MemBillVATEntity entity)
        {
            string sql = @" UPDATE dbo.[MemBillVAT] SET
                       [CompanyName]=@CompanyName,[CompanyCode]=@CompanyCode,[CompanyAddress]=@CompanyAddress,[CompanyPhone]=@CompanyPhone,[CompanyBank]=@CompanyBank,[BankAccount]=@BankAccount  ,
[Status]=0 ,[UpdateTime]=@UpdateTime 
                       WHERE [Id]=@id and [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, entity.CompanyCode);
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, entity.CompanyAddress);
            db.AddInParameter(cmd, "@CompanyPhone", DbType.String, entity.CompanyPhone);
            db.AddInParameter(cmd, "@CompanyBank", DbType.String, entity.CompanyBank);
            db.AddInParameter(cmd, "@BankAccount", DbType.String, entity.BankAccount);   
            db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, entity.UpdateTime); 
            if( db.ExecuteNonQuery(cmd)>0)
            {
                return entity.Id;
            }
            else
            {
                return 0;
            } 
        }

        public int AddMemBillVATMsg(MemBillVATEntity entity)
        {
            string sql = @"insert into MemBillVAT( [MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount], [Status],[CreateTime],[UpdateTime] )VALUES
			            ( @MemId,@CompanyName,@CompanyCode,@CompanyAddress,@CompanyPhone,@CompanyBank,@BankAccount,0,@CreateTime,@UpdateTime );
			SELECT SCOPE_IDENTITY();";  
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName);
            db.AddInParameter(cmd, "@CompanyCode", DbType.String, entity.CompanyCode);
            db.AddInParameter(cmd, "@CompanyAddress", DbType.String, entity.CompanyAddress);
            db.AddInParameter(cmd, "@CompanyPhone", DbType.String, entity.CompanyPhone);
            db.AddInParameter(cmd, "@CompanyBank", DbType.String, entity.CompanyBank);
            db.AddInParameter(cmd, "@BankAccount", DbType.String, entity.BankAccount);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now.ToString());
            db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, DateTime.Now.ToString());
            if (db.ExecuteNonQuery(cmd) > 0)
            {
                return entity.Id;
            }
            else
            {
                return 0;
            }
        }
        public int UpdateMemBillVATReciever(MemBillVATEntity entity)
        {
            string sql = @" UPDATE dbo.[MemBillVAT] SET IsDefault=0 where  [MemId]=@MemId
UPDATE dbo.[MemBillVAT] SET [ReceiverName]=@ReceiverName,[ReceiverPhone]=@ReceiverPhone,[ReceiverProvince]=@ReceiverProvince,[ReceiverCity]=@ReceiverCity,[ReceiverAddress]=@ReceiverAddress  
                     ,IsDefault=1  WHERE [Id]=@id and [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, entity.MemId); 
            db.AddInParameter(cmd, "@ReceiverName", DbType.String, entity.ReceiverName);
            db.AddInParameter(cmd, "@ReceiverPhone", DbType.String, entity.ReceiverPhone);
            db.AddInParameter(cmd, "@ReceiverProvince", DbType.Int32, entity.ReceiverProvince);
            db.AddInParameter(cmd, "@ReceiverCity", DbType.Int32, entity.ReceiverCity);
            db.AddInParameter(cmd, "@ReceiverAddress", DbType.String, entity.ReceiverAddress);
            if (db.ExecuteNonQuery(cmd) > 0)
            {
                return entity.Id;
            }
            else
            {
                return 0;
            }
        }
        public int AddComName(int billid, string name, int memid)
        {
            string sql = @"update MemBillVAT set IsDefault=0 where MemId=@MemId 
insert into  dbo.[MemBillVAT]([MemId],[CompanyName],IsDefault,Status,CreateTime,BillType) VALUES
			            ( @MemId,@CompanyName,1,1,getdate(),1)";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, name);
            return db.ExecuteNonQuery(cmd);
        }
        public int UpdateComName(int billid, string name, int memid)
        {
            string sql = @"update MemBillVAT set IsDefault=0 where MemId=@MemId 
UPDATE dbo.[MemBillVAT] SET [CompanyName]=@CompanyName,[IsDefault]=1 
                       WHERE [Id]=@id and [MemId]=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, billid);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@CompanyName", DbType.String, name);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int  DeleteMemBillVATByKey(int id)
	    {
			string sql=@"delete from MemBillVAT where Id=@Id";
			DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd,"@Id", DbType.Int32,id); 
			return  db.ExecuteNonQuery(cmd);
		}
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemBillVATDisabled()
        {
            string sql = @"delete from  MemBillVAT  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemBillVATByIds(string ids)
        {
            string sql = @"Delete from MemBillVAT  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBillVATByIds(string ids)
        {
            string sql = @"Update   MemBillVAT set Status=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        public int DisableMemBillVATById(int id,int memid)
        {
            string sql = @"Update   MemBillVAT set Status=0  where Id =@Id and MemId=@MemId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   MemBillVATEntity GetMemBillVAT(int id)
		{
			string sql= @"SELECT  Top(1) [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime]
						,IsDefault,BillType	FROM
							dbo.[MemBillVAT] WITH(NOLOCK)	
							WHERE [MemId]=@MemId and BillType=@BillType";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            
			db.AddInParameter(cmd, "@MemId", DbType.Int32,id);
			db.AddInParameter(cmd, "@BillType", DbType.Int32, (int)BillType.VAT);
            MemBillVATEntity entity=new MemBillVATEntity();
			using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyCode=StringUtils.GetDbString(reader["CompanyCode"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.CompanyBank=StringUtils.GetDbString(reader["CompanyBank"]);
					entity.BankAccount=StringUtils.GetDbString(reader["BankAccount"]);
					entity.ReceiverName=StringUtils.GetDbString(reader["ReceiverName"]);
					entity.ReceiverPhone=StringUtils.GetDbString(reader["ReceiverPhone"]);
					entity.ReceiverProvince=StringUtils.GetDbInt(reader["ReceiverProvince"]);
					entity.ReceiverCity=StringUtils.GetDbInt(reader["ReceiverCity"]);
					entity.ReceiverAddress=StringUtils.GetDbString(reader["ReceiverAddress"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
					entity.BillType = StringUtils.GetDbInt(reader["BillType"]);
                }
   		    }
            return entity;
		}
        public MemBillVATEntity GetMemBillVATByMemId(int memid)
        {
            string sql = @"SELECT  top(1) [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime]
						,IsDefault,BillType	FROM
							dbo.[MemBillVAT] WITH(NOLOCK)	
							WHERE [MemId]=@MemId and BillType=@BillType";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            db.AddInParameter(cmd, "@BillType", DbType.Int32, (int)BillType.VAT);
            MemBillVATEntity entity = new MemBillVATEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyCode = StringUtils.GetDbString(reader["CompanyCode"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
                    entity.CompanyPhone = StringUtils.GetDbString(reader["CompanyPhone"]);
                    entity.CompanyBank = StringUtils.GetDbString(reader["CompanyBank"]);
                    entity.BankAccount = StringUtils.GetDbString(reader["BankAccount"]);
                    entity.ReceiverName = StringUtils.GetDbString(reader["ReceiverName"]);
                    entity.ReceiverPhone = StringUtils.GetDbString(reader["ReceiverPhone"]);
                    entity.ReceiverProvince = StringUtils.GetDbInt(reader["ReceiverProvince"]);
                    entity.ReceiverCity = StringUtils.GetDbInt(reader["ReceiverCity"]);
                    entity.ReceiverAddress = StringUtils.GetDbString(reader["ReceiverAddress"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.BillType = StringUtils.GetDbInt(reader["BillType"]);
                }
            }
            return entity;
        }
        public MemBillVATEntity GetMemBillVATByTitle(int memid,int _billid, string title,int billtype )
        {
            string where = " where [MemId]=@MemId  ";
            if (_billid > 0)
            {
                where += " and Id=@Id ";
            }
            else
            {
                if (!string.IsNullOrEmpty(title))
                {
                    where += " and CompanyName=@CompanyName ";
                }
                if (billtype!=-1)
                { 
                    where += " and BillType=@BillType ";
                }
            }
            string sql = @"SELECT  top(1) [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime]
						,IsDefault,BillType	FROM
							dbo.[MemBillVAT] WITH(NOLOCK) "+ where;
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid); 
            if (_billid > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, _billid);
            }
            else
            {
                if (!string.IsNullOrEmpty(title))
                {
                    db.AddInParameter(cmd, "@CompanyName", DbType.String, title);
                }
                if (billtype != -1)
                {
                    db.AddInParameter(cmd, "@BillType", DbType.Int32, billtype); 
                }
            }
            MemBillVATEntity entity = new MemBillVATEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyCode = StringUtils.GetDbString(reader["CompanyCode"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
                    entity.CompanyPhone = StringUtils.GetDbString(reader["CompanyPhone"]);
                    entity.CompanyBank = StringUtils.GetDbString(reader["CompanyBank"]);
                    entity.BankAccount = StringUtils.GetDbString(reader["BankAccount"]);
                    entity.ReceiverName = StringUtils.GetDbString(reader["ReceiverName"]);
                    entity.ReceiverPhone = StringUtils.GetDbString(reader["ReceiverPhone"]);
                    entity.ReceiverProvince = StringUtils.GetDbInt(reader["ReceiverProvince"]);
                    entity.ReceiverCity = StringUtils.GetDbInt(reader["ReceiverCity"]);
                    entity.ReceiverAddress = StringUtils.GetDbString(reader["ReceiverAddress"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.BillType = StringUtils.GetDbInt(reader["BillType"]);
                }
            }
            return entity;
        }
 
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<MemBillVATEntity> GetMemBillVATList(int pagesize, int pageindex, ref  int recordCount, int memid,int status)
        {   //只取有效的
            string where = " where 1=1 ";
            if (memid > 0)
            {
                where += " and MemId=@MemId";
            }
            if (status!=-1)
            {
                where += " and Status=@Status";
            }
            string sql= @"SELECT   [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime],IsDefault,BillType
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY isdefault desc, BillType desc,Id desc) AS ROWNUMBER,
						 [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime],IsDefault,BillType from dbo.[MemBillVAT] WITH(NOLOCK)	
					" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2= @"Select count(1) from dbo.[MemBillVAT] with (nolock) " + where;
            IList<MemBillVATEntity> entityList = new List< MemBillVATEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            }
            if (status != -1)
            { 
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					MemBillVATEntity entity=new MemBillVATEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyCode=StringUtils.GetDbString(reader["CompanyCode"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.CompanyBank=StringUtils.GetDbString(reader["CompanyBank"]);
					entity.BankAccount=StringUtils.GetDbString(reader["BankAccount"]);
					entity.ReceiverName=StringUtils.GetDbString(reader["ReceiverName"]);
					entity.ReceiverPhone=StringUtils.GetDbString(reader["ReceiverPhone"]);
					entity.ReceiverProvince=StringUtils.GetDbInt(reader["ReceiverProvince"]);
					entity.ReceiverCity=StringUtils.GetDbInt(reader["ReceiverCity"]);
					entity.ReceiverAddress=StringUtils.GetDbString(reader["ReceiverAddress"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
					entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]); 
					entity.BillType = StringUtils.GetDbInt(reader["BillType"]);

                    entityList.Add(entity);
			    }
			 }
			cmd = db.GetSqlStringCommand(sql2);
            if (memid > 0)
            {
                db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
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
        /// 获取增值税发票列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="companyName"></param>
        /// <param name="status"></param>
        /// <param name="billType"></param>
        /// <returns></returns>
        public IList<MemBillVATEntity> GetMemBillVATList(int pageSize,int pageIndex, ref int recordCount,string companyName,int status,int billType)
        {
            string where = " where 1=1";
            if (!string.IsNullOrEmpty(companyName))
            {
                where += " And CompanyName like @CompanyName";
            }
            if (status>-1)
            {
                where += " And Status=@Status";
            }
            if (billType>0)
            {
                where += " And BillType=@BillType";
            }

            string sql = @"SELECT   [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime],IsDefault,BillType
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY BillType desc,isdefault desc, Id desc) AS ROWNUMBER,
						 [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime],IsDefault,BillType from dbo.[MemBillVAT] WITH(NOLOCK)	
					" + where + @" ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = " Select count(1) from dbo.[MemBillVAT] "+where;

            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageIndex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pageSize);
            IList<MemBillVATEntity> entityList = new List<MemBillVATEntity>();
            if (!string.IsNullOrEmpty(companyName))
            {
                db.AddInParameter(cmd,"@CompanyName",DbType.String,"%"+companyName+"%");
            }
            if (status>-1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (billType>0)
            {
                db.AddInParameter(cmd, "@BillType", DbType.Int32, billType);
            }

            using (IDataReader reader=db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    MemBillVATEntity entity = new MemBillVATEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.MemId = StringUtils.GetDbInt(reader["MemId"]);
                    entity.CompanyName = StringUtils.GetDbString(reader["CompanyName"]);
                    entity.CompanyCode = StringUtils.GetDbString(reader["CompanyCode"]);
                    entity.CompanyAddress = StringUtils.GetDbString(reader["CompanyAddress"]);
                    entity.CompanyPhone = StringUtils.GetDbString(reader["CompanyPhone"]);
                    entity.CompanyBank = StringUtils.GetDbString(reader["CompanyBank"]);
                    entity.BankAccount = StringUtils.GetDbString(reader["BankAccount"]);
                    entity.ReceiverName = StringUtils.GetDbString(reader["ReceiverName"]);
                    entity.ReceiverPhone = StringUtils.GetDbString(reader["ReceiverPhone"]);
                    entity.ReceiverProvince = StringUtils.GetDbInt(reader["ReceiverProvince"]);
                    entity.ReceiverCity = StringUtils.GetDbInt(reader["ReceiverCity"]);
                    entity.ReceiverAddress = StringUtils.GetDbString(reader["ReceiverAddress"]);
                    entity.Status = StringUtils.GetDbInt(reader["Status"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.UpdateTime = StringUtils.GetDbDateTime(reader["UpdateTime"]);
                    entity.CheckManId = StringUtils.GetDbInt(reader["CheckManId"]);
                    entity.CheckTime = StringUtils.GetDbDateTime(reader["CheckTime"]);
                    entity.IsDefault = StringUtils.GetDbInt(reader["IsDefault"]);
                    entity.BillType = StringUtils.GetDbInt(reader["BillType"]);

                    entityList.Add(entity);
                }
            }


            cmd = db.GetSqlStringCommand(sql2);

            if (!string.IsNullOrEmpty(companyName))
            {
                db.AddInParameter(cmd, "@CompanyName", DbType.String, "%" + companyName + "%");
            }
            if (status > -1)
            {
                db.AddInParameter(cmd, "@Status", DbType.Int32, status);
            }
            if (billType > 0)
            {
                db.AddInParameter(cmd, "@BillType", DbType.Int32, billType);
            }

            using (IDataReader reader=db.ExecuteReader(cmd))
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




        public int SetDefault(int id, int memid)
        {
            string sql = @"UPDATE dbo.[MemBillVAT] SET IsDefault=0 where  [MemId]=@MemId;UPDATE dbo.[MemBillVAT] SET IsDefault =1 where Id=@id and  [MemId]=@MemId;  ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<MemBillVATEntity> GetMemBillVATAll()
        {

            string sql = @"SELECT    [Id],[MemId],[CompanyName],[CompanyCode],[CompanyAddress],[CompanyPhone],[CompanyBank],[BankAccount],[ReceiverName],[ReceiverPhone],[ReceiverProvince],[ReceiverCity],[ReceiverAddress],[Status],[CreateTime],[UpdateTime],[CheckManId],[CheckTime] from dbo.[MemBillVAT] WITH(NOLOCK)	";
		    IList<MemBillVATEntity> entityList = new List<MemBillVATEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                   MemBillVATEntity entity=new MemBillVATEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.CompanyName=StringUtils.GetDbString(reader["CompanyName"]);
					entity.CompanyCode=StringUtils.GetDbString(reader["CompanyCode"]);
					entity.CompanyAddress=StringUtils.GetDbString(reader["CompanyAddress"]);
					entity.CompanyPhone=StringUtils.GetDbString(reader["CompanyPhone"]);
					entity.CompanyBank=StringUtils.GetDbString(reader["CompanyBank"]);
					entity.BankAccount=StringUtils.GetDbString(reader["BankAccount"]);
					entity.ReceiverName=StringUtils.GetDbString(reader["ReceiverName"]);
					entity.ReceiverPhone=StringUtils.GetDbString(reader["ReceiverPhone"]);
					entity.ReceiverProvince=StringUtils.GetDbInt(reader["ReceiverProvince"]);
					entity.ReceiverCity=StringUtils.GetDbInt(reader["ReceiverCity"]);
					entity.ReceiverAddress=StringUtils.GetDbString(reader["ReceiverAddress"]);
					entity.Status=StringUtils.GetDbInt(reader["Status"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
					entity.UpdateTime=StringUtils.GetDbDateTime(reader["UpdateTime"]);
					entity.CheckManId=StringUtils.GetDbInt(reader["CheckManId"]);
					entity.CheckTime=StringUtils.GetDbDateTime(reader["CheckTime"]);
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
        public int  ExistNum(MemBillVATEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[MemBillVAT] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
					     where = where+ "  (CompanyName=@CompanyName) ";
					     where = where+ "  (ReceiverName=@ReceiverName) ";
				 
            }
            else
            {
					     where = where+ " id<>@Id and  (CompanyName=@CompanyName) ";
					     where = where+ " id<>@Id and  (ReceiverName=@ReceiverName) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            if (entity.Id > 0)
            { 
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
					
            db.AddInParameter(cmd, "@CompanyName", DbType.String, entity.CompanyName); 
					
            db.AddInParameter(cmd, "@ReceiverName", DbType.String, entity.ReceiverName); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
     
		
		
		
		
		
		
		
		#endregion
		#endregion
	}
}
