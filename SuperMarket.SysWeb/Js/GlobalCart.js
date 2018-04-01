$(document).ready(function () {
    //NewCheckIsLogin();
    //GetCartCount();

    //$("#regChangeImg").click(function () {
    //    GetVerifyCode("regCodeImg", "Verify_Register");
    //    return false;
    //});

    //$("#regChangeImg_login").click(function () {
    //    GetVerifyCode_Login("regCodeImg_Login", "Verify_Login");
    //    return false;
    //});
});


String.prototype.isHandSet = function () {
    if (this == "") {
        return false;
    }

    var reg = /^1[3,4,5,8][0-9]{9,9}$/;
    return reg.test(this);
}
String.prototype.isEmail = function () {
    if (this == "") {
        return false;
    }
    var pattern1 = /^[_\.0-9a-zA-Z-]+[0-9a-zA-Z_]@([0-9a-zA-Z][0-9a-zA-Z-]*\.)+[a-zA-Z]{2,3}$/;
    if (this.match(pattern1)) {
        return true;
    }
    return false;
}
String.prototype.isPhone = function () {
    if (this == "") {
        return false;
    }
    var reg = /^0[\d]{2,3}[-]?[\d]{7,8}(-?[\d]{1,5})?$/;
    return reg.test(this);
}
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
        debugger
        if (data == -1) {
                 window.location.href = "/Check/index"; 
        } 
        else {
            $("#logmsg").html(""); 
            status = -data;
            alert(_TheArray[status]);
            return false;
        }

    });

    //$("#logauser").attr("disabled", "");
    return false;
}

function Register() {
    var email = $.trim($("#txtEMail").val()); 
    var pwd = $.trim($("#txtPassword").val());
    var repwd = $.trim($("#txtConfirmPwd").val());
    var txtcode = "";// $.trim($("#txt_Code").val()); 
    if (email === "") {
        alert("请输入手机号码或E-mail地址！");
        $("#txtEMail").focus();
        return false;
    }
    if (!email.isEmail() && !email.isHandSet()) {
        alert("你输入的手机号码或E-mail地址不符合规范！");
        $("#txtEMail").focus();
        return false;
    } 

    if (pwd.length < 6 || pwd.length > 12) {
        alert("你输入的密码不符合规范，请重新填写！！");
        $("#txtPassword").focus();
        return false;
    }
    if (pwd != repwd) {
        alert("两次输入的密码不一致，请重新填写！");
        $("#txtConfirmPwd").focus();
        return false;
    } 
    $.post("/Home/UserRegister", { Email: email, Password: pwd, VerCode: txtcode }, function (data) {
          var _returncode = (-data);
        if (_returncode == _TheArray[0]) {
            if (returnurl == "") {
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
        else  { 
            alert(_TheArray[_returncode]);
            return false;
        } 
    }); 
    return false;
}