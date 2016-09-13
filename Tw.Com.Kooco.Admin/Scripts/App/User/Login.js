define(['jquery', 'jIAn', 'jq_validate'], function ($, jIAn) {
    var doLogin = function () {
        var hasError = false;
        if ($('#account').val() == '') {
            hasError = true;
            errorMsg('請輸入帳號')
        } else if ($('#pw').val() == '') {
            hasError = true;
            errorMsg('請輸入密碼')
        }
        if (!hasError) {
            var server = $(this).attr('data-server');

            $.ajax({
                type: 'post',
                url: '/User/AjaxLogin',
                data: {
                    'User.Password': $('#pw').val(),
                    'User.Account': $('#account').val(),
                    'User.LoginServer': server
                },
                success: function (result) {
                    //var code = parseInt(result.Code, 10);
                    if (result.Ok) {
                        var target = jIAn.PHP.$_GET["ReturnUrl"];
                        if (null == target || '' == target) {
                            target = "/";
                        } else {
                            var regex = /\/(E|e)dit\/?/gi;  // 不能寫成 regex="/book\d+/gi";
                            target = target.replace(regex, "");
                        }
                        var form = jIAn.form;
                        form.add_variable('IsTransfer', true);
                        form.add_variable('StayTime', 1);
                        form.add_variable('Target', target);
                        form.add_variable('Method', 'post');
                        form.add_variable('Parameter', '');
                        form.add_variable('Message', 'Sign in success');
                        form.submit_form('post', "/Misc/Message");
                    } else {
                        errorMsg('帳號或密碼錯誤')
                    }
                }
            });
        }
    },
        run = function (param) {
            $('.js_SignIn').click(doLogin);
            $("#account,#pw").keypress(function (event) {
                if (event.which == 13) {
                    doLogin();
                    event.preventDefault();
                }
            });
            /*
            $.each(jIAn.PHP.$_GET, function (index, value) {
                var target = value;
                var regex = /\/(E|e)dit\/?/gi;  // 不能寫成 regex="/book\d+/gi";
                target = target.replace(regex, "");
                //console.log('target:' + target);
                console.log(index + ": " + target);
            });
            */
            //載入完畢再顯示畫面
            $('.box').css('visibility', 'visible');
        };
    return (function () { return { run: run }; })();
});

function errorMsg(msg) {
    $('#message').text(msg);
    $('.alert-danger').show();
}