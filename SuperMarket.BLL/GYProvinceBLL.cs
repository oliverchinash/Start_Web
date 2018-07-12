using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks; 
using SuperMarket.Core;
using System.Linq;
using SuperMarket.Core.Json;

/*****************************************
功能描述：表GYProvince的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL 
{
	  
	/// <summary>
	/// 表GYProvince的业务逻辑层。
	/// </summary>
	public class GYProvinceBLL
	{
	    #region 实例化
        public static GYProvinceBLL Instance
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
            internal static readonly GYProvinceBLL instance = new GYProvinceBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表GYProvince，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYProvince">要添加的GYProvince数据实体对象</param>
		public   int AddGYProvince(GYProvinceEntity gYProvince)
		{
			  
                return GYProvinceDA.Instance.AddGYProvince(gYProvince);
             
	 	}

		/// <summary>
		/// 更新一条GYProvince记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="gYProvince">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateGYProvince(GYProvinceEntity gYProvince)
		{
			return GYProvinceDA.Instance.UpdateGYProvince(gYProvince);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteGYProvinceByKey(int id)
        {
            return GYProvinceDA.Instance.DeleteGYProvinceByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteGYProvinceDisabled()
        {
            return GYProvinceDA.Instance.DeleteGYProvinceDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteGYProvinceByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return GYProvinceDA.Instance.DeleteGYProvinceByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableGYProvinceByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return GYProvinceDA.Instance.DisableGYProvinceByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个GYProvince实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>GYProvince实体</returns>
		/// <param name="columns">要返回的列</param>
		public   GYProvinceEntity GetGYProvince(int id)
		{
			return GYProvinceDA.Instance.GetGYProvince(id);			
		}
 


        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<GYProvinceEntity> GetGYProvinceList(int pageSize, int pageIndex, ref  int recordCount,  string name)
        {
            return GYProvinceDA.Instance.GetGYProvinceList(pageSize, pageIndex, ref recordCount);
        }
	 
        public IList<GYProvinceEntity> GetListGYProvinceAll()
        {
              IList<GYProvinceEntity> listall = null;
      
                  listall = GYProvinceDA.Instance.GetGYProvinceAll();
            
            return listall;
        }
        public string GetListGYProvinceJson()
        {
           string json = "";
           
                IList<GYProvinceEntity> listall = GetListGYProvinceAll();
                var listfilter = listall.Select(
                  p => new
                  {
                      Id = p.Id,
                      Code = p.Code,
                      Name = p.Name
                  });
                json = JsonJC.ObjectToJson(listfilter);
            
            return json;
        }


        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(GYProvinceEntity gYProvince)
        {
            return GYProvinceDA.Instance.ExistNum(gYProvince)>0;
        }
		
	}
}

