using System.Web.Optimization;

namespace VehicleRental
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // ✅ JavaScript Bundles
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/lib/jquery/dist/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"));

            // ✅ CSS Bundles (Removed duplicate loader.css)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/lib/bootstrap/dist/css/bootstrap.min.css",
                      "~/css/site.css",
                      "~/css/addcar.css",
                      "~/css/addcarfoot.css",
                      "~/css/admin.css",
                      "~/css/dashboard.css",
                      "~/css/footer.css",
                      "~/css/loader.css",  
                      "~/css/logsign.css",
                      "~/css/rent.css",
                      "~/css/home.css"));

            // ✅ Enable optimizations for production
            BundleTable.EnableOptimizations = true;
        }
    }
}
