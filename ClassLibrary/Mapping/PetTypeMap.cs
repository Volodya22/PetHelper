using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class PetTypeMap : EntityTypeConfiguration<PetType>
    {
        public PetTypeMap()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Name).IsRequired();

            this.ToTable("PetTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
