using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Scoping;
using Umbraco.Web.Mvc;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class RandomPersonController : SurfaceController
    {
        private readonly IScopeProvider scopeProvider;

        public RandomPersonController(IScopeProvider scopeProvider)
        {
            this.scopeProvider = scopeProvider;
        }

        public ActionResult Index()
        {
            using (var scope = scopeProvider.CreateScope(autoComplete: true))
            {
                // demo code, do not do this in production
                var randomperson = scope.Database.Fetch<Person>("WHERE Id > @0", 0).Shuffle().First();
                return View("Index", randomperson);
            }
        }
    }

    public static class Shuffler
    {
        private static readonly Random rng = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }
    }
}
