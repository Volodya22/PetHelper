using System.Data.Entity;
using System.Threading.Tasks;
using ClassLibrary;

namespace DataContext
{
    public interface IRepository
    {
        DbSet<User> Users { get; set; }
        DbSet<Pet> Pets { get; set; }
        DbSet<PetType> PetTypes { get; set; }
        DbSet<PetBreed> PetBreeds { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<Vaccination> Vaccinations { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<PetVaccination> PetsVaccinations { get; set; }
        DbSet<ServiceForPet> ServicesForPets { get; set; }
        
        Task<int> SaveChangesAsync();
    }
}
