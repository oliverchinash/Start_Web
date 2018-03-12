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
功能描述：AssetChange表的数据访问类。
创建时间：2016/8/7 17:12:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.Data.ShoppingDB
{
	/// <summary>
	/// AssetChangeEntity的数据访问操作
	/// </summary>
	public partial class AssetChangeDA  
    { 
		 
	 
		/// <summary>
		/// 读取记录列表。
		/// </summary>
		/// <param name="db">数据库操作对象</param>
		/// <param name="columns">需要返回的列，不提供任何列名时默认将返回所有列</param>
		public   IList<AssetChangeEntity> GetAssetChangeList(int pagesize, int pageindex, ref  int recordCount,int memid )
		{
			string sql= @"SELECT   [Id],[MemId],[OperateType],[ReChargeAmt],[PayAmt],[Reason],[CreateTime]
						FROM
						(SELECT ROW_NUMBER() OVER (ORDER BY Id desc) AS ROWNUMBER,
						 [Id],[MemId],[OperateType],[ReChargeAmt],[PayAmt],[Reason],[CreateTime] from dbo.[AssetChange] WITH(NOLOCK)	
						 where  MemId=@MemId ) as temp 
						where rownumber BETWEEN ((@PageIndex - 1) * @PageSize + 1) AND @PageIndex * @PageSize";
			
			string sql2= @"Select count(1) from dbo.[AssetChange] with (nolock)  where  MemId=@MemId  ";
            IList<AssetChangeEntity> entityList = new List< AssetChangeEntity>();
            DbCommand cmd = db.GetSqlStringCommand(sql);
		    db.AddInParameter(cmd, "@PageIndex", DbType.Int32, pageindex);
		    db.AddInParameter(cmd, "@PageSize", DbType.Int32, pagesize);
		    db.AddInParameter(cmd, "@MemId", DbType.Int32, memid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
					AssetChangeEntity entity=new AssetChangeEntity();
					entity.Id=StringUtils.GetDbInt(reader["Id"]);
					entity.MemId=StringUtils.GetDbInt(reader["MemId"]);
					entity.OperateType=StringUtils.GetDbInt(reader["OperateType"]);
					entity.ReChargeAmt=StringUtils.GetDbDecimal(reader["ReChargeAmt"]);
					entity.PayAmt=StringUtils.GetDbDecimal(reader["PayAmt"]);
					entity.Reason=StringUtils.GetDbString(reader["Reason"]);
					entity.CreateTime=StringUtils.GetDbDateTime(reader["CreateTime"]);
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
		
		 
	}
}
