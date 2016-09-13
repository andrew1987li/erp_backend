define([
    "jquery",
    "jIAn",
    "jq_uniform",
    "notify",
    "fileUpload"
], function ($, jIAn) {
    var run = function (param) {

        $("#imgFileupload").fileupload({
            dataType: "json",
            url: "/Ammas/Cms/AjaxUploadFiles",
            formData: { imgFolder: "OurGoal" },
            autoUpload: true,
            singleFileUploads: true,
            done: function (e, data) {
                //console.log(data.result);
                var json = data.result;

                $("#img").attr("src", json.path + "/" + json.name + "?" + new Date().getTime());

                $(".bottom-left").notify({
                    message: { text: "Upload file success!" },
                    fadeOut: { enabled: true, delay: 3000 },
                    type: "success"
                }).show(); // for the ones that aren"t closable and don"t fade out there is a .hide() function.
                $("#progress-bar-left").css("width", "0");

                Partner.ImgPath = json.path + "/" + json.name;

                console.log(json.name + "     " + Partner.ImgPath);

                $.post("/Ammas/Cms/AjaxPartnerEdit", Partner, "json").done(function (ajaxResult) {


                    var code = parseInt(ajaxResult.Code, 10);
                    if (code > 0 && Partner.PartnerId <= 0) {
                        Partner.PartnerId = code;
                    }

                }).fail(function () { $("#warning").text("Connection error").show(); });

            },
            change: function (e, data) {
                //progress-bar
                //$(".progress-bar").css("width", "0");
            }
        }).on("fileuploadprogressall", function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $("#progress-bar-left").css("width", progress + "%");
        });


    };
    return (function () { return { run: run }; })();
});