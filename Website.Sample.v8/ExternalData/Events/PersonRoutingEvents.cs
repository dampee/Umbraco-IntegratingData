using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Website.Sample.PublishedContentModels;

namespace Website.Sample.ExternalData
{
    /// <summary>
    /// Class PersonRoutingEvents, will add a route for the persons
    /// </summary>
    /// <seealso cref="Umbraco.Core.ApplicationEventHandler" />
    public class PersonRoutingComposer
        : ComponentComposer<RegisterCustomRouteComponent>
    {
    }

    public class RegisterCustomRouteComponent : IComponent
    {
        private readonly FindPeopleRouteHandler _findPeopleRouteHandler;

        public RegisterCustomRouteComponent(FindPeopleRouteHandler findPeopleRouteHandler)
        {
            _findPeopleRouteHandler = findPeopleRouteHandler;
        }

        public void Initialize()
        {
            RouteTable.Routes.MapUmbracoRoute(
                "PersonDetails",
                "People/Details-{slug}", //"{lang}/People/{slug}",
                new
                {
                    controller = "Person",
                    action = "Details",
                    slug = UrlParameter.Optional
                },
                //next line does the magic
                _findPeopleRouteHandler
                );
        }

        public void Terminate()
        {
            // don't need to do a thing
        }
    }

    public class FindPeopleRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        private readonly PersonRepository _personRepository;
        public FindPeopleRouteHandler(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        protected override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            var peopleDocTypeAlias = People.ModelTypeAlias;
            //var languageName = requestContext.RouteData.Values["lang"].ToString().ToUpper();

            var peoplePage = umbracoContext.ContentCache
                .GetAtRoot()
                .First()
                .FirstChild(c => c.Name.ToLower() == peopleDocTypeAlias);

            return peoplePage;
        }
    }
}
