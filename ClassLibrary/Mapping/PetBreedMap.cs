using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class PetBreedMap : EntityTypeConfiguration<PetBreed>
    {
        public PetBreedMap()
        {
            this.HasKey(t => t.Id);

            this.Property(r => r.Name).IsRequired();

            this.ToTable("PetBreeds");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.TypeId).HasColumnName("TypeId");

            this.HasRequired(p => p.Type).WithMany(t => t.PetBreeds).HasForeignKey(p => p.TypeId).WillCascadeOnDelete(false);
        }
    }
}
