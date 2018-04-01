var proTemplate = function () {


    //tinymce
    var nav = navigator, userAgent = nav.userAgent;
    var opera, webkit, ie, ie11, ie12, gecko, mac, iDevice, android, fileApi;
    opera = window.opera && window.opera.buildNumber;
    android = /Android/.test(userAgent);
    webkit = /WebKit/.test(userAgent);
    ie = !webkit && !opera && (/MSIE/gi).test(userAgent) && (/Explorer/gi).test(nav.appName);
    ie = ie && /MSIE (\w+)\./.exec(userAgent)[1];
    ie11 = userAgent.indexOf('Trident/') != -1 && (userAgent.indexOf('rv:') != -1 || nav.appName.indexOf('Netscape') != -1) ? 11 : false;
    ie12 = (userAgent.indexOf('Edge/') != -1 && !ie && !ie11) ? 12 : false;
    ie = ie || ie11 || ie12;
    var isIE = ie && ie < 11;


    //删除元素
    var deleteHandle = function (opts) {
        var selector = opts.selector;
        var $doc = opts.$doc;
        var $div;
        var $current;
        var resizeHandles;
        var x;
        var y;
        var eleX;
        var eleY;
        var eleW;
        var eleH;
        var offset;
        var buttonW;
        var isHover;
        var hideDelay;
        var showDelay;


        borders = {
            //x, y, w, h
            n: [0, 0, 1, 0],
            e: [1, 0, 0, 1],
            s: [0, 1, 1, 0],
            w: [0, 0, 0, 1]
        };


        $doc.on('mouseenter', selector, function (e) {
            var $this = $(this);

            if (hideDelay) {
                clearTimeout(hideDelay);
            }
            offset = $this.offset();
            x = e.screenX;
            y = e.screenY;
            eleX = offset.left;
            eleY = offset.top;
            eleW = $this.outerWidth();
            eleH = $this.outerHeight();
            show();
            $current = $this;
            return false;
        });

        $doc.on('mouseleave', selector, function () {
            hide();
            return false;
        });

        $doc.on('mouseenter', '#deleteHandleBtn', function (e) {
            if (hideDelay) {
                clearTimeout(hideDelay);
            }
            return false;
        });
        $doc.on('mouseleave', '#deleteHandleBtn', function (e) {
            hide();
            return false;
        });
        $doc.on('click', '#deleteHandleBtn', function (e) {
            deleteEle();
            return false;
        });

        function deleteEle() {
            $current.remove();
            hide();
        }

        function show() {
            showDelay = setTimeout(function () {
                createBorders();
                setBordersPos();
            }, 200);

        }

        function hide() {
            if (showDelay) {
                clearTimeout(showDelay);
            }
            hideDelay = setTimeout(function () {
                deleteHandles();
            }, 200);
            //$current = null;
        }

        function createBorders() {
            var html = '';

            if ($doc.find('#deleteHandlen').length === 0) {
                $.each(borders, function (index, el) {
                    html += '<div id="deleteHandle' + index + '" style="position:absolute;overflow:hidden;background:#428BCA;"></div>';
                });

                html += '<div id="deleteHandleBtn" style="position:absolute;width:60px;height:28px;line-height:28px;text-align:center;background:#428BCA;color:#fff;cursor:pointer;">删除</div>';

                $doc.find('body').append(html);
            }
        }

        function setBordersPos(e) {
            $.each(borders, function (index, el) {
                $doc.find('#deleteHandle' + index).css({
                    width: (el[2] * eleW) === 0 ? 1 : (el[2] * eleW),
                    height: (el[3] * eleH) === 0 ? 1 : (el[3] * eleH),
                    left: eleX + el[0] * eleW,
                    top: eleY + el[1] * eleH
                });
            });
            $doc.find('#deleteHandleBtn').css({
                left: eleX + eleW - 60,
                top: eleY
            });
        }

        function deleteHandles() {
            $.each(borders, function (index, el) {
                $doc.find('#deleteHandle' + index).remove();
            });
            $doc.find('#deleteHandleBtn').remove();
        }

    };


    var temps = '<div style="width:888px;font-family:Microsoft Yahei;font-size:14px;word-break:break-all"><div data-temp-tar=delete style="margin-bottom:35px;margin-top:0;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">产品信息</div><div data-temp-tar=delete style=clear:both;margin-bottom:20px;overflow:hidden><p data-temp-mk=p data-width=429 data-height=386 data-width=429 data-height=386 style="float:left;width:429px;height:386px;overflow:hidden;padding:5px;margin:0 15px 0 0;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r1.jpg"></span></p><div style=float:left;width:432px;font-size:20px><p style="display:block;margin:0 0 5px 0;font-size:22px">产品信息</p><p style=display:block;margin:0;line-height:32px>产品名称：<br>产品型号：<br>产品产地：<br>产品功能：<br></p><p data-temp-mk=tip data-temp-tar=delete style="display:block;height:40px;overflow:hidden;line-height:40px;margin:10px 0 0;padding:0 40px;border:solid 1px #d81414;text-align:center;font-family:Microsoft Yahei;font-size:21px;color:red">提示：你可以按回车键添加更多信息</p></div></div><div data-temp-tar=delete style=clear:both;margin-bottom:20px;overflow:hidden><p data-temp-mk=p data-width=429 data-height=386 style="float:left;width:429px;height:386px;padding:5px;margin:0 15px 0 0;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r2.jpg"></span></p><div style=float:left;width:432px;font-size:20px><p style="display:block;margin:0 0 5px;font-size:22px">产品优势</p><p style=display:block;margin:0;line-height:32px>产品名称：<br>产品型号：<br>产品产地：<br>产品功能：<br></p><p class=editor-delete data-temp-tar=delete data-temp-mk=tip style="display:block;height:40px;line-height:40px;margin:10px 0 0;padding:0 40px;border:solid 1px #d81414;text-align:center;font-family:Microsoft Yahei;font-size:21px;color:red">提示：你可以按回车键添加更多信息</p></div></div><div data-temp-tar=delete style="margin-bottom:35px;margin-top:40px;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">产品细节</div><div style=clear:both;margin-bottom:20px;width:100%;overflow:hidden><div style=width:110%><ul style=list-style:none;margin:0;padding:0><li data-temp-tar=delete style="float:left;clear:none;width:441px;height:458px;overflow:hidden;margin:0 6px 35px 0;padding:0"><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r3.jpg"></span></p><p style="display:block;height:55px;margin:5px 0 0;line-height:55px;overflow:hidden;text-align:center;padding:0 20px;font-size:30px;background:#666;color:#fff">产品正面照片</p></li><li data-temp-tar=delete style="float:left;clear:none;width:441px;height:458px;overflow:hidden;margin:0 6px 35px 0;padding:0"><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r4.jpg"></span></p><p style="display:block;height:55px;margin:5px 0 0;line-height:55px;overflow:hidden;text-align:center;padding:0 20px;font-size:30px;background:#666;color:#fff">产品背面照片</p></li><li data-temp-tar=delete class=editor-delete data-temp-mk=tip style="clear:both;position:relative;width:335px;margin:10px 0 30px 200px;height:28px;line-height:28px;text-align:center;border:solid 1px #e81818;color:#e81818">提示：放正背面图片，便于买家对产品更好地了解 <span style="display:block;position:absolute;left:50%;top:-12px;width:0;height:0;border-width:6px;border-color:transparent transparent #e81818;border-style:dashed dashed solid;line-height:0;overflow:hidden;font-size:0"></span> <span style="display:block;position:absolute;left:50%;top:-11px;width:0;height:0;border-width:6px;border-color:transparent transparent #fff;border-style:dashed dashed solid;line-height:0;overflow:hidden;font-size:0"></span></li><li data-temp-tar=delete style="float:left;clear:none;width:441px;height:458px;overflow:hidden;margin:0 6px 35px 0;padding:0"><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r5.jpg"></span></p><p style="display:block;height:55px;margin:5px 0 0;line-height:55px;overflow:hidden;text-align:center;padding:0 20px;font-size:30px;background:#666;color:#fff">产品更多角度展示</p></li><li data-temp-tar=delete style="float:left;clear:none;width:441px;height:458px;overflow:hidden;margin:0 6px 35px 0;padding:0"><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r6.jpg"></span></p><p style="display:block;height:55px;margin:5px 0 0;line-height:55px;overflow:hidden;text-align:center;padding:0 20px;font-size:30px;background:#666;color:#fff">产品更多角度展示</p></li><li data-temp-tar=delete style="float:left;clear:none;width:441px;height:458px;overflow:hidden;margin:0 6px 35px 0;padding:0"><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r7.jpg"></span></p><p style="display:block;height:55px;margin:5px 0 0;line-height:55px;overflow:hidden;text-align:center;padding:0 20px;font-size:30px;background:#666;color:#fff">产品更多角度展示</p></li><li data-temp-tar=delete style="float:left;clear:none;width:441px;height:458px;overflow:hidden;margin:0 6px 35px 0;padding:0"><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r8.jpg"></span></p><p style="display:block;height:55px;margin:5px 0 0;line-height:55px;overflow:hidden;text-align:center;padding:0 20px;font-size:30px;background:#666;color:#fff">产品更多角度展示</p></li><li data-temp-tar=delete class=editor-delete data-temp-mk=tip style="clear:both;position:relative;width:335px;margin:0;margin:10px 0 30px 200px;height:28px;line-height:28px;text-align:center;border:solid 1px #e81818;color:#e81818">提示：多放点细节图片，便于买家对产品更好地了解 <span style="display:block;position:absolute;left:50%;top:-12px;width:0;height:0;border-width:6px;border-color:transparent transparent #e81818;border-style:dashed dashed solid;line-height:0;overflow:hidden;font-size:0"></span> <span style="display:block;position:absolute;left:50%;top:-11px;width:0;height:0;border-width:6px;border-color:transparent transparent #fff;border-style:dashed dashed solid;line-height:0;overflow:hidden;font-size:0"></span></li></ul></div></div><div data-temp-tar=delete style="margin-bottom:35px;margin-top:40px;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">产品展示</div><div style=clear:both;margin-bottom:20px;width:100%;overflow:hidden><div style=width:110%><div data-temp-tar=delete style=float:left;width:441px;margin-right:6px;margin-bottom:6px><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r9.jpg"></span></p></div><div data-temp-tar=delete style=float:left;width:441px;margin-right:6px;margin-bottom:6px><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r10.jpg"></span></p></div><div data-temp-tar=delete style=float:left;width:441px;margin-right:6px;margin-bottom:6px><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r11.jpg"></span></p></div><div data-temp-tar=delete style=float:left;width:441px;margin-right:6px;margin-bottom:6px><p data-temp-mk=p data-width=429 data-height=386 style="padding:5px;width:429px;height:386px;margin:0;overflow:hidden;border:solid 1px #000"><span data-temp-mk=cell style=display:table-cell;width:429px;height:386px;overflow:hidden;vertical-align:middle;text-align:center><img data-temp-mk=upload width=429 height=386 style=max-width:429px;max-height:386px src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r1.jpg"></span></p></div></div></div><div data-temp-tar=delete style="margin-bottom:35px;margin-top:70px;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">企业证书</div><p data-temp-tar=delete data-temp-mk=p style="padding:5px;border:solid 1px #000"><img style=max-width:876px data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r13.jpg"></p><div data-temp-tar=delete style="margin-bottom:35px;margin-top:70px;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">品牌资质</div><p data-temp-tar=delete data-temp-mk=p style="padding:5px;border:solid 1px #000"><img style=max-width:876px data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r14.jpg"></p><div data-temp-tar=delete style="margin-bottom:35px;margin-top:70px;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">物流包装</div><p data-temp-tar=delete><img style=max-width:876px data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/r15.jpg"></p><div data-temp-tar=delete style="margin-bottom:35px;margin-top:70px;padding:8px 20px;font-size:30px;background:#666;color:#fff;font-weight:400">购物须知</div><table data-temp-tar=delete style=width:100%;border-collapse:collapse;border:none;margin:auto><tbody><tr><td style=width:100px;border:none;text-align:right;vertical-align:middle><img data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/s1.gif"><td style=width:10px;border:none><td style=border:none><p style=color:#a33128;font-size:24px>正品保障</p><p style=font-size:16px>本店所售产品均为全新正品，质量保证，请放心购买！</p><tr><td colspan=3 style=height:30px;border:none><tr><td style=width:100px;border:none;text-align:right;vertical-align:middle><img data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/s2.gif"><td style=width:10px;border:none><td style=border:none><p style=color:#a33128;font-size:24px>关于色差</p><p style=font-size:16px>商品照片都为专业摄影师拍摄后和后期修正调整，尽量与现实商品保持一致。但由于灯光、显示器色彩偏差及个人对颜色理解各有不同，导致少数实物可能会与照片存在色差，如无法接受请谨慎购买。最终颜色会以实际商品为准，谢谢亲的理解！</p><tr><td colspan=3 style=height:30px;border:none><tr><td style=width:100px;text-align:right;vertical-align:middle;border:none><img data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/s3.gif"><td style=width:10px;border:none><td style=border:none><p style=color:#a33128;font-size:24px;border:none>关于尺寸</p><p style=font-size:16px>全由我们手工测量，可能存在0.5cm的误差，请不能接受误差的朋友慎拍。</p><tr><td colspan=3 style=height:30px;border:none><tr><td style=width:100px;border:none;text-align:right;vertical-align:middle><img data-temp-mk=upload src="http://static.kuparts.com/7.0/img/member/product/editorTemplate/temp1/s4.gif"><td style=width:10px;border:none><td style=border:none><p style=color:#a33128;font-size:24px>关于配送</p><p style=font-size:16px>为了确保亲们能收到完好的宝贝，我们提醒亲们在签收快递的时候，注意检查包装是否完好，并且是否有被拆过的痕迹哦！提醒亲们应该与快递当面验收您的快递包裹！本店默认：韵达快递，如需发其它快递，请联系在线客服。</p></table></div>';


    var $selectTarget = $('#selectTemp');
    debugger
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
        debugger 
        bindEvent();
        //funDrag($('#tempPreviewImg')[0]);
        setInterval(function () {
            setTempImg();
        }, 500);
    }



    function bindEvent() {

        $selectTarget
            .on('click', '.temp', function () { 
                $(this).find('.m-follow-msg-md').show().find('img').attr('src', function () {
                    return $(this).attr('data-src');
                });
            })
            .on('click', '.preview-close', function () {
                $(this).parent().hide();
                return false;
            })
            .on('click', '.temp-none', function () {
                if ($.trim(ue.getContentTxt()) !== '') {
                    if (confirm('使用空白模板将清空编辑器里的内容，确定要使用吗？')) {
                        ue.setContent('<p></p>');
                    }
                }
                $('#tempPreview').hide();
                return false;
            })
            .on('click', '.preview-ok', function () {
                if ($.trim(ue.getContentTxt()) !== '') {
                    if (confirm('使用通用模板将替换编辑器里的内容，确定要使用吗？')) {
                        ue.setContent(temps);
                    }
                } else {
                    ue.setContent(temps);
                }
                $(this).parent().hide();
                if (!editorImgHandle && !isIE) {
                    editorImgHandle = resizeHandle.init({
                        $ele: $editorCont.find('img')
                    });
                    deleteHandle({
                        selector: '[data-temp-tar="delete"]',
                        $doc: $editorCont
                    });
                }
                return false;
            });

        $('body').on('click.resizeHandle', function () {
            resizeHandle.clear();
        });

        uploadImg();
        tipHanld();
    }

    function tipHanld() {
        $editorCont.on('click', '[data-temp-mk="tip"]', function () {
            var range = ue.selection.getRange();
            var node = $(this)[0];
            range.selectNode(node).txtToElmBoundary(true).select();

        });

        $editorCont.on('keydown', function (e) {
            if (e.keyCode === 46 || e.keyCode === 8) {
                var range = ue.selection.getRange();
                var type = range.startContainer.nodeType;
                var $p;
                var $node;
                var prevent = true;
                var $firstNode = $(ue.selection.getStart());

                if ($firstNode.attr('data-temp-mk') == 'tip') {
                    $node = $firstNode.prev();
                    //if ($node.length !== 0) {
                    //	$node = $node[0];
                    //	range.setStartBefore($node).setStartBefore($node).setCursor();
                    //}
                    $firstNode.remove();
                    return false;
                }

                //删除图片或者外框
                if (type === 1) {
                    $p = $(range.startContainer);
                } else if (type === 3) {
                    $p = $(range.startContainer.parentElement);
                }

                $node = $p.find('[data-temp-mk]:last');

                if ($node.length > 0) {
                    if ($.trim($node.text()) == '') {
                        $node.remove();
                        return false;
                    }
                } else {
                    if ($p.attr('data-temp-mk')) {
                        if ($.trim($p.text()) == '') {
                            $p.remove();
                            return false;
                        }
                    }
                }

            }
        });
    }

    function uploadImg() {
        $editorCont.on('dblclick', '[data-temp-mk="upload"]', function () {
            var range = ue.selection.getRange();
            var myImage = ue.getDialog("insertimage");
            myImage.open();
        });
    }

    function setTempImg() {
        $editorCont.find('[data-temp-mk="p"]').each(function () {
            var $this = $(this);
            var range = ue.selection.getRange();
            var width = $this.attr('data-width');
            var height = $this.attr('data-height');
            var $img = $this.find('img');
            if ($img.length > 0 && !$img.attr('data-temp-mk')) {
                $img.attr('data-temp-mk', 'upload');
                if ($img.width() > width) {
                    $img.css('width', width);
                }
                if ($img.width() > height) {
                    $img.css('height', height);
                }
                resizeHandle.init({
                    $ele: $img
                });
            }
            if ((range.getClosedNode() && range.getClosedNode().tagName !== 'IMG') ||
                (!range.getClosedNode())) {
                editorImgHandle && editorImgHandle.clear();
            }
        });

    }

}();
