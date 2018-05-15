using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class PersonViewModel : RenderModel
    {
        public PersonViewModel(IPublishedContent content, CultureInfo culture) : base(content, culture)
        {
        }

        public PersonViewModel(IPublishedContent content) : base(content)
        {
        }

        public Person Person { get; set; }
    }
}