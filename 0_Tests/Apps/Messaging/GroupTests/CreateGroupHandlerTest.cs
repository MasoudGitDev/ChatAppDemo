using Apps.Messaging.Groups.Commands.Handlers;
using Apps.Messaging.Groups.Commands.Models;
using AutoFixture;
using Domains.Messaging.GroupEntity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity;
using Domains.Messaging.GroupMemberEntity.Repos;
using Domains.Messaging.GroupMemberEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.Shared.ValueObjects;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;
using Shared.Models;
using Shared.ValueObjects;

namespace Messaging.GroupTests;
public class CreateGroupHandlerTest {


    public readonly Mock<IGroupAdminRepo> _groupAdminRepo;
    private readonly Fixture _fixture;
    private readonly CreateGroupHandler _handler;

    public CreateGroupHandlerTest() {
        _groupAdminRepo = new Mock<IGroupAdminRepo>();
        _fixture = new Fixture();
        _handler = new CreateGroupHandler(_groupAdminRepo.Object);

    }

    [Fact]
    public async void Should_Return_Success_When_Create_Group() {

        //Arrange
        var request = new CreateGroupModel(){
            CreatorId =Guid.Parse("ba6cc063-dbeb-4729-8a8e-668ed6f16675"),
            DisplayId = "GroupMasoud1" ,
            IsRequestable = true,
            Title ="Groud-Masoud",           
            
        };
        _groupAdminRepo.Setup(x => x.General.Queries.GetGroupByDisplayIdAsync(request.DisplayId)).ReturnsAsync(value: null);
        GroupId groupId = new GroupId();
        DateTime startAt = DateTime.UtcNow;
        EntityId appUserId = request.CreatorId;
        var newGroup = new GroupTbl{
            GroupId = groupId,
            DisplayId = new DisplayId(request.DisplayId),
            CreatorId = appUserId,
            CreatedAt=startAt,
            Title = request.Title,
            Description = request.Description,
            IsRequestable = request.IsRequestable,
            Categories = new() ,//
            LogoURLs = new(),        //   
            Members = new(),//
            Requests= new List<GroupRequestTbl>(),
            MessageBlocking = new(),
        };
        _groupAdminRepo.Setup(x => x.General.Commands.CreateGroupAsync(newGroup)).Callback(() => { });
        var creator = new GroupMemberTbl{
            Id = new GroupMemberId(),
            GroupId = groupId,
            MemberId = appUserId,
            IsAdmin =true,
            MemberAt = DateTime.UtcNow,
            IsBlocked = false,
            BlockMemberInfo = null,
            AdminInfo = new AdminMemberInfo(AdminAccessLevels.Owner,startAt ,null,appUserId.Value, "Group-Creator")
        };
        _groupAdminRepo.Setup(x => x.Commands.CreateMemberAsync(creator)).Callback(() => { });
        // Act 
        var result = await _handler.Handle(request , CancellationToken.None);


        //Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage.Should().Be(new ResultMessage ("CreateGroupHandler" , "Created" , "The Group created successfully."));
    }
}
