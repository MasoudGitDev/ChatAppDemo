﻿using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Models;
public record GetGroupRequestsModel : IRequest<Result<List<GroupRequestResult>>> {
    public Guid GroupId { get; set; }
}
