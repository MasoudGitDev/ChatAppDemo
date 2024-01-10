using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.GroupRequests.Commands.Models;

public record UpdateGroupRequestModel : IRequest<Result> {
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public Guid RequesterId { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
}
