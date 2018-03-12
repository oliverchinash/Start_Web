using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemFromBaidu的业务逻辑层。
创建时间：2017/4/5 15:17:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemFromBaidu的业务逻辑层。
	/// </summary>
	public class MemFromBaiduBLL
	{
	    #region 实例化
        public static MemFromBaiduBLL Instance
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
            internal static readonly MemFromBaiduBLL instance = new MemFromBaiduBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemFromBaidu，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memFromBaidu">要添加的MemFromBaidu数据实体对象</param>
		public   int AddMemFromBaidu(MemFromBaiduEntity memFromBaidu)
		{
			  if (memFromBaidu.Id > 0)
            {
                return UpdateMemFromBaidu(memFromBaidu);
            }
            else if (MemFromBaiduBLL.Instance.IsExist(memFromBaidu))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemFromBaiduDA.Instance.AddMemFromBaidu(memFromBaidu);
            }
	 	}
        public int AddOrEditMemFromBaidu(MemFromBaiduEntity memFromBaidu)
        {
            return MemFromBaiduDA.Instance.AddOrEditMemFromBaidu(memFromBaidu);

        }
        /// <summary>
        /// 更新一条MemFromBaidu记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="memFromBaidu">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateMemFromBaidu(MemFromBaiduEntity memFromBaidu)
		{
			return MemFromBaiduDA.Instance.UpdateMemFromBaidu(memFromBaidu);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemFromBaiduByKey(int id)
        {
            return MemFromBaiduDA.Instance.DeleteMemFromBaiduByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemFromBaiduDisabled()
        {
            return MemFromBaiduDA.Instance.DeleteMemFromBaiduDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemFromBaiduByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemFromBaiduDA.Instance.DeleteMemFromBaiduByIds(idstr); 
        }
        public int SetPrintCodeUnRegister(string posstrs,Int64 code)
        {
            posstrs = posstrs.Trim('|');
            return MemFromBaiduDA.Instance.SetPrintCodeUnRegister(posstrs,code);


        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemFromBaiduByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemFromBaiduDA.Instance.DisableMemFromBaiduByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemFromBaidu实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemFromBaidu实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemFromBaiduEntity GetMemFromBaidu(int id)
		{
			return MemFromBaiduDA.Instance.GetMemFromBaidu(id);			
		}
        public int  SetMemberPosition(int memid,decimal posilng,decimal posilat, string title,string address)
        {
            return MemFromBaiduDA.Instance.SetMemberPosition(memid, posilng, posilat,   title,address); 
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemFromBaiduEntity> GetMemFromBaiduList(int pageSize, int pageIndex, ref  int recordCount, decimal lngmax, decimal latmax, decimal lngmin, decimal latmin)
        {
            return MemFromBaiduDA.Instance.GetMemFromBaiduList(pageSize, pageIndex, ref recordCount, lngmax, latmax, lngmin, latmin);
        }
        public IList<MemFromBaiduEntity> GetMemFromBaiduList(int pageSize, int pageIndex, ref int recordCount, Int64 code)
        {
            return MemFromBaiduDA.Instance.GetMemFromBaiduList(pageSize, pageIndex, ref recordCount, code);
        }
        public IList<MemFromBaiduEntity> GetMemFromBaiduAll(Int64 code)
        {

            IList<MemFromBaiduEntity> list = null;
            list = MemFromBaiduDA.Instance.GetMemFromBaiduAll(code);
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemFromBaiduEntity memFromBaidu)
        {
            return MemFromBaiduDA.Instance.ExistNum(memFromBaidu)>0;
        }
		
	}
}

