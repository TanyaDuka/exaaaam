using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace _4
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
    }
    public class Doctor : Employee
    {
        public string Specialization { get; set; }
    }
    public class Engineer : Employee
    {
        public string FavoriteVideogame { get; set; }
    }
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }
    public class Context : DbContext
    {
        public Context()
            : base("DbConnection")
        { }
        public DbSet<City> Cities { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Engineer> Engineers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Doctor>().Property(d => d.Specialization).IsRequired();
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(128);
        }
    }

    class Program
    {
        public static IEnumerable<string> GetSpecialization(int cityId)
        {
            using (var ctx = new Context())
            {
                return ctx.Doctors.Where(d => d.CityId == cityId).Select(d => d.Specialization).ToList();
            }
        }
        static void Main(string[] args)
        {
            //var ctx = new Context();
            //ctx.Cities.Add(new City { Name = "Kiev", Longitude = 1, Latitude = 2 });
            //ctx.Cities.Add(new City { Name = "Kiev2", Longitude = 1, Latitude = 2 });
            //ctx.Doctors.Add(new Doctor { Name = "Name1", CityId = 1, Specialization = "spec1" });
            //ctx.Doctors.Add(new Doctor { Name = "Name2", CityId = 1, Specialization = "spec2" });
            //ctx.Doctors.Add(new Doctor { Name = "Name3", CityId = 1, Specialization = "spec3" });
            //ctx.Doctors.Add(new Doctor { Name = "Name4", CityId = 2, Specialization = "spec4" });
            //ctx.Engineers.Add(new Engineer { Name = "Name5", CityId = 1, FavoriteVideogame = "game1" });
            //ctx.SaveChanges();
            foreach (var item in GetSpecialization(1))
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
