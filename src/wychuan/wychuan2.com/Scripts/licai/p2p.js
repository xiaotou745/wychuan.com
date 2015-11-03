$(function () {
    //$(".datetimepicker").datetimepicker({
    //    lang: "ch",
    //    format: "Y-m-d",
    //    formatDate: "Y-m-d",
    //    timepicker: false
    //});
    $('.datetimepicker').datepicker({
        format: "yyyy-mm-dd"
    });
    $("#btnSave").bind("click", function () {
        var detail = getModel();

        $.ajax({
            url: "/admin/licai/createp2p",
            type: "post",
            data: detail,
            dataType: "text",
            success: function (resp) {
                if (resp != "error") {
                    $("#tblContainer").html(resp);
                    $("#modalP2pProject").modal("hide");
                }
            }
        });
    });

    $(document).delegate(".J_Edit", "click", function () {
        var id = $(this).parents("tr").find("[name=Id]").val();
        $.get("/admin/licai/getlicaibyid", { id: id }, function (resp) {
            $("#modalP2pProject").find(".modal-body").html(resp);
            $("#modalP2pProject").find('.datetimepicker').datepicker({
                format: "yyyy-mm-dd"
            });
            $("#modalP2pProject").modal("show");
        });
    });

    $(document).delegate(".J_Delete", "click", function () {
        var id = $(this).parents("tr").find("[name=Id]").val();
        if (confirm("确定要删除吗？")) {
            $.get("/admin/licai/deletep2p", { id: id }, function (resp) {
                $("#tblContainer").html(resp);
            });
        }
    });
});

function getModel() {

    var form = $("#modalP2pProject").find("form");
    var detail = {
        Id: form.find("[name=Id]").val(),
        AccountId: form.find("[name=selAccount]").val(),
        Project: form.find("[name=txtProject]").val(),
        BuyDay: form.find("[name=txtBuyDay]").val(),
        Times: form.find("[name=txtTimes]").val(),
        TimeUnit: form.find("[name=selTimeUnit]").val(),
        Price: form.find("[name=txtPrice]").val(),
        InterestRate: form.find("[name=txtInterestRate]").val(),
        ExpireDay: form.find("[name=txtExpireDay]").val()
    };
    return detail;
}