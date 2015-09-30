$(function () {
    //服务器列表选择更新
    $("#dbServer").on("change", function () {
        refresh(1, $(this).val(), "", "");
    });

    $("#txtDbName").on("keydown", function (evt) {
        evt = (evt) ? evt : ((window.event) ? window.event : "");
        var keyCode = evt.keyCode ? evt.keyCode : (evt.which ? evt.which : evt.charCode);
        if (keyCode != 13) {
            return;
        }
        var dbName = $("#txtDbName").val();
        if (dbName == "") {
            $("#txtDbName").focus();
            return;
        }
        var dbServer = $("#currentDbServer").val();
        $("#txtTableName").val("");
        refresh(2, dbServer, dbName, "");
    });
    $(document).delegate("td[name='dbname'] a", "click", function () {
        var dbServer = $("#currentDbServer").val();
        var dbname = $(this).text();
        $("#txtDbName").val(dbname);
        refresh(2, dbServer, dbname, "");
    });

    $(document).delegate("td.J_tableName a", "click", function () {
        var dbServer = $("#currentDbServer").val();
        var dbName = $("#currentDbName").val();
        var tableName = $(this).text();
        $("#txtTableName").val(tableName);
        refresh(3, dbServer, dbName, tableName);
    });
    $("#txtTableName").on("keydown", function (evt) {
        evt = (evt) ? evt : ((window.event) ? window.event : "");
        var keyCode = evt.keyCode ? evt.keyCode : (evt.which ? evt.which : evt.charCode);
        if (keyCode != 13) {
            return;
        }
        var dbTableName = $("#txtTableName").val();
        if (dbTableName == "") {
            $("#txtTableName").focus();
            return;
        }
        var dbServer = $("#currentDbServer").val();
        var dbName = $("#currentDbName").val();
        refresh(3, dbServer, dbName, dbTableName);
    });

    //返回上一步
    $("#returnTableList").bind("click", function () {
        var viewType = $("#currentViewType").val();
        var dbServer = $("#currentDbServer").val();
        if (viewType == 2) {
            refresh(1, dbServer, "", "");
            $("#txtDbName").val("");
        } else if (viewType == 3) {
            refresh(2, dbServer, $("#currentDbName").val(), "");
            $("#txtTableName").val("");
        }
    });

    //对话框弹框事件
    $('#modalDescEdit').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var viewType = $("#currentViewType").val();
        if (viewType == 2) {
            var tableName = button.parent().data("table");
            var desc = button.parent().data("desc");
            $("#lblTableName").val(tableName);
            $("#lblColumnName").val("");
            $("#txtDesc").val(desc);
            $("#txtDesc").focus();
        } else if (viewType == 3) {
            var tableName2 = $("#currentTableName").val();
            var columnName = button.parent().data("col");
            var desc2 = button.parent().data("desc");

            $("#lblTableName").val(tableName2);
            $("#lblColumnName").val(columnName);
            $("#txtDesc").val(desc2);
            $("#txtDesc").focus();
        }
    });

    //保存描述信息
    $("#btnSaveDesc").bind("click", function () {
        var dbServer = $("#currentDbServer").val();
        var dbName = $("#currentDbName").val();
        var tableName = $("#lblTableName").val();
        var columnName = $("#lblColumnName").val();
        var desc = $("#txtDesc").val();

        $.post(
            "/admin/dbtools/UpdateDesc",
            { dbServer: dbServer, dbName: dbName, dbTable: tableName, columnName: columnName, desc: desc },
            function (data) {
                if (data) {
                    refresh($("#currentViewType").val(), dbServer, dbName, tableName);
                    $("#modalDescEdit").modal("hide");
                }
            }
        );
    });
});

//刷新
function refresh(viewType, dbServer, dbName, dbTable) {
    $.get("/admin/dbtools/refresh",
        { viewType: viewType, dbServer: dbServer, dbName: dbName, dbTable: dbTable },
        function (resp) {
            $("#contents").html(resp);
        });
}