using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.Extensions;
using Shared.ValueObjects;

namespace Infra.EFCore.Messaging.Configs;
public class MessagingConfigs : 
    IEntityTypeConfiguration<GroupTbl> ,
    IEntityTypeConfiguration<GroupMemberTbl> , 
    IEntityTypeConfiguration<GroupRequestTbl> {
    public void Configure(EntityTypeBuilder<GroupTbl> builder) {
        builder.HasIndex(p => p.GroupId).IsUnique();
        builder.Property(p => p.GroupId).IsRequired().HasConversion(UseIdConvertor(nameof(GroupTbl)));        

        builder.Property(p => p.DisplayId).IsRequired();       
        builder.HasIndex(p => p.DisplayId).IsUnique();

        builder.Property(x=>x.CreatorId).IsRequired().HasConversion(UseIdConvertor(nameof(GroupTbl)));
        builder.Property(p=>p.CreatedAt).IsRequired();
        builder.Property(p=>p.Timestamp).IsRequired().IsConcurrencyToken().ValueGeneratedOnAddOrUpdate();

        builder.Property(p => p.Logos).HasConversion(x => x.ToJson() , r => r.FromJsonTo<LinkedList<Logo>>() ?? new());
        builder.HasOne(x => x.Creator).WithMany().HasForeignKey(x=>x.CreatorId).OnDelete(DeleteBehavior.ClientNoAction);
    }

    public void Configure(EntityTypeBuilder<GroupMemberTbl> builder) {       
        builder.HasIndex(x=> x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(UseIdConvertor(nameof(GroupMemberTbl)));
        builder.Property(x=>x.GroupId).IsRequired().HasConversion(UseIdConvertor(nameof(GroupMemberTbl)));
        builder.Property(x=>x.MemberId).IsRequired().HasConversion(userId => userId.Value , r => new(r, "AppUserGroupTbl"));
        builder.Property(x=>x.AdminInfo).HasConversion(x=> x == null ? null : x.ToJson()  , r => r == null ? new() : r.FromJsonTo<AdminMemberInfo>());
        builder.Property(x => x.BlockMemberInfo).HasConversion(x => x == null ? null : x.ToJson() , r => r == null ? new() : r.FromJsonTo<BlockMemberInfo>());

        builder.HasOne(x=>x.Member).WithMany().HasForeignKey(x=>x.MemberId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(x => x.Group).WithMany(x=>x.Members).HasForeignKey(x => x.MemberId).OnDelete(DeleteBehavior.ClientCascade);
    }

    public void Configure(EntityTypeBuilder<GroupRequestTbl> builder) {        
        builder.HasIndex(x=>x.Id).IsUnique();
        builder.Property(x => x.Id).IsRequired().HasConversion(UseIdConvertor());
        builder.Property(x=>x.GroupId).IsRequired().HasConversion(UseIdConvertor());
        builder.Property(x=>x.RequesterId).IsRequired().HasConversion(UseIdConvertor());

        builder.HasOne(x=>x.Group).WithMany(x=>x.Requesters).HasForeignKey(x=>x.GroupId).OnDelete(DeleteBehavior.ClientCascade);
        builder.HasOne(x=>x.Requester).WithMany().HasForeignKey(x=>x.RequesterId).OnDelete(DeleteBehavior.ClientCascade);
    }

    private ValueConverter<EntityId, Guid> UseIdConvertor(string entityName = "Entity") 
        => new ValueConverter<EntityId , Guid>(entityId => entityId.Value , guid => new(guid , entityName));
    
}
