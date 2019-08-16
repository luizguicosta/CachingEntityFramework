using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CachingEntityFramework.Data;
using CachingEntityFramework.Domain;
using CachingEntityFramework.Helper;

namespace CachingEntityFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            int personID = HelperPerson.AddPerson(new Person
            {
                Name = "Guilherme",
                Age = 33,
                LastName = "Costa"
            });

            Stopwatch sw = new Stopwatch();

            sw.Start();
            CachingEntityFramework(personID);
            sw.Stop();
            Console.WriteLine("tempo decorrido : {0}ms", sw.ElapsedMilliseconds);

            //sw.Start();
            //CachingEntityFrameworkAsNoTracking(personID);
            //sw.Stop();
            //Console.WriteLine("tempo decorrido : {0}ms", sw.ElapsedMilliseconds);

            //sw.Start();
            //CachingEntityFrameworkAsNoTracking2(personID);
            //sw.Stop();
            //Console.WriteLine("tempo decorrido : {0}ms", sw.ElapsedMilliseconds);

            Console.ReadKey();
        }

        /// <summary>
        /// Caching do entity
        /// </summary>
        /// <param name="personID"></param>
        private static void CachingEntityFramework(int personID)
        {
            using (var context1 = new DataBaseContext())
            {
                var guilherme = context1.People.First(p => p.ID == personID);
                Console.WriteLine("Nome: " + guilherme.Name);

                using (var context2 = new DataBaseContext())
                {
                    context2.People.First(p => p.ID == personID).Name = "Luiz Guilherme";
                    context2.SaveChanges();
                }

                using (var context3 = new DataBaseContext())
                {
                    var personUpdate = context3.People.First(p => p.ID == personID);
                    Console.WriteLine("Nome da entidade atualizada: " + personUpdate.Name);
                }

                var personContext1 = context1.People.First(p => p.ID == personID);
                Console.WriteLine("Nome da pessoa do contexto 1: " + personContext1.Name);
            }
        }

        #region AsNoTracking
        /// <summary>
        /// AsNoTracking(sem adicionar dados no caching do entityFramework)
        /// </summary>
        /// <param name="personID"></param>
        private static void CachingEntityFrameworkAsNoTracking(int personID)
        {
            using (var context1 = new DataBaseContext())
            {
                //não adiciona dados no caching
                var guilherme = context1.People.Where(p => p.ID == personID).AsNoTracking().First();
                Console.WriteLine("Nome: " + guilherme.Name);

                using (var context2 = new DataBaseContext())
                {
                    context2.People.First(p => p.ID == personID).Name = "Luiz Guilherme";
                    context2.SaveChanges();
                }

                using (var context3 = new DataBaseContext())
                {
                    var personUpdate = context3.People.First(p => p.ID == personID);
                    Console.WriteLine("Nome da entidade atualizada: " + personUpdate.Name);
                }

                var personContext1 = context1.People.First(p => p.ID == personID);
                Console.WriteLine("Nome da pessoa do contexto 1: " + personContext1.Name);
            }
        }

        /// <summary>
        /// AsNoTracking(não ler dados no caching do entityFramework)
        /// </summary>
        /// <param name="personID"></param>
        private static void CachingEntityFrameworkAsNoTracking2(int personID)
        {
            using (var context1 = new DataBaseContext())
            {
                var guilherme = context1.People.First(p => p.ID == personID);
                Console.WriteLine("Nome: " + guilherme.Name);

                using (var context2 = new DataBaseContext())
                {
                    context2.People.First(p => p.ID == personID).Name = "Luiz Guilherme";
                    context2.SaveChanges();
                }

                using (var context3 = new DataBaseContext())
                {
                    var personUpdate = context3.People.First(p => p.ID == personID);
                    Console.WriteLine("Nome da entidade atualizada: " + personUpdate.Name);
                }

                //não busca dados no caching do EntityFramework
                var personContext1 = context1.People.Where(p => p.ID == personID).AsNoTracking().First();
                Console.WriteLine("Nome da pessoa do contexto 1: " + personContext1.Name);
            }
        }
        #endregion AsNoTracking

    }
}
