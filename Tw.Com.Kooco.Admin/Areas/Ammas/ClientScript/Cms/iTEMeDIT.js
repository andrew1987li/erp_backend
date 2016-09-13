define([
    "jquery",
    "jIAn",
    "jq_uniform",
    "notify",
    "ckeditor",
    "jq_validate_zh_tw"
    //"dateTimePicker",
    //"dateTimePicker_zh_tw"

], function ($, jIAn) {
    var run = function (param) {
        var jForm = jIAn.form;
        //IndexBannerEdit
        //$("#comment").addClass("ckeditor");
        // CKEDITOR.replace($('textarea[id="comment"]').get(0));
        // $("#comment").closest("div.form-group").show();

        $(".js_back").click(function () {
            if (data.KeyWord !== "") {
                jForm.add_variable("KeyWord", data.KeyWord);
            }
            if (data.StartDate !== "") {
                jForm.add_variable("StartDate", data.StartDate);
            }
            if (data.EndDate !== "") {
                jForm.add_variable("EndDate", data.EndDate);
            }

            jForm.submit_form("post", "/Ammas/Cms/ItemList");
        });


        var form1 = $("#form_1");
        var error1 = $(".alert-danger", form1);
        var success1 = $(".alert-success", form1);

        form1.validate({
            lang: "zh_tw",
            errorElement: "span", //default input error message container
            errorClass: "help-block help-block-error", // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            ignore: "", // validate all fields including form hidden input
            onclick: false,
            errorPlacement: function (error, element) {
                error.insertBefore(element);
            },
            //showErrors: function(errorMap, errorList){
            //    var summary = "Please check following errors:";
            //    $.each(errorList, function () {
            //        summary += " * " + this.message + "<br>";
            //    });
            //    alert(summary);
            //},
            invalidHandler: function (event, validator) { //display error alert on form submit
                success1.hide();
                error1.show();
                Metronic.scrollTo(error1, -200);
            },

            highlight: function (element) { // hightlight error inputs
                $(element).closest(".form-group").addClass("has-error"); // set error class to the control group
            },

            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest(".form-group").removeClass("has-error"); // set error class to the control group
            },

            success: function (label) {
                label.closest(".form-group").removeClass("has-error"); // set success class to the control group
            },

            submitHandler: function (form) {
                //htmlspecialchars
                //$("#Announcement.TextBody").val(jIAn.PHP.htmlspecialchars($("#Announcement.TextBody").val()));
                var formData = form1.serialize();
                //console.log(formData);
                $.post("/Ammas/Cms/AjaxItemEdit", form1.serialize()).done(function (ajaxResult) {
                    var target = (data.Entity.Id > 0) ? "/Ammas/Cms/ItemEdit" : "/Ammas/Cms/ItemList";
                    var parameter = [
                        "Entity.Id=" + data.Entity.Id,
                        "StartDate=" + data.StartDate,
                        "EndDate=" + data.EndDate,
                        "KeyWord=" + data.KeyWord
                    ].join("&");

                    var code = parseInt(ajaxResult.Code, 10);
                    // alert(code);
                    if (code > 0) {
                        jForm.add_variable("IsTransfer", true);
                        jForm.add_variable("Target", target);
                        jForm.add_variable("Method", "post");
                        jForm.add_variable("Parameter", parameter);
                        jForm.add_variable("Message", "Done");
                        jForm.submit_form("post", "/Misc/Message");
                    } else {
                        $(".bottom-left").notify({
                            message: { text: "something error... code:" + code },
                            fadeOut: { enabled: true, delay: 3000 },
                            type: "danger"
                        }).show();
                    }
                }).fail(function () { $("#warning").text("Connection error").show(); });
                success1.show();
                error1.hide();
            }
        });
        $("#StartDate").datetimepicker({
            format: "yyyy-mm-dd",
            language: "zh-TW",
            todayHighlight: true,
            todayBtn: "linked",
            orientation: "left",
            autoclose: true,
            startView: "decade",
            minView: "month"
        });
        $("#FinishDate").datetimepicker({
            format: "yyyy-mm-dd",
            language: "zh-TW",
            todayHighlight: true,
            todayBtn: "linked",
            orientation: "left",
            autoclose: true,
            startView: "decade",
            minView: "month"
        });
        var settings = $("#form_1").validate().settings;
        $.extend(settings, {
            rules: {
                'Entity.Id': { required: true },
                'Entity.ProjectName': { required: true },
                'Entity.Stauts': { required: true }
            }
        });
    };
    return (function () { return { run: run }; })();
});