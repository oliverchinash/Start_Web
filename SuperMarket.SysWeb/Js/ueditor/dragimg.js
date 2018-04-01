var resizeHandle = function () {
    var $ele;
    var $clone;
    var $current;
    var rangeEle;
    var selectedHandle;
    var handles;

    var startW;
    var startH;
    var deltaX;
    var deltaY;
    var selectedElmX;
    var selectedElmY;
    var width;
    var height;
    var ratio;
    var handleReady;
    var doc;
    var agent = navigator.userAgent.toLowerCase();
    var isIE = /(msie\s|trident.*rv:)([\w.]+)/.test(agent);

    return {
        init: function (opts) {
            if (isIE) {
                return false;
            }
            var styles = '';

            doc = $(ue.document);
            $ele = opts.$ele;
            handles = {
                // Name: x multiplier, y multiplier, delta size x, delta size y
                n: [0.5, 0, 0, -1],
                e: [1, 0.5, 1, 0],
                s: [0.5, 1, 0, 1],
                w: [0, 0.5, -1, 0],
                nw: [0, 0, -1, -1],
                ne: [1, 0, 1, -1],
                se: [1, 1, 1, 1],
                sw: [0, 1, -1, 1]
            };
            if ($('#resizeHandleStyle').length === 0) {
                style = '<style id="resizeHandleStyle">' +
                    '.resize-handle-dot{ position:absolute; z-index:100; width:5px; height:5px; line-height:0; overflow:hidden; border:solid 1px #000; background:#fff;}' +
                    'img[data-handle-able]{ outline:solid 1px #000; opacity:.5;}' +
                    '</style>';
                doc.find('head').append(style);
            }
            this.addHandles();
            this.bindEvent();
            this.handlesEvent();
            return this;
        },

        addHandles: function () {
            var html = '';
            var $hd = doc.find('#resize-handle-dot-n');
            if ($hd.length === 0) {
                $.each(handles, function (index, el) {
                    html += '<div id="resize-handle-dot-' + index + '" data-handle-type="' + index + '" class="resize-handle-dot" style="display:none;cursor:' + index + '-resize;" contenteditable="false"></div>';
                });
                doc.find('body').append(html);
            }
            handleWidth = doc.find('#resize-handle-dot-n').outerWidth();
        },

        bindEvent: function () {
            var _this = this;

            $ele.on('click.resizeHandle', function () {
                var $this = $(this);
                var range = ue.selection.getRange();
                range.selectNode($this[0]).txtToElmBoundary(true).select();
                $ele.removeAttr('data-handle-able');
                $current = $(this);
                $current.attr('data-handle-able', 1);
                _this.show();
                _this.updateHandlePos();
                _this.removeClone();
                return false;
            });
        },

        clear: function () {
            var _this = this;

            if (!handles) {
                return false
            }
            _this.hide();
            doc.unbind('mousemove.resizeHandle');
            $ele.removeAttr('data-handle-able');
        },

        startDrag: function (e, $this) {
            var _this = this;

            selectedHandle = handles[$this.attr('data-handle-type')];
            startW = $current.outerWidth();
            startH = $current.outerHeight();
            selectedElmX = $current.offset().left;
            selectedElmY = $current.offset().top;

            startX = e.screenX;
            startY = e.screenY;
            ratio = startH / startW;
            _this.handleBefore(e);
            handleReady = true;

            doc.on('mousemove.resizeHandle', function (e) {
                if (handleReady) {
                    _this.colneHandle(e);
                }
                return false;
            });
        },

        handlesEvent: function () {
            var _this = this;
            //释放
            doc.on('mouseup', function (e) {
                var range = ue.selection.getRange();

                if (handleReady && !e.isDefaultPrevented()) {
                    $current.css({
                        width: width,
                        height: height
                    });
                    _this.updateHandlePos();
                    range.selectNode($current[0]).txtToElmBoundary(true).select();
                } else {
                    if ($(e.target)[0].tagName !== 'IMG') {
                        _this.clear();
                    }
                }

                _this.removeClone();
                handleReady = false;
            });

            doc.on('mousedown', '.resize-handle-dot', function (e) {
                e.stopImmediatePropagation();
                e.preventDefault();
                _this.startDrag(e, $(this));
            });
            return false;
        },

        handleBefore: function (e) {

            if ($('#handleEleClone').length === 0) {
                $clone = $('<img/>');
            } else {
                $clone = $('#handleEleClone');
            }
            $clone.appendTo(doc.find('body'));
            $clone
                .attr({
                    id: 'handleEleClone',
                    src: $current.attr('src')
                })
                .css({
                    position: 'absolute',
                    left: selectedElmX,
                    top: selectedElmY,
                    width: width,
                    height: height,
                    opacity: 0.5,
                    display: 'none'
                });
        },

        colneHandle: function (e) {
            var propertys = {};
            var markX = selectedHandle[2];
            var markY = selectedHandle[3];

            deltaX = e.screenX - startX;
            deltaY = e.screenY - startY;
            width = deltaX * markX + startW;
            height = deltaY * markY + startH;
            $clone.show();
            width = width < 50 ? 50 : width;
            height = height < 50 ? 50 : height;
            if ((markX === 0 && markY === -1) ||
                (markX === 0 && markY === 1)) {
                $clone.css({
                    width: startW,
                    height: height
                });
            } else if ((markX === 1 && markY === 0) ||
                (markX === -1 && markY === 0)) {
                $clone.css({
                    width: width,
                    height: startH
                });
            } else {
                if (Math.abs(deltaX) > Math.abs(deltaY)) {
                    height = Math.round(width * ratio);
                    width = Math.round(height / ratio);
                } else {
                    width = Math.round(height / ratio);
                    height = Math.round(width * ratio);
                }
                $clone.css({
                    width: width,
                    height: height
                });
            }
            if (markX < 0 && $clone.outerWidth() <= width) {
                $clone.css('left', $current.offset().left + (startW - width));
            }
            if (markY < 0 && $clone.outerHeight() <= height) {
                $clone.css('top', $current.offset().top + (startH - height));
            }
        },

        show: function () {
            $.each(handles, function (index, el) {
                doc.find('#resize-handle-dot-' + index).show();
            });
        },

        hide: function () {
            $.each(handles, function (index, el) {
                doc.find('#resize-handle-dot-' + index).hide();
            });
        },

        updateHandlePos: function () {
            var targetWidth;
            var targetHeight;
            selectedElmX = $current.offset().left;
            selectedElmY = $current.offset().top;
            $.each(handles, function (index, el) {
                doc.find('#resize-handle-dot-' + index).css({
                    left: selectedElmX + $current.outerWidth() * handles[index][0] - handleWidth / 2,
                    top: selectedElmY + $current.outerHeight() * handles[index][1] - handleWidth / 2
                });
            });
        },

        removeClone: function () {
            if ($clone) {
                $clone.remove();
                $clone = null;
            }
        }
    };

}();
