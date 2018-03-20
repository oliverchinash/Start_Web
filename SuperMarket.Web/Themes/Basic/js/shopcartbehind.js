/**
 * Created by jc100 on 2017/4/19.
 */

/*************关于输入框加减特效***************/
(function($){
    var functions = {
        init:function(input, step, max, min, digit){
            var width = input.width()-3;
            var height = input.width()/4;
            var _this = this;
            width += 3;

            input.attr("readonly", "readonly");
            //设置不分样式
            input.css("border", "none");
            input.css("width", width-height*2-2);
            input.css("height", height);
            input.css("padding", "0px");
            input.css("margin", "0px");
            input.css("text-align", "center");
            input.css("vertical-align", "middle");
            input.css("line-height", height + "px");


            //添加左右加减号
            input.wrap("<div style = 'overflow:hidden;width:" + width + "px;height:" + height + "px;border: 1px solid #CCC;'></div>");
            input.before("<div id = '" + input.attr('id') + "l'  onselectstart = 'return false;' style = '-moz-user-select:none;cursor:pointer;text-align:center;width:" + height + "px;height:" + height + "px;line-height:" + height + "px;border-right: 1px solid #CCC;float:left'>-</div>");
            input.after("<div id = '" + input.attr('id') + "r'  onselectstart = 'return false;' style = '-moz-user-select:none;cursor:pointer;text-align:center;width:" + height + "px;height:" + height + "px;line-height:" + height + "px;border-left: 1px solid #CCC;float:right'> + </div>");
            //左右调用执行
            $("#" + input.attr('id') + "l").click(function(){
                _this.execute(input, step, max, min, digit, true);
            });
            $("#" + input.attr('id') + "r").click(function(){
                _this.execute(input, step, max, min, digit, false);
            });
        },
        execute:function(input, step, max, min, digit, _do){
            var val = parseFloat(this.format(input.val(), digit));
            var ori = val;
            if(_do) val -= step;
            if(!_do) val += step;
            if(val<min){
                val  =  min;
            }else if(val>max){
                val  =  max;
            }
            input.val(this.format(val, digit));
            EvaluateMoney();
        },
        format:function(val, digit){
            if(isNaN(val)){
                val = 1;
            }
            return parseFloat(val).toFixed(digit);
        }
    };


    $(function(){
        //使用控件必须有以下属性或者引用alignment类
        var inputs = $("input[user_data], input[data_digit], input[data_step], input[data_min], input[data_max], input.alignment");
        inputs.each(function(){
            //预设值数据选择
            var data = {
                default_data : 	{"step" : 1, "min" : 1, "max" : 9999, "digit" : 0},
                aaa : 			{"step" : 10, "min" : 5, "max" : 20, "digit" :0},
            }

            var user_data = eval("data." + $(this).attr("user_data"));
            if(user_data == null){
                user_data = data.default_data;
            }

            var digit = $(this).attr("data_digit");
            if(digit != null&&!isNaN(parseFloat(digit))){
                digit  =  parseFloat(digit).toFixed(0);
                user_data.digit = parseFloat(digit);
            }

            var step = $(this).attr("data_step");
            if(step != null &&!isNaN(parseFloat(step))){
                user_data.step = parseFloat(step);
            }
            var min = $(this).attr("data_min");
            if(min != null &&!isNaN(parseFloat(min))){
                user_data.min = parseFloat(min);
            }

            var max = $(this).attr("data_max");
            if(max != null &&!isNaN(parseFloat(max))){
                user_data.max = parseFloat(max);
            }
            //自动装载
            functions.init($(this), user_data.step, user_data.max, user_data.min, user_data.digit);

            var data_edit = $(this).attr("data_edit");
            if(data_edit){
                $(this).attr("readonly",null);
            }
        });
    })
})(jQuery);


/*****************购物车页面的编辑完成特效*******************/
$(document).ready(function () {
    $(".shoping-bianji").click(function () {
        $(this).hide();
        $(".shoping-wancheng").show();
        $(".shoping-navbar-li2").hide();
        $(".shoping-navbar-li3").show();
    })

})

$(document).ready(function () {
    $(".shoping-wancheng").click(function () {
        $(this).hide();
        $(".shoping-bianji").show();
        $(".shoping-navbar-li2").show();
        $(".shoping-navbar-li3").hide();
    })

})
//购物车跟后台的交互
function CheckUpdateShoppingCart(prodetailid, checkstatus) {
    var shoppingCartDomain = "";
    $.ajax({
        url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
        data: { Action: "UpdateCartCheck", prodetailid: prodetailid, checkstatus: checkstatus },
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
function EvaluateMoney() {
    var totalprice = 0.00;
    var totalnum = 0.0;
    $(".shoping-checkbox").each(function () {
        var pdid = $(this).attr("productdetailid");
        var numobj = $(this).parent().parent().find(".alignment");
        var price = $(this).attr("price");
        var num = new Number($(numobj).val());
        var subtotalprice = price * num;
        if ($(this).is(':checked')) {
            totalnum += num;
            totalprice += subtotalprice;
        }
    });
    $("#spancount").html(totalnum);
    $("#spanTotalPrice").html(totalprice.toFixed(2));
}
function FunCheckBoxAllBind() {
    var checkselect = true;
    $(".shoping-checkbox").each(function () {
        if (!$(this).is(':checked')) {
            checkselect = false;
        }
    });
    $(".shoping-chekbox-all").prop("checked", checkselect);
}
function ToSettle() {
    var productdetails = "";
    $(".shoping-checkbox").each(function () {
        if ($(this).is(':checked')) {
            var pdid = $(this).attr("productdetailid");
            var numobj = $(this).parent().parent().find(".alignment");
            var num = new Number($(numobj).val());
            if (isNaN(num) || num == undefined || num < 1)
                num = 1;
            if (productdetails == "")
                productdetails += pdid + "_" + num;
            else
                productdetails += "|" + pdid + "_" + num;
        }
    });
    if (productdetails == "") {
        $("#perrormsg").html("您好，需要您点选商品哦!");
    }
    else {
        $.post("/Order/CreatePreOrder", { productdetails: productdetails }, function (data) {
            var _returncode = data;
            if (_returncode == "" || _returncode == "0") {
                $("#perrormsg").html("该产品已下架！");
            }
            else {
                var js = 0;
                if ($("#spanjishisong").length > 0)
                {
                    js = $("#spanjishisong").html();
                }
             

                location.href = "/MobileCart/PreOrder?js="+js+"&code=" + _returncode;
      
                return false;
            }
        });
    }
}

function DeleteShoppingCart() {
    $(".shoping-checkbox").each(function () {
        if ($(this).is(':checked')) {
            var liobj = $(this).parent().parent();
            var pdid = $(this).attr("productdetailid");
            var num = 0;
            var shoppingCartDomain = "";
            $.ajax({
                url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
                data: { Action: "DelFromCart", prodetailid: pdid, Num: num },
                dataType: "jsonp",
                jsonp: "callback",
                success: function (data) {
                    if (data.Status != "OK") {
                        $("#perrormsg").html(data.Status);
                        return;
                    }
                    GetCartCount();
                    $(liobj).remove();
                    EvaluateMoney();
                }
            });
        }
    });

}

function FunDeleteShoppingCarts() {
    var pdids = "";
    $(".shoping-checkbox").each(function () {
        if ($(this).is(':checked')) {
            pdids += "," + $(this).attr("productdetailid");
        }
    });
    var js = 0;
    if ($("#spanjishisong").length > 0) {
        js = $("#spanjishisong").html();
    }
    var actionname = "delpdsfromcart";
    if (js == 1) actionname = "delpdsfromcartxuqiu"
    var shoppingCartDomain ="";
    $.ajax({
        url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
        data: { Action: actionname, pdids: pdids },
        dataType: "jsonp",
        jsonp: "callback",
        success: function (data) {
            if (data.Status != "OK") {
                alert(data.Status);
                return;
            }
            GetCartCount();
            $(".shoping-checkbox").each(function () {
                if ($(this).is(':checked')) { 
                    var liobj = $(this).parent().parent();
                    $(liobj).remove();
                }
            }); 
            EvaluateMoney();
        }
    }) 
}
function DeleteShoppingCartsToFavorite() {
    var pdids = "";
    $(".shoping-checkbox").each(function () {
        if ($(this).is(':checked')) {
            pdids += "," + $(this).attr("productdetailid");
        }
    });
    var shoppingCartDomain = "";
    $.ajax({
        url: shoppingCartDomain + "/ShoppingHandler/ShoppingHandler",
        data: { Action: "delpdsfromcart", pdids: pdids },
        dataType: "jsonp",
        jsonp: "callback",
        success: function (data) {
            if (data.Status != "OK") {
                alert(data.Status);
                return;
            }
            GetCartCount();
            $(".shoping-checkbox").each(function () {
                if ($(this).is(':checked')) {
                    var liobj = $(this).parent().parent();
                    $(liobj).remove();
                    var pdid=  $(this).attr("productdetailid");
                    FunFavoritesAdd(pdid);
                }
            });
            EvaluateMoney();
        }
    })
   

}




$(function () {
    $(".shoping-chekbox-all").change(function () {
        if ($(this).is(':checked')) {
            $(".shoping-checkbox").each(function () {
                if (!$(this).is(':checked')) {
                    CheckUpdateShoppingCart($(this).attr("productdetailid"), 1);
                }
                $(this).prop("checked", true);
            });
        }
        else {
            $(".shoping-checkbox").each(function () {
                if ($(this).is(':checked')) {
                    CheckUpdateShoppingCart($(this).attr("productdetailid"), 0);
                }
                $(this).removeAttr("checked");
                $(this).prop("checked", false);
            });
        }
        EvaluateMoney();
    });

    $(".shoping-checkbox").change(function () {
        if ($(this).is(':checked')) {
            CheckUpdateShoppingCart($(this).attr("productdetailid"), 1);
        }
        else {
            CheckUpdateShoppingCart($(this).attr("productdetailid"), 0);
        }
        FunCheckBoxAllBind();
        EvaluateMoney();
    });
    FunCheckBoxAllBind();
    EvaluateMoney();
});


