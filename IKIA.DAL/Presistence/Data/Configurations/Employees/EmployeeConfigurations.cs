using IKIA.DAL.Common.Enums;
using IKIA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKIA.DAL.Presistence.Data.Configurations.Employees
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Address).HasColumnType("varchar(100)");
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)");
            builder.Property(e => e.CreatedOn).HasDefaultValueSql("GETDATE()");      

            builder.Property(e => e.Gender)
                .HasConversion
                (
                    (gender) => gender.ToString(),                              
                    (gender) => (Gender)Enum.Parse(typeof(Gender), gender)     
                );

            builder.Property(e => e.EmployeeType)
                .HasConversion
                (
                    (emp) => emp.ToString(),
                    (emp) => (EmployeeType)Enum.Parse(typeof(EmployeeType), emp)
                );
        }
    }
}
