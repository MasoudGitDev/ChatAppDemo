using Apps.Messaging.GroupRequests.Commands.Handlers;
using Apps.Messaging.GroupRequests.Commands.Models;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupMemberEntity.Entity;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.UnitOfWorks;
using Elasticsearch.Net;
using FluentAssertions;
using Moq;
using Shared.Enums;
using Shared.Exceptions;

namespace Tests.App.Messaging.GroupRequest;
public class GroupRequest_Commands_Tests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    public GroupRequest_Commands_Tests()
    {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
    }



    [Fact]
    public async Task RequestMembershipHandler_Should_CreateRequest() {
        //Arrange   
        (
            RequestMembershipModel model ,
            GroupMemberTbl? member,
            GroupRequestTbl? request,
            RequestMembershipHandler handler
        ) 
            = FakeDataForRequestMemberShip(true , true);

        var createRequest = GroupRequestTbl.Create(model.GroupId,model.UserId,model.Description);
        _unitOfWork.Setup(c => c.CreateAsync(It.IsAny<GroupRequestTbl>()))
            .Callback<GroupRequestTbl>(request => { createRequest = request; });

        //Act
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert

        member.Should().BeNull();
        request.Should().BeNull();
        createRequest.Should().NotBeNull();

        _unitOfWork.Verify(c => c.CreateAsync(createRequest) , Times.Once);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);

        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage.Should().NotBeNull();
        result.ResultMessage?.Code.Should().Be("Create");

       await handler.Invoking(c => c.Handle(model , CancellationToken.None))
            .Should().NotThrowAsync<Exception>();

    }

    [Fact]
    public async Task RequestMembershipHandler_Should_UpdateRequest_Successfully() {
        //Arrange   
        (
            RequestMembershipModel model,
            GroupMemberTbl? member,
            GroupRequestTbl? request  ,
            RequestMembershipHandler handler
        )
            = FakeDataForRequestMemberShip(false , true);
        request!.ChangeBlockState(false);

        //Act
        var result = await handler.Handle(model , CancellationToken.None);

        //Assert

        member.Should().BeNull();
        request.Should().NotBeNull();
        request!.IsBlocked.Should().BeFalse();

        _unitOfWork.Verify(c => c.CreateAsync(It.IsAny<GroupRequestTbl>()) , Times.Exactly(0));
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);

        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage.Should().NotBeNull();
        result.ResultMessage?.Code.Should().Be("Update");

        await handler.Invoking(c => c.Handle(model , CancellationToken.None))
             .Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task RequestMembershipHandler_ShouldNot_UpdateTheRequest() {
        //Arrange   
        (
            RequestMembershipModel model,
            GroupMemberTbl? member,
            GroupRequestTbl? request,
            RequestMembershipHandler handler
        )
            = FakeDataForRequestMemberShip(false , true);
        request?.ChangeBlockState(true);

        //Act
        await handler.Invoking(c => c.Handle(model , CancellationToken.None))
             .Should().ThrowAsync<NotPossibleException>();

        //Assert

        member.Should().BeNull();
        request.Should().NotBeNull();
        request!.IsBlocked.Should().BeTrue();

        _unitOfWork.Verify(c => c.CreateAsync(It.IsAny<GroupRequestTbl>()) , Times.Never);
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Never);
        
    }


    [Fact]
    public async Task RemoveRequestHandler_Should_DeleteRequestCompletely() {

        //Arrange
        (
            RemoveRequestModel model,
            GroupRequestTbl? request,
            RemoveRequestHandler handler
        ) = FakeDataForRemove();

        //Act
        var result = await handler.Handle(model, CancellationToken.None);

        //Assert
        request.Should().NotBeNull();
        request!.IsBlocked.Should().BeFalse();
        _unitOfWork.Verify(c => c.Remove(request) , Times.Once);
        result.ResultMessage!.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("Delete");
        _unitOfWork.Verify(c=>c.SaveChangesAsync(), Times.Once);
        await handler.Invoking(c => c.Handle(model , CancellationToken.None))
         .Should().NotThrowAsync<Exception>();
    }

    [Fact]
    public async Task RemoveRequestHandler_Should_HideRequest() {

        //Arrange
        (
            RemoveRequestModel model,
            GroupRequestTbl? request,
            RemoveRequestHandler handler
        ) = FakeDataForRemove();
        request?.ChangeBlockState(true);

        //Act
        var result = await handler.Handle(model, CancellationToken.None);

        //Assert
        request.Should().NotBeNull();
        request!.IsBlocked.Should().BeTrue();
        request.Visibility.showToAdmins.Should().Be(Visibility.Hidden);
        request.Visibility.showToRequester.Should().Be(Visibility.Hidden);
        _unitOfWork.Verify(c => c.Remove(request) , Times.Never);
        result.ResultMessage!.Should().NotBeNull();
        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage?.Code.Should().Be("Warning");
        _unitOfWork.Verify(c => c.SaveChangesAsync() , Times.Once);
        await handler.Invoking(c => c.Handle(model , CancellationToken.None))
         .Should().NotThrowAsync<Exception>();
    }


    // Fake Data
    private (
        RequestMembershipModel ,
        GroupMemberTbl?, 
        GroupRequestTbl? ,
        RequestMembershipHandler
        ) FakeDataForRequestMemberShip(bool isRequestNull = false , bool isMemberNull = true) {
        var model = new RequestMembershipModel{
            GroupId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Description = "Hi"
        };
        var member = isMemberNull ? null : new GroupMemberTbl();
        _unitOfWork.Setup(q => q.MemberQueries.GetMemberAsync(model.GroupId , model.UserId)).ReturnsAsync(member);

        var request = isRequestNull ? null : new GroupRequestTbl();
        _unitOfWork.Setup(q => q.RequestQueries.GetRequestAsync(model.GroupId , model.UserId)).ReturnsAsync(request);
        var handler = new RequestMembershipHandler(_unitOfWork.Object);
        return (model , member, request,handler);
    }

    private (RemoveRequestModel, GroupRequestTbl?, RemoveRequestHandler) FakeDataForRemove() {
        var model = new RemoveRequestModel(){
            GroupId = GroupId.Create() ,
            RequesterId = Guid.NewGuid()
        };
        var request = new GroupRequestTbl();
        _unitOfWork.Setup(q => q.RequestQueries.GetRequestAsync(model.GroupId , model.RequesterId)).ReturnsAsync(request);
        var handler = new RemoveRequestHandler(_unitOfWork.Object);

        return(model , request ,handler);
    }
}
