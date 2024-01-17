﻿using Apps.Messaging.GroupRequests.Shared;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupRequests.Queries.Models;

public record GetUserRequestsModel : IRequest<Result<List<GroupRequestsResult>>> {
    public Guid RequesterId { get; set; }
}