define([
    "jquery",
    "jIAn",
    "clock"
], function ($, jIAn, clock) {
    var run = function (param) {
        Metronic.init(); // init metronic core componets
        if (typeof __URL_CONTENT != 'undefined') {
            Metronic.setAssetsPath(__URL_CONTENT)
        }
        Layout.init(); // init layout

        $(document).ajaxSend(function () { Metronic.startPageLoading({ animate: true }); });
        $(document).ajaxComplete(function (event, request, settings) {
            Metronic.stopPageLoading();
            try {
                if (request.responseJSON && request.responseJSON.Ok == false && request.responseJSON.Code == '-999') {
                    modal('alert', '權限不足')
                }
            } catch (e) {
            }
        });
        $(document).ajaxError(function () { Metronic.stopPageLoading(); });
        $(document).ajaxStop(function () { Metronic.stopPageLoading(); });

        $("#__Logout").on('click', function () {
            $.ajax({
                type: 'post',
                url: '/User/Logout',
                data: {},
                success: function () {
                    var form = jIAn.form;
                    form.add_variable('IsTransfer', true);
                    form.add_variable('StayTime', 1);
                    form.add_variable('Target', "/");
                    form.add_variable('Method', 'post');
                    form.add_variable('Parameter', '');
                    form.add_variable('Message', 'Logout success.');
                    form.submit_form('post', "/Misc/Message");
                }
            });
        });

        clock.init();
    };
    return (function () { return { run: run }; })();
});