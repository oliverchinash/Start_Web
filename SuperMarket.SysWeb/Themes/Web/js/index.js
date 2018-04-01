/**
 * Created by jc100 on 2017/6/22.
 */

/*********top二级菜单显示与隐藏效果***********/
$(document).ready(function () {
    $('.top-menu').mousemove(function(){
        $(this).find('.top-menu-main').show();
        $(this).addClass('menu-hover');
    });
    $('.top-menu').mouseleave(function(){
        $(this).find('.top-menu-main').hide();
        $(this).removeClass('menu-hover');
    });
})

/*********左侧图标切换********/
$(document).ready(function (e) {
   $(".sidebar-nav-title").each(function (index) {
       this.index=index
       $(this).click(function () {
           $(".arrow-up").eq(this.index).toggle();
           $(".arrow-down").eq(this.index).toggle();
           $(".sidebar-nav-ol").eq(this.index).toggleClass("heightauto");
       })
   })
})


/**********图标切换显示更多***********/
/***
$(function(){
    var i=0;
    $(".arrow-up").click(function(){
        i++;
        if(i%2==0){
            $(".sidebar-nav-ol").css("height","auto")
        }else{
            $(".sidebar-nav-ol").css("height","50px")
        }

    });
});


$(function (e) {
    $(".sidebar-nav-li").each(function (index) {
        this.index=index;
        $(this).click(function () {
            var i=0;
            i++;
            if(i%2==0){
                $(".sidebar-nav-ol").eq(this.index).css("height","auto")
            }else{
                $(".sidebar-nav-ol").eq(this.index).css("height","50px")
            }

        })
    })
})

 $(function () {
    $(".brandMore").click(function () {
        if ($.trim($(this).text()) == '更多') {
            $(this).addClass('on');
            $(this).html('收起<b></b>');
            $("#brandsList").addClass('heightAuto');
            $("#brandsList").removeClass('height');
        } else {
            $(this).removeClass('on');
            $(this).html('更多<b></b>');
            $("#brandsList").removeClass('heightAuto');
            $("#brandsList").addClass('height');
        }
    })


});

 ***/

/*******设置店标显示与隐藏*******/
$(document).ready(function () {
    $(".index-module-mingpian").hover(function () {
        $(".index-module-avatar").show();
    },function () {
        $(".index-module-avatar").hide();
    })
})

/********待办事项*******/
$(document).ready(function (e) {
    $(".module-left-box-title").each(function (index) {
        this.index=index
        $(this).click(function () {
            $(".arrow-up2").eq(this.index).toggle();
            $(".arrow-down2").eq(this.index).toggle();
            $(".module-left-box-ol").eq(this.index).toggleClass("heightauto");
        })
    })
})

/******显示与隐藏余额********/
$(document).ready(function () {
    $(".money-show").click(function () {
       $(this).hide();
       $(".money-balance").show();
    })
    $(".money-balance").click(function () {
        $(this).hide();
        $(".money-show").show();
    })
})

/********今日必读新闻列表**********/
/**
$(document).ready(function () {
    $('.module-right-item').hover(function(){
        $(this).find('.module-right-new-wrap').addClass("display").siblings().find('.module-right-new-wrap').removeClass("display")
    });
    $('.module-right-item').mouseleave(function(){
        $(this).find('.module-right-new-wrap').removeClass("display");
    });
})**/
