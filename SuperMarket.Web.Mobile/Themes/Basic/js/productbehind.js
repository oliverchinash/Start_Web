function webTab(sel, conSel, current) {
    $(sel).click(function () {
        $(this).addClass(current).siblings().removeClass(current);	//给li增加高亮
        //获取当前的索引
        var _index = $(this).index(sel);
        $("." + conSel).eq(_index).show().siblings("." + conSel).hide();
    });
}

function FavoritesAddCallback(pdid)
{
    if (pdid>0)
    { 
        $(".details-navbar-li").hide();
        $(".details-navbar-li4").show();
    } 
}
function FavoritesCancelCallback(pdid) {
    if (pdid>0) {
        $(".details-navbar-li4").hide();
        $(".details-navbar-li").show();
    }
}

function FunAddToCartProduct()
{
    var pdid = $("#txtProductDetailId").val();
    var num = $("#txtProductNum" + pdid).val();
    if (num == 0 || num == "" || num==undefined) num = 1;
    AddToCartCom(prodetailid, num);
}
function FunBuyImmediate() {
    var pdid = $("#txtProductDetailId").val();
    var num = $("#txtProductNum" + pdid).val();
    if (num == 0 || num == "" || num == undefined) num = 1;
    location.href = "/MobileCart/ShopCart?pids=" + pdid + "&nums=" + num;
}
function FunBuyImmediateXuQiu() {
    var pdid = $("#txtProductDetailId").val();
    var num = $("#txtProductNum" + pdid).val();
    var jishi = $("#spanjishisong").html();
    if (num == 0 || num == "" || num == undefined) num = 1;
    location.href = "/MobileCart/ShopCart?js=" + jishi + "&pids=" + pdid + "&nums=" + num;
}

$(function () { 
    $.post("/Home/CheckMemberHasLogin", {
    }, function (checklogindata) {
        if (checklogindata == "1") {
            var productdetailid = $("#txtProductDetailId").val();
            $.post("/Member/HasAddToFavorite", {
                pdid: productdetailid
            }, function (data) {
                var _returnsult = eval("(" + data + ")");
                var status = -_returnsult.Status;
                if (status == _TheArray[0]) {
                    var fanum = _returnsult.Obj;
                    if (fanum>0) {
                        $(".details-navbar-li").hide();
                        $(".details-navbar-li4").show();
                    }
                }
            });
        } 
    });
});