
$(function () {
    $(".datetimepicker").datetimepicker({
        lang: "ch"
    });
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
    $("#tab1").find("[name=txtPrice]").select();
    $("#tab1").find("[name=txtPrice]").focus();

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

    $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
        console.log(e, e.target, e.relatedTarget);
        $($(e.target).attr("href")).find("[name=txtPrice]").select();
        $($(e.target).attr("href")).find("[name=txtPrice]").focus();
    });

    $("form").delegate("[name=btnSave]", "click", saveBill);
    $("form").delegate("[name=btnSaveContinue]", "click", saveBill);

});

function saveBill(e) {
    var form = $(e.delegateTarget);
    var btn = $(e.currentTarget);
    var bill = getModel(form, btn);
    //return;
    $.ajax({
        url: "/api/bill/save",
        type: "post",
        data: bill,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                alert("保存成功.");
            }
        }
    });
}

function getModel(form, sender) {
    var isBaoXiao = form.find("[name=chkBaoXiao]").is(':checked');
    if (isBaoXiao == true) {
        isBaoXiao = 1;
    } else {
        isBaoXiao = 0;
    }
    var bill = {
        BookId: form.find("[name=selBook]").val(),
        Price: form.find("[name=txtPrice]").val(),
        FirstCategoryId: form.find("[name=selFirstCategory]").val(),
        FirstCategory: form.find("[name=selFirstCategory] option:selected").text(),
        SecondCategoryId: form.find("[name=selCategory]").val(),
        SecondCategory: form.find("[name=selCategory] option:selected").text(),
        AccountId: form.find("[name=selAccount]").val(),
        ToAccountId: form.find("[name=selAccountTo]").val(),
        ConsumeTime: form.find("[name=txtConsumeTime]").val(),
        Remark: form.find("[name=txtRemark]").val(),
        DetailType: sender.data("type"),
        BusinessId: form.find("[name=selBusiness]").val(),
        Business: form.find("[name=selBusiness] option:selected").text(),
        MemberId: form.find("[name=selMember]").val(),
        Member: form.find("[name=selMember] option:selected").text(),
        ProjectId: form.find("[name=selProject]").val(),
        Project: form.find("[name=selProject] option:selected").text(),
        RefundTime: form.find("[name=txtRefundTime]").val(),
        RefundNotice: form.find("[name=selRefundNotice]").val(),
        CreditorType: form.find("[name=selCreditorType] option:selected").text(),
        BaoXiao: isBaoXiao
    };

    return bill;
}