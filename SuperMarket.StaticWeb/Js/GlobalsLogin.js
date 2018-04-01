

function NewCheckIsLogin() {
    debugger
    var url = $("#hidMainCommonDomain").val();
    alert(url);
    var jsonp = new JsonpUtil();
    jsonp.url = url + "/CommonHandler/CommonHandler";
    jsonp.param = "action=GetDefaultCusName&callBack=NewCheckIsLoginCallBack";
    jsonp.process();
    
}

function NewCheckIsLoginCallBack(str) {
    alert(str);
    if (str != "") {
        var shopcartDomain = $("#hidMainCommonDomain").val();
        $("#logininfo").html('<span>' + str + ',欢迎您 </span> <a style="text-decoration:none;cursor:pointer;" onclick="FunRedirectLogOut()" target=_blank><font color=#999999>退出</font></a>');
     }
}
  
$(function () { 
    NewCheckIsLogin(); 
}) 

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