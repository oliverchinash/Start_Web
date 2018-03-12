using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;
using SuperMarket.BLL.Common;
using System.Threading;
using SuperMarket.BLL.ProductDB;

/*****************************************
功能描述：表OrderDetailPreTemp的业务逻辑层。
创建时间：2016/9/20 17:14:56
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderDetailPreTemp的业务逻辑层。
	/// </summary>
	public class OrderDetailPreTempBLL
	{
        private readonly object thread;
        #region 实例化
        public static OrderDetailPreTempBLL Instance
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
            internal static readonly OrderDetailPreTempBLL instance = new OrderDetailPreTempBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderDetailPreTemp，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderDetailPreTemp">要添加的OrderDetailPreTemp数据实体对象</param>
		public   int AddOrderDetailPreTemp(OrderDetailPreTempEntity orderDetailPreTemp)
		{
			   return OrderDetailPreTempDA.Instance.AddOrderDetailPreTemp(orderDetailPreTemp);
           
	 	}
        public int AddOrderPreList(IList<OrderDetailPreTempEntity> list)
        {
            return OrderDetailPreTempDA.Instance.AddOrderPreList(list);

        }
        public string CreateOrderPre(string detailids,int isstore, int memstatus, int memgrade,int memstoretype)
        {
            IList<OrderDetailPreTempEntity> _listproduct = ProductStyleBLL.Instance.GetOrderProducts(detailids);
            long randcode = 0;
            if (_listproduct != null && _listproduct.Count > 0)
            {
                Random rd = new Random();
                randcode = StringUtils.GetDbLong(DateTime.Now.ToString("yyMMdd") + rd.Next(100000, 999999));
                int pt = (int)SuperMarket.Model.ProductType.SpecialPrice;
                foreach (OrderDetailPreTempEntity _entityproduct in _listproduct)
                {
                    _entityproduct.Status = 1;
                    _entityproduct.ActualPrice = Calculate.GetPrice(memstatus, isstore, memstoretype, memgrade, _entityproduct.TradePrice, _entityproduct.Price, _entityproduct.IsBP, _entityproduct.DealerPrice);
                    _entityproduct.Code = randcode;
                    _entityproduct.CreateTime = DateTime.Now;
                    _entityproduct.TotalMarketPrice = _entityproduct.MarketPrice * _entityproduct.Num;
                    _entityproduct.TotalPrice = _entityproduct.ActualPrice * _entityproduct.Num;
                    if (_entityproduct.ProductType == pt && _entityproduct.StockNum < _entityproduct.Num)
                    {
                        _entityproduct.Status = 0;
                        _entityproduct.Remark = "库存不足";
                    }
                }
                OrderDetailPreTempBLL.Instance.AddOrderPreList(_listproduct);
            }
            return randcode.ToString();
        }
        /// <summary>
        /// 更新一条OrderDetailPreTemp记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="orderDetailPreTemp">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateOrderDetailPreTemp(OrderDetailPreTempEntity orderDetailPreTemp)
		{
			return OrderDetailPreTempDA.Instance.UpdateOrderDetailPreTemp(orderDetailPreTemp);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderDetailPreTempByKey(string code)
        {
            return OrderDetailPreTempDA.Instance.DeleteOrderDetailPreTempByKey(code);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderDetailPreTempDisabled()
        {
            return OrderDetailPreTempDA.Instance.DeleteOrderDetailPreTempDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderDetailPreTempByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderDetailPreTempDA.Instance.DeleteOrderDetailPreTempByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderDetailPreTempByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderDetailPreTempDA.Instance.DisableOrderDetailPreTempByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个OrderDetailPreTemp实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>OrderDetailPreTemp实体</returns>
		/// <param name="columns">要返回的列</param>
		public   OrderDetailPreTempEntity GetOrderDetailPreTemp(string code)
		{
			return OrderDetailPreTempDA.Instance.GetOrderDetailPreTemp(code);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderDetailPreTempEntity> GetOrderDetailPreTempList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return OrderDetailPreTempDA.Instance.GetOrderDetailPreTempList(pageSize, pageIndex, ref recordCount);
        }
        public IList<OrderDetailPreTempEntity> GetOrderPreTempByCode(long code,int status=-1)
        {
            return OrderDetailPreTempDA.Instance.GetOrderPreTempByCode(code, status);
        }
        public IList<VWOrderDetailEntity> GetOrderDetailsByCode(long code )
        {
            return OrderDetailPreTempDA.Instance.GetOrderDetailsByCode(code );
        }
        public Dictionary<int, VWOrderEntity> GetVWOrdersByTempCode(long tempcode)
        {
            Dictionary<int, VWOrderEntity> listsdic = new Dictionary<int, VWOrderEntity>();//产品字典 
            IList<VWOrderDetailEntity> _listproduct = OrderDetailPreTempDA.Instance.GetOrderDetailsByCode(tempcode, -1);
            if (_listproduct != null && _listproduct.Count > 0)
            {
                foreach (VWOrderDetailEntity entity in _listproduct)
                {
                    if (entity.Status > 0)
                    {
                        if (!listsdic.ContainsKey(entity.CGMemId))
                        {
                            listsdic[entity.CGMemId] = new VWOrderEntity();
                            listsdic[entity.CGMemId].IsAhmTake = entity.IsAhmTake;
                        }
                        listsdic[entity.CGMemId].Details.Add(entity);
                    }
                    else
                    {
                        if (!listsdic.ContainsKey(0))//下架产品列表
                        {
                            listsdic[0] = new VWOrderEntity();
                        }
                        listsdic[0].Details.Add(entity);
                    }
                }
               
            }         
            return listsdic;
        }
        public VWOrderEntity GetVWOrderByTempCode(long tempcode)
        {
            VWOrderEntity _vworder = new VWOrderEntity();
            IList <VWOrderDetailEntity> _listproduct = OrderDetailPreTempDA.Instance.GetOrderDetailsByCode(tempcode, 1);
            //IList<OrderDetailPreTempEntity> _listproduct = OrderDetailPreTempBLL.Instance.GetOrderPreTempByCode(tempcode, 1);
            _vworder.Details = _listproduct;
              OrderCommonBLL.Instance.GetTransFeeForOrder(_vworder);
            //IList<VWOrderDetailEntity> listdetail = new List<VWOrderDetailEntity>();
             
            //foreach (OrderDetailPreTempEntity Preentity in _listproduct)
            //{
            //    VWOrderDetailEntity vwdetail = new VWOrderDetailEntity();
                
            //    vwdetail.OrderDetailId = Preentity.p;
            //    vwdetail.ProductDetailId = Preentity.ProductDetailId;
            //    vwdetail.ProductId = Preentity.ProductId;
            //    vwdetail.Num = Preentity.Num;
            //    listdetail.Add(vwdetail);
            //} 
            _vworder.PreOrderCode = tempcode; 
            _vworder.OrderType = (int)OrderType.OnLine;
            _vworder.NeedDeliver = 1;  
            _vworder.PayPrice = 0; 
            return _vworder;
        }


        public async Task GetOrderDetailPreTempAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "OrderDetailPreTempListKey";// SysCacheKey.OrderDetailPreTempListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderDetailPreTempEntity> list = null;
                    list = OrderDetailPreTempDA.Instance.GetOrderDetailPreTempAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });

        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderDetailPreTempEntity orderDetailPreTemp)
        {
            return OrderDetailPreTempDA.Instance.ExistNum(orderDetailPreTemp)>0;
        }
	}
}

