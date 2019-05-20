using Umbraco.Web.Models;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class PersonViewModel : ContentModel
    {
        public PersonViewModel(Umbraco.Core.Models.PublishedContent.IPublishedContent content) : base(content)
        {
        }

        public Person Person { get; set; }
    }
}
