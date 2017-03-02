using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class ServiceMap : EntityTypeConfiguration<Service>
    {
        public ServiceMap()
        {
            this.HasKey(s => s.Id);

            this.Property(r => r.Name).IsRequired();
            this.Property(r => r.Price).IsRequired();
            this.Property(r => r.Description).IsRequired();

            this.ToTable("Services");
            this.Property(s => s.Id).HasColumnName("Id");
            this.Property(s => s.Name).HasColumnName("Name");
            this.Property(s => s.Price).HasColumnName("Price");
            this.Property(s => s.Description).HasColumnName("Description");
        }
    }
}
