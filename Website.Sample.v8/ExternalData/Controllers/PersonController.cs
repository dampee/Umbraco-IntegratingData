using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace Website.Sample.ExternalData
{
    public class PersonController : RenderMvcController
    {
        private readonly PersonRepository _personRepository;

        public PersonController(PersonRepository personRepo)
        {
            _personRepository = personRepo;
        }

        // GET: Person
        public ActionResult Details(ContentModel model, string slug)
        {
            if (model?.Content == null || slug == null)
            {
                return HttpNotFound();
            }

            var person = _personRepository.GetByName(slug);
            if (person == null)
            {
                return NotFound(model);
            }

            var vm = new PersonViewModel(model.Content) { Person = person };

            return base.Index(vm);
        }

        private ActionResult NotFound(ContentModel model)
        {
            throw new NotImplementedException("The person is not found");
            return View(model);
        }
    }
}
