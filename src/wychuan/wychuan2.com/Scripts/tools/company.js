$(document).ready(function () {
    $('.contact-box').each(function () {
        animationHover(this, 'pulse');
    });

    $("#CreateDate").datetimepicker({
        lang: "ch",
        mask: true
    });
    
    $("#frmTimeline").validate({
        rules: {
            Name: {
                required: true
            }
        },
        messages: {
            Name: "请输入公司名称"
        },
        submitHandler: function (form) {
            save();
        }
    });

    $("#btnSave").bind("click", function () {
        $("#frmTimeline").trigger("submit");
    });
});

function save() {
    var id = $("#Id").val();
    var name = $("#Name").val();
    var creator = $("#Creator").val();
    var createDate = $("#CreateDate").val();
    var introdution = $("#Introdution").val();
    
    var company = {
        Id: id,
        Name: name,
        Creator: creator,
        CreateTime: createDate,
        Introdution: introdution
    };

    $.ajax({
        url: "/api/company/save",
        type: "post",
        dataType: "json",
        data: company,
        success: function (resp) {
            console && console.log(resp);
            if (!resp.iserror) {
                refresh();
                $("#timelineModal").modal("hide");
            }
        }
    });
}

function refresh() {
    $.ajax({
        url: "/admin/tools/companylist",
        type: "get",
        success: function(resp) {
            $("#companys").html(resp);
        }
    });
}