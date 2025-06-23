using ElectricalLearning.Repositories.Constants;
using ElectricalLearning.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricalLearning.Repositories.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(TableNames.Accounts);

            List<Account> accounts = new List<Account>()
            {
                 new Account { Id = 1, FullName = "Nguyễn Văn A", Email = "teacher@gmail.com", PasswordHash= BCrypt.Net.BCrypt.HashPassword("teacher123"), Role = "Teacher", CreatedAt = DateTimeOffset.UtcNow},
                new Account { Id = 2, FullName = "Trần Thị B", Email = "student@gmail.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("student123"), Role = "Student", CreatedAt = DateTimeOffset.UtcNow },
                new Account { Id = 3, FullName = "Lê Văn C", Email = "admin@gmail.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("adminr123"), Role = "Admin", CreatedAt = DateTimeOffset.UtcNow }
            };

            builder.HasData(accounts);

           // builder.Property(a => a.FullName)
           //     .IsRequired()
           //     .HasMaxLength(100);

           // builder.Property(a => a.Email)
           //     .IsRequired()
           //     .HasMaxLength(100);

           // builder.Property(a => a.PasswordHash)
           //     .IsRequired()
           //     .HasMaxLength(256);

           // builder.Property(a => a.Role)
           //     .IsRequired()
           //     .HasMaxLength(20);

           // builder.Property(a => a.CreatedAt)
           //     .IsRequired();

           // builder.Property(a => a.UpdatedAt)
           //     .IsRequired();
        }
    }
}
