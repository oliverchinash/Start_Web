using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.CatograyDB;

/*****************************************
功能描述：表CGOrderOfferSub的业务逻辑层。
创建时间：2017/2/5 18:00:14
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{
	  
	/// <summary>
	/// 表CGOrderOfferSub的业务逻辑层。
	/// </summary>
	public class CGOrderOfferSubBLL
	{
	    #region 实例化
        public static CGOrderOfferSubBLL Instance
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
            internal static readonly CGOrderOfferSubBLL instance = new CGOrderOfferSubBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGOrderOfferSub，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrderOfferSub">要添加的CGOrderOfferSub数据实体对象</param>
		public   int AddCGOrderOfferSub(CGOrderOfferSubEntity cGOrderOfferSub)
		{
			  if (cGOrderOfferSub.Id > 0)
            {
                return UpdateCGOrderOfferSub(cGOrderOfferSub);
            }
          
            else if (CGOrderOfferSubBLL.Instance.IsExist(cGOrderOfferSub))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGOrderOfferSubDA.Instance.AddCGOrderOfferSub(cGOrderOfferSub);
            }
	 	}

		/// <summary>
		/// 更新一条CGOrderOfferSub记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrderOfferSub">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGOrderOfferSub(CGOrderOfferSubEntity cGOrderOfferSub)
		{
			return CGOrderOfferSubDA.Instance.UpdateCGOrderOfferSub(cGOrderOfferSub);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderOfferSubByKey(int id)
        {
            return CGOrderOfferSubDA.Instance.DeleteCGOrderOfferSubByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderOfferSubDisabled()
        {
            return CGOrderOfferSubDA.Instance.DeleteCGOrderOfferSubDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderOfferSubByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGOrderOfferSubDA.Instance.DeleteCGOrderOfferSubByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderOfferSubByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderOfferSubDA.Instance.DisableCGOrderOfferSubByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGOrderOfferSub实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGOrderOfferSub实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGOrderOfferSubEntity GetCGOrderOfferSub(int id)
		{
			return CGOrderOfferSubDA.Instance.GetCGOrderOfferSub(id);			
		}
        public IList<VWCGOrderOfferSubEntity> GetVWCGOrderOfferSubS(int cgmemid, long orderCode)
        {
            IList<VWCGOrderOfferSubEntity> list= CGOrderOfferSubDA.Instance.GetVWCGOrderOfferSubS(cgmemid, orderCode);
            if(list!=null&& list.Count>0)
            {
                foreach(VWCGOrderOfferSubEntity entity in list)
                {
                    if (entity.Unit > 0)
                    {
                        entity.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(entity.Unit).Name;
                    }
                    else
                    {
                        entity.UnitName = "件";
                    }
                } 
            }
            return list;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGOrderOfferSubEntity> GetCGOrderOfferSubList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGOrderOfferSubDA.Instance.GetCGOrderOfferSubList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetCGOrderOfferSubAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGOrderOfferSubListKey";// SysCacheKey.CGOrderOfferSubListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGOrderOfferSubEntity> list = null;
                    list = CGOrderOfferSubDA.Instance.GetCGOrderOfferSubAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderOfferSubEntity cGOrderOfferSub)
        {
            return CGOrderOfferSubDA.Instance.ExistNum(cGOrderOfferSub)>0;
        }
		
	}
}

