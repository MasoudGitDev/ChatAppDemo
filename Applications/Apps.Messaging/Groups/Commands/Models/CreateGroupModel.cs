using MediatR;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Apps.Messaging.Groups.Commands.Models;
public record CreateGroupModel : IRequest<Result>
{

    [Required]
    public Guid CreatorId { get; set; } = Guid.Empty;
    [Required]
    public string DisplayId { get; set; } = string.Empty;
    [Required]
    public string Title { get; set; } = String.Empty;

    public string? Logo { get; set; }
    public string? Description { get; set; }
    public string? Categories { get; set; } = "Others";
    public bool IsRequestable { get; set; } = false;
}
