using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.SqlClient;
using SuperMarket.Core.Util;
using SuperMarket.Core;
using SuperMarket.Model;

namespace SuperMarket.Data 
{
    public class BaseSuperMarketDB  
    {
        public static Database db;
        public BaseSuperMarketDB()
        {
            db = DatabaseFactory.CreateDatabase("DefaultConnection");
        }

        /// <summary>
        /// 判断数据是否存在
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        public static bool Exists(string strSql)
        {
            int obj = StringUtils.SafeInt(ExecuteScalar(strSql).ToString());
            if (obj == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public static object ExecuteScalar(string strSql)
        {
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            return db.ExecuteScalar(cmd);
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>影响的记录数</returns>
        public static int ExecuteSql(string strSql)
        {
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            return db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 获取数据数量
        /// </summary>
        /// <returns></returns>
        public static int GetCount(string strSql)
        {
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            return StringUtils.SafeInt(db.ExecuteScalar(cmd).ToString());
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string strSql)
        {
            DbCommand cmd = db.GetSqlStringCommand(strSql);
            return db.ExecuteReader(cmd);
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="SQLStringList">多条SQL语句</param>		
        public static int ExecuteSqlTran(IList<string> strSql)
        {
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadUncommitted);
                int count = 0;
                try
                {
                    for (int n = 0; n < strSql.Count; n++)
                    {
                        string strsql = strSql[n].ToString();
                        DbCommand cmd = db.GetSqlStringCommand(strsql);
                        db.ExecuteNonQuery(cmd, transaction);
                    }
                    transaction.Commit();
                    count += 1;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    count = -1;
                }
                connection.Close();
                return count;
            }
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string strSql)
        {

            DbCommand cmd = db.GetSqlStringCommand(strSql);

            return db.ExecuteDataSet(CommandType.Text, strSql);
        }

    }
}
