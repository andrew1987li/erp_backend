using jIAnSoft.Framework.Configuration;
using System;
using System.Web;
using System.Web.Caching;

namespace Tw.Com.Kooco.Admin.Providers {
    public static class CacheProvider {
        public static void Insert(string key, object obj) {
            HttpRuntime.Cache.Insert(
                key,
                obj,
                null,
                Cache.NoAbsoluteExpiration,
                new TimeSpan(0, 0, Section.Get.Web.CookieTimeout),
                CacheItemPriority.High,
                null);
        }

        public static T Get<T>(string key) {
            return (T)HttpRuntime.Cache.Get(key);
        }

        public static void Clear(string account) {
            HttpRuntime.Cache.Remove(account);
            HttpRuntime.Cache.Remove($"functionTree{account}");
        }
    }
}