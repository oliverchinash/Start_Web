using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;
using SuperMarket.BLL.SysDB;

/*****************************************
功能描述：表PostAddress的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{

    /// <summary>
    /// 表PostAddress的业务逻辑层。
    /// </summary>
    public class PostAddressBLL
    {
        #region 实例化
        public static PostAddressBLL Instance
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
            internal static readonly PostAddressBLL instance = new PostAddressBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表PostAddress，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="postAddress">要添加的PostAddress数据实体对象</param>
        public int AddPostAddress(MemPostAddressEntity postAddress)
        {
            if (postAddress.Id > 0)
            {
                return UpdatePostAddress(postAddress);
            }
            else
            {
                return MemPostAddressDA.Instance.AddMemPostAddress(postAddress);
            }
        }

        /// <summary>
        /// 更新一条PostAddress记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="postAddress">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdatePostAddress(MemPostAddressEntity postAddress)
        {
            return MemPostAddressDA.Instance.UpdateMemPostAddress(postAddress);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePostAddressByKey(int id,int memid)
        {
            return MemPostAddressDA.Instance.DeleteMemPostAddressByKey(id, memid);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePostAddressDisabled()
        {
            return MemPostAddressDA.Instance.DeleteMemPostAddressDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePostAddressByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemPostAddressDA.Instance.DeleteMemPostAddressByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePostAddressByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemPostAddressDA.Instance.DisableMemPostAddressByIds(idstr);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int SetDefaultPostAddress(int id)
        {
            return MemPostAddressDA.Instance.SetDefaultMemPostAddress(id);
        }
        public MemPostAddressEntity GetDefaultAddress(int memid)
        {
            MemPostAddressEntity _entity = MemPostAddressDA.Instance.GetDefaultAddress(memid);
               _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;

            return _entity;
        }
        /// <summary>
        /// 根据主键获取一个PostAddress实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>PostAddress实体</returns>
        /// <param name="columns">要返回的列</param>
        public MemPostAddressEntity GetPostAddress(int id)
        {
            MemPostAddressEntity _entity= MemPostAddressDA.Instance.GetMemPostAddress(id);
            //_entity.ProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_entity.ProvinceId.ToString()).Name;
            _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;
            return _entity;
        }
        public MemPostAddressEntity GetDefaultPostAddress(int memid)
        {
            MemPostAddressEntity _entity = MemPostAddressDA.Instance.GetDefaultMemPostAddress(memid);
            //_entity.ProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_entity.ProvinceId.ToString()).Name;
            _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;
            return _entity;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemPostAddressEntity> GetPostAddressList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _memid = 0;
            foreach (ConditionUnit entity in wherelist)
            {
                switch (entity.FieldName)
                {
                    case SearchFieldName.MemId:
                        {
                            _memid = StringUtils.GetDbInt(entity.CompareValue);
                        }
                        break;
                }
            }
            IList<MemPostAddressEntity> list= MemPostAddressDA.Instance.GetMemPostAddressList(pageSize, pageIndex, ref recordCount, _memid);
            foreach(MemPostAddressEntity _entity in list)
            {
                //_entity.ProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_entity.ProvinceId.ToString()).Name;
                _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;
            }
            return list;
        }

        public IList<MemPostAddressEntity> NewGetPostAddressList(int pagesize, int pageindex, ref int recordCount, int _memid)
        {
            return MemPostAddressDA.Instance.GetMemPostAddressList(pagesize, pageindex, ref recordCount, _memid);
        }

        public async Task GetPostAddressAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "PostAddressListKey";// SysCacheKey.PostAddressListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemPostAddressEntity> list = null;
                    list = MemPostAddressDA.Instance.GetMemPostAddressAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemPostAddressEntity postAddress)
        {
            return MemPostAddressDA.Instance.ExistNum(postAddress) > 0;
        }
      public int  GetNumForAddress(int memid)
        {
            return MemPostAddressDA.Instance.GetNumForAddress(memid) ;

        }
        public IList<MemPostAddressEntity> GetPostListByMemId(int memid)
        {
            return MemPostAddressDA.Instance.GetPostListByMemId(memid);
        }

        public int SetDefault(int addressid,int memid)
        {
            return MemPostAddressDA.Instance.SetDefault(addressid, memid);
        }

    }
}

