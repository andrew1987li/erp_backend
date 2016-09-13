/*功能清單*/
define(['jquery', 'jIAn', 'nestable', 'json'], function ($, jIAn, JSON) {
    function subClick() {
        var form = jIAn.form;
        var id = $(this).attr('id').split("_")[1];
        form.add_variable('Todo', 'add');
        form.add_variable('Function.ParentFunctionId', id);
        form.submit_form('post', "/Function/Edit");
    }

    function editClick() {
        var form = jIAn.form;
        var id = $(this).attr('id').split("_")[1];
        form.add_variable('Todo', 'edit');
        form.add_variable('Function.FunctionId', id);
        form.submit_form('post', "/Function/Edit");
    }

    function delClick() {
        var id = $(this).attr('id').split("_")[1];
        modal('confirm', '確定要刪除 ' + $('#name_' + id).text() + '？', {
            Click_OK: function (e) {
                e.preventDefault();

                $.ajax({
                    type: 'post',
                    url: '/Function/AjaxDelete',
                    data: { 'Function.FunctionId': id },
                    success: function (result) {
                        var code = parseInt(result.Code, 10);
                        if (code > 0) {
                            $('#name_' + id).parent().parent().remove()
                            //var form = jIAn.form;
                            //form.add_variable('IsTransfer', true);
                            //form.add_variable('StayTime', 1);
                            //form.add_variable('Target', "/Function/Index");
                            //form.add_variable('Method', 'post');
                            //form.add_variable('Parameter', '');
                            //form.add_variable('Message', 'Success');
                            //form.submit_form('post', "/Misc/Message");
                        } else {
                            if (code == 0) {
                                modal('alert', '沒有資料被更新')
                            } else if (code == -1) {
                                $('#warning').text('資料庫異常,系統錯誤。');
                                modal('alert', '資料庫異常,系統錯誤。')
                            }
                            else if (code == -2) {
                                modal('alert', 'Please delete subfunction and then delete。')
                            }
                        }
                    }
                });
            }
        });
    }

    function aaa(out, items, parent, dep) {
        $.each(items, function (k, v) {
            var code = new String(k + 1001).substr(1);
            if (parent != '000') {
                code = parent + '/' + code;
            }
            out.push({
                id: v.id,
                priority: k + 1,
                code: code,
                parent: parent,
                dep: dep
            });
            if (typeof (v.children) != 'undefined') {
                aaa(out, v.children, code, dep + 1);
            }
        });
    }

    var run = function (param) {
        $('#addCategory').click(function () {
            var form = jIAn.form;
            form.add_variable('Todo', 'add');
            form.submit_form('post', "/Function/Edit");
        });
        $(":button[id*='edit_']").click(editClick);
        $(":button[id*='sub_']").click(subClick);
        //$(":input[id*='priority_']").click(priorityClick);
        $(":button[id*='del_']").click(delClick);

        var OldOtem = null
        $('#nestable_list').nestable({
            group: 1,
            dropCallback: function (details) {
                //console.log(details);
            },
        }).on('change', function (e) {
            var list = $(e.target)

            var jsonStr = window.JSON.stringify(list.nestable('serialize'));
            //console.log(jsonStr)

            if (OldOtem == null) {
                OldOtem = list.nestable('serialize');
            } else {
                var NewItem = list.nestable('serialize');

                var out = [];
                aaa(out, NewItem, '000', 0);

                jsonStr = window.JSON.stringify(out);
                //console.log(out)
                //console.log(jsonStr)

                $.each(out, function (k, v) {
                    console.log(window.JSON.stringify(v))
                })

                $.ajax({
                    type: 'post',
                    url: '/Function/AjaxSort',
                    data: { 'json': jsonStr },
                    success: function (result) {
                        var code = parseInt(result.Code, 10);
                        if (code > 0) {
                            //var form = jIAn.form;
                            //form.add_variable('IsTransfer', true);
                            //form.add_variable('StayTime', 1);
                            //form.add_variable('Target', "/Function/Index");
                            //form.add_variable('Method', 'post');
                            //form.add_variable('Parameter', '');
                            //form.add_variable('Message', 'Success');
                            //form.submit_form('post', "/Misc/Message");
                        } else {
                            if (code == 0) {
                                modal('alert', '沒有資料被更新')
                            } else if (code == -1) {
                                $('#warning').text('資料庫異常,系統錯誤。');
                                modal('alert', '資料庫異常,系統錯誤。')
                            }
                            else if (code == -2) {
                                modal('alert', 'Please delete subfunction and then delete。')
                            }
                        }
                    }
                });
            }
        }).trigger('change')

        $('#nestable_list_menu').on('click', function (e) {
            var target = $(e.target),
                action = target.data('action');
            if (action === 'expand-all') {
                $('.dd').nestable('expandAll');
            }
            if (action === 'collapse-all') {
                $('.dd').nestable('collapseAll');
            }
        });

        $('.dd').nestable('collapseAll');
    };
    return (function () { return { run: run }; })();
});