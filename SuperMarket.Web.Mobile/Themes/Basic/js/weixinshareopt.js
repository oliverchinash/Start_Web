$(function () {
    wxShare.shareApi = function (shareList) {

        if (wxShare.isReady) {//判断微信config是否被初始化  

            if (shareList.onMenuShareTimeline) {//分享到朋友圈  

                var timelineParameter = shareList.onMenuShareTimeline;//获取传入的参数  

                wx.onMenuShareTimeline({//调用微信接口  

                    title: timelineParameter.title, // 分享标题  
                    link: timelineParameter.link, // 分享链接  
                    imgUrl: timelineParameter.imgUrl, // 分享图标  
                    success: function () {
                        // 用户确认分享后执行的回调函数  

                        timelineParameter.success && timelineParameter.success();

                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数  

                        timelineParameter.cancel && timelineParameter.cancel();


                    }
                });


            } if (shareList.onMenuShareAppMessage) {//分享给朋友  

                var appMessageParameter = shareList.onMenuShareAppMessage;//获取传入的参数  

                wx.onMenuShareAppMessage({
                    title: appMessageParameter.title, // 分享标题  
                    desc: appMessageParameter.desc, // 分享描述  
                    link: appMessageParameter.link, // 分享链接  
                    imgUrl: appMessageParameter.imgUrl, // 分享图标  
                    type: appMessageParameter.type, // 分享类型,music、video或link，  
                    //不填默认为link  
                    dataUrl: appMessageParameter.dataUrl, // 如果type是music或video，  
                    //则要提供数据链接，默认为空  
                    success: function () {
                        alert(appMessageParameter.title);
                        // 用户确认分享后执行的回调函数  
                        appMessageParameter.success && appMessageParameter.success();

                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数  

                        appMessageParameter.cancel && appMessageParameter.cancel();
                    }
                });


            } if (shareList.onMenuShareQQ) {//分享给qq  

                var shareQQParameter = shareList.onMenuShareQQ;//获取传入的参数  

                wx.onMenuShareQQ({
                    title: shareQQParameter.title, // 分享标题  
                    desc: shareQQParameter.desc, // 分享描述  
                    link: shareQQParameter.link, // 分享链接  
                    imgUrl: shareQQParameter.imgUrl, // 分享图标  
                    success: function () {
                        // 用户确认分享后执行的回调函数  
                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数  
                    }
                });


            }





        } else {

            //初始化  
            wxShare.init();
            wxShare.isReady = true;

            console.log("系统提示,微信分享接口调用失败");
        }

    }


    //成功初始化后执行api 分享事务  
    wxShare.readySuccessCall.push(function () {
        var title = "易店心-国内最专业的汽车零部件及相关用品的在线采购网站",
           link = window.location.href,
            imgUrl = "http://m.aahama.com/Themes/Basic/img/ahamatitle.jpg",//分享接口显示的图片  
            desc = "分享接口测试",
            success = function () {
                alert("系统提示", "分享成功1", false);
            },
            cancel = function () {
                alert("系统提示", "分享取消分享", false);
            };

        var inp_title = $("input[name='title']").val();
        if (inp_title) {
            title = inp_title;
        }

        var inp_imgUrl = $("input[name='imgUrl']").val();
        if (inp_imgUrl) {

            imgUrl = inp_imgUrl;
        }

        var inp_desc = $("input[name='desc']").val();
        if (inp_desc) {

            desc = inp_desc;
        }

        wxShare.shareApi({
            onMenuShareTimeline: {//分享到朋友圈  
                title: title, // 分享标题  
                link: link, // 分享链接  
                imgUrl: imgUrl, // 分享图标  
                success: function () {
                    success();

                },
                cancel: function () {
                    cancel();

                }
            },
            onMenuShareAppMessage: {//分享给朋友  
                title: title, // 分享标题  
                desc: desc, // 分享描述  
                link: link, // 分享链接  
                imgUrl: imgUrl, // 分享图标  
                type: "link", // 分享类型,music、video或link，不填默认为link  
                dataUrl: "", // 如果type是music或video，则要提供数据链接，默认为空  
                success: function () {
                    success();
                },
                cancel: function () {
                    cancel();
                }
            },
            onMenuShareQQ: {//分享给qq  
                title: title, // 分享标题  
                desc: desc, // 分享描述  
                link: link, // 分享链接  
                imgUrl: imgUrl, // 分享图标  
                success: function () {
                    success();
                },
                cancel: function () {
                    cancel();
                }
            },
            onMenuShareWeibo: {
                title: title, // 分享标题  
                desc: desc, // 分享描述  
                link: link, // 分享链接  
                imgUrl: imgUrl, // 分享图标  
                success: function () {
                    success();
                },
                cancel: function () {
                    cancel();
                }
            }
        });
    });

});