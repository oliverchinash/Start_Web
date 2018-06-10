using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.MessageDB;

/*****************************************
功能描述：表CGReturn的业务逻辑层。
创建时间：2017/1/20 16:26:01
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{
	  
	/// <summary>
	/// 表CGReturn的业务逻辑层。
	/// </summary>
	public class CGReturnBLL
	{
	    #region 实例化
        public static CGReturnBLL Instance
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
            internal static readonly CGReturnBLL instance = new CGReturnBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGReturn，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGReturn">要添加的CGReturn数据实体对象</param>
		public   int AddCGReturn(CGReturnEntity cGReturn)
		{
			  if (cGReturn.Id > 0)
            {
                return UpdateCGReturn(cGReturn);
            }
          
            //else if (CGReturnBLL.Instance.IsExist(cGReturn))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return CGReturnDA.Instance.AddCGReturn(cGReturn);
            }
	 	}

        public int SaveCGReturn(CGReturnEntity cGReturn)
        {
            return CGReturnDA.Instance.SaveCGReturn(cGReturn); 
        }
        /// <summary>
        /// 更新一条CGReturn记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGReturn">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateCGReturn(CGReturnEntity cGReturn)
		{
			return CGReturnDA.Instance.UpdateCGReturn(cGReturn);
		}
        public int NoteMsgToCGMems(int returnid)
        {
            int result = 0;
           IList <CGReturnEntity> cgenlist = CGReturnBLL.Instance.GetCGReturnAll(returnid);
            bool cansend = false;
            string memids = "";
            string ordercode = "";
            if (cgenlist != null && cgenlist.Count > 0)
            {
                foreach (CGReturnEntity entity in cgenlist)
                {
                    if (!cansend && (entity.Status == (int)CGReturnStatus.Apply || entity.Status == (int)CGReturnStatus.Accept))
                    {
                        cansend = true;
                        ordercode = entity.CGOrderCode.ToString();
                    } 
                } 
            }
            if (cansend)
            {
                //系统接口
                result = CGReturnDA.Instance.NoteMsgToCGMems(returnid);
                string msg = "易店心订单" + ordercode + "需要退换货啦，请注意及时收货额！";
                foreach (CGReturnEntity entity in cgenlist)
                {
                    SMSNoticeBLL.Instance.SMSNoticeAdd(entity.Phone, msg, (int)SystemType.Purchase);
                }
            }
            
            return result;

        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGReturnByKey(int id)
        {
            return CGReturnDA.Instance.DeleteCGReturnByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGReturnDisabled()
        {
            return CGReturnDA.Instance.DeleteCGReturnDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGReturnByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGReturnDA.Instance.DeleteCGReturnByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGReturnByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGReturnDA.Instance.DisableCGReturnByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGReturn实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGReturn实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGReturnEntity GetCGReturn(int id)
		{
			return CGReturnDA.Instance.GetCGReturn(id);			
		}

        public int  ReBackRecived(long cgordercode,int rebackid,int memid)
        {
            return CGReturnDA.Instance.ReBackRecived(cgordercode, rebackid, memid);

        }
        /// <summary>
        /// 根据主键获取一个CGReturn实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGReturn实体</returns>
        /// <param name="columns">要返回的列</param>
        public IList<CGReturnEntity> GetCGReturnById(int id)
        {
            IList < CGReturnEntity > entityList = CGReturnDA.Instance.GetCGReturnById(id);
            if (entityList != null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                       item.CityName = GYCityBLL.Instance.GetGYCityByCode(item.CityId.ToString()).Name;
                }
            }
            return entityList;
        }


        ///// <summary>
        ///// 获得数据列表
        ///// </summary>

        public IList<VWCGReturnEntity> GetVWCGReturnList(int pageSize, int pageIndex, ref int recordCount, string key, int memid,int _status)
        {
            return CGReturnDA.Instance.GetVWCGReturnList(pageSize, pageIndex, ref recordCount, key, memid, _status);
        }
        public IList<CGReturnEntity> GetCGReturnAll(int resultid)
        {
          
                    IList<CGReturnEntity> list = null;
                    list = CGReturnDA.Instance.GetCGReturnAll(resultid);
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGReturnEntity cGReturn)
        {
            return CGReturnDA.Instance.ExistNum(cGReturn)>0;
        }
		
	}
}

