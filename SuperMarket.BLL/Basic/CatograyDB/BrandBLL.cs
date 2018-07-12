using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;
using System.Collections;

/*****************************************
功能描述：表Brand的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{

    /// <summary>
    /// 表Brand的业务逻辑层。
    /// </summary>
    public class BrandBLL
    {
        #region 实例化
        public static BrandBLL Instance
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
            internal static readonly BrandBLL instance = new BrandBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表Brand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="brand">要添加的Brand数据实体对象</param>
        public int AddBrand(BrandEntity brand)
        {
            if (brand.Id > 0)
            {
                return UpdateBrand(brand);
            }
            else if (string.IsNullOrEmpty(brand.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            } 
            else if (BrandBLL.Instance.IsExist(brand))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return BrandDA.Instance.AddBrand(brand);
            }
        }

        /// <summary>
        /// 更新一条Brand记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="brand">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateBrand(BrandEntity brand)
        {
            return BrandDA.Instance.UpdateBrand(brand);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteBrandByKey(int id)
        {
            return BrandDA.Instance.DeleteBrandByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteBrandDisabled()
        {
            return BrandDA.Instance.DeleteBrandDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteBrandByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return BrandDA.Instance.DeleteBrandByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableBrandByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return BrandDA.Instance.DisableBrandByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个Brand实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>Brand实体</returns>
        /// <param name="columns">要返回的列</param>
        public BrandEntity GetBrand(int id, bool iscache=false)
        {
            if (iscache)
            {
                string _cachekey = "Brand_" + id.ToString();
                object _objcache = MemCache.GetCache(_cachekey);
                BrandEntity _obj = new BrandEntity();
                if (_objcache == null)
                {
                    _obj = BrandDA.Instance.GetBrand(id);
                    MemCache.AddCache(_cachekey, _obj);
                }
                else
                {
                    _obj = (BrandEntity)_objcache;
                }
                return _obj;
            }
            else if (iscache == false)
            {
                return BrandDA.Instance.GetBrand(id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据主键获取一个Brand实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>Brand实体</returns>
        /// <param name="columns">要返回的列</param>
        public BrandEntity GetBrandByName(string name)
        {


            BrandEntity _obj = BrandDA.Instance.GetBrandByName(name);
                
                return _obj;
             
        }

        public IList<BrandEntity> GetBrandByKey(int classid, int pid, string key)
        {
            string classidstr = "";
            if (classid > 0)
            {
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, false);

                IList<int> classintlist = new List<int>();
                if (_classentity.RedirectClassId > 0)
                {
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(_classentity.RedirectClassId);
                }
                else
                {
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(classid);
                }
                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
                else
                {
                    classidstr = StringUtils.GetDbString(classid);
                }
            }
            string _cachekey = "GetBrandByKey_" + classid.ToString() + "_" + pid.ToString() + "_" + key;
            object _objcache = MemCache.GetCache(_cachekey);//依据键获取值
            IList<BrandEntity> _obj = new List<BrandEntity>();
            if (_objcache == null)
            {
                _obj = BrandDA.Instance.GetBrandByKey(classidstr, pid, key);
                MemCache.AddCache(_cachekey, _obj);

            }
            else
            {
                _obj = (IList<BrandEntity>)_objcache;
            }
            return _obj;
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<BrandEntity> GetBrandList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _ishot = -1;
            string _name = string.Empty;

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit item in wherelist)
                {
                    switch (item.FieldName)
                    {
                        case "IsHot":
                            {
                                _ishot = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                        case "Name":
                            {
                                _name = StringUtils.GetDbString(item.CompareValue);
                                break;
                            }

                    }
                }
            }
            return BrandDA.Instance.GetBrandList(pageSize, pageIndex, ref recordCount, _ishot,_name);
        }

        public IList<BrandEntity> GetBrandListByBrand(string brand)
        {
            return BrandDA.Instance.GetBrandListByBrand(brand);
        }
        /// <summary>
        /// 获取品牌，并按照首字母建立层级结构
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public IList<VWTreeBrandEntity> GetTreeBrandAll(int classid,bool iscache=false)
        {
            IList<VWTreeBrandEntity> resultlist = new List<VWTreeBrandEntity>();
           
            string _cachekey = "GetTreeBrandAll"+ classid;
            if (iscache)
            {
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    int rediclassid = classid;
                    ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, false);
                    if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
                    IList<int> classintlist = new List<int>();
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(rediclassid);
                    string classidstr = "";
                    if (classintlist != null && classintlist.Count > 0)
                    {
                        classidstr = string.Join("_", classintlist);
                    }
                    else
                    {
                        classidstr = StringUtils.GetDbString(classid);
                    }

                    IList<VWTreeBrandEntity> list = null;
                    list = BrandDA.Instance.GetBrandAllByClassStr(classidstr);
                    if (list != null && list.Count > 0)
                    {
                        //string[] firstletter = new Array();
                        ArrayList firstletter = new ArrayList();
                        foreach (VWTreeBrandEntity entity in list)
                        {
                            if (!firstletter.Contains(entity.PYFirst))
                            {
                                firstletter.Add(entity.PYFirst);
                            }
                            firstletter.Sort();
                        }
                        foreach (string letter in firstletter)
                        {
                            VWTreeBrandEntity entity = new VWTreeBrandEntity();
                            entity.PYFirst = letter;
                            resultlist.Add(entity);
                        }
                        foreach (VWTreeBrandEntity entity in list)
                        {
                            foreach (VWTreeBrandEntity entity2 in resultlist)
                            {
                                if (entity.PYFirst == entity2.PYFirst)
                                {
                                    if (entity2.Children == null)
                                    {
                                        entity2.Children = new List<VWTreeBrandEntity>();
                                    }
                                    entity2.Children.Add(entity);
                                    break;
                                }

                            }


                        }

                    }
                    MemCache.AddCache(_cachekey, resultlist);
                }
                else
                {
                    resultlist = (IList<VWTreeBrandEntity>)obj;
                }
            }
            else
            {
                int rediclassid = classid;
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, false);
                if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
                IList<int> classintlist = new List<int>();
                classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(rediclassid);
                string classidstr = "";
                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
                else
                {
                    classidstr = StringUtils.GetDbString(classid);
                }

                IList<VWTreeBrandEntity> list = null;
                list = BrandDA.Instance.GetBrandAllByClassStr(classidstr);
                if (list != null && list.Count > 0)
                {
                    //string[] firstletter = new Array();
                    ArrayList firstletter = new ArrayList();
                    foreach (VWTreeBrandEntity entity in list)
                    {
                        if (!firstletter.Contains(entity.PYFirst))
                        {
                            firstletter.Add(entity.PYFirst);
                        }
                        firstletter.Sort();
                    }
                    foreach (string letter in firstletter)
                    {
                        VWTreeBrandEntity entity = new VWTreeBrandEntity();
                        entity.PYFirst = letter;
                        resultlist.Add(entity);
                    }
                    foreach (VWTreeBrandEntity entity in list)
                    {
                        foreach (VWTreeBrandEntity entity2 in resultlist)
                        {
                            if (entity.PYFirst == entity2.PYFirst)
                            {
                                if (entity2.Children == null)
                                {
                                    entity2.Children = new List<VWTreeBrandEntity>();
                                }
                                entity2.Children.Add(entity);
                                break;
                            }

                        }


                    }

                }
            }
            return resultlist;
        }

        

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(BrandEntity brand)
        {
            return BrandDA.Instance.ExistNum(brand) > 0;
        }

    }
}

