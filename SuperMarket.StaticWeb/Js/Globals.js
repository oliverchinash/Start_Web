$.ajaxSetup({ cache: false });
$.ajaxSetup({})
function JsonpUtil() {
    this.url = '';
    this.param = '';
    this.process = function () {
        var js = document.createElement('script');
        js.type = 'text/javascript';
        js.src = this.url+'?roid=' + Math.random() + '&' + this.param;
        document.getElementsByTagName('head')[0].appendChild(js);
    }
}

function myescape(str) {
    return escape(str).replace("+", "%2B");
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

String.prototype.isHandSet = function () {
    if (this == "") {
        return false;
    }

    var reg = /^1[3,4,5,6,7,8,9][0-9]{9,9}$/;
    return reg.test(this);
}


String.prototype.isEmail = function () {
    if (this == "") {
        return false;
    }
    var pattern1 = /^[_\.0-9a-zA-Z-]+[0-9a-zA-Z_]@([0-9a-zA-Z][0-9a-zA-Z-]+\.)+[a-zA-Z]{2,3}$/;
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

//检查身份证
String.prototype.isIdnumber = function () {
    if ($.trim(this) == '') return true;
    var s = $.trim(this).toUpperCase();
    if (!/^[1-9]\d{16}[0-9X]$/.test(s)) return false;

    var factors = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
    var check = 0;
    for (i = 0; i < 17; i++) {
        check += parseInt(s.charAt(i)) * factors[i];
    }

    check = 12 - check % 11;
    switch (check) {
        case 10:
            check = 'X';
            break;
        case 11:
            check = 0;
            break;
        case 12:
            check = 1;
            break;
    }
    return s.charAt(17) == check;
}

String.prototype.isInt = function () {
    if ($.trim(this) == '') return true;
    if (/^[+-]?\d+$/.test($.trim(this))) {
        return true;
    }
    return /^[+-]?\d{1,3}(,\d{3})*$/.test($.trim(this));
}

String.prototype.isNumber = function () {
    if ($.trim(this) == '') return true;
    if (/^[+-]?\d+(\.\d+)?$/.test($.trim(this))) {
        return true;
    }
    return /^[+-]?\d{1,3}(,\d{3})*(\.\d+)?$/.test($.trim(this));
}

function   OpenModal(url,name,iWidth,iHeight) {
    var url; //转向网页的地址;
    var name; //网页名称，可为空;
    var iWidth; //弹出窗口的宽度;
    var iHeight; //弹出窗口的高度;
    var iTop = (window.screen.availHeight-30-iHeight)/2; //获得窗口的垂直位置;
    var iLeft = (window.screen.availWidth-10-iWidth)/2; //获得窗口的水平位置;
    window.open(url, '', 'height=' + iHeight + ',,innerHeight=' + iHeight + ',width=' + iWidth + ',innerWidth=' + iWidth + ',top=' + iTop + ',left=' + iLeft + ',toolbar=no,menubar=no,scrollbars=no,location=no,resizeable=no,status=no');
}