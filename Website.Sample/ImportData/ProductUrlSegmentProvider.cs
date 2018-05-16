using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Strings;
using Umbraco.Web.Routing;

namespace Website.Sample.ImportData
{
    public class ProductUrlSegmentEvents : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UrlSegmentProviderResolver.Current.InsertType<ProductUrlSegmentProvider>(0);
        }
    }

    public class ProductUrlSegmentProvider : IUrlSegmentProvider
    {
        readonly IUrlSegmentProvider _provider = new DefaultUrlSegmentProvider();
        
        public string GetUrlSegment(IContentBase content, CultureInfo culture)
        {
            // only for product doctypes
            var productContentType = PublishedContentModels.Product.ModelTypeAlias;
            if (content.GetContentType().Alias != productContentType) return null;

            var segment = _provider.GetUrlSegment(content);

            // Watch the "ToUrlSegment()"
            var productName = content.GetValue<string>("productName")?.ToUrlSegment();
            return $"{segment}-{productName}";
        }

        public string GetUrlSegment(IContentBase content)
        {
            return GetUrlSegment(content, CultureInfo.InvariantCulture);

        }

    }
}