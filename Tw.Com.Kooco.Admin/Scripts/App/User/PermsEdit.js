define([
    'jquery',
    'jIAn',
    'jq_uniform',
], function ($, jIAn) {
    var run = function (param) {
        var form = jIAn.form;
        $('.js_back').click(function (e) {
            e.preventDefault();
            form.submit_form('post', "/User/Index");
        });

        $('.js_submit').click(function (e) {
            e.preventDefault();
            var json = [];
            var f = $('input:radio[name*=allow_]:checked').map(
                function () {
                    var ids = $(this).attr('id').split('_');
                    var contact = new Object();
                    contact.function = ids[1];
                    contact.perms = 0;
                    $('input:checkbox:checked[name=perms_' + ids[1] + ']').each(function (index, value) {
                        contact.perms += parseInt($(value).val(), 10);
                    });
                    json.push(contact);
                    return ($(this).val() > 0) ? $(this).val() : null;
                }).get();
            //jIAn.log(typeof f.toString());
            if (f == null) {
                f = '';
            }
            $.ajax({
                type: 'post',
                url: '/User/AjaxPermsEdit',
                data: {
                    'User.IdentityKey': data.IdentityKey,
                    'User.Functions': f.toString(),
                    'User.Operations': JSON.stringify(json)
                },
                success: function (result) {
                    var code = parseInt(result.Code, 10);
                    if (code > 0) {
                        form.add_variable('IsTransfer', true);
                        form.add_variable('Target', "/User/PermsEdit");
                        form.add_variable('Method', 'post');
                        form.add_variable('Parameter', 'User.IdentityKey=' + data.IdentityKey);
                        form.add_variable('Message', 'Success');
                        form.submit_form('post', "/Misc/Message");
                    } else {
                        if (code == 0) {
                            $('#warning').text('沒有資料被更新');
                        } else if (code == -1) {
                            $('#warning').text('資料庫異常,系統錯誤。');
                        } else if (code == -2) {
                            $('#warning').text('Group name already exists.');
                        }
                        $('#warning').css('display', 'block');
                    }
                }
            });
        });
        //欄位資料綁定
        var ruleGroups = data.Functions.split(',');
        $.each(ruleGroups, function (index, value) {
            $("#allow_" + value + '_1').prop("checked", "checked");
        });
        if (data.Operations != '') {
            var perms = JSON.parse(data.Operations);
            $(perms).each(function (index, obj) {
                if (obj.perms <= 0)
                    return;
                $('input:checkbox[name=perms_' + obj.function + ']').each(function (index, value) {
                    var v = parseInt(obj.perms, 10) & parseInt($(value).val(), 10);
                    $(value).prop('checked', (v > 0) ? 'checked' : '');
                });
            });
        }
        // $('#detail').css('visibility', 'visible');
    };
    return (function () { return { run: run }; })();
});