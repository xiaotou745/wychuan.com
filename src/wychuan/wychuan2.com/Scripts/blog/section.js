$(function () {
    $("#divSummernote").summernote({
        height: 300,                 // set editor height

        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor

        focus: true,                 // set focus to editable area after initializing summernote

        lang: "zh-CN"
    });
    $("#divSummernote").code($("#hidContent").val());

    $("#btnSave").bind("click", function () {
        var section = getSection();
        $.ajax({
            url: "/admin/section/save",
            type: "post",
            data: mvcParamMatch(section, "Tags"),
            dataType: "json",
            success: function (resp) {
                if (!resp.iserror) {
                    alert("保存成功.");
                    location.href = "/admin/section/list";
                }
            }
        });
    });
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

    $("[name=btnTags]").bind("click", function () {
        var $txtTags = $("[name=txtTags]");
        var arrayTags = $txtTags.val().split(",").delete("");
        //arrayTags.delete("");
        var $this = $(this);
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
    });

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
});

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

function getSection() {
    var $container = $("#sectionContainer");
    var section = {
        Id: $("#hidId").val(),
        SectionId: $container.find("[name=txtSectionId]").val(),
        Title: $container.find("[name=txtTitle]").val(),
        Content: $("#divSummernote").code(),
        ParentId: $("#parentId").val()
    };
    var tags = new Array();
    if (window.isParent) {
        tags = getTags();
        section.CategoryId = $container.find("[name=selSecondCategory]").val();
    } else {
        section.CategoryId = $("#hidCategoryId").val();
    }
    section.Tags = tags;

    return section;
}