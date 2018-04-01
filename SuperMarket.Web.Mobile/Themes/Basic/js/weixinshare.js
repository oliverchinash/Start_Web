function getRandom() {//生成签名的随机串  
    var random = "";
    for (var i = 1; i <= 32; i++) {
        var n = Math.floor(Math.random() * 16.0).toString(16);
        random += n;
        if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) random += "";
    }

    return random;
}

//微信接口配置  
var wxShare = {
    isReady: false,//是否初始化完成  
    access_token: "",//公众号token，令牌  
    ticket: "",//调用jssdk的临时凭证  
    readySuccessCall: [],//微信初始化成功后的执行事务  
    appName: "阿哈马车服",//项目名称  
    config: {
        debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来。  
        appId: "", // 必填，公众号的唯一标识  
        timestamp: Math.ceil(new Date().getTime() / 1000).toString(), // 必填，生成签名的时间戳  
        nonceStr: getRandom(), // 必填，生成签名的随机串  
        signature: "",// 必填，签名，见附录1  
        jsApiList: ['onMenuShareTimeline', 'onMenuShareAppMessage', 'onMenuShareQQ', 'chooseWXPay'] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2  
    },
    init: function () {//初始化   
        zfalert("系统提示", "微信接口调用失败1", false);
        if (!wx) { 
            zfalert("系统提示", "微信接口调用失败", false);
            return false;
        } 
        var that = this;//保存作用域   
        this.wx_get_appid(function (data) {//获取appid    
            if (data.appId) { 
                setcookie("appId", data.appId );
                that.config.appId = data.appId; 
            } 
            that.wx_get_access_token(function (data) {//获取token，通过token获取jsapi_ticket  

                if (data.access_token) {

                    setcookie("access_token", data.access_token );

                    that.access_token = data.access_token;

                }

                that.wx_get_ticket(function (data) {//获取jsapi_ticket  

                    if (data.ticket) {

                        setcookie("ticket", data.ticket );

                        that.ticket = data.ticket;

                    }

                    that.wx_get_sign(function (data) {//根据noncestr, jsapi_ticket, timestamp, url获取签名  

                        that.config.signature = data.signature;

                        that.initWx(function () {//初始化微信接口  

                        });



                    });


                });


            });


        });



    },

    wx_get_appid: function (call) {   //获取微信公众号的appid  
        this.appId = getcookie("appId");//从cookie中获取appId  
        if (!this.appId) { 
            $.get("/Common/GetWeiXinAPPId", {}, function (data) {
                call && call(data);
            }, "json");   
            return; 
        }

        call && call({});

    },

    wx_get_access_token: function (call) { //获取公众号访问令牌  

        this.access_token = getcookie("access_token");//从Cookie中获取  

        if (!this.access_token) {//Cookie中  

            $.get("/Common/GetWeiXinAccessToken", {}, function (data) {

                call && call(data);

            }, "json");//请求后台获取access_token  

            return;

        }
        call && call({});


    },

    wx_get_ticket: function (call) { //获取票据使用ajax的get请求获取签名  

        this.ticket = getcookie("ticket");

        if (!this.ticket) {

            $.get("/Common/GetWeiXinJsticket", {},
                    function (data) {
                        call && call(data);
                    }, "json");//请求获取调用jssdk的临时单据  

            return;
        }
        call && call({});


    },

    wx_get_sign: function (call) {  //获取签名  


        while (!this.config.appId || !this.ticket) {

            this.init();
        }

        $.post("/Common/GetWeiXinGetSignature", {//通过ajax请求后台获取签名  
            "noncestr": this.config.nonceStr,
            "ticket": this.ticket,
            "timestamp": this.config.timestamp,
            "url": location.href.split('#')[0]
        }, function (data) {

            call && call(data);

        }, "json");//请求获取调用jssdk的临时单据  


    },
    initWx: function (call, errorCall) {//初始化微信接口  

        var that = this;
        wx.config(this.config);//初始化微信配置  
        this.isReady = true;
        wx.ready(function () {

            this.isReady = true;

            if (that.readySuccessCall.length > 0) {

                $.each(that.readySuccessCall, function (i, n) {

                    n();
                })

            }

            call && call();

        });

        wx.error(function (res) {

            this.isReady = false;

            errorCall && errorCall();

        });


    }



}

//初始化  
wxShare.init();