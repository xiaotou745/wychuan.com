$(function () {
    $('.input-daterange').datepicker({
        format: "yyyy-mm-dd",
        language: "zh-CN"
    });

    $("#btnQuery").bind("click", refresh);

    $("#btnSave").bind("click", save);

    $("#mynotesContainer").delegate(".J_DeleteNote", "click", function () {
        var id = $(this).siblings().first().val();
        remove(id);
    });
});

function save() {
    var title = $("#Title").val();
    var content = $("#Content").val();

    var mynote = {
        Title: title,
        Content: content
    };

    $.ajax({
        url: "/admin/tools/savenotes",
        type: "post",
        data: mynote,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                refresh();
                $("#mynotesModal").modal("hide");
            }
        }
    });
}

function remove(id) {
    $.ajax({
        url: "/admin/tools/removenotes",
        type: "post",
        data: { id: id },
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                refresh();
            }
        }
    });
}

function refresh() {
    var queryInfo = {
        StartTime: $("#starttime").val(),
        OverTime: $("#overtime").val()
    };
    $.ajax({
        url: "/admin/tools/refreshnotes",
        type: "get",
        data: queryInfo,
        success: function (resp) {
            console.log(resp);
            $("#mynotesContainer").html(resp);
        }
    });
}