using ClassLibrary;

namespace TestRest.Services
{
    public class TransformService
    {
        public static Pet TransformPet(Pet x)
        {
            var result = new Pet
            {
                Id = x.Id,
                Name = x.Name,
                Gender = x.Gender,
                BirthDate = x.BirthDate,
                BreedId = x.BreedId,
                MasterId = x.MasterId,
                ChipId = x.ChipId,
                Weight = x.Weight,
                ImageUrl = x.ImageUrl,
                Special = x.Special,

                Breed = new PetBreed
                {
                    Id = x.Breed.Id,
                    Name = x.Breed.Name,
                    TypeId = x.Breed.TypeId,
                    Type = new PetType
                    {
                        Id = x.Breed.Type.Id,
                        Name = x.Breed.Type.Name
                    }
                },
                Master = new User
                {
                    Id = x.Master.Id,
                    Surname = x.Master.Surname,
                    Name = x.Master.Name,
                    Patronymic = x.Master.Patronymic ?? "",
                    Address = x.Master.Address,
                    Contacts = x.Master.Contacts
                }
            };

            foreach (var vac in x.VaccinationsForPet)
            {
                result.VaccinationsForPet.Add(new PetVaccination
                {
                    Id = vac.Id,
                    PetId = vac.PetId,
                    VaccinationId = vac.VaccinationId,
                    Date = vac.Date,
                    State = vac.State,
                    Vaccination = new Vaccination
                    {
                        Id = vac.Vaccination.Id,
                        Name = vac.Vaccination.Name
                    }
                });
            }

            return result;
        }

        public static ServiceForPet TransformServiceForPet(ServiceForPet sfp)
        {
            var result = new ServiceForPet
            {
                Id = sfp.Id,
                Date = sfp.Date,
                PetId = sfp.PetId,
                ServiceId = sfp.ServiceId,

                Pet = new Pet
                {
                    Id = sfp.Pet.Id,
                    Name = sfp.Pet.Name
                },

                Service = new Service
                {
                    Id = sfp.Service.Id,
                    Name = sfp.Service.Name
                }
            };

            return result;
        }
    }
}