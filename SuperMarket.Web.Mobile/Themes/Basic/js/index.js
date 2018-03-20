

/*****home头部滚动背景变色*****/
$(document).ready(function(){
    $(window).scroll(function(){
        var scrollTop = $(window).scrollTop();//获取当前滑动位置
        if(scrollTop >50){
            $(".home-header").addClass("active");
        }
        else {
            $(".home-header").removeClass("active");
        }
    });

});

/********关于主页倒计时特效*********/
function timer()
{
    var ts = (new Date(2017, 11, 11, 9, 0, 0)) - (new Date());//计算剩余的毫秒数
    var hh = parseInt(ts / 1000 / 60 / 60 % 24, 10);//计算剩余的小时数
    var mm = parseInt(ts / 1000 / 60 % 60, 10);//计算剩余的分钟数
    var ss = parseInt(ts / 1000 % 60, 10);//计算剩余的秒数
    hh = checkTime(hh);
    mm = checkTime(mm);
    ss = checkTime(ss);
    document.getElementById("timer").innerHTML = hh + "时" + mm + "分" + ss + "秒";
    setInterval("timer()",1000);
}
function checkTime(i)
{
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}

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




/*************列表页已选导航左右滑动效果******************/
/*$(function(){

    n=$('.product-list-ul li').size();

    var wh=100*n+"%";

    $('.product-list-ul').width(wh);

    var lt=(100/n/5);

    var lt_li=lt+"%";

    $('.product-list-ul li').width(lt_li);

    var y=0;

    var w=n/2;

    $(".product-list-ul").swipe( {

        swipeLeft:function() {

            if(y==-lt*w){

                alert('已经到最后页');

            }else{

                y=y-lt;

                var t=y+"%";

                $(this).css({'-webkit-transform':"translate("+t+")",'-webkit-transition':'500ms linear'} );

            }

        },

        swipeRight:function() {

            if(y==0){

                alert('已经到第一页')

            }else{

                y=y+lt;

                var t=y+"%";

                $(this).css({'-webkit-transform':"translate("+t+")",'-webkit-transition':'500ms linear'} );

            }



        }

    });

});*/



/*************购物车飞入效果*************/
$(function () {
    function addFly(event) {
        var scrollHeight = document.body.scrollHeight;
        var offset = $('.cart').offset(), flyer = $('<img class="u-flyer" src="../img/cart_icon.png"/>');
        var kuan = $(window).width()/2;

        flyer.fly({
            start: {
                left: event.pageX,
                top: event.clientY
            },
            end: {
                left: offset.left,
                top: offset.top - scrollHeight ,
                width: 0,
                height: 0,
            },

        });


    }
    $(function(){
        $(".addcar").on("click",function(){
            addFly(event);
        });
    });
});


/*function addCart() {
    var ev = event || window.event;
    var i = $('.addcar').text();
    var $this = $(this);
    var stylePath=respath;
    var $src = $this.siblings().attr('src');
    var offset = $('.product-list-cart').offset();
    var flyer = $('<img class="u-flyer" src=""/>');
    var kuan = $(window).width()/2;
//            var num = $(this).parents('#pro-list-i').width()/2;
    flyer.attr('src',stylePath+"/images/common/gouwuche.png");
    flyer.fly({
        start: {
            left: kuan,
            top: ev.clientY
        },
        end: {
            left: offset.left,
            top: offset.top,
            width: 0,
            height: 0
        },
        onEnd: function(){
            $('.u-flyer').remove();
        }
    });
    i++;
    $('.product-list-cart').html(i);
};
*/
