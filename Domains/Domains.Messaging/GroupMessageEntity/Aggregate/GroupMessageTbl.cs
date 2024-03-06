using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMessageEntity.ValueObjects;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains.Messaging.GroupMessageEntity.Aggregate;

[Table("GroupMessages")]
public partial record class GroupMessageTbl : Entity
{
    //ids
    [Key]
    public GroupMessageId Id { get; init; }
    public EntityId AppUserId { get; set; }
    public GroupId GroupId { get; set; }

    // message info
    public string Message { get; set; }
    public bool FirstChecked { get; set; }
    public bool LastChecked { get; set; }
    public string? FileUrl { get; set; }


    public byte[] Timestamp { get; set; }

    // Relationships
    public AppUser AppUser { get; set; }
    public GroupTbl Group { get; set; }



}
