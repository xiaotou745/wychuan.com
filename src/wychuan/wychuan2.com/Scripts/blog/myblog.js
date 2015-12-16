$(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });
    initSortable();
    $(".chosen-select").chosen({ width: "350px" });
    //查询段落列表
    $("#btnSearchSections").bind("click", searchSections);
    $("#btnSave").bind("click", saveBlog);//保存随笔
    $("#btnSaveBlogSections").bind("click", saveBlogSections);//保存随笔对应的段落
   
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
        if ($this.hasClass("btn-default")) {
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
    var uploader = WebUploader.create({
        // 选完文件后，是否自动上传。
        auto: true,
        // swf文件路径
        swf: '/Content/plugins/webuploader/Uploader.swf',
        // 文件接收服务端。
        server: '/admin/blog/UploadFile',
        // 选择文件的按钮。可选。
        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
        pick: '#filePicker',
        // 只允许选择图片文件。
        accept: {
            title: 'Images',
            extensions: 'gif,jpg,jpeg,bmp,png',
            mimeTypes: 'image/*'
        }
    });
    // 当有文件添加进来的时候
    uploader.on('fileQueued', function (file) {
        console.log("fileQueued");
        var $li = $(
            '<div id="' + file.id + '" class="file-item thumbnail">' +
                '<img>' +
                '<div class="info">' + file.name + '</div>' +
                '<div class="error"></div>' +
                '</div>'
        ),
            $img = $li.find('img');


        // $list为容器jQuery实例
        $("#fileList").append($li);

        // 创建缩略图
        // 如果为非图片文件，可以不用调用此方法。
        // thumbnailWidth x thumbnailHeight 为 100 x 100
        uploader.makeThumb(file, function (error, src) {
            if (error) {
                $img.replaceWith('<span>不能预览</span>');
                return;
            }

            $img.attr('src', src);
        }, 200, 200);
    });
    uploader.on('uploadSuccess', function (file, resp) {
        $('#fileList').attr("filepath", resp.message);
        $('#' + file.id).find('.error').text('已上传');
    });

    uploader.on('uploadError', function (file, reason) {
        $('#' + file.id).find('.error').text('上传出错');
    });

    uploader.on('uploadComplete', function (file) {
        $('#' + file.id).find('.progress').fadeOut();
    });

    $("#btnPreview").bind("click", function () {
        var blog = getBlog();
        $.ajax({
            url: "/admin/blog/preview",
            type: "post",
            data: blog,
            dataType: "json",
            success: function (resp) {
                if (!resp.iserror) {
                    window.location.href = "/blog/preview";
                }
            }
        });
    });
});

function initSortable() {
    $(".sortable-list").sortable({
        connectWith: ".connectList",
        stop: function (event, ui) {
        }
    }).disableSelection();
}
//查询段落列表
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
        StrTagIds: tagIds.join(",")
    };

    $.get("/admin/blog/searchsections", params, function (resp) {
        $("#sectionsUnCheckedContainer").html(resp);
    });
}
function getTags() {
    var $container = $("#blogContainer");
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
function getBlog() {
    var $container = $("#blogContainer");
    var blog = {
        Id: $("#hidId").val(),
        BlogId: $container.find("[name=txtBlogId]").val(),
        Title: $container.find("[name=txtTitle]").val(),
        Summary: $container.find("[name=txtSummary]").val(),
        ConverImgUrl: $('#fileList').attr("filepath"),
        CategoryId: $container.find("[name=selSecondCategory]").val(),
        IsPublic: $container.find("[name=chkIsPublic]").is(":checked"),
        Tags: getTags()
    };
    return blog;
}

function saveBlog() {
    var blog = getBlog();
    console.log(blog);
    $.ajax({
        url: "/admin/blog/save",
        type: "post",
        data: mvcParamMatch(blog,"Tags"),
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                $("#hidId").val(resp.data);
                alert("success");
                $("#taglist a:eq(1)").tab("show");
            }
        }
    });
}

function saveBlogSections() {
    if ($("#hidId").val() == 0) {
        alert("请先保存基础信息.");
        return;
    }
    var sectionChecked = new Array();
    var sectionIdsChecked = new Array();
    
    $("#sectionsCheckedContainer").find("li").each(function (i, item) {
        sectionChecked.push({
            Id: $(item).data("id"),
            SectionId: $(item).data("sectionid")
        });
        sectionIdsChecked.push($(item).data("sectionid"));
    });
    
    if (sectionChecked.length == 0) {
        alert("请选择随笔段落.");
        return;
    }

    var params = {
        BlogSections: sectionChecked,
        SectionIds:sectionIdsChecked.join(","),
        Id: $("#hidId").val()
    };
    preview(function(resp) {
        params.Htmls = resp;
        $.ajax({
            url: "/admin/blog/savesections",
            type: "post",
            data: mvcParamMatch(params, "BlogSections"),
            dataType: "json",
            success: function(response) {
                console.log(response);
            }
        });
    });
}

function preview(callback) {
    var id = $("#hidId").val();
    if (id == 0) {
        return;
    }
    var arrChecked = new Array();
    $("#sectionsCheckedContainer").find("li").each(function (i, item) {
        arrChecked.push($(item).data("sectionid"));
    });
    if (arrChecked.length == 0) {
        return;
    }
    var strSectionIds = arrChecked.join(",");

    $.get("/admin/blog/GetBlogPre", { id: id, sectionIds: strSectionIds }, function(resp) {
        if (callback) {
            callback(resp);
        }
    });
}