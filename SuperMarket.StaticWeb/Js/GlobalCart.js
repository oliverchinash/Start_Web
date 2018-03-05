  
function Login() { 
    var email = $.trim($("#txtLoginID").val());
    var pwd = $.trim($("#txtRegPwd").val());
    var vercode = "";// $("#txt_LoginCode").val();
    if (email == "") {
        alert("登录E-mail地址或手机号不能为空!");
        $("#txtLoginID").focus();
        return false;
    }
    if (!email.isHandSet() && !email.isEmail()) {
        alert("请填写正确的E-mail地址或手机号！");
        $("#txtLoginID").focus();
        return false;
    }
    if (pwd == "") {
        alert("登录密码不能为空！");
        $("#txtRegPwd").focus();
        return false;
    }
    //if (vercode.length != 4) {
    //    alert("请输入验证码！");
    //    $("#txt_LoginCode").focus();
    //    return false;
    //}
  

    //$("#logauser").attr("disabled", "disabled");

    //登录中，请稍候...
    //$("#logmsg").html("<font color=red>登录中，请稍候...</font>");
    $.post("/Home/UserLogin", { Email: email, Password: pwd, VerCode: vercode }, function (data) {
        //$("#logauser").attr("disabled", ""); 
        var _returncode = (-data); 
        if (_returncode == _TheArray[0]) {  
            if ( typeof (LoginCallBack) == "function") {
                LoginCallBack();
            }
            else if (returnurl == null || returnurl == "" || returnurl.toLowerCase().indexOf("register") > -1) {
                window.location.href = "/Member";
            }
            else if (returnurl.indexOf("http://") == 0) {
                window.location.href = returnurl;
            }
            else {
                if (returnurl.indexOf("/") == 0) {
                    returnurl = returnurl.substring(1, returnurl.length);
                }
                if (returnurl.toLowerCase().indexOf("default.aspx") == 0) {
                    returnurl = "";
                }
                window.location.href = "/" + returnurl;
            }
        } 
        else {
            $("#logmsg").html(""); 
            alert(_TheArray[_returncode]);

            //alert(_TheArray[data]);
            //return false;
        }

    });

    //$("#logauser").attr("disabled", "");
    return false;
}

function LoginFun(usernameid,pwdid,vercodeid)
{
    var email = $.trim(usernameid);
    var pwd = $.trim(pwdid);

    var vercode = "";// $("#txt_LoginCode").val();
    if (vercodeid != "") {
        vercode = $("#txt_LoginCode").val();
    }
   if (email == "") {
        alert("账号不能为空!");
        //$("#" + usernameid).focus();
        return false;
    }
    //if (!email.isHandSet() && !email.isEmail()) {
    //    alert("请填写正确的E-mail地址或手机号！");
    //    $("#" + usernameid).focus();
    //    return false;
    //}
    if (pwd == "") {
        alert("登录密码不能为空！");
        //$("#" + pwdid).focus();
        return false;
    }
    //if (vercode.length != 4) {
    //    alert("请输入验证码！");
    //    $("#txt_LoginCode").focus();
    //    return false;
    //}
     
 
    $.post("/Home/UserLogin", { Email: email, Password: pwd, VerCode: vercode }, function (data) {
        $("#logauser").attr("disabled", "");
        var _returncode = (-data);
        if (_returncode == _TheArray[0]) {
            if (typeof (LoginCallBack)=="function")
            {
                LoginCallBack();
            }
          else  if (returnurl == null || returnurl == "") {
                window.location.href = "/Member";
            }
            else if (returnurl.indexOf("http://") == 0) {
                window.location.href = returnurl;
            }
            else {
                if (returnurl.indexOf("/") == 0) {
                    returnurl = returnurl.substring(1, returnurl.length);
                }
                if (returnurl.toLowerCase().indexOf("default.aspx") == 0) {
                    returnurl = "";
                }
                window.location.href = "/" + returnurl;
            }
        }
        else { 
            //alert(data);
            alert(_TheArray[_returncode]);
            return false;
        } 
    });   
}

function AddToCartCom(prodetailid,num)
{
    var shoppingCartDomain = $("#hidShopCartDomain").val(); 
    $.ajax({
        url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
        data: { Action: "AddToCart", prodetailid: prodetailid, Num: num },
        dataType: "jsonp",
        jsonp: "callback",
        success: function (data) {
            if (data.Status != "OK") {
                alert(data.Status);
                return;
            }
            GetCartCount();
        }
    }); 
    window.open('/Shopping/AddToCart?pdid=' + prodetailid + "&num=" + num, "_blank");
}

function AddToCartXuQiuCom(prodetailid, num) {
    var shoppingCartDomain = $("#hidShopCartDomain").val();
    $.ajax({
        url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
        data: { Action: "AddToXuQiu", prodetailid: prodetailid, Num: num },
        dataType: "jsonp",
        jsonp: "callback",
        success: function (data) {
            if (data.Status != "OK") {
                alert(data.Status);
                return;
            }
            GetCartXuQiuCount();
        }
    });
    window.open('/Shopping/AddToCart?pdid=' + prodetailid + "&num=" + num, "_blank");
}


//function AddToCartAndNum(prodetailid,num)
//{
//    var shoppingCartDomain = $("#hidShopCartDomain").val();

//    $.ajax({
//        url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
//        data: { Action: "AddToCart", prodetailid: prodetailid, Num: num },
//        dataType: "jsonp",
//        jsonp: "callback",
//        success: function (data) {
//            if (data.Status != "OK") {
//                alert(data.Status);
//                return;
//            }
//            GetCartCount();
//        }
//    });

//    window.open('/Shopping/AddToCart?pdid=' + prodetailid + "&num=" + num, "_blank");

//}