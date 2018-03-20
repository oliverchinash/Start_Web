//发票列表
function FunBindBillListBox()
{ 
    var urlstr = "/Member/GetBillComList";
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
                    //var hasbillcookie = false;
                    //var selectbillid = getcookie("selbillid");
                    //if (selectbillid != null && selectbillid != undefined) {
                        for (var i = 0; i < _list.length; i++) {
                    //        if (selectbillid == _list[i].Id) {
                    //            hasbillcookie = true;
                    //            var myTemplateentity = Handlebars.compile($("#BillSelectBox-template").html());
                    //            $('#liSelectBill').html(myTemplateentity(_list[i]));
                    //            $("#txtBillSelectBillId").val(selectbillid);
                    //            $("#txtBillSelectBillCompanyName").val(_list[i].CompanyName);
                    //        }
                            if(_list[i].BillType==2)
                            {
                                BindBillVATData(_list[i]);
                            }
                        }
                    //}
                    //if (!hasbillcookie) {
                    //    //如果没有选择地址，自动以第一个地址为默认地址
                    //    selectbillid = _list[0].Id;
                    //    var myTemplateentity = Handlebars.compile($("#BillSelectBox-template").html());
                    //    $('#liSelectBill').html(myTemplateentity(_list[i]));
                    //    setcookie("selbillid", selectbillid);
                    //    BindSelectBill(_list[0]);
                    //} 
                    var myTemplatelist = Handlebars.compile($("#liBillList-template").html());
                    Handlebars.registerHelper("compare", function (v1, v2, options) {
                        if (v1 == v2) { 
                            return options.fn(this);
                        } else { 
                            return options.inverse(this);
                        }
                    });

                    $('#ulBillListBox').html(myTemplatelist(_list)); 
                    if(typeof(FindDefaultBillCheck)=="function")
                    { 
                        FindDefaultBillCheck();
                    }   
                }
                else {
                    if (typeof (BindSelectBill) == "function") {
                         
                            BindSelectBill("0", "无", "0", "无需发票");
                         
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


//绑定增票输入框
function BindBillVATData(billobj) {
    $("#txtBillEditCompanyName").val(billobj.CompanyName);
    $("#txtBillEditCompanyCode").val(billobj.CompanyCode);
    $("#txtBillEditCompanyAddress").val(billobj.CompanyAddress);
    $("#txtBillEditCompanyPhone").val(billobj.CompanyPhone);
    $("#txtBillEditCompanyBank").val(billobj.CompanyBank);
    $("#txtBillEditBankAccount").val(billobj.BankAccount);
    $("#txtBillEditBillVATId").val(billobj.Id); 
    //审核通过的增票不能修改
    if(billobj.Status==1)
    {
        $("#divbillselectcontent>div[billtype='2'] input").attr("readonly", "readonly");
    }

}

    $(function () { 
        //$(".paycheck-fapiao-xinzeng").click(function () {  //新增发票信息
        //    alert(1);
        //    FunBillEditBoxShow();
        //    alert(2);
        //    $("#spanbilleditbillid").attr("billid", "0");
        //    $("#spanbilleditbillid").attr("billtype", "1");
        //    $("#tabbilltype>li").removeClass("am-active");
        //    $("#tabbilltype>li[billtype=1]").addClass("am-active");
        //    $("#divbillselectcontent>div").removeClass("am-active");
        //    $("#divbillselectcontent>div[billtype=1]").addClass("am-active");
        //    if ($("#txtBillEditBillVATId").val() != "" && $("#txtBillEditBillVATId").val() != "0")
        //    {
        //        $("#tabbilltype>li[billtype=2]").hide();  
        //    }
        //});
        //$(".invoice-tuichu").click(function () {
        //    FunBillEditBoxhide();
        //}); 
    })

    function FunBillXinZeng() {
        FunBillEditBoxShow();
        $("#spanbilleditbillid").attr("billid", "0");
        $("#spanbilleditbillid").attr("billtype", "1");
        $("#tabbilltype>li").removeClass("am-active");
        $("#tabbilltype>li[billtype=1]").addClass("am-active");
        $("#divbillselectcontent>div").removeClass("am-active");
        $("#divbillselectcontent>div[billtype=1]").addClass("am-active");
        if ($("#txtBillEditBillVATId").val() != "" && $("#txtBillEditBillVATId").val() != "0") {
            $("#tabbilltype>li[billtype=2]").hide();
        }
    }
    function  FunSelectBillType(billtype)
    {
        var editbillid=$("#spanbilleditbillid").attr("billid"); 
        if (editbillid == null || editbillid==undefined||editbillid == "" || editbillid == "0")
        {
            $("#spanbilleditbillid").attr("billtype", billtype);
        }
    }
 
    function FunAddNewBill()
    {
        $("#spanbilleditbillid").attr("billtype", 1);
        $("#spanbilleditbillid").attr("billid", 0);
        $("#txtBillEditBillTitle").val("");
    }
    function UpdateReceiptBill(obj) {
        var billtype = $(obj).attr("BillType");
        if (billtype == 1) {
            $("#spanbilleditbillid").attr("billtype", billtype);
            $("#spanbilleditbillid").attr("billid", $(obj).attr("BillId"));
            $("#txtBillEditBillTitle").val($(obj).attr("CompanyName"));
            $("#tabbilltype>li").hide();
            $("#tabbilltype>li[billtype=" + billtype + "]").show().addClass("am-active");
            $("#divbillselectcontent>div").removeClass("am-active");
            $("#divbillselectcontent>div[billtype=" + billtype + "]").addClass("am-active");
        }
        else if (billtype == 2) {
            var status = $(obj).attr("Status");
            if (status == 0) {
                $("#spanbilleditbillid").attr("billtype", billtype);
                $("#spanbilleditbillid").attr("billid", $(obj).attr("BillId"));
                $("#txtBillEditCompanyName").val($(obj).attr("CompanyName"));
                $("#txtBillEditCompanyCode").val($(obj).attr("CompanyCode"));
                $("#txtBillEditCompanyAddress").val($(obj).attr("CompanyAddress"));
                $("#txtBillEditCompanyPhone").val($(obj).attr("CompanyPhone"));
                $("#txtBillEditCompanyBank").val($(obj).attr("CompanyBank"));
                $("#txtBillEditBankAccount").val($(obj).attr("BankAccount"));
                $("#txtBillEditBillVATId").val($(obj).attr("Id"));
                $("#tabbilltype>li").hide();
                $("#tabbilltype>li[billtype=" + billtype + "]").show().addClass("am-active");
                $("#divbillselectcontent>div").removeClass("am-active");
                $("#divbillselectcontent>div[billtype=" + billtype + "]").addClass("am-active");
            }
        }
       
    }


    function SaveBill() 
    {
         
        var billtype = $("#spanbilleditbillid").attr("billtype");
        var param;
        if (billtype == 1) {
            var billid = $("#spanbilleditbillid").attr("billid");
            var billcompangname = $.trim($("#txtBillEditBillTitle").val());
            if (billcompangname == "") {
                alert("亲，发票抬头需要填写额");
                return false;
            }
            param = { billid: billid, billtype: billtype, companyName: billcompangname };
        }
        else if (billtype == 2) {
            var billid = $("#spanbilleditbillid").attr("billid"); 
            var companyName = $.trim($("#txtBillEditCompanyName").val());
            var companyCode = $("#txtBillEditCompanyCode").val();
            var companyaddress = $("#txtBillEditCompanyAddress").val();
            var companyPhone = $("#txtBillEditCompanyPhone").val();
            var companyBank = $("#txtBillEditCompanyBank").val();
            var bankAccount = $("#txtBillEditBankAccount").val();
            if (companyName == "") {
                alert("亲，发票抬头需要填写额");
                return false;
            }
            param = {
                billid: billid, billtype: billtype, companyCode:companyCode,companyName: companyName, companyaddress: companyaddress, companyPhone: companyPhone,
                companyBank: companyBank, bankAccount: bankAccount 
            };
        }
        $.post("/Member/UpdateBillEditSubmit", param, function (data) {
            var _returnsult = eval("(" + data + ")");
            var status = -_returnsult.Status;
            if (status == _TheArray[0]) {
                var obj = _returnsult.Obj;
                if (typeof (BindSelectBill) == "function") {
                    BindSelectBill(obj.Id, obj.CompanyName, obj.BillType, obj.BillTypeName);
                }
           
                FunBillEditBoxhide();
                var myTemplatelist = Handlebars.compile($("#liBillList-template").html().replace("{{#each this}}","").replace("{{/each}}",""));
                Handlebars.registerHelper("compare", function (v1, v2, options) {
                    if (v1 == v2) {
                        return options.fn(this);
                    } else {
                        return options.inverse(this);
                    }
                });
                if ($("#ulBillListBox li[BillId='" + obj.Id + "']").length > 0) {
                    $("#ulBillListBox li[BillId='" + obj.Id + "']").replaceWith(myTemplatelist(obj));
                }
                else {
                    $('#ulBillListBox').append(myTemplatelist(obj)); 
                }
                   $("#ulBillListBox li").removeClass("active");
                    $("#ulBillListBox li[BillId='" + obj.Id + "']").addClass("active"); 
                FunCloseCanvas('divBillCanvas');
            }
        });
       
    }