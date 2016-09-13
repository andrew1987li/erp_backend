define([
    "jquery"
], function ($) {
        var serverOffset;
        var weekday = ['週日', '週一', '週二', '週三', '週四', '週五', '週六'];
        var handleInit = function () {
            $.get('/User/ServerTime', function (serverDate) {
                serverOffset = moment(serverDate).diff(new Date());

                (function update_time() {
                    var now = currentServerDate().format("HH:mm:ss");
                    var date = currentServerDate().format("YYYY年MM月DD日");
                    var week = weekday[currentServerDate().format('d')];
                    $("#__Today").html(date + ' ' + week);
                    $("#__NowTime").html(now);
                    setTimeout(update_time, 1000);
                })();
            });
        };

        var currentServerDate = function () {
            return moment().add('milliseconds', serverOffset);
        };

        return {
            init: function () {
                handleInit();
            }
        }
});