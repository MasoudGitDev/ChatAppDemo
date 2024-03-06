using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupMessageEntity.Aggregate;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Extensions;

namespace Infra.EFCore.Messaging.Configs;
public class MessagingConfigs : 
    IEntityTypeConfiguration<GroupTbl> ,
    IEntityTypeConfiguration<GroupMemberTbl> , 
    IEntityTypeConfiguration<GroupRequestTbl> , 
    IEntityTypeConfiguration<GroupMessageTbl>{
    public void Configure(EntityTypeBuilder<GroupTbl> builder) {
        builder.HasIndex(p => p.GroupId).IsUnique();
        builder.HasIndex(p => p.DisplayId).IsUnique();

        builder.Property(p => p.GroupId).IsRequired().HasConversion(x=> x.Value , r=>new(r));
        builder.Property(p => p.DisplayId).IsRequired().HasConversion(x => x.Value , r => new(r));        
        builder.Property(x => x.CreatorId).IsRequired().HasConversion(x => x.Value , r => new(r));          
        builder.Property(x => x.Categories).HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<string>>());
        builder.Property(p => p.LogoURLs).HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<Logo>>());
        builder.Property(x => x.MessageLocking).HasConversion(x=>x.ToJson() , r => r.FromJsonTo<MessageLocking>());

        builder.Property(p=>p.Timestamp).IsRequired().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();
        builder.Property(p => p.CreatedAt).IsRequired();

        builder.HasOne(x => x.Creator).WithMany().HasForeignKey(x => x.CreatorId).OnDelete(DeleteBehavior.ClientNoAction);
    }

    public void Configure(EntityTypeBuilder<GroupMemberTbl> builder) {       
        builder.HasIndex(x=> x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.GroupId).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.MemberId).IsRequired().HasConversion(x => x.Value, r => new(r));
        builder.Property(x=>x.AdminInfo).HasConversion(x=> x.ToJson()  , r => r.FromJsonTo<AdminMemberInfo>());
        builder.Property(x => x.BlockMemberInfo).HasConversion(x => x.ToJson() , r => r.FromJsonTo<BlockedMemberInfo>());

        builder.HasOne(x=>x.Member).WithMany().HasForeignKey(x=>x.MemberId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(x => x.Group).WithMany(x=>x.Members).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.ClientCascade);
    }

    public void Configure(EntityTypeBuilder<GroupRequestTbl> builder) {        
        builder.HasIndex(x=>x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.GroupId).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x=>x.RequesterId).IsRequired().HasConversion(x => x.Value , r => new(r));
        builder.Property(x => x.Visibility).IsRequired().HasConversion(x => x.ToJson() , r => r.FromJsonTo<RequestVisibility>());
        builder.HasOne(x=>x.Group).WithMany(x=>x.Requests).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(x=>x.Requester).WithMany().HasForeignKey(x=>x.RequesterId).OnDelete(DeleteBehavior.ClientCascade);
    }

    public void Configure(EntityTypeBuilder<GroupMessageTbl> builder) {
        builder.HasIndex(x=>x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(x => x.Value , r => new());
        builder.Property(x=> x.AppUserId).IsRequired().HasConversion(x=>x.Value , r => new(r));
        builder.Property(x => x.GroupId).IsRequired().HasConversion(x => x.Value , r => new(r));        

        builder.Property(x=>x.Timestamp).IsRequired().IsRowVersion().ValueGeneratedOnAddOrUpdate();
        builder.HasOne(x=>x.AppUser).WithMany().HasForeignKey(x=>x.AppUserId).OnDelete(DeleteBehavior.ClientNoAction);
        builder.HasOne(x => x.Group).WithMany(x=>x.Messages).HasForeignKey(x => x.GroupId).OnDelete(DeleteBehavior.ClientCascade);
    }
}

