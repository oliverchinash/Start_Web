var proTemplate = function () {
      
    var temps = '<div >为了确保亲们能收到完好的宝贝，我们提醒亲们在签收快递的时候，注意检查包装是否完好，并且是否有被拆过的痕迹哦！提醒亲们应该与快递当面验收您的快递包裹！本店默认：韵达快递，如需发其它快递，请联系在线客服。</div>';


    var $selectTarget = $('#selectTemp'); 
    var $editorCont;
    var editor;
    var tempIndex = 0;
    var editorImgHandle;


    return {
        init: init
    };

    function init(editor) {
        $editorCont = $(editor.document);
        $selectTarget = $('#selectTemp'); 
        bindEvent(); 
    }



    function bindEvent() {

        $selectTarget
            .on('click', '.temp-none', function () {
                if ($.trim(ue.getContentTxt()) !== '') {
                    if (confirm('使用空白模板将清空编辑器里的内容，确定要使用吗？')) {
                        ue.setContent('<p></p>');
                    }
                }
                $('#tempPreview').hide();
                return false;
            })
            .on('click', '.temp-ok', function () {
                if ($.trim(ue.getContentTxt()) !== '') {
                    if (confirm('使用通用模板将替换编辑器里的内容，确定要使用吗？')) {
                        ue.setContent(temps);
                    }
                } else {
                    ue.setContent(temps);
                } 
                return false;
            }); 
    }
      
}();
