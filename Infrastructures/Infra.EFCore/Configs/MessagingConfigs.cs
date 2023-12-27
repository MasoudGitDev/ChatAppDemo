using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Extensions;

namespace Infra.EFCore.Messaging.Configs;
public class MessagingConfigs : IEntityTypeConfiguration<GroupTbl> {
    public void Configure(EntityTypeBuilder<GroupTbl> builder) {
        builder.HasIndex(p => p.GroupId).IsUnique();
        builder.Property(p => p.GroupId).IsRequired().HasConversion(x => x.Value , r => new(r));        

        builder.Property(p => p.DisplayId).IsRequired();       
        builder.HasIndex(p => p.DisplayId).IsUnique();

        builder.Property(p=>p.CreatedAt).IsRequired();
        builder.Property(p=>p.Timestamp).IsRequired().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();

        builder.Property(p => p.Logos).HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<Logo>>() ?? new());
        builder.Property(p=> p.Admins).IsRequired().HasConversion(x=>x.ToJson() , r=> r.FromJsonTo<LinkedList<Admin>>() ?? new()  );
        builder.Property(p => p.Members).IsRequired().HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<Member>>() ?? new());

        builder.HasOne(x => x.Creator).WithMany().HasForeignKey(x=>x.CreatorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
