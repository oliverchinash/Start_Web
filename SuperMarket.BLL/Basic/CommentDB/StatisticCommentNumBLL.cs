using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CommentDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StatisticCommentNum的业务逻辑层。
创建时间：2016/11/11 23:31:01
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CommentDB
{
	  
	/// <summary>
	/// 表StatisticCommentNum的业务逻辑层。
	/// </summary>
	public class StatisticCommentNumBLL
	{
	    #region 实例化
        public static StatisticCommentNumBLL Instance
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
            internal static readonly StatisticCommentNumBLL instance = new StatisticCommentNumBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StatisticCommentNum，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticCommentNum">要添加的StatisticCommentNum数据实体对象</param>
		public   int AddStatisticCommentNum(StatisticCommentNumEntity statisticCommentNum)
		{
			  if (statisticCommentNum.Id > 0)
            {
                return UpdateStatisticCommentNum(statisticCommentNum);
            }
          
            else if (StatisticCommentNumBLL.Instance.IsExist(statisticCommentNum))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return StatisticCommentNumDA.Instance.AddStatisticCommentNum(statisticCommentNum);
            }
	 	}

		/// <summary>
		/// 更新一条StatisticCommentNum记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticCommentNum">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStatisticCommentNum(StatisticCommentNumEntity statisticCommentNum)
		{
			return StatisticCommentNumDA.Instance.UpdateStatisticCommentNum(statisticCommentNum);
		}
        /// <summary>
        /// 填充评论内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public int UpdateCommentContent(int id,string content)
        {
            return StatisticCommentNumDA.Instance.UpdateCommentContent(id,   content);

        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStatisticCommentNumByKey(int id)
        {
            return StatisticCommentNumDA.Instance.DeleteStatisticCommentNumByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticCommentNumDisabled()
        {
            return StatisticCommentNumDA.Instance.DeleteStatisticCommentNumDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticCommentNumByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return StatisticCommentNumDA.Instance.DeleteStatisticCommentNumByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticCommentNumByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return StatisticCommentNumDA.Instance.DisableStatisticCommentNumByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StatisticCommentNum实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StatisticCommentNum实体</returns>
		/// <param name="columns">要返回的列</param>
		public   StatisticCommentNumEntity GetStatisticCommentNum(int id)
		{
			return StatisticCommentNumDA.Instance.GetStatisticCommentNum(id);			
		}
        public StatisticCommentNumEntity GetCommentNumByProductId(int productid)
        {
            string _cachekey = "GetCommentNumByProductId_"+ productid;// SysCacheKey.StatisticCommentNumListKey;
            object obj = MemCache.GetCache(_cachekey);
            StatisticCommentNumEntity entity = new StatisticCommentNumEntity();
            if (obj == null)
            {
                entity = StatisticCommentNumDA.Instance.GetCommentNumByProductId(productid);
            }
            else
            {
                entity = (StatisticCommentNumEntity)obj;
            }
            return entity;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<StatisticCommentNumEntity> GetStatisticCommentNumList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return StatisticCommentNumDA.Instance.GetStatisticCommentNumList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetStatisticCommentNumAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="StatisticCommentNumListKey";// SysCacheKey.StatisticCommentNumListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<StatisticCommentNumEntity> list = null;
                    list = StatisticCommentNumDA.Instance.GetStatisticCommentNumAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(StatisticCommentNumEntity statisticCommentNum)
        {
            return StatisticCommentNumDA.Instance.ExistNum(statisticCommentNum)>0;
        }
		
	}
}

