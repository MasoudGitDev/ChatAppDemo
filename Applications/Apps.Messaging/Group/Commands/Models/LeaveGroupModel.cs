﻿using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.Group.Commands.Models;
public record LeaveGroupModel : IRequest<Result>
{
    [Required]
    public Guid GroupId { get; set; }

    [Required]
    public Guid MemberId { get; set; }
}
