$(function () { 
    $.post("/Member/GetOrderMsg", {}, function (data) {
        debugger;
        alert(data);
        var _orderentity = eval("("+data+")");
        alert(_orderentity);
        $("#spanWaitPayNum").html(_orderentity.WaitPayNum);
        $("#spanWaitReciveNum").html(_orderentity.WaitReciveNum);
        $("#spanWaitCommentNum").html(_orderentity.WaitCommentNum);
    });
    $(".orderlistclick").bind("click", function () {
        var _term = $(this).attr("term");
        var _s = $("#hidstatus").val(); 
        var url = "/Member/OrderList?s=" + _s + "&t=" + _term; 
        window.location.href = url;        
    }); 
    $(".orderlistshow").each(function () {

    });
});