using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表B2BNotice的业务逻辑层。
创建时间：2016/12/19 18:15:13
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表B2BNotice的业务逻辑层。
	/// </summary>
	public class B2BNoticeBLL
	{
	    #region 实例化
        public static B2BNoticeBLL Instance
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
            internal static readonly B2BNoticeBLL instance = new B2BNoticeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表B2BNotice，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="b2BNotice">要添加的B2BNotice数据实体对象</param>
		public   int AddB2BNotice(B2BNoticeEntity b2BNotice)
		{
			  if (b2BNotice.Id > 0)
            {
                return UpdateB2BNotice(b2BNotice);
            }
          
            //else if (B2BNoticeBLL.Instance.IsExist(b2BNotice))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return B2BNoticeDA.Instance.AddB2BNotice(b2BNotice);
            }
	 	}

		/// <summary>
		/// 更新一条B2BNotice记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="b2BNotice">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateB2BNotice(B2BNoticeEntity b2BNotice)
		{
			return B2BNoticeDA.Instance.UpdateB2BNotice(b2BNotice);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteB2BNoticeByKey(int id)
        {
            return B2BNoticeDA.Instance.DeleteB2BNoticeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteB2BNoticeDisabled()
        {
            return B2BNoticeDA.Instance.DeleteB2BNoticeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteB2BNoticeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return B2BNoticeDA.Instance.DeleteB2BNoticeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableB2BNoticeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return B2BNoticeDA.Instance.DisableB2BNoticeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个B2BNotice实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>B2BNotice实体</returns>
		/// <param name="columns">要返回的列</param>
		public   B2BNoticeEntity GetB2BNotice(int id)
		{
			return B2BNoticeDA.Instance.GetB2BNotice(id);			
		}
        public B2BNoticeEntity GetB2BNoticeByName(int systemtype,string name)
        {
            return B2BNoticeDA.Instance.GetB2BNoticeByName(systemtype,   name);
        }
        public IList<B2BNoticeEntity> GetB2BNoticeListTop4(int systemtype)
        {
            IList<B2BNoticeEntity> list = null;
            string _cachekey = "GetB2BNoticeListTop4_"+ systemtype;// GetB2BNoticeListTop4;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list= B2BNoticeDA.Instance.GetB2BNoticeListTop4(systemtype);
            }
            else
            {
                list = (IList<B2BNoticeEntity>)obj;
            }
            return list;
        }
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<B2BNoticeEntity> GetB2BNoticeList(int pageSize, int pageIndex, ref  int recordCount,string  key, int _status,int  _noticetype, int systype =1)
        {
            return B2BNoticeDA.Instance.GetB2BNoticeList(pageSize, pageIndex, ref recordCount, key, _status, _noticetype, systype);
        }
		
		public async Task GetB2BNoticeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="B2BNoticeListKey";// SysCacheKey.B2BNoticeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<B2BNoticeEntity> list = null;
                    list = B2BNoticeDA.Instance.GetB2BNoticeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(B2BNoticeEntity b2BNotice)
        {
            return B2BNoticeDA.Instance.ExistNum(b2BNotice)>0;
        }
		
	}
}

