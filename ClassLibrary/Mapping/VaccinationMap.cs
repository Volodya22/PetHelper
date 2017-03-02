using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class VaccinationMap : EntityTypeConfiguration<Vaccination>
    {
        public VaccinationMap()
        {
            this.HasKey(t => t.Id);

            this.Property(r => r.Name).IsRequired();

            this.ToTable("Vaccinations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");

            this.HasMany(t => t.PetTypes)
                .WithMany(v => v.Vaccinations)
                .Map(t => t.ToTable("PetTypeVaccinations").MapLeftKey("TypeId").MapRightKey("VaccinationId"));
        }
    }
}
