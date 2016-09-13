function modal(type, body, option) {
    //var data = {};

    var data = $.extend({
        _BODY: "",
        Label_OK: "確認",
        Label_Cancel: "取消",
        Click_OK: null,
        Click_Cancel: null
    }, {
        _BODY: body
    }, option);

    var o = $("#modal-" + type).tmpl(data);
    o.find("button#_OK").click(data.Click_OK);
    o.find("button#_Cancel").click(data.Click_Cancel);
    o.modal({
        backdrop: "static",
        keyboard: false
    });
}