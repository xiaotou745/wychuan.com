$(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
    $("#frmTask").validate({
        rules: {
            Content: {
                required: true
            }
        },
        messages: {
            Content: "请输入任务内容"
        },
        submitHandler: function (form) {
            save();
        }
    });

    $('#modalTask').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var modal = $(this);

        var recipient = button.data('whatever');
        modal.find('.modal-title').text(recipient);

        var operateType = button.data("type");


        if (operateType == 1) { //新增
            modal.find("#Id").val(0);
            modal.find("#Title").val($("#txtMyJobTitle").val());
            modal.find("#Content").val("");
        } else if (operateType == 2) { //编辑
            var $li = button.parents("li");
            var id = $li.data("id");
            var priority = $li.data("priority");
            var title = $li.find("h3").text();
            var context = button.parent().prev().text();
            modal.find("#Id").val(id);
            modal.find("#Title").val(title);
            modal.find("#Content").val(context);
            modal.find("#PriorityLevel").val(priority);
        }
    });
    
    $("#btnSave").bind("click", function () {
        $("#frmTask").trigger("submit");
    });

    initSortable();

    $("#jobs").delegate(".J_Hide", "click", function () {
        var id = $(this).parents("li").data("id");
        if (confirm("确定要隐藏此任务吗？")) {
            Hide(id);
        }
    });

    $("#jobs").delegate(".J_Remove", "click", function() {
        var id = $(this).parents("li").data("id");
        if (confirm("确定要删除此任务吗？")) {
            Remove(id);
        }
    });
});

function initSortable() {
    $(".sortable-list").sortable({
        connectWith: ".connectList",
        stop: function (event, ui) {
            changeStatus(event, ui);
        }
    }).disableSelection();
}

function changeStatus(event, ui) {
    var targetStatus = $(ui.item[0]).parent().data("status");
    var currentStatus = $(ui.item[0]).data("status");
    if (currentStatus == targetStatus) {
        return;
    }
    var id = $(ui.item[0]).data("id");
    var data = {
        id: id,
        status: targetStatus
    };
    $.ajax({
        url: "/api/tasks/modifystatus",
        type: "get",
        dataType: "json",
        data: data,
        success: function (resp) {
            refresh();
        }
    });
}

function save() {
    var id = $("#Id").val();
    var title = $("#Title").val();
    var content = $("#Content").val();
    var priority = $("#PriorityLevel").val();

    var task = {
        Id: id,
        Title: title,
        Content: content,
        PriorityLevel: priority
    };
    $.ajax({
        url: "/api/tasks/save",
        type: "post",
        dataType: "json",
        data: task,
        success: function (resp) {
            if (!resp.iserror) {
                refresh();
                $("#modalTask").modal("hide");
            }
        }
    });
}

function refresh() {
    $.ajax({
        url: "/admin/tools/joblist",
        type: "get",
        success: function(resp) {
            $("#jobs").html(resp);
            initSortable();
        }
    });
}

function Hide(id) {
    $.ajax({
        url: "/api/tasks/hide",
        type: "get",
        dataType: "json",
        data: { id: id },
        success: function (resp) {
            console && console.log(resp);
            if (!resp.iserror) {
                refresh();
            }
        }
    });
}

function Remove(id) {
    $.ajax({
        url: "/api/tasks/remove",
        type: "get",
        dataType: "json",
        data: { id: id },
        success: function(resp) {
            if (!resp.iserror) {
                refresh();
            }
        }
    });
}