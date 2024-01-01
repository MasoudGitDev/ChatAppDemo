using Domains.Auth.RoleEntity;
using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Infra.EFCore.Auth.Configs.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;

namespace Infra.EFCore.Contexts;
internal class AppDbContext : IdentityDbContext<AppUser , AppRole,EntityId> {
    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AuthConfigs).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<GroupTbl> Groups { get; set; }
    public DbSet<GroupMemberTbl> GroupMembers { get; set; }
    public DbSet<GroupRequestTbl> GroupRequests { get; set; }
}
