function BindUlPic($ul, url, param) {
    $.post(url, param, function (data) {
        var jsonobj = eval(data);
        if (jsonobj != null && jsonobj != undefined && jsonobj.length > 0) {
            for (var i = 0; i < jsonobj.length; i++) {
                var listr = "<li liitemcode=StylePics'" + jsonobj[i].Id + "'>" + jsonobj[i].PicUrl + "</li>";
                $ul.append(listr);
            }
        }
    });
}