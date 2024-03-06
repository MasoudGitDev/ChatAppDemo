using Apps.Messaging.GroupAdmins.Commands.Handlers;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.Shared.ValueObjects;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;

namespace Tests.App.Messaging.GroupAdmins;
public class ToNormalHandlerTests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    private readonly ToNormalMemberHandler _handler;

    public ToNormalHandlerTests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
        _handler = new ToNormalMemberHandler(_unitOfWork.Object);
    }

    [Theory]
    [InlineData(AdminLevel.Trusted , AdminLevel.Regular)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Regular)]
    [InlineData(AdminLevel.Owner , AdminLevel.Deputy)]
    [InlineData(AdminLevel.Owner , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Owner , AdminLevel.Regular)]
    public async Task HigherAdminsLevels_Should_ToNormal_LowerAdminsLevels(
         AdminLevel adminLevel , AdminLevel targetAdminLevel
        ) {

        //Arrange
        var (model, adminMember, adminTargetMember) = SharedArrange(adminLevel , targetAdminLevel , false);

        //Act
        var result = await _handler.Handle(model , CancellationToken.None);

        //Assert
        ( (int) adminLevel ).Should().BeGreaterThan((int) targetAdminLevel);
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , It.IsAny<AppUserId>()) , Times.Exactly(2));
        adminTargetMember.AdminInfo.Should().BeNull();
        adminTargetMember.IsAdmin.Should().BeFalse();
        adminTargetMember.MemberId.Should().NotBe(adminMember.MemberId); // Not Same Id
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once());
        result.Should().NotBeNull();
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("ToNormalMember");

    }

    [Theory]
    [InlineData(AdminLevel.Deputy)]
    [InlineData(AdminLevel.Trusted)]
    [InlineData(AdminLevel.Regular)]
    public async Task AdminsExceptOwner_Should_ToNormal_ItSelf(AdminLevel adminLevel) {

        //Arrange
        var (model, adminMember, adminTargetMember) = SharedArrange(adminLevel , adminLevel , true);

        //Act
        var result = await _handler.Handle(model , CancellationToken.None);

        //Assert
        adminMember.Should().NotBe(AdminLevel.Owner);
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , It.IsAny<AppUserId>()) , Times.Exactly(2));
        adminTargetMember.AdminInfo.Should().BeNull();
        adminTargetMember.IsAdmin.Should().BeFalse();
        adminTargetMember.MemberId.Should().Be(adminMember.MemberId); // Same Id
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once());
        result.Should().NotBeNull();
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("ToNormalMember");

    }

    [Fact]
    public async Task Owner_Should_ToNormal_ItSelf_WhenDeputyIsExist() {

        //Arrange
        var (model, owner, adminTargetMember) = SharedArrange(AdminLevel.Owner , AdminLevel.Owner , true);
        var deputyMember = GroupMemberTbl.Create(GroupId.Create() , Guid.NewGuid());
        deputyMember.ToAdmin(owner.MemberId.Value , AdminLevel.Deputy);
        _unitOfWork.Setup(q => q.MemberQueries.GetDeputyAdminAsync(model.GroupId)).ReturnsAsync(deputyMember);
        deputyMember.AdminInfo!.AdminLevel.Should().Be(AdminLevel.Deputy);

        //Act
        var result = await _handler.Handle(model , CancellationToken.None);

        //Assert
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , It.IsAny<AppUserId>()) , Times.Exactly(2));
        adminTargetMember.AdminInfo.Should().BeNull();
        adminTargetMember.IsAdmin.Should().BeFalse();
        adminTargetMember.MemberId.Should().Be(owner.MemberId); // Same Id
        deputyMember.AdminInfo!.AdminLevel.Should().Be(AdminLevel.Owner);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once());
        result.Should().NotBeNull();
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("ToNormalMember");

    }


    //====================== privates
    private (ToNormalMemberModel model, GroupMemberTbl admin, GroupMemberTbl targetMember) SharedArrange(
     AdminLevel adminLevel ,
     AdminLevel targetAdminLevel ,
     bool isAdminAndMemberEqual = false) {

        var sameId = Guid.NewGuid();
        var model = new ToNormalMemberModel {
            GroupId = GroupId.Create() ,
            MemberId = sameId,
            AdminId = isAdminAndMemberEqual ?  sameId : Guid.NewGuid(), // check isAdminAndMemberEqual            
        };

        var adminMember = GroupMemberTbl.Create(model.GroupId,model.AdminId,adminLevel);
        var targetMember = GroupMemberTbl.Create(model.GroupId,model.MemberId);

        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(adminMember.GroupId , adminMember.MemberId.Value)).ReturnsAsync(adminMember);
        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(targetMember.GroupId , targetMember.MemberId.Value)).ReturnsAsync(targetMember);

        adminMember.ToAdmin(Guid.NewGuid() , adminLevel);
        targetMember.ToAdmin(Guid.NewGuid() , targetAdminLevel);

        //Shared Asserts <before> calling ToNormal() method:
        model.Should().NotBeNull();
        adminMember.Should().NotBeNull();
        targetMember.Should().NotBeNull();
        adminMember.IsAdmin.Should().BeTrue();
        targetMember.IsAdmin.Should().BeTrue();
        adminMember.AdminInfo!.AdminLevel.Should().Be(adminLevel);
        targetMember.AdminInfo!.AdminLevel.Should().Be(targetAdminLevel);
        return (model, adminMember, targetMember);
    }

}
