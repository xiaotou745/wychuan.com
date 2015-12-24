$(function () {
    pager.pageChanged = search;
    page(pager);
    refreshPager(1, pager.totalRecord);
    $(".chosen-select").chosen({ width: "350px" });

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

    $("#content").delegate(".J_Remove", "click", function () {
        if (confirm("确定要删除此随笔吗？")) {
            var $tr = $(this).parents("tr");
            var id = $tr.data("id");

            $.get("/admin/blog/remove", { id: id }, function (resp) {
                if (!resp.iserror) {
                    search(1);
                }
            }, "json");
        }
    });

    $("#btnSearch").bind("click", function () {
        search(1);
    });
});

function search(newPage) {
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
        PageIndex: newPage,
    };
    $.post("/admin/blog/search", mvcParamMatch(queryInfo, "TagIds"), function (resp) {
        $("#content").html(resp);
        var totalrecord = $("#content").find("table").data("totalrecord");
        refreshPager(newPage, totalrecord);
    });
}
