using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using System.Linq;

/*****************************************
功能描述：表ClassProperties的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ClassProperties的业务逻辑层。
	/// </summary>
	public class ClassPropertiesBLL
	{
	    #region 实例化
        public static ClassPropertiesBLL Instance
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
            internal static readonly ClassPropertiesBLL instance = new ClassPropertiesBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ClassProperties，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classProperties">要添加的ClassProperties数据实体对象</param>
		public   int AddClassProperties(ClassPropertiesEntity classProperties)
		{
			  if (classProperties.Id > 0)
            {
                return UpdateClassProperties(classProperties);
            }
		    else if (string.IsNullOrEmpty(classProperties.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ClassPropertiesBLL.Instance.IsExist(classProperties))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ClassPropertiesDA.Instance.AddClassProperties(classProperties);
            }
	 	}

		/// <summary>
		/// 更新一条ClassProperties记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classProperties">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateClassProperties(ClassPropertiesEntity classProperties)
		{
			return ClassPropertiesDA.Instance.UpdateClassProperties(classProperties);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassPropertiesByKey(int id)
        {
            return ClassPropertiesDA.Instance.DeleteClassPropertiesByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassPropertiesDisabled()
        {
            return ClassPropertiesDA.Instance.DeleteClassPropertiesDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassPropertiesByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ClassPropertiesDA.Instance.DeleteClassPropertiesByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassPropertiesByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassPropertiesDA.Instance.DisableClassPropertiesByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个ClassProperties实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ClassProperties实体</returns>
        /// <param name="columns">要返回的列</param>
        public ClassPropertiesEntity GetClassProperties(int id,bool cache=false)
        {
            ClassPropertiesEntity list = null;
            if (cache)
            {
                string _cachekey = "GetClassProperties_" + id;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = ClassPropertiesDA.Instance.GetClassProperties(id);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (ClassPropertiesEntity)obj;
                }
            }
            else
            {
                list = ClassPropertiesDA.Instance.GetClassProperties(id);
            }
            return list;
        } 
        public int ProcBindProperties(int classid, string propertiesstr)
        {
            return ClassPropertiesDA.Instance.ProcBindProperties(classid,  propertiesstr);

        }
        public int ProcGetProperties(int classid, string propertiesstr)
        {
            return ClassPropertiesDA.Instance.ProcGetProperties(classid, propertiesstr);

        }
        /// <summary>
        /// 根据分类Id 和属性名称获取对应属性iD
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="propertiesstr"></param>
        /// <returns></returns>
        public int GetPropertiesId(int classid, string propertiesstr)
        {
            return ClassPropertiesDA.Instance.GetPropertiesId(classid, propertiesstr); 
        }
        public int BindProperties(int classid, string propertname,int sort)
        {
            return ClassPropertiesDA.Instance.BindProperties(classid, propertname,sort);

        }
        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ClassPropertiesEntity> GetClassPropertiesList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ClassPropertiesDA.Instance.GetClassPropertiesList(pageSize, pageIndex, ref recordCount);
        }    
        /// <summary>
               /// 根据分类id 获取对应的分类产品属性
               /// </summary>
               /// <param name="classid"></param>
               /// <param name="pid"></param>
               /// <returns></returns> 
        public IList<ClassPropertiesEntity> GetListByClassId(int classid,int parentid,bool cache=false)
        {
            IList<ClassPropertiesEntity> _objlistall = null;
            if (cache)
            {
                string _cachekey = "ClassPropertiesList_" + classid + "_" + parentid;// SysCacheKey.VWClassPropertiesListKey;
                object _objcache = MemCache.GetCache(_cachekey);
                if (_objcache == null)
                {
                    _objlistall = ClassPropertiesDA.Instance.GetListByClassId(classid, parentid);
                }
                else
                {
                    _objlistall = (List<ClassPropertiesEntity>)_objcache;
                }
                MemCache.AddCache(_cachekey, _objlistall);
            }
            else
            {
                _objlistall = ClassPropertiesDA.Instance.GetListByClassId(classid, parentid);

            }
            return _objlistall; 
        }

        /// <summary>
        /// 根据分类id 获取对应的分类产品属性
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="pid"></param>
        /// <returns></returns> 
        public IList<ClassPropertiesEntity> GetPropertiesByClassId(int classid, bool cache=false )
        {
            IList<ClassPropertiesEntity> list= GetListByClassId(classid, 0, cache);
            var objlist = list.Where(p => p.IsSpec == 0);
            if(objlist!=null)
            {
                return objlist.ToList<ClassPropertiesEntity>();
            }
            return null;
        }
        public async Task GetClassPropertiesAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ClassPropertiesListKey";// SysCacheKey.ClassPropertiesListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ClassPropertiesEntity> list = null;
                    list = ClassPropertiesDA.Instance.GetClassPropertiesAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ClassPropertiesEntity classProperties)
        {
            return ClassPropertiesDA.Instance.ExistNum(classProperties)>0;
        }
		
	}
}

