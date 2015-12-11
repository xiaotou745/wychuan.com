
$(function () {
    $("[name=namespacetype]").bind("click", function () {
        var type = $(this).val();
        var curType = $(this).parents(".form-group").data("type");
        if (curType == type) {
            return;
        }
        $(this).parents(".form-group").data("type", type);
        var $container = $(this).parents(".J_Container");
        if (type == 1) { //1表示选择了统一命名方式
            $container.find(".J_Names").attr("disabled", "disabled");
            $container.find("[name=namespace-ty]").show();
        } else {
            $container.find(".J_Names").removeAttr("disabled");
            $container.find("[name=namespace-ty]").hide();
        }
        refreshDemo($container);
    });
    $("[name=namespace]").bind("keyup", function () {
        var commonNs = $(this).val();
        var $container = $(this).parents(".J_Container");
        var language = $container.data("language");
        if (language == 1) {
            $container.find("[name=namespaceOfDTO]").val(commonNs + ".Service.DTO");
            $container.find("[name=namespaceOfServiceInter]").val(commonNs + ".Service");
            $container.find("[name=namespaceOfService]").val(commonNs + ".Service.Impl");
            $container.find("[name=namespaceOfDaoInter]").val(commonNs + ".Dao.Inter");
            $container.find("[name=namespaceOfDao]").val(commonNs + ".Dao");
        } else {
            $container.find("[name=namespaceOfDTO]").val(commonNs + ".entity.domain");
            $container.find("[name=namespaceOfServiceInter]").val(commonNs + ".service.inter");
            $container.find("[name=namespaceOfService]").val(commonNs + ".service.impl");
            $container.find("[name=namespaceOfDaoInter]").val(commonNs + ".dao.inter");
            $container.find("[name=namespaceOfDao]").val(commonNs + ".dao.impl");
        }
    });
    $(document).find(":text").bind("blur", function () {
        var $container = $(this).parents(".J_Container");
        refreshDemo($container);
    });
    $("[name=btnSave]").bind("click", function () {
        var $container = $(this).parents(".J_Container");
        save($container);
    });
    SyntaxHighlighter.all();
});

function refreshDemo($container) {
    var namespaceType = 1;
    $container.find("[name=namespacetype]").each(function (index, item) {
        if ($(item).is(":checked")) {
            namespaceType = $(item).val();
        }
    });

    var author = $container.find("[name=author]").val();
    var model = $container.find("[name=tableName]").html();
    var language = $container.data("language");
    var name = getName($container, namespaceType, model, language, author);

    var serviceInterCode = window.templates[name.language + "-2"].format(name.ServiceInterNs, name.model, name.author, name.modelfield, name.DtoNs);
    //console.log(serviceInterCode);
    $container.find("[name=serviceInterDemo]").html(serviceInterCode);
    $container.find("[name=serviceDemo]").html("serviceDemo");
    $container.find("[name=dtoDemo]").html(window.templates[name.language + "-1"].format(name.DtoNs, name.model, name.author));
    $container.find("[name=daoInterDemo]").html("daoInterDemo");
    $container.find("[name=daoDemo]").html("daoDemo");
    SyntaxHighlighter.highlight();
}

function getName($container, nstype, model, language, author) {
    var name = {
        author: author,
        language: language,
        model: model,
        nstype: nstype,
        modelfield: model.substring(0, 1).toLowerCase() + model.substring(1)
    };
    if (nstype == 1) { //公用命名空间
        var baseNamespace = $container.find("[name=namespace]").val();

        var strfixOfDto = language == 1 ? ".Service.DTO" : ".entity.domain";
        name.DtoNs = baseNamespace + strfixOfDto;

        var strfixOfServiceInter = language == 1 ? ".Service" : ".service.inter";
        name.ServiceInterNs = baseNamespace + strfixOfServiceInter;

        var strfixOfService = language == 1 ? ".Service.Impl" : ".service.impl";
        name.ServiceNs = baseNamespace + strfixOfService;

        var strfixOfDaoInter = language == 1 ? ".Dao.Inter" : ".dao.inter";
        name.DaoInterNs = baseNamespace + strfixOfDaoInter;

        var strfixOfDao = language == 1 ? ".Dao" : ".dao.impl";
        name.DaoNs = baseNamespace + strfixOfDao;
    } else { //私用命名空间
        name.DtoNs = $container.find("[name=namespaceOfDTO]").val();
        name.ServiceInterNs = $container.find("[name=namespaceOfServiceInter]").val();
        name.ServiceNs = $container.find("[name=namespaceOfService]").val();
        name.DaoInterNs = $container.find("[name=namespaceOfDaoInter]").val();
        name.DaoNs = $container.find("[name=namespaceOfDao]").val();
    }

    return name;
}

function save($container) {
    var data2 =
    {
        Language: $container.data("language"),
        Author: $container.find("[name=author]").val(),
        CommonNamespace: $container.find("[name=namespace]").val(),
        DtoOrDomainNamespace: $container.find("[name=namespaceOfDTO]").val(),
        DaoInterNamespace: $container.find("[name=namespaceOfDaoInter]").val(),
        DaoNamespace: $container.find("[name=namespaceOfDao]").val(),
        ServiceInterNamespace: $container.find("[name=namespaceOfServiceInter]").val(),
        ServiceNamespace: $container.find("[name=namespaceOfService]").val()
    };

    $.ajax({
        url: "/admin/dbtools/savesetting",
        type: "post",
        data: data2,
        dataType: "json",
        success: function (resp) {
            if (!resp.iserror) {
                alert("保存成功.");
            }
        }
    });
}