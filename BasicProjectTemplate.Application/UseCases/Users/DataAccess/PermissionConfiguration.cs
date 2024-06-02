using BasicProjectTemplate.Domain.Features.Authentication.RoleModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasicProjectTemplate.Application.UseCases.Users.DataAccess;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder
            .ToTable("permissions")
            .HasKey(p => p.Id);
        builder.HasIndex(p => p.Name).IsUnique();
    }
}