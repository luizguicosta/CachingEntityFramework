using CachingEntityFramework.Data;
using CachingEntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingEntityFramework.Helper
{
    public static class HelperPerson
    {
        public static int AddPerson(Person person)
        {
            using (var context = new DataBaseContext())
            {
                var newPerson = context.People.Add(new Person { Name = person.Name, Age = person.Age, LastName = person.LastName });
                context.SaveChanges();
                return newPerson.ID;
            };
        }
    }
}
