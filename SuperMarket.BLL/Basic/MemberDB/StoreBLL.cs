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
/*****************************************
功能描述：表Store的业务逻辑层。
创建时间：2016/8/3 13:06:43
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{

    /// <summary>
    /// 表Store的业务逻辑层。
    /// </summary>
    public class StoreBLL
    {
        #region 实例化
        public static StoreBLL Instance
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
            internal static readonly StoreBLL instance = new StoreBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表Store，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="store">要添加的Store数据实体对象</param>
        public int AddStore(MemStoreEntity store)
        {
            if (store.Id > 0)
            {
                return UpdateStore(store);
            }
            else if (!string.IsNullOrEmpty(store.StoreName) && StoreBLL.Instance.IsExist(store))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemStoreDA.Instance.AddStore(store);
            }
        }

        /// <summary>
        /// 更新一条Store记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="store">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateStore(MemStoreEntity store)
        {
            return MemStoreDA.Instance.UpdateStore(store);
        }
       
        /// <summary>
        /// 根据主键获取一个Store实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>Store实体</returns>
        /// <param name="columns">要返回的列</param>
        public MemStoreEntity GetStore(int id)
        {
            return MemStoreDA.Instance.GetStore(id);
        }
        public MemStoreEntity GetStoreByNameAdd(string name,string address)
        {
            return MemStoreDA.Instance.GetStoreByNameAdd(name, address);

        }
        /// </summary>
        /// <returns>Store实体</returns>
        /// <param name="columns">要返回的列</param>
        public MemStoreEntity GetStoreByMemId(int memid, bool cache = false)
        {
            MemStoreEntity entity = new MemStoreEntity();
            if (cache)
            {
                string _cachekey = "GetStoreByMemId_" + memid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    entity = MemStoreDA.Instance.GetStoreByMemId(memid);
                    MemCache.AddCache(_cachekey, entity);
                }
                else
                {
                    entity = (MemStoreEntity)obj;
                }
            }
            else
            {
                entity = MemStoreDA.Instance.GetStoreByMemId(memid);
            }

            return entity;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemStoreEntity> GetStoreList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _legalName = string.Empty;
            string _companyName = string.Empty;
            int _checkStatus = -1;
            string _memcode = string.Empty;
            string _contractname = string.Empty;
            string _contractphone = string.Empty;
            int _issupplier = 0;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit _entity in wherelist)
                {
                    if (_entity.FieldName == SearchFieldName.LegalName)
                    {
                        _legalName = StringUtils.GetDbString(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.CompanyName)
                    {
                        _companyName = StringUtils.GetDbString(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.CheckStatus)
                    {
                        _checkStatus = StringUtils.GetDbInt(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.MemCode)
                    {
                        _memcode = StringUtils.GetDbString(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.ContractName)
                    {
                        _contractname = StringUtils.GetDbString(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.ContractPhone)
                    {
                        _contractphone = StringUtils.GetDbString(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.IsSupplier)
                    {
                        _issupplier = StringUtils.GetDbInt(_entity.CompareValue);
                    }
                }
            }
            return MemStoreDA.Instance.GetStoreList(pageSize, pageIndex, ref recordCount, _legalName, _companyName, _checkStatus, _memcode, _contractname, _contractphone, _issupplier);
        }
        public IList<MemStoreEntity> GetStoreNoPositionList(int pageSize, int pageIndex, ref int recordCount)
        { 
            return MemStoreDA.Instance.GetStoreNoPositionList(pageSize, pageIndex, ref recordCount );
        }

        public IList<UnitStr> ShowHasRegister(string str)
        {
            return MemStoreDA.Instance.ShowHasRegister(str);

        }
        public IList<MemStoreEntity> GetStoreListByMems(string memids)
        {
            return MemStoreDA.Instance.GetStoreListByMems(memids); 
        }
        public VWShowStoreEntity GetNextStore(int preid)
        {
            VWShowStoreEntity entity = new VWShowStoreEntity();
            if (preid > 0)
            {
                string _cachekey = "GetNextStore_" + preid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    entity = MemStoreDA.Instance.GetNextStore(preid);
                    MemCache.AddCache(_cachekey, entity);
                }
                else
                {
                    entity = (VWShowStoreEntity)obj;
                }
            }
            else
            {
                entity = MemStoreDA.Instance.GetNextStore(preid);
            }

            return entity;
        }
        public async Task GetStoreAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "StoreListKey";// SysCacheKey.StoreListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemStoreEntity> list = null;
                    list = MemStoreDA.Instance.GetStoreAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemStoreEntity store)
        {
            return MemStoreDA.Instance.ExistNum(store) > 0;
        }
        public bool IsExist(string storename,string address)
        {
            return MemStoreDA.Instance.ExistNum(storename, address) > 0;
        }

        public VWStoreEntity GetVWStoreByMemId(int memid)
        {
            return MemStoreDA.Instance.GetVWStoreByMemId(memid);
        }

        public IList<MemStoreEntity> GetStoreByLegalName(int pagesize, int pageindex, ref int recordcount, string LegalName)
        {
            return MemStoreDA.Instance.GetStoreByLegalName(pagesize, pageindex, ref recordcount, LegalName);
        }


        public IList<MemStoreEntity> GetStoreByCompanyName(int pagesize, int pageindex, ref int recordcount, string compamyName)
        {
            return MemStoreDA.Instance.GetStoreByCompanyName(pagesize, pageindex, ref recordcount, compamyName);
        }

        public IList<MemStoreEntity> GetStoreByStatus(int pagesize, int pageindex, ref int recordcount, int Status)
        {
            return MemStoreDA.Instance.GetStoreByStatus(pagesize, pageindex, ref recordcount, Status);
        }

        public int CheckPhoneIsExist(string mobile)
        {
            return MemStoreDA.Instance.CheckPhoneIsExist(mobile);
        }
    }
}

