using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Email).IsRequired();
            this.Property(u => u.Password).IsRequired();
            this.Property(u => u.Surname).IsRequired();
            this.Property(u => u.Name).IsRequired();
            this.Property(u => u.Gender).IsRequired();
            this.Property(u => u.BirthDate).IsRequired();

            this.ToTable("Users");
            this.Property(u => u.Id).HasColumnName("Id");
            this.Property(u => u.Email).HasColumnName("Email");
            this.Property(u => u.Password).HasColumnName("Password");
            this.Property(u => u.Surname).HasColumnName("Surname");
            this.Property(u => u.Name).HasColumnName("Name");
            this.Property(u => u.Patronymic).HasColumnName("Patronymic");
            this.Property(u => u.Gender).HasColumnName("Gender");
            this.Property(u => u.BirthDate).HasColumnName("BirthDate");
        }
    }
}
