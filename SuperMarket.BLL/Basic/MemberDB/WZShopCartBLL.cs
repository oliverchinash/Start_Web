using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WZ_ShopCart的业务逻辑层。
创建时间：2016/9/18 12:34:35
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表WZ_ShopCart的业务逻辑层。
	/// </summary>
	public class WZShopCartBLL
	{
	    #region 实例化
        public static WZShopCartBLL Instance
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
            internal static readonly WZShopCartBLL instance = new WZShopCartBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WZ_ShopCart，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wZShopCart">要添加的WZ_ShopCart数据实体对象</param>
		public   int AddWZShopCart(WZShopCartEntity wZShopCart)
		{
			if (wZShopCart.Id > 0|| WZShopCartBLL.Instance.IsExist(wZShopCart))
            {
                return UpdateWZShopCart(wZShopCart);
            } 
            else  
            {
                return WZShopCartDA.Instance.AddWZShopCart(wZShopCart);
            }
	 	}
        /// <summary>
        /// 插入一条记录到表WZ_ShopCart，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="wZShopCart">要添加的WZ_ShopCart数据实体对象</param>
        public int AddWZShopCartXuQiu(WZShopCartEntity wZShopCart)
        {
            if (wZShopCart.Id > 0 || WZShopCartBLL.Instance.IsExist(wZShopCart))
            {
                return UpdateWZShopCartXuQiu(wZShopCart);
            }
            else
            {
                return WZShopCartDA.Instance.AddWZShopCart(wZShopCart);
            }
        }
        /// <summary>
        /// 更新一条WZ_ShopCart记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="wZShopCart">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateWZShopCart(WZShopCartEntity wZShopCart)
		{
			return WZShopCartDA.Instance.UpdateWZShopCart(wZShopCart);
		}
        public int UpdateWZShopCartXuQiu(WZShopCartEntity wZShopCart)
        {
            return WZShopCartDA.Instance.UpdateWZShopCartXuQiu(wZShopCart);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWZShopCartByKey(int id)
        {
            return WZShopCartDA.Instance.DeleteWZShopCartByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWZShopCartDisabled()
        {
            return WZShopCartDA.Instance.DeleteWZShopCartDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWZShopCartByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WZShopCartDA.Instance.DeleteWZShopCartByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWZShopCartByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WZShopCartDA.Instance.DisableWZShopCartByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WZ_ShopCart实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WZ_ShopCart实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WZShopCartEntity GetWZShopCart(int id)
		{
			return WZShopCartDA.Instance.GetWZShopCart(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WZShopCartEntity> GetWZShopCartList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WZShopCartDA.Instance.GetWZShopCartList(pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 获取数据库购物车数据
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public string GetCartCookie(int memberid)
        {
            WZShopCartEntity _entity = WZShopCartDA.Instance.GetCartCookie(memberid);
            if (_entity != null && _entity.CookieValue != null&&_entity.CookieValue != "")
            {
                return _entity.CookieValue;
            }
            else { return ""; }

        }
        public string GetXuQiuCookie(int memberid)
        {
            WZShopCartEntity _entity = WZShopCartDA.Instance.GetCartCookie(memberid);
            if (_entity != null && _entity.CookieValueXuQiu != null && _entity.CookieValueXuQiu != "")
            {
                return _entity.CookieValueXuQiu;
            }
            else { return ""; }

        }
        public async Task GetWZShopCartAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WZShopCartListKey";// SysCacheKey.WZShopCartListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WZShopCartEntity> list = null;
                    list = WZShopCartDA.Instance.GetWZShopCartAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WZShopCartEntity wZShopCart)
        {
            return WZShopCartDA.Instance.ExistNum(wZShopCart)>0;
        }
		
	}
}

