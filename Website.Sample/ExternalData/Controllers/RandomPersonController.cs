using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using umbraco;
using Umbraco.Web.Mvc;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class RandomPersonController : SurfaceController
    {
        public ActionResult Index()
        {
            var db = ApplicationContext.DatabaseContext.Database;
            // demo code, do not do this in production
            var randomperson = db.Fetch<Person>("WHERE Id > @0", 0).RandomOrder().First();
            return View("Index", randomperson);
        }
    }
}