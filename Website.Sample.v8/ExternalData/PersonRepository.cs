using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Persistence;
using Website.Sample.DbModels;

namespace Website.Sample.ExternalData
{
    public class PersonRepository
    {
        private readonly UmbracoDatabase _database;

        public PersonRepository()
        {
            // bad practice, use DI
            // see more info here: https://our.umbraco.org/documentation/Reference/Common-Pitfalls/#usage-of-singletons-and-statics
            _database = ApplicationContext.Current.DatabaseContext.Database;
        }

        public Person GetById(int personId)
        {
            return _database.SingleOrDefault<Person>(personId);
        }

        public Person GetByName(string personName)
        {
            return _database.FirstOrDefault<Person>("WHERE [Name] = @0", personName);
        }

        public IEnumerable<Person> GetAll()
        {
            return _database.Fetch<Person>("WHERE id>@0", 0);
        }
    }
}