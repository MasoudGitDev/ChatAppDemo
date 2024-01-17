using Domains.Auth.RoleEntity;
using Domains.Auth.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EFCore.Auth.Configs.Auth;
public class AuthConfigs : IEntityTypeConfiguration<AppUser>, IEntityTypeConfiguration<AppRole> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Id).IsRequired().HasConversion(x => x.Value , guid => new(guid));
        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
    }

    public void Configure(EntityTypeBuilder<AppRole> builder) {
        builder.HasIndex(x => x.Id);
        builder.Property(x => x.Id).IsRequired().HasConversion(entityId => entityId.Value , guid => new(guid));
    }
}
