using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CommentDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CommentPic的业务逻辑层。
创建时间：2016/9/22 11:08:03
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CommentDB
{
	  
	/// <summary>
	/// 表CommentPic的业务逻辑层。
	/// </summary>
	public class CommentPicBLL
	{
	    #region 实例化
        public static CommentPicBLL Instance
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
            internal static readonly CommentPicBLL instance = new CommentPicBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CommentPic，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="commentPic">要添加的CommentPic数据实体对象</param>
		public   int AddCommentPic(CommentPicEntity commentPic)
		{
			  if (commentPic.Id > 0)
            {
                return UpdateCommentPic(commentPic);
            }
          
            else if (CommentPicBLL.Instance.IsExist(commentPic))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CommentPicDA.Instance.AddCommentPic(commentPic);
            }
	 	}

		/// <summary>
		/// 更新一条CommentPic记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="commentPic">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCommentPic(CommentPicEntity commentPic)
		{
			return CommentPicDA.Instance.UpdateCommentPic(commentPic);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCommentPicByKey(int id)
        {
            return CommentPicDA.Instance.DeleteCommentPicByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCommentPicDisabled()
        {
            return CommentPicDA.Instance.DeleteCommentPicDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCommentPicByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CommentPicDA.Instance.DeleteCommentPicByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCommentPicByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CommentPicDA.Instance.DisableCommentPicByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CommentPic实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CommentPic实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CommentPicEntity GetCommentPic(int id)
		{
			return CommentPicDA.Instance.GetCommentPic(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CommentPicEntity> GetCommentPicList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CommentPicDA.Instance.GetCommentPicList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<CommentPicEntity> GetCommentPicAll(int commentid)
        {
        
                    IList<CommentPicEntity> list = null;
                    list = CommentPicDA.Instance.GetCommentPicAll(commentid);
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CommentPicEntity commentPic)
        {
            return CommentPicDA.Instance.ExistNum(commentPic)>0;
        }
		
	}
}

