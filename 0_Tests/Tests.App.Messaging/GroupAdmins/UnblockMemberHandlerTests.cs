using Apps.Messaging.GroupAdmins.Commands.Handlers;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;

namespace Tests.App.Messaging.GroupAdmins;
public class UnblockMemberHandlerTests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    private readonly UnblockMemberHandler _handler;

    public UnblockMemberHandlerTests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
        _handler = new UnblockMemberHandler(_unitOfWork.Object);
    }

    [Theory]
    [InlineData(AdminLevel.Owner)]
    [InlineData(AdminLevel.Deputy)]
    [InlineData(AdminLevel.Trusted)]
    [InlineData(AdminLevel.Regular)]
    public async Task Admins_Should_Unblock_BlockedMembers(AdminLevel adminLevel) {

        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel);
        targetMember.Block(admin.MemberId.Value);

        //Act
        var result = await _handler.Handle(model,CancellationToken.None);

        //Assert
        targetMember.IsBlocked.Should().BeFalse();
        targetMember.BlockMemberInfo.Should().BeNull();
        _unitOfWork.Verify(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("UnblockMember");
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
