using Apps.Messaging.GroupRequests.Queries.Handlers;
using Apps.Messaging.GroupRequests.Queries.Models;
using Apps.Messaging.GroupRequests.Shared;
using Domains.Messaging.GroupEntity.ValueObjects;
using Domains.Messaging.GroupRequestEntity;
using Domains.Messaging.UnitOfWorks;
using FluentAssertions;
using Moq;
using Shared.Enums;

namespace Tests.App.Messaging.GroupRequest;
public class GroupRequest_Queries_Tests {

    private readonly Mock<IGroupMessagingUOW> _unitOfWork;
    public GroupRequest_Queries_Tests() {
        _unitOfWork = new Mock<IGroupMessagingUOW>();
    }

    [Fact]
    public async Task GetGroupRequestsHandler_Should_Return_GroupRequestResults_Result() {
        //Arrange
        var model = new GetGroupRequestsModel{
            GroupId = GroupId.Create()
        };
        var groupRequests = new List<GroupRequestTbl>();
        _unitOfWork.Setup(q => q.RequestQueries.GetGroupRequestsAsync(model.GroupId,Visibility.Visible))
            .Returns(Task.FromResult(groupRequests));
        var handler =  new GetGroupRequestsHandler(_unitOfWork.Object);

        //Act
        var result = await handler.Handle(model,CancellationToken.None);

        //Assert
        groupRequests.Should().NotBeNull();
        _unitOfWork.Verify(q => q.RequestQueries.GetGroupRequestsAsync(model.GroupId, Visibility.Visible) , Times.Once);
        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage.Should().NotBeNull();
        result.ResultMessage!.Code.Should().Be("GroupRequests");
        result.Content.Should().BeOfType<List<GroupRequestResult>>().And.NotBeNull();
        await handler.Invoking(c => c.Handle(model , CancellationToken.None))
         .Should().NotThrowAsync<Exception>();

    }

    [Fact]
    public async Task GetUserRequestsHandler_Should_Return_UserRequestResults_Result() {
        //Arrange
        var model = new GetUserRequestsModel{
            RequesterId = Guid.NewGuid()
        };
        var userRequests = new List<GroupRequestTbl>();
        _unitOfWork.Setup(q => q.RequestQueries.GetUserRequestsAsync(model.RequesterId, Visibility.Visible))
            .Returns(Task.FromResult(userRequests));
        var handler =  new GetUserRequestsHandler(_unitOfWork.Object);

        //Act
        var result = await handler.Handle(model,CancellationToken.None);

        //Assert
        userRequests.Should().NotBeNull();
        _unitOfWork.Verify(q => q.RequestQueries.GetUserRequestsAsync(model.RequesterId, Visibility.Visible) , Times.Once);
        result.Status.Should().Be(ResultStatus.Success);
        result.ResultMessage.Should().NotBeNull();
        result.ResultMessage!.Code.Should().Be("UserRequests");
        result.Content.Should().BeOfType<List<GroupRequestResult>>().And.NotBeNull();
        await handler.Invoking(c => c.Handle(model , CancellationToken.None))
            .Should().NotThrowAsync<Exception>();
    }

}
