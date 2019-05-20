using System.Globalization;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Strings;
using Umbraco.Web.Routing;

namespace Website.Sample.ImportData
{
    public class ProductUrlSegmentComponent : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.UrlSegmentProviders().Append<ProductUrlSegmentProvider>();
            //UrlSegmentProviderResolver.Current.InsertType<ProductUrlSegmentProvider>(0);
        }
    }

    public class ProductUrlSegmentProvider : IUrlSegmentProvider
    {
        readonly IUrlSegmentProvider _provider = new DefaultUrlSegmentProvider();

        public string GetUrlSegment(IContentBase content, string culture = null)
        {

            // only for product doctypes
            var productContentType = PublishedContentModels.Product.ModelTypeAlias;
            if (content.ContentType.Alias != productContentType) { return null; };

            var segment = _provider.GetUrlSegment(content, culture);

            // Watch the "ToUrlSegment()"
            var productName = content.GetValue<string>("productName")?.ToUrlSegment();
            return $"{segment}-{productName}";
        }
    }
}
