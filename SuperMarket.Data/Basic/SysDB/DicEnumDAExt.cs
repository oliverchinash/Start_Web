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
功能描述：DicEnum表的数据访问类。
创建时间：2016/7/14 17:40:23
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.SysDB 
{
	/// <summary>
	/// DicEnumEntity的数据访问操作
	/// </summary>
	public partial class DicEnumDA: BaseSuperMarketDB
    {
        
        #region 代码生成
        #region  自动产生
 
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public   IList<DicEnumEntity> GetDicEnumList(int pagesize, int pageindex,ref  int recordCount,string keyword,int pid )
		{
            string where = "where 1=1 ";
            if (!string.IsNullOrEmpty(keyword))
            {
                where += "and (Name Like @Keyword or Code Like @Keyword) ";
            }
            if (pid==CommonKey.RootParent || pid>0)
            {
                where += "and  ParentId=@ParentId ";
            }

            string sql = @"SELECT   [Id],[Code],[Name],[Description],[ParentCode],[ParentId],[ParentId1],[ParentId2],[ParentId3],[IsActive],[CreateTime],[CreateMan],[CanEdit]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[Code],[Name],[Description],[ParentCode],[ParentId],[ParentId1],[ParentId2],[ParentId3],[IsActive],[CreateTime],[CreateMan],[CanEdit] from dbo.[DicEnum] WITH(NOLOCK)	
						" + where+@") as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";

            string sql2 = @"Select count(1) from dbo.[DicEnum] with (nolock) "+ where;
            IList<DicEnumEntity> entityList = new List<DicEnumEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
            db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
            if (!string.IsNullOrEmpty(keyword))
            { 
                db.AddInParameter(cmd, "@Keyword", DbType.String, "%"+keyword+"%");
            }
            if (pid == CommonKey.RootParent || pid > 0)
            {
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid); 
            }
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    DicEnumEntity entity = new DicEnumEntity();
                    entity.Id = StringUtils.GetDbInt(reader["Id"]);
                    entity.Code = StringUtils.GetDbString(reader["Code"]);
                    entity.Name = StringUtils.GetDbString(reader["Name"]);
                    entity.Description = StringUtils.GetDbString(reader["Description"]);
                    entity.ParentCode = StringUtils.GetDbString(reader["ParentCode"]);
                    entity.ParentId = StringUtils.GetDbInt(reader["ParentId"]);
                    entity.ParentId1 = StringUtils.GetDbInt(reader["ParentId1"]);
                    entity.ParentId2 = StringUtils.GetDbInt(reader["ParentId2"]);
                    entity.ParentId3 = StringUtils.GetDbInt(reader["ParentId3"]);
                    entity.IsActive = StringUtils.GetDbInt(reader["IsActive"]);
                    entity.CreateTime = StringUtils.GetDbDateTime(reader["CreateTime"]);
                    entity.CreateMan = StringUtils.GetDbInt(reader["CreateMan"]);
                    entity.CanEdit = StringUtils.GetDbInt(reader["CanEdit"]);
                    entityList.Add(entity);
                }
            }
            cmd = db.GetSqlStringCommand(sql2);
            if (!string.IsNullOrEmpty(keyword))
            {
                db.AddInParameter(cmd, "@Keyword", DbType.String, "%" + keyword + "%");
            }
            if (pid >= 0)
            {
                db.AddInParameter(cmd, "@ParentId", DbType.Int32, pid);
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

         
        #endregion
        /// <summary>
        /// 判断当前节点是否已存在相同的
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int  ExistNum(DicEnumEntity entity)
        {
            ///id=0,判断总数，ID>0判断除自己之外的总数
            string sql = @"Select count(1) from dbo.[DicEnum] WITH(NOLOCK) ";
            string where = "where ";
            if (entity.Id == 0)
            {
                where = where+ "  (Name=@Name and ParentId=@ParentId) ";
                if (!string.IsNullOrEmpty(entity.Code))
                {
                    where = where + " or Code=@Code  ";
                }
            }
            else
            {
                where = where+" id<>@Id and  ((Name=@Name and ParentId=@ParentId) ";
                if (!string.IsNullOrEmpty(entity.Code))
                {
                    where = where + " or Code=@Code)  ";
                }
                else
                {
                    where = where + ")  ";
                }
            }
            sql = sql + where;
            DbCommand cmd = db.GetSqlStringCommand(sql);
            if (!string.IsNullOrEmpty(entity.Code))
            {
                db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            }
            if (entity.Id > 0)
            {
                db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            }
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name); 
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, entity.ParentId); 
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }
       
        #endregion
    }
}
