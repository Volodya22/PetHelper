using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class PetVaccinationMap : EntityTypeConfiguration<PetVaccination>
    {
        public PetVaccinationMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("PetVaccinations");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PetId).HasColumnName("PetId");
            this.Property(t => t.VaccinationId).HasColumnName("VaccinationId");
            this.Property(t => t.Date).HasColumnName("Date");

            this.HasRequired(p => p.Pet).WithMany(t => t.VaccinationsForPet).HasForeignKey(p => p.PetId).WillCascadeOnDelete(false);
            this.HasRequired(p => p.Vaccination).WithMany(t => t.Pets).HasForeignKey(p => p.VaccinationId).WillCascadeOnDelete(false);
        }
    }
}
