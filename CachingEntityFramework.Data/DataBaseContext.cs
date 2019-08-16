using CachingEntityFramework.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingEntityFramework.Data
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("CachingDataBase")
        {
            Database.SetInitializer<DataBaseContext>(new CreateDatabaseIfNotExists<DataBaseContext>());

            Database.Log = d => System.Diagnostics.Debug.WriteLine(d);
        }

        public DbSet<Person> People { get; set; }
    }
}
