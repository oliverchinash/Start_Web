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
        public int AddPostAddress(PostAddressEntity postAddress)
        {
            if (postAddress.Id > 0)
            {
                return UpdatePostAddress(postAddress);
            }
            else
            {
                return PostAddressDA.Instance.AddPostAddress(postAddress);
            }
        }

        /// <summary>
        /// 更新一条PostAddress记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="postAddress">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdatePostAddress(PostAddressEntity postAddress)
        {
            return PostAddressDA.Instance.UpdatePostAddress(postAddress);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePostAddressByKey(int id,int memid)
        {
            return PostAddressDA.Instance.DeletePostAddressByKey(id, memid);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePostAddressDisabled()
        {
            return PostAddressDA.Instance.DeletePostAddressDisabled();
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
            return PostAddressDA.Instance.DeletePostAddressByIds(idstr);
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
            return PostAddressDA.Instance.DisablePostAddressByIds(idstr);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int SetDefaultPostAddress(int id)
        {
            return PostAddressDA.Instance.SetDefaultPostAddress(id);
        }
        public PostAddressEntity GetDefaultAddress(int memid)
        {
            PostAddressEntity _entity = PostAddressDA.Instance.GetDefaultAddress(memid);
               _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;

            return _entity;
        }
        /// <summary>
        /// 根据主键获取一个PostAddress实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>PostAddress实体</returns>
        /// <param name="columns">要返回的列</param>
        public PostAddressEntity GetPostAddress(int id)
        {
            PostAddressEntity _entity= PostAddressDA.Instance.GetPostAddress(id);
            //_entity.ProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_entity.ProvinceId.ToString()).Name;
            _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;
            return _entity;
        }
        public PostAddressEntity GetDefaultPostAddress(int memid)
        {
            PostAddressEntity _entity = PostAddressDA.Instance.GetDefaultPostAddress(memid);
            //_entity.ProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_entity.ProvinceId.ToString()).Name;
            _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;
            return _entity;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PostAddressEntity> GetPostAddressList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
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
            IList<PostAddressEntity> list= PostAddressDA.Instance.GetPostAddressList(pageSize, pageIndex, ref recordCount, _memid);
            foreach(PostAddressEntity _entity in list)
            {
                //_entity.ProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_entity.ProvinceId.ToString()).Name;
                _entity.CityName = GYCityBLL.Instance.GetGYCityByCode(_entity.CityId.ToString()).Name;
            }
            return list;
        }

        public IList<PostAddressEntity> NewGetPostAddressList(int pagesize, int pageindex, ref int recordCount, int _memid)
        {
            return PostAddressDA.Instance.GetPostAddressList(pagesize, pageindex, ref recordCount, _memid);
        }

        public async Task GetPostAddressAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "PostAddressListKey";// SysCacheKey.PostAddressListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PostAddressEntity> list = null;
                    list = PostAddressDA.Instance.GetPostAddressAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PostAddressEntity postAddress)
        {
            return PostAddressDA.Instance.ExistNum(postAddress) > 0;
        }
      public int  GetNumForAddress(int memid)
        {
            return PostAddressDA.Instance.GetNumForAddress(memid) ;

        }
        public IList<PostAddressEntity> GetPostListByMemId(int memid)
        {
            return PostAddressDA.Instance.GetPostListByMemId(memid);
        }

        public int SetDefault(int addressid,int memid)
        {
            return PostAddressDA.Instance.SetDefault(addressid, memid);
        }

    }
}

