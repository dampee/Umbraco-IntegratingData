using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Configuration;

namespace Website.Sample.ImportData
{
    public class RouteRegistration : IComponent
    {
        private readonly IGlobalSettings _globalSettings;
        public RouteRegistration(IGlobalSettings globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public void Initialize()
        {
            {
                RouteTable.Routes.MapRoute(
                    name: "googleImport",
                    url: _globalSettings.GetUmbracoMvcArea() + "/backoffice/Import/{action}",
                    defaults: new
                    {
                        controller = "Import",
                        action = "GoogleImport"
                    });
            }
        }

        public void Terminate()
        {
            // Do nothing
        }
    }
}
