using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.GroupRequesters.Commands.Models;

public record UpdateGroupRequesterModel : IRequest<Result> {
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public Guid RequesterId { get; set; }
    [Required]
    public string Description { get; set; } = String.Empty;
}
