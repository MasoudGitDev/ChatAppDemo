using Domains.Auth.UserEntity;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;
using Shared.Exceptions;
using Shared.Generics;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Generics = Shared.Generics;
using VisibilityType = Shared.Enums.Visibility;

namespace Domains.Messaging.GroupRequestEntity;
[Table("GroupRequests")]
public record class GroupRequestTbl : Generics.Entity {

    // Ids
    [Key]
    public GroupRequestId Id { get; private set; }
    public EntityId RequesterId { get; private set; }
    public GroupId GroupId { get; private set; }

    // otherProps
    public uint RequestNumbers { get; private set; }
    public string? Description { get; private set; }
    public DateTime RequestedAt { get; private set; }

    public bool IsBlocked { get; private set; }
    public RequestVisibility Visibility { get; private set; } = RequestVisibility.Default;


    [Timestamp]
    public byte[] Timestamp { get; private set; }


    public static GroupRequestTbl Create(GroupId groupId , AppUserId requesterId , string? description) {
        return new() {
            Id = GroupRequestId.Create() ,
            RequesterId = requesterId ,
            GroupId = groupId ,
            RequestNumbers = 1 ,
            Description = description ?? "Hi, I Like to be a member in your group." ,
            IsBlocked = false ,
            RequestedAt = DateTime.UtcNow
        };
    }

    /// <summary>
    /// Request updating is possible after 1 day.
    /// </summary>
    public void Update(string? description = "Please See My Request :(") {
        if(DateTime.UtcNow < RequestedAt.AddDays(1)) {
            throw new NotPossibleException("Request updating is possible after 1 day");
        }
        if(description?.Length < 2) {
            throw new NotPossibleException("your description must have atleast 2 characters.");
        }
        RequestNumbers += 1;
        Description = description;
        Raise(
          new DomainEvent(nameof(Update) ,
          DomainEventType.Modified ,
          "The request has been updated."));
    }

    public void ChangeBlockState(bool isBlocked) {
        IsBlocked = isBlocked;
        var visibilityType =
            isBlocked ? VisibilityType.Hidden : VisibilityType.Visible;
        Visibility.VisibleToRequester(visibilityType);
        Visibility.VisibleToAdmins(visibilityType);
        Raise(
            new DomainEvent(nameof(ChangeBlockState) ,
            DomainEventType.Modified ,
            "The <IsBlocked> state has been changed."));
    }

    // Relation ships
    public AppUser Requester { get; private set; }
    public GroupTbl Group { get; private set; }
}
