using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ClassBrand的业务逻辑层。
创建时间：2016/10/31 13:53:20
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{

    /// <summary>
    /// 表ClassBrand的业务逻辑层。
    /// </summary>
    public class ClassBrandBLL
    {
        #region 实例化
        public static ClassBrandBLL Instance
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
            internal static readonly ClassBrandBLL instance = new ClassBrandBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表ClassBrand，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="classBrand">要添加的ClassBrand数据实体对象</param>
        public int AddClassBrand(ClassBrandEntity classBrand)
        {
            if (classBrand.Id > 0)
            {
                return UpdateClassBrand(classBrand);
            }

            else if (ClassBrandBLL.Instance.IsExist(classBrand))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ClassBrandDA.Instance.AddClassBrand(classBrand);
            }
        }

        /// <summary>
        /// 更新一条ClassBrand记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="classBrand">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateClassBrand(ClassBrandEntity classBrand)
        {
            if (ClassBrandBLL.Instance.IsExist(classBrand))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            return ClassBrandDA.Instance.UpdateClassBrand(classBrand);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassBrandByKey(int id)
        {
            return ClassBrandDA.Instance.DeleteClassBrandByKey(id);
        }


        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteClassBrandByBrandId(int brandid)
        {
            return ClassBrandDA.Instance.DeleteClassBrandByBrandId(brandid);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteClassBrandDisabled()
        {
            return ClassBrandDA.Instance.DeleteClassBrandDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteClassBrandByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassBrandDA.Instance.DeleteClassBrandByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableClassBrandByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ClassBrandDA.Instance.DisableClassBrandByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个ClassBrand实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ClassBrand实体</returns>
        /// <param name="columns">要返回的列</param>
        public ClassBrandEntity GetClassBrand(int id)
        {
            return ClassBrandDA.Instance.GetClassBrand(id);
        } 
        public IList<BrandEntity> GetBrandByClass(int classid,bool iscache=false)
        {
            IList<BrandEntity> _obj = new List<BrandEntity>();
            if (!iscache)
            {
                _obj = ClassBrandDA.Instance.GetBrandByClass(classid);

            }
            else
            {

                string _cachekey = "GetBrandByClass_" + classid.ToString()  ;
                object _objcache = MemCache.GetCache(_cachekey);//依据键获取值
                if (_objcache == null)
                {
                    _obj = ClassBrandDA.Instance.GetBrandByClass(classid );
                    MemCache.AddCache(_cachekey, _obj);
                }
                else
                {
                    _obj = (IList<BrandEntity>)_objcache;
                }
            }
            return _obj;
        }

        public BrandEntity  GetBrandByCB(int classid,int brandid)
        {

            BrandEntity _obj = ClassBrandDA.Instance.GetBrandByCB(classid, brandid);
              
            return _obj;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ClassBrandEntity> GetClassBrandList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _classid = 0;

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit item in wherelist)
                {
                    switch (item.FieldName)
                    {
                        case "ClassId":
                            {
                                _classid = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }

                    }
                }
            }

            return ClassBrandDA.Instance.GetClassBrandList(pageSize, pageIndex, ref recordCount, _classid);
        }

        

        public async Task GetClassBrandAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "ClassBrandListKey";// SysCacheKey.ClassBrandListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ClassBrandEntity> list = null;
                    list = ClassBrandDA.Instance.GetClassBrandAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ClassBrandEntity classBrand)
        {
            return ClassBrandDA.Instance.ExistNum(classBrand) > 0;
        }

    }
}

