using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.Shared.ValueObjects;

namespace Domains.Messaging.GroupEntity.Entity;
public partial record class GroupTbl
{

    public static GroupTbl Create(GroupModel model , GroupId? groupId = null) => new() {
        GroupId = groupId ?? GroupId.Create() ,
        DisplayId = new DisplayId(model.DisplayId) ,
        CreatorId = model.CreatorId ,
        CreatedAt = DateTime.UtcNow ,
        Title = model.Title ,
        Description = model.Description ,
        IsRequestable = model.IsRequestable ,
        Categories = model.Categories ,
        LogoURLs = model.LogoURLs ,
        Members = new() ,
        Requests = new() ,
        MessageLocking = new() ,
    };

    public GroupTbl AddLogo(Logo logo) {
        if(LogoURLs == null) {
            LogoURLs = new();
        }
        LogoURLs.AddLast(logo);
        return this;
    }
    public GroupTbl RemoveLogoURLs() {
        LogoURLs = null;
        return this;
    }
    public void ChangeRequestableState(bool isRequestable) {
        IsRequestable = isRequestable;
    }
    public GroupTbl ChangeDisplayId(DisplayId displayId) {
        DisplayId = displayId;
        return this;
    }
    public GroupTbl ChangeInfo(string title , string description) {
        Description = description;
        Title = title;
        return this;
    }
    public GroupTbl ChangeCategories(LinkedList<string> categories) {
        Categories = categories;
        return this;
    }


    public void ChangeMessageLockingTo(bool isLock , DateTime? startAt , DateTime? endAt) {
        MessageLocking.IsLock = isLock;
        MessageLocking.StartAt = startAt;
        MessageLocking.EndAt = endAt;
    }


}
