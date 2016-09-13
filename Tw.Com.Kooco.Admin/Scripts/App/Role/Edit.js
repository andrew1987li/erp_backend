define([
    "jquery",
    "jIAn",
    "bootstrap",
    "bootstrap_switch",
    "jq_validate_zh_tw"
], function ($, jIAn) {
    var run = function (param) {
        var form = jIAn.form;

        $('.js_back').click(function (e) {
            e.preventDefault();
            form.submit_form('get', "/Role/Index");
        });

        $('.js_all_on').click(function (e) {
            e.preventDefault();

            $(":checkbox[name=\"ActionIDs\"]").each(function (index) {
                //$(this).bootstrapSwitch('state', true);
                $(this).prop('checked', true);
            });
        });

        $(".js_all_off").click(function (e) {
            e.preventDefault();

            $(":checkbox[name=\"ActionIDs\"]").each(function (index) {
                //$(this).bootstrapSwitch('state', false);
                $(this).prop('checked', false);
            });
        });

        var validateForm = function (form_id) {
            var form1 = $('#' + form_id);
            var error1 = $('.alert-danger', form1);
            var success1 = $('.alert-success', form1);

            form1.validate({
                errorElement: 'span', //default input error message container
                errorClass: 'help-block help-block-error', // default input error message class
                focusInvalid: false, // do not focus the last invalid input
                ignore: "",  // validate all fields including form hidden input

                invalidHandler: function (event, validator) { //display error alert on form submit
                    success1.hide();
                    error1.show();
                    Metronic.scrollTo(error1, -200);
                },

                highlight: function (element) { // hightlight error inputs
                    $(element)
                        .closest('.form-group').addClass('has-error'); // set error class to the control group
                },

                unhighlight: function (element) { // revert the change done by hightlight
                    $(element)
                        .closest('.form-group').removeClass('has-error'); // set error class to the control group
                },

                success: function (label) {
                    label
                        .closest('.form-group').removeClass('has-error'); // set success class to the control group
                },

                submitHandler: function () {
                    var formData = form1.serialize();

                    $.post('/Role/AjaxEdit', formData, 'json').done(function (result) {
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
                    success1.show();
                    error1.hide();
                }
            })
        }

        validateForm('form1');
    };
    return (function () { return { run: run }; })();
});