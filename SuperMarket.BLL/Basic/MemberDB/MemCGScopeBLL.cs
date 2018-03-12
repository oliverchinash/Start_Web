using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.CatograyDB;

/*****************************************
功能描述：表MemCGScope的业务逻辑层。
创建时间：2017/9/22 15:02:41
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemCGScope的业务逻辑层。
	/// </summary>
	public class MemCGScopeBLL
	{
	    #region 实例化
        public static MemCGScopeBLL Instance
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
            internal static readonly MemCGScopeBLL instance = new MemCGScopeBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表MemCGScope，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="memCGScope">要添加的MemCGScope数据实体对象</param>
        public int AddMemCGScope(MemCGScopeEntity memCGScope)
        {
            if(memCGScope.ScopeType==(int)ScopeTypeEnum.Car)
            {
                CarTypeBrandEntity cartype = CarTypeBrandBLL.Instance.GetParentByName(memCGScope.BrandName);
                if(cartype!=null&& cartype.Id>0)
                {
                    if(cartype.BrandName== memCGScope.BrandName)
                    {
                        memCGScope.BrandId = cartype.Id;
                    }
                    else
                    {
                        MemCGScopeEntity parentcgscope = new MemCGScopeEntity();
                        parentcgscope.BrandName = cartype.BrandName;
                        parentcgscope.CGMemId = memCGScope.CGMemId;
                        parentcgscope.ClassName = memCGScope.ClassName;
                        if (MemCGScopeBLL.Instance.IsExist(parentcgscope))
                        {
                            return (int)CommonStatus.ADD_Fail_Exist;
                        }
                    }
                }
            }
            else if (memCGScope.ScopeType == (int)ScopeTypeEnum.Normal) 
            {
                ClassCGScopeEntity classscope = ClassCGScopeBLL.Instance.GetParentByScopeName(memCGScope.BrandName);
                if (classscope != null && classscope.Id > 0)
                {
                    if (classscope.ScopeClassName == memCGScope.BrandName)
                    {
                        memCGScope.BrandId = classscope.Id;
                    }
                    else
                    {
                        MemCGScopeEntity parentcgscope = new MemCGScopeEntity();
                        parentcgscope.BrandName = classscope.ScopeClassName;
                        parentcgscope.CGMemId = memCGScope.CGMemId;
                        parentcgscope.ClassName = memCGScope.ClassName;
                        if (MemCGScopeBLL.Instance.IsExist(parentcgscope))
                        {
                            return (int)CommonStatus.ADD_Fail_Exist;
                        }
                    }
                }
            }
            if (MemCGScopeBLL.Instance.IsExist(memCGScope))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemCGScopeDA.Instance.AddMemCGScope(memCGScope);
            }
        }

		/// <summary>
		/// 更新一条MemCGScope记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memCGScope">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemCGScope(MemCGScopeEntity memCGScope)
		{
			return MemCGScopeDA.Instance.UpdateMemCGScope(memCGScope);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemCGScopeByKey(int id,int cgmemid)
        {
            return MemCGScopeDA.Instance.DeleteMemCGScopeByKey(id, cgmemid);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemCGScopeDisabled()
        {
            return MemCGScopeDA.Instance.DeleteMemCGScopeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemCGScopeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemCGScopeDA.Instance.DeleteMemCGScopeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemCGScopeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemCGScopeDA.Instance.DisableMemCGScopeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemCGScope实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemCGScope实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemCGScopeEntity GetMemCGScope(int id)
		{
			return MemCGScopeDA.Instance.GetMemCGScope(id);			
		}
        public string GetCGMemIdsByCarBrand(int scopetype,string carbrandname,string oldmemids)
        {
            if (scopetype == (int)ScopeTypeEnum.Normal)
            {
                ClassCGScopeEntity scopeparent = ClassCGScopeBLL.Instance.GetParentByScopeName(carbrandname);
                if (scopeparent != null && scopeparent.Id > 0 && !string.IsNullOrEmpty(scopeparent.ScopeClassName))
                {
                    carbrandname += "|" + scopeparent.ScopeClassName;
                }
            }
            else
            {
                CarTypeBrandEntity carbrandparent = CarTypeBrandBLL.Instance.GetParentByName(carbrandname);
                if (carbrandparent != null && carbrandparent.Id > 0 && !string.IsNullOrEmpty(carbrandparent.BrandName))
                {
                    carbrandname += "|" + carbrandparent.BrandName;
                }
            }
            string memids= MemCGScopeDA.Instance.GetCGMemIdsByCarBrand(carbrandname, oldmemids);
            return memids.Trim(',');
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemCGScopeEntity> GetMemCGScopeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemCGScopeDA.Instance.GetMemCGScopeList(pageSize, pageIndex, ref recordCount);
        }

        public IList<MemCGScopeEntity> GetMemCGScopeAllByMemId(int memid,bool cache=false)
        {  IList<MemCGScopeEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetMemCGScopeAllByMemId_" + memid;// SysCacheKey.MemCGScopeListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {

                    list = MemCGScopeDA.Instance.GetMemCGScopeAllByMemId(memid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<MemCGScopeEntity>)obj;
                }
            }
            else
            {
                list = MemCGScopeDA.Instance.GetMemCGScopeAllByMemId(memid);
            }

            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemCGScopeEntity memCGScope)
        {
            return MemCGScopeDA.Instance.ExistNum(memCGScope)>0;
        }
		
	}
}

