$(function () {
    //$(".datetimepicker").datetimepicker({
    //    lang: "ch"
    //});
    dateformat($("#modalBill").find("[name=txtConsumeTime]"));
    
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
    //类别一级菜单事件
    $("[name=selFirstCategory]").bind("change", function () {
        var $second = $(this).next().next();
        var firstId = $(this).val();
        var seconds = $.grep(window.categories, function (item, index) {
            return item.ParentId == firstId;
        });
        $second.html("");
        $.each(seconds, function (i, category) {
            $second.append("<option value='" + category.Id + "'>" + category.Name + "</option>");
        });
    });

    //添加模板按钮
    $("#btnAddTemplate").bind("click", function () {
        $("#modalExpendTemplate").find("form").find("[name=Id]").val(0);
        $("#modalExpendTemplate").modal("show");
    });
    //保存模板
    $("#btnSaveTemplate").bind("click", function () {
        save();
    });
    //删除
    $(document).delegate(".bill .a1", "click", function () {
        if (confirm("确定要删除此模板吗？")) {
            var id = $(this).siblings("input[name=Id]").val();
            remove(id);
        }
        return false;
    });
    //编辑
    $(document).delegate(".bill .a2", "click", function (e) {
        var dataTemplate = $(this).parent().data("template");
        setModel(dataTemplate);
        $("#modalExpendTemplate").modal("show");
        return false;
    });

    $(document).delegate(".bill", "click", function () {
        var dataTemplate = $(this).data("template");
        setBill(dataTemplate);
        $("#modalBill").modal("show");
    });
    $("#btnSaveBill").bind("click", function () {
        saveBill();
    });
    
    $("#modalExpendTemplate").bind("shown.bs.modal", function(event) {
        $("#modalExpendTemplate").find("form").find("[name=txtName]").select();
        $("#modalExpendTemplate").find("form").find("[name=txtName]").focus();
    });
    $("#modalBill").bind("shown.bs.modal", function (event) {
        $("#modalBill").find("form").find("[name=txtPrice]").select();
        $("#modalBill").find("form").find("[name=txtPrice]").focus();
        $("#modalBill").find("form").find("[name=txtPrice]").trigger("click");
        //$("#modalBill").find("form").find("[name=txtPrice]").keyboard({
        //    layout: 'num',
        //    restrictInput: true, // Prevent keys not in the displayed keyboard from being typed in
        //    preventPaste: true,  // prevent ctrl-v and right click
        //    autoAccept: true
        //}).addTyping();
    });
});
function dateformat(sender) {
    var curDate = new Date();
    var currYear = curDate.getFullYear();
    var opt = {
        preset: 'datetime',
        //theme: 'android-ics light', //皮肤样式
        //display: 'modal', //显示方式 
        //mode: 'scroller', //日期选择模式
        showNow: true,
        lang: 'zh',
        startYear: currYear - 2, //开始年份
        endYear: currYear + 2 //结束年份
    };
    sender.mobiscroll(opt).datetime(opt);;
}
//删除模板
function remove(id) {
    $.ajax({
        url: "/api/bill/removetemplate",
        type: "get",
        data: { id: id },
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                refresh();
            }
        }
    });
}
//保存模板
function save() {
    var billTemplate = getModel();
    if (billTemplate == null) {
        return;
    }
    $.ajax({
        url: "/api/bill/savetemplate",
        type: "post",
        data: billTemplate,
        dataType: "json",
        success: function (resp) {
            $("#modalExpendTemplate").modal("hide");
            refresh();
        }
    });
}
//获取模板信息
function getModel() {
    var form = $("#modalExpendTemplate").find("form");
    if (form.find("[name=txtName]").val() == "") {
        alert("模板名称不能为空.");
        return null;
    }
    var isBaoXiao = form.find("[name=chkBaoXiao]").is(':checked');
    if (isBaoXiao == true) {
        isBaoXiao = 1;
    } else {
        isBaoXiao = 0;
    }
    var bill = {
        ID: form.find("[name=Id]").val(),
        Name: form.find("[name=txtName]").val(),
        BookId: form.find("[name=selBook]").val(),
        FirstCategoryId: form.find("[name=selFirstCategory]").val(),
        FirstCategory: form.find("[name=selFirstCategory] option:selected").text(),
        SecondCategoryId: form.find("[name=selCategory]").val(),
        SecondCategory: form.find("[name=selCategory] option:selected").text(),
        AccountId: form.find("[name=selAccount]").val(),
        AccountDesc: form.find("[name=selAccount] option:selected").text(),
        Remark: form.find("[name=txtRemark]").val(),
        BusinessId: form.find("[name=selBusiness]").val(),
        Business: form.find("[name=selBusiness] option:selected").text(),
        MemberId: form.find("[name=selMember]").val(),
        Member: form.find("[name=selMember] option:selected").text(),
        ProjectId: form.find("[name=selProject]").val(),
        Project: form.find("[name=selProject] option:selected").text(),
        BaoXiao: isBaoXiao
    };
    if (bill.AccountId == 0) {
        bill.AccountDesc = "";
    }
    return bill;
}
//设置模板信息
function setModel(model) {
    var form = $("#modalExpendTemplate").find("form");

    form.find("[name=Id]").val(model.ID);
    form.find("[name=txtName]").val(model.Name);
    form.find("[name=selBook]").val(model.BookId);
    form.find("[name=selFirstCategory]").val(model.FirstCategoryId);
    form.find("[name=selFirstCategory]").trigger("change");
    form.find("[name=selCategory]").val(model.SecondCategoryId);
    form.find("[name=selAccount]").val(model.AccountId);
    form.find("[name=txtRemark]").val(model.Remark);
    if (model.BusinessId != 0) {
        form.find("[name=selBusiness]").val(model.BusinessId);
    }
    form.find("[name=selMember] option:selected").val(model.MemberId);
    form.find("[name=selProject] option:selected").val(model.ProjectId);
    if (model.BaoXiao == 1) {
        form.find("[name=chkBaoXiao]").iCheck("check");
    } else {
        form.find("[name=chkBaoXiao]").iCheck("uncheck");
    }
}

function refresh() {
    $.ajax({
        url: "/admin/licai/refreshbill",
        type: "get",
        success: function (resp) {
            $("#content").html(resp);
        }
    });
}

//账单
function setBill(model) {
    var form = $("#modalBill").find("form");

    form.find("[name=templateId]").val(model.ID);
    form.find("[name=BookId]").val(model.BookId);
    form.find("[name=BaoXiao]").val(model.BaoXiao);
    form.find("[name=ProjectId]").val(model.ProjectId);
    form.find("[name=Project]").val(model.Project);
    
    form.find("[name=txtConsumeTime]").mobiscroll('setDate', new Date(), true);
    
    form.find("[name=selFirstCategory]").val(model.FirstCategoryId);
    form.find("[name=selFirstCategory]").trigger("change");
    form.find("[name=selCategory]").val(model.SecondCategoryId);
    form.find("[name=selAccount]").val(model.AccountId);
    form.find("[name=txtRemark]").val(model.Remark);
    if (model.BusinessId != 0) {
        form.find("[name=selBusiness]").val(model.BusinessId);
    }
    form.find("[name=selMember] option:selected").val(model.MemberId);
    form.find("[name=selProject] option:selected").val(model.ProjectId);
}
function getBill() {
    var form = $("#modalBill").find("form");
    
    var bill = {
        BookId: form.find("[name=BookId]").val(),
        Price: form.find("[name=txtPrice]").val(),
        FirstCategoryId: form.find("[name=selFirstCategory]").val(),
        FirstCategory: form.find("[name=selFirstCategory] option:selected").text(),
        SecondCategoryId: form.find("[name=selCategory]").val(),
        SecondCategory: form.find("[name=selCategory] option:selected").text(),
        AccountId: form.find("[name=selAccount]").val(),
        ConsumeTime: form.find("[name=txtConsumeTime]").val(),
        Remark: form.find("[name=txtRemark]").val(),
        BusinessId: form.find("[name=selBusiness]").val(),
        Business: form.find("[name=selBusiness] option:selected").text(),
        MemberId: form.find("[name=selMember]").val(),
        Member: form.find("[name=selMember] option:selected").text(),
        ProjectId: form.find("[name=ProjectId]").val(),
        Project: form.find("[name=Project]").val(),
        BaoXiao: form.find("[name=BaoXiao]").val()
    };
    return bill;
}
function saveBill() {
    var bill = getBill();
    console.log(bill);
    return;
    $.ajax({
        url: "/api/bill/fastsave",
        type: "post",
        data: bill,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#modalBill").modal("hide");
            }
        }
    });
}

/* 质朴长存法  by lifesinger */
function pad(num, n) {
    var len = num.toString().length;
    while (len < n) {
        num = "0" + num;
        len++;
    }
    return num;
}