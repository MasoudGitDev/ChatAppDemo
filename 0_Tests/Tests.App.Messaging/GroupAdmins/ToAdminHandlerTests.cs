using Apps.Messaging.GroupAdmins.Commands.Handlers;
using Apps.Messaging.GroupAdmins.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Abstractions.Messaging.Constants;
using Shared.Enums;
using Shared.Exceptions;

namespace Tests.App.Messaging.GroupAdmins;
public class ToAdminHandlerTests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    private readonly ToAdminMemberHandler _handler;
    public ToAdminHandlerTests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
        _handler = new ToAdminMemberHandler(_unitOfWork.Object);
    }

    /// <summary>
    /// Each admins Should Not Make Any One or itself To Same admin level or higher!
    /// </summary>
    [Theory]
    [InlineData(AdminLevel.Regular , AdminLevel.Regular)]
    [InlineData(AdminLevel.Regular , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Regular , AdminLevel.Deputy)]
    [InlineData(AdminLevel.Regular , AdminLevel.Owner)]

    [InlineData(AdminLevel.Trusted , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Trusted , AdminLevel.Deputy)]
    [InlineData(AdminLevel.Trusted , AdminLevel.Owner)]

    [InlineData(AdminLevel.Deputy , AdminLevel.Deputy)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Owner)]

    [InlineData(AdminLevel.Owner , AdminLevel.Owner)]
    public async Task ShouldNotPass_When_LevelToAssign_EqualOrGreaterThan_AdminLevel(
        AdminLevel adminLevel , AdminLevel levelToAssign
        ) {

        var (model, adminMember, targetMember) = SharedArrange(adminLevel , levelToAssign , true);

        //Act 


        //Assert
        model.Should().NotBeNull();
        adminMember.IsAdmin.Should().BeTrue();
        await _handler.Invoking(c => c.Handle(model , CancellationToken.None))
           .Should().ThrowAsync<NotPossibleException>();

        _unitOfWork.Verify(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());

        ( (int) levelToAssign ).Should().BeGreaterThanOrEqualTo((int) adminLevel);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Never());
    }


    #region Admins_ShouldMake_Itself_ToLowerLevel

    [Theory]
    [InlineData(AdminLevel.Trusted , AdminLevel.Regular)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Trusted)]
    [InlineData(AdminLevel.Deputy , AdminLevel.Regular)]
    public async Task EachAdminsExceptOwner_Should_Decrease_ItsAdminLevel(
         AdminLevel adminLevel , AdminLevel levelToAssign) {

        //Arrange
        var (model, adminMember, targetMember) = SharedArrange(adminLevel , levelToAssign , true);
        targetMember.AdminInfo?.AdminLevel.Should().NotBe(levelToAssign); // must be check before any action


        //Act 
        var result = await _handler.Handle(model, CancellationToken.None);

        //Assert
        model.Should().NotBeNull();
        result.Should().NotBeNull();
        _unitOfWork.Verify(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
        ( (int) levelToAssign ).Should().BeLessThan((int) adminLevel);
        adminMember.IsAdmin.Should().BeTrue();

        adminMember.MemberId.Should().Be(targetMember.MemberId); // must same Id
        targetMember.IsAdmin.Should().BeTrue();
        adminMember.AdminInfo?.AdminLevel.Should().NotBe(AdminLevel.Owner);
        adminMember.AdminInfo?.AdminLevel.Should().NotBe(AdminLevel.Regular);
        targetMember.AdminInfo?.AdminLevel.Should().Be(levelToAssign);

        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once());
        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage!.Code.Should().Be("ToAdminMember");
    }

    //===================== Owner Should  ...  

    [Theory]
    [InlineData(AdminLevel.Regular)]
    [InlineData(AdminLevel.Trusted)]
    [InlineData(AdminLevel.Deputy)]
    public async Task Owner_ShouldMake_Itself_To_LowerAdminLevel_When_DeputyExist(AdminLevel levelToAssign) {
        //Arrange
        var ownerLevel = AdminLevel.Owner;
        var (model, adminMember, targetMember) = SharedArrange(ownerLevel , levelToAssign , true);

        var deputyMember = GroupMemberTbl.Create(model.GroupId,Guid.NewGuid());
        deputyMember.ToAdmin(adminMember.MemberId.Value , AdminLevel.Deputy);
        _unitOfWork.Setup(q => q.MemberQueries.GetDeputyAdminAsync(model.GroupId)).ReturnsAsync(deputyMember);
        deputyMember.AdminInfo?.AdminLevel.Should().Be(AdminLevel.Deputy);
        adminMember.AdminInfo?.AdminLevel.Should().Be(AdminLevel.Owner);


        //Act
        var result = await _handler.Handle(model,CancellationToken.None);

        //Assert        
        SharedAssert(new ParamData(adminMember , targetMember , ownerLevel , model , true , true));
        _unitOfWork.Verify(q => q.MemberQueries.GetDeputyAdminAsync(model.GroupId) , Times.Once);
        deputyMember.Should().NotBeNull();
        deputyMember.AdminInfo.Should().NotBeNull();
        deputyMember.AdminInfo?.AdminLevel.Should().Be(AdminLevel.Owner); // After change level
        adminMember.AdminInfo?.AdminLevel.Should().Be(levelToAssign); // After change level
        result.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);
    }

    #endregion

    [Theory]
    [InlineData(AdminLevel.Deputy , AdminLevel.Trusted , AdminLevel.Regular)]
    public async Task EachAdminExceptRegular_ShouldMake_OtherAdminsToValidLevel(
        AdminLevel adminLevel , AdminLevel levelToAssign , AdminLevel targetAdminLevel
        ) {

        //Arrange
        var (model, adminMember, targetMember) = SharedArrange(adminLevel , levelToAssign , false);
        targetMember.ToAdmin(adminMember.MemberId.Value , targetAdminLevel);
        targetMember.AdminInfo!.AdminLevel.Should().NotBe(levelToAssign); // before any call method

        //Act
        var result = await _handler.Handle(model,CancellationToken.None);

        //Assert
        model.Should().NotBeNull();
        adminMember.Should().NotBeNull();
        targetMember.Should().NotBeNull();
        adminMember.IsAdmin.Should().BeTrue();

        targetMember.IsAdmin.Should().BeTrue();
        adminMember.MemberId.Value.Should().NotBe(targetMember.MemberId.Value); // Other admin
        ( (int) levelToAssign ).Should().BeLessThan((int) adminLevel);
        ( (int) targetMember.AdminInfo!.AdminLevel ).Should().BeLessThan((int) adminLevel); // important
        adminMember.AdminInfo!.AdminLevel.Should().NotBe(AdminLevel.Regular);
        targetAdminLevel.Should().NotBe(levelToAssign);
        ( (int) targetAdminLevel ).Should().BeLessThan((int) adminLevel);
        targetMember.AdminInfo!.AdminLevel.Should().Be(levelToAssign);

        result.Status.Should().Be(ResultStatus.Success);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);
    }



    // ==================================================== private methods


    private (ToAdminMemberModel model, GroupMemberTbl admin, GroupMemberTbl targetMember) SharedArrange(
        AdminLevel adminLevel ,
        AdminLevel levelToAssign ,
        bool isAdminAndMemberEqual = false) {

        var sameId = Guid.NewGuid();
        var model = new ToAdminMemberModel {
            GroupId = GroupId.Create() ,
            MemberId = sameId,
            AdminId = isAdminAndMemberEqual ?  sameId : Guid.NewGuid(), // check isAdminAndMemberEqual
            StartAt = DateTime.Now ,
            EndAt = null ,
            Reason = "" ,
            LevelToAssign = levelToAssign ,
        };

        var targetMember = GroupMemberTbl.Create(model.GroupId,model.MemberId);
        _unitOfWork.Setup(q => q.MemberQueries
            .GetMemberAsync(targetMember.GroupId , targetMember.MemberId.Value)).ReturnsAsync(targetMember);

        var adminMember = GroupMemberTbl.Create(model.GroupId,model.AdminId,adminLevel);
        adminMember.ToAdmin(Guid.NewGuid() , adminLevel);
        _unitOfWork.Setup(q => q.MemberQueries
            .GetAdminMemberAsync(adminMember.GroupId , adminMember.MemberId.Value)).ReturnsAsync(adminMember);

        if(isAdminAndMemberEqual) {
            targetMember.ToAdmin(Guid.NewGuid() , adminLevel);
        }
        return (model, adminMember, targetMember);
    }

    private void SharedAssert(ParamData data) {
        var (admin, targetMember, adminLevel, model, isAdminAndMemberEqual, ownerWantToDecreaseLevel) = data;
        model.Should().NotBeNull();
        _unitOfWork.Verify(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.MemberId) , Times.Once());
        _unitOfWork.Verify(q => q.MemberQueries.GetAdminMemberAsync(model.GroupId , model.AdminId) , Times.Once());
        admin.Should().NotBeNull();
        targetMember.Should().NotBeNull();
        if(!ownerWantToDecreaseLevel) {
            admin.AdminInfo!.AdminLevel.Should().Be(adminLevel);
        }
        if(isAdminAndMemberEqual) {
            targetMember.IsAdmin.Should().BeTrue();
            targetMember.MemberId.Should().Be(admin.MemberId); // important
        }
        else {
            targetMember.MemberId.Should().NotBe(admin.MemberId); // important
        }

    }

    internal record ParamData(
        GroupMemberTbl Admin ,
        GroupMemberTbl Member ,
        AdminLevel AdminLevel ,
        ToAdminMemberModel Model ,
        bool IsAdminAndMemberEqual ,
        bool ownerWantToDecreaseLevel = false);
}