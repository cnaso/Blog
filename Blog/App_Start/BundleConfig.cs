using System.Web.Optimization;

namespace Blog.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Admin/Styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/bootstrap-theme.css")
                .Include("~/Content/Styles/Admin.css"));

            bundles.Add(new StyleBundle("~/Styles")
                .Include("~/Content/Styles/bootstrap.css")
                .Include("~/Content/Styles/Site.css"));

            bundles.Add(new ScriptBundle("~/Admin/Scripts")
                .Include("~/Scripts/jquery-3.1.1.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/bootstrap.js")
                .Include("~/Areas/Admin/Scripts/AdminHandlers.js"));

            bundles.Add(new ScriptBundle("~/Scripts")
                .Include("~/Scripts/jquery-3.1.1.js")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Scripts/bootstrap.js"));
        }
    }
}