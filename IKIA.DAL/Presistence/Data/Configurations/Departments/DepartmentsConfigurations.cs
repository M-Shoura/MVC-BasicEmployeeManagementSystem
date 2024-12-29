using IKIA.DAL.Models.Departments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IKIA.DAL.Presistence.Data.Configurations.Departments
{
    internal class DepartmentsConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(d => d.Code).HasColumnType("varchar(20)").IsRequired();
            builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()");        
            builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()"); 

            builder.HasMany(d=>d.Employees)
                .WithOne(e=>e.Department)
                .HasForeignKey(e=>e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
