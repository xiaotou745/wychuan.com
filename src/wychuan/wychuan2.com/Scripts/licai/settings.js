$(function () {
    $("#treeItems").on("changed.jstree", function(e, data) {
    }).on("rename_node.jstree", saveItem)
    .on("delete_node.jstree", deleteItem)
        .jstree({
        "core": {
            "check_callback": true
        },
        "plugins": ["contextmenu"],
        "contextmenu": {
            select_node: true,
            items: createItemsContextMenu
        }
    });


    $("#treeCategories").on("changed.jstree", function(e, data) {
    }).on("create_node.jstree", function(a, b, c) {
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
});

/* 保存分类 */
function saveItem(e, data) {
    var item = {
        Id: data.node.li_attr["cid"],
        Name: data.text,
        Type: data.node.li_attr["type"]
    };

    $.ajax({
        url: "/api/items/saveitems",
        type: "post",
        data: item,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#treeItems").find("li[id=" + data.node.id + "]").attr("cid", resp.data);
            }
        }
    });
}
/* 删除分类 */
function deleteItem(e, data) {
    var id = data.node.li_attr["cid"];
    $.ajax({
        url: "/api/items/removeitem",
        type: "get",
        data: { id: id },
        dataType: "json",
        success: function (resp) {
        }
    });
}
function createItemsContextMenu(node) {
    var items = {};
    var cid = node.li_attr.cid;

    var createItem = {
        "createItem": createContextMenuItem("新增", createItemHandler, "fa fa-plus", false, false, false)
    };
    var updateItem = {
        "updateItem": createContextMenuItem("重命名", renameItemHandler, "fa fa-fa-edit", false, false, false)
    };
    var deleteItem = {
        "deleteItem": createContextMenuItem("删除", deleteItemHandler, "fa fa-edit", true, false, false)
    };

    if (cid == 0) {
        items = $.extend({}, createItem);
    } else {
        items = $.extend({}, updateItem, deleteItem);
    }
    return items;
}
/* 新增菜单 */
function createItemHandler() {
    var instance = $("#treeItems").jstree(true);
    var selected = instance.get_selected();
    if (!selected.length) {
        return false;
    }
    selected = selected[0];
    var node = {
        li_attr: {
            cid: -1,
            "type": instance.get_node(selected).li_attr["type"]
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
function renameItemHandler() {
    var ref = $('#treeItems').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) { return false; }
    sel = sel[0];
    ref.edit(sel);
    return true;
}
/* 删除 */
function deleteItemHandler() {
    var ref = $('#treeItems').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) { return false; }
    ref.delete_node(sel);
    return true;
}
/**
 * 分类
 */
/* 保存分类 */
function saveCategory(e, data) {
    var category = {
        Id: data.node.li_attr["cid"],
        ParentId: data.node.li_attr["parentid"],
        Name: data.text,
        InOutType: data.node.li_attr["type"]
    };

    $.ajax({
        url: "/api/items/savecategory",
        type: "post",
        data: category,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#treeCategories").find("li[id=" + data.node.id + "]").attr("cid", resp.data);
            }
        }
    });
}
/* 删除分类 */
function deleteCategory(e, data) {
    var id = data.node.li_attr["cid"];
    $.ajax({
        url: "/api/items/removecategory",
        type: "get",
        data: {id:id},
        dataType: "json",
        success: function(resp) {
        }
    });
}
/*创建右键菜单*/
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
    
    if (parentid == 0) {
        items = $.extend({}, createItem, updateItem, deleteItem);
    } else {
        items = $.extend({}, updateItem, deleteItem);
    }
    return items;
}
/* 新增菜单 */
function createCategoryHandler() {
    var instance = $("#treeCategories").jstree(true);
    var selected = instance.get_selected();
    if (!selected.length) {
        return false;
    }
    selected = selected[0];
    var node = {
        li_attr: {
            cid: 0,
            "parentid": instance.get_node(selected).li_attr["cid"],
            "type": instance.get_node(selected).li_attr["type"]
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
    var ref = $('#treeCategories').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) { return false; }
    sel = sel[0];
    ref.edit(sel);
    return true;
}
/* 删除 */
function deleteCategoryHandler() {
    var ref = $('#treeCategories').jstree(true),
        sel = ref.get_selected();
    if (!sel.length) { return false; }
    ref.delete_node(sel);
    return true;
}

function createContextMenuItem(label, action, icon, separator_before, separator_after, shortcut, shortcut_label) {
    //separator_before - a boolean indicating if there should be a separator before this item
    //separator_after - a boolean indicating if there should be a separator after this item
    //_disabled - a boolean indicating if this action should be disabled
    //label - a string - the name of the action (could be a function returning a string)
    //    action - a function to be executed if this item is chosen
    //    icon - a string, can be a path to an icon or a className, if using an image that is in the current directory use a ./ prefix, otherwise it will be detected as a class
    //    shortcut - keyCode which will trigger the action if the menu is open (for example 113 for rename, which equals F2)
    //        shortcut_label - shortcut label (like for example F2 for rename)
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