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
        #region 实例化
        public static DicEnumDA Instance
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
            internal static readonly DicEnumDA instance = new DicEnumDA();
        }
        #endregion
        #region 代码生成
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表DicEnum，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="dicEnum">待插入的实体对象</param>
        public int AddDicEnum(DicEnumEntity entity)
        {
            string sql = @"insert into DicEnum( [Code],[Name],[Description],[ParentCode],[ParentId],[ParentId1],[ParentId2],[ParentId3],[IsActive],[CreateTime],[CreateMan],[CanEdit])VALUES
        	            ( @code,@name,@description,@parentCode,@parentId,@parentId1,@parentId2,@parentId3,@isActive,@createTime,@createMan,@canEdit);
        	SELECT SCOPE_IDENTITY();";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
            db.AddInParameter(cmd, "@ParentCode", DbType.String, entity.ParentCode);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, entity.ParentId);
            db.AddInParameter(cmd, "@ParentId1", DbType.Int32, entity.ParentId1);
            db.AddInParameter(cmd, "@ParentId2", DbType.Int32, entity.ParentId2);
            db.AddInParameter(cmd, "@ParentId3", DbType.Int32, entity.ParentId3);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@CreateMan", DbType.Int32, entity.CreateMan);
            db.AddInParameter(cmd, "@CanEdit", DbType.Int32, entity.CanEdit);
            object identity = db.ExecuteScalar(cmd);
            if (identity == null || identity == DBNull.Value) return 0;
            return Convert.ToInt32(identity);
        }

        /// <summary>
        /// 根据主键值更新记录的全部字段（注意：该方法不会对自增字段、timestamp类型字段以及主键字段更新！如果要更新主键字段，请使用Update方法）。
        /// 如果数据库有数据被更新了则返回True，否则返回False
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="dicEnum">待更新的实体对象</param>
        public int UpdateDicEnum(DicEnumEntity entity)
        {
            string sql = @" UPDATE dbo.[DicEnum] SET
                             [Code]=@code,[Name]=@name,[Description]=@description,[ParentCode]=@parentCode,[ParentId]=@parentId,[ParentId1]=@parentId1,[ParentId2]=@parentId2,[ParentId3]=@parentId3,[IsActive]=@isActive,[CreateTime]=@createTime,[CreateMan]=@createMan,[CanEdit]=@canEdit
                             WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, entity.Id);
            db.AddInParameter(cmd, "@Code", DbType.String, entity.Code);
            db.AddInParameter(cmd, "@Name", DbType.String, entity.Name);
            db.AddInParameter(cmd, "@Description", DbType.String, entity.Description);
            db.AddInParameter(cmd, "@ParentCode", DbType.String, entity.ParentCode);
            db.AddInParameter(cmd, "@ParentId", DbType.Int32, entity.ParentId);
            db.AddInParameter(cmd, "@ParentId1", DbType.Int32, entity.ParentId1);
            db.AddInParameter(cmd, "@ParentId2", DbType.Int32, entity.ParentId2);
            db.AddInParameter(cmd, "@ParentId3", DbType.Int32, entity.ParentId3);
            db.AddInParameter(cmd, "@IsActive", DbType.Int32, entity.IsActive);
            db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, entity.CreateTime);
            db.AddInParameter(cmd, "@CreateMan", DbType.Int32, entity.CreateMan);
            db.AddInParameter(cmd, "@CanEdit", DbType.Int32, entity.CanEdit);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDicEnumByKey(int id)
        {
            string sql = @"delete from DicEnum where   Id=@Id";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicEnumDisabled()
        {
            string sql = @"delete from DicEnum where  IsActive=0 and DisabledTime<DATEADD(month, -2, getDate())";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicEnumByIds(string ids)
        {
            string sql = @"Delete from DicEnum  where Id in (" + ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql);
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicEnumByIds(string ids)
        {
            string sql = @"Update  DicEnum set IsActive=0  where Id in ("+ ids + ")";
            DbCommand cmd = db.GetSqlStringCommand(sql); 
            return db.ExecuteNonQuery(cmd);
        }
        /// <summary>
        /// 根据主键值读取记录。如果数据库不存在这条数据将返回null
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public DicEnumEntity GetDicEnum(int id)
        {
            string sql = @"SELECT  [Id],[Code],[Name],[Description],[ParentCode],[ParentId],[ParentId1],[ParentId2],[ParentId3],[IsActive],[CreateTime],[CreateMan],[CanEdit]
        					FROM
        					dbo.[DicEnum] WITH(NOLOCK)	
        					WHERE [Id]=@id";
            DbCommand cmd = db.GetSqlStringCommand(sql);

            db.AddInParameter(cmd, "@Id", DbType.Int32, id);
            DicEnumEntity entity = new DicEnumEntity();
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
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
                }
            }
            return entity;
        }

       
        /// <summary>
        /// 读取记录列表。
        /// </summary>
        /// <param name="db">数据库操作对象</param>
        /// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
        public IList<DicEnumEntity> GetDicEnumAll()
        {

            string sql = @"SELECT   [Id],[Code],[Name],[Description],[ParentCode],[ParentId],[ParentId1],[ParentId2],[ParentId3],[IsActive],[CreateTime],[CreateMan],[CanEdit] from dbo.[DicEnum] WITH(NOLOCK)	";
		    IList<DicEnumEntity> entityList = new List<DicEnumEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql); 
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
            return entityList;
        }
        
        #endregion 
        #endregion
    }
}
