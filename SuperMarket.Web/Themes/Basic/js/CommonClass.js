function BindSeriesDiv(brandid)
{
    $.post("/Common/GetSeriesByBrandId", { brandid: brandid }, function (data) {
        var jsonobj = eval(data);
        if (jsonobj != null && jsonobj != undefined && jsonobj.length > 0) {

            $("#divCarTypeSeriesBox>div.cartype-second-one").remove();
             for (var i = 0; i < jsonobj.length; i++) {
                var strdiv = "";
                strdiv += "<div class='cartype-second-one'><p>" + jsonobj[i].Name + "</p>";
                if (jsonobj[i].Children != null && jsonobj[i].Children.length > 0)
                {  
                    strdiv += "<ul class='am-g'>"; 
                    var childrenobj = jsonobj[i].Children;
                    for(var j=0;j<childrenobj.length;j++)
                    { 
                        strdiv += "<li class='am-u-sm-6'><a href='#' onclick='SelectCarTypeSeries(\"" + childrenobj[j].Id + "\")' >" + childrenobj[j].FullName + "</a></li>";
                    }
                    strdiv += "</ul>";
                }
                strdiv += "</div>";
                $("#divCarTypeSeriesBox").append(strdiv);
            }

        }
    });
}

function BindModelDiv(series)
{
    $.post("/Common/GetModelBySeriesId", { seriesid: series }, function (data) {
         
        var jsonobj = eval("("+data+")");
     
        var stryears = jsonobj.CarYears;
        var strcapacitys = jsonobj.Capacitys;
        var models = jsonobj.Children;
        if(stryears!=null&&strcapacitys!=null&&models!=null)
        {
            $("#ulCarTypeModeYears>li").remove();
            $("#ulCarTypeModeCapacitys>li").remove();
            $("#ulCarTypeModels>li:not(:first)").remove();
            if(stryears.length>0)
            {
                for(var i=0;i<stryears.length;i++)
                { 
                    $("#ulCarTypeModeYears").append("<li year='"+stryears[i]+"'><a  onclick='FunSelectCarTypeModelYear(\"" + stryears[i] + "\")'>" + stryears[i] + "</a></li>");
                }
            }
            if (strcapacitys.length > 0) {
                for (var i = 0; i < strcapacitys.length; i++) {
                    $("#ulCarTypeModeCapacitys").append("<li  capacity='"+strcapacitys[i]+"'><a href='#'  onclick='FunSelectCarTypeModelCapacitys(\""+strcapacitys[i]+"\")'>" + strcapacitys[i] + "</a></li>");
                }
            }
            if (models.length > 0) {
                for (var i = 0; i < models.length; i++) {
                    var str = "<li capacity='" + models[i].Capacity + "'  year='" + models[i].CarYear + "'><a href='#' onclick='FunSelectCarTypeModel(\"" + models[i] .Id+ "\")' style='padding: 0;'>" + models[i].ModelName + "<span class='am-list-icon am-icon-angle-right am-fr' style='padding-right: 1rem'></span></a></li>";
                    $("#ulCarTypeModels").append(str);
                }
            }
        }
    });
}
function SelectCarTypeBrand(brandid)
{
    BindSeriesDiv(brandid);
    FunOpenCanvas("divCarTypeSeriesCanvas");
    $("#spanSelectCarTypeBrand").attr("spanval", brandid);
    $("#spanSelectCarTypeSeries").attr("spanval", "");
    $("#spanSelectCarTypeModel").attr("spanval", "");
 
}

function SelectCarTypeSeries(series)
{ 
    BindModelDiv(series); 
    FunOpenCanvas("divCarTypeModelCanvas"); 
    $("#spanSelectCarTypeSeries").attr("spanval", series);
    $("#spanSelectCarTypeModel").attr("spanval", "");
}

function FunSelectCarTypeModelYear(year)
{
    var selectcapacity=$("#divSelectCapacity").html();
    var capastrs = new Array();
    $("#divSelectYear").html(year);
    $("#spanCarTypeYear").html(year);
    $("#ulCarTypeModeCapacitys>li").hide(); 
    $("#ulCarTypeModels>li:not(:first)").each(function () {
        var thecapacity = $(this).attr("capacity");
        var theyear = $(this).attr("year");
        if (year == theyear) { 
            $("#ulCarTypeModeCapacitys li[capacity='" + thecapacity + "']").show(); 
            if(selectcapacity==""||selectcapacity==thecapacity)
            {
                $(this).show();
            }
        }
        else {
            $(this).hide();
        }
       

    });

    $("#btnCarTypeYear").dropdown('toggle');

}

function FunSelectCarTypeModelCapacitys(capacit) {

    $("#divSelectCapacity").html(capacit);
    $("#spanCarTypeCapacity").html(capacit);
    var selectyear = $("#divSelectYear").html(); 
    $("#ulCarTypeModeYears>li").hide();
    $("#ulCarTypeModels>li:not(:first)").each(function () {
        var thecapacity = $(this).attr("capacity");
        var theyear = $(this).attr("year");
        if (capacit == thecapacity) {
            $("#ulCarTypeModeYears li[year='" + theyear + "']").show();
            if (selectyear == "" || selectyear == thecapacity) {
                $(this).show();
            }
        }
        else {
            $(this).hide();
        } 
    });
    $("#btnCarTypeCapacity").dropdown('toggle');

}

function FunSelectCarTypeModel(modelid)
{ 
    $("#spanSelectCarTypeModel").attr("spanval", modelid);
    FunSearch(1);
}
function FunClickCarType()
{ 
    $("#spanSelectCarTypeBrand").html(  $("#spanSelectCarTypeBrand").attr("spanval" ));
    $("#spanSelectCarTypeSeries").html($("#spanSelectCarTypeSeries").attr("spanval"));
    $("#spanSelectCarTypeModel").html($("#spanSelectCarTypeModel").attr("spanval"));
    FunSearch(1);
}
$(function () {
    $("#btnCarTypeYear").click(function () { 
        $("#btnCarTypeYear").dropdown('toggle');
    })
    $("#btnCarTypeCapacity").click(function () {
        $("#btnCarTypeCapacity").dropdown('toggle');
    })
})
