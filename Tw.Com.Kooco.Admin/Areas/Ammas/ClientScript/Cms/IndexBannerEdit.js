define([
    "jquery",
    "jIAn",
    "jq_uniform",
    "notify",
    "fileUpload",
    "jq_validate_zh_tw"
], function ($, jIAn) {
   // $(".numeric").numeric();
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

            jForm.submit_form("post", "/Ammas/Cms/IndexBanners");
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
                console.log(formData);
                // { "Entity.Name": $("#Name").val(), "Entity.Title": $("#Title").val(), "Entity.ImgPath": $("#img").val() }
                //return;
                $.post("/Ammas/Cms/AjaxIndexBannerEdit", form1.serialize()).done(function (ajaxResult) {
                    var target = (data.IndexBannerId > 0) ? "/Ammas/Cms/IndexBannerEdit" : "/Ammas/Cms/IndexBanners";
                    var parameter = [
                        "Entity.IndexBannerId=" + data.IndexBannerId,
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

        $("#fileupload").fileupload({
            dataType: "json",
            url: "/Ammas/Cms/AjaxUploadFiles",
            formData: { imgFolder: "IndexBanner" },
            autoUpload: true,
            singleFileUploads: true,
            done: function (e, data) {
                //console.log(data.result);
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
        var settings = $("#form_1").validate().settings;
        $.extend(settings, {
            rules: {
                'Entity.FirstString': { required: true },
                'Entity.FirstStringColor': { required: true },
                'Entity.SecondString': { required: true },
                'Entity.SecondStringColor': { required: true },
                'Entity.ThreeString': { required: true },
                'Entity.ThreeStringColor': { required: true },
                'Entity.Link': { required: true },
                'Entity.ImgPath': { required: true },
                'Entity.TextBody': { required: true }
            }
        });
    };
    return (function () { return { run: run }; })();
});