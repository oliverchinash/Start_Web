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
    var url ="";
    var jsonp = new JsonpUtil();
    jsonp.url = url + "/ShoppingHandler/ShoppingHandler";
    jsonp.param = "action=GetShoppingCartCount&callBack=GetCartCountCallBack";
    jsonp.process();
}

function GetCartCountCallBack(count) {
    if (count == "") {
        count = 0;
    }
    $("#CartCount").html(count); 
}



function FunOpenCanvas(divid) {
    $("#" + divid).offCanvas('open')
}
function FunCloseCanvas(divid)
{
    $("#" + divid).offCanvas('close')

}
function FunSearchByKey(pageindex) { 
    var key = $("#txtSearchkey").val();
    var clid = 0;
    var bdid = 0;
    if ($("#spannavclass").length>0) {
        clid = $("#spannavclass").html();
    }
    if ($("#spanselectbrand").length>0) {
        bdid = $("#spanselectbrand").html();
    } 
    location.href = "/MobileProduct/Search?cl=" + clid + "&bd=" + bdid + "&key=" + key;
}
 