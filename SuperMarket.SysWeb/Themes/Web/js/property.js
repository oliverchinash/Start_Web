/**
 * Created by jc100 on 2017/6/24.
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

/*************颜色分类选项卡***************/
function webTab(sel,conSel,current){
    $(sel).click(function(){
        $(this).addClass(current).siblings().removeClass(current);	//给li增加高亮
        //获取当前的索引
        var _index = $(this).index(sel);
        $("."+conSel).eq(_index).show().siblings("."+conSel).hide();
    });
}

/**********点击选择颜色分类***********/
$(document).ready(function () {
    var n=$(".property-spec-content input").attr("checked")==true.length;
    $(".property-spec-content").click(function () {
        if(n>0){
            $(".property-spec-list").show();
            $(".property-spec-list2").show();
        }else{
            if(n=0){
                $(".property-spec-list").hide();
                $(".property-spec-list2").hide();
            }

        }
    })

})
/****
$(document).ready(function () {
    $(".property-spec-content input").click(function(){
        var i= $('.property-spec-content tr').length;
        var check = $(this).attr("checked") ;
        if(check=="checked"){
            $(".property-spec-list").show();
        }else{
            if($('.property-spec-content input[type=checkbox]:checked').length==0){
                $(".property-spec-list").hide();
            }

        }
    });
})


$(function () {
    $(".property-spec-content input").each(function () {
        $(this).bind("click", function () {
            if ($(".Self_support input[checked]=checked").length >0) {
                $(".property-spec-list").show();
                $(".property-spec-list2").show();
            }else {
                $(".property-spec-list").hide();
                $(".property-spec-list2").hide();
            }
        })
    })
})
*/


/**********手机端添加显示隐藏*********/
$(document).ready(function () {
    $(".property-mobile-bottom").hover(function () {
        $(".property-mobile-bottom-main2").show();
        $(".property-mobile-bottom-main").hide();
    },function () {
        $(".property-mobile-bottom-main2").hide();
        $(".property-mobile-bottom-main").show();
    })
    
    $(".property-mobile-wenzi").click(function () {
        $(".property-mobile-text").show();
    })
    $(".property-mobile-text-delete").click(function () {
        $(".property-mobile-text").hide();
    })
})
