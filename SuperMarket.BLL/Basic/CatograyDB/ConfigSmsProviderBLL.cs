using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using System.Collections;
using SuperMarket.Core.Safe;
using SuperMarket.Core.WebService;
using SuperMarket.Core.Util;
using System.Xml;

/*****************************************
功能描述：表ConfigSmsProvider的业务逻辑层。
创建时间：2017/4/18 14:25:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ConfigSmsProvider的业务逻辑层。
	/// </summary>
	public class ConfigSmsProviderBLL
	{
	    #region 实例化
        public static ConfigSmsProviderBLL Instance
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
            internal static readonly ConfigSmsProviderBLL instance = new ConfigSmsProviderBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ConfigSmsProvider，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="configSmsProvider">要添加的ConfigSmsProvider数据实体对象</param>
		public   int AddConfigSmsProvider(ConfigSmsProviderEntity configSmsProvider)
		{
			  if (configSmsProvider.Id > 0)
            {
                return UpdateConfigSmsProvider(configSmsProvider);
            }
				  else if (string.IsNullOrEmpty(configSmsProvider.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ConfigSmsProviderBLL.Instance.IsExist(configSmsProvider))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ConfigSmsProviderDA.Instance.AddConfigSmsProvider(configSmsProvider);
            }
	 	}

		/// <summary>
		/// 更新一条ConfigSmsProvider记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="configSmsProvider">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateConfigSmsProvider(ConfigSmsProviderEntity configSmsProvider)
		{
			return ConfigSmsProviderDA.Instance.UpdateConfigSmsProvider(configSmsProvider);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteConfigSmsProviderByKey(int id)
        {
            return ConfigSmsProviderDA.Instance.DeleteConfigSmsProviderByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfigSmsProviderDisabled()
        {
            return ConfigSmsProviderDA.Instance.DeleteConfigSmsProviderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfigSmsProviderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ConfigSmsProviderDA.Instance.DeleteConfigSmsProviderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfigSmsProviderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ConfigSmsProviderDA.Instance.DisableConfigSmsProviderByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ConfigSmsProvider实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ConfigSmsProvider实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ConfigSmsProviderEntity GetConfigSmsProvider(int id)
		{
			return ConfigSmsProviderDA.Instance.GetConfigSmsProvider(id);			
		}
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="sendentity">短信对象</param>
        /// <param name="smstype">通道类型</param>
        /// <returns></returns>
        public MobileMessageEntity SendSms(MobileMessageEntity sendentity,int smstype=1)
        {
            string mobile = sendentity.MobilePhone;
            string smsbody = sendentity.MessageContent;
            sendentity.ResultSMS = "";
            IList<ConfigSmsProviderEntity> list = GetConfigSmsProviderAll(smstype,1);//获取所有有效的短信提供商
            foreach(ConfigSmsProviderEntity entity in list)
            {
                try
                {
                    sendentity.SendProvider = entity.Name; 
                    if (entity.Name.ToLower() == SMSProviders.ZhuTongKeJi.ToLower())
                    {
                        sendentity.ResultSMS = ZhuTongKeJiSend(entity.AppId, entity.UserCode, entity.PassWord, entity.Url, mobile, smsbody);
                        if (sendentity.ResultSMS.Contains(","))
                        {
                            string[] temp = sendentity.ResultSMS.Split(',');
                            if (temp[0] == "1")
                            {
                                break;
                            }
                        }
                    }
                    else if (entity.Name.ToLower() == SMSProviders.ChuangShiManDao.ToLower())
                    {
                        sendentity.ResultSMS = ChuangShiManDaoSend(entity.AppId, entity.PassWord, entity.Url, mobile, smsbody);
                        if (StringUtils.GetDbLong(sendentity.ResultSMS) > 0)
                        {
                            break;
                        }
                    }
                }
                catch(Exception ex)
                {
                    sendentity.ResultSMS = ex.Message;
                }
            }
            return sendentity;
        }

        /// <summary>
        /// 发送营销短信
        /// </summary>
        /// <param name="sendentity">短信对象</param>
        /// <param name="smstype">通道类型</param>
        /// <returns></returns>
        public SMSNoticeEntity SendSmsBehind(SMSNoticeEntity sendentity, int smstype = 2)
        {
            string mobile = sendentity.MobilePhone;
            string smsbody = sendentity.SMSContent.EndsWith("【易店心】")? sendentity.SMSContent: sendentity.SMSContent+ "【易店心】";
            sendentity.ReturnMsg = "";
            int isactive = 1;
            IList<ConfigSmsProviderEntity> list = GetConfigSmsProviderAll(smstype, isactive, false);//获取所有有效的短信提供商
            foreach (ConfigSmsProviderEntity entity in list)
            {
                try
                {
                    sendentity.SendProvider = entity.Name;
                    if (entity.Name.ToLower() == SMSProviders.ZhuTongKeJi.ToLower())
                    {
                        sendentity.ReturnMsg = ZhuTongKeJiSend(entity.AppId, entity.UserCode, entity.PassWord, entity.Url, mobile, smsbody);
                        if (sendentity.ReturnMsg.Contains(","))
                        {
                            string[] temp = sendentity.ReturnMsg.Split(',');
                            if (temp[0] == "1")
                            {
                                break;
                            }
                        }
                    }
                    else if (entity.Name.ToLower() == SMSProviders.ChuangShiManDao.ToLower())
                    {
                        sendentity.ReturnMsg = ChuangShiManDaoSend(entity.AppId, entity.PassWord, entity.Url, mobile, smsbody,"1");
                        if (StringUtils.GetDbLong(sendentity.ReturnMsg) > 0)
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    sendentity.ReturnMsg = ex.Message;
                }
            }
            return sendentity;
        }



        private string ZhuTongKeJiSend(string AppId, string UserCode,string PassWord ,string Url, string mobilestr, string msgbody)
        {
            string datenowstr = DateTime.Now.ToString("yyyyMMddHHmmss");
            Hashtable _Pars = new Hashtable();
            _Pars.Add("username",  UserCode);
            _Pars.Add("password", CryptMD5.Encrypt(CryptMD5.Encrypt( PassWord.Trim()).ToLower() + datenowstr).ToLower());
            _Pars.Add("mobile", mobilestr);
            _Pars.Add("content", msgbody);
            _Pars.Add("tkey", datenowstr);
            _Pars.Add("productid",  AppId);
            _Pars.Add("xh", "");
            string resultr = WebServiceClient.QueryPostWebService( Url, _Pars);
            return resultr;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="PassWord"></param>
        /// <param name="Url"></param>
        /// <param name="mobilestr"></param>
        /// <param name="msgbody"></param>
        /// <param name="ext">营销短信传“1”</param>
        /// <returns></returns>
        private string ChuangShiManDaoSend(string AppId, string PassWord, string Url, string mobilestr, string msgbody,string ext="")
        { 
            Hashtable _Pars = new Hashtable();
            string pwd = CryptMD5.Encrypt(AppId + PassWord);
            _Pars.Add("sn", AppId);
            _Pars.Add("pwd", pwd);
            _Pars.Add("mobile", mobilestr);
            _Pars.Add("content", msgbody);
            _Pars.Add("ext", ext); 
            _Pars.Add("stime", ""); 
            _Pars.Add("rrid", ""); 
            _Pars.Add("msgfmt", ""); 
            string resultr = WebServiceClient.QueryPostWebService(Url, _Pars);
           
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(resultr.Trim())));
                if (xDoc != null)
                {
                    resultr = xDoc.GetElementsByTagName("string")[0].InnerText;
                }  
            return resultr;
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ConfigSmsProviderEntity> GetConfigSmsProviderList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ConfigSmsProviderDA.Instance.GetConfigSmsProviderList(pageSize, pageIndex, ref recordCount);
        }
		/// <summary>
        /// 获取短信发送通道配置信息
        /// </summary>
        /// <param name="smstype">通道类型：1注册，2营销</param>
        /// <param name="isactive"></param>
        /// <returns></returns>
		public IList<ConfigSmsProviderEntity> GetConfigSmsProviderAll(int smstype,int isactive,bool cache=false)
        {
            IList<ConfigSmsProviderEntity> list = null;
            if (cache)
            {
                string _cachekey = "GetConfigSmsProviderAll_" + smstype + "_" + isactive;// SysCacheKey.ConfigSmsProviderListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = ConfigSmsProviderDA.Instance.GetConfigSmsProviderAll(smstype, isactive);
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<ConfigSmsProviderEntity>)obj;
                }
            }
            else
            {
                list = ConfigSmsProviderDA.Instance.GetConfigSmsProviderAll(smstype, isactive); 
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ConfigSmsProviderEntity configSmsProvider)
        {
            return ConfigSmsProviderDA.Instance.ExistNum(configSmsProvider)>0;
        }
		
	}
}

