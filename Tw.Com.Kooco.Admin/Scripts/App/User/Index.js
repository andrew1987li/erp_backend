/*帳號清單*/
define(['jquery', 'jIAn'], function ($, jIAn, bootbox) {
    var run = function (param) {
        // $('#list').LineLight().css("visibility", "visible").hide().fadeIn('fast');
        var form = jIAn.form;
        $('#search').click(function () {
            if ($('#kw').val() != '') {
                form.add_variable('KeyWord', $('#kw').val());
            }
            form.submit_form('post', "/User/Index");
        });
        $('#reset').click(function () {
            $('#kw').val('');
        });
        $(":button[id*='edit_']").click(function () {
            var id = $(this).attr('id').split("_")[1];
            form.add_variable('Todo', 'edit');
            form.add_variable('User.IdentityKey', id);
            form.submit_form('post', "/User/Edit");
        });

        //$(":button[id*='perm_']").click(function () {
        //    var id = $(this).attr('id').split("_")[1];
        //    form.add_variable('Todo', 'edit');
        //    form.add_variable('User.IdentityKey', id);
        //    form.submit_form('post', "/User/PermsEdit");
        //});

        $(":button[id*='action_']").click(function () {
            var id = $(this).attr('id').split("_")[1];
            form.add_variable('Todo', 'edit');
            form.add_variable('User.IdentityKey', id);
            form.submit_form('post', "/User/Action");
        });
    };
    return (function () { return { run: run }; })();
});