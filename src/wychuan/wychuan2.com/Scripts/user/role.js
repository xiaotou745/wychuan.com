$(function () {
    initJsTree();

    $("#menusContainer").on("changed.jstree", function (e, data) {
    }).jstree({
        "core": {
            "check_callback": true
        },
        "plugins": ["checkbox"]
    });

    $("#btnSaveRole").bind("click", saveRole);

    $("#btnSaveRolePrivilege").bind("click", saveRolePrivilege);

    $("#rolesContainer").jstree().trigger("select_node.jstree");
});

function saveRole() {
    var role = {
        Name: $("#modalRoles").find("[name=txtName]").val()
    };

    $.ajax({
        url: "/api/account/saverole",
        type: "post",
        data: role,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                refreshRoles(resp.data);
                $("#modalRoles").modal("hide");
            }
        }
    });
}

function saveRolePrivilege() {
    var selectedNode = $("#rolesContainer").jstree().get_node($("#rolesContainer").jstree().get_selected());
    var roleId = selectedNode.data.id;

    var checked = $("#menusContainer").jstree().get_checked();
    var params = {
        RoleId: roleId,
        MenuIds: checked
    };
    $.ajax({
        url: "/api/menu/SavePrivilege",
        type: "post",
        data: params,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                alert("保存成功.");
            }
        }
    });
}

function initJsTree() {
    $("#rolesContainer").on("select_node.jstree", function (e, data) {
        var selected = $("#rolesContainer").jstree().get_selected();
        var selectedNode = $("#rolesContainer").jstree().get_node(selected);
        if (selectedNode == false) {
            $("#menusContainer").jstree().uncheck_all();
            $("#lblRoleName").text("未选择");
            return;
        }
        var roleId = selectedNode.data.id;
        $.ajax({
            url: "/api/menu/GetPrivilege",
            type: "get",
            data: { roleId: roleId },
            dataType: "json",
            success: function (resp) {
                if (!resp.iserror) {
                    $("#menusContainer").jstree().uncheck_all();
                    $("#menusContainer").jstree().check_node(resp.data);
                    $("#lblRoleName").text(selectedNode.text);
                }
            }
        });
    }).on("delete_node.jstree", deleteRoleItem).jstree({
        "core": {
            "check_callback": true
        },
        "plugins": ["contextmenu"],
        "contextmenu": {
            select_node: true,
            items: {
                "deleteItem": {
                    label: "删除",
                    action: function () {
                        var ref = $('#rolesContainer').jstree(true),
                            sel = ref.get_selected();
                        if (!sel.length) { return false; }
                        ref.delete_node(sel);
                        return true;
                    },
                    icon: "fa fa-edit",
                    _disabled: false
                }
            }
        }
    });
}
function deleteRoleItem(e, data) {
    var id = data.node.data.id;
    $.ajax({
        url: "/api/account/removerole",
        type: "get",
        data: { roleId: id },
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#rolesContainer").jstree().trigger("select_node.jstree");
                //$("#menusContainer").jstree().uncheck_all();
            }
        }
    });
}
function refreshRoles(curId) {
    $.ajax({
        url: "/admin/user/refreshrole",
        data: { roleId: curId },
        type: "get",
        success: function (resp) {
            $("#rolesContainer").jstree().destroy();
            $("#rolesContainer").html(resp);
            initJsTree();
            $("#rolesContainer").jstree().trigger("select_node.jstree");
        }
    });
}