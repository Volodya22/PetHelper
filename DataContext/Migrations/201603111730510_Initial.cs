namespace DataContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PetBreeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        TypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PetTypes", t => t.TypeId)
                .Index(t => t.TypeId);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Gender = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        BreedId = c.Int(nullable: false),
                        MasterId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PetBreeds", t => t.BreedId)
                .ForeignKey("dbo.Users", t => t.MasterId)
                .Index(t => t.BreedId)
                .Index(t => t.MasterId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Patronymic = c.String(),
                        Gender = c.Boolean(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicesForPets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PetId = c.Guid(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pets", t => t.PetId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.PetId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetVaccinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PetId = c.Guid(nullable: false),
                        VaccinationId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pets", t => t.PetId)
                .ForeignKey("dbo.Vaccinations", t => t.VaccinationId)
                .Index(t => t.PetId)
                .Index(t => t.VaccinationId);
            
            CreateTable(
                "dbo.Vaccinations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PetTypeVaccinations",
                c => new
                    {
                        TypeId = c.Int(nullable: false),
                        VaccinationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TypeId, t.VaccinationId })
                .ForeignKey("dbo.Vaccinations", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("dbo.PetTypes", t => t.VaccinationId, cascadeDelete: true)
                .Index(t => t.TypeId)
                .Index(t => t.VaccinationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetBreeds", "TypeId", "dbo.PetTypes");
            DropForeignKey("dbo.PetVaccinations", "VaccinationId", "dbo.Vaccinations");
            DropForeignKey("dbo.PetTypeVaccinations", "VaccinationId", "dbo.PetTypes");
            DropForeignKey("dbo.PetTypeVaccinations", "TypeId", "dbo.Vaccinations");
            DropForeignKey("dbo.PetVaccinations", "PetId", "dbo.Pets");
            DropForeignKey("dbo.ServicesForPets", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServicesForPets", "PetId", "dbo.Pets");
            DropForeignKey("dbo.Pets", "MasterId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Pets", "BreedId", "dbo.PetBreeds");
            DropIndex("dbo.PetTypeVaccinations", new[] { "VaccinationId" });
            DropIndex("dbo.PetTypeVaccinations", new[] { "TypeId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.PetVaccinations", new[] { "VaccinationId" });
            DropIndex("dbo.PetVaccinations", new[] { "PetId" });
            DropIndex("dbo.ServicesForPets", new[] { "ServiceId" });
            DropIndex("dbo.ServicesForPets", new[] { "PetId" });
            DropIndex("dbo.Pets", new[] { "MasterId" });
            DropIndex("dbo.Pets", new[] { "BreedId" });
            DropIndex("dbo.PetBreeds", new[] { "TypeId" });
            DropTable("dbo.PetTypeVaccinations");
            DropTable("dbo.UserRoles");
            DropTable("dbo.PetTypes");
            DropTable("dbo.Vaccinations");
            DropTable("dbo.PetVaccinations");
            DropTable("dbo.Services");
            DropTable("dbo.ServicesForPets");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Pets");
            DropTable("dbo.PetBreeds");
        }
    }
}
