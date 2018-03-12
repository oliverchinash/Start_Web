using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表MemBillVAT的业务逻辑层。
创建时间：2016/11/17 22:48:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemBillVAT的业务逻辑层。
	/// </summary>
	public class MemBillVATBLL
	{
	    #region 实例化
        public static MemBillVATBLL Instance
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
            internal static readonly MemBillVATBLL instance = new MemBillVATBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemBillVAT，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memBillVAT">要添加的MemBillVAT数据实体对象</param>
		public   int AddMemBillVAT(MemBillVATEntity memBillVAT)
		{
            return MemBillVATDA.Instance.AddMemBillVAT(memBillVAT);
          
	 	}

		/// <summary>
		/// 更新一条MemBillVAT记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memBillVAT">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public int UpdateMemBillVAT(MemBillVATEntity memBillVAT)
        { 
            return MemBillVATDA.Instance.UpdateMemBillVAT(memBillVAT);
		}



        /// <summary>
        /// 更新状态
        /// </summary>
        /// <returns></returns>
        public int UpdateMemBillVATStatus(int id,int status)
        {
            return MemBillVATDA.Instance.UpdateMemBillVATStatus(id,status);
        }


        public int UpdateMemBillVATMsg(MemBillVATEntity entity)
        {
            return MemBillVATDA.Instance.UpdateMemBillVATMsg(entity);
        }
        public int AddMemBillVATMsg(MemBillVATEntity entity)
        {
            return MemBillVATDA.Instance.AddMemBillVATMsg(entity);
        }
        public int UpdateMemBillVATReciever(MemBillVATEntity entity)
        {
            return MemBillVATDA.Instance.UpdateMemBillVATReciever(entity);

        }
        public int UpdateComName(int billid,string name,int memid)
        {
            return MemBillVATDA.Instance.UpdateComName(billid,   name,   memid); 
        }
        public int AddComName(int billid, string name, int memid)
        {
            return MemBillVATDA.Instance.AddComName(billid, name, memid);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemBillVATByKey(int id)
        {
            return MemBillVATDA.Instance.DeleteMemBillVATByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemBillVATDisabled()
        {
            return MemBillVATDA.Instance.DeleteMemBillVATDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemBillVATByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemBillVATDA.Instance.DeleteMemBillVATByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBillVATByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemBillVATDA.Instance.DisableMemBillVATByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBillVATById(int id,int memid )
        { 
            return MemBillVATDA.Instance.DisableMemBillVATById(id, memid);
        }
        /// <summary>
        /// 根据主键获取一个MemBillVAT实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>MemBillVAT实体</returns>
        /// <param name="columns">要返回的列</param>
        public   MemBillVATEntity GetMemBillVAT(int id)
		{
            MemBillVATEntity _entity= MemBillVATDA.Instance.GetMemBillVAT(id);
            return _entity;

        }
        public MemBillVATEntity GetMemBillVATByMemId(int memid)
        {
            MemBillVATEntity _entity = MemBillVATDA.Instance.GetMemBillVATByMemId(memid);
            return _entity;
        }
        public MemBillVATEntity GetMemBillVATByTitle(int memid,int _billid,string title,int billtype)
        {
            MemBillVATEntity _entity = MemBillVATDA.Instance.GetMemBillVATByTitle(memid, _billid, title, billtype);
            return _entity;
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemBillVATEntity> GetMemBillVATList(int pageSize, int pageIndex, ref  int recordCount,  int memid,int status)
        {
            return MemBillVATDA.Instance.GetMemBillVATList(pageSize, pageIndex, ref recordCount, memid,status);
        }


        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public IList<MemBillVATEntity> GetMemBillVATList(int pageSize,int pageIndex,ref int recordCount,IList<ConditionUnit> whereList)
        {
            string companyName = string.Empty;
            int status = -1;
            int billType = 0;

            if (whereList!=null&&whereList.Count>0)
            {
                foreach (ConditionUnit item in whereList)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.CompanyName:
                            {
                                companyName = StringUtils.GetDbString(item.CompareValue);
                                break;
                            }
                        case SearchFieldName.MemBillVATStatus:
                            {
                                status = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                        case SearchFieldName.BillType:
                            {
                                billType = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                    }

                }
            }

            return MemBillVATDA.Instance.GetMemBillVATList(pageSize,pageIndex,ref recordCount,companyName,status,billType);
        }


        public int SetDefault(int id, int memid)
        {
            return MemBillVATDA.Instance.SetDefault(id, memid);

        }

        public async Task GetMemBillVATAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemBillVATListKey";// SysCacheKey.MemBillVATListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemBillVATEntity> list = null;
                    list = MemBillVATDA.Instance.GetMemBillVATAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemBillVATEntity memBillVAT)
        {
            return MemBillVATDA.Instance.ExistNum(memBillVAT)>0;
        }
		
	}
}

