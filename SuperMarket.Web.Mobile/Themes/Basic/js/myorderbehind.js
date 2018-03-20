
function FunSearchOrderList(obj)
{
    var k = $("#txtKey").val();
    var s = $(obj).attr("Status");
    var t = $("#txtTerm").val();
    var os = $("#txtOrderStyle").val();
    var url = "/MobileMember/OrderList?os=" + os + "&t=" + t + "&s=" + s + "&k=" + k;
    location.href = url;
} 
$(function () {

    $("#ulcancelreason li").click(function () {
        $(this).addClass("active").siblings().removeClass("active");
        $("#divCancelReason").val($(this).attr("name"));
    });


    // 页数
    var page = 1;
    var maxpage = $("#spanmaxpageindex").html();
    var k = $("#txtKey").val();
    var s = $("#txtStatus").val();
    var t = $("#txtTerm").val();
    var os = $("#txtOrderStyle").val();

    // dropload
    $(".dicOrderListDropUp").dropload({
        scrollArea: window,
        domDown: {
            domClass: 'dropload-down',
            domRefresh: '<div class="dropload-refresh">↑上拉加载更多</div>',
            domLoad: '<div class="dropload-load"><span class="loading"></span>加载中....</div>',
            domNoData: '<div class="dropload-noData">没有更多数据了</div>'
        },
        loadDownFn: function (me) {
            page++;
            var result = '';
            $.ajax({
                type: 'GET',
                url: '/MobileMember/GetOrderListJson',
                data: {os:os, k: k, s: s, t: t, pageindex: page },
                dataType: 'json',
                success: function (data) { 
                    if (page <= maxpage) {
                        var objlist = data.List;
                        var myTemplate = Handlebars.compile($("#divOrderList-template").html());
                        //注册一个比较大小的Helper,判断v1是否大于v2
                        Handlebars.registerHelper("compare", function (v1, v2, options) {
                            if (v1 == v2) {
                                return options.fn(this);
                            } else {
                                return options.inverse(this);
                            }
                        });
                        Handlebars.registerHelper("transformat", function (value) {
                            return value.toFixed(2);
                        });
                   
                           // 为了测试，延迟1秒加载
                        setTimeout(function () {
                            // 插入数据到页面，放到最后面
                            $('.myorder-all').append(myTemplate(objlist));
                            // 每次数据插入，必须重置
                            me.resetload();
                        }, 1000);
                    } else {
                        // 锁定
                        me.lock();
                        // 无数据
                        me.noData();
                        me.resetload();
                    }
                     
                },
                error: function (xhr, type) {
                    alert('Ajax error!');
                    // 即使加载出错，也得重置
                    me.resetload();
                }
            });
        },
        threshold: 50
    });
});

function FunOrderCancelOpen(ordercode)
{
    $("#divCancelOrderCode").val(ordercode);
    FunOpenCanvas('divCancelReasonCanvas');
}
//取消订单
function FunOrderCancel()
{
    var cancelcode = $("#divCancelOrderCode").val();
    var cancelreson = $("#divCancelReason").val();
    $.post("/Order/OrderCancel", {
        code: cancelcode, reason: cancelreson
    }, function (data) {
        var _returncode = eval("(" + data + ")");
        var status = -_returncode.Status;
        if (status == _TheArray[0]) {
            var vworder = _returncode.Obj;
            BindListObj(vworder.Code, vworder);
           
            //var myTemplate = Handlebars.compile($("#divOrderSub-template").html());
            ////注册一个比较大小的Helper,判断v1是否大于v2
            //Handlebars.registerHelper("compare", function (v1, v2, options) {
            //    if (v1 == v2) {
            //        return options.fn(this);
            //    } else {
            //        return options.inverse(this);
            //    }
            //});
            //Handlebars.registerHelper("transformat", function (value) {
            //    return value.toFixed(2);
            //});   
            //$(".myorder-all>div[OrderCode='" + vworder.Code + "']").replaceWith(myTemplate(vworder));
            FunCloseCanvas('divCancelReasonCanvas');
            //location.href = location.href;
            return false;
        }
        else {
            $("#ordercancelerrormsg").html(_TheArray[status]);
            return false;
        }

    });
}

//继续购买
function BugFromOrder(ordercode)
{
    debugger
    var products = "";
    var nums = "";
    var orderstyle = $(".myorder-all>div[OrderCode='" + ordercode + "']").attr("OrderStyle");
    $(".myorder-all>div[OrderCode='" + ordercode + "']>ul>li.liproduct").each(function () {
        if (products == "") {
            products += $(this).attr("prodetailid");
            nums += $(this).attr("num");
        }
        else {
            products += "_" + $(this).attr("prodetailid");
            nums += "_" + $(this).attr("num");
        } 
    });
    if (products != "" && nums!="")
    {
        if (orderstyle == 2)//需求订单,需求订单由及时送产品产生
        {
            location.href = "/MobileCart/ShopCart?js=1&pids=" + products + "&nums=" + nums; 
        }
        else { 
        location.href = "/MobileCart/ShopCart?pids=" + products + "&nums=" + nums;
        }
    }
}
//确认收货
function OrderRecivedFun(code) {
    $.post("/Order/OrderRecived", {
        code: code
    }, function (data) {
        var _returncode = eval("(" + data + ")");
        var status = -_returncode.Status;
        if (status == _TheArray[0]) {
            var vworder = _returncode.Obj;
            BindListObj(vworder.Code, vworder);

            return false;
        }
        else {
            alert(_TheArray[status]);
            return false;
        }

    });
}
//绑定列表修改后的单个内容
function BindListObj(code,obj)
{
    var vworder = obj;
    var myTemplate = Handlebars.compile($("#divOrderSub-template").html());
    //注册一个比较大小的Helper,判断v1是否大于v2
    Handlebars.registerHelper("compare", function (v1, v2, options) {
        if (v1 == v2) {
            return options.fn(this);
        } else {
            return options.inverse(this);
        }
    });
    Handlebars.registerHelper("transformat", function (value) {
        return value.toFixed(2);
    });
    $(".myorder-all>div[OrderCode='" + code + "']").replaceWith(myTemplate(vworder));
}

function FunOrderPayForword(code)
{
    location.href = "/MobileOrder/OrderConfirm?code=" + code;
}