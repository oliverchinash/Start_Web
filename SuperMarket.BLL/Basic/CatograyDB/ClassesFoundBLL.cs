﻿using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ClassesFound的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{

    /// <summary>
    /// 表ClassesFound的业务逻辑层。
    /// </summary>
    public class ClassesFoundBLL
    {
        #region 实例化
        public static ClassesFoundBLL Instance
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
            internal static readonly ClassesFoundBLL instance = new ClassesFoundBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表ClassesFound，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="classesFound">要添加的ClassesFound数据实体对象</param>
        public int AddClassesFound(ClassesFoundEntity classesFound)
        {
            if (classesFound.Id > 0)
            {
                return UpdateClassesFound(classesFound);
            }
            else if (string.IsNullOrEmpty(classesFound.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }

            else if (ClassesFoundBLL.Instance.IsExist(classesFound))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ClassesFoundDA.Instance.AddClassesFound(classesFound);
            }
        }

        /// <summary>
        /// 更新一条ClassesFound记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="classesFound">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateClassesFound(ClassesFoundEntity classesFound)
        {
            return ClassesFoundDA.Instance.UpdateClassesFound(classesFound);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassesFoundByKey(int id)
        {
            return ClassesFoundDA.Instance.DeleteClassesFoundByKey(id);
        }



        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassesFoundByParentId(int parentid)
        {
            return ClassesFoundDA.Instance.DeleteClassesFoundByParentId(parentid);
        }

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassesFoundDisabled()
        {
            return ClassesFoundDA.Instance.DeleteClassesFoundDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassesFoundByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassesFoundDA.Instance.DeleteClassesFoundByIds(idstr);
        }

        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassesFoundByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassesFoundDA.Instance.DisableClassesFoundByIds(idstr);
        }

        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int EnableClassesFoundByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassesFoundDA.Instance.EnableClassesFoundByIds(idstr);
        }

        /// <summary>
        /// 根据主键获取一个ClassesFound实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ClassesFound实体</returns>
        /// <param name="columns">要返回的列</param>
        public ClassesFoundEntity GetClassesFound(int id, bool iscache = false)
        {
            if (iscache)
            {
                string _cachekey = "ClassesFound_" + id.ToString();// SysCacheKey.ClassesFoundListKey;
                object _objcache = MemCache.GetCache(_cachekey);
                ClassesFoundEntity _obj = new ClassesFoundEntity();
                if (_objcache == null)
                {
                    _obj = ClassesFoundDA.Instance.GetClassesFound(id);
                    MemCache.AddCache(_cachekey, _obj);

                }
                else
                {
                    _obj = (ClassesFoundEntity)_objcache;
                }
                return _obj;
            }
            else if (iscache == false)
            {
                return ClassesFoundDA.Instance.GetClassesFound(id);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 更新产品信息的时候根据分类名称获取分类所属信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="classtype"></param>
        /// <param name="classmenutype"></param>
        /// <param name="isend"></param>
        /// <returns></returns>
        public ClassesFoundEntity GetClassesFoundByName(string name, int classtype,int classmenutype=(int)ClassMenuTypeEnum.Default,int isend=1)
        {


            ClassesFoundEntity _obj = ClassesFoundDA.Instance.GetClassesFoundByName(name, classtype, classmenutype, isend);

            return _obj;

        }
        public IList<ClassesFoundEntity> GetClassListByLevel(int pid, int level, int siteid )
        {
            string _cachekey = "GetClassListByLevel_" + pid.ToString() + "_" + level.ToString() + "_" + siteid.ToString() ;// SysCacheKey.ClassesFoundListKey;
            object _objcache = MemCache.GetCache(_cachekey);
            IList<ClassesFoundEntity> _objlistall = new List<ClassesFoundEntity>();
            _objlistall = ClassesFoundDA.Instance.GetClassesAllByPId(pid, level, siteid );
            MemCache.AddCache(_cachekey, _objlistall);
            return _objlistall;
        }
        public  List<int> GetSubClassEndList(int classid,bool cache = false)
        {
            List<int> _listint = new List<int>();
            if (cache == false)
            {
                ClassesFoundEntity _entity = GetClassesFound(classid, cache);
                int redirectclassid = _entity.RedirectClassId;
                if (redirectclassid == 0) redirectclassid = _entity.Id;
                _listint.Add(redirectclassid);
                if (_entity.IsEnd == 0 && redirectclassid > 0)
                {
                    IList<ClassesFoundEntity> _entitylist2 = GetClassesAllByPId(redirectclassid, cache, -1 );
                    if (_entitylist2 != null && _entitylist2.Count > 0)
                    {
                        foreach (ClassesFoundEntity _entity2 in _entitylist2)
                        {
                            List<int> listsub = GetSubClassEndList(_entity2.Id, cache);
                            _listint.AddRange(listsub);
                        }
                    }
                }
            }
            else
            {
                string _cachekey = "GetSubClassEndList_" + classid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    ClassesFoundEntity _entity = GetClassesFound(classid, false);
                    int redirectclassid = _entity.RedirectClassId;
                    if (redirectclassid == 0) redirectclassid = _entity.Id;
                    _listint.Add(redirectclassid);
                    if (_entity.IsEnd == 0 && redirectclassid > 0)
                    {
                        IList<ClassesFoundEntity> _entitylist2 = GetClassesAllByPId(redirectclassid, false, -1 );
                        if (_entitylist2 != null && _entitylist2.Count > 0)
                        {
                            foreach (ClassesFoundEntity _entity2 in _entitylist2)
                            {
                                List<int> listsub = GetSubClassEndList(_entity2.Id);
                                _listint.AddRange(listsub);
                            }
                        }
                    }

                }
                else
                {
                    _listint = (List<int>)obj;
                }
            }

            return _listint;
        }

        public   List<int> GetSubClassEndListBySite(int siteid,bool iscache=false)
        {
            List<int> _listint = new List<int>();
            if (iscache)
            {
                string _cachekey = "GetSubClassEndListBySite_" + siteid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ClassesFoundEntity> _entitylist2 = GetClassesAllByPId(0, false, siteid);
                    if (_entitylist2 != null && _entitylist2.Count > 0)
                    {
                        foreach (ClassesFoundEntity entity in _entitylist2)
                        {
                            List<int> listsub = GetSubClassEndList(entity.Id);
                            _listint.AddRange(listsub);
                        }
                        MemCache.AddCache(_cachekey, _listint);
                    }
                }
                else
                {
                    _listint = (List<int>)obj;
                }
            }
            else
            {
                IList<ClassesFoundEntity> _entitylist2 = GetClassesAllByPId(0, false, siteid);
                if (_entitylist2 != null && _entitylist2.Count > 0)
                {
                    foreach (ClassesFoundEntity entity in _entitylist2)
                    {
                        List<int> listsub = GetSubClassEndList(entity.Id);
                        _listint.AddRange(listsub);
                    }
                }
            }
            return _listint;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ClassesFoundEntity> GetClassesFoundList(int pageSize, int pageIndex, ref int recordCount, int level=0, string name = "", int parentid = -1, int isactive = -1, int classtype = -1, int classmenutype = 1, int siteid = 1)
        {
           
            return ClassesFoundDA.Instance.GetClassesFoundList(pageSize, pageIndex, ref recordCount,  level,  name,  parentid,  isactive,  classtype,  classmenutype,  siteid);
        }
       /// <summary>
       /// 货物分类导航
       /// </summary>
       /// <param name="siteid"></param>
       /// <param name="classnenutype"></param>
       /// <param name="parentid"></param>
       /// <param name="iscache"></param>
       /// <returns></returns>
        public IList<VWClassesFoundEntity> GetClassMenuAll(int siteid,  int parentid, bool iscache=false)
        {
            IList<VWClassesFoundEntity> list = new List<VWClassesFoundEntity>();
            if (iscache)
            {
                string _cachekey = "GetClassMenuAll_" + siteid + "_" + parentid;
                object obj = MemCache.GetCache(_cachekey);

                if (obj == null)
                {
                    IList<VWClassesFoundEntity> listtemp = null;
                    listtemp = ClassesFoundDA.Instance.GetClassMenuAll(siteid, parentid);
                    if (listtemp != null && listtemp.Count > 0)
                    {
                        Dictionary<int, VWClassesFoundEntity> IDCTEMP = new Dictionary<int, VWClassesFoundEntity>();
                        for  (int i=0;i<  listtemp.Count ; i++)
                        {
                            IDCTEMP.Add(listtemp[i].Id, listtemp[i]);
                        }
                        foreach (int key in IDCTEMP.Keys)
                        {
                            if (IDCTEMP[key].ParentId == 0)
                            {
                                list.Add(IDCTEMP[key]);
                            }
                            else
                            {
                                if (IDCTEMP[IDCTEMP[key].ParentId].Children == null) IDCTEMP[IDCTEMP[key].ParentId].Children = new List<VWClassesFoundEntity>();
                                IDCTEMP[IDCTEMP[key].ParentId].Children.Add(IDCTEMP[key]);
                            }
                        }
                        if (list != null && list.Count > 0)
                        {
                            foreach (VWClassesFoundEntity classentity in list)
                            {
                                classentity.MenuBrands = ClassesMenuADBLL.Instance.GetBrandsByClassId(classentity.Id);
                                classentity.MenuProducts = ClassesMenuADBLL.Instance.GetProductsByClassId(classentity.Id);
                            }
                        }
                    }
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<VWClassesFoundEntity>)obj;
                } 
            }
            else
            {
                IList<VWClassesFoundEntity> listtemp = null;
                listtemp = ClassesFoundDA.Instance.GetClassMenuAll(siteid,   parentid);
                if (listtemp != null && listtemp.Count > 0)
                {
                    Dictionary<int, VWClassesFoundEntity> IDCTEMP = new Dictionary<int, VWClassesFoundEntity>();
                    for (int i = 0; i < listtemp.Count; i++)
                    {

                        IDCTEMP.Add(listtemp[i].Id, listtemp[i]);
                    }
                    foreach (int key in IDCTEMP.Keys)
                    {
                        if (IDCTEMP[key].ParentId == 0)
                        {
                            list.Add(IDCTEMP[key]);
                        }
                        else
                        {
                            if (IDCTEMP[IDCTEMP[key].ParentId].Children == null) IDCTEMP[IDCTEMP[key].ParentId].Children = new List<VWClassesFoundEntity>();
                            IDCTEMP[IDCTEMP[key].ParentId].Children.Add(IDCTEMP[key]);
                        }
                    }
                    if (list != null && list.Count > 0)
                    {
                        foreach (VWClassesFoundEntity classentity in list)
                        {
                            classentity.MenuBrands = ClassesMenuADBLL.Instance.GetBrandsByClassId(classentity.Id);
                            classentity.MenuProducts = ClassesMenuADBLL.Instance.GetProductsByClassId(classentity.Id);
                        }
                    }
                } 
            }
           
            return list;
        }

        /// <summary>
        /// 仅仅获取分类
        /// </summary>
        /// <param name="siteid"></param>
        /// <param name="classnenutype"></param>
        /// <param name="parentid"></param>
        /// <param name="iscache"></param>
        /// <returns></returns>
        public IList<VWClassesFoundEntity> GetClassFoundAll(int siteid,  int parentid , bool iscache=false)
        {
            IList<VWClassesFoundEntity> list = new List<VWClassesFoundEntity>();
            if (iscache == false)
            {
                string _cachekey = "GetClassFoundAll_" + siteid + "_" + parentid ;
                object obj = MemCache.GetCache(_cachekey);

                if (obj == null)
                {
                    list = ClassesFoundDA.Instance.GetClassMenuAll(siteid,   parentid );
                   
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<VWClassesFoundEntity>)obj;
                }
            }
            else
            {
                list = ClassesFoundDA.Instance.GetClassMenuAll(siteid,   parentid ); 
            } 
            return list;
        }

        public IList<ClassesFoundEntity> GetClassesAllByPId(int parentid, bool iscache,int siteid)
        {
            if (iscache )
            {
                string _cachekey = "GetClassesAllByPId_" + parentid+"_"+ siteid ;
                object obj = MemCache.GetCache(_cachekey);
                IList<ClassesFoundEntity> list = null;
                if (obj == null)
                {
                    list = ClassesFoundDA.Instance.GetClassesAllByPId(parentid, -1, siteid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<ClassesFoundEntity>)obj;
                }
                return list;
            }
            else
            {
                IList<ClassesFoundEntity> list = null;
                list = ClassesFoundDA.Instance.GetClassesAllByPId(parentid, -1, siteid );
                return list;
            }

        }

        public IList<ClassesFoundEntity> GetClassesAllByBrandId(int siteid, int brandid,bool iscache)
        {
            if (iscache == false)
            {
                string _cachekey = "GetClassesAllByBrandId_" + siteid + "_" + brandid ;
                object obj = MemCache.GetCache(_cachekey);
                IList<ClassesFoundEntity> list = null;
                if (obj == null)
                {
                    list = ClassesFoundDA.Instance.GetClassesAllByBrandId(siteid,brandid);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<ClassesFoundEntity>)obj;
                }
                return list;
            }
            else
            {
                IList<ClassesFoundEntity> list = null;
                list = ClassesFoundDA.Instance.GetClassesAllByBrandId(siteid, brandid);
                return list;
            }
        }

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ClassesFoundEntity classesFound)
        {
            return ClassesFoundDA.Instance.ExistNum(classesFound) > 0;
        }

    }
}

