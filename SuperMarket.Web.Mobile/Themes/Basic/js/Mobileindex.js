
var lesshour;
var lessmm;
var lessss;
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
    if ($("#spantimer").length > 0)
    {
     HuoDongLessTimer();
    }
});

/********关于主页倒计时特效*********/
function HuoDongLessTimer()
{
    var endtime =$("#spanendtime").html();
    var ts = (new Date(endtime)) - (new Date());//计算剩余的毫秒数
    var total = ts / 1000; 
      lesshour = parseInt(total / (60 * 60));//计算整数天数
    total = total - lesshour * 60 * 60;
      lessmm = parseInt(total / 60);// 取得算出天数后剩余的秒数
    total = total - lessmm * 60;
      lessss =parseInt(total);//取得算出分后剩余的秒数
    BindLessTime();
    setInterval("BindLessTime()", 1000);
}
function BindLessTime()
{
    lesshour = parseInt(lesshour);
    lessmm = parseInt(lessmm);
    lessss = parseInt(lessss); 
    if (lessss == 0) { lessss = 59; lessmm = lessmm - 1 } else { lessss = lessss - 1;}
    if (lessmm == -1) { lessmm = 59; lesshour = lesshour - 1 }  
    if (lesshour < 0) { lesshour = 0; lessmm = 0; lessss = 0; }  

    lesshour = checkTime(lesshour);
    lessmm = checkTime(lessmm);
    lessss = checkTime(lessss);
    document.getElementById("spantimer").innerHTML = lesshour + "时" + lessmm + "分" + lessss + "秒";
   
}
function checkTime(i)
{
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}
