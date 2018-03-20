using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ClassProDetails的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ClassProDetails的业务逻辑层。
	/// </summary>
	public class ClassProDetailsBLL
	{
	    #region 实例化
        public static ClassProDetailsBLL Instance
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
            internal static readonly ClassProDetailsBLL instance = new ClassProDetailsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ClassProDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classProDetails">要添加的ClassProDetails数据实体对象</param>
		public   int AddClassProDetails(ClassProDetailsEntity classProDetails)
		{
			if (classProDetails.Id > 0)
            {
                return UpdateClassProDetails(classProDetails);
            }
		    else if (string.IsNullOrEmpty(classProDetails.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	  
            else if (ClassProDetailsBLL.Instance.IsExist(classProDetails))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ClassProDetailsDA.Instance.AddClassProDetails(classProDetails);
            }
	 	}

		/// <summary>
		/// 更新一条ClassProDetails记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classProDetails">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateClassProDetails(ClassProDetailsEntity classProDetails)
		{
			return ClassProDetailsDA.Instance.UpdateClassProDetails(classProDetails);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassProDetailsByKey(int id)
        {
            return ClassProDetailsDA.Instance.DeleteClassProDetailsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassProDetailsDisabled()
        {
            return ClassProDetailsDA.Instance.DeleteClassProDetailsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassProDetailsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ClassProDetailsDA.Instance.DeleteClassProDetailsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassProDetailsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassProDetailsDA.Instance.DisableClassProDetailsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ClassProDetails实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ClassProDetails实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ClassProDetailsEntity GetClassProDetails(int id,bool cache=false)
		{     ClassProDetailsEntity list = null;
            if (cache)
            {
                string _cachekey = "GetClassProDetails_" + id;

                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = ClassProDetailsDA.Instance.GetClassProDetails(id);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (ClassProDetailsEntity)obj;
                }
            }
            else
            {
                list = ClassProDetailsDA.Instance.GetClassProDetails(id);

            }
            return list;			
		}

        /// <summary>
        /// 根据分类属性Id,属性值获取属性值Id,没有就新增一个，并返回对应的值Id
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="properyid"></param>
        /// <param name="prodetailname"></param>
        /// <returns></returns>
        public int GetAndAddPropertDetailId(int properyid,string prodetailname)
        {
            return ClassProDetailsDA.Instance.GetAndAddProPropertDetailId(properyid, prodetailname);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ClassProDetailsEntity> GetClassProDetailsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ClassProDetailsDA.Instance.GetClassProDetailsList(pageSize, pageIndex, ref recordCount);
        }
        public IList<ClassProDetailsEntity> GetListByPropertyId(int propertyid, int parentid, bool cache = true)
        {
            IList<ClassProDetailsEntity> list = null;
            if (cache)
            {
                string _cachekey = "ClassProDetailsList_" + propertyid + parentid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = ClassProDetailsDA.Instance.GetListByPropertyId(propertyid, parentid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<ClassProDetailsEntity>)obj;
                }
            }
            else
            {
                list = ClassProDetailsDA.Instance.GetListByPropertyId(propertyid, parentid);
            }

            return list;
        }
        public IList<ClassProDetailsEntity> GetProDetailsByClassId(int classid)
        {
            string _cachekey = "GetProDetailsByClassId_" + classid ;
            object obj = MemCache.GetCache(_cachekey);
            IList<ClassProDetailsEntity> list = null;
            if (obj == null)
            {
                list = ClassProDetailsDA.Instance.GetProDetailsByClassId(classid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<ClassProDetailsEntity>)obj;
            }
            return list;
        }
        public async Task GetClassProDetailsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ClassProDetailsListKey";// SysCacheKey.ClassProDetailsListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ClassProDetailsEntity> list = null;
                    list = ClassProDetailsDA.Instance.GetClassProDetailsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ClassProDetailsEntity classProDetails)
        {
            return ClassProDetailsDA.Instance.ExistNum(classProDetails)>0;
        }
		
	}
}

