using System.Web.Mvc;
using System.Web.Routing;
using umbraco;
using Umbraco.Core;

namespace Website.Sample.ImportData
{
    public class RouteRegistration : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext
            applicationContext)
        {
            RouteTable.Routes.MapRoute(
                name: "googleImport",
                url: GlobalSettings.UmbracoMvcArea + "/backoffice/Import/{action}",
                defaults: new
                {
                    controller = "Import",
                    action = "GoogleImport"
                });
          
        }
    }
}