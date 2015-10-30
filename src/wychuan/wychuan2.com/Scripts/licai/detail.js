$(function () {
    initCheck();
    $(".J_hide .collapse-link").trigger("click");
    $(".datetimepicker").datetimepicker({
        lang: "ch"
    });

    $('.input-daterange').datepicker({
        format: "yyyy-mm-dd",
        language: "zh-CN"
    });

    $(document).delegate("[name=selFirstCategory]", "change", function () {
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
    $(document).delegate("[name=selFirstCategoryQuery]", "change", function () {
        var $second = $(this).next().next();
        var firstId = $(this).val();
        var seconds = [{ Id: 0, Name: "二级类别" }];
        if (firstId != 0) {
            var secondLst = $.grep(window.categories, function (item, index) {
                return item.ParentId == firstId;
            });
            seconds = seconds.concat(secondLst);
        }
        $second.html("");
        $.each(seconds, function (i, category) {
            $second.append("<option value='" + category.Id + "'>" + category.Name + "</option>");
        });
    });

    $(document).delegate("[name=btnLastedWeek]", "click", function () {
        if ($(this).hasClass("active")) {
            return;
        }
        query($(this));
        $(this).siblings("button").removeClass("active");
        $(this).addClass("active");
    });
    $(document).delegate("[name=btnLastedMonth]", "click", function () {
        if ($(this).hasClass("active")) {
            return;
        }
        query($(this));
        $(this).siblings("button").removeClass("active");
        $(this).addClass("active");
    });
    $(document).delegate("[name=btnDateQuery]", "click", function () {
        query($(this));
    });
    $(document).delegate("[name=selProjectQuery]", "change", function () {
        query($(this));
    });
    $(document).delegate("[name=selMemberQuery]", "change", function () {
        query($(this));
    });
    $(document).delegate("[name=selAccountQuery]", "change", function () {
        query($(this));
    });
    $(document).delegate("[name=btnSave]", "click", function () {
        modifybill($(this));
    });
    $(document).delegate("[name=btnDelete]", "click", function () {
        removebill($(this));
    });
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var btnLastedWeek = $($(e.target).attr("href")).find("[name=btnLastedWeek]");
        btnLastedWeek.removeClass("active");
        btnLastedWeek.trigger("click");
    });
});

function initCheck() {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
}

function query(sender) {
    var tab = sender.parents(".J_Tab");
    var billtype = tab.data("billtype");
    var queryInfo = {
        DetailType: billtype
    };
    var querytype = sender.data("querytype");
    if (querytype != undefined) { //最近一周,最近一月两个按钮
        queryInfo.TimeZone = querytype;
    } else { //其它查询条件
        queryInfo.StartTime = tab.find("[name=start]").val();
        queryInfo.EndTime = tab.find("[name=end]").val();
        queryInfo.AccountId = tab.find("[name=selAccountQuery]").val();
        queryInfo.ProjectId = tab.find("[name=selProjectQuery]").val();
        queryInfo.MemberId = tab.find("[name=selMemberQuery]").val();
        queryInfo.FirstCategoryId = tab.find("[name=selFirstCategoryQuery]").val();
        queryInfo.SecondCategoryId = tab.find("[name=selCategoryQuery]").val();
        queryInfo.BaoXiao = tab.find("[name=selBaoXiao]").val();
        queryInfo.AccountIdTo = tab.find("[name=selAccountToQuery]").val();
        queryInfo.CreditorType = tab.find("[name=selCreditorTypeQuery]").val();
    }
    $.get("/admin/licai/querydetails", queryInfo, function (resp) {
        $("[name=billlist]").html("");
        tab.find("[name=billlist]").html(resp);
    });
}

function modifybill(sender) {
    var form = sender.parents("[name=billEditForm]");
    var isBaoXiao = form.find("[name=chkBaoXiao]").is(':checked');
    if (isBaoXiao == true) {
        isBaoXiao = 1;
    } else {
        isBaoXiao = 0;
    }
    var billInfo = {
        ID: form.find("[name=Id]").val(),
        Price: form.find("[name=txtPrice]").val(),
        ConsumeTime: form.find("[name=txtConsumeTime]").val(),
        Remark: form.find("[name=txtRemark]").val(),
        FirstCategoryId: form.find("[name=selFirstCategory]").val(),
        FirstCategory: form.find("[name=selFirstCategory] option:selected").text(),
        SecondCategoryId: form.find("[name=selCategory]").val(),
        SecondCategory: form.find("[name=selCategory] option:selected").text(),
        AccountId: form.find("[name=selAccount]").val(),
        ToAccountId: form.find("[name=selAccountTo]").val(),
        BusinessId: form.find("[name=selBusiness]").val(),
        Business: form.find("[name=selBusiness] option:selected").text(),
        MemberId: form.find("[name=selMember]").val(),
        Member: form.find("[name=selMember] option:selected").text(),
        ProjectId: form.find("[name=selProject]").val(),
        Project: form.find("[name=selProject] option:selected").text(),
        RefundTime: form.find("[name=txtRefundTime]").val(),
        RefundNotice: form.find("[name=selRefundNotice]").val(),
        CreditorType: form.find("[name=selCreditorType]").val(),
        BaoXiao: isBaoXiao
    };
    saveBill(billInfo);
}

function removebill(sender) {
    var form = sender.parents("[name=billEditForm]");
    var id = form.find("[name=Id]").val();
    
    $.ajax({
        url: "/api/bill/remove",
        type: "get",
        data: { id: id },
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#item" + id).remove();
            }
        }
    });
}

function saveBill(bill) {
    //return;
    $.ajax({
        url: "/api/bill/save",
        type: "post",
        data: bill,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                refreshBillItem(bill.ID);
            }
        }
    });
}

function refreshBillItem(id) {
    $.get("/admin/licai/billitem", { id: id },
        function (resp) {
            $("#item" + id).html(resp);
        });
}