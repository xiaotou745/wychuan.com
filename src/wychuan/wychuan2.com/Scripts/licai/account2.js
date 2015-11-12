
$(document).ready(function() {
    initCheck();

    $('#accountModal').on('show.bs.modal', function(event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var modal = $(this);
        var recipient = button.data('whatever');
        modal.find('.modal-title').text(recipient);

        var operateType = button.data("type");

        if (operateType == 1) { //新增
            modal.find("#Id").val(0);
            modal.find("#Type").val(modal.find("#Type option:first").val());
            modal.find("#Name").val("");
            modal.find("#Balance").val("0.00");
            modal.find("#InNetAssets").iCheck("uncheck");
            modal.find("#Remark").val("");
        } else if (operateType == 2) { //编辑
            var $tr = button.parents("tr");
            modal.find("#Id").val($tr.find("[name=Id]").val());
            modal.find("#Type").val($tr.find("[name=Type]").val());
            modal.find("#Name").val($tr.find("[name=Name]").text());
            modal.find("#Balance").val($tr.find("[name=Balance] a").text());
            var inNetAssets = $tr.find("[name=InNetAssets]").val();
            if (inNetAssets) {
                modal.find("#InNetAssets").iCheck("check");
            } else {
                modal.find("#InNetAssets").iCheck("uncheck");
            }
            modal.find("#Remark").val($tr.find("[name=Remark]").text());
        }
    });
    $('#accountBalanceModal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var modal = $(this);
        var $tr = button.parents("tr");
        modal.find("[name=Id]").val($tr.find("[name=Id]").val());
        modal.find("[name=Name]").html($tr.find("[name=Name]").text());
        modal.find("[name=Balance]").val($tr.find("[name=Balance] a").text());
    });
    $("#frmAccount").validate({
        rules: {
            Name: {
                required: true
            },
            Balance: {
                required: true,
                number: true
            }
        },
        messages: {
            Name: "请输入账户名称",
            Balance: "余额必须为数字"
        },
        submitHandler: function(form) {
            save();
        }
    });


    $("#btnSave").bind("click", function() {
        $("#frmAccount").trigger("submit");
    });
    $("#btnSaveBalance").bind("click", function() {
        adjustBalance();
    });

    $("#btnRefresh").bind("click", function() {
        refresh();
    });

    $(document).delegate(".J_Remove", "click", function () {
        if (confirm("确定要删除吗？")) {
            remove($(this).parents("tr").find("[name=Id]").val());
        }
    });

    $(document).delegate(".J_AccountType", "click", function() {
        var typeId = $(this).data("type");
        refresh(typeId);
    });
});

//复选框
function initCheck() {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
}

function save() {
    var account = {
        Id: $("#Id").val(),
        Type: $("#Type").val(),
        Name: $("#Name").val(),
        Balance: $("#Balance").val(),
        Remark: $("#Remark").val(),
        InNetAssets: $("#InNetAssets").is(":checked"),
        Currency: $("#Currency option:selected").text()
    };

    $.ajax({
        url: "/admin/licai/saveaccount",
        type: "post",
        dataType: "json",
        data: account,
        success: function(resp) {
            //console && console.log(resp);
            if (!resp.iserror) {
                refresh();
                $("#accountModal").modal("hide");
            }
        }
    });
}

function adjustBalance() {
    var modal = $("#accountBalanceModal");
    var adjustParam = {
        Id: modal.find("[name=Id]").val(),
        Balance: modal.find("[name=Balance]").val()
    };
    $.ajax({
        url: "/api/bill/adjustbalance",
        type: "post",
        dataType: "json",
        data: adjustParam,
        success: function (resp) {
            if (!resp.iserror) {
                refresh();
                modal.modal("hide");
            }
        }
    });
}

//删除一个账户
function remove(id) {
    $.ajax({
        url: "/admin/licai/deleteaccount",
        type: "post",
        data: { id: id },
        dataType: "json",
        success: function(resp) {
            if (!resp.iserror) {
                refresh();
                alert("删除成功.");
            }
        }
    });
}

function refresh(type) {
    var params = {};
    if (type != undefined) {
        params.type = type;
    }
    $.ajax({
        url: "/admin/licai/accountdetails",
        type: "get",
        data: params,
        success: function(resp) {
            $("#content").html(resp);
            initCheck();
        }
    });
}