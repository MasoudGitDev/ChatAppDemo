using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.Groups.Commands.Models;
public record CreateCModel : IRequest<Result>
{

    [Required]
    public Guid CreatorId { get; set; } = Guid.Empty;
    [Required]
    public string DisplayId { get; set; } = string.Empty;

    public string? Picture { get; set; }
    public string? Description { get; set; }
    // public string? Categories { get; set; }
}
