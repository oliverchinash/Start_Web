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
功能描述：ReturnXSWL表的数据访问类。
创建时间：2017/1/16 12:07:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
    /// <summary>
    /// ReturnXSWLEntity的数据访问操作
    /// </summary>
    public partial class ReturnXSWLDA : BaseSuperMarketDB
    {
        #region 实例化
        public static ReturnXSWLDA Instance
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
            internal static readonly ReturnXSWLDA instance = new ReturnXSWLDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表ReturnXSWL，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="returnXSWL">待插入的实体对象</param>
        public int AddReturnXSWL(ReturnXSWLEntity entity)
        {
            string sql = @"
IF not  EXISTS(SELECT 1 FROM ReturnXSWL where CGMemId=@CGMemId and ReturnId=@ReturnId)
begin
insert into ReturnXSWL(CGMemId, [AccepterName],[AccepterPhone],[ReturnId],[AahamaPost],[AahamaAddress],[AahamaPhone],[AahamaProvince],[AahamaCity],[AahamaName],[WLCode],[WLComName],[Remark],[WLTime])VALUES
			            (@CGMemId, @AccepterName,@AccepterPhone,@ReturnId,@AahamaPost,@AahamaAddress,@AahamaPhone,@AahamaProvince,@AahamaCity,@AahamaName,@WLCode,@WLComName,@Remark,@WLTime);
			SELECT SCOPE_IDENTITY();
end
else
begin
 UPDATE dbo.[ReturnXSWL] SET
                     [AahamaPost]=@AahamaPost,[AahamaAddress]=@AahamaAddress,[AahamaPhone]=@AahamaPhone,[AahamaProvince]=@AahamaProvince,[AahamaCity]=@AahamaCity,[AahamaName]=@AahamaName,[WLCode]=@WLCode,[WLComName]=@WLComName,[Remark]=@Remark,[WLTime]=@WLTime
                       WHERE   [ReturnId]=@ReturnId and CGMemId=@CGMemId
end

";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, entity.ReturnId);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@AahamaPost", DbType.String, entity.AahamaPost);
            db.AddInParameter(cmd, "@AahamaAddress", DbType.String, entity.AahamaAddress);
            db.AddInParameter(cmd, "@AahamaPhone", DbType.String, entity.AahamaPhone);
            db.AddInParameter(cmd, "@AahamaProvince", DbType.Int32, entity.AahamaProvince);
            db.AddInParameter(cmd, "@AahamaCity", DbType.Int32, entity.AahamaCity);
            db.AddInParameter(cmd, "@AahamaName", DbType.String, entity.AahamaName);
            db.AddInParameter(cmd, "@WLCode", DbType.String, entity.WLCode);
            db.AddInParameter(cmd, "@WLComName", DbType.String, entity.WLComName);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@WLTime", DbType.DateTime, entity.WLTime);
            db.AddInParameter(cmd, "@AccepterName", DbType.String, entity.AccepterName);
            db.AddInParameter(cmd, "@AccepterPhone", DbType.String, entity.AccepterPhone);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }


        public int SaveReturnXSWL(ReturnXSWLEntity entity)
        {
            string sql = @"
IF not  EXISTS(SELECT 1 FROM ReturnXSWL where CGMemId=@CGMemId and ReturnId=@ReturnId)
begin
insert into ReturnXSWL(CGMemId, [AccepterName],NewOrderCode,ReturnType,[AccepterPhone],[ReturnId],[ReturnNum],[AahamaPost],[AahamaAddress],[AahamaPhone],[AahamaProvince],[AahamaCity],[AahamaName],[WLCode],[WLComName],[Remark],[WLTime])VALUES
			            (@CGMemId, @AccepterName,@NewOrderCode,@ReturnType,@AccepterPhone,@ReturnId,@ReturnNum,@AahamaPost,@AahamaAddress,@AahamaPhone,@AahamaProvince,@AahamaCity,@AahamaName,@WLCode,@WLComName,@Remark,@WLTime);
			SELECT SCOPE_IDENTITY();
end
else
begin
 UPDATE dbo.[ReturnXSWL] SET [ReturnNum]=@ReturnNum,
                     [AahamaPost]=@AahamaPost,NewOrderCode=@NewOrderCode,ReturnType=@ReturnType,[AahamaAddress]=@AahamaAddress,[AahamaPhone]=@AahamaPhone,[AahamaProvince]=@AahamaProvince,[AahamaCity]=@AahamaCity,[AahamaName]=@AahamaName,[WLCode]=@WLCode,[WLComName]=@WLComName,[Remark]=@Remark,[WLTime]=@WLTime
                       WHERE   [ReturnId]=@ReturnId and CGMemId=@CGMemId
end

";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, entity.ReturnId);
            db.AddInParameter(cmd, "@ReturnNum", DbType.Int32, entity.ReturnNum); 
            db.AddInParameter(cmd, "@NewOrderCode", DbType.Int64, entity.NewOrderCode); 
            db.AddInParameter(cmd, "@ReturnType", DbType.Int32, entity.ReturnType); 
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@AahamaPost", DbType.String, entity.AahamaPost);
            db.AddInParameter(cmd, "@AahamaAddress", DbType.String, entity.AahamaAddress);
            db.AddInParameter(cmd, "@AahamaPhone", DbType.String, entity.AahamaPhone);
            db.AddInParameter(cmd, "@AahamaProvince", DbType.Int32, entity.AahamaProvince);
            db.AddInParameter(cmd, "@AahamaCity", DbType.Int32, entity.AahamaCity);
            db.AddInParameter(cmd, "@AahamaName", DbType.String, entity.AahamaName);
            db.AddInParameter(cmd, "@WLCode", DbType.String, entity.WLCode);
            db.AddInParameter(cmd, "@WLComName", DbType.String, entity.WLComName);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@WLTime", DbType.DateTime, entity.WLTime);
            db.AddInParameter(cmd, "@AccepterName", DbType.String, entity.AccepterName);
            db.AddInParameter(cmd, "@AccepterPhone", DbType.String, entity.AccepterPhone);
           return db.ExecuteNonQuery(cmd);
         
        }
        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="returnXSWL">待更新的实体对象</param>
        public int UpdateReturnXSWL(ReturnXSWLEntity entity)
        {
            string sql = @" UPDATE dbo.[ReturnXSWL] SET
                     [AahamaPost]=@AahamaPost,[AahamaAddress]=@AahamaAddress,[AahamaPhone]=@AahamaPhone,[AahamaProvince]=@AahamaProvince,[AahamaCity]=@AahamaCity,[AahamaName]=@AahamaName,[WLCode]=@WLCode,[WLComName]=@WLComName,[Remark]=@Remark,[WLTime]=@WLTime
                       WHERE   [ReturnId]=@ReturnId and CGMemId=@CGMemId ";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, entity.ReturnId);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);
            db.AddInParameter(cmd, "@AahamaPost", DbType.String, entity.AahamaPost);
            db.AddInParameter(cmd, "@AahamaAddress", DbType.String, entity.AahamaAddress);
            db.AddInParameter(cmd, "@AahamaPhone", DbType.String, entity.AahamaPhone);
            db.AddInParameter(cmd, "@AahamaProvince", DbType.Int32, entity.AahamaProvince);
            db.AddInParameter(cmd, "@AahamaCity", DbType.Int32, entity.AahamaCity);
            db.AddInParameter(cmd, "@AahamaName", DbType.String, entity.AahamaName);
            db.AddInParameter(cmd, "@WLCode", DbType.String, entity.WLCode);
            db.AddInParameter(cmd, "@WLComName", DbType.String, entity.WLComName);
            db.AddInParameter(cmd, "@Remark", DbType.String, entity.Remark);
            db.AddInParameter(cmd, "@WLTime", DbType.DateTime, entity.WLTime);
            return db.ExecuteNonQuery(cmd);
        }



        public int UpdateReturnXSWLOfPickUp(int returnId,string accepterName,string accepterPhone)
        {
            string sql = @"update dbo.ReturnXSWL set AccepterName=@AccepterName,AccepterPhone=@AccepterPhone where ReturnId=@ReturnId";
            DbCommand cmd = db.GetSqlStringCommand(sql);
             
            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, returnId);
            db.AddInParameter(cmd, "@AccepterName", DbType.String, accepterName);
            db.AddInParameter(cmd, "@AccepterPhone", DbType.String, accepterPhone);
          
            return db.ExecuteNonQuery(cmd);
        }

        public int UpdateReturnXSWLOfExpress(int returnId, string WLCode, string WLComName)
        {
            string sql = @"update dbo.ReturnXSWL set WLCode=@WLCode,WLComName=@WLComName where ReturnId=@ReturnId";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, returnId);
            db.AddInParameter(cmd, "@WLCode", DbType.String, WLCode);
            db.AddInParameter(cmd, "@WLComName", DbType.String, WLComName);

            return db.ExecuteNonQuery(cmd);
        }

        public int ReBackRecivedCG(int returnid, int cgmemid)
        {
            string sql = @"
begin tran

DECLARE @rowaccount int
update dbo.ReturnXSWL set Status=@WLStatus where ReturnId=@ReturnId and CGMemId=@CGMemId 
SET @rowaccount=@@rowcount 
IF @rowaccount>0 AND NOT EXISTS(SELECT 1 FROM dbo.ReturnXSWL WHERE  ReturnId=@ReturnId  AND STATUS=@WLStatusWait)
BEGIN
UPDATE dbo.ReturnXS SET STATUS=@ReturnXSStatus WHERE id=@ReturnId
END
IF @rowaccount>0 AND NOT EXISTS(SELECT 1 FROM dbo.[FinanceRefund] WHERE   [ReturnXSId]=@ReturnId )
BEGIN 
INSERT INTO [dbo].[FinanceRefund]
								([OrderCode]
								,[PayTradeNo]
								,[RebackFee]
								,[ActRebackFee]
								,[Description]
								,[ReturnXSId]
								,[CreateTime]
								,[Status] )	
						SELECT a.ReturnOrderCode,b.tradeno,a.Price*a.Num,0,'退款原因:退货',a.Id,GETDATE(),0 
						FROM DBO.ReturnXS A INNER JOIN DBO.PayAliResultOrder B ON A.ReturnOrderCode=B.outtradeno
						WHERE a.Id= @ReturnId; 
END 
COMMIT TRAN

";
        
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, returnid);
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, cgmemid);
            db.AddInParameter(cmd, "@WLStatus", DbType.Int32, (int)ReturnXSWLStatus.HasRecived);
            db.AddInParameter(cmd, "@WLStatusWait", DbType.Int32, (int)ReturnXSWLStatus.Default);
            db.AddInParameter(cmd, "@ReturnXSStatus", DbType.Int32, (int) ReturnOrderStatus.Complete);

            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteReturnXSWLByKey(int id)
        {
            string sql = @"delete from ReturnXSWL where Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteReturnXSWLDisabled()
        {
            string sql = @"delete from  ReturnXSWL  where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteReturnXSWLByIds(string ids)
        {
            string sql = @"Delete from ReturnXSWL  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableReturnXSWLByIds(string ids)
        {
            string sql = @"Update   ReturnXSWL set IsActive=0  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public ReturnXSWLEntity GetReturnXSWL(int id)
        {
            string sql = @"SELECT  [Id],[ReturnId],[AahamaPost],[AahamaAddress],[AahamaPhone],[AahamaProvince],[AahamaCity],[AahamaName],[WLCode],[WLComName],[Remark],[WLTime]
							FROM
							dbo.[ReturnXSWL] WITH(NOLOCK)	
							WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            ReturnXSWLEntity entity = new ReturnXSWLEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ReturnId = StringUtils.GetDbInt(reader["ReturnId"]);
                    entity.AahamaPost = StringUtils.GetDbString(reader["AahamaPost"]);
                    entity.AahamaAddress = StringUtils.GetDbString(reader["AahamaAddress"]);
                    entity.AahamaPhone = StringUtils.GetDbString(reader["AahamaPhone"]);
                    entity.AahamaProvince = StringUtils.GetDbInt(reader["AahamaProvince"]);
                    entity.AahamaCity = StringUtils.GetDbInt(reader["AahamaCity"]);
                    entity.AahamaName = StringUtils.GetDbString(reader["AahamaName"]);
                    entity.WLCode = StringUtils.GetDbString(reader["WLCode"]);
                    entity.WLComName = StringUtils.GetDbString(reader["WLComName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.WLTime = StringUtils.GetDbDateTime(reader["WLTime"]);
                }
            }
            return entity;
        }

        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<ReturnXSWLEntity> GetReturnXSWLList(int pagesize, int pageindex, ref int recordCount)
        {
            string sql = @"SELECT   [Id],[ReturnId],[AahamaPost],[AahamaAddress],[AahamaPhone],[AahamaProvince],[AahamaCity],[AahamaName],[WLCode],[WLComName],[Remark],[WLTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[ReturnId],[AahamaPost],[AahamaAddress],[AahamaPhone],[AahamaProvince],[AahamaCity],[AahamaName],[WLCode],[WLComName],[Remark],[WLTime] from dbo.[ReturnXSWL] WITH(NOLOCK)	
						WHERE  1=1 ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[ReturnXSWL] with (nolock) ";
            IList<ReturnXSWLEntity> entityList = new List<ReturnXSWLEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReturnXSWLEntity entity = new ReturnXSWLEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ReturnId = StringUtils.GetDbInt(reader["ReturnId"]);
                    entity.AahamaPost = StringUtils.GetDbString(reader["AahamaPost"]);
                    entity.AahamaAddress = StringUtils.GetDbString(reader["AahamaAddress"]);
                    entity.AahamaPhone = StringUtils.GetDbString(reader["AahamaPhone"]);
                    entity.AahamaProvince = StringUtils.GetDbInt(reader["AahamaProvince"]);
                    entity.AahamaCity = StringUtils.GetDbInt(reader["AahamaCity"]);
                    entity.AahamaName = StringUtils.GetDbString(reader["AahamaName"]);
                    entity.WLCode = StringUtils.GetDbString(reader["WLCode"]);
                    entity.WLComName = StringUtils.GetDbString(reader["WLComName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.WLTime = StringUtils.GetDbDateTime(reader["WLTime"]);
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
        public IList<ReturnXSWLEntity> GetReturnXSWLAll()
        {

            string sql = @"SELECT    [Id],[ReturnId],[AahamaPost],[AahamaAddress],[AahamaPhone],[AahamaProvince],[AahamaCity],[AahamaName],[WLCode],[WLComName],[Remark],[WLTime] from dbo.[ReturnXSWL] WITH(NOLOCK)	";
            IList<ReturnXSWLEntity> entityList = new List<ReturnXSWLEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    ReturnXSWLEntity entity = new ReturnXSWLEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.ReturnId = StringUtils.GetDbInt(reader["ReturnId"]);
                    entity.AahamaPost = StringUtils.GetDbString(reader["AahamaPost"]);
                    entity.AahamaAddress = StringUtils.GetDbString(reader["AahamaAddress"]);
                    entity.AahamaPhone = StringUtils.GetDbString(reader["AahamaPhone"]);
                    entity.AahamaProvince = StringUtils.GetDbInt(reader["AahamaProvince"]);
                    entity.AahamaCity = StringUtils.GetDbInt(reader["AahamaCity"]);
                    entity.AahamaName = StringUtils.GetDbString(reader["AahamaName"]);
                    entity.WLCode = StringUtils.GetDbString(reader["WLCode"]);
                    entity.WLComName = StringUtils.GetDbString(reader["WLComName"]);
                    entity.Remark = StringUtils.GetDbString(reader["Remark"]);
                    entity.WLTime = StringUtils.GetDbDateTime(reader["WLTime"]);
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
        public int ExistNum(ReturnXSWLEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[ReturnXSWL] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where + "  (ReturnId=@ReturnId and CGMemId=@CGMemId) ";
            }
            else
            {
                where = where + " id<>@Id and  ( ReturnId=@ReturnId and CGMemId=@CGMemId) ";
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            } 
            db.AddInParameter(cmd, "@ReturnId", DbType.Int32, entity.ReturnId); 
            db.AddInParameter(cmd, "@CGMemId", DbType.Int32, entity.CGMemId);

            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }








        #endregion
        #endregion
    }
}
