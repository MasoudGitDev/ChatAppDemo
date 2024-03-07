using Apps.Messaging.GroupAdmins.Commands.Handlers;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupEntity.Entity;
using Domains.Messaging.GroupEntity.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;

namespace Tests.App.Messaging.GroupAdmins;
public class DeleteGroupHandlerTests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    private readonly DeleteGroupHandler _handler;

    public DeleteGroupHandlerTests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
        _handler = new(_unitOfWork.Object);
    }

    [Fact]
    public async Task JustOwner_Should_Delete_TheGroup() {

        //Arrange
        var model = new RemoveGroupModel {
            GroupId = GroupId.Create() ,
            OwnerId = Guid.NewGuid(),
        };

        var group = GroupTbl.Create(new GroupModel(){
            DisplayId = "Test-DisplayId" , Title = "Test-Title",
        });
        _unitOfWork.Setup(q => q.MemberQueries.GetGroupAsync(model.GroupId)).ReturnsAsync(group);

        var groupRequests = new List<GroupRequestTbl>(){
            GroupRequestTbl.Create(model.GroupId,Guid.NewGuid(),null)
        };
        _unitOfWork.Setup(q => q.RequestQueries
            .GetGroupRequestsAsync(model.GroupId , Shared.Enums.Visibility.Visible)).ReturnsAsync(groupRequests);

        var groupMembers = new List<GroupMemberTbl>(){
            GroupMemberTbl.Create(model.GroupId, model.OwnerId ,AdminLevel.Owner),
            GroupMemberTbl.Create(model.GroupId, Guid.NewGuid())
        };
        _unitOfWork.Setup(q => q.MemberQueries.GetMembersAsync(model.GroupId)).ReturnsAsync(groupMembers);

        var ownerMember = GroupMemberTbl.Create(model.GroupId,model.OwnerId,AdminLevel.Owner);
        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(model.GroupId , model.OwnerId)).ReturnsAsync(ownerMember);

        //Act
        var result = await _handler.Handle(model,CancellationToken.None);

        //Assert
        model.Should().NotBeNull();
        ownerMember.Should().NotBeNull();
        ownerMember.AdminInfo!.AdminLevel.Should().Be(AdminLevel.Owner);

        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , model.OwnerId) , Times.Once());
        _unitOfWork.Verify(q => q.MemberQueries.GetGroupAsync(model.GroupId) , Times.Once());
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("DeleteGroup");

        _unitOfWork.Verify(x => x.Remove(group) , Times.Once());
        _unitOfWork.Verify(x => x.RemoveRange(It.IsAny<List<GroupMemberTbl>>()) , Times.Once());
        _unitOfWork.Verify(x => x.RemoveRange(It.IsAny<List<GroupRequestTbl>>()) , Times.Once());
        _unitOfWork.Verify(x => x.SaveChangesAsync() , Times.Once());

    }


    //====================== privates
    private (UnblockMemberModel model, GroupMemberTbl admin, GroupMemberTbl targetMember) SharedArrange(
        AdminLevel adminLevel) {

        var model = new UnblockMemberModel {
            GroupId = GroupId.Create() ,
            MemberId =  Guid.NewGuid(),
            AdminId = Guid.NewGuid(),
        };

        var adminMember = GroupMemberTbl.Create(model.GroupId,model.AdminId,adminLevel);
        var targetMember = GroupMemberTbl.Create(model.GroupId,model.MemberId);

        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(adminMember.GroupId , adminMember.MemberId.Value)).ReturnsAsync(adminMember);
        _unitOfWork.Setup(q => q.MemberQueries
            .GetMemberAsync(targetMember.GroupId , targetMember.MemberId.Value)).ReturnsAsync(targetMember);

        adminMember.ToAdmin(Guid.NewGuid() , adminLevel);

        //Shared Asserts <before> calling ToNormal() method:
        model.Should().NotBeNull();
        adminMember.Should().NotBeNull();
        targetMember.Should().NotBeNull();
        adminMember.IsAdmin.Should().BeTrue();
        targetMember.IsAdmin.Should().BeFalse();
        return (model, adminMember, targetMember);
    }
}
