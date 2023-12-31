﻿using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.GroupRequesters.Commands.Models;
public record RemoveGroupRequesterModel : IRequest<Result> {
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public Guid RequesterId { get; set; }
}