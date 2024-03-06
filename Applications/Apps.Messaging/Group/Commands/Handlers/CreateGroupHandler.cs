using Apps.Messaging.Group.Commands.Models;
using Apps.Messaging.Shared.Manager;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Extensions;
using Shared.Models;

namespace Apps.Messaging.Group.Commands.Handlers;
internal sealed class CreateGroupHandler(IGroupMessagingUOW _unitOfWork)
    : GroupManager<CreateGroupModel , Result>(_unitOfWork.ThrowIfNull()) {
    public override async Task<Result> Handle(CreateGroupModel request , CancellationToken cancellationToken) {
        var findGroup = await GetGroupByDisplayIdAsync(request.DisplayId);
        findGroup.ThrowIfFound("Your group must have a unique <displayId>.");

        (GroupId groupId, AppUserId creatorId) = (GroupId.Create(), request.CreatorId);

        await CreateGroupAsync(groupId , creatorId , request);
        CreateOwner(groupId , creatorId);

        await SaveChangesAsync();

        return new Result(ResultStatus.Success , new("Created" ,
            $"The Group with Display-Id : <{request.DisplayId}> has been created successfully."));
    }

    // ============== private methods

    // #solve new GroupModel() & Categories & LogoURLs
    private async Task CreateGroupAsync(
        GroupId groupId ,
        AppUserId creatorId ,
        CreateGroupModel model
        ) {
        await _unitOfWork.CreateAsync(GroupTbl.Create(
            new GroupModel() {
                CreatorId = creatorId ,
                Description = model.Description ,
                DisplayId = model.DisplayId ,
                IsRequestable = model.IsRequestable.GetValueOrDefault() ,
                Title = model.Title ,
                Categories = model.Categories.ToLinkedList() ,
                LogoURLs = new() ,
            } ,
            groupId
        ));
    }

    private async void CreateOwner(GroupId groupId , AppUserId appUserId) {
        await _unitOfWork.CreateAsync(GroupMemberTbl.Create(
            groupId , appUserId , AdminLevel.Owner , "Owner-by-system"));
    }

}
