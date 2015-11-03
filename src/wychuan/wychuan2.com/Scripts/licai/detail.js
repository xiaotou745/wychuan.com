$(function () {
    initCheck();
    $(".J_hide .collapse-link").trigger("click");
    //$(".datetimepicker").datetimepicker({
    //    lang: "ch"
    //});
    dateformat($(".datetimepicker"));
    
    $('.input-daterange').datepicker({
        format: "yyyy-mm-dd",
        language: "zh-CN"
    });
    //类别联动
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
    //类别联动
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

    //最近一周
    $(document).delegate("[name=btnLastedWeek]", "click", function () {
        if ($(this).hasClass("active")) {
            return;
        }
        query($(this));
        $(this).siblings("button").removeClass("active");
        $(this).addClass("active");
    });
    //最近一月
    $(document).delegate("[name=btnLastedMonth]", "click", function () {
        if ($(this).hasClass("active")) {
            return;
        }
        query($(this));
        $(this).siblings("button").removeClass("active");
        $(this).addClass("active");
    });
    //日期范围查询
    $(document).delegate("[name=btnDateQuery]", "click", function () {
        query($(this));
    });
    //项目列表查询
    $(document).delegate("[name=selProjectQuery]", "change", function () {
        query($(this));
    });
    //成员查询
    $(document).delegate("[name=selMemberQuery]", "change", function () {
        query($(this));
    });
    //账户查询
    $(document).delegate("[name=selAccountQuery]", "change", function () {
        query($(this));
    });
    //借贷类型查询
    $(document).delegate("[name=selCreditorTypeQuery]", "change", function() {
        query($(this));
    });
    //保存按钮单击事件，修改一个账单
    $(document).delegate("[name=btnSave]", "click", function () {
        modifybill($(this));
    });
    //删除按钮单击事件，删除一个账单
    $(document).delegate("[name=btnDelete]", "click", function () {
        removebill($(this));
    });
    //标签切换事件
    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        var btnLastedWeek = $($(e.target).attr("href")).find("[name=btnLastedWeek]");
        btnLastedWeek.removeClass("active");
        btnLastedWeek.trigger("click");
    });
});

function dateformat(sender) {
    var curDate = new Date();
    var currYear = curDate.getFullYear();
    var thisDate = sender.val();
    var object = new Date(thisDate);
    
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

function initCheck() {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
}
//查询
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
        dateformat(tab.find("[name=billlist]").find(".datetimepicker"));
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
            dateformat($("#item" + id).find(".datetimepicker"));
        });
}