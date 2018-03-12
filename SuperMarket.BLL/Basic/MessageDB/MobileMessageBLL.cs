using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MobileMessage的业务逻辑层。
创建时间：2016/11/15 15:31:23
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表MobileMessage的业务逻辑层。
	/// </summary>
	public class MobileMessageBLL
	{
	    #region 实例化
        public static MobileMessageBLL Instance
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
            internal static readonly MobileMessageBLL instance = new MobileMessageBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MobileMessage，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="mobileMessage">要添加的MobileMessage数据实体对象</param>
		public   int AddMobileMessage(MobileMessageEntity mobileMessage)
		{
       
               return MobileMessageDA.Instance.AddMobileMessage(mobileMessage);
            
	 	}
     
        
        /// <summary>
        /// 更新一条MobileMessage记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="mobileMessage">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateMobileMessage(MobileMessageEntity mobileMessage)
		{
			return MobileMessageDA.Instance.UpdateMobileMessage(mobileMessage);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMobileMessageByKey(int id)
        {
            return MobileMessageDA.Instance.DeleteMobileMessageByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMobileMessageDisabled()
        {
            return MobileMessageDA.Instance.DeleteMobileMessageDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMobileMessageByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MobileMessageDA.Instance.DeleteMobileMessageByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMobileMessageByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MobileMessageDA.Instance.DisableMobileMessageByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MobileMessage实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MobileMessage实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MobileMessageEntity GetMobileMessage(int id)
		{
			return MobileMessageDA.Instance.GetMobileMessage(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MobileMessageEntity> GetMobileMessageList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MobileMessageDA.Instance.GetMobileMessageList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMobileMessageAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MobileMessageListKey";// SysCacheKey.MobileMessageListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MobileMessageEntity> list = null;
                    list = MobileMessageDA.Instance.GetMobileMessageAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MobileMessageEntity mobileMessage)
        {
            return MobileMessageDA.Instance.ExistNum(mobileMessage)>0;
        }

        public int SendNum(Int64 ipint)
        {
            return MobileMessageDA.Instance.SendNum(ipint) ;

        }
     
        public DateTime? GetLatelySend(Int64 ipint)
        {
            return MobileMessageDA.Instance.GetLatelySend(ipint);

        }
    }
}

