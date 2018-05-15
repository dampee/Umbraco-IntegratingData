using System.Collections.Generic;
using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class PersonListViewModel : RenderModel
    {
        public PersonListViewModel(IPublishedContent content, CultureInfo culture) : base(content, culture)
        {
        }

        public PersonListViewModel(IPublishedContent content) : base(content)
        {
        }

        public IEnumerable<Person> Persons { get; set; }

    }
}