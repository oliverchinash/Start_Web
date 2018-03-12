using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CommentDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CommentShow的业务逻辑层。
创建时间：2016/9/8 13:30:19
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CommentDB
{
	  
	/// <summary>
	/// 表CommentShow的业务逻辑层。
	/// </summary>
	public class CommentShowBLL
	{
	    #region 实例化
        public static CommentShowBLL Instance
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
            internal static readonly CommentShowBLL instance = new CommentShowBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CommentShow，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="commentShow">要添加的CommentShow数据实体对象</param>
		public   int AddCommentShow(CommentShowEntity commentShow)
		{
			  if (commentShow.Id > 0)
            {
                return UpdateCommentShow(commentShow);
            }
          
            else if (CommentShowBLL.Instance.IsExist(commentShow))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CommentShowDA.Instance.AddCommentShow(commentShow);
            }
	 	}

		/// <summary>
		/// 更新一条CommentShow记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="commentShow">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCommentShow(CommentShowEntity commentShow)
		{
			return CommentShowDA.Instance.UpdateCommentShow(commentShow);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCommentShowByKey(int id)
        {
            return CommentShowDA.Instance.DeleteCommentShowByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCommentShowDisabled()
        {
            return CommentShowDA.Instance.DeleteCommentShowDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCommentShowByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CommentShowDA.Instance.DeleteCommentShowByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCommentShowByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CommentShowDA.Instance.DisableCommentShowByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CommentShow实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CommentShow实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CommentShowEntity GetCommentShow(int id)
		{
			return CommentShowDA.Instance.GetCommentShow(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CommentShowEntity> GetCommentShowList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CommentShowDA.Instance.GetCommentShowList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetCommentShowAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CommentShowListKey";// SysCacheKey.CommentShowListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CommentShowEntity> list = null;
                    list = CommentShowDA.Instance.GetCommentShowAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CommentShowEntity commentShow)
        {
            return CommentShowDA.Instance.ExistNum(commentShow)>0;
        }
		
	}
}

