﻿using MediatR;
using Shared.Models;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;
namespace Apps.Messaging.GroupAdmins.Commands.Models
{
    public record UpdateGroupInfoModel : IRequest<Result>
    {
        [Required]
        public EntityId GroupId { get; set; }
        [Required]
        public string DisplayId { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
