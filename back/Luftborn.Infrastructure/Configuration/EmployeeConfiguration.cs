using Luftborn.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luftborn.Infrastructure.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(c => c.FirstName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.LastName).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Salary).IsRequired();
            builder.Property(c => c.EmailAddress).IsRequired().HasMaxLength(200);
            builder.ToTable("Employees");
        }
    }
}
