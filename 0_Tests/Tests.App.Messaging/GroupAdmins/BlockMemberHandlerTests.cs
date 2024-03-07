using Apps.Messaging.GroupAdmins.Commands.Handlers;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;
using Shared.Exceptions;

namespace Tests.App.Messaging.GroupAdmins;
public class BlockMemberHandlerTests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    private readonly BlockMemberGroupHandler _handler;
    public BlockMemberHandlerTests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
        _handler = new BlockMemberGroupHandler(_unitOfWork.Object);
    }

    [Theory]
    [InlineData(AdminLevel.Owner)]
    [InlineData(AdminLevel.Deputy)]
    [InlineData(AdminLevel.Trusted)]
    [InlineData(AdminLevel.Regular)]
    public async Task EachAdmin_ShouldNot_Block_ItSelf(AdminLevel adminLevel) {

        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel , true , adminLevel , true);

        //Act

        //Assert
        targetMember.IsAdmin.Should().BeTrue();
        admin.MemberId.Should().Be(targetMember.MemberId);
        await _handler.Invoking(x => x.Handle(model , CancellationToken.None))
            .Should().ThrowAsync<NotPossibleException>();
        _unitOfWork.Verify(x => x.SaveChangesAsync() , Times.Never);
    }

    [Theory]
    [InlineData(AdminLevel.Owner , AdminLevel.Deputy)]
    [InlineData(AdminLevel.Owner , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Owner , AdminLevel.Regular)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Regular)]
    [InlineData(AdminLevel.Trusted , AdminLevel.Regular)]
    public async Task HigherAdminsLevel_Should_Block_LowerAdminsLevel(
        AdminLevel adminLevel ,
        AdminLevel targetAdminLevel) {

        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel , false , targetAdminLevel , true);
        targetMember.IsBlocked.Should().BeFalse(); // ensure not blocked before any action

        //Act
        var result = await _handler.Handle(model , CancellationToken.None);

        //Assert        
        admin.MemberId.Should().NotBe(targetMember.MemberId);
        ( (int) adminLevel ).Should().BeGreaterThan((int) targetAdminLevel);
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("BlockMember");
        targetMember.IsBlocked.Should().BeTrue();
        _unitOfWork.Verify(x => x.SaveChangesAsync() , Times.Once());
        _unitOfWork.Verify(x=>x.MemberQueries.GetMemberAsync(model.GroupId,model.MemberId), Times.Once());
        _unitOfWork.Verify(x => x.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
    }

    [Theory]
    [InlineData(AdminLevel.Owner)]
    [InlineData(AdminLevel.Deputy)]
    [InlineData(AdminLevel.Trusted)]
    [InlineData(AdminLevel.Regular)]
    public async Task ShouldNot_Block_AnyBlockedMembers(AdminLevel adminLevel) {
        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel , false);
        targetMember.Block(admin.MemberId.Value);
        //Act 

        //Assert
        targetMember.IsAdmin.Should().BeFalse();
        admin.MemberId.Should().NotBe(targetMember.MemberId);
        targetMember.IsBlocked.Should().BeTrue();
        await _handler.Invoking(x => x.Handle(model , CancellationToken.None))
            .Should().ThrowAsync<NotPossibleException>();
        _unitOfWork.Verify(x=>x.SaveChangesAsync(), Times.Never);
    }

    // ============================ privates

    private (BlockMemberModel model, GroupMemberTbl admin, GroupMemberTbl targetMember) SharedArrange(
        AdminLevel adminLevel ,
        bool isAdminAndMemberEqual = false ,
        AdminLevel targetAdminLevel = AdminLevel.Regular ,
        bool isTargetMemberAdmin = false) {

        var sameId = Guid.NewGuid();
        var model = new BlockMemberModel {
            GroupId = GroupId.Create() ,
            MemberId = sameId,
            AdminId = isAdminAndMemberEqual ?  sameId : Guid.NewGuid(), // check isAdminAndMemberEqual            
        };

        var adminMember = GroupMemberTbl.Create(model.GroupId,model.AdminId,adminLevel);
        var targetMember = GroupMemberTbl.Create(model.GroupId,model.MemberId);

        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(adminMember.GroupId , adminMember.MemberId.Value)).ReturnsAsync(adminMember);
        _unitOfWork.Setup(q => q.MemberQueries
            .GetMemberAsync(targetMember.GroupId , targetMember.MemberId.Value)).ReturnsAsync(targetMember);

        adminMember.ToAdmin(Guid.NewGuid() , adminLevel);
        if(isTargetMemberAdmin) {
            targetMember.ToAdmin(Guid.NewGuid() , targetAdminLevel);
        }
        else {
            targetMember.ToNormal();
        }

        //Shared Asserts <before> calling ToNormal() method:
        model.Should().NotBeNull();
        adminMember.Should().NotBeNull();
        targetMember.Should().NotBeNull();
        adminMember.IsAdmin.Should().BeTrue();
        adminMember.AdminInfo!.AdminLevel.Should().Be(adminLevel);
        if(isTargetMemberAdmin) {
            targetMember.IsAdmin.Should().BeTrue();
            targetMember.AdminInfo!.AdminLevel.Should().Be(targetAdminLevel);
        }
        return (model, adminMember, targetMember);
    }

}
