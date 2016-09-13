define([
    "jquery",
    "jIAn",
    "jq_uniform",
    "notify",
    "fileUpload"
], function ($, jIAn) {
    var run = function (param) {

        $("#imgLeftFileupload").fileupload({
            dataType: "json",
            url: "/Ammas/Cms/AjaxUploadFiles",
            formData: { imgFolder: "OurGoal" },
            autoUpload: true,
            singleFileUploads: true,
            done: function (e, data) {
                console.log(data.result);
                var json = data.result;
                
                $("#imgLeft").attr("src", json.path + "/" + json.name + "?" + new Date().getTime());
               
                $(".bottom-left").notify({
                    message: { text: "Upload file success!" },
                    fadeOut: { enabled: true, delay: 3000 },
                    type: "success"
                }).show(); // for the ones that aren"t closable and don"t fade out there is a .hide() function.
                $("#progress-bar-left").css("width", "0");

                OurGoal.LeftImg = json.path + "/" + json.name;

                $.post("/Ammas/Cms/AjaxOurGoalEdit", OurGoal, "json").done(function (ajaxResult) {
                    

                    var code = parseInt(ajaxResult.Code, 10);
                    if (code > 0 && OurGoal.OurGoalId <= 0) {
                        OurGoal.OurGoalId = code;
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

        $("#imgRightFileupload").fileupload({
            dataType: "json",
            url: "/Ammas/Cms/AjaxUploadFiles",
            formData: { imgFolder: "OurGoal" },
            autoUpload: true,
            singleFileUploads: true,
            done: function (e, data) {
                console.log(data.result);
                var json = data.result;

                $("#imgRight").attr("src", json.path + "/" + json.name + "?" + new Date().getTime());

                $(".bottom-left").notify({
                    message: { text: "Upload file success!" },
                    fadeOut: { enabled: true, delay: 3000 },
                    type: "success"
                }).show(); // for the ones that aren"t closable and don"t fade out there is a .hide() function.
                $("#progress-bar-right").css("width", "0");

                OurGoal.RightImg = json.path + "/" + json.name;

                $.post("/Ammas/Cms/AjaxOurGoalEdit", OurGoal, "json").done(function (ajaxResult) {
                    var code = parseInt(ajaxResult.Code, 10);
                    if (code > 0 && OurGoal.OurGoalId <= 0) {
                        OurGoal.OurGoalId = code;
                    }

                }).fail(function () { $("#warning").text("Connection error").show(); });

            },
            change: function (e, data) {
                //progress-bar
                //$(".progress-bar").css("width", "0");
            }
        }).on("fileuploadprogressall", function (e, data) {
            var progress = parseInt(data.loaded / data.total * 100, 10);
            $("#progress-bar-right").css("width", progress + "%");
        });
    };
    return (function () { return { run: run }; })();
});