using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ClassCGScope的业务逻辑层。
创建时间：2017/10/25 11:05:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ClassCGScope的业务逻辑层。
	/// </summary>
	public class ClassCGScopeBLL
	{
	    #region 实例化
        public static ClassCGScopeBLL Instance
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
            internal static readonly ClassCGScopeBLL instance = new ClassCGScopeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ClassCGScope，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classCGScope">要添加的ClassCGScope数据实体对象</param>
		public   int AddClassCGScope(ClassCGScopeEntity classCGScope)
		{
		    if (ClassCGScopeBLL.Instance.IsExist(classCGScope))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ClassCGScopeDA.Instance.AddClassCGScope(classCGScope);
            }
	 	}

		/// <summary>
		/// 更新一条ClassCGScope记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="classCGScope">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateClassCGScope(ClassCGScopeEntity classCGScope)
		{
			return ClassCGScopeDA.Instance.UpdateClassCGScope(classCGScope);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassCGScopeByKey(int id)
        {
            return ClassCGScopeDA.Instance.DeleteClassCGScopeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassCGScopeDisabled()
        {
            return ClassCGScopeDA.Instance.DeleteClassCGScopeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassCGScopeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ClassCGScopeDA.Instance.DeleteClassCGScopeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassCGScopeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassCGScopeDA.Instance.DisableClassCGScopeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ClassCGScope实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ClassCGScope实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ClassCGScopeEntity GetClassCGScope(int id)
		{
			return ClassCGScopeDA.Instance.GetClassCGScope(id);			
		}
        public ClassCGScopeEntity GetClassCGScopeByName(string name)
        {
            return ClassCGScopeDA.Instance.GetClassCGScopeByName(name);
        }
        public ClassCGScopeEntity GetParentByScopeName(string name)
        {
            return ClassCGScopeDA.Instance.GetParentByScopeName(name);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ClassCGScopeEntity> GetClassCGScopeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ClassCGScopeDA.Instance.GetClassCGScopeList(pageSize, pageIndex, ref recordCount);
        }
        public IList<VWClassCGScopeEntity> GetClassCGScopeTree()
        {
            IList<VWClassCGScopeEntity> result = new List<VWClassCGScopeEntity>();

            string _cachekey = "GetClassCGScopeTree";// SysCacheKey.ClassCGScopeListKey;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                Dictionary<int, IList<VWClassCGScopeEntity>> diclist = new Dictionary<int, IList<VWClassCGScopeEntity>>();
                IList<VWClassCGScopeEntity> list = GetClassCGScopeAll();
                if (list != null && list.Count > 0)
                {
                    foreach (VWClassCGScopeEntity entity in list)
                    {
                        if (entity.ParentId == 0)
                        {
                            if (!diclist.ContainsKey(entity.Id))
                            {
                                diclist.Add(entity.Id, new List<VWClassCGScopeEntity>());
                            }
                            entity.Children = diclist[entity.Id];
                            result.Add(entity);
                        }
                        else
                        {
                            if (!diclist.ContainsKey(entity.ParentId))
                            {
                                diclist.Add(entity.ParentId, new List<VWClassCGScopeEntity>());
                            }
                            diclist[entity.ParentId].Add(entity);
                        }
                    }
                }
                MemCache.AddCache(_cachekey, result);
            }
            else
            {
                result = (IList<VWClassCGScopeEntity>)obj;
            }
         
     
            return result;
        }
        /// <summary>
        /// 获取所有产品范围
        /// </summary>
        /// <param name="isroot">是否标准产品</param>
        /// <returns></returns>

        public IList<VWClassCGScopeEntity> GetClassCGScopeAll(int scopetype=(int)ScopeTypeEnum.Normal, int isroot=1,int isactive=1)
        {
           IList<VWClassCGScopeEntity> list = null;
        string _cachekey = "GetClassCGScopeAll_"+ scopetype+"_"+ isroot +"_"+ isactive;// SysCacheKey.ClassCGScopeListKey;
        object obj = MemCache.GetCache(_cachekey);
        if (obj == null)
        { 
            list = ClassCGScopeDA.Instance.GetClassCGScopeAll(scopetype,isroot, isactive);
            MemCache.AddCache(_cachekey, list);
        }
            else
            {
                list = (IList<VWClassCGScopeEntity>)obj;
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ClassCGScopeEntity classCGScope)
        {
            return ClassCGScopeDA.Instance.ExistNum(classCGScope)>0;
        }
		
	}
}

