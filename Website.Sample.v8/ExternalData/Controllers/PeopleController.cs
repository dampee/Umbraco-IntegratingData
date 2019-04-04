using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;
using Website.Sample.PublishedContentModels;

namespace Website.Sample.ExternalData
{
    public class PeopleController : RenderMvcController
    {
        private readonly PersonRepository _personRepository;

        public PeopleController(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: Person
        public override ActionResult Index(ContentModel model)
        {
            if (model?.Content == null) { return HttpNotFound(); }

            var persons = _personRepository.GetAll();
            if (persons == null) { return HttpNotFound(); }

            var vm = new PersonListViewModel(model.Content) { Persons = persons };

            return base.Index(vm);
        }
    }
}
