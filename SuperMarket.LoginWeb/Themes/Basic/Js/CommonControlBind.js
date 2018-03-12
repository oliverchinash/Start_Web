 

function BindDropParent(selectobj,urlstr,param) {
    ///默认第一行不变
    $(selectobj).find("option:not(:first)").remove(); 
    $.ajax({
        url: urlstr,
        type: "POST",
        //async: false,
        dataType: 'json',
        data: param,
        success: function (json) {//客户端jquery预先定义好的callback函数,成功获取跨域服务器上的json数据后,会动态执行这个callback函数
            if (json != null && json != "") {
                var objjson = eval(json);
                if (objjson != null && objjson != undefined && objjson.length > 0) {
                    for (var i = 0; i < objjson.length; i++) {
                        $(selectobj).append("<option value=" + objjson[i].Code + ">" + objjson[i].Name + "</option>");
                    }
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Http status: " + xhr.status + " " + xhr.statusText + "\najaxOptions: " + ajaxOptions + "\nthrownError:" + thrownError + "\n" + xhr.responseText);
        },
        complete: function (data, status) {
        }
    });
}

function BindDropParent(selectobj, urlstr, param, selectvalue) {
    ///默认第一行不变
    $(selectobj).find("option:not(:first)").remove();
    $.ajax({
        url: urlstr,
        type: "POST",
        //async: false,
        dataType: 'json',
        data: param,
        success: function (json) {//客户端jquery预先定义好的callback函数,成功获取跨域服务器上的json数据后,会动态执行这个callback函数
            if (json != null && json != "") {
                var objjson = eval(json);
                if (objjson != null && objjson != undefined && objjson.length > 0) {
                    for (var i = 0; i < objjson.length; i++) {
                        $(selectobj).append("<option value=" + objjson[i].Code + ">" + objjson[i].Name + "</option>");
                    }
                    if (selectvalue != null && selectvalue != undefined && selectvalue != "") {
                        $(selectobj).val(selectvalue);
                    }
                }
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Http status: " + xhr.status + " " + xhr.statusText + "\najaxOptions: " + ajaxOptions + "\nthrownError:" + thrownError + "\n" + xhr.responseText);
        },
        complete: function (data, status) {
        }
    });
}

function BindDropParent(selectobj, urlstr, param, selectvalue,selecttext,settextobj) {
    ///默认第一行不变
    $(selectobj).find("option:not(:first)").remove();
    $.ajax({
        url: urlstr,
        type: "POST",
        async: false,
        dataType: 'json',
        data: param,
        success: function (json) {//客户端jquery预先定义好的callback函数,成功获取跨域服务器上的json数据后,会动态执行这个callback函数
            if (json != null && json != "") {
                var objjson = eval(json);
                if (objjson != null && objjson != undefined && $(objjson).length > 0) {
                    for (var i = 0; i < $(objjson).length; i++) {
                        $(selectobj).append("<option  value=" + $(objjson)[i].Code + ">" + $(objjson)[i].Name + "</option>");
                    }
                    if (selectvalue != null && selectvalue != undefined && selectvalue != "") {
                        $(selectobj).val(selectvalue);
                    }
                    if (selecttext != null && selecttext != undefined && selecttext != "") {
                        $(selectobj).find("option").filter(function () { return $(this).text() == selecttext }).attr("selected",true);
                    }
                    if (settextobj) {
                        var _text = $(selectobj).find("option:selected").text();
                        $(settextobj).append(_text);
                    }
                }
            }

        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert("Http status: " + xhr.status + " " + xhr.statusText + "\najaxOptions: " + ajaxOptions + "\nthrownError:" + thrownError + "\n" + xhr.responseText);
        },
        complete: function (data, status) {
        }
    });
}

function BindSelectData(obj,param) { 
    var precolumnid = $(obj).attr("precolumnid");
    var nextcolumnid = $(obj).attr("nextcolumnid");
    var columnid = $(obj).attr("objcolumnid");
    var url = $(obj).attr("url");
    var isend = $(obj).attr("isend");
    var url = $(obj).attr("url");
    var pid =0;
    if (precolumnid != null && precolumnid != undefined && precolumnid != "") {
        pid = $("#hid" + precolumnid).val();
        param.pid = pid;
    }
    $(obj).find("option:not(:first)").remove();
    $.post(url, param, function (data) { 
        var jsonobj = eval(data);
        if (jsonobj != null && jsonobj != undefined && jsonobj.length > 0) {
            for (var i = 0; i <  jsonobj.length; i++) {
                $(obj).append("<option value=" + jsonobj[i].Code + ">" + jsonobj[i].Name + "</option>");
            }
        }
    });
}
function ChangeSelectData(obj, param) {
    var nextcolumnid = $(obj).attr("nextcolumnid");
    var columnid = $(obj).attr("objcolumnid"); 
    var isend = $(obj).attr("isend"); 
    $("#hid" + columnid).val($(obj).val());
    if (isend == "0" && nextcolumnid != null && nextcolumnid != undefined && nextcolumnid!="") {
        BindSelectData($("#sel" + nextcolumnid), param);
    }
   
}