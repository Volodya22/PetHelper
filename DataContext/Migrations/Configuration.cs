using System.Collections.Generic;
using System.Security.Cryptography;
using ClassLibrary;

namespace DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PetHelperContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PetHelperContext context)
        {
            context.Roles.AddOrUpdate(r => r.Id,
                new Role { Id = 1, Name = "Admin"},
                new Role { Id = 2, Name = "Doctor"},
                new Role { Id = 3, Name = "User"}
            );

            string hashAdmin, hashDoc, hashUser;
            using (var md5 = MD5.Create())
            {
                hashAdmin = CryptService.GetMd5Hash(md5, "admin");
                hashDoc = CryptService.GetMd5Hash(md5, "doctor");
                hashUser = CryptService.GetMd5Hash(md5, "user");
            }

            context.Users.AddOrUpdate(u => u.Id,
                new User
                {
                    Id = new Guid("548ec074-18cb-436f-b15d-5f4f64e413fa"),
                    Email = "admin@admin.com",
                    Password = hashAdmin,
                    Surname = "admin",
                    Name = "admin",
                    Gender = true,
                    BirthDate = new DateTime(2010, 10, 10),
                    Pets = new List<Pet>(),
                    Roles = new List<Role>
                    {
                        context.Roles.Find(1)
                    }
                },
                new User
                {
                    Id = new Guid("aa2aa994-ef35-4f0a-81d5-f1b225a9a911"),
                    Email = "ivanov@doctor.com",
                    Password = hashDoc,
                    Surname = "������",
                    Name = "����",
                    Patronymic = "��������",
                    Gender = true,
                    Education = "������",
                    Specialization = "������",
                    BirthDate = new DateTime(1965, 10, 10),
                    Pets = new List<Pet>(),
                    Roles = new List<Role>
                    {
                        context.Roles.Find(2)
                    }
                },
                new User
                {
                    Id = new Guid("4e6ce13d-a158-4f60-acec-0bb53cf9905c"),
                    Email = "petrov@user.com",
                    Password = hashUser,
                    Surname = "������",
                    Name = "����",
                    Patronymic = "����������",
                    Gender = true,
                    BirthDate = new DateTime(1990, 10, 10),
                    Pets = new List<Pet>(),
                    Roles = new List<Role>
                    {
                        context.Roles.Find(3)
                    }
                }
            );

            context.PetTypes.AddOrUpdate(t => t.Id,
                new PetType
                {
                    Id = 1,
                    Name = "������",
                    PetBreeds = new List<PetBreed>()
                },
                new PetType
                {
                    Id = 2,
                    Name = "���",
                    PetBreeds = new List<PetBreed>()
                },
                new PetType
                {
                    Id = 3,
                    Name = "�������",
                    PetBreeds = new List<PetBreed>()
                }
            );

            context.PetBreeds.AddOrUpdate(b => b.Id,
                new PetBreed
                {
                    Id = 1,
                    Name = "�������� �������",
                    TypeId = 1,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 2,
                    Name = "��������",
                    TypeId = 1,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 3,
                    Name = "��������",
                    TypeId = 1,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 4,
                    Name = "�����",
                    TypeId = 1,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 5,
                    Name = "��������",
                    TypeId = 2,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 6,
                    Name = "������",
                    TypeId = 2,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 7,
                    Name = "���������",
                    TypeId = 3,
                    Pets = new List<Pet>()
                },
                new PetBreed
                {
                    Id = 8,
                    Name = "���",
                    TypeId = 3,
                    Pets = new List<Pet>()
                }
            );

            context.Pets.AddOrUpdate(p => p.Id,
                new Pet
                {
                    Id = new Guid("59013158-72ce-4ab8-b405-f31bc17e24a6"),
                    Name = "ϸ��",
                    BirthDate = new DateTime(2015, 12, 12),
                    Gender = true,
                    MasterId = new Guid("4e6ce13d-a158-4f60-acec-0bb53cf9905c"),
                    BreedId = 1,
                    Weight = 3,
                    ServicesForPet = new List<ServiceForPet>(),
                    VaccinationsForPet = new List<PetVaccination>()
                }
            );

            context.Services.AddOrUpdate(s => s.Id,
                new Service
                {
                    Id = 1,
                    Name = "�������",
                    Price = 1000,
                    Description = "������� ����� ����� ������ � ��������!",
                    Pets = new List<ServiceForPet>()
                },
                new Service
                {
                    Id = 2,
                    Name = "������ �����",
                    Price = 300,
                    Description = "�������� ���, ��� �������� ����� ��� �����!",
                    Pets = new List<ServiceForPet>()
                },
                new Service
                {
                    Id = 3,
                    Name = "������� ������",
                    Price = 500,
                    Description = "��� ������� ������ �� �������� ������!",
                    Pets = new List<ServiceForPet>()
                },
                new Service
                {
                    Id = 4,
                    Name = "�����������",
                    Price = 5000,
                    Description = "�� ������ ������� ������ �������!",
                    Pets = new List<ServiceForPet>()
                }
            );

            context.Vaccinations.AddOrUpdate(v => v.Id,
                new Vaccination
                {
                    Id = 1,
                    Name = "�������� �� ���������",
                    PetTypes = new List<PetType>
                    {
                        context.PetTypes.Find(1),
                        context.PetTypes.Find(2)
                    },
                    Pets = new List<PetVaccination>()
                },
                new Vaccination
                {
                    Id = 2,
                    Name = "�������� �� ������� �����",
                    PetTypes = new List<PetType>
                    {
                        context.PetTypes.Find(1)
                    },
                    Pets = new List<PetVaccination>()
                },
                new Vaccination
                {
                    Id = 3,
                    Name = "�������� �� ������������� �����������",
                    PetTypes = new List<PetType>
                    {
                        context.PetTypes.Find(2)
                    },
                    Pets = new List<PetVaccination>()
                },
                new Vaccination
                {
                    Id = 4,
                    Name = "�������� �� �������� ������",
                    PetTypes = new List<PetType>
                    {
                        context.PetTypes.Find(1),
                        context.PetTypes.Find(2),
                        context.PetTypes.Find(3)
                    },
                    Pets = new List<PetVaccination>()
                }
            );
        }
    }
}
