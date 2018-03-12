using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Api
{
    /// <summary>
    /// WEB api 结果返回对象
    /// </summary>
  public  class ApiResultObj 
    { 
        /// <summary>
        /// WebAPI返回状态码
        /// </summary>
        public int ApiStatus;
        /// <summary>
        /// 返回的提示信息（或者报错信息）
        /// </summary>
        public string ApiMsg;
        /// <summary>
        /// 实际需要的对象
        /// </summary>
        public string FactJson;
    }
    
}
