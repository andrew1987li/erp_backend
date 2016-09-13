using System.Web.Optimization;

namespace Tw.Com.Kooco.Admin
{
    public class BundleConfig
    {
        // 如需「搭配」的詳細資訊，請瀏覽 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var transform = new CssRewriteUrlTransform();
            bundles.Add(new StyleBundle("~/css/golbal_mandatory").Include(
                "~/Content/global/plugins/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform()).Include(
                "~/Content/global/plugins/uniform/css/uniform.default.css", new CssRewriteUrlTransform()).Include(
                "~/Content/global/plugins/bootstrap-switch/css/bootstrap-switch.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/css/page_level").Include(
               "~/Content/global/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css", new CssRewriteUrlTransform()).Include(
               "~/Content/global/plugins/bootstrap-modal/css/bootstrap-modal.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/css/page_styles").Include(
               "~/Content/admin/pages/css/tasks.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/css/theme_styles").Include(
               "~/Content/global/css/components.css", new CssRewriteUrlTransform()).Include(
               //"~/Content/global/css/plugins.csss", new CssRewriteUrlTransform()).Include(
               "~/Content/admin/layout/css/layout.css", new CssRewriteUrlTransform()).Include(
               "~/Content/admin/layout/css/themes/darkblue.css", new CssRewriteUrlTransform()).Include(
               "~/Content/admin/layout/css/custom.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/js/bootstrap").Include(
                "~/Content/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                "~/Content/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                "~/Content/global/plugins/bootstrap-daterangepicker/daterangepicker.js"
                ));
            //Awes模組
            bundles.Add(new ScriptBundle("~/bundle/Scripts/js").Include(
                "~/Scripts/AwesomeMvc.js",
                "~/Scripts/awem.js",
                "~/Scripts/utils.js",
                "~/Scripts/Site.js")
                );
            //JQuery-Validation Plugin
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            /*
            bundles.Add(new ScriptBundle("~/js/jq_vmap").Include(
                "~/Content/global/plugins/jqvmap/jqvmap/jquery.vmap.js",
                "~/Content/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js",
                "~/Content/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js",
                "~/Content/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js",
                "~/Content/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js",
                "~/Content/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js",
                "~/Content/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js"));

            bundles.Add(new ScriptBundle("~/js/jq_flot").Include(
                "~/Content/global/plugins/flot/jquery.flot.min.js",
                "~/Content/global/plugins/flot/jquery.flot.resize.min.js",
                "~/Content/global/plugins/flot/jquery.flot.categories.min.js"));
            */
            //打包和壓縮
            BundleTable.EnableOptimizations = false;
        }
    }
}