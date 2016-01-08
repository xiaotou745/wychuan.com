$(function () {
    $("#divSummernote").summernote({
        height: 300,                 // set editor height

        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor

        focus: true,                 // set focus to editable area after initializing summernote

        lang: "zh-CN"
    });
    $("#divSummernote").code($("#hidContent").val());
    
    initSortable();
    $(".chosen-select").chosen({ width: "350px" });

    /*保存section基本信息*/
    $("#btnSave").bind("click", saveSection);
    
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
    $("[name=selFirstCategoryQ]").bind("change", function () {
        var $second = $(this).next().next();
        var firstId = $(this).val();
        var seconds = $.grep(window.categories, function (item, index) {
            return item.ParentId == firstId;
        });
        $second.html("");
        $second.append("<option value=''>二级类目</option>");
        $.each(seconds, function (i, category) {
            $second.append("<option value='" + category.Id + "'>" + category.Name + "</option>");
        });
    });
    
    /*插入现有标签按钮事件*/
    $("[name=btnTags]").bind("click", function () {
        insertTagsButtonHandler($(this));
    });
    /*标签对话框弹出事件*/
    $("#modalTags").bind("show.bs.modal", function (event) {
        var $txtTags = $("[name=txtTags]");
        var arrayTags = $txtTags.val().split(",").delete("");
        $("#modalTags").find("button[name=btnTags]").each(function (index, item) {
            var text = $(item).text();
            if (arrayTags.contain(text)) {
                $(item).removeClass("btn-default");
                $(item).addClass("btn-primary");
            } else {
                $(item).removeClass("btn-primary");
                $(item).addClass("btn-default");
            }
        });
    });
    
    /*锚点对话框弹出事件*/
    $("#modalAnchors").bind("show.bs.modal", function (event) {
        var button = $(event.relatedTarget); // Button that triggered the modal
        var modal = $(this);
        var operateType = button.data("type");

        if (operateType == 1) { //新增
            modal.find("[name=id]").val(0);
            modal.find("[name=AnchorId]").val("");
            modal.find("[name=AnchorTitle]").val("");
        } else if (operateType == 2) { //编辑
            var $tr = button.parents("tr");
            modal.find("[name=id]").val($tr.data("id"));
            modal.find("[name=AnchorId]").val($tr.find("td:eq(0)").html());
            modal.find("[name=AnchorTitle]").val($tr.find("td:eq(1)").html());
        }
    });
    /*锚点对话框保存按钮事件*/
    $("#btnSaveAnchor").bind("click", saveAnchor);
    /*删除锚点事件*/
    $(document).delegate(".J_RemoveAnchor", "click", function() {
        var id = $(this).parents("tr").data("id");
        removeAnchor(id);
    });
    
    /*段落查询*/
    $("#btnSearchSections").bind("click", searchSections);
    $("#txtSectionIdQ").bind("keypress", function (e) {
        if (e.keyCode == "13") {
            searchSections();
        }
    });
    //保存子段落
    $("#btnSaveChilds").bind("click", saveChilds);//保存随笔对应的段落
});
/*拖拽事件*/
function initSortable() {
    $(".sortable-list").sortable({
        connectWith: ".connectList",
        stop: function (event, ui) {
        }
    }).disableSelection();
}
/**获取section tags 信息*/
function getTags() {
    var $container = $("#sectionContainer");
    var selectedTags = $container.find("[name=txtTags]").val().split(",").delete("");
    var tags = new Array();
    $.each(selectedTags, function (i, item) {
        var isNewTag = true;
        for (var index = 0; index < window.tags.length; index++) {
            var tag = window.tags[index];
            if (tag.TagName == item) {
                isNewTag = false;
                tags.push({
                    TagId: tag.Id,
                    TagName: item
                });
                break;
            }
        }

        if (isNewTag) {
            tags.push({
                TagId: 0,
                TagName: item
            });
        }
    });
    return tags;
}

/*获取section对象信息*/
function getSection() {
    var $container = $("#sectionContainer");
    var section = {
        Id: $("#hidId").val(),
        SectionId: $container.find("[name=txtSectionId]").val(),
        Title: $container.find("[name=txtTitle]").val(),
        Content: $("#divSummernote").code(),
        ParentId: $("#parentId").val()
    };
    section.CategoryId = $container.find("[name=selSecondCategory]").val();
    section.Tags = getTags();

    return section;
}

/*保存section*/
function saveSection() {
    var section = getSection();
    $.ajax({
        url: "/admin/section/save",
        type: "post",
        data: mvcParamMatch(section, "Tags"),
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                alert("保存成功.");
                $("#hidId").val(resp.data);
                $("#taglist li:eq(1) a").tab("show");
                //                    location.href = "/admin/section/list";
            } else {
                alert("保存失败：" + resp.message);
            }
        }
    });
}

/*插入现有标签按钮事件*/
function insertTagsButtonHandler($button) {
    var $txtTags = $("[name=txtTags]");
    var arrayTags = $txtTags.val().split(",").delete("");
    //arrayTags.delete("");
    var $this = $button;
    var selected = $this.text();
    if ($this.hasClass("btn-default")) { //选中
        arrayTags.push(selected);
        $txtTags.val(arrayTags.join(","));
        $this.removeClass("btn-default");
        $this.addClass("btn-primary");
    } else {
        arrayTags.delete(selected);
        $txtTags.val(arrayTags.join(","));
        $this.removeClass("btn-primary");
        $this.addClass("btn-default");
    }
}


/*锚点对话框 保存锚点 按钮事件*/
function saveAnchor() {
    var $modal = $("#modalAnchors");
    var id = $modal.find("[name=id]").val();
    var anchorId = $modal.find("[name=AnchorId]").val();
    var anchorTitle = $modal.find("[name=AnchorTitle]").val();
    var anchor = {
        Id: id,
        SectionId: $("#hidId").val(),
        AnchorId: anchorId,
        AnchorText: anchorTitle
    };
    $.ajax({
        url: "/admin/section/saveanchor",
        type: "post",
        data: anchor,
        dataType: "json",
        success: function(resp) {
            if (resp.iserror) {
                alert(resp.message);
                return;
            }
            refreshAnchors();
        }
    });
}
/*锚点表格刷新方法*/
function refreshAnchors() {
    var sectionId = $("#hidId").val();
    $.get("/admin/section/refreshanchors", { sectionId: sectionId }, function(resp) {
        $("#sectionAnchorsContainer").html(resp);
        $("#modalAnchors").modal("hide");
    });
}
/*删除锚点*/
function removeAnchor(id) {
    if (!confirm("确定删除吗？不可恢复。")) {
        return;
    }
    $.get("/admin/section/removeanchor", { id: id }, function(resp) {
        refreshAnchors();
    });
}

/*查询段落列表*/
function searchSections() {
    var arrChecked = new Array();
    $("#sectionsCheckedContainer").find("li").each(function (i, item) {
        arrChecked.push($(item).data("id"));
    });
    var tagIds = $("#selTagsQ").val();
    if (tagIds == null) {
        tagIds = new Array();
    }
    var params = {
        StrCheckedSectionIds: arrChecked.join(","),
        SectionId: $("#txtSectionIdQ").val(),
        StrTagIds: tagIds.join(","),
        CategoryId: $("[name=selSecondCategoryQ]").val(),
        FirstCategoryId: $("[name=selFirstCategoryQ]").val()
    };

    $.get("/admin/blog/searchsections", params, function (resp) {
        $("#sectionsUnCheckedContainer").html(resp);
    });
}
function saveChilds() {
    if ($("#hidId").val() == 0) {
        alert("请先保存基础信息.");
        return;
    }
    
    var childs = new Array();

    $("#sectionsCheckedContainer").find("li").each(function (i, item) {
        childs.push({
            Id: $(item).data("id")
        });
    });

    if (childs.length == 0) {
        alert("请选择子随笔段落.");
        return;
    }
    
    var params = {
        Childs: childs,
        Id: $("#hidId").val()
    };
    
    $.ajax({
        url: "/admin/section/savechilds",
        type: "post",
        data: mvcParamMatch(params, "Childs"),
        dataType: "json",
        success: function (response) {
            if (!response.iserror) {
                alert("保存成功");
                //window.open("/blog/item/" + $("[name=txtBlogId]").val());
                location.href = "/admin/section/list";
            } else {
                alert("保存失败:" + response.message);
            }
        }
    });
}
