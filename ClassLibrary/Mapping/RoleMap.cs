using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.HasKey(r => r.Id);

            this.Property(r => r.Name).IsRequired();

            this.ToTable("Roles");
            this.Property(r => r.Id).HasColumnName("Id");
            this.Property(r => r.Name).HasColumnName("Name");

            this.HasMany(t => t.Users)
                .WithMany(t => t.Roles)
                .Map(t => t.MapLeftKey("RoleId").MapRightKey("UserId").ToTable("UserRoles"));
        }
    }
}
