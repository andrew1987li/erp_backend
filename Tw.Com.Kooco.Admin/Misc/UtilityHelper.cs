using jIAnSoft.Framework.Configuration;
using System;
using System.Text.RegularExpressions;

namespace Tw.Com.Kooco.Admin.Misc {
    public class UtilityHelper {
        /// <summary>
        /// 將轉入的日期時間（Timezone）轉成依設定檔指定時區進行 UTC 的轉換 格式︰yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime TimeZoneToUtc(string datetime) {
            DateTime d;
            if (!DateTime.TryParse(datetime, out d)) {
                d = DateTime.MinValue;
            }
            return TimeZoneToUtc(d);
        }

        /// <summary>
        /// 將轉入的日期時間（Timezone）轉成依設定檔指定時區進行 UTC 的轉換 格式︰yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime TimeZoneToUtc(DateTime datetime) {
            var utc = TimeZoneInfo.ConvertTimeToUtc(datetime, Section.Get.Common.TimeZone);
            return utc;
        }

        /// <summary>
        /// 將轉入的日期時間（UTC）轉成依設定檔指定時區進行 Timezone 的轉換 格式︰yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime UtcToTimeZone(string datetime) {
            DateTime d;
            if (!DateTime.TryParse(datetime, out d)) {
                d = DateTime.MinValue;
            }
            return UtcToTimeZone(d);
        }

        /// <summary>
        /// 將轉入的日期時間（UTC）轉成依設定檔指定時區進行 Timezone 的轉換 格式︰yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime UtcToTimeZone(DateTime datetime) {
            var timezone = TimeZoneInfo.ConvertTimeFromUtc(
                datetime,
                Section.Get.Common.TimeZone);
            return timezone;
        }

        public static string StripHtml(string input) {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}