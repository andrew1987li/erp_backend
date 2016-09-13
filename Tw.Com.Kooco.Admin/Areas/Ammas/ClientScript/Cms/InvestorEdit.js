define([
    "jquery",
    "jIAn",
    "jq_uniform",
    "notify",
    "ckeditor",
    "jq_validate_zh_tw",
    "dateTimePicker",
    "dateTimePicker_zh_tw",
    "fileUpload"

], function ($, jIAn) {

    var
        run = function (param) {
            var jForm = jIAn.form;
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

                jForm.submit_form("post", "/Ammas/Cms/Investor");
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
                    $.post("/Ammas/Cms/AjaxInvestorEdit", formData, "json").done(function (ajaxResult) {
                        var target = (data.Entity.InvestorId > 0) ? "/Ammas/Cms/InvestorEdit" : "/Ammas/Cms/Investor";
                        var parameter = [
                            "Entity.InvestorId=" + data.Entity.InvestorId,
                            "StartDate=" + data.StartDate,
                            "EndDate=" + data.EndDate,
                            "KeyWord=" + data.KeyWord
                        ].join("&");

                        var code = parseInt(ajaxResult.Code, 10);
                        //alert(code);
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

            $("#PublishDate").datetimepicker({
                format: "yyyy-mm-dd",
                language: "zh-TW",
                todayHighlight: true,
                todayBtn: "linked",
                orientation: "left",
                autoclose: true,
                startView: "decade",
                minView: "month"
            });

            $("#fileupload").fileupload({
                dataType: "json",
                url: "/Ammas/Cms/AjaxUploadFiles",
                formData: { imgFolder: "IndexBanner" },
                autoUpload: true,
                singleFileUploads: true,
                done: function (e, data) {
                    console.log(data.result);
                    var json = data.result;

                    var oldFile = $("#img").val();
                    $("#img").val(json.path + "/" + json.name);
                    $("#previewImg").attr("src", json.path + "/" + json.name + "?" + new Date().getTime());
                    //$.ajax({
                    //    type: "post",
                    //    url: "/Ammas/Cms/AjaxIndexBannerDeleteFile",
                    //    data: { "Entity.ImgPath": oldFile }
                    //});
                    $(".bottom-left").notify({
                        message: { text: "Upload file success!" },
                        fadeOut: { enabled: true, delay: 3000 },
                        type: "success"
                    }).show(); // for the ones that aren"t closable and don"t fade out there is a .hide() function.
                    $(".progress-bar").css("width", "0");
                },
                change: function (e, data) {
                    //progress-bar
                    //$(".progress-bar").css("width", "0");
                }
            }).on("fileuploadprogressall", function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $(".progress .progress-bar").css("width", progress + "%");
            });
        };
    return (function () { return { run: run }; })();
});