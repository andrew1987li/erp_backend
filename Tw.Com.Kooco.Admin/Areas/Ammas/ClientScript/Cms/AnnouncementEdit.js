define([
    "jquery",
    "jIAn",
    "jq_uniform",
    "notify",
    "fileUpload",
    "ckeditor",
    "jq_validate_zh_tw",
    "dateTimePicker",
    "dateTimePicker_zh_tw",
    "numeric"
], function($, jIAn) {
    $(".numeric").numeric();
    var areaDisplay = function() {
            var settings = $("#form_1").validate().settings;
            var announcementType = parseInt($("#announcementType").val(), 10);
            if ($.inArray(announcementType, [1]) >= 0) {
                $('input[name="Announcement.ImgPath"]').closest("div.form-group").hide();
                $('input[name="Announcement.Link"]').closest("div.form-group").hide();

                var editor = CKEDITOR.instances["Announcement.TextBody"];
                if (editor) {
                    var text = editor.document.getBody().getText();
                    editor.destroy(true);
                    $('textarea[name="Announcement.TextBody"]').text(text);
                }
                $('textarea[name="Announcement.TextBody"]').closest("div.form-group").show();

                $.extend(settings, {
                    rules: {
                        'Announcement.Sort': { required: true },
                        'Announcement.Type': { required: true },
                        'Announcement.Title': { required: true },
                        'Announcement.StartDate': { required: true },
                        'Announcement.ImgPath': { required: false },
                        'Announcement.TextBody': { required: true }
                    }
                });
            } else if ($.inArray(announcementType, [2, 4, 5, 6, 7, 8, 9, 10, 11]) >= 0) {
                $('input[name="Announcement.ImgPath"]').closest("div.form-group").show();
                $('input[name="Announcement.Link"]').closest("div.form-group").show();
             

                $('textarea[name="Announcement.TextBody"]').closest("div.form-group").hide();

                $.extend(settings, {
                    rules: {
                        'Announcement.Sort': { required: true },
                        'Announcement.Type': { required: true },
                        'Announcement.Title': { required: true },
                        'Announcement.StartDate': { required: true },
                        'Announcement.ImgPath': { required: true },
                        'Announcement.TextBody': { required: false }
                    }
                });
            } else if ($.inArray(announcementType, [3]) >= 0) {
                $('input[name="Announcement.ImgPath"]').closest("div.form-group").show();
                $('input[name="Announcement.Link"]').closest("div.form-group").show();

                $('textarea[name="Announcement.TextBody"]').addClass("ckeditor");
                CKEDITOR.replace($('textarea[name="Announcement.TextBody"]').get(0));
                $('textarea[name="Announcement.TextBody"]').closest("div.form-group").show();

                $.extend(settings, {
                    rules: {
                        'Announcement.Sort': { required: true },
                        'Announcement.Type': { required: true },
                        'Announcement.Title': { required: true },
                        'Announcement.StartDate': { required: true },
                        'Announcement.ImgPath': { required: false },
                        'Announcement.TextBody': { required: true }
                    }
                });
            }
        },
        run = function(param) {
            var jForm = jIAn.form;

            $("#announcementType").change(areaDisplay);

            $(".js_back").click(function() {
                jForm.add_variable("AnnouncementType", data.ParamAnnouncementType);
                if (data.ParamKeyWord !== "") {
                    jForm.add_variable("KeyWord", data.ParamKeyWord);
                }
                if (data.ParamStartDate !== "") {
                    jForm.add_variable("StartDate", data.ParamStartDate);
                }
                if (data.ParamEndDate !== "") {
                    jForm.add_variable("EndDate", data.ParamEndDate);
                }

                jForm.submit_form("post", "/Ammas/Cms/Announcements");
            });

            $(".js_submit").click(function(e) {
                var announcementType = parseInt($("#announcementType").val());
                if (announcementType === 3) {
                    CKEDITOR.instances["Announcement.TextBody"].updateElement();
                }
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
                errorPlacement: function(error, element) {
                    if (element.attr("name") === "Announcement.TextBody") {
                        error.insertBefore('textarea[name="Announcement.TextBody"]');
                    } else {
                        error.insertBefore(element);
                    }
                },
                //showErrors: function(errorMap, errorList){
                //    var summary = "Please check following errors:";
                //    $.each(errorList, function () {
                //        summary += " * " + this.message + "<br>";
                //    });
                //    alert(summary);
                //},
                invalidHandler: function(event, validator) { //display error alert on form submit
                    success1.hide();
                    error1.show();
                    Metronic.scrollTo(error1, -200);
                },

                highlight: function(element) { // hightlight error inputs
                    $(element).closest(".form-group").addClass("has-error"); // set error class to the control group
                },

                unhighlight: function(element) { // revert the change done by hightlight
                    $(element).closest(".form-group").removeClass("has-error"); // set error class to the control group
                },

                success: function(label) {
                    label.closest(".form-group").removeClass("has-error"); // set success class to the control group
                },

                submitHandler: function(form) {
                    //htmlspecialchars
                    //$("#Announcement.TextBody").val(jIAn.PHP.htmlspecialchars($("#Announcement.TextBody").val()));
                    var formData = form1.serialize();
                    $.post("/Ammas/Cms/AjaxAnnouncementEdit", formData, "json").done(function (ajaxResult) {
                        console.log(formData);
                        var target = (data.AnnouncementId > 0) ? "/Ammas/Cms/AnnouncementEdit" : "/Ammas/Cms/Announcements";
                        var parameter = [
                            "Announcement.AnnouncementId=" + data.AnnouncementId,
                            "StartDate=" + data.ParamStartDate,
                            "EndDate=" + data.ParamEndDate,
                            "AnnouncementType=" + data.ParamAnnouncementType,
                            "KeyWord=" + data.ParamKeyWord
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
                    }).fail(function() { $("#warning").text("Connection error").show(); });
                    success1.show();
                    error1.hide();
                }
            });

            $(".form_datetime").datetimepicker({
                language: "zh-TW",
                todayHighlight: true,
                todayBtn: "linked",
                autoclose: true,
                isRTL: Metronic.isRTL(),
                format: "yyyy-mm-dd hh:ii",
                pickerPosition: (Metronic.isRTL() ? "bottom-right" : "bottom-left")
            });

            $("form").on("click", ".js_forever", function() {
                if ($(this).prop("checked")) {
                    $(".js_eDate").attr("required", false).val("");
                } else {
                    $(".js_eDate").attr("required", true);
                }
            });

            $("form").on("click change", ".js_eDate", function() {
                $(".js_forever").prop("checked", $(this).val() === "");
                $.uniform.update(".js_forever");
            });

            $(".js_eDate").trigger("click");

            $("#fileupload").fileupload({
                dataType: "json",
                url: "/Ammas/Cms/AjaxUploadFiles",
                formData: { imgFolder: "Announcement" },
                autoUpload: true,
                singleFileUploads: true,
                done: function(e, data) {
                    //console.log(data.result);
                    var json = data.result;

                    //var oldFile = $("#img").val();
                    $("#img").val(json.path + "/" + json.name);
                    $("#previewImg").attr("src", json.path + "/" + json.name + "?" + new Date().getTime());
                    /*$.ajax({
                        type: "post",
                        url: "/Ammas/Cms/AjaxAnnouncementDeleteFile",
                        data: { "Announcement.ImgPath": oldFile }
                    });*/
                    $(".bottom-left").notify({
                        message: { text: "Upload file success!" },
                        fadeOut: { enabled: true, delay: 3000 },
                        type: "success"
                    }).show(); // for the ones that aren"t closable and don"t fade out there is a .hide() function.
                    $(".progress-bar").css("width", "0");
                },
                change: function(e, data) {
                    //progress-bar
                    //$(".progress-bar").css("width", "0");
                }
            }).on("fileuploadprogressall", function(e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $(".progress .progress-bar").css("width", progress + "%");
            });

            areaDisplay();
        };
    return (function() { return { run: run }; })();
});