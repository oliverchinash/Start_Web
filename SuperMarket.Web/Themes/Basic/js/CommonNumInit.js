 function FunNumInit(input, step, max, min, digit){
    var width = input.width()-3;
    var height = input.width()/4; 
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
        FunNumExecute(input, step, max, min, digit, true);
    });
    $("#" + input.attr('id') + "r").click(function(){
        FunNumExecute(input, step, max, min, digit, false);
    });
    $("#" + input.attr('id')  ).change(function () {
        FunNumChange(input, max, min, digit );
    });
    $(input).attr("hascreate", "1");
} 
function FunNumExecute(input, step, max, min, digit, _do){
    var val = parseFloat(FunNumFormat(input.val(), digit));
    var ori = val;
    if(_do) val -= step;
    if (!_do) val += step;
    var limitmax = FunNumFormat(input.attr("maxlimit"));
    if (limitmax > 0 && limitmax < max) max = limitmax;
    if(val<min){
        val  =  min;
    }else if(val>max){
        val  =  max;
    }
    input.val(FunNumFormat(val, digit));
} 
function FunNumFormat(val, digit) {
    if(isNaN(val)){
        val = 0;
    }
    return parseFloat(val).toFixed(digit);
}

function FunNumChange(input, max, min, digit)
{
    var val = parseFloat(FunNumFormat(input.val(), digit));
    var ori = val;
    var limitmax = FunNumFormat(input.attr("maxlimit"));
    if (limitmax > 0 && limitmax < max) max = limitmax;
    if (val < min) {
        val = min;
    } else if (val > max) {
        val = max;
    }
    input.val(FunNumFormat(val, digit));
}

function FunNumCreate(thisobj) {
 
        //预设值数据选择
        var data = {
            default_data: { "step": 1, "min": 1, "max": 9999, "digit": 0 },
            aaa: { "step": 10, "min": 5, "max": 20, "digit": 0 },
        }

        var user_data = eval("data." + $(thisobj).attr("user_data"));
        if (user_data == null) {
            user_data = data.default_data;
        }

        var digit = $(thisobj).attr("data_digit");
        if (digit != null && !isNaN(parseFloat(digit))) {
            digit = parseFloat(digit).toFixed(0);
            user_data.digit = parseFloat(digit);
        }

        var step = $(thisobj).attr("data_step");
        if (step != null && !isNaN(parseFloat(step))) {
            user_data.step = parseFloat(step);
        }
        var min = $(thisobj).attr("data_min");
        if (min != null && !isNaN(parseFloat(min))) {
            user_data.min = parseFloat(min);
        }

        var max = $(thisobj).attr("data_max");
        if (max != null && !isNaN(parseFloat(max))) {
            user_data.max = parseFloat(max);
        }
        //自动装载
        FunNumInit($(thisobj), user_data.step, user_data.max, user_data.min, user_data.digit);

        var data_edit = $(thisobj).attr("data_edit");
        //if (data_edit) {
            $(thisobj).attr("readonly", null);
        //} 
}

function Create_Num()
{
    $("input[hascreate='0']").each(function () {
        FunNumCreate(this);
    });
}
$(function () {
    //绑定添加数量按钮
    Create_Num();
})


