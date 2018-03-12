using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.MemberDB;

/*****************************************
功能描述：表MemCoupons的业务逻辑层。
创建时间：2017/3/25 18:44:20
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表MemCoupons的业务逻辑层。
	/// </summary>
	public class MemCouponsBLL
    {
	    #region 实例化
        public static MemCouponsBLL Instance
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
            internal static readonly MemCouponsBLL instance = new MemCouponsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemCoupons，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="MemCoupons">要添加的MemCoupons数据实体对象</param>
		public   int AddMemCoupons(MemCouponsEntity MemCoupons)
		{ 
          
                return MemCouponsDA.Instance.AddMemCoupons(MemCoupons);
          
	 	}

		/// <summary>
		/// 更新一条MemCoupons记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="MemCoupons">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemCoupons(MemCouponsEntity MemCoupons)
		{
			return MemCouponsDA.Instance.UpdateMemCoupons(MemCoupons);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemCouponsByKey(int id)
        {
            return MemCouponsDA.Instance.DeleteMemCouponsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemCouponsDisabled()
        {
            return MemCouponsDA.Instance.DeleteMemCouponsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemCouponsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemCouponsDA.Instance.DeleteMemCouponsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemCouponsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemCouponsDA.Instance.DisableMemCouponsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemCoupons实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemCoupons实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemCouponsEntity GetMemCoupons(int id)
		{
			return MemCouponsDA.Instance.GetMemCoupons(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemCouponsEntity> GetMemCouponsList(int pageIndex, int pageSize, ref  int recordCount,int status,int memid)
        {
            IList<MemCouponsEntity> list= MemCouponsDA.Instance.GetMemCouponsList(pageIndex, pageSize,  ref recordCount, status, memid);
            if(list!=null&& list.Count>0)
            {
                foreach(MemCouponsEntity entity in list)
                {
                    entity.DicCoupons = DicCouponsBLL.Instance.GetDicCoupons(entity.CouponsId);
                } 
            }
            return list;
        }
        /// <summary>
        /// 获取当前人员有效的优惠券
        /// </summary>
        /// <param name="memid"></param>
        /// <returns></returns>
        public IList<DicCouponsEntity> GetCouponsByMem(int memid)
        {
            IList<DicCouponsEntity> listresult = new List<DicCouponsEntity>();
            IList<MemCouponsEntity> list = MemCouponsDA.Instance.GetCanUseCoupons(  memid);
            if (list != null && list.Count > 0)
            {
                foreach (MemCouponsEntity entity in list)
                { 
                    DicCouponsEntity en = DicCouponsBLL.Instance.GetDicCoupons(entity.CouponsId);
                    listresult.Add(en);
                }
            }
            return listresult;
        }
        public IList<MemCouponsEntity> GetCanUseMemCoupons(int memid)
        { 
            IList<MemCouponsEntity> list = MemCouponsDA.Instance.GetCanUseCoupons(memid);
            if (list != null && list.Count > 0)
            {
                foreach (MemCouponsEntity entity in list)
                {
                    entity.DicCoupons = DicCouponsBLL.Instance.GetDicCoupons(entity.CouponsId);
                 
                }
            }
            return list;
        }
        public int GetCanUseCouponsNum(int memid)
        {
            return MemCouponsDA.Instance.GetCanUseCouponsNum(memid);
             
        }
        public MemCouponsEntity GetByCouponIdAndMem(int couponid,int memid)
        {
           MemCouponsEntity  entity = MemCouponsDA.Instance.GetByCouponIdAndMem(couponid,memid);
            if (entity!=null&& entity.Id>0)
            {
                entity.DicCoupons = DicCouponsBLL.Instance.GetDicCoupons(entity.CouponsId);
            }
            return entity;
        }

        public MemCouponsEntity GetCouponByMemCouponId(int memid,int memcouponid)
        {
            MemCouponsEntity entity = MemCouponsDA.Instance.GetByMemCouponId(  memid,memcouponid);
            if (entity != null && entity.Id > 0)
            {
                entity.DicCoupons = DicCouponsBLL.Instance.GetDicCoupons(entity.CouponsId);
            }
            return entity;
        }
        public async Task GetMemCouponsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemCouponsListKey";// SysCacheKey.MemCouponsListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<MemCouponsEntity> list = null;
                    list = MemCouponsDA.Instance.GetMemCouponsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemCouponsEntity MemCoupons)
        {
            return MemCouponsDA.Instance.ExistNum(MemCoupons)>0;
        }

        /// <summary>
        /// 订单完成送券，送优惠券
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int ProvideCouponsByOrder(long ordercode)
        {
            int num = 0;
            OrderEntity orderen=   OrderBLL.Instance.GetOrderByCode(ordercode);
            if (orderen != null && (orderen.Status == (int)OrderStatus.Finished ))
            {
                ///获取限量乐购的总金额
                decimal totalprice = OrderDetailBLL.Instance.GetTotalPrice(ordercode, (int)ProductType.SpecialPrice);
                if (totalprice >= 200)
                { 
                    int i = (int)totalprice / 200;
                    for(int j=0;j<i;j++)
                    {
                        MemCouponsEntity memcouentity = new MemCouponsEntity();
                        memcouentity.MemId = orderen.MemId;
                        memcouentity.CouponsId = (int)DicCouponsEnum.Cash10_11;
                        memcouentity.CreateTime = DateTime.Now;
                        memcouentity.EndTime = DateTime.Now.AddDays(60);
                        memcouentity.Status = 1;
                        memcouentity.Num = 1; 
                        MemCouponsBLL.Instance.AddMemCoupons(memcouentity);
                        num++;
                    }
                }
                else if (totalprice >= 200) 
                {
                    MemCouponsEntity memcouentity = new MemCouponsEntity();
                    memcouentity.MemId = orderen.MemId;
                    memcouentity.CouponsId = (int)DicCouponsEnum.Cash5_6;
                    memcouentity.CreateTime = DateTime.Now;
                    memcouentity.EndTime = DateTime.Now.AddDays(60);
                    memcouentity.Status = 1;
                    memcouentity.Num = 1;
                    MemCouponsBLL.Instance.AddMemCoupons(memcouentity);
                    num++;
                }
                if(num>0)
                {
                    MemGiftLogEntity gift = new MemGiftLogEntity();
                    gift.MemId = orderen.MemId;
                    gift.CreateTme = DateTime.Now;
                    gift.Status = 1;
                    gift.Remark = "购物送券:Code:" + ordercode.ToString();
                    MemGiftLogBLL.Instance.AddMemGiftLog(gift);
                }
            }
            return num;

        }

    }
}

