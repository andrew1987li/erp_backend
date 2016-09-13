define(['jquery', 'jIAn'], function ($, jIAn) {
    var run = function (param) {
        var form = jIAn.form;

        $('.js_edit').click(function (e) {
            e.preventDefault();

            var id = $(this).attr('data-id');
            var form = jIAn.form;
            form.add_variable('ID', id);
            form.submit_form('post', "/Role/Edit");
        });

        $('.js_del').click(function (e) {
            e.preventDefault();

            var id = $(this).attr('data-id');

            $.post('/Role/AjaxDel', { ID: id }, 'json').done(function (result) {
                var code = parseInt(result.Code, 10);
                if (code > 0) {
                    form.add_variable('IsTransfer', true);
                    form.add_variable('Target', '/Role/Index');
                    form.add_variable('Method', 'post');
                    form.add_variable('Parameter', '');
                    form.add_variable('Message', 'Success');
                    form.submit_form('post', "/Misc/Message");
                } else {
                    if (code == 0) {
                        $('#warning').text('沒有資料被更新');
                    } else if (code == -1) {
                        $('#warning').text('資料庫異常,系統錯誤。');
                    }
                    $('#warning').css('display', 'block');
                }
            }).fail(function () { $('#warning').text('Connection error').css('display', 'block'); });
        })
    };
    return (function () { return { run: run }; })();
});