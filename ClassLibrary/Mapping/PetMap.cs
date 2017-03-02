using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ClassLibrary.Mapping
{
    public class PetMap : EntityTypeConfiguration<Pet>
    {
        public PetMap()
        {
            this.HasKey(p => p.Id);

            this.Property(t => t.Name).IsRequired().HasMaxLength(30);
            this.Property(r => r.Gender).IsRequired();
            this.Property(r => r.BirthDate).IsRequired();

            this.ToTable("Pets");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.BreedId).HasColumnName("BreedId");
            this.Property(t => t.MasterId).HasColumnName("MasterId");

            this.HasRequired(p => p.Breed).WithMany(t => t.Pets).HasForeignKey(p => p.BreedId).WillCascadeOnDelete(false);
            this.HasRequired(p => p.Master).WithMany(t => t.Pets).HasForeignKey(p => p.MasterId).WillCascadeOnDelete(false);
        }
    }
}
