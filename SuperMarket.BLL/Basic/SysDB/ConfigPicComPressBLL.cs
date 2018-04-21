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
功能描述：表ConfigPicComPress的业务逻辑层。
创建时间：2018/4/17 23:52:13
创 建 人：lgzh
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表ConfigPicComPress的业务逻辑层。
	/// </summary>
	public class ConfigPicComPressBLL
	{
	    #region 实例化
        public static ConfigPicComPressBLL Instance
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
            internal static readonly ConfigPicComPressBLL instance = new ConfigPicComPressBLL();
        }
        #endregion
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ConfigPicComPress，返回操作数。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="configPicComPress">要添加的ConfigPicComPress数据实体对象</param>
		public   int AddConfigPicComPress(ConfigPicComPressEntity configPicComPress)
		{
			return ConfigPicComPressDA.Instance.AddConfigPicComPress(configPicComPress);
		}

		/// <summary>
		/// 更新一条ConfigPicComPress记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="configPicComPress">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateConfigPicComPress(ConfigPicComPressEntity configPicComPress)
		{
			return ConfigPicComPressDA.Instance.UpdateConfigPicComPress(configPicComPress);
		}
				/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteConfigPicComPressByKey(int id)
	    {
		  	return  ConfigPicComPressDA.Instance.DeleteConfigPicComPressByKey(id);
	
		}
		/// <summary>
		/// 根据主键获取一个ConfigPicComPress实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ConfigPicComPress实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ConfigPicComPressEntity GetConfigPicComPress(int id)
		{
			return ConfigPicComPressDA.Instance.GetConfigPicComPress(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ConfigPicComPressEntity> GetConfigPicComPressList(int pageSize, int pageIndex, ref  int recordCount)
        {
            return ConfigPicComPressDA.Instance.GetConfigPicComPressList(pageSize, pageIndex, ref recordCount);
        }
        public IList<ConfigPicComPressEntity> GetConfigListByPicSource(int source)
        {
            return ConfigPicComPressDA.Instance.GetConfigListByPicSource(source);
        }
        
        #endregion
    }
}
