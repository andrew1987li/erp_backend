var config = {
    baseUrl: "/Scripts",
    waitSeconds: 200,
    paths: {
        crossroads: ["/Content/kooco/plugins/crossroads/crossroads.min", "https://cdnjs.cloudflare.com/ajax/libs/crossroads/0.12.0/crossroads.min"],
        signals: ["/Content/kooco/plugins/signals/signals.min", "https://cdnjs.cloudflare.com/ajax/libs/js-signals/1.0.0/js-signals.min"],
        //respond: ["//cdnjs.cloudflare.com/ajax/libs/respond.js/1.4.2/respond.min", "respond.min"],
        jquery: ["/Content/kooco/plugins/jquery/jquery-2.1.3.min", "https://cdnjs.cloudflare.com/ajax/libs/jquery/2.1.3/jquery.min"],
        jIAn: "/Content/kooco/plugins/jIAn/jIAn",
        typeahead: ["/Content/kooco/plugins/typeahead/typeahead.bundle.min", "typeahead/typeahead.bundle"],
        handlebars: ["/Content/kooco/plugins/handlebars/handlebars-v1.3.0"],
        contextmenu: ["/Content/kooco/plugins/jquery.ui/jquery.ui-contextmenu"],
        fileDownload: ["/Content/kooco/plugins/jquery.file/jquery.fileDownload.min"],
        fileUpload: ["/Content/kooco/plugins/jquery.file/jquery.fileupload.min"],
        jq_ui_widget: ["/Content/kooco/plugins/jquery.ui/jquery.ui.widget.min"],
        umeditor: ["/Content/kooco/plugins/umeditor/umeditor"],
        umeditorconfig: ["/Content/kooco/plugins/umeditor/umeditor.config"],
        umeditorlang: ["/Content/kooco/plugins/umeditor/lang/en/en"],
        notify: ["/Content/kooco/plugins/bootstrap/bootstrap.notify.min", "/Content/kooco/plugins/bootstrap/bootstrap.notify"],
        tmpl: ["/Content/kooco/plugins/jquery.tmpl/jquery.tmpl.min"],
        hashset: "/Content/kooco/plugins/jshashtable/hashset",
        hashtable: "/Content/kooco/plugins/jshashtable/hashtable",
        numberformatter: ["/Content/kooco/plugins/numberformatter/jquery.numberformatter-1.2.4.min", "/Content/kooco/plugins/numberformatter/jquery.numberformatter-1.2.4"],
        rangeSlider: ["/Content/kooco/plugins/ion.rangeslider/js/ion.rangeSlider.min"],
        numeric: ["/Content/kooco/plugins/numeric/jquery.numeric.min", "/Content/kooco/plugins/numeric/jquery.numeric"],
        json: ["/Content/kooco/plugins/json/json3.min"],
        bs_contextmenu: ["/Content/kooco/plugins/bootstrap/bootstrap-contextmenu.min", "/Content/kooco/plugins/bootstrap/bootstrap-contextmenu"],
        lazyload: "/Content/global/plugins/jquery.lazyload.min",

        moment: ["/Content/kooco/plugins/moment/moment-with-locales"],
        moment_tz: ["/Content/kooco/plugins/moment/moment-timezone-with-data"],

        bootstrap: "/js/bootstrap?v=1",
        bootstrap_switch: "/Content/global/plugins/bootstrap-switch/js/bootstrap-switch",
        //jq_vmap: "/js/jq_vmap?v=1",
        //jq_flot: "/js/jq_flot?v=1",

        kooco: ["kooco"],
        modal: ["modal"],
        clock: ["clock"],
        
        GoblinModal: ["App/PingBall/Shared/GoblinModal"],

        "amcharts": "/Content/global/plugins/amcharts/amcharts/amcharts",
        "amcharts.funnel": "/Content/global/plugins/amcharts/amcharts/funnel",
        "amcharts.gauge": "/Content/global/plugins/amcharts/amcharts/gauge",
        "amcharts.pie": "/Content/global/plugins/amcharts/amcharts/pie",
        "amcharts.radar": "/Content/global/plugins/amcharts/amcharts/radar",
        "amcharts.serial": "/Content/global/plugins/amcharts/amcharts/serial",
        "amcharts.xy": "/Content/global/plugins/amcharts/amcharts/xy",

        "amcharts.exporting.amexport": "/Content/global/plugins/amcharts/amcharts/exporting/amexport",
        "amcharts.exporting.canvg": "/Content/global/plugins/amcharts/amcharts/exporting/canvg",
        "amcharts.exporting.rgbcolor": "/Content/global/plugins/amcharts/amcharts/exporting/rgbcolor",
        "amcharts.exporting.filesaver": "/Content/global/plugins/amcharts/amcharts/exporting/filesaver",

        "amcharts.themes.black": "/Content/global/plugins/amcharts/amcharts/themes/black",
        "amcharts.themes.chalk": "/Content/global/plugins/amcharts/amcharts/themes/chalk",
        "amcharts.themes.dark": "/Content/global/plugins/amcharts/amcharts/themes/dark",
        "amcharts.themes.light": "/Content/global/plugins/amcharts/amcharts/themes/light",
        "amcharts.themes.patterns": "/Content/global/plugins/amcharts/amcharts/themes/patterns",

        layout: ["/Content/admin/layout/scripts/layout"],
        quick_idebar: ["/Content/admin/layout/scripts/quick-sidebar"],

        bs_modalmanager: ["/Content/global/plugins/bootstrap-modal/js/bootstrap-modalmanager"],
        bs_modal: ["/Content/global/plugins/bootstrap-modal/js/bootstrap-modal"],
        bs_dropdown: ["/Content/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min"],

        jq_migrate: ["/Content/global/plugins/jquery-migrate.min"],
        jq_ui: ["/Content/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min"],

        jq_slimscroll: ["/Content/global/plugins/jquery-slimscroll/jquery.slimscroll.min"],
        jq_blockui: ["/Content/global/plugins/jquery.blockui.min"],
        jq_cokie: ["/Content/global/plugins/jquery.cokie.min"],
        jq_uniform: ["/Content/global/plugins/uniform/jquery.uniform.min"],
        jq_pulsate: ["/Content/global/plugins/jquery.pulsate.min"],
        fullcalendar: ["/Content/global/plugins/fullcalendar/fullcalendar.min"],
        jq_easypiechart: ["/Content/global/plugins/jquery-easypiechart/jquery.easypiechart.min"],
        jq_sparkline: ["/Content/global/plugins/jquery.sparkline.min"],
        metronic: ["/Content/global/scripts/metronic"],

        select2: ["/Content/global/plugins/select2/select2.min"],
        datatablesAll: ["/Content/global/plugins/datatables/all.min"],

        datatables: ["/Content/global/plugins/datatables/media/js/jquery.dataTables"],
        bs_datatables: ["/Content/global/plugins/datatables/plugins/bootstrap/dataTables.bootstrap"],

        datePicker: ["/Content/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker"],
        datePicker_zh_tw: ["/Content/global/plugins/bootstrap-datepicker/js/locales/bootstrap-datepicker.zh-TW"],

        dateTimePicker: ["/Content/global/plugins/bootstrap-datetimepicker/js/bootstrap-datetimepicker.min"],
        dateTimePicker_zh_tw: ["/Content/global/plugins/bootstrap-datetimepicker/js/locales/bootstrap-datetimepicker.zh-TW"],

        jq_validate: ["/Content/global/plugins/jquery-validation/js/jquery.validate.min"],
        jq_validate_zh_tw: ["/Content/global/plugins/jquery-validation/js/localization/messages_zh_TW"],
        jq_validate_methods: ["/Content/global/plugins/jquery-validation/js/additional-methods.min"],

        ckeditor: ["/Content/global/plugins/ckeditor/ckeditor"],

        pace: ["/Content/global/plugins/pace/pace.min"],

        nestable: ["/Content/global/plugins/jquery-nestable/jquery.nestable"],
        icheck: ["/Content/global/plugins/icheck/icheck.min"],
        nouislider: ["/Content/global/plugins/nouislider/jquery.nouislider.min"],

        touchspin: ["/Content/global/plugins/bootstrap-touchspin/bootstrap.touchspin"],
        inputmask: ["/Content/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min"],

        jstree: ["/Content/global/plugins/jstree/dist/jstree.min"],

        lightbox: ["/Content/kooco/plugins/ekko-lightbox/ekko-lightbox.min"]
    },

    shim: {
        "crossroads": ["signals"],
        "jquery": ["crossroads"],
        "jquery.colorize": ["jquery"],
        "jq_migrate": ["jquery"],
        "jq_ui": ["jquery"],
        "jq_ui_widget": ["jquery"],
        "bootstrap": ["jquery"],
        "bootstrap_switch" : { deps: ["jquery","bootstrap"] },
        "jq_slimscroll": ["jquery"],
        "jq_blockui": ["jquery"],
        "jq_cokie": ["jquery"],
        "jq_uniform": ["jquery"],
        "jq_vmap": ["jquery"],
        "jq_flot": ["jquery"],
        "jq_pulsate": ["jquery"],
        "fullcalendar": ["jquery"],
        "jq_easypiechart": ["jquery"],
        "jq_sparkline": ["jquery"],

        typeahead: { deps: ["handlebars"] },

        numberformatter: ["jquery", "hashtable"],

        "datatables": ["jquery"],
        "bs_datatables": ["datatables", "bootstrap"],

        bs_contextmenu: ["bootstrap"],

        "metronic": ["jquery"],
        "layout": ["jquery", "metronic"],
        "quick_idebar": ["jquery", "metronic"],

        "datePicker": ["jquery"],
        "datePicker_zh_tw": ["datePicker"],

        "dateTimePicker": ["jquery"],
        "dateTimePicker_zh_tw": ["dateTimePicker"],

        "umeditor": ["umeditorconfig"],
        "umeditorlang": ["umeditor"],

        "bs_modalmanager": ["bootstrap"],
        "bs_modal": ["bs_modalmanager"],

        "kooco": ["jquery"],
        "clock": ["jquery", "moment"],
        "modal": ["jquery", "bootstrap"],
        "tmpl": ["jquery"],

        nestable: ["jquery"],
        icheck: ["jquery"],
        nouislider: ["jquery"],
        rangeSlider: ["jquery"],
        touchspin: ["bootstrap"],
        inputmask: ["jquery"],
        numeric: ["jquery"],
        jstree: ["jquery"],

        moment_tz: ["moment"],

        "amcharts.funnel": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.gauge": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.pie": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.radar": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.serial": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.xy": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },

        "amcharts.exporting.amexport": {
            deps: ["amcharts"],
            exports: "amExport ",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.exporting.canvg": {
            deps: ["amcharts"],
            exports: "amExport ",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.exporting.rgbcolor": {
            deps: ["amcharts"],
            exports: "amExport ",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.exporting.filesaver": {
            deps: ["amcharts"],
            exports: "amExport ",
            init: function () {
                AmCharts.isReady = true;
            }
        },

        "amcharts.themes.black": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.themes.chalk": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.themes.dark": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.themes.light": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },
        "amcharts.themes.patterns": {
            deps: ["amcharts"],
            exports: "AmCharts",
            init: function () {
                AmCharts.isReady = true;
            }
        },

        enforceDefine: true
    }
};
require.config(config);

require([
    "crossroads",
    "jIAn",
    "jquery",
    "bootstrap",
    "metronic",
    "layout",
    "jq_cokie",
    "tmpl",
    "json",
    "modal",
    "moment"
], function (crossroads, jIAn) {
    var route = location.pathname + ((location.search !== "") ? "{?query}" : "");
    var loadJs = function (url) {
        var js = url.substr(1, url.length).split("?")[0];
        if (js.indexOf("/", js.length - 1) !== -1) {
            js = js.substr(0, js.length - 1);
        }
        if (js === "") {
            js = "Home";
        }
        ///Ammas/Cms/Announcements
        // jIAn.log("Ammas/ClientScript/Cms/Announcements load");
        //var output = [a.slice(0, position), b, a.slice(position)].join('');
        if (js.toLowerCase().indexOf("ammas") === 0) {
            js = [js.slice(0, 6), "ClientScript/", js.slice(6)].join("");
            if (js.split("/").length === 3) {
                js += "/Index";
            } 
            return "/areas/" + js + ".js";;
        } else {
            if (js.split("/").length === 1) {
                js += "/Index";
            }
            return "App/" + js;
        }
    };
    crossroads.addRoute(route, function (param) {
        var js = loadJs(location.pathname);
        require([js, "kooco"], function (obj,kooco) {
            obj.run(param);
            jIAn.log("路由-載入完畢︰" + location.pathname + location.search);
            jIAn.log(js);
          
            kooco.run(param);

            //require([loadJs(location.pathname)], function (obj) {
            //    obj.run(param);
            //    jIAn.log("loadJs:" + loadJs(location.pathname));
            //});
        });
    });

    crossroads.parse(location.pathname + location.search);
});

function getMyJS(url) {
    var js = url.substr(1, url.length).split("?")[0];
    if (js.indexOf("/", js.length - 1) !== -1) {
        js = js.substr(0, js.length - 1);
    }
    if (js === "") {
        js = "Home";
    }

    if (js.toLowerCase().indexOf("ammas") === 0) {
        if (js.split("/").length === 2) {
            js += "/Index";
        }
        //return js;
    } else {
        if (js.split("/").length === 1) {
            js += "/Index";
        }
        //return "App/" + js;
    }
    js = "App/" + js;

    return js;
}

function loadCss(url) {
    var link = document.createElement("link");
    link.type = "text/css";
    link.rel = "stylesheet";
    link.href = url;
    document.getElementsByTagName("head")[0].appendChild(link);
}