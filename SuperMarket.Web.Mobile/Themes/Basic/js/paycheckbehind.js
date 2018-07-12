// JavaScript Document
/*********侧边栏点击事件***********/
 
 //地址
function FunAddressEdit(obj)
{
    $(".paycheck").hide();
    $(".divCanvasContainer").hide();
    $(".newAddress").show();
    UpdateReceiptAddress(obj);
}

function FunAddressEditBoxShow()
{
    $(".paycheck").hide();
    $(".divCanvasContainer").hide();
    $(".newAddress").show(); 
} 
function FunAddressEditBoxhide() {
    $(".newAddress").hide();
    $(".paycheck").show();
    $(".divCanvasContainer").show();
}
////修改后绑定到列表
//function BindListUpdateBeh(jsonobj)
//{
//    var myTemplate = Handlebars.compile($("#liAddressEdit-template").html());
//    var liobj = $("#ulAddressListBox li[AddressId='" + jsonobj.Id + "']");
//    $(liobj).html(myTemplate(jsonobj));
//}
//找到默认的发票类型并绑定显示
function FindDefaultAddressCheck() {
    hasdefault = false;
    var selectaddresssid = getcookie("seladdressid");
    if (selectaddresssid != null && selectaddresssid != undefined) {
        $("#ulAddressListBox li[AddressId='" + selectaddresssid + "']").each(function () {
            hasdefault = true;
            BindSelectAddress($(this).attr("AddressId"), $(this).attr("AccepterName"), $(this).attr("MobilePhone"), $(this).attr("ProvinceId"), $(this).attr("CityId"), $(this).attr("ProvinceName"), $(this).attr("CityName"), $(this).attr("Address"), $(this).attr("IsDefault"), $(this).attr("Email"));
        });
    }
    if (!hasdefault) {

        $("#ulAddressListBox li").each(function () {
            if ($(this).attr("IsDefault") == 1) {
                hasdefault = true;
                BindSelectAddress($(this).attr("AddressId"), $(this).attr("AccepterName"), $(this).attr("MobilePhone"), $(this).attr("ProvinceId"), $(this).attr("CityId"), $(this).attr("ProvinceName"), $(this).attr("CityName"), $(this).attr("Address"), $(this).attr("IsDefault"), $(this).attr("Email"));
            }
        });
    }
    if (!hasdefault) {
        //如果没有选择地址，自动以第一个地址为默认地址
        $("#ulAddressListBox li:first").each(function () {
            BindSelectAddress($(this).attr("AddressId"), $(this).attr("AccepterName"), $(this).attr("MobilePhone"), $(this).attr("ProvinceId"), $(this).attr("CityId"), $(this).attr("ProvinceName"), $(this).attr("CityName"), $(this).attr("Address"), $(this).attr("IsDefault"), $(this).attr("Email"));
        });
    } 
}

////选中地址事件
//function FunSelectAddress(liobj)
//{
//    BindSelectAddress($(liobj).attr("AddressId"), $(liobj).attr("AccepterName"), $(liobj).attr("MobilePhone"), $(liobj).attr("ProvinceId"), $(liobj).attr("CityId"), $(liobj).attr("ProvinceName"), $(liobj).attr("CityName"), $(liobj).attr("Address"), $(liobj).attr("IsDefault"), $(liobj).attr("Email"));
//}
//发票
function FunBillEditBoxShow() {
    $(".paycheck").hide();
    $(".divCanvasContainer").hide();
    $(".newBill").show();
}

function FunBillEditBoxhide() {
    $(".newBill").hide();
    $(".paycheck").show();
    $(".divCanvasContainer").show();
}
function FunBillEdit(obj)
{
    FunBillEditBoxShow();
    UpdateReceiptBill(obj);
}
//function BindSelectBill(billobj) { 
//    $("#txtBillSelectBillId").val(addressobj.BillId);
//    $("#txtBillSelectBillType").val(addressobj.BillType); 
//}


function FunSelectPayMethod(liobj)
{
    $("#txtSelectPayType").html($(liobj).attr("PayType"));
    $("#spanSelectPayTypeName").html($(liobj).attr("PayName"));
    setcookie("selpaymethodid", $(liobj).attr("PayType"));
    $(liobj).addClass("active").siblings().removeClass("active");
}
function BindSelectPayMethod(paytype,payname)
{
    $("#txtSelectPayType").html(paytype);
    $("#spanSelectPayTypeName").html(payname);
    setcookie("selpaymethodid", paytype);
    $("#ulpaymethodbox li[PayType='" + paytype + "']").addClass("active").siblings().removeClass("active");
}
//找到默认的发票类型并绑定显示
function FindDefaultPayMethodCheck() {
  
    var hasdefault = false;
    var selectpaymethodid = getcookie("selpaymethodid");
 
    if (selectpaymethodid != null && selectpaymethodid != undefined && selectpaymethodid != '') {
        $("#ulpaymethodbox li[paytype='" + selectpaymethodid + "'").each(function () {
            hasdefault = true;
            BindSelectPayMethod($(this).attr("PayType"), $(this).attr("PayName")); 
        });
    } 
    if (!hasdefault) {
        $("#ulpaymethodbox li").each(function () {
            if ($(this).attr("IsDefault") == 1) {
                hasdefault = true;
                BindSelectPayMethod($(this).attr("PayType"), $(this).attr("PayName"))
            }
        });
    }
    if (!hasdefault) {
        //如果没有选择地址，自动以第一个地址为默认地址
        $("#ulBillListBox li:first").each(function () {
            BindSelectPayMethod($(this).attr("PayType"), $(this).attr("PayName"))
        });
    }
}
//检查积分
function CheckIntegral() {
    var _jifen = $("#txtUseintegral").val();
    var _val = new Number(_jifen);
    if (isNaN(_val) || _val == undefined || _val < 1) {
        _val = 0;
        $("#txtUseintegral").val(_val);
        $("#spanhidUseintegral").html(_val);
        //$("#errormsg").html("");
        CalculateAmt();
        return;
    }
    if (_val == 0) {
        $("#txtUseintegral").val(_val);
        $("#spanhidUseintegral").html(_val);
        //$("#errormsg").html("");
        CalculateAmt();
        return;
    }
    else {
        var yushu = _val % 100;
        if (yushu != 0) {
            $("#txtUseintegral").val(_val);
            $("#spanhidUseintegral").html(_val);
            alert("亲，积分只能使用100的整数倍积分哦~");
            CalculateAmt();
            return;
        }
    }
    if (_val > 0) {
        $.post("/Member/GetIntegral", {}, function (data) {
            var _entity = eval("(" + data + ")");
            var hasjifen = _entity.AvailableIntegral;
            if (_val > hasjifen) {
                alert("亲，您的积分不够额");
            }
            $("#txtUseintegral").html(0);
            $("#spanhidUseintegral").html(0);
            CalculateAmt();
        });
    } 
}
//计算金额
function CalculateAmt() {
    var _totalPrice = parseFloat($("#spanhidallproductamt").text());//订单实际产品金额
    var _transFee = parseFloat($("#spanhidalltransfeeamt").text());//运费
    var huodongamt = parseFloat($("#spanhidhuodongamt").html());//活动优惠金额
    var youhuiamt = huodongamt;// 优惠总金额
    var preneedpayamt = _totalPrice - huodongamt;
    preneedpayamt = preneedpayamt + _transFee;
    var zhekouamt = 0;
    if (preneedpayamt <= 1) {
        $("#spanusejifenAmt").html(0);
        $("#txtUseintegral").val(0);
        $("#spanhidUseintegral").html(0);
        $("#spanneedpayamt").html(preneedpayamt);
    }
    else {
        var _jifen = $("#spanhidUseintegral").html();
        var jifen = new Number(_jifen);
        var jifenamt = parseFloat(jifen / 100);
        var needpayamt = preneedpayamt - jifenamt;
        if (needpayamt < 1) {
            var actamt = preneedpayamt - 1;
            var useintegral = actamt * 100;
            $("#spanusejifenAmt").html(actamt.toFixed(2));
            $("#txtUseintegral").val(useintegral.toFixed(2));
            $("#spanhidUseintegral").val(useintegral);
            $("#spanneedpayamt").html(1);
        }
        else {
            var memcouponid = $("#spanhidMemCouponId").html();
            if (memcouponid != "" && memcouponid != "0" && memcouponid > 0) {
                var couponid = $("#spanhidMemCouponId").attr("CouponId");
                var coupontype = $("#spanhidMemCouponId").attr("CouponType");
                var minimumReqAmount = parseFloat($("#spanhidMemCouponId").attr("MinimumReqAmount"));
                var couponValue = parseFloat($("#spanhidMemCouponId").attr("CouponValue"));

                if (coupontype == 1) {
                    if (needpayamt <= minimumReqAmount) {
                        $("#spanhidMemCouponId").html("0");
                        alert("优惠券最低金额限制，不能使用！");
                    }
                    else {
                        needpayamt = needpayamt - couponValue;
                        youhuiamt = youhuiamt + couponValue;
                        zhekouamt = couponValue;
                    }
                }
            }

            $("#spanusejifenAmt").html(jifenamt.toFixed(2));
            $("#spanzhekouamt").html(zhekouamt.toFixed(2))
            $("#spanneedpayamt").html((needpayamt).toFixed(2));
        }
    }
}


/*********新增地址显示隐藏页面**********/
$(function () {
    FunBindAddressListBox();//绑定地址
    FunBindBillListBox();
    FindDefaultPayMethodCheck();
    //$(".paycheck-dizhi-new").click(function () {
    //    FunAddressEditBoxShow();
    //    FunAddNewAddress();
    //})
    //$(".paycheck-bill-new").click(function () {
    //    FunBillEditBoxShow();
    //    FunAddNewBill();
    //})
    
    //$(".newAddress-tuichu").click(function () {
    //    FunAddressEditBoxhide();
    //});
    GetAssetCoupons();
    $("#txtUseintegral").blur(function () {
        CheckIntegral();
    });
   
})
function FunAddNewAddressOpen()
{
    FunAddressEditBoxShow();
    FunAddNewAddress();

}
//获取优惠券
function GetAssetCoupons() {
    $.post("/Member/GetUseCouponsByMem", {}, function (data) {
        var list = eval(data);
        if (list != null && list != "" && list.length > 0) {
            var myTemplatelist = Handlebars.compile($("#liMemCoupsList-template").html());  
            $('#ulCoupsBox').html(myTemplatelist(list)); 
        }
        else {
            $('#pnohascoups').show(); $('#ulCoupsBox').hide();
        }
    });
}

function FunSelectCoups(liobj) {
        $("#spanhidMemCouponId").html("0");
        var couponid = $(liobj).attr("couponid");
        $.post("/Member/GetUseCoupon", { couponid: couponid }, function (data) {
            var _returncode = eval("(" + data + ")");
            var status = -_returncode.Status;
            if (status == _TheArray[0]) {
                $("#coupserrormsg").html("");
                var memcoup = _returncode.Obj;
                var coup = memcoup.DicCoupons;
                var needpayamt =parseFloat($("#spanneedpayamt").html());
                if (needpayamt <= coup.MinimumReqAmount) {
                    $("#spanhidMemCouponId").html("0");
                    $("#coupserrormsg").html("优惠券最低金额限制，不能使用！");
                }
                else {
                    $("#spanhidMemCouponId").html(memcoup.Id);
                    $("#spanhidMemCouponId").attr("CouponId", coup.Id);
                    $("#spanhidMemCouponId").attr("CouponType", coup.CouponType);
                    $("#spanhidMemCouponId").attr("MinimumReqAmount", coup.MinimumReqAmount);
                    $("#spanhidMemCouponId").attr("CouponValue", coup.CouponValue);
                    $("#spanselectcoupon").html(coup.Name);
                    $(liobj).addClass("active").siblings().removeClass("active");
                    CalculateAmt();
                }
              
                return false;
            }
            else {
                $("#coupserrormsg").html(_TheArray[status]);
                return false;
            }
        }); 

}


function OrderSubmit() {
 
    var addressid = $("#txtSelectAddressId").val();
    if (addressid == "" || addressid == "0") {
        alert("请设置选择邮寄地址！");
        return;
    }
    var preordercode = $("#txtPreOrderCode").val();
    if (preordercode == "" || preordercode == "0") {
        alert("订单商品没有了！");
        return;
    }
    var paytype = $("#txtSelectPayType").html();
    if (paytype == "" || paytype == "0") {
        alert("请设置支付方式！");
        return;
    } 
    var expressid = getcookie("selexpress");
    if (expressid == null || expressid == undefined || expressid == "") {
        expressid = 0;
    }
    var remark = $("#txtRemark").val();
    var acceptername = $("#txtSelectAccepterName").val();
    var province = $("#txtSelectProvince").val();
    var city = $("#txtSelectCity").val();
    var address = $("#txtSelectAddress").val();
    var mobilephone = $("#txtSelectMobilePhone").val();
    var jifen = $("#spanhidUseintegral").html();
    var memcouponid = $("#spanhidMemCouponId").html();

    var param = {
        preordercode: preordercode, addressid: addressid, paytype: paytype,systype:4,//系统类别：4代表手机网站
        remark: remark, acceptername: acceptername, province: province, city: city, address: address, mobilephone: mobilephone,
        jifen: jifen, expressid: expressid, memcouponid: memcouponid
    }
    var billtype = $("#txtBillSelectBillType").val();
    //if (billtype != 1 && billtype != 2) {
    //    alert("亲，需要选择发票信息额");
    //    return;
    //}

    param["billtype"] = billtype;
    //普通发票
    if (billtype == 1) {
        param["billid"] = $("#txtBillSelectBillId").val();
        param["billtitle"] = $("#txtBillSelectBillCompanyName").val();
    }
    else if (billtype == 2)//增票
    {
        //if ($("#txtSelectBillVATStatus").val() != 1) {
        //    alert("亲，您的增票未审核通过额，请尽快与客服联系，谢谢");
        //    return;
        //}
        param["billid"] = $("#txtBillSelectBillId").val();
        param["billvatid"] = $("#txtBillSelectBillId").val();
    }
    param["ordertype"] = 4;//订单来源4代表手机
    $("#divordersubmit").attr("disabled", "disabled");
    var createorderurl = "/Order/CreateOrder";
    var jishi = 0; 
    if ($("#spanjishisong").length > 0) 
    { 
            jishi = $("#spanjishisong").html(); 
    }
    if (jishi == 1) createorderurl = "/Order/CreateOrderXuQiu";
    $.post(createorderurl, param, function (data) {
        var _returncode = eval("(" + data + ")");
        var status = -_returncode.Status; 
        if (status == _TheArray[0]) {
            var code = _returncode.Obj;
            if (jishi == 1)
            { 
               location.href = "/MobileOrder/XuQiuSuccess?code=" + code+"&s=1";
            }
            else {
                location.href = "/MobileOrder/OrderConfirm?code=" + code + "&s=1"; 
            }
           
            return false;
        }

        else {
            alert(_TheArray[status]);
            return false;
        }

    });
}

function GetBillComList(_pageindex) {
    var urlstr = "/Member/GetBillComList";
    var param = { pageindex: _pageindex }
    $.ajax({
        url: urlstr,
        type: "POST",
        //async: false,
        dataType: 'json',
        data: param,
        success: function (json) {//客户端jquery预先定义好的callback函数,成功获取跨域服务器上的json数据后,会动态执行这个callback函数
            if (json != null && json != "") {
                var objjson = eval(json);
                var _list = objjson.List;
            }
        }
    });
}


function FunSelectBill(liobj) { 
    var innertemplet = $("#BillSelectBox-template").html();
    var strhtmlinnercontent = innertemplet.replace(/{{BillTypeName}}/g, $(liobj).attr("BillTypeName"));
    var strhtmlinnercontent = strhtmlinnercontent.replace(/{{CompanyName}}/g, $(liobj).attr("CompanyName"));
    $("#liSelectBill").html(strhtmlinnercontent); 
    setcookie("selbillid", $(liobj).attr("BillId")); 
    $("#txtBillSelectBillId").val($(liobj).attr("BillId"));
    $("#txtBillSelectBillType").val($(liobj).attr("BillType"));
    $(liobj).addClass("active").siblings().removeClass("active");

}

//找到默认的发票类型并绑定显示
function FindDefaultBillCheck() {
    var hasdefault = false;
    var selectbillid = getcookie("selbillid");
    var selectbillobj; 
    if (selectbillid != null && selectbillid != undefined) {
       
        selectbillobj = $("#ulBillListBox li[BillId='" + selectbillid + "']");
        BindSelectBill($(selectbillobj).attr("BillId"), $(selectbillobj).attr("CompanyName"), $(selectbillobj).attr("BillType"), $(selectbillobj).attr("BillTypeName"));
        // $("#ulBillListBox li[BillId='" + selectbillid + "']").each(function () {
        //        alert($(this).attr("BillId"));
        //        BindSelectBill($(this).attr("BillId"), $(this).attr("CompanyName"), $(this).attr("BillType"), $(this).attr("BillTypeName"))
        //    });
    } 
    if (!hasdefault) {
          selectbillobj = $("#ulBillListBox li[IsDefault='1']");
          BindSelectBill($(selectbillobj).attr("BillId"), $(selectbillobj).attr("CompanyName"), $(selectbillobj).attr("BillType"), $(selectbillobj).attr("BillTypeName"));

        //$("#ulBillListBox li").each(function () {
        //    if ($(this).attr("IsDefault") == 1) {
        //        hasdefault = true;
        //        BindSelectBill($(this).attr("BillId"), $(this).attr("CompanyName"), $(this).attr("BillType"), $(this).attr("BillTypeName"))
        //    }
        //});
    }  
    if (!hasdefault) {
        selectbillobj = $("#ulBillListBox li:first");
        BindSelectBill($(selectbillobj).attr("BillId"), $(selectbillobj).attr("CompanyName"), $(selectbillobj).attr("BillType"), $(selectbillobj).attr("BillTypeName"));
        //如果没有选择地址，自动以第一个地址为默认地址
        //$("#ulBillListBox li:first").each(function () {
        //    hasdefault = true;
        //    BindSelectBill($(this).attr("BillId"), $(this).attr("CompanyName"), $(this).attr("BillType"), $(this).attr("BillTypeName"))
        //});
    }
   
}


function BindSelectBill(billid, billname, billtype, billtypename) {
    var innertemplet = $("#BillSelectBox-template").html();
    var strhtmlinnercontent = innertemplet.replace(/{{BillTypeName}}/g, billtypename);
    var strhtmlinnercontent = strhtmlinnercontent.replace(/{{CompanyName}}/g, billname);
    $("#liSelectBill").html(strhtmlinnercontent);
    $("#txtBillSelectBillId").val(billid);
    $("#txtBillSelectBillType").val(billtype);
    $("#txtBillSelectBillCompanyName").val(billname);
    $("#ulBillListBox li[BillId='" + billid + "']").addClass("active").siblings().removeClass("active");
    setcookie("selbillid", billid);
}