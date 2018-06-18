using SuperMarket.Core.Safe;
using SuperMarket.Core.WebService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.Sms
{
    public   class SmsNotices
    {
        /// <summary>
        /// 助通科技
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ZhuTongKeJiSend(string mobilestr,string msgbody)
        {
            MobileMessageConfig config =( MobileMessageConfig)SuperMarket.Core.ConfigCore.Instance.ConfigEntity.SMSConfigS.Where(x => x.Name == "ZhuTongKeJi");
            string datenowstr = DateTime.Now.ToString("yyyyMMddHHmmss");
            Hashtable _Pars = new Hashtable();
            _Pars.Add("username", config.UserCode);
            _Pars.Add("password", CryptMD5.Encrypt(CryptMD5.Encrypt(config.PassWord.Trim()).ToLower() + datenowstr).ToLower());
            _Pars.Add("mobile", mobilestr);
            _Pars.Add("content", msgbody);
            _Pars.Add("tkey", datenowstr);
            _Pars.Add("productid", config.AppId);
            _Pars.Add("xh", "");
            string resultr= WebServiceClient.QueryPostWebService(config.Url, _Pars);
            return resultr;
        } 


    }
}
