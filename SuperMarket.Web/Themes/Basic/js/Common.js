$(function () {
    $('.am-goback').click(function () { 
        window.history.go(-1);
        return false;
    });
});

$.ajaxSetup({ cache: false });
$.ajaxSetup({})
function JsonpUtil() {
    this.url = '';
    this.param = '';
    this.process = function () {
        var js = document.createElement('script');
        js.type = 'text/javascript';
        js.src = this.url + '?roid=' + Math.random() + '&' + this.param;
        document.getElementsByTagName('head')[0].appendChild(js);
    }
}

String.prototype.isHandSet = function () {
    if (this == "") {
        return false;
    }

    var reg = /^1[3,4,5,6,7,8,9][0-9]{9,9}$/;
    return reg.test(this);
}


function getcookie(name) {
    var arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
    if (arr != null) {
        return decodeURIComponent(arr[2]);
    } else {
        return "";
    }
}

function setcookie(name, value) {
    var Days = 30;
    var exp = new Date();
    exp.setTime(exp.getTime() + Days * 24 * 60 * 60 * 1000);
    document.cookie = name + "=" + encodeURIComponent(value) + ";expires=" + exp.toGMTString();
}

function NewCheckIsLogin() {
    var url = "";
    var jsonp = new JsonpUtil();
    jsonp.url = url + "/ShoppingHandler/ShoppingHandler";
    jsonp.param = "action=GetDefaultCusName&callBack=NewCheckIsLoginCallBack";
    jsonp.process();

}

function NewCheckIsLoginCallBack(str) {
    if (str != "") { 
        $("#aheadlogin").html('欢迎'); 
    }
}
function GetCartCount() {
    var url = "";
    var methodname = "GetShoppingCartCount";
    if ($("#spanjishisong").length > 0) {
        var js = $("#spanjishisong").html();
        if (js == 1) {
            methodname = "GetShoppingCartXuQiuCount";
        }
    }
    var jsonp = new JsonpUtil();
    jsonp.url = url + "/ShoppingHandler/ShoppingHandler";
    jsonp.param = "action=" + methodname + "&callBack=GetCartCountCallBack";
    jsonp.process();
}

function GetCartCountCallBack(count) {
    if (count == "") {
        count = 0;
    }
    $("#CartCount").html(count); 
}



function FunOpenCanvas(divid) {
    $("#" + divid).offCanvas('open');
}
function FunCloseCanvas(divid)
{
    $("#" + divid).offCanvas('close');
}

function FunRedirectLogOut() {
    var loginurl = "";
    if ($("#hidLoginWebUrl").length > 0) {
        loginurl = $("#hidLoginWebUrl").val();
    } 
    var returnurl = location.href;
    location.href = loginurl + "/Home/LogOut?returnurl=" + encodeURIComponent(returnurl);
    
}
function FunRedirectLogin() {
    var loginurl = "";
    if ($("#hidLoginWebUrl").length > 0) {
        loginurl = $("#hidLoginWebUrl").val();
    } 
        var returnurl = location.href;
        location.href = loginurl + "/Home/Login?returnurl=" + encodeURIComponent(returnurl);
    
}

function FunRedirectRegister() {
    var loginurl = "";
    if ($("#hidLoginWebUrl").length > 0) {
        loginurl = $("#hidLoginWebUrl").val();
    } 
        var returnurl = location.href;
        location.href = loginurl + "/Home/Reg?returnurl=" + encodeURIComponent(returnurl); 
}


function FunSearchByKey(pageindex) {
    var key = $("#txtSearchkey").val();
    var clid = 0;
    var bdid = 0;
    var js = 0;
    if ($("#spannavclass").length > 0) {
        clid = $("#spannavclass").html();
    }
    if ($("#spanselectbrand").length > 0) {
        bdid = $("#spanselectbrand").html();
    }
    //if ($("#spanjishisong").length > 0) {
    //    js = $("#spanjishisong").html();
    //}
    location.href = "/MobileProduct/Search?js=" + js + "&cl=" + clid + "&bd=" + bdid + "&key=" + key;
}

//添加收藏
function FunFavoritesAdd(productdetailid) {
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            $.post("/Member/FavoriteAdd", {
                pdid: productdetailid, sysid: 4
            }, function (data) {
                var _returnsult = eval("(" + data + ")");
                var status = -_returnsult.Status;
                if (status == _TheArray[0]) {
                    if (typeof (FavoritesAddCallback) == "function") {
                        FavoritesAddCallback(true);
                    }
                }
            });
        }
        else {
            FunRedirectLogin();
        }
    });
}

function FunFavoritsCancel(productdetailid) {
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            $.post("/Member/FavoriteCancel", {
                pdids: productdetailid
            }, function (data) {
                var _returnsult = eval("(" + data + ")");
                var status = -_returnsult.Status;
                if (status == _TheArray[0]) {
                    if (typeof (FavoritesCancelCallback) == "function") {
                        FavoritesCancelCallback(productdetailid);
                    }
                }
            });
        }
        else {
            FunRedirectLogin();
        }
    });
}
function FunFavoritsCancelAll() {
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            $.post("/Member/FavoriteCancelAll", {
            }, function (data) {
                var _returnsult = eval("(" + data + ")");
                var status = -_returnsult.Status;
                if (status == _TheArray[0]) {
                    if (typeof (FavoritesCancelAllCallback) == "function") {
                        FavoritesCancelAllCallback(productdetailid);
                    }
                }
            });
        }
        else {
            FunRedirectLogin();
        }
    });
}

var storeid = 0;
var storenum = 0;
function ScrollRegisterMembers() {
    for (storenum = 0; storenum < 8; storenum++) {
        GetStoreMsgToScroll();
    }

}
function GetCJAmount() {
    $.ajax({
        url: "/Common/GetCJAmount",
        type: "POST",
        dataType: 'json',
        success: function (json) {

            var jsonobj = json;
            var status = -jsonobj.Status;

            if (status == _TheArray[0]) {
                var tradeobj = jsonobj.Obj;
                $("#spanchengjiao").html(tradeobj.TradeAmount);
            }
        }
    });
}
function GetStoreMsgToScroll() {
    $.ajax({
        url: "/Common/GetNextStore",
        type: "POST",
        async: false,
        dataType: 'json',
        data: { preid: storeid },
        success: function (json) {//客户端jquery预先定义好的callback函数,成功获取跨域服务器上的json数据后,会动态执行这个callback函数
            if (json != null && json != "") {
                var objjson = eval(json);
                var firstul = $("#deal-scroll").find("ul:first");
                var namestore = "";
                if (objjson.CompanyName.length > 4) {
                    namestore = objjson.CompanyName.substring(0, objjson.CompanyName.length / 2 - 1);
                    namestore += "****";
                    namestore += objjson.CompanyName.substring(objjson.CompanyName.length / 2 + 1, objjson.CompanyName.length);
                }
                else if (objjson.CompanyName.length > 2) {
                    namestore = objjson.CompanyName.substring(0, objjson.CompanyName.length / 2);
                    namestore += "****";
                    namestore += objjson.CompanyName.substring(objjson.CompanyName.length / 2 + 1, objjson.CompanyName.length);
                }
                else if (objjson.CompanyName.length == 2) {
                    namestore = objjson.CompanyName.substring(0, 1);
                    namestore += "****";
                    namestore += objjson.CompanyName.substring(1, objjson.CompanyName.length);
                }
                if (objjson.CompanyName.length >= 2) {
                    $(firstul).append("<li><span>" + namestore + "</span></li>");
                    storeid = objjson.StoreId;
                    if (storenum < 8) {
                        storenum++;
                    }
                    else {
                        $(firstul).find("li:first").remove();
                    }
                }
            }
            else {
                storeid = 0;
            }
        },
        complete: function (data, status) {
        }
    });
}

