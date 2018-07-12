var addressarray = new Array();
function FunBindAddressListBox()
{
    var urlstr = "/Member/GetPostAddressList";
    var param = { pageindex: 1 }
    $.ajax({
        url: urlstr,
        type: "POST", 
        dataType: 'json',
        data: param,
        success: function (json) { 
            if (json != null && json != "") {
                var objjson = eval(json);
                var _list = objjson.List; 
                if (_list != null && _list != undefined && _list.length > 0) {  
                    var myTemplatelist = Handlebars.compile($("#liAddressList-template").html());
                    Handlebars.registerHelper("compare", function (v1, v2, options) {
                        if (v1 == v2) { 
                            return options.fn(this);
                        } else { 
                            return options.inverse(this);
                        }
                    }); 
                    $('#ulAddressListBox').html(myTemplatelist(_list));
                     
                    if(typeof(FindDefaultAddressCheck)=="function"){
                        FindDefaultAddressCheck();
                    } ;
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
//选中地址事件
function FunSelectAddress(liobj) {
    BindSelectAddress($(liobj).attr("AddressId"), $(liobj).attr("AccepterName"), $(liobj).attr("MobilePhone"), $(liobj).attr("ProvinceId"), $(liobj).attr("CityId"), $(liobj).attr("ProvinceName"), $(liobj).attr("CityName"), $(liobj).attr("Address"), $(liobj).attr("IsDefault"), $(liobj).attr("Email"));
}

function BindSelectAddress(addressid, AccepterName, MobilePhone, ProvinceId, CityId, ProvinceName, CityName, Address, IsDefault, Email) {
 
    setcookie("seladdressid", addressid);
  
    var obj = { Id: addressid, AccepterName: AccepterName, MobilePhone: MobilePhone, ProvinceName: ProvinceName, CityName: CityName, Address: Address, IsDefault: IsDefault, Email: Email, ProvinceId: ProvinceId, CityId: CityId };
 
    var myTemplateentity = Handlebars.compile($("#AddressSelectBox-template").html());
 
    $('#divSelectAddress').html(myTemplateentity(obj));

    $("#txtSelectAddressId").val(addressid);
    $("#txtSelectAccepterName").val(AccepterName);
    $("#txtSelectProvince").val(ProvinceId);
    $("#txtSelectCity").val(CityId);
    $("#txtSelectAddress").val(Address);
    $("#txtSelectMobilePhone").val(MobilePhone);
    $("#ulAddressListBox li[AddressId='" + addressid + "']").addClass("active").siblings().removeClass("active");
}
//提交新地址或者更改后的地址
function FunSaveAddressEdit() {

    var _addressid = $.trim($("#txtAddressEditAddressId").val());
    var _memid = $.trim($("#txtAddressEditMemId").val());
    var _province = $.trim($("#hidAddressEditProvince").val());
    var _city = $.trim($("#hidAddressEditCity").val());
    var _countryname = $.trim($("#txtAddressEditCountryName").val());
    var _cttype = $.trim($("#txtAddressEditCtType").val());
    var _address = $.trim($("#txtAddressEditAddress").val());
    var _acceptername = $.trim($("#txtAddressEditAccepterName").val());
    var _mobilephone = $.trim($("#txtAddressEditMobilePhone").val());
    var _email = $.trim($("#txtAddressEditEmail").val());
    var _isdefault = $.trim($("#hidAddressEditIsDefault").val());
    //var Telephone = $.trim($("#txtTelephone").val());

    if ($("#txtAccepterName").val() == "") {
        $("#Addresserrormsg").html("请输入收货人姓名!");
        return;
    } 
    if ($("#selProvince option:selected").text() == "--选择省份--") {
        $("#Addresserrormsg").html("请选择省份!");
        return;
    }
    if ($("#selCity option:selected").text() == "--选择城市--") {
        $("#Addresserrormsg").html("请选择城市!");
        return;
    } 
    if ($("#txtAddress").val() == "") {
        $("#Addresserrormsg").html("请输入详细地址!");
        return;
    }
    if (_mobilephone == "") {
        $("#Addresserrormsg").html("请输入手机号码!");
        return;
    }
    if (_address.length > 500) {
        $("#Addresserrormsg").html("详细地址长度不能超过500!");
        return;
    } 
    $.post("/Member/AddPostAddress", {
        addressid: _addressid, memid: _memid, province: _province,
        city: _city, cttype: _cttype, mobilephone: _mobilephone, address: _address,
        countryname: _countryname, Telephone: "", email: _email, acceptername: _acceptername, isdefault: _isdefault
    }, function (data) {
    
        var _returnsult = eval("(" + data + ")");
        var status = -_returnsult.Status;
        if (status == _TheArray[0]) {
            var jsonobj = _returnsult.Obj;
             if (jsonobj != null && jsonobj != undefined) { 
                 var myTemplate = Handlebars.compile($("#liAddressEdit-template").html());
                 Handlebars.registerHelper("compare", function (v1, v2, options) {
                     if (v1 == v2) {
                         return options.fn(this);
                     } else {
                         return options.inverse(this);
                     }
                 });  
                var liobj = $("#ulAddressListBox").children("*[AddressId='" + jsonobj.Id + "']");
                if (liobj != null && liobj != undefined && liobj.length > 0)
                {
                       //$(liobj).html(myTemplate(jsonobj)); 
                    $(liobj).after(myTemplate(jsonobj));
                     $(liobj).remove();
                
                }
                else {   
                    $("#ulAddressListBox").append(myTemplate(jsonobj));
                    
                } 
               FunSelectAddress($("#ulAddressListBox").children("*[AddressId='" + jsonobj.Id + "']"));
               FunAddressEditBoxhide();
              
            }
        }
        else {
            $("#Addresserrormsg").html(_TheArray[status]);
        }
    });
     
}

function FunAddNewAddress()
{
   $("#txtAddressEditAddressId").val("");
   $("#hidAddressEditProvince").val("0");
   $("#selAddressEditProvince option").filter(function () { return $(this).val() == "0"; }).prop("selected", true);//一个标签具有文本和属性
   $("#hidAddressEditCity").val("0");  
   $("#selAddressEditCity option").filter(function () { return $(this).val() == "0"; }).prop("selected", true);//一个标签具有文本和属性
   $("#txtAddressEditAddress").val("");
   $("#txtAddressEditAccepterName").val("");
   $("#txtAddressEditMobilePhone").val("");
   $("#txtAddressEditEmail").val("");
   $("#hidAddressEditIsDefault").val("0");
   $(".am-icon-toggle-off").show();
   $(".am-icon-toggle-on").hide();
   $("#Addresserrormsg").html("");
}
//显示修改界面,获取收货地址并更新
function UpdateReceiptAddress(obj) {

    $("#Addresserrormsg").html("");
    var objdiv = $(obj);
    $("#txtAddressEditAddressId").val($(objdiv).attr("AddressId"));
    $("#hidAddressEditProvince").val($(objdiv).attr("ProvinceId"));
    $("#hidAddressEditCity").val($(objdiv).attr("CityId"));
    $("#txtAddressEditAddress").val($(objdiv).attr("Address"));
    $("#txtAddressEditAccepterName").val($(objdiv).attr("AccepterName"));
    $("#txtAddressEditMobilePhone").val($(objdiv).attr("MobilePhone"));
    //$("#txtTelephone").val($(objdiv).attr("Telephone"));
    $("#txtAddressEditEmail").val($(objdiv).attr("Email")); 
    $("#hidAddressEditIsDefault").val($(objdiv).attr("IsDefault"));
    if ($(objdiv).attr("IsDefault") == "1") {
        $(".am-icon-toggle-off").hide();
        $(".am-icon-toggle-on").show();
    }
    else {   $(".am-icon-toggle-off").show();
        $(".am-icon-toggle-on").hide();
    }
    $("#selAddressEditProvince option").filter(function () { return $(this).val() == $(objdiv).attr("ProvinceId"); }).prop("selected", true);//一个标签具有文本和属性
    var param = { pcode: $("#selAddressEditProvince").val() };
    BindDropParent($("#selAddressEditCity"), "/Common/GetCityJson", param, $(objdiv).attr("CityId"));

}

$(function () {
    BindDropParent($("#selAddressEditProvince"), "/Common/GetProvinceJson", ""); //绑定省份下拉框
    $("#selAddressEditProvince").change(function () { //省份下拉框change事件
        $("#hidAddressEditProvince").val($(this).val());
        $("#hidAddressEditCity").val("");
        var param = { pcode: $(this).val() }
        BindDropParent($("#selAddressEditCity"), "/Common/GetCityJson", param); //绑定市下拉框
    }); 
    $("#selAddressEditCity").change(function () { //绑定市下拉框change事件
        $("#hidAddressEditCity").val($(this).val());
    }); 
    $(".am-icon-toggle-off").click(function () {
        $(this).hide();
        $(".am-icon-toggle-on").show();
        $("#hidAddressEditIsDefault").val($(this).val());
    })
    $(".am-icon-toggle-on").click(function () {
        $(this).hide();
        $(".am-icon-toggle-off").show();
        $("#hidAddressEditIsDefault").val( $(this).val()); 
    }) 
})