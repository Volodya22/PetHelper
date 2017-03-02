using System.Data.Entity;
using ClassLibrary.Mapping;

namespace DataContext
{
    public class PetHelperContext : DbContext, IRepository
    {
        static PetHelperContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PetHelperContext, Migrations.Configuration>());
        }

        public PetHelperContext() : base("name=PetHelperContext")
        {
        }

        public DbSet<ClassLibrary.User> Users { get; set; } 
        public DbSet<ClassLibrary.Role> Roles { get; set; } 
        public DbSet<ClassLibrary.Pet> Pets { get; set; }
        public DbSet<ClassLibrary.PetType> PetTypes { get; set; }
        public DbSet<ClassLibrary.PetBreed> PetBreeds { get; set; }
        public DbSet<ClassLibrary.Service> Services { get; set; }
        public DbSet<ClassLibrary.Vaccination> Vaccinations { get; set; }
        public DbSet<ClassLibrary.ServiceForPet> ServicesForPets { get; set; }
        public DbSet<ClassLibrary.PetVaccination> PetsVaccinations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new PetMap());
            modelBuilder.Configurations.Add(new PetTypeMap());
            modelBuilder.Configurations.Add(new PetBreedMap());
            modelBuilder.Configurations.Add(new ServiceMap());
            modelBuilder.Configurations.Add(new VaccinationMap());
            modelBuilder.Configurations.Add(new ServiceForPetMap());
            modelBuilder.Configurations.Add(new PetVaccinationMap());
        }
    }
}
