// JavaScript Document
/*********侧边栏点击事件***********/
$(function(){
    $(".paycheck-offcanvas-ul li,.paycheck-pay-ul li").click(function(){
        $(this).addClass("active").siblings().removeClass("active");
    });
});



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
        },
        format:function(val, digit){
            if(isNaN(val)){
                val = 0;
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
                default_data : 	{"step" : 1, "min" : 0, "max" : 99, "digit" : 0},
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




/***$(".paycheck-offcanvas-fanhui").bind("mousedown",function(){
    $("#doc-oc-demo3").toggle();
});
$(".paycheck-dizhi").click(function () {
    $("#doc-oc-demo3").show();
})


/********关于侧边栏退出效果**********/
/***$(function () {
    $(".paycheck-offcanvas-fanhui2").click(function () {
        $("#doc-oc-demo4").hide();
    })
    $(".paycheck-pay-li").click(function () {
        $("#doc-oc-demo4").show();
    })
})
******/

/*********开关按钮*********/
$(function () {
    $(".am-icon-toggle-off").click(function () {
        $(this).hide();
        $(".am-icon-toggle-on").show();
    })
    $(".am-icon-toggle-on").click(function () {
        $(this).hide();
        $(".am-icon-toggle-off").show();
    })
})


/*********新增地址显示隐藏页面**********/
$(function () {
    $(".paycheck-dizhi-new").click(function () {
       $(".paycheck").hide();
       $(".newAddress").show();
    })

    $(".newAddress-tuichu").click(function () {
        $(".newAddress").hide();
        $(".paycheck").show();
    })
})

/************编辑地址触发事件*************/
$(function () {
    $(".paycheck-dizhi-bianji").click(function () {
        alert("编辑地址")
    });
});


/***********发票信息控制不可编辑***********/
$(function () {
    $(".invoice-main3 input").attr("readOnly",true);
});

/********发票编辑与新增显示与隐藏页面********/
//$(function () {
//    $(".paycheck-fapiao-xinzeng,.paycheck-fapiao-bianji").click(function () {
//        $(".paycheck").hide();
//        $(".invoice").show();
//    })

//    //$(".invoice-tuichu").click(function () {
//    //    $(".invoice").hide();
//    //    $(".paycheck").show();
//    //});
//});


/***********购买记录的删除弹出框效果**************/
//$(function () {
//    //$(".payHistory-delete").click(function () {
//    //    layer.confirm('确定要删除选中的商品吗？', {
//    //        btn: ['取消','删除'] //按钮
//    //    }, function(){
//    //        layer.msg('已取消', {icon: 1});
//    //    }, function(){
//    //        layer.msg('已删除', {
//    //            time: 20000, //20s后自动关闭
//    //            btn: ['好的']
//    //        });
//    //    });
//    //});

//    //$(".payHistory-header-delete").click(function () {
//    //    layer.confirm('确定要清空所有的商品吗？', {
//    //        btn: ['取消','清空'] //按钮
//    //    }, function(){
//    //        layer.msg('已取消', {icon: 1});
//    //    }, function(){
//    //        layer.msg('已清空', {
//    //            time: 20000, //20s后自动关闭
//    //            btn: ['好的']
//    //        });
//    //    });
//    //});

//});


/**********地址页面新建页面隐藏显示**********/
$(function () {
    $(".address-header-xinjian").click(function () {
        $(".address").hide();
        $(".newAddress").show();
    })

    $(".newAddress-tuichu").click(function () {
        $(".newAddress").hide();
        $(".address").show();
    });
});

/************订单确认支付页面侧边栏隐藏与显示***********/
/**
$(function () {
    $(".confirmation-offcanvas-fanhui").bind("mousedown",function(){
        $("#doc-oc-demo3").toggle();
    });
    $(".confirmation-main2").click(function () {
        $("#doc-oc-demo3").show(1000);
    });
});***/

/***********关闭侧边栏效果**********/
$(function () {
    function FunOpenCanvas(divid) {
        $("#" + divid).offCanvas('open')
    }
    function FunCloseCanvas(divid)
    {
        $("#" + divid).offCanvas('close')

    }
})
