using Apps.Messaging.Group.Commands.Handlers;
using Apps.Messaging.Group.Commands.Models;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
namespace Tests.App.Messaging.Group;
public class Group_Commands_Tests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;

    public Group_Commands_Tests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
    }

    [Fact]
    public async Task CreateGroupHandler_Should_CreateGroup_Successfully() {

        //Arrange
        DisplayId displayId = "TestGroup";
        var model = new CreateGroupModel(){
            DisplayId = displayId,
            Title = "Title-Test_Group",
            CreatorId = Guid.NewGuid(),
            Description = "new Description" ,
            IsRequestable = true
        };
        GroupTbl? findGroup = null;
        _unitOfWork.Setup(x => x.MemberQueries.GetGroupByDisplayIdAsync(displayId)).Returns(Task.FromResult(findGroup));
        var handler = new CreateGroupHandler(_unitOfWork.Object);

        //Act

        var result = await handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(x => x.MemberQueries.GetGroupByDisplayIdAsync(displayId) , Times.Once());
        findGroup.Should().BeNull();

        _unitOfWork.Verify(x => x.CreateAsync(It.IsAny<GroupTbl>()) , Times.Once);
        _unitOfWork.Verify(x => x.CreateAsync(
            It.Is<GroupMemberTbl>(g => g.IsAdmin && g.AdminInfo!.AdminLevel == AdminLevel.Owner)) ,
            Times.Once);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);

        await handler.Invoking(x => x.Handle(model , CancellationToken.None))
            .Should().NotThrowAsync<Exception>();

        result.Should().NotBeNull();
        result?.Status.Should().Be(ResultStatus.Success);
        result?.ResultMessage?.Code.Should().Be("Created");

    }

    [Fact]
    public async Task LeaveGroupHandler_Should_LeaveGroup_Successfully() {
        //Arrange 
        LeaveGroupModel model = new(){GroupId = Guid.NewGuid(), MemberId = Guid.NewGuid()};
        GroupMemberTbl findMember = GroupMemberTbl.Create(model.GroupId, model.MemberId ,AdminLevel.Regular);

        _unitOfWork.Setup(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId))
            .Returns(Task.FromResult(findMember)!);

        var handler = new LeaveGroupHandler(_unitOfWork.Object);

        //Act
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once);
        findMember.Should().NotBeNull();
        findMember.AdminInfo?.AdminLevel.Should().NotBe(AdminLevel.Owner);        

        _unitOfWork.Verify(c => c.Remove(findMember) , Times.Once);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);
        await handler.Invoking(m => m.Handle(model , CancellationToken.None))
            .Should().NotThrowAsync<Exception>();
    }
}
