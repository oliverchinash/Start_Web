using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SMSTemplet的业务逻辑层。
创建时间：2017/2/2 9:46:25
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表SMSTemplet的业务逻辑层。
	/// </summary>
	public class SMSTempletBLL
	{
	    #region 实例化
        public static SMSTempletBLL Instance
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
            internal static readonly SMSTempletBLL instance = new SMSTempletBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SMSTemplet，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sMSTemplet">要添加的SMSTemplet数据实体对象</param>
		public   int AddSMSTemplet(SMSTempletEntity sMSTemplet)
		{
			  if (sMSTemplet.Id > 0)
            {
                return UpdateSMSTemplet(sMSTemplet);
            }
				  else if (string.IsNullOrEmpty(sMSTemplet.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (SMSTempletBLL.Instance.IsExist(sMSTemplet))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return SMSTempletDA.Instance.AddSMSTemplet(sMSTemplet);
            }
	 	}
      
        /// <summary>
        /// 更新一条SMSTemplet记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="sMSTemplet">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateSMSTemplet(SMSTempletEntity sMSTemplet)
		{
			return SMSTempletDA.Instance.UpdateSMSTemplet(sMSTemplet);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSMSTempletByKey(int id)
        {
            return SMSTempletDA.Instance.DeleteSMSTempletByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSMSTempletDisabled()
        {
            return SMSTempletDA.Instance.DeleteSMSTempletDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSMSTempletByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SMSTempletDA.Instance.DeleteSMSTempletByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSMSTempletByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SMSTempletDA.Instance.DisableSMSTempletByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SMSTemplet实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SMSTemplet实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SMSTempletEntity GetSMSTemplet(int id)
		{
			return SMSTempletDA.Instance.GetSMSTemplet(id);			
		}
        public SMSTempletEntity GetSMSTempletByCode(string code)
        {
            return SMSTempletDA.Instance.GetSMSTempletByCode(code);
        }
        /// <summary>
		/// 根据主键获取一个SMSTemplet实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SMSTemplet实体</returns>
		/// <param name="columns">要返回的列</param>
		public string GetSMSContentByCode(string code)
        {
            SMSTempletEntity entity = new SMSTempletEntity();
            entity= SMSTempletDA.Instance.GetSMSContentByCode(code);
            if(entity.Id>0)
            {
                return entity.SMSTContent;
            }
            return "";
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SMSTempletEntity> GetSMSTempletList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SMSTempletDA.Instance.GetSMSTempletList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSMSTempletAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SMSTempletListKey";// SysCacheKey.SMSTempletListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<SMSTempletEntity> list = null;
                    list = SMSTempletDA.Instance.GetSMSTempletAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SMSTempletEntity sMSTemplet)
        {
            return SMSTempletDA.Instance.ExistNum(sMSTemplet)>0;
        }
		
	}
}

