$(function () {
    $("#treeItems").on("changed.jstree", function (e, data) {
    }).on("create_node.jstree", function (a, b, c) {
    }).on("rename_node.jstree", saveCategory)
        .on("delete_node.jstree", deleteCategory)
        .jstree({
            "core": {
                "check_callback": true
            },
            "plugins": ["contextmenu"],
            "contextmenu": {
                select_node: true,
                items: createCategoriesContextMenu
            }
        });

    $("#btnSaveTags").bind("click", function () {
        var tag = {
            TagName: $("#txtTagsName").val()
        };

        $.ajax({
            url: "/api/blog/savetags",
            type: "post",
            data: tag,
            dataType: "json",
            success: function (resp) {
                if (!resp.iserror) {
                    $("#tagContainer").append("<button type=\"button\" class=\"btn btn-outline btn-default\">" + tag.TagName + "</button>");
                    $("#modalTags").modal("hide");
                }
            }
        });
    });
});

function createCategoriesContextMenu(node) {
    var items = {};
    var parentid = node.li_attr.parentid;

    var createItem = {
        "createItem": createContextMenuItem("新增", createCategoryHandler, "fa fa-plus", false, false, false)
    };
    var updateItem = {
        "updateItem": createContextMenuItem("重命名", renameCategoryHandler, "fa fa-fa-edit", false, false, false)
    };
    var deleteItem = {
        "deleteItem": createContextMenuItem("删除", deleteCategoryHandler, "fa fa-edit", true, false, false)
    };
    if (parentid == -1) {
        items = $.extend({}, createItem);
    } else if (parentid == 0) {
        items = $.extend({}, createItem, updateItem, deleteItem);
    } else {
        items = $.extend({}, updateItem, deleteItem);
    }
    return items;
}

/* 新增菜单 */

function createCategoryHandler() {
    var instance = $("#treeItems").jstree(true);
    var selected = instance.get_selected();
    if (!selected.length) {
        return false;
    }
    selected = selected[0];
    var node = {
        li_attr: {
            cid: 0,
            "parentid": instance.get_node(selected).li_attr["cid"]
        },
        "type": "file"
    };
    selected = instance.create_node(selected, node);
    if (selected) {
        instance.edit(selected);
    }
    return true;
}

/* 重命名 */

function renameCategoryHandler() {
    var ref = $('#treeItems').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) {
        return false;
    }
    sel = sel[0];
    ref.edit(sel);
    return true;
}

/* 删除 */

function deleteCategoryHandler() {
    var ref = $('#treeItems').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) {
        return false;
    }
    ref.delete_node(sel);
    return true;
}

function createContextMenuItem(label, action, icon, separator_before, separator_after, shortcut, shortcut_label) {
    return {
        label: label,
        action: action,
        icon: icon,
        _disabled: false,
        separator_before: separator_before,
        separator_after: separator_after,
        shortcut: shortcut,
        shortcut_label: shortcut_label
    };
}

/**
* 分类
*/
/* 保存分类 */

function saveCategory(e, data) {
    var category = {
        Id: data.node.li_attr["cid"],
        ParentId: data.node.li_attr["parentid"],
        Name: data.text
    };

    $.ajax({
        url: "/api/blog/savecategory",
        type: "post",
        data: category,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#treeItems").find("li[id=" + data.node.id + "]").attr("cid", resp.data);
            }
        }
    });
}

/* 删除分类 */

function deleteCategory(e, data) {
    var id = data.node.li_attr["cid"];
    $.ajax({
        url: "/api/blog/removecategory",
        type: "get",
        data: { id: id },
        dataType: "json",
        success: function (resp) {
        }
    });
}