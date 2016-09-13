using jIAnSoft.Framework.Configuration;

namespace Tw.Com.Kooco.Admin.Models {
    public static class WebStatus {
        static WebStatus() {
            Version = Section.Get.Common.Name;
        }

        public static string Version { get; }
    }
}