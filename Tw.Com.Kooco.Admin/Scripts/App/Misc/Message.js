define(['jquery', 'jIAn'], function($, jIAn) {
    return (function() {
        var redirect = function (param) {
            var form = jIAn.form;
            var ps = decodeURIComponent(param.Parameter).split('&');
            $.each(ps, function(index, value) {
                var p = value.split('=');
                form.add_variable(p[0], p[1]);
            });
            form.submit_form(param.Method, (param.Target == '') ? "/Home/Index" : param.Target);
        }, run = function (param) {
            if (param == null) {
                param = {};
            }
            if ((typeof(param.IsTransfer) == 'undefined' )) {
                param.IsTransfer = isTransfer;
            }

            if (typeof (param.Target) == 'undefined') {
                param.Target = target;
            }
            if (typeof (param.StayTime) == 'undefined') {
                param.StayTime = stayTime;
            }
            if (typeof (param.Parameter) == 'undefined') {
                param.Parameter = parameter;
            }

            if (typeof (param.Method) == 'undefined') {
                param.Method = method;
            }

            if (param.IsTransfer) {
               // jIAn.log( param.StayTime * 1000);
                setTimeout(function() { redirect(param); }, param.StayTime * 500);
                $('#tranfer').on('click', function() { redirect(param); });
                //jIAn.log('animate start');
                $("#progress-bar").animate({
                        width: "100%",
                    }, param.StayTime * 500, function() {
                        // jIAn.log('animate done');
                    });
            } else {
                $('#tranfer').css('visibility', 'hidden');
                $('#progress').css('display', 'none');
            }
        };
        return { run: run };
    })();
});