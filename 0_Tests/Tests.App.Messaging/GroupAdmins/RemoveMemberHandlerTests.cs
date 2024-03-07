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
public class RemoveMemberHandlerTests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    private readonly RemoveMemberHandler _handler;
    public RemoveMemberHandlerTests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
        _handler = new (_unitOfWork.Object);
    }

    /// <summary>
    ///  others = Other Admins or normal members
    /// </summary>
    [Theory]
    [InlineData(AdminLevel.Owner)]
    [InlineData(AdminLevel.Deputy)]
    public async Task JustOwnerOrDeputy_Should_Remove_Others(AdminLevel adminLevel) {

        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel , false);

        //Act
        var result = await _handler.Handle(model , CancellationToken.None);

        //Assert
        admin.AdminInfo!.AdminLevel.Should().Be(adminLevel);
        ( (int) adminLevel ).Should().BeOneOf((int)AdminLevel.Deputy,(int)AdminLevel.Owner);
        admin.MemberId.Should().NotBe(targetMember.Id);
        result.Status.Should().Be(Shared.Enums.ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("RemoveMember");

        _unitOfWork.Verify(x => x.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(x => x.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
        _unitOfWork.Verify(x=>x.SaveChangesAsync(), Times.Once());
    }

    [Theory]
    [InlineData(AdminLevel.Owner)]
    [InlineData(AdminLevel.Deputy)]
    public async Task OwnerOrDeputy_ShouldNot_Remove_Itself(AdminLevel adminLevel) {

        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel , true , adminLevel , true);

        //Act

        //Assert
        admin.AdminInfo!.AdminLevel.Should().Be(adminLevel);
        admin.MemberId.Should().Be(targetMember.MemberId); // Same Id
        await _handler.Invoking(x=>x.Handle(model , CancellationToken.None))
            .Should().ThrowAsync<NotPossibleException>();
        _unitOfWork.Verify(x => x.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(x => x.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
        _unitOfWork.Verify(x => x.SaveChangesAsync() , Times.Never);
    }

    [Theory]
    [InlineData(AdminLevel.Deputy , AdminLevel.Owner)]
    public async Task LowerLevelAdmins_ShouldNot_Remove_HigherLevelAdmins(
        AdminLevel adminLevel , AdminLevel targetAdminLevel) {

        //Arrange
        var (model, admin, targetMember) = SharedArrange(adminLevel , false , targetAdminLevel , true);

        //Act

        //Assert
        admin.AdminInfo!.AdminLevel.Should().Be(adminLevel);
        admin.MemberId.Should().NotBe(targetMember.MemberId); //must not Same Id
        ( (int) adminLevel ).Should().NotBeInRange(0 , 1);
        ( (int) adminLevel).Should().BeLessThanOrEqualTo((int)targetAdminLevel);
        await _handler.Invoking(x => x.Handle(model , CancellationToken.None))
            .Should().ThrowAsync<NotPossibleException>();
        _unitOfWork.Verify(x => x.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(x => x.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
        _unitOfWork.Verify(x => x.SaveChangesAsync() , Times.Never);
    }


    // ==================================================== private methods

    private (RemoveMemberModel model, GroupMemberTbl admin, GroupMemberTbl targetMember) SharedArrange(
        AdminLevel adminLevel ,
        bool isAdminAndMemberEqual = false ,
        AdminLevel targetAdminLevel = AdminLevel.Regular ,
        bool isTargetMemberAdmin = false) {

        var sameId = Guid.NewGuid();
        var model = new RemoveMemberModel {
            GroupId = GroupId.Create() ,
            MemberId = sameId,
            AdminId = isAdminAndMemberEqual ?  sameId : Guid.NewGuid(), // check isAdminAndMemberEqual            
        };

        var adminMember = GroupMemberTbl.Create(model.GroupId,model.AdminId,adminLevel);
        var targetMember = GroupMemberTbl.Create(model.GroupId,model.MemberId);

        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(model.GroupId , model.AdminId)).ReturnsAsync(adminMember);

        _unitOfWork.Setup(q => q.MemberQueries
            .GetMemberAsync(model.GroupId , model.MemberId)).ReturnsAsync(targetMember);

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
