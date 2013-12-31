using System.Web;
using System.Web.Optimization;

namespace cpl_azure
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js", 
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*", 
                        "~/Scripts/bootstrap",
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUploader").Include(
                        "~/Scripts/jquery.fileupload.js",
                        "~/Scripts/mvcfileupload/tmpl.min.js",
                        "~/Scripts/mvcfileupload/mvcfileupload.js",
                        "~/Scripts/mvcfileupload/jquery.fileupload-ui.js"));

            //bundles.Add(new ScriptBundle("~/bundles/CKEditor").Include(
            //            "~/libs/ckeditor/ckeditor.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/bootstrap.css",
                        "~/Content/themes/base/jquery-ui.*",
                        "~/Content/mvcfileupload/jquery.fileupload-bui.css"));        
        }
    }
}