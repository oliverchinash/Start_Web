/*** Created by jc100 on 2017/4/19.*/

/***********我的订单删除效果***********/
$(function () {
    $(".myorder-all-shanchu").click(function () {
        layer.confirm('确定要删除选中的商品吗？', {
            btn: ['取消','删除'] //按钮
        }, function(){
            layer.msg('已取消', {icon: 1});
        }, function(){
            layer.msg('已删除', {
                time: 20000, //20s后自动关闭
                btn: ['好的']
            });
        });
    })
})


/**********提交退订理由**********/
$(function () {
    $(".waitpay-tijiao").click(function () {
        alert("提交成功！")
    })
})

/**************待付款全选特效******************/
/************全选特效*****************/
$(".waitpay-chekbox-all").click(function(){
    if(this.checked){
        $(".waitpay-checkbox").each(function(){this.checked=true;});
    }else{
        $(".waitpay-checkbox").each(function(){this.checked=false;});
    }
});

/*********侧边栏点击事件***********/
$(function(){
    $(".waitpay-pay-ul li,.waitpay-pay-ul li").click(function(){
        $(this).addClass("active").siblings().removeClass("active");
    });
});



