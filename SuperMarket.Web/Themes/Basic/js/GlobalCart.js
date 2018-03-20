
  
var offsetcart  ;// $(".am-icon-shopping-cart").offset();//购物车要飞入的目的地
var clickcartevent;  

 
function GetCartCount() {
    var methodname = "GetShoppingCartCount";
    if ($("#spanjishisong").length > 0) {
        var js = $("#spanjishisong").html();
        if(js==1)
        {
            methodname = "GetShoppingCartXuQiuCount";
        }
    }
    var url = "";
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
  
function AddToCartCom(prodetailid)
{
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            var shoppingCartDomain = "";
            $.ajax({
                url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
                data: { Action: "AddToCart", prodetailid: prodetailid, Num: 1 },
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
        }
        else {
            FunRedirectLogin();
        }
    }); 
}


function AddToCartCom(prodetailid, num) {
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            var shoppingCartDomain = "";
            $.ajax({
                url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
                data: { Action: "AddToCart", prodetailid: prodetailid, Num: num },
                dataType: "jsonp",
                jsonp: "callback",
                success: function (data) {
                    if (data.Status != "OK") {
                        alert(data.Status);
                        return;
                    } else {
                        if (typeof (CartFly) == "function") {
                            CartFly();
                        }
                    }
                    GetCartCount();
                }
            });
        }
        else {
            FunRedirectLogin();
        }
    });
}

function AddToXuQiuCom(prodetailid, num) {
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            var shoppingCartDomain = "";
            $.ajax({
                url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
                data: { Action: "AddToXuQiu", prodetailid: prodetailid, Num: num },
                dataType: "jsonp",
                jsonp: "callback",
                success: function (data) {
                    if (data.Status != "OK") {
                        alert(data.Status);
                        return;
                    } else {
                        if (typeof (CartFly) == "function") {
                            CartFly();
                        }
                    }
                    GetCartCount();
                }
            });
        }
        else {
            FunRedirectLogin();
        }
    });
}

function AddToCartClick() {
    $(".clsaddtocart").each(function () {
        var objthis = this;
        $(objthis).on("click", function (event) {
            offsetbeginx = event.pageX;
            offsetbeginy = event.pageY;
            clickcartevent = event;
            var prodetailid = $(this).attr("ProductDetailId");
            var jishi = $(this).attr("jishi");
            var num = $("#txtProductNum" + prodetailid).val();
            if (num == 0) num = 1;
            if (jishi == 1)//及时送产品
            { 
                AddToXuQiuCom(prodetailid, num);
            }
            else { 
                AddToCartCom(prodetailid, num);
            }
        });
        $(objthis).removeClass("clsaddtocart");
    });  
}
function CartFly() {
    var scrollHeight = document.body.scrollHeight;
    var flyer = $('<img class="u-flyer" src="/themes/basic/img/cart_icon.png"/>');
    var kuan = $(window).width() / 2;
    flyer.fly({
        start: {
            left: clickcartevent.pageX - kuan,
            top: clickcartevent.clientY
        },
        end: {
            left: offsetcart.left,
            top: 10,
            width: 0,
            height: 0,
        },
    });
}
