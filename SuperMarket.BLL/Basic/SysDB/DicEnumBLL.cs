using System;
using System.Data;
using System.Linq;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB; 
using SuperMarket.Core.Util;
using SuperMarket.Core;
using SuperMarket.Model.Cache;
using System.Threading.Tasks;
using SuperMarket.Model.Common;

/*****************************************
功能描述：表DicEnum的业务逻辑层。
创建时间：2016/7/14 17:40:33
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{

    /// <summary>
    /// 表DicEnum的业务逻辑层。
    /// </summary>
    public class DicEnumBLL
    {
        #region 实例化
        public static DicEnumBLL Instance
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
            internal static readonly DicEnumBLL instance = new DicEnumBLL();
        }
        #endregion
        #region  自动产生
        /// <summary>
        /// 插入一条记录到表DicEnum，返回操作数。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="dicEnum">要添加的DicEnum数据实体对象</param>
        public int AddDicEnum(DicEnumEntity dicEnum)
        {
            if (dicEnum.Id > 0)
            {
                return UpdateDicEnum(dicEnum);
            }
            else if (string.IsNullOrEmpty(dicEnum.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }
            else if (DicEnumBLL.Instance.IsExist(dicEnum))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return DicEnumDA.Instance.AddDicEnum(dicEnum);
            }
        }

        /// <summary>
        /// 更新一条DicEnum记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="dicEnum">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateDicEnum(DicEnumEntity dicEnum)
        {
            if (string.IsNullOrEmpty(dicEnum.Name) || dicEnum.Id == 0)
            {
                return (int)CommonStatus.Update_Fail_Empty;
            }
            else if (DicEnumBLL.Instance.IsExist(dicEnum))
            {
                return (int)CommonStatus.Update_Fail_Exist;
            }
            return DicEnumDA.Instance.UpdateDicEnum(dicEnum);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDicEnumByKey(int id)
        {
            return DicEnumDA.Instance.DeleteDicEnumByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDicEnumDisabled()
        {
            return DicEnumDA.Instance.DeleteDicEnumDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDicEnumByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return DicEnumDA.Instance.DeleteDicEnumByIds(idstr); 
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDicEnumByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return DicEnumDA.Instance.DisableDicEnumByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个DicEnum实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>DicEnum实体</returns>
        /// <param name="columns">要返回的列</param>
        public DicEnumEntity GetDicEnum(int id)
        {
            string _cachekey = SysCacheKey.DicEnumListKey;
            object _objcache = MemCache.GetCache(_cachekey);
            List<DicEnumEntity> _objlistall = null;//总数 
            if (_objcache != null)
            {
                _objlistall = (List<DicEnumEntity>)_objcache;
                var templist = from c in _objlistall
                               where c.Id==id 
                               select c;
                return (DicEnumEntity)templist;
            }
            return DicEnumDA.Instance.GetDicEnum(id);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public IList<DicEnumEntity> GetDicEnumList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _keyword = "";
            int _pid = -1;
            string _cachekey = SysCacheKey.DicEnumListKey;
            object _objcache = MemCache.GetCache(_cachekey);

            List<DicEnumEntity> _objlistall = null;//总数 
            if (_objcache != null)
            {
                _objlistall = (List<DicEnumEntity>)_objcache;
            } 
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.SeachDefault:
                            {
                                if (entity.CompareValue!=null)
                                {
                                    _keyword = StringUtils.GetDbString(entity.CompareValue);
                                    if (_objcache != null)
                                    {
                                        var templist = from c in _objlistall
                                                       where c.Code.Contains(_keyword) || c.Name.Contains(_keyword)
                                                       orderby c.Name
                                                       select c;
                                        _objlistall = templist.ToList<DicEnumEntity>();
                                    }
                                }
                            }
                            break;
                        case SearchFieldName.ParentId:
                            {
                                _pid = StringUtils.GetDbInt(entity.CompareValue);
                                if (_objcache != null)
                                {
                                    var templist = from c in _objlistall
                                                   where c.ParentId == StringUtils.GetDbInt(entity.CompareValue)
                                                   orderby c.Name
                                                   select c;
                                    _objlistall = templist.ToList<DicEnumEntity>();
                                }
                            }
                            break;
                        default: break;
                    }
                }
            }
            //  _cachekey = SysCacheKey.DicListKey + "_" + pageSize.ToString() + "_" + pageIndex.ToString() + "_" + keyword + "_" + pid;
            //  obj = MemCache.GetCache(_cachekey);  
            //if (obj != null)
            //{
            //    return (IList<DicEnumEntity>)obj;
            //}
            //else
            //{

            IList<DicEnumEntity> list = null;
            if (pageIndex == 0) pageIndex = 1;
            if (_objcache != null&& _objlistall!=null&& _objlistall.Count>0)
            {
                list = LinqQuery.QueryByPage<DicEnumEntity>(pageSize, pageIndex, _objlistall);
            }
            else
            {
                list = DicEnumDA.Instance.GetDicEnumList(  pageSize, pageIndex, ref recordCount, _keyword, _pid);
            }
                //MemCache.AddCache(_cachekey, list); 
            return list;
            //}
        }
        public async Task GetDicEnumAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = SysCacheKey.DicEnumListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<DicEnumEntity> list = null;
                    list = DicEnumDA.Instance.GetDicEnumAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(DicEnumEntity dicEnum)
        {
            return DicEnumDA.Instance.ExistNum(dicEnum)>0;
        }
	  #endregion   
	}
}
