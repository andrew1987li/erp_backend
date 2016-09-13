define(['jquery', 'jIAn'], function ($, jIAn) {
    return (function () {
        var run = function (param) {
            /*if (code == 0) {
                $('#warning').text('沒有資料被更新');
            } else if (code == -1) {
                $('#warning').text('資料庫異常,系統錯誤');
            } else if (code == -2) {
                $('#warning').text('指定的帳號已被註冊');
            } else if (code == -11) {
                $('#warning').text('帳號欄位必填');
            } else if (code == -12) {
                $('#warning').text('密碼欄位必填');
            } else if (code == -13) {
                $('#warning').text('使用者名字欄位必填');
            } else {
                $('#warning').text('');
            }

            if (!ok) {
                $('#warning').css('display', 'block');
            }*/
            $('#firstName,#account,#pw').attr("data-container", "body").attr("data-placement", "right").attr("data-toggle", "popover");
            $('#cancel').click(function () {
                var form = jIAn.form;
                form.submit_form('post', "/User/Login");
            });
            $('#register').click(function () {
                var hasError = false;
                if ($('#account').val() == '') {
                    hasError = true;
                    $('#accountGroup').addClass('has-error');
                    $('#account').attr('data-content', 'Account field is required.').popover('show');
                } else if ($('#account').val().length > 64 || $('#account').val().length < 3) {
                    hasError = true;
                    $('#accountGroup').addClass('has-error');
                    $('#account').attr('data-content', 'Account should be between 4-32 characters.').popover('show');
                } else {
                    if ($('#accountGroup').hasClass('has-error')) {
                        $('#accountGroup').removeClass('has-error');
                        $('#account').attr('data-content', '').popover('hide');
                    }
                }

                if ($('#pw').val() == '') {
                    hasError = true;
                    $('#passwordGroup').addClass('has-error');
                    $('#pw').attr('data-content', 'Password field is required.').popover('show');
                } else if ($('#pw').val().length < 4 || $('#pw').val().length > 18) {
                    hasError = true;
                    $('#passwordGroup').addClass('has-error');
                    $('#pw').attr('data-content', 'passwords are case-sensitive and should be between 4-18 characters in length, alpha-numeric.').popover('show');
                } else {
                    if ($('#passwordGroup').hasClass('has-error')) {
                        $('#passwordGroup').removeClass('has-error');
                        $('#pw').attr('data-content', '').popover('hide');
                    }
                }

                if ($('#firstName').val() == '') {
                    hasError = true;
                    $('#firstNameGroup').addClass('has-error');
                    $('#firstName').attr('data-content', 'First name field is required').popover('show');
                } else {
                    if ($('#firstNameGroup').hasClass('has-error')) {
                        $('#firstNameGroup').removeClass('has-error');
                        $('#firstName').attr('data-content', '').popover('hide');
                    }
                }

                if ($('#lastName').val() == '') {
                    hasError = true;
                    $('#lastNameGroup').addClass('has-error');
                    $('#lastName').attr('data-content', 'Last name field is required.').popover('show');
                } else {
                    if ($('#lastNameGroup').hasClass('has-error')) {
                        $('#lastNameGroup').removeClass('has-error');
                        $('#lastName').attr('data-content', '').popover('hide');
                    }
                }
                if (!hasError) {
                    $.ajax({
                        type: 'post',
                        url: '/User/AjaxRegister',
                        data: {
                            'User.FirstName': $('#firstName').val(),
                            'User.Password': $('#pw').val(),
                            'User.Account': $('#account').val(),
                            'User.LastName': $('#lastName').val(),
                            'User.IdentityKey': 0
                        },
                        success: function (result) {
                            var code = parseInt(result.Code, 10);
                            if (code > 0) {
                                var form = jIAn.form;
                                form.add_variable('IsTransfer', true);
                                form.add_variable('StayTime', 3);
                                form.add_variable('Target', '/User/Login');
                                form.add_variable('Method', 'post');
                                form.add_variable('Parameter', '');
                                form.add_variable('Message', 'Account registration is complete will soon go to the login page.');
                                form.submit_form('post', "/Misc/Message");
                            } else {
                                if (code == 0) {
                                    $('#warning').text('沒有資料被更新');
                                } else if (code == -1) {
                                    $('#warning').text('資料庫異常,系統錯誤');
                                } else if (code == -2) {
                                    $('#warning').text('Account already exists');
                                } else if (code == -11) {
                                    $('#warning').text('Account field is required');
                                } else if (code == -12) {
                                    $('#warning').text('Password field is required');
                                } else if (code == -13) {
                                    $('#warning').text('User name field is required');
                                } else {
                                    $('#warning').text('');
                                }
                                $('#warning').css('display', 'block');
                            }
                        }
                    });
                }
            });
            //載入完畢再顯示畫面
            $('.box').css('visibility', 'visible');
            // $('#detail').css('visibility', 'visible');
        };
        return { run: run };
    })();
});