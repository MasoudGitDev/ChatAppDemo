using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Infra.EFCore.Messaging.Configs;
public class MessagingConfigs : 
    IEntityTypeConfiguration<GroupTbl> ,
    IEntityTypeConfiguration<GroupMemberTbl> , 
    IEntityTypeConfiguration<GroupRequestTbl> {
    public void Configure(EntityTypeBuilder<GroupTbl> builder) {
        builder.HasIndex(p => p.GroupId).IsUnique();
        builder.HasIndex(p => p.DisplayId).IsUnique();

        builder.Property(p => p.GroupId).IsRequired().HasConversion(x=> x.Value , r=>new(r));
        builder.Property(p => p.DisplayId).IsRequired().HasConversion(x => x.Value , r => new(r));        
        builder.Property(x => x.CreatorId).IsRequired().HasConversion(x => x.Value , r => new(r,"AspNetUsers"));          
        builder.Property(x => x.Categories).HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<string>>());
        builder.Property(p => p.Logos).HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<Logo>>());

        builder.Property(p=>p.Timestamp).IsRequired().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
        builder.Property(p => p.CreatedAt).IsRequired();

        builder.HasOne(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.ClientNoAction);
    }

    public void Configure(EntityTypeBuilder<GroupMemberTbl> builder) {       
        builder.HasIndex(x=> x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.GroupId).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.MemberId).IsRequired().HasConversion(x => x.Value, r => new(r , "AspNetUsers"));
        builder.Property(x=>x.AdminInfo).HasConversion(x=> x.ToJson()  , r => r.FromJsonTo<AdminMemberInfo>());
        builder.Property(x => x.BlockMemberInfo).HasConversion(x => x.ToJson() , r => r.FromJsonTo<BlockedMemberInfo>());

        builder.HasOne(x=>x.Member).WithMany().HasForeignKey(x=>x.MemberId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(x => x.Group).WithMany(x=>x.Members).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.ClientCascade);
    }

    public void Configure(EntityTypeBuilder<GroupRequestTbl> builder) {        
        builder.HasIndex(x=>x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.GroupId).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.RequesterId).IsRequired().HasConversion(x => x.Value , r => new(r , "AspNetUsers"));

        builder.HasOne(x=>x.Group).WithMany(x=>x.Requests).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(x=>x.Requester).WithMany().HasForeignKey(x=>x.RequesterId).OnDelete(DeleteBehavior.ClientCascade);
    }
}

