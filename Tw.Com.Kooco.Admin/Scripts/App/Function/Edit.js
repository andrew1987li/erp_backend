define([
    'jquery',
    'jIAn',
    'jq_validate_zh_tw',
], function ($, jIAn) {
    function reBuilePath() {
        var tmp = [];
        var area = $('#Area').val()
        var controller = $('#Controller').val()
        var action = $('#Action').val()
        if (area && area.length > 0) tmp.push(area);
        if (controller && controller.length > 0) tmp.push(controller);
        if (action && action.length > 0) tmp.push(action);

        var path = (tmp.length > 0) ? '/' + tmp.join('/') : '';
        var param = $('#Parameters').val();
        var target = (param == '') ? path : path + '?' + param

        $('#Target').val(target)
    }

    var run = function (param) {
        $('#tmplIconList').tmpl({ IconList: _IconList }).appendTo('#IconList');

        $('.js_openModal').click(function (e) {
            $('#IconModal').modal('toggle')
        });

        $('.js_clearIcon').click(function (e) {
            $('#Function_Icon').val('')
            $('#IconPreview').attr('class', '')
        });

        $('.js_select_icon').click(function (e) {
            var icon = $(this).find('span').attr('class')
            $('#Function_Icon').val(icon)
            $('#IconPreview').attr('class', icon)
            $('#IconModal').modal('toggle');
        });

        var AreaOptopns = {
            ROOT: []
        }

        var ControllerOptions = {}

        for (var i = 0; i < Controllers.length; i++) {
            var c = Controllers[i];

            if (c.Area == '') {
                AreaOptopns.ROOT.push(c.Name);
            } else {
                if (!AreaOptopns[c.Area]) {
                    AreaOptopns[c.Area] = []
                }
                AreaOptopns[c.Area].push(c.Name);
            }

            ControllerOptions[c.Name] = c.Actions
        }

        $('#Area').empty()
        $.each(AreaOptopns, function (k, v) {
            var value = (k == 'ROOT') ? '' : k;
            $('#Area')
                .append($("<option></option>")
                .attr("value", value)
                .text(value));
        })

        $('#Area').change(function (e) {
            var Key = $(this).val();
            Key = (Key == '') ? 'ROOT' : Key;
            $('#Controller').empty()
                .append($("<option></option>")
                    .attr("value", '')
                    .text(''));
            $.each(AreaOptopns[Key], function (k, v) {
                var value = v;
                $('#Controller')
                    .append($("<option></option>")
                    .attr("value", value)
                    .text(value));
            });
            reBuilePath();
        }).trigger('change');

        $('#Controller').change(function (e) {
            var Key = $(this).val();
            $('#Action').empty()
            if (Key == '' || typeof (ControllerOptions[Key]) == 'undefined') return;
            $.each(ControllerOptions[Key], function (k, v) {
                var value = v;
                $('#Action')
                    .append($("<option></option>")
                    .attr("value", value)
                    .text(value));
            });
            reBuilePath();
        }).trigger('change');

        $('#Action').change(function (e) {
            reBuilePath();
        });

        $('#Area').val(data.Area).change();
        $('#Controller').val(data.Controller).change();
        $('#Action').val(data.Action).change();

        $('#Parameters').keyup(function (e) {
            reBuilePath();
        });

        $('#linkTest').click(function (e) {
            $(this).attr('href', $('#Target').val())
        })

        var form = jIAn.form;
        $('.js_back').click(function () {
            form.submit_form('get', "/Function/Index");
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
                messages: {
                    select_multi: {
                        maxlength: jQuery.validator.format("Max {0} items allowed for selection"),
                        minlength: jQuery.validator.format("At least {0} items must be selected")
                    }
                },

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

                    console.log('success')
                },

                submitHandler: function () {
                    var formData = form1.serialize();

                    $.post('/Function/AjaxEdit', formData, 'json').done(function (result) {
                        var code = parseInt(result.Code, 10);
                        if (code > 0) {
                            form.add_variable('IsTransfer', true);
                            form.add_variable('Target', '/Function/Index');
                            form.add_variable('Method', 'post');
                            form.add_variable('Parameter', '');
                            form.add_variable('Message', 'Success');
                            form.submit_form('post', "/Misc/Message");
                        } else {
                            if (code == 0) {
                                modal('alert', '沒有資料被更新')
                            } else if (code == -1) {
                                modal('alert', '資料庫異常,系統錯誤。')
                            } else if (code == -2) {
                                modal('alert', '路徑不能重複')
                            } else if (code < 0) {
                                modal('alert', '程式錯誤')
                            }
                        }
                    }).fail(function () { $('#warning').text('Connection error').css('display', 'block'); });
                    success1.show();
                    error1.hide();
                }
            })
        }

        validateForm('form1')
    };
    return (function () { return { run: run }; })();
});

var _IconList = [
    "icon-user",
    "icon-user-female",
    "icon-users",
    "icon-user-follow",
    "icon-user-following",
    "icon-user-unfollow",
    "icon-trophy",
    "icon-speedometer",
    "icon-social-youtube",
    "icon-social-twitter",
    "icon-social-tumblr",
    "icon-social-facebook",
    "icon-social-dropbox",
    "icon-social-dribbble",
    "icon-shield",
    "icon-screen-tablet",
    "icon-screen-smartphone",
    "icon-screen-desktop",
    "icon-plane",
    "icon-notebook",
    "icon-moustache",
    "icon-mouse",
    "icon-magnet",
    "icon-magic-wand",
    "icon-hourglass",
    "icon-graduation",
    "icon-ghost",
    "icon-game-controller",
    "icon-fire",
    "icon-eyeglasses",
    "icon-envelope-open",
    "icon-envelope-letter",
    "icon-energy",
    "icon-emoticon-smile",
    "icon-disc",
    "icon-cursor-move",
    "icon-crop",
    "icon-credit-card",
    "icon-chemistry",
    "icon-bell",
    "icon-badge",
    "icon-anchor",
    "icon-action-redo",
    "icon-action-undo",
    "icon-bag",
    "icon-basket",
    "icon-basket-loaded",
    "icon-book-open",
    "icon-briefcase",
    "icon-bubbles",
    "icon-calculator",
    "icon-call-end",
    "icon-call-in",
    "icon-call-out",
    "icon-compass",
    "icon-cup",
    "icon-diamond",
    "icon-direction",
    "icon-directions",
    "icon-docs",
    "icon-drawer",
    "icon-drop",
    "icon-earphones",
    "icon-earphones-alt",
    "icon-feed",
    "icon-film",
    "icon-folder-alt",
    "icon-frame",
    "icon-globe",
    "icon-globe-alt",
    "icon-handbag",
    "icon-layers",
    "icon-map",
    "icon-picture",
    "icon-pin",
    "icon-playlist",
    "icon-present",
    "icon-printer",
    "icon-puzzle",
    "icon-speech",
    "icon-vector",
    "icon-wallet",
    "icon-arrow-down",
    "icon-arrow-left",
    "icon-arrow-right",
    "icon-arrow-up",
    "icon-bar-chart",
    "icon-bulb",
    "icon-calendar",
    "icon-control-end",
    "icon-control-forward",
    "icon-control-pause",
    "icon-control-play",
    "icon-control-rewind",
    "icon-control-start",
    "icon-cursor",
    "icon-dislike",
    "icon-equalizer",
    "icon-graph",
    "icon-grid",
    "icon-home",
    "icon-like",
    "icon-list",
    "icon-login",
    "icon-logout",
    "icon-loop",
    "icon-microphone",
    "icon-music-tone",
    "icon-music-tone-alt",
    "icon-note",
    "icon-pencil",
    "icon-pie-chart",
    "icon-question",
    "icon-rocket",
    "icon-share",
    "icon-share-alt",
    "icon-shuffle",
    "icon-size-actual",
    "icon-size-fullscreen",
    "icon-support",
    "icon-tag",
    "icon-trash",
    "icon-umbrella",
    "icon-wrench",
    "icon-ban",
    "icon-bubble",
    "icon-camcorder",
    "icon-camera",
    "icon-check",
    "icon-clock",
    "icon-close",
    "icon-cloud-download",
    "icon-cloud-upload",
    "icon-doc",
    "icon-envelope",
    "icon-eye",
    "icon-flag",
    "icon-folder",
    "icon-heart",
    "icon-info",
    "icon-key",
    "icon-link",
    "icon-lock",
    "icon-lock-open",
    "icon-magnifier",
    "icon-magnifier-add",
    "icon-magnifier-remove",
    "icon-paper-clip",
    "icon-paper-plane",
    "icon-plus",
    "icon-pointer",
    "icon-power",
    "icon-refresh",
    "icon-reload",
    "icon-settings",
    "icon-star",
    "icon-symbol-female",
    "icon-symbol-male",
    "icon-target",
    "icon-volume-1",
    "icon-volume-2",
    "icon-volume-off"
];