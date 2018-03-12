using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表BankInfo的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表BankInfo的业务逻辑层。
	/// </summary>
	public class BankInfoBLL
	{
	    #region 实例化
        public static BankInfoBLL Instance
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
            internal static readonly BankInfoBLL instance = new BankInfoBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表BankInfo，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="bankInfo">要添加的BankInfo数据实体对象</param>
		public   int AddBankInfo(BankInfoEntity bankInfo)
		{
			  if (bankInfo.Id > 0)
            {
                return UpdateBankInfo(bankInfo);
            }
				  else if (string.IsNullOrEmpty(bankInfo.CardName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(bankInfo.BankName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(bankInfo.BankSubName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (BankInfoBLL.Instance.IsExist(bankInfo))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return BankInfoDA.Instance.AddBankInfo(bankInfo);
            }
	 	}

		/// <summary>
		/// 更新一条BankInfo记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="bankInfo">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateBankInfo(BankInfoEntity bankInfo)
		{
			return BankInfoDA.Instance.UpdateBankInfo(bankInfo);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteBankInfoByKey(int id)
        {
            return BankInfoDA.Instance.DeleteBankInfoByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBankInfoDisabled()
        {
            return BankInfoDA.Instance.DeleteBankInfoDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBankInfoByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return BankInfoDA.Instance.DeleteBankInfoByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBankInfoByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return BankInfoDA.Instance.DisableBankInfoByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个BankInfo实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>BankInfo实体</returns>
		/// <param name="columns">要返回的列</param>
		public   BankInfoEntity GetBankInfo(int id)
		{
			return BankInfoDA.Instance.GetBankInfo(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<BankInfoEntity> GetBankInfoList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return BankInfoDA.Instance.GetBankInfoList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetBankInfoAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="BankInfoListKey";// SysCacheKey.BankInfoListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<BankInfoEntity> list = null;
                    list = BankInfoDA.Instance.GetBankInfoAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(BankInfoEntity bankInfo)
        {
            return BankInfoDA.Instance.ExistNum(bankInfo)>0;
        }
		
	}
}

