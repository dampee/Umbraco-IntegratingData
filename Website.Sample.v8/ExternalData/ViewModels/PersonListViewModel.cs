using System.Collections.Generic;
using System.Globalization;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class PersonListViewModel : ContentModel
    {
        public PersonListViewModel(IPublishedContent content) : base(content)
        {
        }

        public IEnumerable<Person> Persons { get; set; }

    }
}
