$(function () {
    pager.pageChanged = search;
    page(pager);
    refreshPager(1, pager.totalRecord);
    $(".chosen-select").chosen({ width: "350px" });

    $("#content").delegate(".J_Toggle", "click", function () {
        var $tr = $(this).parents("tr");
        var id = $tr.data("id");
        var currentShow = $(this).data("show");
        if (currentShow) { //如果当前是显示状态，则隐藏子段落
            $("[parentid=" + id + "]").hide();
            $(this).data("show", false);
            $(this).find("i").removeClass("fa-minus").addClass("fa-plus");
        } else {
            var hasload = $tr.data("childs");
            if (!hasload) {
                var queryInfo = {
                    ParentId: id,
                    PageIndex: 1,
                    PageSize: 200
                };
                $.ajax({
                    url: "/admin/section/refresh",
                    type: "post",
                    data: queryInfo,
                    success: function (resp) {
                        $tr.data("childs", true);
                        $tr.after(resp);
                    }
                });
            }
            $("[parentid=" + id + "]").show();
            $(this).data("show", true);
            $(this).find("i").removeClass("fa-plus").addClass("fa-minus");
        }
    });

    $("#content").delegate(".J_Remove", "click", function () {
        if (!confirm("确定要删除此段落吗？不可恢复。")) {
            return;
        }
        var id = $(this).parents("tr").data("id");
        $.get("/admin/section/remove", { id: id }, function (resp) {
            if (resp.iserror) {
                alert(resp.message);
                return;
            }
            search(1);
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

    $("#btnSearch").bind("click", function () {
        search(1);
    });
});

function search(newPage) {
    refresh(getQueryInfo(newPage));
}

function getQueryInfo(newPage) {
    var firstCategoryId = $("[name=selFirstCategoryQ]").val();
    var secondCategoryId = $("[name=selSecondCategoryQ]").val();
    var tagIds = $("#selTagsQ").val();
    if (tagIds == null) {
        tagIds = new Array();
    }
    var queryInfo = {
        FirstCategoryId: firstCategoryId,
        CategoryId: secondCategoryId,
        TagIds: tagIds,
        ParentId: 0,
        PageIndex: newPage,
    };
    return queryInfo;
}
function refresh(queryInfo) {
    $.ajax({
        url: "/admin/section/refresh",
        type: "post",
        data: mvcParamMatch(queryInfo, "TagIds"),
        success: function (resp) {
            $("#content").html(resp);
            var totalrecord = $("#content").find("table").data("totalrecord");
            refreshPager(queryInfo.PageIndex, totalrecord);
        }
    });
}