using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CommentDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表Comment的业务逻辑层。
创建时间：2016/9/8 13:30:19
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CommentDB
{
	  
	/// <summary>
	/// 表Comment的业务逻辑层。
	/// </summary>
	public class CommentBLL
	{
	    #region 实例化
        public static CommentBLL Instance
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
            internal static readonly CommentBLL instance = new CommentBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表Comment，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="comment">要添加的Comment数据实体对象</param>
		public   int AddComment(CommentEntity comment)
		{
			if (comment.Id > 0)
            {
                return CommentDA.Instance.UpdateComment(comment);//如果是已经存在的ID 则是更新
            }

            else if (CommentBLL.Instance.IsExist(comment))//如果是再次实例化的对象 则是判断一下是否已经存在
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CommentDA.Instance.AddComment(comment);
            }
	 	}
        public int CreateCommentProc(CommentEntity comment)
        {
            return CommentDA.Instance.CreateCommentProc(comment);

        }
        /// <summary>
        /// 更新一条Comment记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="comment">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateComment(CommentEntity comment)
		{
			return CommentDA.Instance.UpdateComment(comment);
		}

        /// <summary>
        /// 更新评论审核状态
        /// </summary>
        /// <returns></returns>
        public int UpdateCommentStatus(int id,int status)
        {
            return CommentDA.Instance.UpdateCommentStatus(id,status);
        }
         /// <summary>
         /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
         /// </summary>
        public int DeleteCommentByKey(int id)
        {
            return CommentDA.Instance.DeleteCommentByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCommentDisabled()
        {
            return CommentDA.Instance.DeleteCommentDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCommentByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CommentDA.Instance.DeleteCommentByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCommentByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CommentDA.Instance.DisableCommentByIds(idstr);
        }

		/// <summary>
		/// 根据主键获取一个Comment实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Comment实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CommentEntity GetComment(int id)
		{
			return CommentDA.Instance.GetComment(id);			
		}

        /// <summary>
        /// 根据OrderDetailID获取一个Comment实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>Comment实体</returns>
        /// <param name="columns">要返回的列</param>
        public CommentEntity GetCommentByOrderDetailId(int orderdetailid)
        {
            return CommentDA.Instance.GetCommentByOrderDetailId(orderdetailid );
        }


        /// <summary>
        /// 依据订单详情号获取评论详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public IList<CommentEntity> GetCommentByODId(int id,int memid)
        {
            return CommentDA.Instance.GetCommentByODId(id,memid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CommentEntity> GetCommentList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _productid = -1;
            int _memid = -1;
            int _isadd = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.ProductId:
                            {
                                _productid = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.MemId:
                            {
                                _memid = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.CommentIsAdd:
                            {
                                _isadd = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                    }
                }
            }
            return CommentDA.Instance.GetCommentList(pageSize, pageIndex, ref recordCount, _productid,_memid, _isadd);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CommentEntity> GetCommentListByProductId(int pageSize, int pageIndex, ref int recordCount, int productid)
        {
            string _cachekey = "GetCommentListByProductId_" + pageSize+"_"+ pageIndex+"_"+ productid; 
            object obj = MemCache.GetCache(_cachekey);
            IList<CommentEntity> list = null;
            if (obj == null)
            {
                list = CommentDA.Instance.GetListByProductId(pageSize, pageIndex, ref recordCount, productid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<CommentEntity>)obj;
            }
            return list;
        }
        public async Task GetCommentAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CommentListKey";// SysCacheKey.CommentListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CommentEntity> list = null;
                    list = CommentDA.Instance.GetCommentAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CommentEntity comment)
        {
            return CommentDA.Instance.ExistNum(comment)>0;
        }
		
	}
}

