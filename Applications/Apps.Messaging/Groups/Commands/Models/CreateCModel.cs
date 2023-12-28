using MediatR;
using Shared.Models;
using Shared.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.Groups.Commands.Models;
public record CreateCModel : IRequest<Result>
{

    [Required]
    public EntityId CreatorId { get; set; }
    [Required]
    public string DisplayId { get; set; } = string.Empty;

    public string? Picture { get; set; }
    public string? Description { get; set; }
    // public string? Categories { get; set; }
}
