﻿using Apps.Messaging.GroupRequesters.Shared;
using MediatR;
using Shared.Models;

namespace Apps.Messaging.GroupRequesters.Queries.Models;  
public record GetGroupRequesterModel : IRequest<Result<GroupRequesterResult>> {
    public Guid GroupId { get; set; }
    public Guid RequesterId { get; set; }
}
