using System;
using System.Collections.Generic;
using System.Web;

namespace SuperMarket.Pay.WeiXin
{
    /**
    * 	配置账号信息
    */
    public class WxPayConfig
    {
        //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        /// <summary>
        /// 微信获取支付码链接
        /// </summary>
        public const string gateway = "https://api.mch.weixin.qq.com/pay/unifiedorder?"; 
        
        //=======【基本信息设置】=====================================
        /* 微信公众号信息配置
        * APPID：绑定支付的APPID（必须配置）
        * MCHID：商户号（必须配置）
        * KEY：商户支付密钥，参考开户邮件设置（必须配置）
        * APPSECRET：公众帐号secert（仅JSAPI支付的时候需要配置）
        */
        public const string APPID = "wxa66997fc709985e0";
        public const string MCHID = "1487459852";
        public const string KEY = "a193c0fdfe301277acc73c38fa9aead6";
        public const string APPSECRET = "a193c0fdfe301277acc73c38fa9aead6";

        //=======【证书路径设置】===================================== 
        /* 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        */
        public const string SSLCERT_PATH = "cert/apiclient_cert.p12";
        public const string SSLCERT_PASSWORD = "a193c0fdfe301277acc73c38fa9aead6";



        //=======【支付结果通知url】===================================== 
        /* 支付结果通知回调url，用于商户接收支付结果
        */
        public const string NOTIFY_URL = "http://pay.xindahv.com/Cashier/WeiXinNotify";

        //=======【商户系统后台机器IP】===================================== 
        /* 此参数可手动配置也可在程序中自动获取
        */
        public const string IP = "120.55.160.236";


        //=======【代理服务器设置】===================================
        /* 默认IP和端口号分别为0.0.0.0和0，此时不开启代理（如有需要才设置）
        */
        public const string PROXY_URL = "http://10.152.18.220:8080";

        //=======【上报信息配置】===================================
        /* 测速上报等级，0.关闭上报; 1.仅错误时上报; 2.全量上报
        */
        public const int REPORT_LEVENL = 0;

        //=======【日志级别】===================================
        /* 日志等级，0.不输出日志；1.只输出错误信息; 2.输出错误和正常信息; 3.输出错误信息、正常信息和调试信息
        */
        public const int LOG_LEVENL = 0;


        // 字符编码格式 目前支持 gbk 或 utf-8
        public const string input_charset = "utf-8";
        // 签名方式
        public static string sign_type = "MD5";
    }
}