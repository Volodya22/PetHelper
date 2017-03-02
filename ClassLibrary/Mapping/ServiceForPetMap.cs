using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class ServiceForPetMap : EntityTypeConfiguration<ServiceForPet>
    {
        public ServiceForPetMap()
        {
            this.HasKey(t => t.Id);

            this.ToTable("ServicesForPets");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PetId).HasColumnName("PetId");
            this.Property(t => t.ServiceId).HasColumnName("ServiceId");
            this.Property(t => t.Date).HasColumnName("Date");

            this.HasRequired(p => p.Pet).WithMany(t => t.ServicesForPet).HasForeignKey(p => p.PetId).WillCascadeOnDelete(false);
            this.HasRequired(p => p.Service).WithMany(t => t.Pets).HasForeignKey(p => p.ServiceId).WillCascadeOnDelete(false);
        }
    }
}
