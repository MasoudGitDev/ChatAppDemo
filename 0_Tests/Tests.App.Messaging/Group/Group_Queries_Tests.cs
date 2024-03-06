using Apps.Messaging.Group.Queries.Handlers;
using Apps.Messaging.Group.Queries.Models;
using Apps.Messaging.Shared.ResultModels;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.Models;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.DTOs.Group;
using Shared.Enums;
namespace Tests.App.Messaging.Group;
public class Group_Queries_Tests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;

    public Group_Queries_Tests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
    }

    [Fact]
    public async Task FindGroupByDisplayIdHandler_Should_Return_GroupResultModel_Result() {
        // Arrange 
        var model = new FindGroupByDisplayIdModel(){ DisplayId = "TestDisplayId" };
        GroupTbl findGroup = (new GroupTbl()).ChangeDisplayId(model.DisplayId);
        _unitOfWork.Setup(q => q.MemberQueries.GetGroupByDisplayIdAsync(model.DisplayId)).Returns(Task.FromResult(findGroup)!);
        var handler = new FindGroupByDisplayIdHandler(_unitOfWork.Object);

        //Act 
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(q => q.MemberQueries.GetGroupByDisplayIdAsync(model.DisplayId) , Times.Once());
        findGroup.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        result.Content.Should().BeOfType<GroupResultModel>().And.NotBeNull();
        await handler.Invoking(x => x.Handle(model , CancellationToken.None)).Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task FindGroupByTitleHandler_Should_Return_GroupResultModels_Result() {
        // Arrange 
        var model = new FindGroupsByTitleModel {Title = "Test-Title"};
        var findGroups = new List<GroupTbl>();
        _unitOfWork.Setup(q => q.MemberQueries.FindGroupsByTitleAsync(It.IsAny<string>())).Returns(Task.FromResult(findGroups));
        var handler = new FindGroupsByTitleHandler(_unitOfWork.Object);

        //Act 
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(q => q.MemberQueries.FindGroupsByTitleAsync(It.IsAny<string>()) , Times.Once());
        findGroups.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        result.Content.Should().BeOfType<List<GroupResultModel>>().And.NotBeNull();
        await handler.Invoking(x => x.Handle(model , CancellationToken.None)).Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetGroupMembersHandler_Should_Return_GroupMembers_Result() {
        // Arrange 
        var model = new GetGroupMembersModel {GroupId = GroupId.Create()};
        var findMembers = new List<GroupMemberTbl>();
        _unitOfWork.Setup(q => q.MemberQueries.GetMembersAsync(It.IsAny<GroupId>())).Returns(Task.FromResult(findMembers));
        var handler = new GetGroupMembersHandler(_unitOfWork.Object);

        //Act 
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(q => q.MemberQueries.GetMembersAsync(It.IsAny<GroupId>()) , Times.Once());
        findMembers.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        result.Content.Should().BeOfType<List<MemberInfo>>().And.NotBeNull();
        await handler.Invoking(x => x.Handle(model , CancellationToken.None)).Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetUserGroupsHandler_Should_Return_UserGroups() {
        // Arrange 
        var model = new GetUserGroupsModel { AppUserId = Guid.NewGuid()};
        var userGroups = new List<GroupTbl>();
        _unitOfWork.Setup(q => q.MemberQueries.GetUserGroupsAsync(It.IsAny<AppUserId>())).Returns(Task.FromResult(userGroups));
        var handler = new GetUserGroupsHandler(_unitOfWork.Object);

        //Act 
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(q => q.MemberQueries.GetUserGroupsAsync(It.IsAny<AppUserId>()) , Times.Once());
        userGroups.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        result.Content.Should().BeOfType<LinkedList<GroupResultDto>>().And.NotBeNull();
        await handler.Invoking(x => x.Handle(model , CancellationToken.None)).Should().NotThrowAsync<Exception>();
    }

}
