using Domains.Auth.RoleEntity;
using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Infra.EFCore.Auth.Configs.Auth;
using Infra.EFCore.Messaging.Configs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.EFCore.Contexts;
internal class AppDbContext : IdentityDbContext<AppUser , AppRole , string> {
    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthConfigs).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(MessagingConfigs).Assembly);
    }

    public DbSet<GroupTbl> GroupTbl { get; set; }
}
