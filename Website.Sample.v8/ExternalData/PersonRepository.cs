using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class PersonRepository
    {
        private readonly IScopeProvider _scopeProvider;

        public PersonRepository(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }

        public Person GetById(int personId)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.SingleById<Person>(personId);
            }
        }

        public Person GetByName(string personName)
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.FirstOrDefault<Person>("WHERE [Name] = @0", personName);
            }
        }

        public IEnumerable<Person> GetAll()
        {
            using (var scope = _scopeProvider.CreateScope())
            {
                return scope.Database.Fetch<Person>("WHERE id>@0", 0);
            }
        }
    }

    public class PersonComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<PersonRepository>();
        }
    }
}
