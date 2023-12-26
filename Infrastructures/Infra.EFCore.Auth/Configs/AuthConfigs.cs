using Domains.Auth.RoleEntity;
using Domains.Auth.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EFCore.Auth.Configs.Auth;
public class AuthConfigs : IEntityTypeConfiguration<AppUser>, IEntityTypeConfiguration<AppRole> {
    public void Configure(EntityTypeBuilder<AppUser> builder) {
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.UserName).IsRequired();
        builder.Property(x => x.Email).IsRequired();
    }

    public void Configure(EntityTypeBuilder<AppRole> builder) {

    }
}
